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
        /// <summary>
        /// 经测试 无需实现 ContentPageAdv ,
        /// 只需加载 V7版本 的 Widget.Toolbar, 即可实现软硬Back的监控
        /// 故不使用 PageC的方式进行 Back 的监控
        /// </summary>
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



    }
}