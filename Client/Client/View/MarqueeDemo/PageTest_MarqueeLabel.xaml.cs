using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.MarqueeDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTest_MarqueeLabel : ContentPage
    {
        public PageTest_MarqueeLabel()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}