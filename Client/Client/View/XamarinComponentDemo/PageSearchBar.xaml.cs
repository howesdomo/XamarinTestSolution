using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinComponentDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSearchBar : ContentPage
    {
        public PageSearchBar()
        {
            InitializeComponent();
            this.BindingContext = new PageSearchBar_ViewModel();

            //// 先触发 Command  再触发 SearchButtonPressed
            //sb1.SearchButtonPressed += (s, e) =>
            //{
            //    if (sb1.Text.IsNullOrWhiteSpace())
            //    {
            //        sb1.SearchCommand.Execute(string.Empty);
            //    }
            //};            
        }

        private void Txt1_Completed(object sender, EventArgs e)
        {           
            string msg = $"COmplete";
            System.Diagnostics.Debug.WriteLine(msg);

            System.Diagnostics.Debugger.Break();

        }
    }

    public class PageSearchBar_ViewModel : ViewModel.BaseViewModel
    {
        public PageSearchBar_ViewModel()
        {
            this.Results = new List<string>()
            {
                "1",
                "1",
                "1",
                "4",
                "5",
                "6",
                "7",
            };

            this.SearchResults = this.Results;
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            if (query.IsNullOrWhiteSpace()) // 输入框没有值时, 不会触发 SearchCommand, 故不会进入这段代码
            {
                SearchResults = Results;
            }
            else
            {
                SearchResults = Results.Where(i => System.Text.RegularExpressions.Regex.IsMatch(input: i, pattern: $"{query}")).ToList();
            }
        });

        private List<string> _Results;
        public List<string> Results
        {
            get
            {
                return _Results;
            }
            set
            {
                _Results = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> searchResults;
        public List<string> SearchResults
        {
            get
            {
                return searchResults;
            }
            set
            {
                searchResults = value;
                NotifyPropertyChanged();
            }
        }
    }
}