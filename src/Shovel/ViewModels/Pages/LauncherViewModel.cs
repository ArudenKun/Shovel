using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed class LauncherViewModel : ViewModelBase
{
    public LauncherViewModel(IDialogService dialogService)
        : base(dialogService) { }

    protected override void HandleLoaded()
    {
        Console.WriteLine("LOADED BITCH");
    }
}
