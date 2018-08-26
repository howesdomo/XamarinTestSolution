using System;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;

using Com.Baidu.Location;

namespace Client.Droid
{
    public class BaiduLBS : Service, Com.Baidu.Location.IBDLocationListener, Common.ILBS
    {
        private static Com.Baidu.Location.LocationClient sLocationClient = null;

        private static object objLock = new object();

        public BaiduLBS(Android.Content.Context context)
        {
            lock (objLock)
            {
                if (sLocationClient == null)
                {
                    sLocationClient = new Com.Baidu.Location.LocationClient(context);     //声明LocationClient类
                    sLocationClient.RegisterLocationListener(this);    //注册监听函数
                }
            }
        }

        // public void startLocationService(LocationClientOption option = null)
        public void GetGPSInfo(object args = null)
        {
            LocationClientOption option = null;
            if (args != null && args is LocationClientOption)
            {
                option = args as LocationClientOption;
            }

            if (option == null)
            {
                option = new LocationClientOption();
                option.SetLocationMode(LocationClientOption.LocationMode.HightAccuracy);//可选，默认高精度，设置定位模式，高精度，低功耗，仅设备
                option.CoorType = "bd09ll";//可选，默认gcj02，设置返回的定位结果坐标系
                int scanSpan = 0; // 默认0，即仅定位一次
                option.ScanSpan = scanSpan;//可选，默认0，即仅定位一次，设置发起定位请求的间隔需要大于等于1000ms才是有效的
                option.SetIsNeedAddress(true);//可选，设置是否需要地址信息，默认不需要
                option.OpenGps = true;//可选，默认false,设置是否使用gps
                option.LocationNotify = true;//可选，默认false，设置是否当GPS有效时按照1S/1次频率输出GPS结果
                option.SetIsNeedLocationDescribe(true);//可选，默认false，设置是否需要位置语义化结果，可以在BDLocation.getLocationDescribe里得到，结果类似于“在北京天安门附近”
                option.SetIsNeedLocationPoiList(true);//可选，默认false，设置是否需要POI结果，可以在BDLocation.getPoiList里得到
                option.SetIgnoreKillProcess(false);//可选，默认true，定位SDK内部是一个SERVICE，并放到了独立进程，设置是否在stop的时候杀死这个进程，默认不杀死  
                option.SetIgnoreCacheException(false);//可选，默认false，设置是否收集CRASH信息，默认收集
                option.EnableSimulateGps = false;//可选，默认false，设置是否需要过滤GPS仿真结果，默认需要
                sLocationClient.LocOption = option;
            }

            if (sLocationClient.IsStarted)
            {
                sLocationClient.Restart();
            }
            else
            {
                sLocationClient.Start();
            }
        }

        public void OnReceiveLocation(BDLocation location)
        {
            #region 官方DEMO 组织信息

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

            #endregion 官方DEMO 组织信息


            Client.Common.LBSModel r = null;

            #region 判断是否定位错误

            string errorMsg = string.Empty;
            if (location.LocType == BDLocation.TypeServerError)
            {
                errorMsg = "服务端网络定位失败，可以反馈IMEI号和大体定位时间到loc-bugs@baidu.com，会有人追查原因";
            }
            else if (location.LocType == BDLocation.TypeNetWorkException)
            {
                errorMsg = "网络不同导致定位失败，请检查网络是否通畅";
            }
            else if (location.LocType == BDLocation.TypeCriteriaException)
            {
                errorMsg = "无法获取有效定位依据导致定位失败，一般是由于手机的原因，处于飞行模式下一般会造成这种结果，可以试着重启手机";
            }

            #endregion

            // 处理定位错误
            if (errorMsg.IsNullOrWhiteSpace() == false)
            {
                r = new Common.LBSModel(errorMsg);
                Client.Common.LBS.OnGetGPSInfo(r);
                return;
            }

            string tmpGPSInfoType = string.Empty;
            #region 判断定位方式

            if (location.LocType == BDLocation.TypeGpsLocation)
            {
                tmpGPSInfoType = "gps定位成功";
            }
            else if (location.LocType == BDLocation.TypeNetWorkLocation)
            {
                tmpGPSInfoType = "网络定位成功";
            }
            else if (location.LocType == BDLocation.TypeOffLineLocation)
            {
                tmpGPSInfoType = "离线定位成功";
            }
            #endregion


            // 处理定位正常
            r = new Common.LBSModel
            (
                _GPSInfoType: tmpGPSInfoType,
                _Latitude: location.Latitude.ToString(),
                _Longitude: location.Longitude.ToString(),
                _Radius: location.Radius.ToString(),
                _Address: location.AddrStr,
                _LocationDescribe: location.LocationDescribe
            );

            Client.Common.LBS.OnGetGPSInfo(r);
            return;
        }

        #region 实现 Service 接口

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        #endregion

    }
}