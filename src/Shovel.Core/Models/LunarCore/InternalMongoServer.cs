using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class InternalMongoServer
{
    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("filePath")]
    public string FilePath { get; set; }
}
