using System.Text.Json.Serialization;
using Cogwheel;
using Shovel.Core.Helpers;

namespace Shovel.Core.Models.LunarCore;

public class LunarCoreConfig : SettingsBase
{
    public LunarCoreConfig()
        : base(EnvironmentHelper.GetApplicationDataPath("server", "config.json")) { }

    [JsonPropertyName("accountDatabase")]
    public AccountDatabase AccountDatabase { get; set; } = new();

    [JsonPropertyName("gameDatabase")]
    public GameDatabase GameDatabase { get; set; } = new();

    [JsonPropertyName("internalMongoServer")]
    public InternalMongoServer InternalMongoServer { get; set; } = new();

    [JsonPropertyName("useSameDatabase")]
    public bool UseSameDatabase { get; set; } = true;

    [JsonPropertyName("keystore")]
    public Keystore Keystore { get; set; } = new();

    [JsonPropertyName("httpServer")]
    public HttpServer HttpServer { get; set; } = new();

    [JsonPropertyName("gameServer")]
    public GameServer GameServer { get; set; } = new();

    [JsonPropertyName("serverOptions")]
    public ServerOptions ServerOptions { get; set; } = new();

    [JsonPropertyName("serverTime")]
    public ServerTime ServerTime { get; set; } = new();

    [JsonPropertyName("serverRates")]
    public ServerRates ServerRates { get; set; } = new();

    [JsonPropertyName("logOptions")]
    public LogOptions LogOptions { get; set; } = new();

    [JsonPropertyName("downloadData")]
    public DownloadData DownloadData { get; set; } = new();

    [JsonPropertyName("resourceDir")]
    public string ResourceDir { get; set; } = "./resources";

    [JsonPropertyName("dataDir")]
    public string DataDir { get; set; } = "./data";
}
