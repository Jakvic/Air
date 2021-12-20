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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            
        }

        private void StartAnimation()
        {
            Storyboard sb = new();
            var leftAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(1000)));
            Storyboard.SetTarget(leftAnimation, rect);
            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            sb.Children.Add(leftAnimation);

            var rightAnimation = new DoubleAnimation(border.ActualWidth, new Duration(TimeSpan.FromMilliseconds(1000)));
            Storyboard.SetTarget(rightAnimation, rect);
            Storyboard.SetTargetProperty(rightAnimation, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            sb.Children.Add(rightAnimation);

          
            sb.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }
    }
}