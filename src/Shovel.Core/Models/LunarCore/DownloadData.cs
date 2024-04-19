using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class DownloadData
{
    [JsonPropertyName("assetBundleUrl")]
    public object AssetBundleUrl { get; set; }

    [JsonPropertyName("exResourceUrl")]
    public object ExResourceUrl { get; set; }

    [JsonPropertyName("luaUrl")]
    public object LuaUrl { get; set; }

    [JsonPropertyName("ifixUrl")]
    public object IfixUrl { get; set; }
}
