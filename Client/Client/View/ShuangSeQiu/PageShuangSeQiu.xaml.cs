using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.ShuangSeQiu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageShuangSeQiu : ContentPage
    {
        public PageShuangSeQiu()
        {
            InitializeComponent();
            initUI();
        }

        private void initUI()
        {
            for (int i = 0; i < 33; i++)
            {
                UcShuangSeQiu toAdd_Child = new UcShuangSeQiu();
                gRedBalls.Children.Add(toAdd_Child);
            }

            for (int i = 0; i < 15; i++)
            {
                UcShuangSeQiu toAdd_Child = new UcShuangSeQiu();
                gBlueBalls.Children.Add(toAdd_Child);
            }
        }
    }
}