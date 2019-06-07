using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


using Android.Bluetooth;
using Android.Content;
using Java.IO;
using Java.Util;

namespace Client.Droid
{
    public class MyBluetooth :
          Android.Content.BroadcastReceiver
        , Client.Common.IBluetooth

    {
        public static int Bluetooth_RequestCode = 8766;

        private BluetoothSocket mBluetoothSocket { get; set; } = null;

        private CancellationTokenSource mCancellationTokenSource { get; set; }

        public bool mIsConnected { get; set; }

        public string mDeviceName { get; set; }

        private Android.App.Activity mAppActivity { get; set; }

        #region 构造函数 + 单例模式

        private MyBluetooth()
        {

        }

        private static MyBluetooth s_Instance;

        private static object objLock = new object();

        public static MyBluetooth GetInstance(Android.App.Activity activity = null)
        {
            lock (objLock)
            {
                if (s_Instance == null)
                {
                    s_Instance = new MyBluetooth();
                    if (s_Instance.mAppActivity == null && activity == null)
                    {
                        throw new Exception("MyBluetooth.GetInstance 蓝牙单例首次创建, 请传入 activity 参数");
                    }
                    if (activity != null)
                    {
                        s_Instance.mAppActivity = activity;
                    }
                }
                return s_Instance;
            }
        }

        #endregion

        /// <summary>
        /// 蓝牙是否已开启
        /// </summary>
        /// <returns></returns>
        public bool BluetoothIsEnabled
        {
            get
            {
                if (BluetoothAdapter.DefaultAdapter == null)
                {
                    throw new BluetoothException("没有蓝牙设备（BluetoothAdapter.DefaultAdapter == null）");
                }

                bool r = false;
                try
                {
                    r = BluetoothAdapter.DefaultAdapter.IsEnabled;
                }
                catch (Exception)
                {
                    r = false;
                }
                return r;
            }
        }

        /// <summary>
        /// 打开授权 : XXX应用, 想要打开蓝牙。
        /// </summary>
        public void OpenBluetooth()
        {
            if (this.BluetoothIsEnabled == false)
            {
                // 打开授权 : XXX应用, 想要打开蓝牙。
                Intent intent = new Intent(BluetoothAdapter.ActionRequestEnable);
                mAppActivity.StartActivityForResult(intent, MyBluetooth.Bluetooth_RequestCode);
            }
        }

        public void Handle_OpenBluetooth(int requestCode, Android.App.Result resultCode, Intent data)
        {
            // TODO switch
            switch (requestCode)
            {
                case 8766:
                    string a = "";
                    break;
                case 8767:
                    string b = "";
                    break;
                case 8768:
                    string c = "";
                    break;
                default:
                    break;
            }

            // return this.BluetoothIsEnabled;
        }

        /// <summary>
        /// 打开授权 : 某个应用想让其他蓝牙设备在 x 秒内可检测到你的手机。
        /// </summary>
        /// <param name="discoverableDuration"></param>
        public void OpenDiscoverable(int discoverableDuration = 300)
        {
            // 打开授权 : 某个应用想让其他蓝牙设备在 300 秒内可检测到你的手机。
            Intent discoverableIntent = new Intent(BluetoothAdapter.ActionRequestDiscoverable);
            discoverableIntent.PutExtra(BluetoothAdapter.ExtraDiscoverableDuration, discoverableDuration);
            mAppActivity.StartActivity(discoverableIntent);
        }

        /// <summary>
        /// 获取已配对列表
        /// </summary>
        public List<Util.XamariN.BluetoothDeviceInfo> GetBondedDevices()
        {
            if (this.BluetoothIsEnabled == false)
            {
                throw new BluetoothException("蓝牙未开启");
            }

            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter; //获取蓝牙

            List<Util.XamariN.BluetoothDeviceInfo> r = new List<Util.XamariN.BluetoothDeviceInfo>();

            foreach (BluetoothDevice item in adapter.BondedDevices)
            {
                string msg = "Name:{0}, MAC:{1}".FormatWith(item.Name, item.Address);
                System.Diagnostics.Debug.WriteLine(msg);

                r.Add
                (
                    new Util.XamariN.BluetoothDeviceInfo
                    (
                        name: item.Name,
                        address: item.Address,
                        bluetoothDeviceType: (int)item.Type,
                        bluetoothBondState: (int)item.BondState
                    )
                );
            }

            return r;
        }

        private static object _LOCK_ = new object();

        private bool mDiscoverying { get; set; }

        private List<Util.XamariN.BluetoothDeviceInfo> mUnbondDevicesList { get; set; }

        public List<Util.XamariN.BluetoothDeviceInfo> DiscoveryUnbondDevices()
        {
            // 新东西
            // BluetoothDevice.ExtraDevice
            // BluetoothDevice.ActionFound

            lock (_LOCK_)
            {
                mDiscoverying = true;
                mUnbondDevicesList = new List<Util.XamariN.BluetoothDeviceInfo>();

                Task.Run(() =>
                {
                    IntentFilter filter = new IntentFilter(BluetoothDevice.ActionFound);
                    mAppActivity.RegisterReceiver(this, filter);

                    BluetoothAdapter.DefaultAdapter.StartDiscovery();
                });

                int countDown = 10; // 最长等待秒数
                while (mDiscoverying)
                {
                    if (countDown <= 0)
                    {
                        break;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    countDown -= 1;
                }

                return mUnbondDevicesList;
            }
        }


        public async Task<bool> Connect(string name)
        {
            return await ConnectToDevice(name);
        }

        private async Task<bool> ConnectToDevice(string name)
        {

            BluetoothDevice device = null;
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;

            if (mIsConnected)
            {
                return true;
            }

            mCancellationTokenSource = new CancellationTokenSource();
            while (mCancellationTokenSource.IsCancellationRequested == false)
            {
                try
                {
                    Thread.Sleep(200);

                    adapter = BluetoothAdapter.DefaultAdapter;

                    if (adapter?.IsEnabled == null)
                        return false;

                    // paired devices
                    foreach (var bondedDevice in adapter.BondedDevices)
                    {
                        if (bondedDevice.Name.ToLower().IndexOf(name.ToLower()) >= 0)
                        {
                            device = bondedDevice;
                            break;
                        }
                    }

                    if (device == null)
                    {
                        // 找不到指定的蓝牙名称
                        return false;
                    }

                    var uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");
                    mBluetoothSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);

                    if (mBluetoothSocket == null)
                        return false;

                    await mBluetoothSocket.ConnectAsync();

                    mIsConnected = mBluetoothSocket.IsConnected;
                    if (mIsConnected)
                    {
                        mDeviceName = device.Name;
                        await Task.Run(TalkToDevice);
                    }

                    // TalkToDevice 会返回 Task.CompletedTask;
                    // 故以下的代码都是异常的情况

                    var a2dp = adapter.GetProfileConnectionState(ProfileType.A2dp);
                    var gatt = adapter.GetProfileConnectionState(ProfileType.Gatt);
                    var gattserver = adapter.GetProfileConnectionState(ProfileType.GattServer);
                    var headset = adapter.GetProfileConnectionState(ProfileType.Headset);
                    var health = adapter.GetProfileConnectionState(ProfileType.Health);
                    var sap = adapter.GetProfileConnectionState(ProfileType.Sap);

                    return mIsConnected;
                }
                catch (Exception ex)
                {
                    string msg = "{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg);
                }

                return false;
            }

            return false;
        }

        private Task TalkToDevice()
        {
            // check _socket.IsConnected, read w/InputStreamReader, BufferedReader
            return Task.CompletedTask;
        }

        public void Disconnect()
        {
            if (mBluetoothSocket != null && mBluetoothSocket.IsConnected == true)
            {
                try
                {
                    mBluetoothSocket.Close();

                    mDeviceName = string.Empty;
                    mIsConnected = false;
                    mBluetoothSocket = null;
                }
                catch (Exception ex)
                {
                    string msg = "{0}".FormatWith(ex.GetInfo());
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            }
        }

        public void SendZPL(string zpl, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            byte[] bBuf = encoding.GetBytes(zpl);
            mBluetoothSocket.OutputStream.WriteAsync(bBuf, 0, bBuf.Length);
        }


        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            if (BluetoothDevice.ActionFound.Equals(action) == true) // 搜索到的蓝牙设备
            {
                BluetoothDevice item = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                mUnbondDevicesList.Add
                (
                    new Util.XamariN.BluetoothDeviceInfo
                    (
                        name: item.Name,
                        address: item.Address,
                        bluetoothDeviceType: (int)item.Type,
                        bluetoothBondState: (int)item.BondState
                    )
                );
            }
            else
            {
                // 取消注册
                mDiscoverying = false;
                mAppActivity.UnregisterReceiver(this);
            }
        }
    }
}