using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using HanumanInstitute.MvvmDialogs;
using Microsoft.Extensions.Logging;
using Shovel.SourceGenerators.Attributes;
using Shovel.ViewModels.Abstractions;
using Shovel.ViewModels.Pages;
using Velopack;

namespace Shovel.ViewModels;

[Singleton]
public sealed partial class MainWindowViewModel : ViewModelBase
{
    private readonly UpdateManager _updateManager;
    private readonly ILogger<MainWindowViewModel> _logger;

    [ObservableProperty]
    private NavigationViewItem _selectedPage = null!;

    public MainWindowViewModel(
        IDialogService dialogService,
        UpdateManager updateManager,
        INavigationPageFactory navigationPageFactory,
        ILogger<MainWindowViewModel> logger
    )
        : base(dialogService)
    {
        _updateManager = updateManager;

        NavigationPageFactory = navigationPageFactory;

        SelectedPage = Menus[0];
        _logger = logger;
    }

    public IReadOnlyList<NavigationViewItem> Menus { get; } =
        [
            new NavigationViewItem
            {
                Content = "Launcher",
                IconSource = new SymbolIconSource { Symbol = Symbol.Play },
                Tag = typeof(LauncherViewModel)
            },
            new NavigationViewItem
            {
                Content = "LunarCore",
                IconSource = new SymbolIconSource { Symbol = Symbol.DrinkCoffee },
                Tag = typeof(LunarCoreViewModel)
            },
            new NavigationViewItem
            {
                Content = "Environment",
                IconSource = new SymbolIconSource { Symbol = Symbol.Book },
                Tag = typeof(EnvironmentViewModel)
            },
            new NavigationViewItem
            {
                Content = "Proxy",
                IconSource = new SymbolIconSource { Symbol = Symbol.Certificate },
                Tag = typeof(ProxyViewModel)
            }
        ];

    public IReadOnlyList<NavigationViewItem> Footers { get; } =
        [
            new NavigationViewItem
            {
                Content = "Settings",
                IconSource = new SymbolIconSource { Symbol = Symbol.Settings },
                Tag = typeof(SettingsViewModel)
            }
        ];

    public INavigationPageFactory NavigationPageFactory { get; }

    protected override async Task HandleLoadedAsync()
    {
        try
        {
            var newVersion = await _updateManager.CheckForUpdatesAsync();
            if (newVersion is not null)
            {
                await DialogService.ShowMessageBoxAsync(
                    this,
                    $"{newVersion.TargetFullRelease.Version.ToFullString()} is available",
                    "Update"
                );
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Update error");
        }
    }

    partial void OnSelectedPageChanged(NavigationViewItem? oldValue, NavigationViewItem newValue)
    {
        if (oldValue is not null)
        {
            var oldIcon = (SymbolIconSource)oldValue.IconSource;
            oldIcon.IsFilled = false;
        }

        var newIcon = (SymbolIconSource)newValue.IconSource;
        newIcon.IsFilled = true;
    }
}
