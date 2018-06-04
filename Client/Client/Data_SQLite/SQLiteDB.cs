using Client.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// howe 的 设计思路
/// 本cs为基础框架(方便项目迁移), 不在此进行具体逻辑编写
/// 请在 _p.cs 编写所有逻辑, 
/// </summary>
namespace Client.Data
{
    public partial class SQLiteDB
    {
        readonly SQLite.SQLiteAsyncConnection mDatabase;

        public SQLiteDB(LocationEnum dbLocation, string connStr)
        {
            mDatabase = new SQLiteAsyncConnection(connStr);
            switch (dbLocation)
            {
                case LocationEnum.Inner:
                    initInnerDB();
                    break;
                case LocationEnum.External:
                    initExternalDB();
                    break;

                default:
                    initInnerDB();
                    break;
            }
        }
    }

    public class DBVersion
    {
        [PrimaryKey]
        public string ID
        {
            get
            {
                return "DBVersion";
            }
            set
            {

            }
        }

        public LocationEnum Loaction { get; set; }

        public int Version { get; set; }

    }

    public enum LocationEnum
    {
        Inner = 0,
        External = 1
    }

}
