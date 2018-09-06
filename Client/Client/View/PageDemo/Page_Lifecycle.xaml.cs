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
    public partial class Page_Lifecycle : ContentPage
    {
        public Page_Lifecycle()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.SizeChanged += Page_Lifecycle_SizeChanged; // 1
            this.Appearing += Page_Lifecycle_Appearing; // 2
            this.Disappearing += Page_Lifecycle_Disappearing; // 3

            this.Focused += Page_Lifecycle_Focused;
            this.Unfocused += Page_Lifecycle_Unfocused;


            this.btnPageB.Clicked += BtnPageB_Clicked;
            this.btnPageC.Clicked += BtnPageC_Clicked;
        }


        View.PageDemo.PageB mPageB { get; set; }

        async void BtnPageB_Clicked(object sender, EventArgs e)
        {
            if (mPageB == null)
            {
                mPageB = new View.PageDemo.PageB();
            }

            await Navigation.PushAsync(mPageB);

            // string msg = "{0}".FormatWith("b页面退出了?");
            // System.Diagnostics.Debug.WriteLine(msg);

            // 执行完 PushAsync 后, 马上输出 "b页面退出了?" 证明不能在 await Navigation.PushAsync(mPageB); 获取任何返回的结果
        }

        #region Lifecycle 测试

        private void Page_Lifecycle_Unfocused(object sender, FocusEventArgs e)
        {
            string msg = "Page_Lifecycle_Unfocused".FormatWith();
            System.Diagnostics.Debug.WriteLine(msg);
        }

        private void Page_Lifecycle_SizeChanged(object sender, EventArgs e)
        {
            string msg = "Page_Lifecycle_SizeChanged".FormatWith();
            System.Diagnostics.Debug.WriteLine(msg);
        }

        private void Page_Lifecycle_Appearing(object sender, EventArgs e)
        {
            string msg = "Page_Lifecycle_Appearing".FormatWith();
            System.Diagnostics.Debug.WriteLine(msg);

            if (mPageB != null)
            {
                string msg2 = "{0}".FormatWith(Util.JsonUtils.SerializeObject(mPageB.ViewModel));
                System.Diagnostics.Debug.WriteLine(msg2);

            }
        }

        private void Page_Lifecycle_Disappearing(object sender, EventArgs e)
        {
            string msg = "Page_Lifecycle_Disappearing".FormatWith();
            System.Diagnostics.Debug.WriteLine(msg);
        }

        private void Page_Lifecycle_Focused(object sender, FocusEventArgs e)
        {
            string msg = "Page_Lifecycle_Focused".FormatWith();
            System.Diagnostics.Debug.WriteLine(msg);
        }

        #endregion

        async void BtnPageC_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageC());
        }
    }
}