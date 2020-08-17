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
            initXAMLResources();

            this.BindingContextChanged += bindingContextChanged;
        }

        private void bindingContextChanged(object sender, EventArgs e)
        {
            initXAMLResources();
        }

        void initXAMLResources()
        {
            if (this.BindingContext != null)
            {
                (this.BindingContext as PageFontAwesome_ViewModel).initXAMLResources
                (
                    (IItemsLayout)this.Resources["ItemLayoutOfList"],
                    (IItemsLayout)this.Resources["ItemLayoutOfGrid"],

                    (DataTemplate)this.Resources["DataTemplateOfList"],
                    (DataTemplate)this.Resources["DataTemplateOfGrid"]
                );
            }
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

        public void initXAMLResources(IItemsLayout a, IItemsLayout b, DataTemplate aa, DataTemplate bb)
        {
            if (this.ItemLayoutOfList == null)
            {
                this.ItemLayoutOfList = a;
                this.ItemsLayout = this.ItemLayoutOfList;
            }

            if (this.ItemLayoutOfGrid == null)
            {
                this.ItemLayoutOfGrid = b;
            }

            if (this.DataTemplateOfList == null)
            {
                this.DataTemplateOfList = aa;
                this.ItemTemplate = this.DataTemplateOfList;
            }

            if (this.DataTemplateOfGrid == null)
            {
                this.DataTemplateOfGrid = bb;
            }
        }

        void initCommand()
        {
            this.CMD_Filter = new Command<string>((args) => filter(args));
            this.CMD_ViewOfList = new Command(viewOfList);
            this.CMD_ViewOfGrid = new Command(viewOfGrid);
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
                    Device.BeginInvokeOnMainThread(async () =>
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

        private IItemsLayout _ItemsLayout;
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

        public IItemsLayout ItemLayoutOfList { get; private set; }

        public DataTemplate DataTemplateOfList { get; private set; }

        public Command CMD_ViewOfList { get; private set; }

        void viewOfList()
        {
            this.ItemsLayout = ItemLayoutOfList;
            this.ItemTemplate = DataTemplateOfList;
        }

        #endregion

        #region 视图 - 图标

        public IItemsLayout ItemLayoutOfGrid { get; private set; }

        public DataTemplate DataTemplateOfGrid { get; private set; }

        public Command CMD_ViewOfGrid { get; private set; }

        void viewOfGrid()
        {
            this.ItemsLayout = ItemLayoutOfGrid;
            this.ItemTemplate = DataTemplateOfGrid;
        }

        #endregion

        #endregion
    }
}