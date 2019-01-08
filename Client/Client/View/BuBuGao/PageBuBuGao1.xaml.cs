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
            initData();
            initEvent();
        }

        private void initData()
        {
            if (this.ViewModel.All == null) this.ViewModel.All = new List<Question>();

            Question a1 = new Question();
            a1.Name = "天空";
            a1.Words = new List<Word>();
            a1.Words.Add(new Word() { Content = "天空" });
            a1.Words.Add(new Word() { Content = "空气" });
            a1.Words.Add(new Word() { Content = "气体" });
            a1.Words.Add(new Word() { Content = "体力" });
            a1.Words.Add(new Word() { Content = "力度" });
            a1.Words.Add(new Word() { Content = "度过" });
            a1.Words.Add(new Word() { Content = "过去" });
            a1.Words.Add(new Word() { Content = "去年" });
            a1.Words.Add(new Word() { Content = "年轻" });
            a1.Words.Add(new Word() { Content = "轻松" });
            a1.Words.Add(new Word() { Content = "松树" });
            a1.Words.Add(new Word() { Content = "树木" });

            this.ViewModel.All.Add(a1);


            Question a2 = new Question();
            a2.Name = "大人";
            a2.Words = new List<Word>();
            a2.Words.Add(new Word() { Content = "大人" });
            a2.Words.Add(new Word() { Content = "人生" });
            a2.Words.Add(new Word() { Content = "生命" });
            a2.Words.Add(new Word() { Content = "命运" });
            a2.Words.Add(new Word() { Content = "运货" });
            a2.Words.Add(new Word() { Content = "货物" });
            a2.Words.Add(new Word() { Content = "物品" });
            a2.Words.Add(new Word() { Content = "品尝" });
            a2.Words.Add(new Word() { Content = "尝试" });
            a2.Words.Add(new Word() { Content = "试验" });
            a2.Words.Add(new Word() { Content = "验证" });
            a2.Words.Add(new Word() { Content = "证明" });

            this.ViewModel.All.Add(a2);


            Question a3 = new Question();
            a3.Name = "红豆";
            a3.Words = new List<Word>();
            a3.Words.Add(new Word() { Content = "红豆" });
            a3.Words.Add(new Word() { Content = "豆沙" });
            a3.Words.Add(new Word() { Content = "沙子" });
            a3.Words.Add(new Word() { Content = "子女" });
            a3.Words.Add(new Word() { Content = "女巫" });
            a3.Words.Add(new Word() { Content = "巫师" });
            a3.Words.Add(new Word() { Content = "师父" });
            a3.Words.Add(new Word() { Content = "父亲节" });
            a3.Words.Add(new Word() { Content = "节约" });
            a3.Words.Add(new Word() { Content = "约见" });
            a3.Words.Add(new Word() { Content = "见面" });
            a3.Words.Add(new Word() { Content = "面粉" });

            this.ViewModel.All.Add(a3);

            Question a4 = new Question();
            a4.Name = "太黑";
            a4.Words = new List<Word>();
            a4.Words.Add(new Word() { Content = "太黑" });
            a4.Words.Add(new Word() { Content = "黑白" });
            a4.Words.Add(new Word() { Content = "白饭" });
            a4.Words.Add(new Word() { Content = "饭菜" });
            a4.Words.Add(new Word() { Content = "菜园" });
            a4.Words.Add(new Word() { Content = "园丁" });
            a4.Words.Add(new Word() { Content = "丁香花" });
            a4.Words.Add(new Word() { Content = "花生" });
            a4.Words.Add(new Word() { Content = "生气" });
            a4.Words.Add(new Word() { Content = "气球" });
            a4.Words.Add(new Word() { Content = "球体" });
            a4.Words.Add(new Word() { Content = "体检" });

            this.ViewModel.All.Add(a4);


            Question a5 = new Question();
            a5.Name = "上面";
            a5.Words = new List<Word>();

            a5.Words.Add(new Word() { Content = "上面" });
            a5.Words.Add(new Word() { Content = "面条" });
            a5.Words.Add(new Word() { Content = "条件" });
            a5.Words.Add(new Word() { Content = "件数" });
            a5.Words.Add(new Word() { Content = "数学" });
            a5.Words.Add(new Word() { Content = "学习" });
            a5.Words.Add(new Word() { Content = "习惯" });
            a5.Words.Add(new Word() { Content = "惯性" });
            a5.Words.Add(new Word() { Content = "性格" });
            a5.Words.Add(new Word() { Content = "格子" });
            a5.Words.Add(new Word() { Content = "子孙" });
            a5.Words.Add(new Word() { Content = "孙悟空" });

            this.ViewModel.All.Add(a5);

        }

        private void initEvent()
        {
            this.btnStudy.Clicked += BtnStudy_Clicked;
            this.btnPractice.Clicked += BtnPractice_Clicked;
            this.btnAddNew.Clicked += BtnAddNew_Clicked;
        }

        private void BtnStudy_Clicked(object sender, EventArgs e)
        {
            touchItem(this.ViewModel.SelectedQuestion, true);
        }

        private void BtnPractice_Clicked(object sender, EventArgs e)
        {
            touchItem(this.ViewModel.SelectedQuestion, false);
        }

        async void touchItem(Question q, bool isSoundPlay)
        {
            var page = new PageBuBuGao2();
            page.ViewModel.Question = q;
            page.ViewModel.IsSoundPlay = isSoundPlay;
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
        public string Name { get; set; }

        public List<Word> Words { get; set; }
    }

    public class Word
    {
        public string Content { get; set; }
    }
}