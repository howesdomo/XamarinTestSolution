using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface IBluetooth
    {        
        bool mIsConnected { get; set; }

        string mDeviceName { get; set; }

        void OpenBluetooth();

        void OpenDiscoverable(int discoverableDuration);

        List<Util.XamariN.BluetoothDeviceInfo> GetBondedDevices();

        List<Util.XamariN.BluetoothDeviceInfo> DiscoveryUnbondDevices();

        System.Threading.Tasks.Task<bool> Connect(string deviceName);

        void Disconnect(); // TODO 改为Task

        void SendZPL(string zpl, Encoding encoding = null);
    }
}
