using CommunityToolkit.Mvvm.ComponentModel;
using HanumanInstitute.MvvmDialogs;

namespace Shovel.ViewModels.Abstractions;

public abstract partial class ViewModelBase : ObservableObject
{
    protected IDialogService DialogService { get; }

    protected ViewModelBase(IDialogService dialogService)
    {
        DialogService = dialogService;
    }
}
