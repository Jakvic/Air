using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirTreeView : TreeView
    {
        static AirTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTreeView),
                new FrameworkPropertyMetadata(typeof(AirTreeView)));
        }
    }
}