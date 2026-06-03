namespace Pokemon.Core;

public class PokemonPastTypeDetails
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public TypeDetails? Type { get; set; }
}