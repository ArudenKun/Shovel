using System.Text.Json.Serialization;
using Cogwheel;
using Shovel.Core.Helpers;

namespace Shovel.Core.Models.LunarCore;

public class LunarCoreConfig : SettingsBase
{
    public LunarCoreConfig()
        : base(EnvironmentHelper.GetApplicationDataPath("server", "config.json")) { }

    [JsonPropertyName("accountDatabase")]
    public AccountDatabase AccountDatabase { get; set; }

    [JsonPropertyName("gameDatabase")]
    public GameDatabase GameDatabase { get; set; }

    [JsonPropertyName("internalMongoServer")]
    public InternalMongoServer InternalMongoServer { get; set; }

    [JsonPropertyName("useSameDatabase")]
    public bool UseSameDatabase { get; set; }

    [JsonPropertyName("keystore")]
    public Keystore Keystore { get; set; }

    [JsonPropertyName("httpServer")]
    public HttpServer HttpServer { get; set; }

    [JsonPropertyName("gameServer")]
    public GameServer GameServer { get; set; }

    [JsonPropertyName("serverOptions")]
    public ServerOptions ServerOptions { get; set; }

    [JsonPropertyName("serverTime")]
    public ServerTime ServerTime { get; set; }

    [JsonPropertyName("serverRates")]
    public ServerRates ServerRates { get; set; }

    [JsonPropertyName("logOptions")]
    public LogOptions LogOptions { get; set; }

    [JsonPropertyName("downloadData")]
    public DownloadData DownloadData { get; set; }

    [JsonPropertyName("resourceDir")]
    public string ResourceDir { get; set; }

    [JsonPropertyName("dataDir")]
    public string DataDir { get; set; }
}
