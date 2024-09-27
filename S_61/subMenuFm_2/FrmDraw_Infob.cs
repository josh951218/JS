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
    public partial class FrmDraw_Infob : Formbase
    {
        public DataTable table;
        public List<DataRow> list;
        public string DateRange { get; set; }
        public string date{ get; set; }
        public string date1{ get; set; }
        public string stnoi{ get; set; }
        public string stnoi1{ get; set; }
        public string stnoo{ get; set; }
        public string stnoo1{ get; set; }
        public string emno{ get; set; }
        public string emno1{ get; set; }
        public string itno{ get; set; }
        public string itno1{ get; set; }
        public string memo{ get; set; }

        string ReportFileName = "";
        string ReportPath = "";
        bool No_Data = false;
        string SortState = "F2";

        List<Button> qury;
        DataTable tableB = new DataTable();
        string index = "";
        DataRow dr;

        public FrmDraw_Infob()
        {
            InitializeComponent();

            this.說明.HeaderText = Common.Sys_MemoUdf;
            qury = new List<Button> { qury2, qury3, qury4, qury5};
        }

        private void FrmDraw_Infob_Load(object sender, EventArgs e)
        {
            ReportFileName = "領料資料瀏覽";
             
            rd2.SetUserDefineRpt("領料資料瀏覽_簡要自訂.rpt");
            rd3.SetUserDefineRpt("領料資料瀏覽_組件自訂.rpt");
             
            this.領料數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;

            dataGridViewT1.DataSource = table;
            dr = list.First();
            index = dr["序號"].ToString();
            WriteToTex(dr);
            SetQuryColor();
            qury2.ForeColor = Color.Red;

        }

        void WriteToTex(DataRow dr)
        {
            StNoI.Text = dr["stnoi"].ToString();
            StNameI.Text = dr["stnamei"].ToString();
            StNoO.Text = dr["stnoo"].ToString();
            StNameO.Text = dr["stnameo"].ToString();
            EmNo.Text = dr["emno"].ToString();
            EmName.Text = dr["emname"].ToString();
            ItNo.Text = dr["itno"].ToString();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0)
                return;
            string num = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dr = list.Find(r => r["序號"].ToString() == num);
            WriteToTex(dr);
            
        }

        void SetQuryColor()
        {
            qury.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (dataGridViewT1["序號",i].Value.ToString() == index)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }
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
                    ReportPath += "_簡要自訂.rpt";
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
                if (rd3.Checked)
                {
                    ReportPath += "_組件自訂.rpt";
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
                if (rd5.Checked)
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        string sql = "select 日期='',c.itpareprs as bomitpareprs,c.itpkgqty as bomitpkgqty,c.itname as bomitname,c.itunit as bomitunit,c.itqty as bomqty,a.emname,b.* from draw as a"
                            + " left join drawd as b on a.drno = b.drno"
                            + " left join drawbom as c on b.bomid = c.bomid"
                            + " where '0'='0'";
                        if (stnoi != "")
                            sql += " and b.stnoi >=@stnoi";
                        if (stnoi1 != "")
                            sql += " and b.stnoi <=@stnoi1";
                        if (stnoo != "")
                            sql += " and b.stnoo >=@stnoo";
                        if (stnoo1 != "")
                            sql += " and b.stnoo <=@stnoo1";
                        if (emno != "")
                            sql += " and b.emno >=@emno";
                        if (emno1 != "")
                            sql += " and b.emno <=@emno1";
                        if (itno != "")
                            sql += " and b.itno >=@itno";
                        if (itno1 != "")
                            sql += " and b.itno <=@itno1";
                        if (memo != "")
                            sql += " and b.memo like '%' + @memo + '%'";
                        if (Common.User_DateTime == 1)
                        {
                            if (date != "")
                                sql += " and b.drdate >=@drdate";
                            if (date1 != "")
                                sql += " and b.drdate <=@drdate1";
                        }
                        else
                        {
                            if (date != "")
                                sql += " and b.drdate1 >=@drdate";
                            if (date1 != "")
                                sql += " and b.drdate1 <=@drdate1";
                        }

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            if (stnoi != "") cmd.Parameters.AddWithValue("stnoi",stnoi);
                            if (stnoi1 != "") cmd.Parameters.AddWithValue("stnoi1",stnoi1);
                            if (stnoo != "") cmd.Parameters.AddWithValue("stnoo",stnoo);
                            if (stnoo1 != "") cmd.Parameters.AddWithValue("stnoo1",stnoo1);
                            if (emno != "") cmd.Parameters.AddWithValue("emno",emno);
                            if (emno1 != "") cmd.Parameters.AddWithValue("emno1",emno1);
                            if (itno != "") cmd.Parameters.AddWithValue("itno",itno);
                            if (itno1 != "") cmd.Parameters.AddWithValue("itno1",itno1);
                            if (memo != "") cmd.Parameters.AddWithValue("memo",memo);
                            if (date != "") cmd.Parameters.AddWithValue("drdate",date);
                            if (date1 != "") cmd.Parameters.AddWithValue("drdate1",date1);
                            cmd.CommandText=sql;
                            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                            {
                                tableB.Clear();
                                dd.Fill(tableB);
                                for (int i = 0; i < tableB.Rows.Count; i++)
                                {
                                    if (Common.User_DateTime == 1)
                                    {
                                        tableB.Rows[i]["日期"] = Date.AddLine(tableB.Rows[i]["drdate"].ToString());
                                    }
                                    else
                                    {
                                        tableB.Rows[i]["日期"] = Date.AddLine(tableB.Rows[i]["drdate1"].ToString());
                                    }
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
                    myFieldTitleName.Text = DateRange;
                }
                //製表日期
                if (Txt.Find(t => t.Name == "DateCreated") != null)
                {
                    string datetime = "";
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateCreated"];
                    if(Common.User_DateTime ==1)
                    {
                        datetime = Date.GetDateTime(1, false);
                        datetime = Date.AddLine(date);
                    }
                    else
                    {
                        datetime = Date.GetDateTime(2, false);
                        datetime = Date.AddLine(date);
                    }
                    myFieldTitleName.Text = datetime;
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
                            myFieldTitleName.Text = "製表日期：" + Date.GetDateTime(1, true);
                            break;
                        case 2:
                            oRpt.SetParameterValue("date", "西元");
                            myFieldTitleName.Text = "製表日期：" + Date.GetDateTime(2, true);
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
            this.Close();
            this.Dispose();
        }

        private void qury2_Click(object sender, EventArgs e)
        {
            SortState = "F2";
            index = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "drdate,drno";
            SetQuryColor();
            qury2.ForeColor = Color.Red;
            SetSelectRow();
        }

        private void qury3_Click(object sender, EventArgs e)
        {
            SortState = "F3";
            index = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "itno,drdate";
            SetQuryColor();
            qury3.ForeColor = Color.Red;
            SetSelectRow();
        }

        private void qury4_Click(object sender, EventArgs e)
        {
            SortState = "F4";
            index = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "emno,drdate";
            SetQuryColor();
            qury4.ForeColor = Color.Red;
            SetSelectRow();
        }

        private void qury5_Click(object sender, EventArgs e)
        {
            SortState = "F5";
            index = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "memo,drdate";
            SetQuryColor();
            qury5.ForeColor = Color.Red;
            SetSelectRow();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                table.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

         
    }
}
