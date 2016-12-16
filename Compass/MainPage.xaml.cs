using System;
using Windows.Devices.Sensors;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompassSensors
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var compass = Compass.GetDefault();
            if (compass == null)
            {
                return;
            }
            compass.ReadingChanged += async (s, e) =>
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    RotateNeedle(e.Reading.HeadingMagneticNorth);
                });
            };
        }

        private void RotateNeedle(double angle)
        {
            var transform = new RotateTransform() { Angle = angle };
            Needle.RenderTransformOrigin = new Windows.Foundation.Point(0.5, 0.5);
            Needle.RenderTransform = transform;
        }
    }
}
