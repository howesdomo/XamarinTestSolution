using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Client.View.Games
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGamesList : ContentPage
    {
        public PageGamesList()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnCRW.Clicked += BtnCRW_Clicked;
        }

        async void BtnCRW_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Client.View.Games.CRW.PageMain());
        }
    }
}