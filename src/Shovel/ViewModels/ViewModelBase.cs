using CommunityToolkit.Mvvm.ComponentModel;

namespace Shovel.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    protected ViewModelBase()
    {
        OnLoaded();
    }

    partial void OnLoaded();
}
