using Avalonia;
using FluentAvalonia.UI.Controls;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HotAvalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.FileEx;
using Shovel.Core;
using Shovel.Core.Helpers;
using Shovel.Core.Models.LunarCore;
using Shovel.Extensions;
using Shovel.Models;
using Shovel.Services;
using Velopack;
using Velopack.Sources;

namespace Shovel;

public static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        ConfigureLogger();

        try
        {
            VelopackApp.Build().Run();
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Initialization Error");
        }
        finally
        {
            Log.CloseAndFlush();
            Environment.Exit(1);
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseMicrosoftDependencyInjection(
                services =>
                {
                    services.AddCore();
                    services.AddSingleton<AppConfig>();
                    services.AddSingleton<LunarCoreConfig>();
                    services.AddSingleton(
                        new UpdateManager(
                            new GithubSource("https://github.com/ArudenKun/Shovel", null, true)
                        )
                    );
                    services.AddSingleton<ViewLocator>();
                    services.AddSingleton<IViewLocator>(sp => sp.GetRequiredService<ViewLocator>());
                    services.AddSingleton<IDialogService>(sp => new DialogService(
                        new DialogManager(
                            sp.GetRequiredService<IViewLocator>(),
                            new DialogFactory().AddFluent()
                        ),
                        sp.GetRequiredService
                    ));
                    services.AddSingleton<INavigationPageFactory, NavigationPageFactory>();
                    services.AddSingleton<IThemeService, ThemeService>();

                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddSerilog(dispose: true);
                    });
                },
                (sp, app) =>
                {
                    app.DataTemplates.Add(sp.GetRequiredService<ViewLocator>());
                    app.EnableHotReload();

                    sp.GetRequiredService<IThemeService>().LoadTheme();
                }
            );

    private static void ConfigureLogger()
    {
        const string OUTPUT_TEMPLATE =
            "[{Timestamp:HH:mm:ss} {Level:u3}][{SourceContext}]: {Message:lj}{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(LogEventLevel.Debug)
            .WriteTo.Console(outputTemplate: OUTPUT_TEMPLATE)
            .WriteTo.FileEx(
                EnvironmentHelper.GetApplicationDataPath("logs", "logs.log"),
                "_yyyy-MM-dd",
                outputTemplate: OUTPUT_TEMPLATE,
                rollOnFileSizeLimit: true,
                rollingInterval: RollingInterval.Day
            )
            .Enrich.FromLogContext()
            .CreateLogger();
    }
}
