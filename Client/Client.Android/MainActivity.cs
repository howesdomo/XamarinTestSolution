using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using System.Text;
using Com.Baidu.Location;

namespace Client.Droid
{
    [Activity(Label = "Client", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Com.Baidu.Location.IBDLocationListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // Add by Howe
            init();
            // End Add by Howe

            LoadApplication(new App());
        }

        private void init()
        {
            #region VN7

            //Common.WebSetting appWebSetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.215",
            //    port: "17911",
            //    appName: "AppWebApplication461/AppWebService.asmx"
            //); // TODO Read In webSetting.json

            //Common.WebSetting webAPISetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.215",
            //    port: "17911",
            //    appName: "AppWebApplication461/api/orders"
            //); // TODO Read In webSetting.json

            #endregion

            #region HOME-PC

            Common.WebSetting appWebSetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.216",
                port: "17911",
                appName: "AppWebApplication461/AppWebService.asmx"
            ); // TODO Read In webSetting.json

            Common.WebSetting webAPISetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.216",
                port: "17911",
                appName: "AppWebApplication461/api/orders"
            ); // TODO Read In webSetting.json

            #endregion


            string innerSQLiteConnStr = System.IO.Path.Combine
            (
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                Util.Principle.DatabaseName_SQLite
            );

            string externalSQLiteConnStr = System.IO.Path.Combine
            (
                "", // TODO android 外部存储路径
                Util.Principle.DatabaseName_SQLite
            );

            Client.Common.StaticInfo.Init
            (
                new Client.Common.StaticInfoInitArgs()
                {
                    AppName = "你好Xamarin",
                    AppWebSetting = appWebSetting,
                    WebAPISetting = webAPISetting,
                    InnerSQLiteConnStr = innerSQLiteConnStr,
                    ExternalSQLiteConnStr = externalSQLiteConnStr
                }
            );

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            // 初始化百度定位
            initBaiduLBS();
        }

        #region

        public Com.Baidu.Location.LocationClient mLocationClient = null;

        void initBaiduLBS()
        {
            mLocationClient = new Com.Baidu.Location.LocationClient(ApplicationContext);     //声明LocationClient类
            mLocationClient.RegisterLocationListener(this);    //注册监听函数

            startLocationService();
        }

        private void startLocationService()
        {
            LocationClientOption option = new LocationClientOption();
            option.SetLocationMode(LocationClientOption.LocationMode.HightAccuracy);//可选，默认高精度，设置定位模式，高精度，低功耗，仅设备
            option.CoorType = "bd09ll";//可选，默认gcj02，设置返回的定位结果坐标系
            int span = 1000;
            option.ScanSpan = span;//可选，默认0，即仅定位一次，设置发起定位请求的间隔需要大于等于1000ms才是有效的
            option.SetIsNeedAddress(true);//可选，设置是否需要地址信息，默认不需要
            option.OpenGps = true;//可选，默认false,设置是否使用gps
            option.LocationNotify = true;//可选，默认false，设置是否当GPS有效时按照1S/1次频率输出GPS结果
            option.SetIsNeedLocationDescribe(true);//可选，默认false，设置是否需要位置语义化结果，可以在BDLocation.getLocationDescribe里得到，结果类似于“在北京天安门附近”
            option.SetIsNeedLocationPoiList(true);//可选，默认false，设置是否需要POI结果，可以在BDLocation.getPoiList里得到
            option.SetIgnoreKillProcess(false);//可选，默认true，定位SDK内部是一个SERVICE，并放到了独立进程，设置是否在stop的时候杀死这个进程，默认不杀死  
            option.SetIgnoreCacheException(false);//可选，默认false，设置是否收集CRASH信息，默认收集
            option.EnableSimulateGps = false;//可选，默认false，设置是否需要过滤GPS仿真结果，默认需要
            mLocationClient.LocOption = option;
            mLocationClient.Start();

        }

        public void OnReceiveLocation(BDLocation location)
        {
            //System.Diagnostics.Debug.Write(p0.LocType);
            //if (p0.LocType == 161)
            //{
            //    //从此处取值即可
            //    System.Diagnostics.Debug.Write(p0.AddrStr);

            //}
            //Receive Location
            StringBuilder sb = new StringBuilder();
            sb.Append("time : ");
            sb.Append(location.Time);
            sb.Append("\nerror code : ");
            sb.Append(location.LocType);
            sb.Append("\nlatitude : ");
            sb.Append(location.Latitude);
            sb.Append("\nlontitude : ");
            sb.Append(location.Longitude);
            sb.Append("\nradius : ");
            sb.Append(location.Radius);
            if (location.LocType == BDLocation.TypeGpsLocation)
            {// GPS定位结果
                sb.Append("\nspeed : ");
                sb.Append(location.Speed);// 单位：公里每小时
                sb.Append("\nsatellite : ");
                sb.Append(location.SatelliteNumber);
                sb.Append("\nheight : ");
                sb.Append(location.Altitude);// 单位：米
                sb.Append("\ndirection : ");
                sb.Append(location.Direction);// 单位度
                sb.Append("\naddr : ");
                sb.Append(location.AddrStr);
                sb.Append("\ndescribe : ");
                sb.Append("gps定位成功");

            }
            else if (location.LocType == BDLocation.TypeNetWorkLocation)
            {// 网络定位结果
                sb.Append("\naddr : ");
                sb.Append(location.AddrStr);
                //运营商信息
                sb.Append("\noperationers : ");
                sb.Append(location.Operators);
                sb.Append("\ndescribe : ");
                sb.Append("网络定位成功");
            }
            else if (location.LocType == BDLocation.TypeOffLineLocation)
            {// 离线定位结果
                sb.Append("\ndescribe : ");
                sb.Append("离线定位成功，离线定位结果也是有效的");
            }
            else if (location.LocType == BDLocation.TypeServerError)
            {
                sb.Append("\ndescribe : ");
                sb.Append("服务端网络定位失败，可以反馈IMEI号和大体定位时间到loc-bugs@baidu.com，会有人追查原因");
            }
            else if (location.LocType == BDLocation.TypeNetWorkException)
            {
                sb.Append("\ndescribe : ");
                sb.Append("网络不同导致定位失败，请检查网络是否通畅");
            }
            else if (location.LocType == BDLocation.TypeCriteriaException)
            {
                sb.Append("\ndescribe : ");
                sb.Append("无法获取有效定位依据导致定位失败，一般是由于手机的原因，处于飞行模式下一般会造成这种结果，可以试着重启手机");
            }
            sb.Append("\nlocationdescribe : ");
            sb.Append(location.LocationDescribe);// 位置语义化信息
            // System.Collections.Generic.IList list = location.PoiList;// POI数据
            if (location.PoiList != null)
            {
                sb.Append("\npoilist size = : ");
                sb.Append(location.PoiList.Count.ToString());
                foreach (Poi p in location.PoiList)
                {
                    sb.Append("\npoi= : ");
                    sb.Append(p.Id + " " + p.Name + " " + p.Rank);
                }
            }
            System.Diagnostics.Debug.Write(sb.ToString());

        }



        #endregion
    }



}

