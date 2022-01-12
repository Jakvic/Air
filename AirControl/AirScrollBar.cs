using System.Windows;
using System.Windows.Controls.Primitives;

namespace AirControl
{
    public class AirScrollBar : ScrollBar
    {
        static AirScrollBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirScrollBar), new FrameworkPropertyMetadata(typeof(AirScrollBar)));
        }
    }
}