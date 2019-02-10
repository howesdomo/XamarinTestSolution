using Xamarin.Forms;

[assembly: Dependency(typeof(Util.XamariN.Essentials.DeviceInfoUtils))]
namespace Util.XamariN.Essentials
{
    public class DeviceInfoUtils : Util.XamariN.Essentials.IDeviceInfoUtils
    {
        public DeviceInfoUtils()
        {

        }

        public Util.XamariN.Essentials.DeviceInfo GetDeviceInfo()
        {
            return new Util.XamariN.Essentials.DeviceInfo()
            {
                Model = Xamarin.Essentials.DeviceInfo.Model,
                Manufacturer = Xamarin.Essentials.DeviceInfo.Manufacturer,
                DeviceName = Xamarin.Essentials.DeviceInfo.Name,

                VersionMajor = Xamarin.Essentials.DeviceInfo.Version.Major,
                VersionMinor = Xamarin.Essentials.DeviceInfo.Version.Minor,
                VersionBuild = Xamarin.Essentials.DeviceInfo.Version.Build,
                VersionRevision = Xamarin.Essentials.DeviceInfo.Version.Revision,
                VersionInfo = Xamarin.Essentials.DeviceInfo.VersionString,

                Platform = Xamarin.Essentials.DeviceInfo.Platform.ToString(),
                Idiom = Xamarin.Essentials.DeviceInfo.Idiom.ToString(),
                DeviceType = Xamarin.Essentials.DeviceInfo.DeviceType.ToString(),
            };
        }

        public string GetDeviceInfoMessage()
        {
            //// Device Model (SMG-950U)
            //var device = Xamarin.Essentials.DeviceInfo.Model;

            //// Manufacturer (Samsung)
            //var manufacturer = Xamarin.Essentials.DeviceInfo.Manufacturer;

            //// Device Name (Motz's iPhone)
            //var deviceName = Xamarin.Essentials.DeviceInfo.Name;

            //// Operating System Version Number (7.0)
            //var version = Xamarin.Essentials.DeviceInfo.VersionString;

            //// Platform (Android)
            //var platform = Xamarin.Essentials.DeviceInfo.Platform;

            //// Idiom (Phone)
            //var idiom = Xamarin.Essentials.DeviceInfo.Idiom;

            //// Device Type (Physical)
            //var deviceType = Xamarin.Essentials.DeviceInfo.DeviceType.ToString();

            var m = GetDeviceInfo();
            return m.ToString();
        }

        public string GetDeviceInfoJsonStr()
        {
            return Util.JsonUtils.SerializeObject(GetDeviceInfo());
        }
    }
}
