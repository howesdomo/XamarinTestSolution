using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    /// <summary>
    /// V 1.0.2 2019-9-18 16:54:57
    /// 整理参数名称, 整理注释
    /// 
    /// V 1.0.1 2019-9-18 14:06:30
    /// 若服务器判断不需要更新程序, 弹出Toast提示 
    /// 
    /// V 1.0.0 2019-9-16 14:27:48
    /// 整合以下2个方法
    /// 检测程序最新版本 & 用浏览器打开下载地址
    /// 检测程序最新版本 & 下载并安装最新程序
    /// </summary>
    public class AppUpdateUtils
    {
        Xamarin.Forms.ContentPage mPage;

        public AppUpdateUtils(Xamarin.Forms.ContentPage page)
        {
            mPage = page;
        }

        public void CheckUpdate_DownloadFromBrowser()
        {
            checkUpdate_DoSomething(isDownloadFromApplication: false);
        }

        public void CheckUpdate_DownloadFromApplication()
        {
            checkUpdate_DoSomething(isDownloadFromApplication: true);
        }

        void checkUpdate_DoSomething(bool isDownloadFromApplication)
        {
            string version = Xamarin.Essentials.VersionTracking.CurrentVersion;
            string build = Xamarin.Essentials.VersionTracking.CurrentBuild;

            string platform = Xamarin.Essentials.DeviceInfo.Platform.ToString();

            UpdateInfo argUpdateInfo = new UpdateInfo()
            {

                Platform = platform,
                Version = int.Parse(build)
            };

            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("访问服务器中，请稍候...");
            new WebService().GetLastestVersion
            (
                updateInfoJsonStr: Util.JsonUtils.SerializeObject(argUpdateInfo),
                page: mPage,
                handle: async (soapResult) =>
                {
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    if (soapResult.IsComplete == false)
                    {
                        await mPage.DisplayAlert("Error", soapResult.ExceptionInfo, "确定");
                        return;
                    }
                    else if (soapResult.IsSuccess == false)
                    {
                        await mPage.DisplayAlert("Error", soapResult.BusinessExceptionInfo, "确定");
                        return;
                    }
                    else
                    {
                        UpdateInfo updateInfo = Util.JsonUtils.DeserializeObject<UpdateInfo>(soapResult.ReturnObjectJson);
                        if (updateInfo.IsLastestVersion == true)
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Toast(updateInfo.Message);
                            return;
                        }

                        // 询问用户是否更新
                        bool r1 = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(new Acr.UserDialogs.ConfirmConfig()
                        {
                            Title = "确认更新",
                            Message = updateInfo.Message,
                            OkText = "更新",
                            CancelText = "拒绝"
                        });

                        if (r1 == false) { return; } // 用户拒绝更新

                        if (isDownloadFromApplication == true)
                        {
                            download(updateInfo);
                        }
                        else
                        {
                            browserOpenUrl(updateInfo);
                        }
                    }
                }
            );
        }

        void download(UpdateInfo updateInfo)
        {
            System.ComponentModel.BackgroundWorker bgWorker = null;

            Acr.UserDialogs.IProgressDialog progressDialog = null;

            bgWorker = new System.ComponentModel.BackgroundWorker();
            bgWorker.DoWork += (bgSender, bgArgs) =>
            {
                var objArgs = bgArgs.Argument as object[];
                UpdateInfo _updateInfo_ = objArgs[0] as UpdateInfo;
                new Util.DownloadUtils().DownloadFileByHttpRequest
                (
                    requestUri: _updateInfo_.Url,
                    fileLength: _updateInfo_.FileLength,
                    saveFileFolder: System.IO.Path.Combine(Common.StaticInfo.AndroidExternalCachePath, "UpdateAPKs"),
                    renameDownloadFileName: string.Empty, // string.Empty 取默认文件名
                    backgroundWorker: bgWorker,
                    eventArgs: bgArgs // 传入 bgArgs 参数, 在 DownloadFileByHttpRequest 返回结果 -- 下载文件的最终路径
                );

                System.Threading.Thread.Sleep(500); // 为显示进度 100 %
            };

            bgWorker.RunWorkerCompleted += (bgSender, bgResult) =>
            {
                progressDialog.Hide();
                if (bgResult.Error != null)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                    {
                        Title = "捕获异常",
                        Message = bgResult.Error.GetFullInfo()
                    });
                }
                else
                {
                    try
                    {
                        string path = bgResult.Result as string;
                        System.Diagnostics.Debug.WriteLine($"打开APK安装器。APK路径[{path}]");
                        App.AndroidIntentUtils.InstallAPK(path);
                    }
                    catch (Exception ex)
                    {
                        string msg = $"{ex.GetFullInfo()}";
                        System.Diagnostics.Debug.WriteLine(msg);
                        Acr.UserDialogs.UserDialogs.Instance.Alert(new Acr.UserDialogs.AlertConfig()
                        {
                            Title = "捕获异常",
                            Message = msg
                        });
                    }
                }
            };

            bgWorker.WorkerReportsProgress = true;
            bgWorker.ProgressChanged += (bgSender, bgArgs) =>
            {
                if (progressDialog.IsShowing == false)
                {
                    progressDialog.Show();
                }
                progressDialog.Title = $"正在下载...";
                progressDialog.PercentComplete = bgArgs.ProgressPercentage;
            };

            progressDialog = Acr.UserDialogs.UserDialogs.Instance.Progress();
            bgWorker.RunWorkerAsync(new object[] { updateInfo });
        }

        async void browserOpenUrl(UpdateInfo updateInfo)
        {
            try
            {
                await Xamarin.Essentials.Browser.OpenAsync(updateInfo.Url);
            }
            catch (Exception ex)
            {
                string msg = $"{ex.GetFullInfo()}";
                System.Diagnostics.Debug.WriteLine(msg);
                await mPage.DisplayAlert("捕获异常", msg, "确定");
            }
        }
    }
}
