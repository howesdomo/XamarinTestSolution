using Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Data
{
    public partial class SQLiteDB
    {
        /// <summary>
        /// 1 初始化数据库结构 和 数据
        /// 2 根据数据库版本号升级数据库结构
        /// </summary>
        public void initExternalDB()
        {
            DBVersion dbVersion = null;
            mDatabase.CreateTableAsync<DBVersion>().Wait();

            var taskR = mDatabase.Table<DBVersion>().FirstOrDefaultAsync();
            dbVersion = taskR.Result;

            if (dbVersion == null || dbVersion.Version < 1)
            {
                dbVersion = new DBVersion();
                initExternalDBStruct_v1(dbVersion);
            }

            // 2 根据数据库版本号升级数据库结构
            if (dbVersion.Version < 2)
            {
                updateExternalDBStruct_v2(dbVersion); // update database struct script
            }

            if (dbVersion.Version < 3)
            {
                updateExternalDBStruct_v3(dbVersion);// update database struct script
            }
        }

        private void initExternalDBStruct_v1(DBVersion dbVersion)
        {
            dbVersion.Version = 1;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            #region 初始化数据库结构

            mDatabase.CreateTableAsync<NoteItem>().Wait();

            #endregion

        }

        private void updateExternalDBStruct_v2(DBVersion dbVersion)
        {
            dbVersion.Version = 2;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            // 执行数据库升级脚本
        }

        private void updateExternalDBStruct_v3(DBVersion dbVersion)
        {
            dbVersion.Version = 3;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            // 执行数据库升级脚本
        }
    }
}
