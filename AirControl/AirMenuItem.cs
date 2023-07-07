using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirMenuItem : MenuItem
{
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(AirMenuItem), new PropertyMetadata(default(CornerRadius)));

    static AirMenuItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirMenuItem),
            new FrameworkPropertyMetadata(typeof(AirMenuItem)));
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}