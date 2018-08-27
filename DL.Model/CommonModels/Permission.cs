using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    public class Permission
    {
        public Permission()
        {
            IsMenu = true;
        }

        public Guid ID { get; set; }

        public Guid ModuleID { get; set; }

        public string PermissionName { get; set; }

        public string SysCode { get; set; }

        public int Seq { get; set; }

        public string ClassName { get; set; }

        /// <summary>
        /// 默认权限即是菜单, 特例特殊排除
        /// </summary>
        public bool IsMenu { get; set; }

    }
}
