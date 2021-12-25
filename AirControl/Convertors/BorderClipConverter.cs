using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace AirControl.Convertors
{
    public class BorderClipConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 && values[0] is double && values[1] is double && values[2] is CornerRadius)
            {
                var width = (double)values[0];
                var height = (double)values[1];
                if (width < double.Epsilon || height < double.Epsilon)
                {
                    return Geometry.Empty;
                }
                var cornerRadius = (CornerRadius)values[2];
                var clip =new RectangleGeometry(new Rect(0,0,width,height),cornerRadius.TopLeft,cornerRadius.TopLeft);
                clip.Freeze();
                return clip;
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
    }
}