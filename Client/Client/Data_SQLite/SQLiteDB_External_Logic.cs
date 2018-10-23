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

 
        }

        private void initExternalDBStruct_v1(DBVersion dbVersion)
        {
            dbVersion.Version = 1;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            #region 初始化数据库结构

            mDatabase.CreateTableAsync<View.Games.CRW.Game_User>().Wait();
            mDatabase.CreateTableAsync<View.Games.CRW.CRWLog>().Wait();

            #endregion

        }

        //private void updateExternalDBStruct_v2(DBVersion dbVersion)
        //{
        //    dbVersion.Version = 2;
        //    mDatabase.InsertOrReplaceAsync(dbVersion);

        //    // 执行数据库升级脚本
        //}

        //private void updateExternalDBStruct_v3(DBVersion dbVersion)
        //{
        //    dbVersion.Version = 3;
        //    mDatabase.InsertOrReplaceAsync(dbVersion);

        //    // 执行数据库升级脚本
        //}


        #region Game - CRW

        public View.Games.CRW.Game_User CRW_rcUser(View.Games.CRW.Game_User args)
        {
            var taskResult = mDatabase.Table<View.Games.CRW.Game_User>().Where(i => i.Account == args.Account).ToListAsync();
            if (taskResult != null)
            {
                if (taskResult.Result == null || taskResult.Result.Count == 0)
                {
                    var toAdd = new View.Games.CRW.Game_User() { ID = Guid.NewGuid().ToString().ToUpper(), Account = args.Account };
                    mDatabase.InsertAsync(toAdd).Wait();
                    return this.CRW_rcUser(args);
                }
            }

            return taskResult.Result[0];
        }

        public View.Games.CRW.CRWLog CRW_rLog(View.Games.CRW.Game_User args, int argsCRWTypeID)
        {
            var now = WebDateTime.Now;
            var today = now.Date;

            var taskResult = mDatabase.Table<View.Games.CRW.CRWLog>()
                             .Where(i => i.UserID == args.ID && i.CRWTypeID == argsCRWTypeID && i.DateValue == today.Ticks)
                             .OrderByDescending(i => i.UpdateTimeValue)
                             .FirstOrDefaultAsync();

            if (taskResult != null )
            {
                if (taskResult.Result != null)
                {
                    return taskResult.Result;
                }
            }

            var nearLastest = mDatabase.Table<View.Games.CRW.CRWLog>()
                 .Where(i => i.UserID == args.ID && i.CRWTypeID == argsCRWTypeID)
                 .OrderByDescending(i => i.UpdateTimeValue)
                 .FirstOrDefaultAsync();

            View.Games.CRW.CRWLog toAdd = new View.Games.CRW.CRWLog()
            {
                UserID = args.ID,
                CRWTypeID = argsCRWTypeID,
                Level = 1,
                DateValue = today.Ticks,
                DateDisplay = today.ToString("yyyy-MM-dd"),
                UpdateTimeValue = now.Ticks,
                UpdateTimeDisplay = now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Percentage = null,
                NextLevel = null,
                UseTime = 0,
                UseTimeDisplay = "0秒"
            };


            if (nearLastest != null && nearLastest.Result != null)
            {
                var lastestLog = nearLastest.Result;

                toAdd.Level = lastestLog.Level;
                toAdd.NextLevel = lastestLog.NextLevel;
            }

            CRW_cLog(toAdd);

            return CRW_rLog(args, argsCRWTypeID);
        }

        public void CRW_cLog(View.Games.CRW.CRWLog args)
        {
            mDatabase.InsertAsync(args);
        }

        public void CRW_uLog(View.Games.CRW.CRWLog args)
        {
            mDatabase.UpdateAsync(args);
        }

        #endregion

        #region Game - 玩家信息

        public List<View.Games.CRW.ModelA> Game_rUserList()
        {
            List<View.Games.CRW.ModelA> r = new List<View.Games.CRW.ModelA>();

            var taskResult = mDatabase.Table<View.Games.CRW.Game_User>().ToListAsync();

            foreach (View.Games.CRW.Game_User user in taskResult.Result)
            {
                var lastCRWType1 = mDatabase.Table<View.Games.CRW.CRWLog>()
                        .Where(i=>i.CRWTypeID == 1)
                        .Where(i=>i.UserID == user.ID)
                        .OrderByDescending(i=>i.DateValue)
                        .FirstOrDefaultAsync();

                var lastCRWType2 = mDatabase.Table<View.Games.CRW.CRWLog>()
                        .Where(i => i.CRWTypeID == 2)
                        .Where(i => i.UserID == user.ID)
                        .OrderByDescending(i => i.DateValue)
                        .FirstOrDefaultAsync();

                var toAdd = new View.Games.CRW.ModelA()
                {
                    User = user,
                    Type1_CRW_LevelLog = lastCRWType1.Result,
                    Type2_CRW_LevelLog = lastCRWType2.Result
                };

                if (toAdd.Type1_CRW_LevelLog != null)
                {
                    var lastCRWType1_MaxLevel = mDatabase.Table<View.Games.CRW.CRWLog>()
                                     .Where(i => i.CRWTypeID == 1)
                                     .Where(i => i.UserID == user.ID)
                                     .Where(i => i.DateValue == toAdd.Type1_CRW_LevelLog.DateValue)
                                     .OrderByDescending(i => i.Level)
                                     .FirstAsync();

                    toAdd.Type1_CRW_Level = new View.Games.CRW.CRW_Level(lastCRWType1_MaxLevel.Result.Level, 1);
                }

                if (toAdd.Type2_CRW_LevelLog != null)
                {
                    var lastCRWType2_MaxLevel = mDatabase.Table<View.Games.CRW.CRWLog>()
                                     .Where(i => i.CRWTypeID == 2)
                                     .Where(i => i.UserID == user.ID)
                                     .Where(i => i.DateValue == toAdd.Type2_CRW_LevelLog.DateValue)
                                     .OrderByDescending(i => i.Level)
                                     .FirstAsync();

                    toAdd.Type2_CRW_Level = new View.Games.CRW.CRW_Level(lastCRWType2_MaxLevel.Result.Level, 2);
                }

                r.Add(toAdd);
            }

            return r;
        }

        public void Game_rUserDetail(View.Games.CRW.Game_User user)
        {
            var taskResult = mDatabase.Table<View.Games.CRW.CRWLog>()
                .Where(i => i.UserID == user.ID)
                .Where(i => i.CRWTypeID == 1);
                // .GroupBy

        }

        #endregion
    }
}
