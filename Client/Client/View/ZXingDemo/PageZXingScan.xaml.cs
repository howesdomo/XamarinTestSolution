using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Client.View
{
    /// <summary>
    /// 项目自定义扫描界面
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageZXingScan : ContentPage
    {
        /// <summary>
        /// 持续扫描
        /// </summary>
        private bool mIsScanContinuously { get; set; }

        bool mIsBusy { get; set; }

        ZXingScannerView mZXingScannerView { get; set; }

        ZXingOverlay mZXingOverlay { get; set; }

        public PageZXingScan(bool isScanContinuously = false) : base()
        {
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

        private void initEvent()
        {
            mZXingScannerView.OnScanResult += MZXingScannerView_OnScanResult;
            mZXingOverlay.FlashButtonClicked += MZXingOverlay_FlashButtonClicked;
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
        private void MZXingScannerView_OnScanResult(ZXing.Result result)
        {
            if (mIsBusy == false)
            {
                mIsBusy = true;
            }
            else
            {
                return;
            }
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                string msg = "扫描内容(Text)\r\n{0}\r\n\r\n条码格式(BarcodeFormat)\r\n{1}".FormatWith(result.Text, result.BarcodeFormat);
                System.Diagnostics.Debug.WriteLine(msg);

                try
                {
                    // ** 引用 Xamarin.Essentials 包 **
                    // 将内容复制到剪贴板
                    Xamarin.Essentials.Clipboard.SetText(result.Text);

                    msg += "\r\n扫描内容已复制到粘贴板";
                }
                catch (Exception ex)
                {
                    string msg2 = "{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg2);

                    msg += "\r\n扫描内容复制到粘贴板时发生错误\r\n{0}".FormatWith(ex.GetFullInfo());
                }

                await DisplayAlert("扫描内容：", msg, "确定");

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
            });
        }

        /// <summary>
        /// 控制闪光灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MZXingOverlay_FlashButtonClicked(Button sender, EventArgs e)
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
}