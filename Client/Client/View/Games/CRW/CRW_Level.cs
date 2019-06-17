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

            this.SuDa = this.LevelNo / 2 + this.LevelNo % 2;

            if (this.LevelNo % 2 == 0)
            {
                this.LevelName = "快速{0}溯答".FormatWith(this.SuDa);
                this.LevelTTSName = "快速{0}溯答".FormatWith(CRWBll.ToChineseNumber(this.SuDa));
                this.LevelTTS_SleepTime = 3500;
            }
            else
            {
                this.LevelName = "{0}溯答".FormatWith(this.SuDa);
                this.LevelTTSName = "{0}溯答".FormatWith(CRWBll.ToChineseNumber(this.SuDa));
                this.LevelTTS_SleepTime = 2000;
            }

            this.QuestionCount = 20 + this.SuDa * 2;
            // ****** 测试模式减少题目数量 ******
            // this.QuestionCount = 2 + this.SuDa * 2;

            this.MaxIndex = this.QuestionCount + this.SuDa - 1;

            if (this.LevelNo % 2 == 0)
            {
                switch (Common.StaticInfo.DeviceInfo_Platform)
                {
                    case "ANDROID": this.SpeechRate = 0.5f; break;
                    case "IOS": this.SpeechRate = 0.5f; break;
                }
            }
            else
            {
                switch (Common.StaticInfo.DeviceInfo_Platform)
                {
                    case "ANDROID": this.SpeechRate = 1f; break;
                    case "IOS": this.SpeechRate = 0.55f; break;
                }
            }


            if (this.LevelNo % 2 == 0) // 快速
            {
                this.AnswerTime = 3 * 1000;
            }
            else // 常速
            {
                this.AnswerTime = 4 * 1000;
            }

            this.AnswerStartIndex = this.SuDa;
            this.RememberEndIndex = this.QuestionCount - 1;

        }

        /// <summary>
        /// 1 溯答
        /// 2 听力溯答
        /// </summary>
        public int CRWTypeID { get; set; }

        public int LevelNo { get; set; }

        /// <summary>
        /// 溯答
        /// (快速) x 溯答
        /// </summary>
        public int SuDa { get; set; }

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
