using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Avalonia.Threading;
using DependencyPropertyGenerator;
using FluentIcons.Common;

namespace Shovel.Controls;

[DependencyProperty<bool>("AlternativeStyle")]
[DependencyProperty<int>("Index")]
[DependencyProperty<IEnumerable>("Steps")]
public partial class Stepper : TemplatedControl
{
    private Grid? _grid;
    private IDisposable? _subscriptionDisposables;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (e.NameScope.Get<Grid>("PART_GridStepper") is not { } grid)
            return;
        _grid = grid;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        var indexObs = this.GetObservable(IndexProperty)
            .Do(_ => StepsChangedHandler(Steps))
            .Select(_ => Unit.Default);
        _subscriptionDisposables = this.GetObservable(StepsProperty)
            .Do(_ => StepsChangedHandler(Steps))
            .Select(_ => Unit.Default)
            .Merge(indexObs)
            .ObserveOn(new AvaloniaSynchronizationContext())
            .Subscribe();
    }

    private void StepsChangedHandler(IEnumerable? newSteps)
    {
        if (newSteps is not IEnumerable<object> stepsEnumerable)
            return;
        var steps = stepsEnumerable.ToArray();

        if (AlternativeStyle)
            UpdateAlternate(steps);
        else
            Update(steps);

        if (newSteps is INotifyCollectionChanged notify)
            notify.CollectionChanged += (_, _) => Update(steps);
    }

    #region StepperBaseStyle

    private void Update(object[] steps)
    {
        if (_grid is null)
            return;
        _grid.Children.Clear();

        SetColumnDefinitions(_grid, steps);

        for (var i = 0; i < steps.Length; i++)
            AddStep(steps[i], i, _grid, steps.Length);
    }

    private void SetColumnDefinitions(Grid grid, object[] steps)
    {
        var columns = new ColumnDefinitions();
        for (int i = 0; i < steps.Length; i++)
        {
            columns.Add(new ColumnDefinition());
        }

        grid.ColumnDefinitions = columns;
    }

    private void AddStep(object step, int index, Grid grid, int stepCount)
    {
        var griditem = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            ]
        };

        var icon = new SymbolIcon { Symbol = Symbol.ChevronRight };
        if (index == stepCount - 1)
            icon.IsVisible = false;

        Grid.SetColumn(icon, 2);
        griditem.Children.Add(icon);

        var circle = new Border
        {
            Margin = new Thickness(0, 0, 0, 2),
            Height = 24,
            Width = 24,
            CornerRadius = new CornerRadius(25),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        if (index <= Index)
        {
            circle[!BackgroundProperty] = new DynamicResourceExtension(
                "AccentFillColorDefaultBrush"
            );

            circle.BorderThickness = new Thickness(0);
            circle.Child = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (index + 1).ToString(),
                FontSize = 13,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };
        }
        else
        {
            circle[!BackgroundProperty] = new DynamicResourceExtension(
                "ControlStrokeColorOnAccentSecondaryBrush"
            );

            circle.BorderThickness = new Thickness(0);
            circle.Child = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (index + 1).ToString(),
                FontSize = 13,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };
        }

        Grid.SetColumn(circle, 0);
        griditem.Children.Add(circle);
        Control content = step switch
        {
            string s
                => new TextBlock
                {
                    FontWeight = index <= Index ? FontWeight.DemiBold : FontWeight.Normal,
                    Margin = new Thickness(10, 0, 0, 0),
                    Text = s,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    TextWrapping = TextWrapping.Wrap
                },
            _ => new ContentControl { Content = step }
        };

        Grid.SetColumn(content, 1);
        griditem.Children.Add(content);

        Grid.SetColumn(griditem, index);

        grid.Children.Add(griditem);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        _subscriptionDisposables?.Dispose();
    }

    #endregion

    #region StepperAlternateStyle

    public void UpdateAlternate(object[] steps)
    {
        if (_grid is null)
            return;
        _grid.Children.Clear();

        SetColumnDefinitionsAlternate(_grid);

        for (var i = 0; i < steps.Length; i++)
        {
            AddStepAlternate(steps[i], i, _grid, steps);
        }
    }

    private void SetColumnDefinitionsAlternate(Grid grid)
    {
        var columns = new ColumnDefinitions();
        foreach (var unused in Steps!)
            columns.Add(new ColumnDefinition());
        grid.ColumnDefinitions = columns;
    }

    private void AddStepAlternate(object step, int index, Grid grid, object[] steps)
    {
        var gridItem = new Grid
        {
            ColumnDefinitions = [new ColumnDefinition(), new ColumnDefinition()]
        };

        var line = new Border
        {
            CornerRadius = new CornerRadius(3),
            Margin = new Thickness(-5, 0, 23, 0),
            Height = 2,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Center,
            [!BackgroundProperty] = new DynamicResourceExtension(
                "ControlStrokeColorOnAccentSecondaryBrush"
            )
        };
        var line1 = new Border
        {
            CornerRadius = new CornerRadius(3),
            Margin = new Thickness(23, 0, -5, 0),
            Height = 2,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Center,
            [!BackgroundProperty] = new DynamicResourceExtension(
                "ControlStrokeColorOnAccentSecondaryBrush"
            )
        };

        if (index == 0)
            line.IsVisible = false;
        if (index == steps.Length - 1)
            line1.IsVisible = false;

        if (index == Index)
            line[!BackgroundProperty] = new DynamicResourceExtension("AccentFillColorDefaultBrush");

        if (index < Index)
        {
            line1[!BackgroundProperty] = new DynamicResourceExtension(
                "AccentFillColorDefaultBrush"
            );
            line[!BackgroundProperty] = new DynamicResourceExtension("AccentFillColorDefaultBrush");
        }

        Grid.SetColumn(line, 0);
        Grid.SetColumn(line1, 1);

        gridItem.Children.Add(line);
        gridItem.Children.Add(line1);

        var gridBorder = new Grid();

        var circle = new Border
        {
            Margin = new Thickness(0, 0, 0, 2),
            Height = 30,
            Width = 30,
            CornerRadius = new CornerRadius(25),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        if (index == Index)
        {
            circle[!BackgroundProperty] = new DynamicResourceExtension(
                "AccentFillColorDefaultBrush"
            );
            circle.BorderThickness = new Thickness(0);
            circle.Child = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (index + 1).ToString(),
                Foreground = Brushes.White
            };
        }
        else if (index < Index)
        {
            circle.Background = Brushes.Transparent;
            circle.BorderThickness = new Thickness(1.5);
            circle[!BorderBrushProperty] = new DynamicResourceExtension(
                "AccentFillColorDefaultBrush"
            );
            circle.Child = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (index + 1).ToString(),
                [!ForegroundProperty] = new DynamicResourceExtension("AccentFillColorDefaultBrush")
            };
        }
        else
        {
            circle.Background = Brushes.Transparent;
            circle.BorderThickness = new Thickness(1.5);
            circle[!BorderBrushProperty] = new DynamicResourceExtension(
                "ControlStrokeColorOnAccentSecondaryBrush"
            );
            circle.Child = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (index + 1).ToString(),
                [!ForegroundProperty] = new DynamicResourceExtension(
                    "ControlStrokeColorOnAccentSecondaryBrush"
                )
            };
        }

        gridBorder.Children.Add(circle);

        gridBorder.Children.Add(
            new TextBlock
            {
                FontWeight = index == Index ? FontWeight.Medium : FontWeight.Normal,
                Text = step.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 55, 0, 0)
            }
        );

        Grid.SetColumn(gridItem, index);
        Grid.SetColumn(gridBorder, index);
        grid.Children.Add(gridItem);
        grid.Children.Add(gridBorder);
    }

    #endregion
}
