namespace Pokemon.Core;

public class OtherSprites
{
    [JsonPropertyName("dream_world")]
    public DreamWorldSprites? DreamWorld { get; set; }

    [JsonPropertyName("home")]
    public HomeSprites? Home { get; set; }

    [JsonPropertyName("official-artwork")]
    public OfficialArtworkSprites? OfficialArtwork { get; set; }

    [JsonPropertyName("showdown")]
    public ShowdownSprites? Showdown { get; set; }
}