namespace Pokemon.Core;

public class PokemonItemVersionDetail
{
    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }

    // Riutilizziamo la classe creata in precedenza!
    [JsonPropertyName("version")]
    public GameVersionDetails? Version { get; set; }
}