using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVFoundation;
using Foundation;
using UIKit;

namespace Client.iOS
{
    public class MyAudioPlayer : Util.XamariN.IAudioPlayer
    {
        #region 构造函数 + 单例模式

        private MyAudioPlayer()
        {

        }

        private static MyAudioPlayer s_Instance;

        private static object objLock = new object();

        public static MyAudioPlayer GetInstance()
        {
            lock (objLock)
            {
                if (s_Instance == null)
                {
                    s_Instance = new MyAudioPlayer();
                }

                return s_Instance;
            }
        }

        #endregion

        private static object _LOCK_ = new object();

        #region BGM音量 / 音效音量

        public bool mIsBackgroundMusicOn = true;

        public bool GetIsBackgroundMusicOn()
        {
            return this.mIsBackgroundMusicOn;
        }

        public bool SetIsBackgroundMusicOn(bool a)
        {
            this.mIsBackgroundMusicOn = a;
            return this.mIsBackgroundMusicOn;
        }



        public float mBackgroundMusicVolume = 1.0f;

        public float GetBackgroundMusicVolume()
        {
            return this.mBackgroundMusicVolume;
        }
        public float SetBackgroundMusicVolume(float a)
        {
            this.mBackgroundMusicVolume = a;

            if (mPlayer_BackgroundMusic != null && mPlayer_BackgroundMusic.Playing == true)
            {
                mPlayer_BackgroundMusic.Volume = a;
            }

            return this.mBackgroundMusicVolume;
        }




        public bool mIsEffectsOn = true;

        public bool GetIsEffectsOn()
        {
            return this.mIsEffectsOn;
        }

        public bool SetIsEffectsOn(bool a)
        {
            this.mIsEffectsOn = a;
            return this.mIsEffectsOn;
        }




        public float mEffectsVolume = 1.0f;

        public float GetEffectsVolume()
        {
            return this.mEffectsVolume;
        }

        public float SetEffectsVolume(float a)
        {
            this.mEffectsVolume = a;

            if (mPlayer_SoundEffect != null && mPlayer_SoundEffect.Playing == true)
            {
                mPlayer_SoundEffect.Volume = a;
            }

            return this.mEffectsVolume;
        }

        #endregion BGM音量 / 音效音量

        private AVAudioPlayer mPlayer_SoundEffect;


        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="fileName"></param>
        public void PlaySoundEffect(string fileName)
        {
            if (mIsEffectsOn == false)
            {
                return;
            }

            if (mPlayer_SoundEffect != null) // Stop and dispose of any background music
            {
                mPlayer_SoundEffect.Stop();
                mPlayer_SoundEffect.Dispose();
            }

            // Initialize background music
            NSUrl songURL = new NSUrl("raw/{0}".FormatWith(fileName));
            NSError err = null;

            mPlayer_SoundEffect = new AVAudioPlayer(songURL, "mp3", out err);
            mPlayer_SoundEffect.Volume = mBackgroundMusicVolume;
            mPlayer_SoundEffect.FinishedPlaying += delegate
            {
                mPlayer_SoundEffect = null;
            };

            mPlayer_SoundEffect.NumberOfLoops = 0;
            mPlayer_SoundEffect.Play();
        }


        private AVAudioPlayer mPlayer_BackgroundMusic;

        /// <summary>
        /// 播放背景音
        /// </summary>
        /// <param name="fileName"></param>
        public void PlayBackgroundMusic(string fileName)
        {
            if (mIsBackgroundMusicOn == false)
            {
                return;
            }

            if (mPlayer_BackgroundMusic != null) // Stop and dispose of any background music
            {
                mPlayer_BackgroundMusic.Stop();
                mPlayer_BackgroundMusic.Dispose();
            }

            // Initialize background music
            NSUrl songURL = new NSUrl("raw/{0}".FormatWith(fileName));
            NSError err = null;

            mPlayer_BackgroundMusic = new AVAudioPlayer(songURL, "mp3", out err);
            mPlayer_BackgroundMusic.Volume = mBackgroundMusicVolume;
            mPlayer_BackgroundMusic.FinishedPlaying += delegate
            {
                mPlayer_BackgroundMusic = null;
            };

            mPlayer_BackgroundMusic.NumberOfLoops = -1; // 循环播放背景音
            mPlayer_BackgroundMusic.Play();
        }

        /// <summary>
        /// 停止播放背景音
        /// </summary>
        public void StopBackgroundMusic()
        {
            if (mPlayer_BackgroundMusic != null && mPlayer_BackgroundMusic.Playing == true)
            {
                mPlayer_BackgroundMusic.Stop();
            }
        }


        /// <summary>
        /// 扫描成功
        /// </summary>
        private AVAudioPlayer mPlayer_SoundEffect_Beep;

        public void PlayBeep()
        {
            if (mIsEffectsOn == false)
            {
                return;
            }

            if (mPlayer_SoundEffect_Beep == null)
            {
                NSUrl songURL = new NSUrl("raw/beep.mp3");
                NSError err = null;
                mPlayer_SoundEffect_Beep = new AVAudioPlayer(songURL, "mp3", out err);
                // mPlayer_SoundEffect_Beep.Volume = 1f;

                // mPlayer_SoundEffect_Beep.FinishedPlaying += delegate { mPlayer_SoundEffect_Beep = null; };
                mPlayer_SoundEffect_Beep.NumberOfLoops = 0;
            }

            if (mPlayer_SoundEffect_Beep.Playing == true)
            {
                mPlayer_SoundEffect_Beep.Stop();
            }

            if (mPlayer_SoundEffect_Beep.CurrentTime > 0d)
            {
                mPlayer_SoundEffect_Beep.CurrentTime = 0d;
            }

            mPlayer_SoundEffect_Beep.Volume = mEffectsVolume;
            mPlayer_SoundEffect_Beep.Play();
        }


        /// <summary>
        /// 异常错误
        /// </summary>
        private AVAudioPlayer mPlayer_SoundEffect_Error;

        public void PlayError()
        {
            if (mIsEffectsOn == false)
            {
                return;
            }

            if (mPlayer_SoundEffect_Error == null)
            {
                NSUrl songURL = new NSUrl("raw/error.mp3");
                NSError err = null;
                mPlayer_SoundEffect_Error = new AVAudioPlayer(songURL, "mp3", out err);
                // mPlayer_SoundEffect_Error.Volume = 1f;
                // mPlayer_SoundEffect_Error.FinishedPlaying += delegate { mPlayer_SoundEffect_Error = null; };
                mPlayer_SoundEffect_Error.NumberOfLoops = 0;
            }

            if (mPlayer_SoundEffect_Error.Playing == true)
            {
                mPlayer_SoundEffect_Error.Stop();
            }

            if (mPlayer_SoundEffect_Error.CurrentTime > 0)
            {
                mPlayer_SoundEffect_Error.CurrentTime = 0d;
            }

            mPlayer_SoundEffect_Error.Volume = mEffectsVolume;
            mPlayer_SoundEffect_Error.Play();
        }


        /// <summary>
        /// 安卓的接口, 不实现此方法
        /// </summary>
        /// <param name="filePath"></param>
        public void PlayAssetsFile(string filePath)
        {

        }

    }
}