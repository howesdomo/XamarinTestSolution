using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    // 知识点
    // EndpointConfiguration 是枚举类型
    // EndpointConfiguration.AppWebServiceSoap == 0
    // EndpointConfiguration.AppWebServiceSoap12 == 12

    public class AppWebWebService : AppWebServiceReference.AppWebServiceSoapClient
    {
        public AppWebWebService(EndpointConfiguration endpointConfiguration = EndpointConfiguration.AppWebServiceSoap)
            : base(endpointConfiguration)
        {
            var setting = StaticInfo.AppWebSetting;
            string uri = string.Format("http://{0}:{1}/{2}", setting.IPOrWebAddress, setting.Port, setting.AppName);

            if (StaticInfo.IsDebugMode)
            {
                // TODO AppWeb DebugMode 
                // uri = string.Format("http://localhost:18888/PCWebService.asmx");
            }

            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(new Uri(uri) { });
        }

        public AppWebWebService(WebSetting setting, EndpointConfiguration endpointConfiguration = EndpointConfiguration.AppWebServiceSoap)
            : base(endpointConfiguration)
        {
            string uri = string.Format("http://{0}:{1}/{2}", setting.IPOrWebAddress, setting.Port, setting.AppName);
            if (StaticInfo.IsDebugMode)
            {
                // TODO AppWeb DebugMode 
                // uri = string.Format("http://localhost:18888/PCWebService.asmx");
            }

            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(new Uri(uri) { });
        }
    }


    // TODO SecurityWeb ????

   
}
