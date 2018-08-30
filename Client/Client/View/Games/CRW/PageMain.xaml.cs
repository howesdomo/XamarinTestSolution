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
        }

        #region UI        

        private void initUI()
        {
            // Xamarin.Forms 版本 3.0.0.530893
            // 由于在XAML中设置 Margin 会导致编译时报错
            // 故将部分的 Margin 设置写在C#代码中
            this.gRight.Margin = new Thickness(left: -10d, top: 0d, right: 0d, bottom: 0d);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string msg = "{0}".FormatWith("PageMain OnAppearing");
            System.Diagnostics.Debug.WriteLine(msg);

            Device.BeginInvokeOnMainThread(() =>
            {
                App.ScreenDirection.ForceLandscape();
            });
        }

        protected override void OnDisappearing()
        {
            string msg = "{0}".FormatWith("PageMain OnDisappearing");
            System.Diagnostics.Debug.WriteLine(msg);

            Device.BeginInvokeOnMainThread(() =>
            {
                App.ScreenDirection.Unspecified();
            });

            base.OnDisappearing();
        }

        #endregion

        private void initEvent()
        {
            this.btnGiveUp.Clicked += BtnGiveUp_Clicked;
            this.btnResetLevel.Clicked += BtnResetLevel_Clicked;

            this.gCRWKeyboard.InputValueEvent += new EventHandler<CRW_Keyboard_EventArgs>(receiveUserAnswer);
        }

        #region Level

        private void BtnResetLevel_Clicked(object sender, EventArgs e)
        {
            changeLevel();
        }

        void changeLevel()
        {
            this.ViewModel.CurrentIndex = null;
            readLevel();
            calcQuestion();
        }

        private void readLevel()
        {
            // TODO 到SQLite读取Level

            int levelNo = int.Parse(this.txtLevelNo.Text);
            CRW_Level level = new CRW_Level(levelNo);

            //#region 

            //level.LevelNo = int.Parse(this.txtLevelNo.Text);

            //level.SuSuan = level.LevelNo / 2 + level.LevelNo % 2;

            //if (level.LevelNo % 2 == 0)
            //{
            //    level.LevelName = "快速{0}溯答".FormatWith(level.SuSuan);
            //}
            //else
            //{
            //    level.LevelName = "{0}溯答".FormatWith(level.SuSuan);
            //}

            //level.QuestionCount = 20 + level.SuSuan * 2;
            //level.MaxIndex = level.QuestionCount + level.SuSuan - 1;

            //level.YuSu = 1;
            //level.AnswerTime = TimeSpan.FromSeconds(5).Milliseconds;

            //level.AnswerStartIndex = level.SuSuan;
            //level.RememberEndIndex = level.QuestionCount - 1;


            //#endregion

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

            if (this.ViewModel.RememberQuestion == null && this.ViewModel.AnswerQuestion == null)
            {
                string msg = "{0}".FormatWith("答题完毕, 计算正确率, 准备下一局游戏");
                System.Diagnostics.Debug.WriteLine(msg);

                var level = mBll.CalcLevel(this.ViewModel.Level, this.ViewModel.QuestionList);

                this.ViewModel.Level = level;
                return;
            }
            else
            {
                this.ViewModel.CurrentIndex = r.Item1;
            }
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


                // TODO
                // 超出答题时间
                // 在答案区域显示用户录入的值, 并显示开一个大红叉
                if (this.ViewModel.AnswerQuestion.Result != userResult)
                {

                }
            }
            else // 回答正确
            {
                this.ViewModel.AnswerQuestion.ChangeStatus(CRW_Question_Status.InputCorrectAnswer);
                this.ViewModel.NotifiyAnswerQuestion();
                playInputCorrectAnswerVedio();
            }
        }

        System.ComponentModel.BackgroundWorker mBGWorker { get; set; }

        void playInputCorrectAnswerVedio()
        {
            if (mBGWorker == null)
            {
                mBGWorker = new System.ComponentModel.BackgroundWorker();
                mBGWorker.DoWork += MBGWorker_DoWork;
                mBGWorker.RunWorkerCompleted += MBGWorker_RunWorkerCompleted;
            }

            if (mBGWorker.IsBusy == true)
            {
                string msg = "{0}".FormatWith("正在等待回答正确的BackgroundWorker结束");
                System.Diagnostics.Debug.WriteLine(msg);
                return;
            }

            mBGWorker.RunWorkerAsync();
        }


        private void MBGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(mInputCorrectAnswerSleepTime);
        }

        private void MBGWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            readNextQuestion();
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

        System.Diagnostics.Stopwatch swCRW_UseTime { get; set; }

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