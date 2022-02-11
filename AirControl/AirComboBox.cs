using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirComboBox : ComboBox
    {
        static AirComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirComboBox),
                new FrameworkPropertyMetadata(typeof(AirComboBox)));
        }
    }
}