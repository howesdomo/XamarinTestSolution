using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDisplayAlertDemoList : ContentPage
    {
        public PageDisplayAlertDemoList()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnDisplayAlert.Clicked += BtnDisplayAlert_Clicked;
            this.btnDisplayAlert_2Selection.Clicked += BtnDisplayAlert_2Selection_Clicked;
            this.btnDisplayAlert_2Selection_V2.Clicked += BtnDisplayAlert_2Selection_V2_Clicked;

            this.btnDisplayActionSheet.Clicked += BtnDisplayActionSheet_Clicked;
            this.btnDisplayActionSheet_V2.Clicked += BtnDisplayActionSheet_V2_Clicked;
        }

        private void BtnDisplayAlert_Clicked(object sender, EventArgs e)
        {
            DisplayAlert
            (
                title: "提示",
                message: "这里是提示信息",
                cancel: "确定"
            );
        }

        async void BtnDisplayAlert_2Selection_Clicked(object sender, EventArgs e)
        {
            // 推荐写法, 学习 async, await 的使用方法
            bool r = await DisplayAlert
            (
                title: "提示",
                message: "这里是提示信息",
                accept: "确定",
                cancel: "取消"
            );

            string msg = "{0}".FormatWith(r);
            System.Diagnostics.Debug.WriteLine(msg);

            await DisplayAlert
            (
                title: "获取返回值",
                message: msg,
                cancel: "确定"
            );
        }

        private void BtnDisplayAlert_2Selection_V2_Clicked(object sender, EventArgs e)
        {
            Task<bool> tr1 = DisplayAlert
            (
                title: "提示",
                message: "这里是提示信息",
                accept: "确定",
                cancel: "取消"
            );

            // 两种写法任选其一
            // 1. 常规写法
            tr1.ContinueWith(tr1_OnHandle);

            // 2. 匿名方法写法
            tr1.ContinueWith((tr) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("选择结果", tr.Result.ToString(), "确定");
                });
            });
        }

        void tr1_OnHandle(Task<bool> tr)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool a = tr.Result;

                string msg = "{0}".FormatWith(a);
                System.Diagnostics.Debug.WriteLine(msg);

                tr1_ShowSelectedResult(msg);

                await DisplayAlert("选择结果", msg, "确定");
            });
        }

        void tr1_ShowSelectedResult(string msg)
        {

        }


        async void BtnDisplayActionSheet_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet
            (
                title: "请选择分享到的位置",
                cancel: "取消",
                destruction: null,
                buttons: new string[] { "QQ空间", "微博", "微信" }
            );

            await DisplayAlert("提示", "选中了[{0}]".FormatWith(action), "确定");
        }

        async void BtnDisplayActionSheet_V2_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet
            (
                title: "请选择分享到的位置",
                cancel: "取消",
                destruction: "删除",
                buttons: new string[] { "QQ空间", "微博", "微信" }
            );

            await DisplayAlert("提示", "选中了[{0}]".FormatWith(action), "确定");
        }
    }
}