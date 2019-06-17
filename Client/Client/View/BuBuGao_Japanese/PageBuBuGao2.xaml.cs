using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.BuBuGao_Japanese
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBuBuGao2 : ContentPage
    {
        private PageBuBuGao2_ViewModel ViewModel { get; set; }

        public PageBuBuGao2(Question_Japanese q, bool isAutoPlaySound)
        {
            InitializeComponent();
            initUI();
            this.ViewModel = new PageBuBuGao2_ViewModel();
            this.ViewModel.Question = q;
            this.Title = "共 {0} 题".FormatWith(this.ViewModel.Question.Words.Count);

            this.ViewModel.IsAutoPlaySound = isAutoPlaySound;
            if (isAutoPlaySound)
            {
                this.gBottom.IsVisible = false;
            }

            this.BindingContext = this.ViewModel;
            initEvent();
        }

        private void initUI()
        {
            //this.btnPlaySound.Margin = new Thickness()
            //{
            //    Left = 5,
            //    Top = 0,
            //    Right = 5,
            //    Bottom = 0
            //};

            //this.gBottom.Margin = new Thickness()
            //{
            //    Left = 5,
            //    Top = 0,
            //    Right = 5,
            //    Bottom = 2
            //};

            //this.btnSwitch.Margin = new Thickness()
            //{
            //    Left = 0,
            //    Top = 0,
            //    Right = 10,
            //    Bottom = 0
            //};
        }

        private void initEvent()
        {
            this.btnNext.Clicked += BtnNext_Clicked;
            this.btnNext2.Clicked += BtnNext2_Clicked;
            this.btnLast.Clicked += BtnLast_Clicked;

            this.btnPlaySound.Clicked += BtnPlaySound_Clicked;

            this.btnPass.Clicked += BtnPass_Clicked;
            this.btnFail.Clicked += BtnFail_Clicked;


            var imgTapGestureRecognizer = new TapGestureRecognizer();
            imgTapGestureRecognizer.Tapped += (s, e) =>
            {
                imgHiragana.IsVisible = !imgHiragana.IsVisible;
                imgKatakana.IsVisible = !imgKatakana.IsVisible;
            };

            this.btnSwitch.GestureRecognizers.Add(imgTapGestureRecognizer);

            //imgHiragana.
        }

        private void BtnLast_Clicked(object sender, EventArgs e)
        {
            changeIndex(this.ViewModel.Index - 1);
        }

        void BtnNext_Clicked(object sender, EventArgs e)
        {
            changeIndex(this.ViewModel.Index + 1);
        }


        void BtnNext2_Clicked(object sender, EventArgs e)
        {
            changeIndex(this.ViewModel.Index + 1);
        }

        async void changeIndex(int newValue)
        {
            if (newValue < 0)
            {
                return;
            }

            if (newValue >= this.ViewModel.Question.Words.Count)
            {
                await DisplayAlert("提示", "答题完毕", "确定");
            }

            this.ViewModel.Index = newValue;
        }

        private void BtnPlaySound_Clicked(object sender, EventArgs e)
        {
            App.TTS.PlayJapanese(this.ViewModel.HiraganaContent);
        }

        #region 通过 & 未通过

        private void BtnFail_Clicked(object sender, EventArgs e)
        {
            var match = this.ViewModel.Question.Words[this.ViewModel.Index];
            match.IsPass = -1;
            // Common.StaticInfo.InnerSQLiteDB.BuBuGao_uWord(match);
            this.ViewModel.ViewModelOnPropertyChanged("btnFailBackgroundColor");
        }

        private void BtnPass_Clicked(object sender, EventArgs e)
        {
            var match = this.ViewModel.Question.Words[this.ViewModel.Index];
            match.IsPass = 1;
            // Common.StaticInfo.InnerSQLiteDB.BuBuGao_uWord(match);
            this.ViewModel.ViewModelOnPropertyChanged("btnPassBackgroundColor");
        }

        #endregion
    }

    public class PageBuBuGao2_ViewModel : ViewModel.BaseViewModel
    {
        public void ViewModelOnPropertyChanged(string name)
        {
            this.OnPropertyChanged(name);
        }

        private int rowNumber = 1;

        public int RowNumber
        {
            get { return rowNumber; }
            set
            {
                if (this.Index != value - 1)
                {
                    this.Index = value - 1;
                }

                rowNumber = value;
                this.OnPropertyChanged("RowNumber");
            }
        }

        private int index = -1;

        public int Index
        {
            get { return index; }
            set
            {
                if (value < 0)
                {
                    return;
                }

                if (value >= this.Question.Words.Count)
                {
                    return;
                }

                index = value;
                this.OnPropertyChanged("Index");

                this.rowNumber = value + 1;
                this.OnPropertyChanged("RowNumber");

                if (this.Question.Words != null && this.Question.Words.Count > 0)
                {
                    this.SelectedWord = this.Question.Words[value];
                }

                if (value >= this.Question.Words.Count - 1 && this.IsSaved == false)
                {
                    try
                    {
                        this.IsSaved = true;
                        if (this.IsAutoPlaySound)
                        {
                            this.studyFinish();
                        }
                        else
                        {
                            this.practiceFinish();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.IsSaved = false;
                        string msg = "{0}".FormatWith(ex.GetFullInfo());
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                }
            }
        }

        #region 学习完毕

        private void studyFinish()
        {
            var match = this.Question;
            match.LastStudyDateTime = DateTime.Now;
            match.LastStudyDateTimeValue = match.LastStudyDateTime.Value.Ticks;
            match.StudyTimes += 1;
            // Common.StaticInfo.InnerSQLiteDB.BuBuGao_uQuestion(match);
        }

        #endregion

        #region 练习完毕

        private void practiceFinish()
        {
            var match = this.Question;
            match.LastPracticeDateTime = DateTime.Now;
            match.LastPracticeDateTimeValue = match.LastPracticeDateTime.Value.Ticks;
            match.PracticeTimes += 1;
            // Common.StaticInfo.InnerSQLiteDB.BuBuGao_uQuestion(match);
        }

        #endregion

        private Word_Japanese selectedWord;

        public Word_Japanese SelectedWord
        {
            get { return selectedWord; }
            set
            {
                selectedWord = value;
                this.OnPropertyChanged("Content");
                this.OnPropertyChanged("HiraganaContent");
                this.OnPropertyChanged("KatakanaContent");
                this.OnPropertyChanged("ChineseEtymology");

                this.OnPropertyChanged("HiraganaGifImage");
                this.OnPropertyChanged("KatakanaGifImage");

                this.OnPropertyChanged("WordInfo");                

                this.OnPropertyChanged("btnPassBackgroundColor");
                this.OnPropertyChanged("btnFailBackgroundColor");

                if (this.IsAutoPlaySound && value != null)
                {
                    App.TTS.PlayJapanese(value.HiraganaContent);
                }
            }
        }

        public string Content
        {
            get
            {
                string r = string.Empty;
                if (this.SelectedWord != null)
                {
                    r = this.SelectedWord.Content;
                }
                return r;
            }
        }

        public string HiraganaContent
        {
            get
            {
                string r = string.Empty;
                if (this.SelectedWord != null)
                {
                    r = this.SelectedWord.HiraganaContent;
                }
                return r;
            }
        }

        public string KatakanaContent
        {
            get
            {
                string r = string.Empty;
                if (this.SelectedWord != null)
                {
                    r = this.SelectedWord.KatakanaContent;
                }
                return r;
            }
        }

        public string ChineseEtymology
        {
            get
            {
                string r = string.Empty;
                if (this.SelectedWord != null)
                {
                    r = this.SelectedWord.ChineseEtymology;
                }
                return r;
            }
        }

        public string WordInfo
        {
            get
            {
                string r = string.Empty;
                if (this.SelectedWord != null)
                {
                    r = "发音:{0}; 汉字字源:{1}".FormatWith(this.SelectedWord.Content, this.selectedWord.ChineseEtymology);
                }
                return r;
            }
        }

        Xamarin.Forms.ImageSource _HiraganaGifImage = null;
        public Xamarin.Forms.ImageSource HiraganaGifImage
        {
            get
            {
                if (_HiraganaGifImage != null)
                {
                    _HiraganaGifImage = null;
                    GC.Collect();
                }

                if (this.SelectedWord != null)
                {
                    string resources = "Client.Images.BuBuGao_Japanese.Hiragana.{0}_{1}.gif"
                    .FormatWith
                    (
                        this.SelectedWord.ID,
                        this.SelectedWord.Content
                    );

                    _HiraganaGifImage = ImageSource.FromResource(resources);
                }
                return _HiraganaGifImage;
            }
        }

        Xamarin.Forms.ImageSource _KatakanaGifImage = null;
        public Xamarin.Forms.ImageSource KatakanaGifImage
        {
            get
            {
                if (_KatakanaGifImage != null)
                {
                    _KatakanaGifImage = null;
                    GC.Collect();
                }

                if (this.SelectedWord != null)
                {
                    string resources = "Client.Images.BuBuGao_Japanese.Katakana.{0}_{1}.gif"
                    .FormatWith
                    (
                        this.SelectedWord.ID,
                        this.SelectedWord.Content
                    );

                    _KatakanaGifImage = ImageSource.FromResource(resources);
                }
                return _KatakanaGifImage;
            }
        }

        public string btnPassBackgroundColor
        {
            get
            {
                string r = "Silver";
                if (this.SelectedWord != null && this.SelectedWord.IsPass == 1)
                {
                    r = "Green";
                }
                return r;
            }
        }

        public string btnFailBackgroundColor
        {
            get
            {
                string r = "Silver";
                if (this.SelectedWord != null && this.SelectedWord.IsPass == -1)
                {
                    r = "Red";
                }
                return r;
            }
        }

        private Question_Japanese question;

        public Question_Japanese Question
        {
            get { return question; }
            set
            {
                question = value;
                this.OnPropertyChanged("Question");
            }
        }

        private bool isAutoPlaySound;

        public bool IsAutoPlaySound
        {
            get { return isAutoPlaySound; }
            set
            {
                isAutoPlaySound = value;
                this.OnPropertyChanged("IsAutoPlaySound");
            }
        }

        /// <summary>
        /// 进入本界面只进行一次 (学习完毕 or 练习完毕) 的保存
        /// </summary>
        public bool IsSaved = false;

    }
}