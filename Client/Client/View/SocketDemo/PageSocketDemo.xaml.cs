using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.SocketDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSocketDemo : ContentPage
    {
        //定义Socket对象
        Socket clientSocket;

        //创建接收消息的线程
        Thread threadReceive;

        //接收服务端发送的数据
        string str;


        public PageSocketDemo()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnStart.Clicked += BtnStart_Clicked;
            this.btnStop.Clicked += BtnStop_Clicked;
            this.btnSend.Clicked += BtnSend_Clicked;
        }



        async void BtnStart_Clicked(object sender, EventArgs e)

        {
            IPAddress ip = IPAddress.Parse(this.txtIP.Text.Trim());
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //连接服务端
                clientSocket.Connect(ip, Convert.ToInt32(this.txtPort.Text.Trim()));
                //开启线程不停的接收服务端发送的数据
                threadReceive = new Thread(new ThreadStart(Receive));
                threadReceive.IsBackground = true;
                threadReceive.Start();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.GetFullInfo(), "确定");
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
                    int r = clientSocket.Receive(buff);
                    str = Encoding.Default.GetString(buff, 0, r);
                    // this.Dispatcher.Invoke(new Action(() => { this.txtReceive.Text += "\r\n{0}".FormatWith(str); }));
                    Device.BeginInvokeOnMainThread(new Action(() => { this.txtReceive.Text += "\r\n{0}".FormatWith(str); }));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.GetFullInfo(), "确定");
            }
        }

        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            //clientSocket关闭
            clientSocket.Close();
            //threadReceive关闭
            threadReceive.Abort();
        }

        async void BtnSend_Clicked(object sender, EventArgs e)
        {
            try
            {
                string strMsg = this.txtToSend.Text.Trim();
                byte[] buffer = Encoding.Default.GetBytes(strMsg);
                clientSocket.Send(buffer);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.GetFullInfo(), "确定");
            }
        }



    }
}