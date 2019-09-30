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
    public partial class PageStepper : ContentPage
    {
        public PageStepper()
        {
            InitializeComponent();
            this.BindingContext = new PageStepper_ViewModel();
        }
    }

    // TODO 脚本
    public class PageStepper_ViewModel : ViewModel.BaseViewModel
    {
        public PageStepper_ViewModel()
        {
            CMD_ShowStepperValue = new Command(async (args) =>
            {
                if (args is string)
                {
                    string query = args.ToString();
                    
                    // TODO ViewModel 脚本
                    await App.Current.MainPage.DisplayAlert("输出结果", $"{query}", "确定");
                }
            });
        }

        public Command CMD_ShowStepperValue { get; set; }
    }
}