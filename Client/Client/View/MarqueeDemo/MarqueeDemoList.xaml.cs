using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.MarqueeDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarqueeDemoList : ContentPage
    {
        public MarqueeDemoList()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            Button btn1 = new Button()
            {
                Text = "Label跑马灯V1(效果不好)"
            };

            btn1.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new MarqueeLabel_V1());
            };

            sMain.Children.Add(btn1);





            Button btn2 = new Button()
            {
                Text = "Label跑马灯V2"
            };

            btn2.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new MarqueeLabel_V2());
            };

            sMain.Children.Add(btn2);




            Button btn3 = new Button()
            {
                Text = "Label跑马灯V3"
            };

            btn3.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new MarqueeLabel_V3());
            };

            sMain.Children.Add(btn3);





            Button btn4 = new Button()
            {
                Text = "Label跑马灯V4"
            };

            btn4.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new MarqueeLabel_V4());
            };

            sMain.Children.Add(btn4);
        }
    }
}