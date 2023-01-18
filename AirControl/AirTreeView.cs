using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AirControl
{
    public class AirTreeView : TreeView
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(AirTreeView), new PropertyMetadata(default(CornerRadius)));

        static AirTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirTreeView),
                new FrameworkPropertyMetadata(typeof(AirTreeView)));
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}