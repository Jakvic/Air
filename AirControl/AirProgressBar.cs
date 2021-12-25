using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AirControl
{
    public class AirProgressBar : ProgressBar
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AirProgressBar),
                new PropertyMetadata(default(CornerRadius)));

        public new static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
            "IsIndeterminate", typeof(bool), typeof(AirProgressBar),
            new PropertyMetadata(default(bool), PropertyChangedCallback));

        public new static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(AirProgressBar),
            new PropertyMetadata(default(double), ProgressValueChanged));

        private static void ProgressValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var airProgressBar = d as AirProgressBar;
            airProgressBar.CalcWidth();
        }

        private AirBorder border;
        private Border indicator;

        static AirProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirProgressBar),
                new FrameworkPropertyMetadata(typeof(AirProgressBar)));
        }

        public AirProgressBar()
        {
            SizeChanged += (sender, args) =>
            {
                DoAnimation();
                CalcWidth();
                indicator.Height = border.ActualHeight - border.BorderThickness.Top - border.BorderThickness.Bottom;
            };
        }

        public double Value
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

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var airProgressBar = d as AirProgressBar;
            airProgressBar?.DoAnimation();
            airProgressBar?.CalcWidth();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            border = (GetTemplateChild("PART_Border") as AirBorder)!;
            indicator = (GetTemplateChild("PART_Indicator") as Border)!;
            if (border.CornerRadius is { } radius && radius.TopLeft >= 1)
            {
                indicator.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft - 1, border.CornerRadius.TopRight - 1, border.CornerRadius.BottomRight - 1, border.CornerRadius.BottomLeft - 1);
            }
        }

        private void CalcWidth()
        {
            if (IsIndeterminate)
            {
                return;
            }

            if (border is null)
            {
                return;
            }

            Value = Math.Max(0d, Value);
            var percentage = Value / 100;
            indicator.Width = (border.ActualWidth - border.BorderThickness.Left - border.BorderThickness.Right) * percentage;
        }

        private void DoAnimation()
        {
            if (IsIndeterminate is false)
            {
                return;
            }

            if (border is null)
            {
                return;
            }

            indicator.Width = ActualWidth / 4;
            Storyboard sb = new() { RepeatBehavior = RepeatBehavior.Forever };
            var thicknessAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(6000)),
                From = new Thickness(-indicator.Width, indicator.Margin.Top,
                    indicator.Margin.Right, indicator.Margin.Bottom),
                To = new Thickness(Width, indicator.Margin.Top,
                    -indicator.Width, indicator.Margin.Bottom)
            };
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            sb.Children.Add(thicknessAnimation);
            sb.Begin(indicator);

        }
    }
}