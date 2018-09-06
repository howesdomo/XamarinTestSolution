﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using System.Text;
using Com.Baidu.Location;
using Android.Content;

namespace Client.Droid
{
    [Activity(Label = "Client", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // Add by Howe
            init();
            // End Add by Howe

            var app = new App();
            LoadApplication(app);

            // 为了用 ContentPageAdv 实现, Navigation Back 按钮的监控
            // 为了能进入 OnOptionsItemSelected 事件, 采用 Android.Support.V7.Widget.Toolbar 代替默认的 Toolbar
            Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        private void init()
        {
            #region VN7

            Common.WebSetting appWebSetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.215",
                port: "17911",
                appName: "AppWebApplication461/APPWebServiceHandler.ashx"
            ); // TODO Read In webSetting.json

            Common.WebSetting webAPISetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.215",
                port: "17911",
                appName: "AppWebApplication461/api/orders"
            ); // TODO Read In webSetting.json

            #endregion

            #region HOME-PC

            //Common.WebSetting appWebSetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.216",
            //    port: "17911",
            //    appName: "AppWebApplication461/AppWebService.asmx"
            //); // TODO Read In webSetting.json

            //Common.WebSetting webAPISetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.216",
            //    port: "17911",
            //    appName: "AppWebApplication461/api/orders"
            //); // TODO Read In webSetting.json

            #endregion


            string innerSQLiteConnStr = System.IO.Path.Combine
            (
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                Util.Principle.DatabaseName_SQLite
            );

            string externalSQLiteConnStr = System.IO.Path.Combine
            (
                "", // TODO android 外部存储路径
                Util.Principle.DatabaseName_SQLite
            );

            Client.Common.StaticInfo.Init
            (
                new Client.Common.StaticInfoInitArgs()
                {
                    AppName = "你好Xamarin",
                    AppWebSetting = appWebSetting,
                    WebAPISetting = webAPISetting,
                    InnerSQLiteConnStr = innerSQLiteConnStr,
                    ExternalSQLiteConnStr = externalSQLiteConnStr
                }
            );

            // 屏幕方向
            App.ScreenDirection = new ScreenDirection(this);

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            // 初始化百度定位
            BaiduLBS baiduLBS = new BaiduLBS(ApplicationContext);
            App.LBS = baiduLBS;

            // 初始化TTS
            MyTTS tts = MyTTS.GetInstance();
            App.TTS = tts;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == MyTTS.TTS_RequestCode)
            {
                MyTTS.GetInstance().Handle_OnActivityResult(requestCode, resultCode, data);
            }
        }

        /// <summary>
        /// 用 ContentPageAdv 实现, Navigation Back 按钮的监控
        /// 核心代码
        /// 
        /// * 注意 * 必须使用V7.Widget.Toolbar, 请在 onCreate 中加上以下2行代码
        /// Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
        /// SetSupportActionBar(toolbar);
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // check if the current item id 
            // is equals to the back button id
            if (item.ItemId == 16908332) // 返回按钮ID必定为 16908332
            {
                Xamarin.Forms.ContentPageAdv currentpage = null;
                try
                {
                    var currentStack = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack;
                    Xamarin.Forms.Page tmp = currentStack[currentStack.Count - 1];
                    // NavigationStack.LastOrDefault(); // 找不到 LastOrDefault 扩展方法
                    if (tmp is Xamarin.Forms.ContentPageAdv) // 避免强转程序报错
                    {
                        currentpage = tmp as Xamarin.Forms.ContentPageAdv;
                    }
                }
                catch (Exception ex)
                {
                    string msg = "{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg);
                }

                // check if the page has subscribed to 
                // the custom back button event
                if (currentpage?.CustomBackButtonAction != null)
                {
                    // invoke the Custom back button action
                    currentpage?.CustomBackButtonAction.Invoke();
                    // and disable the default back button action
                    return false;
                }

                // if its not subscribed then go ahead 
                // with the default back button action
                return base.OnOptionsItemSelected(item);
            }
            else
            {
                // since its not the back button 
                //click, pass the event to the base
                return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            // this is not necessary, but in Android user 
            // has both Nav bar back button and
            // physical back button its safe 
            // to cover the both events

            // retrieve the current xamarin forms page instance
            Xamarin.Forms.ContentPageAdv currentpage = null;
            try
            {
                var currentStack = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack;
                Xamarin.Forms.Page tmp = currentStack[currentStack.Count - 1];
                // NavigationStack.LastOrDefault(); // 找不到 LastOrDefault 扩展方法
                if (tmp is Xamarin.Forms.ContentPageAdv) // 避免强转程序报错
                {
                    currentpage = tmp as Xamarin.Forms.ContentPageAdv;
                }
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }

            // NavigationStack.LastOrDefault();

            // check if the page has subscribed to 
            // the custom back button event
            if (currentpage?.CustomBackButtonAction != null)
            {
                currentpage?.CustomBackButtonAction.Invoke();
            }
            else
            {
                base.OnBackPressed();
            }
        }

    }



}

