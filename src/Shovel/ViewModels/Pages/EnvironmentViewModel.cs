using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed class EnvironmentViewModel : ViewModelBase
{
    public EnvironmentViewModel(IDialogService dialogService)
        : base(dialogService) { }
}
