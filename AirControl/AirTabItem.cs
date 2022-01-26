using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AirTabItem), new PropertyMetadata(default));

    }
}
