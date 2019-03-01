using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.AllPageDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllPageList : ContentPage
    {
        public AllPageList()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            btnMasterDetailPage.Clicked += BtnMasterDetailPage_Clicked;
            btnCarousePage.Clicked += BtnCarousePage_Clicked;
        }

        async void BtnMasterDetailPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MasterDetailPage()
            {
                Master = new MasterDetailPage1Master(),
                Detail = new MasterDetailPage1Detail()
            });
        }


        async void BtnCarousePage_Clicked(object sender, EventArgs e)
        {
            CarouselPage carouselPage = new CarouselPage();
            carouselPage.Children.Add(new CarousePageDetail1());
            carouselPage.Children.Add(new CarousePageDetail2());
            carouselPage.Children.Add(new CarousePageDetail3());

            await Navigation.PushAsync(carouselPage);
        }

    }
}