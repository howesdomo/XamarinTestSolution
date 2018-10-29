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
        public static string Tag
        {
            get
            {
                return "PageUserListDetail";
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(() =>
            {
                App.ScreenDirection.ForceLandscape();
            });
        }

        protected override void OnDisappearing()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.ScreenDirection.Unspecified();
            });

            base.OnDisappearing();
        }

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
            var a1 = new ObservableCollection<DailyUserRecord>();

            foreach (var item in Common.StaticInfo.ExternalSQLiteDB.Game_rUserDetail(this.ViewModel.User, 1))
            {
                a1.Add(item);
            }

            this.ViewModel.CRWType1List = a1;
            uc1.SetBindingContext(this.ViewModel.CRWType1List);


            var a2 = new ObservableCollection<DailyUserRecord>();

            foreach (var item in Common.StaticInfo.ExternalSQLiteDB.Game_rUserDetail(this.ViewModel.User, 2))
            {
                a2.Add(item);
            }

            this.ViewModel.CRWType2List = a2;
            uc2.SetBindingContext(this.ViewModel.CRWType2List);
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
            btnClick(1);
        }

        private void BtnCRW_Type2_Clicked(object sender, EventArgs e)
        {
            btnClick(2);
        }

        private void btnClick(int i)
        {
            uc1.IsVisible = false;
            uc2.IsVisible = false;

            switch (i)
            {
                case 1: uc1.IsVisible = true; break;
                case 2: uc2.IsVisible = true; break;
            }
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


        private ObservableCollection<DailyUserRecord> _CRWType1List;
        public ObservableCollection<DailyUserRecord> CRWType1List
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


        private ObservableCollection<DailyUserRecord> _CRWType2List;
        public ObservableCollection<DailyUserRecord> CRWType2List
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
}