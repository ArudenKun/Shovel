using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerTime
{
    [JsonPropertyName("spoofTime")]
    public bool SpoofTime { get; set; } = false;

    [JsonPropertyName("spoofDate")]
    public DateTime SpoofDate { get; set; } = DateTime.Parse("15-01-2024 08:00:00");
}
