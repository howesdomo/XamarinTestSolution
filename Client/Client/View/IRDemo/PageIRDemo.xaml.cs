using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.IRDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageIRDemo : ContentPage
    {
        public PageIRDemo()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btn1.Clicked += Btn1_Clicked;
            this.btn2.Clicked += Btn2_Clicked;
        }

        // 音量减
        int[] mJian = new int[] {
            2400, 600, 1200, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 600, 600, 600, 25750,
            2400, 600, 1200, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 600, 600, 600, 25750,
            2400, 600, 1200, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 600, 600, 600
        };

        // 音量加
        int[] mJia = new int[] {
            2400, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 600, 600, 600, 25750,
            2400, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 600, 600, 600, 25750,
            2400, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 600, 600, 600, 25750,
            2400, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 1200, 600, 600, 600, 600, 600, 600, 600, 600
        };

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            int freq = 0;
            try
            {
                freq = Convert.ToInt32(this.txtFreq.Text);
            }
            catch (Exception ex)
            {
                DisplayAlert("错误", ex.GetFullInfo(), "确认");
                return;
            }

            App.IR.Send(freq, mJia);
        }



        private void Btn2_Clicked(object sender, EventArgs e)
        {
            int freq = 0;
            try
            {
                freq = Convert.ToInt32(this.txtFreq.Text);
            }
            catch (Exception ex)
            {
                DisplayAlert("错误", ex.GetFullInfo(), "确认");
                return;
            }

            App.IR.Send(freq, mJian);
        }
    }
}