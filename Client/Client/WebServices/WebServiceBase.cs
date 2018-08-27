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

        public void Execute(Xamarin.Forms.Page page, Uri mUri, Util.WebService.RequestData requestData, Action handle = null)
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
                    page.DisplayAlert("Error", args.Error.GetFullInfo(), "确定");
                    return;
                }

                if (args.Result == null)
                {
                    page.DisplayAlert("Error", "SOAPResult为空", "确定");
                    return;
                }

                Util.WebService.SOAPResult soapResult = args.Result as Util.WebService.SOAPResult;

                if (soapResult.IsComplete == false)
                {
                    page.DisplayAlert("Error", soapResult.ExceptionInfo, "确定");
                }
                else if (soapResult.IsSuccess == false)
                {
                    page.DisplayAlert("Error", soapResult.BusinessExceptionInfo, "确定");
                }
                else
                {
                    if (handle != null)
                    {
                        handle.Invoke();
                    }
                }
            };
            
            bw.RunWorkerAsync();
        }

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
    }
}
