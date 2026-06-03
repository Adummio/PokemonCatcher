namespace Pokemon.Core;

public class StatDetails
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }


    [JsonPropertyName("url")]
    public string? Url { get; set; }
}