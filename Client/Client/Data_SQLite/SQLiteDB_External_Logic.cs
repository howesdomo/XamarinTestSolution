using Util.Data_SQLite;

namespace Client.Data
{
    public partial class SQLiteDB // SQLiteDB_External_Logic.cs -- 安卓外部存储
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
        }

        private void updateExternalDBStruct_v2(DBVersion dbVersion)
        {
            dbVersion.Loaction = LocationEnum.External;
            dbVersion.Version = 2;
            mDatabase.InsertOrReplaceAsync(dbVersion);

            //// 执行数据库升级脚本
            //mDatabase.CreateTableAsync<View.BuBuGao.Word>().Wait();
            //mDatabase.CreateTableAsync<View.BuBuGao.Question>().Wait();

            //#region 基本数据
            //DateTime now = DateTime.Now;

            //View.BuBuGao.Question a1 = new View.BuBuGao.Question();
            //a1.Name = "天空";
            //a1.Words = new List<View.BuBuGao.Word>()
            //{
            //   new View.BuBuGao.Word() { Content = "天空" },
            //   new View.BuBuGao.Word() { Content = "空气" },
            //   new View.BuBuGao.Word() { Content = "气体" },
            //   new View.BuBuGao.Word() { Content = "体力" },
            //   new View.BuBuGao.Word() { Content = "力度" },
            //   new View.BuBuGao.Word() { Content = "度过" },
            //   new View.BuBuGao.Word() { Content = "过去" },
            //   new View.BuBuGao.Word() { Content = "去年" },
            //   new View.BuBuGao.Word() { Content = "年轻" },
            //   new View.BuBuGao.Word() { Content = "轻松" },
            //   new View.BuBuGao.Word() { Content = "松树" },
            //   new View.BuBuGao.Word() { Content = "树木" }
            //};

            //a1.CreateDateTimeValue = now.Ticks;

            //View.BuBuGao.Question a2 = new View.BuBuGao.Question();
            //a2.Name = "大人";
            //a2.Words = new List<View.BuBuGao.Word>()
            //{
            //    new View.BuBuGao.Word() { Content = "大人" },
            //    new View.BuBuGao.Word() { Content = "人生" },
            //    new View.BuBuGao.Word() { Content = "生命" },
            //    new View.BuBuGao.Word() { Content = "命运" },
            //    new View.BuBuGao.Word() { Content = "运货" },
            //    new View.BuBuGao.Word() { Content = "货物" },
            //    new View.BuBuGao.Word() { Content = "物品" },
            //    new View.BuBuGao.Word() { Content = "品尝" },
            //    new View.BuBuGao.Word() { Content = "尝试" },
            //    new View.BuBuGao.Word() { Content = "试验" },
            //    new View.BuBuGao.Word() { Content = "验证" },
            //    new View.BuBuGao.Word() { Content = "证明" }
            //};
            //a2.CreateDateTimeValue = now.Ticks;

            //View.BuBuGao.Question a3 = new View.BuBuGao.Question();
            //a3.Name = "红豆";
            //a3.Words = new List<View.BuBuGao.Word>()
            //{
            //    new View.BuBuGao.Word() { Content = "红豆" },
            //    new View.BuBuGao.Word() { Content = "豆沙" },
            //    new View.BuBuGao.Word() { Content = "沙子" },
            //    new View.BuBuGao.Word() { Content = "子女" },
            //    new View.BuBuGao.Word() { Content = "女巫" },
            //    new View.BuBuGao.Word() { Content = "巫师" },
            //    new View.BuBuGao.Word() { Content = "师父" },
            //    new View.BuBuGao.Word() { Content = "父亲节" },
            //    new View.BuBuGao.Word() { Content = "节约" },
            //    new View.BuBuGao.Word() { Content = "约见" },
            //    new View.BuBuGao.Word() { Content = "见面" },
            //    new View.BuBuGao.Word() { Content = "面粉" }
            //};

            //a3.CreateDateTimeValue = now.Ticks;

            //View.BuBuGao.Question a4 = new View.BuBuGao.Question();
            //a4.Name = "太黑";
            //a4.Words = new List<View.BuBuGao.Word>()
            //{
            //    new View.BuBuGao.Word() { Content = "太黑" },
            //    new View.BuBuGao.Word() { Content = "黑白" },
            //    new View.BuBuGao.Word() { Content = "白饭" },
            //    new View.BuBuGao.Word() { Content = "饭菜" },
            //    new View.BuBuGao.Word() { Content = "菜园" },
            //    new View.BuBuGao.Word() { Content = "园丁" },
            //    new View.BuBuGao.Word() { Content = "丁香花" },
            //    new View.BuBuGao.Word() { Content = "花生" },
            //    new View.BuBuGao.Word() { Content = "生气" },
            //    new View.BuBuGao.Word() { Content = "气球" },
            //    new View.BuBuGao.Word() { Content = "球体" },
            //    new View.BuBuGao.Word() { Content = "体检" }
            //};

            //a4.CreateDateTimeValue = now.Ticks;

            //View.BuBuGao.Question a5 = new View.BuBuGao.Question();
            //a5.Name = "上面";
            //a5.Words = new List<View.BuBuGao.Word>()
            //{
            //    new View.BuBuGao.Word() { Content = "上面" },
            //    new View.BuBuGao.Word() { Content = "面条" },
            //    new View.BuBuGao.Word() { Content = "条件" },
            //    new View.BuBuGao.Word() { Content = "件数" },
            //    new View.BuBuGao.Word() { Content = "数学" },
            //    new View.BuBuGao.Word() { Content = "学习" },
            //    new View.BuBuGao.Word() { Content = "习惯" },
            //    new View.BuBuGao.Word() { Content = "惯性" },
            //    new View.BuBuGao.Word() { Content = "性格" },
            //    new View.BuBuGao.Word() { Content = "格子" },
            //    new View.BuBuGao.Word() { Content = "子孙" },
            //    new View.BuBuGao.Word() { Content = "孙悟空" }
            //};
            //a5.CreateDateTimeValue = now.Ticks;


            //#endregion 基本数据

            //mDatabase.InsertAsync(a1);
            //mDatabase.InsertAsync(a2);
            //mDatabase.InsertAsync(a3);
            //mDatabase.InsertAsync(a4);
            //mDatabase.InsertAsync(a5).Wait(); // 等待的原因 - 等待 a1 ~ a5 插入完毕, 并且取回插入到 Sqlite后 由系统自动生成的ID


            //BuBuGao_cWordList(a1);
            //BuBuGao_cWordList(a2);
            //BuBuGao_cWordList(a3);
            //BuBuGao_cWordList(a4);
            //BuBuGao_cWordList(a5);
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


    }
}
