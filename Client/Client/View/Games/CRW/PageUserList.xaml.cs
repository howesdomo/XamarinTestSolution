using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.Games.CRW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUserList : ContentPage
    {
        public PageUserList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            App.Screen.ForceLandscapeRight();
            base.OnAppearing();
        }
    }

    public class PageUserListViewModel : ViewModel.BaseViewModel
    {
        public PageUserListViewModel()
        {
            initData();
            initCommand();
        }

        void initData()
        {

            var users = new ObservableCollection<ModelA>();

            foreach (ModelA toAdd in Common.StaticInfo.InnerSQLiteDB.Game_rUserList())
            {
                users.Add(toAdd);
            }

            this.Users = users;
        }

        void initCommand() 
        {
            this.CMD_UserDetail = new Command<ModelA>(showUserDetail);
        }

        private ObservableCollection<ModelA> _Users;

        public ObservableCollection<ModelA> Users
        {
            get { return this._Users; }
            set
            {
                if (this.Users != null)
                {
                    this.Users.CollectionChanged -= Users_CollectionChanged;
                }

                this._Users = value;

                if (this.Users != null)
                {
                    this.Users.CollectionChanged += Users_CollectionChanged;
                }

                this.OnPropertyChanged("Users");
                this.OnPropertyChanged("ListInfo");
            }
        }

        private void Users_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged("Users");
            this.OnPropertyChanged("ListInfo");
        }

        public string ListInfo
        {
            get
            {
                string r = string.Empty;

                if (this.Users != null && this.Users.Count > 0)
                {
                    r = "共 {0} 条".FormatWith(this.Users.Count);
                }

                if (this.SelectedUser != null)
                {
                    r = "共 {0} 条, 选中用户 {1}".FormatWith(this.Users.Count, this.SelectedUser.User.Account);
                }

                return r;
            }
        }

        private ModelA _SelectedUser;

        public ModelA SelectedUser
        {
            get
            {
                return _SelectedUser;
            }
            set
            {
                _SelectedUser = value;
                this.OnPropertyChanged("SelectedUser");
                this.OnPropertyChanged("ListInfo");
            }
        }

        public Command<ModelA> CMD_UserDetail { get; private set; }

        async void showUserDetail(ModelA m)
        {
            System.Diagnostics.Debug.WriteLine(Util.JsonUtils.SerializeObjectWithFormatted(m));            
            await App.Navigation.PushAsync(new Client.View.Games.CRW.PageUserListDetail(m.User));
        }
    }

    public class ModelA
    {
        public CRW.Game_User User { get; set; }

        public CRWLog Type1_CRW_LevelLog { get; set; }

        public CRW_Level Type1_CRW_Level { get; set; }

        public CRWLog Type2_CRW_LevelLog { get; set; }

        public CRW_Level Type2_CRW_Level { get; set; }
    }
}