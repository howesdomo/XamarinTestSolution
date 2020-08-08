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
    public partial class PageCollectionView_LoadDataIncrementally : ContentPage
    {
        public PageCollectionView_LoadDataIncrementally()
        {
            InitializeComponent();
        }
    }

    public class PageCollectionView_LoadDataIncrementally_ViewModel : BaseViewModel
    {
        private bool _IsRefresh;
        public bool IsRefresh
        {
            get { return _IsRefresh; }
            set
            {
                _IsRefresh = value;
                this.OnPropertyChanged();
            }
        }

        public Command CMD_ThresholdReached { get; private set; }

        public Command CMD_Refresh_Orders { get; private set; }

        public PageCollectionView_LoadDataIncrementally_ViewModel()
        {
            var o = new ObservableCollection<Order>();
            o.Add(new Order() { OrderNo = "A01", Title = "No.1", OrderType = new OrderType() { Title = "文具", OrderTypeId = 1 } });
            o.Add(new Order() { OrderNo = "A02", Title = "No.2", OrderType = new OrderType() { Title = "文具", OrderTypeId = 2 } });
            o.Add(new Order() { OrderNo = "A03", Title = "No.3", OrderType = new OrderType() { Title = "文具", OrderTypeId = 3 } });
            o.Add(new Order() { OrderNo = "A04", Title = "No.4", OrderType = new OrderType() { Title = "文具", OrderTypeId = 4 } });
            o.Add(new Order() { OrderNo = "A05", Title = "No.5", OrderType = new OrderType() { Title = "文具", OrderTypeId = 5 } });
            o.Add(new Order() { OrderNo = "A06", Title = "No.6", OrderType = new OrderType() { Title = "文具", OrderTypeId = 6 } });

            this.Orders = o;

            CMD_Refresh_Orders = new Command(() =>
            {
                int max = 0;
                if (this.Orders != null)
                {
                    max = this.Orders.Max(i => i.OrderType.OrderTypeId);
                }

                for (int i = 0; i < 3; i++)
                {
                    var toAdd = new Order()
                    {
                        OrderNo = $"A{(max + i).ToString().PadLeft(2, '0')}",
                        Title = $"No.{(max + i)}",
                        OrderType = new OrderType() { Title = "文具", OrderTypeId = (max + i) }
                    };

                    this.Orders.Add(toAdd);
                }

                IsRefresh = false;
            });

            CMD_ThresholdReached = new Command(()=> 
            {
                int max = 0;
                if (this.Orders != null)
                {
                    max = this.Orders.Max(i => i.OrderType.OrderTypeId);
                }

                if (max > 50)
                {
                    return;
                }

                for (int i = 0; i < 3; i++)
                {
                    var toAdd = new Order()
                    {
                        OrderNo = $"A{(max + i).ToString().PadLeft(2, '0')}",
                        Title = $"No.{(max + i)}",
                        OrderType = new OrderType() { Title = "文具", OrderTypeId = (max + i) }
                    };

                    this.Orders.Add(toAdd);
                }

                IsRefresh = false;
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
    }
}