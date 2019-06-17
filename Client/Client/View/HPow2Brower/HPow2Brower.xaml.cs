using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HPow2Brower : ContentPage
    {
        HPow2Brower_ViewModel ViewModel { get; set; }

        public HPow2Brower()
        {
            InitializeComponent();
            initUI();

            initEvent();

            this.ViewModel = new HPow2Brower_ViewModel();
            this.BindingContext = this.ViewModel;

            // 安卓需要输入包含 http://
            // 苹果则不需要
            // TODO 待完美解决两个平台不同的需要
            // txtAddressTemplate.Text = ""; // 默认网址
        }

        private void initUI()
        {
            // gNavigation.Margin = new Thickness(left: 5, top: 5, right: 5, bottom: 0);

            btnBack.Text = "<-";
            btnForword.Text = "->";

        }

        private void initEvent()
        {
            Appearing += PageAppearing;

            btnCopyWebViewAddress.Clicked += btnCopyWebViewAddress_Clicked;
            btnCopyPasteWebviewAddress.Clicked += btnCopyPasteWebviewAddress_Clicked;

            btnGo.Clicked += btnGo_Clicked;
            btnForword.Clicked += btnForword_Clicked;
            btnBack.Clicked += btnBack_Clicked;

            btnLeftOrRight.Clicked += btnLeftOrRight_Clicked;
            btnFullScreen.Clicked += btnFullScreen_Clicked;
            btnSetting.Clicked += btnSetting_Clicked;

            btnAddArg.Clicked += btnAddArg_Clicked;
            btnRemoveArg.Clicked += btnRemoveArg_Clicked;


            this.webView.Navigating += webView_Navigating;
            this.webView.Navigated += webView_Navigated;


        }



        private void btnCopyWebViewAddress_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Xamarin.Essentials.Clipboard.SetTextAsync(this.txtAddress.Text);
                await DisplayAlert(title: "提示", message: "已复制[当前地址]到系统剪贴板。", cancel: "确定");
            });
        }

        async void btnCopyPasteWebviewAddress_Clicked(object sender, EventArgs e)
        {
            bool r = await DisplayAlert(title: "提示", message: "拷贝[当前地址]到[地址模版]?", accept: "确定", cancel: "取消");
            if (r == true)
            {
                this.txtAddressTemplate.Text = this.txtAddress.Text;
            }            
        }


        private void btnFullScreen_Clicked(object sender, EventArgs e)
        {
            if (btnFullScreen.Text == "全")
            {
                gNavigation.IsVisible = false;
                gArgs.IsVisible = false;

                btnFullScreen.Text = "普";
            }
            else
            {
                gNavigation.IsVisible = true;

                btnFullScreen.Text = "全";
            }
        }

        private void btnLeftOrRight_Clicked(object sender, EventArgs e)
        {
            if (btnLeftOrRight.Text == "左")
            {
                gToolbar1.HorizontalOptions = LayoutOptions.Start;
                gToolbar2.HorizontalOptions = LayoutOptions.End;
                btnLeftOrRight.Text = "右";
            }
            else
            {
                gToolbar1.HorizontalOptions = LayoutOptions.End;
                gToolbar2.HorizontalOptions = LayoutOptions.Start;
                btnLeftOrRight.Text = "左";
            }
        }

        private void btnSetting_Clicked(object sender, EventArgs e)
        {
            gArgs.IsVisible = !gArgs.IsVisible;
        }

        private void PageAppearing(object sender, EventArgs e)
        {

        }

        async void btnGo_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnGo_ActualMethod();
            }
            catch (Exception ex)
            {
                await DisplayAlert
                (
                    title: "捕获异常",
                    message: ex.GetFullInfo(),
                    cancel: "确定"
                );
            }
        }

        private void btnGo_ActualMethod()
        {
            string calcUrl = string.Empty;
            string addressTemplate = this.txtAddressTemplate.Text;

            for (int i = 0; i < this.ViewModel.ArgsList.Count; i++)
            {
                string tempR = "{" + i + "}";
                addressTemplate = addressTemplate.Replace(tempR, this.ViewModel.ArgsList[i].Value.ToString());
            }

            this.txtAddress.Text = addressTemplate;
            this.webView.Source = this.txtAddress.Text;

            if (this.ViewModel.ArgsList.Count > 0)
            {
                // 最后一个参数在执行GO后 自动 +1 
                this.ViewModel.ArgsList[this.ViewModel.ArgsList.Count - 1].Value += 1;
            }
        }



        private void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            string msg = "{0}".FormatWith("Navigating");
            System.Diagnostics.Debug.WriteLine(msg);
        }

        async void webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            string msg = "{0}".FormatWith("Navigated");
            System.Diagnostics.Debug.WriteLine(msg);

            // WebViewSource webViewSource = this.webView.Source; // 无法使用 Source 来获取当前地址
            // 使用 JS 获取当前最新的URL
            this.txtAddress.Text = await this.webView.EvaluateJavaScriptAsync("document.URL");
        }

        #region 前进 后退

        async void btnForword_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnForword_ActualMethod();
            }
            catch (Exception ex)
            {
                await DisplayAlert
                (
                    title: "捕获异常",
                    message: ex.GetFullInfo(),
                    cancel: "确定"
                );
            }
        }

        void btnForword_ActualMethod()
        {
            if (this.webView.CanGoForward)
            {
                this.webView.GoForward();
            }
        }

        async void btnBack_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnBack_ActualMethod();
            }
            catch (Exception ex)
            {
                await DisplayAlert
                (
                    title: "捕获异常",
                    message: ex.GetFullInfo(),
                    cancel: "确定"
                );
            }
        }

        void btnBack_ActualMethod()
        {
            if (this.webView.CanGoBack)
            {
                this.webView.GoBack();
            }
        }

        #endregion


        async void btnAddArg_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnAddArg_ActualMethod();
            }
            catch (Exception ex)
            {
                await DisplayAlert
                (
                    title: "捕获异常",
                    message: ex.GetFullInfo(),
                    cancel: "确定"
                );
            }
        }

        private void btnAddArg_ActualMethod()
        {
            var toAdd = new MMMMM()
            {
                Index = this.ViewModel.ArgsList.Count,
                Value = 0
            };

            this.ViewModel.ArgsList.Add(toAdd);
        }

        async void btnRemoveArg_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnRemoveArg_ActualMethod();
            }
            catch (Exception ex)
            {
                await DisplayAlert
                (
                    title: "捕获异常",
                    message: ex.GetFullInfo(),
                    cancel: "确定"
                );
            }
        }

        private void btnRemoveArg_ActualMethod()
        {
            if (this.ViewModel.ArgsList.Count <= 0)
            {
                return;
            }

            var toRemove = this.ViewModel.ArgsList.Last();
            this.ViewModel.ArgsList.Remove(toRemove);
        }
    }

    public class HPow2Brower_ViewModel : ViewModel.BaseViewModel
    {
        public HPow2Brower_ViewModel()
        {
            this.ArgsList = new Util.UIComponent.BaseCollection<MMMMM>();
            this.ArgsList.Add(new MMMMM() { Index = 0, Value = 0 });
        }

        private Util.UIComponent.BaseCollection<MMMMM> _ArgsList;

        public Util.UIComponent.BaseCollection<MMMMM> ArgsList
        {
            get { return _ArgsList; }
            set
            {
                _ArgsList = value;
                this.OnPropertyChanged("ArgsList");

                if (_ArgsList != null)
                {
                    _ArgsList.CollectionChanged += _ArgsList_CollectionChanged;
                    _ArgsList.ItemsPropertyChanged += _ArgsList_ItemsPropertyChanged;
                }
            }
        }

        private void _ArgsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged("ArgsList");
        }

        private void _ArgsList_ItemsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged("ArgsList");
        }
    }

    public class MMMMM : Util.UIComponent.VirtualModel
    {
        private int _Index;

        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                this.OnPropertyChanged("Index");
            }
        }

        private int _Value;

        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                this.OnPropertyChanged("Value");
            }
        }

    }
}