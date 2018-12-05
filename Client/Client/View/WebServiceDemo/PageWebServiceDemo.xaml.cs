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
            this.btnWebServiceLastest.Clicked += BtnWebServiceLastest_Clicked;

            this.btnTest5_1.Clicked += BtnTest5_1_Clicked;
            this.btnTest5_2.Clicked += BtnTest5_2_Clicked;

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


        private void BtnWebServiceLastest_Clicked(object sender, EventArgs e)
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

            new WebService().CollectUnhandleException
            (
                errorMsg: "Test",
                u: Common.StaticInfo.CurrentUser,
                page: this,
                handle: (soapResult) =>
                {
                    if (soapResult.IsComplete == false)
                    {
                        DisplayAlert("Error", soapResult.ExceptionInfo, "确定");
                        return;
                    }
                    else if (soapResult.IsSuccess == false)
                    {
                        DisplayAlert("Error", soapResult.BusinessExceptionInfo, "确定");
                        return;
                    }
                    else
                    {
                        DisplayAlert("提交", "提交成功", "确认");
                    }
                }
            );

        }


        async void BtnTest5_1_Clicked(object sender, EventArgs e)
        {
            Random random = new Random();

            DateTime now = DateTime.Now;
            List<DL.Model.Order> orders = new List<DL.Model.Order>();
            int orderCount = Convert.ToInt32(this.txtListCount.Text);

            for (int i = 0; i < orderCount; i++)
            {
                DL.Model.Order toAdd = new DL.Model.Order();
                orders.Add(toAdd);

                toAdd.OrderNo = "Order{0}".FormatWith(i);
                decimal tempDiffQty = i * random.Next(4) + i * random.Next(5);
                toAdd.ScanQty = i * random.Next(6) + i * random.Next(7);
                toAdd.PlanQty = toAdd.ScanQty.Value + tempDiffQty;
                toAdd.CreateTime = now;
                toAdd.EndTime = DateTime.Now;
                toAdd.CartonList = new List<DL.Model.Carton>();
                toAdd.CartonList.Add(new DL.Model.Carton() { CartonNo = "CartonNo{0}".FormatWith(i) });
            }

            new WebService().TestWebService_GetOrders_isCompress_False
            (
                orders: orders,
                u: Common.StaticInfo.CurrentUser,
                page: this,
                handle: (soapResult) =>
                {
                    if (soapResult.IsComplete == false)
                    {
                        DisplayAlert("Error", soapResult.ExceptionInfo, "确定");
                        return;
                    }
                    else if (soapResult.IsSuccess == false)
                    {
                        DisplayAlert("Error", soapResult.BusinessExceptionInfo, "确定");
                        return;
                    }
                    else
                    {
                        int len = soapResult.ReturnObjectJson.Length;
                        string msg = "接收信息长度 {0}".FormatWith(len);

                        // 1 解密
                        // 2 解压
                        List<DL.Model.Order> fromWebService = Util.JsonUtils.DeserializeObject<List<DL.Model.Order>>(soapResult.ReturnObjectJson);
                        msg += "\r\nList<DL.Model.Order> 的长度 {0}".FormatWith(fromWebService.Count);
                        DisplayAlert("成功接收", msg, "确定");
                        return;
                    }
                }
            );
        }


        async void BtnTest5_2_Clicked(object sender, EventArgs e)
        {

            Random random = new Random();

            DateTime now = DateTime.Now;
            List<DL.Model.Order> orders = new List<DL.Model.Order>();

            // 尝试过发送 1百万条压缩过的数据到服务器中, 服务器能成功接收到
            // 记录到的 压缩信息文件大小为 28.2MB
            // 但由于返回的数据未进行压缩, 暂时会由于内存溢出而崩掉

            int orderCount = Convert.ToInt32(this.txtListCount.Text);
            for (int i = 0; i < orderCount; i++)
            {
                DL.Model.Order toAdd = new DL.Model.Order();
                orders.Add(toAdd);

                toAdd.OrderNo = "Order{0}".FormatWith(i);
                decimal tempDiffQty = i * random.Next(4) + i * random.Next(5);
                toAdd.ScanQty = i * random.Next(6) + i * random.Next(7);
                toAdd.PlanQty = toAdd.ScanQty.Value + tempDiffQty;
                toAdd.CreateTime = now;
                toAdd.EndTime = DateTime.Now;
                toAdd.CartonList = new List<DL.Model.Carton>();
                toAdd.CartonList.Add(new DL.Model.Carton() { CartonNo = "CartonNo{0}".FormatWith(i) });
            }

            new WebService().TestWebService_GetOrders_isCompress_True
            (
                orders: orders,
                u: Common.StaticInfo.CurrentUser,
                page: this,
                handle: (soapResult) =>
                {
                    if (soapResult.IsComplete == false)
                    {
                        DisplayAlert("Error", soapResult.ExceptionInfo, "确定");
                        return;
                    }
                    else if (soapResult.IsSuccess == false)
                    {
                        DisplayAlert("Error", soapResult.BusinessExceptionInfo, "确定");
                        return;
                    }
                    else
                    {
                        int len = soapResult.ReturnObjectJson.Length;
                        string msg = "接收信息长度 {0}".FormatWith(len);                        
                        List<DL.Model.Order> fromWebService = Util.JsonUtils.DeserializeObject<List<DL.Model.Order>>(soapResult.ReturnObjectJson);
                        msg += "\r\nList<DL.Model.Order> 的长度 {0}".FormatWith(fromWebService.Count);
                        DisplayAlert("成功接收", msg, "确定");
                        return;
                    }
                }
            );

        }
    }
}