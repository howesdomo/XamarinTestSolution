using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    /// <summary>
    /// V 1.0.1 - 2020-10-16 15:25:28
    /// 1 增加停止定位方法
    /// 2 增加打开系统定位权限页面
    /// </summary>
    public interface ILBS
    {
        void GetGPSInfo(object args = null);

        /// <summary>
        /// 停止定位
        /// </summary>
        void Stop();

        /// <summary>
        /// 打开系统定位权限页面
        /// </summary>
        void Open_GPSSetting_InOS();
    }
}
