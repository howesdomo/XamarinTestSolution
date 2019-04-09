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
    public partial class PageBuBuGao1 : ContentPage
    {
        PageBuBuGao_Japanese_1_ViewModel ViewModel { get; set; }

        public PageBuBuGao1()
        {
            InitializeComponent();
            this.ViewModel = new PageBuBuGao_Japanese_1_ViewModel();
            this.BindingContext = this.ViewModel;
            initEvent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            getLastestData();
        }

        private void getLastestData()
        {
            List<Question_Japanese> all = new List<Question_Japanese>();

            Question_Japanese q = new Question_Japanese();
            q.Name = "50音片假名";
            q.Words = new List<Word_Japanese>()
            {
                new Word_Japanese(){ ID = 1, Content="a", HiraganaContent="あ", KatakanaContent="ア", ChineseEtymology="安"},
                new Word_Japanese(){ ID = 2, Content="i", HiraganaContent="い", KatakanaContent="イ", ChineseEtymology="以"},
                new Word_Japanese(){ ID = 3, Content="u", HiraganaContent="う", KatakanaContent="ウ", ChineseEtymology="宇"},
                new Word_Japanese(){ ID = 4, Content="e", HiraganaContent="え", KatakanaContent="エ", ChineseEtymology="衣"},
                new Word_Japanese(){ ID = 5, Content="o", HiraganaContent="お", KatakanaContent="オ", ChineseEtymology="於"},
                new Word_Japanese(){ ID = 6, Content="ka", HiraganaContent="か", KatakanaContent="カ", ChineseEtymology="加"},
                new Word_Japanese(){ ID = 7, Content="ki", HiraganaContent="き", KatakanaContent="キ", ChineseEtymology="幾"},
                new Word_Japanese(){ ID = 8, Content="ku", HiraganaContent="く", KatakanaContent="ク", ChineseEtymology="久"},
                new Word_Japanese(){ ID = 9, Content="ke", HiraganaContent="け", KatakanaContent="ケ", ChineseEtymology="計"},
                new Word_Japanese(){ ID = 10, Content="ko", HiraganaContent="こ", KatakanaContent="コ", ChineseEtymology="己"},
                new Word_Japanese(){ ID = 11, Content="sa", HiraganaContent="さ", KatakanaContent="サ", ChineseEtymology="左"},
                new Word_Japanese(){ ID = 12, Content="shi", HiraganaContent="し", KatakanaContent="シ", ChineseEtymology="之"},
                new Word_Japanese(){ ID = 13, Content="su", HiraganaContent="す", KatakanaContent="ス", ChineseEtymology="寸"},
                new Word_Japanese(){ ID = 14, Content="se", HiraganaContent="せ", KatakanaContent="セ", ChineseEtymology="世"},
                new Word_Japanese(){ ID = 15, Content="so", HiraganaContent="そ", KatakanaContent="ソ", ChineseEtymology="曽"},
                new Word_Japanese(){ ID = 16, Content="ta", HiraganaContent="た", KatakanaContent="タ", ChineseEtymology="太"},
                new Word_Japanese(){ ID = 17, Content="chi", HiraganaContent="ち", KatakanaContent="チ", ChineseEtymology="知"},
                new Word_Japanese(){ ID = 18, Content="tsu", HiraganaContent="つ", KatakanaContent="ツ", ChineseEtymology="川"},
                new Word_Japanese(){ ID = 19, Content="te", HiraganaContent="て", KatakanaContent="テ", ChineseEtymology="天"},
                new Word_Japanese(){ ID = 20, Content="to", HiraganaContent="と", KatakanaContent="ト", ChineseEtymology="止"},
                new Word_Japanese(){ ID = 21, Content="na", HiraganaContent="な", KatakanaContent="ナ", ChineseEtymology="奈"},
                new Word_Japanese(){ ID = 22, Content="ni", HiraganaContent="に", KatakanaContent="ニ", ChineseEtymology="仁"},
                new Word_Japanese(){ ID = 23, Content="nu", HiraganaContent="ぬ", KatakanaContent="ヌ", ChineseEtymology="奴"},
                new Word_Japanese(){ ID = 24, Content="ne", HiraganaContent="ね", KatakanaContent="ネ", ChineseEtymology="祢"},
                new Word_Japanese(){ ID = 25, Content="no", HiraganaContent="の", KatakanaContent="ノ", ChineseEtymology="乃"},
                new Word_Japanese(){ ID = 26, Content="ha", HiraganaContent="は", KatakanaContent="ハ", ChineseEtymology="波"},
                new Word_Japanese(){ ID = 27, Content="hi", HiraganaContent="ひ", KatakanaContent="ヒ", ChineseEtymology="比"},
                new Word_Japanese(){ ID = 28, Content="fu", HiraganaContent="ふ", KatakanaContent="フ", ChineseEtymology="不"},
                new Word_Japanese(){ ID = 29, Content="he", HiraganaContent="へ", KatakanaContent="ヘ", ChineseEtymology="部"},
                new Word_Japanese(){ ID = 30, Content="ho", HiraganaContent="ほ", KatakanaContent="ホ", ChineseEtymology="保"},
                new Word_Japanese(){ ID = 31, Content="ma", HiraganaContent="ま", KatakanaContent="マ", ChineseEtymology="末"},
                new Word_Japanese(){ ID = 32, Content="mi", HiraganaContent="み", KatakanaContent="ミ", ChineseEtymology="美"},
                new Word_Japanese(){ ID = 33, Content="mu", HiraganaContent="む", KatakanaContent="ム", ChineseEtymology="武"},
                new Word_Japanese(){ ID = 34, Content="me", HiraganaContent="め", KatakanaContent="メ", ChineseEtymology="女"},
                new Word_Japanese(){ ID = 35, Content="mo", HiraganaContent="も", KatakanaContent="モ", ChineseEtymology="毛"},
                new Word_Japanese(){ ID = 36, Content="ya", HiraganaContent="や", KatakanaContent="ヤ", ChineseEtymology="也"},
                new Word_Japanese(){ ID = 37, Content="yu", HiraganaContent="ゆ", KatakanaContent="ユ", ChineseEtymology="由"},
                new Word_Japanese(){ ID = 38, Content="yo", HiraganaContent="よ", KatakanaContent="ヨ", ChineseEtymology="与"},
                new Word_Japanese(){ ID = 39, Content="ra", HiraganaContent="ら", KatakanaContent="ラ", ChineseEtymology="良"},
                new Word_Japanese(){ ID = 40, Content="ri", HiraganaContent="り", KatakanaContent="リ", ChineseEtymology="利"},
                new Word_Japanese(){ ID = 41, Content="ru", HiraganaContent="る", KatakanaContent="ル", ChineseEtymology="留"},
                new Word_Japanese(){ ID = 42, Content="re", HiraganaContent="れ", KatakanaContent="レ", ChineseEtymology="礼"},
                new Word_Japanese(){ ID = 43, Content="ro", HiraganaContent="ろ", KatakanaContent="ロ", ChineseEtymology="呂"},
                new Word_Japanese(){ ID = 44, Content="wa", HiraganaContent="わ", KatakanaContent="ワ", ChineseEtymology="和"},
                new Word_Japanese(){ ID = 45, Content="o", HiraganaContent="を", KatakanaContent="ヲ", ChineseEtymology="遠"},
                new Word_Japanese(){ ID = 46, Content="n", HiraganaContent="ん", KatakanaContent="ン", ChineseEtymology="无"},
            };

            all.Add(q);

            this.ViewModel.All = all;
        }

        private void initEvent()
        {
            this.btnStudy.Clicked += BtnStudy_Clicked;
            this.btnStudyDesc.Clicked += BtnStudyDesc_Clicked;
            this.btnStudyRand.Clicked += BtnStudyRand_Clicked;

            this.btnPractice.Clicked += BtnPractice_Clicked;
            this.btnPracticeDesc.Clicked += BtnPracticeDesc_Clicked;
            this.btnPracticeRand.Clicked += BtnPracticeRand_Clicked;
        }

        private void BtnStudy_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question_Japanese>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            touchItem(cloneArgs, true);
        }

        private void BtnStudyDesc_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question_Japanese>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            cloneArgs.Words = cloneArgs.Words.OrderByDescending(i => i.ID).ToList();
            touchItem(cloneArgs, true);
        }

        private void BtnStudyRand_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question_Japanese>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            cloneArgs.Words = cloneArgs.Words.OrderByRandom().ToList();
            touchItem(cloneArgs, true);
        }

        private void BtnPractice_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question_Japanese>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            touchItem(cloneArgs, false);
        }

        private void BtnPracticeDesc_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question_Japanese>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            cloneArgs.Words = cloneArgs.Words.OrderByDescending(i => i.ID).ToList();
            touchItem(cloneArgs, false);
        }

        private void BtnPracticeRand_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question_Japanese>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            cloneArgs.Words = cloneArgs.Words.OrderByRandom().ToList();
            touchItem(cloneArgs, false);
        }

        async void touchItem(Question_Japanese q, bool isAutoPlaySound)
        {
            var page = new PageBuBuGao2(q, isAutoPlaySound);
            await Navigation.PushAsync(page);
        }
    }

    public class PageBuBuGao_Japanese_1_ViewModel : ViewModel.BaseViewModel
    {
        private List<Question_Japanese> all;

        public List<Question_Japanese> All
        {
            get { return all; }
            set
            {
                all = value;
                this.OnPropertyChanged("All");
            }
        }

        private Question_Japanese selectedQuestion;

        public Question_Japanese SelectedQuestion
        {
            get { return selectedQuestion; }
            set
            {
                selectedQuestion = value;
                this.OnPropertyChanged("SelectedQuestion");
            }
        }
    }

    public class Question_Japanese
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        [SQLite.Ignore]
        public List<Word_Japanese> Words { get; set; }

        public int StudyTimes { get; set; }

        public int PracticeTimes { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateDateTimeValue { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SQLite.Ignore]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 最后一次学习时间
        /// </summary>
        public long LastStudyDateTimeValue { get; set; }

        /// <summary>
        /// 最后一次学习时间
        /// </summary>
        [SQLite.Ignore]
        public DateTime? LastStudyDateTime { get; set; }

        /// <summary>
        /// 最后一次测试时间
        /// </summary>
        public long LastPracticeDateTimeValue { get; set; }

        /// <summary>
        /// 最后一次测试时间
        /// </summary>
        [SQLite.Ignore]
        public DateTime? LastPracticeDateTime { get; set; }

        /// <summary>
        /// 已熟练数量
        /// </summary>
        [SQLite.Ignore]
        public int IsPassWordsCount { get; set; }

        /// <summary>
        /// 未掌握数量
        /// </summary>
        [SQLite.Ignore]
        public int IsNotPassWordsCount { get; set; }
    }

    public class Word_Japanese
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 罗马发音
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 平假名
        /// </summary>
        public string HiraganaContent { get; set; }

        /// <summary>
        /// 片假名
        /// </summary>
        public string KatakanaContent { get; set; }

        /// <summary>
        /// 汉字字源
        /// </summary>
        public string ChineseEtymology { get; set; }

        [SQLite.Indexed]
        public int QuestionID { get; set; }

        // 1 通过
        // 0 未标记
        // -1 失败
        public int IsPass { get; set; }
    }
}