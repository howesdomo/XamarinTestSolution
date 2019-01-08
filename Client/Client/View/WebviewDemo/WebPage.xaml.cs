using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.WebviewDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        public WebPage()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnGO.Clicked += BtnGO_Clicked;
        }

        async void BtnGO_Clicked(object sender, EventArgs e)
        {
            try
            {
                this.webView1.Source = this.txtUrl.Text;
            }
            catch (Exception ex)
            {
                await DisplayAlert
                (
                    title: "错误信息",
                    message: ex.GetFullInfo(),
                    cancel: "确定"
                );
            }
        }
    }
}