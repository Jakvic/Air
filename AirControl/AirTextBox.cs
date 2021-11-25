using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AirControl
{
    public class AirTextBox : TextBox
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(AirTextBox), new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty HelpTextProperty = DependencyProperty.Register(
            "HelpText", typeof(string), typeof(AirTextBox), new PropertyMetadata(default(string)));

        static AirTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTextBox),
                new FrameworkPropertyMetadata(typeof(AirTextBox)));
        }

        public string HelpText
        {
            get => (string) GetValue(HelpTextProperty);
            set => SetValue(HelpTextProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}