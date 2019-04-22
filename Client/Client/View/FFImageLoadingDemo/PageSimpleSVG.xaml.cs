using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.FFImageLoadingDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSimpleSVG : ContentPage
    {
        PageSimpleSVG_ViewModel ViewModel { get; set; }

        public PageSimpleSVG()
        {
            InitializeComponent();
            this.ViewModel = new PageSimpleSVG_ViewModel();
            this.BindingContext = this.ViewModel;
            
            svg2.Source = ImageSource.FromResource("Client.Images.FFImageLoading.sample.svg");
        }
    }

    public class PageSimpleSVG_ViewModel : ViewModel.BaseViewModel
    {
        public ImageSource Source { get; set; } = ImageSource.FromResource("Client.Images.FFImageLoading.sample.svg");
    }
}