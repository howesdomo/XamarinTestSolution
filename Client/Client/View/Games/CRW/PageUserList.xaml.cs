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
        public static string Tag
        {
            get
            {
                return "PageUserList";
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(() =>
            {
                App.Screen.ForceLandscapeRight();
            });
        }

        protected override void OnDisappearing()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Screen.Unspecified();
            });

            base.OnDisappearing();
        }

        public PageUserListViewModel ViewModel { get; set; }

        public PageUserList()
        {
            InitializeComponent();

            initEvent();

            this.ViewModel = new PageUserListViewModel();
            this.BindingContext = this.ViewModel;

            #region init Data

            var users = new ObservableCollection<ModelA>();
            foreach (ModelA toAdd in Common.StaticInfo.InnerSQLiteDB.Game_rUserList())
            {
                users.Add(toAdd);
            }

            this.ViewModel.Users = users;

            #endregion init Data
        }

        private void initEvent()
        {
            this.grid.RowTap += Grid_RowTap;
        }

        async void Grid_RowTap(object sender, DevExpress.Mobile.DataGrid.RowTapEventArgs e)
        {
            if (this.grid.SelectedDataObject != null && this.grid.SelectedDataObject is ModelA)
            {
                this.ViewModel.SelectedUser = this.grid.SelectedDataObject as ModelA;
            }

            if (e.FieldName == "rowItemBtn")
            {
                string msg = "{0}".FormatWith(Util.JsonUtils.SerializeObject(this.ViewModel.SelectedUser));
                System.Diagnostics.Debug.WriteLine(msg);

                await Navigation.PushAsync(new Client.View.Games.CRW.PageUserListDetail(this.ViewModel.SelectedUser.User));
            }
        }

    }

    public class PageUserListViewModel : ViewModel.BaseViewModel
    {
        private ObservableCollection<ModelA> _Users;

        public ObservableCollection<ModelA> Users
        {
            get { return this._Users; }
            set
            {
                this._Users = value;
                this.OnPropertyChanged("Users");
                this.OnPropertyChanged("ListInfo");
            }
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