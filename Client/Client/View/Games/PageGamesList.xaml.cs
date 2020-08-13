using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Client.View.Games
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGamesList : ContentPage
    {
        public static CRW.Game_User Game_User { get; set; }

        public PageGamesList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 监控安卓物理返回键
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // 由以下3个 showCloseDisplayAlert 得出
            // 最简单的写法 showCloseDisplayAlert
            // 需要注意 showCloseDisplayAlertV1 无法运行的原因
            showCloseDisplayAlert();
            return true;
        }

        protected override void OnAppearing()
        {
            App.Screen.Unspecified();
            base.OnAppearing();
        }

        async void showCloseDisplayAlert()
        {
            var result = await this.DisplayAlert
            (
                title: "提示",
                message: "确认退出？",
                accept: "确认",
                cancel: "取消"
            );

            if (result)
            {
                await Navigation.PopAsync(true);
            }
        }
    }

    public class PageGamesList_ViewModel : ViewModel.BaseViewModel
    {
        public PageGamesList_ViewModel()
        {
            initCommand();
        }

        void initCommand() 
        {
            CMD_Tap_CRW_TypeX = new Command<int>(tap_CRW_TypeX);
            CMD_TapUserList = new Command(tapUserList);
        }

        private string _UI_UserName = "Howe";

        public string UI_UserName
        {
            get { return _UI_UserName; }
            set { _UI_UserName = value;
                this.OnPropertyChanged();
            }
        }

        public Command<int> CMD_Tap_CRW_TypeX { get; private set; }

        void tap_CRW_TypeX(int CRWTypeID)
        {
            openGamePage_CRW(CRWTypeID);
        }

        System.ComponentModel.BackgroundWorker mBGWorker_OpenGamePage_CRW { get; set; }

        async void openGamePage_CRW(int CRWTypeID)
        {
            if (App.TTS.Check_InitTextToSpeech() == true)
            {
                PageGamesList.Game_User = Client.Common.StaticInfo.InnerSQLiteDB.CRW_rcUser(new CRW.Game_User() { Account = UI_UserName });
                var page = new Client.View.Games.CRW.PageMain(CRWTypeID);
                await App.Navigation.PushAsync(page);
                return;
            }

            // else -- App.TTS.Check_InitTextToSpeech() == false 
            await App.Navigation.DisplayAlert("提示", "确认打开TTS合成语音", "确认");
            App.TTS.InitTextToSpeech();

            if (mBGWorker_OpenGamePage_CRW == null)
            {
                mBGWorker_OpenGamePage_CRW = new System.ComponentModel.BackgroundWorker();
                mBGWorker_OpenGamePage_CRW.DoWork += mBGWorker_DoWork;
                mBGWorker_OpenGamePage_CRW.RunWorkerCompleted += mBGWorker_RunWorkerCompleted;
            }

            if (mBGWorker_OpenGamePage_CRW.IsBusy == true)
            {
                return;
            }

            mBGWorker_OpenGamePage_CRW.RunWorkerAsync(CRWTypeID);
        }

        private void mBGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int countDown = 2;

            int argsCRWTypeID = (int)e.Argument;

            while (countDown > 0)
            {
                if (App.TTS.Check_InitTextToSpeech() == true)
                {
                    break;
                }
                System.Threading.Thread.Sleep(500);
            }

            e.Result = argsCRWTypeID;
        }

        void mBGWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string msg = "{0}".FormatWith(e.Error.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
            else
            {
                int rCRWTypeID = (int)e.Result;
                openGamePage_CRW(rCRWTypeID);
            }
        }




        public Command CMD_TapUserList { get; private set; }
        
        async void tapUserList()
        {
            await App.Navigation.PushAsync(new Client.View.Games.CRW.PageUserList());
        }

    }

    public class PageGameListEventArgs : EventArgs
    {
        public PageGameListEventArgs(int _CRWTypeID, bool _IsTTSOpen)
        {
            this.CRWTypeID = _CRWTypeID;
            this.IsTTSOpen = _IsTTSOpen;
        }

        public int CRWTypeID { get; private set; }

        public bool IsTTSOpen { get; private set; }
    }
}