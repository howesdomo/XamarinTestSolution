using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.MarqueeDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarqueeLabel_V4 : ContentPage
    {
        public MarqueeLabel_V4()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnScroll.Clicked += BtnScroll_Clicked;
        }

        BackgroundWorker mBgWorker { get; set; }

        private void BtnScroll_Clicked(object sender, EventArgs e)
        {
            if (mBgWorker != null && mBgWorker.IsBusy == true)
            {
                return;
            }

            mBgWorker = new BackgroundWorker();
            mBgWorker.WorkerReportsProgress = true;
            mBgWorker.DoWork += mBgWorker_DoWork;
            mBgWorker.ProgressChanged += mBgWorker_ProgressChanged;
            mBgWorker.RunWorkerCompleted += mBgWorker_RunWorkerCompleted;

            index = 0;
            mScrollX = 0;
            mScrollX_Before = null;
            mIsContinue = true;

            mBgWorker.RunWorkerAsync();
        }

        private int index = 0;       
        private double mScrollX = 0;
        private double? mScrollX_Before = null;

        private bool mIsContinue = true;

        private void mBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (mIsContinue)
            {
                if (mScrollX_Before.HasValue == true && mScrollX <= mScrollX_Before.Value)
                {
                    mIsContinue = false;
                }
                else
                {
                    index = index + 1;
                    mScrollX_Before = mScrollX;
                    mBgWorker.ReportProgress(index);
                    System.Threading.Thread.Sleep(500);
                    mScrollX = scrollV.ScrollX;
                }
            }
        }

        private void mBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                string msg = "ProgressPercentage: {0}".FormatWith(e.ProgressPercentage);
                System.Diagnostics.Debug.WriteLine(msg);

                // var argsX = e.ProgressPercentage * 70;
                var argsX = e.ProgressPercentage * 40;
                scrollV.ScrollToAsync(x: argsX, y: 0, animated: true);
            });
        }

        private void mBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string msg = "{0}".FormatWith("mBgWorker_RunWorkerCompleted");
            System.Diagnostics.Debug.WriteLine(msg);

            // 隐藏界面, 回滚到 0 号位, 重新执行


            Device.BeginInvokeOnMainThread(() =>
            {
                scrollV.ScrollToAsync(0, 0, true);
                System.Threading.Thread.Sleep(4000);
                BtnScroll_Clicked(null, null);
            });

        }

    }
}