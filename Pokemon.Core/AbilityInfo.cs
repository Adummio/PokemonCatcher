namespace Pokemon.Core;

public class AbilityInfo
{
    [JsonPropertyName("abilities")]
    public Ability? Ability { get; set; }

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }

    [JsonPropertyName("slot")]
    public int Slot { get; set; }
}