
using Pokemon.Grpc;
using Grpc.Core;

namespace BlazorWebApp.Services;
public class PokemonAudioService(IHttpClientFactory httpClientFactory) : AudioService.AudioServiceBase
{
    public override async Task GetPokemonCry(AudioRequest request, IServerStreamWriter<AudioChunk> responseStream, ServerCallContext context)
    {
        // Validazione base dell'URL ricevuto dal client Blazor
        if (string.IsNullOrWhiteSpace(request.AudioUrl))
        {
            Console.WriteLine("[gRPC Server] Errore: URL audio vuoto o non valido.");
            return;
        }

        var client = httpClientFactory.CreateClient();

        try
        {
            // Eseguiamo il download dall'URL dinamico (p.cries.latest)
            using var response = await client.GetAsync(request.AudioUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var remoteStream = await response.Content.ReadAsStreamAsync();
            var buffer = new byte[32768]; // Chunk di 32 KB
            
            while (true)
            {
                int bytesRead = await remoteStream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                await responseStream.WriteAsync(new AudioChunk
                {
                    Data = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead)
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[gRPC Server] Impossibile recuperare l'audio da {request.AudioUrl}: {ex.Message}");
        }
    }
}