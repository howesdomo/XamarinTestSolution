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
using Client.Common;

namespace Client.Droid
{
    public class MyOutput : IOutput
    {
        public void Info(string tag, string msg)
        {
            Android.Util.Log.Info(tag, msg);
        }

        public void Error(string tag, string msg)
        {
            Android.Util.Log.Error(tag, msg);
        }

        public void Warn(string tag, string msg)
        {
            Android.Util.Log.Warn(tag, msg);
        }

    }
}