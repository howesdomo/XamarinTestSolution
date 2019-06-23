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
using Util.XamariN;
using Android.Runtime;

namespace Client.Droid
{
    public class MyBluetooth :
          Android.Content.BroadcastReceiver
        , IBluetoothProfileServiceListener
        , Client.Common.IBluetooth

    {
        private static object _LOCK_ = new object();

        public static int Bluetooth_RequestCode = 8766; // Blue

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
        public bool mBluetoothIsEnabled
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
            if (this.mBluetoothIsEnabled == false)
            {
                // 打开授权 : XXX应用, 想要打开蓝牙。
                Intent intent = new Intent(BluetoothAdapter.ActionRequestEnable);
                mAppActivity.StartActivityForResult(intent, MyBluetooth.Bluetooth_RequestCode);
            }
        }

        /// <summary>
        /// 关闭蓝牙
        /// </summary>
        public void CloseBluetooth()
        {
            if (this.mBluetoothIsEnabled == true)
            {
                BluetoothAdapter.DefaultAdapter.Disable();
            }
        }

        public void Handle_OpenBluetooth(int requestCode, Android.App.Result resultCode, Intent data)
        {
            // TODO 设想 : 对于未开启蓝牙的情况下, 要求某个蓝牙的操作, 在开启后根据 requestCode 自动执行对应行动(某个蓝牙的操作)
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
            if (this.mBluetoothIsEnabled == false)
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

            // 查看是否蓝牙是否连接到三种设备的一种，以此来判断是否处于连接状态还是打开并没有连接的状态
            Android.Bluetooth.ProfileState a2dp = adapter.GetProfileConnectionState(ProfileType.A2dp); // 可操控蓝牙设备，如带播放暂停功能的蓝牙耳机
            Android.Bluetooth.ProfileState headset = adapter.GetProfileConnectionState(ProfileType.Headset); // 蓝牙头戴式耳机，支持语音输入输出
            Android.Bluetooth.ProfileState health = adapter.GetProfileConnectionState(ProfileType.Health); // 蓝牙穿戴式设备

            if (a2dp == ProfileState.Connected)
            {
                BluetoothAdapter.DefaultAdapter.GetProfileProxy
                (
                    context: mAppActivity.ApplicationContext,
                    listener: this,
                    profile: ProfileType.A2dp
                );
            }

            if (headset == ProfileState.Connected)
            {
                BluetoothAdapter.DefaultAdapter.GetProfileProxy
                (
                    context: mAppActivity.ApplicationContext,
                    listener: this,
                    profile: ProfileType.Headset
                );
            }

            if (health == ProfileState.Connected)
            {
                BluetoothAdapter.DefaultAdapter.GetProfileProxy
                (
                    context: mAppActivity.ApplicationContext,
                    listener: this,
                    profile: ProfileType.Health
                );
            }

            return r;
        }

        #region 搜索 未配对蓝牙 设备

        /// <summary>
        /// 正在搜索蓝牙中
        /// </summary>
        private bool mDiscoverying { get; set; }

        /// <summary>
        /// 搜索蓝牙等待剩余秒数
        /// </summary>
        private int mCountDown { get; set; }

        /// <summary>
        /// 安卓系统专用
        /// </summary>
        private List<Android.Bluetooth.BluetoothDevice> mUnbondDevicesList { get; set; } = new List<BluetoothDevice>();

        /// <summary>
        /// Xamarin.Forms 合用
        /// </summary>
        private List<Util.XamariN.BluetoothDeviceInfo> mUnbondDeviceInfoList { get; set; } = new List<BluetoothDeviceInfo>();

        public async Task<List<Util.XamariN.BluetoothDeviceInfo>> DiscoveryUnbondDevices(int countDown = 10)
        {
            if (mDiscoverying == true)
            {
                throw new Exception("mDiscoverying is true");
            }

            mDiscoverying = true;

            mUnbondDevicesList = new List<BluetoothDevice>(); // 清空安卓专用列表
            mUnbondDeviceInfoList = new List<Util.XamariN.BluetoothDeviceInfo>(); // 清空Xamarin.Forms专用列表

            // 广播接收器 - 动态注册
            IntentFilter filter = new IntentFilter(BluetoothDevice.ActionFound);
            mAppActivity.RegisterReceiver(GetInstance(), filter);

            // 开始搜索蓝牙
            BluetoothAdapter.DefaultAdapter.StartDiscovery(); // 请到 public override void OnReceive() 中编写发现蓝牙逻辑

            // 蓝牙搜索过程持续等待 x 秒
            mCountDown = countDown; // 设置 搜索蓝牙等待剩余秒数
            await Task.Run(waitForDiscoveryComplete);

            return mUnbondDeviceInfoList;
        }

        /// <summary>
        /// 蓝牙搜索过程持续等待
        /// </summary>
        /// <returns></returns>
        private Task waitForDiscoveryComplete()
        {
            while (mCountDown > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                mCountDown -= 1;
            }

            mDiscoverying = false;
            mAppActivity.UnregisterReceiver(GetInstance()); // 广播接收器 - 取消注册

            return Task.CompletedTask;
        }

        #endregion

        #region 连接蓝牙设备

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

        public async Task<bool> ConnectV2(Util.XamariN.BluetoothDeviceInfo bluetoothDevice, string pinCode = null)
        {
            mCancellationTokenSource = new CancellationTokenSource();
            while (mCancellationTokenSource.IsCancellationRequested == false)
            {
                if (mBluetoothIsEnabled == false)
                {
                    return false; // 缺少蓝牙设备抛错, 故这里可以不做逻辑
                }

                BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;

                // 从已配对列表寻找
                var device = adapter.BondedDevices.FirstOrDefault(i => i.Address == bluetoothDevice.Address);
                if (device == null)
                {
                    device = mUnbondDevicesList.FirstOrDefault(i => i.Address == bluetoothDevice.Address);
                    if (device == null)
                    {
                        throw new BluetoothException("mUnbondDevicesList 找不到 {0}[{1}] 设备".FormatWith(bluetoothDevice.Name, bluetoothDevice.Address));
                    }

                    // 未配对设备, 使用PIN码进行蓝牙配对
                    if (pinCode.IsNullOrWhiteSpace() == false)
                    {
                        byte[] pinCodeByteArr = System.Text.Encoding.Default.GetBytes(pinCode);
                        device.SetPin(pinCodeByteArr);
                    }
                    // // TODO 未配对, 但用户不输入pin码, 弹出配对窗口????? 待测试
                    // else if ...... 
                    // {
                    //     device.SetPairingConfirmation(true);
                    // }
                    else
                    {
                        throw new BluetoothException("");
                    }

                }

                var uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");

                mBluetoothSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);
                await mBluetoothSocket.ConnectAsync();

                mIsConnected = mBluetoothSocket.IsConnected;
                if (mIsConnected)
                {
                    mDeviceName = device.Name;
                    await Task.Run(TalkToDevice);
                }

                // 未知道下面这些 State 有何作用
                var a2dp = adapter.GetProfileConnectionState(ProfileType.A2dp);
                var gatt = adapter.GetProfileConnectionState(ProfileType.Gatt);
                var gattserver = adapter.GetProfileConnectionState(ProfileType.GattServer);
                var headset = adapter.GetProfileConnectionState(ProfileType.Headset);
                var health = adapter.GetProfileConnectionState(ProfileType.Health);
                var sap = adapter.GetProfileConnectionState(ProfileType.Sap);

                return mIsConnected;
            }

            return false;
        }

        private Task TalkToDevice()
        {
            // check _socket.IsConnected, read w/InputStreamReader, BufferedReader
            return Task.CompletedTask;
        }

        #endregion

        #region 断开蓝牙连接

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

        #endregion

        public override void OnReceive(Context context, Intent intent)
        {
            switch (intent.Action)
            {
                case BluetoothDevice.ActionFound: // 搜索到的蓝牙设备
                    bluetoothDeviceFound(intent);
                    break;
                case BluetoothAdapter.ActionStateChanged:
                    string toDO = string.Empty;
                    break;
            }
        }

        private void bluetoothDeviceFound(Intent intent)
        {
            BluetoothDevice bluetoothDevice = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

            if (mUnbondDevicesList.Exists(i => i.Address == bluetoothDevice.Address) == true)
            {
                return; // 已存在
            }

            mUnbondDevicesList.Add(bluetoothDevice);

            mUnbondDeviceInfoList.Add
            (
                new Util.XamariN.BluetoothDeviceInfo
                (
                    name: bluetoothDevice.Name,
                    address: bluetoothDevice.Address,
                    bluetoothDeviceType: (int)bluetoothDevice.Type,
                    bluetoothBondState: (int)bluetoothDevice.BondState
                )
            );
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

        public void OnServiceConnected(ProfileType profile, IBluetoothProfile proxy)
        {
            string msg = "Profile Type : {0}".FormatWith(profile.ToString());
            System.Diagnostics.Debug.WriteLine(msg);

            foreach (BluetoothDevice item in proxy.ConnectedDevices)
            {
                msg = "Connected Device Name:{0}".FormatWith(item.Name);
                System.Diagnostics.Debug.WriteLine(msg);
                
                var args = new Common.BLuetoothEventArgs(new BluetoothDeviceInfo(item.Name, item.Address, (int)item.Type, (int)item.BondState) { IsConnected = true });
                Client.Common.Bluetooth.OnUpdateInfo(args);
            }
        }

        public void OnServiceDisconnected(ProfileType profile)
        {

        }
    }
}