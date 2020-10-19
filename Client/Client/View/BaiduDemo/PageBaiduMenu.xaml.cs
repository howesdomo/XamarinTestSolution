using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.BaiduDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBaiduMenu : ContentPage
    {
        public PageBaiduMenu()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnPageBaiduLocation.Clicked += BtnPageBaiduLocation_Clicked;
            this.btnLBSPicker.Clicked += BtnLBSPicker_Clicked;
            this.btnLBSMultiPicker.Clicked += BtnLBSMultiPicker_Clicked;
        }

        void BtnPageBaiduLocation_Clicked(object sender, EventArgs e)
        {
            App.DebounceAction.Debounce
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PushAsync(new PageBaiduLocation());
                    });
                },
                syncInvoke: null
            );
        }

        private void BtnLBSPicker_Clicked(object sender, EventArgs e)
        {
            App.DebounceAction.Debounce
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var view = await Common.LBSPicker.GetInstance();
                        if (view != null)
                        {
                            await App.Navigation.PushAsync(view);
                        }
                    });
                },
                syncInvoke: null
            );
        }

        private void BtnLBSMultiPicker_Clicked(object sender, EventArgs e)
        {
            App.DebounceAction.Debounce
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var view = await Common.LBSMultiPicker.GetInstance();
                        if (view != null)
                        {
                            await App.Navigation.PushAsync(view);
                        }
                    });
                },
                syncInvoke: null
            );
        }
    }
}