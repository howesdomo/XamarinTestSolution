using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Client.Common
{
    /// <summary>
    /// NativeSettingsUtils 各个项目自由编写区域
    /// </summary>
    public partial class NativeSettingsUtils
    {

        public static void InitConfig(string path)
        {
            XDocument xDoc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement
                (
                    "configuration",
                    new XElement
                    (
                        "DebugMode", 0
                    )
                )

                #region 各个程序自行配置


                #endregion
            );

            xDoc.Save(path);
        }

        public static void ReadConfig(string path, Common.StaticInfoInitArgs staticInfoInitArgs)
        {
            #region 旧版 纯XML 配置

            //XDocument xDoc = XDocument.Load(path);

            //var matchDebugMode = xDoc.Elements("configuration").Elements("DebugMode").FirstOrDefault();
            //if (matchDebugMode != null)
            //{
            //    int outDebugMode = -9999;
            //    if (int.TryParse(matchDebugMode.Value, out outDebugMode) == true)
            //    {
            //        staticInfoInitArgs.DebugMode = outDebugMode;
            //    }
            //}

            //#region 各个程序自行配置


            //#endregion

            #endregion

            XDocument xDoc = XDocument.Load(path);

            staticInfoInitArgs.DebugMode = ConfigUtils.GetObject<int>(xDoc, "DebugMode");

            #region 各个程序自行配置


            #endregion
        }
    }
}
