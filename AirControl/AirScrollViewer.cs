using System.Windows;
using System.Windows.Controls;

namespace AirControl
{
    public class AirScrollViewer : ScrollViewer
    {
        //https://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/windows/Controls/ScrollViewer.cs,2919
        //Custom ScrollBar not works ? the answer in this link

        public static readonly DependencyProperty ScrollBarCornerRadiusProperty = DependencyProperty.Register(
            "ScrollBarCornerRadius", typeof(CornerRadius), typeof(AirScrollViewer), new PropertyMetadata(default(CornerRadius)));

        static AirScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirScrollViewer),
                new FrameworkPropertyMetadata(typeof(AirScrollViewer)));
        }

        public CornerRadius ScrollBarCornerRadius
        {
            get => (CornerRadius) GetValue(ScrollBarCornerRadiusProperty);
            set => SetValue(ScrollBarCornerRadiusProperty, value);
        }
    }
}