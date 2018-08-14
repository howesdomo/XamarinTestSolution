using Client.Common;
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
    public partial class PageBaiduLocation : ContentPage
    {
        public PageBaiduLocation()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnGetGPSInfo.Clicked += BtnGetGPSInfo_Clicked;


            Common.LBS.GetGPSInfoEvent += new EventHandler<Common.LBSModel>(OnGetGPSInfoHandler);
        }

        private void BtnGetGPSInfo_Clicked(object sender, EventArgs e)
        {
            App.LBS.GetGPSInfo();
        }

        private void OnGetGPSInfoHandler(object o, LBSModel args)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.gErrorInfo.IsVisible = false;

                if (args.IsComplete == false)
                {
                    string msg = "{0}".FormatWith(args.ExceptionInfo);
                    System.Diagnostics.Debug.WriteLine(msg);
                    this.gErrorInfo.IsVisible = true;
                }

                if (args.IsSuccess == false)
                {
                    string msg = "{0}".FormatWith(args.BusinessExceptionInfo);
                    System.Diagnostics.Debug.WriteLine(msg);
                    this.gErrorInfo.IsVisible = true;
                }

                bindData(args);
            });
        }

        private void bindData(object obj)
        {
            this.BindingContext = obj;
        }

    }
}