using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerTime
{
    [JsonPropertyName("spoofTime")]
    public bool SpoofTime { get; set; }

    [JsonPropertyName("spoofDate")]
    public string SpoofDate { get; set; }
}