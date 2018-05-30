using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageWebServiceDemo : ContentPage
    {
        public PageWebServiceDemo()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnTest.Clicked += BtnTest_Clicked;
        }

        async void BtnTest_Clicked(object sender, EventArgs e)
        {
            //AppWebServer.WebService1SoapClient web = new AppWebServer.WebService1SoapClient(new AppWebServer.WebService1SoapClient.EndpointConfiguration()
            //{

            //});
            //var tr = await web.HelloWorldAsync();
        }
    }
}