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
            initEvent();
        }

        private void initUI()
        {
            barcodeImageView1.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            var date = barcodeImageView1.BarcodeOptions.Hints.ContainsKey(ZXing.EncodeHintType.CHARACTER_SET);
            if (!date)
            {
                barcodeImageView1.BarcodeOptions.Hints.Add(ZXing.EncodeHintType.CHARACTER_SET, "UTF-8");
            }

            barcodeImageView1.BarcodeOptions.Width = 300;
            barcodeImageView1.BarcodeOptions.Height = 300;

            barcodeImageView1.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcodeImageView1.BarcodeValue = this.txtContent.Text;
        }

        private void initEvent()
        {
            this.btnCreate.Clicked += BtnCreate_Clicked;
        }

        private void BtnCreate_Clicked(object sender, EventArgs e)
        {
            if (this.txtContent.Text.IsNullOrWhiteSpace() == false)
            {
                this.barcodeImageView1.BarcodeValue = this.txtContent.Text;
            }
        }

    }
}