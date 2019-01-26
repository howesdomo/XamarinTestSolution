using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Client.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZXingBarcodeScanner : ContentPage
    {
        /// <summary>
        /// 持续扫描
        /// </summary>
        private bool mIsScanContinuously { get; set; }

        bool mIsBusy { get; set; }

        ZXingScannerView mZXingScannerView { get; set; }

        ZXingOverlay mZXingOverlay { get; set; }

        public ZXingBarcodeScanner
        (
            string title = "",
            bool isScanContinuously = false,
            Action<ZXing.Result, ZXingBarcodeScanner> scanResultHandle = null
        ) : base()
        {
            if (title.IsNullOrWhiteSpace())
            {
                this.Title = "扫描条码";
            }
            else
            {
                this.Title = title;
            }

            myScanResultHandle = scanResultHandle;

            mIsScanContinuously = isScanContinuously;
            initComponent();
            initEvent();
        }

        private void initComponent()
        {
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            mZXingScannerView = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            grid.Children.Add(mZXingScannerView);


            mZXingOverlay = new ZXingOverlay
            {
                TopText = "请对准条码",
                BottomText = "光线不足请打开闪光灯",
                ShowFlashButton = true,
                ButtonText = "开灯"
            };
            grid.Children.Add(mZXingOverlay);

            Content = grid;
        }


        Action<ZXing.Result, ZXingBarcodeScanner> myScanResultHandle;

        private void initEvent()
        {
            mZXingScannerView.OnScanResult += mZXingScannerView_OnScanResult;
            mZXingOverlay.FlashButtonClicked += mZXingOverlay_FlashButtonClicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mZXingScannerView.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            mZXingScannerView.IsScanning = false;
            mZXingScannerView.IsTorchOn = false;

            base.OnDisappearing();
        }

        /// <summary>
        /// 处理扫描结果
        /// </summary>
        /// <param name="result"></param>
        private void mZXingScannerView_OnScanResult(ZXing.Result result)
        {
            if (mIsBusy == false)
            {
                mIsBusy = true;
            }
            else
            {
                return;
            }

            if (myScanResultHandle != null) // 程序员自定义处理
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //var myCallBack = new AsyncCallback((ar) =>
                    //{
                    //    endWith();
                    //});
                    //var iAsyncResult = myScanResultHandle.BeginInvoke(result, myCallBack, null);
                    //myScanResultHandle.EndInvoke(iAsyncResult);

                    //// TODO 想做到 执行完 myScanResultHandle 后才运行endWith
                    //endWith();


                    myScanResultHandle.Invoke(result, this);
                });
            }
            else // 默认处理
            {
                defaultScanResultHandler(result);
            }
        }

        void defaultScanResultHandler(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string msg = "扫描内容(Text)\r\n{0}\r\n\r\n条码格式(BarcodeFormat)\r\n{1}".FormatWith(result.Text, result.BarcodeFormat);
                System.Diagnostics.Debug.WriteLine(msg);

                try
                {
                    // ** 引用 Xamarin.Essentials 包 **
                    // 将内容复制到剪贴板
                    // Xamarin.Essentials.Clipboard.SetText(result.Text);
                    await Xamarin.Essentials.Clipboard.SetTextAsync(result.Text);

                    msg += "\r\n扫描内容已复制到粘贴板";
                }
                catch (Exception ex)
                {
                    // 已查明 复制内容到剪贴板报错是由于 android 6.0 以后的动态权限导致的
                    // 改用 targetSdkVersion 为 21, 避开 android 6.0 动态权限后, 原本报错的设备正常运行

                    string msg2 = "{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg2);

                    msg += "\r\n扫描内容复制到粘贴板时发生错误\r\n{0}".FormatWith(ex.GetFullInfo());
                }

                await DisplayAlert("扫描内容：", msg, "确定");

                endWith();
            });
        }

        public async void endWith()
        {
            if (mIsScanContinuously == true)
            {
                mIsBusy = false;
            }
            else
            {
                mZXingScannerView.IsAnalyzing = false;
                mZXingScannerView.IsScanning = false;

                try
                {
                    mZXingScannerView.IsTorchOn = false;
                }
                catch (Exception ex)
                {
                    string msgEx = "执行 MZXingScannerView_OnScanResult 发生了错误。错误详情\r\n{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msgEx);
                }

                await Navigation.PopAsync();
            }
        }

        /// <summary>
        /// 控制闪光灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mZXingOverlay_FlashButtonClicked(Button sender, EventArgs e)
        {
            try
            {
                if (mZXingScannerView.IsTorchOn == true)
                {
                    sender.Text = "开灯";
                    mZXingScannerView.IsTorchOn = false;
                }
                else
                {
                    sender.Text = "关灯";
                    mZXingScannerView.IsTorchOn = true;
                }
            }
            catch (Exception ex)
            {
                string msg = "执行 FlashButtonClicked 发生了错误。错误详情\r\n{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }

    /// <summary>
    /// 扫描界面
    /// </summary>
    public class ZXingOverlay : Grid
    {
        Label topText;
        Label botText;
        Button flash;
        int weizhi = -120;
        public delegate void FlashButtonClickedDelegate(Button sender, EventArgs e);
        public event FlashButtonClickedDelegate FlashButtonClicked;

        public ZXingOverlay()
        {
            BindingContext = this;
            //ColumnSpacing = 0;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;

            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            var boxview = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,

            };
            var boxview2 = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,

            };
            Children.Add(boxview, 0, 0);
            Children.Add(boxview2, 0, 2);

            SetColumnSpan(boxview, 5);
            SetColumnSpan(boxview2, 5);
            // Children.Add(boxview, 0, 3);
            Children.Add(new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
            }, 0, 1);
            Children.Add(new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
            }, 4, 1);
            //Children.Add(new BoxView
            //{
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    BackgroundColor = Color.Black,
            //    Opacity = 0.7,
            //}, 0, 3);
            //Children.Add(new BoxView
            //{
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    BackgroundColor = Color.Black,
            //    Opacity = 0.7,
            //}, 0, 2);
            var AbsoluteLayouts = new AbsoluteLayout();

            var redline = new Image
            {
                Source = "saomiao.png"
            };
            AbsoluteLayout.SetLayoutBounds(redline, new Rectangle(1, weizhi, 1, 1));
            AbsoluteLayout.SetLayoutFlags(redline, AbsoluteLayoutFlags.SizeProportional);
            AbsoluteLayouts.Children.Add(redline);
            Children.Add(AbsoluteLayouts, 1, 1);
            SetColumnSpan(AbsoluteLayouts, 3);
            topText = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.White,
                AutomationId = "zxingDefaultOverlay_TopTextLabel",
            };
            topText.SetBinding(Label.TextProperty, new Binding(nameof(TopText)));
            Children.Add(topText, 0, 0);
            SetColumnSpan(topText, 5);
            botText = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.White,
                AutomationId = "zxingDefaultOverlay_BottomTextLabel",
            };
            botText.SetBinding(Label.TextProperty, new Binding(nameof(BottomText)));
            //Children.Add(botText, 0, 2);
            //SetColumnSpan(botText, 5);
            var MyStackLayout = new StackLayout
            {

                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            flash = new Button
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                //HeightRequest = 3,
                Text = "按钮",
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
                AutomationId = "zxingDefaultOverlay_FlashButton",
            };
            flash.SetBinding(Button.IsVisibleProperty, new Binding(nameof(ShowFlashButton)));
            flash.SetBinding(Button.TextProperty, new Binding(nameof(ButtonText)));
            flash.Clicked += (sender, e) =>
            {
                FlashButtonClicked?.Invoke(flash, e);
            };
            MyStackLayout.Children.Add(botText);
            MyStackLayout.Children.Add(flash);
            Children.Add(MyStackLayout, 0, 2);
            SetColumnSpan(MyStackLayout, 5);
            //this.ColumnSpacing = 0;
            this.RowSpacing = 0;
            Device.StartTimer(TimeSpan.FromSeconds(0.2), () =>
            {
                weizhi += 7;
                AbsoluteLayout.SetLayoutBounds(redline, new Rectangle(1, weizhi, 1, 1));
                if (weizhi > 150)
                {
                    weizhi = -100;
                }
                return true;
            });
        }

        public static readonly BindableProperty TopTextProperty =
            BindableProperty.Create(nameof(TopText), typeof(string), typeof(ZXingOverlay), string.Empty);
        public string TopText
        {
            get { return (string)GetValue(TopTextProperty); }
            set { SetValue(TopTextProperty, value); }
        }

        public static readonly BindableProperty BottomTextProperty =
            BindableProperty.Create(nameof(BottomText), typeof(string), typeof(ZXingOverlay), string.Empty);
        public string BottomText
        {
            get { return (string)GetValue(BottomTextProperty); }
            set { SetValue(BottomTextProperty, value); }
        }

        public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(ZXingOverlay), string.Empty);
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        public static readonly BindableProperty ShowFlashButtonProperty =
            BindableProperty.Create(nameof(ShowFlashButton), typeof(bool), typeof(ZXingOverlay), false);
        public bool ShowFlashButton
        {
            get { return (bool)GetValue(ShowFlashButtonProperty); }
            set { SetValue(ShowFlashButtonProperty, value); }
        }

        public static BindableProperty FlashCommandProperty =
            BindableProperty.Create(nameof(FlashCommand), typeof(System.Windows.Input.ICommand), typeof(ZXingOverlay),
                defaultValue: default(System.Windows.Input.ICommand),
                propertyChanged: OnFlashCommandChanged);
        public System.Windows.Input.ICommand FlashCommand
        {
            get { return (System.Windows.Input.ICommand)GetValue(FlashCommandProperty); }
            set { SetValue(FlashCommandProperty, value); }
        }

        private static void OnFlashCommandChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var overlay = bindable as ZXingOverlay;
            if (overlay?.flash == null) return;
            overlay.flash.Command = newValue as Command;
        }
    }
}