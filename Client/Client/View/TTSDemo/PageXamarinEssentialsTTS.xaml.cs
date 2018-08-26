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
    public partial class PageXamarinEssentialsTTS : ContentPage
    {
        Common.TTSByXamarin_Essentials mTTS = new Common.TTSByXamarin_Essentials();

        public PageXamarinEssentialsTTS()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {

            btnTTS_1.Clicked += BtnTTS_1_Clicked;
            btnTTS_2.Clicked += BtnTTS_2_Clicked;

            btnTTS_3.Clicked += BtnTTS_3_Clicked;
            btnTTS_3Stop.Clicked += BtnTTS_3Stop_Clicked;

            btnTTS_4.Clicked += BtnTTS_4_Clicked;
            btnTTS_5.Clicked += BtnTTS_5_Clicked;
            btnTTS_6.Clicked += BtnTTS_6_Clicked;
        }


        async void BtnTTS_1_Clicked(object sender, EventArgs e)
        {
            await mTTS.SpeakNowDefaultSettings();
        }

        private void BtnTTS_2_Clicked(object sender, EventArgs e)
        {
            mTTS.SpeakNowDefaultSettings2();
        }

        async void BtnTTS_3_Clicked(object sender, EventArgs e)
        {
            await mTTS.SpeakNowDefaultSettings3();
        }


        private void BtnTTS_3Stop_Clicked(object sender, EventArgs e)
        {
            mTTS.CancelSpeech();
        }

        private void BtnTTS_4_Clicked(object sender, EventArgs e)
        {
            mTTS.SpeakMultiple();
        }

        async void BtnTTS_5_Clicked(object sender, EventArgs e)
        {
            await mTTS.SpeakNow();
        }

        async void BtnTTS_6_Clicked(object sender, EventArgs e)
        {
            await mTTS.SpeakNow2();
        }

    }
}