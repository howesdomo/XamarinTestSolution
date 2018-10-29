using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.ShuangSeQiu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageShuangSeQiuExport : ContentPage
    {
        public PageShuangSeQiuExport()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            this.img.Source = ImageSource.FromResource("Client.Images.ShuangSeQiu.TheGodofFortune.png");
        }
    }

    public class PageShuangSeQiuExport_ViewModel
    {
        public string RedInfo { get; set; }

        public string BlueInfo { get; set; }
    }
}