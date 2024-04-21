using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using FluentAvalonia.UI.Controls;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HotAvalonia;
using Microsoft.Extensions.DependencyInjection;
using Shovel.Core;
using Shovel.Services;
using Shovel.ViewModels;
using Velopack;
using Velopack.Sources;

namespace Shovel;

public sealed class App : Application
{
    private IDialogService _dialogService = null!;

    public override void Initialize()
    {
        this.EnableHotReload();
        AvaloniaXamlLoader.Load(this);
        _dialogService = BuildServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            _dialogService.Show(null, _dialogService.CreateViewModel<MainWindowViewModel>());
        }

        base.OnFrameworkInitializationCompleted();
    }

    private IDialogService BuildServices()
    {
        var services = new ServiceCollection();

        services.AddCore();
        services.AddSingleton(
            new UpdateManager(new GithubSource("https://github.com/ArudenKun/Shovel", null, true))
        );
        services.AddSingleton((IViewLocator)DataTemplates.First());
        services.AddSingleton(
            (Func<IServiceProvider, IDialogService>)(
                sp => new DialogService(
                    new DialogManager(
                        sp.GetRequiredService<IViewLocator>(),
                        new DialogFactory().AddFluent()
                    ),
                    sp.GetRequiredService
                )
            )
        );
        services.AddSingleton<INavigationPageFactory, NavigationPageFactory>();

        var serviceProvider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(serviceProvider);
        return serviceProvider.GetRequiredService<IDialogService>();
    }
}
