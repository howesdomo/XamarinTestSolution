using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Client.Common
{
    /// <summary>
    /// ServiceSettingsUtils 各个项目自由编写区域
    /// </summary>
    public partial class ServiceSettingsUtils
    {
        public static void InitConfig(string path)
        {
            #region 旧版 纯XML 配置

            //List<WebSetting> list = new List<WebSetting>();

            //list.Add(new WebSetting(
            //            serviceSettingName: "AppWebServer_WebServiceHandler",
            //            ipOrWebAddress: "10.20.1.49",
            //            port: "80",
            //            appName: "APPWebServiceHandler.ashx"
            //        ));

            //list.Add(new WebSetting(
            //            serviceSettingName: "WebAPISetting",
            //            ipOrWebAddress: "110.114.119.120",
            //            port: "7974",
            //            appName: "APPWebServiceHandler.ashx",
            //            isIndependent: true
            //        ));

            //XDocument xDoc = new XDocument
            //(
            //    new XDeclaration("1.0", "utf-8", "yes"),
            //    new XElement
            //    (
            //        "configuration",
            //        new XElement
            //        (
            //            "IP", "123.321.123.456"
            //        ),
            //        new XElement
            //        (
            //            "Port", "808"
            //        ),
            //        new XElement
            //        (
            //            "WebSettings",
            //            list.Select
            //            (
            //                i => new XElement
            //                (
            //                    "WebSetting",
            //                    new XAttribute("Name", i.ServiceSettingName),
            //                    new XElement("IsIndependent", i.IsIndependent),
            //                    new XElement("IPOrWebAddress", i.IPOrWebAddress),
            //                    new XElement("Port", i.Port),
            //                    new XElement("AppName", i.AppName)
            //                )
            //            )
            //        )
            //    )
            //);

            //xDoc.Save(path);

            #endregion

            // 新版 XML+Json 的配置文件
            List<WebSetting> list = new List<WebSetting>();

            #region 各个程序自行配置

            list.Add(new WebSetting(
                        serviceSettingName: "AppWebServer_WebServiceHandler",
                        ipOrWebAddress: "10.20.1.49",
                        port: "80",
                        appName: "PCWebServer/APPWebServiceHandler.ashx"
                    ));

            list.Add(new WebSetting(
                        serviceSettingName: "WebAPISetting",
                        ipOrWebAddress: "110.114.119.120",
                        port: "7974",
                        appName: "test0/test1",
                        isIndependent: true
                    ));

            #endregion

            XDocument xDoc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement
                (
                    "configuration",

                    new XComment("通用的IP端口配置"),
                    new XElement
                    (
                        "IP", "10.20.1.49"
                    ),
                    new XElement
                    (
                        "Port", "80"
                    ),

                    new XComment("服务器配置集合"),
                    new XElement
                    (
                        "WebSettings",
                        list.Select
                        (
                            i => new XElement
                            (
                                "WebSetting",
                                new XAttribute("class", i.GetType().FullName), // class attr
                                Util.JsonUtils.SerializeObjectWithFormatted(i) // value
                            )
                        )
                    )
                )
            );

            xDoc.Save(path);
        }

        public static void ReadConfig(string path, Common.StaticInfoInitArgs staticInfoInitArgs)
        {
            #region 旧版 纯XML 配置

            //XDocument xDoc = XDocument.Load(path);

            //staticInfoInitArgs.IP = xDoc.Elements("configuration").Elements("IP").First().Value;

            //staticInfoInitArgs.Port = xDoc.Elements("configuration").Elements("Port").First().Value;

            //List<WebSetting> r = new List<WebSetting>();
            //foreach (var element in xDoc.Elements("configuration").Elements("WebSettings").Elements())
            //{
            //    WebSetting toAdd = new WebSetting();

            //    toAdd.ServiceSettingName = element.Attribute("Name").Value;

            //    toAdd.IPOrWebAddress = element.Elements("IPOrWebAddress").First().Value;
            //    toAdd.Port = element.Elements("Port").First().Value;
            //    toAdd.AppName = element.Elements("AppName").First().Value;

            //    toAdd.IsIndependent = Convert.ToBoolean(element.Elements("IsIndependent").First().Value);

            //    r.Add(toAdd);
            //}

            //#region 各个程序自行配置

            //staticInfoInitArgs.AppWebServer_WebServiceHandler = r.FirstOrDefault(i => i.ServiceSettingName == "AppWebServer_WebServiceHandler");

            //staticInfoInitArgs.WebAPISetting = r.FirstOrDefault(i => i.ServiceSettingName == "WebAPISetting");

            //#endregion

            #endregion

            // 新版 XML+Json 的配置文件

            XDocument xDoc = XDocument.Load(path);

            staticInfoInitArgs.IP = ConfigUtils.GetObject<string>(xDoc: xDoc, descendantsName: "IP");

            staticInfoInitArgs.Port = ConfigUtils.GetObject<string>(xDoc: xDoc, descendantsName: "Port");

            List<WebSetting> webSettingList = ConfigUtils.GetList<WebSetting>(xDoc: xDoc, descendantsName: "WebSetting");

            #region 各个程序自行配置

            staticInfoInitArgs.AppWebSetting = webSettingList.FirstOrDefault(i => i.ServiceSettingName == "AppWebSetting");

            staticInfoInitArgs.WebAPISetting = webSettingList.FirstOrDefault(i => i.ServiceSettingName == "WebAPISetting");

            #endregion
        }
    }
}
