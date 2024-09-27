using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace S_61.Basic
{
    public class RPT
    {
        DataTable dt;
        DataSet ds;
        string path = "";

        public List<string[]> lobj = new List<string[]>();
        public List<string[]> lval = new List<string[]>();
        public string txtstart = "";
        public string txtend = "";
        public string txtadress = "";
        public string txttel = "";
        public string office = "";
        public string 列印地址 = "";
        public short PCS = 1;

        public RPT() { }




        public RPT(DataTable d, String p)
        {
            try
            {
                dt = d.Copy();
                path = p;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //DataTable OR DataTable + subReport
        public void PreView(string name = "", DataTable subTable = null)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dt.Rows.Count == 0)
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DTInit(name, subTable);
                    Common.FrmReport.ShowDialog();

                    if (dt != null)
                        dt.Dispose();

                    if (subTable != null)
                        subTable.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public void Print(string name = "", DataTable subTable = null)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dt.Rows.Count == 0)
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DTInit(name, subTable);
                    Common.FrmReport.button1_Click(null, null);

                    if (dt != null)
                        dt.Dispose();

                    if (subTable != null)
                        subTable.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public void Word(string name = "", DataTable subTable = null)
        {
            if (dt.Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DTInit(name, subTable);
                Common.FrmReport.word("");

                if (dt != null)
                    dt.Dispose();

                if (subTable != null)
                    subTable.Dispose();

            }
        }

        //public void Excel(string name = "", DataTable subTable = null)
        //{
        //    if (dt.Rows.Count == 0)
        //        MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    else
        //    {
        //        DTInit(name, subTable);
        //        Common.FrmReport.excel("");

        //        if (dt != null)
        //            dt.Dispose();

        //        if (subTable != null)
        //            subTable.Dispose();

        //    }
        //}

        public void Excel(string name = "", DataTable subTable = null, bool 是否解析Excel = false, string 明細區_頭value = "", string 明細區_尾value = "", string 明細區_欄位對應value = "", bool IncludeDetailHeadRow_ = true, bool IncludeDetailTailRow_ = true)
        {
            if (dt.Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DTInit(name, subTable);
                Common.FrmReport.excel("", 是否解析Excel, 明細區_頭value, 明細區_尾value, 明細區_欄位對應value, IncludeDetailHeadRow_, IncludeDetailTailRow_);

                if (dt != null)
                    dt.Dispose();

                if (subTable != null)
                    subTable.Dispose();

            }
        }

        public void Mail(string pdfname, string name = "", DataTable subTable = null)
        {
            if (dt.Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                this.DTInit(name, subTable);
                Common.FrmReport.Mail(pdfname);

                if (dt != null)
                    dt.Dispose();

                if (subTable != null)
                    subTable.Dispose();

            }
        }

        void DTInit(string name = "", DataTable subTable = null)
        {
            try
            {
                Common.FrmReport = new Report.Frmreport();
                Common.FrmReport.rpt1 = new ReportDocument();
                Common.FrmReport.rpt1.Load(path);
                Common.FrmReport.rpt1.SetDataSource(dt);
                Common.FrmReport.PCS = this.PCS;

                if (name.Length > 0 && subTable != null)
                    Common.FrmReport.rpt1.Subreports[name].SetDataSource(subTable);

                if (Common.Sql_LogMod == 2)//混合驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in Common.FrmReport.rpt1.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                    Common.FrmReport.rpt1.Refresh();
                }
                else if (Common.Sql_LogMod == 1)//SQL驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in Common.FrmReport.rpt1.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                    Common.FrmReport.rpt1.Refresh();
                }

                TextObject myFieldTitleName;
                List<TextObject> Txt = Common.FrmReport.rpt1.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
                if (Txt.Find(t => t.Name == "title") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["title"];
                    myFieldTitleName.Text = Common.Sys_SaleHead;
                }
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();
                }
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtend"];
                    myFieldTitleName.Text = txtend;
                }
                if (Txt.Find(t => t.Name == "txtadress") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtadress"];
                    myFieldTitleName.Text = txtadress;
                }
                if (Txt.Find(t => t.Name == "txttel") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txttel"];
                    myFieldTitleName.Text = txttel;
                }
                for (int i = 0; i < lobj.Count; i++)
                {
                    if (Txt.Find(t => t.Name == lobj[i][0]) != null)
                    {
                        myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects[lobj[i][0]];
                        myFieldTitleName.Text = lobj[i][1];
                    }
                }

                List<ParameterField> Num = Common.FrmReport.rpt1.ParameterFields.OfType<ParameterField>().ToList();
                if (Num.Find(p => p.Name == "date") != null)
                {
                    switch (Common.User_DateTime)
                    {
                        case 1:
                            Common.FrmReport.rpt1.SetParameterValue("date", "民國");
                            break;
                        case 2:
                            Common.FrmReport.rpt1.SetParameterValue("date", "西元");
                            break;
                    }
                }
                if (Num.Find(p => p.Name == "備註說明") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                }
                if (Num.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "") pVar.ShowTRS = true;
                    Common.FrmReport.rpt1.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (Num.Find(p => p.Name == "千分位") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("千分位", pVar.TRS);
                }
                if (Num.Find(p => p.Name == "f") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("f", Common.dtSysSettings.Rows[0]["MnyDeciS"].ToString());
                }

                if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷貨單價小數", Common.MS);
                if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷貨單據小數", Common.MST);
                if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷項稅額小數", Common.TS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    Common.FrmReport.rpt1.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                if (Num.Find(p => p.Name == "進貨單價小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進貨單價小數", Common.MF);
                if (Num.Find(p => p.Name == "進貨單據小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進貨單據小數", Common.MFT);
                if (Num.Find(p => p.Name == "進項稅額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進項稅額小數", Common.TF);

                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進項金額小數", Common.TPF);

                for (int i = 0; i < lval.Count; i++)
                {
                    if (Num.Find(t => t.Name == lval[i][0]) != null)
                    {
                        Common.FrmReport.rpt1.SetParameterValue(lval[i][0], lval[i][1]);
                    }
                }

                if (name.Length > 0 && subTable != null)
                {
                    if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷貨單價小數", Common.MS, name);
                    if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷貨單據小數", Common.MST, name);
                    if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷項稅額小數", Common.TS, name);
                    if (Num.Find(p => p.Name == "本幣金額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("本幣金額小數", Common.M, name);
                    if (Num.Find(p => p.Name == "庫存數量小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("庫存數量小數", Common.Q, name);
                    if (Num.Find(p => p.Name == "是否顯示金額") != null)
                        Common.FrmReport.rpt1.SetParameterValue("是否顯示金額", Common.User_SalePrice, name);

                    if (Num.Find(p => p.Name == "進貨單價小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進貨單價小數", Common.MF, name);
                    if (Num.Find(p => p.Name == "進貨單據小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進貨單據小數", Common.MFT, name);
                    if (Num.Find(p => p.Name == "進項稅額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進項稅額小數", Common.TF, name);

                    if (Num.Find(p => p.Name == "銷項金額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷項金額小數", Common.TPS, name);
                    if (Num.Find(p => p.Name == "進項金額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進項金額小數", Common.TPF, name);

                    if (Num.Find(p => p.Name == "顯示千分位") != null)
                    {
                        if (pVar.TRS != "") pVar.ShowTRS = true;
                        Common.FrmReport.rpt1.SetParameterValue("顯示千分位", pVar.ShowTRS, name);
                    }
                    if (Num.Find(p => p.Name == "千分位") != null)
                    {
                        Common.FrmReport.rpt1.SetParameterValue("千分位", pVar.TRS, name);
                    }

                }

                Common.FrmReport.cview.ReportSource = Common.FrmReport.rpt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }





        public RPT(DataSet d, String p)
        {
            try
            {
                ds = d.Copy();
                path = p;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //DataSet
        public void PreView(int i)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DSInit();
                    Common.FrmReport.ShowDialog();

                    if (ds != null)
                        ds.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Print(int i)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DSInit();
                    Common.FrmReport.button1_Click(null, null);

                    if (ds != null)
                        ds.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Word(int i)
        {
            if (ds.Tables[0].Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DSInit();
                Common.FrmReport.word("");

                if (ds != null)
                    ds.Dispose();

            }
        }

        public void Excel(int i)
        {
            if (ds.Tables[0].Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DSInit();
                Common.FrmReport.excel("");

                if (ds != null)
                    ds.Dispose();

            }
        }

        void DSInit()
        {
            try
            {
                Common.FrmReport = new Report.Frmreport();
                Common.FrmReport.rpt1 = new ReportDocument();
                Common.FrmReport.rpt1.Load(path);
                Common.FrmReport.rpt1.SetDataSource(ds);
                Common.FrmReport.PCS = this.PCS;

                if (Common.Sql_LogMod == 2)//混合驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in Common.FrmReport.rpt1.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                    Common.FrmReport.rpt1.Refresh();
                }
                else if (Common.Sql_LogMod == 1)//SQL驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in Common.FrmReport.rpt1.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                    Common.FrmReport.rpt1.Refresh();
                }

                TextObject myFieldTitleName;
                List<TextObject> Txt = Common.FrmReport.rpt1.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
                if (Txt.Find(t => t.Name == "title") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["title"];
                    myFieldTitleName.Text = Common.Sys_SaleHead;
                }
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();
                }
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtend"];
                    myFieldTitleName.Text = txtend;
                }
                if (Txt.Find(t => t.Name == "txtadress") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtadress"];
                    myFieldTitleName.Text = txtadress;
                }
                if (Txt.Find(t => t.Name == "txttel") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txttel"];
                    myFieldTitleName.Text = txttel;
                }
                for (int i = 0; i < lobj.Count; i++)
                {
                    if (Txt.Find(t => t.Name == lobj[i][0]) != null)
                    {
                        myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects[lobj[i][0]];
                        myFieldTitleName.Text = lobj[i][1];
                    }
                }

                List<ParameterField> Num = Common.FrmReport.rpt1.ParameterFields.OfType<ParameterField>().ToList();
                if (Num.Find(p => p.Name == "date") != null)
                {
                    switch (Common.User_DateTime)
                    {
                        case 1:
                            Common.FrmReport.rpt1.SetParameterValue("date", "民國");
                            break;
                        case 2:
                            Common.FrmReport.rpt1.SetParameterValue("date", "西元");
                            break;
                    }
                }
                if (Num.Find(p => p.Name == "備註說明") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                }
                if (Num.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "") pVar.ShowTRS = true;
                    Common.FrmReport.rpt1.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (Num.Find(p => p.Name == "千分位") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("千分位", pVar.TRS);
                }
                if (Num.Find(p => p.Name == "f") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("f", Common.dtSysSettings.Rows[0]["MnyDeciS"].ToString());
                }

                if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷貨單價小數", Common.MS);
                if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷貨單據小數", Common.MST);
                if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷項稅額小數", Common.TS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    Common.FrmReport.rpt1.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                if (Num.Find(p => p.Name == "進貨單價小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進貨單價小數", Common.MF);
                if (Num.Find(p => p.Name == "進貨單據小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進貨單據小數", Common.MFT);
                if (Num.Find(p => p.Name == "進項稅額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進項稅額小數", Common.TF);

                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進項金額小數", Common.TPF);

                for (int i = 0; i < lval.Count; i++)
                {
                    if (Num.Find(t => t.Name == lval[i][0]) != null)
                    {
                        Common.FrmReport.rpt1.SetParameterValue(lval[i][0], lval[i][1]);
                    }
                }

                Common.FrmReport.cview.ReportSource = Common.FrmReport.rpt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        //DataSet + subReport
        public void PreView(int i, string name, DataTable subTable)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ds.Tables[0].Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DSInit_subT(name, subTable);
                Common.FrmReport.ShowDialog();

                if (ds != null)
                    ds.Dispose();

                if (subTable != null)
                    subTable.Dispose();

            }
        }

        public void Print(int i, string name, DataTable subTable)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ds.Tables[0].Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DSInit_subT(name, subTable);
                Common.FrmReport.button1_Click(null, null);

                if (ds != null)
                    ds.Dispose();

                if (subTable != null)
                    subTable.Dispose();

            }
        }

        public void Word(int i, string name, DataTable subTable)
        {
            if (ds.Tables[0].Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DSInit_subT(name, subTable);
                Common.FrmReport.word("");

                if (ds != null)
                    ds.Dispose();

                if (subTable != null)
                    subTable.Dispose();

            }
        }

        public void Excel(int i, string name, DataTable subTable)
        {
            if (ds.Tables[0].Rows.Count == 0)
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DSInit_subT(name, subTable);
                Common.FrmReport.excel("");

                if (ds != null)
                    ds.Dispose();

                if (subTable != null)
                    subTable.Dispose();

            }
        }

        void DSInit_subT(string name, DataTable subTable)
        {
            try
            {
                Common.FrmReport = new Report.Frmreport();
                Common.FrmReport.rpt1 = new ReportDocument();
                Common.FrmReport.rpt1.Load(path);
                Common.FrmReport.rpt1.SetDataSource(ds);
                Common.FrmReport.PCS = this.PCS;

                if (name.Length > 0 && subTable != null)
                    Common.FrmReport.rpt1.Subreports[name].SetDataSource(subTable);

                if (Common.Sql_LogMod == 2)//混合驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in Common.FrmReport.rpt1.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                    Common.FrmReport.rpt1.Refresh();
                }
                else if (Common.Sql_LogMod == 1)//SQL驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in Common.FrmReport.rpt1.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                    Common.FrmReport.rpt1.Refresh();
                }

                TextObject myFieldTitleName;
                List<TextObject> Txt = Common.FrmReport.rpt1.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
                if (Txt.Find(t => t.Name == "title") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["title"];
                    myFieldTitleName.Text = Common.Sys_SaleHead;
                }
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();
                }
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtend"];
                    myFieldTitleName.Text = txtend;
                }
                if (Txt.Find(t => t.Name == "txtadress") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txtadress"];
                    myFieldTitleName.Text = txtadress;
                }
                if (Txt.Find(t => t.Name == "txttel") != null)
                {
                    myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects["txttel"];
                    myFieldTitleName.Text = txttel;
                }
                for (int i = 0; i < lobj.Count; i++)
                {
                    if (Txt.Find(t => t.Name == lobj[i][0]) != null)
                    {
                        myFieldTitleName = (TextObject)Common.FrmReport.rpt1.ReportDefinition.ReportObjects[lobj[i][0]];
                        myFieldTitleName.Text = lobj[i][1];
                    }
                }

                List<ParameterField> Num = Common.FrmReport.rpt1.ParameterFields.OfType<ParameterField>().ToList();
                if (Num.Find(p => p.Name == "date") != null)
                {
                    switch (Common.User_DateTime)
                    {
                        case 1:
                            Common.FrmReport.rpt1.SetParameterValue("date", "民國");
                            break;
                        case 2:
                            Common.FrmReport.rpt1.SetParameterValue("date", "西元");
                            break;
                    }
                }
                if (Num.Find(p => p.Name == "備註說明") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                }
                if (Num.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "") pVar.ShowTRS = true;
                    Common.FrmReport.rpt1.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (Num.Find(p => p.Name == "千分位") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("千分位", pVar.TRS);
                }
                if (Num.Find(p => p.Name == "f") != null)
                {
                    Common.FrmReport.rpt1.SetParameterValue("f", Common.dtSysSettings.Rows[0]["MnyDeciS"].ToString());
                }

                if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷貨單價小數", Common.MS);
                if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷貨單據小數", Common.MST);
                if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷項稅額小數", Common.TS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    Common.FrmReport.rpt1.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                if (Num.Find(p => p.Name == "進貨單價小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進貨單價小數", Common.MF);
                if (Num.Find(p => p.Name == "進貨單據小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進貨單據小數", Common.MFT);
                if (Num.Find(p => p.Name == "進項稅額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進項稅額小數", Common.TF);

                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    Common.FrmReport.rpt1.SetParameterValue("進項金額小數", Common.TPF);

                for (int i = 0; i < lval.Count; i++)
                {
                    if (Num.Find(t => t.Name == lval[i][0]) != null)
                    {
                        Common.FrmReport.rpt1.SetParameterValue(lval[i][0], lval[i][1]);
                    }
                }

                if (name.Length > 0 && subTable != null)
                {
                    if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷貨單價小數", Common.MS, name);
                    if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷貨單據小數", Common.MST, name);
                    if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷項稅額小數", Common.TS, name);
                    if (Num.Find(p => p.Name == "本幣金額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("本幣金額小數", Common.M, name);
                    if (Num.Find(p => p.Name == "庫存數量小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("庫存數量小數", Common.Q, name);
                    if (Num.Find(p => p.Name == "是否顯示金額") != null)
                        Common.FrmReport.rpt1.SetParameterValue("是否顯示金額", Common.User_SalePrice, name);

                    if (Num.Find(p => p.Name == "進貨單價小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進貨單價小數", Common.MF, name);
                    if (Num.Find(p => p.Name == "進貨單據小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進貨單據小數", Common.MFT, name);
                    if (Num.Find(p => p.Name == "進項稅額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進項稅額小數", Common.TF, name);

                    if (Num.Find(p => p.Name == "銷項金額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("銷項金額小數", Common.TPS, name);
                    if (Num.Find(p => p.Name == "進項金額小數") != null)
                        Common.FrmReport.rpt1.SetParameterValue("進項金額小數", Common.TPF, name);

                    if (Num.Find(p => p.Name == "顯示千分位") != null)
                    {
                        if (pVar.TRS != "") pVar.ShowTRS = true;
                        Common.FrmReport.rpt1.SetParameterValue("顯示千分位", pVar.ShowTRS, name);
                    }
                    if (Num.Find(p => p.Name == "千分位") != null)
                    {
                        Common.FrmReport.rpt1.SetParameterValue("千分位", pVar.TRS, name);
                    }

                }

                Common.FrmReport.cview.ReportSource = Common.FrmReport.rpt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
