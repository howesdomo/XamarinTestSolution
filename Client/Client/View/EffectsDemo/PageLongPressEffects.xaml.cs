using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.EffectsDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLongPressEffects : ContentPage
    {
        PageLongPressEffects_ViewModel ViewModel { get; set; }

        public PageLongPressEffects()
        {
            InitializeComponent();
            this.ViewModel = new PageLongPressEffects_ViewModel();
            this.BindingContext = ViewModel;

            // 检测 XAML 中绑定 Command 是否成功
            object o = lbl1.GetValue(Client.Effects.LongPressedEffect.CommandProperty);
            if (o != null && o is ICommand)
            {
                string msg = "sab";
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

    }

    public class PageLongPressEffects_ViewModel : ViewModel.BaseViewModel
    {
        public PageLongPressEffects_ViewModel()
        {
            this.ShowAlertCommand = new Command(execute: (args) =>
            {
                string msg = $"!!!long press!!!!\r\n{Util.JsonUtils.SerializeObject(args)}";
                System.Diagnostics.Debug.WriteLine(msg);
            });

            this.ItemLongPressCommand = new Command(execute: (args) =>
            {
                string msg = $"!!!item long press!!!!\r\n{Util.JsonUtils.SerializeObject(args)}";
                System.Diagnostics.Debug.WriteLine(msg);

                System.Diagnostics.Debugger.Break();

            });

            this.OrderList = new List<CellModel>()
            {
                new CellModel(){ OrderNo = "1" },
                new CellModel(){ OrderNo = "2" } ,
                new CellModel(){ OrderNo = "3" },
                new CellModel(){ OrderNo = "4" },
                new CellModel(){ OrderNo = "5" }
            };
        }

        public ICommand ShowAlertCommand { get; private set; }
        public ICommand ItemLongPressCommand { get; private set; }

        private List<CellModel> _OrderList;
        public List<CellModel> OrderList
        {
            get
            {
                return _OrderList;
            }
            set
            {
                _OrderList = value;
                this.OnPropertyChanged(nameof(OrderList));
            }
        }
    }

    public class CellModel : ViewModel.BaseViewModel
    {
        public CellModel()
        {
            this.ItemLongPressCommand = new Command(execute: (args) =>
            {
                string msg = $"!!!item long press!!!!\r\n{Util.JsonUtils.SerializeObject(args)}";
                System.Diagnostics.Debug.WriteLine(msg);
            });
        }

        public ICommand ItemLongPressCommand { get; private set; }

        private string _OrderNo;
        public string OrderNo
        {
            get { return _OrderNo; }
            set
            {
                _OrderNo = value;
                this.OnPropertyChanged(nameof(OrderNo));
            }
        }
    }


}