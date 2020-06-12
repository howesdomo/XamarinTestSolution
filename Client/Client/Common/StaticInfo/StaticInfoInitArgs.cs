using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    /// <summary>
    /// V 1.0.0 - 2020-3-21 12:35:17
    /// 首次稳定版标记
    /// </summary>
    public class StaticInfoInitArgs
    {
        /// <summary>
        /// 调试模式
        /// </summary>
        public int? DebugMode { get; set; }

        /// <summary>
        /// 程序名称
        /// </summary>
        public string AppName { get; set; }

        public string IP { get; set; }

        public string Port { get; set; }

        /// <summary>
        /// App Web 服务配置
        /// </summary>
        public WebSetting AppWebSetting { get; set; }

        public WebSetting WebAPISetting { get; set; }

        #region 程序内部存储路径 - iOS 与 Android 通用

        public string AppFilesPath { get; set; }

        public string AppCachePath { get; set; }

        #endregion

        #region 外部存储路径 - Android 特有

        /// <summary>
        /// Android 外部存储器路径
        /// </summary>
        public string AndroidExternalPath { get; set; }

        public string AndroidExternalFilesPath { get; set; }

        public string AndroidExternalCachePath { get; set; }

        #endregion


        /// <summary>
        /// 程序内部SQLite数据库连接字符串
        /// </summary>
        public string InnerSQLiteConnStr { get; set; }

        /// <summary>
        /// 外部存储器SQLite数据库连接字符串
        /// </summary>
        public string ExternalSQLiteConnStr { get; set; }
    }
}
