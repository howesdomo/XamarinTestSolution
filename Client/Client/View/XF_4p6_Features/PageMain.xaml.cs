using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XF_4p6_Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMain : ContentPage
    {
        public PageMain()
        {
            InitializeComponent();
        }
    }

    public class PageMain_4point6_Features_ViewModel : ViewModel.BaseViewModel
    {
        public Command CMD_Page_FontEmbedded { get; private set; }

        public Command CMD_Page_FontAwesome { get; private set; }

        public PageMain_4point6_Features_ViewModel()
        {
            this.CMD_Page_FontEmbedded = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new PageFontEmbedded());
            });

            this.CMD_Page_FontAwesome = new Command(()=> 
            {
                App.Current.MainPage.Navigation.PushAsync(new PageFontAwesome());
            });
        }
    }
}