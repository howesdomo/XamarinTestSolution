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
    public class ScreenDirection : Util.XamariN.IScreenDirection
    {
        MainActivity At { get; set; }

        public ScreenDirection(MainActivity at)
        {
            At = at;
        }

        public void Unspecified()
        {
            this.At.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
        }

        public void ForceLandscape()
        {
            this.At.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
        }

        public void ForceNosensor()
        {
            this.At.RequestedOrientation = Android.Content.PM.ScreenOrientation.Nosensor;
        }

        public void ForcePortrait()
        {
            this.At.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
        }


    }
}