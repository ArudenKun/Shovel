using Avalonia.Controls;
using Avalonia.Interactivity;
using HanumanInstitute.MvvmDialogs;

namespace Shovel.Views.Abstractions;

public abstract class UserControlBase : UserControl
{
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        if (DataContext is IViewLoaded viewLoaded)
        {
            viewLoaded.OnLoaded();
        }
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);

        if (DataContext is IViewClosed viewClosed)
        {
            viewClosed.OnClosed();
        }
    }
}
