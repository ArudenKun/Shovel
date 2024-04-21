using System.Text.Json.Serialization;
using Cogwheel;
using CommunityToolkit.Mvvm.ComponentModel;
using Shovel.Core.Helpers;

namespace Shovel.Models;

[ObservableObject]
public sealed partial class AppConfig : SettingsBase
{
    [ObservableProperty]
    [property: JsonConverter(typeof(JsonStringEnumConverter<ThemeMode>))]
    private ThemeMode _themeMode = ThemeMode.Light;

    [ObservableProperty]
    private bool _checkForUpdates = true;

    public AppConfig()
        : base(EnvironmentHelper.GetApplicationDataPath("appconfig.json"))
    {
        Load();

        PropertyChanged += (_, _) =>
        {
            Save();
        };
    }
}
