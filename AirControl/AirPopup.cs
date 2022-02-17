using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace AirControl
{
    public class AirPopup : Popup
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(AirPopup), new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(
            "BorderBrush", typeof(Brush), typeof(AirPopup), new PropertyMetadata(default(Brush)));

        static AirPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirPopup), new FrameworkPropertyMetadata(typeof(AirPopup)));
        }
        public AirPopup()
        {
            IsOpenProperty.DefaultMetadata.PropertyChangedCallback += PropertyChangedCallback;
        }

        private void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue is true)
            {
                
            }
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
            "BorderThickness", typeof(Thickness), typeof(AirPopup), new PropertyMetadata(default(Thickness)));

        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}