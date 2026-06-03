namespace Pokemon.Core;

public class PokemonModel
{
    [JsonPropertyName("abilities")]
    public AbilityInfo[]? Abilities { get; set; }

    [JsonPropertyName("base_experience")]
    public int BaseExperience { get; set; }

    [JsonPropertyName("cries")]
    public Cries? Cries { get; set; }

    [JsonPropertyName("forms")]
    public Form[]? Forms { get; set; }

    [JsonPropertyName("game_indices")]
    public List<PokemonGameIndexInfo>? GameIndices { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("held_items")]
    public List<PokemonHeldItemInfo>? HeldItems { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("location_area_encounters")]
    public string? LocationAreaEncounters { get; set; }

    [JsonPropertyName("moves")]
    public List<PokemonMoveInfo>? Moves { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("past_abilities")]
    public List<PokemonPastAbilityInfo>? PastAbilities { get; set; }

    [JsonPropertyName("past_stats")]
    public List<PokemonPastStatInfo>? PastStats { get; set; }

    [JsonPropertyName("past_types")]
    public List<PokemonPastTypeInfo>? PastTypes { get; set; }

    [JsonPropertyName("species")]
    public PokemonSpeciesInfo? Species { get; set; }

    [JsonPropertyName("sprites")]
    public PokemonSprites? Sprites { get; set; }

    [JsonPropertyName("stats")]
    public List<PokemonStatDetails>? Stats { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonTypeDetails>? Types { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }
}

