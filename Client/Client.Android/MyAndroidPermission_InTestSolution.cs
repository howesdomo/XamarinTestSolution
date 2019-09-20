using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Client.Droid
{
    /// <summary>
    /// V 1.0.2 - 2019-9-20 11:06:50
    /// 对于 [对于多个授权操作, 采用分别逐个权限申请] 做法测试效果不理想
    /// 
    /// V 1.0.1 - 2019-8
    /// 优化授权提示
    /// mDict 定义授权提示信息
    /// 计划对于多个授权操作, 采用分别逐个权限申请, 并提示用户选择信息
    ///  
    /// V 1.0.0 
    /// 首次创建安卓授权接口的实现
    /// </summary>
    public class MyAndroidPermission_InTestSolution : Util.XamariN.IAndroidPermission
    {
        private Android.App.Activity mAppActivity { get; set; }

        #region 构造函数 + 单例模式

        private MyAndroidPermission_InTestSolution()
        {
            mDict.Add
            (
                new MyPermissionModel()
                {
                    FullName = READ_EXTERNAL_STORAGE,
                    Name = "READ_EXTERNAL_STORAGE",
                    RequestCode = REQUEST_CODE___READ_EXTERNAL_STORAGE,
                    Description = "读取外部存储权限"
                }
            );

            mDict.Add
            (
                new MyPermissionModel()
                {
                    FullName = WRITE_EXTERNAL_STORAGE,
                    Name = "WRITE_EXTERNAL_STORAGE",
                    RequestCode = REQUEST_CODE___WRITE_EXTERNAL_STORAGE,
                    Description = "写入外部存储权限"
                }
            );

            mDict.Add
            (
                new MyPermissionModel()
                {
                    FullName = ACCESS_COARSE_LOCATION,
                    Name = "ACCESS_COARSE_LOCATION",
                    RequestCode = REQUEST_CODE___ACCESS_COARSE_LOCATION,
                    Description = "获取位置信息(通过基站的服务信号)"
                }
            );
        }

        private static MyAndroidPermission_InTestSolution s_Instance;

        private static object objLock = new object();

        public static MyAndroidPermission_InTestSolution GetInstance(Android.App.Activity activity = null)
        {
            lock (objLock)
            {
                if (s_Instance == null)
                {
                    s_Instance = new MyAndroidPermission_InTestSolution();
                    if (s_Instance.mAppActivity == null && activity == null)
                    {
                        throw new Exception("MyPermission.GetInstance 单例首次创建, 请传入 activity 参数");
                    }
                    if (activity != null)
                    {
                        s_Instance.mAppActivity = activity;
                    }
                }
                return s_Instance;
            }
        }

        #endregion

        /// <summary>
        /// 判断用户是否有权限
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public bool CheckPermission(string permissionName)
        {
            if (Xamarin.Essentials.DeviceInfo.Version.Major < 6)
            {
#if DEBUG
                string msg = $"当前系统为 Android{Xamarin.Essentials.DeviceInfo.Version.Major}";
                System.Diagnostics.Debug.WriteLine(msg);

                System.Diagnostics.Debugger.Break();
#endif
                return true;
            }

            Android.Content.PM.Permission permission = mAppActivity.CheckSelfPermission(permissionName);
            return permission == Android.Content.PM.Permission.Granted ? true : false;
        }

        /// <summary>
        /// 判断权限在字典里
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public bool IsPermissionInDict(string permissionName)
        {
            return mDict.Any(i => i.FullName == permissionName || i.Name == permissionName);
        }

        /// <summary>
        /// 如果应用之前请求过此权限但用户拒绝了请求，此方法将返回 true。
        /// 如果用户在过去拒绝了权限请求，并在权限请求系统对话框中选择了 Don't ask again 选项，此方法将返回 false。
        /// 如果设备规范禁止应用具有该权限，此方法也会返回 false。
        /// </summary>
        /// <returns></returns>
        public bool ShouldShowRequestPermissionRationale(string permissionName)
        {
            return mAppActivity.ShouldShowRequestPermissionRationale(permissionName);
        }

        /// <summary>
        /// 请求权限
        /// </summary>
        /// <param name="args"></param>
        public void RequestPermission(string permissionName)
        {
            int value = 0;
            var match = mDict.FirstOrDefault(i => i.FullName == permissionName || i.Name == permissionName);
            if (match != null)
            {
                value = match.RequestCode;
            }
            mAppActivity.RequestPermissions(new string[1] { permissionName }, value);
        }

        /// <summary>
        /// 请求权限
        /// </summary>
        /// <param name="args"></param>
        public void RequestPermissions(string[] args)
        {
            mAppActivity.RequestPermissions(args, 0);
        }

        public void OnRequestPermissionsResult(int requestCode, bool[] grantResults)
        {
            string funcDescription = string.Empty;

            if (requestCode > 0) // 若申请只有一项权限, requestCode > 0 // 若有多项权限, requestCode = 0
            {
                funcDescription = mDict.First(i => i.RequestCode == requestCode).Description;
            }

            bool grantResult = true; // 只要含有 1 项授权失败即为失败
            foreach (var item in grantResults)
            {
                grantResult = grantResult && item;
            }

            if (grantResult == true)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    Looper.Prepare();
                    Toast.MakeText(mAppActivity, $"{funcDescription}授权成功", ToastLength.Long).Show();
                    Looper.Loop();
                });
            }
            else
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    Looper.Prepare();
                    Toast.MakeText(mAppActivity, $"{funcDescription}授权失败", ToastLength.Long).Show();
                    Looper.Loop();
                });
            }
        }

        /// <summary>
        /// 用于定位权限的 REQUEST_CODE
        /// </summary>
        private List<MyPermissionModel> mDict = new List<MyPermissionModel>();

        #region 向用户请求的权限

        public const int REQUEST_CODE___READ_EXTERNAL_STORAGE = 101;
        public const string READ_EXTERNAL_STORAGE = "android.permission.READ_EXTERNAL_STORAGE";

        public const int REQUEST_CODE___WRITE_EXTERNAL_STORAGE = 102;
        public const string WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";

        public const int REQUEST_CODE___ACCESS_COARSE_LOCATION = 103;
        public const string ACCESS_COARSE_LOCATION = "android.permission.ACCESS_COARSE_LOCATION";

        #endregion 

        #region 以下权限只需要在AndroidManifest.xml中声明即可使用, 不需要向用户申请

        //android.permission.ACCESS_LOCATION_EXTRA_COMMANDS
        //android.permission.ACCESS_NETWORK_STATE
        //android.permission.ACCESS_NOTIFICATION_POLICY
        //android.permission.ACCESS_WIFI_STATE
        //android.permission.ACCESS_WIMAX_STATE
        //android.permission.BLUETOOTH
        //android.permission.BLUETOOTH_ADMIN
        //android.permission.BROADCAST_STICKY
        //android.permission.CHANGE_NETWORK_STATE
        //android.permission.CHANGE_WIFI_MULTICAST_STATE
        //android.permission.CHANGE_WIFI_STATE
        //android.permission.CHANGE_WIMAX_STATE
        //android.permission.DISABLE_KEYGUARD
        //android.permission.EXPAND_STATUS_BAR
        //android.permission.FLASHLIGHT
        //android.permission.GET_ACCOUNTS
        //android.permission.GET_PACKAGE_SIZE
        //android.permission.INTERNET
        //android.permission.KILL_BACKGROUND_PROCESSES
        //android.permission.MODIFY_AUDIO_SETTINGS
        //android.permission.NFC
        //android.permission.READ_SYNC_SETTINGS
        //android.permission.READ_SYNC_STATS
        //android.permission.RECEIVE_BOOT_COMPLETED
        //android.permission.REORDER_TASKS
        //android.permission.REQUEST_INSTALL_PACKAGES
        //android.permission.SET_TIME_ZONE
        //android.permission.SET_WALLPAPER
        //android.permission.SET_WALLPAPER_HINTS
        //android.permission.SUBSCRIBED_FEEDS_READ
        //android.permission.TRANSMIT_IR
        //android.permission.USE_FINGERPRINT
        //android.permission.VIBRATE
        //android.permission.WAKE_LOCK
        //android.permission.WRITE_SYNC_SETTINGS
        //com.android.alarm.permission.SET_ALARM
        //com.android.launcher.permission.INSTALL_SHORTCUT
        //com.android.launcher.permission.UNINSTALL_SHORTCUT

        #endregion
    }

    public class MyPermissionModel
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public int RequestCode { get; set; }

        public string Description { get; set; }
    }
}