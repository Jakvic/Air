using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirComboBox : ComboBox
{
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        "CornerRadius", typeof(CornerRadius), typeof(AirComboBox), new PropertyMetadata(default(CornerRadius)));

    static AirComboBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirComboBox),
            new FrameworkPropertyMetadata(typeof(AirComboBox)));
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}