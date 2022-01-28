using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            "ProgressValue", typeof(double), typeof(Loading), new PropertyMetadata(40d));

        static Loading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loading), new FrameworkPropertyMetadata(typeof(Loading)));
        }

        public double ProgressValue
        {
            get => (double) GetValue(ProgressValueProperty);
            set => SetValue(ProgressValueProperty, value);
        }

        public double Diameter
        {
            get => (double) GetValue(DiameterProperty);
            set => SetValue(DiameterProperty, value);
        }

        public bool IsLoading
        {
            get => (bool) GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public new double BorderThickness
        {
            get => (double) GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }


        public new SolidColorBrush BorderBrush
        {
            get => (SolidColorBrush) GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var ellipse = GetTemplateChild("PART_Ellipse") as Ellipse;
            var perimeter = Math.PI * Diameter;

            if (!(Diameter >= 0) && !(BorderThickness >= 0))
            {
                return;
            }

            var lineLength = perimeter * (Math.Floor(ProgressValue) / 100);
            var gapLength = perimeter - lineLength;
            ellipse!.StrokeDashArray = new DoubleCollection(new[]
            {
                lineLength / BorderThickness, gapLength / BorderThickness
            });
        }
    }
}