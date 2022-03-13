using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Brush BorderBrush
        {
            get => (Brush) GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
            "BorderThickness", typeof(Thickness), typeof(AirPopup), new PropertyMetadata(default(Thickness)));

        public Thickness BorderThickness
        {
            get { return (Thickness) GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawImage(
                new BitmapImage(new Uri(
                    @"C:\Users\Administrator\Documents\Tencent Files\3034736566\Image\Group\$0@Z}$]9JQD{GFL1COJ2IQM.jpg",
                    UriKind.RelativeOrAbsolute)), new Rect(0, 0, base.Width, base.Height));
        }
    }
}