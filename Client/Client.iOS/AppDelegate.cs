using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public override bool FinishedLaunching(UIApplication uiApp, NSDictionary options)
        {
            #region 注册全局异常捕获事件

            AppDomain.CurrentDomain.UnhandledException += MyApplication.CurrentDomain_UnhandledException;
            System.Threading.Tasks.TaskScheduler.UnobservedTaskException += MyApplication.TaskScheduler_UnobservedTaskException;

            #endregion

            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental"); // 设置支持 CollectionView

            global::Xamarin.Forms.Forms.Init();
            var app = new App();
            LoadApplication(app);

            // Add by Howe

            init();

            // TODO Remove this
            // Aspose.Cells
            Client.App.ExcelUtils_Aspose = new AsposeCellsHelper();

            // End Add by Howe

            return base.FinishedLaunching(uiApp, options);
        }

        private void init()
        {
            #region 初始化 Common.StaticInfo

            var staticInfoInitArgs = new Common.StaticInfoInitArgs();

            staticInfoInitArgs.AppName = "XamarinTest";

            #region iOS项目路径赋值

            staticInfoInitArgs.AppFilesPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            staticInfoInitArgs.AppCachePath = System.IO.Path.Combine(staticInfoInitArgs.AppFilesPath, "..", "Library", "Cache");

            #endregion

            #region 服务器配置

            string pathServiceSettings = Common.ServiceSettingsUtils.GetConfigFilePath(argsDirPath: staticInfoInitArgs.AppFilesPath);

            if (System.IO.File.Exists(pathServiceSettings) == false)
            {
                Common.ServiceSettingsUtils.InitConfig(pathServiceSettings);
            }

            Common.ServiceSettingsUtils.ReadConfig(pathServiceSettings, staticInfoInitArgs);

            #endregion

            #region 本机配置

            string pathNativeSettings = Common.NativeSettingsUtils.GetConfigFilePath(argsDirPath: staticInfoInitArgs.AppFilesPath);

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

            //staticInfoInitArgs.ExternalSQLiteConnStr = System.IO.Path.Combine
            //(
            //    staticInfoInitArgs.AndroidExternalFilesPath,
            //    Util.Principle.DatabaseName_SQLite
            //);

            #endregion

            Common.StaticInfo.Init(staticInfoInitArgs);

            #endregion

            //// iOS 数据库存储位置 (拷贝自 TODO 项目), 值得参考
            //string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //string libFolder = System.IO.Path.Combine(docFolder, "..", "Library", "Databases");
            //if (!Directory.Exists(libFolder))
            //{
            //    Directory.CreateDirectory(libFolder);
            //}

            // 实现IOutput接口 - 用 Logcat 来实现
            App.Output = new MyOutput(); // TODO 未知道 iOS 是否有类似安卓的 LogUtil

            // 屏幕方向
            App.Screen = new MyScreen();

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            // 初始化定位
            App.LBS = MyLocation.GetInstance();

            // 初始化Audio
            App.AudioPlayer = MyAudioPlayer.GetInstance();

            // 初始化TTS
            App.TTS = MyTTS.GetInstance();

            //// 初始化IR ( iOS设备暂时未发现有红外装置 )
            //App.IR = MyIR.GetInstance(ApplicationContext);

            //// 初始化动态权限 ( iOS还没有需要 )

            //// 初始化Bluetooth ( iOS未能使用Xamarin.iOS的代码监测到蓝牙设备 )







            #region 初始化第三方 DLL 库

            // 初始化 Acr.UserDialogs
            // Nothing is necessary any longer as of v4.x.  There is an Init function for iOS but it is OPTIONAL and only required if you want/need to control
            // the top level viewcontroller for things like iOS extensions.Progress prompts will not use this factory function though!


            // 初始化 DevExpress.Mobile.Forms
            DevExpress.Mobile.Forms.Init();
            // 由于DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName 默认主题为 Themes.Dark, 
            // 这里初始化主题颜色为 Theme.Light
            DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName = DevExpress.Mobile.DataGrid.Theme.Themes.Light;
            DevExpress.Mobile.DataGrid.Theme.ThemeManager.RefreshTheme();


            // FFImageLoading ( Gif 动图 ) ( SVG 矢量图显示 )
            var svgAssembly = typeof(FFImageLoading.Svg.Forms.SvgCachedImage).GetTypeInfo().Assembly;
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();


            // Plugin.MediaManager.Forms ( 视频播放 )
            // MediaManager.Forms.Platforms.iOS.VideoViewRenderer.Init();
            // Plugin.MediaManager.Forms.iOS.VideoViewRenderer.Init();


            // 初始化 XLab
            var resolverContainer = new global::XLabs.Ioc.SimpleContainer();
            resolverContainer.Register<XLabs.Platform.Services.Media.IMediaPicker, XLabs.Platform.Services.Media.MediaPicker>();
            XLabs.Ioc.Resolver.SetResolver(resolverContainer.GetResolver());


            // TODO Aspose.Cells
            #endregion
        }
    }
}
