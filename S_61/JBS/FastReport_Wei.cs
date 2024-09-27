using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using S_61.Basic;
using FastReport.Export.OoXML;
using System.Windows.Forms;
using System.Drawing;

namespace JBS
{
    class FastReport_Wei : IDisposable
    {
        #region 建構解構

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                Paramaters.Clear();
                dy.Clear();
            }

            // Free any unmanaged objects here.
            //

            disposed = true;
        }

        ~FastReport_Wei()
        {
            Dispose(false);
        }
        #endregion
        public List<string> Paramaters = new List<string>();
        public Dictionary<string, object> dy = new Dictionary<string, object>();

        public FastReport_Wei()
        {
            dy.Add("date", Common.User_DateTime == 1 ? "民國" : "西元");
            
            dy.Add("備註說明", Common.Sys_MemoUdf);
            if (pVar.TRS != "") pVar.ShowTRS = true;
            dy.Add("顯示千分位", pVar.ShowTRS);
            dy.Add("千分位", pVar.TRS);

            dy.Add("銷貨單價小數", Common.MS.ToString().Trim());
            dy.Add("銷貨單據小數", Common.MST.ToString().Trim());
            dy.Add("銷項稅額小數", Common.TS.ToString().Trim());

            dy.Add("本幣金額小數", Common.M.ToString().Trim());
            dy.Add("庫存數量小數", Common.Q.ToString().Trim());//庫存數量小數

            dy.Add("是否顯示金額", Common.User_SalePrice);

            dy.Add("進貨單價小數", Common.MF.ToString().Trim());
            dy.Add("進貨單據小數", Common.MFT.ToString().Trim());
            dy.Add("進項稅額小數", Common.TF.ToString().Trim());
            dy.Add("銷項金額小數", Common.TPS.ToString().Trim());
            dy.Add("進項金額小數", Common.TPF.ToString().Trim());
            dy.Add("製表日期", Date.GetDateTime(Common.User_DateTime,true));
        }

        void SetParamaters(FastReport.Report fs)
        {
            for (int i = 0; i < dy.Count; i++)
            {
                var p = fs.GetParameter(dy.ElementAt(i).Key);
                if (p != null)
                {
                    p.Value = dy.ElementAt(i).Value;
                }
            }
            for (int i = 0; i < Paramaters.Count; i += 2)
            {
                fs.SetParameterValue(Paramaters[i].ToString(), Paramaters[i + 1]);
            }
        }

        public void PreView(string path, DataTable dt1, string dtName, DataTable dt2 = null, string dt2Name = null, RptMode mode = RptMode.PreView, string ReportFileName = "")
        {
            try
            {
                using (FastReport.Report fs = new FastReport.Report())
                {
                    fs.Clear();
                    fs.Load(path);
                    fs.RegisterData(dt1, dtName);
                    if (dt2 != null)
                    {
                        fs.RegisterData(dt2, dt2Name);
                    }

                    SetParamaters(fs);

                    /*
                    FastReport.Watermark mark = new FastReport.Watermark();
                    mark.Enabled = true;
                    mark.TextRotation = FastReport.WatermarkTextRotation.BackwardDiagonal;
                    mark.Font = new Font(mark.Font.FontFamily, (float)70, FontStyle.Bold);
                    string text = Common.User_Name + Environment.NewLine + Date.GetDateTime(2, true) + "印製" + Environment.NewLine;//設置文字內容 
                    mark.Text = text + text + text + text + text;
                    FastReport.ReportPage pageRecept = (FastReport.ReportPage)fs.Pages[0];//設定要印的頁數
                    pageRecept.Watermark = mark;
                     */

                    //fs.PrintSettings.Printer = "123";可設定印表機

                    if (mode == RptMode.Print)
                        fs.Print();
                    else if (mode == RptMode.PreView)
                        fs.Show();
                    else if (mode == RptMode.Excel)
                    {
                        fs.Prepare();
                        FastReport.Export.OoXML.Excel2007Export export = new Excel2007Export();
                        if (export.ShowDialog())
                            fs.Export(export, Application.StartupPath + "\\temp\\" + ReportFileName + GetDateTime() + ".xlsx");
                    }
                    else if (mode == RptMode.Word)
                    {
                        fs.Prepare();
                        FastReport.Export.OoXML.Word2007Export export = new Word2007Export();
                        if (export.ShowDialog())
                        {
                            fs.Export(export, Application.StartupPath + "\\temp\\" + ReportFileName + GetDateTime() + ".docx");
                        }
                    }
                    else if (mode == RptMode.Design)
                    {
                        fs.Design();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public void PreViewEINV(string path, DataTable dt1, string dtName, RptMode mode = RptMode.PreView, string ReportFileName = "")
        {
            using (FastReport.Report fs = new FastReport.Report())
            {
                fs.Clear();
                fs.Load(path);
                fs.RegisterData(dt1, dtName);
                SetParamaters(fs);

                if (mode == RptMode.Print)
                    fs.Print();
                else if (mode == RptMode.PreView)
                    fs.Show();
                else if (mode == RptMode.Excel)
                {
                    fs.Prepare();
                    FastReport.Export.OoXML.Excel2007Export export = new Excel2007Export();
                    if (export.ShowDialog())
                        fs.Export(export, Application.StartupPath + "\\temp\\" + ReportFileName + GetDateTime() + ".xlsx");
                }
                else if (mode == RptMode.Word)
                {
                    fs.Prepare();
                    FastReport.Export.OoXML.Word2007Export export = new Word2007Export();
                    if (export.ShowDialog())
                        fs.Export(export, Application.StartupPath + "\\temp\\" + ReportFileName + GetDateTime() + ".docx");
                }
            }
        }

        private string GetDateTime()
        {

            System.Globalization.TaiwanCalendar tw = new System.Globalization.TaiwanCalendar();
            switch (Common.User_DateTime)
            {
                case 1:
                    return "_" + tw.GetYear(DateTime.Now) + DateTime.Now.ToString("MMdd");
                case 2:
                    return "_" + tw.GetYear(DateTime.Now) + 1911 + DateTime.Now.ToString("MMdd");

            }
            return "";
        }



    }
}
