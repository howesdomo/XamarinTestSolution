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
            DateTime now = new DateTime(2018, 9, 5);

            ObservableCollection<HelloDevExpressOrder> toShow = new ObservableCollection<HelloDevExpressOrder>();
            toShow.Add(new HelloDevExpressOrder() { Photo = ImageSource.FromResource("Client.Images.DevExpressDemo.1.jpg"), IsSelected = true,
                OrderNo = "A1", CreateTime = now, TotalPrice = 13.50m, DeliveryTime = now, LackPrice = 5.72m,
                Description = new Sub_HelloDevExpressOrder() { Msg = "A1-415", CreateTime = DateTime.Now } });

            toShow.Add(new HelloDevExpressOrder() { Photo = ImageSource.FromResource("Client.Images.DevExpressDemo.3.jpg"),
                OrderNo = "A3", CreateTime = now.AddDays(2), TotalPrice = 89.32m, DeliveryTime = null, LackPrice = 1.142m,
                Description = new Sub_HelloDevExpressOrder() { Msg = "A3-215", CreateTime = DateTime.Now } });

            // 测试过若没有图片资源不会报错
            toShow.Add(new HelloDevExpressOrder() { Photo = ImageSource.FromResource("Client.Images.DevExpressDemo.2.jpg"), 
                OrderNo = "A2", CreateTime = now.AddDays(-1), TotalPrice = 0m, DeliveryTime = null, LackPrice = 3.88m,
                Description = new Sub_HelloDevExpressOrder() { Msg = "A2-531", CreateTime = DateTime.Now } });

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

    
}