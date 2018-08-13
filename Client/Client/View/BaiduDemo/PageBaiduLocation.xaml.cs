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
		public PageBaiduLocation ()
		{
			InitializeComponent ();
            initEvent();
		}

        private void initEvent()
        {
            this.btnGetGPSInfo.Clicked += BtnGetGPSInfo_Clicked;

            // TODO 注销事件
            Common.LBS.GetGPSInfoEvent += new EventHandler<Common.LBSModel>(OnGetGPSInfoHandler);
            
        }

        private void BtnGetGPSInfo_Clicked(object sender, EventArgs e)
        {
            App.LBS.GetGPSInfo();
        }

        private void OnGetGPSInfoHandler(object o, LBSModel e)
        {
            string msg = "{0}".FormatWith(Util.JsonUtils.SerializeObject(e));
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}