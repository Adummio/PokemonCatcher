namespace Pokemon.Core;

public class PokemonPastStatInfo
{
    // Riutilizziamo la classe GenerationDetails creata prima!
    [JsonPropertyName("generation")]
    public GenerationDetails? Generation { get; set; }

    [JsonPropertyName("stats")]
    public List<PokemonPastStatDetails>? Stats { get; set; }
}