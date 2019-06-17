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

        // Execute 永远是最新的方法, 要优化Copy一份加 V{版本号}

        /// <summary>
        /// Copy From V4
        /// 
        /// 在V3的基础上
        /// 增加对 requestData.JsonArgs 的压缩与加密
        /// </summary>
        /// <param name="uri">Web Service Uri</param>
        /// <param name="requestData">参数</param>
        /// <param name="page">UI Page</param>
        /// <param name="handle">UI Handler</param>
        /// <param name="isCompress">是否压缩(默认不压缩)</param>
        /// <param name="isEncrypt">是否加密(默认不加密)</param>
        public void Execute
        (
            Uri uri,
            Util.WebService.RequestData requestData,
            Xamarin.Forms.Page page = null,
            Action<Util.WebService.SOAPResult> handle = null,
            bool isCompress = false,
            bool isEncrypt = false
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
                    #region 先压缩, 后加密

                    requestData.IsCompress = isCompress;
                    if (requestData.IsCompress)
                    {
                        requestData.CompressType = "GZip"; // 默认使用GZip压缩
                        for (int i = 0; i < requestData.JsonArgs.Count; i++)
                        {
                            requestData.JsonArgs[i] = requestData.JsonArgs[i].GZip_Compress2String();
                        }
                    }

                    requestData.IsEncrypt = isEncrypt;
                    if (requestData.IsEncrypt)
                    {
                        requestData.EncryptType = "RSA"; // 默认使用RSA加密
                        for (int i = 0; i < requestData.JsonArgs.Count; i++)
                        {
                            requestData.JsonArgs[i] = requestData.JsonArgs[i].RSA_Encrypt();
                        }
                    }

                    #endregion

                    data = mClient.UploadString(uri, "POST", Util.JsonUtils.SerializeObject(requestData));
                }
                catch (System.Net.WebException webEx)
                {
                    string msg = "执行 {0} 发生未知错误".FormatWith(requestData.MethodName);
                    throw new Exception(msg, webEx);
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
                    string msg = "执行 {0} 发生未知错误：args.Result为空值".FormatWith(requestData.MethodName);
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

                #region soapResult.ReturnObjectJson 解密 & 解压

                // 1 是否经过加密, 若是进行解密
                if (soapResult.IsEncrypt == true)
                {
                    switch (soapResult.EncryptType.ToUpper())
                    {
                        case "RSA": soapResult.ReturnObjectJson = soapResult.ReturnObjectJson.RSA_Decrypt(); break;
                        case "DES": soapResult.ReturnObjectJson = soapResult.ReturnObjectJson.DES_Decrypt(); break;
                        default: break;
                    }

                    soapResult.IsEncrypt = false; // 解密后设置为False
                }

                // 2 是否经过压缩, 若是进行解压
                if (soapResult.IsCompress == true)
                {
                    switch (soapResult.CompressType.ToUpper())
                    {
                        case "GZIP": soapResult.ReturnObjectJson = soapResult.ReturnObjectJson.GZip_Decompress2String(); break;
                        default: break;
                    }

                    soapResult.IsEncrypt = false; // 解压后设置为False
                }

                #endregion

                handle.Invoke(soapResult);

            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// 缺点 : 应该把 BackgroundWorker 用于mClient.UploadString(), 而不是在 WebService 具体方法中创建 BackgroundWorker
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public Util.WebService.SOAPResult ExecuteV1(Uri uri, Util.WebService.RequestData requestData)
        {
            string data = string.Empty;
            try
            {
                data = mClient.UploadString(uri, "POST", Util.JsonUtils.SerializeObject(requestData));
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
        /// <param name="uri"></param>
        /// <param name="requestData"></param>
        /// <param name="page"></param>
        /// <param name="handle"></param>
        public void ExecuteV2
        (
            Uri uri,
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
                    data = mClient.UploadString(uri, "POST", Util.JsonUtils.SerializeObject(requestData));
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
                    string msg = "执行 {0} 发生未知错误：args.Result为空值".FormatWith(requestData.MethodName);
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

        /// <summary>
        /// 待优化: 支持压缩与加密 requestData.JsonArgs
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="requestData"></param>
        /// <param name="page"></param>
        /// <param name="handle"></param>
        public void ExecuteV3
        (
            Uri uri,
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
                    data = mClient.UploadString(uri, "POST", Util.JsonUtils.SerializeObject(requestData));
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
                    string msg = "执行 {0} 发生未知错误：args.Result为空值".FormatWith(requestData.MethodName);
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
        /// 在V3的基础上
        /// 增加对 requestData.JsonArgs 的压缩与加密
        /// </summary>
        /// <param name="uri">Web Service Uri</param>
        /// <param name="requestData">参数</param>
        /// <param name="page">UI Page</param>
        /// <param name="handle">UI Handler</param>
        /// <param name="isCompress">是否压缩(默认不压缩)</param>
        /// <param name="isEncrypt">是否加密(默认不加密)</param>
        public void ExecuteV4
        (
            Uri uri,
            Util.WebService.RequestData requestData,
            Xamarin.Forms.Page page = null,
            Action<Util.WebService.SOAPResult> handle = null,
            bool isCompress = false,
            bool isEncrypt = false
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
                    #region 先压缩, 后加密

                    requestData.IsCompress = isCompress;
                    if (requestData.IsCompress)
                    {
                        requestData.CompressType = "GZip"; // 默认使用GZip压缩
                        for (int i = 0; i < requestData.JsonArgs.Count; i++)
                        {
                            requestData.JsonArgs[i] = requestData.JsonArgs[i].GZip_Compress2String();
                        }
                    }

                    requestData.IsEncrypt = isEncrypt;
                    if (requestData.IsEncrypt)
                    {
                        requestData.EncryptType = "RSA"; // 默认使用RSA加密
                        for (int i = 0; i < requestData.JsonArgs.Count; i++)
                        {
                            requestData.JsonArgs[i] = requestData.JsonArgs[i].RSA_Encrypt();
                        }
                    }

                    #endregion

                    data = mClient.UploadString(uri, "POST", Util.JsonUtils.SerializeObject(requestData));
                }
                catch (System.Net.WebException webEx)
                {
                    string msg = "执行 {0} 发生未知错误".FormatWith(requestData.MethodName);
                    throw new Exception(msg, webEx);
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
                    string msg = "执行 {0} 发生未知错误：args.Result为空值".FormatWith(requestData.MethodName);
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

                #region soapResult.ReturnObjectJson 解密 & 解压

                // 1 是否经过加密, 若是进行解密
                if (soapResult.IsEncrypt == true)
                {
                    switch (soapResult.EncryptType.ToUpper())
                    {
                        case "RSA": soapResult.ReturnObjectJson = soapResult.ReturnObjectJson.RSA_Decrypt(); break;
                        case "DES": soapResult.ReturnObjectJson = soapResult.ReturnObjectJson.DES_Decrypt(); break;
                        default: break;
                    }

                    soapResult.IsEncrypt = false; // 解密后设置为False
                }

                // 2 是否经过压缩, 若是进行解压
                if (soapResult.IsCompress == true)
                {
                    switch (soapResult.CompressType.ToUpper())
                    {
                        case "GZIP": soapResult.ReturnObjectJson = soapResult.ReturnObjectJson.GZip_Decompress2String(); break;
                        default: break;
                    }

                    soapResult.IsEncrypt = false; // 解压后设置为False
                }

                #endregion

                handle.Invoke(soapResult);

            };

            bw.RunWorkerAsync();
        }
    }
}
