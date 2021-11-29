using System.Windows.Controls;

namespace AirControl
{
    public class AirProgressBar : ProgressBar
    {
        static AirProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirProgressBar), 
                new System.Windows.PropertyMetadata(typeof(AirProgressBar)));
        }
    }
}