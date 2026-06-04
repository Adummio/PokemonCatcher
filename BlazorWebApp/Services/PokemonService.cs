namespace BlazorWebApp.Services;
using System.Net.Http.Json;
using Pokemon.Core;

public class PokemonService(IHttpClientFactory clientFactory) : IPokemonService
{
    public async Task<PokemonModel?> GetPokemonAsync(int id)
    {
        // 1. Recuperiamo il client configurato nel Program.cs
        // Nota: Assicurati che il nome "PokéAPI" sia identico a quello nel Program.cs
        var client = clientFactory.CreateClient("PokéAPI");

        try
        {
            // 2. Eseguiamo la chiamata e deserializziamo automaticamente
            // pokemon/{id} viene unito alla BaseAddress (https://pokeapi.co/api/v2/)
            var pokemon = await client.GetFromJsonAsync<PokemonModel>($"pokemon/{id}");
            
            return pokemon;
        }
        catch (HttpRequestException ex)
        {
            // Gestione errore: Pokemon non trovato o server offline
            Console.WriteLine($"Errore durante il recupero del Pokemon {id}: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore generico: {ex.Message}");
            return null;
        }
    }

    public async Task<PokemonModel?> GetRandomPokemonAsync()
    {
        // Genera un ID casuale tra i primi 1000 Pokemon
        int randomId = new Random().Next(1, 1025);
        return await GetPokemonAsync(randomId);
    }
}