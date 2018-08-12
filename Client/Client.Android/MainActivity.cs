using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using System.Text;
using Com.Baidu.Location;

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

            LoadApplication(new App());
        }

        private void init()
        {
            #region VN7

            //Common.WebSetting appWebSetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.215",
            //    port: "17911",
            //    appName: "AppWebApplication461/AppWebService.asmx"
            //); // TODO Read In webSetting.json

            //Common.WebSetting webAPISetting = new Common.WebSetting
            //(
            //    serviceSettingName: "A",
            //    ipOrWebAddress: "192.168.1.215",
            //    port: "17911",
            //    appName: "AppWebApplication461/api/orders"
            //); // TODO Read In webSetting.json

            #endregion

            #region HOME-PC

            Common.WebSetting appWebSetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.216",
                port: "17911",
                appName: "AppWebApplication461/AppWebService.asmx"
            ); // TODO Read In webSetting.json

            Common.WebSetting webAPISetting = new Common.WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.216",
                port: "17911",
                appName: "AppWebApplication461/api/orders"
            ); // TODO Read In webSetting.json

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

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            // 初始化百度定位
            BaiduLBS baiduLBS = new BaiduLBS(ApplicationContext);
        }
    }



}

