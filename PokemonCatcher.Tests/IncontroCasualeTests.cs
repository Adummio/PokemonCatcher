using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Pokemon.Core;
using BlazorWebApp.Components.Pages;
using BlazorWebApp.Services;
using Pokemon.Grpc;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

public class IncontroCasualeTests : BunitContext 
{
    [Fact]
    public void IncontroCasuale_ShouldShowPokemonName_WhenLoaded()
    {
        // 1. ARRANGE
        var mockPokeService = new Mock<IPokemonService>();
        var mockPokedexService = new Mock<IPokedexClientService>();
        var mockAudioClient = new Mock<AudioService.AudioServiceClient>();
        var mockJS = new Mock<IJSRuntime>();
        var mockAuthState = new Mock<AuthenticationStateProvider>();

        // Mock Autenticazione
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "test_trainer_id") };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var user = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(user);

        mockAuthState.Setup(a => a.GetAuthenticationStateAsync())
            .ReturnsAsync(authState);

        // Mock Pokémon
        mockPokeService.Setup(s => s.GetRandomPokemonAsync())
            .ReturnsAsync(new PokemonModel 
            { 
                Name = "Pikachu", 
                Id = 25,
                Sprites = new() { FrontDefault = "https://fake-url.com/pikachu.png" } 
            });

        Services.AddSingleton(mockPokeService.Object);
        Services.AddSingleton(mockPokedexService.Object);
        Services.AddSingleton(mockAudioClient.Object);
        Services.AddSingleton(mockJS.Object);
        Services.AddSingleton(mockAuthState.Object);

        // 2. ACT
        var cut = Render<IncontroCasuale>();

        // 3. ASSERT
        cut.WaitForState(() => cut.Find(".pokemon-name") != null);
        var nameElement = cut.Find(".pokemon-name");
        Assert.Contains("PIKACHU", nameElement.TextContent);
    }
}