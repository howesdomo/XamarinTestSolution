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
    public partial class PageXamarinEssentialsDemo : ContentPage
    {
        public PageXamarinEssentialsDemo()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            btnDeviceInfo.Clicked += BtnDeviceInfo_Clicked;
        }

        async void BtnDeviceInfo_Clicked(object sender, EventArgs e)
        {
            // TODO dynamic 貌似不可以绑定, 待深入研究
            await Navigation.PushAsync(new XamarinEssentials.PageDeviceInfo() { BindingContext = Common.StaticInfo.DeviceInfo });

            //// 暂时先用正常的 Class 进行绑定
            //var model = new DeviceInfo()
            //{
            //    Model = Common.StaticInfo.DeviceInfo.Model,
            //    Manufacturer = Common.StaticInfo.DeviceInfo.Manufacturer,
            //    Name = Common.StaticInfo.DeviceInfo.Name,
            //    VersionString = Common.StaticInfo.DeviceInfo.VersionString,
            //    Platform = Common.StaticInfo.DeviceInfo.Platform,
            //    Idiom = Common.StaticInfo.DeviceInfo.Idiom,
            //    DeviceType = Common.StaticInfo.DeviceInfo.DeviceType
            //};

            //await Navigation.PushAsync(new XamarinEssentials.PageDeviceInfo() { BindingContext = model });
        }
    }

    
}