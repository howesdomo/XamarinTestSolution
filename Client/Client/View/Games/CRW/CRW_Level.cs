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
        public CRW_Level(int levelNo, int argsCRWTypeID)
        {
            this.CRWTypeID = argsCRWTypeID;
            this.LevelNo = levelNo;

            this.SuSuan = this.LevelNo / 2 + this.LevelNo % 2;

            if (this.LevelNo % 2 == 0)
            {
                this.LevelName = "快速{0}溯答".FormatWith(this.SuSuan);
                this.LevelTTSName = "快速{0}溯答".FormatWith(CRWBll.ToChineseNumber(this.SuSuan));
                this.LevelTTS_SleepTime = 3500;
            }
            else
            {
                this.LevelName = "{0}溯答".FormatWith(this.SuSuan);
                this.LevelTTSName = "{0}溯答".FormatWith(CRWBll.ToChineseNumber(this.SuSuan));
                this.LevelTTS_SleepTime = 2000;
            }

            this.QuestionCount = 20 + this.SuSuan * 2;
            // TODO 测试模式减少题目数量
            this.QuestionCount = 2 + this.SuSuan * 2;

            this.MaxIndex = this.QuestionCount + this.SuSuan - 1;

            if (this.LevelNo % 2 == 0)
            {
                this.SpeechRate = 0.5f;
            }
            else
            {
                this.SpeechRate = 1f;
            }


            if (this.LevelNo % 2 == 0) // 快速
            {
                this.AnswerTime = 3 * 1000;
            }
            else // 常速
            {
                this.AnswerTime = 5 * 1000;
            }

            this.AnswerStartIndex = this.SuSuan;
            this.RememberEndIndex = this.QuestionCount - 1;

        }

        /// <summary>
        /// 1 溯答
        /// 2 听力溯答
        /// </summary>
        public int CRWTypeID { get; set; }

        public int LevelNo { get; set; }

        public int SuSuan { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 等级名称 TTS 播放使用
        /// </summary>
        public string LevelTTSName { get; set; }

        /// <summary>
        /// 等级名称 TTS 播放等待时间
        /// </summary>
        public int LevelTTS_SleepTime { get; set; }

        /// <summary>
        /// 等级语速
        /// </summary>
        public float SpeechRate { get; set; }

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
