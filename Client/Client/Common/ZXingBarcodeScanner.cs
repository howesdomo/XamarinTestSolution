/// <summary>
/// V 1.0.4 - 2020-3-20 16:58:28
/// 去掉 XAML 文件
/// 
/// V 1.0.3 - 2020-3-20 15:50:07
/// 更新内容 :
/// 1 扫描成功默认处理代码块中加入播放音效
/// 
/// V 1.0.2 - 2020-3-16 16:00:52
/// 更新内容 : 
/// 1 进入扫描界面时判断主页面是否 NavigationPage, 若是取消本页面 NavigationBar 的显示
/// 2 进入扫描界面时, 设置全屏显示, 退出时取消全屏显示
/// 3 增加返回图标, 扫描线 ( 以 Base64Str 的形式存放在代码中 )
/// 4 优化预览画面效果，尽量减少变形
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Client.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ZXingBarcodeScanner : ContentPage
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
            if (title.IsNullOrWhiteSpace() == true)
            {
                title = "扫码";
            }

            // 若主界面 是 NavigationPage, 取消本扫描页面 NavigationBar的显示
            if (Application.Current.MainPage is NavigationPage)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            #region (暂无效果) 由于已关闭NavigationBar的显示, 故修改无效

            this.Title = title;

            #endregion

            myScanResultHandle = scanResultHandle;
            mIsScanContinuously = isScanContinuously;
            initComponent(title);
            initEvent();
        }

        private void initComponent(string title)
        {
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            mZXingScannerView = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,

                // 优化预览画面比例
                Options = new ZXing.Mobile.MobileBarcodeScanningOptions()
                {
                    // 相机分辨率选择器
                    CameraResolutionSelector = new ZXing.Mobile.MobileBarcodeScanningOptions.CameraResolutionSelectorDelegate(selectLowestResolutionMatchingDisplayAspectRatio)
                }
            };

            grid.Children.Add(mZXingScannerView);


            mZXingOverlay = new ZXingOverlay
            {
                TitleInfo = title,
                LabelScanTipsText = "将二维码/条码放入框内, 即可自动扫描", // TODO 通过参数区分 二维码 / 条码
                ShowFlashButton = true,
                ButtonFlashText = "开灯"
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
            #region (弃用) 设置全屏

            //try
            //{
            //    App.Screen.FullScreen();
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.GetFullInfo());
            //    System.Diagnostics.Debugger.Break();
            //}

            #endregion
        }

        protected override void OnDisappearing()
        {
            mZXingScannerView.IsScanning = false;
            mZXingScannerView.IsTorchOn = false;
            #region (弃用) 取消全屏

            //try
            //{
            //    App.Screen.CancelFullScreen();
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.GetFullInfo());
            //    System.Diagnostics.Debugger.Break();
            //}

            #endregion
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
                // *********** 自行设置扫描成功音效
                // App.AudioPlayer.PlayBeep(); 
                // *********** 自行设置扫描成功音效

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
                App.AudioPlayer.PlayBeep(); // 播放扫描音效, 程序员自定义处理另外
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

        /// <summary>
        /// 优化预览画面效果，尽量减少变形
        /// </summary>
        /// <param name="availableResolutions"></param>
        /// <returns></returns>
        private ZXing.Mobile.CameraResolution selectLowestResolutionMatchingDisplayAspectRatio(List<ZXing.Mobile.CameraResolution> availableResolutions)
        {
            ZXing.Mobile.CameraResolution result = null;

            //a tolerance of 0.1 should not be visible to the user
            double aspectTolerance = 0.1;
            var displayOrientationHeight = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Height : DeviceDisplay.MainDisplayInfo.Width;
            var displayOrientationWidth = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Width : DeviceDisplay.MainDisplayInfo.Height;

            //calculatiing our targetRatio
            var targetRatio = displayOrientationHeight / displayOrientationWidth;
            var targetHeight = displayOrientationHeight;
            var minDiff = double.MaxValue;

            //camera API lists all available resolutions from highest to lowest, perfect for us
            //making use of this sorting, following code runs some comparisons to select the lowest resolution that matches the screen aspect ratio and lies within tolerance
            //selecting the lowest makes Qr detection actual faster most of the time
            foreach (var r in availableResolutions.Where(r => Math.Abs(((double)r.Width / r.Height) - targetRatio) < aspectTolerance))
            {
                //slowly going down the list to the lowest matching solution with the correct aspect ratio
                if (Math.Abs(r.Height - targetHeight) < minDiff)
                {
                    minDiff = Math.Abs(r.Height - targetHeight);
                }

                result = r;
            }

            return result;
        }
    }

    /// <summary>
    /// 扫描界面
    /// </summary>
    public class ZXingOverlay : Grid
    {
        Label txtTitle;
        Label txtScanTipsText;
        Button btnFlash;
        Image btnBack;

        int scanLineOriginY = -9999;
        int scanLineY = 0;

        bool scanLineOpacityDirection = false;

        public delegate void FlashButtonClickedDelegate(Button sender, EventArgs e);

        public event FlashButtonClickedDelegate FlashButtonClicked;

        // 图片 - 扫描线
        private static string ScanLine_Base64Str { get; set; } = "iVBORw0KGgoAAAANSUhEUgAAApcAAABXCAMAAABmxZ4+AAAAOVBMVEU6qv+z//81pO4hmuMlneUimuQlne0jmuMpn+Qnnecim+IjmuQjm+UlnOchmuMkmuMjoekinuQpmeNW5LidAAAAEnRSTlMEAQlUIEoOTxIaNC45KUQ/FiUhHY6sAAAEuklEQVR42u3d0XLaMBBGYeHQtAmBpn3/h60pJL+9q/XKyM24M+dA7jpNLr5ZG2NL5UC0v3BJe6zTZSGq1gsLhPRFrbIGSPriulzmJgdevJJXWJfLGCNRYzHRx1xWRd5DJjVVrE9T4jJRyYSkvmKcq1w2oHwmyht8TTJLxhKPtLnPHGZZVDmUjOQPolo1LOHQbHEZjEow0mPFNFXuUionMOsk34kWq+qszszMpVh6lJHHF6JpEU/R9DKXXVaGpUGJRmovpOmP5bFLsYxU1kgeiWw1nHOZycQs1WnpWAplxSM0Sc1A1GiGEzN1WSKV9RH560hkQQiobEqmmZi5Sz8tjcr5X0AUNHViZBqYiUszLc2wFMoayDPRtZpO0TQj08OMXQ4zlzWVTuPpTGQxiElNpp2YsctyK2Q5R6m/g8ikQTWnGcEs9wKXIUujEpC0gqeRGcC0LnOWV+VSaUy+EvkERDQ1Mg1MfSaPXXqWTiUiaZVOI7MKc4VLx1IqX9Ub0bzXt1clmQ5m7lIfxpdYGpVvmKSlpjIF86UFZgnH5Y8xsZRKPyZ/Ek3zNCVTE/MKLHcZjMs6S0RSs04P8+UYDMzIpcaljuJiKZVzlBeiaaI5kymYOpI/4tKzlEpI0nIzmjWYTS7LLTsuxfI0ZWlMfrt8I7o2UrA2BXNMMO3ALLdqLgfv0k7LT5WQpIWcTE3MwOVgXEaH8StLuZywlEpMUkrTwRw96QyzzaUflzFLqfxONE0yI5hmYIYui3dpx6VYXv4GSkppGphuYHqXpcXlcex+dnnVPp2WVuUTL17j6zMP80rInGEmLoPTy2BcOpVPREoyBbM6MEdj7S6fqy41LjUtP1UCk6RSMjUyNTC9S8FMXOr0UodxjcvPacmspIimnZgamMkJZrNLHcbNuOQYTmFTmDrD1IF8Q5dvv4n6wiXtMVzSHvtwyfklNfV/nl+e3eVLYNJCHZ/H+65fclmdWi8T9V+/LMn3PRFMLqyTVPZ839P//Th3bVBQ3/fjm99P9MQPP/33E21w/yU3YNIm919yvzp9VZcN71df9XwPNilEuf75npLPy/bnIdFJVuSjz0PeZPL8ONXb1fPjrLdBl8tu19tgfSL61+sT2aP4M+u5UdDO13Mba1n/khVZae36l8nCrKXqUjAH1gum/sTj1Lhe8NC/vjpL/lNLpy3XVy9r96OAJ3mQa/ejuMf+PbTQnvbvYb8zOp12uN+ZYCb7Q7JbKbXk9ofMWG69n+4ZoPSB8bzxfrqCubz/ONuP08b7jw/ZvvgBTMm803w5EiXJpFQGwzJzqQRTMkXzGjYpEzk1KZV2WmYuBXP8xxbmjaZsiifjk2Ya5dGi9CyLOjiXqigrUzTvOImCDEmhlMp4WsplPDEF09NEKCUaHUqxDKelXMYTUzRlU70TLTXFIkJiVfy0lMsMpmgKJ1GeRKpBFVVReAhhxjQFFKKUcLQopXIQy8Slkzl8yBROovUNKlUplzFMDU18Uo9HZXQdcpeq2Aai/u6YYpVymcrU/zPAkx6rGJJSmbn0FWNzYHjS6rwgocxdxja9TqKkxEzO7tBQ+At58156K2uy36UqRB0l1lKXUKXuemEdiPYXLmmP/QGxrzK0lEarCgAAAABJRU5ErkJggg==";

        // 图片 - 返回按钮
        private static string btnBack_Image_Base64Str { get; set; } = "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";

        public ZXingOverlay()
        {
            BindingContext = this;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;

            // RowDefinitions - 4行
            RowDefinitions.Add(new RowDefinition { Height = 30 }); // *
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // *
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }); // 3*
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) }); // 2*

            // ColumnDefinitions - 5列
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // *

            this.RowSpacing = 0;
            this.ColumnSpacing = 0;

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
            Children.Add(bv_Bottom_Black, 0, 3);

            SetColumnSpan(bv_Top_Black, 5);
            SetColumnSpan(bv_Bottom_Black, 5);

            SetRowSpan(bv_Top_Black, 2);

            var bv_Left_Black = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
            };
            Children.Add(bv_Left_Black, 0, 2);

            var bv_Right_Black = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.7,
            };
            Children.Add(bv_Right_Black, 4, 2);

            #endregion

            txtTitle = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White,
                FontSize = 26,
                AutomationId = "zxingDefaultOverlay_txtTitle",
            };

            txtTitle.SetBinding(Label.TextProperty, new Binding(nameof(TitleInfo)));
            Children.Add(txtTitle, 1, 0);
            SetColumnSpan(txtTitle, 3);

            #region ScanArea

            var slScanArea = new AbsoluteLayout();

            slScanArea.InputTransparent = true;
            Children.Add(slScanArea, 1, 2);
            SetColumnSpan(slScanArea, 3);

            var scanLine = new Image
            {
                Source = Util.XamariN.Common.ImageSourceUtils.String2ImageSource($"stream://{ScanLine_Base64Str}")
            };

            scanLine.InputTransparent = true;
            AbsoluteLayout.SetLayoutBounds(scanLine, new Rectangle(1, scanLineY, 1, 1));
            AbsoluteLayout.SetLayoutFlags(scanLine, AbsoluteLayoutFlags.SizeProportional);
            slScanArea.Children.Add(scanLine);

            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                if (scanLineOriginY == -9999) // 初始化 -- 只执行一次
                {
                    scanLineOriginY = -(((int)slScanArea.Height) / 2);
                    scanLineY = scanLineOriginY;
                }

                #region 上下移动效果

                scanLineY += 5;
                AbsoluteLayout.SetLayoutBounds(scanLine, new Rectangle(1, scanLineY, 1, 1));
                if (scanLineY >= slScanArea.Height / 2)
                {
                    scanLineY = scanLineOriginY;
                }

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
                Padding = new Thickness(left: 15, top: 0, right: 15, bottom: 0),
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
            Children.Add(slFlash, 0, 3);
            SetColumnSpan(slFlash, 5);

            #endregion

            #region 返回上一层按钮

            btnBack = new Image()
            {
                HeightRequest = 27,
                WidthRequest = 27,
                Margin = new Thickness(left: 5, top: 5, right: 0, bottom: 0),
                Source = Util.XamariN.Common.ImageSourceUtils.String2ImageSource($"stream://{btnBack_Image_Base64Str}")
            };

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer() { };
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                if (Application.Current.MainPage is NavigationPage)
                {
                    var index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;

                    if (Application.Current.MainPage.Navigation.NavigationStack[index] is ZXingBarcodeScanner)
                    {
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                }
            };

            btnBack.GestureRecognizers.Add(tapGestureRecognizer);
            btnBack.HorizontalOptions = LayoutOptions.StartAndExpand;
            Children.Add(btnBack, 0, 0);

            #endregion
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(TitleInfo), typeof(string), typeof(ZXingOverlay), string.Empty);

        public string TitleInfo
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

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