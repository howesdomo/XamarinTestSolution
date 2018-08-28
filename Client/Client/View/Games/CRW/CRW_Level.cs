using System;
using System.Collections.Generic;
using System.Text;

namespace Client.View.Games.CRW
{
    /// <summary>
    /// 溯算等级
    /// </summary>
    public class CRW_Level
    {
        public int LevelNo { get; set; }

        /// <summary>
        /// 登记名称
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 等级语速
        /// </summary>
        public int YuSu { get; set; }

        /// <summary>
        /// 规定时间内完成答题
        /// </summary>
        public int AnswerTime { get; set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public int QuestionCount { get; set; }

    }
}
