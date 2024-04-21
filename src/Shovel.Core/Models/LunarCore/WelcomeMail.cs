using System.Text.Json.Serialization;

namespace Shovel.Core.Models.LunarCore;

public class WelcomeMail
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "Welcome to a LunarCore server";

    [JsonPropertyName("sender")]
    public string Sender { get; set; } = "Server";

    [JsonPropertyName("content")]
    public string Content { get; set; } =
        "Welcome to Lunar Core! Please take these items as a starter gift. For a list of commands, type /help in the server chat window. Check out our <a type=OpenURL1 href=https://discord.gg/cfPKJ6N5hw>Discord</a> and <a type=OpenURL1 href=https://github.com/Melledy/LunarCore>Github</a> for more information about the server.";

    [JsonPropertyName("attachments")]
    public List<Attachment> Attachments { get; set; } =
        [
            new Attachment { Id = 2, Count = 1000000 },
            new Attachment { Id = 101, Count = 100 },
            new Attachment { Id = 102, Count = 100 },
            new Attachment { Id = 1001, Count = 1 },
            new Attachment { Id = 1002, Count = 1 }
        ];
}
