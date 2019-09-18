using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Client.Common
{
    /// <summary>
    /// V 1.0.0 - 2019-9-17 15:37:39 编写 Config工具类
    /// </summary>
    public class ConfigUtils
    {
        public static T GetObject<T>(XDocument xDoc, string descendantsName)
        {
            foreach (var element in xDoc.Descendants(descendantsName))
            {
                string value = element.Value;

                var classAttr = element.Attribute("class");

                Type convertType = typeof(T);

                if (classAttr != null
                    && classAttr.Value != convertType.FullName)
                {
                    throw new Exception($"转换类型错误。descendantsName:{descendantsName}\r\nxml声明类型:{classAttr.Value};待转换类型:{convertType.FullName}");
                }

                if (convertType.FullName == "System.String")
                {
                    return (T)(Object)value;
                }

                return Util.JsonUtils.DeserializeObject<T>(value);
            }

            return default(T);
        }

        public static List<T> GetList<T>(XDocument xDoc, string descendantsName)
        {
            List<T> r = null;

            Type convertType = typeof(T);

            foreach (var element in xDoc.Descendants(descendantsName))
            {
                var classAttr = element.Attribute("class");

                if (classAttr != null
                    && classAttr.Value != convertType.FullName)
                {
                    throw new Exception($"转换类型错误。descendantsName:{descendantsName}\r\nxml声明类型:{classAttr.Value};待转换类型:{convertType.FullName}");
                }

                T toAdd = default(T);

                string value = element.Value;
                if (convertType.FullName == "System.String")
                {
                    toAdd = (T)(Object)value;
                }
                else
                {
                    toAdd = Util.JsonUtils.DeserializeObject<T>(value);
                }

                if (r == null)
                {
                    r = new List<T>();
                }

                r.Add(toAdd);
            }

            return r;
        }

        public static XElement GetElement(XDocument xDoc, string descendantsName)
        {
            foreach (var element in xDoc.Descendants(descendantsName))
            {
                return element;
            }

            return null;
        }

        public static IEnumerable<XElement> GetElements(XDocument xDoc, string descendantsName)
        {
            return xDoc.Descendants(descendantsName);
        }

        /// <summary>
        /// 添加一项新的 XElement
        /// </summary>
        /// <param name="xDoc">XML对象</param>
        /// <param name="descendantsName">XElement Name</param>
        /// <param name="value">(选填)object.ToString()(value与toJsonValue二选一)</param>
        /// <param name="toJsonValue">(选填).ToJsonString()(value与toJsonValue二选一)</param>
        /// <param name="comment">添加注释</param>
        public static void Add(XDocument xDoc, string descendantsName, object value = null, object toJsonValue = null, string comment = "")
        {
            var match = xDoc.Descendants(descendantsName);
            if (match.Count() > 0)
            {
                throw new Exception($"添加的 XElement 已存在。 descendantsName:{descendantsName}");
            }
            else
            {
                // 添加注释 ( 若传入注释的值不为空 )
                if (comment.IsNullOrWhiteSpace() == false)
                {
                    xDoc.Element("configuration")
                        .Add(new XComment(comment));
                }

                // 添加内容

                if (value != null)
                {
                    xDoc.Element("configuration")
                    .Add
                    (
                        new XElement
                        (
                            descendantsName,
                            value.ToString()
                        )
                    );
                }
                else if (toJsonValue != null)
                {
                    xDoc.Element("configuration")
                        .Add
                        (
                            new XElement
                            (
                                descendantsName,
                                new XAttribute("class", toJsonValue.GetType().FullName),
                                Util.JsonUtils.SerializeObjectWithFormatted(toJsonValue)
                            )
                        );
                }
                else
                {
                    throw new Exception("请传入value。value与toJsonValue 均为 Null");
                }
            }
        }

        /// <summary>
        /// 更新一项 XElement 的值
        /// </summary>
        /// <param name="xDoc">XML对象</param>
        /// <param name="descendantsName">XElement Name</param>
        /// <param name="value">(选填)object.ToString()(value与toJsonValue二选一)</param>
        /// <param name="toJsonValue">(选填).ToJsonString()(value与toJsonValue二选一)</param>
        public static void Update(XDocument xDoc, string descendantsName, object value = null, object toJsonValue = null)
        {
            var match = xDoc.Descendants(descendantsName);
            if (match.Count() > 0)
            {
                if (match.Count() > 1)
                {
                    throw new Exception("xDoc.Descendants 匹配结果多于 1 项");
                }

                foreach (var item in match)
                {
                    if (value != null)
                    {
                        item.Value = value.ToString();
                    }
                    else if (toJsonValue != null)
                    {
                        item.Value = Util.JsonUtils.SerializeObjectWithFormatted(toJsonValue);
                    }
                    else
                    {
                        throw new Exception("请传入value。value与toJsonValue 均为 Null");
                    }
                    
                    break;
                }
            }
            else
            {
                throw new Exception("不存在");
            }
        }
    }

}
