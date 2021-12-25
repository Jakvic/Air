using System;
using System.ComponentModel;
using System.Runtime.Loader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AirControl
{
    public class AirBorder : Border
    {
        public AirBorder()
        {
            SizeChanged += OnSizeChanged;
            DependencyPropertyDescriptor.FromProperty(CornerRadiusProperty, typeof(AirBorder))
                .AddValueChanged(this, OnCornerRadiusChanged);
        }

        private void OnCornerRadiusChanged(object? sender, EventArgs e)
        {
            DoClip();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            DoClip();
        }

        private void DoClip()
        {
            if (Child is null)
            {
                return;
            }

            Child.Clip ??= new RectangleGeometry();
            var rectangleGeometry = Child.Clip as RectangleGeometry;
            if (rectangleGeometry is not null)
            {
                rectangleGeometry.RadiusX = rectangleGeometry.RadiusY =
                    Math.Max(0.0, CornerRadius.TopLeft - (BorderThickness.Left * 0.5));
                rectangleGeometry.Rect = new Rect(Child.RenderSize);
            }
        }
    }
}