using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.BaiduDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBaiduMenu : ContentPage
    {
        public PageBaiduMenu()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnPageBaiduLocation.Clicked += BtnPageBaiduLocation_Clicked;
        }

        async void BtnPageBaiduLocation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageBaiduLocation());
        }
    }
}