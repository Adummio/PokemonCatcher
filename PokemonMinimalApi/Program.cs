using Pokemon.Core;
using PokemonMinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPokedexStore, PokedexStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/api/pokedex/{trainerId}", (string trainerId, PokemonModel pokemon, IPokedexStore store) =>
{
    if (pokemon == null) return Results.BadRequest("Dati pokemon non validi");
    
    store.AddPokemon(trainerId, pokemon);
    return Results.Ok(new { Message = $"{pokemon.Name} salvato nel Pokedex di {trainerId}" });
});

// Endpoint per recuperare il pokedex (ci servirà dopo)
app.MapGet("/api/pokedex/{trainerId}", (string trainerId, IPokedexStore store) =>
{
    return Results.Ok(store.GetPokedex(trainerId));
});


app.UseHttpsRedirection();

app.Run();

