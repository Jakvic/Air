using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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

        public TextBlock templateChild { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            templateChild = (GetTemplateChild("HelpTextBox") as TextBlock)!;
            templateChild.IsVisibleChanged += TemplateChildOnIsVisibleChanged;
        }

        private void TemplateChildOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement is null)
            {
                return;
            }

            return;
            var doubleAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(2000))
            };
            if (frameworkElement.Visibility is Visibility.Hidden)
            {
                if (frameworkElement.Opacity == 0)
                {
                    return;
                }

                doubleAnimation.From = frameworkElement.Opacity;
                doubleAnimation.To = 0.0;
            }
            else
            {
                if (frameworkElement.Opacity >= 0)
                {
                    return;
                }

                doubleAnimation.From = 0.0;
                doubleAnimation.To = frameworkElement.Opacity;
            }

            var storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimation);
            frameworkElement.BeginAnimation(OpacityProperty, doubleAnimation);
            storyboard.Children.Clear();
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