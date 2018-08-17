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
	public partial class PageWebviewMenu : ContentPage
	{
		public PageWebviewMenu ()
		{
			InitializeComponent ();
            initEvent();
		}

        private void initEvent()
        {
            btn1.Clicked += Btn1_Clicked;
            btn2.Clicked += Btn2_Clicked;
            btn3.Clicked += Btn3_Clicked;
            btn4.Clicked += Btn4_Clicked;
        }

        async void Btn1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocalHtml());
        }

        async void Btn2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocalHtmlBaseUrl());
        }

        async void Btn3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebAppPage());
        }

        async void Btn4_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebPage());
        }
    }
}