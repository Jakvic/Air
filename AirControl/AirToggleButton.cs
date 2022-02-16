using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AirControl
{
    public class AirToggleButton : ToggleButton
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(AirToggleButton), new PropertyMetadata(default(CornerRadius)));

        static AirToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirToggleButton),
                new FrameworkPropertyMetadata(typeof(AirToggleButton)));
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

    }
}