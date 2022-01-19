using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
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

        static AirScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirScrollViewer),
                new FrameworkPropertyMetadata(typeof(AirScrollViewer)));
        }

        public CornerRadius ScrollBarCornerRadius
        {
            get => (CornerRadius)GetValue(ScrollBarCornerRadiusProperty);
            set => SetValue(ScrollBarCornerRadiusProperty, value);
        }

        public double ScrollBarWidth
        {
            get => (double)GetValue(ScrollBarWidthProperty);
            set => SetValue(ScrollBarWidthProperty, value);
        }

        Border animationBorder;
        Border hitTestBorder;
        Storyboard startStoryboard;
        Storyboard endStoryboard;
        ScrollBar vsb;
        ScrollBar hsb;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            vsb = GetTemplateChild("PART_VerticalScrollBar") as ScrollBar;
            hsb = GetTemplateChild("PART_HorizontalScrollBar") as ScrollBar;

            vsb.ApplyTemplate();
            animationBorder = vsb.Template.FindName("BorderRoot", vsb) as Border;
            vsb.ApplyTemplate();
            hitTestBorder = vsb.Template.FindName("HitTestBorder", vsb) as Border;

            hitTestBorder.MouseEnter += ScrollBar_MouseEnter;
            hitTestBorder.MouseLeave += ScrollBar_MouseLeave;
        }

        private void ScrollBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            endStoryboard?.Stop();
            startStoryboard = new Storyboard();
            var animation = new DoubleAnimation(ScrollBarWidth, 10, new Duration(TimeSpan.FromMilliseconds(100)));
            Storyboard.SetTarget(animation, animationBorder);
            var scrollBar = vsb ?? hsb;
            if (scrollBar.Orientation is Orientation.Vertical)
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));
                startStoryboard.Children.Add(animation);
            }
            else if (scrollBar.Orientation is Orientation.Horizontal)
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                startStoryboard.Children.Add(animation);
            }
            startStoryboard.Begin();

        }
        private void ScrollBar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var scrollBar = vsb ?? hsb;
            startStoryboard?.Stop();
            endStoryboard?.Stop();
            endStoryboard = new Storyboard();
            var animation = new DoubleAnimation(10, ScrollBarWidth, new Duration(TimeSpan.FromMilliseconds(100)));
            Storyboard.SetTarget(animation, animationBorder);
            if (scrollBar.Orientation is Orientation.Vertical)
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));
                endStoryboard.Children.Add(animation);
            }
            else if (scrollBar.Orientation is Orientation.Horizontal)
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                endStoryboard.Children.Add(animation);
            }
            endStoryboard.Begin();
        }

    }
}