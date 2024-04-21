using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using Shovel.Core.Helpers;
using Shovel.Models;
using Shovel.Services;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed partial class SettingsViewModel : ViewModelBase
{
    private readonly IThemeService _themeService;

    [ObservableProperty]
    private ComboBoxModel<ThemeMode> _currentTheme;

    public SettingsViewModel(
        IDialogService dialogService,
        AppConfig appConfig,
        IThemeService themeService
    )
        : base(dialogService)
    {
        _themeService = themeService;

        CurrentTheme = Themes.First(x => x.Enum == appConfig.ThemeMode);
    }

    public IReadOnlyList<ComboBoxModel<ThemeMode>> Themes { get; } =
        [
            new ComboBoxModel<ThemeMode>(ThemeMode.Light),
            new ComboBoxModel<ThemeMode>(ThemeMode.Dark),
            new ComboBoxModel<ThemeMode>(ThemeMode.System)
        ];

    [RelayCommand]
    private static void OpenUrl(string url) => EnvironmentHelper.OpenUrl(url);

    partial void OnCurrentThemeChanged(ComboBoxModel<ThemeMode> value) =>
        _themeService.ChangeTheme(value.Enum);
}
