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
    public partial class PageDevExpressDemoMenu : ContentPage
    {
        public static string Tag
        {
            get
            {
                return "PageDevExpressDemoMenu";
            }
        }

        public PageDevExpressDemoMenu()
        {
            InitializeComponent();
            initEvent();

            
        }

        private void initEvent()
        {
            this.btnPageHelloDevExpress.Clicked += BtnPageHelloDevExpress_Clicked;
            this.btnDevExpressV2.Clicked += BtnDevExpressV2_Clicked;

            this.btnImgTest.Clicked += BtnImgTest_Clicked;
        }

        async void BtnPageHelloDevExpress_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.DevExpressDemo.PageHelloDevExpress());
        }

        async void BtnDevExpressV2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.DevExpressDemo.PageDevExpressV2());
        }

        private void BtnImgTest_Clicked(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Forms.ImageSource s = ImageSource.FromResource(this.txtImgTest.Text);
                imgTest.Source = s;
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Error(Tag, msg);
            }
        }
    }
}