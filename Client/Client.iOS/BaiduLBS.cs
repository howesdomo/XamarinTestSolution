using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMKLocationKitIdfa;
using CoreLocation;
using Foundation;
using UIKit;

namespace Client.iOS
{
    public class BaiduLBS : BMKLocationManagerDelegate, Common.ILBS
    {
        private BMKLocationKitIdfa.BMKLocationManager mLocationManager;

        public BaiduLBS()
        {
            // BMKLocationManager类。初始化之前请设置 BMKLocationAuth 中的APIKey，否则将无法正常使用服务.
            BMKLocationAuth auth = new BMKLocationAuth();           
            auth.SetNilValueForKey(new NSString("a1HFgWRRLpF1nsdma9EpZQpFsv8SKGFn")); // 走到这里会报错位置如何解决

            mLocationManager = new BMKLocationKitIdfa.BMKLocationManager();
            mLocationManager.Delegate = this;

            mLocationManager.CoordinateType = BMKLocationKitIdfa_Structs.BMKLocationCoordinateType.Bmk09ll;

            // (CLLocationDistance)distanceFilter - 设定定位的最小更新距离。默认为 kCLDistanceFilterNone。 
            mLocationManager.DistanceFilter = CoreLocation.CLLocationDistance.FilterNone;

            // (CLLocationAccuracy)desiredAccuracy - 设定定位精度。默认为 kCLLocationAccuracyBest。 
            // 由于苹果系统的首次定位结果为粗定位，其可能无法满足需要高精度定位的场景。
            // 所以，百度提供了 kCLLocationAccuracyBest 参数，设置该参数可以获取到精度在10m左右的定位结果，但是相应的需要付出比较长的时间（10s左右），越高的精度需要持续定位时间越长。
            // 推荐使用kCLLocationAccuracyHundredMeters，偏差在百米左右，超时时间设置在2s - 3s左右即可。
            mLocationManager.DesiredAccuracy = CoreLocation.CLLocation.AccuracyBest;

            // (CLActivityType)activityType - 设定定位类型。默认为 CLActivityTypeAutomotiveNavigation。 
            mLocationManager.ActivityType = CoreLocation.CLActivityType.AutomotiveNavigation;

            // 指定定位是否会被系统自动暂停。默认为NO。
            mLocationManager.PausesLocationUpdatesAutomatically = false;

            // 是否允许后台定位。默认为NO。只在iOS 9.0及之后起作用。
            // 设置为YES的时候必须保证 Background Modes 中的 Location updates 处于选中状态，否则会抛出异常。
            // 由于iOS系统限制，需要在定位未开始之前或定位停止之后，修改该属性的值才会有效果。
            mLocationManager.AllowsBackgroundLocationUpdates = true;

            // 指定单次定位超时时间,默认为10s。最小值是2s。注意单次定位请求前设置。注意: 单次定位超时时间从确定了定位权限(非kCLAuthorizationStatusNotDetermined状态)后开始计算。
            mLocationManager.LocationTimeout = 10;

            // 指定单次定位逆地理超时时间,默认为10s。最小值是2s。注意单次定位请求前设置。
            mLocationManager.ReGeocodeTimeout = 10;
        }

        public void GetGPSInfo(object args = null)
        {
            mLocationManager.RequestLocationWithReGeocode(true, true, handleABC);
        }

        private void handleABC(BMKLocation location, BMKLocationKitIdfa_Structs.BMKLocationNetworkState networkState, NSError error)
        {
            if (error != null)
            {
                string errorMsg = "[{0}]{1}".FormatWith(error.Code, error.LocalizedDescription);
                System.Diagnostics.Debug.WriteLine(errorMsg);
            }

            Client.Common.LBSModel r = null;

            //r = new Common.LBSModel
            //(
            //    _GPSInfoType: tmpGPSInfoType,
            //    _Latitude: location.Location.Coordinate.Latitude.ToString(),
            //    _Longitude: location.Location.Coordinate.Longitude.ToString(),
            //    _Radius: location.Radius.ToString(),
            //    _Address: location.AddrStr,
            //    _LocationDescribe: location.LocationDescribe
            //);

            r = new Common.LBSModel
            (
                _GPSInfoType: "", // tmpGPSInfoType,
                _Latitude: location.Location.Coordinate.Latitude.ToString(),
                _Longitude: location.Location.Coordinate.Longitude.ToString(),
                _Radius: "", // location.Radius.ToString(),
                _Address: "", // location.AddrStr,
                _LocationDescribe: "" // location.LocationDescribe
            );

            Client.Common.LBS.OnGetGPSInfo(r);
        }

        #region ICLLocationManagerDelegate

        [Export("locationManager:didUpdateHeading:")]
        public void UpdatedHeading(BMKLocationManager manager, CLHeading newHeading)
        {
            //trueHeadingLabel.Text = $"{newHeading.TrueHeading}º";
            //magneticHeadingLabel.Text = $"{newHeading.MagneticHeading}º";
        }

        [Export("locationManager:didUpdateLocations:")]
        public void LocationsUpdated(BMKLocation r_Location, CLLocation[] locations)
        {
            var location = locations.LastOrDefault();
            if (location != null)
            {
                // altitudeLabel.Text = $"{location.Altitude} meters";
                // lblLongitude.Text = "Lng:{0}º".FormatWith(location.Coordinate.Longitude);
                // lblLatitude.Text = "Lat:{0}º".FormatWith(location.Coordinate.Latitude);
                // courseLabel.Text = $"{location.Course}º";
                // speedLabel.Text = $"{location.Speed} meters/s";

                // get the distance from here to paris
                //distanceLabel.Text = $"{location.DistanceFrom(new CLLocation(48.857, 2.351)) / 1000} km";



                Client.Common.LBSModel r = null;

                r = new Common.LBSModel
                (
                    _GPSInfoType: string.Empty,
                    _Latitude: location.Coordinate.Latitude.ToString(),
                    _Longitude: location.Coordinate.Longitude.ToString(),
                    _Radius: string.Empty, // location.Radius.ToString(),
                    _Address: string.Empty, // location.AddrStr,
                    _LocationDescribe: string.Empty //location.LocationDescribe
                );

                Client.Common.LBS.OnGetGPSInfo(r);
            }
        }

        #endregion




    }
}