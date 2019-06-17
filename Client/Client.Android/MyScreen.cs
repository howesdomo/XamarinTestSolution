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
        #region 构造函数 + 单例模式

        private MyScreen()
        {

        }

        private static MyScreen s_Instance;

        private Activity mAppActivity { get; set; }

        private static object objLock = new object();

        public static MyScreen GetInstance(Android.App.Activity activity = null)
        {
            lock (objLock)
            {
                if (s_Instance == null)
                {
                    s_Instance = new MyScreen();
                    if (s_Instance.mAppActivity == null && activity == null)
                    {
                        throw new Exception("MyBluetooth.GetInstance 蓝牙单例首次创建, 请传入 activity 参数");
                    }
                    if (activity != null)
                    {
                        s_Instance.mAppActivity = activity;
                    }
                }
                return s_Instance;
            }
        }

        #endregion        

        #region ScreenDirection

        public void Unspecified()
        {
            this.mAppActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
        }

        public void ForcePortrait()
        {
            this.mAppActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
        }

        public void ForceReversePortrait()
        {
            this.mAppActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.ReversePortrait;
        }

        public void ForceLandscapeLeft()
        {
            this.mAppActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.ReverseLandscape;
        }

        public void ForceLandscapeRight()
        {
            this.mAppActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
        }

        public void ForceNosensor()
        {
            this.mAppActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Nosensor;
        }

        #endregion

        #region ScreenKeepOn

        PowerManager mPowerManager = null;
        PowerManager.WakeLock mWakeLock = null;

        private bool _IsScreenKeepOn = false;

        /// <summary>
        /// 屏幕常亮
        /// Get 获取是否屏幕常亮状态
        /// Set 设置/取消 屏幕常亮
        /// </summary>
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
                mPowerManager = (PowerManager)mAppActivity.GetSystemService(Context.PowerService);
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