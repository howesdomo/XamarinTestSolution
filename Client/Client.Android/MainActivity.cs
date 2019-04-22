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
    [Activity(Label = "Client", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle); // Android Resources 在 raw 中放入 音频资源后报错, 挪动到首位后没有报错.
            global::Xamarin.Forms.Forms.Init(this, bundle);

            // Add by Howe

            init();
            initXLabs();

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

            string externalSQLiteConnStr = System.IO.Path.Combine
            (
                Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, // 获取 Android 外部存储路径
                Util.Principle.ExternalStorageDirectoryTemplate.FormatWith(Client.Common.StaticInfo.AppName, Util.Principle.DatabaseName_SQLite)
            );

            Client.Common.StaticInfo.Init
            (
                new Client.Common.StaticInfoInitArgs()
                {
                    AppName = "你好Xamarin",
                    AppWebSetting = appWebSetting,
                    WebAPISetting = webAPISetting,
                    AndroidExternalPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,
                    InnerSQLiteConnStr = innerSQLiteConnStr,
                    ExternalSQLiteConnStr = externalSQLiteConnStr
                }
            );

            // 设置软键盘显示时, View 自动调整适应
            // Window.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);

            // 实现IOutput接口 - 用 Logcat 来实现
            App.Output = new MyOutput();

            // 屏幕方向
            App.ScreenDirection = new ScreenDirection(this);

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            // 初始化百度定位
            BaiduLBS baiduLBS = new BaiduLBS(ApplicationContext);
            App.LBS = baiduLBS;

            // 初始化Audio
            MyAudioPlayer audioPlayer = MyAudioPlayer.GetInstance();
            App.AudioPlayer = audioPlayer;

            // 初始化TTS
            MyTTS tts = MyTTS.GetInstance();
            App.TTS = tts;

            // 初始化IR
            MyIR ir = MyIR.GetInstance(ApplicationContext);
            App.IR = ir;
            // 初始化动态权限

            MyPermission myPermission = new MyPermission();
            App.Permission = myPermission;

            // 初始化 DevExpress.Mobile.Forms
            DevExpress.Mobile.Forms.Init();
            // 由于DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName 默认主题为 Themes.Dark, 
            // 这里初始化主题颜色为 Theme.Light
            DevExpress.Mobile.DataGrid.Theme.ThemeManager.ThemeName = DevExpress.Mobile.DataGrid.Theme.Themes.Light;
            DevExpress.Mobile.DataGrid.Theme.ThemeManager.RefreshTheme();


            // 初始化 FFImageLoading
            // 2.4.4.589(稳定版)执行 init 方式会报错 
            // System.TypeLoadException: Could not load list of method overrides due to Method not found: void
            // Nuget 引用 2.4.5.880-pre
            // 1) Xamarin.FFImageLoading
            // 2) Xamarin.FFImageLoading.Forms
            // 然后以下的执行初始化语句
            var svgAssembly = typeof(FFImageLoading.Svg.Forms.SvgCachedImage).GetTypeInfo().Assembly;                                  // <-- 追加
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
        }

        // XLabs
        private void initXLabs()
        {
            var resolverContainer = new global::XLabs.Ioc.SimpleContainer();
            resolverContainer.Register<XLabs.Platform.Services.Media.IMediaPicker, XLabs.Platform.Services.Media.MediaPicker>();
            XLabs.Ioc.Resolver.SetResolver(resolverContainer.GetResolver());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == MyTTS.TTS_RequestCode)
            {
                MyTTS.GetInstance().Handle_OnActivityResult(requestCode, resultCode, data);
            }
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


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            doNext(requestCode, grantResults);
        }

        private void doNext(int requestCode, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == MyPermission.WRITE_EXTERNAL_STORAGE_REQUEST_CODE)
            {
                if (grantResults[0] == Permission.Granted) // 允许授权
                {
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        Looper.Prepare();
                        Toast.MakeText(this, "授权成功", ToastLength.Long).Show();
                        Looper.Loop();
                    });
                }
                else // 拒绝授权
                {
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        Looper.Prepare();
                        Toast.MakeText(this, "授权失败", ToastLength.Long).Show();
                        Looper.Loop();
                    });
                }
            }
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

