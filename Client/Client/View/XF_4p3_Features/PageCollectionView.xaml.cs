using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XF_4p3_Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCollectionView : ContentPage
    {
        public PageCollectionView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var selectedItem = Util.JsonUtils.SerializeObjectWithFormatted(cv0.SelectedItem);
            var selectedItems = Util.JsonUtils.SerializeObjectWithFormatted(cv0.SelectedItems);
        }
    }

    public class PageCollectionView_ViewModel : BaseViewModel
    {
        public Command CMD_CheckSelectedItems { get; private set; }

        public Command CMD_Select3 { get; private set; }

        public Command CMD_Select1And3 { get; private set; }

        public Command CMD_ChangeSelectionMode { get; private set; }

        public Command CMD_UpdateOrder { get; private set; }

        public Command CMD_DeleteOrder { get; private set; }

        public PageCollectionView_ViewModel()
        {
            var o = new ObservableCollection<Order>();
            o.Add(new Order() { OrderNo = "A01", Title = "No.1", OrderType = new OrderType() { Title = "文具", OrderTypeId = 1 } });
            o.Add(new Order() { OrderNo = "A02", Title = "No.2", OrderType = new OrderType() { Title = "文具", OrderTypeId = 2 } });
            o.Add(new Order() { OrderNo = "A03", Title = "No.3", OrderType = new OrderType() { Title = "文具", OrderTypeId = 3 } });
            o.Add(new Order() { OrderNo = "A04", Title = "No.4", OrderType = new OrderType() { Title = "文具", OrderTypeId = 4 } });
            o.Add(new Order() { OrderNo = "A05", Title = "No.5", OrderType = new OrderType() { Title = "文具", OrderTypeId = 5 } });
            o.Add(new Order() { OrderNo = "A06", Title = "No.6", OrderType = new OrderType() { Title = "文具", OrderTypeId = 6 } });

            this.Orders = o;

            this.CMD_CheckSelectedItems = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("SelectedItem");
                System.Diagnostics.Debug.WriteLine(Util.JsonUtils.SerializeObjectWithFormatted(this.SelectedItem));
                System.Diagnostics.Debug.WriteLine("SelectedItems");
                System.Diagnostics.Debug.WriteLine(Util.JsonUtils.SerializeObjectWithFormatted(this.SelectedItems));
            });

            this.CMD_Select3 = new Command(() =>
            {
                this.SelectedItem = this.Orders[2];
            });

            this.CMD_Select1And3 = new Command(() =>
            {
                this.SelectedItems.Clear();
                this.SelectedItems.Add(this.Orders[0]);
                this.SelectedItems.Add(this.Orders[2]);
            });

            this.CMD_ChangeSelectionMode = new Command(() =>
            {
                this.SelectionMode =
                    this.SelectionMode == SelectionMode.Single ? SelectionMode.Multiple : SelectionMode.Single
                ;
            });

            this.CMD_UpdateOrder = new Command<Order>((order) =>
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast($"Update Order: {order.OrderNo}");
            });

            this.CMD_DeleteOrder = new Command<Order>((order) =>
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast($"Delete Order: {order.OrderNo}");
            });
        }

        private SelectionMode _SelectionMode = SelectionMode.Single;

        public SelectionMode SelectionMode
        {
            get { return _SelectionMode; }
            set
            {
                _SelectionMode = value;
                this.OnPropertyChanged();
            }
        }


        private ObservableCollection<Order> _Orders;
        public ObservableCollection<Order> Orders
        {
            get { return _Orders; }
            set
            {
                this.SetProperty
                (
                    ref _Orders,
                    value,
                    onChanged: () =>
                    {
                        value.CollectionChanged += Orders_OnCollectionChanged;
                    }
                );
            }
        }

        private void Orders_OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged(nameof(this.Orders));
        }

        private Order _SelectedItem;
        public Order SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        private IList<object> _SelectedItems;
        /// <summary>
        /// 注意 XAML 中, SelectedItems 必须要填写 Mode = TwoWay
        /// </summary>
        public IList<object> SelectedItems
        {
            get { return _SelectedItems; }
            set
            {
                _SelectedItems = value;
                this.OnPropertyChanged();
            }
        }

        #region !!! 绑定到 Selecteditems 失败 !!!

        //private IList<Order> _SelectedItems;

        //public IList<Order> SelectedItems
        //{
        //    get { return _SelectedItems; }
        //    set
        //    {
        //        _SelectedItems = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        //private List<object> _SelectedItems;

        //public List<object> SelectedItems
        //{
        //    get { return _SelectedItems; }
        //    set
        //    {
        //        _SelectedItems = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<object> _SelectedItems;

        //public ObservableCollection<object> SelectedItems
        //{
        //    get { return _SelectedItems; }
        //    set
        //    {
        //        _SelectedItems = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        #endregion
    }

    public class Order : BaseViewModel
    {
        private string _OrderNo;
        public string OrderNo
        {
            get { return _OrderNo; }
            set
            {
                _OrderNo = value;
                this.OnPropertyChanged();
            }
        }

        private List<OrderItem> _Items;
        public List<OrderItem> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
                this.OnPropertyChanged();
            }
        }

        private OrderType _OrderType;
        public OrderType OrderType
        {
            get { return _OrderType; }
            set
            {
                _OrderType = value;
                this.OnPropertyChanged();
            }
        }
    }

    public class OrderType : BaseViewModel
    {
        private int _OrderTypeId;

        public int OrderTypeId
        {
            get { return _OrderTypeId; }
            set
            {
                _OrderTypeId = value;
                this.OnPropertyChanged();
            }
        }

        public string _OrderTypeName;

        public string OrderTypeName
        {
            get { return _OrderTypeName; }
            set
            {
                _OrderTypeName = value;
                this.OnPropertyChanged();
            }
        }
    }


    public class OrderItem : BaseViewModel
    {
        private string _ProductCode;
        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                _ProductCode = value;
                this.OnPropertyChanged();
            }
        }

        private int _Qty;
        public int Qty
        {
            get { return _Qty; }
            set
            {
                _Qty = value;
                this.OnPropertyChanged();
            }
        }

    }
}