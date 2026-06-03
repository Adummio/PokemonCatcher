namespace PokemonMinimalApi.Services;

using System.Collections.Concurrent;
using Pokemon.Core;

public class PokedexStore : IPokedexStore
{
    // Chiave: TrainerId, Valore: Lista di Pokemon catturati
    private readonly ConcurrentDictionary<string, List<PokemonModel>> _store = new();

    public void AddPokemon(string trainerId, PokemonModel pokemon)
    {
        var pokedex = _store.GetOrAdd(trainerId, _ => new List<PokemonModel>());
        
        // Evitiamo duplicati (opzionale)
        if (!pokedex.Any(p => p.Id == pokemon.Id))
        {
            pokedex.Add(pokemon);
        }
    }

    public List<PokemonModel> GetPokedex(string trainerId)
    {
        return _store.TryGetValue(trainerId, out var pokedex) ? pokedex : new List<PokemonModel>();
    }
}