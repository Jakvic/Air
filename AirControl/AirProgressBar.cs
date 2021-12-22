using System;
using System.ComponentModel;
using System.Windows;
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
            "IsIndeterminate", typeof(bool), typeof(AirProgressBar), new PropertyMetadata(default(bool),PropertyChangedCallback));

        public new bool IsIndeterminate
        {
            get => (bool) GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }
        
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
            };
        }
        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var airProgressBar = d as AirProgressBar;
            airProgressBar?.DoAnimation();
        }

        /// <summary>
        ///     THe `Indicator` & `Animation` Rectangle's RadiusX binding CornerRadius.TopLeft
        ///     RadiusY binding CornerRadius.TopRight
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private Border border;
        private Rectangle rect;
        private Thickness rawRectMargin;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            border = (GetTemplateChild("PART_Border") as Border)!;
            rect = (GetTemplateChild("Animation") as Rectangle)!;
            rawRectMargin = rect.Margin;
            rawRectMargin.Left -= rect.Width;
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
            
            rect.Width = border.ActualWidth / 4;
            Storyboard sb = new(){RepeatBehavior = RepeatBehavior.Forever};
            var thicknessAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
                From = rawRectMargin,
                To = new Thickness(border.Width, rect.Margin.Top,
                    -rect.Width, rect.Margin.Bottom)
            };
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            sb.Children.Add(thicknessAnimation);
            sb.Begin(rect);
        }
    }
}