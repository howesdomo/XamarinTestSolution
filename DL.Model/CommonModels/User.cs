using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DL.Model
{
    /// <summary>
    /// 登录用户
    /// </summary>
    [Serializable]
    public class User
    {
        public Guid ID { get; set; }

        public string LoginAccount { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Menu> MenuList { get; set; }

        public List<Permission> PermissionList { get; set; }


        


        /// <summary>
        /// 一个账号登录一台设备
        /// </summary>
        public string TokenID { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        public string DeviceInfo { get; set; }
    }
}