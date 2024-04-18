using Shovel.ViewModels.Abstractions;
using Shovel.Views.Pages;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Shovel.ViewModels.Pages;

public sealed partial class SettingsPageViewModel : PageViewModelBase, INavigationAware
{
    public override int Index => 999;
    public override SymbolIcon Icon => new(SymbolRegular.Settings24);
    public override bool IsFooter => true;
    public override Type PageType => typeof(SettingsPage);

    private bool _isInitialized;

    [ObservableProperty]
    private string _appVersion = string.Empty;

    [ObservableProperty]
    private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        CurrentTheme = ApplicationThemeManager.GetAppTheme();
        AppVersion = $"UiDesktopApp1 - {GetAssemblyVersion()}";

        _isInitialized = true;
    }

    private string GetAssemblyVersion() =>
        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
        ?? string.Empty;

    [RelayCommand]
    private void OnChangeTheme(string parameter)
    {
        switch (parameter)
        {
            case "theme_light":
                if (CurrentTheme == ApplicationTheme.Light)
                    break;
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                CurrentTheme = ApplicationTheme.Light;
                break;
            case "theme_dark":
                if (CurrentTheme == ApplicationTheme.Dark)
                    break;
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                CurrentTheme = ApplicationTheme.Dark;
                break;
            default:
                ApplicationThemeManager.Apply(ApplicationTheme.Unknown);
                CurrentTheme = ApplicationTheme.Unknown;
                break;
        }
    }
}
