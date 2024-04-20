using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using Shovel.ViewModels.Abstractions;
using Shovel.ViewModels.Pages;

namespace Shovel.ViewModels;

public sealed partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private INotifyPropertyChanged _content = null!;

    [RelayCommand]
    private void Change()
    {
        IsLoading = !IsLoading;
    }

    public MainWindowViewModel(IDialogService dialogService, SettingsViewModel settingsViewModel)
        : base(dialogService)
    {
        Content = settingsViewModel;
    }
}
