using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinComponentDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageXamarinComponentMenu : ContentPage
    {
        public PageXamarinComponentMenu()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnEntry.Clicked += btnEntry_Clicked;
            this.btnSearchBar.Clicked += btnSearchBar_Clicked;
            this.btnFilterBar.Clicked += btnFilterBar_Clicked;
        }


        async void btnEntry_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageEntry());
        }

        async void btnSearchBar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSearchBar());
        }

        async void btnFilterBar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageFilterBar());
        }
    }
}