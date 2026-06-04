namespace Pokemon.Core;

public class PokemonPastAbilityInfo
{

    [JsonPropertyName("abilities")]
    public List<PastAbilityDetailsInfo>? Abilities { get; set; }

    [JsonPropertyName("generation")]
    public GenerationDetails? Generation { get; set; }
}