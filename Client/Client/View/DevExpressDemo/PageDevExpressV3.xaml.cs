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
                }
            };
        }

        private void initData()
        {
            DevExpress.Mobile.Core.Containers.BindingList<DevExpressV3_DataModel> list = new DevExpress.Mobile.Core.Containers.BindingList<DevExpressV3_DataModel>();

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
        private DevExpress.Mobile.Core.Containers.BindingList<DevExpressV3_DataModel> _Orders;

        public DevExpress.Mobile.Core.Containers.BindingList<DevExpressV3_DataModel> Orders
        {
            get
            {
                return _Orders;
            }
            set
            {
                _Orders = value;
                this.OnPropertyChanged("Orders");
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