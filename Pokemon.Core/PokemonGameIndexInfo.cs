namespace Pokemon.Core;

public class PokemonGameIndexInfo
{
    [JsonPropertyName("game_index")]
    public int GameIndex { get; set; }

    [JsonPropertyName("version")]
    public GameVersionDetails? Version { get; set; }
}