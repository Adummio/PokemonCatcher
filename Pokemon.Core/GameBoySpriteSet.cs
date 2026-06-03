namespace Pokemon.Core;

public class GameBoySpriteSet
{
    [JsonPropertyName("front_default")] public string? FrontDefault { get; set; }
    [JsonPropertyName("front_gray")] public string? FrontGray { get; set; }
    [JsonPropertyName("front_transparent")] public string? FrontTransparent { get; set; }
    [JsonPropertyName("back_default")] public string? BackDefault { get; set; }
    [JsonPropertyName("back_gray")] public string? BackGray { get; set; }
    [JsonPropertyName("back_transparent")] public string? BackTransparent { get; set; }
}