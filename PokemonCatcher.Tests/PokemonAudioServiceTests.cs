using Xunit;
using Moq;
using Moq.Protected;
using System.Net;
using Grpc.Core;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Pokemon.Grpc; 
using BlazorWebApp.Services;

public class PokemonAudioServiceTests
{
    [Fact]
    public async Task GetPokemonCry_ShouldStreamData_WhenUrlIsValid()
    {
        // 1. ARRANGE - Mock dell'infrastruttura di rete (HttpClient)
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
                Content = new ByteArrayContent(new byte[] { 0x01, 0x02, 0x03, 0x04 })
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        // Istanza del servizio gRPC passandogli la fabbrica mockata
        var service = new PokemonAudioService(mockHttpClientFactory.Object);

        // Mock dello stream gRPC e del contesto
        var mockStreamWriter = new Mock<IServerStreamWriter<AudioChunk>>();
        var mockServerCallContext = new Mock<ServerCallContext>();

        // FIX: Firma a 1 parametro per WriteAsync (match con il log Performed Invocations)
        mockStreamWriter.Setup(w => w.WriteAsync(It.IsAny<AudioChunk>()))
            .Returns(Task.CompletedTask);

        // 2. ACT - Esecuzione della chiamata gRPC
        var request = new AudioRequest { AudioUrl = "http://fake-audio-url.ogg" };
        
        await service.GetPokemonCry(request, mockStreamWriter.Object, mockServerCallContext.Object);

        // 3. ASSERT - Verifica che WriteAsync sia stato chiamato almeno una volta
        mockStreamWriter.Verify(w => w.WriteAsync(It.IsAny<AudioChunk>()), Times.AtLeastOnce());
    }
}