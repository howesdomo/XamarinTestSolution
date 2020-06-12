using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    /// <summary>
    /// V 1.0.1 2019-9-16 16:47:45 增加属性 IsIndependent
    /// V 1.0.0 2018-6-4 17:37:19 创建 WebSetting 类, 用于定义 .asmx, .ashx, web api 等 Web 应用
    /// </summary>
    public class WebSetting
    {
        /// <summary>
        /// 是独立的
        /// 若 '是', 执行 GetUri 只用回自身的 IP 与 Port, 不跟随 StaticInfo 的 IP, Port
        /// 若 '否', 则跟随
        /// </summary>
        public bool IsIndependent { get; set; }

        /// <summary>
        /// 服务名称，配置文件对应的名称
        /// </summary>
        public string ServiceSettingName { get; set; }

        /// <summary>
        /// IP地址 / 网址
        /// </summary>
        public string IPOrWebAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 应用程序
        /// </summary>
        public string AppName { get; set; }

        public WebSetting()
        {

        }

        public WebSetting(string serviceSettingName, string ipOrWebAddress, string port, string appName, bool isIndependent = false)
        {
            this.ServiceSettingName = serviceSettingName;
            this.IPOrWebAddress = ipOrWebAddress;
            this.Port = port;
            this.AppName = appName;

            this.IsIndependent = isIndependent;
        }

        public Uri GetUri()
        {
            string r = string.Empty;

            if (IsIndependent)
            {
                r = string.Format("http://{0}:{1}/{2}", this.IPOrWebAddress, this.Port, this.AppName);
            }
            else
            {
                r = string.Format("http://{0}:{1}/{2}", StaticInfo.IP, StaticInfo.Port, this.AppName);
            }

            return new Uri(r);
        }
    }
}
