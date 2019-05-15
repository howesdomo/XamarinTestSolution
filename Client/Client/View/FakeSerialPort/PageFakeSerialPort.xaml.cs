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

        // 定义Socket对象
        TcpClient mTcpClient { get; set; }

        // 创建接收消息的线程
        Task mReceiveTask { get; set; }

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
            this.btnStart.IsEnabled = false;
            try
            {
                IPAddress ip = IPAddress.Parse(this.txtIP.Text.TrimAdv());
                int port = Convert.ToInt32(this.txtPort.Text.TrimAdv());

                // 连接服务端
                mTcpClient = new TcpClient();
                mTcpClient.Connect(ip, port); // 开始侦听

                // 开启线程不停的接收服务端发送的数据
                mReceiveTask = new Task(() => Receive());
                mReceiveTask.Start();

                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "连接服务器成功。", Foreground = "Green" });

                this.btnStop.IsEnabled = true;
                this.btnScan.IsEnabled = true;
            }
            catch (Exception ex)
            {
                // obj
                mTcpClient = null;
                mReceiveTask = null;

                // UI
                this.btnStart.IsEnabled = true;

                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "连接服务器失败。", Foreground = "Red" });
                await DisplayAlert("连接服务器失败", ex.GetFullInfo(), "确定");
            }
        }

        //接收服务端消息的线程方法
        async void Receive()
        {
            while (true)
            {
                try
                {
                    string str = mTcpClient.Receive();
                    Device.BeginInvokeOnMainThread(new Action(() =>
                    {
                        this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "接收", Content = str, Foreground = "Silver" });
                    }));
                }
                catch (System.IO.IOException ioException)
                {
                    if (mTcpClient.Connected == false)
                    {
                        break;
                    }

                    string msg = "{0}".FormatWith(ioException.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg);

                    throw ioException;
                }
                catch (Exception ex)
                {
                    this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "接收服务器信息失败。", Foreground = "Red" });
                    await DisplayAlert("接收服务器信息失败", ex.GetFullInfo(), "确定");
                }
            }
        }

        async void BtnStop_Clicked(object sender, EventArgs e)
        {
            this.btnStop.IsEnabled = false;
            try
            {
                mTcpClient.Close();

                this.btnStart.IsEnabled = true;
                this.btnScan.IsEnabled = false;

                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "停止连接服务器。", Foreground = "Green" });
            }
            catch (Exception ex)
            {
                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "停止连接服务器失败。", Foreground = "Red" });
                await DisplayAlert("停止连接服务器失败", ex.GetFullInfo(), "确定");
                this.btnStop.IsEnabled = true;
            }
        }

        async void BtnSend_Clicked(object sender, EventArgs e)
        {
            if (mTcpClient == null || mReceiveTask == null)
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
            try
            {
                string str = result.Text;
                mTcpClient.Send(result.Text);

                var toAdd = new FakeSerialPortModel() { MsgType = "发送", Content = str, Foreground = "Silver" };
                this.ViewModel.Result.Add(toAdd);

                App.AudioPlayer.PlayBeep();
                Thread.Sleep(500);
                await DisplayAlert("成功发送", str, "确定");

                Device.BeginInvokeOnMainThread(() =>
                {
                    lv.ScrollTo(toAdd, ScrollToPosition.End, false);
                });
            }
            catch (Exception ex)
            {
                this.ViewModel.Result.Add(new FakeSerialPortModel() { MsgType = "系统", Content = "发送信息到服务器失败。", Foreground = "Red" });

                App.AudioPlayer.PlayError();
                Thread.Sleep(500);
                await DisplayAlert("发送信息到服务器失败", ex.GetFullInfo(), "确定");
            }

            page.EndWith();
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