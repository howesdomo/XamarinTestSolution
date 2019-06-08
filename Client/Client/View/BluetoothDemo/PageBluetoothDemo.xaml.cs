using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBluetoothDemo : ContentPage
    {
        PageBluetoothDemo_ViewModel ViewModel { get; set; }

        public PageBluetoothDemo()
        {
            InitializeComponent();
            initEvent();
            this.ViewModel = new PageBluetoothDemo_ViewModel();
            this.BindingContext = this.ViewModel;

            this.txtZPLContent.Text = @"^XA
^LH0,0
^FO20,20^A1N,25,25
^FDHello World^FS
^XZ";
        }

        private void initEvent()
        {
            this.swBluetoothEnable.Toggled += swBluetoothEnable_Toggled;

            this.btnBondedDevices.Clicked += BtnBondedDevices_Clicked;
            this.btnDiscoveryUnbondDevices.Clicked += BtnDiscoveryUnbondDevices_Clicked;
            this.lvBoudedList.ItemSelected += lvBoudedList_ItemSelected;
            this.lvUnboudList.ItemSelected += lvUnboudList_ItemSelected;

            this.btnConnect.Clicked += BtnConnect_Clicked;
            this.btnPrintZPL.Clicked += BtnPrintZPL_Clicked;
            this.btnDisconnect.Clicked += BtnDisconnect_Clicked;

            this.btnShowSLConnection.Clicked += btnShowSLConnection_Clicked;
            this.btnShowSLCommunication.Clicked += btnShowSLCommunication_Clicked;

            this.Appearing += PageBluetoothDemo_Appearing;

            Common.Bluetooth.UpdateInfo += new EventHandler<Common.BLuetoothEventArgs>(this.UpdateInfo_Handle);
        }



        private void btnShowSLConnection_Clicked(object sender, EventArgs e)
        {
            slCommunication.IsVisible = false;
        }

        private void btnShowSLCommunication_Clicked(object sender, EventArgs e)
        {
            slCommunication.IsVisible = true;
        }

        private void UpdateInfo_Handle(object sender, BLuetoothEventArgs e)
        {
            if (this.lvBoudedList.ItemsSource != null)
            {
                foreach (var item in this.lvBoudedList.ItemsSource)
                {
                    var device = item as Util.XamariN.BluetoothDeviceInfo;
                    if (device.Equals(e.Update) == true)
                    {
                        device.IsConnected = e.Update.IsConnected;
                    }
                }
            }
        }

        #region 两个 ListView 的 SelectedItem 互斥

        private void lvUnboudList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            lvBoudedList.SelectedItem = null;
        }

        private void lvBoudedList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            lvUnboudList.SelectedItem = null;
        }

        #endregion

        private void PageBluetoothDemo_Appearing(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void swBluetoothEnable_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                swBluetoothEnable_Toggled_ActualMethod(e.Value);
                UpdateUI();
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private void swBluetoothEnable_Toggled_ActualMethod(bool value)
        {
            if (App.Bluetooth.mBluetoothIsEnabled == value)
            {
                return;
            }
            else
            {
                if (value == true)
                {
                    App.Bluetooth.OpenBluetooth();
                }
                else
                {
                    App.Bluetooth.CloseBluetooth();
                }
            }
        }

        async void BtnBondedDevices_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Util.XamariN.BluetoothDeviceInfo> list = App.Bluetooth.GetBondedDevices();
                this.lvBoudedList.ItemsSource = list;
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

        async void BtnDiscoveryUnbondDevices_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Util.XamariN.BluetoothDeviceInfo> list = await App.Bluetooth.DiscoveryUnbondDevices(countDown: 10);
                this.lvUnboudList.ItemsSource = list;
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
            Device.BeginInvokeOnMainThread(() =>
            {
                // 注销检测事件
                this.swBluetoothEnable.Toggled -= swBluetoothEnable_Toggled;
                System.Threading.Thread.Sleep(200);

                this.swBluetoothEnable.IsToggled = App.Bluetooth.mBluetoothIsEnabled;

                this.slConnection.IsVisible = App.Bluetooth.mBluetoothIsEnabled;

                this.btnBondedDevices.IsEnabled = App.Bluetooth.mBluetoothIsEnabled;
                this.btnDiscoveryUnbondDevices.IsEnabled = App.Bluetooth.mBluetoothIsEnabled;

                this.btnConnect.IsEnabled = !App.Bluetooth.mIsConnected;
                this.txtZPLContent.IsEnabled = App.Bluetooth.mIsConnected;
                this.btnPrintZPL.IsEnabled = App.Bluetooth.mIsConnected;
                this.btnDisconnect.IsEnabled = App.Bluetooth.mIsConnected;

                // 重新注测检测事件
                this.swBluetoothEnable.Toggled += swBluetoothEnable_Toggled;
            });
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

    public class PageBluetoothDemo_ViewModel : ViewModel.BaseViewModel
    {
        private Util.XamariN.BluetoothDeviceInfo _SelectedBluetoothDeviceInfo;

        public Util.XamariN.BluetoothDeviceInfo SelectedBluetoothDeviceInfo
        {
            get
            {
                return this._SelectedBluetoothDeviceInfo;
            }
            set
            {
                _SelectedBluetoothDeviceInfo = value;
            }
        }
    }
}