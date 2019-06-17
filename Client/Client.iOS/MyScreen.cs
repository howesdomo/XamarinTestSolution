using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Client.iOS
{
    public class MyScreen : Util.XamariN.IScreen
    {
        #region ScreenDirection

        // UIInterfaceOrientation.Unknown
        // UIInterfaceOrientation.LandscapeLeft
        // UIInterfaceOrientation.LandscapeRight
        // UIInterfaceOrientation.Portrait
        // UIInterfaceOrientation.PortraitUpsideDown

        /// <summary>
        /// 屏幕方向根据陀螺仪监控结果改变
        /// </summary>
        public void Unspecified()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Unknown), new NSString("orientation"));
        }

        /// <summary>
        /// 屏幕方向强制竖屏
        /// </summary>
        public void ForcePortrait()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
        }

        /// <summary>
        /// 屏幕方向强制倒转竖屏
        /// </summary>
        public void ForceReversePortrait()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.PortraitUpsideDown), new NSString("orientation"));
        }

        /// <summary>
        /// 屏幕方向强制向左横屏
        /// </summary>
        public void ForceLandscapeLeft()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
        }

        /// <summary>
        /// 屏幕方向强制向右横屏
        /// </summary>
        public void ForceLandscapeRight()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeRight), new NSString("orientation"));
        }

        /// <summary>
        /// 屏幕方向固定 ( 不根据陀螺仪监控结果改变 )
        /// </summary>
        public void ForceNosensor()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
        }

        #endregion

        #region ScreenKeepOn
        // TODO iOS系统 屏幕常亮代码实现

        //PowerManager mPowerManager = null;
        //PowerManager.WakeLock mWakeLock = null;

        /// <summary>
        /// 屏幕常亮
        /// Get 获取是否屏幕常亮状态
        /// Set 设置/取消 屏幕常亮
        /// </summary>
        public bool ScreenKeepOn { get; set; }

        #endregion

    }
}