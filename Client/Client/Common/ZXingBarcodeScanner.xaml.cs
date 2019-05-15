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
                // LabelTopText = "请对准条码",

                ShowFlashButton = false,
                LabelScanTipsText = "将二维码/条码放入框内, 即可自动扫描", // TODO 通过参数区分 二维码 / 条码
                ButtonFlashText = "闪光灯开关"
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

                EndWith();
            });
        }

        public async void EndWith()
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
        Label txtTop;
        Label txtScanTipsText;
        Button btnFlash;

        int scanLineY = -120;
        bool scanLineOpacityDirection = false;

        public delegate void FlashButtonClickedDelegate(Button sender, EventArgs e);

        public event FlashButtonClickedDelegate FlashButtonClicked;

        public ZXingOverlay()
        {
            BindingContext = this;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;

            // RowDefinitions - 3行
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // *
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }); // 2*
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) }); // 3*

            // ColumnDefinitions - 5列
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *

            this.RowSpacing = 0;

            #region BoxView - Black

            var bv_Top_Black = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
            };

            var bv_Bottom_Black = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,

            };

            Children.Add(bv_Top_Black, 0, 0);
            Children.Add(bv_Bottom_Black, 0, 2);

            SetColumnSpan(bv_Top_Black, 5);
            SetColumnSpan(bv_Bottom_Black, 5);

            var bv_Left_Black = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
            };
            Children.Add(bv_Left_Black, 0, 1);

            var bv_Right_Black = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
            };
            Children.Add(bv_Right_Black, 4, 1);

            #endregion

            #region ScanArea

            var slScanArea = new AbsoluteLayout();

            var scanLine = new Image
            {
                Source = ImageSource.FromResource("Client.Images.BaseView.scanLine.png")
            };

            AbsoluteLayout.SetLayoutBounds(scanLine, new Rectangle(1, scanLineY, 1, 1));
            AbsoluteLayout.SetLayoutFlags(scanLine, AbsoluteLayoutFlags.SizeProportional);
            slScanArea.Children.Add(scanLine);
            Children.Add(slScanArea, 1, 1);
            SetColumnSpan(slScanArea, 3);

            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                #region 上下移动效果

                //scanLineY += 7;
                //AbsoluteLayout.SetLayoutBounds(scanLine, new Rectangle(1, scanLineY, 1, 1));
                //if (scanLineY > 150)
                //{
                //    scanLineY = -100;
                //}

                #endregion

                #region 闪烁效果

                if (scanLineOpacityDirection == true)
                {
                    scanLine.Opacity = scanLine.Opacity - 0.1;
                }

                if (scanLineOpacityDirection == false)
                {
                    scanLine.Opacity = scanLine.Opacity + 0.1;
                }

                if (scanLine.Opacity == 0 || scanLine.Opacity == 1)
                {
                    scanLineOpacityDirection = !scanLineOpacityDirection;
                }

                #endregion

                return true;
            });


            //txtTop = new Label
            //{
            //    VerticalOptions = LayoutOptions.Center,
            //    HorizontalOptions = LayoutOptions.Center,
            //    TextColor = Color.White,
            //    AutomationId = "zxingDefaultOverlay_TopTextLabel",
            //};
            //txtTop.SetBinding(Label.TextProperty, new Binding(nameof(LabelTopText)));
            //Children.Add(txtTop, 0, 0);
            //SetColumnSpan(txtTop, 5);

            txtScanTipsText = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.White,
                AutomationId = "zxingDefaultOverlay_BottomTextLabel",
            };
            txtScanTipsText.SetBinding(Label.TextProperty, new Binding(nameof(LabelScanTipsText)));

            #endregion

            #region Flash Area

            var slFlash = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            btnFlash = new Button
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(left: 20, top: 0, right: 20, bottom: 0),
                Text = "按钮",
                TextColor = Color.White,
                BackgroundColor = Color.Silver,
                Opacity = 0.7,
                AutomationId = "zxingDefaultOverlay_FlashButton",
            };
            btnFlash.SetBinding(Button.IsVisibleProperty, new Binding(nameof(ShowFlashButton)));
            btnFlash.SetBinding(Button.TextProperty, new Binding(nameof(ButtonFlashText)));
            btnFlash.Clicked += (sender, e) =>
            {
                FlashButtonClicked?.Invoke(btnFlash, e);
            };

            slFlash.Children.Add(txtScanTipsText);
            slFlash.Children.Add(btnFlash);
            Children.Add(slFlash, 0, 2);
            SetColumnSpan(slFlash, 5);

            #endregion
        }

        //public static readonly BindableProperty TopTextProperty =
        //    BindableProperty.Create(nameof(LabelTopText), typeof(string), typeof(ZXingOverlay), string.Empty);

        //public string LabelTopText
        //{
        //    get { return (string)GetValue(TopTextProperty); }
        //    set { SetValue(TopTextProperty, value); }
        //}

        public static readonly BindableProperty BottomTextProperty =
            BindableProperty.Create(nameof(LabelScanTipsText), typeof(string), typeof(ZXingOverlay), string.Empty);

        public string LabelScanTipsText
        {
            get { return (string)GetValue(BottomTextProperty); }
            set { SetValue(BottomTextProperty, value); }
        }

        public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(nameof(ButtonFlashText), typeof(string), typeof(ZXingOverlay), string.Empty);

        public string ButtonFlashText
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
            if (overlay?.btnFlash == null) return;
            overlay.btnFlash.Command = newValue as Command;
        }
    }
}