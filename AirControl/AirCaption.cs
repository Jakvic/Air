using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirCaption : ContentControl
{
    static AirCaption()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirCaption), new FrameworkPropertyMetadata(typeof(AirCaption)));
    }

    public static readonly DependencyProperty SystemButtonWidthProperty = DependencyProperty.Register(
        nameof(SystemButtonWidth), typeof(double), typeof(AirCaption), new PropertyMetadata(default(double)));

    public double SystemButtonWidth
    {
        get => (double)GetValue(SystemButtonWidthProperty);
        set => SetValue(SystemButtonWidthProperty, value);
    }
}