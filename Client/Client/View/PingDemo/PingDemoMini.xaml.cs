using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PingDemoMini : ContentPage
    {
        System.Net.NetworkInformation.Ping mPing { get; set; }

        PingDemoViewModel ViewModel { get; set; }

        BackgroundWorker mBgWorker { get; set; }

        Random rand = new Random();

        List<Color> colorList = new List<Color>()
        {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Purple,
        };

        public PingDemoMini()
        {
            InitializeComponent();
            mPing = new System.Net.NetworkInformation.Ping();

            this.ViewModel = new PingDemoViewModel();
            this.BindingContext = this.ViewModel;

            initEvent();
            initData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.Screen.ScreenKeepOn = true;
            App.Screen.ForcePortrait();
        }

        protected override void OnDisappearing()
        {
            App.Screen.ScreenKeepOn = false;
            App.Screen.Unspecified();

            base.OnDisappearing();
        }

        private void initEvent()
        {
            this.btnPingTest.Clicked += BtnPingTest_Clicked;
            this.btnPingTestStop.Clicked += BtnPingTestStop_Clicked;

            TapGestureRecognizer tapShowHidden = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 2,
                Command = new Command(() =>
                {
                    this.BtnPingTestStop_Clicked(this.btnPingTestStop, null);
                })
            };
            this.gPingDisplay.GestureRecognizers.Add(tapShowHidden);
        }

        private void BtnPingTestStop_Clicked(object sender, EventArgs e)
        {
            mContinue = false;
        }

        private void initData()
        {
            this.txtIP.Text = "192.168.1.1";
        }

        async void BtnPingTest_Clicked(object sender, EventArgs e)
        {
            if (mBgWorker != null && mBgWorker.IsBusy == true)
            {
                await DisplayAlert("警告", "mBgWorker.IsBusy == true", "确定");
                return;
            }

            if (mBgWorker == null)
            {
                mBgWorker = new BackgroundWorker();
                mBgWorker.WorkerReportsProgress = true;
                mBgWorker.DoWork += Bg_DoWork;
                mBgWorker.ProgressChanged += Bg_ProgressChanged;
                mBgWorker.RunWorkerCompleted += Bg_RunWorkerCompleted;
            }

            mContinue = true;

            PingReplyModel toAdd = new PingReplyModel();
            toAdd.Foreground = "Green";
            toAdd.Content = "开始ping";
            this.ViewModel.Result.Add(toAdd);

            this.btnPingTest.IsEnabled = false;
            this.btnPingTestStop.IsEnabled = true;
            this.gPingDisplay.IsVisible = true;

            mBgWorker.RunWorkerAsync();
        }

        private bool mContinue { get; set; }
        private System.Net.NetworkInformation.PingReply mReply { get; set; }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            string ipOrAddress = this.txtIP.Text;
            while (mContinue)
            {
                mReply = mPing.Send(ipOrAddress, 5000, new byte[32]);
                mBgWorker.ReportProgress(200);
                System.Threading.Thread.Sleep(1000);
            }
        }


        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (mReply != null)
            {
                PingReplyModel toAdd = new PingReplyModel();
                try
                {
                    if (mReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        toAdd.Foreground = "Transparent";
                        toAdd.Content = "来自 {0} 的回复: 字节={1} 时间={2}".FormatWith(mReply.Address.ToString(), mReply.Buffer.Length, mReply.RoundtripTime);
                    }
                    else
                    {
                        toAdd.Foreground = "Red";
                        toAdd.Content = mReply.Status.ToString();
                    }
                }
                catch (Exception ex)
                {
                    toAdd.Foreground = "Red";
                    toAdd.Content = ex.GetFullInfo();
                }

                string msg = "{0} - {1}".FormatWith(toAdd.Content, DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(msg);
                this.txtLastStatus.Text = msg;

                Grid.SetRow(this.txtLastStatus, rand.Next(colorList.Count));
                this.gPingDisplay.BackgroundColor = this.colorList[rand.Next(colorList.Count)];

                //this.ViewModel.Result.Add(toAdd);
                //this.lv.ScrollTo(toAdd, ScrollToPosition.Start, true);
            }
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnPingTest.IsEnabled = true;
            this.btnPingTestStop.IsEnabled = false;
            this.gPingDisplay.IsVisible = false;

            if (e.Error != null)
            {
                string msg = "{0}".FormatWith(e.Error.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                DisplayAlert("捕获异常", e.Error.GetFullInfo(), "确定");
            }

            PingReplyModel toAdd = new PingReplyModel();
            toAdd.Foreground = "Red";
            toAdd.Content = "结束ping";
            this.ViewModel.Result.Add(toAdd);
        }

    }
}