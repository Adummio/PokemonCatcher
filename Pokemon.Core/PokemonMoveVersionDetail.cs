namespace Pokemon.Core;

public class PokemonMoveVersionDetail
{
    [JsonPropertyName("level_learned_at")]
    public int LevelLearnedAt { get; set; }

    [JsonPropertyName("move_learn_method")]
    public MoveLearnMethod? LearnMethod { get; set; }

    // Usiamo int? perché nel JSON può arrivare "null"
    [JsonPropertyName("order")]
    public int? Order { get; set; }

    [JsonPropertyName("version_group")]
    public VersionGroupDetails? VersionGroup { get; set; }
}