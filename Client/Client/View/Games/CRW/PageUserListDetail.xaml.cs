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
        private CRW.Game_User User { get; set; }        

        public PageUserListDetail(Game_User user)
        {
            InitializeComponent();
            this.User = user;
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as PageUserListDetail_ViewModel).initData(this.User);
            base.OnAppearing();
        }
    }

    public class PageUserListDetail_ViewModel : ViewModel.BaseViewModel
    {
        private static readonly object _LOCK_ = new object();

        public PageUserListDetail_ViewModel()
        {
            initCommand();
        }

        public void initData(CRW.Game_User data)
        {
            if (this.User != null)
            {
                return;
            }

            lock (_LOCK_)
            {
                if (this.User != null)
                {
                    return;
                }

                this.User = data;

                var a1 = new ObservableCollection<DailyUserRecord>();

                foreach (var item in Common.StaticInfo.InnerSQLiteDB.Game_rUserDetail(this.User, 1))
                {
                    a1.Add(item);
                }

                this.CRWType1List = a1;


                var a2 = new ObservableCollection<DailyUserRecord>();

                foreach (var item in Common.StaticInfo.InnerSQLiteDB.Game_rUserDetail(this.User, 2))
                {
                    a2.Add(item);
                }

                this.CRWType2List = a2;
            }
        }

        void initCommand()
        {
            this.CMD_TapTypeButton = new Command<int>(tapTypeButton);
        }


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


        public Command<int> CMD_TapTypeButton { get; private set; }

        void tapTypeButton(int typeId)
        {
            this.Type1_IsVisible = false;
            this.Type2_IsVisible = false;

            switch (typeId)
            {
                case 1:
                    this.Type1_IsVisible = true;

                    break;
                case 2:
                    this.Type2_IsVisible = true;
                    break;
                default:
                    break;
            }
        }


        private bool _Type1_IsVisible = true;
        public bool Type1_IsVisible
        {
            get { return _Type1_IsVisible; }
            set
            {
                _Type1_IsVisible = value;
                this.OnPropertyChanged();
            }
        }


        private bool _Type2_IsVisible;
        public bool Type2_IsVisible
        {
            get { return _Type2_IsVisible; }
            set
            {
                _Type2_IsVisible = value;
                this.OnPropertyChanged();
            }
        }


    }
}