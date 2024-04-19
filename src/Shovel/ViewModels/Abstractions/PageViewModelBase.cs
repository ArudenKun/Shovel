using HanumanInstitute.MvvmDialogs;

namespace Shovel.ViewModels.Abstractions;

public abstract class PageViewModelBase : ViewModelBase
{
    protected PageViewModelBase(IDialogService dialogService)
        : base(dialogService) { }
}
