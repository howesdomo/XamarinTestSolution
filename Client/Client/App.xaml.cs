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

        

        public static Client.Common.IExcelUtils_Aspose ExcelUtils_Aspose { get; set; }

        #region ��׿

        public static Util.XamariN.IAndroidAssetsUtils AndroidAssetsUtils { get; set; }

        public static Util.XamariN.IAndroidIntentUtils AndroidIntentUtils { get; set; }

        public static Util.XamariN.IAndroidPermission AndroidPermissionUtils { get; set; }

        public static Util.XamariN.IAndroidPermission AndroidPermissionUtils_InTestSolution { get; set; }

        #endregion

        public App()
        {
            InitializeComponent();

#if DEBUG
            // VS 2019 16.3 �汾��, �����õ� Hot Reload

            // HotReloader.Current.Run(this);

            //HotReloader.Current.Run(this, new HotReloader.Configuration
            //{
            //    ExtensionIpAddress = System.Net.IPAddress.Parse("192.168.1.215") // ��д����Ե�ip
            //});
#endif

            // MainPage = new MainPage(); // Դ����ע��, ���� MainPage = new NavigationPage(new MainPage()); ����

            // *** ���´��뼫Ϊ��Ҫ ***
            // MainPage������ NavigationPage, �޷�ʹ�� Navigation.PushAsync(somePage) ������ת��һ������
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
