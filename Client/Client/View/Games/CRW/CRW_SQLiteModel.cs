using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.View.Games.CRW
{
    public class Game_User
    {
        [PrimaryKey]
        public string ID { get; set; }

        public string Account { get; set; }
    }

    public class CRWLog
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string UserID { get; set; }

        public int CRWTypeID { get; set; }

        public int Level { get; set; }

        public int DateValue { get; set; }

        public string DateDisplay { get; set; }

        /// <summary>
        /// 成功率
        /// </summary>
        public int? Percentage { get; set; }

        public int? NextLevel { get; set; }

        /// <summary>
        /// 锻炼时间
        /// </summary>
        public int? UseTime { get; set; }

        /// <summary>
        /// 锻炼时间
        /// </summary>
        public string UseTimeDisplay { get; set; }


        public int UpdateTimeValue { get; set; }

        public string UpdateTimeDisplay { get; set; }
    }
}
