using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Client
{
	public partial class App : Application
	{
        public static Client.Common.ILBS LBS { get; set; }

		public App ()
		{
			InitializeComponent();
            // MainPage = new MainPage(); // Դ����ע��, ���� MainPage = new NavigationPage(new MainPage()); ����

            // *** ���´��뼫Ϊ��Ҫ ***
            // MainPage������ NavigationPage, �޷�ʹ�� Navigation.PushAsync(somePage) ������ת��һ������
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
