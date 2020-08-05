using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XF_4p3_Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMain : ContentPage
    {
        public PageMain()
        {
            InitializeComponent();
        }
    }

    public class PageMain_4point3_Features_ViewModel : ViewModel.BaseViewModel
    {
        public Command CMD_Page_CollectionView { get; private set; }

        public PageMain_4point3_Features_ViewModel()
        {
            this.CMD_Page_CollectionView = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new PageCollectionView());
            });
        }
    }
}