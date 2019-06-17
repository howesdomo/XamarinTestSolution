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
    public partial class PageScreen : ContentPage
    {
        public PageScreen()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            // 屏幕方向
            this.btn1.Clicked += Btn1_Clicked;
            this.btn2.Clicked += Btn2_Clicked;
            this.btn2_2.Clicked += Btn2_2_Clicked;
            this.btn3.Clicked += Btn3_Clicked;
            this.btn3_2.Clicked += Btn3_2_Clicked;
            this.btn4.Clicked += Btn4_Clicked;

            // 屏幕常亮 --> OnAppearing()
            // 由于绑定 App.Screen.ScreenKeepOn 的值
            // 故每次 Apperaing() 对值进行一次更新
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // 屏幕常亮
            swScreenKeepOn.IsToggled = App.Screen.ScreenKeepOn;
            swScreenKeepOn.Toggled += swScreenKeepOn_Toggled;
        }

        protected override void OnDisappearing()
        {
            // 注销屏幕常亮
            swScreenKeepOn.Toggled -= swScreenKeepOn_Toggled;
            // 屏幕还原到根据陀螺仪方向
            App.Screen.Unspecified();

            base.OnDisappearing();
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            App.Screen.Unspecified();
        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            App.Screen.ForcePortrait();
        }

        private void Btn2_2_Clicked(object sender, EventArgs e)
        {
            App.Screen.ForceReversePortrait();
        }

        private void Btn3_Clicked(object sender, EventArgs e)
        {
            App.Screen.ForceLandscapeLeft();
        }

        private void Btn3_2_Clicked(object sender, EventArgs e)
        {
            App.Screen.ForceLandscapeRight();
        }

        private void Btn4_Clicked(object sender, EventArgs e)
        {
            App.Screen.ForceNosensor();
        }

        async void swScreenKeepOn_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                App.Screen.ScreenKeepOn = e.Value;

                string msg = string.Empty;

                if (App.Screen.ScreenKeepOn)
                {
                    msg = "屏幕设为常亮";
                }
                else
                {
                    msg = "取消屏幕常亮";
                }
                await DisplayAlert("提示", msg, "确定");
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                await DisplayAlert("捕获异常", msg, "确定");
            }
        }
    }
}