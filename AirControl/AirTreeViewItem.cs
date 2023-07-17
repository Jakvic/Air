using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirTreeViewItem : TreeViewItem
{
    static AirTreeViewItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTreeViewItem),
            new FrameworkPropertyMetadata(typeof(AirTreeViewItem)));
    }


    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AirTreeViewItem), new PropertyMetadata(0));

}