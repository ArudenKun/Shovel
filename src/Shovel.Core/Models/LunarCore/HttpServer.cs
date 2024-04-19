using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class HttpServer
{
    [JsonPropertyName("useSSL")]
    public bool UseSsl { get; set; }

    [JsonPropertyName("regionListRefresh")]
    public int RegionListRefresh { get; set; }

    [JsonPropertyName("bindAddress")]
    public string BindAddress { get; set; }

    [JsonPropertyName("bindPort")]
    public int BindPort { get; set; }

    [JsonPropertyName("publicAddress")]
    public string PublicAddress { get; set; }

    [JsonPropertyName("publicPort")]
    public object PublicPort { get; set; }
}
