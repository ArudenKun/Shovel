using Avalonia.Styling;
using Cogwheel;
using Shovel.Core.Helpers;

namespace Shovel.Models;

internal sealed class AppConfig : SettingsBase
{
    public ThemeVariant Theme { get; }

    public AppConfig()
        : base(EnvironmentHelper.GetApplicationDataPath("appconfig.json")) { }
}
