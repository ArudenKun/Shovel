using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class GameServer
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("kcpInterval")]
    public int KcpInterval { get; set; }

    [JsonPropertyName("kcpTimeout")]
    public int KcpTimeout { get; set; }

    [JsonPropertyName("bindAddress")]
    public string BindAddress { get; set; }

    [JsonPropertyName("bindPort")]
    public int BindPort { get; set; }

    [JsonPropertyName("publicAddress")]
    public string PublicAddress { get; set; }

    [JsonPropertyName("publicPort")]
    public object PublicPort { get; set; }
}
