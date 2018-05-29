using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinEssentials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDeviceInfo : ContentPage
    {
        public PageDeviceInfo()
        {
            InitializeComponent();
            // 测试输出 dynamic ==> Common.StaticInfo.DeviceInfo
            //string json = CoreUtil.JsonUtils.SerializeObject(Common.StaticInfo.DeviceInfo);
            //string msg = "{0}".FormatWith(json);
            //System.Diagnostics.Debug.WriteLine(msg);

            //try
            //{
            //    string args = Common.StaticInfo.DeviceInfo.Name;
            //    System.Diagnostics.Debug.WriteLine("Name : {0}".FormatWith(args));

            //    args = Common.StaticInfo.DeviceInfo.VersionString;
            //    System.Diagnostics.Debug.WriteLine("VersionString : {0}".FormatWith(args));

            //    args = Common.StaticInfo.DeviceInfo.Manufacturer;
            //    System.Diagnostics.Debug.WriteLine("Manufacturer : {0}".FormatWith(args));
            //}
            //catch (Exception)
            //{

            //}
        }
    }
}