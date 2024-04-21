using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class DownloadData
{
    [JsonPropertyName("assetBundleUrl")]
    public string? AssetBundleUrl { get; set; }

    [JsonPropertyName("exResourceUrl")]
    public string? ExResourceUrl { get; set; }

    [JsonPropertyName("luaUrl")]
    public string? LuaUrl { get; set; }

    [JsonPropertyName("ifixUrl")]
    public string? IfixUrl { get; set; }
}
