namespace Pokemon.Core;

public class CrystalSpriteSet : StandardSpriteSet
{
    [JsonPropertyName("back_shiny_transparent")] public string? BackShinyTransparent { get; set; }
    [JsonPropertyName("front_shiny_transparent")] public string? FrontShinyTransparent { get; set; }
}