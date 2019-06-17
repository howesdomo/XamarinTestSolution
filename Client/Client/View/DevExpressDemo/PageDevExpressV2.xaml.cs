using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.DevExpressDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDevExpressV2 : ContentPage
    {
        public PageDevExpressV2()
        {
            InitializeComponent();
            initEvent();
            BindData();
        }

        private void initEvent()
        {
            this.Appearing += PageDevExpressV2_Appearing;
            this.Disappearing += PageDevExpressV2_Disappearing;
        }

        private void PageDevExpressV2_Appearing(object sender, EventArgs e)
        {
            App.Screen.ForceLandscapeLeft();
        }

        private void PageDevExpressV2_Disappearing(object sender, EventArgs e)
        {
            App.Screen.Unspecified();
        }

        async void BindData()
        {
            BindingContext = await LoadData();
        }

        Task<PageHelloDevExpressViewModel> LoadData()
        {
            return Task<PageHelloDevExpressViewModel>.Run(() => new PageHelloDevExpressViewModel());
        }
    }
}