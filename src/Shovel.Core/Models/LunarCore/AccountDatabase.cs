using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class AccountDatabase
{
    [JsonPropertyName("uri")]
    public string Uri { get; set; } = "mongodb://localhost:27017";

    [JsonPropertyName("collection")]
    public string Collection { get; set; } = "lunarcore";

    [JsonPropertyName("useInternal")]
    public bool UseInternal { get; set; } = true;
}
