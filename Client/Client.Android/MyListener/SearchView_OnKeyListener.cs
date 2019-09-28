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

namespace Client.Droid.MyListener
{
    public class SearchView_OnKeyListener : Java.Lang.Object, Android.Views.View.IOnKeyListener
    {

        public bool OnKey(Android.Views.View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            string msg = $"Hell Android{0}";
            System.Diagnostics.Debug.WriteLine(msg);

            System.Diagnostics.Debugger.Break();

            return false;
        }
    }
}