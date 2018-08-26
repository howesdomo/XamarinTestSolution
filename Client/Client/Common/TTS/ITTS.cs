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
    }
}
