using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed partial class LunarCoreViewModel : ViewModelBase
{
    public LunarCoreViewModel(IDialogService dialogService)
        : base(dialogService) { }

    [RelayCommand]
    private async Task CloneLunarCore()
    {
        await DialogService.ShowMessageBoxAsync(
            DialogService.CreateViewModel<MainWindowViewModel>(),
            "Finished Cloning",
            "Clone"
        );
    }
}
