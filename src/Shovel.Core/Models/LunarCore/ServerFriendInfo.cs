using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerFriendInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "Server";

    [JsonPropertyName("signature")]
    public string Signature { get; set; } = "Type /help for a list of commands";

    [JsonPropertyName("level")]
    public int Level { get; set; } = 1;

    [JsonPropertyName("headIcon")]
    public int HeadIcon { get; set; } = 201001;

    [JsonPropertyName("chatBubbleId")]
    public int ChatBubbleId { get; set; } = 0;

    [JsonPropertyName("displayAvatarId")]
    public int DisplayAvatarId { get; set; } = 1001;

    [JsonPropertyName("displayAvatarLevel")]
    public int DisplayAvatarLevel { get; set; } = 1;
}
