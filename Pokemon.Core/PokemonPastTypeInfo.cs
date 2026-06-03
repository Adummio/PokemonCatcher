namespace Pokemon.Core;

public class PokemonPastTypeInfo
{
    // Riutilizziamo la classe GenerationDetails!
    [JsonPropertyName("generation")]
    public GenerationDetails? Generation { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonPastTypeDetails>? Types { get; set; }
}