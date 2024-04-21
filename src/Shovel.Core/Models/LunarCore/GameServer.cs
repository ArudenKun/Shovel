using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class GameServer
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "lunar_rail_test";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "Lunar Core";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "A LunarCore server";

    [JsonPropertyName("kcpInterval")]
    public int KcpInterval { get; set; } = 40;

    [JsonPropertyName("kcpTimeout")]
    public int KcpTimeout { get; set; } = 30;

    [JsonPropertyName("bindAddress")]
    public string BindAddress { get; set; } = "0.0.0.0";

    [JsonPropertyName("bindPort")]
    public int BindPort { get; set; } = 23301;

    [JsonPropertyName("publicAddress")]
    public string PublicAddress { get; set; } = "127.0.0.1";

    [JsonPropertyName("publicPort")]
    public int? PublicPort { get; set; }
}
