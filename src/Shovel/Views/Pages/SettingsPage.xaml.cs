using Shovel.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Shovel.Views.Pages;

public partial class SettingsPage : INavigableView<SettingsPageViewModel>
{
    public SettingsPageViewModel ViewModel { get; }

    public SettingsPage(SettingsPageViewModel pageViewModel)
    {
        ViewModel = pageViewModel;

        InitializeComponent();
    }
}
