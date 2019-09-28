using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UcSelectFile : ContentView
    {
        public UcSelectFile()
        {
            InitializeComponent();
        }
    }

    public class UcSelectFile_ViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        #region INotifyPropertyChanged成员

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void A()
        {
        }

    }
}