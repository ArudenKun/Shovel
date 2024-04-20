using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel(IDialogService dialogService)
        : base(dialogService) { }
}
