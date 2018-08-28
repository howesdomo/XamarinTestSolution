using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class WebService
    {

        public void CollectUnhandleException
        (
            string errorMsg, 
            DL.Model.User u,
            Xamarin.Forms.Page page = null,
            Action<Util.WebService.SOAPResult> handle = null
        )
        {
            Uri uri = Common.StaticInfo.AppWebSetting.GetUri();

            // 方法名
            string methodName = "CollectUnhandleException";

            // 方法参数
            List<string> args = new List<string>();
            args.Add(Util.JsonUtils.SerializeObject(errorMsg));
            args.Add(Util.JsonUtils.SerializeObject(u));


            Util.WebService.RequestData requestData = new Util.WebService.RequestData();
            requestData.MethodName = methodName;
            requestData.JsonArgs = args;

            new WebServiceBase().Execute(uri, requestData, page, handle);
        }

        public void CollectUnhandleExceptionV1
        (
            string errorMsg,
            DL.Model.User u,
            System.ComponentModel.RunWorkerCompletedEventHandler runWorkerCompletedHandle = null
        )
        {
            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();

            bw.DoWork += (s, e) =>
            {
                Uri uri = Common.StaticInfo.AppWebSetting.GetUri();

                // 方法名
                string methodName = "CollectUnhandleException";

                // 方法参数
                List<string> args = new List<string>();
                args.Add(Util.JsonUtils.SerializeObject(errorMsg));
                args.Add(Util.JsonUtils.SerializeObject(u));

                Util.WebService.RequestData requestData = new Util.WebService.RequestData();
                requestData.MethodName = methodName;
                requestData.JsonArgs = args;

                e.Result = new WebServiceBase().ExecuteV1(uri, requestData);
            };

            if (runWorkerCompletedHandle != null)
            {
                bw.RunWorkerCompleted += runWorkerCompletedHandle;
            }

            // TODO 加漏斗
            bw.RunWorkerAsync();
        }

        public void CollectUnhandleExceptionV2
        (
            string errorMsg,
            DL.Model.User u,
            Xamarin.Forms.Page page = null,
            Action<string> handle = null
        )
        {
            Uri uri = Common.StaticInfo.AppWebSetting.GetUri();

            // 方法名
            string methodName = "CollectUnhandleException";

            // 方法参数
            List<string> args = new List<string>();
            args.Add(Util.JsonUtils.SerializeObject(errorMsg));
            args.Add(Util.JsonUtils.SerializeObject(u));


            Util.WebService.RequestData requestData = new Util.WebService.RequestData();
            requestData.MethodName = methodName;
            requestData.JsonArgs = args;

            new WebServiceBase().ExecuteV2(uri, requestData, page, handle);
        }

    }
}
