using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.MediaManagerDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMediaManagerDemo : ContentPage
    {
        private MediaManager.Forms.VideoView videoView { get; set; } 

        public PageMediaManagerDemo()
        {
            InitializeComponent();
            videoView = new MediaManager.Forms.VideoView();
            gridPlayer.Children.Add(videoView);

            initEvent();
        }

        private void initEvent()
        {
            this.btnPlay.Clicked += BtnPlay_Clicked;

            this.btnContinue.Clicked += BtnContinue_Clicked;
            this.btnPause.Clicked += ButtonPause_Clicked;
            this.btnFullScreen.Clicked += ButtonFullScreen_Clicked;
        }

        void BtnContinue_Clicked(object sender, EventArgs e)
        {
            if (mPosition.HasValue == false)
            {
                return;
            }

            //await MediaManager.CrossMediaManager.Current.SeekTo(mPosition.Value);
            //await MediaManager.CrossMediaManager.Current.Play();

            btnPause.IsVisible = false;
            btnContinue.IsVisible = true;

            mPosition = null;
        }

        public TimeSpan? mPosition { get; set; } = null;

        async void BtnPlay_Clicked(object sender, EventArgs e)
        {
            //if (MediaManager.CrossMediaManager.IsSupported == false)
            //{
            //    await DisplayAlert("错误", "IsSupported == false", "确定");
            //    return;
            //}

            try
            {
                string path = this.txtUrl.Text;
                //if (path.IndexOf("http") < 0)
                //{
                //    path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), path);

                //    if (System.IO.File.Exists(path) == false) { return; }

                //    await Plugin.MediaManager.CrossMediaManager.Current.Play(file: new System.IO.FileInfo(path));
                //    // await MediaManager.CrossMediaManager.Current.MediaPlayer.Play(r.Result);
                //}
                //else
                //{
                await MediaManager.CrossMediaManager.Current.Play(path);
                //}

                btnPause.IsVisible = false;
                btnContinue.IsVisible = true;
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                await DisplayAlert("错误", msg, "确定");
                return;
            }
        }

        private async void ButtonPause_Clicked(object sender, EventArgs e)
        {
            await MediaManager.CrossMediaManager.Current.Pause();

            // mPosition = MediaManager.CrossMediaManager.Current.Position;

            btnPause.IsVisible = false;
            btnContinue.IsVisible = true;
        }

        private void ButtonFullScreen_Clicked(object sender, EventArgs e)
        {
            // Plugin.MediaManager.CrossMediaManager.Current.VideoPlayer.AspectMode = Plugin.MediaManager.Abstractions.Enums.VideoAspectMode.AspectFill;
            videoView.AspectMode = MediaManager.Video.VideoAspectMode.AspectFill;
        }

    }
}