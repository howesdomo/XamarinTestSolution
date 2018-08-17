using System;
using Xamarin.Forms;

namespace Client.View.WebviewDemo
{
    public class WebPage : ContentPage
    {
        public WebPage()
        {
            var browser = new WebView();
            browser.Source = "http://www.bing.com";
            Content = browser;
        }
    }
}

