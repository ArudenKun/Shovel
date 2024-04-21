using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class LogOptions
{
    [JsonPropertyName("commands")]
    public bool Commands { get; set; }

    [JsonPropertyName("connections")]
    public bool Connections { get; set; }

    [JsonPropertyName("packets")]
    public bool Packets { get; set; }

    [JsonPropertyName("filterLoopingPackets")]
    public bool FilterLoopingPackets { get; set; }
}
