using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;

namespace Client.Droid
{
    public class MyTTS : Activity, Client.Common.ITTS, TextToSpeech.IOnInitListener
    {
        public static int TTS_RequestCode = 775;

        // 成功启动 TTS
        private bool mTTS_Init_Success = false;

        private TextToSpeech mTextToSpeech;

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
            if (mTTS_Init_Success == false || mTextToSpeech == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 实现 ITTS 接口方法
        /// 选择TTS语音合成引擎
        /// </summary>
        /// <param name="args"></param>
        public void InitTextToSpeech()
        {
            mTTS_Init_Success = false;

            Intent intent = new Intent(Android.Speech.Tts.TextToSpeech.Engine.ActionCheckTtsData);
            ((Activity)Xamarin.Forms.Forms.Context).StartActivityForResult(intent, TTS_RequestCode);
        }

        public void Handle_OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == TTS_RequestCode)
            {
                // if (resultCode == Engine.CHECK_VOICE_DATA_PASS) --> 安卓源码
                if (resultCode.ToString() == "FirstUser")
                {
                    mTextToSpeech = new TextToSpeech((Activity)Xamarin.Forms.Forms.Context, this);
                }
            }
        }

        /// <summary>
        /// 实现 TextToSpeech.IOnInitListener 接口方法
        /// 初始化TTS
        /// </summary>
        /// <param name="status"></param>
        public void OnInit([GeneratedEnum] OperationResult status)
        {
            // if (status != TextToSpeech.SUCCESS) --> 安卓源码
            if (status.ToString() != "Success")
            {
                // 返回并非成功结果, 继续弹出TTS语音选择界面
                InitTextToSpeech();
                return;
            }

            if (mTextToSpeech.IsLanguageAvailable(Java.Util.Locale.Uk) >= 0)
            {
                mTextToSpeech.SetLanguage(Java.Util.Locale.Uk);
            }

            mTextToSpeech.SetPitch(1f);
            mTextToSpeech.SetSpeechRate(1f);

            mTTS_Init_Success = true;

            System.Threading.Tasks.Task.Run(() =>
            {
                Looper.Prepare();
                Toast.MakeText(((Activity)Xamarin.Forms.Forms.Context), "TTS启动成功", ToastLength.Long).Show();
                Looper.Loop();
            });
        }

        /// <summary>
        /// 实现 ITTS 接口方法
        /// 设置语速
        /// </summary>
        /// <param name="args"></param>
        public void SetSpeechRate(float args)
        {
            mTextToSpeech.SetSpeechRate(args);

            string msg = "语速设置为 {0}".FormatWith(args);
            System.Threading.Tasks.Task.Run(() =>
            {
                Looper.Prepare();
                Toast.MakeText(((Activity)Xamarin.Forms.Forms.Context), msg, ToastLength.Long).Show();
                Looper.Loop();
            });
        }

        /// <summary>
        /// 实现 ITTS 接口方法
        /// 设置语速
        /// </summary>
        /// <param name="args"></param>
        public void SetSpeechRateSilent(float args)
        {
            mTextToSpeech.SetSpeechRate(args);
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
                mTextToSpeech.Speak(content, QueueMode.Flush, null);
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
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        Looper.Prepare();
                        Toast.MakeText(((Activity)Xamarin.Forms.Forms.Context), msg, ToastLength.Long).Show();
                        Looper.Loop();
                    });
                }
            });

            myTask.Start();
        }
    }

}