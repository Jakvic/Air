using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirMenuItem : MenuItem
{
    static AirMenuItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirMenuItem),
            new FrameworkPropertyMetadata(typeof(AirMenuItem)));
    }
    
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(AirMenuItem), new PropertyMetadata(default(CornerRadius)));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}