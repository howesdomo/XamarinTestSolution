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
            #region 隐藏内容

            TapGestureRecognizer tapShowHidden = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 2,
                Command = new Command(() =>
                {
                    this.btnGames.IsVisible = !this.btnGames.IsVisible;
                    this.btnBuBuGao.IsVisible = !this.btnBuBuGao.IsVisible;
                    this.btnBuBuGao_Japanese.IsVisible = !this.btnBuBuGao_Japanese.IsVisible;
                    this.btnPageShuangSeQiu.IsVisible = !this.btnPageShuangSeQiu.IsVisible;
                })
            };
            this.btnShowHidden.GestureRecognizers.Add(tapShowHidden);

            this.btnBuBuGao.Clicked += BtnBuBuGao_Clicked;
            this.btnBuBuGao_Japanese.Clicked += BtnBuBuGao_Japanese_Clicked;
            this.btnGames.Clicked += BtnGames_Clicked;
            this.btnPageShuangSeQiu.Clicked += BtnPageShuangSeQiu_Clicked;

            #endregion

            this.btnAllPage.Clicked += BtnAllPage_Clicked;
            this.btnLayoutDemoList.Clicked += BtnLayoutDemoList_Clicked;
            this.btnTimer.Clicked += BtnTimer_Clicked;
            this.btnDevExpress.Clicked += BtnDevExpress_Clicked;
            this.btnPageLifecycle.Clicked += BtnPageLifecycle_Clicked;
            this.btnTestUnhandledExceptionHandler.Clicked += BtnTestUnhandledExceptionHandler_Clicked;
            this.btnPageOutputDemo.Clicked += BtnPageOutputDemo_Clicked;
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
            this.btnAndroidPermission.Clicked += BtnAndroidPermission_Clicked;
            this.btnAudioPlayer.Clicked += BtnAudioPlayer_Clicked;
            // this.btnExcelByAsposeCell.Clicked += BtnExcelByAsposeCell_Clicked;
            this.btnSharpIf.Clicked += BtnSharpIf_Clicked;
            this.btnAccommodatingViewAndKeyboard.Clicked += BtnAccommodatingViewAndKeyboard_Clicked;
            this.btnGestureDemo.Clicked += BtnGestureDemo_Clicked;
            this.btnGesturePinchDemo.Clicked += BtnGesturePinchDemo_Clicked;
            this.btnMarqueeDemo.Clicked += BtnMarqueeDemo_Clicked;
            this.btnLabelDemo.Clicked += BtnLabelDemo_Clicked;
            this.btnXLabsDemo.Clicked += BtnXLabsDemo_Clicked;
            this.btnSocketDemo.Clicked += BtnSocketDemo_Clicked;
            this.btnFakeSerailPort.Clicked += BtnFakeSerailPort_Clicked;
        }


        async void BtnAllPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.AllPageDemo.AllPageList());
        }

        async void BtnLayoutDemoList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.LayoutDemo.LayoutDemoList());
        }

        void BtnAndroidPermission_Clicked(object sender, EventArgs e)
        {
            // 通过校验
            // 1. android 平台
            // 2. android 6.0 及其以上版本

            #region 平台信息校验

            if (Common.StaticInfo.DeviceInfo.Platform.StartsWith("android", StringComparison.CurrentCultureIgnoreCase) == false)
            {
                string msg = "{0}".FormatWith("(无需进行动态权限设置)非android平台");
                System.Diagnostics.Debug.WriteLine(msg);
                return;
            }

            if (Common.StaticInfo.DeviceInfo.VersionMajor < 6)
            {
                string msg = "{0}".FormatWith("(无需进行动态权限设置)版本号小于6");
                System.Diagnostics.Debug.WriteLine(msg);
                return;
            }

            #endregion

            List<string> toRequest = new List<string>(); // 需要申请的权限列表

            string args = string.Empty;

            // 外部存储设备读写权限
            args = "android.permission.READ_EXTERNAL_STORAGE";
            if (App.Permission.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            args = "android.permission.WRITE_EXTERNAL_STORAGE";
            if (App.Permission.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            // 网络定位
            args = "android.permission.ACCESS_COARSE_LOCATION";
            if (App.Permission.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }


            // 需要申请的权限列表 大于 0
            if (toRequest.Count > 0)
            {
                App.Permission.RequestPermissions(toRequest.ToArray());
            }
        }

        async void BtnGames_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.Games.PageGamesList());
        }

        async void BtnTimer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageTimer());
        }

        async void BtnPageShuangSeQiu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.ShuangSeQiu.PageShuangSeQiu());
        }

        async void BtnDevExpress_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.DevExpressDemo.PageDevExpressDemoMenu());
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

        async void BtnPageOutputDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Client.View.PageOutputDemo());
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

        async void BtnAudioPlayer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageAudioDemo());
        }

        //async void BtnExcelByAsposeCell_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new View.PageExcel_Aspose());
        //}

        async void BtnBuBuGao_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.BuBuGao.PageBuBuGao1());
        }


        async void BtnBuBuGao_Japanese_Clicked(object sender, EventArgs e)
        {
            //View.BuBuGao.Question q = new View.BuBuGao.Question();
            //q.Name = "50音片假名";
            //q.Words = new List<View.BuBuGao.Word>()
            //{
            //    new View.BuBuGao.Word(){ Content = "ア" },
            //    new View.BuBuGao.Word(){ Content = "イ" },
            //    new View.BuBuGao.Word(){ Content = "ウ" },
            //    new View.BuBuGao.Word(){ Content = "エ" },
            //    new View.BuBuGao.Word(){ Content = "オ" },
            //    new View.BuBuGao.Word(){ Content = "カ" },
            //    new View.BuBuGao.Word(){ Content = "キ" },
            //    new View.BuBuGao.Word(){ Content = "ク" },
            //    new View.BuBuGao.Word(){ Content = "ケ" },
            //    new View.BuBuGao.Word(){ Content = "コ" },
            //    new View.BuBuGao.Word(){ Content = "サ" },
            //    new View.BuBuGao.Word(){ Content = "シ" },
            //    new View.BuBuGao.Word(){ Content = "ス" },
            //    new View.BuBuGao.Word(){ Content = "セ" },
            //    new View.BuBuGao.Word(){ Content = "ソ" },
            //    new View.BuBuGao.Word(){ Content = "タ" },
            //    new View.BuBuGao.Word(){ Content = "チ" },
            //    new View.BuBuGao.Word(){ Content = "ツ" },
            //    new View.BuBuGao.Word(){ Content = "テ" },
            //    new View.BuBuGao.Word(){ Content = "ト" },
            //    new View.BuBuGao.Word(){ Content = "ナ" },
            //    new View.BuBuGao.Word(){ Content = "ニ" },
            //    new View.BuBuGao.Word(){ Content = "ヌ" },
            //    new View.BuBuGao.Word(){ Content = "ネ" },
            //    new View.BuBuGao.Word(){ Content = "ノ" },
            //    new View.BuBuGao.Word(){ Content = "ハ" },
            //    new View.BuBuGao.Word(){ Content = "ヒ" },
            //    new View.BuBuGao.Word(){ Content = "フ" },
            //    new View.BuBuGao.Word(){ Content = "ヘ" },
            //    new View.BuBuGao.Word(){ Content = "ホ" },
            //    new View.BuBuGao.Word(){ Content = "マ" },
            //    new View.BuBuGao.Word(){ Content = "ミ" },
            //    new View.BuBuGao.Word(){ Content = "ム" },
            //    new View.BuBuGao.Word(){ Content = "メ" },
            //    new View.BuBuGao.Word(){ Content = "モ" },
            //    new View.BuBuGao.Word(){ Content = "ヤ" },
            //    new View.BuBuGao.Word(){ Content = "ユ" },
            //    new View.BuBuGao.Word(){ Content = "ヨ" },
            //    new View.BuBuGao.Word(){ Content = "ラ" },
            //    new View.BuBuGao.Word(){ Content = "リ" },
            //    new View.BuBuGao.Word(){ Content = "ル" },
            //    new View.BuBuGao.Word(){ Content = "レ" },
            //    new View.BuBuGao.Word(){ Content = "ロ" },
            //    new View.BuBuGao.Word(){ Content = "ワ" },
            //    new View.BuBuGao.Word(){ Content = "ヲ" },
            //    new View.BuBuGao.Word(){ Content = "ン" },
            //};
            //q.CreateDateTimeValue = DateTime.Now.Ticks;
            //App.TTS.SetSpeechRateSilent(0.2f);
            //await Navigation.PushAsync(new View.BuBuGao_Japanese.PageBuBuGao2(q, true));

            await Navigation.PushAsync(new View.BuBuGao_Japanese.PageBuBuGao1());
        }

        async void BtnSharpIf_Clicked(object sender, EventArgs e)
        {
            string msg = "没有进入代码块";

#if __IOS__
            msg = "Run at iOS(进入代码块)";
#elif __ANDROID__
            msg = "Run at Android(进入代码块)";
#endif

            if (msg.Equals("没有进入代码块") == true)
            {
                switch (Common.StaticInfo.DeviceInfo_Platform)
                {
                    case "ANDROID": msg = "Run at Android (没有进入代码块)"; break;
                    case "IOS": msg = "Run at iOS (没有进入代码块)"; break;
                }
            }

            await DisplayAlert("提示", msg, "确认");
        }

        async void BtnAccommodatingViewAndKeyboard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.AccommodatingViewAndKeyboard.Page1());
        }

        async void BtnGestureDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.GestureDemo.PageGestureDemo());
        }

        async void BtnGesturePinchDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.GestureDemo.PagePinch());
        }

        async void BtnMarqueeDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.MarqueeDemo.MarqueeDemoList());
        }

        async void BtnLabelDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.LabelDemo.LabelDemo_V1());
        }

        async void BtnXLabsDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.XLabDemo.PageCamera1());
        }

        async void BtnSocketDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.SocketDemo.PageSocketDemo());
        }


        async void BtnFakeSerailPort_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FakeSerialPort.PageFakeSerialPort());
        }

    }


}
