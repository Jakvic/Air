using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AirControl;

/// <summary>
///     A border that clips its contents.
/// </summary>
public class AirBorder : Border
{
    private readonly RectangleGeometry _clipRect = new();

    private void DoClipX()
    {
        if (Child is null)
        {
            return;
        }

        _clipRect.RadiusX = _clipRect.RadiusY = Math.Max(0d, CornerRadius.TopLeft - BorderThickness.Left * 0.5);
        _clipRect.Rect = new Rect(Child.RenderSize);
        Child.Clip = _clipRect;
    }

    protected override void OnRender(DrawingContext dc)
    {
        DoClipX();
        base.OnRender(dc);
    }
}