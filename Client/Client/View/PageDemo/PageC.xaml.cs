using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.PageDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageC : ContentPageAdv
    {
        public PageC()
        {
            InitializeComponent();

            if (EnableBackButtonOverride == true)
            {
                this.CustomBackButtonAction = async () =>
                {
                    var result = await this.DisplayAlert
                    (
                        title: "提示",
                        message:"确认退出？",
                        accept:"确认",
                        cancel:"取消"
                    );

                    if (result)
                    {
                        await Navigation.PopAsync(true);
                    }
                };
            }
        }


        // NavigationPage返回按钮
        // XAML 代码设置
        // NavigationPage.HasBackButton="False"

        // C# 代码设置
        // NavigationPage.SetHasBackButton(this, false);
    }
}