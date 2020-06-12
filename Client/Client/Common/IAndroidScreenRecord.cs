using System;
using System.Collections.Generic;
using System.Text;

namespace Util.XamariN_Test
{
    public interface IAndroidScreenRecord
    {
        /// <summary>
        /// 开始屏幕录制
        /// </summary>
        void StartRecord(DateTime? imageFileDateTime = null, string dirName = "");

        /// <summary>
        /// 停止屏幕录制
        /// </summary>
        void StopRecord();

        /// <summary>
        /// 设置静默模式
        /// </summary>
        /// <param name="v"></param>
        void SetIsSilent(bool v);

        /// <summary>
        /// 设置 Dpi
        /// </summary>
        /// <param name="v"></param>
        void SetDpi(int v);
    }
}
