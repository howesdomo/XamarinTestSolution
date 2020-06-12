using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Client.Android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Client.Android")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed

// 网络
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.ChangeNetworkState)]
[assembly: UsesPermission(Android.Manifest.Permission.ChangeWifiState)]

// 读写存储设备
[assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.MountUnmountFilesystems)] // SD卡读取权限，用户写入离线定位数据

// 设置屏幕常亮
[assembly: UsesPermission(Android.Manifest.Permission.WakeLock)]

// 蓝牙
[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothAdmin)]

// 安装
[assembly: UsesPermission(Android.Manifest.Permission.InstallPackages)]


// ZXing.Barcode
[assembly: UsesPermission(Android.Manifest.Permission.Camera)]
[assembly: UsesPermission(Android.Manifest.Permission.Flashlight)]
[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]

// 定位
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)] // ACCESS_COARSE_LOCATION访问CellID或WiFi,只要当前设备可以接收到基站的服务信号，便可获得位置信息。（COARSE英文原意为：粗略的，可以理解为这种方式获得的位置信息是相对粗略的数据）。
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessWifiState)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]

[assembly: UsesPermission(Android.Manifest.Permission.ReadPhoneState)]

// 红外
[assembly: UsesPermission(Android.Manifest.Permission.TransmitIr)]

// 震动
[assembly: UsesPermission(Android.Manifest.Permission.Vibrate)]

// 录音
[assembly: UsesPermission(Android.Manifest.Permission.RecordAudio)]


