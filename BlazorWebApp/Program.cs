using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorWebApp.Components;
using BlazorWebApp.Components.Account;
using BlazorWebApp.Data;
using BlazorWebApp.Services;
using Pokemon.Grpc;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
//using Grpc.AspNetCore.Web;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURAZIONE KESTREL (Puntata sulla 7066) ---
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(7066, listenOptions =>
    {
        listenOptions.UseHttps();
        // Supporto simultaneo per Blazor e gRPC
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

// --- 2. SERVIZI UI & HTTP ---
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("PokéAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});
builder.Services.AddHttpClient("PokemonMinimalApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:5083");
});

// --- 3. CONFIGURAZIONE gRPC UNIFICATA (Porta 7066) ---
builder.Services.AddGrpc();

// Registriamo il client usando gRPC-Web per massima compatibilità browser
builder.Services.AddScoped(sp =>
{
    var backendUrl = "https://localhost:7066"; 
    var handler = new GrpcWebHandler(new HttpClientHandler());
    
    var channel = GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions
    {
        HttpHandler = handler
    });

    return new AudioService.AudioServiceClient(channel);
});

// --- 4. ALTRI SERVIZI & IDENTITY ---
builder.Services.AddScoped<IPokedexClientService, PokedexClientService>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IPokemonService, PokemonService>();

// Configurazione Identity e DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options => {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    }).AddIdentityCookies();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// --- 5. PIPELINE MIDDLEWARE (ORDINE RIGOROSO) ---
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 1. Routing deve venire prima di tutto il resto
app.UseRouting();

app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

// 2. Abilita gRPC-Web prima della mappatura degli endpoint
app.UseGrpcWeb();

// 3. Mappatura Endpoint
app.MapGrpcService<PokemonAudioService>().EnableGrpcWeb();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapAdditionalIdentityEndpoints();

app.Use(async (context, next) =>
{
    // Permettiamo i media da 'self' (il tuo server), dai blob (gRPC-Web) e da pokeapi (se scaricati direttamente)
    context.Response.Headers.Append("Content-Security-Policy", "media-src 'self' blob: https://raw.githubusercontent.com;");
    await next();
});

app.Run();