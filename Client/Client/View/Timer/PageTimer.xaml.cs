using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTimer : ContentPage
    {
        bool mBreak = false;

        PageTimerViewModel ViewModel { get; set; }

        public PageTimer()
        {
            InitializeComponent();
            this.ViewModel = new PageTimerViewModel();
            this.BindingContext = this.ViewModel;

            initEvent();
        }

        private void initEvent()
        {
            this.btnStart.Clicked += btnStart_Clicked;
            this.btnStop.Clicked += btnStop_Clicked;

            this.btnModeDefault.Clicked += (s, e) => 
            {
                this.ViewModel.TypeA = 30;
                this.ViewModel.TypeB = 10;
                this.ViewModel.PlanCount = 1;
                this.ViewModel.IsVibrate = true;
                this.ViewModel.IsTTS = true;
                this.ViewModel.IsScreenKeepOn = true;
            };

            this.btnModeProtectEyes.Clicked += (s, e) =>
            {
                this.ViewModel.TypeA = 30;
                this.ViewModel.TypeB = 30;
                this.ViewModel.PlanCount = 6;
                this.ViewModel.IsVibrate = true;
                this.ViewModel.IsTTS = true;
                this.ViewModel.IsScreenKeepOn = true;
            };

            this.btnModeKeepFit.Clicked += (s, e) =>
            {
                this.ViewModel.TypeA = 5;
                this.ViewModel.TypeB = 7;
                this.ViewModel.PlanCount = -1;
                this.ViewModel.IsVibrate = true;
                this.ViewModel.IsTTS = true;
                this.ViewModel.IsScreenKeepOn = true;
            };
        }

        protected override bool OnBackButtonPressed()
        {
            if (mBgWorker != null)
            {
                return true;
            }

            if (mBgWorker != null && mBgWorker.IsBusy == true)
            {
                return true;
            }

            return false;
        }

        #region 计时器逻辑

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            start();
        }        

        System.ComponentModel.BackgroundWorker mBgWorker { get; set; }

        private void start()
        {
            if (mBgWorker != null
                // && mBgWorker.IsBusy == true
                )
            {
                return;
            }

            mBgWorker = new System.ComponentModel.BackgroundWorker();
            mBgWorker.DoWork += (bgSender, bgArgs) =>
            {
                var bgWorker = bgSender as System.ComponentModel.BackgroundWorker;
                object[] args = bgArgs.Argument as object[];
                int typeA = Convert.ToInt32(args[0]);
                int typeB = Convert.ToInt32(args[1]);
                int planCount = Convert.ToInt32(args[2]);

                if (planCount >= 0)
                {
                    for (int i = 0; i < planCount; i++)
                    {
                        for (int countUpA = 0; countUpA < typeA; countUpA++)
                        {
                            if (mBreak == true) { break; }

                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                            bgWorker.ReportProgress(countUpA + 1);

                            if (mBreak == true) { break; }
                        }

                        if (mBreak == true) { break; }

                        for (int countUpB = 0; countUpB < typeB; countUpB++)
                        {
                            if (mBreak == true) { break; }

                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                            bgWorker.ReportProgress(-(countUpB + 1));

                            if (mBreak == true) { break; }
                        }
                    }
                }
                else
                {
                    while (true)
                    {
                        for (int countUpA = 0; countUpA < typeA; countUpA++)
                        {
                            if (mBreak == true) { break; }

                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                            bgWorker.ReportProgress(countUpA + 1);

                            if (mBreak == true) { break; }
                        }

                        if (mBreak == true) { break; }

                        for (int countUpB = 0; countUpB < typeB; countUpB++)
                        {
                            if (mBreak == true) { break; }

                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                            bgWorker.ReportProgress(-(countUpB + 1));

                            if (mBreak == true) { break; }
                        }
                    }
                }
            };

            mBgWorker.RunWorkerCompleted += async (bgSender, bgResult) =>
            {
                if (bgResult.Error != null)
                {
                    await DisplayAlert(title: "捕获错误", message: bgResult.Error.GetFullInfo(), cancel: "确定");
                }
                else
                {
                    gMain.IsVisible = false;
                    App.Screen.ScreenKeepOn = false;
                    mBgWorker = null;
                }
            };

            mBgWorker.WorkerReportsProgress = true;
            mBgWorker.ProgressChanged += (bgSender, bgArgs) =>
            {
                if (bgArgs.ProgressPercentage > 0) // TypeA
                {
                    gMain.BackgroundColor = Color.Orange;
                    lblStatus.Text = "进行中...";
                    lblInfo.Text = TimeSpan.FromSeconds(bgArgs.ProgressPercentage).ToStringAdv();

                    if (this.ViewModel.TypeA - bgArgs.ProgressPercentage == 5)
                    {
                        if (this.ViewModel.IsTTS == true)
                        {
                            App.TTS.Play("还剩五秒");
                        }
                    }

                    if (this.ViewModel.TypeA - bgArgs.ProgressPercentage == 0)
                    {
                        if (this.ViewModel.IsVibrate == true)
                        {
                            Xamarin.Essentials.Vibration.Vibrate();
                        }
                    }
                }
                else // TypeB
                {
                    gMain.BackgroundColor = Color.SkyBlue;
                    lblStatus.Text = "休息中...";
                    lblInfo.Text = TimeSpan.FromSeconds(-bgArgs.ProgressPercentage).ToStringAdv();

                    if (this.ViewModel.TypeB - (-(bgArgs.ProgressPercentage)) == 5)
                    {
                        if (this.ViewModel.IsTTS == true)
                        {
                            App.TTS.Play("还剩五秒");
                        }
                    }

                    if (this.ViewModel.TypeB - (-(bgArgs.ProgressPercentage)) == 0)
                    {
                        if (this.ViewModel.IsVibrate == true)
                        {
                            Xamarin.Essentials.Vibration.Vibrate();
                        }
                    }
                }
            };

            mBreak = false;
            gMain.IsVisible = true;

            if (this.ViewModel.IsScreenKeepOn == true)
            {
                App.Screen.ScreenKeepOn = true;
            }
            else
            {
                App.Screen.ScreenKeepOn = false;
            }


            mBgWorker.RunWorkerAsync(new object[]
            {
                this.ViewModel.TypeA,
                this.ViewModel.TypeB,
                this.ViewModel.PlanCount
            });
        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            if (mBgWorker == null)
            {
                return;
            }

            mBreak = true;
        }

        #endregion
    }

    public class PageTimerViewModel : ViewModel.BaseViewModel
    {
        private int _TypeA = 30;
        public int TypeA
        {
            get { return _TypeA; }
            set
            {
                _TypeA = value;
                this.OnPropertyChanged("TypeA");
            }
        }

        private int _TypeB = 30;
        public int TypeB
        {
            get { return _TypeB; }
            set
            {
                _TypeB = value;
                this.OnPropertyChanged("TypeB");
            }
        }

        public int _PlanCount = 1;
        public int PlanCount
        {
            get { return _PlanCount; }
            set
            {
                _PlanCount = value;
                this.OnPropertyChanged("PlanCount");
            }
        }

        public bool _IsVibrate = true;
        public bool IsVibrate
        {
            get { return _IsVibrate; }
            set
            {
                _IsVibrate = value;
                this.OnPropertyChanged("IsVibrate");
            }
        }

        public bool _IsTTS = true;
        public bool IsTTS
        {
            get { return _IsTTS; }
            set
            {
                _IsTTS = value;
                this.OnPropertyChanged("IsTTS");
            }
        }

        protected bool _IsScreenKeepOn = true;
        public bool IsScreenKeepOn
        {
            get { return _IsScreenKeepOn; }
            set
            {
                _IsScreenKeepOn = value;
                this.OnPropertyChanged("IsScreenKeepOn");
            }
        }
    }
}