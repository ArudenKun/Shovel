using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class InternalMongoServer
{
    [JsonPropertyName("address")]
    public string Address { get; set; } = "localhost";

    [JsonPropertyName("port")]
    public int Port { get; set; } = 27017;

    [JsonPropertyName("filePath")]
    public string FilePath { get; set; } = "database.mv";
}
