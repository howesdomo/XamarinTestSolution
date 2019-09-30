using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinComponentDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageFilterBar : ContentPage
    {
        public PageFilterBar()
        {
            InitializeComponent();
            this.BindingContext = new PageFilterBarViewModel();

            // 测试注册事件 正常
            //fb1.Search += new EventHandler<Components.FilterBarEventArgs>((s, e) =>
            //{
            //    string msg = $"Search -- query:{e.Query}";
            //    System.Diagnostics.Debug.WriteLine(msg);
            //});
        }
    }

    public class PageFilterBarViewModel : ViewModel.BaseViewModel
    {
        public PageFilterBarViewModel()
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

            this.SearchCommand = new Command((args) =>
            {
                if (args is string)
                {
                    string query = args as string;
                    if (query.IsNullOrWhiteSpace()) // 输入框没有值时, 不会触发 SearchCommand, 故不会进入这段代码
                    {
                        SearchResults = Results;
                    }
                    else
                    {
                        SearchResults = Results.Where(i => System.Text.RegularExpressions.Regex.IsMatch(input: i, pattern: $"{query}")).ToList();
                    }
                }
                else 
                {
                    string msg = $"{args}";
                    System.Diagnostics.Debug.WriteLine(msg);

                    System.Diagnostics.Debugger.Break();

                }
            });
        }

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


        private Command _SearchCommand;

        public Command SearchCommand
        {
            get { return _SearchCommand; }
            set
            {
                _SearchCommand = value;
                this.OnPropertyChanged(nameof(SearchCommand));
            }
        }

    }
}