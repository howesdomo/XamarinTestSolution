using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.GestureDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePinch : ContentPage
    {

        /// <summary>
        /// 使用此 Container 在 Android 7.0 (小米4C) 中仍然无法实现缩放功能, 
        /// 依然会报错 System.MissingMethodException : Method not found :void
        /// Android.Support.V4.View.ScaleGestureDetectorCompat.SetQuickScaleEnabled(Android.Views.ScaleGestureDetector, bool)
        /// </summary>

        public PagePinch()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            imgPinch.Source = ImageSource.FromResource("Client.Images.GestureDemo.Pinch.png");
        }
    }
}