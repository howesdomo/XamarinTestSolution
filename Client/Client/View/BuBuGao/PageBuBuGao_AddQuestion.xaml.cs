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
            this.BindingContext = this.ViewModel;
            initEvent();

        }

        private void initEvent()
        {
            this.txtAddNew.TextChanged += TxtAddNew_TextChanged;
            this.btnAddNew.Clicked += BtnAddNew_Clicked;
            this.btnClear.Clicked += BtnClear_Clicked;
            this.btnSave.Clicked += BtnSave_Clicked;

            // 点击图片事件
            TapGestureRecognizer imageAddWordByScan_TapGesture = new TapGestureRecognizer();
            imageAddWordByScan_TapGesture.Tapped += imageAddWordByScan_TapGesture_Tapped;
            btnAddWordsByScan.GestureRecognizers.Add(imageAddWordByScan_TapGesture);

            // 点击图片事件
            TapGestureRecognizer imageMinusWords_TapGesture = new TapGestureRecognizer();
            imageMinusWords_TapGesture.Tapped += imageMinusWords_TapGesture_Tapped;
            btnMinusWords.GestureRecognizers.Add(imageMinusWords_TapGesture);
        }

        protected override bool OnBackButtonPressed()
        {
            showCloseDisplayAlert();
            return true;
        }

        async void showCloseDisplayAlert()
        {
            if (this.ViewModel.ToAddWords.Count > 0)
            {
                var result = await this.DisplayAlert
                (
                    title: "提示",
                    message: "确认退出？",
                    accept: "确认",
                    cancel: "取消"
                );

                if (result == false)
                {
                    return;
                }
            }

            await Navigation.PopAsync();

        }


        private void TxtAddNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Contains("\n") || e.NewTextValue.Contains("\r"))
            {
                addWord(this.txtAddNew.Text);
            }
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
            var value = args.TrimAdv();
            if (value.IsNullOrWhiteSpace() == true)
            {
                await DisplayAlert("提示", "请输入词组", "确认");
                return;
            }

            this.ViewModel.AddWord(value);
            this.txtAddNew.Text = string.Empty;
            this.txtAddNew.Focus();
        }

        async void imageAddWordByScan_TapGesture_Tapped(object sender, EventArgs e)
        {
            var page = new Common.ZXingBarcodeScanner
            (
                title: "补补高 扫码新增",
                isScanContinuously : true,
                scanResultHandle: scanResultHandle
            );

            await Navigation.PushAsync(page);
        }

        async void scanResultHandle(ZXing.Result result, Common.ZXingBarcodeScanner page)
        {
            string msg = "{0}".FormatWith(Util.JsonUtils.SerializeObject(result));
            System.Diagnostics.Debug.WriteLine(msg);

            addWord(result.Text);


            bool r = await DisplayAlert("添加成功", "已成功添加[{0}]".FormatWith(result.Text), "确认", "取消");

            msg = "{0}".FormatWith(r);
            System.Diagnostics.Debug.WriteLine(msg);

            page.EndWith();
        }

        #endregion


        #region 删除

        async void imageMinusWords_TapGesture_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("提示", "TODO 删除单项 / 多项", "取消");
        }

        #endregion

        #region 清空

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            var task = DisplayAlert("提示", "确认清除?", "确认", "取消");
            if (task.Result == true)
            {
                this.ViewModel.ToAddWords = new ObservableCollection<Word>();
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
                toSave.Words.AddRange(this.ViewModel.ToAddWords);

                toSave.Name = toSave.Words[0].Content;
                toSave.CreateDateTime = DateTime.Now;
                toSave.CreateDateTimeValue = toSave.CreateDateTime.Ticks;

                //var temp = Common.StaticInfo.InnerSQLiteDB.BuBuGao_cQuestion(toSave);
                //await temp;

                var temp = await Common.StaticInfo.InnerSQLiteDB.BuBuGao_cQuestion(toSave); // 上面代码简化得出

                foreach (var item in toSave.Words)
                {
                    item.QuestionID = toSave.ID;
                }

                Common.StaticInfo.InnerSQLiteDB.BuBuGao_cWordList(toSave);

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
        private ObservableCollection<Word> toAddWords = new ObservableCollection<Word>();

        public ObservableCollection<Word> ToAddWords
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
            Word toAdd = new Word() { Content = a };
            this.ToAddWords.Add(toAdd);
            this.OnPropertyChanged("ToAddWords");
            this.OnPropertyChanged("ListInfo");
        }

    }

    //public class WordModel : ViewModel.ModelItem<Word>
    //{
    //    public WordModel(Word m) : base(m)
    //    {
    //        this.Model = m;
    //    }

    //    public string Content
    //    {
    //        get { return this.Model.Content; }
    //        set
    //        {
    //            this.Model.Content = value;
    //            this.OnPropertyChanged("Content");
    //        }
    //    }

    //}

}