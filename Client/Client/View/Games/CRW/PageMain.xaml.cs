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

            this.mBll = new CRWBll();
            this.initEvent();
            this.ViewModel = new PageMainViewModel();
            this.BindingContext = this.ViewModel;


            readLevel();

            calcQuestion();
            
        }



        private void initEvent()
        {
            this.btnGiveUp.Clicked += BtnGiveUp_Clicked;
        }

        private void readLevel()
        {
            // TODO 到SQLite读取Level
            CRW_Level level = new CRW_Level();

            #region

            level.LevelNo = 3;
            level.LevelName = "2溯答";
            level.QuestionCount = 24;
            level.YuSu = 1;
            level.AnswerTime = TimeSpan.FromSeconds(5).Milliseconds;

            #endregion

            this.ViewModel.Level = level;
        }

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

            CRW_Question toRemember = null;
            CRW_Question toAnswer = null;

            if (this.ViewModel.RememberQuestion == null && this.ViewModel.AnswerQuestion == null)
            {
                toRemember = this.ViewModel.QuestionList[index];
                toRemember.ChangeStatus(CRW_Question_Status.Remember);
                this.ViewModel.RememberQuestion = toRemember;

                this.ViewModel.AnswerQuestion = null;
                return;
            }

            index = this.ViewModel.RememberQuestion.No;

            toRemember = this.ViewModel.QuestionList[index];
            toRemember.ChangeStatus(CRW_Question_Status.Remember);
            this.ViewModel.RememberQuestion = toRemember;

            if (index > 3) // TODO 3 ==> 速算开始的题目
            {
                toAnswer = this.ViewModel.QuestionList[2];
                toAnswer.ChangeStatus(CRW_Question_Status.Answer);
                this.ViewModel.AnswerQuestion = toAnswer;
            }            
        }

        private void initQuestionList()
        {

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