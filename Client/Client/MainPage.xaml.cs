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
            initUI();
            initEvent();
        }

        private void initUI()
        {
            lblTitle.Text = $"V {Xamarin.Essentials.VersionTracking.CurrentVersion}";

            if (Common.StaticInfo.DebugMode > 0)
            {
                lblTitle.Text = lblTitle.Text + $" (Debug模式{Common.StaticInfo.DebugMode})";
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // 回到主界面, 重新根据陀螺仪来改变屏幕方向
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Screen.Unspecified();
            });
        }

        private void initEvent()
        {
            #region 隐藏内容

            TapGestureRecognizer tapShowHidden = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 2,
                Command = new Command(() =>
                {
                    this.gHiddenContent.IsVisible = !this.gHiddenContent.IsVisible;
                })
            };
            this.btnShowHidden.GestureRecognizers.Add(tapShowHidden);

            this.btnBuBuGao.Clicked += BtnBuBuGao_Clicked;
            this.btnBuBuGao_Japanese.Clicked += BtnBuBuGao_Japanese_Clicked;
            this.btnGames.Clicked += BtnGames_Clicked;
            this.btnPageShuangSeQiu.Clicked += BtnPageShuangSeQiu_Clicked;
            this.btnH_Pow2_Brower.Clicked += BtnH_Pow2_Brower_Clicked;

            #endregion

            this.btnAllPage.Clicked += BtnAllPage_Clicked;
            this.btnLayoutDemoList.Clicked += BtnLayoutDemoList_Clicked;
            this.btnTimer.Clicked += BtnTimer_Clicked;
            this.btnPageLifecycle.Clicked += BtnPageLifecycle_Clicked;
            this.btnTestUnhandledExceptionHandler.Clicked += BtnTestUnhandledExceptionHandler_Clicked;
            this.btnPageOutputDemo.Clicked += BtnPageOutputDemo_Clicked;
            this.btnPageScreen.Clicked += BtnPageScreen_Clicked;
            this.btnPageDisplayAlertDemo.Clicked += BtnPageDisplayAlertDemo_Clicked;
            this.btnPageMessagingCenterDemo.Clicked += BtnPageMessagingCenterDemo_Clicked;
            this.btnPageSQLiteDemo.Clicked += BtnPageSQLiteDemo_Clicked;
            this.btnPageXamarinEssentialsDemo.Clicked += BtnPageXamarinEssentialsDemo_Clicked;
            this.btnPageWebServiceReferenceDemo.Clicked += BtnPageWebServiceReferenceDemo_Clicked;
            this.btnPageWebviewDemo.Clicked += BtnPageWebviewDemo_Clicked;
            this.btnPickerDemo.Clicked += BtnPickerDemo_Clicked;

            this.btnPageBluetoothDemo.Clicked += BtnPageBluetoothDemo_Clicked;
            this.btnPageIRDemo.Clicked += BtnPageIRDemo_Clicked;
            
            this.btnAudioPlayer.Clicked += BtnAudioPlayer_Clicked;
            // this.btnExcelByAsposeCell.Clicked += BtnExcelByAsposeCell_Clicked;
            this.btnSharpIf.Clicked += BtnSharpIf_Clicked;
            this.btnAccommodatingViewAndKeyboard.Clicked += BtnAccommodatingViewAndKeyboard_Clicked;
            this.btnGestureDemo.Clicked += BtnGestureDemo_Clicked;
            this.btnGesturePinchDemo.Clicked += BtnGesturePinchDemo_Clicked;
            this.btnMarqueeDemo.Clicked += BtnMarqueeDemo_Clicked;
            this.btnLabelDemo.Clicked += BtnLabelDemo_Clicked;
            this.btnXLabsDemo.Clicked += BtnXLabsDemo_Clicked;
            
            this.btnFFImageLoading.Clicked += BtnFFImageLoading_Clicked;
            this.btnFileExplorer.Clicked += BtnFileExplorer_Clicked;
            this.btnPluginMediaManagerForms.Clicked += BtnPluginMediaManagerForms_Clicked;
            this.btnExcelByAsposeCell.Clicked += BtnExcelByAsposeCell_Clicked;

            #region 实用工具

            this.btnBarcodeScanner.Clicked += btnBarcodeScanner_Clicked;
            this.btnSocketDemo.Clicked += BtnSocketDemo_Clicked;
            this.btnFakeSerailPort.Clicked += BtnFakeSerailPort_Clicked;
            this.btnPingDemo.Clicked += BtnPingDemo_Clicked;
            this.btnPingDemoMini.Clicked += BtnPingDemoMini_Clicked;

            #endregion

            #region 第三方DLL库测试

            this.btnPageZXingDemo.Clicked += btnPageZXingDemo_Clicked;

            this.btnAcrUserDialogsDemo.Clicked += btnAcrUserDialogsDemo_Clicked;

            this.btnDevExpress.Clicked += btnDevExpress_Clicked;

            this.btnTTSDemo.Clicked += btnTTSDemo_Clicked;

            this.btnPageBaiduMenu.Clicked += btnPageBaiduMenu_Clicked;

            #endregion

            #region 安卓功能测试

            this.btnAndroidIntentUtils_InstallAPK.Clicked += btnAndroidIntentUtils_InstallAPK_Clicked;
            this.btnAndroidPermission.Clicked += btnAndroidPermission_Clicked;
            this.btnAndroidPermissionV2.Clicked += btnAndroidPermissionV2_Clicked;

            #endregion

            #region 系统框架功能测试

            this.btnTest_CheckUpdate_DownloadFromApplication.Clicked += btnTest_CheckUpdate_DownloadFromApplication_Clicked;
            this.btnTest_CheckUpdate_DownloadFromBrowser.Clicked += btnTest_CheckUpdate_DownloadFromBrowser_Clicked;

            #endregion
        }


        #region 实用工具

        async void btnBarcodeScanner_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Common.ZXingBarcodeScanner("条码扫描(连续)", isScanContinuously: true));
        }

        #endregion

        #region 第三方DLL库测试

        async void btnAcrUserDialogsDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.AcrUserDialogDemo.PageTestAcrUserDialog());
        }

        #endregion

        #region 安卓功能测试

        async void btnAndroidIntentUtils_InstallAPK_Clicked(object sender, EventArgs e)
        {
            var p = System.IO.Path.Combine(Common.StaticInfo.AndroidExternalFilesPath, "testInstall.apk");
            if (System.IO.File.Exists(p) == false)
            {
                await DisplayAlert("异常", $"安装文件缺失。安装文件路径:{p}", "确认");
                return;
            }
            App.AndroidIntentUtils.InstallAPK(p);
        }

        #endregion

        #region 系统框架功能测试

        private void btnTest_CheckUpdate_DownloadFromBrowser_Clicked(object sender, EventArgs e)
        {
            new Client.Common.AppUpdateUtils(this).CheckUpdate_DownloadFromBrowser();

        }

        private void btnTest_CheckUpdate_DownloadFromApplication_Clicked(object sender, EventArgs e)
        {
            new Client.Common.AppUpdateUtils(this).CheckUpdate_DownloadFromApplication();
        }

        void btnAndroidPermission_Clicked(object sender, EventArgs e)
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

        async void btnAndroidPermissionV2_Clicked(object sender, EventArgs e)
        {
            if (Xamarin.Essentials.DeviceInfo.Platform != Xamarin.Essentials.DevicePlatform.Android)
            {
                await DisplayAlert("提示", "当前操作系统不是安卓平台", "确定");
                return;
            }

            List<string> toRequest = new List<string>(); // 需要申请的权限列表

            string args = string.Empty;

            // 外部存储设备读写权限
            args = "android.permission.READ_EXTERNAL_STORAGE";
            if (App.AndroidPermissionUtils.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            args = "android.permission.WRITE_EXTERNAL_STORAGE";
            if (App.AndroidPermissionUtils.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            // 网络定位
            args = "android.permission.ACCESS_COARSE_LOCATION";
            if (App.AndroidPermissionUtils.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            if (toRequest.Count > 0)
            {
                // TODO 安卓权限V2 Android 9 测试效果未达到理想效果
                App.AndroidPermissionUtils.RequestPermissions(toRequest.ToArray());
            }
            else
            {
                await DisplayAlert("提示", "没有需要请求的权限", "确定");
            }
        }


        #endregion

        async void BtnAllPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.AllPageDemo.AllPageList());
        }

        async void BtnLayoutDemoList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.LayoutDemo.LayoutDemoList());
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

        async void btnDevExpress_Clicked(object sender, EventArgs e)
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

        async void BtnPageScreen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageScreen());
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

        async void btnPageZXingDemo_Clicked(object sender, EventArgs e)
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

        async void BtnPingDemoMini_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MiniPing());
        }

        async void btnTTSDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageTTSMenu());
        }

        async void btnPageBaiduMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageBaiduMenu());
        }

        async void BtnPageBluetoothDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageBluetoothDemo());
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

        async void BtnFFImageLoading_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FFImageLoadingDemo.PageDemoMenu());
        }

        async void BtnFileExplorer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorerMenu());
        }

        void BtnPluginMediaManagerForms_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new View.MediaManagerDemo.PageMediaManagerDemo());
        }


        async void BtnH_Pow2_Brower_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.HPow2Brower());
        }

        private void BtnExcelByAsposeCell_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.ExcelUtils_Aspose.test();
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }


}
