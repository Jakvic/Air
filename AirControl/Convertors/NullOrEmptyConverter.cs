using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AirControl.Convertors;

public class NullOrEmptyConverter : MarkupExtension, IValueConverter
{
    public bool Reversed { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            var nullOrEmpty = !string.IsNullOrWhiteSpace(str);
            return Reversed ? !nullOrEmpty : nullOrEmpty;
        }

        return true;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return default;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}