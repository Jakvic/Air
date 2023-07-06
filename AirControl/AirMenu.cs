using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirMenu : Menu
{
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(AirMenu), new PropertyMetadata(default(CornerRadius)));

    static AirMenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirMenu), new FrameworkPropertyMetadata(typeof(AirMenu)));
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}