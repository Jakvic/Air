using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirTabControl : TabControl
    {
        static AirTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTabControl),
                new FrameworkPropertyMetadata(typeof(AirTabControl)));
        }
    }
}