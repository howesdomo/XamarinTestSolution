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
        public Command<string> CMD_Filter { get; set; }

        public PageFontAwesome_ViewModel()
        {
            this.CMD_Filter = new Command<string>((args) =>
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
            });

            AllList = Util_Font.FontAwesomeModel.GetAllList();
            FiltedList = new List<Util_Font.FontAwesomeModel>();
            FiltedList.AddRange(AllList);
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
    }
}