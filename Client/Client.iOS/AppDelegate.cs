using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Client.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //// Start 注册全局异常捕获事件
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //System.Threading.Tasks.TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //// End


            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        //#region iOS 全局异常捕获

        //private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    string msg = "{0}".FormatWith(e.ExceptionObject.ToString());
        //    System.Diagnostics.Debug.WriteLine(msg);
        //    DisplayCrashReport();
        //}

        //private void TaskScheduler_UnobservedTaskException(object sender, System.Threading.Tasks.UnobservedTaskExceptionEventArgs e)
        //{
        //    string msg = "{0}".FormatWith(e.Exception.GetFullInfo());
        //    System.Diagnostics.Debug.WriteLine(msg);
        //    DisplayCrashReport();
        //}

        /////<summary>
        ////If there is an unhandled exception, the exception information is diplayed 
        ////on screen the next time the app is started (only in debug configuration)
        /////</summary>
        //[System.Diagnostics.Conditional("DEBUG")]
        //private static void DisplayCrashReport()
        //{
        //    const string errorFilename = "Fatal.log";
        //    var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
        //    var errorFilePath = System.IO.Path.Combine(libraryPath, errorFilename);
        //    if (System.IO.File.Exists(errorFilePath) == false)
        //    {
        //        return;
        //    }
        //    var errorText = System.IO.File.ReadAllText(errorFilePath);

        //    #region UIAlert 已被弃用

        //    //var alertView = new UIAlertView
        //    //(
        //    //    title: "Crash Report",
        //    //    message: errorText,
        //    //    del: null,
        //    //    cancelButtonTitle: "关闭",
        //    //    otherButtons: "Clear"
        //    //);

        //    //alertView.UserInteractionEnabled = true;

        //    //alertView.Clicked += (sender, args) =>
        //    //{
        //    //    if (args.ButtonIndex != 0)
        //    //    {
        //    //        System.IO.File.Delete(errorFilePath);
        //    //    }
        //    //};
        //    //alertView.Show();

        //    #endregion


        //    UIAlertController alert = UIAlertController.Create("Login", "Enter your credentials", UIAlertControllerStyle.Alert);

        //    alert.AddAction(UIAlertAction.Create("Login", UIAlertActionStyle.Default, action => {
        //        // This code is invoked when the user taps on login, and this shows how to access the field values
        //        Console.WriteLine("User: {0}/Password: {1}", alert.TextFields[0].Text, alert.TextFields[1].Text);
        //    }));

        //    alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, myCancel));
        //    alert.AddTextField((field) => {
        //        field.Placeholder = "email address";
        //    });
        //    alert.AddTextField((field) => {
        //        field.SecureTextEntry = true;
        //    });
        //    PresentViewController(alert, animated: true, completionHandler: null);

        //}

        //#endregion
    }
}
