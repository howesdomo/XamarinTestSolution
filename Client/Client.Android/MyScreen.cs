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
    public class MyScreen : Util.XamariN.IScreen
    {
        MainActivity mMainActivity { get; set; }

        public MyScreen(MainActivity mainActivity)
        {
            mMainActivity = mainActivity;
        }

        #region ScreenDirection

        public void Unspecified()
        {
            this.mMainActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
        }

        public void ForcePortrait()
        {
            this.mMainActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
        }

        public void ForceReversePortrait()
        {
            this.mMainActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.ReversePortrait;
        }

        public void ForceLandscapeLeft()
        {
            this.mMainActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.ReverseLandscape;
        }

        public void ForceLandscapeRight()
        {
            this.mMainActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
        }

        public void ForceNosensor()
        {
            this.mMainActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Nosensor;
        }

        #endregion

        #region ScreenKeepOn

        PowerManager mPowerManager = null;
        PowerManager.WakeLock mWakeLock = null;

        private bool _IsScreenKeepOn = false;

        public bool ScreenKeepOn
        {
            get
            {
                return _IsScreenKeepOn;
            }
            set
            {
                if (_IsScreenKeepOn == value)
                {
                    return;
                }

                if (value == true)
                {
                    this.screenKeepOn_ActualMethod();
                }
                else
                {
                    this.screenCancelKeepOn_ActualMethod();
                }

                _IsScreenKeepOn = value;
            }
        }

        private void screenKeepOn_ActualMethod()
        {
            if (mPowerManager == null)
            {
                mPowerManager = (PowerManager)mMainActivity.GetSystemService(Context.PowerService);
            }

            if (mWakeLock == null)
            {
                mWakeLock = mPowerManager.NewWakeLock(WakeLockFlags.ScreenBright, "MyWakeLock");
            }

            mWakeLock.Acquire();
        }

        private void screenCancelKeepOn_ActualMethod()
        {
            if (mPowerManager == null || mWakeLock == null)
            {
                return;
            }

            mWakeLock.Release();
            mWakeLock = null;
        }

        #endregion

    }
}