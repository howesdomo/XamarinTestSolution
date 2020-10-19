using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// !!!!
// 迁移到 AssemblyInfo.cs 文件中
// [assembly: XamlCompilation(XamlCompilationOptions.Compile)] 

namespace Client
{
    public partial class App : Application
    {
        public static NavigationPage Navigation { get; set; }



        public static Util.XamariN.IScreen Screen { get; set; }

        public static Util.XamariN.IAudioPlayer AudioPlayer { get; set; }

        public static Util.XamariN.ITTS TTS { get; set; }

        public static Util.XamariN.IBluetooth Bluetooth { get; set; }

        public static Util.XamariN.IShareUtils MyShareUtils { get; set; }

        public static Client.Common.ILBS LBS { get; set; }

        public static Client.Common.I_IR IR { get; set; }

        public static Client.Common.IOutput Output { get; set; }

        

        public static Util.Excel.IExcelUtils ExcelUtils_Aspose { get; set; }


        #region 防止误触

        /// <summary>
        /// 默认防止误触间隔时间
        /// </summary>
        public static double ActionIntervalDefault { get { return 1000; } }

        /// <summary>
        /// 连续的多次调用，在每个时间段的周期内只执行第一次处理过程。
        /// </summary>
        public static Util.ActionUtils.ThrottleAction ThrottleAction { get; set; } = new Util.ActionUtils.ThrottleAction();

        /// <summary>
        /// 连续的多次调用，只有在调用停止之后的一段时间(指定间隔时间)内不再调用，然后才执行一次处理过程。
        /// </summary>
        public static Util.ActionUtils.DebounceAction DebounceAction { get; set; } = new Util.ActionUtils.DebounceAction();

        #endregion

        #region 安卓

        public static Util.XamariN.IAndroidAssetsUtils AndroidAssetsUtils { get; set; }

        public static Util.XamariN.IAndroidIntentUtils AndroidIntentUtils { get; set; }

        public static Util.XamariN.IAndroidPermission AndroidPermissionUtils { get; set; }

        public static Util.XamariN.IAndroidPermission AndroidPermissionUtils_InTestSolution { get; set; }

        public static Util.XamariN.IAndroidScreenshot AndroidScreenshot { get; set; }

        public static Util.XamariN.IAndroidScreenRecord AndroidScreenRecord { get; set; }

        #endregion
        
        public static Client.Common.IWechatOpenSDK MyWechatOpenSDK { get; set; }

        public App()
        {
            InitializeComponent();

            // 实验性标志
            Device.SetFlags(new string[]
            {
                "Expander_Experimental",
                "SwipeView_Experimental"
            });

#if DEBUG
            // VS 2019 16.3 版本后, 有内置的 Hot Reload

            // HotReloader.Current.Run(this);

            //HotReloader.Current.Run(this, new HotReloader.Configuration
            //{
            //    ExtensionIpAddress = System.Net.IPAddress.Parse("192.168.1.215") // 填写你电脑的ip
            //});
#endif

            // MainPage = new MainPage(); // 源代码注释, 采用 MainPage = new NavigationPage(new MainPage()); 代替

            // *** 以下代码极为重要 ***
            // MainPage不采用 NavigationPage, 无法使用 Navigation.PushAsync(somePage) 方法跳转下一个界面

            var nPage = new NavigationPage(new MainPage());
            
            App.Navigation = nPage;
            MainPage = nPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
