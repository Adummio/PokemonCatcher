using Pokemon.Core;

public class PokedexClientService(IHttpClientFactory clientFactory) : IPokedexClientService
{

    private readonly string _clientName = "PokemonMinimalApi";
    public async Task<bool> SalvaPokemonAsync(string? trainerId, PokemonModel pokemon)
    {
        // "PokemonMinimalApi" è il nome che dovrai registrare nel Program.cs di Blazor
        var client = clientFactory.CreateClient(_clientName);
        
        var response = await client.PostAsJsonAsync($"/api/pokedex/{trainerId}", pokemon);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<PokemonModel>> GetPokedexAsync(string trainerId)
    {
        // 1. Recuperiamo l'HttpClient configurato per la Minimal API
        var client = clientFactory.CreateClient(_clientName);

        try
        {
            // 2. Facciamo la richiesta GET all'endpoint della Minimal API
            // L'URL sarà ad esempio: https://localhost:7xxx/api/pokedex/IdUtente
            var pokedex = await client.GetFromJsonAsync<List<PokemonModel>>($"/api/pokedex/{trainerId}");

            // 3. Restituiamo la lista o una lista vuota se il risultato è null
            return pokedex ?? new List<PokemonModel>();
        }
        catch (HttpRequestException ex)
        {
            // Log dell'errore di connessione
            Console.WriteLine($"Errore di rete durante il recupero del Pokedex: {ex.Message}");
            return new List<PokemonModel>();
        }
        catch (Exception ex)
        {
            // Log di errori generici (es. problemi di deserializzazione)
            Console.WriteLine($"Errore imprevisto: {ex.Message}");
            return new List<PokemonModel>();
        }
    }
}