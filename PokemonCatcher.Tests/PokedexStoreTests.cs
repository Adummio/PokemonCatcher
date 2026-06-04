using Xunit;
using System.Collections.Generic;
using System.Linq;
using Pokemon.Core;
using PokemonMinimalApi.Services;

// Sostituisci con i tuoi namespace corretti
// using TuoProgetto.Models; 
// using TuoProgetto.Stores;

public class PokedexStoreTests
{
    private readonly PokedexStore _store;

    public PokedexStoreTests()
    {
        // Inizializziamo lo store prima di ogni test
        _store = new PokedexStore();
    }

    [Fact]
    public void AddPokemon_DovrebbeAggiungerePokemon_QuandoAllenatoreENuovo()
    {
        // Arrange
        var trainerId = "allenatore_rosso";
        var pokemon = new PokemonModel { Id = 25, Name = "Pikachu" };

        // Act
        _store.AddPokemon(trainerId, pokemon);
        var result = _store.GetPokedex(trainerId);

        // Assert
        Assert.Single(result); // Verifica che ci sia esattamente 1 elemento
        Assert.Equal("Pikachu", result.First().Name);
    }

    [Fact]
    public void AddPokemon_NonDovrebbeAggiungereDuplicati_ConStessoId()
    {
        // Arrange
        var trainerId = "allenatore_blu";
        var pokemon1 = new PokemonModel { Id = 4, Name = "Charmander" };
        var pokemon2 = new PokemonModel { Id = 4, Name = "Charmander" }; // Stesso ID

        // Act
        _store.AddPokemon(trainerId, pokemon1);
        _store.AddPokemon(trainerId, pokemon2);
        var result = _store.GetPokedex(trainerId);

        // Assert
        Assert.Single(result); // La lista deve contenere solo un elemento nonostante le due chiamate
    }

    [Fact]
    public void AddPokemon_DovrebbeMantenerePokedexSeparati_PerAllenatoriDiversi()
    {
        // Arrange
        var trainer1 = "Ash";
        var trainer2 = "Gary";
        var p1 = new PokemonModel { Id = 25, Name = "Pikachu" };
        var p2 = new PokemonModel { Id = 150, Name = "Mewtwo" };

        // Act
        _store.AddPokemon(trainer1, p1);
        _store.AddPokemon(trainer2, p2);

        // Assert
        Assert.Contains(_store.GetPokedex(trainer1), p => p.Name == "Pikachu");
        Assert.DoesNotContain(_store.GetPokedex(trainer1), p => p.Name == "Mewtwo");
    }

    [Fact]
    public void GetPokedex_DovrebbeRestituireListaVuota_SeAllenatoreNonEsiste()
    {
        // Act
        var result = _store.GetPokedex("id_inesistente");

        // Assert
        Assert.NotNull(result); // Non deve mai essere null
        Assert.Empty(result);   // Deve essere una lista vuota
    }
}