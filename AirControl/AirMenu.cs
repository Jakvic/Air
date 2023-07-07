using System.Windows;
using System.Windows.Controls;

namespace AirControl;

public class AirMenu : Menu
{
    static AirMenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirMenu), new FrameworkPropertyMetadata(typeof(AirMenu)));
    }
}