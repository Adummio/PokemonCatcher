namespace Pokemon.Core;

public class PokemonHeldItemInfo
{
    [JsonPropertyName("item")]
    public ItemDetails? Item { get; set; }

    [JsonPropertyName("version_details")]
    public List<PokemonItemVersionDetail>? VersionDetails { get; set; }
}