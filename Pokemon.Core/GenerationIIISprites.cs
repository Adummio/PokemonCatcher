namespace Pokemon.Core;

public class GenerationIIISprites
{
    [JsonPropertyName("emerald")] public StandardSpriteSet? Emerald { get; set; }
    [JsonPropertyName("firered-leafgreen")] public StandardSpriteSet? FireRedLeafGreen { get; set; }
    [JsonPropertyName("ruby-sapphire")] public StandardSpriteSet? RubySapphire { get; set; }
}