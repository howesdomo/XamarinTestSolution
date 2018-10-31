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
    public partial class PageDevExpressV3 : ContentPage
    {
        DevExpressV3_ViewModel ViewModel { get; set; }

        public PageDevExpressV3()
        {
            InitializeComponent();

            this.ViewModel = new DevExpressV3_ViewModel();
            this.BindingContext = this.ViewModel;

            initEvent();
            initData();

        }

        private void initEvent()
        {
            grid.RowTap += (sender, e) =>
            {
                if (e.FieldName == "IsChecked")
                {
                    int sourceRowIndex = grid.GetSourceRowIndex(e.RowHandle);
                    this.ViewModel.Orders[sourceRowIndex].IsChecked = !this.ViewModel.Orders[sourceRowIndex].IsChecked;
                    this.ViewModel._Orders_CollectionChanged(null, null);
                }
            };
        }

        private void initData()
        {
            System.Collections.ObjectModel.ObservableCollection<DevExpressV3_DataModel> list = new System.Collections.ObjectModel.ObservableCollection<DevExpressV3_DataModel>();

            for (int i = 0; i < 10; i++)
            {
                DevExpressV3_DataModel data = new DevExpressV3_DataModel();
                data.Name = "Row{0}".FormatWith(i);
                data.IsChecked = (i % 2) == 0;

                list.Add(data);
            }

            this.ViewModel.Orders = list;
        }
    }

    public class DevExpressV3_ViewModel : ViewModel.BaseViewModel
    {
        private System.Collections.ObjectModel.ObservableCollection<DevExpressV3_DataModel> _Orders;

        public System.Collections.ObjectModel.ObservableCollection<DevExpressV3_DataModel> Orders
        {
            get
            {
                return _Orders;
            }
            set
            {
                _Orders = value;

                if (_Orders != null)
                {
                    _Orders.CollectionChanged += _Orders_CollectionChanged;
                }

                this.OnPropertyChanged("Orders");
                this.OnPropertyChanged("OrdersInfo");
            }
        }

        public void _Orders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // 只有在增加 删除 移位 才会触发
            this.OnPropertyChanged("OrdersInfo");
        }

        public string OrdersInfo
        {
            get
            {
                string r = string.Empty;
                if (this.Orders != null && this.Orders.Count > 0)
                {
                    r = "共 {0} 条, 选中 {1} 条".FormatWith(this.Orders.Count, this.Orders.Count(i => i.IsChecked == true));
                }
                return r;
            }
        }
    }

    public class DevExpressV3_DataModel : ViewModel.BaseViewModel
    {
        private bool _IsChecked;

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                this.OnPropertyChanged("IsChecked");
            }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                this.OnPropertyChanged("Name");
            }
        }
    }


}