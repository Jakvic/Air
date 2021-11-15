using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirTextBox : TextBox
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(AirTextBox), new PropertyMetadata(default(CornerRadius)));

        static AirTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTextBox),
                new FrameworkPropertyMetadata(typeof(AirTextBox)));
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}