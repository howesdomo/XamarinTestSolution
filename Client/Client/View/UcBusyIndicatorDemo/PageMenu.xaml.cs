using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.UcBusyIndicatorDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMenu : ContentPage
    {
        public PageMenu()
        {
            InitializeComponent();
        }
    }
}

namespace Client.UcBusyIndicatorDemo.ViewModels
{
    public class PageMenu_ViewModel : ViewModel.BaseViewModel
    {
        public PageMenu_ViewModel()
        {
            initCMD();
        }

        void initCMD()
        {
            this.CMD_Demo1 = new Command(Demo1);
            this.CMD_Demo2 = new Command(Demo2);
            this.CMD_Demo3 = new Command(Demo3);

        }

        public Command CMD_Demo1 { get; private set; }
        async void Demo1(object objPage)
        {
            if (objPage is Page p)
            {
                await p.Navigation.PushAsync(new Client.View.UcBusyIndicatorDemo.PageDemo1());
            }
        }

        public Command CMD_Demo2 { get; private set; }
        async void Demo2(object objPage)
        {
            if (objPage is Page p)
            {
                await p.Navigation.PushAsync(new Client.View.UcBusyIndicatorDemo.PageDemo2());
            }
        }

        public Command CMD_Demo3 { get; private set; }
        async void Demo3(object objPage)
        {
            if (objPage is Page p)
            {
                await p.Navigation.PushAsync(new Client.View.UcBusyIndicatorDemo.PageDemo3());
            }
        }


    }
}