using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.BuBuGao
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBuBuGao1 : ContentPage
    {
        PageBuBuGao1_ViewModel ViewModel { get; set; }

        public PageBuBuGao1()
        {
            InitializeComponent();
            this.ViewModel = new PageBuBuGao1_ViewModel();
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
            this.ViewModel.All = Common.StaticInfo.ExternalSQLiteDB.BuBuGao_rQuestionList();
        }

        private void initEvent()
        {
            this.btnStudy.Clicked += BtnStudy_Clicked;
            this.btnStudyDesc.Clicked += BtnStudyDesc_Clicked;
            this.btnStudyRand.Clicked += BtnStudyRand_Clicked;

            this.btnPractice.Clicked += BtnPractice_Clicked;
            this.btnPracticeDesc.Clicked += BtnPracticeDesc_Clicked;
            this.btnPracticeRand.Clicked += BtnPracticeRand_Clicked;

            this.btnAddNew.Clicked += BtnAddNew_Clicked;


            // 点击图片事件
            TapGestureRecognizer imageTapGesture = new TapGestureRecognizer();
            imageTapGesture.Tapped += imageTapGesture_Tapped;
            btnAddQuesiton.GestureRecognizers.Add(imageTapGesture);
        }


        private void BtnStudy_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            touchItem(cloneArgs, true);
        }

        private void BtnStudyDesc_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
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

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
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

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            touchItem(cloneArgs, false);
        }

        private void BtnPracticeDesc_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.SelectedQuestion == null)
            {
                DisplayAlert("提示", "请选择 1 项", "确认");
                return;
            }

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
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

            var cloneArgs = Util.JsonUtils.DeserializeObject<Question>(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedQuestion));
            cloneArgs.Words = cloneArgs.Words.OrderByRandom().ToList();
            touchItem(cloneArgs, false);
        }

        async void touchItem(Question q, bool isAutoPlaySound)
        {
            var page = new PageBuBuGao2(q, isAutoPlaySound);
            await Navigation.PushAsync(page);
        }

        async void BtnAddNew_Clicked(object sender, EventArgs e)
        {
            try
            {
                addNew(this.txtAddNew.Text);
            }
            catch (Exception ex)
            {
                await DisplayAlert("提示", ex.GetFullInfo(), "确定");
            }
        }

        async void addNew(string a)
        {
            var query = a.Split(',', '|', ';', '，', '；')
                .Where(i => i.IsNullOrWhiteSpace() == false)
                .ToList();

            if (query.Count <= 0)
            {
                await DisplayAlert("提示", "添加失败", "确定");
                return;
            }

            Question toAdd = new Question();
            toAdd.Words = new List<Word>();

            foreach (var item in query)
            {
                toAdd.Words.Add(new Word() { Content = item });
            }

            toAdd.Name = toAdd.Words[0].Content;

            this.ViewModel.All_Add(toAdd);

        }

        async void imageTapGesture_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageBuBuGao_AddQuestion());
        }
    }

    public class PageBuBuGao1_ViewModel : ViewModel.BaseViewModel
    {
        public void All_Add(Question q)
        {
            this.All.Add(q);
            this.OnPropertyChanged("All");
        }

        private List<Question> all;

        public List<Question> All
        {
            get { return all; }
            set
            {
                all = value;
                this.OnPropertyChanged("All");
            }
        }

        private Question selectedQuestion;

        public Question SelectedQuestion
        {
            get { return selectedQuestion; }
            set
            {
                selectedQuestion = value;
                this.OnPropertyChanged("SelectedQuestion");
            }
        }
    }

    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        [Ignore]
        public List<Word> Words { get; set; }

        public int StudyTimes { get; set; }

        public int PracticeTimes { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateDateTimeValue { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Ignore]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 最后一次学习时间
        /// </summary>
        public long LastStudyDateTimeValue { get; set; }

        /// <summary>
        /// 最后一次学习时间
        /// </summary>
        [Ignore]
        public DateTime? LastStudyDateTime { get; set; }

        /// <summary>
        /// 最后一次测试时间
        /// </summary>
        public long LastPracticeDateTimeValue { get; set; }

        /// <summary>
        /// 最后一次测试时间
        /// </summary>
        [Ignore]
        public DateTime? LastPracticeDateTime { get; set; }

        /// <summary>
        /// 已熟练数量
        /// </summary>
        [Ignore]
        public int IsPassWordsCount { get; set; }

        /// <summary>
        /// 未掌握数量
        /// </summary>
        [Ignore]
        public int IsNotPassWordsCount { get; set; }

    }

    public class Word : ViewModel.BaseViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Content { get; set; }

        [Indexed]
        public int QuestionID { get; set; }

        // 1 通过
        // 0 未标记
        // -1 失败
        public int IsPass { get; set; }
    }
}