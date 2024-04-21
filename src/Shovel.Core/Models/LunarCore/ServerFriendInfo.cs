using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerFriendInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("signature")]
    public string Signature { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("headIcon")]
    public int HeadIcon { get; set; }

    [JsonPropertyName("chatBubbleId")]
    public int ChatBubbleId { get; set; }

    [JsonPropertyName("displayAvatarId")]
    public int DisplayAvatarId { get; set; }

    [JsonPropertyName("displayAvatarLevel")]
    public int DisplayAvatarLevel { get; set; }
}
