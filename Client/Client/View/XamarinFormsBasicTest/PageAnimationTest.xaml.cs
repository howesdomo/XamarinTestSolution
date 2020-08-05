using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XamarinFormsBasicTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAnimationTest : ContentPage
    {
        public PageAnimationTest()
        {
            InitializeComponent();
        }

        async void b0_Btn0_Clicked(object sender, EventArgs e)
        {
            await b0.RotateTo(90, TimeSpan.FromSeconds(1).AnimationDuration_XamarinForms(), Easing.Linear);
        }

        async void b0_Btn1_Clicked(object sender, EventArgs e)
        {
            await b0.RelRotateTo(90, TimeSpan.FromSeconds(1).AnimationDuration_XamarinForms(), Easing.Linear);
        }

        async void b1_Btn0_Clicked(object sender, EventArgs e)
        {
            await b1.ScaleTo(1.2d, TimeSpan.FromSeconds(1).AnimationDuration_XamarinForms(), Easing.Linear);
        }

        async void b1_Btn1_Clicked(object sender, EventArgs e)
        {
            await b1.RelScaleTo(0.2d, TimeSpan.FromSeconds(1).AnimationDuration_XamarinForms(), Easing.Linear);
        }

        private void b1_Reset_Clicked(object sender, EventArgs e)
        {
            b1.Scale = 1;
        }

        async void b2_Btn0_Clicked(object sender, EventArgs e)
        {
            double radius = Math.Min(sl2.Width, sl2.Height) / 2;
            b2.AnchorY = radius / b2.Height;
            await b2.RotateTo(360, TimeSpan.FromSeconds(2).AnimationDuration_XamarinForms());
        }

        async void b2_Btn1_Clicked(object sender, EventArgs e)
        {
            double radius = Math.Min(sl2.Width, sl2.Height) / 2;
            b2.AnchorY = radius / b2.Height;
            await b2.RelRotateTo(360, TimeSpan.FromSeconds(2).AnimationDuration_XamarinForms());
        }

        async void b3_Btn0_Clicked(object sender, EventArgs e)
        {
            await b3.TranslateTo(-25, 25, TimeSpan.FromSeconds(2).AnimationDuration_XamarinForms());
        }

        private void b3_Reset_Clicked(object sender, EventArgs e)
        {
            b3.TranslationX = 0;
            b3.TranslationY = 0;
        }

        async void b4_Btn0_Clicked(object sender, EventArgs e)
        {
            await b4.FadeTo(0, TimeSpan.FromSeconds(2).AnimationDuration_XamarinForms());
        }

        private void b4_Reset_Clicked(object sender, EventArgs e)
        {
            b4.Opacity = 1;
        }

        async void b5_Btn0_Clicked(object sender, EventArgs e)
        {
            await b5.TranslateTo(-100, 0, 1000);    // Move image left
            await b5.TranslateTo(-100, -100, 1000); // Move image up
            await b5.TranslateTo(100, 100, 2000);   // Move image diagonally down and right
            await b5.TranslateTo(0, 100, 1000);     // Move image left
            await b5.TranslateTo(0, 0, 1000);       // Move image up
        }

        async void b6_Btn0_Clicked(object sender, EventArgs e)
        {
            // b6.RotateTo(360, 4000); // 进行旋转的同时
            _ = b6.RotateTo(360, 4000); // 进行旋转的同时
            await b6.ScaleTo(2, 2000); // 1. 进行放大2倍
            await b6.ScaleTo(1, 2000); // 2. 进行缩小回原状


            b6.Rotation = 0; // 重置旋转角度
        }

        async void b7_Btn0_Clicked(object sender, EventArgs e)
        {
            // 只要有一个完成了, 就继续进行下面操作
            await Task.WhenAny<bool>
            (
                b7.RotateTo(360, 4000),
                b7.ScaleTo(2, 2000)
            );

            await b7.ScaleTo(1, 2000);

            b7.Rotation = 0; // 重置旋转角度
        }

        private void b7_Btn1_Clicked(object sender, EventArgs e)
        {
            ViewExtensions.CancelAnimations(b7);
        }

    }
}