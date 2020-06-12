﻿using System;
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
            initEvent();
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

        private void initEvent()
        {
            this.btnCRW.Clicked += BtnCRW_Clicked;
            this.btnCRW_Type2.Clicked += BtnCRW_Type2_Clicked;

            this.btnUserList.Clicked += BtnUserList_Clicked;
            this.btnSetScreen.Clicked += BtnSetScreen_Clicked;

            this.btnDB.Clicked += BtnDB_Clicked;
        }

        private void BtnSetScreen_Clicked(object sender, EventArgs e)
        {
            App.Screen.ForcePortrait();
        }

        private void BtnDB_Clicked(object sender, EventArgs e)
        {
            CRW.Game_User o = Client.Common.StaticInfo.InnerSQLiteDB.CRW_rcUser(new CRW.Game_User() { Account = "Howe" });

            string msg = "{0}".FormatWith(Util.JsonUtils.SerializeObject(o));
            System.Diagnostics.Debug.WriteLine(msg);
        }

        System.ComponentModel.BackgroundWorker mBGWorker_OpenGamePage_CRW { get; set; }

        void BtnCRW_Clicked(object sender, EventArgs e)
        {
            openGamePage_CRW(CRWTypeID: 1);
        }

        void BtnCRW_Type2_Clicked(object sender, EventArgs e)
        {
            openGamePage_CRW(CRWTypeID: 2);
        }

        async void openGamePage_CRW(int CRWTypeID)
        {
            if (App.TTS.Check_InitTextToSpeech() == true)
            {
                Game_User = Client.Common.StaticInfo.InnerSQLiteDB.CRW_rcUser(new CRW.Game_User() { Account = this.txtUser.Text });
                var page = new Client.View.Games.CRW.PageMain(CRWTypeID);
                await Navigation.PushAsync(page);
                return;
            }

            // else -- App.TTS.Check_InitTextToSpeech() == false 
            await this.DisplayAlert("提示", "确认打开TTS合成语音", "确认");
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

        async void BtnUserList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Client.View.Games.CRW.PageUserList());
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