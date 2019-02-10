using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public class StaticInfo
    {
        public static void Init(StaticInfoInitArgs args)
        {
            if (args.AppName.IsNullOrWhiteSpace() == false)
            {
                _AppName = args.AppName;
            }

            if (args.AppWebSetting != null)
            {
                _AppWebSetting = args.AppWebSetting;
            }

            if (args.AndroidExternalPath.IsNullOrWhiteSpace() == false)
            {
                _AndroidExternalPath = args.AndroidExternalPath;
            }

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

        public static bool IsDebugMode
        {
            get
            {
                // 设计思路 
                // 外部存储器中读取指定路径文件, 判断 DebugMode, 一般 DebugMode 为false
                // 这样做可以灵活修改已发布的版本
                // 测试机只需要放置配置文件到指定路径即可
                bool r = false;

                try
                {
                    string a = Util.IO.FileUtils.GetString("");

                    if (a.Contains("debugMode:true"))
                    {
                        r = true;
                    }
                }
                catch (Exception ex)
                {
                    string msg = "{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg);
                }

                return r;
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
        /// 公司号
        /// </summary>
        public static String CompanyCode = "2000";

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


        private static string _AndroidExternalPath;

        /// <summary>
        /// Android 外部存储器路径
        /// </summary>
        public static string AndroidExternalPath
        {
            get { return _AndroidExternalPath; }
        }

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
                        _InnerSQLiteDB = new Client.Data.SQLiteDB(Data.LocationEnum.Inner, InnerSQLiteConnStr);
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

                        _ExternalSQLiteDB = new Client.Data.SQLiteDB(Data.LocationEnum.External, ExternalSQLiteConnStr);
                    }
                }
                return _ExternalSQLiteDB;
            }
        }

        #endregion
    }

    public class StaticInfoInitArgs
    {
        /// <summary>
        /// 程序名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// App Web 服务配置
        /// </summary>
        public WebSetting AppWebSetting { get; set; }

        public WebSetting WebAPISetting { get; set; }

        /// <summary>
        /// Android 外部存储器路径
        /// </summary>
        public string AndroidExternalPath { get; set; }

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
