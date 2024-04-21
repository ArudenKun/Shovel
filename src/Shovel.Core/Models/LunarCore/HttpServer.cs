using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class HttpServer
{
    [JsonPropertyName("useSSL")]
    public bool UseSsl { get; set; } = false;

    [JsonPropertyName("regionListRefresh")]
    public int RegionListRefresh { get; set; } = 60000;

    [JsonPropertyName("bindAddress")]
    public string BindAddress { get; set; } = "0.0.0.0";

    [JsonPropertyName("bindPort")]
    public int BindPort { get; set; } = 443;

    [JsonPropertyName("publicAddress")]
    public string PublicAddress { get; set; } = "127.0.0.1";

    [JsonPropertyName("publicPort")]
    public int? PublicPort { get; set; }
}
