using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerRates
{
    [JsonPropertyName("exp")]
    public double Exp { get; set; } = 1.0;

    [JsonPropertyName("credit")]
    public double Credit { get; set; } = 1.0;

    [JsonPropertyName("jade")]
    public double Jade { get; set; } = 1.0;

    [JsonPropertyName("material")]
    public double Material { get; set; } = 1.0;

    [JsonPropertyName("equip")]
    public double Equip { get; set; } = 1.0;
}
