using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Client
{
    public partial class App : Application
    {
        public static Util.XamariN.IScreen Screen { get; set; }

        public static Util.XamariN.IAudioPlayer AudioPlayer { get; set; }

        public static Util.XamariN.ITTS TTS { get; set; }

        public static Util.XamariN.IBluetooth Bluetooth { get; set; }


        public static Client.Common.ILBS LBS { get; set; }

        public static Client.Common.I_IR IR { get; set; }

        public static Client.Common.IOutput Output { get; set; }

        public static Client.Common.IPermission Permission { get; set; }

        public static Client.Common.IExcelUtils_Aspose ExcelUtils_Aspose { get; set; }

        #region 安卓

        public static Util.XamariN.IAndroidAssetsUtils AndroidAssetsUtils { get; set; }

        public static Util.XamariN.IAndroidIntentUtils AndroidIntentUtils { get; set; }

        public static Util.XamariN.IAndroidPermission AndroidPermissionUtils { get; set; } // TODO

        #endregion

        public App()
        {
            InitializeComponent();

#if DEBUG

            HotReloader.Current.Run(this);

            // HotReloader.Current.Run(this);

            //HotReloader.Current.Run(this, new HotReloader.Configuration
            //{
            //    ExtensionIpAddress = System.Net.IPAddress.Parse("192.168.1.215") // 填写你电脑的ip
            //});
#endif

            // MainPage = new MainPage(); // 源代码注释, 采用 MainPage = new NavigationPage(new MainPage()); 代替

            // *** 以下代码极为重要 ***
            // MainPage不采用 NavigationPage, 无法使用 Navigation.PushAsync(somePage) 方法跳转下一个界面
            MainPage = new NavigationPage(new MainPage());
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
