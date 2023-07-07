using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirDataGrid : DataGrid
{
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(AirDataGrid), new PropertyMetadata(default(CornerRadius)));

    static AirDataGrid()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirDataGrid),
            new FrameworkPropertyMetadata(typeof(AirDataGrid)));
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}