using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Client.Droid
{
    public class MyAudioPlayer : Client.Common.IAudioPlayer
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

            if (mPlayer_BackgroundMusic != null && mPlayer_BackgroundMusic.IsPlaying == true)
            {
                mPlayer_BackgroundMusic.SetVolume(a, a);
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

            if (mPlayer_SoundEffect != null && mPlayer_SoundEffect.IsPlaying == true)
            {
                mPlayer_SoundEffect.SetVolume(a, a);
            }

            return this.mEffectsVolume;
        }

        #endregion BGM音量 / 音效音量

        Android.Media.MediaPlayer mPlayer_SoundEffect;

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

            mPlayer_SoundEffect = new Android.Media.MediaPlayer();
            mPlayer_SoundEffect.Prepared += (s, e) =>
            {
                mPlayer_SoundEffect.Start();
            };
            Android.Content.Res.AssetFileDescriptor assetFileDescriptor = Application.Context.Assets.OpenFd(fileName);
            mPlayer_SoundEffect.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
            mPlayer_SoundEffect.SetVolume(mEffectsVolume, mEffectsVolume);
            mPlayer_SoundEffect.Prepare();
        }




        Android.Media.MediaPlayer mPlayer_BackgroundMusic;

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

            mPlayer_BackgroundMusic = new Android.Media.MediaPlayer();
            mPlayer_BackgroundMusic.Prepared += (s, e) =>
            {
                mPlayer_BackgroundMusic.Start();
            };

            mPlayer_BackgroundMusic.SeekComplete += (s, e) => // 循环播放背景音乐
            {
                mPlayer_BackgroundMusic.SeekTo(0);
                mPlayer_BackgroundMusic.Start();
            };

            Android.Content.Res.AssetFileDescriptor assetFileDescriptor = Application.Context.Assets.OpenFd(fileName);
            mPlayer_BackgroundMusic.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
            mPlayer_BackgroundMusic.SetVolume(mBackgroundMusicVolume, mBackgroundMusicVolume);
            mPlayer_BackgroundMusic.Prepare();
        }

        public void StopBackgroundMusic()
        {
            if (mPlayer_BackgroundMusic != null && mPlayer_BackgroundMusic.IsPlaying == true)
            {
                mPlayer_BackgroundMusic.Stop();
            }
        }


        private Android.Media.MediaPlayer mPlayer_Asset { get; set; }

        /// <summary>
        /// 测试播放 Android Assets 内的音频资源
        /// </summary>
        /// <param name="fileName"></param>
        public void PlayAssetsFile(string fileName)
        {
            lock (_LOCK_)
            {
                if (mPlayer_Asset == null)
                {
                    mPlayer_Asset = new Android.Media.MediaPlayer();
                    mPlayer_Asset.Prepared += (s, e) =>
                    {
                        mPlayer_Asset.Start();
                    };
                }
                else
                {
                    mPlayer_Asset.Reset();
                }

                Android.Content.Res.AssetFileDescriptor assetFileDescriptor = Application.Context.Assets.OpenFd(fileName);
                mPlayer_Asset.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
                mPlayer_Asset.Prepare();
            }
        }









        private Android.Media.MediaPlayer mPlayer_SoundEffect_Beep { get; set; }

        public void PlayBeep()
        {
            lock (_LOCK_)
            {
                if (mIsEffectsOn == false)
                {
                    return;
                }

                if (mPlayer_SoundEffect_Beep == null)
                {
                    mPlayer_SoundEffect_Beep = Android.Media.MediaPlayer.Create((Activity)Xamarin.Forms.Forms.Context, Resource.Raw.beep);
                }

                mPlayer_SoundEffect_Beep.SetVolume(mEffectsVolume, mEffectsVolume);
                mPlayer_SoundEffect_Beep.Start();
            }
        }

        private Android.Media.MediaPlayer mPlayer_SoundEffect_Error { get; set; }

        public void PlayError()
        {
            lock (_LOCK_)
            {
                if (mIsEffectsOn == false)
                {
                    return;
                }

                if (mPlayer_SoundEffect_Error == null)
                {
                    mPlayer_SoundEffect_Error = Android.Media.MediaPlayer.Create((Activity)Xamarin.Forms.Forms.Context, Resource.Raw.error);
                }

                mPlayer_SoundEffect_Error.SetVolume(mEffectsVolume, mEffectsVolume);
                mPlayer_SoundEffect_Error.Start();
            }
        }


    }
}