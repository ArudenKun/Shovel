using Humanizer;

namespace Shovel.Models;

public sealed class ComboBoxModel<TEnum>
    where TEnum : Enum
{
    public ComboBoxModel(TEnum @enum)
    {
        Enum = @enum;
    }

    public string DisplayName => Enum.Humanize().Transform(To.TitleCase);
    public TEnum Enum { get; }
}
