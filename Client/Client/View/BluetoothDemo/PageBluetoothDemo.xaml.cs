using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBluetoothDemo : ContentPage
    {
        public PageBluetoothDemo()
        {
            InitializeComponent();
            initEvent();

            this.txtZPLContent.Text = @"^XA
^LH0,0
^FO20,20^A1N,25,25
^FDHello World^FS
^XZ";
        }

        private void initEvent()
        {
            this.btnOpenBluetooth.Clicked += BtnOpenBluetooth_Clicked;

            this.btnBondedDevices.Clicked += BtnBondedDevices_Clicked;
            this.btnDiscoveryUnbondDevices.Clicked += BtnDiscoveryUnbondDevices_Clicked;

            this.btnConnect.Clicked += BtnConnect_Clicked;
            this.btnPrintZPL.Clicked += BtnPrintZPL_Clicked;
            this.btnDisconnect.Clicked += BtnDisconnect_Clicked;
        }

        private void BtnOpenBluetooth_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.Bluetooth.OpenBluetooth();
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        async void BtnBondedDevices_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Util.XamariN.BluetoothDeviceInfo> list = App.Bluetooth.GetBondedDevices();
            }
            catch (BluetoothException btEx)
            {
                await DisplayAlert(title: "蓝牙设备异常", message: btEx.Message, cancel: "确定");
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                await DisplayAlert(title: "捕获异常", message: ex.GetInfo(), cancel: "确定");
            }
        }

        private void BtnDiscoveryUnbondDevices_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Util.XamariN.BluetoothDeviceInfo> list = App.Bluetooth.DiscoveryUnbondDevices();
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        async void BtnConnect_Clicked(object sender, EventArgs e)
        {
            bool r = await App.Bluetooth.Connect("ZT410"); // TODO 从列表中选择
            UpdateUI();

            if (r == false)
            {
                await DisplayAlert(title: "异常", message: "蓝牙连接失败", cancel: "确定");
            }
        }

        private void UpdateUI()
        {
            this.btnConnect.IsEnabled = !App.Bluetooth.mIsConnected;

            this.txtZPLContent.IsEnabled = App.Bluetooth.mIsConnected;
            this.btnPrintZPL.IsEnabled = App.Bluetooth.mIsConnected;
            this.btnDisconnect.IsEnabled = App.Bluetooth.mIsConnected;
        }

        void BtnPrintZPL_Clicked(object sender, EventArgs e)
        {
            App.Bluetooth.SendZPL(txtZPLContent.Text);
        }


        private void BtnDisconnect_Clicked(object sender, EventArgs e)
        {
            App.Bluetooth.Disconnect();
            UpdateUI();
        }
    }
}