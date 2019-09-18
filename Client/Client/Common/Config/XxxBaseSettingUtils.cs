using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 本文件用于设置 最通用 & 最基本的配置 ( 方便拷贝到其他项目中使用 )
/// 请将项目中较为特殊的设置写在对应的 XxxSettingUtils.cs 内
/// </summary>
namespace Client.Common
{

    /// <summary>
    /// V 1.0.0 - 2019-9-17 15:37:39 编写 ServiceSettingsUtils 基础代码种子
    /// </summary>
    public partial class ServiceSettingsUtils
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public static string ConfigFileName
        {
            get
            {
                return "ServiceSettings.config";
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="argsDirPath">传入文件夹路径(可空)</param>
        /// <returns>配置文件路径</returns>
        public static string GetConfigFilePath(string argsDirPath = "")
        {
            if (argsDirPath.IsNullOrEmpty())
            {
                return System.IO.Path.Combine(StaticInfo.AndroidExternalFilesPath, ConfigFileName);
            }
            else
            {
                return System.IO.Path.Combine(argsDirPath, ConfigFileName);
            }
        }

        public static void UpdateIPOrAddress(string newValue)
        {
            string name = "IP";
            System.Xml.Linq.XDocument xDoc = System.Xml.Linq.XDocument.Load(GetConfigFilePath());
            ConfigUtils.GetElement(xDoc, name).Value = newValue;
            xDoc.Save(GetConfigFilePath());
        }

        public static void UpdatePort(string newValue)
        {
            string name = "Port";
            System.Xml.Linq.XDocument xDoc = System.Xml.Linq.XDocument.Load(GetConfigFilePath());
            ConfigUtils.GetElement(xDoc, name).Value = newValue;
            xDoc.Save(GetConfigFilePath());
        }
    }

    /// <summary>
    /// V 1.0.0 - 2019-9-17 15:37:39 编写 NativeSettingsUtils 基础代码种子
    /// </summary>
    public partial class NativeSettingsUtils
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public static string ConfigFileName
        {
            get
            {
                return "NativeSettings.config";
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="argsDirPath">传入文件夹路径(可空)</param>
        /// <returns>配置文件路径</returns>
        public static string GetConfigFilePath(string argsDirPath = "")
        {
            if (argsDirPath.IsNullOrEmpty() == true)
            {
                return System.IO.Path.Combine(StaticInfo.AndroidExternalFilesPath, ConfigFileName);
            }
            else
            {
                return System.IO.Path.Combine(argsDirPath, ConfigFileName);
            }
        }

        public static void UpdateDebugMode(string newValue)
        {
            string name = "DebugMode";
            System.Xml.Linq.XDocument xDoc = System.Xml.Linq.XDocument.Load(GetConfigFilePath());
            ConfigUtils.GetElement(xDoc, name).Value = newValue;
            xDoc.Save(GetConfigFilePath());
        }
    }
}
