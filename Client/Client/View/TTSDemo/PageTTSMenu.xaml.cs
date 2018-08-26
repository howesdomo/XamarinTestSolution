using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.TTSDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTTSMenu : ContentPage
    {

        public PageTTSMenu()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            btnTTS_1.Clicked += BtnTTS_1_Clicked;
            btnTTS_2_1.Clicked += BtnTTS_2_1_Clicked;
            btnTTS_2_2.Clicked += BtnTTS_2_2_Clicked;
            btnTTS_2_3.Clicked += BtnTTS_2_3_Clicked;
            btnTTS_2_4.Clicked += BtnTTS_2_4_Clicked;
        }


        async void BtnTTS_1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageXamarinEssentialsTTS());
        }


        private void BtnTTS_2_1_Clicked(object sender, EventArgs e)
        {
            App.TTS.InitTextToSpeech();
        }

        async void BtnTTS_2_2_Clicked(object sender, EventArgs e)
        {
            var r = App.TTS.Check_InitTextToSpeech();

            string msg = "TTS已启动 : {0}".FormatWith(r);
            System.Diagnostics.Debug.WriteLine(msg);

            await DisplayAlert
            (
                title: "结果",
                message: msg,
                cancel: "确定"
            );
        }

        private void BtnTTS_2_3_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.TTS.Play(this.txtContent.Text);
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private void BtnTTS_2_4_Clicked(object sender, EventArgs e)
        {
            try
            {
                float args = 1f;
                float.TryParse(this.txtSpeechRate.Text, out args);
                App.TTS.SetSpeechRate(args);
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }


    }
}