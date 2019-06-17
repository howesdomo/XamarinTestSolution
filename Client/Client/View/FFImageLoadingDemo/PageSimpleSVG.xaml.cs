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

            // SVG资源放置到 Client 中 无法正常读取
            svg3.Source = ImageSource.FromResource("Client.Images.FFImageLoading.sample.svg");

            // SVG资源放置到 Client.iOS 中能正常读取
            string svgFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", "FFImageLoading", "sample.svg");
            if (System.IO.File.Exists(svgFile) == true)
            {
                svg5.Source = ImageSource.FromFile(svgFile);
            }

            // svg6.Source = ImageSource.FromUri(new Uri(@"http://s.cdpn.io/3/kiwi22.svg"));
            svg6.LoadingPlaceholder = ImageSource.FromResource("Client.Images.FFImageLoading.loading.png");
            svg6.ErrorPlaceholder = ImageSource.FromResource("Client.Images.FFImageLoading.error.png");            
        }
    }

    public class PageSimpleSVG_ViewModel : ViewModel.BaseViewModel
    {
        public ImageSource Source { get; set; } = ImageSource.FromResource("Client.Images.FFImageLoading.sample.svg");
    }
}