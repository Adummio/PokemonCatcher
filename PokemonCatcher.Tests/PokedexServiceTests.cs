using Moq;
using Moq.Protected;
using System.Net;
using Pokemon.Core;
using System.Text.Json;

namespace PokemonCatcher.Tests;


public class PokedexServiceTests
{
    [Fact]
    public async Task SalvaPokemonAsync_ReturnsTrue_OnSuccess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://localhost:5083/") };
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(_ => _.CreateClient("PokemonMinimalApi")).Returns(httpClient);

        var service = new PokedexClientService(mockFactory.Object);
        var dummyPokemon = new PokemonModel { Id = 25, Name = "pikachu" };

        // Act
        var result = await service.SalvaPokemonAsync("trainer123", dummyPokemon);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetPokedexAsync_ReturnsList_WhenDataExists()
    {
        // Arrange
        var trainerId = "trainer_123";
        var mockList = new List<PokemonModel>
        {
            new() { Id = 1, Name = "bulbasaur" },
            new() { Id = 4, Name = "charmander" }
        };
        var jsonResponse = JsonSerializer.Serialize(mockList);

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://localhost:5083/") };
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(_ => _.CreateClient("PokemonMinimalApi")).Returns(httpClient);

        var service = new PokedexClientService(mockFactory.Object);

        // Act
        var result = await service.GetPokedexAsync(trainerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("bulbasaur", result[0].Name);
        Assert.Equal("charmander", result[1].Name);
    }

    // 2. TEST DI SUCCESSO: LISTA VUOTA
    [Fact]
    public async Task GetPokedexAsync_ReturnsEmptyList_WhenTrainerHasNoPokemon()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[]") // Array vuoto
            });

        var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://localhost:5083/") };
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(_ => _.CreateClient("PokemonMinimalApi")).Returns(httpClient);

        var service = new PokedexClientService(mockFactory.Object);

        // Act
        var result = await service.GetPokedexAsync("new_trainer");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    // 3. TEST DI FALLIMENTO: ERRORE SERVER 500 DURANTE SALVATAGGIO
    [Fact]
    public async Task SalvaPokemonAsync_ReturnsFalse_OnServerError()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://localhost:5083/") };
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(_ => _.CreateClient("PokemonMinimalApi")).Returns(httpClient);

        var service = new PokedexClientService(mockFactory.Object);

        // Act
        var result = await service.SalvaPokemonAsync("trainer1", new PokemonModel { Name = "Pikachu" });

        // Assert
        Assert.False(result);
    }
}
