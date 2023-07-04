﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace AirControl.Convertors;

public class ClipConverter : MarkupExtension, IMultiValueConverter
{
    private const double DBL_EPSILON = 2.2204460492503131e-016;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length > 1 && values[0] is double width && values[1] is double height)
        {
            if (width < 1.0 || height < 1.0) return Geometry.Empty;

            CornerRadius cornerRadius = default;
            Thickness borderThickness = default;
            if (values.Length > 2 && values[2] is CornerRadius radius)
            {
                cornerRadius = radius;
                if (values.Length > 3 && values[3] is Thickness thickness) borderThickness = thickness;
            }

            // var geometry = GetRoundRectangle(new Rect(0, 0, width, height), borderThickness, cornerRadius);
            // geometry.Freeze();
            var radii = new Radii(cornerRadius, borderThickness, true);
            var streamGeometry = new StreamGeometry();
            using var streamGeometryContext = streamGeometry.Open();
            GenerateGeometry(streamGeometryContext, new Rect(0, 0, width, height), radii);
            streamGeometry.Freeze();
            return streamGeometry;
        }

        return DependencyProperty.UnsetValue;
    }

    public object[]? ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return default;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    private static bool IsZero(double value)
    {
        return Math.Abs(value) < 10.0 * DBL_EPSILON;
    }

    private static void GenerateGeometry(StreamGeometryContext ctx, Rect rect, Radii radii)
    {
        //
        //  compute the coordinates of the key points
        //

        var topLeft = new Point(radii.LeftTop, 0);
        var topRight = new Point(rect.Width - radii.RightTop, 0);
        var rightTop = new Point(rect.Width, radii.TopRight);
        var rightBottom = new Point(rect.Width, rect.Height - radii.BottomRight);
        var bottomRight = new Point(rect.Width - radii.RightBottom, rect.Height);
        var bottomLeft = new Point(radii.LeftBottom, rect.Height);
        var leftBottom = new Point(0, rect.Height - radii.BottomLeft);
        var leftTop = new Point(0, radii.TopLeft);

        //
        //  check keypoints for overlap and resolve by partitioning radii according to
        //  the percentage of each one.  
        //

        //  top edge is handled here
        if (topLeft.X > topRight.X)
        {
            var v = radii.LeftTop / (radii.LeftTop + radii.RightTop) * rect.Width;
            topLeft.X = v;
            topRight.X = v;
        }

        //  right edge
        if (rightTop.Y > rightBottom.Y)
        {
            var v = radii.TopRight / (radii.TopRight + radii.BottomRight) * rect.Height;
            rightTop.Y = v;
            rightBottom.Y = v;
        }

        //  bottom edge
        if (bottomRight.X < bottomLeft.X)
        {
            var v = radii.LeftBottom / (radii.LeftBottom + radii.RightBottom) * rect.Width;
            bottomRight.X = v;
            bottomLeft.X = v;
        }

        // left edge
        if (leftBottom.Y < leftTop.Y)
        {
            var v = radii.TopLeft / (radii.TopLeft + radii.BottomLeft) * rect.Height;
            leftBottom.Y = v;
            leftTop.Y = v;
        }

        //
        //  add on offsets
        //

        var offset = new Vector(rect.TopLeft.X, rect.TopLeft.Y);
        topLeft += offset;
        topRight += offset;
        rightTop += offset;
        rightBottom += offset;
        bottomRight += offset;
        bottomLeft += offset;
        leftBottom += offset;
        leftTop += offset;

        //
        //  create the border geometry
        //
        ctx.BeginFigure(topLeft, true /* is filled */, true /* is closed */);

        // Top line
        ctx.LineTo(topRight, true /* is stroked */, false /* is smooth join */);

        // Upper-right corner
        var radiusX = rect.TopRight.X - topRight.X;
        var radiusY = rightTop.Y - rect.TopRight.Y;
        if (!IsZero(radiusX)
            || !IsZero(radiusY))
            ctx.ArcTo(rightTop, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);

        // Right line
        ctx.LineTo(rightBottom, true /* is stroked */, false /* is smooth join */);

        // Lower-right corner
        radiusX = rect.BottomRight.X - bottomRight.X;
        radiusY = rect.BottomRight.Y - rightBottom.Y;
        if (!IsZero(radiusX)
            || !IsZero(radiusY))
            ctx.ArcTo(bottomRight, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);

        // Bottom line
        ctx.LineTo(bottomLeft, true /* is stroked */, false /* is smooth join */);

        // Lower-left corner
        radiusX = bottomLeft.X - rect.BottomLeft.X;
        radiusY = rect.BottomLeft.Y - leftBottom.Y;
        if (!IsZero(radiusX)
            || !IsZero(radiusY))
            ctx.ArcTo(leftBottom, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);

        // Left line
        ctx.LineTo(leftTop, true /* is stroked */, false /* is smooth join */);

        // Upper-left corner
        radiusX = topLeft.X - rect.TopLeft.X;
        radiusY = leftTop.Y - rect.TopLeft.Y;
        if (!IsZero(radiusX)
            || !IsZero(radiusY))
            ctx.ArcTo(topLeft, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
    }

    private struct Radii
    {
        internal Radii(CornerRadius radii, Thickness borders, bool outer)
        {
            var left = 0.5 * borders.Left;
            var top = 0.5 * borders.Top;
            var right = 0.5 * borders.Right;
            var bottom = 0.5 * borders.Bottom;

            if (outer)
            {
                if (IsZero(radii.TopLeft))
                {
                    LeftTop = TopLeft = 0.0;
                }
                else
                {
                    LeftTop = radii.TopLeft + left;
                    TopLeft = radii.TopLeft + top;
                }

                if (IsZero(radii.TopRight))
                {
                    TopRight = RightTop = 0.0;
                }
                else
                {
                    TopRight = radii.TopRight + top;
                    RightTop = radii.TopRight + right;
                }

                if (IsZero(radii.BottomRight))
                {
                    RightBottom = BottomRight = 0.0;
                }
                else
                {
                    RightBottom = radii.BottomRight + right;
                    BottomRight = radii.BottomRight + bottom;
                }

                if (IsZero(radii.BottomLeft))
                {
                    BottomLeft = LeftBottom = 0.0;
                }
                else
                {
                    BottomLeft = radii.BottomLeft + bottom;
                    LeftBottom = radii.BottomLeft + left;
                }
            }
            else
            {
                LeftTop = Math.Max(0.0, radii.TopLeft - left);
                TopLeft = Math.Max(0.0, radii.TopLeft - top);
                TopRight = Math.Max(0.0, radii.TopRight - top);
                RightTop = Math.Max(0.0, radii.TopRight - right);
                RightBottom = Math.Max(0.0, radii.BottomRight - right);
                BottomRight = Math.Max(0.0, radii.BottomRight - bottom);
                BottomLeft = Math.Max(0.0, radii.BottomLeft - bottom);
                LeftBottom = Math.Max(0.0, radii.BottomLeft - left);
            }
        }

        internal readonly double LeftTop;
        internal readonly double TopLeft;
        internal readonly double TopRight;
        internal readonly double RightTop;
        internal readonly double RightBottom;
        internal readonly double BottomRight;
        internal readonly double BottomLeft;
        internal readonly double LeftBottom;
    }
}