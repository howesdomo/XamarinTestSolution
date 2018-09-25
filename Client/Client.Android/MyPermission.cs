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
    /// 由于 Android 6.0 以后, 某些权限除了需要在 AndroidManifest.xml 中定义,
    /// 还需要动态向用户示意是否授权 ( 以下附上只需要在 AndroidManifest.xml 授权即可使用 ), 此外其他权限均需要用户授权
    /// </summary>
    public class MyPermission : Client.Common.IPermission
    {
        // 权限

        #region 以下权限只需要在AndroidManifest.xml中声明即可使用

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

        #endregion 以下权限只需要在AndroidManifest.xml中声明即可使用

        public const int WRITE_EXTERNAL_STORAGE_REQUEST_CODE = 100;

        public bool CheckPermission(string permissionName)
        {
            Activity activity = (Activity)Xamarin.Forms.Forms.Context;
            Android.Content.PM.Permission permission = activity.CheckSelfPermission(permissionName);
            return permission == Permission.Granted ? true : false;
        }

        public void RequestPermissions(string[] args)
        {
            Activity activity = (Activity)Xamarin.Forms.Forms.Context;
            activity.RequestPermissions(args, WRITE_EXTERNAL_STORAGE_REQUEST_CODE);
        }

    }
}