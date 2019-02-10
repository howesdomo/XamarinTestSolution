using Xamarin.Forms;

[assembly: Dependency(typeof(Client.iOS.DisplayInfoUtilsV2))]
namespace Client.iOS
// namespace Util.XamariN.Essentials
{
    public class DisplayInfoUtilsV2 : Util.XamariN.Essentials.IDisplayInfoUtils
    {
        public DisplayInfoUtilsV2()
        {

        }

        /// <summary>
        /// 实现接口方法
        /// </summary>
        /// <returns></returns>
        public Util.XamariN.Essentials.DisplayInfo GetDisplayInfo()
        {
            // Get Metrics
            // var metrics = Xamarin.Essentials.DeviceDisplay.ScreenMetrics; // 0.1.0 preview
            var metrics = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo; // 1.0.2

            //// Orientation (Landscape, Portrait, Square, Unknown)
            //var orientation = metrics.Orientation;

            //// Rotation (0, 90, 180, 270)
            //var rotation = metrics.Rotation;

            //// Width (in pixels)
            //var width = metrics.Width;

            //// Height (in pixels)
            //var height = metrics.Height;

            //// Screen density
            //var density = metrics.Density;

            return new Util.XamariN.Essentials.DisplayInfo()
            {
                Orientation = metrics.Orientation.ToString(),
                RotationInfo = metrics.Rotation.ToString(),
                Rotation = int.Parse(metrics.Rotation.ToString().ToUpper().Replace("ROTATION", "")),
                Width = metrics.Width,
                Height = metrics.Height,
                Density = metrics.Density
            };

        }
    }
}
