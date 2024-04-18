using Shovel.ViewModels.Abstractions;
using Shovel.Views.Pages;
using Wpf.Ui.Controls;

namespace Shovel.ViewModels.Pages;

public sealed class LauncherPageViewModel : PageViewModelBase
{
    public override SymbolIcon Icon => new(SymbolRegular.Home24);
    public override Type PageType => typeof(LauncherPage);
}
