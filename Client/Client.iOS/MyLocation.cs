using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using UIKit;

namespace Client.iOS
{
    public class MyLocation : UIViewController, CoreLocation.ICLLocationManagerDelegate, Common.ILBS
    {
        #region 构造函数 + 单例模式

        private MyLocation()
        {
            mLocationManager = new CoreLocation.CLLocationManager();
        }

        private static MyLocation s_Instance;

        private static object objLock = new object();

        public static MyLocation GetInstance()
        {
            lock (objLock)
            {
                if (s_Instance == null)
                {
                    s_Instance = new MyLocation();
                }

                return s_Instance;
            }
        }

        #endregion

        private readonly CoreLocation.CLLocationManager mLocationManager;

        // public IntPtr Handle => throw new NotImplementedException();

        public void Start()
        {
            // you can set the update threshold and accuracy if you want:
            //locationManager.DistanceFilter = 10d; // move ten meters before updating
            //locationManager.HeadingFilter = 3d; // move 3 degrees before updating

            // you can also set the desired accuracy:
            mLocationManager.DesiredAccuracy = 1000; // 1000 meters/1 kilometer
            // you can also use presets, which simply evalute to a double value:
            //locationManager.DesiredAccuracy = CLLocation.AccuracyNearestTenMeters;

            mLocationManager.Delegate = this;
            mLocationManager.RequestWhenInUseAuthorization();

            if (CoreLocation.CLLocationManager.LocationServicesEnabled)
            {
                mLocationManager.StartUpdatingLocation();
            }

            if (CoreLocation.CLLocationManager.HeadingAvailable)
            {
                mLocationManager.StartUpdatingHeading();
            }
        }

        public void Stop()
        {
            //if (CoreLocation.CLLocationManager.LocationServicesEnabled)
            //{
            //    locationManager.StopUpdatingLocation();
            //}

            //if (CoreLocation.CLLocationManager.HeadingAvailable)
            //{
            //    locationManager.StopUpdatingHeading();
            //}

            mLocationManager.StopUpdatingLocation();
            mLocationManager.StopUpdatingHeading();
        }
        
        #region ICLLocationManagerDelegate

        [Export("locationManager:didUpdateHeading:")]
        public void UpdatedHeading(CLLocationManager manager, CLHeading newHeading)
        {
            //trueHeadingLabel.Text = $"{newHeading.TrueHeading}º";
            //magneticHeadingLabel.Text = $"{newHeading.MagneticHeading}º";
        }

        [Export("locationManager:didUpdateLocations:")]
        public void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
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

        public void GetGPSInfo(object args = null)
        {
            Start();
        }

    }
}