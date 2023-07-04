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
}