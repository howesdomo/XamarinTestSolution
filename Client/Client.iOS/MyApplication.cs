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
            // DisplayCrashReport();
            HandleException("CurrentDomain_UnhandledException", new Exception(msg));
        }

        public static void TaskScheduler_UnobservedTaskException(object sender, System.Threading.Tasks.UnobservedTaskExceptionEventArgs e)
        {
            string msg = "{0}".FormatWith(e.Exception.GetFullInfo());
            System.Diagnostics.Debug.WriteLine(msg);
            // DisplayCrashReport();
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
                const string errorFilename = "Fatal.log";
                var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
                var errorFilePath = System.IO.Path.Combine(libraryPath, errorFilename);

                string[] directories = System.IO.Directory.GetDirectories(libraryPath);

                //if (System.IO.Directory.Exists(libraryPath) == true)
                //{
                //    getDir(libraryPath, 1);
                //}

                if (System.IO.File.Exists(errorFilePath) == true)
                {
                    var errorText = System.IO.File.ReadAllText(errorFilePath);
                    System.Diagnostics.Debug.WriteLine(errorText);
                }


                // TODO 将错误信息传导服务器中

                //var alert = UIAlertController.Create("捕获全局异常", ex.GetFullInfo(), UIAlertControllerStyle.Alert);
                //alert.AddAction(UIAlertAction.Create("Yes",
                //    UIAlertActionStyle.Default,
                //    null
                //    // TODO 将错误信息发送到服务器
                //    //action => 
                //    //{
                //    //    // throw new Exception("Throw Exception By Howe"); 
                //    //}
                // ));
                //Window.RootViewController.PresentViewController(viewControllerToPresent: alert, animated: false, completionHandler: null);


                //var alertView = new UIAlertView
                //(
                //    title: "Crash Report",
                //    message: ex.GetFullInfo(),
                //    del: null,
                //    cancelButtonTitle: "关闭",
                //    otherButtons: "Clear"
                //);

                //alertView.UserInteractionEnabled = true;

                //alertView.Clicked += (sender, args) =>
                //{
                //    System.Diagnostics.Debug.WriteLine("Click alterView");
                //};

                //alertView.Show();
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


        // #endregion

    }
}