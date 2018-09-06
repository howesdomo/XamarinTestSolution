using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.PageDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageB : ContentPage
    {
        public PageB_ViewModel ViewModel { get; set; }

        public PageB()
        {
            InitializeComponent();
            this.ViewModel = new PageB_ViewModel();
            this.BindingContext = this.ViewModel;
        }

        /// <summary>
        /// 监控安卓物理返回键
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            bool baseResult = base.OnBackButtonPressed();
            // 由程序来控制
            showCloseDisplayAlert();
            return true;
        }

        private void showCloseDisplayAlert()
        {
            Task<bool> task = DisplayAlert("提示", "确认返回？", "确认", "取消");
            task.ContinueWith(async (r) =>
            {
                if (r.Result == false)
                {
                    return;
                }
                await Navigation.PopAsync(true);
            });
        }
    }

    public class PageB_ViewModel : ViewModel.BaseViewModel
    {
        private string _OrderNo;
        public string OrderNo
        {
            get { return _OrderNo; }
            set
            {
                _OrderNo = value;
                this.OnPropertyChanged("OrderNo");
            }
        }


        private int _Qty;
        public int Qty
        {
            get { return _Qty; }
            set
            {
                _Qty = value;
                this.OnPropertyChanged("Qty");
            }
        }

        private decimal _Price;
        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                this.OnPropertyChanged("Price");
            }
        }

        private DateTime _DeadLine;
        public DateTime DeadLine
        {
            get { return _DeadLine; }
            set
            {
                _DeadLine = value;
                this.OnPropertyChanged("DeadLine");
            }
        }
    }
}