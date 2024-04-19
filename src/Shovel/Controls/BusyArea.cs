using Avalonia.Controls;
using DependencyPropertyGenerator;

namespace Shovel.Controls;

[DependencyProperty<bool>("IsBusy")]
[DependencyProperty<string>("BusyText")]
public partial class BusyArea : ContentControl;