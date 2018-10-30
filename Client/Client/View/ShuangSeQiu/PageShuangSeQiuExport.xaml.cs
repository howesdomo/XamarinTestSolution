using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.ShuangSeQiu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageShuangSeQiuExport : ContentPage
    {
        public PageShuangSeQiuExport()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            //this.lblRedInfo.Margin = new Thickness(left: 20, top: 0, right: 20, bottom: 0);
            //this.lblBlueInfo.Margin = new Thickness(left: 20, top: 0, right: 20, bottom: 0);

            this.img.Source = ImageSource.FromResource("Client.Images.ShuangSeQiu.TheGodofFortune.png");
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
    }

    public class PageShuangSeQiuExport_ViewModel
    {
        public string RedInfo { get; set; }

        public string BlueInfo { get; set; }
    }
}