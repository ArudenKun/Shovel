using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerOptions
{
    [JsonPropertyName("autoCreateAccount")]
    public bool AutoCreateAccount { get; set; }

    [JsonPropertyName("sceneMaxEntites")]
    public int SceneMaxEntites { get; set; }

    [JsonPropertyName("maxCustomRelicLevel")]
    public int MaxCustomRelicLevel { get; set; }

    [JsonPropertyName("unlockAllChallenges")]
    public bool UnlockAllChallenges { get; set; }

    [JsonPropertyName("spendStamina")]
    public bool SpendStamina { get; set; }

    [JsonPropertyName("staminaRecoveryRate")]
    public int StaminaRecoveryRate { get; set; }

    [JsonPropertyName("staminaReserveRecoveryRate")]
    public int StaminaReserveRecoveryRate { get; set; }

    [JsonPropertyName("startTrailblazerLevel")]
    public int StartTrailblazerLevel { get; set; }

    [JsonPropertyName("autoUpgradeWorldLevel")]
    public bool AutoUpgradeWorldLevel { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("defaultPermissions")]
    public List<string> DefaultPermissions { get; set; }

    [JsonPropertyName("serverFriendInfo")]
    public ServerFriendInfo ServerFriendInfo { get; set; }

    [JsonPropertyName("welcomeMail")]
    public WelcomeMail WelcomeMail { get; set; }
}
