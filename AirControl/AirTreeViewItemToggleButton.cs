using System.Windows;
using System.Windows.Controls.Primitives;

namespace AirControl
{
    public class AirTreeViewItemToggleButton:ToggleButton
    {
        static  AirTreeViewItemToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTreeViewItemToggleButton), new FrameworkPropertyMetadata(typeof(AirTreeViewItemToggleButton)));
        }
    }
}