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

namespace S_61.subMenuFm_1
{
    public partial class FrmQuote_Infob : Formbase
    {
        public string DateRange { get; set; }
        public DataTable table;
        public DataTable tableB = new DataTable();
        public List<DataRow> list;
        public string date { get; set; }
        public string date1 { get; set; }
        public string dates { get; set; }
        public string dates1 { get; set; }
        public string cuno { get; set; }
        public string cuno1 { get; set; }
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

        public FrmQuote_Infob()
        {
            InitializeComponent();

            this.說明.HeaderText = Common.Sys_MemoUdf;

            qury = new List<Button> { qury2, qury3, qury4, qury5 };

            this.售價.Visible = Common.User_SalePrice;
            this.稅前售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
        }

        private void FrmQuote_Infob_Load(object sender, EventArgs e)
        {
            SearchUserReport("F2");         

            this.報價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前售價.DefaultCellStyle.Format = "f6";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;

            dataGridViewT1.DataSource = table;

            qury2.ForeColor = Color.Red;
        }

        void FastReport列印(string path, RptMode mode)
        {
            string sql="";
            DataTable printTB = new DataTable();
            printTB.TableName = "QUOTE_";

            if (rd5.Checked)
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    sql = "select cu.cufax1,c.itrec,c.itpareprs as bomitpareprs,c.itpkgqty as bomitpkgqty,c.itname as bomitname,c.itunit as bomitunit,c.itqty as bomqty,b.*,a.cuper1,a.cutel1,a.cuname1,a.emname,a.cuno,a.emno,報價日期='',預交日期='',序號=''"
                        + " from quoted as b left join quote as a on b.quno=a.quno "
                        + " left join quotebom as c on c.bomid = b.bomid "
                        + " left join cust as cu on cu.cuno = b.cuno where '0'='0' ";
                    if (Common.User_DateTime == 1)
                    {
                        sql += " and a.qudate >=@qudate";
                        sql += " and a.qudate <=@qudate1";
                        if (dates != "")
                            sql += " and a.qudates >=@qudates";
                        if (dates1 != "")
                            sql += " and a.qudates <=@qudates1";
                    }
                    else
                    {
                        sql += " and a.qudate1 >=@qudate";
                        sql += " and a.qudate1 <=@qudate1";
                        if (dates != "")
                            sql += " and a.qudates1 >=@qudates";
                        if (dates1 != "")
                            sql += " and a.qudates1 <=@qudates1";
                    }
                    if (cuno != "")
                        sql += " and a.cuno >=@cuno";
                    if (cuno1 != "")
                        sql += " and a.cuno <=@cuno1";
                    if (emno != "")
                        sql += " and a.emno >=@emno";
                    if (emno1 != "")
                        sql += " and a.emno <=@emno1";
                    if (itno != "")
                        sql += " and b.itno >=@itno";
                    if (itno1 != "")
                        sql += " and b.itno <=@itno1";
                    if (memo != "")
                        sql += " and b.memo like '%' + @memo + '%'";
                    sql += " order by b.qudate,b.quno";

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("qudate", date);
                    cmd.Parameters.AddWithValue("qudate1", date1);
                    if (dates != "") cmd.Parameters.AddWithValue("qudates", dates);
                    if (dates1 != "") cmd.Parameters.AddWithValue("qudates1", dates1);
                    cmd.Parameters.AddWithValue("cuno", cuno);
                    cmd.Parameters.AddWithValue("cuno1", cuno1);
                    cmd.Parameters.AddWithValue("emno", emno);
                    cmd.Parameters.AddWithValue("emno1", emno1);
                    cmd.Parameters.AddWithValue("itno", itno);
                    cmd.Parameters.AddWithValue("itno1", itno1);
                    cmd.Parameters.AddWithValue("memo", memo);
                    cmd.CommandText = sql;
                    SqlDataAdapter dd = new SqlDataAdapter(cmd);
                    tableB.Clear();
                    dd.Fill(tableB);
                    string bomid = "";
                    for (int i = 0; i < tableB.Rows.Count; i++)
                    {
                        if (tableB.Rows[i]["bomid"].ToString().Trim().Equals(bomid) == false)
                        {
                            tableB.Rows[i]["序號"] = "V";
                            bomid = tableB.Rows[i]["bomid"].ToString().Trim();
                        }
                        //tableB.Rows[i]["序號"] = i;
                        //if (i < tableB.Rows.Count-1)
                        //{
                        //    if (tableB.Rows[i]["bomid"].ToString().Trim() == tableB.Rows[i + 1]["bomid"].ToString().Trim()) 
                        //    {
                        //        tableB.Rows[i]["qty"] = 0;
                        //        tableB.Rows[i]["mny"] = 0;
                        //    }
                        //}
                        if (Common.User_DateTime == 1)
                        {
                            tableB.Rows[i]["報價日期"] = Date.AddLine(tableB.Rows[i]["qudate"].ToString());
                            if (tableB.Rows[i]["qudates"].ToString() == "")
                            {
                                tableB.Rows[i]["預交日期"] = "   /  /";
                            }
                            else
                            {
                                tableB.Rows[i]["預交日期"] = Date.AddLine(tableB.Rows[i]["qudates"].ToString());
                            }
                        }
                        else
                        {
                            tableB.Rows[i]["報價日期"] = Date.AddLine(tableB.Rows[i]["qudate1"].ToString());
                            if (tableB.Rows[i]["qudates1"].ToString() == "")
                            {
                                tableB.Rows[i]["預交日期"] = "    /  /";
                            }
                            else
                            {
                                tableB.Rows[i]["預交日期"] = Date.AddLine(tableB.Rows[i]["qudates1"].ToString());
                            }
                        }
                    }
                }
            }


            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                //銷貨單標題
                var title = Common.Sys_SaleHead;
                FastReport.dy.Add("title", title);
                //三行註腳
                string txtend = "";
                if (rd6.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (rd7.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (rd8.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (rd9.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (rd10.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                FastReport.dy.Add("txtend", txtend);
                FastReport.dy.Add("txtRange", DateRange);
                FastReport.dy.Add("txtstart", Common.Sys_StcPnName);
                FastReport.dy.Add("price", Common.User_SalePrice);
                FastReport.PreView(path, rd5.Checked == true ? tableB : table, "QUOTE_", null, null, mode, ReportFileName);
            }
        }

        string 確認列印報表()
        {
            string path = "";

            path = "報價資料瀏覽" + SortState;

            if (!(rd2.Checked || rd3.Checked))
            {
                if (rd5.Checked)
                    path += "B";
            }
            if (rd1.Checked)
                path += "_內定報表";
            if (rd2.Checked)
                path += "_明細自定";
            if (rd3.Checked)
                path += "_組件明細";


            return Common.判斷開啟報表類型(path);
        }

        void dataintodocument()
        {
            No_Data = false;

            Common.FrmReport = new Report.Frmreport();
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;
            string udfPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;

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
                    ReportPath += "_明細自定.rpt";
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
                    ReportPath += "_組件明細.rpt";
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

                        string sql = "";
                        sql = "select cu.cufax1,c.itpareprs as bomitpareprs,c.itpkgqty as bomitpkgqty,c.itname as bomitname,c.itunit as bomitunit,c.itqty as bomqty,b.*,a.cuper1,a.cutel1,a.cuname1,a.emname,a.cuno,a.emno,報價日期='',預交日期='',序號=''"
                            + " from quoted as b left join quote as a on b.quno=a.quno "
                            + " left join quotebom as c on c.bomid = b.bomid "
                            + " left join cust as cu on cu.cuno = b.cuno where '0'='0' ";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.qudate >=@qudate";
                            sql += " and a.qudate <=@qudate1";
                            if (dates != "")
                                sql += " and a.qudates >=@qudates";
                            if (dates1 != "")
                                sql += " and a.qudates <=@qudates1";
                        }
                        else
                        {
                            sql += " and a.qudate1 >=@qudate";
                            sql += " and a.qudate1 <=@qudate1";
                            if (dates != "")
                                sql += " and a.qudates1 >=@qudates";
                            if (dates1 != "")
                                sql += " and a.qudates1 <=@qudates1";
                        }
                        if (cuno != "")
                            sql += " and a.cuno >=@cuno";
                        if (cuno1 != "")
                            sql += " and a.cuno <=@cuno1";
                        if (emno != "")
                            sql += " and a.emno >=@emno";
                        if (emno1 != "")
                            sql += " and a.emno <=@emno1";
                        if (itno != "")
                            sql += " and b.itno >=@itno";
                        if (itno1 != "")
                            sql += " and b.itno <=@itno1";
                        if (memo != "")
                            sql += " and b.memo like '%' + @memo + '%'";
                        sql += " order by b.qudate,b.quno";

                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("qudate", date);
                        cmd.Parameters.AddWithValue("qudate1", date1);
                        if (dates != "") cmd.Parameters.AddWithValue("qudates", dates);
                        if (dates1 != "") cmd.Parameters.AddWithValue("qudates1", dates1);
                        cmd.Parameters.AddWithValue("cuno", cuno);
                        cmd.Parameters.AddWithValue("cuno1", cuno1);
                        cmd.Parameters.AddWithValue("emno", emno);
                        cmd.Parameters.AddWithValue("emno1", emno1);
                        cmd.Parameters.AddWithValue("itno", itno);
                        cmd.Parameters.AddWithValue("itno1", itno1);
                        cmd.Parameters.AddWithValue("memo", memo);
                        cmd.CommandText = sql;
                        SqlDataAdapter dd = new SqlDataAdapter(cmd);
                        tableB.Clear();
                        dd.Fill(tableB);
                        for (int i = 0; i < tableB.Rows.Count; i++)
                        {
                            tableB.Rows[i]["序號"] = i;
                            if (Common.User_DateTime == 1)
                            {
                                tableB.Rows[i]["報價日期"] = Date.AddLine(tableB.Rows[i]["qudate"].ToString());
                                if (tableB.Rows[i]["qudates"].ToString() == "")
                                {
                                    tableB.Rows[i]["預交日期"] = "   /  /";
                                }
                                else
                                {
                                    tableB.Rows[i]["預交日期"] = Date.AddLine(tableB.Rows[i]["qudates"].ToString());
                                }
                            }
                            else
                            {
                                tableB.Rows[i]["報價日期"] = Date.AddLine(tableB.Rows[i]["qudate1"].ToString());
                                if (tableB.Rows[i]["qudates1"].ToString() == "")
                                {
                                    tableB.Rows[i]["預交日期"] = "    /  /";
                                }
                                else
                                {
                                    tableB.Rows[i]["預交日期"] = Date.AddLine(tableB.Rows[i]["qudates1"].ToString());
                                }
                            }
                        }
                        oRpt.SetDataSource(tableB);
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
                //製表日期
                if (Txt.Find(t => t.Name == "DateCreated") != null)
                {
                    string datetime = "";
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateCreated"];
                    if (Common.User_DateTime == 1)
                    {
                        datetime = Date.GetDateTime(1, false);
                        datetime = Date.AddLine(datetime);
                    }
                    else
                    {
                        datetime = Date.GetDateTime(2, false);
                        datetime = Date.AddLine(datetime);
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

        void WriteToTxt(DataRow dr)
        {
            if (dr == null) return;
            CuNo.Text = dr["cuno"].ToString();
            ItNo.Text = dr["itno"].ToString();
            EmNo.Text = dr["emno"].ToString();
        }

        //ToolTip _Tip = new ToolTip();

        void SearchUserReport(string sort)
        {
            rd1.Checked = true;
            ReportFileName = "報價資料瀏覽";

            rd2.判斷有無CF或RF("報價資料瀏覽_明細自定");
            rd3.判斷有無CF或RF("報價資料瀏覽_組件明細");
        }

        private void qury2_Click(object sender, EventArgs e)
        {
            SearchUserReport("F2");
            SortState = "F2";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "qudate,quno";
            SetButtonColor();
            qury2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury3_Click(object sender, EventArgs e)
        {
            SearchUserReport("F3");
            SortState = "F3";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "cuno,qudate";
            SetButtonColor();
            qury3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury4_Click(object sender, EventArgs e)
        {
            SearchUserReport("F4");
            SortState = "F4";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "itno,itunit,qudate";
            SetButtonColor();
            qury4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury5_Click(object sender, EventArgs e)
        {
            SearchUserReport("F5");
            SortState = "F5";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "emno,qudate";
            SetButtonColor();
            qury5.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.Print);
            }

            else
            {
                dataintodocument();
                if (!No_Data)
                {
                    Common.FrmReport.button1_Click(null, null);
                }
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.PreView);
            }
            else
            {
                dataintodocument();
                if (!No_Data)
                    Common.FrmReport.ShowDialog();
            }
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.Word);
            }
            else
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
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.Excel);
            }
            else
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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            dr = list.Find(r => r["序號"].ToString().Trim() == dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim());
            WriteToTxt(dr);
        }

        void SetButtonColor()
        {
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                table.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var path = 確認列印報表();
                if (path.EndsWith(".frx") == false) return;

                var dl = MessageBox.Show("是否要修改報表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl != DialogResult.Yes) return;

                JBS.FReport.Design(path);
            }
        }
    }
}
