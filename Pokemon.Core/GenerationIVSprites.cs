namespace Pokemon.Core;

public class GenerationIVSprites
{
    [JsonPropertyName("diamond-pearl")] public StandardSpriteSet? DiamondPearl { get; set; }
    [JsonPropertyName("heartgold-soulsilver")] public StandardSpriteSet? HeartGoldSoulSilver { get; set; }
    [JsonPropertyName("platinum")] public StandardSpriteSet? Platinum { get; set; }
}