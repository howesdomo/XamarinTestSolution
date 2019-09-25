using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.EffectsDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLongPressEffects : ContentPage
    {
        PageLongPressEffects_ViewModel ViewModel { get; set; }

        public PageLongPressEffects()
        {
            this.ViewModel = new PageLongPressEffects_ViewModel();
            this.BindingContext = ViewModel;

            InitializeComponent();


            object o = lbl1.GetValue(Client.Effects.LongPressedEffect.CommandProperty);
            //if (o == null)
            //{
            //    lbl1.SetValue(Client.Effects.LongPressedEffect.CommandProperty, ShowAlertCommand);
            //}

            //o = lbl1.GetValue(Client.Effects.LongPressedEffect.CommandProperty);
            if (o != null && o is ICommand)
            {
                string msg = "sa";
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

    }

    public class PageLongPressEffects_ViewModel : ViewModel.BaseViewModel
    {
        public PageLongPressEffects_ViewModel()
        {
            this.ShowAlertCommand = new Command(execute: () =>
            {
                string msg = "long press!!!!";
                System.Diagnostics.Debug.WriteLine(msg);

                System.Diagnostics.Debugger.Break();
            });
        }

        public ICommand ShowAlertCommand { get; private set; }

    }
}