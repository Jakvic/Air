using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirRadioButton : RadioButton
    {
        static AirRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirRadioButton),new FrameworkPropertyMetadata(typeof(AirRadioButton)));
        }
    }
}
