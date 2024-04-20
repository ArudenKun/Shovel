using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public class LauncherViewModel : PageViewModelBase
{
    public LauncherViewModel(IDialogService dialogService)
        : base(dialogService) { }
}
