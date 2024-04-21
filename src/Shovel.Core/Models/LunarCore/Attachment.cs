using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class Attachment
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}
