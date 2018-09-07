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

            this.ViewModel.DeadLine = DateTime.Now;
            this.ViewModel.DeadLine_Time = new TimeSpan
            (
                days: 0,
                hours: this.ViewModel.DeadLine.Hour,
                minutes: this.ViewModel.DeadLine.Minute,
                seconds: this.ViewModel.DeadLine.Second,
                milliseconds: this.ViewModel.DeadLine.Millisecond
            );

            this.BindingContext = this.ViewModel;

            // 知识点 1
            // NavigationPage返回按钮显示设置
            // XAML 代码设置
            // NavigationPage.HasBackButton="False"

            // C# 代码设置
            // NavigationPage.SetHasBackButton(this, false);

            // 知识点 2
            // 有关 NavigationPage Back 的控制
            // 若使用默认的 toolbar, 无法使用 OnBackButtonPressed 对软 Back 按钮进行
            // 安卓版本在 MainActivity. OnCreate() 中设置
            // Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            // SetSupportActionBar(toolbar);
            // 设置 V7 后, OnBackButtonPressed(), 可以监控物理back和软Back按钮都能实现监控


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string msg = "{0}".FormatWith("PageMain OnAppearing");
            System.Diagnostics.Debug.WriteLine(msg);

            Device.BeginInvokeOnMainThread(() =>
            {
                App.ScreenDirection.ForcePortrait();
            });
        }

        protected override void OnDisappearing()
        {
            string msg = "{0}".FormatWith("PageMain OnDisappearing");
            System.Diagnostics.Debug.WriteLine(msg);

            Device.BeginInvokeOnMainThread(() =>
            {
                App.ScreenDirection.Unspecified();
            });

            base.OnDisappearing();
        }


        /// <summary>
        /// 监控安卓物理返回键
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // 由以下3个 showCloseDisplayAlert 得出
            // 最简单的写法 showCloseDisplayAlert
            // 需要注意 showCloseDisplayAlertV1 无法运行的原因
            showCloseDisplayAlert();
            return true;
        }

        /// <summary>
        /// 测试到可用方法
        /// </summary>
        async void showCloseDisplayAlert()
        {
            var result = await this.DisplayAlert
            (
                title: "提示",
                message: "确认退出？",
                accept: "确认",
                cancel: "取消"
            );

            if (result)
            {
                await Navigation.PopAsync(true);
            }
        }

        /// <summary>
        /// V1 会卡住, 无法关闭
        /// </summary>
        private void showCloseDisplayAlertV1()
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

        /// <summary>
        /// 修复V1无法关闭的问题
        /// </summary>
        private void showCloseDisplayAlertV1_Fix()
        {
            Task<bool> task = DisplayAlert("提示", "确认返回？", "确认", "取消");
            task.ContinueWith((r) =>
            {
                if (r.Result == false)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync(true);
                });
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
                DateTime tmp = value;
                _DeadLine = tmp.Add(this.DeadLine_Time);
                this.OnPropertyChanged("DeadLine");

                this.OnPropertyChanged("DeadLineDisplay");
                this.OnPropertyChanged("DeadLine_TimeDisplay");
            }
        }

        private TimeSpan _DeadLine_Time;
        public TimeSpan DeadLine_Time
        {
            get { return _DeadLine_Time; }
            set
            {
                _DeadLine_Time = value;
                this.OnPropertyChanged("DeadLine_Time");

                this.DeadLine = this.DeadLine.Date.Add(DeadLine_Time);
                this.OnPropertyChanged("DeadLineDisplay");
                this.OnPropertyChanged("DeadLine_TimeDisplay");

            }
        }

        public string DeadLineDisplay
        {
            get
            {
                return this.DeadLine.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
        }

        public string DeadLine_TimeDisplay
        {
            get
            {
                return this.DeadLine.ToString("HH:mm:ss.fff");
            }
        }


    }
}