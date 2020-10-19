using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Util.Excel;

namespace Util.Excel
{
    /// <summary>
    /// V 1.0.0 - 2019-9-23 09:25:40
    /// 使用于 Xamarin.Android 的  ExcelUtils_Aspose
    /// 实现了目前 IExcelUtils接口的 4 个方法
    /// </summary>
    public class ExcelUtils_Aspose_XamarinAndroid : IExcelUtils
    {
        #region **** Hot Patch 恢复到未加密状态 **** 

        /// <summary>
        /// 最新版本的恢复到未加密状态方法
        /// </summary>
        public static void InitializeAsposeCells()
        {
            // TODO 读取版本号, 根据版本号执行对应的初始化方法
            // InitializeAsposeCells_v8_6_3();
            // InitializeAsposeCells_v18_12_0();
            InitializeAsposeCells_v19_5_0();
        }

        /// <summary>
        /// Aspose 8.6.3 Hot Patch 恢复到未加密状态方式
        /// 
        /// Winform, WPF 请在 OnStartup 中调用此方法
        /// Web程序, 请在全局应用类(Global.asax)中 Application_Start 方法中调用此方法
        /// </summary>
        public static void InitializeAsposeCells_v8_6_3()
        {
            const BindingFlags BINDING_FLAGS_ALL = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

            const string CLASS_LICENSER = "\u0092\u0092\u0008.\u001C";
            const string CLASS_LICENSERHELPER = "\u0011\u0001\u0006.\u001A";
            const string ENUM_ISTRIAL = "\u0092\u0092\u0008.\u001B";

            const string FIELD_LICENSER_CREATED_LICENSE = "\u0001";     // static
            const string FIELD_LICENSER_EXPIRY_DATE = "\u0002";         // instance
            const string FIELD_LICENSER_ISTRIAL = "\u0001";             // instance

            const string FIELD_LICENSERHELPER_INT128 = "\u0001";        // static
            const string FIELD_LICENSERHELPER_BOOLFALSE = "\u0001";     // static

            const int CONST_LICENSER_ISTRIAL = 1;
            const int CONST_LICENSERHELPER_INT128 = 128;
            const bool CONST_LICENSERHELPER_BOOLFALSE = false;

            //- Field setter for convinient
            Action<FieldInfo, Type, string, object, object> setValue =
                delegate (FieldInfo field, Type chkType, string chkName, object obj, object value)
                {
                    if ((field.FieldType == chkType) && (field.Name == chkName))
                    {
                        field.SetValue(obj, value);
                    }
                };


            //- Get types
            Assembly assembly = Assembly.GetAssembly(typeof(Aspose.Cells.License));
            Type typeLic = null, typeIsTrial = null, typeHelper = null;
            foreach (Type type in assembly.GetTypes())
            {
                if ((typeLic == null) && (type.FullName == CLASS_LICENSER))
                {
                    typeLic = type;
                }
                else if ((typeIsTrial == null) && (type.FullName == ENUM_ISTRIAL))
                {
                    typeIsTrial = type;
                }
                else if ((typeHelper == null) && (type.FullName == CLASS_LICENSERHELPER))
                {
                    typeHelper = type;
                }
            }
            if (typeLic == null || typeIsTrial == null || typeHelper == null)
            {
                throw new Exception();
            }

            //- In class_Licenser
            object license = Activator.CreateInstance(typeLic);
            foreach (FieldInfo field in typeLic.GetFields(BINDING_FLAGS_ALL))
            {
                setValue(field, typeLic, FIELD_LICENSER_CREATED_LICENSE, null, license);
                setValue(field, typeof(DateTime), FIELD_LICENSER_EXPIRY_DATE, license, DateTime.MaxValue);
                setValue(field, typeIsTrial, FIELD_LICENSER_ISTRIAL, license, CONST_LICENSER_ISTRIAL);
            }

            //- In class_LicenserHelper
            foreach (FieldInfo field in typeHelper.GetFields(BINDING_FLAGS_ALL))
            {
                setValue(field, typeof(int), FIELD_LICENSERHELPER_INT128, null, CONST_LICENSERHELPER_INT128);
                setValue(field, typeof(bool), FIELD_LICENSERHELPER_BOOLFALSE, null, CONST_LICENSERHELPER_BOOLFALSE);
            }
        }

        /// <summary>
        /// Aspose 18.12.0 Hot Patch 恢复到未加密状态方式
        /// 
        /// Winform, WPF 请在 OnStartup 中调用此方法
        /// Web程序, 请在全局应用类(Global.asax)中 Application_Start 方法中调用此方法
        /// </summary>
        public static bool InitializeAsposeCells_v18_12_0()
        {
            var name = Assembly.CreateQualifiedName(typeof(Aspose.Cells.License).Assembly.FullName,
                "\u0002\u200B\u2001\u2000");
            var licType = Type.GetType(name, false, false);
            var helperName = Assembly.CreateQualifiedName(typeof(Aspose.Cells.License).Assembly.FullName,
                "\u000E\u2001\u2002\u2000");
            var helperType = Type.GetType(helperName, false, false);

            if (licType == null || helperType == null)
                return false;
            try
            {
                object helper = Activator.CreateInstance(helperType);
                helperType.GetField("\u0008", BindingFlags.NonPublic | BindingFlags.Instance)
                    .SetValue(helper, (256));

                helperType.GetField("\u0002", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                   .SetValue(null, helper);


                object lic = Activator.CreateInstance(licType);

                licType.GetField("\u000E", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                    .SetValue(lic, (int)1);
                licType.GetField("\u0008", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                    .SetValue(lic, DateTime.Today.AddDays(1000).ToString());
                licType.GetField("\u000F", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                    .SetValue(null, lic);

            }
            catch (Exception ex)
            {
                string msg = $"{ex.GetFullInfo()}";
                System.Diagnostics.Debug.WriteLine(msg);

#if DEBUG
                System.Diagnostics.Debugger.Break();
#endif

                return false;
            }
            return true;
        }

        public static void InitializeAsposeCells_v19_5_0()
        {
            // 热心网友提供的 Key , 未清楚可以使用到什么时间, 但确实能够正常使用
            string LData = "77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiID8+DQo8TGljZW5zZT4NCiAgPERhdGE+DQogICAgPExpY2Vuc2VkVG8+U2hhbmdoYWkgSHVkdW4gSW5mb3JtYXRpb24gVGVjaG5vbG9neSBDby4sIEx0ZDwvTGljZW5zZWRUbz4NCiAgICA8RW1haWxUbz4zMTc3MDE4MDlAcXEuY29tPC9FbWFpbFRvPg0KICAgIDxMaWNlbnNlVHlwZT5EZXZlbG9wZXIgT0VNPC9MaWNlbnNlVHlwZT4NCiAgICA8TGljZW5zZU5vdGU+TGltaXRlZCB0byAxIGRldmVsb3BlciwgdW5saW1pdGVkIHBoeXNpY2FsIGxvY2F0aW9uczwvTGljZW5zZU5vdGU+DQogICAgPE9yZGVySUQ+MTgwNTE0MjAxMTE2PC9PcmRlcklEPg0KICAgIDxVc2VySUQ+MjY2MTY2PC9Vc2VySUQ+DQogICAgPE9FTT5UaGlzIGlzIGEgcmVkaXN0cmlidXRhYmxlIGxpY2Vuc2U8L09FTT4NCiAgICA8UHJvZHVjdHM+DQogICAgICA8UHJvZHVjdD5Bc3Bvc2UuVG90YWwgZm9yIC5ORVQ8L1Byb2R1Y3Q+DQogICAgPC9Qcm9kdWN0cz4NCiAgICA8RWRpdGlvblR5cGU+RW50ZXJwcmlzZTwvRWRpdGlvblR5cGU+DQogICAgPFNlcmlhbE51bWJlcj4yMTBlYzhlNy04MWUxLTQ1MzctYjQ0Ni02OTJkZTQ5ODEyMTc8L1NlcmlhbE51bWJlcj4NCiAgICA8U3Vic2NyaXB0aW9uRXhwaXJ5PjIwMTkwNTE3PC9TdWJzY3JpcHRpb25FeHBpcnk+DQogICAgPExpY2Vuc2VWZXJzaW9uPjMuMDwvTGljZW5zZVZlcnNpb24+DQogICAgPExpY2Vuc2VJbnN0cnVjdGlvbnM+aHR0cDovL3d3dy5hc3Bvc2UuY29tL2NvcnBvcmF0ZS9wdXJjaGFzZS9saWNlbnNlLWluc3RydWN0aW9ucy5hc3B4PC9MaWNlbnNlSW5zdHJ1Y3Rpb25zPg0KICA8L0RhdGE+DQogIDxTaWduYXR1cmU+Y3RKM3lMeFNBUHNCUWQwSmNxZjdDQTUzRnpOMVlydmFBNWRTclRwZEZXL0FmaDBoeUtLd3J5K0MxdGpXSU9FRnl6S1lXSCtOZ24vSGVYVXpNUUpBMFJvb3djcTExMm5WL1FuclNTcURtNkZKVk5zc0g0cC9ZbVhSamw3TEJpeHdWOEFieVdYOGxoVm95b2s3bEk1azVLOGJiYUsrVDhVcitqSXdTWkFjbVZBPTwvU2lnbmF0dXJlPg0KPC9MaWNlbnNlPg==";
            Stream stream = new MemoryStream(Convert.FromBase64String(LData));
            stream.Seek(0, SeekOrigin.Begin);
            new Aspose.Cells.License().SetLicense(stream);
        }

        /// <summary>
        /// 测试 Hot Patch 恢复到未加密状态方式是否成功
        /// 检验 X 次, 查看是否含有未注册的Worksheet出现
        /// </summary>
        /// <returns></returns>
        public static string TestAsposeCellsHotPatch(string pathTemplate = null, int totalTestCount = 30)
        {
            if (pathTemplate.IsNullOrEmpty())
            {
                string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test");
                if (System.IO.Directory.Exists(folderPath) == false) { System.IO.Directory.CreateDirectory(folderPath); }
                string zero = "{0}";
                pathTemplate = $"{folderPath}\\TestAspose{zero}.xlsx";
            }
            string path = pathTemplate.FormatWith(0);
            string exportPath = string.Empty;

            List<string> headList = new List<string>();
            headList.Add("HLD");
            headList.Add("HSN");
            headList.Add("HSW");

            Random random = new Random();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < totalTestCount; i++)
            {
                if (i % 100 == 0)
                {
                    GC.Collect();
                }

                Aspose.Cells.Workbook wb = null;

                if (System.IO.File.Exists(path) == false)
                {
                    wb = new Aspose.Cells.Workbook();
                }
                else
                {
                    wb = new Aspose.Cells.Workbook(path);
                }

                if (wb != null)
                {
                    bool isLicensed = wb.IsLicensed;
                    if (isLicensed == false)
                    {
                        sb.AppendLine("Run Time {0} : isLicensed = false;".FormatWith(i));
                    }

                    if (wb.Worksheets.Count > 1)
                    {
                        sb.AppendLine("Run Time {0} : Worksheets Count = {1};".FormatWith(i, wb.Worksheets.Count));
                    }

                    var ws = wb.Worksheets[0];
                    var cell0 = ws.Cells[i, 0];
                    cell0.Value = i;

                    var cell1 = ws.Cells[i, 1];
                    cell1.Value = "{0}ABC".FormatWith(i);

                    var cell2 = ws.Cells[i, 2];

                    int randomIndex = random.Next(3);
                    cell2.Value = "{0}{1}".FormatWith(headList[randomIndex], random.Next(1000000).ToString().PadLeft(9, '0'));

                    var cell3 = ws.Cells[i, 3];
                    string formula = "=CHOOSE(MATCH(MID(C" + (i + 1).ToString() + ",1,3),{\"HLD\",\"HSN\",\"HSW\"}),\"123HLD\",\"123HSN\",\"123HSW\")";
                    cell3.SetFormula(formula, headList[randomIndex]);

                    var cell4 = ws.Cells[i, 4];
                    cell4.SetFormula(formula, headList[randomIndex]);

                    exportPath = pathTemplate.FormatWith(i + 1);
                    wb.Save(exportPath);
                    wb.Dispose();

                    using (Aspose.Cells.Workbook checkFormula = new Aspose.Cells.Workbook(exportPath))
                    {
                        var checkCell3 = checkFormula.Worksheets[0].Cells[i, 3];
                        var checkCell4 = checkFormula.Worksheets[0].Cells[i, 4];

                        if (checkCell4.Value == null)
                        {
                            sb.AppendLine("Run Time {0} : cell4 value is null, Expect [{1}]".FormatWith(i, headList[randomIndex]));
                        }
                        else if (checkCell4.Value.ToString().EndsWith(headList[randomIndex]) == false)
                        {
                            sb.AppendLine("Run Time {0} : cell4 value [{1}], Expect [{2}]".FormatWith(i, checkCell4.StringValue, headList[randomIndex]));
                        }

                        checkFormula.Dispose();
                    }

                    path = exportPath;
                }
            }

            string errorMsg = sb.ToString();
            return errorMsg;
        }

        #endregion

        /// <summary>
        /// 读取Excel文件(读取步骤自己控制)
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="config">读取Excel配置</param>
        /// <returns>DataSet 结果集</returns>
        public DataSet Excel2DataSetStepByStep(string path, ExcelReaderConfig config = null)
        {
            string copyToTempPath = CopyExcelFileToTempPath(path);

            try
            {
                using (Aspose.Cells.Workbook workBook = new Aspose.Cells.Workbook(copyToTempPath))
                {
                    DataSet result = new DataSet();

                    int sheetCount = workBook.Worksheets.Count; // 工作表总数

                    for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
                    {
                        Worksheet worksheet = workBook.Worksheets[sheetIndex];

                        SheetReadConfig matchReadConfig = null;

                        if (config != null && config.Config != null) // 跳过不读取的Sheet (名称 或 顺序)
                        {
                            matchReadConfig = config.Config.FirstOrDefault(j => j.SheetName == worksheet.Name || (j.SheetIndex.HasValue == true && j.SheetIndex.Value == sheetIndex));
                            if (matchReadConfig == null)
                            {
                                continue;
                            }
                        }

                        DataTable dt = new DataTable();
                        if (worksheet.Cells.Rows.Count <= 0)
                        {
                            result.Tables.Add(dt);
                            continue;
                        }

                        bool isContainColumnHeader = matchReadConfig == null ? true : matchReadConfig.IsContainColumnHeader;

                        #region 表头

                        string columnTemplate = "Column{0}";

                        List<int> columns = new List<int>();

                        int startCellRowIndex = 0;
                        int startCellColumnIndex = 0;
                        int cellColumnCount = worksheet.Cells.MaxDataColumn + 1;

                        if (matchReadConfig != null)
                        {
                            startCellRowIndex = matchReadConfig.StartCellRowIndex;
                            startCellColumnIndex = matchReadConfig.StartCellColumnIndex;

                            cellColumnCount = cellColumnCount - matchReadConfig.StartCellColumnIndex;
                        }

                        if (isContainColumnHeader == false)
                        {
                            for (int i = 0; i < cellColumnCount; i++)
                            {
                                int cellColumnIndex = startCellColumnIndex + i;
                                columns.Add(cellColumnIndex);

                                string columnName = columnTemplate.FormatWith(i + 1);
                                dt.Columns.Add(new DataColumn(columnName));
                            }
                        }
                        else // if (isContainColumnHeader == true)
                        {
                            Row columnHeader = worksheet.Cells.Rows[startCellRowIndex];

                            if (columnHeader == null) { continue; }

                            for (int i = 0; i < cellColumnCount; i++)
                            {
                                int cellColumnIndex = startCellColumnIndex + i;

                                Cell cell = worksheet.Cells[startCellRowIndex, cellColumnIndex];
                                object obj = GetValueType(cell);
                                if (obj == null || obj.ToString() == string.Empty)
                                {
                                    string columnName = columnTemplate.FormatWith(i + 1);
                                    dt.Columns.Add(new DataColumn(columnName));
                                }
                                else
                                {
                                    dt.Columns.Add(new DataColumn(obj.ToString()));
                                }
                                columns.Add(cellColumnIndex);
                            }
                        }

                        #endregion

                        #region 增加Excel行号

                        dt.Columns.Add("ExcelRowNumber", typeof(int));

                        #endregion

                        var maxRowIndex = worksheet.Cells.MaxDataRow; // 读取工作表中最大的行指针 ( 由0开始 )
                        var maxColumnIndex = worksheet.Cells.MaxDataColumn; // 读取工作表中最大的列指针 ( 由0开始 )

                        var rowsCount = maxRowIndex + 1; // 总行数

                        int startRowIndex = 0;

                        if (matchReadConfig != null)
                        {
                            startRowIndex = matchReadConfig.StartCellRowIndex;
                        }

                        if (isContainColumnHeader == true)
                        {
                            startRowIndex = startRowIndex + 1;
                        }

                        #region 数据
                        for (int rowIndex = startRowIndex; rowIndex < rowsCount; rowIndex++)
                        {
                            DataRow dr = dt.NewRow();
                            bool rowHasValue = false;

                            dr["ExcelRowNumber"] = rowIndex + 1; // 计算行号

                            for (int dataColumnIndex = 0; dataColumnIndex < columns.Count; dataColumnIndex++)
                            {
                                int columnIndex = columns[dataColumnIndex];
                                Cell cell = worksheet.Cells[rowIndex, columnIndex];

                                if (cell != null)
                                {
                                    #region 命中Excel读取配置中的条件, 按照读取的配置进行读取
                                    if (matchReadConfig != null && matchReadConfig.CellReadRule != null && matchReadConfig.CellReadRule.ContainsKey(columnIndex))
                                    {
                                        CellType t = CellType.Blank;
                                        if (matchReadConfig.CellReadRule.TryGetValue(columnIndex, out t))
                                        {
                                            try
                                            {
                                                switch (t)
                                                {
                                                    case CellType.String:
                                                        {
                                                            dr[dataColumnIndex] = cell.StringValue;
                                                            if (rowHasValue != true && dr[dataColumnIndex] != null && string.IsNullOrEmpty(dr[dataColumnIndex].ToString()) == false)
                                                            {
                                                                rowHasValue = true;
                                                            }
                                                        }
                                                        break;
                                                    case CellType.Formula:
                                                        {
                                                            dr[dataColumnIndex] = cell.Formula;
                                                            if (rowHasValue != true && dr[dataColumnIndex] != null && string.IsNullOrEmpty(dr[dataColumnIndex].ToString()) == false)
                                                            {
                                                                rowHasValue = true;
                                                            }
                                                        }
                                                        break;
                                                    case CellType.DateTime:
                                                        {
                                                            dr[dataColumnIndex] = cell.DateTimeValue.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                            if (rowHasValue != true && dr[dataColumnIndex].Equals(DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss.fff")) == false)
                                                            {
                                                                rowHasValue = true;
                                                            }
                                                        }
                                                        break;
                                                    case CellType.Blank:
                                                        dr[dataColumnIndex] = string.Empty;
                                                        break;
                                                    default:
                                                        dr[dataColumnIndex] = string.Empty;
                                                        break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                throw new Exception("Aspose - Match ReadConfig.CellReadRule Swicth Throw Exception", ex);
                                            }
                                        }
                                        else // matchReadConfig.CellReadRule.TryGetValue == false
                                        {
                                            dr[dataColumnIndex] = GetValueType(cell);
                                            if (rowHasValue != true && dr[dataColumnIndex] != null && dr[dataColumnIndex].ToString() != string.Empty)
                                            {
                                                rowHasValue = true;
                                            }
                                        }
                                    }
                                    #endregion
                                    else // 没有指定
                                    {
                                        dr[dataColumnIndex] = GetValueType(cell);
                                        if (rowHasValue != true && dr[dataColumnIndex] != null && dr[dataColumnIndex].ToString() != string.Empty)
                                        {
                                            rowHasValue = true;
                                        }
                                    }
                                }
                            }

                            if (rowHasValue) // 遇到空行, 不读取该行数据
                            {
                                dt.Rows.Add(dr);
                            }
                        }

                        #endregion

                        result.Tables.Add(dt);
                    }

                    return result;
                }
            }
            finally
            {
                DeleteCopyExcelFileForFinally(copyToTempPath);
            }
        }

        #region 采用 Aspose 内置 ExportDataTable 方法导出 DataTable

        private static DataTable worksheet2DataTable(Aspose.Cells.Worksheet worksheet, bool exportColumnName = true, bool skipErrorValue = false)
        {
            if (worksheet == null)
            {
                throw new Exception("worksheet2DataTable 发生异常：worksheet 为 null。");
            }

            var maxRowIndex = worksheet.Cells.MaxDataRow; // 读取工作表中最大的行指针 ( 由0开始 )
            var maxColumnIndex = worksheet.Cells.MaxDataColumn; // 读取工作表中最大的列指针 ( 由0开始 )

            var rowsCount = maxRowIndex + 1; // 总行数
            var columnsCount = maxColumnIndex + 1; // 总列数

            //DataTable dt = worksheet.Cells.ExportDataTable 
            //(
            //    firstRow: 0,
            //    firstColumn: 0,
            //    totalRows: rowsCount,
            //    totalColumns: columnsCount,
            //    options: new ExportTableOptions()
            //    {
            //        SkipErrorValue = skipErrorValue,
            //        ExportColumnName = exportColumnName
            //    }
            //);
            // dt.TableName = worksheet.Name;

            // return dt;

            throw new Exception("Aspose.Cells 在 Xamarin.Android 中无方法 worksheet.Cells.ExportDataTable");
        }

        /// <summary>
        /// 读取Excel文件到DataTable
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="sheetIndex">默认读取第 1 个工作表</param>
        /// <param name="exportColumnName">将首行的值设置为DataColumn, 默认设置</param>
        /// <param name="skipErrorValue">忽略转换出现异常的值</param>
        /// <returns></returns>
        public static DataTable Excel2DataTable(string path, int sheetIndex = 0, bool exportColumnName = true, bool skipErrorValue = false)
        {
            // 复制需要导入的文件到 \exe目录\Temp\ExcelFiles， 用于解决文件占用问题
            string copyToTempPath = CopyExcelFileToTempPath(path);

            try
            {
                using (Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(copyToTempPath))
                {
                    Aspose.Cells.Worksheet worksheet = workbook.Worksheets[sheetIndex];
                    return worksheet2DataTable(worksheet: worksheet, exportColumnName: exportColumnName, skipErrorValue: skipErrorValue);
                }
            }
            finally
            {
                DeleteCopyExcelFileForFinally(copyToTempPath);
            }
        }

        /// <summary>
        /// 读取Excel文件到DataSet
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="exportColumnName">将首行的值设置为DataColumn, 默认设置</param>
        /// <param name="skipErrorValue">忽略转换出现异常的值</param>
        /// <returns></returns>
        public static DataSet Excel2DataSet(string path, bool exportColumnName = true, bool skipErrorValue = false)
        {
            // 复制需要导入的文件到 \exe目录\Temp\ExcelFiles， 用于解决文件占用问题
            string copyToTempPath = CopyExcelFileToTempPath(path);

            try
            {
                using (Aspose.Cells.Workbook workbook = new Workbook(copyToTempPath))
                {
                    DataSet ds = new DataSet();
                    for (int sheetCount = 0; sheetCount < workbook.Worksheets.Count; sheetCount++)
                    {
                        Aspose.Cells.Worksheet worksheet = workbook.Worksheets[sheetCount];
                        var dt = worksheet2DataTable(worksheet: worksheet, exportColumnName: exportColumnName, skipErrorValue: skipErrorValue);
                        ds.Tables.Add(dt);
                    }

                    return ds;
                }
            }
            finally
            {
                DeleteCopyExcelFileForFinally(copyToTempPath);
            }
        }

        #endregion

        #region 采用 Aspose 内置 ExportDataTableAsString 方法导出 DataTable

        private static DataTable worksheet2DataTableAsString(Aspose.Cells.Worksheet worksheet, bool exportColumnName = true)
        {
            if (worksheet == null)
            {
                throw new Exception("worksheet2DataTableAsString 发生异常：worksheet 为 null。");
            }

            var maxRowIndex = worksheet.Cells.MaxDataRow; // 读取工作表中最大的行指针 ( 由0开始 )
            var maxColumnIndex = worksheet.Cells.MaxDataColumn; // 读取工作表中最大的列指针 ( 由0开始 )

            var rowsCount = maxRowIndex + 1; // 总行数
            var columnsCount = maxColumnIndex + 1; // 总列数


            //DataTable dt = worksheet.Cells.ExportDataTableAsString // TODO Xamarin.Android - Excel - 报错
            //(
            //    firstRow: 0,
            //    firstColumn: 0,
            //    totalRows: rowsCount,
            //    totalColumns: columnsCount,
            //    exportColumnName: exportColumnName
            //);

            //dt.TableName = worksheet.Name;

            //return dt;

            throw new Exception("Aspose.Cells 在 Xamarin.Android 中无方法 worksheet.Cells.ExportDataTableAsString");
        }

        /// <summary>
        /// 读取Excel文件到DataTable;全部单元格的值读取采用 StringValue
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="sheetIndex">默认读取第 1 个工作表</param>
        /// <param name="exportColumnName">将首行的值设置为DataColumn, 默认设置</param>
        /// <param name="skipErrorValue">忽略转换出现异常的值</param>
        /// <returns></returns>
        public static DataTable Excel2DataTableAsString(string path, int sheetIndex = 0, bool exportColumnName = true)
        {
            // 复制需要导入的文件到 \exe目录\Temp\ExcelFiles， 用于解决文件占用问题
            string copyToTempPath = CopyExcelFileToTempPath(path);

            try
            {
                using (Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(copyToTempPath))
                {
                    Aspose.Cells.Worksheet worksheet = workbook.Worksheets[sheetIndex];
                    return worksheet2DataTableAsString(worksheet: worksheet, exportColumnName: exportColumnName);
                }
            }
            finally
            {
                DeleteCopyExcelFileForFinally(copyToTempPath);
            }
        }

        /// <summary>
        /// 读取Excel文件到DataSet;全部单元格的值读取采用 StringValue
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="exportColumnName">将首行的值设置为DataColumn, 默认设置</param>
        /// <returns></returns>
        public static DataSet Excel2DataSetAsString(string path, bool exportColumnName = true)
        {
            // 复制需要导入的文件到 \exe目录\Temp\ExcelFiles， 用于解决文件占用问题
            string copyToTempPath = CopyExcelFileToTempPath(path);

            try
            {
                using (Aspose.Cells.Workbook workbook = new Workbook(copyToTempPath))
                {
                    DataSet ds = new DataSet();
                    for (int sheetCount = 0; sheetCount < workbook.Worksheets.Count; sheetCount++)
                    {
                        Aspose.Cells.Worksheet worksheet = workbook.Worksheets[sheetCount];
                        var dt = worksheet2DataTableAsString(worksheet: worksheet, exportColumnName: exportColumnName);
                        ds.Tables.Add(dt);
                    }

                    return ds;
                }
            }
            finally
            {
                DeleteCopyExcelFileForFinally(copyToTempPath);
            }
        }

        #endregion

        #region GetValueType

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetValueType(Cell cell)
        {
            if (cell == null)
            {
                return null;
            }

            switch (cell.Type)
            {
                case CellValueType.IsBool:
                    return cell.BoolValue;
                case CellValueType.IsDateTime:
                    return cell.DateTimeValue;
                case CellValueType.IsError:
                    // return cell.IsErrorValue; // 返回 string 值为 "True"
                    return cell.Value; // 返回string 值为 "#N/A"
                case CellValueType.IsNull:
                    return null;
                case CellValueType.IsNumeric:
                    return Util.CommonDal.ReadDecimal(cell.Value);
                case CellValueType.IsString:
                    return cell.StringValue;
                case CellValueType.IsUnknown:
                default:
                    {
                        if (cell.IsFormula == true)
                        {
                            return cell.StringValue; // TODO 读取复杂的公式时出现问题
                        }
                        else
                        {
                            return null;
                        }
                    }
            }
        }

        #endregion

        #region WorkSheet2List

        /// <summary>
        /// 读取工作表信息并将其转换为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Excel文件路径</param>
        /// <param name="objectProps">转换规则</param>
        /// <param name="worksheetIndex">获取工作表Index 默认获取第一张工作表内容( Index = 0 )</param>
        /// <param name="isContainColumnHeader">是否含有列定义行， 默认true</param>
        /// <param name="startCellRowIndex">读取表格起始单元格 的 RowIndex( 默认 0 )</param>
        /// <param name="startCellColumnIndex">读取表格起始单元格 的 ColumnIndex( 默认 0 )</param>
        /// <param name="ignoreRepeatColumnHeaderName">检测到重复的列名时, 不进行报错( 默认false )</param>
        /// <returns></returns>
        public List<T> WorkSheet2List<T>(
            string path,
            List<PropertyColumn> objectProps,
            int worksheetIndex = 0,
            bool isContainColumnHeader = true,
            int startCellRowIndex = 0, int startCellColumnIndex = 0,
            bool ignoreRepeatColumnHeaderName = false
            ) where T : class, new()
        {
            // 复制需要导入的文件到 \exe目录\Temp\ExcelFiles， 用于解决文件占用问题
            string copyToTempPath = CopyExcelFileToTempPath(path);
            try
            {
                Workbook workbook = new Aspose.Cells.Workbook(copyToTempPath);
                Worksheet worksheet = workbook.Worksheets[worksheetIndex]; // 默认取第一张工作表
                var maxRowIndex = worksheet.Cells.MaxDataRow; // 读取工作表中最大的行指针 ( 由0开始 )
                var maxColumnIndex = worksheet.Cells.MaxDataColumn; // 读取工作表中最大的列指针 ( 由0开始 )

                var rowsCount = maxRowIndex + 1; // 总行数
                var columnsCount = maxColumnIndex + 1; // // 总列数

                StringBuilder columnNotExist = new StringBuilder();

                #region Columns

                List<PropertyColumn> header = null;

                // 若已定义 Header Index, 则跳过下面方法
                if (objectProps.Exists(i => i.ColumnIndex < 0) == true) // 获取工作表列头信息
                {
                    header = this.GetHeader
                    (
                        worksheet: worksheet,
                        columnCount: columnsCount,
                        startCellRowIndex: startCellRowIndex,
                        startCellColumnIndex: startCellColumnIndex,
                        ignoreRepeatColumnHeaderName: ignoreRepeatColumnHeaderName
                    );

                    for (int i = 0; i < objectProps.Count; i++)
                    {
                        var matchObjectProps = header.FirstOrDefault(h => h.ExcelColumn == objectProps[i].ExcelColumn);

                        if (matchObjectProps == null)
                        {
                            columnNotExist.AppendLine("找不到列【{0}】;".FormatWith(objectProps[i].ExcelColumn));
                        }
                        else
                        {
                            objectProps[i].ColumnIndex = matchObjectProps.ColumnIndex;
                            objectProps[i].ColumnIndexName = Util.Excel.ExcelCommonMethod.ToExcelColumnName(objectProps[i].ColumnIndex);
                        }
                    }
                }
                else if (isContainColumnHeader == false && objectProps.Exists(i => i.ColumnIndex < 0) == true)
                {
                    columnNotExist.Append("无列定义行，且程序员未指定固定的列位置与对象的属性关联。");
                }

                #endregion

                string errorMsg = columnNotExist.ToString();
                if (errorMsg.IsNullOrEmpty() == false)
                {
                    workbook.Dispose();
                    workbook = null;
                    GC.Collect();
                    throw new Exception(errorMsg);
                }

                List<T> result = new List<T>();

                #region Rows

                Type type = typeof(T);

                // 若 T 包含 ExcelRowNumber属性 (int)，将 Excel行号赋值给 ExcelRowNumber属性
                System.Reflection.PropertyInfo prop_ExcelRowNumber = type.GetProperty("ExcelRowNumber"); // Excel 行号

                // 若 T 包含 ExcelRowErrorInfo (string)，将 Excel 每行转换时遇到的问题赋值到本属性
                System.Reflection.PropertyInfo prop_ExcelRowErrorInfo = type.GetProperty("ExcelRowErrorInfo"); // 记录读取 Excel 的异常信息
                StringBuilder errorInfoStringBuilder = new StringBuilder(); // 每行转换时遇到的问题记录

                // 若为无单头, for ( lieDingYi_RowIndex + 1 == >  for ( lieDingYi_RowIndex + 0
                if (isContainColumnHeader == false)
                {
                    startCellRowIndex = startCellRowIndex - 1;
                }

                for (int rowIndex = startCellRowIndex + 1; rowIndex <= worksheet.Cells.MaxDataRow; rowIndex++)
                {
                    T item = new T();

                    if (prop_ExcelRowNumber != null)
                    {
                        prop_ExcelRowNumber.SetValue(item, rowIndex + 1, null); // RowNumber = 指针 + 1
                    }

                    errorInfoStringBuilder.Clear();

                    foreach (PropertyColumn itemColumn in objectProps)
                    {
                        Cell cell = worksheet.Cells[rowIndex, itemColumn.ColumnIndex];

                        System.Reflection.PropertyInfo propInfo = type.GetProperty(itemColumn.PropertyName);
                        if (propInfo == null) { throw new Exception("返回结果List<{0}>，{0} 不包含有属性【{1}】。".FormatWith(type.Name, itemColumn.PropertyName)); }

                        object value = GetValueType(cell);
                        try
                        {
                            PropertyInfoSetValue(propInfo, item, value);
                        }
                        catch (Exception ex)
                        {
                            if (propInfo.PropertyType.FullName.ToString().IndexOf("System.Nullable") == 0) // 可空属性
                            {
                                propInfo.SetValue(item, null, null);
                            }
                            else
                            {
                                errorInfoStringBuilder.AppendLine("读取【{0}】发生错误（{1}）；".FormatWith(itemColumn.ExcelColumn, ex.Message));
                            }
                        }
                    }

                    string errorInfo = errorInfoStringBuilder.ToString();

                    if (prop_ExcelRowErrorInfo != null && errorInfo.IsNullOrEmpty() == false)
                    {
                        prop_ExcelRowErrorInfo.SetValue(item, errorInfo, null);
                    }

                    result.Add(item);
                }

                #endregion

                workbook.Dispose();
                workbook = null;
                GC.Collect();

                return result;
            }
            finally
            {
                DeleteCopyExcelFileForFinally(copyToTempPath);
            }
        }


        /// <summary>
        /// (异步) 读取工作表信息并将其转换为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionHandler"></param>
        /// <param name="path">Excel文件路径</param>
        /// <param name="objectProps">转换规则</param>
        /// <param name="worksheetIndex">获取工作表Index 默认获取第一张工作表内容( Index = 0 )</param>
        /// <param name="isContainColumnHeader">是否含有列定义行， 默认true</param>
        /// <param name="startCellRowIndex">读取表格起始单元格 的 RowIndex( 默认 0 )</param>
        /// <param name="startCellColumnIndex">读取表格起始单元格 的 ColumnIndex( 默认 0 )</param>
        /// <param name="ignoreRepeatColumnHeaderName">检测到重复的列名时, 不进行报错( 默认false )</param>
        /// <returns></returns>
        public void WorkSheet2ListAsync<T>(
            Action<Task<List<T>>> actionHandler,
            string path,
            List<PropertyColumn> objectProps,
            int worksheetIndex = 0,
            bool isContainColumnHeader = true,
            int startCellRowIndex = 0, int startCellColumnIndex = 0,
            bool ignoreRepeatColumnHeaderName = false
            ) where T : class, new()
        {
            Task<List<T>> mTask = new System.Threading.Tasks.Task<List<T>>(() => WorkSheet2List<T>
                    (
                        path: path,
                        objectProps: objectProps,
                        worksheetIndex: worksheetIndex,
                        isContainColumnHeader: isContainColumnHeader,
                        startCellRowIndex: startCellRowIndex,
                        startCellColumnIndex: startCellColumnIndex,
                        ignoreRepeatColumnHeaderName: ignoreRepeatColumnHeaderName
                    )
                );

            mTask.ContinueWith((task) => actionHandler(task));
            mTask.Start();

            #region (UI代码参考)下载完毕 Handler 可以参考以下代码

            //public void DownloadFileByHttpRequestAynsc_Handler(System.Threading.Tasks.Task task)
            //{
            //    string msg = "IsCanceled={0}\nIsCompleted={1}\nIsFaulted={2};"
            //    .FormatWith
            //    (
            //        task.IsCanceled,  // 因被取消而完成
            //        task.IsCompleted, // 成功完成
            //        task.IsFaulted    // 因发生异常而完成
            //    );

            //    Console.WriteLine(msg);

            //    if (task.IsFaulted == true)
            //    {
            //        if (task.Exception != null)
            //        {
            //            Console.WriteLine(task.IsFaulted);
            //            MessageBox.Show(task.Exception.GetFullInfo());
            //        }
            //        else
            //        {
            //            MessageBox.Show("下载失败。");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("下载完成。");
            //    }
            //}

            #endregion
        }

        private void PropertyInfoSetValue<T>(PropertyInfo propInfo, T item, object value, object[] index = null) where T : class, new()
        {
            // propInfo 如果 是某些特殊类型 要进行什么处理?
            string propFullName = propInfo.PropertyType.FullName.ToString();
            if (propFullName.StartsWith("System.String") == true)
            {
                if (value == null)
                {
                    propInfo.SetValue(item, null, index);
                }
                else
                {
                    string v = Convert.ToString(value);
                    // 经过思考, 作为一个通用的DLL, 这里不应该随意更改读取出来的信息, 含有空格若需要处理, 请在各自具体的项目进行去掉, 故注释以下代码
                    // v = v.Trim(); 
                    propInfo.SetValue(item, v, index);
                }
            }
            // 若是 TimeSpan, 会输出 1899 12 31 的某个时间, 在业务逻辑中再进行处理D
            //else if (propFullName.StartsWith("System.TimeSpan") == true || (propFullName.StartsWith("System.Nullable") && propFullName.Contains("System.TimeSpan")))
            //{

            //}
            else
            {
                try
                {
                    propInfo.SetValue(item, value, index);
                }
                catch (System.ArgumentException argEx)
                {
                    #region (弃用) 配对类型 改用正则表达式配对的方式

                    //int convertToXIndex = argEx.Message.LastIndexOf("System.");

                    //string convertToX = argEx.Message.Substring(convertToXIndex);
                    //convertToX = convertToX
                    //    .Replace(',', ' ')
                    //    .Replace('"', ' ')
                    //    .Replace('”', ' ')
                    //    .Replace('。', ' ')
                    //    ;

                    //convertToX = convertToX.TrimAdv();

                    #endregion

                    // 需要慢慢完善
                    // 处理Excel文本格式无法 setValue的问题
                    // 报错信息 "类型“System.String”的对象无法转换为类型“System.Decimal”。"
                    var matchCollection = System.Text.RegularExpressions.Regex.Matches(input: argEx.Message, pattern: "System.[A-Za-z0-9.]{1,}");
                    if (matchCollection.Count <= 0) { throw argEx; } // 匹配信息失败, 抛出捕获的异常

                    int matchLastIndex = matchCollection.Count - 1;
                    string convertToX = argEx.Message.Substring(matchCollection[matchLastIndex].Index, matchCollection[matchLastIndex].Length);

                    switch (convertToX.ToUpper())
                    {
                        case "SYSTEM.INT32":
                            {
                                int valueAfterConvert = Convert.ToInt32(value);
                                propInfo.SetValue(item, valueAfterConvert, index);
                            }
                            break;
                        case "SYSTEM.FLOAT":
                            {
                                float valueAfterConvert = Convert.ToSingle(value);
                                propInfo.SetValue(item, valueAfterConvert, index);
                            }
                            break;
                        case "SYSTEM.DOUBLE":
                            {
                                double valueAfterConvert = Convert.ToDouble(value);
                                propInfo.SetValue(item, valueAfterConvert, index);
                            }
                            break;
                        case "SYSTEM.DECIMAL":
                            {
                                decimal valueAfterConvert = Convert.ToDecimal(value);
                                propInfo.SetValue(item, valueAfterConvert, index);
                            }
                            break;
                        default:
                            throw argEx;
                    }
                }
            }
        }

        /// <summary>
        /// 获取工作表列头信息
        /// </summary>
        /// <param name="worksheet">工作表</param>
        /// <param name="columnCount">列个数</param>
        /// <param name="startCellRowIndex">起始读取位置RowIndex，默认 0 （第一行）</param>
        /// <param name="startCellColumnIndex">起始读取位置ColumnIndex，默认 0 （第一列）</param>
        /// <returns></returns>
        public List<PropertyColumn> GetHeader(Worksheet worksheet, int columnCount, int startCellRowIndex = 0, int startCellColumnIndex = 0, bool ignoreRepeatColumnHeaderName = false)
        {
            string errorMsg = string.Empty;

            try
            {
                List<PropertyColumn> result = new List<PropertyColumn>();

                object[,] object2DArray = worksheet.Cells.ExportArray
                (
                    firstRow: startCellRowIndex, // 默认是第一行 ( index = 0 )
                    firstColumn: startCellColumnIndex, // 默认是第一列 ( index = 0 )
                    totalRows: 1, // 列定义通常是 1 行数据, 这里只取位于 firstRow, firstColumn 的一行
                    totalColumns: columnCount - startCellColumnIndex // 列个数
                );

                for (int i = 0; i < object2DArray.Length; i++)
                {
                    PropertyColumn toAdd = new PropertyColumn()
                    {
                        ColumnIndex = startCellColumnIndex + i,
                        ExcelColumn = Util.CommonDal.ReadString(object2DArray[0, i])
                    };

                    result.Add(toAdd);
                }

                var query_group_by = (from i in result
                                      group i by new { i.ExcelColumn } into ix
                                      select new
                                      {
                                          ExcelColumn = ix.Key.ExcelColumn,
                                          OccurTimes = ix.Count()
                                      }).ToList();


                foreach (var item in query_group_by.Where(i => i.OccurTimes > 1))
                {
                    if (errorMsg.IsNullOrEmpty() == false)
                    {
                        errorMsg += "\r\n";
                    }

                    errorMsg += item.ExcelColumn + ";";
                }

                if (errorMsg.IsNullOrEmpty() == false)
                {
                    if (ignoreRepeatColumnHeaderName == false)
                    {
                        throw new Exception("列定义出现重复。\r\n" + errorMsg);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                if (errorMsg.IsNullOrEmpty() == false)
                {
                    throw new Exception("列定义出现重复。\r\n" + errorMsg);
                }
                else
                {
                    throw new Exception("获取工作表列头信息发生了错误。（ExcelUtils_Aspose GetHeader）\r\n", ex);
                }
            }

        }

        #endregion

        /// <summary>
        /// 将 DataSet 转换为 Excel文档
        /// </summary>
        /// <param name="path">生成Excel文档存放路径</param>
        /// <param name="dataSet">DataSet</param>
        /// <param name="showColumnNameArray">Sheet首行内容采用DataTable的ColumnName</param>
        /// <param name="positionArray">输出到工作簿的位置(rowIndex, columnIndex)</param>
        public void DataSet2Excel(string path, DataSet dataSet, bool[] showColumnNameArray = null, int[,] positionArray = null)
        {
            FileFormatType fileFormatType = FileFormatType.Xlsx;

            if (path.EndsWith("xlsx", StringComparison.InvariantCultureIgnoreCase))
            {
                fileFormatType = FileFormatType.Xlsx;
            }
            else if (path.EndsWith("xls", StringComparison.InvariantCultureIgnoreCase))
            {
                fileFormatType = FileFormatType.Xlsx;
            }
            else
            {
                throw new Exception("后缀名必须是.xlsx或.xls"); // TODO 补充到其他方法
            }

            using (Workbook workbook = new Workbook(fileFormatType: fileFormatType))
            {
                for (int index = 0; index < dataSet.Tables.Count; index++)
                {
                    if (workbook.Worksheets.Count - 1 < index)
                    {
                        workbook.Worksheets.Add();
                    }

                    Worksheet workSheet = workbook.Worksheets[index];

                    DataTable dt = dataSet.Tables[index];
                    if (dt.TableName.IsNullOrEmpty() == true)
                    {
                        workSheet.Name = "Sheet{0}".FormatWith(index + 1);
                    }
                    else
                    {
                        workSheet.Name = dt.TableName;
                    }

                    bool isFieldNameShown = true;
                    if (showColumnNameArray != null)
                    {
                        isFieldNameShown = showColumnNameArray[index];
                    }

                    int tmpFirstRowIndex = 0;
                    int tmpFirstColumnIndex = 0;
                    if (positionArray != null)
                    {
                        tmpFirstRowIndex = positionArray[index, 0];
                        tmpFirstColumnIndex = positionArray[index, 1];
                    }                    

                    workSheet.Cells.ImportTwoDimensionArray
                    (
                        objArray: dt.ToString2DArray(isContainColumnName: isFieldNameShown),
                        firstRow: tmpFirstRowIndex,
                        firstColumn: tmpFirstColumnIndex
                    );
                }

                workbook.Save(path);
            }
        }

        public void DataSet2ExcelStepByStep(string path, DataSet dataSet, bool[] showColumnNameArray = null, int[,] positionArray = null)
        {
            FileFormatType fileFormatType = FileFormatType.Xlsx;

            if (path.EndsWith("xlsx", StringComparison.InvariantCultureIgnoreCase))
            {
                fileFormatType = FileFormatType.Xlsx;
            }
            else if (path.EndsWith("xls", StringComparison.InvariantCultureIgnoreCase))
            {
                fileFormatType = FileFormatType.Xlsx;
            }
            else
            {
                throw new Exception("后缀名必须是.xlsx或.xls"); // TODO 补充到其他方法
            }

            using (Workbook workbook = new Workbook(fileFormatType: fileFormatType))
            {
                for (int index = 0; index < dataSet.Tables.Count; index++)
                {
                    if (workbook.Worksheets.Count - 1 < index)
                    {
                        workbook.Worksheets.Add();
                    }

                    Worksheet workSheet = workbook.Worksheets[index];

                    DataTable dt = dataSet.Tables[index];
                    if (dt.TableName.IsNullOrEmpty() == true)
                    {
                        workSheet.Name = "Sheet{0}".FormatWith(index + 1);
                    }
                    else
                    {
                        workSheet.Name = dt.TableName;
                    }

                    bool isFieldNameShown = true;
                    if (showColumnNameArray != null)
                    {
                        isFieldNameShown = showColumnNameArray[index];
                    }

                    int tmpFirstRowIndex = 0;
                    int tmpFirstColumnIndex = 0;
                    if (positionArray != null)
                    {
                        tmpFirstRowIndex = positionArray[index, 0];
                        tmpFirstColumnIndex = positionArray[index, 1];
                    }

                    int currentRowIndex = tmpFirstRowIndex;

                    #region 表头

                    if (isFieldNameShown == true)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            workSheet.Cells[tmpFirstRowIndex, tmpFirstColumnIndex + i].Value = dt.Columns[i].ColumnName;
                        }

                        currentRowIndex = currentRowIndex + 1;
                    }

                    #endregion

                    #region 数据

                    for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                    {
                        for (int columnIndex = 0; columnIndex < dt.Columns.Count; columnIndex++)
                        {
                            workSheet.Cells[currentRowIndex, tmpFirstColumnIndex + columnIndex].Value = dt.Rows[rowIndex][columnIndex];
                        }

                        currentRowIndex = currentRowIndex + 1;
                    }

                    #endregion
                }

                workbook.Save(path);
            }
        }

        ///// <summary>
        ///// 打印DEMO
        ///// Aspose.Cell 原理输出为Image, 打印Image
        ///// </summary>
        //public static void PrintDemo(string filePath)
        //{
        //    Workbook workbook = new Workbook(filePath);

        //    //Get the worksheet to be printed
        //    Worksheet worksheet = workbook.Worksheets[0];

        //    PageSetup pageSetup = worksheet.PageSetup;
        //    pageSetup.Orientation = PageOrientationType.Landscape;
        //    //pageSetup.LeftMargin = 0;
        //    //pageSetup.RightMargin = 0.1;
        //    //pageSetup.BottomMargin = 0.3;
        //    //pageSetup.PrintArea = "A2:J29"; // 打印区域
        //    //Apply different Image / Print options.

        //    Aspose.Cells.Rendering.ImageOrPrintOptions options = new Aspose.Cells.Rendering.ImageOrPrintOptions();

        //    //Set the Printing page property
        //    //options.PrintingPage = PrintingPageType.IgnoreStyle;
        //    //Render the worksheet

        //    Aspose.Cells.Rendering.SheetRender sr = new Aspose.Cells.Rendering.SheetRender(worksheet, options);

        //    System.Drawing.Image map = sr.ToImage(0); // 核心 -- 将 SheetRendar转为 Image 进行打印
        //                                              // map.Save(@"D:\HoweDesktop\A{0}".FormatWith(DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")));

        //    // send to printer 核心打印 Image
        //    System.Drawing.Printing.PrinterSettings printSettings = new System.Drawing.Printing.PrinterSettings();
        //    string strPrinterName = printSettings.PrinterName;
        //    sr.ToPrinter(strPrinterName);
        //}





        /// <summary>
        /// 复制需要导入的文件到 \exe目录\Temp\ExcelFiles， 用于解决文件占用问题
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string CopyExcelFileToTempPath(string path)
        {
            string copyToTempDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp", "ExcelFiles");
            if (System.IO.Directory.Exists(copyToTempDirectory) == false)
            {
                System.IO.Directory.CreateDirectory(copyToTempDirectory);
            }

            string copyToTempPath = System.IO.Path.Combine(copyToTempDirectory, "{0}.{1}".FormatWith(Guid.NewGuid().ToString(), path.Substring(path.LastIndexOf(".") + 1)));
            System.IO.File.Copy(path, copyToTempPath, true);

            return copyToTempPath;
        }

        /// <summary>
        /// <para>删除 为解决占用问题而复制的 Excel文件，并进行一次GC.Collect()</para>
        /// <para>请在 finally 代码块中调用</para>
        /// </summary>
        /// <param name="copyToTempPath">待删除的Excel文件</param>
        private static void DeleteCopyExcelFileForFinally(string copyToTempPath)
        {
            // 删除 copyToTempPath 文件
            if (System.IO.File.Exists(copyToTempPath))
            {
                System.IO.File.Delete(copyToTempPath);
            }

            GC.Collect();
        }
    }

}
