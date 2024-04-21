using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;

namespace Shovel.Views.Abstractions;

public abstract class UserControlBase<TViewModel> : UserControl
    where TViewModel : class, INotifyPropertyChanged
{
    public TViewModel ViewModel { get; }

    protected UserControlBase()
    {
        DataContext = ViewModel = Ioc.Default.GetRequiredService<TViewModel>();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (ViewModel is IViewLoaded vm)
        {
            vm.OnLoaded();
        }
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);

        if (ViewModel is IViewClosed vm)
        {
            vm.OnClosed();
        }
    }
}
