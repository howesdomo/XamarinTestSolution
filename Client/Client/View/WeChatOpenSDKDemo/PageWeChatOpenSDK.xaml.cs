using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.WeChatOpenSDKDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageWeChatOpenSDK : ContentPage
    {
        public PageWeChatOpenSDK()
        {
            InitializeComponent();
        }        

        private void Button_OpenWXApp(object sender, EventArgs e)
        {
            try
            {
                var isOpened = App.MyWechatOpenSDK.OpenWechat();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                {
                    Title = "捕获异常",
                    Message = ex.GetInfo(),
                    OkText = "确认"
                });
            }
        }

        private void Button_SendMsg0(object sender, EventArgs e)
        {
            try
            {
                bool r = App.MyWechatOpenSDK.SendMsg("发送的内容", "显示在分享对话框的信息", Common.WXScene.Session);
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                {
                    Title = "捕获异常",
                    Message = ex.GetInfo(),
                    OkText = "确认"
                });
            }
        }

        private void Button_SendMsg1(object sender, EventArgs e)
        {
            try
            {
                bool r = App.MyWechatOpenSDK.SendMsg("发送的内容", "显示在分享对话框的信息", Common.WXScene.Timeline);
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                {
                    Title = "捕获异常",
                    Message = ex.GetInfo(),
                    OkText = "确认"
                });
            }
        }

        private void Button_SendImage0(object sender, EventArgs e)
        {
            try
            {
                bool r = App.MyWechatOpenSDK.SendImage("/storage/emulated/0/DCIM/Screenshots/20200610_1759000318140.png", Common.WXScene.Session);
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                {
                    Title = "捕获异常",
                    Message = ex.GetInfo(),
                    OkText = "确认"
                });
            }
        }

        private void Button_SendImage1(object sender, EventArgs e)
        {
            try
            {
                bool r = App.MyWechatOpenSDK.SendImage("/storage/emulated/0/DCIM/Screenshots/20200610_1759000318140.png", Common.WXScene.Timeline);
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                {
                    Title = "捕获异常",
                    Message = ex.GetInfo(),
                    OkText = "确认"
                });
            }
        }

    }
}