using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirTabItem : TabItem
    {
        static AirTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTabItem), new FrameworkPropertyMetadata(typeof(AirTabItem)));
        }
    }
}
