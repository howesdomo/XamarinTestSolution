using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Client.iOS
{
    public class FileLooper
    {
        /// <summary>
        /// 遍历文件夹，获取该文件结构
        /// </summary>
        /// <param name="dirPath">Dir path.</param>
        /// <param name="level">Level.</param>
        public static void Loop(string dirPath, int level)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(dirPath);
                string[] directories = System.IO.Directory.GetDirectories(dirPath);

                foreach (var item in directories)
                {
                    string msg = "|{0}{1}".FormatWith("".PadLeft(level, '_'), item);
                    System.Diagnostics.Debug.WriteLine(msg);
                    Loop(item, level + 1);
                }

                foreach (var item in files)
                {
                    string msg = "|{0}{1}".FormatWith("".PadLeft(level, '_'), item);
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.GetFullInfo();
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}