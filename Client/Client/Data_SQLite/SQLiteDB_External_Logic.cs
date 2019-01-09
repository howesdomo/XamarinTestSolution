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

            if (dbVersion.Version < 2)
            {
                updateExternalDBStruct_v2(dbVersion);
            }

        }

        private void initExternalDBStruct_v1(DBVersion dbVersion)
        {
            dbVersion.Loaction = LocationEnum.External;
            dbVersion.Version = 1;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            #region 初始化数据库结构

            mDatabase.CreateTableAsync<View.Games.CRW.Game_User>().Wait();
            mDatabase.CreateTableAsync<View.Games.CRW.CRWLog>().Wait();

            #endregion

        }

        private void updateExternalDBStruct_v2(DBVersion dbVersion)
        {
            dbVersion.Loaction = LocationEnum.External;
            dbVersion.Version = 2;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            // 执行数据库升级脚本
            mDatabase.CreateTableAsync<View.BuBuGao.Word>().Wait();
            mDatabase.CreateTableAsync<View.BuBuGao.Question>().Wait();

            #region 基本数据
            View.BuBuGao.Question a1 = new View.BuBuGao.Question();
            a1.Name = "天空";
            a1.Words = new List<View.BuBuGao.Word>();
            a1.Words.Add(new View.BuBuGao.Word() { Content = "天空" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "空气" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "气体" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "体力" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "力度" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "度过" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "过去" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "去年" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "年轻" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "轻松" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "松树" });
            a1.Words.Add(new View.BuBuGao.Word() { Content = "树木" });

            View.BuBuGao.Question a2 = new View.BuBuGao.Question();
            a2.Name = "大人";
            a2.Words = new List<View.BuBuGao.Word>();
            a2.Words.Add(new View.BuBuGao.Word() { Content = "大人" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "人生" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "生命" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "命运" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "运货" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "货物" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "物品" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "品尝" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "尝试" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "试验" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "验证" });
            a2.Words.Add(new View.BuBuGao.Word() { Content = "证明" });

            View.BuBuGao.Question a3 = new View.BuBuGao.Question();
            a3.Name = "红豆";
            a3.Words = new List<View.BuBuGao.Word>();
            a3.Words.Add(new View.BuBuGao.Word() { Content = "红豆" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "豆沙" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "沙子" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "子女" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "女巫" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "巫师" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "师父" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "父亲节" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "节约" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "约见" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "见面" });
            a3.Words.Add(new View.BuBuGao.Word() { Content = "面粉" });

            View.BuBuGao.Question a4 = new View.BuBuGao.Question();
            a4.Name = "太黑";
            a4.Words = new List<View.BuBuGao.Word>();
            a4.Words.Add(new View.BuBuGao.Word() { Content = "太黑" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "黑白" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "白饭" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "饭菜" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "菜园" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "园丁" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "丁香花" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "花生" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "生气" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "气球" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "球体" });
            a4.Words.Add(new View.BuBuGao.Word() { Content = "体检" });

            View.BuBuGao.Question a5 = new View.BuBuGao.Question();
            a5.Name = "上面";
            a5.Words = new List<View.BuBuGao.Word>();

            a5.Words.Add(new View.BuBuGao.Word() { Content = "上面" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "面条" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "条件" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "件数" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "数学" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "学习" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "习惯" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "惯性" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "性格" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "格子" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "子孙" });
            a5.Words.Add(new View.BuBuGao.Word() { Content = "孙悟空" });

            #endregion 基本数据

            mDatabase.InsertAsync(a1);
            mDatabase.InsertAsync(a2);
            mDatabase.InsertAsync(a3);
            mDatabase.InsertAsync(a4);
            mDatabase.InsertAsync(a5).Wait();


            BuBuGao_cWordList(a1);
            BuBuGao_cWordList(a2);
            BuBuGao_cWordList(a3);
            BuBuGao_cWordList(a4);
            BuBuGao_cWordList(a5);
        }

        //private void updateExternalDBStruct_vX(DBVersion dbVersion)
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

            if (taskResult != null)
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
                        .Where(i => i.CRWTypeID == 1)
                        .Where(i => i.UserID == user.ID)
                        .OrderByDescending(i => i.DateValue)
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

        public List<View.Games.CRW.DailyUserRecord> Game_rUserDetail(View.Games.CRW.Game_User user, int _CRWTypeID)
        {
            string sql = "select UserID, CRWTypeID, DateValue, DateDisplay, Max(Level) as Level, Max(UseTime) as UseTime " +
                         " from CRWLog " +
                         " where UserID = ? and " +
                         " CRWTypeID = ? and " +
                         " NextLevel is not null " +
                         " group by UserID, CRWTypeID, DateValue, DateDisplay " +
                         " order by DateValue desc ";

            var taskResult = mDatabase.QueryAsync<View.Games.CRW.CRWLog>(sql, new object[2] { user.ID, _CRWTypeID });

            List<View.Games.CRW.DailyUserRecord> r = new List<View.Games.CRW.DailyUserRecord>();

            foreach (View.Games.CRW.CRWLog item in taskResult.Result)
            {

                r.Add(new View.Games.CRW.DailyUserRecord()
                {
                    DateDisplay = item.DateDisplay,
                    Level = item.Level,
                    MaxLevelName = new View.Games.CRW.CRW_Level(item.Level, _CRWTypeID).LevelName,
                    MaxUseTime = item.UseTime.Value,
                    MaxUseTimeDisplay = TimeSpan.FromTicks(item.UseTime.Value).ToStringAdv()
                });
            }

            return r;
        }

        #endregion

        #region 补补高

        public List<View.BuBuGao.Question> BuBuGao_rQuestionList()
        {
            var task = mDatabase.Table<View.BuBuGao.Question>().ToListAsync();
            if (task.Exception != null)
            {
                throw task.Exception;
            }
            else
            {
                foreach (var item in task.Result)
                {
                    item.Words = mDatabase.Table<View.BuBuGao.Word>().Where(i => i.QuestionID == item.ID).ToListAsync().Result;
                }
            }

            return task.Result;
        }


        public void BuBuGao_cWordList(View.BuBuGao.Question question)
        {
            foreach (View.BuBuGao.Word item in question.Words)
            {
                item.QuestionID = question.ID;
                mDatabase.InsertAsync(item).Wait(); // 重要 // 批量插入时不加上 Wait() 会取不到 Word id, 从而被后面插入的数据覆盖
            }
        }

        public void BuBuGao_cQuestion(View.BuBuGao.Question question)
        {
            mDatabase.InsertAsync(question);
        }

        #endregion
    }
}
