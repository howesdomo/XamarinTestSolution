using System;
using System.Reflection;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using System.Text;
using Com.Baidu.Location;
using Android.Content;


namespace Client.Droid
{
    [Activity(Label = "XamarinTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle); // Android Resources 在 raw 中放入 音频资源后报错, 挪动到首位后没有报错.

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // Add by Howe

            //// 初始化 Xamarin.Essentials 尝试使用传入 Applaction 的方法
            //Xamarin.Essentials.Platform.Init(activity:this, bundle: bundle);
            Xamarin.Essentials.Platform.Init(application: Application);

            init();

            // End Add by Howe

            var app = new App();
            LoadApplication(app);

            // V2
            // 经测试 无需实现 ContentPageAdv ,
            // 只需加载 V7版本 的 Widget.Toolbar, 即可实现软硬Back的监控
            // V1
            // 为了用 ContentPageAdv 实现, Navigation Back 按钮的监控
            // 为了能进入 OnOptionsItemSelected 事件, 采用 Android.Support.V7.Widget.Toolbar 代替默认的 Toolbar
            Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        private void init()
        {
            #region 初始化 Common.StaticInfo

            var staticInfoInitArgs = new Common.StaticInfoInitArgs();

            staticInfoInitArgs.AppName = "XamarinTest";

            #region 安卓项目路径赋值

            // 安卓系统外部存储绝对路径
            staticInfoInitArgs.AndroidExternalPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

            // App外部缓存绝对路径 -- /{安卓系统外部存储路径}/Android/data/{appPackageName}/cache
            foreach (var item in this.GetExternalCacheDirs())
            {
                staticInfoInitArgs.AndroidExternalCachePath = item.AbsolutePath;
                break;
            }

            // App外部文件绝对路径 -- /{安卓系统外部存储路径}/Android/data/{appPackageName}/files
            foreach (var item in this.GetExternalFilesDirs(string.Empty))
            {
                staticInfoInitArgs.AndroidExternalFilesPath = item.AbsolutePath;
                break;
            }

            #endregion

            #region 服务器配置

            string pathServiceSettings = Common.ServiceSettingsUtils.GetConfigFilePath(argsDirPath: staticInfoInitArgs.AndroidExternalFilesPath);

            if (System.IO.File.Exists(pathServiceSettings) == false)
            {
                Common.ServiceSettingsUtils.InitConfig(pathServiceSettings);
            }

            Common.ServiceSettingsUtils.ReadConfig(pathServiceSettings, staticInfoInitArgs);

            #endregion

            #region 本机配置

            string pathNativeSettings = Common.NativeSettingsUtils.GetConfigFilePath(argsDirPath: staticInfoInitArgs.AndroidExternalFilesPath);

            if (System.IO.File.Exists(pathNativeSettings) == false)
            {
                Common.NativeSettingsUtils.InitConfig(pathNativeSettings);
            }

            Common.NativeSettingsUtils.ReadConfig(pathNativeSettings, staticInfoInitArgs);

            #endregion

            #region SQLite

            staticInfoInitArgs.InnerSQLiteConnStr = System.IO.Path.Combine
            (
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                Util.Principle.DatabaseName_SQLite
            );

            staticInfoInitArgs.ExternalSQLiteConnStr = System.IO.Path.Combine
            (
                staticInfoInitArgs.AndroidExternalFilesPath,
                Util.Principle.DatabaseName_SQLite
            );

            #endregion

            Common.StaticInfo.Init(staticInfoInitArgs);

            #endregion

            // 实现IOutput接口 - 用 Logcat 来实现
            App.Output = new MyOutput();

            // 屏幕方向
            App.Screen = Util.XamariN.AndroiD.MyScreen.GetInstance(this);

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            // 初始化百度定位
            App.LBS = new BaiduLBS(ApplicationContext);

            // 初始化Audio
            App.AudioPlayer = Util.XamariN.AndroiD.MyAudioPlayer.GetInstance();

            // 初始化TTS
            App.TTS = Util.XamariN.AndroiD.MyTTS.GetInstance();

            // 初始化IR
            App.IR = MyIR.GetInstance(ApplicationContext);

            // 初始化动态权限
            // 1)实现封装好的接口
            App.AndroidPermissionUtils = Util.XamariN.AndroiD.MyAndroidPermission.GetInstance(this);
            // 2)下面这个实现用于本项目快速测试, 不需要更新 Util.XamariN.AndroiD 的 DLL 到 nuget
            MyAndroidPermission_InTestSolution myPermission = MyAndroidPermission_InTestSolution.GetInstance(this);
            App.AndroidPermissionUtils_InTestSolution = myPermission;

            // 初始化Bluetooth
            App.Bluetooth = Util.XamariN.AndroiD.MyBluetooth.GetInstance(this);

            #region 安卓特有

            // 访问 Assets 资源
            App.AndroidAssetsUtils = Util.XamariN.AndroiD.MyAndroidAssetsUtils.GetInstance(this);

            // 初始化 Intent 工具类
            App.AndroidIntentUtils = Util.XamariN.AndroiD.MyAndroidIntentUtils.GetInstance(this);
            string auth = $"{this.Application.PackageName}.fileprovider";
            App.AndroidIntentUtils.SetFileProvider_Authority(auth);

            // 初始化 Android 权限工具类
            App.AndroidPermissionUtils = Util.XamariN.AndroiD.MyAndroidPermission.GetInstance(this);

            // 初始化截屏
            App.AndroidScreenshot = Util.XamariN.AndroiD.MyAndroidScreenshot.GetInstance(this);

            // 初始化录屏
            App.AndroidScreenRecord = Util.XamariN.AndroiD.MyAndroidScreenRecord.GetInstance(this);

            #endregion





            #region 初始化第三方 DLL 库

            // 初始化 Acr.UserDialogs
            Acr.UserDialogs.UserDialogs.Init(this);

            //// [弃用] DevExpress.Mobile.Forms
            //// 初始化 DevExpress.Mobile.Forms
            //DevExpress.Mobile.Forms.Init();
            //// 由于DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName 默认主题为 Themes.Dark, 
            //// 这里初始化主题颜色为 Theme.Light
            //DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName = DevExpress.Mobile.DataGrid.Theme.Themes.Light;
            //DevExpress.Mobile.DataGrid.Theme.ThemeManager.RefreshTheme();


            // 初始化 FFImageLoading
            // 2.4.4.589(稳定版)执行 init 方式会报错 
            // System.TypeLoadException: Could not load list of method overrides due to Method not found: void
            // Nuget 引用 2.4.5.880-pre
            // 1) Xamarin.FFImageLoading
            // 2) Xamarin.FFImageLoading.Forms
            // 然后以下的执行初始化语句
            var svgAssembly = typeof(FFImageLoading.Svg.Forms.SvgCachedImage).GetTypeInfo().Assembly;                                  // <-- 追加
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);


            // Plugin.MediaManager.Forms ( 视频播放类库 )
            //MediaManager.CrossMediaManager.Current.Init();


            // 初始化 XLab
            var resolverContainer = new global::XLabs.Ioc.SimpleContainer();
            resolverContainer.Register<XLabs.Platform.Services.Media.IMediaPicker, XLabs.Platform.Services.Media.MediaPicker>();
            XLabs.Ioc.Resolver.SetResolver(resolverContainer.GetResolver());

            // TODO Aspose.Cells

            #endregion
        }

        #region (弃用) 实现软硬 Back 按钮的代码监控

        // V2
        // 经测试 无需实现 ContentPageAdv ,
        // 只需加载 V7版本 的 Widget.Toolbar, 即可实现软硬Back的监控
        ///// <summary>
        ///// 用 ContentPageAdv 实现, Navigation Back 按钮的监控
        ///// 核心代码
        ///// 
        ///// * 注意 * 必须使用V7.Widget.Toolbar, 请在 onCreate 中加上以下2行代码
        ///// Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
        ///// SetSupportActionBar(toolbar);
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    // check if the current item id 
        //    // is equals to the back button id
        //    if (item.ItemId == 16908332) // 返回按钮ID必定为 16908332
        //    {
        //        Xamarin.Forms.ContentPageAdv currentpage = null;
        //        try
        //        {
        //            var currentStack = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack;
        //            Xamarin.Forms.Page tmp = currentStack[currentStack.Count - 1];
        //            // NavigationStack.LastOrDefault(); // 找不到 LastOrDefault 扩展方法
        //            if (tmp is Xamarin.Forms.ContentPageAdv) // 避免强转程序报错
        //            {
        //                currentpage = tmp as Xamarin.Forms.ContentPageAdv;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = "{0}".FormatWith(ex.GetFullInfo());
        //            System.Diagnostics.Debug.WriteLine(msg);
        //        }

        //        // check if the page has subscribed to 
        //        // the custom back button event
        //        if (currentpage?.CustomBackButtonAction != null)
        //        {
        //            // invoke the Custom back button action
        //            currentpage?.CustomBackButtonAction.Invoke();
        //            // and disable the default back button action
        //            return false;
        //        }

        //        // if its not subscribed then go ahead 
        //        // with the default back button action
        //        return base.OnOptionsItemSelected(item);
        //    }
        //    else
        //    {
        //        // since its not the back button 
        //        //click, pass the event to the base
        //        return base.OnOptionsItemSelected(item);
        //    }
        //}

        //public override void OnBackPressed()
        //{
        //    // this is not necessary, but in Android user 
        //    // has both Nav bar back button and
        //    // physical back button its safe 
        //    // to cover the both events

        //    // retrieve the current xamarin forms page instance
        //    Xamarin.Forms.ContentPageAdv currentpage = null;
        //    try
        //    {
        //        var currentStack = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack;
        //        Xamarin.Forms.Page tmp = currentStack[currentStack.Count - 1];
        //        // NavigationStack.LastOrDefault(); // 找不到 LastOrDefault 扩展方法
        //        if (tmp is Xamarin.Forms.ContentPageAdv) // 避免强转程序报错
        //        {
        //            currentpage = tmp as Xamarin.Forms.ContentPageAdv;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = "{0}".FormatWith(ex.GetFullInfo());
        //        System.Diagnostics.Debug.WriteLine(msg);
        //    }

        //    // NavigationStack.LastOrDefault();

        //    // check if the page has subscribed to 
        //    // the custom back button event
        //    if (currentpage?.CustomBackButtonAction != null)
        //    {
        //        currentpage?.CustomBackButtonAction.Invoke();
        //    }
        //    else
        //    {
        //        base.OnBackPressed();
        //    }
        //}

        #endregion

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case Util.XamariN.AndroiD.MyTTS.TTS_RequestCode:
                    Util.XamariN.AndroiD.MyTTS.GetInstance().Handle_OnActivityResult(requestCode, resultCode, data);
                    break;
                case Util.XamariN.AndroiD.MyBluetooth.Bluetooth_RequestCode:
                    Util.XamariN.AndroiD.MyBluetooth.GetInstance().Handle_OpenBluetooth(requestCode, resultCode, data);
                    break;
                case Util.XamariN.AndroiD.MyAndroidScreenshot.s_Screenshot_Request_Code:
                    Util.XamariN.AndroiD.MyAndroidScreenshot.GetInstance().Screenshot_ActualMethod(resultCode, data);
                    break;
                case Util.XamariN.AndroiD.MyAndroidScreenRecord.s_ScreenRecord_Request_Code:
                    Util.XamariN.AndroiD.MyAndroidScreenRecord.GetInstance().StartRecord_ActualMethod(resultCode, data);
                    break;
                default:
                    break;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            // TODO 准备弃用自己写的安卓权限工具类
            //if (grantResults != null && grantResults.Length > 0)
            //{
            //    bool[] grantResultArr = new bool[grantResults.Length];

            //    for (int i = 0; i < grantResults.Length; i++)
            //    {
            //        grantResultArr[i] = (grantResults[i] == Permission.Granted);
            //    }

            //    MyAndroidPermission_InTestSolution.GetInstance().OnRequestPermissionsResult(requestCode, grantResultArr);
            //}
        }
    }


    public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }

        public void Error(string errorMessage, Exception ex)
        {
            Error(errorMessage + System.Environment.NewLine + ex.ToString());
        }
    }

}

