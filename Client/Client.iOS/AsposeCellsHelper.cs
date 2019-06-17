using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Client.iOS
{
    public class AsposeCellsHelper : Common.IExcelUtils_Aspose
    {
        public void test() // 测试结果, 暂时无法在 Xamarin.Android 中授权, 能够读取Excel文件内容
        {
            //string LData = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4NCjxMaWNlbnNlPg0KICAgIDxEYXRhPg0KICAgICAgICA8TGljZW5zZWRUbz5pckRldmVsb3BlcnMuY29tPC9MaWNlbnNlZFRvPg0KICAgICAgICA8RW1haWxUbz5pbmZvQGlyRGV2ZWxvcGVycy5jb208L0VtYWlsVG8+DQogICAgICAgIDxMaWNlbnNlVHlwZT5EZXZlbG9wZXIgT0VNPC9MaWNlbnNlVHlwZT4NCiAgICAgICAgPExpY2Vuc2VOb3RlPkxpbWl0ZWQgdG8gMTAwMCBkZXZlbG9wZXIsIHVubGltaXRlZCBwaHlzaWNhbCBsb2NhdGlvbnM8L0xpY2Vuc2VOb3RlPg0KICAgICAgICA8T3JkZXJJRD43ODQzMzY0Nzc4NTwvT3JkZXJJRD4NCiAgICAgICAgPFVzZXJJRD4xMTk0NDkyNDM3OTwvVXNlcklEPg0KICAgICAgICA8T0VNPlRoaXMgaXMgYSByZWRpc3RyaWJ1dGFibGUgbGljZW5zZTwvT0VNPg0KICAgICAgICA8UHJvZHVjdHM+DQogICAgICAgICAgICA8UHJvZHVjdD5Bc3Bvc2UuVG90YWwgUHJvZHVjdCBGYW1pbHk8L1Byb2R1Y3Q+DQogICAgICAgIDwvUHJvZHVjdHM+DQogICAgICAgIDxFZGl0aW9uVHlwZT5FbnRlcnByaXNlPC9FZGl0aW9uVHlwZT4NCiAgICAgICAgPFNlcmlhbE51bWJlcj57RjJCOTcwNDUtMUIyOS00QjNGLUJENTMtNjAxRUZGQTE1QUE5fTwvU2VyaWFsTnVtYmVyPg0KICAgICAgICA8U3Vic2NyaXB0aW9uRXhwaXJ5PjIwOTkxMjMxPC9TdWJzY3JpcHRpb25FeHBpcnk+DQogICAgICAgIDxMaWNlbnNlVmVyc2lvbj4zLjA8L0xpY2Vuc2VWZXJzaW9uPg0KICAgIDwvRGF0YT4NCiAgICA8U2lnbmF0dXJlPlFYTndiM05sTGxSdmRHRnNMb1B5YjJSMVkzUWdSbUZ0YVd4NTwvU2lnbmF0dXJlPg0KPC9MaWNlbnNlPg==";
            //System.IO.Stream stream = new System.IO.MemoryStream(Convert.FromBase64String(LData));
            //stream.Seek(0, System.IO.SeekOrigin.Begin);
            //Aspose.Cells.License license = new Aspose.Cells.License();
            //license.SetLicense(stream);

            try
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aspose.xlsx");

                StringBuilder sb = new StringBuilder();

                Aspose.Cells.Workbook wb = null;

                if (System.IO.File.Exists(path) == false)
                {
                    wb = new Aspose.Cells.Workbook();
                }
                else
                {
                    wb = new Aspose.Cells.Workbook(path);
                }

                wb = new Aspose.Cells.Workbook();

                if (wb != null)
                {
                    bool isLicensed = wb.IsLicensed;
                    if (isLicensed == false)
                    {
                        sb.AppendLine("Run Time {0} : isLicensed = false;".FormatWith(0));
                    }

                    if (wb.Worksheets.Count > 1)
                    {
                        sb.AppendLine("Run Time {0} : Worksheets Count = {1};".FormatWith(0, wb.Worksheets.Count));
                    }

                    var ws = wb.Worksheets[0];
                    Aspose.Cells.Cell cell0 = ws.Cells[0, 0];

                    string msg = "{0}".FormatWith(cell0.Value);
                    System.Diagnostics.Debug.WriteLine(msg);

                    cell0.Value = "A1 hello";

                    // 保存
                    string savePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aspose_Save.xlsx");
                    wb.Save(savePath);
                }
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetInfo());
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}