using Client.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.Data
{
    public partial class SQLiteDB
    {
        /// <summary>
        /// 1 初始化数据库结构 和 数据
        /// 2 根据数据库版本号升级数据库结构
        /// </summary>
        public void initInnerDB()
        {
            DBVersion dbVersion = null;
            mDatabase.CreateTableAsync<DBVersion>().Wait();

            var taskR = mDatabase.Table<DBVersion>().FirstOrDefaultAsync();
            dbVersion = taskR.Result;

            if (dbVersion == null || dbVersion.Version < 1)
            {
                dbVersion = new DBVersion();
                initInnerDBStruct_v1(dbVersion);
            }

            // 2 根据数据库版本号升级数据库结构
            if (dbVersion.Version < 2)
            {
                updateInnerDBStruct_v2(dbVersion); // update database struct script
            }

            if (dbVersion.Version < 3)
            {
                updateInnerDBStruct_v3(dbVersion);// update database struct script
            }
        }

        private void initInnerDBStruct_v1(DBVersion dbVersion)
        {
            dbVersion.Version = 1;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            #region 初始化数据库结构

            mDatabase.CreateTableAsync<NoteItem>().Wait();

            #endregion

        }

        private void updateInnerDBStruct_v2(DBVersion dbVersion)
        {
            dbVersion.Version = 2;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            // 执行数据库升级脚本
        }

        private void updateInnerDBStruct_v3(DBVersion dbVersion)
        {
            dbVersion.Version = 3;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            // 执行数据库升级脚本
        }

        #region NoteItem - SQLiteDemo演示所用

        public Task<List<NoteItem>> GetItemsAsync()
        {
            return mDatabase.Table<NoteItem>().ToListAsync();
        }

        public Task<List<NoteItem>> GetItemsNotDoneAsync()
        {
            return mDatabase.QueryAsync<NoteItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<NoteItem> GetItemAsync(int id)
        {
            return mDatabase.Table<NoteItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(NoteItem item)
        {
            if (item.ID != 0)
            {
                return mDatabase.UpdateAsync(item);
            }
            else
            {
                return mDatabase.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(NoteItem item)
        {
            return mDatabase.DeleteAsync(item);
        }

        #endregion
    }
}
