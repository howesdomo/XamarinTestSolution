using Acr.UserDialogs;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
        }
    }

    public class PageScreenshot_ViewModel : BaseViewModel
    {
        public Command CMD_Screenshot { get; private set; }

        public Command CMD_ScreenshotFromActivity { get; private set; }

        public Command CMD_ThrowExceptionAndScreenshot { get; private set; }

        public Command CMD_ScreenRecord_Start { get; private set; }

        public Command CMD_ScreenRecord_Stop { get; private set; }

        public Command CMD_ScreenRecord_Share { get; private set; }

        public PageScreenshot_ViewModel()
        {
            initCommand();
        }

        void initCommand()
        {
            this.CMD_Screenshot = new Command(screenshot);
            this.CMD_ScreenshotFromActivity = new Command(screenshotFromActivity);
            this.CMD_ThrowExceptionAndScreenshot = new Command(throwExceptionAndScreenshot);
            this.CMD_ScreenRecord_Start = new Command(screenRecord_Start);
            this.CMD_ScreenRecord_Stop = new Command(screenRecord_Stop);
            this.CMD_ScreenRecord_Share = new Command(screenRecord_Share2Tencent);
        }

        void screenshot()
        {
            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: () =>
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                        {
                            Title = "捕获异常",
                            Message = ex.GetInfo(),
                            OkText = "确认"
                        });
                    }

                },
                syncInvoke: null
            );
        }

        void screenshotFromActivity()
        {
            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: () =>
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                        {
                            Title = "捕获异常",
                            Message = ex.GetInfo(),
                            OkText = "确认"
                        });
                    }

                },
                syncInvoke: null
            );
        }


        async void throwExceptionAndScreenshot()
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

        private static DateTime? s_ScreenRecordDateTime;

        void screenRecord_Start()
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

            s_ScreenRecordDateTime = DateTime.Now;

            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    try
                    {
                        App.AndroidScreenRecord.SetIsSilent(false);
                        App.AndroidScreenRecord.SetDpi(5);
                        
                        // string dirName = "exceptionScreenshots";
                        // string dirName = "testVideo";
                        App.AndroidScreenRecord.StartRecord(new DateTime(s_ScreenRecordDateTime.Value.Ticks));
                    }
                    catch (Exception ex)
                    {
                        string msg = $"{ex.GetFullInfo()}";
                        System.Diagnostics.Debug.WriteLine(msg);
                        Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                        {
                            Title = "捕获异常",
                            Message = ex.GetInfo(),
                            OkText = "确认"
                        });
                    }
                },
                syncInvoke: null
            );
        }

        void screenRecord_Stop()
        {
            App.ThrottleAction.Throttle
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    try
                    {
                        App.AndroidScreenRecord.StopRecord();
                    }
                    catch (Exception ex)
                    {
                        string msg = $"{ex.GetFullInfo()}";
                        System.Diagnostics.Debug.WriteLine(msg);
                        Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                        {
                            Title = "捕获异常",
                            Message = ex.GetInfo(),
                            OkText = "确认"
                        });
                    }
                },
                syncInvoke: null
            );
        }

        void screenRecord_Share2Tencent()
        {
            if (s_ScreenRecordDateTime.HasValue == false)
            {

                Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                {
                    Title = "捕获异常",
                    Message = "最近未进行录像",
                    OkText = "确认",                    
                });

                return;
            }

            FileInfo fi = App.AndroidScreenRecord.Get_ScreenVideoFileInfo(s_ScreenRecordDateTime);
            if (fi.Exists == false)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                {
                    Title = "捕获异常",
                    Message = "录像文件不存在",
                    OkText = "确认",
                });

                return;
            }

            App.MyShareUtils.ShareFile(fi.FullName, new List<string>() { "tencent" }, "腾讯系");
        }



    }
}