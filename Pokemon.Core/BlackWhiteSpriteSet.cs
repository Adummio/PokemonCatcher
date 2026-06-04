namespace Pokemon.Core;

public class BlackWhiteSpriteSet : StandardSpriteSet
{
    [JsonPropertyName("animated")] public StandardSpriteSet? Animated { get; set; }
}