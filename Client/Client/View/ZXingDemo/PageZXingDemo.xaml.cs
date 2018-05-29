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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageZXingDemo : ContentPage
    {

        ZXingScannerPage scanPage;
        Button buttonScanDefaultOverlay;
        Button buttonScanCustomOverlay;
        Button buttonScanContinuously;
        Button buttonScanCustomPage;
        Button buttonScanContinuouslyCustomPage;
        Button buttonGenerateBarcode;

        public PageZXingDemo() : base()
        {
            // ************ buttonScanDefaultOverlay ************

            buttonScanDefaultOverlay = new Button
            {
                Text = "Scan with Default Overlay",
                AutomationId = "scanWithDefaultOverlay",
            };

            buttonScanDefaultOverlay.Clicked += async delegate
            {
                scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        string msg = "扫描内容(Text)\r\n{0}\r\n\r\n条码格式(BarcodeFormat)\r\n{1}".FormatWith(result.Text, result.BarcodeFormat);
                        System.Diagnostics.Debug.WriteLine(msg);

                        // TODO 处理 字符集GB2312 乱码无法识别问题
                        //string msg2 = "{0}".FormatWith(CoreUtil.EncodingUtils.GetString(result.Text));
                        //System.Diagnostics.Debug.WriteLine(msg2);

                        DisplayAlert("扫描成功", msg, "OK");
                    });
                };

                await Navigation.PushAsync(scanPage);
            };

            // ************ buttonScanCustomOverlay ************
            buttonScanCustomOverlay = new Button
            {
                Text = "Scan with Custom Overlay",
                AutomationId = "scanWithCustomOverlay",
            };

            buttonScanCustomOverlay.Clicked += async delegate
            {
                // Create our custom overlay
                var customOverlay = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                var torch = new Button
                {
                    Text = "Toggle Torch"
                };
                torch.Clicked += delegate
                {
                    scanPage.ToggleTorch();
                };
                customOverlay.Children.Add(torch);

                scanPage = new ZXingScannerPage(customOverlay: customOverlay);
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        System.Diagnostics.Debug.WriteLine("Scanned Barcode : {0}".FormatWith(result.Text));
                        DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };
                await Navigation.PushAsync(scanPage);
            };

            // ************ buttonScanContinuously ************
            buttonScanContinuously = new Button
            {
                Text = "Scan Continuously (缺点:镜头不移开一直DisplayAlert结果)",
                AutomationId = "scanContinuously",
            };

            buttonScanContinuously.Clicked += async delegate
            {
                scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(() =>
                {
                    System.Diagnostics.Debug.WriteLine("Scanned Barcode : {0}".FormatWith(result.Text));
                    DisplayAlert("Scanned Barcode", result.Text, "OK");
                });

                await Navigation.PushAsync(scanPage);
            };

            // ************ buttonScanCustomPage ************
            buttonScanCustomPage = new Button
            {
                Text = "Scan with Custom Page",
                AutomationId = "scanWithCustomPage",
            };

            buttonScanCustomPage.Clicked += async delegate
            {
                var customScanPage = new PageZXingScan();
                await Navigation.PushAsync(customScanPage);
            };

            // ************ buttonScanContinuouslyCustomPage ************
            buttonScanContinuouslyCustomPage = new Button
            {
                Text = "Scan Continuously with Custom Page ( Add By Howe )",
                AutomationId = "scanWithCustomPage",
            };

            buttonScanContinuouslyCustomPage.Clicked += async delegate
            {
                var customScanPage = new PageZXingScan(isScanContinuously: true);
                await Navigation.PushAsync(customScanPage);
            };



            // ************ buttonGenerateBarcode ************
            buttonGenerateBarcode = new Button
            {
                Text = "Barcode Generator",
                AutomationId = "barcodeGenerator",
            };

            buttonGenerateBarcode.Clicked += async delegate
            {
                await Navigation.PushAsync(new ZXingDemo.PageZXingCreateBarcode());
            };

            var stack = new StackLayout();

            stack.Children.Add(buttonScanDefaultOverlay);
            stack.Children.Add(buttonScanCustomOverlay);
            stack.Children.Add(buttonScanContinuously);
            stack.Children.Add(buttonScanCustomPage);
            stack.Children.Add(buttonScanContinuouslyCustomPage);
            stack.Children.Add(buttonGenerateBarcode);

            Content = stack;
        }

    }
}