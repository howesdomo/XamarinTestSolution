using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.Games.CRW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMain : ContentPage
    {
        public static string Tag
        {
            get
            {
                return "CRW_PageMain";
            }
        }

        CRWBll mBll { get; set; }

        PageMainViewModel ViewModel { get; set; }

        bool mStop { get; set; }

        bool mQuit { get; set; }

        public PageMain(int selectedCRWTypeID)
        {
            InitializeComponent();
            initUI();

            this.mBll = new CRWBll();
            this.initEvent();
            this.ViewModel = new PageMainViewModel();
            this.BindingContext = this.ViewModel;

            this.ViewModel.CRWTypeID = selectedCRWTypeID;

            readLevel();
            calcQuestion();
            showStopWatch();
        }

        #region UI        

        private void initUI()
        {
            // Xamarin.Forms 版本 3.0.0.530893
            // 由于在XAML中设置 Margin 会导致编译时报错
            // 故将部分的 Margin 设置写在C#代码中
            this.gRight.Margin = new Thickness(left: -10d, top: 0d, right: 0d, bottom: 0d);

            this.lblLevelName.Margin = new Thickness(left: 10d, top: 0d, right: 0d, bottom: 0d);

            this.lblNextLevel.Margin = new Thickness(left: 10d, top: 0d, right: 10d, bottom: 0d);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string msg = "{0}".FormatWith("PageMain OnAppearing");
            System.Diagnostics.Debug.WriteLine(msg);
            App.Output.Info(Tag, msg);

            Device.BeginInvokeOnMainThread(() =>
            {
                // TODO 设置常亮
                // 暂时无法使用 Xamarin.Essentials.ScreenLock.RequestActive 来设置屏幕常亮
                App.ScreenDirection.ForceLandscape();

                gameContinue();
            });
        }

        private void gameContinue()
        {
            mStop = false;

            switch (mCurrentStep)
            {
                case 0:
                    {
                        if (mBGWorker_CalcTimeout != null && mBGWorker_CalcTimeout.IsBusy == false)
                        {
                            string msg = "(游戏继续) currentStep:{0}".FormatWith(mCurrentStep);
                            System.Diagnostics.Debug.WriteLine(msg);
                            App.Output.Info(Tag, msg);

                            mBGWorker_CalcTimeout_RunWorkerCompleted(null, new System.ComponentModel.RunWorkerCompletedEventArgs(result: null, error: null, cancelled: false));
                        }
                    };
                    break;
                case 3:
                    {
                        if (mBGWorker_WaitNextLevel != null && mBGWorker_WaitNextLevel.IsBusy == false)
                        {
                            string msg = "(游戏继续) currentStep:{0}".FormatWith(mCurrentStep);
                            System.Diagnostics.Debug.WriteLine(msg);
                            App.Output.Info(Tag, msg);

                            mBGWorker_WaitNextLevel_RunWorkerCompleted(null, null);
                        }
                    }
                    break;
            }
        }

        protected override void OnDisappearing()
        {
            string msg = "{0}".FormatWith("PageMain OnDisappearing");
            System.Diagnostics.Debug.WriteLine(msg);
            App.Output.Info(Tag, msg);

            if (mQuit)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.ScreenDirection.Unspecified();
                });
            }

            mStop = true;
            base.OnDisappearing();
        }

        #endregion

        private void initEvent()
        {
            this.btnGiveUp.Clicked += BtnGiveUp_Clicked;
            this.gCRWKeyboard.InputValueEvent += new EventHandler<CRW_Keyboard_EventArgs>(receiveUserAnswer);

            this.btnNextLevel.Clicked += BtnNextLevel_Clicked;
        }

        protected override bool OnBackButtonPressed()
        {
            showCloseDisplayAlert();
            return true;
        }

        async void showCloseDisplayAlert()
        {
            mStop = true;

            var result = await this.DisplayAlert
            (
                title: "提示",
                message: "确认退出？",
                accept: "确认",
                cancel: "取消"
            );

            if (result)
            {
                quitGame();
            }
            else
            {
                gameContinue();
            }
        }

        private void quitGame()
        {
            mStop = true;
            mQuit = true;

            string msg = "(退出游戏)";
            System.Diagnostics.Debug.WriteLine(msg);
            App.Output.Info(Tag, msg);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync(true);
            });
        }

        /// <summary>
        /// 回答问题中 - 0
        /// 等待评级中 - 3
        /// </summary>
        int mCurrentStep { get; set; }

        #region Level

        //void changeLevelForTest()
        //{
        //    this.ViewModel.CurrentIndex = null;
        //    readLevel();
        //    calcQuestion();
        //}

        private void readLevel()
        {
            CRWLog log = Client.Common.StaticInfo.ExternalSQLiteDB.CRW_rLog(PageGamesList.Game_User, this.ViewModel.CRWTypeID);
            if (log.NextLevel.HasValue)
            {
                var now = DateTime.Now;
                var today = now.Date;

                var toAdd = new CRWLog()
                {
                    UserID = log.UserID,
                    CRWTypeID = this.ViewModel.CRWTypeID,
                    Level = log.NextLevel.Value,
                    DateValue = today.Ticks,
                    DateDisplay = today.ToString("yyyy-MM-dd"),
                    UpdateTimeValue = now.Ticks,
                    UpdateTimeDisplay = now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    Percentage = null,
                    NextLevel = null,
                    UseTime = null,
                    UseTimeDisplay = string.Empty
                };

                Client.Common.StaticInfo.ExternalSQLiteDB.CRW_cLog(toAdd);

                // 再次获取
                log = Client.Common.StaticInfo.ExternalSQLiteDB.CRW_rLog(PageGamesList.Game_User, this.ViewModel.CRWTypeID);
            }           

            CRW_Level level = new CRW_Level(log.Level, this.ViewModel.CRWTypeID);
            this.ViewModel.Level = level;

            #region 听力溯算 根据难度调整语速

            if (
                this.ViewModel.CRWTypeID == 2 &&
                this.ViewModel.Level != null
               )
            {
                App.TTS.SetSpeechRateSilent(this.ViewModel.Level.SpeechRate);
            }

            #endregion
        }

        #endregion

        #region Question

        private void calcQuestion()
        {
            // 根据当前等级计算出题目
            this.ViewModel.QuestionList = mBll.GetCRW_QuestionByList(this.ViewModel.Level);
        }

        void BtnGiveUp_Clicked(object sender, EventArgs e)
        {
            readNextQuestion();
        }

        private void readNextQuestion()
        {
            var r = mBll.ReadNextQuestion(this.ViewModel.CurrentIndex, this.ViewModel.Level, this.ViewModel.QuestionList);

            this.ViewModel.RememberQuestion = r.Item2;
            this.ViewModel.AnswerQuestion = r.Item3;

            #region 听力溯算 播放题目内容

            if (
                this.ViewModel.CRWTypeID == 2 &&
                this.ViewModel.RememberQuestion != null &&
                this.ViewModel.RememberQuestion.TTSMsg.IsNullOrWhiteSpace() == false
               )
            {
                App.TTS.Play(this.ViewModel.RememberQuestion.TTSMsg);
            }

            #endregion

            #region 答题完毕

            if (this.ViewModel.RememberQuestion == null && this.ViewModel.AnswerQuestion == null)
            {
                this.ViewModel.swCRW_UseTime.Stop();

                this.ViewModel.CurrentIndex = null;

                decimal correctPercentage = mBll.CheckCorrectPercentage(this.ViewModel.QuestionList);
                var result = mBll.CalcLevel(this.ViewModel.Level, this.ViewModel.QuestionList);

                System.Diagnostics.Debug.WriteLine(result.Item2);
                App.Output.Info(Tag, result.Item2);

                var lastestLog = Client.Common.StaticInfo.ExternalSQLiteDB.CRW_rLog(PageGamesList.Game_User, this.ViewModel.CRWTypeID);
                lastestLog.NextLevel = result.Item1.LevelNo;
                lastestLog.Percentage = Convert.ToInt32(correctPercentage);
                lastestLog.UseTime = this.ViewModel.swCRW_UseTime.ElapsedTicks;
                lastestLog.UseTimeDisplay = this.ViewModel.CRW_UseTimeInfo;

                Client.Common.StaticInfo.ExternalSQLiteDB.CRW_uLog(lastestLog);

                // 设置新的等级, 计算新的题目
                this.ViewModel.Level = result.Item1;
                this.calcQuestion();
                // 播放检测正确率动画, 播放完毕后执行, readNextQuestion()

                var now = DateTime.Now;
                var today = now.Date;

                var toAdd = new CRWLog()
                {
                    UserID = PageGamesList.Game_User.ID,
                    CRWTypeID = 1,
                    Level = this.ViewModel.Level.LevelNo,
                    DateValue = today.Ticks,
                    DateDisplay = today.ToString("yyyy-MM-dd"),
                    UpdateTimeValue = now.Ticks,
                    UpdateTimeDisplay = now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    Percentage = null,
                    NextLevel = null,
                    UseTime = this.ViewModel.swCRW_UseTime.ElapsedTicks,
                    UseTimeDisplay = this.ViewModel.CRW_UseTimeInfo
                };

                Client.Common.StaticInfo.ExternalSQLiteDB.CRW_cLog(toAdd);

                this.playNextLevelVideo(result.Item2);
                return;
            }

            #endregion

            // 继续回答下一题
            mCurrentStep = 0;
            this.ViewModel.swCRW_UseTime.Start();
            calcTimeOut_BgWorker_Start();
            this.ViewModel.CurrentIndex = r.Item1;
        }

        #endregion

        private void receiveUserAnswer(object sender, CRW_Keyboard_EventArgs e)
        {
            validateUserAnswer(e.Value);
        }

        private void validateUserAnswer(int userResult)
        {
            if (IsPlayingAnswerVideo == true)
            {
                string msg = "{0}".FormatWith("正在播放动画(Answer), 忽略结果输入");
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Warn(Tag, msg);
                return;
            }

            if (IsWaitNextLevel == true)
            {
                string msg = "{0}".FormatWith("正在播放动画(Next Level), 忽略结果输入");
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Warn(Tag, msg);
                return;
            }

            if (this.ViewModel.AnswerQuestion == null)
            {
                // 未有可以回答的问题, 暂时处理为
                // 读取下一条问题
                readNextQuestion();
                return;
            }

            // 判断在答题时间内 
            // 在答案区域显示用户录入的值
            if (this.ViewModel.AnswerQuestion.Result != userResult)
            {
                this.ViewModel.AnswerQuestion.ResultInfo = userResult.ToString();
                this.ViewModel.AnswerQuestion.ChangeStatus(CRW_Question_Status.InputWrongAnswer);
                this.ViewModel.NotifiyAnswerQuestion();
            }
            else // 回答正确
            {
                this.ViewModel.AnswerQuestion.ChangeStatus(CRW_Question_Status.InputCorrectAnswer);
                this.ViewModel.NotifiyAnswerQuestion();

                string msg = "(正确回答）校验用户录入结果";
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Info(Tag, msg);

                calcTimeOut_BgWorker_Stop();
                playAnswerVideo();
            }
        }

        #region 播放显示答案动画

        System.ComponentModel.BackgroundWorker mBGWorker_PlayAnswerVideo { get; set; }

        /// <summary>
        /// 播放显示正确答案动画
        /// </summary>
        void playAnswerVideo()
        {
            if (mBGWorker_PlayAnswerVideo == null)
            {
                mBGWorker_PlayAnswerVideo = new System.ComponentModel.BackgroundWorker();
                mBGWorker_PlayAnswerVideo.DoWork += mBGWorker_PlayAnswerVideo_DoWork;
                mBGWorker_PlayAnswerVideo.RunWorkerCompleted += mBGWorker_PlayAnswerVideo_RunWorkerCompleted;
            }

            if (mBGWorker_PlayAnswerVideo.IsBusy == true)
            {
                string msg = "(警告)播放显示正确答案动画BgWorker正在工作中，有操作尝试再次进入，请程序员排除异常";
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Warn(Tag, msg);

                return;
            }

            mBGWorker_PlayAnswerVideo.RunWorkerAsync();
        }

        /// <summary>
        /// 判断是否正在播放动画
        /// </summary>
        bool IsPlayingAnswerVideo
        {
            get
            {
                if (mBGWorker_PlayAnswerVideo != null && mBGWorker_PlayAnswerVideo.IsBusy)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 录入正确答案后等待动画时间
        /// </summary>
        public const int mInputCorrectAnswerSleepTime = 1000;

        private void mBGWorker_PlayAnswerVideo_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(mInputCorrectAnswerSleepTime);
        }

        private void mBGWorker_PlayAnswerVideo_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            readNextQuestion();
        }

        #endregion

        #region 判断回答是否超时

        System.ComponentModel.BackgroundWorker mBGWorker_CalcTimeout { get; set; }

        private void calcTimeOut_BgWorker_Start()
        {
            if (mBGWorker_CalcTimeout == null)
            {
                mBGWorker_CalcTimeout = new System.ComponentModel.BackgroundWorker();
                mBGWorker_CalcTimeout.WorkerSupportsCancellation = true; // 设置允许取消Bgworker
                mBGWorker_CalcTimeout.DoWork += mBGWorker_CalcTimeout_DoWork;
                mBGWorker_CalcTimeout.RunWorkerCompleted += mBGWorker_CalcTimeout_RunWorkerCompleted;
            }

            if (mBGWorker_CalcTimeout.IsBusy == true)
            {
                string msg = "(警告)判断回答是否超时BgWorker正在工作中，有操作尝试再次进入，请程序员排除异常";
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Warn(Tag, msg);
                return;
            }

            mCalcTimeout_Cancel = false;
            mBGWorker_CalcTimeout.RunWorkerAsync();
        }

        bool mCalcTimeout_Cancel { get; set; }

        private void calcTimeOut_BgWorker_Stop()
        {
            mCalcTimeout_Cancel = true;
            mBGWorker_CalcTimeout.CancelAsync();
        }

        private void mBGWorker_CalcTimeout_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker thisBgWorker = sender as System.ComponentModel.BackgroundWorker;

            int argsWaitTime = this.ViewModel.Level.AnswerTime;
            string msg = "开始答题倒计时:{0}毫秒".FormatWith(argsWaitTime);
            System.Diagnostics.Debug.WriteLine(msg);
            App.Output.Info(Tag, msg);

            while (argsWaitTime >= 0)
            {
                System.Threading.Thread.Sleep(100);
                argsWaitTime -= 100;

                if (mCalcTimeout_Cancel == true)
                {
                    break;
                }
            }

            if (mCalcTimeout_Cancel == false)
            {
                msg = "停止答题倒计时:0毫秒".FormatWith(argsWaitTime);
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Info(Tag, msg);
            }
        }

        private void mBGWorker_CalcTimeout_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string msg = string.Empty;
            if (mStop == true)
            {
                msg = "(游戏暂停)CurrentStep:{0}".FormatWith(mCurrentStep);
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Info(Tag, msg);
                return;
            }

            if (e.Error != null)
            {
                msg = "{0}".FormatWith(e.Error.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Error(Tag, msg);

                this.DisplayAlert("错误", e.Error.Message, "确定");
                return;
            }


            if (mCalcTimeout_Cancel == true) // 由于正确回答, 计算超时线程被取消
            {
                msg = "（正确回答）取消答题倒计时";
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Info(Tag, msg);
            }
            else
            {
                if (this.ViewModel.AnswerQuestion == null)
                {
                    msg = "(等待回答)由于开始溯答, 自动跳到下一题";
                    System.Diagnostics.Debug.WriteLine(msg);
                    App.Output.Info(Tag, msg);
                    playAnswerVideo();
                    return;
                }

                msg = "(错误回答)由于未在规定时间内正确回答, 显示答案后自动跳到下一题";
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Info(Tag, msg);

                this.ViewModel.AnswerQuestion.ChangeStatus(CRW_Question_Status.TimeOutWrongAnswer);
                this.ViewModel.NotifiyAnswerQuestion();
                playAnswerVideo();
            }
        }

        #endregion

        #region 播放下一等级动画

        System.ComponentModel.BackgroundWorker mBGWorker_WaitNextLevel { get; set; }

        void playNextLevelVideo(string ttsContent)
        {
            if (mBGWorker_WaitNextLevel == null)
            {
                mBGWorker_WaitNextLevel = new System.ComponentModel.BackgroundWorker();
                mBGWorker_WaitNextLevel.DoWork += mBGWorker_WaitNextLevel_DoWork;
                mBGWorker_WaitNextLevel.RunWorkerCompleted += mBGWorker_WaitNextLevel_RunWorkerCompleted;
            }

            if (mBGWorker_WaitNextLevel.IsBusy == true)
            {
                string msg = "{0}".FormatWith("正在等待回答正确的BackgroundWorker结束");
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Warn(Tag, msg);

                return;
            }

            mCurrentStep = 3;

            gNextLevel.IsVisible = true;
            btnNextLevel.IsEnabled = false;
            lblNextLevel.Text = ttsContent;

            App.TTS.SetSpeechRateSilent(1f);
            App.TTS.Play(ttsContent);
            mBGWorker_WaitNextLevel.RunWorkerAsync();
        }

        bool IsWaitNextLevel
        {
            get
            {
                if (mBGWorker_WaitNextLevel != null && mBGWorker_WaitNextLevel.IsBusy)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void mBGWorker_WaitNextLevel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(10 * 1000);
        }

        private void mBGWorker_WaitNextLevel_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (mStop == false)
            {
                // this.readNextQuestion();
                this.btnNextLevel.IsEnabled = true;
            }
            else
            {
                string msg = "(游戏暂停) CurrentStep:{0}".FormatWith(mCurrentStep);
                System.Diagnostics.Debug.WriteLine(msg);
                App.Output.Info(Tag, msg);
            }
        }

        private void BtnNextLevel_Clicked(object sender, EventArgs e)
        {
            this.gNextLevel.IsVisible = false;
            this.lblNextLevel.Text = string.Empty;
            this.readNextQuestion();
        }

        #endregion

        #region 锻炼时间

        System.ComponentModel.BackgroundWorker mBGWorker_ShowStopWatch { get; set; }

        private void showStopWatch()
        {
            if (mBGWorker_ShowStopWatch == null)
            {
                mBGWorker_ShowStopWatch = new System.ComponentModel.BackgroundWorker();
                mBGWorker_ShowStopWatch.DoWork += mBGWorker_ShowStopWatch_DoWork;
                mBGWorker_ShowStopWatch.ProgressChanged += mBGWorker_ShowStopWatch_ProgressChanged;
                mBGWorker_ShowStopWatch.WorkerReportsProgress = true;
            }

            if (mBGWorker_ShowStopWatch.IsBusy == true)
            {
                return;
            }

            mBGWorker_ShowStopWatch.RunWorkerAsync();
        }


        private void mBGWorker_ShowStopWatch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (mQuit == false)
            {
                System.Threading.Thread.Sleep(1000);
                if (mStop == false)
                {
                    mBGWorker_ShowStopWatch.ReportProgress(0);
                }
            }
        }


        private void mBGWorker_ShowStopWatch_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.ViewModel.NotifiyUseTime();
            });
        }

        #endregion
    }

    public class PageMainViewModel : ViewModel.BaseViewModel
    {
        private int _CRWTypeID;

        public int CRWTypeID
        {
            get
            {
                return _CRWTypeID;
            }
            set
            {
                _CRWTypeID = value;

            }
        }

        private CRW_Level _Level;

        public CRW_Level Level
        {
            get
            {
                return _Level;
            }
            set
            {
                _Level = value;
                this.OnPropertyChanged("Level");
                this.OnPropertyChanged("LevelName");

            }
        }

        private int? _CurrentIndex;

        public int? CurrentIndex
        {
            get
            {
                return _CurrentIndex;
            }
            set
            {
                _CurrentIndex = value;
            }
        }

        public string LevelName
        {
            get
            {
                string r = string.Empty;

                if (Level != null)
                {
                    r = this.Level.LevelName;
                }

                return r;
            }
        }

        public System.Diagnostics.Stopwatch swCRW_UseTime = new System.Diagnostics.Stopwatch();

        public string CRW_UseTimeInfo
        {
            get
            {
                string r = string.Empty;

                if (swCRW_UseTime != null)
                {
                    var a = TimeSpan.FromTicks(swCRW_UseTime.ElapsedTicks);
                    int tmpM = a.Minutes;
                    int tmpS = a.Seconds;

                    if (tmpM > 0)
                    {
                        r = "{0}分{1}秒".FormatWith(tmpM, tmpS);
                    }

                    else
                    {
                        r = "{0}秒".FormatWith(tmpS);
                    }
                }

                return r;
            }
        }

        public void NotifiyUseTime()
        {
            this.OnPropertyChanged("CRW_UseTimeInfo");
        }

        private List<CRW_Question> _QuestionList;

        public List<CRW_Question> QuestionList
        {
            get
            {
                return _QuestionList;
            }
            set
            {
                _QuestionList = value;
                this.OnPropertyChanged("QuestionList");
            }
        }

        private CRW_Question _RememberQuestion;

        public CRW_Question RememberQuestion
        {
            get
            {
                return _RememberQuestion;
            }
            set
            {
                _RememberQuestion = value;
                this.OnPropertyChanged("RememberQuestion");
            }
        }

        private CRW_Question _AnswerQuestion;

        public CRW_Question AnswerQuestion
        {
            get
            {
                return _AnswerQuestion;
            }
            set
            {
                _AnswerQuestion = value;
                this.OnPropertyChanged("AnswerQuestion");
            }
        }

        public void NotifiyAnswerQuestion()
        {
            this.OnPropertyChanged("AnswerQuestion");
        }

    }
}