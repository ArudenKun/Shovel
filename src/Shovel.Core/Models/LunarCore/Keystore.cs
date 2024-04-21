using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class Keystore
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = "./keystore.p12";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "lunar";
}
