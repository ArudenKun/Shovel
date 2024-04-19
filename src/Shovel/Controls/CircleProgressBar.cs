using Avalonia;
using Avalonia.Controls;
using DependencyPropertyGenerator;

namespace Shovel.Controls;

[DependencyProperty<int>(
    "Height",
    DefaultValue = 150,
    DefaultBindingMode = DefaultBindingMode.TwoWay
)]
[DependencyProperty<int>(
    "Width",
    DefaultValue = 150,
    DefaultBindingMode = DefaultBindingMode.TwoWay
)]
[DependencyProperty<int>(
    "StrokeWidth",
    DefaultValue = 10,
    DefaultBindingMode = DefaultBindingMode.TwoWay
)]
[DependencyProperty<bool>(
    "IsIndeterminate",
    DefaultValue = false,
    DefaultBindingMode = DefaultBindingMode.TwoWay
)]
public partial class CircleProgressBar : ContentControl
{
    private double _value = 50;

    public double Value
    {
        get => _value;
        set
        {
            _value = (int)(value * 3.6);
            SetValue(ValueProperty, _value);
        }
    }

    /// <summary>
    /// Defines the <see cref="Value"/> property.
    /// </summary>
    public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<
        CircleProgressBar,
        double
    >(nameof(Value), defaultValue: 50, coerce: (_, d) => d * 3.6);
}
