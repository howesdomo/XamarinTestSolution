using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Client.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            #region 注册全局异常捕获事件

            AppDomain.CurrentDomain.UnhandledException += MyApplication.CurrentDomain_UnhandledException;
            System.Threading.Tasks.TaskScheduler.UnobservedTaskException += MyApplication.TaskScheduler_UnobservedTaskException;

            #endregion

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            // Add by Howe

            init();
            initXLabs();

            // End Add by Howe

            return base.FinishedLaunching(app, options);
        }

        private void init()
        {
            #region VN7

            Common.WebSetting appWebSetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.215",
                port: "17911",
                appName: "AppWebApplication461/APPWebServiceHandler.ashx"
            ); // TODO Read In webSetting.json

            Common.WebSetting webAPISetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.215",
                port: "17911",
                appName: "AppWebApplication461/api/orders"
            ); // TODO Read In webSetting.json

            #endregion

            #region HOME-PC

            //Common.WebSetting appWebSetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.216",
            //    port: "17911",
            //    appName: "AppWebApplication461/AppWebService.asmx"
            //); // TODO Read In webSetting.json

            //Common.WebSetting webAPISetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.216",
            //    port: "17911",
            //    appName: "AppWebApplication461/api/orders"
            //); // TODO Read In webSetting.json

            #endregion


            string innerSQLiteConnStr = System.IO.Path.Combine
            (
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                Util.Principle.DatabaseName_SQLite
            );

            //// iOS 数据库存储位置 (拷贝自 TODO 项目), 值得参考
            //string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //string libFolder = System.IO.Path.Combine(docFolder, "..", "Library", "Databases");
            //if (!Directory.Exists(libFolder))
            //{
            //    Directory.CreateDirectory(libFolder);
            //}



            //string externalSQLiteConnStr = System.IO.Path.Combine
            //(
            //    Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, // 获取 Android 外部存储路径
            //    Util.Principle.ExternalStorageDirectoryTemplate.FormatWith(Client.Common.StaticInfo.AppName, Util.Principle.DatabaseName_SQLite)
            //);

            Client.Common.StaticInfo.Init
            (
                new Client.Common.StaticInfoInitArgs()
                {
                    AppName = "你好Xamarin",
                    AppWebSetting = appWebSetting,
                    WebAPISetting = webAPISetting,
                    // AndroidExternalPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,
                    InnerSQLiteConnStr = innerSQLiteConnStr,
                    // ExternalSQLiteConnStr = externalSQLiteConnStr
                }
            );

            // 实现IOutput接口 - 用 Logcat 来实现
            App.Output = new MyOutput(); // TODO 未知道 iOS 是否有类似安卓的 LogUtil

            // 屏幕方向
            App.ScreenDirection = new ScreenDirection();

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            // 初始化百度定位
            // BaiduLBS myLBS = new BaiduLBS();
            MyLocation myLBS = MyLocation.GetInstance();
            App.LBS = myLBS;

            // 初始化Audio
            MyAudioPlayer audioPlayer = MyAudioPlayer.GetInstance();
            App.AudioPlayer = audioPlayer;

            // 初始化TTS
            MyTTS tts = MyTTS.GetInstance();
            App.TTS = tts;

            //// 初始化IR (苹果暂时没有红外)
            //MyIR ir = MyIR.GetInstance(ApplicationContext);
            //App.IR = ir;

            //// 初始化动态权限
            //MyPermission myPermission = new MyPermission();
            //App.Permission = myPermission;

            // 初始化 DevExpress.Mobile.Forms
            DevExpress.Mobile.Forms.Init();
            // 由于DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName 默认主题为 Themes.Dark, 
            // 这里初始化主题颜色为 Theme.Light
            DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName = DevExpress.Mobile.DataGrid.Theme.Themes.Light;
            DevExpress.Mobile.DataGrid.Theme.ThemeManager.RefreshTheme();
        }

        // XLabs
        private void initXLabs()
        {
            var resolverContainer = new global::XLabs.Ioc.SimpleContainer();
            resolverContainer.Register<XLabs.Platform.Services.Media.IMediaPicker, XLabs.Platform.Services.Media.MediaPicker>();
            XLabs.Ioc.Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}
