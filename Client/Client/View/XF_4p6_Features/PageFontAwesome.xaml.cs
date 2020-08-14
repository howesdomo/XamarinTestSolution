using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XF_4p6_Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageFontAwesome : ContentPage
    {
        public PageFontAwesome()
        {
            InitializeComponent();
        }
    }

    public class PageFontAwesome_ViewModel : ViewModel.BaseViewModel
    {       
        public PageFontAwesome_ViewModel()
        {


            initCommand();

            AllList = Util_Font.FontAwesomeModel.GetAllList();
            FiltedList = new List<Util_Font.FontAwesomeModel>();
            FiltedList.AddRange(AllList);
        }

        void initCommand()
        {
            this.CMD_Filter = new Command<string>((args) => filter(args));
            this.CMD_ViewOfList = new Command<DataTemplate>(viewOfList);
            this.CMD_ViewOfGrid = new Command<DataTemplate>(viewOfGrid);
        }

        public List<Util_Font.FontAwesomeModel> _FiltedList;
        public List<Util_Font.FontAwesomeModel> FiltedList
        {
            get { return _FiltedList; }
            set
            {
                SetProperty(ref _FiltedList, value);
            }
        }


        public List<Util_Font.FontAwesomeModel> _AllList;
        public List<Util_Font.FontAwesomeModel> AllList
        {
            get { return _AllList; }
            set
            {
                SetProperty(ref _AllList, value);
            }
        }


        public Command<string> CMD_Filter { get; set; }

        void filter(string args)
        {
            FiltedList.Clear();

            string inputValue = args;

            if (inputValue.IsNullOrWhiteSpace() == false)
            {
                FiltedList = this.AllList.Where(i => new Regex(inputValue, RegexOptions.IgnoreCase).IsMatch(i.Name) == true)
                                         .ToList();
            }
            else
            {
                FiltedList = this.AllList.ToList();
            }
        }


        private Util_Font.FontAwesomeModel _SelectedItem;
        public Util_Font.FontAwesomeModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                this.OnPropertyChanged();

                if (value != null && this.ItemsLayout == ItemLayoutOfGrid)
                {
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast(value.Name);
                        try
                        {
                            await Xamarin.Essentials.Clipboard.SetTextAsync(value.Name);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.GetInfo());
                            System.Diagnostics.Debugger.Break();
                        }
                    });
                }
            }
        }


        #region 视图

        private IItemsLayout _ItemsLayout = ItemLayoutOfList;
        public IItemsLayout ItemsLayout
        {
            get { return _ItemsLayout; }
            set
            {
                _ItemsLayout = value;
                this.OnPropertyChanged();
            }
        }

        private DataTemplate _ItemTemplate;
        public DataTemplate ItemTemplate
        {
            get { return _ItemTemplate; }
            set
            {
                _ItemTemplate = value;
                this.OnPropertyChanged("ItemTemplate");
            }
        }

        #region 视图 - 详情

        private static readonly IItemsLayout ItemLayoutOfList = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
        {
            ItemSpacing = 5
        };

        public Command<DataTemplate> CMD_ViewOfGrid { get; private set; }

        void viewOfList(DataTemplate dt)
        {
            this.ItemsLayout = ItemLayoutOfList;
            this.ItemTemplate = dt;
        }

        #endregion

        #region 视图 - 图标

        private static readonly IItemsLayout ItemLayoutOfGrid = new GridItemsLayout(ItemsLayoutOrientation.Vertical)
        {
            Span = 4,
            VerticalItemSpacing = 20
        };

        public Command<DataTemplate> CMD_ViewOfList { get; private set; }

        void viewOfGrid(DataTemplate dt)
        {
            this.ItemsLayout = ItemLayoutOfGrid;
            this.ItemTemplate = dt;
        }

        #endregion

        #endregion
    }
}