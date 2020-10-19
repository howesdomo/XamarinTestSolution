using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Common
{
    /// <summary>
    /// 待完成 多点选择
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LBSMultiPicker : ContentPage
    {
        #region (示例) 调用界面获取用户提交信息

        //MessagingCenter.Subscribe<Application, LBSModel>(App.Current, "GPSInfo", (sender, args) =>
        //{
        //    // args 为 数据
        //    args ....
        //});

        #endregion

        public async static Task<LBSMultiPicker> GetInstance()
        {
            var permission = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
            if (permission != Xamarin.Essentials.PermissionStatus.Granted)
            {
                permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
            }

            if (permission != Xamarin.Essentials.PermissionStatus.Granted)
            {
                // 未有程序的定位权限无法打开界面
                Acr.UserDialogs.UserDialogs.Instance.Toast("获取GPS信息(在使用App期间)权限授权失败");
                return null;
            }

            if (Instance == null)
            {
                lock (_LOCK_)
                {
                    if (Instance == null)
                    { 
                        Instance = new LBSMultiPicker();
                    }
                }
            }
            
            return Instance;
        }

        private static LBSMultiPicker Instance;
        private static readonly object _LOCK_ = new object();

        private LBSMultiPicker()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var viewModel = this.BindingContext as LBSPicker_ViewModel;
            if (viewModel != null)
            {
                viewModel.StartGPSInfo();
                viewModel.setWebView(this.webView);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Acr.UserDialogs.UserDialogs.Instance.Confirm(new Acr.UserDialogs.ConfirmConfig()
            {
                Title = "提示",
                Message = "确认退出定位？",
                OkText = "退出",
                CancelText = "取消",
                OnAction = (args) =>
                {
                    if (args == true)
                    {
                        App.Navigation.PopAsync();
                    }
                }
            });

            return true;
        }

        protected override void OnDisappearing()
        {
            closeAndSend();
            base.OnDisappearing();
        }

        void closeAndSend()
        {
            var viewModel = this.BindingContext as LBSPicker_ViewModel;

            viewModel.StopGPSInfo(); // 停止定位

            if (viewModel != null && viewModel.ConfirmLBSModel != null) // 用户提交定位信息
            {
                MessagingCenter.Send<Application, LBSModel>(App.Current, "GPSInfo", viewModel.ConfirmLBSModel);
            }
        }
    }

    public class LBSMultiPicker_ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private readonly object _Lock_ = new object();

        #region WebViewAdv 控件

        public Util.XamariN.Components.WebViewAdv mWebView { get; private set; }

        public void setWebView(Util.XamariN.Components.WebViewAdv w)
        {
            if (mWebView != null)
            {
                return;
            }

            lock (_Lock_)
            {
                if (mWebView != null)
                {
                    return;
                }

                mWebView = w;
            }
        }

        #endregion

        #region 属性

        private string _ErrorMsg;
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set
            {
                _ErrorMsg = value;
                this.OnPropertyChanged("ErrorMsg");
            }
        }

        private LBSModel _CurrentLBSModel;
        /// <summary>
        /// 成功定位的 LBS 数据
        /// </summary>
        public LBSModel CurrentLBSModel
        {
            get { return _CurrentLBSModel; }
            set
            {
                _CurrentLBSModel = value;
                this.OnPropertyChanged("CurrentLBSModel");
                this.OnPropertyChanged("IsLoading");
            }
        }

        public bool IsLoading
        {
            get
            {
                return this.CurrentLBSModel == null;
            }
        }

        private string _Url;
        public string Url
        {
            get { return _Url; }
            set
            {
                _Url = value;
                this.OnPropertyChanged("Url");
            }
        }

        private LBSModel _ConfirmLBSModel;
        /// <summary>
        /// 用户提交的定位信息
        /// </summary>
        public LBSModel ConfirmLBSModel
        {
            get { return _ConfirmLBSModel; }
            set
            {
                _ConfirmLBSModel = value;
                this.OnPropertyChanged("ConfirmLBSModel");
            }
        }

        private bool _BtnAppGPSPermission_IsVisible;
        public bool BtnAppGPSPermission_IsVisible
        {
            get { return _BtnAppGPSPermission_IsVisible; }
            set
            {
                _BtnAppGPSPermission_IsVisible = value;
                this.OnPropertyChanged("BtnAppGPSPermission_IsVisible");
            }
        }

        #endregion

        public LBSMultiPicker_ViewModel()
        {
            initCommand();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var g = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
                BtnAppGPSPermission_IsVisible = g != Xamarin.Essentials.PermissionStatus.Granted;
            });
        }

        void initCommand()
        {
            CMD_Open_GPSSetting_InOS = new Command(open_GPSSetting_InOS);
            CMD_Confirm = new Command(confirm);
        }

        public Command CMD_Open_GPSSetting_InOS { get; private set; }

        public Command CMD_Confirm { get; private set; }

        public void StartGPSInfo()
        {
            Common.LBS.GetGPSInfoEvent += new EventHandler<Common.LBSModel>(OnGetGPSInfoHandler);
            App.LBS.GetGPSInfo();
        }

        public void StopGPSInfo()
        {
            Common.LBS.GetGPSInfoEvent -= new EventHandler<Common.LBSModel>(OnGetGPSInfoHandler);
            App.LBS.Stop();
        }

        #region 处理 LBS.GetGPSInfoEvent


        private void OnGetGPSInfoHandler(object o, LBSModel args)
        {
            Device.BeginInvokeOnMainThread(() => getGPSInfo_ActualMethod(args));
        }

        void getGPSInfo_ActualMethod(LBSModel args)
        {
            if (args.IsComplete == false)
            {
                string msg = "{0}".FormatWith(args.ExceptionInfo);
                System.Diagnostics.Debug.WriteLine(msg);
                this.ErrorMsg = msg;
                this.CurrentLBSModel = null;
                return;
            }

            if (args.IsSuccess == false)
            {
                string msg = "{0}".FormatWith(args.BusinessExceptionInfo);
                System.Diagnostics.Debug.WriteLine(msg);
                this.ErrorMsg = msg;
                this.CurrentLBSModel = null;
                return;
            }

            this.ErrorMsg = string.Empty;


            //if (args.Address.IsNullOrWhiteSpace() == false && args.Address!="未知") // 对于地址有业务要求, 可以开启 if 注释
            {
                string paramStr = $"lng={args.Longitude}&lat={args.Latitude}&address={args.Address}";
                if (args.GPSInfoType.Contains("离线") == false)
                {
                    if (Url.IsNullOrWhiteSpace())
                    {
                        string UrlBase = @"http://116.255.187.86:18888/TestByHowe/index.html"; // TODO urlbase 改为全局参数
                        Url = $"{UrlBase}?{paramStr}";
                    }
                    else
                    {
                        string jsScript = $"update('{paramStr}')";
                        mWebView.EvaluateJavaScriptAsync(jsScript);
                    }
                }

                this.CurrentLBSModel = args;
            }
        }

        #endregion

        #region 用户提交定位信息

        void confirm()
        {
            App.DebounceAction.Debounce
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    Device.BeginInvokeOnMainThread(confirm_ActualMethod);
                },
                syncInvoke: null
            );
        }

        async void confirm_ActualMethod()
        {
            if (this.CurrentLBSModel == null)
            {
                App.AudioPlayer.PlayError();
                Acr.UserDialogs.UserDialogs.Instance.Toast("未能成功定位，无法进行提交。");
                return;
            }

            if (this.CurrentLBSModel.ReceiveTime < DateTime.Now.AddMinutes(-30d))
            {
                App.AudioPlayer.PlayError();
                Acr.UserDialogs.UserDialogs.Instance.Toast("最近一次成功定位距离现在已经超过30分钟，请重新进行定位。");
                return;
            }

            Common.LBS.GetGPSInfoEvent -= new EventHandler<Common.LBSModel>(OnGetGPSInfoHandler);

            this.ConfirmLBSModel = this.CurrentLBSModel.DeepClone();

            await App.Navigation.PopAsync();
        }

        #endregion

        #region 前往系统配置定位权限

        void open_GPSSetting_InOS()
        {
            App.DebounceAction.Debounce
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.LBS.Open_GPSSetting_InOS();
                    });
                },
                syncInvoke: null
            );
        }

        #endregion
    }
}