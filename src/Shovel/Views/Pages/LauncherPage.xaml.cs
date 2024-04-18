using Shovel.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Shovel.Views.Pages;

public sealed partial class LauncherPage : INavigableView<LauncherPageViewModel>
{
    public LauncherPageViewModel ViewModel { get; }

    public LauncherPage(LauncherPageViewModel viewModel)
    {
        ViewModel = viewModel;
        
        InitializeComponent();
    }

}