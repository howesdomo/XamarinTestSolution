using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.FakeSerialPort
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageFakeSerialPort : ContentPage
    {
        private PageFakeSerialPort_ViewModel ViewModel { get; set; }

        //定义Socket对象
        Socket mClientSocket;

        //创建接收消息的线程
        Thread mThreadReceive;

        //接收服务端发送的数据
        string str;


        public PageFakeSerialPort()
        {
            InitializeComponent();
            initEvent();
            this.ViewModel = new PageFakeSerialPort_ViewModel();
            this.BindingContext = this.ViewModel;
        }

        private void initEvent()
        {
            this.btnStart.Clicked += BtnStart_Clicked;
            this.btnStop.Clicked += BtnStop_Clicked;
            this.btnScan.Clicked += BtnSend_Clicked;
        }

        async void BtnStart_Clicked(object sender, EventArgs e)

        {
            IPAddress ip = IPAddress.Parse(this.txtIP.Text.Trim());
            mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //连接服务端
                mClientSocket.Connect(ip, Convert.ToInt32(this.txtPort.Text.Trim()));
                //开启线程不停的接收服务端发送的数据
                mThreadReceive = new Thread(new ThreadStart(Receive));
                mThreadReceive.IsBackground = true;
                mThreadReceive.Start();

                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "连接服务器成功。", Foreground = "Green" });
            }
            catch (Exception ex)
            {
                mClientSocket = null;
                mThreadReceive = null;

                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "连接服务器失败。", Foreground = "Red" });
                await DisplayAlert("连接服务器失败", ex.GetFullInfo(), "确定");
            }
        }

        //接收服务端消息的线程方法
        async void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] buff = new byte[20000];
                    int r = mClientSocket.Receive(buff);
                    str = Encoding.Default.GetString(buff, 0, r);                    
                    Device.BeginInvokeOnMainThread(new Action(() =>
                    {
                        this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "接收", Content = str, Foreground = "Silver" });
                        // this.txtReceive.Text += "\r\n{0}".FormatWith(str);
                    }));
                }
            }
            catch (Exception ex)
            {
                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "接收服务器信息失败。", Foreground = "Red" });
                await DisplayAlert("接收服务器信息失败", ex.GetFullInfo(), "确定");
            }
        }

        async void BtnStop_Clicked(object sender, EventArgs e)
        {
            if (mClientSocket == null || mThreadReceive == null)
            {
                await DisplayAlert("Error", "未连接服务器", "确定");
                return;
            }

            try
            {
                mClientSocket.Close();
                mThreadReceive.Abort();
            }
            catch (Exception ex)
            {
                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "停止连接服务器失败。", Foreground = "Red" });
                await DisplayAlert("停止连接服务器失败", ex.GetFullInfo(), "确定");
            }
        }

        async void BtnSend_Clicked(object sender, EventArgs e)
        {
            if (mClientSocket == null || mThreadReceive == null)
            {
                await DisplayAlert("Error", "请连接服务器", "确定");
                return;
            }

            var page = new Common.ZXingBarcodeScanner
            (
                title: "模拟串口助手",
                isScanContinuously: true,
                scanResultHandle: scanResultHandle
            );

            await Navigation.PushAsync(page);
        }

        async void scanResultHandle(ZXing.Result result, Common.ZXingBarcodeScanner page)
        {
            string msg = "{0}".FormatWith(Util.JsonUtils.SerializeObject(result));
            System.Diagnostics.Debug.WriteLine(msg);

            App.AudioPlayer.PlayBeep();

            // Send to Server
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(result.Text);
                mClientSocket.Send(buffer);

                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "发送", Content = str, Foreground = "Silver" });
            }
            catch (Exception ex)
            {
                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "发送信息到服务器失败。", Foreground = "Red" });
                await DisplayAlert("发送信息到服务器失败", ex.GetFullInfo(), "确定");
            }

            Thread.Sleep(1000);
            page.endWith();
        }

    }

    public class PageFakeSerialPort_ViewModel : ViewModel.BaseViewModel
    {
        public PageFakeSerialPort_ViewModel()
        {
            Result = new ObservableCollection<FakeSerialPortModel>();
        }

        private ObservableCollection<FakeSerialPortModel> _Result;

        public ObservableCollection<FakeSerialPortModel> Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
                if (_Result != null)
                {
                    _Result.CollectionChanged += _Result_CollectionChanged;
                }
                this.OnPropertyChanged("Result");
            }
        }

        private void _Result_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged("Result");
        }
    }

    public class FakeSerialPortModel
    {
        public string MsgType { get; set; }

        public string Content { get; set; }

        public string Foreground { get; set; }
    }
}