using Pokemon.Core;

public interface IPokedexClientService
{
    Task<bool> SalvaPokemonAsync(string? trainerId, PokemonModel pokemon);
    Task<List<PokemonModel>> GetPokedexAsync(string trainerId);
}