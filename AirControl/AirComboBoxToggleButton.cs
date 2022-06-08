using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AirControl
{
    public class AirComboBoxToggleButton : ToggleButton
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(AirComboBoxToggleButton), new PropertyMetadata(default(CornerRadius)));

        static AirComboBoxToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirComboBoxToggleButton),
                new FrameworkPropertyMetadata(typeof(AirComboBoxToggleButton)));
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

    }
}