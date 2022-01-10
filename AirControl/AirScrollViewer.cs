using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirScrollViewer : ScrollViewer
    {
        static AirScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirScrollViewer),
                new PropertyMetadata(typeof(AirScrollViewer)));
        }
    }
}