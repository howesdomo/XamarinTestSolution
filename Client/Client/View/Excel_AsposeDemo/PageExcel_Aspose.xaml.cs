using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageExcel_Aspose : ContentPage
    {
        public PageExcel_Aspose()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            btnImportExcel.Clicked += BtnImportExcel_Clicked;
        }

        private void BtnImportExcel_Clicked(object sender, EventArgs e)
        {
            try
            {

                var path = System.IO.Path.Combine(Common.StaticInfo.AndroidExternalPath, "HoweApp", "Excel2DataSetTest.xlsx");
                if (System.IO.File.Exists(path) == false)
                {
                    string msg = "{0}".FormatWith("File not exists");
                    System.Diagnostics.Debug.WriteLine(msg);
                    DisplayAlert("错误", msg, "确定");
                    return;
                }

                System.Data.DataSet ds = Util.Excel.ExcelUtils_Aspose.Excel2DataSet(path);

                if (ds.Tables.Count == 3)
                {
                    foreach (var dt in ds.Tables)
                    {
                        string f = "success";
                    }
                }                
            }
            catch (Exception ex)
            {
                string msg = "{0}".FormatWith(ex.GetFullInfo());
                System.Diagnostics.Debug.WriteLine(msg);
                DisplayAlert("错误", msg, "确定");
            }
        }
    }
}