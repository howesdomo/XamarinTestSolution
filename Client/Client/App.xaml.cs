using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Client
{
    public partial class App : Application
    {
        public static Util.XamariN.IScreenDirection ScreenDirection { get; set; }

        public static Client.Common.ILBS LBS { get; set; }

        public static Client.Common.ITTS TTS { get; set; }

        public static Client.Common.I_IR IR { get; set; }

        public static Client.Common.IOutput Output { get; set; }


        public App()
        {
            // ��ʼ�� Xamarin.LiveReload
#if DEBUG
            LiveReload.Init();
#endif

            InitializeComponent();

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
