using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using HanumanInstitute.MvvmDialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Shovel.Services;

public sealed class NavigationPageFactory : INavigationPageFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewLocator _viewLocator;

    public NavigationPageFactory(IServiceProvider serviceProvider, IViewLocator viewLocator)
    {
        _serviceProvider = serviceProvider;
        _viewLocator = viewLocator;
    }

    public Control GetPage(Type srcType) =>
        (Control)_viewLocator.Create(_serviceProvider.GetRequiredService(srcType));

    public Control GetPageFromObject(object target) => (Control)_viewLocator.Create(target);
}
