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

            this.btnScreenStayOn.Clicked += BtnScreenStayOn_Clicked;
            this.btnScreenCanTurnOff.Clicked += BtnScreenCanTurnOff_Clicked;

            this.btnDB.Clicked += BtnDB_Clicked;
        }

        private void BtnDB_Clicked(object sender, EventArgs e)
        {
            CRW.Game_User o = Client.Common.StaticInfo.ExternalSQLiteDB.CRW_rcUser(new CRW.Game_User() { Account = "Howe" });

            string msg = "{0}".FormatWith(Util.JsonUtils.SerializeObject(o));
            System.Diagnostics.Debug.WriteLine(msg);
        }

        async void BtnCRW_Clicked(object sender, EventArgs e)
        {
            if (App.TTS.Check_InitTextToSpeech() == false)
            {
                bool r1 = await this.DisplayAlert("提示", "检测到未打开TTS合成语音, 确认打开?", "确认", "取消");

                if (r1 == true)
                {
                    App.TTS.InitTextToSpeech();
                    return;
                }
            }

            Game_User = Client.Common.StaticInfo.ExternalSQLiteDB.CRW_rcUser(new CRW.Game_User() { Account = this.txtUser.Text });

            var page = new Client.View.Games.CRW.PageMain(1);

            await Navigation.PushAsync(page);
        }


        async void BtnCRW_Type2_Clicked(object sender, EventArgs e)
        {
            if (App.TTS.Check_InitTextToSpeech() == false)
            {
                bool r1 = await this.DisplayAlert("提示", "检测到未打开TTS合成语音, 确认打开?", "确认", "取消");

                if (r1 == true)
                {
                    App.TTS.InitTextToSpeech();
                    return;
                }
            }

            Game_User = Client.Common.StaticInfo.ExternalSQLiteDB.CRW_rcUser(new CRW.Game_User() { Account = this.txtUser.Text });

            var page = new Client.View.Games.CRW.PageMain(2);

            await Navigation.PushAsync(page);
        }

        /// <summary>
        /// 测试失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScreenStayOn_Clicked(object sender, EventArgs e)
        {
            // Xamarin.Essentials.Platform.GetCurrentActivity (System.Boolean throwOnNull) [0x00018] in <805b0dc2c64d43b7831031b129534f87>:0 
            // 由于获取到的 GetCurrentActivity 故无法设置常亮

            try
            {
                Xamarin.Essentials.ScreenLock.RequestActive();
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        /// <summary>
        /// 测试失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScreenCanTurnOff_Clicked(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Essentials.ScreenLock.RequestRelease();
            }
            catch (Exception ex)
            {

                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }



    }
}