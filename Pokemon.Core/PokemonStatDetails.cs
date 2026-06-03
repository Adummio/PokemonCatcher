namespace Pokemon.Core;

public class PokemonStatDetails
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }

    [JsonPropertyName("effort")]
    public int Effort { get; set; }

    [JsonPropertyName("stat")]
    public StatDetails? Stat { get; set; }
}