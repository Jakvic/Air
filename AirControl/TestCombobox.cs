using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class TestCombobox : ComboBox
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(TestCombobox), new PropertyMetadata(default(CornerRadius)));

        static TestCombobox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TestCombobox),
                new FrameworkPropertyMetadata(typeof(TestCombobox)));
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}