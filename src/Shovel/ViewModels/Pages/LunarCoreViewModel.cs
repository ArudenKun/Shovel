using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed class LunarCoreViewModel : ViewModelBase
{
    public LunarCoreViewModel(IDialogService dialogService)
        : base(dialogService) { }
}
