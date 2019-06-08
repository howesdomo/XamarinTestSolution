using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface IBluetooth
    {        
        bool mBluetoothIsEnabled { get; }

        bool mIsConnected { get; set; }

        string mDeviceName { get; set; }

        void OpenBluetooth();

        void CloseBluetooth();

        void OpenDiscoverable(int discoverableDuration);

        List<Util.XamariN.BluetoothDeviceInfo> GetBondedDevices();

        System.Threading.Tasks.Task<List<Util.XamariN.BluetoothDeviceInfo>> DiscoveryUnbondDevices(int countDown = 10);

        System.Threading.Tasks.Task<bool> Connect(string deviceName);

        System.Threading.Tasks.Task<bool> ConnectV2(Util.XamariN.BluetoothDeviceInfo bluetoothDevice, string pinCode = null);

        void Disconnect(); // TODO 改为Task

        void SendZPL(string zpl, Encoding encoding = null);


    }
}
