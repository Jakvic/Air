using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AirControl.Convertors
{
    public enum Corner
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }


    public class CornerRadius2CornerConverter : MarkupExtension, IValueConverter
    {
        public bool UseCornerRadius { get; set; }

        public Corner Corner { get; set; } = Corner.TopLeft;

        /// <summary>
        ///     Covert CornerRadius to 1 Corner, or Corner
        /// </summary>
        /// <param name="value">CorneRadius</param>
        /// <param name="targetType">who cares</param>
        /// <param name="parameter">TopLeft,TopRight,BottomRight,BottomLeft,-1:no change</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not CornerRadius cornerRadius)
            {
                return DependencyProperty.UnsetValue;
            }

            if (!UseCornerRadius)
            {
                return Corner switch
                {
                    Corner.TopLeft => cornerRadius.TopLeft,
                    Corner.TopRight => cornerRadius.TopRight,
                    Corner.BottomLeft => cornerRadius.BottomLeft,
                    Corner.BottomRight => cornerRadius.BottomRight,
                    _ => cornerRadius.TopLeft
                };
            }

            var radius = cornerRadius;
            var s = (string)parameter;
            var strings = s.Split('|');
            var topLeft = System.Convert.ToInt32(strings[0]);
            var topRight = System.Convert.ToInt32(strings[1]);
            var bottomRight = System.Convert.ToInt32(strings[2]);
            var bottomLeft = System.Convert.ToInt32(strings[3]);
            return new CornerRadius(topLeft == -1 ? radius.TopLeft : topLeft,
                topRight == -1 ? radius.TopRight : topRight,
                bottomRight == -1 ? radius.BottomRight : bottomRight,
                bottomLeft == -1 ? radius.BottomLeft : bottomLeft);
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
}