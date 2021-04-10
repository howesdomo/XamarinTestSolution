using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Client.View.ZXingDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageZXingCreateBarcode : ContentPage
    {
        public PageZXingCreateBarcode()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            barcodeImageView1.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            var date = barcodeImageView1.BarcodeOptions.Hints.ContainsKey(ZXing.EncodeHintType.CHARACTER_SET);
            
            // TODO 下拉框选择条码的编码格式
            // TODO 下拉框选择条码的类型 (一维码, 二维码)

            if (!date)
            {
                barcodeImageView1.BarcodeOptions.Hints.Add(ZXing.EncodeHintType.CHARACTER_SET, "UTF-8");
            }

            barcodeImageView1.BarcodeOptions.Width = 300;
            barcodeImageView1.BarcodeOptions.Height = 300;

            barcodeImageView1.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
        }
    }

    public class PageZXingCreateBarcode_ViewModel : ViewModel.BaseViewModel
    {
        public PageZXingCreateBarcode_ViewModel()
        {
        }

        private string _QRCodeContent;
        public string QRCodeContent
        {
            get { return _QRCodeContent; }
            set
            {
                _QRCodeContent = value;
                this.OnPropertyChanged("QRCodeContent");

                if (value.IsNullOrEmpty() == false)
                {
                    this.OnPropertyChanged("BarcodeValue");
                }
            }
        }

        public string BarcodeValue
        {
            get { return this.QRCodeContent; }
        }
    }
}