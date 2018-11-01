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

        public MyAudioPlayer()
        {

        }

        /// <summary>
        /// 测试播放 Android Raw 资源成功
        /// </summary>
        /// <param name="fileName"></param>
        public void PlayAudioFile(string fileName)
        {
            lock (_LOCK_)
            {
                if (sAudioPlayer == null)
                {
                    sAudioPlayer = Android.Media.MediaPlayer.Create((Activity)Xamarin.Forms.Forms.Context, Resource.Raw.error);
                }

                sAudioPlayer.Start();
            }
        }

        public void PlayAudioFileV1(string fileName)
        {
            lock (_LOCK_)
            {
                if (sAudioPlayer == null)
                {
                    sAudioPlayer = new Android.Media.MediaPlayer();
                }

                Android.Content.Res.AssetFileDescriptor assetFileDescriptor = Application.Context.Assets.OpenFd(fileName);
                sAudioPlayer.Prepared += (s, e) =>
                {
                    sAudioPlayer.Start();
                };
                sAudioPlayer.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
                sAudioPlayer.Prepare();
            }
        }
    }
}