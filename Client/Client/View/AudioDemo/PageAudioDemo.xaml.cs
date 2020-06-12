using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAudioDemo : ContentPage
    {
        public PageAudioDemo()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnSoundEffect_Beep.Clicked += btnSoundEffect_Beep_Clicked;
            this.btnSoundEffect_Error.Clicked += btnSoundEffect_Error_Clicked;
            this.btnSoundEffect_Warn.Clicked += btnSoundEffect_Warn_Clicked;
            this.btnSoundEffect_Takephoto.Clicked += btnSoundEffect_Takephoto_Clicked;
            this.btnSoundEffect_Screenshot.Clicked += btnSoundEffect_Screenshot_Clicked;


            this.btnSoundEffect.Clicked += BtnSoundEffect_Clicked;
            this.btnBGMPlay.Clicked += BtnBGMPlay_Clicked;
            this.btnBGMStop.Clicked += BtnBGMStop_Clicked;

            this.btnAssetsFileName.Clicked += BtnAssetsFileName_Clicked;
        }

        private void btnSoundEffect_Beep_Clicked(object sender, EventArgs e)
        {
            App.AudioPlayer.PlayBeep();
        }

        private void btnSoundEffect_Error_Clicked(object sender, EventArgs e)
        {
            App.AudioPlayer.PlayError();
        }

        private void btnSoundEffect_Warn_Clicked(object sender, EventArgs e)
        {
            App.AudioPlayer.PlayWarn();
        }

        private void btnSoundEffect_Screenshot_Clicked(object sender, EventArgs e)
        {
            App.AudioPlayer.PlayScreenshot();
        }

        private void btnSoundEffect_Takephoto_Clicked(object sender, EventArgs e)
        {
            App.AudioPlayer.PlayTakePhoto();
        }

        async void BtnSoundEffect_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.AudioPlayer.PlaySoundEffect(this.txtSoundEffect.Text);
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                await DisplayAlert("错误", msg, "确定");
            }
        }

        async void BtnBGMPlay_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.AudioPlayer.PlayBackgroundMusic(this.txtBGM.Text);
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                await DisplayAlert("错误", msg, "确定");
            }
        }

        async void BtnBGMStop_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.AudioPlayer.StopBackgroundMusic();
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                await DisplayAlert("错误", msg, "确定");
            }
        }

        async void BtnAssetsFileName_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.AudioPlayer.PlayAssetsFile(this.txtAssetsFileName.Text);
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                await DisplayAlert("错误", msg, "确定");
            }
        }


    }
}