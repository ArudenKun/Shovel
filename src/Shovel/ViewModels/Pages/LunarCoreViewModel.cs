using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public class LunarCoreViewModel : PageViewModelBase
{
    public LunarCoreViewModel(IDialogService dialogService)
        : base(dialogService) { }
}
