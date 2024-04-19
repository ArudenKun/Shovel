using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
}