using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XLabDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCamera1 : ContentPage
    {
        XLabs.Platform.Services.Media.IMediaPicker mediaPicker { get; set; }

        public PageCamera1()
        {
            InitializeComponent();
            this.mediaPicker = XLabs.Ioc.Resolver.Resolve<XLabs.Platform.Services.Media.IMediaPicker>();
            initEvent();
        }

        private void initEvent()
        {
            this.btnTakePhoto.Clicked += BtnTakePhoto_Clicked;
            this.btnSelectPhoto.Clicked += BtnSelectPhoto_Clicked;
        }



        async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                XLabs.Platform.Services.Media.MediaFile mediaFile = await mediaPicker.TakePhotoAsync(new XLabs.Platform.Services.Media.CameraMediaStorageOptions()
                {
                    DefaultCamera = XLabs.Platform.Services.Media.CameraDevice.Rear, // 后置摄像头
                                                                                     // MaxPixelDimension = 400
                });

                System.Diagnostics.Debug.WriteLine($"picture path : { mediaFile.Path }");
                this.txtImgPath.Text = mediaFile.Path;
                img1.Source = mediaFile.Path;
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        async void BtnSelectPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                XLabs.Platform.Services.Media.MediaFile mediaFile = await mediaPicker.SelectPhotoAsync(new XLabs.Platform.Services.Media.CameraMediaStorageOptions());

                System.Diagnostics.Debug.WriteLine($"picture path : { mediaFile.Path }");

                this.txtImgPath.Text = mediaFile.Path;
                img1.Source = mediaFile.Path;
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }



    }
}