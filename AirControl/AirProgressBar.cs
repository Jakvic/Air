using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

        private Border border;
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
            };
        }

        public double Value
        {
            get => (double) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public new bool IsIndeterminate
        {
            get => (bool) GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void ProgressValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var airProgressBar = d as AirProgressBar;
            airProgressBar.CalcWidth();
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
            border = (GetTemplateChild("PART_Border") as Border)!;
            indicator = (GetTemplateChild("PART_Indicator") as Border)!;
            if (border.CornerRadius is { } radius && radius.TopLeft >= 1)
            {
                indicator.CornerRadius = new CornerRadius(CornerRadius.TopLeft - BorderThickness.Left,
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

            if (border is null)
            {
                return;
            }

            Value = Math.Max(0d, Value);
            var percentage = Value / 100;
            indicator.Width = border.ActualWidth * percentage;
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
            Storyboard sb = new() {RepeatBehavior = RepeatBehavior.Forever};
            var thicknessAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
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