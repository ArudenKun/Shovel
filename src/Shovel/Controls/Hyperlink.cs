using Avalonia.Controls;
using Avalonia.Logging;
using DependencyPropertyGenerator;
using Shovel.Core.Helpers;

namespace Shovel.Controls;

[DependencyProperty<Uri>("NavigateUri")]
public partial class Hyperlink : Button
{
    public Hyperlink()
    {
        Classes.Add("hyperlink");
    }
    
    protected override void OnClick()
    {
        base.OnClick();

        if (NavigateUri is null) return;

        try
        {
            EnvironmentHelper.OpenUrl(NavigateUri);
        }
        catch
        {
            Logger.TryGet(LogEventLevel.Error, $"Unable to open Uri {NavigateUri}");
        }
    }
}