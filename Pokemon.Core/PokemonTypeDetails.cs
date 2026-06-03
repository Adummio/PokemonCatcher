namespace Pokemon.Core;

public class PokemonTypeDetails
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public TypeDetails? Type { get; set; }
}