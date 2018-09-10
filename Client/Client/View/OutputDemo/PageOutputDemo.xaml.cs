using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageOutputDemo : ContentPage
    {
        public static string Tag = "PageOutputDemo";

        public PageOutputDemo()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnInfo.Clicked += BtnInfo_Clicked;
            this.btnError.Clicked += BtnError_Clicked;
            this.btnWarn.Clicked += BtnWarn_Clicked;
        }

        private void BtnInfo_Clicked(object sender, EventArgs e)
        {
            App.Output.Info(Tag, "Click Info Button");
        }

        private void BtnError_Clicked(object sender, EventArgs e)
        {
            App.Output.Error(Tag, "Click Error Button");
        }

        private void BtnWarn_Clicked(object sender, EventArgs e)
        {
            App.Output.Warn(Tag, "Click Warn Button");
        }

    }
}