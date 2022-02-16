using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AirControl.Convertors
{
    public class CornerRadius2RectRadiusConverter : MarkupExtension, IValueConverter
    {

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is CornerRadius cornerRadius)
            {
                return cornerRadius.TopLeft;

            }
            return DependencyProperty.UnsetValue;

        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return default;
        }
    }
}