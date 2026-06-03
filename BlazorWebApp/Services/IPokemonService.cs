namespace BlazorWebApp.Services;
using Pokemon.Core;

public interface IPokemonService
{
    Task<PokemonModel?> GetPokemonAsync(int id);
    Task<PokemonModel?> GetRandomPokemonAsync();
}