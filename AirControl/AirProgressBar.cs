using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AirControl;

public class AirProgressBar : ProgressBar
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(AirProgressBar),
            new PropertyMetadata(default(CornerRadius)));

    public new static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
        nameof(IsIndeterminate), typeof(bool), typeof(AirProgressBar),
        new PropertyMetadata(default(bool), PropertyChangedCallback));

    public new static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(double), typeof(AirProgressBar),
        new PropertyMetadata(default(double), ProgressValueChanged));

    private Border? _border;
    private Border? _indicator;

    static AirProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirProgressBar),
            new FrameworkPropertyMetadata(typeof(AirProgressBar)));
    }

    public AirProgressBar()
    {
        SizeChanged += (_, _) =>
        {
            DoAnimation();
            CalcWidth();
        };
    }

    public new double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public new bool IsIndeterminate
    {
        get => (bool)GetValue(IsIndeterminateProperty);
        set => SetValue(IsIndeterminateProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    private static void ProgressValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var airProgressBar = d as AirProgressBar;
        airProgressBar?.CalcWidth();
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var airProgressBar = d as AirProgressBar;
        airProgressBar?.DoAnimation();
        airProgressBar?.CalcWidth();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _border = (GetTemplateChild("PART_Border") as Border)!;
        _indicator = (GetTemplateChild("PART_Indicator") as Border)!;
        if (_border.CornerRadius is { TopLeft: >= 1 })
        {
            _indicator.CornerRadius = new CornerRadius(CornerRadius.TopLeft - BorderThickness.Left,
                CornerRadius.TopRight - BorderThickness.Top,
                CornerRadius.BottomRight - BorderThickness.Right, CornerRadius.BottomLeft - BorderThickness.Bottom);
        }
    }

    private void CalcWidth()
    {
        if (IsIndeterminate)
        {
            return;
        }

        if (_border is null)
        {
            return;
        }

        Value = Math.Max(0d, Value);
        var percentage = Value / 100;
        if (_indicator != null)
        {
            _indicator.Width = _border.ActualWidth * percentage;
        }
    }

    private void DoAnimation()
    {
        if (IsIndeterminate is false)
        {
            return;
        }

        if (_border is null)
        {
            return;
        }

        if (_indicator is null)
        {
            return;
        }

        _indicator.Width = ActualWidth / 4;
        Storyboard sb = new() { RepeatBehavior = RepeatBehavior.Forever };
        var thicknessAnimation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
            From = new Thickness(-_indicator.Width, _indicator.Margin.Top,
                _indicator.Margin.Right, _indicator.Margin.Bottom),
            To = new Thickness(Width, _indicator.Margin.Top,
                -_indicator.Width, _indicator.Margin.Bottom)
        };
        Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
        sb.Children.Add(thicknessAnimation);
        sb.Begin(_indicator);
    }
}