using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.ScreenShotTools
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageScreenshot : ContentPage
    {
        public PageScreenshot()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnScreenshot.Clicked += btnScreenshot_Clicked;
            this.btnScreenshotFromActivity.Clicked += btnScreenshotFromActivity_Clicked;
            this.btnScreenRecord_Start.Clicked += btnScreenRecord_Start_Clicked;
            this.btnScreenRecord_Stop.Clicked += btnScreenRecord_Stop_Clicked;
            this.btnThrowExceptionAndScreenshot.Clicked += btnThrowExceptionAndScreenshot_Clicked;
        }

        private void btnScreenshot_Clicked(object sender, EventArgs e)
        {
            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: async () =>
                {
                    try
                    {
                        App.AndroidScreenshot.SetIsSilent(false);
                        DateTime now = DateTime.Now;
                        App.AndroidScreenshot.OnScreenshot(now); // 第二个参数可以指定存放目录名称
                    }
                    catch (Exception ex)
                    {
                        string msg = $"{ex.GetFullInfo()}";
                        System.Diagnostics.Debug.WriteLine(msg);
                        await DisplayAlert(title: "捕获错误", message: ex.GetFullInfo(), cancel: "确定");
                    }

                },
                syncInvoke: null
            );
        }

        private void btnScreenshotFromActivity_Clicked(object sender, EventArgs e)
        {
            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: async () =>
                {
                    try
                    {
                        App.AndroidScreenshot.SetIsSilent(false);
                        DateTime now = DateTime.Now;
                        App.AndroidScreenshot.OnScreenshotFromActivity(now); // 第二个参数可以指定存放目录名称
                    }
                    catch (Exception ex)
                    {
                        string msg = $"{ex.GetFullInfo()}";
                        System.Diagnostics.Debug.WriteLine(msg);
                        await DisplayAlert(title: "捕获错误", message: ex.GetFullInfo(), cancel: "确定");
                    }

                },
                syncInvoke: null
            );
        }

        private async void btnScreenRecord_Start_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.PermissionStatus permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.Microphone>();

            //if (permission != Xamarin.Essentials.PermissionStatus.Granted)
            //{
            //    permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.Microphone>();
            //}

            //if (permission != Xamarin.Essentials.PermissionStatus.Granted)
            //{
            //    return;
            //}

            // TODO 报错未知原因

            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: async () =>
                {
                    try
                    {
                        App.AndroidScreenRecord.SetIsSilent(false);
                        App.AndroidScreenRecord.SetDpi(5);

                        DateTime now = DateTime.Now;
                    // string dirName = "exceptionScreenshots";
                    // string dirName = "testVideo";
                    App.AndroidScreenRecord.StartRecord(now);
                    }
                    catch (Exception ex)
                    {
                        string msg = $"{ex.GetFullInfo()}";
                        System.Diagnostics.Debug.WriteLine(msg);
                        await DisplayAlert(title: "捕获错误", message: ex.GetFullInfo(), cancel: "确定");
                    }
                },
                syncInvoke: null
            );
        }

        private void btnScreenRecord_Stop_Clicked(object sender, EventArgs e)
        {
            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: async () =>
                {
                    try
                    {
                        App.AndroidScreenRecord.StopRecord();
                    }
                    catch (Exception ex)
                    {
                        string msg = $"{ex.GetFullInfo()}";
                        System.Diagnostics.Debug.WriteLine(msg);
                        await DisplayAlert(title: "捕获错误", message: ex.GetFullInfo(), cancel: "确定");
                    }
                },
                syncInvoke: null
            );
        }

        private async void btnThrowExceptionAndScreenshot_Clicked(object sender, EventArgs e)
        {
            try
            {
                var r = await UserDialogs.Instance.ConfirmAsync(message: "抛出异常", title: "提示", okText: "确认", cancelText: "取消");
                if (r == true)
                {
                    throw new BusinessException("抛出异常");
                }
            }
            catch (Exception ex)
            {
                var r = await UserDialogs.Instance.ConfirmAsync(message: ex.GetFullInfo(), title: "捕获异常", okText: "提交异常报告", cancelText: "关闭");

                if (r == false)
                {
                    return;
                }

                App.AndroidScreenshot.SetIsSilent(true);
                DateTime now = DateTime.Now;
                App.AndroidScreenshot.OnScreenshotFromActivity(now);

                var fileInfo = App.AndroidScreenshot.Get_ScreenshotFileInfo(now);

                //var k = new ExceptionInfo();
                //k.ExceptionMessage = ex.Message;
                //k.ExceptionFullInfo = ex.GetFullInfo();
                //k.ImageFileInfo = fileInfo;
                //k.EntryTime = now;
            }
        }
    }
}