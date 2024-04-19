using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;

namespace Shovel.ViewModels;

public sealed partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isLoading;

    [RelayCommand]
    private void Change()
    {
        IsLoading = !IsLoading;
    }

    public MainWindowViewModel(IDialogService dialogService)
        : base(dialogService) { }
}
