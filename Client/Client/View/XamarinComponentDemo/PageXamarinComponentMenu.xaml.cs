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
    public partial class PageXamarinComponentMenu : ContentPage
    {
        public PageXamarinComponentMenu()
        {
            InitializeComponent();
            this.BindingContext = new PageXamarinComponentMenu_ViewModel();
        }
    }

    public class PageXamarinComponentMenu_ViewModel : ViewModel.BaseViewModel
    {
        public PageXamarinComponentMenu_ViewModel()
        {
            BtnPageLabel_Command = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new PageLabel());
            });

            BtnPageEntry_Command = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new PageEntry());
            });

            BtnPageStepper_Command = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new PageStepper());
            });

            BtnPageSearchBar_Command = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new PageSearchBar());
            });

            BtnPageFilter_Command = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new PageFilterBar());
            });

            BtnWebViewAdv_Command = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new PageWebViewAdv());
            });
        }

        public Command BtnPageLabel_Command { get; set; }

        public Command BtnPageEntry_Command { get; set; }

        public Command BtnPageStepper_Command { get; set; }

        public Command BtnPageSearchBar_Command { get; set; }

        public Command BtnPageFilter_Command { get; set; }

        public Command BtnWebViewAdv_Command { get; set; }

    }
}