namespace Pokemon.Core;

public class GenerationISprites
{
    [JsonPropertyName("red-blue")] public GameBoySpriteSet? RedBlue { get; set; }
    [JsonPropertyName("yellow")] public GameBoySpriteSet? Yellow { get; set; }
}