using Shovel.SourceGenerators.Attributes;
using Shovel.ViewModels.Abstractions;
using Shovel.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Shovel.ViewModels.Windows;

[Singleton]
public sealed class MainWindowViewModel : ViewModelBase
{
    private readonly IContentDialogService _contentDialogService;

    public MainWindowViewModel(
        IEnumerable<PageViewModelBase> pages,
        IContentDialogService contentDialogService
    )
    {
        _contentDialogService = contentDialogService;

        pages = pages.ToArray();
        MenuItems = pages
            .Where(x => !x.IsFooter)
            .OrderBy(x => x.Index)
            .Select(x => new NavigationViewItem
            {
                Content = x.Name,
                Icon = x.Icon,
                TargetPageType = x.PageType
            });

        FooterMenuItems = pages
            .Where(x => x.IsFooter)
            .OrderBy(x => x.Index)
            .Select(x => new NavigationViewItem
            {
                Content = x.Name,
                Icon = x.Icon,
                TargetPageType = x.PageType
            });
    }

    public IEnumerable<NavigationViewItem> MenuItems { get; }

    public IEnumerable<object> FooterMenuItems { get; }

    // partial void OnSelectedPageChanged(INavigationViewItem? oldValue, INavigationViewItem newValue)
    // {
    //     if (oldValue is not null)
    //     {
    //         var oldIcon = (SymbolIcon)oldValue.Icon!;
    //         oldIcon.Filled = false;
    //     }
    //
    //     var newIcon = (SymbolIcon)newValue.Icon!;
    //     newIcon.Filled = true;
    // }
}
