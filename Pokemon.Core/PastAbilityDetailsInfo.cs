namespace Pokemon.Core;

public class PastAbilityDetailsInfo
{
    // Il punto di domanda "?" indica che la proprietà può essere null
    [JsonPropertyName("ability")]
    public Ability? Ability { get; set; }

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }

    [JsonPropertyName("slot")]
    public int Slot { get; set; }
}