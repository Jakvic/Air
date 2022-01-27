using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Converters;

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

        public static readonly DependencyProperty SpaceProperty = DependencyProperty.Register(
            "Space", typeof(double), typeof(AirPanel),
            new FrameworkPropertyMetadata(0d,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public PanelType Type
        {
            get => (PanelType) GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public double Space
        {
            get => (double) GetValue(SpaceProperty);
            set => SetValue(SpaceProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return Type == PanelType.Horizontal
                ? HorizontalMeasure(availableSize)
                : VerticalMeasure(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Type == PanelType.Horizontal)
            {
                HorizontalArrange(finalSize);
            }
            else
            {
                VerticalArrange(finalSize);
            }

            return finalSize;
        }

        private Size HorizontalMeasure(Size availableSize)
        {
            var width = 0d;
            var height = 0d;
            var size = new Size();
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(availableSize.Width,availableSize.Height));
                if (child.DesiredSize.Height > height)
                {
                    height = child.DesiredSize.Height;
                }

                width += child.DesiredSize.Width + Space;
            }

            size.Width = double.IsPositiveInfinity(availableSize.Width) ? width : availableSize.Width;
            size.Height = double.IsPositiveInfinity(availableSize.Height) ? height : availableSize.Height;
            
        }

        private void HorizontalArrange(Size size)
        {
           
        }
        
        
        private Size VerticalMeasure(Size size)
        {
          
        }
        
        private void VerticalArrange(Size size)
        {
          
        }
    }
}