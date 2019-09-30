using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinComponentDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEntry : ContentPage
    {
        public PageEntry()
        {
            InitializeComponent();
            this.BindingContext = new PageEntry_ViewModel();
        }
    }

    public class PageEntry_ViewModel : ViewModel.BaseViewModel
    {
        public PageEntry_ViewModel()
        {
            this.BtnShowIDCode_Command = new Command(async () =>
            {
                await App.Current.MainPage.DisplayAlert("身份证号码", this.IDCode, "确定");
            });
        }

        public Command BtnShowIDCode_Command { get; set; }

        private string _IDCode = "";
        public string IDCode
        {
            get => _IDCode;
            set
            {
                _IDCode = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}