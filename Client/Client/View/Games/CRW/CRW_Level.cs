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
        public CRW_Level(int levelNo)
        {

            this.LevelNo = levelNo;

            this.SuSuan = this.LevelNo / 2 + this.LevelNo % 2;

            if (this.LevelNo % 2 == 0)
            {
                this.LevelName = "快速{0}溯答".FormatWith(this.SuSuan);
            }
            else
            {
                this.LevelName = "{0}溯答".FormatWith(this.SuSuan);
            }

            this.QuestionCount = 20 + this.SuSuan * 2;
            this.MaxIndex = this.QuestionCount + this.SuSuan - 1;

            this.YuSu = 1;
            this.AnswerTime = TimeSpan.FromSeconds(5).Milliseconds;

            this.AnswerStartIndex = this.SuSuan;
            this.RememberEndIndex = this.QuestionCount - 1;

        }

        public int LevelNo { get; set; }

        public int SuSuan { get; set; }

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

        /// <summary>
        /// 开始作答
        /// </summary>
        public int AnswerStartIndex { get; set; }

        /// <summary>
        /// 停止出题
        /// </summary>
        public int RememberEndIndex { get; set; }

        /// <summary>
        /// CurrentIndex 最大索引
        /// </summary>
        public int MaxIndex { get; set; }
    }
}
