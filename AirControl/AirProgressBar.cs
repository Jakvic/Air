using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirProgressBar : ProgressBar
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AirProgressBar),
                new PropertyMetadata(default(CornerRadius)));

        static AirProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirProgressBar),
                new FrameworkPropertyMetadata(typeof(AirProgressBar)));
        }

        /// <summary>
        ///     THe `Indicator` & `Animation` Rectangle's RadiusX binding CornerRadius.TopLeft
        ///     RadiusY binding CornerRadius.TopRight
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

    }
}