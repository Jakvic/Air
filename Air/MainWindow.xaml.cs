using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Air
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private Thickness rect_raw_margin;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rect_raw_margin = rect.Margin;
            rect_raw_margin.Left -= rect.Width;
        }

        private void StartAnimation()
        {
            Storyboard sb = new();
            var thicknessAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
                From = rect_raw_margin,
                To = new Thickness(border.Width, rect.Margin.Top,
                    -rect.Width, rect.Margin.Bottom),
                RepeatBehavior = RepeatBehavior.Forever,
            };
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            sb.Children.Add(thicknessAnimation);
            sb.Begin(rect);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }
    }
}