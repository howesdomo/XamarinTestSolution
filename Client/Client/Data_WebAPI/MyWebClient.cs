using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Data_WebAPI
{
    public partial class MyWebClient
    {
        public System.Net.WebClient mClient;

        public Uri mUri;

        public MyWebClient(string url)
        {
            if (url.IsNullOrWhiteSpace() == true)
            {
                mUri = Common.StaticInfo.AppWebSetting.GetUri();
            }
            else
            {
                mUri = new Uri(url);
            }

            this.mClient = new System.Net.WebClient();
            this.mClient.Encoding = Encoding.UTF8;
            this.mClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        }

        public string GetOrder()
        {
            string data = string.Empty;
            try
            {
                int count = 0;
                data = mClient.UploadString(mUri, CoreUtil.JsonUtils.SerializeObject(count));
            }
            catch (System.Net.WebException webEx)
            {
                string msg = "{0}".FormatWith(webEx.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                mClient.Dispose();
            }
            return data;
        }

        public string ABC(object model)
        {
            string data = string.Empty;
            try
            {
                data = mClient.UploadString(mUri, CoreUtil.JsonUtils.SerializeObject(model));
            }
            catch (System.Net.WebException webEx)
            {
                string msg = "{0}".FormatWith(webEx.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                mClient.Dispose();
            }
            return data;
        }


    }
}
