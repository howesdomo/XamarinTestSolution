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
        CRWBll mBll { get; set; }

        PageMainViewModel ViewModel { get; set; }

        bool mStop { get; set; }

        bool mQuit { get; set; }

        public PageMain()
        {
            InitializeComponent();
            initUI();

            this.mBll = new CRWBll();
            this.initEvent();
            this.ViewModel = new PageMainViewModel();
            this.BindingContext = this.ViewModel;

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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string msg = "{0}".FormatWith("PageMain OnAppearing");
            System.Diagnostics.Debug.WriteLine(msg);

            Device.BeginInvokeOnMainThread(() =>
            {
                // TODO 设置常亮
                // 暂时无法使用 Xamarin.Essentials.ScreenLock.RequestActive 来设置屏幕常亮
                App.ScreenDirection.ForceLandscape();
            });
        }

        protected override void OnDisappearing()
        {
            string msg = "{0}".FormatWith("PageMain OnDisappearing");
            System.Diagnostics.Debug.WriteLine(msg);

            if (mQuit)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.ScreenDirection.Unspecified();
                });
            }         

            base.OnDisappearing();
        }

        #endregion

        private void initEvent()
        {
            this.btnGiveUp.Clicked += BtnGiveUp_Clicked;
            this.btnResetLevel.Clicked += BtnResetLevel_Clicked;

            this.gCRWKeyboard.InputValueEvent += new EventHandler<CRW_Keyboard_EventArgs>(receiveUserAnswer);
        }

        protected override bool OnBackButtonPressed()
        {
            showCloseDisplayAlert();
            return true;
        }

        async void showCloseDisplayAlert()
        {
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
        }

        private void quitGame()
        {

            mQuit = true;

            mBGWorker_PlayAnswerVedio.CancelAsync();

            mBGWorker_CalcTimeout.CancelAsync();

            mBGWorker_WaitNextLevel.CancelAsync();


            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync(true);
            });
        }

        int mCurrentStep = 0;

        #region Level

        private void BtnResetLevel_Clicked(object sender, EventArgs e)
        {
            changeLevelForTest();
        }

        void changeLevelForTest()
        {
            this.ViewModel.CurrentIndex = null;
            readLevel();
            calcQuestion();
        }

        private void readLevel()
        {
            int levelNo = int.Parse(this.txtLevelNo.Text);
            CRW_Level level = new CRW_Level(levelNo);
            this.ViewModel.Level = level;
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

            #region 答题完毕

            if (this.ViewModel.RememberQuestion == null && this.ViewModel.AnswerQuestion == null)
            {
                this.ViewModel.CurrentIndex = null;

                decimal correctPercentage = mBll.CheckCorrectPercentage(this.ViewModel.QuestionList);
                var result = mBll.CalcLevel(this.ViewModel.Level, this.ViewModel.QuestionList);

                System.Diagnostics.Debug.WriteLine(result.Item2);


                // 设置新的等级, 计算新的题目
                this.ViewModel.Level = result.Item1;
                this.calcQuestion();
                // 播放检测正确率动画, 播放完毕后执行, readNextQuestion()

                this.ViewModel.swCRW_UseTime.Stop();

                this.playNextLevelVedio(result.Item2);
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

        /// <summary>
        /// 录入正确答案后等待动画时间
        /// </summary>
        public const int mInputCorrectAnswerSleepTime = 1000;

        private void validateUserAnswer(int userResult)
        {
            if (IsPlayingAnswerVedio == true)
            {
                string msg = "{0}".FormatWith("正在播放动画(Answer), 忽略结果输入");
                System.Diagnostics.Debug.WriteLine(msg);
                return;
            }

            if (IsWaitNextLevel == true)
            {
                string msg = "{0}".FormatWith("正在播放动画(Next Level), 忽略结果输入");
                System.Diagnostics.Debug.WriteLine(msg);
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
                calcTimeOut_BgWorker_Stop();
                playAnswerVedio();
            }
        }

        System.ComponentModel.BackgroundWorker mBGWorker_PlayAnswerVedio { get; set; }

        /// <summary>
        /// 播放显示正确答案动画
        /// </summary>
        void playAnswerVedio()
        {
            if (mBGWorker_PlayAnswerVedio == null)
            {
                mBGWorker_PlayAnswerVedio = new System.ComponentModel.BackgroundWorker();
                mBGWorker_PlayAnswerVedio.DoWork += mBGWorker_PlayAnswerVedio_DoWork;
                mBGWorker_PlayAnswerVedio.RunWorkerCompleted += mBGWorker_PlayAnswerVedio_RunWorkerCompleted;
            }

            if (mBGWorker_PlayAnswerVedio.IsBusy == true)
            {
                string msg = "{0}".FormatWith("正在等待回答正确的BackgroundWorker结束");
                System.Diagnostics.Debug.WriteLine(msg);
                return;
            }

            mBGWorker_PlayAnswerVedio.RunWorkerAsync();
        }

        /// <summary>
        /// 判断是否正在播放动画
        /// </summary>
        bool IsPlayingAnswerVedio
        {
            get
            {
                if (mBGWorker_PlayAnswerVedio != null && mBGWorker_PlayAnswerVedio.IsBusy)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void mBGWorker_PlayAnswerVedio_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(mInputCorrectAnswerSleepTime);
        }

        private void mBGWorker_PlayAnswerVedio_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            readNextQuestion();
        }

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
                string msg = "{0}".FormatWith("!!!!!!!!!!!!!!!!超时判断BgWorker正在工作中");
                System.Diagnostics.Debug.WriteLine(msg);
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

            while (argsWaitTime >= 0)
            {
                System.Threading.Thread.Sleep(100);
                argsWaitTime -= 100;

                if (mCalcTimeout_Cancel == true)
                {
                    break;
                }
            }

            if (mCalcTimeout_Cancel == true)
            {
                msg = "取消答题倒计时（已正确回答）- 1";
                System.Diagnostics.Debug.WriteLine(msg);
            }
            else
            {
                msg = "停止答题倒计时:0毫秒".FormatWith(argsWaitTime);
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private void mBGWorker_CalcTimeout_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string msg = string.Empty;

            if (e.Error != null)
            {
                msg = "{0}".FormatWith();
                System.Diagnostics.Debug.WriteLine(msg);
                this.DisplayAlert("错误", e.Error.Message, "确定");
                return;
            }

            if (mCalcTimeout_Cancel == true)
            {
                // 由于正确回答, 计算超时线程被取消
                msg = "取消答题倒计时（已正确回答） - 2";
                System.Diagnostics.Debug.WriteLine(msg);
            }
            else
            {
                if (this.ViewModel.AnswerQuestion == null)
                {
                    msg = "由于未开始作答, 自动跳到下一题";
                    System.Diagnostics.Debug.WriteLine(msg);
                    playAnswerVedio();
                    return;
                }

                // 超出答题时间
                // 在答案区域显示用户录入的值, 并显示开一个大红叉
                msg = "判断答题超时";
                System.Diagnostics.Debug.WriteLine(msg);

                this.ViewModel.AnswerQuestion.ChangeStatus(CRW_Question_Status.TimeOutWrongAnswer);
                this.ViewModel.NotifiyAnswerQuestion();
                playAnswerVedio();
            }
        }

        #endregion

        System.ComponentModel.BackgroundWorker mBGWorker_WaitNextLevel { get; set; }

        void playNextLevelVedio(string ttsContent)
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
                return;
            }

            mCurrentStep = 3;
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
            this.readNextQuestion();
        }


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
    }

    public class PageMainViewModel : ViewModel.BaseViewModel
    {
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