using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed class SettingsViewModel : PageViewModelBase
{
    public SettingsViewModel(IDialogService dialogService)
        : base(dialogService) { }

    protected override void HandleLoaded()
    {
        Console.WriteLine("LOADED");
    }
}
