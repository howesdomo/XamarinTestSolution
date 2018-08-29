using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.Games.CRW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UcQuestion : ContentView
    {
        public UcQuestion()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            this.lbl.Margin = new Thickness(left: 0d, top: 0d, right: 0d, bottom: 0d);
        }
    }
}