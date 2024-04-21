using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerRates
{
    [JsonPropertyName("exp")]
    public double Exp { get; set; }

    [JsonPropertyName("credit")]
    public double Credit { get; set; }

    [JsonPropertyName("jade")]
    public double Jade { get; set; }

    [JsonPropertyName("material")]
    public double Material { get; set; }

    [JsonPropertyName("equip")]
    public double Equip { get; set; }
}
