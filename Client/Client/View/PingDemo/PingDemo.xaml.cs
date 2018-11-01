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
    public partial class PingDemo : ContentPage
    {
        System.Net.NetworkInformation.Ping mPing { get; set; }
        PingDemoViewModel ViewModel { get; set; }

        BackgroundWorker mBgWorker { get; set; }

        public PingDemo()
        {
            InitializeComponent();
            mPing = new System.Net.NetworkInformation.Ping();

            this.ViewModel = new PingDemoViewModel();
            this.BindingContext = this.ViewModel;

            initEvent();
            initData();
        }

        private void initEvent()
        {
            this.btnPingTest.Clicked += BtnPingTest_Clicked;
            this.btnPingTestStop.Clicked += BtnPingTestStop_Clicked;
        }

        private void BtnPingTestStop_Clicked(object sender, EventArgs e)
        {
            mContinue = false;
        }

        private void initData()
        {
            this.txtIP.Text = "app.enpot.com.cn";
            // this.txtIP.Text = "47.96.14.178";
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

            this.ViewModel.Result.Clear();

            this.btnPingTest.IsEnabled = false;
            this.btnPingTestStop.IsEnabled = true;
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
                System.Threading.Thread.Sleep(200);
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

                string msg = "{0}".FormatWith(toAdd.Content);
                System.Diagnostics.Debug.WriteLine(msg);
                this.ViewModel.Result.Add(toAdd);

                this.lv.ScrollTo(toAdd, ScrollToPosition.Start, true);
            }            
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnPingTest.IsEnabled = true;
            this.btnPingTestStop.IsEnabled = false;

            if (e.Error != null)
            {
                string msg = "{0}".FormatWith(e.Error.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

    }

    public class PingDemoViewModel : ViewModel.BaseViewModel
    {
        public PingDemoViewModel()
        {
            Result = new ObservableCollection<PingReplyModel>();
        }

        private ObservableCollection<PingReplyModel> _Result;

        public ObservableCollection<PingReplyModel> Result
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

    public class PingReplyModel
    {
        public string Content { get; set; }

        public string Foreground { get; set; }
    }
}