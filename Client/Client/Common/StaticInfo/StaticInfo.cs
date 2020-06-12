using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    /// <summary>
    /// V 1.0.0 - 2020-3-21 12:35:17
    /// 首次稳定版标记
    /// </summary>
    public class StaticInfo
    {
        public static void Init(StaticInfoInitArgs args)
        {
            if (args.DebugMode.HasValue == true && args.DebugMode.Value >= 0)
            {
                _DebugMode = args.DebugMode.Value;
            }

            if (args.AppName.IsNullOrWhiteSpace() == false)
            {
                _AppName = args.AppName;
            }

            #region 服务器配置

            if (args.IP.IsNullOrWhiteSpace() == false)
            {
                _IP = args.IP;
            }

            if (args.Port.IsNullOrWhiteSpace() == false)
            {
                _Port = args.Port;
            }

            if (args.AppWebSetting != null)
            {
                _AppWebSetting = args.AppWebSetting;
            }

            if (args.WebAPISetting != null)
            {
                _WebAPISetting = args.WebAPISetting;
            }

            #endregion

            #region 程序内部存储路径 - iOS 与 Android 通用

            if (args.AppFilesPath.IsNullOrWhiteSpace() == false)
            {
                _AppFilesPath = args.AppFilesPath;
            }

            if (args.AppCachePath.IsNullOrWhiteSpace() == false)
            {
                _AppCachePath = args.AppCachePath;
            }

            #endregion

            #region 外部存储路径 - Android 特有

            // 安卓系统外部存储绝对路径
            if (args.AndroidExternalPath.IsNullOrWhiteSpace() == false)
            {
                _AndroidExternalPath = args.AndroidExternalPath;
            }

            // App外部缓存绝对路径 -- /{安卓系统外部存储路径}/Android/data/{appPackageName}/cache
            if (args.AndroidExternalCachePath.IsNullOrWhiteSpace() == false)
            {
                _AndroidExternalCachePath = args.AndroidExternalCachePath;
            }

            // App外部文件绝对路径 -- /{安卓系统外部存储路径}/Android/data/{appPackageName}/files
            if (args.AndroidExternalFilesPath.IsNullOrWhiteSpace() == false)
            {
                _AndroidExternalFilesPath = args.AndroidExternalFilesPath;
            }

            #endregion

            #region InnerSQLite
            if (args.InnerSQLiteConnStr.IsNullOrWhiteSpace() == false)
            {
                StaticInfo.InnerSQLiteConnStr = args.InnerSQLiteConnStr;
            }

            #endregion

            #region ExternalSQLite

            if (args.ExternalSQLiteConnStr.IsNullOrWhiteSpace() == false)
            {
                StaticInfo.ExternalSQLiteConnStr = args.ExternalSQLiteConnStr;
            }

            #endregion
        }

        private static int _DebugMode = 0;
        public static int DebugMode
        {
            get
            {
                return _DebugMode;
            }
            set
            {
                _DebugMode = value;
            }
        }

        private static String _AppName = "HoweApp";
        /// <summary>
        /// 程序名称
        /// </summary>
        public static string AppName
        {
            get
            {
                return _AppName;
            }
        }


        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static DL.Model.User CurrentUser;

        /// <summary>
        /// 当前运行设备信息
        /// </summary>
        private static Util.XamariN.Essentials.DeviceInfo _DeviceInfo;

        /// <summary>
        /// 当前运行设备信息
        /// </summary>
        public static Util.XamariN.Essentials.DeviceInfo DeviceInfo
        {
            get
            {
                if (_DeviceInfo == null)
                {
                    Util.XamariN.Essentials.IDeviceInfoUtils match = Xamarin.Forms.DependencyService.Get<Util.XamariN.Essentials.IDeviceInfoUtils>();
                    if (match == null)
                    {
                        throw new Exception("未能找到实现 Util.XamariN.Essentials.IDeviceInfoUtils 的依赖");
                    }
                    else
                    {
                        _DeviceInfo = match.GetDeviceInfo();
                    }
                }
                return _DeviceInfo;
            }
        }

        /// <summary>
        /// 当前运行设备平台
        /// </summary>
        public static string DeviceInfo_Platform
        {
            get
            {
                if (DeviceInfo == null)
                {
                    return string.Empty;
                }
                else
                {
                    return DeviceInfo.Platform.ToUpper();
                }
            }
        }

        /// <summary>
        /// 当前运行设备显示信息
        /// </summary>
        public static Util.XamariN.Essentials.DisplayInfo DisplayInfo
        {
            get
            {
                // 采用依赖注入接口方式处理 Android / iOS 的调用问题
                // 具体如何实现接口请参考 CoreUtil.XamariN.AndroiD , DisplayInfoUtils

                // 由于屏幕会旋转, 所以每次都获取最新信息
                Util.XamariN.Essentials.IDisplayInfoUtils match = Xamarin.Forms.DependencyService.Get<Util.XamariN.Essentials.IDisplayInfoUtils>();
                if (match == null)
                {
                    throw new Exception("未能找到实现 Util.XamariN.Essentials.IDisplayInfoUtils 的依赖");
                }
                else
                {
                    return match.GetDisplayInfo();
                }
            }
        }


        private static string _IP;
        /// <summary>
        /// 通用的IP
        /// </summary>
        public static string IP
        {
            get { return _IP; }
            set
            {
                _IP = value;
                ServiceSettingsUtils.UpdateIPOrAddress(value);
            }
        }

        private static string _Port;
        /// <summary>
        /// 通用的Port
        /// </summary>
        public static string Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                ServiceSettingsUtils.UpdatePort(value);
            }
        }


        private static WebSetting _AppWebSetting;
        public static WebSetting AppWebSetting
        {
            get { return _AppWebSetting; }
            set { _AppWebSetting = value; }
        }

        private static WebSetting _WebAPISetting;
        public static WebSetting WebAPISetting
        {
            get { return _WebAPISetting; }
            set { _WebAPISetting = value; }
        }

        #region 程序内部存储路径 - iOS 与 Android 通用

        private static string _AppFilesPath;
        public static string AppFilesPath
        {
            get { return _AppFilesPath; }
        }

        private static string _AppCachePath;
        public static string AppCachePath
        {
            get { return _AppCachePath; }
        }

        #endregion

        #region 外部存储路径 - Android 特有

        private static string _AndroidExternalPath;
        /// <summary>
        /// Android 外部存储器路径
        /// </summary>
        public static string AndroidExternalPath
        {
            get { return _AndroidExternalPath; }
        }

        private static string _AndroidExternalFilesPath;
        public static string AndroidExternalFilesPath
        {
            get { return _AndroidExternalFilesPath; }
        }

        private static string _AndroidExternalCachePath;
        public static string AndroidExternalCachePath
        {
            get { return _AndroidExternalCachePath; }
        }

        #endregion

        #region InnerSQLite

        /// <summary>
        /// 程序内部SQLite数据库连接字符串
        /// </summary>
        public static string InnerSQLiteConnStr { get; private set; }

        private static Client.Data.SQLiteDB _InnerSQLiteDB;

        /// <summary>
        /// 程序内部SQLite数据库
        /// </summary>
        public static Client.Data.SQLiteDB InnerSQLiteDB
        {
            get
            {
                if (_InnerSQLiteDB == null)
                {
                    if (StaticInfo.InnerSQLiteConnStr.IsNullOrWhiteSpace() == false)
                    {
                        _InnerSQLiteDB = new Client.Data.SQLiteDB(Util.Data_SQLite.LocationEnum.Inner, InnerSQLiteConnStr);
                    }
                }
                return _InnerSQLiteDB;
            }
        }

        #endregion

        #region ExternalSQLite

        public static string ExternalSQLiteConnStr { get; private set; }

        private static Client.Data.SQLiteDB _ExternalSQLiteDB;

        /// <summary>
        /// 外部存储器SQLite数据库
        /// </summary>
        public static Client.Data.SQLiteDB ExternalSQLiteDB
        {
            get
            {
                if (_ExternalSQLiteDB == null)
                {
                    if (StaticInfo.ExternalSQLiteConnStr.IsNullOrWhiteSpace() == false)
                    {

                        var fileInfo = new System.IO.FileInfo(StaticInfo.ExternalSQLiteConnStr);
                        if (System.IO.Directory.Exists(fileInfo.DirectoryName) == false)
                        {
                            System.IO.Directory.CreateDirectory(fileInfo.DirectoryName);
                        }

                        _ExternalSQLiteDB = new Client.Data.SQLiteDB(Util.Data_SQLite.LocationEnum.External, ExternalSQLiteConnStr);
                    }
                }
                return _ExternalSQLiteDB;
            }
        }

        #endregion
    }    
}