using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Model
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Serializable]
    public class Menu
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public string Image { get; set; }

    }
}
