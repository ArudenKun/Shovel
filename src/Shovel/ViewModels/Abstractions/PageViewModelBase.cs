using Humanizer;
using Wpf.Ui.Controls;

namespace Shovel.ViewModels.Abstractions;

public abstract class PageViewModelBase : ViewModelBase
{
    public virtual string Name => GetName();
    public virtual int Index => 0;
    public virtual SymbolIcon Icon => new(SymbolRegular.Home24);
    public virtual bool IsFooter => false;
    public abstract Type PageType { get; }

    protected virtual string GetName() => GetType().Name.Replace("PageViewModel", "").Titleize();
}
