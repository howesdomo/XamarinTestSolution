using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.MultiSelectDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDemo_MultiSelect : ContentPage
    {
        public PageDemo_MultiSelect()
        {
            InitializeComponent();
            btnShowValue2.Clicked += BtnShowValue2_Clicked;
        }

        private void BtnShowValue2_Clicked(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Indexes : {Util.JsonUtils.SerializeObjectWithFormatted(testUI.SelectedIndexes)}");
            sb.AppendLine($"Items : {Util.JsonUtils.SerializeObjectWithFormatted(testUI.SelectedItems)}");
            sb.AppendLine($"Nos : {Util.JsonUtils.SerializeObjectWithFormatted(testUI.SelectedNos)}");
            sb.AppendLine($"Texts : {Util.JsonUtils.SerializeObjectWithFormatted(testUI.SelectedTexts)}");

            Application.Current.MainPage.DisplayAlert(title: "输出 From 控件", message: sb.ToString(), cancel: "确定");
        }
    }

    public class PageDemo_MultiSelect_ViewModel : ViewModel.BaseViewModel
    {
        public Command CMD_ShowValue { get; set; }

        public PageDemo_MultiSelect_ViewModel()
        {
            CMD_ShowValue = new Command(() => 
            {
                StringBuilder sb = new StringBuilder();                
                sb.AppendLine($"Indexes : {Util.JsonUtils.SerializeObjectWithFormatted(this.sl2_SelectedIndexes)}");
                sb.AppendLine($"Items : {Util.JsonUtils.SerializeObjectWithFormatted(this.sl2_SelectedItems)}");
                sb.AppendLine($"Nos : {Util.JsonUtils.SerializeObjectWithFormatted(this.sl2_SelectedNos)}");
                sb.AppendLine($"Texts : {Util.JsonUtils.SerializeObjectWithFormatted(this.sl2_SelectedTexts)}");

                Application.Current.MainPage.DisplayAlert(title: "输出 From ViewModel", message: sb.ToString(), cancel: "确定");
            });

            initData();
        }        

        #region initData
        private void initData()
        {
            this.MyList = new List<RadioButtonDemo.CheckBoxTestModel>()
            {
                new RadioButtonDemo.CheckBoxTestModel() { DisplayName = "Item 0", IsChecked = false },
                new RadioButtonDemo.CheckBoxTestModel() { DisplayName = "Item 1", IsChecked = false },
                new RadioButtonDemo.CheckBoxTestModel() { DisplayName = "Item 2", IsChecked = false },
                new RadioButtonDemo.CheckBoxTestModel() { DisplayName = "长篇大论测试:四六级考试在即，有道考神的老师们精心准备了两篇特稿！内容涵盖四六级考前救命100词、写作秘籍和8大万能基础句。文章很长，但每一个知识点，都曾拯救过数十万同学的四六级考试。请务必要耐心的读到最后！你的四六级会因它而提分！", IsChecked = false }
            };
        }

        public List<RadioButtonDemo.CheckBoxTestModel> _MyList;
        public List<RadioButtonDemo.CheckBoxTestModel> MyList
        {
            get { return _MyList; }
            set
            {
                _MyList = value;
                this.OnPropertyChanged();
            }
        }

        public List<string> XingBie { get; set; } = new List<string>() { "男", "女" };

        public string[] XueLi { get; set; } = new string[3] { "初中", "高中", "大学" };

        #endregion

        private bool _sl2_IsVisible;
        public bool sl2_IsVisible
        {
            get => _sl2_IsVisible;
            set
            {
                _sl2_IsVisible = value;
                this.OnPropertyChanged();
            }
        }

        private IEnumerable<int> _sl2_SelectedIndexes;
        public IEnumerable<int> sl2_SelectedIndexes
        {
            get => _sl2_SelectedIndexes;
            set
            {
                _sl2_SelectedIndexes = value;
                this.OnPropertyChanged();
            }
        }

        private object _sl2_SelectedItems;
        public object sl2_SelectedItems
        {
            get => _sl2_SelectedItems;
            set
            {
                _sl2_SelectedItems = value;
                this.OnPropertyChanged();
            }
        }

        private IEnumerable<string> _sl2_SelectedNos;
        public IEnumerable<string> sl2_SelectedNos
        {
            get => _sl2_SelectedNos;
            set
            {
                _sl2_SelectedNos = value;
                this.OnPropertyChanged();
            }
        }

        private IEnumerable<string> _sl2_SelectedTexts;
        public IEnumerable<string> sl2_SelectedTexts
        {
            get => _sl2_SelectedTexts;
            set
            {
                _sl2_SelectedTexts = value;
                this.OnPropertyChanged();
            }
        }
    }
}