using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface ITTS
    {
        void InitTextToSpeech();

        bool Check_InitTextToSpeech();

        void Play(string content);

        void SetSpeechRate(float args);

        /// <summary>
        /// 设置语速 静默模式
        /// </summary>
        /// <param name="args"></param>
        void SetSpeechRateSilent(float args);
    }
}
