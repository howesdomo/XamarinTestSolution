using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.FFImageLoadingDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDemoMenu : ContentPage
    {
        public PageDemoMenu()
        {
            InitializeComponent();
            initEvent();

            // FFImageLoading Sample 的 App.cs 中含有此代码
            FFImageLoading.Forms.CachedImage.FixedOnMeasureBehavior = true;
            FFImageLoading.Forms.CachedImage.FixedAndroidMotionEventHandler = true;
        }

        private void initEvent()
        {
            this.btnPageSimpleGif.Clicked += BtnPageSimpleGif_Clicked;
            this.btnPageSimpleWebp.Clicked += BtnPageSimpleWebp_Clicked;
            this.btnPageSimpleSvg.Clicked += BtnPageSimpleSvg_Clicked;
        }

        async void BtnPageSimpleGif_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSimpleGif());
        }

        async void BtnPageSimpleWebp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSimpleWebp());
        }

        async void BtnPageSimpleSvg_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSimpleSVG());
        }
    }
}