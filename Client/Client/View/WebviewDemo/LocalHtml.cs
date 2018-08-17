using Xamarin.Forms;

namespace Client.View.WebviewDemo
{
    public class LocalHtml : ContentPage
    {
        public LocalHtml()
        {
            var browser = new WebView();

            var htmlSource = new HtmlWebViewSource();

            htmlSource.Html = @"<html><body>
<h1>Xamarin.Forms</h1>
<p>Welcome to WebView.</p>
<p>加载静态HTML到WebView.</p>
</body>
</html>";

            browser.Source = htmlSource;
            Content = browser;
        }
    }
}
