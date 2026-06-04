namespace Pokemon.Core;

public class GenerationIISprites
{
    [JsonPropertyName("crystal")] public CrystalSpriteSet? Crystal { get; set; }
    [JsonPropertyName("gold")] public StandardSpriteSet? Gold { get; set; }
    [JsonPropertyName("silver")] public StandardSpriteSet? Silver { get; set; }
}