using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.BuBuGao
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBuBuGao_AddQuestion : ContentPage
    {
        PageBuBuGao_AddQuestion_ViewModel ViewModel { get; set; }

        public PageBuBuGao_AddQuestion()
        {
            InitializeComponent();
            this.ViewModel = new PageBuBuGao_AddQuestion_ViewModel();

            var abc = new ObservableCollection<WordModel>();
            abc.Add(new WordModel(new Word() { Content = "中岛" }));
            abc.Add(new WordModel(new Word() { Content = "美雪" }));

            this.ViewModel.ToAddWords = abc;

            this.BindingContext = this.ViewModel;
            initEvent();

        }

        private void initEvent()
        {
            this.btnAddNew.Clicked += BtnAddNew_Clicked;
            this.btnClear.Clicked += BtnClear_Clicked;
            this.btnSave.Clicked += BtnSave_Clicked;

        }


        #region 添加

        private void BtnAddNew_Clicked(object sender, EventArgs e)
        {
            try
            {
                addWord(this.txtAddNew.Text);
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        async void addWord(string args)
        {
            if (args.IsNullOrWhiteSpace() == true)
            {
                await DisplayAlert("提示", "请输入词组", "确认");
                return;
            }

            this.ViewModel.AddWord(args);
        }

        #endregion


        #region 清空

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            var task = DisplayAlert("提示", "确认清除?", "确认", "取消");
            if (task.Result == true)
            {
                this.ViewModel.ToAddWords = new ObservableCollection<WordModel>();
            }
        }

        #endregion


        #region 保存

        async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.ToAddWords == null || this.ViewModel.ToAddWords.Count <= 0)
            {
                await DisplayAlert("提示", "保存失败，至少需要添加1项。", "确认");
                return;
            }

            try
            {
                Question toSave = new Question();

                toSave.Words = new List<Word>();
                toSave.Words.AddRange(this.ViewModel.ToAddWords.Select(i => i.Model));

                toSave.Name = toSave.Words[0].Content;
                toSave.CreateDateTime = DateTime.Now;
                toSave.CreateDateTimeValue = toSave.CreateDateTime.Ticks;

                Common.StaticInfo.ExternalSQLiteDB.BuBuGao_cQuestion(toSave);

                foreach (var item in toSave.Words)
                {
                    item.QuestionID = toSave.ID;
                }

                Common.StaticInfo.ExternalSQLiteDB.BuBuGao_cWordList(toSave);

                await DisplayAlert("提示", "保存成功。", "确认");
                await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("提示", "保存报错\r\n{0}".FormatWith(ex.GetFullInfo()), "确认");
            }
        }

        #endregion

    }

    public class PageBuBuGao_AddQuestion_ViewModel : ViewModel.BaseViewModel
    {
        private ObservableCollection<WordModel> toAddWords = new ObservableCollection<WordModel>();

        public ObservableCollection<WordModel> ToAddWords
        {
            get { return toAddWords; }
            set
            {
                this.toAddWords = value;
                this.OnPropertyChanged("ListInfo");
            }
        }

        public string ListInfo
        {
            get
            {
                string r = string.Empty;
                if (this.ToAddWords != null)
                {
                    r = "共 {0} 题".FormatWith(this.ToAddWords.Count);
                }
                return r;

            }
        }

        public void AddWord(string a)
        {
            WordModel toAdd = new WordModel(new Word() { Content = a });
            this.ToAddWords.Add(toAdd);
            this.ToAddWords = this.ToAddWords;
        }

    }

    public class WordModel : ViewModel.ModelItem<Word>
    {
        public WordModel(Word m) : base(m)
        {
            this.Model = m;
        }

        public string Content
        {
            get { return this.Model.Content; }
            set
            {
                this.Model.Content = value;
                this.OnPropertyChanged("Content");
            }
        }

    }

}