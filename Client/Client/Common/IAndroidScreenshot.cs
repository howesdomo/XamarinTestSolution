using System;
using System.Collections.Generic;
using System.Text;

namespace Util.XamariN_Test
{
    public interface IAndroidScreenshot
    {
        /// <summary>
        /// 截取屏幕图片
        /// </summary>
        /// <param name="imageFileDateTime">时间参数 - 屏幕截图文件名命名规则所需</param>
        /// <param name="dirName">屏幕截图保存文件夹名称</param>
        void OnScreenshot(DateTime? imageFileDateTime = null, string dirName = "");

        /// <summary>
        /// 截取屏幕图片
        /// </summary>
        /// <param name="imageFileDateTime">时间参数 - 屏幕截图文件名命名规则所需</param>
        /// <param name="dirName">屏幕截图保存文件夹名称</param>
        void OnScreenshotFromActivity(DateTime? imageFileDateTime = null, string dirName = "");

        /// <summary>
        /// 设置静默模式
        /// </summary>
        /// <param name="v"></param>
        void SetIsSilent(bool v);
    }
}
