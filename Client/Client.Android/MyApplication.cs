using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Client.Droid
{
    /// <summary>
    /// V 1.0.1 2019-9-18 14:35:23
    /// 1) 修复bug 记录日志路径
    /// 2) 优化 记录日志到 外部存储空间
    /// 
    /// 处理这些异常的时候，应用程序已经崩溃且无法恢复，这时android系统已经杀死了应用程序，
    /// 唯一能做的就是：记录异常、场景等重要信息，在合适的时候发送到服务器，以供错误分析
    /// </summary>
    [Application(Label = "XamarinTest")] // Label 的值影响 apk安装包在安装时显示的名称 若这里不填写任何值 则显示 Client.Android
    public class MyApplication : Application
    {
        public string mLogDirectory { get; set; }

        public MyApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            mLogDirectory = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android", "data", Application.Context.PackageName, "files", "Errors");

            // 注册未处理异常事件
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
            {
                Exception ex = e.ExceptionObject as Exception;
                string msg = ex.GetFullInfo();
                System.Diagnostics.Debug.WriteLine(msg);

                Util.LogUtils.Log
                (
                    content: $"AppDomain_UnhandledException\r\n{msg}",
                    baseDirectory: mLogDirectory
                );
            }
        }

        protected override void Dispose(bool disposing)
        {
            AndroidEnvironment.UnhandledExceptionRaiser -= AndroidEnvironment_UnhandledExceptionRaiser;

            base.Dispose(disposing);
        }

        void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            UnhandledExceptionHandler(e.Exception, e);
        }

        /// <summary>
        /// 处理未处理异常
        /// </summary>
        /// <param name="e"></param>
        private void UnhandledExceptionHandler(Exception ex, RaiseThrowableEventArgs e)
        {
            #region 处理程序（记录 异常、设备信息、时间等重要信息）

            try
            {
                string errorMsg = ex.GetFullInfo();
                System.Diagnostics.Debug.WriteLine(errorMsg);

                #region (选做) 信息上传到服务器

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("账号信息:");

                if (Client.Common.StaticInfo.CurrentUser == null)
                {
                    Client.Common.StaticInfo.CurrentUser = new DL.Model.User()
                    {
                        ID = Guid.Empty,
                        LoginAccount = "D3333",
                        UserName = "Howe",
                        DeviceInfo = Util.JsonUtils.SerializeObject(Common.StaticInfo.DeviceInfo)
                    };
                }

                sb.AppendLine(Client.Common.StaticInfo.CurrentUser.ID.ToString());
                sb.AppendLine(Client.Common.StaticInfo.CurrentUser.LoginAccount);
                sb.AppendLine(Client.Common.StaticInfo.CurrentUser.UserName);
                sb.AppendLine();
                sb.AppendLine("设备信息:");
                sb.AppendLine(Client.Common.StaticInfo.DeviceInfo.ToString());
                sb.AppendLine();
                sb.AppendLine("异常信息:");
                sb.AppendLine(errorMsg);

                Android.Util.Log.Error("UnhandledEx", errorMsg);

                new WebService().CollectUnhandleException
                (
                    sb.ToString(),
                    Client.Common.StaticInfo.CurrentUser
                 );

                #endregion

                Android.Util.Log.Error("UnhandledEx", errorMsg);

                Util.LogUtils.Log
                (
                    content: $"AndroidEnvironment.UnhandledExceptionRaiser\r\n{errorMsg}",
                    baseDirectory: mLogDirectory
                );
            }
            catch (Exception ex2)
            {
                string msg = "{0}".FormatWith(ex2.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }

            #endregion

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                Looper.Prepare();
                Toast.MakeText(this, "程序捕获到异常。", ToastLength.Long).Show();
                Looper.Loop();
            });

            e.Handled = true;
        }
    }
}