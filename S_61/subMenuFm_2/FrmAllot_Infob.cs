using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmAllot_Infob : Formbase
    {
        public string DateRange { get; set; }
        public DataTable table;
        public DataTable tableB = new DataTable();
        public List<DataRow> list;
        public string date { get; set; }
        public string date1 { get; set; }
        public string stnoi { get; set; }
        public string stnoi1 { get; set; }
        public string stnoo { get; set; }
        public string stnoo1 { get; set; }
        public string emno { get; set; }
        public string emno1 { get; set; }
        public string itno { get; set; }
        public string itno1 { get; set; }
        public string memo { get; set; }

        string ReportFileName = "";
        string ReportPath = "";
        bool No_Data = false;
        string SortState = "F2";
        string NO = "";
        DataRow dr;
        List<Button> qury;
        ToolTip _Tip = new ToolTip();

        public FrmAllot_Infob()
        {
            InitializeComponent();
            this.說明.HeaderText = Common.Sys_MemoUdf;
            qury = new List<Button> { qury2, qury3, qury4, qury5 };
        }

        private void FrmAllot_Infob_Load(object sender, EventArgs e)
        {
            ReportFileName = "調撥資料瀏覽";

            if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_簡要自定.rpt"))
            {
                rd2.SetUserDefineRpt("調撥資料瀏覽F2_簡要自定.rpt");
            }
            else if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_簡要自訂.rpt"))
            {
                rd2.SetUserDefineRpt("調撥資料瀏覽F2_簡要自訂.rpt");
            }
            else
            {
                rd2.Enabled = false;
            }

            if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_組件自定.rpt"))
            {
                rd3.SetUserDefineRpt("調撥資料瀏覽F2_組件自定.rpt");
            }
            else if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_組件自訂.rpt"))
            {
                rd3.SetUserDefineRpt("調撥資料瀏覽F2_組件自訂.rpt");
            }
            else
            {
                rd3.Enabled = false;
            }
              
            this.調撥數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;

            if (Common.User_DateTime == 1)
                this.調撥日期.DataPropertyName = "aldate";
            else
                this.調撥日期.DataPropertyName = "aldate1";

            dataGridViewT1.DataSource = table;

            qury2.ForeColor = Color.Red;
        }

        void dataintodocument()
        {
            No_Data = false;

            Common.FrmReport = new Report.Frmreport();
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;
            string udfPath = Common.reportaddress + "Report\\" + ReportFileName;

            bool isUserUdf = false;
            if (rd2.Checked)
                isUserUdf = true;
            else if (rd3.Checked)
                isUserUdf = true;

            if (!isUserUdf)
            {
                if (rd5.Checked)
                    ReportPath += "B";
            }

            try
            {
                if (rd1.Checked)
                {
                    ReportPath += "_內定報表.rpt";
                    if (File.Exists(ReportPath))
                    {
                        oRpt.Load(ReportPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        No_Data = true;
                        return;
                    }
                }
                if (rd2.Checked)
                {
                    if (File.Exists(ReportPath + "_簡要自定.rpt"))
                    {
                        ReportPath += "_簡要自定.rpt";
                        oRpt.Load(ReportPath);
                    }
                    else if (File.Exists(ReportPath + "_簡要自訂.rpt"))
                    {
                        ReportPath += "_簡要自訂.rpt";
                        oRpt.Load(ReportPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        No_Data = true;
                        return;
                    }
                }
                if (rd3.Checked)
                {
                    if (File.Exists(ReportPath + "_組件自定.rpt"))
                    {
                        ReportPath += "_組件自定.rpt";
                        oRpt.Load(ReportPath);
                    }
                    else if (File.Exists(ReportPath + "_組件自訂.rpt"))
                    {
                        ReportPath += "_組件自訂.rpt";
                        oRpt.Load(ReportPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        No_Data = true;
                        return;
                    }
                }
                if (rd5.Checked)
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();

                        string sql = "select C.itpareprs as Bompareprs,C.itpkgqty as Bomitpkgqty,C.itname as Bomitname,C.itunit as Bomitunit,C.itqty as Bomitqty,A.emname,A.stnamei,A.stnameo,B.*";
                        sql += " from  allot as A left join allotd as B on A.alno=B.alno";
                        sql += " left join allobom as C on C.bomid = B.bomid where '0'='0'";
                        if (stnoi != "")
                            sql += " and B.stnoi >=@stnoi";
                        if (stnoi1 != "")
                            sql += " and B.stnoi <=@stnoi1";
                        if (stnoo != "")
                            sql += " and B.stnoo >=@stnoo";
                        if (stnoo1 != "")
                            sql += " and B.stnoo <=@stnoo1";
                        if (emno != "")
                            sql += " and B.emno >=@emno";
                        if (emno1 != "")
                            sql += " and B.emno <=@emno1";
                        if (itno != "")
                            sql += " and B.itno >=@itno";
                        if (itno1 != "")
                            sql += " and B.itno <=@itno1";
                        if (memo != "")
                            sql += " and B.memo like '%' + @memo + '%'";
                        if (Common.User_DateTime == 1)
                        {
                            if (date != "")
                                sql += " and B.aldate >=@aldate";
                            if (date1 != "")
                                sql += " and B.aldate <=@aldate1";
                        }
                        else
                        {
                            if (date != "")
                                sql += " and B.aldate1 >=@aldate";
                            if (date1 != "")
                                sql += " and B.aldate1 <=@aldate1";
                        }

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            if (stnoi != "") cmd.Parameters.AddWithValue("stnoi", stnoi);
                            if (stnoi1 != "") cmd.Parameters.AddWithValue("stnoi1", stnoi1);
                            if (stnoo != "") cmd.Parameters.AddWithValue("stnoo", stnoo);
                            if (stnoo1 != "") cmd.Parameters.AddWithValue("stnoo1", stnoo1);
                            if (emno != "") cmd.Parameters.AddWithValue("emno", emno);
                            if (emno1 != "") cmd.Parameters.AddWithValue("emno1", emno1);
                            if (itno != "") cmd.Parameters.AddWithValue("itno", itno);
                            if (itno1 != "") cmd.Parameters.AddWithValue("itno1", itno1);
                            if (memo != "") cmd.Parameters.AddWithValue("memo", memo);
                            if (date != "") cmd.Parameters.AddWithValue("aldate", date);
                            if (date1 != "") cmd.Parameters.AddWithValue("aldate1", date1);
                            cmd.CommandText = sql;
                            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                            {
                                tableB.Clear();
                                dd.Fill(tableB);
                                for (int i = 0; i < tableB.Rows.Count; i++)
                                {
                                    if (Common.User_DateTime == 1)
                                        tableB.Rows[i]["aldate"] = Date.AddLine(tableB.Rows[i]["aldate"].ToString());
                                    else
                                        tableB.Rows[i]["aldate1"] = Date.AddLine(tableB.Rows[i]["aldate1"].ToString());
                                }
                                oRpt.SetDataSource(tableB);
                            }
                        }
                    }
                }
                else
                {
                    oRpt.SetDataSource(table);
                }
                if (Common.Sql_LogMod == 2)//混合驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table dt in oRpt.Database.Tables)
                    {
                        dt.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                else if (Common.Sql_LogMod == 1)//SQL驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table dt in oRpt.Database.Tables)
                    {
                        dt.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                oRpt.Refresh();
                TextObject myFieldTitleName = null;
                List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
                //公司抬頭
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.Sys_StcPnName;
                }
                //單行註腳
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (rd6.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (rd7.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (rd8.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (rd9.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (rd10.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                //日期區間
                if (Txt.Find(t => t.Name == "DateRange") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateRange"];
                    myFieldTitleName.Text = "日期區間：" + DateRange;
                }
                //制表日期
                if (Txt.Find(t => t.Name == "DateCreated") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateCreated"];
                    myFieldTitleName.Text = "制表日期：" + Date.GetDateTime(Common.User_DateTime, true);
                }


                List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
                //備註說明
                if (Num.Find(p => p.Name == "備註說明") != null)
                    oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                //日期格式


                if (Num.Find(p => p.Name == "date") != null)
                {
                    switch (Common.User_DateTime)
                    {
                        case 1:
                            oRpt.SetParameterValue("date", "民國");
                            break;
                        case 2:
                            oRpt.SetParameterValue("date", "西元");
                            break;
                    }
                }

                //報表參數設定
                if (Num.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "")
                        pVar.ShowTRS = true;
                    oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (Num.Find(p => p.Name == "千分位") != null)
                    oRpt.SetParameterValue("千分位", pVar.TRS);
                if (Num.Find(p => p.Name == "進貨單價小數") != null)
                    oRpt.SetParameterValue("進貨單價小數", Common.MF);
                if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                    oRpt.SetParameterValue("銷貨單價小數", Common.MS);
                if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                    oRpt.SetParameterValue("銷貨單據小數", Common.MST);
                if (Num.Find(p => p.Name == "進貨單據小數") != null)
                    oRpt.SetParameterValue("進貨單據小數", Common.MFT);
                if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                    oRpt.SetParameterValue("銷項稅額小數", Common.TS);
                if (Num.Find(p => p.Name == "進項稅額小數") != null)
                    oRpt.SetParameterValue("進項稅額小數", Common.TF);
                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    oRpt.SetParameterValue("進項金額小數", Common.TPF);
                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                Common.FrmReport.cview.ReportSource = oRpt;
                Common.FrmReport.rpt1 = oRpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null) return;
            StNoO.Text = dr["stnoo"].ToString();
            StNameO.Text = dr["stnameo"].ToString();
            StNoI.Text = dr["stnoi"].ToString();
            StNameI.Text = dr["stnamei"].ToString();
            ItNo.Text = dr["itno"].ToString();
            EmNo.Text = dr["emno"].ToString();
            EmName.Text = dr["emname"].ToString();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            dr = list.Find(r => r["序號"].ToString().Trim() == dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim());
            WriteToTxt(dr);
        }

        void UdfReport()
        {
            ReportFileName = "調撥資料瀏覽";

            if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_簡要自定.rpt"))
            {
                rd2.SetUserDefineRpt("調撥資料瀏覽F2_簡要自定.rpt");
            }
            else if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_簡要自訂.rpt"))
            {
                rd2.SetUserDefineRpt("調撥資料瀏覽F2_簡要自訂.rpt");
            }
            else
            {
                rd2.Enabled = false;
            }

            if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_組件自定.rpt"))
            {
                rd3.SetUserDefineRpt("調撥資料瀏覽F2_組件自定.rpt");
            }
            else if (System.IO.File.Exists(Common.reportaddress + "Report\\" + ReportFileName + "F2_組件自訂.rpt"))
            {
                rd3.SetUserDefineRpt("調撥資料瀏覽F2_組件自訂.rpt");
            }
            else
            {
                rd3.Enabled = false;
            }
        }

        void SetButtonColor()
        {
            rd1.Checked = true;
            UdfReport();
            qury.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow(string NO)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == NO)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void qury2_Click(object sender, EventArgs e)
        {
            SortState = "F2";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "aldate,alno";
            SetButtonColor();
            qury2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury3_Click(object sender, EventArgs e)
        {
            SortState = "F3";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "itno,aldate";
            SetButtonColor();
            qury3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury4_Click(object sender, EventArgs e)
        {
            SortState = "F4";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "emno,aldate";
            SetButtonColor();
            qury4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury5_Click(object sender, EventArgs e)
        {
            SortState = "F5";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "memo,aldate";
            SetButtonColor();
            qury5.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (!No_Data)
            {
                Common.FrmReport.button1_Click(null, null);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (!No_Data)
                Common.FrmReport.ShowDialog();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (No_Data) return;
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".doc");
            Process.Start(Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".doc");
            Common.FrmReport.Dispose();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (No_Data) return;
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".xls");
            Process.Start(Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".xls");
            Common.FrmReport.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                table.Excel匯出並開啟(this.Text);
            }
            if (keyData == Keys.F2)
            {
                qury2.PerformClick();
            }
            else if (keyData == Keys.F3)
            {
                qury3.PerformClick();
            }
            else if (keyData == Keys.F4)
            {
                qury4.PerformClick();
            }
            else if (keyData == Keys.F5)
            {
                qury5.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
