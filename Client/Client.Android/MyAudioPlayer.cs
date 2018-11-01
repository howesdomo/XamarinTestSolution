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
        private static object _LOCK_ = new object();

        private static Android.Media.MediaPlayer sAudioPlayer { get; set; }
        private static Android.Media.MediaPlayer sAudioPlayer_Beep { get; set; }
        private static Android.Media.MediaPlayer sAudioPlayer_Error { get; set; }

        public MyAudioPlayer()
        {

        }

        /// <summary>
        /// 测试播放 Android Raw 资源成功
        /// </summary>
        /// <param name="fileName"></param>
        public void PlayAssetsFile(string fileName)
        {
            lock (_LOCK_)
            {
                if (sAudioPlayer == null)
                {
                    sAudioPlayer = new Android.Media.MediaPlayer();
                    sAudioPlayer.Prepared += (s, e) =>
                    {
                        sAudioPlayer.Start();
                    };
                }
                else
                {
                    sAudioPlayer.Reset();
                }

                Android.Content.Res.AssetFileDescriptor assetFileDescriptor = Application.Context.Assets.OpenFd(fileName);
                sAudioPlayer.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
                sAudioPlayer.Prepare();
            }
        }

        public void PlayBeep()
        {
            lock (_LOCK_)
            {
                if (sAudioPlayer_Beep == null)
                {
                    sAudioPlayer_Beep = Android.Media.MediaPlayer.Create((Activity)Xamarin.Forms.Forms.Context, Resource.Raw.beep);
                }

                //if (sAudioPlayer_Beep.IsPlaying)
                //{
                //    sAudioPlayer_Beep.Stop();
                //}

                sAudioPlayer_Beep.Start();
            }
        }

        public void PlayError()
        {
            lock (_LOCK_)
            {
                if (sAudioPlayer_Error == null)
                {
                    sAudioPlayer_Error = Android.Media.MediaPlayer.Create((Activity)Xamarin.Forms.Forms.Context, Resource.Raw.error);
                }

                //if (sAudioPlayer_Error.IsPlaying)
                //{
                //    sAudioPlayer_Error.Stop();
                //}

                sAudioPlayer_Error.Start();
            }
        }


    }
}