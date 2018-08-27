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

            mAppWeb = new AppWebWebService(AppWebServiceReference.AppWebServiceSoapClient.EndpointConfiguration.AppWebServiceSoap);

            initEvent();
        }

        private void initEvent()
        {
            this.btnTest.Clicked += BtnTest_Clicked;
            this.btnTestWebAPI.Clicked += BtnTestWebAPI_Clicked;
            this.btnTest3.Clicked += BtnTest3_Clicked;
            
        }


        private void BtnTestWebAPI_Clicked(object sender, EventArgs e)
        {
            string uri = "http://192.168.1.216:17911/AppWebApplication461/api/orders";
            Data_WebAPI.MyWebClient web = new Data_WebAPI.MyWebClient(uri);
            web.GetOrder();
        }

        async void BtnTest_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 报错 无法调用 asmx // TODO
                var r = await mAppWeb.GetOrderAsync();
                System.Diagnostics.Debug.WriteLine(Util.JsonUtils.SerializeObject(r));
            }
            catch (Exception ex)
            {
                string errorMsg = ex.GetFullInfo();
                await DisplayAlert("Error", errorMsg, "确定");
            }
        }

        async void BtnTest3_Clicked(object sender, EventArgs e)
        {
            if (Common.StaticInfo.CurrentUser == null)
            {
                Common.StaticInfo.CurrentUser = new DL.Model.User()
                {
                    ID = Guid.Empty,
                    LoginAccount = "D3333",
                    UserName = "Howe",
                    DeviceInfo = Util.JsonUtils.SerializeObject(Common.StaticInfo.DeviceInfo)
                };
            }
            try
            {
                new WebService().CollectUnhandleExceptionV1
                (
                    "Test",
                    Common.StaticInfo.CurrentUser,
                    Bw_RunWorkerCompleted
                 );
            }
            catch (Exception ex)
            {
                string errorMsg = ex.GetFullInfo();
                await DisplayAlert("Error", errorMsg, "确定");
            }
        }

        void Bw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs args)
        {
            if (args.Error != null)
            {
                DisplayAlert("Error", args.Error.GetFullInfo(), "确定");
                return;
            }

            if (args.Result == null)
            {
                DisplayAlert("Error", "SOAPResult为空", "确定");
                return;
            }

            Util.WebService.SOAPResult soapResult = args.Result as Util.WebService.SOAPResult;

            if (soapResult.IsComplete == false)
            {
                DisplayAlert("Error", soapResult.ExceptionInfo, "确定");
            }
            else if (soapResult.IsSuccess == false)
            {
                DisplayAlert("Error", soapResult.BusinessExceptionInfo, "确定");
            }
            else
            {
                DisplayAlert("提示", "执行成功。", "确定");
            }
        }
    }
}