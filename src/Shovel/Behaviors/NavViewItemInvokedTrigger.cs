using Avalonia.Xaml.Interactivity;
using FluentAvalonia.UI.Controls;

namespace Shovel.Behaviors;

public sealed class NavViewItemInvokedBehavior : Trigger<NavigationView>
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Trimming",
        "IL2046:'RequiresUnreferencedCodeAttribute' annotations must match across all interface implementations or overrides.",
        Justification = "<Pending>"
    )]
    protected override void OnAttached()
    {
        ArgumentNullException.ThrowIfNull(AssociatedObject);
        AssociatedObject.ItemInvoked += AssociatedObjectOnItemInvoked;
    }

    protected override void OnDetaching()
    {
        ArgumentNullException.ThrowIfNull(AssociatedObject);
        AssociatedObject.ItemInvoked -= AssociatedObjectOnItemInvoked;
    }

    private void AssociatedObjectOnItemInvoked(object? sender, NavigationViewItemInvokedEventArgs e)
    {
        Interaction.ExecuteActions(AssociatedObject, Actions, e);
    }
}
