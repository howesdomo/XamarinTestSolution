using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class WebServiceBase
    {
        public System.Net.WebClient mClient;

        public WebServiceBase()
        {
            this.mClient = new System.Net.WebClient();
            this.mClient.Encoding = System.Text.Encoding.UTF8;
            this.mClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
        }

        // 永远是最新的方法, 要优化Copy一份加 V{版本号}
        public void Execute 
        (
            Uri mUri,
            Util.WebService.RequestData requestData,
            Xamarin.Forms.Page page = null,
            Action<Util.WebService.SOAPResult> handle = null
        )
        {
            var current = Xamarin.Essentials.Connectivity.NetworkAccess;
            if (current == Xamarin.Essentials.NetworkAccess.None)
            {
                string msg = "检测设备没有可用的网络，请开启 数据 或 Wifi。".FormatWith(requestData.MethodName);
                if (page == null)
                {
                    System.Diagnostics.Debug.WriteLine(msg);
                }
                else
                {
                    page.DisplayAlert("Error", msg, "确定");
                }
                return;
            }


            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();

            bw.DoWork += (s, e) =>
            {

                string data = string.Empty;
                try
                {
                    // TODO 测试超时, 超长内容
                    data = mClient.UploadString(mUri, "POST", Util.JsonUtils.SerializeObject(requestData));
                }
                catch (System.Net.WebException webEx)
                {
                    string msg = string.Format("{0}", webEx.Message);
                    System.Diagnostics.Debug.WriteLine(msg);
                }
                finally
                {
                    mClient.Dispose();
                }

                Util.WebService.SOAPResult soapResult = Util.JsonUtils.DeserializeObject<Util.WebService.SOAPResult>(data);
                e.Result = soapResult;
            };


            bw.RunWorkerCompleted += (obj, args) =>
            {
                if (args.Error != null)
                {
                    string msg = "{0}".FormatWith(args.Error.GetFullInfo());
                    if (page == null)
                    {
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                    else
                    {
                        page.DisplayAlert("Error", msg, "确定");
                    }
                    return;
                }

                if (args.Result == null)
                {
                    string msg = "执行 {0} 发生未知错误：args.Result未空值".FormatWith(requestData.MethodName);
                    if (page == null)
                    {
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                    else
                    {
                        page.DisplayAlert("Error", msg, "确定");
                    }
                    return;
                }

                if (handle == null)
                {
                    return;
                }

                Util.WebService.SOAPResult soapResult = args.Result as Util.WebService.SOAPResult;
                handle.Invoke(soapResult);
                 
            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// 缺点 : 应该把 BackgroundWorker 用于mClient.UploadString(), 而不是在 WebService 具体方法中创建 BackgroundWorker
        /// </summary>
        /// <param name="mUri"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public Util.WebService.SOAPResult ExecuteV1(Uri mUri, Util.WebService.RequestData requestData)
        {
            string data = string.Empty;
            try
            {
                // TODO 测试超时, 超长内容
                data = mClient.UploadString(mUri, "POST", Util.JsonUtils.SerializeObject(requestData));
            }
            catch (System.Net.WebException webEx)
            {
                string msg = string.Format("{0}", webEx.Message);
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                mClient.Dispose();
            }

            Util.WebService.SOAPResult soapResult = Util.JsonUtils.DeserializeObject<Util.WebService.SOAPResult>(data);
            return soapResult;
        }

        /// <summary>
        /// 缺点 : 逻辑处理不方便, 报错时页面不知道接下去要做什么
        /// </summary>
        /// <param name="mUri"></param>
        /// <param name="requestData"></param>
        /// <param name="page"></param>
        /// <param name="handle"></param>
        public void ExecuteV2
        (
            Uri mUri,
            Util.WebService.RequestData requestData,
            Xamarin.Forms.Page page = null,
            Action<string> handle = null
        )
        {

            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();

            bw.DoWork += (s, e) =>
            {

                string data = string.Empty;
                try
                {
                    // TODO 测试超时, 超长内容
                    data = mClient.UploadString(mUri, "POST", Util.JsonUtils.SerializeObject(requestData));
                }
                catch (System.Net.WebException webEx)
                {
                    string msg = string.Format("{0}", webEx.Message);
                    System.Diagnostics.Debug.WriteLine(msg);
                }
                finally
                {
                    mClient.Dispose();
                }

                Util.WebService.SOAPResult soapResult = Util.JsonUtils.DeserializeObject<Util.WebService.SOAPResult>(data);
                e.Result = soapResult;
            };


            bw.RunWorkerCompleted += (obj, args) =>
            {
                if (args.Error != null)
                {
                    string msg = "{0}".FormatWith(args.Error.GetFullInfo());
                    if (page == null)
                    {
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                    else
                    {
                        page.DisplayAlert("Error", msg, "确定");
                    }
                    return;
                }

                if (args.Result == null)
                {
                    string msg = "执行 {0} 发生未知错误：args.Result未空值".FormatWith(requestData.MethodName);
                    if (page == null)
                    {
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                    else
                    {
                        page.DisplayAlert("Error", msg, "确定");
                    }
                    return;
                }


                Util.WebService.SOAPResult soapResult = args.Result as Util.WebService.SOAPResult;
                if (soapResult.IsComplete == false)
                {
                    if (page == null)
                    {
                        System.Diagnostics.Debug.WriteLine(soapResult.ExceptionInfo);
                    }
                    else
                    {
                        page.DisplayAlert("Error", soapResult.ExceptionInfo, "确定");
                    }
                    return;
                }
                else if (soapResult.IsSuccess == false)
                {
                    if (page == null)
                    {
                        System.Diagnostics.Debug.WriteLine(soapResult.BusinessExceptionInfo);
                    }
                    else
                    {
                        page.DisplayAlert("Error", soapResult.BusinessExceptionInfo, "确定");
                    }
                    return;
                }
                else
                {
                    if (handle != null)
                    {
                        handle.Invoke(soapResult.ReturnObjectJson);
                    }
                }
            };

            bw.RunWorkerAsync();
        }
    }
}
