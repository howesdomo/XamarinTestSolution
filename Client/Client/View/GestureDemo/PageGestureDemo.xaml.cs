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
    public partial class PageGestureDemo : ContentPage
    {
        public PageGestureDemo()
        {
            InitializeComponent();
            initUI();
            initEvent();
        }

        private void initUI()
        {
            imgPinch.Source = ImageSource.FromResource("Client.Images.GestureDemo.Pinch.png");
        }

        private void initEvent()
        {
            var tap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2,
                Command = new Command(() =>
                {
                    lblTap2.Text = "你点击了2次(手势识别C#定义)";
                })
            };
            this.lblTap2.GestureRecognizers.Add(tap);
        }

        /// <summary>
        /// XAML定义点击手势
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            this.lblTap.Text = "你点击了2次";
        }

        /// <summary>
        /// XAML定义缩放手势
        /// 
        /// 安卓使用缩放手势会报错 // 未解决
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            // ScaleOrigin：缩放手势中心点
            if (sender is Image == false)
            {
                return;
            }

            Image lbl = (Image)sender;
            switch (e.Status)
            {
                case GestureStatus.Started:
                    break;
                case GestureStatus.Running:
                    {
                        if (e.Scale > 1) // 放大
                        {
                            lbl.Scale += 0.1;
                        }
                        else if (e.Scale < 1) // 缩小
                        {
                            lbl.Scale -= 0.1;
                        }
                        else // e.Scale == 1 // 无变化
                        {

                        }
                    }
                    break;
                case GestureStatus.Completed:
                    break;
                case GestureStatus.Canceled:
                    break;
            }

        }

        /// <summary>
        /// XAML定义平移手势
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (sender is Label == false)
            {
                return;
            }

            Label lbl = sender as Label;

            switch (e.StatusType)
            {
                case GestureStatus.Started: break;
                case GestureStatus.Running:
                    lbl.TranslateTo(e.TotalX, e.TotalY);
                    break;
                case GestureStatus.Completed: break;

            }
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            if (sender is Label == false)
            {
                return;
            }

            Label lbl = sender as Label;
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    lbl.Text = "滑动我 - 正在向左滑动";
                    break;
                case SwipeDirection.Up:
                    lbl.Text = "滑动我 - 正在向上滑动";
                    break;
                case SwipeDirection.Right:
                    lbl.Text = "滑动我 - 正在向右滑动";
                    break;
                case SwipeDirection.Down:
                    lbl.Text = "滑动我 - 正在向下滑动";
                    break;
            }
        }
    }
}