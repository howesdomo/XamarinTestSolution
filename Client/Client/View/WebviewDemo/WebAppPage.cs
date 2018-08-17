using System;
using Xamarin.Forms;

namespace Client.View.WebviewDemo
{
    public class WebAppPage : ContentPage
    {
        public WebAppPage()
        {
            var openUrl = new Button
            {
                Text = "用系统默认浏览器打开地址"
            };
            openUrl.Clicked += (sender, e) =>
            {
                Device.OpenUri(new Uri("http://www.bing.com"));
            };

            var makeCall = new Button
            {
                Text = "用系统拨号软件拨打号码"
            };
            makeCall.Clicked += (sender, e) =>
            {
                Device.OpenUri(new Uri("tel:13126486562"));
            };

            Content = new StackLayout
            {
                Padding = new Thickness(5, 20, 5, 0),
                HorizontalOptions = LayoutOptions.Fill,
                Children = {
                    openUrl,
                    makeCall
                }
            };
        }
    }
}
