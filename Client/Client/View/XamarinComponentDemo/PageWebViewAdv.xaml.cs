using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinComponentDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageWebViewAdv : ContentPage
    {
        public PageWebViewAdv()
        {
            InitializeComponent();

            var IndexHtmlPath = System.IO.Path.Combine(Common.StaticInfo.AndroidExternalFilesPath, "wwwroot", "Index.html");


             

            var htmlSource = new HtmlWebViewSource();
            string html = System.IO.File.ReadAllText(IndexHtmlPath);
            html = html.Replace("****####****", System.IO.Path.Combine(Common.StaticInfo.AndroidExternalFilesPath, "wwwroot", "js", "vue.js"));

            System.Diagnostics.Debug.WriteLine(html);

            htmlSource.Html = html;

            this.webView.Source = htmlSource;

        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            string uri = "allforone://hello?id=d2cff034-c725-459e-b62b-182678446fe1";

            if (await Xamarin.Essentials.Launcher.CanOpenAsync(uri))
            {
                await Xamarin.Essentials.Launcher.OpenAsync(uri);
            }
        }

        void Button_Clicked2(object sender, EventArgs e)
        {
            var IndexJsPath = System.IO.Path.Combine(Common.StaticInfo.AndroidExternalFilesPath, "wwwroot", "Index.js");
            this.webView.EvaluateJavaScriptAsync(System.IO.File.ReadAllText(IndexJsPath));
        }
    }
}