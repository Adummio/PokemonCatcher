namespace Pokemon.Core;

public class PokemonVersionHistory
{
    [JsonPropertyName("generation-i")]
    public GenerationISprites? Gen1 { get; set; }

    [JsonPropertyName("generation-ii")]
    public GenerationIISprites? Gen2 { get; set; }

    [JsonPropertyName("generation-iii")]
    public GenerationIIISprites? Gen3 { get; set; }

    [JsonPropertyName("generation-iv")]
    public GenerationIVSprites? Gen4 { get; set; }

    [JsonPropertyName("generation-v")]
    public GenerationVSprites? Gen5 { get; set; }

    [JsonPropertyName("generation-vi")]
    public GenerationVISprites? Gen6 { get; set; }

    [JsonPropertyName("generation-vii")]
    public GenerationVIISprites? Gen7 { get; set; }

    [JsonPropertyName("generation-viii")]
    public GenerationVIIISprites? Gen8 { get; set; }

    [JsonPropertyName("generation-ix")]
    public GenerationIXSprites? Gen9 { get; set; }
}