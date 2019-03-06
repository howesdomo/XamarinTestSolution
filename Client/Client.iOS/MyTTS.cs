using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using AVFoundation;
using System.Threading.Tasks;

namespace Client.iOS
{
    public class MyTTS : Client.Common.ITTS
    {
        private float mVolume = 0.5f;
        private float mPitch = 1.0f; // 音高
        private float mRate = 0.5f; // 语速

        private AVSpeechSynthesizer mSpeechSynthesizer = new AVSpeechSynthesizer();

        #region 构造函数 + 单例模式

        private MyTTS()
        {

        }

        private static MyTTS s_Instance;

        private static object objLock = new object();

        public static MyTTS GetInstance()
        {
            lock (objLock)
            {
                if (s_Instance == null)
                {
                    s_Instance = new MyTTS();
                }

                return s_Instance;
            }
        }

        #endregion

        /// <summary>
        /// 检测 TTS 是否已经成功开启
        /// </summary>
        /// <returns></returns>
        public bool Check_InitTextToSpeech()
        {
            // 苹果无需检测是否已打开TTS
            return true;
        }

        /// <summary>
        /// 实现 ITTS 接口方法
        /// 选择TTS语音合成引擎
        /// </summary>
        /// <param name="args"></param>
        public void InitTextToSpeech()
        {
            // 苹果无需选择TTS语音合成引擎
            // DoNothing
        }

        /// <summary>
        /// 实现 ITTS 接口方法
        /// 设置语速
        /// </summary>
        /// <param name="args"></param>
        public void SetSpeechRate(float args)
        {
            SetSpeechRateSilent(args);
            // TODO iOS MyTTS 弹窗提示
        }

        /// <summary>
        /// 实现 ITTS 接口方法
        /// 设置语速(静默模式)
        /// </summary>
        /// <param name="args"></param>
        public void SetSpeechRateSilent(float args)
        {
            // this.mPitch = args;
            this.mRate = args;
        }

        /// <summary>
        /// 实现 ITTS 接口方法
        /// 播放语音
        /// </summary>
        /// <param name="content"></param>
        public void Play(string content)
        {
            if (Check_InitTextToSpeech() == false)
            {
                return;
            }

            Task myTask = new Task(() =>
            {
                AVSpeechUtterance speechUtterance = new AVSpeechUtterance(content)
                {
                    Rate = AVSpeechUtterance.MaximumSpeechRate / 3,
                    Voice = AVSpeechSynthesisVoice.FromLanguage("zh-CN"),
                    Volume = mVolume,
                    PitchMultiplier = mPitch,
                };

                speechUtterance.Rate = mRate;

                mSpeechSynthesizer.SpeakUtterance(speechUtterance);

            });

            myTask.ContinueWith((task) =>
            {
                if (task.IsFaulted == true)
                {
                    string msg = string.Empty;
                    if (task.Exception != null)
                    {
                        msg = "TTS Play 异常 : \r\n{0}".FormatWith(task.Exception.GetFullInfo());
                    }
                    else
                    {
                        msg = "TTS Play 异常 : ".FormatWith();
                    }

                    System.Diagnostics.Debug.WriteLine(msg);

                    // TODO iOS MyTTS 弹窗提示
                    //System.Threading.Tasks.Task.Run(() =>
                    //{
                    //    Looper.Prepare();
                    //    Toast.MakeText(((Activity)Xamarin.Forms.Forms.Context), msg, ToastLength.Long).Show();
                    //    Looper.Loop();
                    //});
                }
            });

            myTask.Start();
        }

        // avspeech支持的语言种类包括：
        //"[AVSpeechSynthesisVoice 0x978a0b0]Language: th-TH", 
        //"[AVSpeechSynthesisVoice 0x977a450]Language: pt-BR", 
        //"[AVSpeechSynthesisVoice 0x977a480]Language: sk-SK",
        //"[AVSpeechSynthesisVoice 0x978ad50]Language: fr-CA", 
        //"[AVSpeechSynthesisVoice 0x978ada0]Language: ro-RO", 
        //"[AVSpeechSynthesisVoice 0x97823f0]Language: no-NO",
        //"[AVSpeechSynthesisVoice 0x978e7b0]Language: fi-FI", 
        //"[AVSpeechSynthesisVoice 0x978af50]Language: pl-PL", 
        //"[AVSpeechSynthesisVoice 0x978afa0]Language: de-DE", 
        //"[AVSpeechSynthesisVoice 0x978e390] Language:nl-NL", 
        //"[AVSpeechSynthesisVoice 0x978b030]Language: id-ID", 
        //"[AVSpeechSynthesisVoice 0x978b080]Language: tr-TR", 
        //"[AVSpeechSynthesisVoice 0x978b0d0]Language: it-IT", 
        //"[AVSpeechSynthesisVoice 0x978b120]Language: pt-PT",
        //"[AVSpeechSynthesisVoice 0x978b170]Language: fr-FR",
        //"[AVSpeechSynthesisVoice 0x978b1c0]Language: ru-RU", 
        //"[AVSpeechSynthesisVoice0x978b210]Language: es-MX", 
        //"[AVSpeechSynthesisVoice 0x978b2d0]Language: zh-HK",
        //"[AVSpeechSynthesisVoice 0x978b320]Language: sv-SE", 
        //"[AVSpeechSynthesisVoice 0x978b010]Language: hu-HU",
        //"[AVSpeechSynthesisVoice 0x978b440]Language: zh-TW",
        //"[AVSpeechSynthesisVoice 0x978b490]Language: es-ES",
        //"[AVSpeechSynthesisVoice 0x978b4e0]Language: zh-CN", 
        //"[AVSpeechSynthesisVoice 0x978b530]Language: nl-BE", 
        //"[AVSpeechSynthesisVoice 0x978b580]Language: en-GB",
        //"[AVSpeechSynthesisVoice 0x978b5d0]Language: ar-SA", 
        //"[AVSpeechSynthesisVoice 0x978b620]Language: ko-KR",
        //"[AVSpeechSynthesisVoice 0x978b670]Language: cs-CZ",
        //"[AVSpeechSynthesisVoice 0x978b6c0]Language: en-ZA", 
        //"[AVSpeechSynthesisVoice 0x978aed0]Language: en-AU",
        //"[AVSpeechSynthesisVoice 0x978af20]Language: da-DK",
        //"[AVSpeechSynthesisVoice 0x978b810]Language: en-US",
        //"[AVSpeechSynthesisVoice 0x978b860]Language: en-IE",
        //"[AVSpeechSynthesisVoice 0x978b8b0]Language: hi-IN", 
        //"[AVSpeechSynthesisVoice 0x978b900]Language: el-GR",
        //"[AVSpeechSynthesisVoice 0x978b950]Language: ja-JP" 
    }
}