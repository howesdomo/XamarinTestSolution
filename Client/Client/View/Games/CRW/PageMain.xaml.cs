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
            CRW_Level level = new CRW_Level();

            #region

            level.LevelNo = int.Parse(this.txtLevelNo.Text);

            level.SuSuan = level.LevelNo / 2 + level.LevelNo % 2;

            if (level.LevelNo % 2 == 0)
            {
                level.LevelName = "快速{0}溯答".FormatWith(level.SuSuan);
            }
            else
            {
                level.LevelName = "{0}溯答".FormatWith(level.SuSuan);
            }

            level.QuestionCount = 20 + level.SuSuan * 2;
            level.MaxIndex = level.QuestionCount + level.SuSuan - 1;

            level.YuSu = 1;
            level.AnswerTime = TimeSpan.FromSeconds(5).Milliseconds;

            level.AnswerStartIndex = level.SuSuan;
            level.RememberEndIndex = level.QuestionCount - 1;


            #endregion


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
            int index = 0;

            if (this.ViewModel.CurrentIndex.HasValue == false)
            {
                this.ViewModel.CurrentIndex = 0;
            }

            index = this.ViewModel.CurrentIndex.Value;

            //if (index > this.ViewModel.Level.MaxIndex)
            //{
            //    return;
            //}

            CRW_Question toRemember = null;
            CRW_Question toAnswer = null;

            if (index <= this.ViewModel.Level.RememberEndIndex)
            {
                toRemember = this.ViewModel.QuestionList[index];
                toRemember.ChangeStatus(CRW_Question_Status.Remember);
            }

            if (index >= this.ViewModel.Level.AnswerStartIndex && index <= this.ViewModel.Level.MaxIndex)
            {
                toAnswer = this.ViewModel.QuestionList[index - this.ViewModel.Level.SuSuan];
                toAnswer.ChangeStatus(CRW_Question_Status.Answer);
            }

            this.ViewModel.RememberQuestion = toRemember;
            this.ViewModel.AnswerQuestion = toAnswer;

            this.ViewModel.CurrentIndex = index + 1;
        }

        #endregion

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

    }
}