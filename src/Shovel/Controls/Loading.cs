using Avalonia.Controls;
using Avalonia.Media;
using DependencyPropertyGenerator;

namespace Shovel.Controls;

[DependencyProperty<IBrush>("Foreground", DefaultValueExpression = "Avalonia.Media.Brushes.Aqua")]
public partial class Loading : ContentControl;