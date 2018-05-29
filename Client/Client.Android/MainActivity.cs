﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

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
            var deviceInfo = CoreUtil.XamariN.Essentials.DeviceInfoUtils.GetDeviceInfo();

            // TODO 可能弃用
            var displayInfo = CoreUtil.XamariN.Essentials.DisplayInfoUtils.GetDisplayInfo();

            string innerSQLiteConnStr = System.IO.Path.Combine
            (
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                CoreUtil.Principle.DatabaseName_SQLite
            );

            string externalSQLiteConnStr = System.IO.Path.Combine
            (
                "", // TODO android 外部存储路径
                CoreUtil.Principle.DatabaseName_SQLite
            );

            Client.Common.StaticInfo.Init
            (
                new Client.Common.StaticInfoInitArgs()
                {
                    AppName = "你好Xamarin",
                    DeviceInfo = deviceInfo,
                    InnerSQLiteConnStr = innerSQLiteConnStr,
                    ExternalSQLiteConnStr = externalSQLiteConnStr
                }
            );

            // 初始化条码扫描器
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

        }
    }
}

