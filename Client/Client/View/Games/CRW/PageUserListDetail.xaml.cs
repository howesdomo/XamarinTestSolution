using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.Games.CRW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUserListDetail : ContentPage
    {
        PageUserListDetailViewModel ViewModel { get; set; }

        public PageUserListDetail(Game_User user)
        {
            InitializeComponent();
            initUI();
            initEvent();

            this.ViewModel = new PageUserListDetailViewModel();
            this.BindingContext = this.ViewModel;

            this.ViewModel.User = user;
            initData();
        }

        private void initData()
        {
            
        }

        private void initUI()
        {
            uc1.IsVisible = true;
            uc2.IsVisible = false;
        }


        private void initEvent()
        {
            this.btnCRW_Type1.Clicked += BtnCRW_Type1_Clicked;
            this.btnCRW_Type2.Clicked += BtnCRW_Type2_Clicked;
        }

        private void BtnCRW_Type1_Clicked(object sender, EventArgs e)
        {
            A(1);
        }

        private void BtnCRW_Type2_Clicked(object sender, EventArgs e)
        {
            A(2);
        }

        private void A(int i)
        {
            uc1.IsVisible = false;
            uc2.IsVisible = false;

            switch (i)
            {
                case 1: uc1.IsVisible = true; break;
                case 2: uc2.IsVisible = true; break;
            }

            Common.StaticInfo.ExternalSQLiteDB.Game_rUserDetail(this.ViewModel.User);
        }

    }

    public class PageUserListDetailViewModel : ViewModel.BaseViewModel
    {
        private CRW.Game_User _User;
        public CRW.Game_User User
        {
            get
            {
                return _User;
            }
            set
            {
                this._User = value;
                this.OnPropertyChanged("User");
            }
        }


        private ObservableCollection<AAA> _CRWType1List;
        public ObservableCollection<AAA> CRWType1List
        {
            get
            {
                return _CRWType1List;
            }
            set
            {
                this._CRWType1List = value;
                this.OnPropertyChanged("CRWType1List");
            }
        }

        private ObservableCollection<AAA> _CRWType2List;
        public ObservableCollection<AAA> CRWType2List
        {
            get
            {
                return _CRWType2List;
            }
            set
            {
                this._CRWType2List = value;
                this.OnPropertyChanged("CRWType2List");
            }
        }

    }

    public class AAA
    {
        public string Date { get; set; }

        public string LevelName { get; set; }

        public string Time { get; set; }
    }
}