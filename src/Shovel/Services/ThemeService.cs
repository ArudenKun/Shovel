using AutoInterfaceAttributes;
using Avalonia;
using Avalonia.Styling;
using FluentAvalonia.Styling;
using Shovel.Models;

namespace Shovel.Services;

[AutoInterface]
public sealed class ThemeService : IThemeService
{
    private readonly AppConfig _appConfig;

    public ThemeService(AppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    public ThemeVariant ThemeVariant => Application.Current!.ActualThemeVariant;
    public ThemeMode ThemeMode => GetCurrentThemeMode();

    public void ChangeTheme() =>
        ChangeTheme(_appConfig.ThemeMode == ThemeMode.Dark ? ThemeMode.Light : ThemeMode.Dark);

    public void ChangeTheme(ThemeMode themeMode)
    {
        var newTheme = GetThemeVariant(themeMode);
        var faTheme = GetTheme();

        Application.Current!.RequestedThemeVariant = newTheme;
        faTheme.PreferSystemTheme = themeMode == ThemeMode.System;

        _appConfig.ThemeMode = themeMode;
    }

    public void LoadTheme()
    {
        if (_appConfig.ThemeMode == ThemeMode.Dark)
        {
            ChangeTheme(ThemeMode.Dark);
        }

        if (_appConfig.ThemeMode == ThemeMode.Light)
        {
            ChangeTheme(ThemeMode.Light);
        }

        if (_appConfig.ThemeMode == ThemeMode.System)
        {
            ChangeTheme(ThemeMode.System);
        }
    }

    public ThemeMode GetCurrentThemeMode()
    {
        var actualTheme = Application.Current!.ActualThemeVariant;
        if (actualTheme == ThemeVariant.Dark)
        {
            return ThemeMode.Dark;
        }

        return actualTheme == ThemeVariant.Light ? ThemeMode.Light : ThemeMode.System;
    }

    public static ThemeVariant GetThemeVariant(ThemeMode themeMode)
    {
        switch (themeMode)
        {
            case ThemeMode.Dark:
                return ThemeVariant.Dark;
            case ThemeMode.Light:
                return ThemeVariant.Light;
            case ThemeMode.System:
            default:
                return ThemeVariant.Default;
        }
    }

    private static FluentAvaloniaTheme GetTheme() =>
        (FluentAvaloniaTheme)Application.Current!.Styles[0];
}
