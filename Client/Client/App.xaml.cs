using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Client
{
	public partial class App : Application
	{
        public static Util.XamariN.IScreenDirection ScreenDirection { get; set; }

        public static Client.Common.ILBS LBS { get; set; }

        public static Client.Common.ITTS TTS { get; set; }

		public App ()
		{
			InitializeComponent();
            // MainPage = new MainPage(); // 源代码注释, 采用 MainPage = new NavigationPage(new MainPage()); 代替

            // *** 以下代码极为重要 ***
            // MainPage不采用 NavigationPage, 无法使用 Navigation.PushAsync(somePage) 方法跳转下一个界面
            MainPage = new NavigationPage(new MainPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
