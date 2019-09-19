using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class WebService
    {
        #region (系统框架) 上传异常信息到服务器

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


        public void CollectUnhandleExceptionV3
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

            new WebServiceBase().ExecuteV3(uri, requestData, page, handle);
        }

        #endregion

        #region (系统框架) 检测程序是否有更新

        public void GetLastestVersion
        (
            string updateInfoJsonStr,
            Xamarin.Forms.Page page = null,
            Action<Util.WebService.SOAPResult> handle = null
        )
        {
            Uri uri = Common.StaticInfo.AppWebSetting.GetUri();

            // 方法名
            string methodName = "GetLastestVersion";

            // 方法参数
            List<string> args = new List<string>();
            args.Add(updateInfoJsonStr);


            Util.WebService.RequestData requestData = new Util.WebService.RequestData();
            requestData.MethodName = methodName;
            requestData.JsonArgs = args;

            new WebServiceBase().Execute(uri, requestData, page, handle);
        }

        #endregion

        #region 测试采用压缩方式后提交集合信息

        public void TestWebService_GetOrders_isCompress_True
        (
            List<DL.Model.Order> orders,
            DL.Model.User u,
            Xamarin.Forms.Page page = null,
            Action<Util.WebService.SOAPResult> handle = null
        )
        {
            Uri uri = Common.StaticInfo.AppWebSetting.GetUri();

            // 方法名
            string methodName = "TestWebService_GetOrders";

            // 方法参数
            List<string> args = new List<string>();
            args.Add(Util.JsonUtils.SerializeObject(orders));

            Util.WebService.RequestData requestData = new Util.WebService.RequestData();
            requestData.MethodName = methodName;
            requestData.JsonArgs = args;

            new WebServiceBase().Execute
            (
                uri: uri,
                requestData: requestData,
                page: page,
                handle: handle,
                isCompress: true,
                isEncrypt: false
            );
        }

        public void TestWebService_GetOrders_isCompress_False
        (
            List<DL.Model.Order> orders,
            DL.Model.User u,
            Xamarin.Forms.Page page = null,
            Action<Util.WebService.SOAPResult> handle = null
        )
        {
            Uri uri = Common.StaticInfo.AppWebSetting.GetUri();

            // 方法名
            string methodName = "TestWebService_GetOrders";

            // 方法参数
            List<string> args = new List<string>();
            args.Add(Util.JsonUtils.SerializeObject(orders));

            Util.WebService.RequestData requestData = new Util.WebService.RequestData();
            requestData.MethodName = methodName;
            requestData.JsonArgs = args;

            new WebServiceBase().Execute
            (
                uri: uri,
                requestData: requestData,
                page: page,
                handle: handle,
                isCompress: false,
                isEncrypt: false
            );
        }

        #endregion        
    }
}
