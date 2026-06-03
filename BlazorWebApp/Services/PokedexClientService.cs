using Pokemon.Core;

public class PokedexClientService(IHttpClientFactory clientFactory) : IPokedexClientService
{
    public async Task<bool> SalvaPokemonAsync(string trainerId, PokemonModel pokemon)
    {
        // "PokemonMinimalApi" è il nome che dovrai registrare nel Program.cs di Blazor
        var client = clientFactory.CreateClient("PokemonMinimalApi");
        
        var response = await client.PostAsJsonAsync($"/api/pokedex/{trainerId}", pokemon);
        return response.IsSuccessStatusCode;
    }
}