using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.FFImageLoadingDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSimpleGif : ContentPage
    {
        public PageSimpleGif()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            FFImageLoading.Forms.CachedImage cachedImage = new FFImageLoading.Forms.CachedImage();
            cachedImage.HorizontalOptions = LayoutOptions.Center;
            cachedImage.VerticalOptions = LayoutOptions.Center;
            cachedImage.WidthRequest = 300;
            cachedImage.HeightRequest = 300;
            cachedImage.CacheDuration = TimeSpan.FromSeconds(1d); // CacheDuration (Timespan `, default: `TimeSpan.FromDays(90))

            //Downsampling properties
            //DownSample always maintain original image aspect ratio.If you set both DownsampleWidth and DownsampleHeight, one of them is ignored.

            //DownsampleWidth (int, default: 0)
            //Resizes image to defined width in pixels(or DIP if DownsampleUseDipUnits property is set to true.If you set this property don’t set DownsampleHeight as aspect ratio will be maintained.

            //DownsampleHeight(int, default: 0)
            //Resizes image to defined height in pixels(or DIP if DownsampleUseDipUnits property is set to true.If you set this property don’t set `DownsampleWidth ` as aspect ratio will be maintained.
            cachedImage.DownsampleToViewSize = true;
            cachedImage.DownsampleWidth = 100;
            cachedImage.DownsampleHeight = 100;

            cachedImage.RetryCount = 0; // RetryCount (int, default: 3)
            cachedImage.RetryDelay = 250; // RetryDelay (int, default: 250)
            cachedImage.LoadingPlaceholder = ImageSource.FromResource("Client.Images.FFImageLoading.loading.png");
            cachedImage.ErrorPlaceholder = ImageSource.FromResource("Client.Images.FFImageLoading.error.png"); // 读取失败时没有出现 Error 图片
            // cachedImage.Source = ImageSource.FromUri(new Uri(@"http://cn.bing.com/th?id=OHR.BigWindDay_ZH-CN1837859776_1920x1080.jpg"));
            // cachedImage.Source = ImageSource.FromUri(new Uri(@"http://cn.bing.com/th?id=OHR.BigWindDay_ZH-CN332211_1920x1080.jpg")); // 错误的地址 但没有显示 ErrorPlaceholder
            cachedImage.Source = ImageSource.FromResource("Client.Images.BuBuGao_Japanese.Hiragana.2_i.gif"); // 成功
            // cachedImage.Source = ImageSource.FromResource("Client.Images.FFImageLoading.error.png"); // 成功

            sl1.Children.Add(cachedImage);
        }
    }
}