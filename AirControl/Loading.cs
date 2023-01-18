using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AirControl
{
    public class Loading : Control
    {
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(Loading), new PropertyMetadata(true));

        public new static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(Loading),
                new PropertyMetadata(default));

        public new static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(SolidColorBrush), typeof(Loading),
                new PropertyMetadata(default));


        public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register(
            "Diameter", typeof(double), typeof(Loading), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ProgressValueProperty = DependencyProperty.Register(
            "ProgressValue", typeof(double), typeof(Loading), new PropertyMetadata(40d, PropertyChangedCallback));

        private Ellipse ellipse;

        private double perimeter;
        private Storyboard storyboard;

        static Loading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loading), new FrameworkPropertyMetadata(typeof(Loading)));
        }

        public double ProgressValue
        {
            get => (double)GetValue(ProgressValueProperty);
            set => SetValue(ProgressValueProperty, value);
        }

        public double Diameter
        {
            get => (double)GetValue(DiameterProperty);
            set => SetValue(DiameterProperty, value);
        }

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public new double BorderThickness
        {
            get => (double)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }


        public new SolidColorBrush BorderBrush
        {
            get => (SolidColorBrush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var loading = d as Loading;

            var lineLength = loading.perimeter * (loading.ProgressValue / 100);
            var gapLength = loading.perimeter - lineLength;
            loading.ellipse.StrokeDashArray = new DoubleCollection(new[]
            {
                lineLength / loading.BorderThickness, gapLength / loading.BorderThickness + loading.BorderThickness
            });
            if (Math.Floor(loading.ProgressValue) is 100 or 0)
            {
                loading.storyboard?.Stop();
            }
            else
            {
                var state = loading.storyboard.GetCurrentState();
                if (state is ClockState.Stopped)
                {
                    loading.storyboard?.Begin();
                }
            }
        }

        private void DoAnimation()
        {
            storyboard = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 359.99
            };
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.Angle"));
            Storyboard.SetTarget(animation, ellipse);
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ellipse = GetTemplateChild("PART_Ellipse") as Ellipse;
            perimeter = Math.PI * Diameter;

            if (!(Diameter >= 0) && !(BorderThickness >= 0))
            {
                return;
            }

            var lineLength = perimeter * (ProgressValue / 100);
            var gapLength = perimeter - lineLength;
            ellipse!.StrokeDashArray = new DoubleCollection(new[]
            {
                lineLength / BorderThickness, gapLength / BorderThickness
            });
            DoAnimation();
        }
    }
}