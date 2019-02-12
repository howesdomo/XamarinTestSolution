using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Client.iOS
{
    public static class MyApplication
    {
        // #region iOS 全局异常捕获

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string msg = "{0}".FormatWith(e.ExceptionObject.ToString());
            System.Diagnostics.Debug.WriteLine(msg);
            HandleException("CurrentDomain_UnhandledException", new Exception(msg));
        }

        public static void TaskScheduler_UnobservedTaskException(object sender, System.Threading.Tasks.UnobservedTaskExceptionEventArgs e)
        {
            string msg = "{0}".FormatWith(e.Exception.GetFullInfo());
            System.Diagnostics.Debug.WriteLine(msg);

            if (msg.IsNullOrWhiteSpace() == false 
                && msg.StartsWith(@"A Task's exception(s) were not observed either by Waiting on the Task or accessing its Exception property. As a result, the unobserved exception was rethrown by the finalizer thread.") == true)
            {
                // 暂时未能找出为什么经常会捕获到此异常的原因, 暂时忽略此错误
                return;
            }

            HandleException("TaskScheduler_UnobservedTaskException", e.Exception);
        }

        private static void HandleException(string from, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            System.Diagnostics.Debug.WriteLine("Has Exception from : {0}".FormatWith(from));
            System.Diagnostics.Debug.WriteLine("{0}".FormatWith(ex.GetFullInfo()));

            //#if DEBUG
            //            System.Diagnostics.Debugger.Break();
            //#endif

            try
            {
                //const string errorFilename = "Fatal.log";
                //var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
                //var errorFilePath = System.IO.Path.Combine(libraryPath, errorFilename);

                //string[] directories = System.IO.Directory.GetDirectories(libraryPath);

                ////if (System.IO.Directory.Exists(libraryPath) == true)
                ////{
                ////    getDir(libraryPath, 1);
                ////}

                //if (System.IO.File.Exists(errorFilePath) == true)
                //{
                //    var errorText = System.IO.File.ReadAllText(errorFilePath);
                //    System.Diagnostics.Debug.WriteLine(errorText);
                //}

                #region 处理程序（记录 异常、设备信息、时间等重要信息）

                StringBuilder sb = new StringBuilder();

                string errorMsg = ex.GetFullInfo();
                System.Diagnostics.Debug.WriteLine(errorMsg);

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

                // Android.Util.Log.Error("UnhandledEx", errorMsg); // TODO Log

                new WebService().CollectUnhandleException
                (
                    sb.ToString(),
                    Client.Common.StaticInfo.CurrentUser
                 );

                #endregion

                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                {
                    var alertView = new UIAlertView
                    (
                        title: "Crash Report",
                        message: sb.ToString(), // ex.GetFullInfo(),
                        del: null,
                        cancelButtonTitle: "关闭",
                        otherButtons: "Clear"
                    );

                    alertView.UserInteractionEnabled = true;

                    alertView.Clicked += (sender, args) =>
                    {
                        System.Diagnostics.Debug.WriteLine("Click alterView");
                    };

                    alertView.Show();
                });
            }
            catch (Exception ex2)
            {
#if DEBUG
                System.Diagnostics.Debugger.Break();
#endif

                System.Diagnostics.Debug.WriteLine("+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O+O");
                System.Diagnostics.Debug.WriteLine(ex2.GetFullInfo());
            }
        }


    }
}