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
        // CRW_Question ViewModel = new CRW_Question();

        public UcQuestion()
        {
            InitializeComponent();
            // initUI();
        }

        //private void initUI()
        //{

        //    var a = new ColumnDefinitionCollection();
        //    a.Add(new ColumnDefinition() { Width = 80 });
        //    a.Add(new ColumnDefinition() { Width = GridLength.Star });

        //}

        //public void bindViewModel(CRW_Question args)
        //{
        //    this.ViewModel = args;
        //    this.BindingContext = this.ViewModel;
        //}
    }
}