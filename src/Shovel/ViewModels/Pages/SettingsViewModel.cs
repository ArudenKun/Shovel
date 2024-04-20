using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed class SettingsViewModel : PageViewModelBase, IViewLoaded
{
    public SettingsViewModel(IDialogService dialogService)
        : base(dialogService) { }

    public void OnLoaded()
    {
        Console.WriteLine("LOADED");
    }
}
