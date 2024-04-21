using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class ServerOptions
{
    [JsonPropertyName("autoCreateAccount")]
    public bool AutoCreateAccount { get; set; } = true;

    [JsonPropertyName("sceneMaxEntites")]
    public int SceneMaxEntites { get; set; } = 500;

    [JsonPropertyName("maxCustomRelicLevel")]
    public int MaxCustomRelicLevel { get; set; } = 15;

    [JsonPropertyName("unlockAllChallenges")]
    public bool UnlockAllChallenges { get; set; } = true;

    [JsonPropertyName("spendStamina")]
    public bool SpendStamina { get; set; } = true;

    [JsonPropertyName("staminaRecoveryRate")]
    public int StaminaRecoveryRate { get; set; } = 300;

    [JsonPropertyName("staminaReserveRecoveryRate")]
    public int StaminaReserveRecoveryRate { get; set; } = 1080;

    [JsonPropertyName("startTrailblazerLevel")]
    public int StartTrailblazerLevel { get; set; } = 1;

    [JsonPropertyName("autoUpgradeWorldLevel")]
    public bool AutoUpgradeWorldLevel { get; set; } = true;

    [JsonPropertyName("language")]
    public string Language { get; set; } = "EN";

    [JsonPropertyName("defaultPermissions")]
    public List<string> DefaultPermissions { get; set; } = ["*"];

    [JsonPropertyName("serverFriendInfo")]
    public ServerFriendInfo ServerFriendInfo { get; set; } = new ServerFriendInfo();

    [JsonPropertyName("welcomeMail")]
    public WelcomeMail WelcomeMail { get; set; } = new WelcomeMail();
}
