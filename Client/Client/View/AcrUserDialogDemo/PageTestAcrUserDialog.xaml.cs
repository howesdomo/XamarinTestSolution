using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.AcrUserDialogDemo
{
    /// <summary>
    /// V 1.0.1 整理代码顺序
    /// V 1.0.0 从 All For One 项目拷贝过来
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTestAcrUserDialog : ContentPage
    {
        public PageTestAcrUserDialog()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            btnAlter.Clicked += btnAlter_Clicked;
            btnAlterWithOnAction.Clicked += btnAlterWithOnAction_Clicked;

            btnConfirm.Clicked += btnConfirm_Clicked;
            btnToast.Clicked += btnToast_Clicked;
            btnLoadingDialog.Clicked += btnLoadingDialog_Clicked;
            btnProgressDialog.Clicked += btnProgressDialog_Clicked;
            btnDate.Clicked += btnDate_Clicked;
            btnTime.Clicked += btnTime_Clicked;
            btnPrompt.Clicked += btnPrompt_Clicked;
            btnActionSheet.Clicked += btnActionSheet_Clicked;
            btnLogin.Clicked += btnLogin_Clicked;
        }

        async void btnAlter_Clicked(object sender, EventArgs e)
        {
            //// ** 设置了 OnAction 使用 AlertAsync 的方式进行调用 ** 调用方式参考 btnAlterWithOnAction_Clicked 方法
            //await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(new Acr.UserDialogs.AlertConfig()
            //{
            //    Title = "Alter Title",
            //    Message = "Alter Message",
            //    OkText = "收到",
            //    OnAction = (() =>
            //    {
            //        string msg = "{0}".FormatWith("Alter On Action");
            //        System.Diagnostics.Debug.WriteLine(msg);
            //    })
            //});

            await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(new Acr.UserDialogs.AlertConfig()
            {
                Title = "Alter Title",
                Message = "Alter Message",
                OkText = "收到"
            });
        }

        void btnAlterWithOnAction_Clicked(object sender, EventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
            {
                Title = "Alter Title",
                Message = "Alter Message",
                OkText = "收到",
                OnAction = (() =>
                {
                    string msg = "{0}".FormatWith("Alter On Action");
                    System.Diagnostics.Debug.WriteLine(msg);
                })
            });
        }

        async void btnConfirm_Clicked(object sender, EventArgs e)
        {
            bool r1 = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(new Acr.UserDialogs.ConfirmConfig()
            {
                Title = "Confirm Title",
                Message = "Confirm Message",
                OkText = "收到",
                CancelText = "拒绝",
                // 抛错 OnAction should not be set as async will not use it
                // 故 Async 不要写OnAction
                //OnAction= ((result) => 
                //{
                //    string msg = "Confirm Result : {0}".FormatWith(result);
                //    System.Diagnostics.Debug.WriteLine(msg);
                //})
            });

            string msg = "Confirm Result : {0}".FormatWith(r1);
            System.Diagnostics.Debug.WriteLine(msg);


            Acr.UserDialogs.UserDialogs.Instance.Confirm(new Acr.UserDialogs.ConfirmConfig()
            {
                Title = "Confirm Title2",
                Message = "Confirm Message2",
                OkText = "收到2",
                CancelText = "拒绝2",
                OnAction = ((result) =>
                {
                    string msg2 = "Confirm Result 2: {0}".FormatWith(result);
                    System.Diagnostics.Debug.WriteLine(msg);
                })
            });
        }

        void btnToast_Clicked(object sender, EventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.Toast("toast", dismissTimer: TimeSpan.FromSeconds(5));
        }

        void btnLoadingDialog_Clicked(object sender, EventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("正在打印, 10秒后完成");

            Task task = new Task(() =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            });

            task.ContinueWith((r) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                });
            });

            task.Start();
        }

        #region ProgressDialog 测试

        System.ComponentModel.BackgroundWorker bgWorker { get; set; }

        Acr.UserDialogs.IProgressDialog dialog { get; set; }

        void btnProgressDialog_Clicked(object sender, EventArgs e)
        {
            if (bgWorker != null && bgWorker.IsBusy == true)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("BackgroundWorker is busy.", dismissTimer: TimeSpan.FromSeconds(5));
                return;
            }

            bgWorker = new System.ComponentModel.BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;


            dialog = Acr.UserDialogs.UserDialogs.Instance.Progress();
            bgWorker.RunWorkerAsync();
        }

        void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int i = 0;
            while (i < 100)
            {
                i += 10;
                bgWorker.ReportProgress(i);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        void bgWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (dialog.IsShowing == false)
            {
                dialog.Show();
            }
            dialog.Title = $"当前进度";
            dialog.PercentComplete = e.ProgressPercentage;
        }

        void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            dialog.Hide();
        }

        #endregion

        async void btnDate_Clicked(object sender, EventArgs e)
        {
            var dateResult = await Acr.UserDialogs.UserDialogs.Instance.DatePromptAsync(new Acr.UserDialogs.DatePromptConfig()
            {
                Title = "Date Prompt Title",
                OkText = "确认",
                CancelText = "取消",
                MinimumDate = DateTime.Today.AddDays(-5),
                MaximumDate = DateTime.Today.AddDays(5),
            });

            if (dateResult.Ok == true)
            {
                string msg = "选择日期:{0}".FormatWith(dateResult.SelectedDate);
                System.Diagnostics.Debug.WriteLine(msg);
            }
            else
            {
                string msg = "{0}".FormatWith("用户取消了选择");
                System.Diagnostics.Debug.WriteLine(msg);
            }


            Acr.UserDialogs.UserDialogs.Instance.DatePrompt(new Acr.UserDialogs.DatePromptConfig()
            {
                Title = "Date Prompt Title",
                OkText = "确认",
                CancelText = "取消",
                MinimumDate = DateTime.Today.AddDays(-5),
                MaximumDate = DateTime.Today.AddDays(5),
                OnAction = ((r) =>
                {
                    if (r.Ok == true)
                    {
                        string msg = "选择日期2:{0}".FormatWith(r.SelectedDate);
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                    else
                    {
                        string msg = "{0}".FormatWith("用户取消了选择2");
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                })
            });

        }

        async void btnTime_Clicked(object sender, EventArgs e)
        {
            var dateResult = await Acr.UserDialogs.UserDialogs.Instance.TimePromptAsync(new Acr.UserDialogs.TimePromptConfig()
            {
                Title = "Date Prompt Title",
                OkText = "确认",
                CancelText = "取消",
                MinimumMinutesTimeOfDay = (int)TimeSpan.FromMinutes(60 * 8 + 45).TotalMinutes, // 08:45
                MaximumMinutesTimeOfDay = (int)TimeSpan.FromMinutes(60 * 18).TotalMinutes, // 18:00
            });

            if (dateResult.Ok == true)
            {
                string msg = "选择时间:{0}".FormatWith(dateResult.SelectedTime.ToStringAdv());
                System.Diagnostics.Debug.WriteLine(msg);
            }
            else
            {
                string msg = "{0}".FormatWith("用户取消了选择");
                System.Diagnostics.Debug.WriteLine(msg);
            }

            Acr.UserDialogs.UserDialogs.Instance.TimePrompt(new Acr.UserDialogs.TimePromptConfig()
            {
                Title = "Date Prompt Title",
                OkText = "确认",
                CancelText = "取消",
                MinimumMinutesTimeOfDay = (int)TimeSpan.FromMinutes(60 * 8 + 45).TotalMinutes, // 08:45
                MaximumMinutesTimeOfDay = (int)TimeSpan.FromMinutes(60 * 18).TotalMinutes, // 18:00
                OnAction = ((r) =>
                {
                    if (r.Ok == true)
                    {
                        string msg = "选择时间:{0}".FormatWith(r.SelectedTime.ToStringAdv());
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                    else
                    {
                        string msg = "{0}".FormatWith("用户取消了选择");
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                })
            });
        }

        void btnActionSheet_Clicked(object sender, EventArgs e)
        {
            // TODO Acr.UserDialogs.UserDialogs --- ActionSheet
            Acr.UserDialogs.UserDialogs.Instance.ActionSheet(new Acr.UserDialogs.ActionSheetConfig()
            {

            });
        }

        void btnPrompt_Clicked(object sender, EventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.Prompt(new Acr.UserDialogs.PromptConfig()
            {
                Title = "提示 Title",
                Message = "请输入手机号 Message",
                Placeholder = "my Placeholder",
                InputType = Acr.UserDialogs.InputType.Phone,
                OkText = "发送",
                CancelText = "取消",
                OnAction = ((r) =>
                {
                    if (r.Ok)
                    {
                        string msg = "录入手机号:{0}".FormatWith(r.Text);
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                })
            });
        }

        void btnLogin_Clicked(object sender, EventArgs e)
        {
            // 软键盘无法使用退格键
            Acr.UserDialogs.UserDialogs.Instance.Login(new Acr.UserDialogs.LoginConfig()
            {
                Title = "User Login",
                LoginPlaceholder = "请输入账号",
                PasswordPlaceholder = "请输入密码",
                Message = "All for one 登录",
                OkText = "登录",
                CancelText = "返回",
                LoginValue = "login Value",
                OnAction = ((r) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert
                        (
                            "提示",
                            Util.JsonUtils.SerializeObjectWithFormatted(r),
                            "确认"
                        );
                    });
                })
            });
        }
    }
}