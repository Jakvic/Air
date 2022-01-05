using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace AirControl
{
    public class AirScrollBar: ScrollBar
    {
        static AirScrollBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirScrollBar),new PropertyMetadata(typeof(AirScrollBar)));
        }
    }
}
