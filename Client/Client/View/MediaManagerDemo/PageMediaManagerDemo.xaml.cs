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
        public PageMediaManagerDemo()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnPlay.Clicked += BtnPlay_Clicked;

            this.btnContinue.Clicked += BtnContinue_Clicked;
            this.btnPause.Clicked += ButtonPause_Clicked;
            this.btnFullScreen.Clicked += ButtonFullScreen_Clicked;
        }

        async void BtnContinue_Clicked(object sender, EventArgs e)
        {
            if (mPosition.HasValue == false)
            {
                return;
            }

            await MediaManager.CrossMediaManager.Current.SeekTo(mPosition.Value);

            btnPause.IsVisible = false;
            btnContinue.IsVisible = true;

            mPosition = null;
        }

        public TimeSpan? mPosition { get; set; } = null;

        async void BtnPlay_Clicked(object sender, EventArgs e)
        {
            try
            {
                await MediaManager.CrossMediaManager.Current.Play(this.txtUrl.Text);

                mPosition = MediaManager.CrossMediaManager.Current.Position;

                btnPause.IsVisible = false;
                btnContinue.IsVisible = true;
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private async void ButtonPause_Clicked(object sender, EventArgs e)
        {
            await MediaManager.CrossMediaManager.Current.Pause();
            btnPause.IsVisible = false;
            btnContinue.IsVisible = true;
        }

        private void ButtonFullScreen_Clicked(object sender, EventArgs e)
        {
            player.AspectMode = MediaManager.Video.VideoAspectMode.AspectFill;
        }

    }
}