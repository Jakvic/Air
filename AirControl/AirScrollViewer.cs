using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AirControl
{
    public class AirScrollViewer : ScrollViewer
    {
        //https://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/windows/Controls/ScrollViewer.cs,2919
        //Custom ScrollBar not works ? the answer in this link

        public static readonly DependencyProperty ScrollBarCornerRadiusProperty = DependencyProperty.Register(
            "ScrollBarCornerRadius", typeof(CornerRadius), typeof(AirScrollViewer),
            new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty ScrollBarWidthProperty = DependencyProperty.Register(
            "ScrollBarWidth", typeof(double), typeof(AirScrollViewer), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty CanBeWidthProperty = DependencyProperty.Register(
            "CanBeWidth", typeof(bool), typeof(AirScrollViewer), new PropertyMetadata(default(bool)));

        private Border animationBorder;
        private Storyboard endStoryboard;
        private Storyboard startStoryboard;

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

        public double ScrollBarWidth
        {
            get => (double) GetValue(ScrollBarWidthProperty);
            set => SetValue(ScrollBarWidthProperty, value);
        }

        public bool CanBeWidth
        {
            get => (bool) GetValue(CanBeWidthProperty);
            set => SetValue(CanBeWidthProperty, value);
        }
    }
}