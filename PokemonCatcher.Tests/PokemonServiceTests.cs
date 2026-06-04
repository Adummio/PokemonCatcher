using System.Net;
using System.Net.Http.Json;
using BlazorWebApp.Services;
using Moq;
using Moq.Protected;
using Pokemon.Core;
using Xunit;

public class PokemonServiceTests
{
    private readonly Mock<IHttpClientFactory> _mockFactory;
    private readonly Mock<HttpMessageHandler> _handlerMock;

    public PokemonServiceTests()
    {
        _mockFactory = new Mock<IHttpClientFactory>();
        _handlerMock = new Mock<HttpMessageHandler>();
    }

    [Fact]
    public async Task GetPokemonAsync_DovrebbeRestituirePokemon_QuandoApiRispondeCorrettamente()
    {
        // Arrange
        var pokemonId = 25;
        var expectedPokemon = new PokemonModel { Id = pokemonId, Name = "Pikachu" };

        // Simuliamo la risposta 200 OK con l'oggetto JSON
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(expectedPokemon)
        };

        SetupMockHttpClient(responseMessage);

        var service = new PokemonService(_mockFactory.Object);

        // Act
        var result = await service.GetPokemonAsync(pokemonId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pikachu", result?.Name);
        Assert.Equal(25, result?.Id);
    }

    [Fact]
    public async Task GetPokemonAsync_DovrebbeRestituireNull_QuandoApiRestituisce404()
    {
        // Arrange
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };

        SetupMockHttpClient(responseMessage);

        var service = new PokemonService(_mockFactory.Object);

        // Act
        var result = await service.GetPokemonAsync(9999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetRandomPokemonAsync_DovrebbeChiamareApiEPrendereUnPokemon()
    {
        // Arrange
        var expectedPokemon = new PokemonModel { Id = 123, Name = "Scyther" };
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(expectedPokemon)
        };

        SetupMockHttpClient(responseMessage);

        var service = new PokemonService(_mockFactory.Object);

        // Act
        var result = await service.GetRandomPokemonAsync();

        // Assert
        Assert.NotNull(result);
        // Verifichiamo che la chiamata sia avvenuta (tramite Moq.Protected verifichiamo il send)
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    // Helper per configurare il Mock dell'HttpClient
    private void SetupMockHttpClient(HttpResponseMessage response)
    {
        _handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(response);

        var httpClient = new HttpClient(_handlerMock.Object)
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/")
        };

        // Configuriamo la factory per restituire il nostro client finto quando viene chiesto "PokéAPI"
        _mockFactory.Setup(_ => _.CreateClient("PokéAPI")).Returns(httpClient);
    }
}