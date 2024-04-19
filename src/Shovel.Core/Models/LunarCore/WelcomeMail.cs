using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class WelcomeMail
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("sender")]
    public string Sender { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("attachments")]
    public List<Attachment> Attachments { get; set; }
}