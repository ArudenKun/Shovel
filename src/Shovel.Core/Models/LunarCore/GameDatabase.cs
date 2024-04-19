using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class GameDatabase
{
    [JsonPropertyName("uri")]
    public string Uri { get; set; }

    [JsonPropertyName("collection")]
    public string Collection { get; set; }

    [JsonPropertyName("useInternal")]
    public bool UseInternal { get; set; }
}