using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Client
{
    public partial class App : Application
    {
        public static Util.XamariN.IScreen Screen { get; set; }

        public static Client.Common.ILBS LBS { get; set; }

        public static Client.Common.IAudioPlayer AudioPlayer { get; set; }

        public static Client.Common.ITTS TTS { get; set; }

        public static Client.Common.I_IR IR { get; set; }

        public static Client.Common.IOutput Output { get; set; }

        public static Client.Common.IPermission Permission { get; set; }

        public static Client.Common.IBluetooth Bluetooth { get; set; }

        public static Client.Common.IExcelUtils_Aspose ExcelUtils_Aspose { get; set; }

        public App()
        {
            InitializeComponent();

#if DEBUG

            HotReloader.Current.Run(this);

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
