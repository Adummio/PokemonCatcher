namespace PokemonMinimalApi.Services;
using Pokemon.Core;

public interface IPokedexStore
{
    void AddPokemon(string trainerId, PokemonModel pokemon);
    List<PokemonModel> GetPokedex(string trainerId);
}