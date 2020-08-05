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
using Client.View.ScreenShotTools;
using Xamarin.Forms.PlatformConfiguration;

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
            #region HoweTools

            TapGestureRecognizer tapShowHidden = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 2,
                Command = new Command(() =>
                {
                    this.gHiddenContent.IsVisible = !this.gHiddenContent.IsVisible;
                })
            };

            this.btnShowHidden.GestureRecognizers.Add(tapShowHidden);

            this.btnBuBuGao.Clicked += btnBuBuGao_Clicked;
            this.btnBuBuGao_Japanese.Clicked += btnBuBuGao_Japanese_Clicked;
            this.btnGames.Clicked += BtnGames_Clicked;
            this.btnPageShuangSeQiu.Clicked += BtnPageShuangSeQiu_Clicked;
            this.btnH_Pow2_Brower.Clicked += btnH_Pow2_Brower_Clicked;
            this.btnTimer.Clicked += btnTimer_Clicked;

            #endregion            

            #region 实用工具

            this.btnBarcodeScanner.Clicked += btnBarcodeScanner_Clicked;
            this.btnFakeSerailPort.Clicked += btnFakeSerailPort_Clicked;
            this.btnPingDemo.Clicked += btnPingDemo_Clicked;
            this.btnPingDemoMini.Clicked += btnPingDemoMini_Clicked;
            this.btnSocketDemo.Clicked += btnSocketDemo_Clicked;
            this.btnPageBluetoothDemo.Clicked += btnPageBluetoothDemo_Clicked;

            #endregion

            #region 待测试

            this.btnXLabsDemo.Clicked += btnXLabsDemo_Clicked;

            this.btnFFImageLoading.Clicked += btnFFImageLoading_Clicked;

            this.btnFileExplorer.Clicked += btnFileExplorer_Clicked;

            this.btnPluginMediaManagerForms.Clicked += btnPluginMediaManagerForms_Clicked;

            this.btnExcelByAsposeCell.Clicked += btnExcelByAsposeCell_Clicked;

            this.btnMarqueeDemo.Clicked += btnMarqueeDemo_Clicked;

            this.btnLongPressEffects.Clicked += btnLongPressEffects_Clicked;

            this.btnRadioButtonDemo.Clicked += btnRadioButtonDemo_Clicked;

            this.btnMultiSelectDemo.Clicked += btnMultiSelectDemo_Clicked;

            this.btnColorList.Clicked += btnColorList_Clicked;

            #endregion

            #region 常用功能(已测试)

            this.btnPageSQLiteDemo.Clicked += btnPageSQLiteDemo_Clicked;

            this.btnAudioPlayer.Clicked += btnAudioPlayer_Clicked;

            this.btnTTSDemo.Clicked += btnTTSDemo_Clicked;

            this.btnPageScreen.Clicked += BtnPageScreen_Clicked;

            this.btnSendEMail.Clicked += btnSendEMail_Clicked;

            #endregion

            #region 第三方DLL库测试

            this.btnPageXamarinEssentialsDemo.Clicked += BtnPageXamarinEssentialsDemo_Clicked;

            this.btnPageZXingDemo.Clicked += btnPageZXingDemo_Clicked;

            this.btnAcrUserDialogsDemo.Clicked += btnAcrUserDialogsDemo_Clicked;

            this.btnDevExpress.Clicked += btnDevExpress_Clicked;

            this.btnPageBaiduMenu.Clicked += btnPageBaiduMenu_Clicked;

            #endregion

            #region 安卓功能测试

            this.btnPageIRDemo.Clicked += btnPageIRDemo_Clicked;
            this.btnAndroidIntentUtils_InstallAPK.Clicked += btnAndroidIntentUtils_InstallAPK_Clicked;
            this.btnAndroidPermission.Clicked += btnAndroidPermission_Clicked;
            // this.btnAndroidPermissionV2.Clicked += btnAndroidPermissionV2_Clicked;
            this.btnAndroidPermissionV3.Clicked += btnAndroidPermissionV3_Clicked;
            this.btnAndroidScreenshotTools.Clicked += btnAndroidScreenshotTools_Clicked;

            #endregion

            #region iOS功能测试

            this.btnAccommodatingViewAndKeyboard.Clicked += btnAccommodatingViewAndKeyboard_Clicked;

            #endregion

            #region 系统框架功能测试

            this.btnTest_CheckUpdate_DownloadFromApplication.Clicked += btnTest_CheckUpdate_DownloadFromApplication_Clicked;
            this.btnTest_CheckUpdate_DownloadFromBrowser.Clicked += btnTest_CheckUpdate_DownloadFromBrowser_Clicked;

            #endregion

            #region C#测试

            this.btnSharpIf.Clicked += btnSharpIf_Clicked;

            #endregion

            #region Xamarin.Forms 基本功能

            this.btnAllPage.Clicked += BtnAllPage_Clicked;
            this.btnLayoutDemoList.Clicked += BtnLayoutDemoList_Clicked;
            this.btnButtonDemo.Clicked += BtnButtonDemo_Clicked;
            this.btnAnimation.Clicked += BtnAnimation_Clicked;
            this.btnPageLifecycle.Clicked += BtnPageLifecycle_Clicked;
            this.btnTestUnhandledExceptionHandler.Clicked += BtnTestUnhandledExceptionHandler_Clicked;
            this.btnPageDisplayAlertDemo.Clicked += BtnPageDisplayAlertDemo_Clicked;
            this.btnPageOutputDemo.Clicked += BtnPageOutputDemo_Clicked;
            this.btnPageMessagingCenterDemo.Clicked += BtnPageMessagingCenterDemo_Clicked;
            this.btnPageWebviewDemo.Clicked += BtnPageWebviewDemo_Clicked;
            this.btnPickerDemo.Clicked += BtnPickerDemo_Clicked;
            this.btnPageWebServiceReferenceDemo.Clicked += BtnPageWebServiceReferenceDemo_Clicked;
            this.btnGestureDemo.Clicked += BtnGestureDemo_Clicked;
            this.btnGesturePinchDemo.Clicked += BtnGesturePinchDemo_Clicked;
            this.btnLabelDemo.Clicked += BtnLabelDemo_Clicked;
            this.btnXamarinComponentDemo.Clicked += btnXamarinComponentDemo_Clicked;

            #endregion

            #region Xamarin.Forms 新特性

            this.btnXamarinFormsFeatures_4p3.Clicked += BtnXamarinFormsFeatures_4p3_Clicked;
            this.btnXamarinFormsFeatures_4p6.Clicked += btnXamarinFormsFeatures_4p6_Clicked;

            #endregion
        }



        #region HoweTools

        async void btnBuBuGao_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.BuBuGao.PageBuBuGao1());
        }

        async void btnBuBuGao_Japanese_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.BuBuGao_Japanese.PageBuBuGao1());
        }

        async void BtnGames_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.Games.PageGamesList());
        }

        async void BtnPageShuangSeQiu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.ShuangSeQiu.PageShuangSeQiu());
        }

        async void btnTimer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageTimer());
        }

        #endregion

        #region 实用工具

        async void btnBarcodeScanner_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Common.ZXingBarcodeScanner("条码扫描(连续)", isScanContinuously: true));
        }

        async void btnFakeSerailPort_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FakeSerialPort.PageFakeSerialPort());
        }

        async void btnPingDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PingDemo());
        }

        async void btnPingDemoMini_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MiniPing());
        }

        async void btnSocketDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.SocketDemo.PageSocketDemo());
        }

        async void btnPageBluetoothDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageBluetoothDemo());
        }

        async void btnH_Pow2_Brower_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.HPow2Brower());
        }

        #endregion

        #region 待测试

        async void btnXLabsDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.XLabDemo.PageCamera1());
        }

        async void btnFFImageLoading_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FFImageLoadingDemo.PageDemoMenu());
        }

        async void btnFileExplorer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorerMenu());
        }

        void btnPluginMediaManagerForms_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new View.MediaManagerDemo.PageMediaManagerDemo());
        }

        private void btnExcelByAsposeCell_Clicked(object sender, EventArgs e)
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

        async void btnLongPressEffects_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.EffectsDemo.PageLongPressEffects());
        }

        async void btnRadioButtonDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.RadioButtonDemo.PageRadioButtonDemo());
        }

        async void btnMultiSelectDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.MultiSelectDemo.PageDemo_MultiSelect());
        }

        async void btnColorList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.ColorListDemo.PageColorListDemo());
        }

        #endregion        

        #region 常用功能(已测试)

        async void btnPageSQLiteDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSQLiteDemo_NoteList());
        }

        async void btnTTSDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageTTSMenu());
        }

        async void btnAudioPlayer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.PageAudioDemo());
        }

        async void BtnPageScreen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageScreen());
        }

        async void btnSendEMail_Clicked(object sender, EventArgs e)
        {
            try
            {
                string senderAddress = "howe@enpot.com.cn";

                var r = await Acr.UserDialogs.UserDialogs.Instance.PromptAsync(new Acr.UserDialogs.PromptConfig()
                {
                    Title = "请输入密码",
                    Message = $"请输入邮箱 {senderAddress} 密码",
                    OkText = "确认",
                    CancelText = "取消"
                });

                if (r.Ok == false)
                {
                    return;
                }

                string password = r.Text;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;
                smtp.Host = "smtp.qiye.163.com";
                smtp.Port = 25;
                // smtp.UseDefaultCredentials = true; // 与 PC 的 DBAutoBackup 不同点, Xamarin.Android 运行这句会报错, 注释后能正常发送邮件
                smtp.Credentials = new System.Net.NetworkCredential(senderAddress, password);

                List<string> receive = new List<string>()
                {
                    "howe@enpot.com.cn" // senderAddress
                };

                System.Net.Mail.MailPriority mailPriority = System.Net.Mail.MailPriority.Normal;

                string subject = "Xamarin.Forms 发送邮件测试";
                string content = $"发送邮件测试{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
                mailPriority = System.Net.Mail.MailPriority.Normal;

                List<string> attachmentPathList = new List<string>();

                new Util.EMailUtils().SendEMail
                (
                    sender: senderAddress,
                    smtp: smtp,
                    receiverList: receive,
                    subject: subject,
                    content: content,
                    attachmentPathList: attachmentPathList,
                    mailPriority: mailPriority
                );
            }
            catch (Exception ex)
            {
                await DisplayAlert(title: "捕获错误", message: ex.GetFullInfo(), cancel: "确定");
            }
        }

        #endregion

        #region 第三方DLL库测试

        async void btnPageZXingDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageZXingDemo());
        }

        async void btnAcrUserDialogsDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.AcrUserDialogDemo.PageTestAcrUserDialog());
        }

        async void btnDevExpress_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.DevExpressDemo.PageDevExpressDemoMenu());
        }


        async void btnPageBaiduMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageBaiduMenu());
        }


        #endregion

        #region 安卓功能测试

        async void btnPageIRDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.IRDemo.PageIRDemo());
        }

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

        [Obsolete] // 弃用改用 Xamarin.Essentials.Permissions 进行授权
        async void btnAndroidPermission_Clicked(object sender, EventArgs e)
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
            if (App.AndroidPermissionUtils_InTestSolution.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            args = "android.permission.WRITE_EXTERNAL_STORAGE";
            if (App.AndroidPermissionUtils_InTestSolution.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            // 网络定位
            args = "android.permission.ACCESS_COARSE_LOCATION";
            if (App.AndroidPermissionUtils_InTestSolution.CheckPermission(args) == false)
            {
                toRequest.Add(args);
            }

            if (toRequest.Count <= 0)
            {
                await DisplayAlert("提示", "没有需要请求的权限", "确定");
            }
            else // 需要申请的权限列表 大于 0
            {
                App.AndroidPermissionUtils_InTestSolution.RequestPermissions(toRequest.ToArray());
            }
        }

        [Obsolete] // 弃用改用 Xamarin.Essentials.Permissions 进行授权
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

            if (toRequest.Count <= 0)
            {
                await DisplayAlert("提示", "没有需要请求的权限", "确定");
            }
            else // 需要申请的权限列表 大于 0
            {
                // TODO 安卓权限V2 Android 9 测试效果未达到理想效果
                App.AndroidPermissionUtils.RequestPermissions(toRequest.ToArray());
            }
        }

        async void btnAndroidPermissionV3_Clicked(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Essentials.PermissionStatus permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.Camera>();
                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.Camera>();
                }

                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    System.Diagnostics.Debug.WriteLine("摄像头权限授权失败");
                    return;
                }

                permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.Microphone>();
                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.Microphone>();
                }

                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    System.Diagnostics.Debug.WriteLine("麦克风权限授权失败");
                    return;
                }

                permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.StorageRead>();
                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.StorageRead>();
                }

                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    System.Diagnostics.Debug.WriteLine("读取外部存储权限授权失败");
                    return;
                }

                permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.StorageWrite>();
                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.StorageWrite>();
                }

                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    System.Diagnostics.Debug.WriteLine("读取外部存储权限授权失败");
                    return;
                }

                permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.StorageWrite>();
                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.StorageWrite>();
                }

                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    System.Diagnostics.Debug.WriteLine("写入外部存储权限授权失败");
                    return;
                }


                permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
                }

                if (permission != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    System.Diagnostics.Debug.WriteLine("获取GPS信息(在使用App期间)权限授权失败");
                    return;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(title: "捕获错误", message: ex.GetFullInfo(), cancel: "确定");
            }
        }

        void btnAndroidScreenshotTools_Clicked(object sender, EventArgs e)
        {
            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: async () =>
                {
                    await Navigation.PushAsync(new PageScreenshot());
                },
                syncInvoke: null
            );
        }

        #endregion

        #region iOS功能测试

        async void btnAccommodatingViewAndKeyboard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.AccommodatingViewAndKeyboard.Page1());
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

        #endregion

        async void btnSharpIf_Clicked(object sender, EventArgs e)
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

        #region Xamarin.Forms 基本功能

        async void BtnAllPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.AllPageDemo.AllPageList());
        }

        async void BtnLayoutDemoList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.LayoutDemo.LayoutDemoList());
        }

        async void BtnButtonDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.XamarinFormsBasicTest.PageButtonTest());
        }

        async void BtnAnimation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.XamarinFormsBasicTest.PageAnimationTest());
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

        async void BtnPageDisplayAlertDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageDisplayAlertDemoList());
        }

        async void BtnPageMessagingCenterDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageMessagingCenterDemo());
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

        async void BtnGestureDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.GestureDemo.PageGestureDemo());
        }

        async void BtnGesturePinchDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.GestureDemo.PagePinch());
        }

        async void BtnLabelDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.LabelDemo.LabelDemo_V1());
        }

        async void btnXamarinComponentDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.XamarinComponentDemo.PageXamarinComponentMenu());
        }

        async void btnMarqueeDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.MarqueeDemo.MarqueeDemoList());
        }

        #endregion

        #region Xamarin.Forms 新特性


        async void BtnXamarinFormsFeatures_4p3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.XF_4p3_Features.PageMain());

        }

        async void btnXamarinFormsFeatures_4p6_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.XF_4p6_Features.PageMain());
        }

        #endregion
    }
}
