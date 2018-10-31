using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.Games.CRW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UcQuestion : ContentView
    {
        public UcQuestion()
        {
            InitializeComponent();
            initUI();

            this.BindingContextChanged += UcQuestion_BindingContextChanged;
        }

        // 修复用 Xamarin.Forms 3.3.0.912540 后, 
        // 绑定 Image 的 IsVisible 与较早前版本在 UI 的体现有所不同
        // 当 Image 的 Source 绑定过内容后, 再绑定 null 对象, 仍会显示图像
        // 有可能 null 时, 对于 IsVisible 的默认值为 True
        // 故用以下方式修复此现象
        private void UcQuestion_BindingContextChanged(object sender, EventArgs e)
        {
            if (this.BindingContext is null)
            {
                gRoot.IsVisible = false;
            }
            else
            {
                gRoot.IsVisible = true;
            }
        }

        private void initUI()
        {
            this.lbl.Margin = new Thickness(left: 0d, top: 0d, right: 0d, bottom: 0d);
        }
    }
}