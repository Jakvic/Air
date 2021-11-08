using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AirControl
{
    public enum PanelType
    {
        Horizontal,
        Vertical
    }

    public class AirPanel : Panel
    {
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(PanelType), typeof(AirPanel), new PropertyMetadata(default(PanelType)));

        public PanelType Type
        {
            get => (PanelType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty SpaceProperty = DependencyProperty.Register(
            "Space", typeof(double), typeof(AirPanel),
            new FrameworkPropertyMetadata(0d,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Space
        {
            get => (double)GetValue(SpaceProperty);
            set => SetValue(SpaceProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return Type == PanelType.Horizontal
                ? HorizontalLayout(availableSize, true)
                : VerticalLayout(availableSize, true);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Type == PanelType.Horizontal)
            {
                HorizontalLayout(finalSize, false);
            }
            else
            {
                VerticalLayout(finalSize, false);
            }

            return finalSize;
        }

        private Size HorizontalLayout(Size size, bool isMeasure)
        {
            var result = default(Size);
            var isInfinity = double.IsInfinity(size.Height);
            result.Height = isInfinity ? 0 : size.Height;
            foreach (UIElement child in InternalChildren)
            {
                if (child is Popup || child.Visibility is Visibility.Collapsed)
                {
                    continue;
                }

                if (isMeasure)
                {
                    child.Measure(new Size(double.PositiveInfinity, size.Height));
                }
                else
                {
                    child.Arrange(new Rect(result.Width, 0, child.DesiredSize.Width, size.Height));
                }

                result.Width += child.DesiredSize.Width + Space;

                if (isInfinity)
                {
                    result.Height = Math.Max(result.Height, child.DesiredSize.Height);
                }
                else
                {
                    result.Height = child.DesiredSize.Height;
                }
            }

            if (result.Width > 0)
            {
                result.Width -= Space;
            }

            result.Width = Math.Max(0, result.Width);
            result.Height = Math.Max(0, result.Height);

            return result;
        }

        private Size VerticalLayout(Size size, bool isMeasure)
        {
            var result = default(Size);
            var isInfinity = double.IsInfinity(size.Width);
            result.Width = isInfinity ? 0 : size.Width;

            foreach (UIElement child in InternalChildren)
            {
                if (child is Popup || child.Visibility is Visibility.Collapsed)
                {
                    continue;
                }

                if (isMeasure)
                {
                    child.Measure(new Size(size.Width, double.PositiveInfinity));
                }
                else
                {
                    child.Arrange(new Rect(0, result.Height, size.Width, child.DesiredSize.Height));
                }

                result.Height += child.DesiredSize.Height + Space;

                if (isInfinity)
                {
                    result.Width = Math.Max(result.Width, child.DesiredSize.Width);
                }
                else
                {
                    result.Width = child.DesiredSize.Width;
                }
            }

            if (result.Height > 0)
            {
                result.Height -= Space;
            }

            result.Width = Math.Max(0, result.Width);
            result.Height = Math.Max(0, result.Height);

            return result;
        }
    }
}