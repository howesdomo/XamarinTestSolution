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
    public partial class UcShuangSeQiu : ContentView
    {
        public UcShuangSeQiu()
        {
            InitializeComponent();
            Btn = new Button();
            gMain.Children.Add(Btn);
        }

        public Button Btn { get; set; }

        public bool IsSelected { get; set; }

        

        public void SetIsSelected(bool arg, bool isRedBall = true)
        {
            if (arg == true)
            {
                if (isRedBall)
                {
                    Btn.BackgroundColor = Color.Red;
                }
                else
                {
                    Btn.BackgroundColor = Color.SkyBlue;
                }
            }
            else
            {
                Btn.BackgroundColor = Color.Default; // 知识点 用 Color.Default 还原最初颜色
            }
        }
    }
}