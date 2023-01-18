using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public enum AirButtonType
    {
        Main,
        Default
    }

    public class AirButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(AirButton), new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(AirButtonType), typeof(AirButton), new PropertyMetadata(default(AirButtonType)));

        static AirButton()
        {
            // see https://stackoverflow.com/questions/1228875/what-is-so-special-about-generic-xaml
            // so I need to create a generic.xaml in themes
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirButton),
                new FrameworkPropertyMetadata(typeof(AirButton)));
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public AirButtonType Type
        {
            get => (AirButtonType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }
    }
}