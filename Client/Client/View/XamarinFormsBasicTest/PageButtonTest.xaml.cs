using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinFormsBasicTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageButtonTest : ContentPage
    {
        public PageButtonTest()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Tap On UI");
        }
    }
}