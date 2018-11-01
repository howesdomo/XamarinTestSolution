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
            this.btn1.Clicked += Btn1_Clicked;
            this.btn2.Clicked += Btn2_Clicked;
            this.btn3.Clicked += Btn3_Clicked;
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            App.AudioPlayer.PlayBeep();
        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            App.AudioPlayer.PlayError();
        }

        async void Btn3_Clicked(object sender, EventArgs e)
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