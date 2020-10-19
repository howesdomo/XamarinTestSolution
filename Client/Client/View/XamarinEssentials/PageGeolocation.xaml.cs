﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinEssentials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGeolocation : ContentPage
    {
        public PageGeolocation()
        {
            InitializeComponent();

            this.btnGetLocation.Clicked += btnGetLocation_Clicked;
        }

        private async void btnGetLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                var p = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (p != PermissionStatus.Granted)
                {
                    p = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (p != PermissionStatus.Granted)
                {
                    await DisplayAlert(title: "提示", message: "没有定位权限", cancel: "确定");
                    return;
                }

                //// 由于 GPS 经常抽风, 所以不使用最近的GPS信息
                //var l = await Geolocation.GetLastKnownLocationAsync();

                //if (l == null && l.Timestamp >= new DateTimeOffset(DateTime.Now.AddMinutes(-1)))
                //{
                //    l = await Geolocation.GetLocationAsync
                //    (
                //        new GeolocationRequest(accuracy: GeolocationAccuracy.Medium, timeout: TimeSpan.FromSeconds(3600))
                //    );
                //}

                var l = await Geolocation.GetLocationAsync
                (
                    new GeolocationRequest(accuracy: GeolocationAccuracy.Medium, timeout: TimeSpan.FromSeconds(3600))
                );

                if (l == null) { return; }

                lblGetLocation.Text = Util.JsonUtils.SerializeObjectWithFormatted(l);
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.GetFullInfo());

                System.Diagnostics.Debugger.Break();

            }

        }
    }
}