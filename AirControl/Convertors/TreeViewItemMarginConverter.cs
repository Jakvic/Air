using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace AirControl.Convertors;

public class TreeViewItemMarginConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var left = 0.0;
        UIElement? element = value as TreeViewItem;
        while (element != null && element.GetType() != typeof(TreeView))
        {
            element = VisualTreeHelper.GetParent(element) as UIElement;
            if (element is TreeViewItem)
            {
                left += 19.0;
            }
        }

        return new Thickness(left, 0, 0, 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}