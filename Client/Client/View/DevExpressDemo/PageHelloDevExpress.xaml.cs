using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.DevExpressDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHelloDevExpress : ContentPage
    {
        public PageHelloDevExpress()
        {
            InitializeComponent();
            BindData();

            // 若离奇的出现Null报错, 很大可能是由于没有初始化 DevExpress控件
            // 请在 Client.Android 中 加上以下初始化代码
            // DevExpress.Mobile.Forms.Init();
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

    public class PageHelloDevExpressViewModel : Xamarin.Forms.BindableObject
    {
        public PageHelloDevExpressViewModel()
        {
            var toShow = new ObservableCollection<HelloDevExpressOrder>();

            toShow.Add(new HelloDevExpressOrder() { OrderNo = "A1", CreateTime = DateTime.Now, TotalPrice = 13.50m, DeliveryTime = DateTime.Now, LackPrice = 5m, Description = new Sub_HelloDevExpressOrder() { Msg = "12315", CreateTime = DateTime.Now } });
            toShow.Add(new HelloDevExpressOrder() { OrderNo = "A3", CreateTime = DateTime.Now, TotalPrice = 89.32m, DeliveryTime = null, LackPrice = 1m, Description = new Sub_HelloDevExpressOrder() { Msg = "12315", CreateTime = DateTime.Now } });
            toShow.Add(new HelloDevExpressOrder() { OrderNo = "A2", CreateTime = DateTime.Now, TotalPrice = 0m, DeliveryTime = null, LackPrice = 3m, Description = new Sub_HelloDevExpressOrder() { Msg = "12315", CreateTime = DateTime.Now } });

            Orders = toShow;
        }

        ObservableCollection<HelloDevExpressOrder> orders;

        public ObservableCollection<HelloDevExpressOrder> Orders
        {
            get { return orders; }
            set
            {
                if (orders != value)
                {
                    orders = value;
                    OnPropertyChanged("Orders");
                }
            }
        }
    }

    public class HelloDevExpressOrder
    {
        public string OrderNo { get; set; }

        public DateTime CreateTime { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public decimal? LackPrice { get; set; }

        public Sub_HelloDevExpressOrder Description { get; set; }
    }

    public class Sub_HelloDevExpressOrder
    {
        public string Msg { get; set; }

        public DateTime CreateTime { get; set; }
    }
}