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
using Microsoft.Extensions.DependencyInjection;
using Shovel.Core;
using Shovel.Services;
using Shovel.ViewModels;
using Velopack;
using Velopack.Sources;

namespace Shovel;

public sealed class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var dialogService = BuildServices();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            dialogService.Show(null, dialogService.CreateViewModel<MainWindowViewModel>());
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
