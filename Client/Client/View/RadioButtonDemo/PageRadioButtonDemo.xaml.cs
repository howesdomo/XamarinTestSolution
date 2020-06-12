using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.RadioButtonDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRadioButtonDemo : ContentPage
    {
        public PageRadioButtonDemo()
        {
            InitializeComponent();
        }
    }

    public class PageTest_MyRadioButtons_ViewModel : ViewModel.BaseViewModel
    {
        public Command CMD_ShowSL1 { get; set; }
        public Command CMD_ShowSL2 { get; set; }

        public PageTest_MyRadioButtons_ViewModel()
        {
            initCMD();
            initData();

            this.sl2_SelectedItem = this.MyList.FirstOrDefault(i => i.IsChecked == true);
        }

        private void initCMD()
        {
            CMD_ShowSL1 = new Command(() =>
            {
                this.sl2_IsVisible = false;
            });

            CMD_ShowSL2 = new Command(() =>
            {
                this.sl2_IsVisible = true;
            });
        }

        #region initData
        private void initData()
        {
            this.MyList = new List<CheckBoxTestModel>()
            {
                new CheckBoxTestModel() { DisplayName = "Item 0", IsChecked = false },
                new CheckBoxTestModel() { DisplayName = "Item 1", IsChecked = false },
                new CheckBoxTestModel() { DisplayName = "Item 2 (默认选中)", IsChecked = true }, // 默认选择
                new CheckBoxTestModel() { DisplayName = "长篇大论测试:四六级考试在即，有道考神的老师们精心准备了两篇特稿！内容涵盖四六级考前救命100词、写作秘籍和8大万能基础句。文章很长，但每一个知识点，都曾拯救过数十万同学的四六级考试。请务必要耐心的读到最后！你的四六级会因它而提分！", IsChecked = false }
            };
        }

        public List<CheckBoxTestModel> _MyList;
        public List<CheckBoxTestModel> MyList
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

        private int _sl2_SelectedIndex;
        public int sl2_SelectedIndex
        {
            get => _sl2_SelectedIndex;
            set
            {
                _sl2_SelectedIndex = value;
                this.OnPropertyChanged();
            }
        }

        private CheckBoxTestModel _sl2_SelectedItem;
        public CheckBoxTestModel sl2_SelectedItem
        {
            get => _sl2_SelectedItem;
            set
            {
                _sl2_SelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        private string _sl2_SelectedNo;
        public string sl2_SelectedNo
        {
            get => _sl2_SelectedNo;
            set
            {
                _sl2_SelectedNo = value;
                this.OnPropertyChanged();
            }
        }

        private string _sl2_SelectedText;
        public string sl2_SelectedText
        {
            get => _sl2_SelectedText;
            set
            {
                _sl2_SelectedText = value;
                this.OnPropertyChanged();
            }
        }
    }

    public class CheckBoxTestModel
    {
        public string DisplayName { get; set; }

        public bool IsChecked { get; set; }
    }
}