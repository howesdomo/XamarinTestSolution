using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public class UpdateInfo
    {
        /// <summary>
        /// 平台
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 当前为最新版本
        /// </summary>
        public bool IsLastestVersion { get; set; }

        /// <summary>
        /// 强制用户更新到最新版本
        /// </summary>
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 更新提示语句
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件长度
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        /// 安卓下载地址
        /// </summary>
        public string AndroidURL { get; set; }
        public string AndroidURL_Debug { get; set; }

        /// <summary>
        /// 苹果下载地址
        /// </summary>
        public string iOSURL { get; set; }
        public string iOSURL_Debug { get; set; }

        /// <summary>
        /// 是 Debug模式
        /// 若是 ( >0 ) 返回 本机能方便调试的URL
        /// 若是 ( =0 ) 返回 app.enpot.com.cn 的URL
        /// </summary>
        public int DebugMode { get; set; }

    }
}
