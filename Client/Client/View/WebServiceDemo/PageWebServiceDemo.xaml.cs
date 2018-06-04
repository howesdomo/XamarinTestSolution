using Client.Common;
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
        AppWebWebService mAppWeb { get; set; }

        public PageWebServiceDemo()
        {
            InitializeComponent();

            // test 1 == 运行 await mAppWeb.GetOrderAsync(); 报错
            // mAppWeb = new AppWebWebService(); 

            WebSetting setting = new WebSetting
            (
                serviceSettingName: "A",
                ipOrWebAddress: "192.168.1.172",
                port: "17904",
                appName: "AppWebApplication45/AppWebService.asmx"
            );

            mAppWeb = new AppWebWebService(
                setting, 
                AppWebServiceReference.AppWebServiceSoapClient.EndpointConfiguration.AppWebServiceSoap12);

            initEvent();
        }

        private void initEvent()
        {
            this.btnTest.Clicked += BtnTest_Clicked;
            this.btnTestWebAPI.Clicked += BtnTestWebAPI_Clicked;
        }

        private void BtnTestWebAPI_Clicked(object sender, EventArgs e)
        {
            string uri = "http://192.168.1.215:17911/AppWebApplication461/api/orders";
            Data_WebAPI.MyWebClient web = new Data_WebAPI.MyWebClient(uri);
            web.GetOrder();
        }

        async void BtnTest_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 报错 无法调用 asmx // TODO
                await mAppWeb.GetOrderAsync();
            }
            catch (Exception ex)
            {
                string errorMsg = ex.GetFullInfo();
                await DisplayAlert("Error", errorMsg, "确定");
            }
        }
    }
}