using Client.Model;
using Client.View;
using Client.View.BaiduDemo;
using Client.View.WebviewDemo;
using Client.View.PickerDemo;
using Client.View.TTSDemo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnGames.Clicked += BtnGames_Clicked;
            this.btnPageLifecycle.Clicked += BtnPageLifecycle_Clicked;
            this.btnTestUnhandledExceptionHandler.Clicked += BtnTestUnhandledExceptionHandler_Clicked;
            this.btnPageScreenDirection.Clicked += BtnPageScreenDirection_Clicked;
            this.btnPageDisplayAlertDemo.Clicked += BtnPageDisplayAlertDemo_Clicked;
            this.btnPageMessagingCenterDemo.Clicked += BtnPageMessagingCenterDemo_Clicked;
            this.btnPageSQLiteDemo.Clicked += BtnPageSQLiteDemo_Clicked;
            this.btnPageZXingDemo.Clicked += BtnPageZXingDemo_Clicked;
            this.btnPageXamarinEssentialsDemo.Clicked += BtnPageXamarinEssentialsDemo_Clicked;
            this.btnPageWebServiceReferenceDemo.Clicked += BtnPageWebServiceReferenceDemo_Clicked;
            this.btnPageWebviewDemo.Clicked += BtnPageWebviewDemo_Clicked;
            this.btnPickerDemo.Clicked += BtnPickerDemo_Clicked;
            this.btnPingDemo.Clicked += BtnPingDemo_Clicked;
            this.btnTTSDemo.Clicked += BtnTTSDemo_Clicked;
            this.btnPageBaiduMenu.Clicked += BtnPageBaiduMenu_Clicked;
            this.btnPageIRDemo.Clicked += BtnPageIRDemo_Clicked;
        }

        async void BtnGames_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.Games.PageGamesList());
        }

        async void BtnPageLifecycle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageDemo.Page_Lifecycle());
        }

        async void BtnTestUnhandledExceptionHandler_Clicked(object sender, EventArgs e)
        {
            if (Common.StaticInfo.CurrentUser == null)
            {
                Common.StaticInfo.CurrentUser = new DL.Model.User()
                {
                    ID = Guid.Empty,
                    LoginAccount = "D3333",
                    UserName = "Howe",
                    DeviceInfo = Util.JsonUtils.SerializeObject(Common.StaticInfo.DeviceInfo)
                };
            }

            bool r = await DisplayAlert("警告", "确认测试全局捕获异常, 按确认将会抛出。", "确认", "取消");
            if (r == true)
            {
                throw new Exception("我来测试全局捕获异常");
            }
        }

        async void BtnPageScreenDirection_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageScreenDirection());
        }

        async void BtnPageDisplayAlertDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageDisplayAlertDemoList());
        }

        async void BtnPageMessagingCenterDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageMessagingCenterDemo());
        }

        async void BtnPageSQLiteDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSQLiteDemo_NoteList());
        }

        async void BtnPageZXingDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageZXingDemo());
        }

        async void BtnPageXamarinEssentialsDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageXamarinEssentialsDemo());
        }

        async void BtnPageWebServiceReferenceDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageWebServiceDemo());
        }

        async void BtnPageWebviewDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageWebviewMenu());
        }

        async void BtnPickerDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickerDemoPage());
        }

        async void BtnPingDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PingDemo());
        }

        async void BtnTTSDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageTTSMenu());
        }

        async void BtnPageBaiduMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageBaiduMenu());
        }

        async void BtnPageIRDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.IRDemo.PageIRDemo());
        }
    }


}
