using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AirControl;

public enum PanelType
{
    Horizontal,
    HorizontalFull,
    Vertical,
    VerticalFull
}

public class AirPanel : Panel
{
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(PanelType), typeof(AirPanel), new PropertyMetadata(default(PanelType)));

    public static readonly DependencyProperty SpaceProperty = DependencyProperty.Register(
        nameof(Space), typeof(double), typeof(AirPanel),
        new FrameworkPropertyMetadata(0d,
            FrameworkPropertyMetadataOptions.AffectsArrange |
            FrameworkPropertyMetadataOptions.AffectsMeasure));

    public PanelType Type
    {
        get => (PanelType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public double Space
    {
        get => (double)GetValue(SpaceProperty);
        set => SetValue(SpaceProperty, value);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        Size size;
        switch (Type)
        {
            case PanelType.Horizontal:
                size = HorizontalMeasure(availableSize);
                break;
            case PanelType.HorizontalFull:
                size = HorizontalFullMeasure(availableSize);
                break;
            case PanelType.Vertical:
                size = VerticalMeasure(availableSize);
                break;
            case PanelType.VerticalFull:
                size = VerticalFullMeasure(availableSize);
                break;
        }

        return size;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        switch (Type)
        {
            case PanelType.Horizontal:
            case PanelType.HorizontalFull:
                HorizontalArrange(finalSize);
                break;
            case PanelType.Vertical:
            case PanelType.VerticalFull:
                VerticalArrange(finalSize);
                break;
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
            if (child is Popup) continue;

            child.Measure(new Size(availableSize.Width, availableSize.Height));
            height = Math.Max(height, child.DesiredSize.Height);
            width += child.DesiredSize.Width + Space;
        }

        width -= Space;
        size.Width = Math.Min(width, availableSize.Width);
        size.Height = Math.Min(height, availableSize.Height);
        //size.Height = double.IsPositiveInfinity(availableSize.Height) ? height : availableSize.Height;
        return size;
    }

    private Size HorizontalFullMeasure(Size availableSize)
    {
        var width = 0d;
        var height = 0d;
        var size = new Size();
        foreach (UIElement child in InternalChildren)
        {
            if (child is Popup) continue;

            child.Measure(new Size(availableSize.Width, availableSize.Height));
            height = Math.Max(height, child.DesiredSize.Height);
            width += child.DesiredSize.Width + Space;
        }

        width -= Space;
        size.Width = double.IsPositiveInfinity(availableSize.Width) ? width : availableSize.Width;
        size.Height = Math.Min(height, availableSize.Height);
        return size;
    }

    private void HorizontalArrange(Size finalSize)
    {
        var rcChild = new Rect(finalSize);
        var previousChildSize = 0d;
        foreach (UIElement child in InternalChildren)
        {
            if (child is Popup) continue;

            rcChild.X += previousChildSize + (previousChildSize == 0 ? 0 : Space);
            previousChildSize = child.DesiredSize.Width;
            rcChild.Width = previousChildSize;
            rcChild.Height = Math.Max(finalSize.Height, child.DesiredSize.Height);

            child.Arrange(rcChild);
        }
    }

    private Size VerticalMeasure(Size availableSize)
    {
        var width = 0d;
        var height = 0d;
        var size = new Size();
        foreach (UIElement child in InternalChildren)
        {
            if (child is Popup) continue;

            child.Measure(new Size(availableSize.Width, availableSize.Height));
            width = Math.Max(width, child.DesiredSize.Width);
            height += child.DesiredSize.Height + Space;
        }

        height -= Space;
        //size.Width = double.IsPositiveInfinity(availableSize.Width) ? width : availableSize.Width;
        size.Width = Math.Min(width, availableSize.Width);
        size.Height = Math.Min(height, availableSize.Height);
        return size;
    }

    private Size VerticalFullMeasure(Size availableSize)
    {
        var width = 0d;
        var height = 0d;
        var size = new Size();
        foreach (UIElement child in InternalChildren)
        {
            if (child is Popup) continue;

            child.Measure(new Size(availableSize.Width, availableSize.Height));
            width = Math.Max(width, child.DesiredSize.Width);
            height += child.DesiredSize.Height + Space;
        }

        height -= Space;
        size.Width = Math.Min(width, availableSize.Width);
        size.Height = double.IsPositiveInfinity(availableSize.Height) ? height : availableSize.Height;
        return size;
    }

    private void VerticalArrange(Size finalSize)
    {
        var rcChild = new Rect(finalSize);
        var previousChildSize = 0d;
        foreach (UIElement child in InternalChildren)
        {
            if (child is Popup) continue;

            rcChild.Y += previousChildSize + (previousChildSize == 0 ? 0 : Space);
            previousChildSize = child.DesiredSize.Height;
            rcChild.Height = previousChildSize;
            rcChild.Width = Math.Max(finalSize.Width, child.DesiredSize.Width);
            child.Arrange(rcChild);
        }
    }
}