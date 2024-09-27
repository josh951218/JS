using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace JBS
{
    class ResloveCrystalReportExcel
    {
        bool IncludeDetailHeadRow , IncludeDetailTailRow ;
        List<int> 明細ColumnIndexList = new List<int>();

        public ResloveCrystalReportExcel()
        {

        }

        public ResloveCrystalReportExcel(string FilePath, string ExportPath, string 明細區_頭value, string 明細區_尾value, string 明細區_欄位對應value, bool IncludeDetailHeadRow_, bool IncludeDetailTailRow_)
        {
            IncludeDetailHeadRow = IncludeDetailHeadRow_;
            IncludeDetailTailRow = IncludeDetailTailRow_;
            using (DataTable dt = new DataTable())
            {
                LoadExcel(FilePath, dt);
                ResolveExcelDt(dt, 明細區_頭value, 明細區_尾value, 明細區_欄位對應value);
                ExportExcelDt(dt, ExportPath,false,false);
            }
        }

        public void ExportExcelDt(DataTable dt, string ExportPath, bool 包含ColumnName = false, bool 包含Column1 = true)
        {
            Stream stream = RenderDataTableToExcel(dt, 包含ColumnName, 包含Column1);
            using (FileStream file = new FileStream(ExportPath , FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                file.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            dt.Dispose();
        }
        /// <summary>
        /// 不包含第一格COLUMN
        /// </summary>
        /// <param name="srcTable"></param>
        /// <param name="包含ColumnName"></param>
        /// <returns></returns>
        private Stream RenderDataTableToExcel(DataTable srcTable,bool 包含ColumnName = false,bool 包含column1 = false)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
            int rowIndex = 0;
            if (!包含column1)
            {
                if (包含ColumnName)
                {
                    #region 加入dt.表頭之資料
                    int ColumnCurrent = 1;
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in srcTable.Columns)
                    {
                        if (ColumnCurrent != 1)
                        {
                            dataRow.CreateCell(column.Ordinal - 1).SetCellValue(column.ToString());
                        }
                        ColumnCurrent++;
                    }
                    rowIndex++;
                    #endregion
                }
                #region 加入dt.Row之資料
                foreach (DataRow row in srcTable.Rows)
                {
                        int ColumnCurrent = 1;
                        HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in srcTable.Columns)
                        {
                            if (ColumnCurrent != 1)
                            {
                                dataRow.CreateCell(column.Ordinal - 1).SetCellValue(row[column].ToString());
                            }
                            ColumnCurrent++;
                        }
                    rowIndex++;
                }
                #endregion
            }
            else
            {
                if (包含ColumnName)
                {
                    #region 加入dt.表頭之資料
                    int ColumnCurrent = 1;
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in srcTable.Columns)
                    {
                        //if (ColumnCurrent != 1)
                        //{
                        dataRow.CreateCell(column.Ordinal).SetCellValue(column.ToString());
                        //}
                        ColumnCurrent++;
                    }
                    rowIndex++;
                    #endregion
                }
                #region 加入dt.Row之資料
                foreach (DataRow row in srcTable.Rows)
                {
                    int ColumnCurrent = 1;
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in srcTable.Columns)
                    {
                        //if (ColumnCurrent != 1)
                        //{
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        //}
                        ColumnCurrent++;
                    }
                    rowIndex++;
                }
                #endregion
            }
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            stream.Flush();
            stream.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;
            return stream;
        }


        ///// <summary>
        ///// 包含第一格COLUMN
        ///// </summary>
        ///// <param name="srcTable"></param>
        ///// <returns></returns>
        //private Stream RenderDataTableToExcel_1(DataTable srcTable, bool 包含ColumnName = false)
        //{
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
        //    HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
        //    // handling header.
        //    //foreach (DataColumn column in srcTable.Columns)
        //    //    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
        //    // handling value.
        //    int rowIndex = 0;
        //    if (包含ColumnName)
        //    {
        //        #region 加入dt.表頭之資料
        //        int ColumnCurrent = 1;
        //        HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
        //        foreach (DataColumn column in srcTable.Columns)
        //        {
        //            //if (ColumnCurrent != 1)
        //            //{
        //                dataRow.CreateCell(column.Ordinal).SetCellValue(column.ToString());
        //            //}
        //            ColumnCurrent++;
        //        }
        //        rowIndex++;
        //        #endregion
        //    }
        //    #region 加入dt.Row之資料
        //    foreach (DataRow row in srcTable.Rows)
        //    {
        //        int ColumnCurrent = 1;
        //        HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
        //        foreach (DataColumn column in srcTable.Columns)
        //        {
        //            //if (ColumnCurrent != 1)
        //            //{
        //                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
        //            //}
        //            ColumnCurrent++;
        //        }
        //        rowIndex++;
        //    }
        //    #endregion
        //    MemoryStream stream = new MemoryStream();
        //    workbook.Write(stream);
        //    stream.Flush();
        //    stream.Position = 0;

        //    sheet = null;
        //    headerRow = null;
        //    workbook = null;
        //    return stream;
        //}

        private void ResolveExcelDt(DataTable dt, string 明細區_頭value, string 明細區_尾value, string 明細區_欄位對應value)
        {
            try
            {
                DataTable TempDt = dt.Copy();
                清除EmptyRow(TempDt);
                dt.Clear();
                #region get 明細ColumnIndexList
                明細ColumnIndexList.Clear();
                int 明細欄位對應Row = -1;
                for (int i = 0; i < TempDt.Rows.Count; i++)
                {
                    if (TempDt.Rows[i][1].ToString().Trim() == 明細區_欄位對應value)
                    {
                        明細欄位對應Row = i;
                        break;
                    }
                }
                if (明細欄位對應Row != -1)
                {
                    for (int i = 0; i < TempDt.Columns.Count; i++)
                    {
                        if (TempDt.Rows[明細欄位對應Row][i].ToString().Trim() != "")
                        {
                            明細ColumnIndexList.Add(i);
                        }
                    }
                }
                #endregion
                for (int i = 0; i < TempDt.Rows.Count; i++)
                {
                    #region 明細區  first
                    if (TempDt.Rows[i][1].ToString().Trim() == 明細區_頭value)
                    {
                        i = 加入明細區(dt, TempDt, 明細區_頭value, 明細區_尾value, i);
                        continue;
                    }
                    #endregion
                    #region 非明細區  second
                    加入非明細區(dt, TempDt, i);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 加入非明細區(DataTable dt, DataTable TempDt, int i)
        {
            int 非明細區CunnentAddCount = 0;
            bool HaveBeenAddRow = false;
            for (int j = 0; j < TempDt.Columns.Count; j++)
            {
                if (TempDt.Rows[i][j].ToString().Trim() != "")
                {
                    if (HaveBeenAddRow == false)
                    {
                        NewEmptyRow(dt);
                        HaveBeenAddRow = true;
                    }
                    dt.Rows[dt.Rows.Count - 1][非明細區CunnentAddCount] = TempDt.Rows[i][j].ToString().Trim();
                    非明細區CunnentAddCount++;
                }
            }
        }

        private void 清除EmptyRow(DataTable TempDt)
        {
            int i = TempDt.Rows.Count - 1;
            while (i >= 0)
            {
                bool EmptyRow = true;
                for (int j = 1; j < TempDt.Columns.Count; j++)
                {
                    if (TempDt.Rows[i][j].ToString().Trim() != "")
                    {
                        EmptyRow = false;
                        continue;
                    }
                }
                if (EmptyRow)
                    TempDt.Rows.RemoveAt(i);
                i--;
            }
        }

        private void NewEmptyRow(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
        }

        private int 加入明細區(DataTable dt, DataTable TempDt, string 明細區_頭value, string 明細區_尾value, int 明細區PrefirstRowIndex)
        {
            object lock_ = new object();
            int 明細區LeastRowIndex = -1; // initial
            int 明細區firstRowIndex = 明細區PrefirstRowIndex + 1;

            #region 找出明細頭、尾之index
            //找出明細頭
            if (IncludeDetailHeadRow)
            {
                明細區firstRowIndex = 明細區PrefirstRowIndex;
            }
            else
            {
                加入非明細區(dt, TempDt, 明細區PrefirstRowIndex);
            }
            //找出明細尾
            for (int i = 明細區firstRowIndex; i < TempDt.Rows.Count; i++)
            {
                if (明細區LeastRowIndex != -1) break;
                for (int k = 0; k < TempDt.Columns.Count - 1; k++)
                {
                    var a = TempDt.Rows[i][k].ToString().Trim();
                    if (TempDt.Rows[i][k].ToString().Trim() == 明細區_尾value)
                    {
                        if (IncludeDetailTailRow)
                            明細區LeastRowIndex = i;
                        else
                            明細區LeastRowIndex = i - 1;
                        break;
                    }
                }
            }

            #endregion
            #region 對應_明細ColumnIndexList_填入Row
            for (int i = 明細區firstRowIndex; i <= 明細區LeastRowIndex; i++)
            {
                int 明細區CunnentAddCount = 0;
                bool HaveBeenAddRow = false;
                for (int j = 0; j < 明細ColumnIndexList.Count; j++)
                {
                    int 明細ColumnIndex = 明細ColumnIndexList[j];
                    if (TempDt.Rows[i][明細ColumnIndex].ToString().Trim() != "")
                    {
                        #region add Row
                        if (HaveBeenAddRow == false)
                        {
                            NewEmptyRow(dt);
                            HaveBeenAddRow = true;
                        }
                        #endregion
                        dt.Rows[dt.Rows.Count - 1][j] = TempDt.Rows[i][明細ColumnIndex].ToString().Trim();
                        明細區CunnentAddCount++;
                    }
                }
            }
            #endregion
            return 明細區LeastRowIndex;
        }

        private void LoadExcel(string FilePath, DataTable dt)
        {
            if (File.Exists(FilePath))
            {
                using (FileStream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = WorkbookFactory.Create(stream);//Can Read 2003 or 2007 Excel File
                    ISheet sheet = workbook.GetSheetAt(0);//get sheet 0
                    #region AddColumn
                    IRow headerRow = (IRow)sheet.GetRow(0);
                    int ColumnCount = headerRow.LastCellNum;
                    DataColumn column_ = new DataColumn("Excel筆數");
                    dt.Columns.Add(column_);
                    for (int i = headerRow.FirstCellNum; i < ColumnCount; i++)
                    {
                        //string ColumnName = headerRow.GetCell(i) == null ? "" : headerRow.GetCell(i).ToString().Trim();
                        //if (ColumnName == "") continue;
                        //DataColumn column = new DataColumn(ColumnName);
                        //dt.Columns.Add(column);
                        DataColumn column = new DataColumn(i.ToString());
                        dt.Columns.Add(column);
                    }
                    #endregion
                    #region AddRow
                    for (int j = 0; j <= sheet.LastRowNum; j++)
                    {
                        try
                        {
                            headerRow = (IRow)sheet.GetRow(j);
                            DataRow dr = dt.NewRow();
                            dr[0] = j.ToString();
                            for (int i = 0; i <= headerRow.LastCellNum; i++)//Column
                            {
                                if (i >= dt.Columns.Count - 1)
                                    break;//防止超過 dt's columns 會產生錯誤
                                string ExcelCellValue = headerRow.GetCell(i) == null ? "" : headerRow.GetCell(i).ToString().Trim();
                                dr[i + 1] = ExcelCellValue;
                            }
                            dt.Rows.Add(dr);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    #endregion
                }
            }
            else
            {
                MessageBox.Show("找不到該路徑檔案:" + FilePath);
            }
        }
    }
}
