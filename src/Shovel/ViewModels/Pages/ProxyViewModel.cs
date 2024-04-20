using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels.Pages;

public sealed class ProxyViewModel : ViewModelBase
{
    public ProxyViewModel(IDialogService dialogService)
        : base(dialogService) { }
}
