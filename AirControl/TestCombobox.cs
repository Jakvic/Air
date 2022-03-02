using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class TestCombobox : ComboBox 
    {
        static  TestCombobox() 
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TestCombobox),new FrameworkPropertyMetadata(typeof(TestCombobox)));
        }
    }
}