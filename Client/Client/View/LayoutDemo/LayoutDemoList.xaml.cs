using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.LayoutDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LayoutDemoList : ContentPage
    {
        public LayoutDemoList()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnFlexLayoutDemo.Clicked += BtnFlexLayoutDemo_Clicked;
            this.btnAbsoluteLayoutDemo.Clicked += BtnAbsoluteLayoutDemo_Clicked;
            this.btnRelativeLayoutDemo.Clicked += BtnRelativeLayoutDemo_Clicked;
        }


        async void BtnFlexLayoutDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FlexLayoutDemo());
        }

        async void BtnAbsoluteLayoutDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AbsoluteLayoutDemo());
        }

        async void BtnRelativeLayoutDemo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RelativeLayoutDemo());
        }

    }
}