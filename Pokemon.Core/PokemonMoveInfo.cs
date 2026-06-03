namespace Pokemon.Core;

public class PokemonMoveInfo
{
    [JsonPropertyName("move")]
    public MoveDetails? Move { get; set; }

    [JsonPropertyName("version_group_details")]
    public List<PokemonMoveVersionDetail>? VersionGroupDetails { get; set; }
}