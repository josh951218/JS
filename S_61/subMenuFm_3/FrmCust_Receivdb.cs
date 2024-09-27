using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;
using System.Data;
using System.Data.SqlClient;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_Receivdb : Formbase
    {
        public DataTable dt_datagrid = new DataTable();
        public List<DataRow> list = new List<DataRow>();
        public DataRow dr;
        public string current;
        int temp = 0;
        int 銷貨單價小數, 銷貨單據小數, 銷項稅額小數, 本幣金額小數, 庫存數量小數;
        string reportfilename = "";

        public FrmCust_Receivdb()
        {
            InitializeComponent();

            tabControl1.ItemSize = new Size(0, 1);

            銷貨單價小數 = Common.MS;
            銷貨單據小數 = Common.MST;
            銷項稅額小數 = Common.TS;
            本幣金額小數 = Common.M;
            庫存數量小數 = Common.Q;

            if (pVar.FrmCust_Receivd.radio1.Checked)
            {
                foreach (Control a in tabPage1.Controls)
                {
                    if (a is TextBoxNumberT)
                    {
                        (a as TextBoxNumberT).FirstNum = Common.nFirst;
                        (a as TextBoxNumberT).LastNum = 銷貨單據小數;

                    }
                }
                沖帳總筆數1.LastNum = 0;
            }
            else if (pVar.FrmCust_Receivd.radio2.Checked)
            {
                foreach (Control a in tabPage2.Controls)
                {
                    if (a is TextBoxNumberT)
                    {
                        (a as TextBoxNumberT).FirstNum = Common.nFirst;
                        (a as TextBoxNumberT).LastNum = 本幣金額小數;

                    }
                }
                沖帳總筆數1.LastNum = 0;
            }

            this.折讓金額.Set銷貨單據小數();
            this.現金金額.Set銷貨單據小數();
            this.刷卡金額.Set銷貨單據小數();
            this.禮卷金額.Set銷貨單據小數();
            this.支票金額.Set銷貨單據小數();
            this.匯入金額.Set銷貨單據小數();
            this.其他金額.Set銷貨單據小數();
            this.取用預收.Set銷貨單據小數();
            this.沖帳合計.Set銷貨單據小數();
            this.累入預收.Set銷貨單據小數();
            this.沖抵帳款.Set銷貨單據小數();

            this.折讓金額d2.Set本幣金額小數();
            this.現金金額d2.Set本幣金額小數();
            this.刷卡金額d2.Set本幣金額小數();
            this.禮卷金額d2.Set本幣金額小數();
            this.支票金額d2.Set本幣金額小數();
            this.匯入金額d2.Set本幣金額小數();
            this.其他金額d2.Set本幣金額小數();
            this.取用預收d2.Set本幣金額小數();
            this.沖帳合計d2.Set本幣金額小數();
            this.累入預收d2.Set本幣金額小數();
            this.沖抵帳款d2.Set本幣金額小數();

            this.折讓金額d3.Set銷貨單據小數();
            this.現金金額d3.Set銷貨單據小數();
            this.刷卡金額d3.Set銷貨單據小數();
            this.禮卷金額d3.Set銷貨單據小數();
            this.支票金額d3.Set銷貨單據小數();
            this.匯入金額d3.Set銷貨單據小數();
            this.其他金額d3.Set銷貨單據小數();
            this.取用預收d3.Set銷貨單據小數();
            this.沖帳合計d3.Set銷貨單據小數();
            this.累入預收d3.Set銷貨單據小數();
            this.沖抵帳款d3.Set銷貨單據小數();
        }

        private void FrmCust_Receivdb_Load(object sender, EventArgs e)
        {
            this.tabControl1.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabPage1.Width, this.tabPage1.Height));

            if (pVar.FrmCust_Receivd.dt_Main.Rows.Count > 0)
            {
                if (pVar.FrmCust_Receivd.radio1.Checked)
                {
                    switch (Common.User_DateTime)
                    {
                        case 1:
                            dataGridViewT1.Columns["收款日期西元"].Visible = false;
                            break;
                        case 2:
                            dataGridViewT1.Columns["收款日期民國"].Visible = false;
                            break;
                    }

                    reportfilename = "客戶別已收帳款_明細簡要表";

                    radio3.SetUserDefineRpt("客戶別已收帳款_明細簡要表_自定一.rpt");
                }
                else if (pVar.FrmCust_Receivd.radio2.Checked)
                {
                    reportfilename = "客戶別已收帳款_本幣總額表";
                }
                else if (pVar.FrmCust_Receivd.radio3.Checked)
                {
                    reportfilename = "客戶別已收帳款_幣別總額表";
                }
            }

            try
            {
                if (pVar.FrmCust_Receivd.dt_Main.Rows.Count > 0)
                {
                    if (pVar.FrmCust_Receivd.radio1.Checked)
                    {
                        if (pVar.FrmCust_Receivd.dt_Main.Rows.Count > 0)
                        {
                            list.Clear();
                            list = pVar.FrmCust_Receivd.dt_Main.AsEnumerable().ToList();
                        }
                        else
                        {
                            list.Clear();
                        }
                        dr = pVar.FrmCust_Receivd.dt_Main.Rows[0];
                        writeToText(tabPage1, "");
                        writeToDatagridview(getCurrentDataRow());
                        dataGridViewT1.ReadOnly = true;
                    }
                    else if (pVar.FrmCust_Receivd.radio2.Checked)
                    {

                        var p = pVar.FrmCust_Receivd.dt_Main.AsEnumerable();
                        沖帳總筆數1.Text = p.Sum(r => r["沖帳筆數"].ToDecimal()).ToString("f0");
                        折讓總金額1.Text = p.Sum(r => r["折讓金額"].ToDecimal()).ToString("f" + Common.MS);
                        現金總金額1.Text = p.Sum(r => r["現金金額"].ToDecimal()).ToString("f" + Common.M);
                        刷卡總金額1.Text = p.Sum(r => r["刷卡金額"].ToDecimal()).ToString("f" + Common.M);
                        禮券總金額1.Text = p.Sum(r => r["禮卷金額"].ToDecimal()).ToString("f" + Common.M);
                        支票總金額1.Text = p.Sum(r => r["支票金額"].ToDecimal()).ToString("f" + Common.M);
                        匯入總金額1.Text = p.Sum(r => r["匯入金額"].ToDecimal()).ToString("f" + Common.M);
                        其他總金額1.Text = p.Sum(r => r["其他金額"].ToDecimal()).ToString("f" + Common.M);
                        取用總金額1.Text = p.Sum(r => r["取用預收"].ToDecimal()).ToString("f" + Common.M);
                        沖帳總合計1.Text = p.Sum(r => r["沖帳合計"].ToDecimal()).ToString("f" + Common.M);
                        累入總金額1.Text = p.Sum(r => r["累入預收"].ToDecimal()).ToString("f" + Common.M);
                        沖抵總金額1.Text = p.Sum(r => r["沖抵帳款"].ToDecimal()).ToString("f" + Common.M);

                        dataGridViewT2.DataSource = pVar.FrmCust_Receivd.dt_Main;
                        dataGridViewT2.ReadOnly = true;
                    }
                    else if (pVar.FrmCust_Receivd.radio3.Checked)
                    {
                        dataGridViewT3.DataSource = pVar.FrmCust_Receivd.dt_Main;
                        dataGridViewT3.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            SetRdUdf();
        }

        void writeToText(TabPage TLP, string tbstr)
        {
            foreach (Control a in TLP.Controls)
            {
                if (a is TextBox)
                {
                    if (tbstr == "")
                    {
                        if(a is TextBoxT)
                            (a as TextBox).Text = dr[a.Name].ToString();
                        else
                            (a as TextBox).Text = dr[a.Name].ToDecimal().ToString("f" + (a as TextBoxNumberT).LastNum);
                    }
                    else
                    {
                        if (a is TextBoxT)
                            (a as TextBox).Text = dr[a.Name].ToString();
                        else
                            (a as TextBox).Text = dr[a.Name.Substring(0, a.Name.Length - 1)].ToDecimal().ToString("f" + (a as TextBoxNumberT).LastNum);
                    }
                }
            }
        }

        DataTable 撈支票與匯入資料2(DataTable t)
        {
            var reno = t.AsEnumerable().Select(r => r["reno"].ToString().Trim()).Distinct().ToList();
            DataTable chki = new DataTable();
            DataTable remiti = new DataTable();
            DataTable temp = new DataTable();
            string str = "";
            reno.ForEach(r => str += "N'" + r + "',");
            str = str.Substring(0, str.Length - 1);
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString.Replace("Initial Catalog=" + Common.logOnInfo.ConnectionInfo.DatabaseName, "Initial Catalog=" + Common.pathC.Trim())))
                {
                    cn.Open();
                    SqlDataAdapter dd;
                    string sql = "select * from chki where chstnum=N'" + Common.CoNo + "' and chstno in (" + str + ")";
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(chki);

                    sql = "select * from remiti where chstnum=N'" + Common.CoNo + "' and restno in (" + str + ")";
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(remiti);
                }
                if (t.Columns["類別"] == null)
                    t.Columns.Add("類別", typeof(string));
                if (t.Columns["憑證"] == null)
                    t.Columns.Add("憑證", typeof(string));
                if (t.Columns["到期日"] == null)
                    t.Columns.Add("到期日", typeof(string));
                if (t.Columns["帳戶簡稱"] == null)
                    t.Columns.Add("帳戶簡稱", typeof(string));
                if (t.Columns["金額"] == null)
                    t.Columns.Add("金額", typeof(decimal));
                t.AcceptChanges();

                DataRow row;
                bool no = false;
                temp = t.Copy();
                temp.Clear();

                for (int i = 0; i < t.Rows.Count; i++)
                {
                    no = false;
                    if (chki.AsEnumerable().ToList().Find(r => r["chstno"].ToString().Trim() == t.Rows[i]["reno"].ToString()) != null)
                    {
                        no = true;
                        var c = chki.AsEnumerable().Where(r => r["chstno"].ToString().Trim() == t.Rows[i]["reno"].ToString()).CopyToDataTable();
                        for (int j = 0; j < c.Rows.Count; j++)
                        {
                            row = t.Rows[i];
                            row["類別"] = "支票金額";
                            row["憑證"] = c.Rows[j]["chno"].ToString();
                            row["到期日"] = Common.User_DateTime == 1 ? Date.AddLine(c.Rows[j]["chdate2"].ToString()) : Date.AddLine(c.Rows[j]["chdate2_1"].ToString());
                            row["帳戶簡稱"] = c.Rows[j]["acname1"].ToString();
                            row["金額"] = c.Rows[j]["chmny"].ToString();
                            temp.ImportRow(row);
                            temp.AcceptChanges();
                        }
                    }
                    if (remiti.AsEnumerable().ToList().Find(r => r["restno"].ToString().Trim() == t.Rows[i]["reno"].ToString()) != null)
                    {
                        no = true;
                        var R = remiti.AsEnumerable().Where(r => r["restno"].ToString().Trim() == t.Rows[i]["reno"].ToString()).CopyToDataTable();
                        for (int j = 0; j < R.Rows.Count; j++)
                        {
                            row = t.Rows[i];
                            row["類別"] = "匯入金額";
                            row["憑證"] = R.Rows[j]["reno"].ToString();
                            row["到期日"] = Common.User_DateTime == 1 ? Date.AddLine(R.Rows[j]["redate"].ToString()) : Date.AddLine(R.Rows[j]["redate"].ToString());
                            row["帳戶簡稱"] = R.Rows[j]["acname1"].ToString();
                            row["金額"] = R.Rows[j]["remny"].ToString();
                            temp.ImportRow(row);
                            temp.AcceptChanges();
                        }
                    }
                    if (!no)
                    {
                        row = t.Rows[i];
                        temp.ImportRow(row);
                        temp.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return temp;
        }

        DataTable 撈支票與匯入資料(DataTable t)
        {
            var reno = t.AsEnumerable().Select(r => r["reno"].ToString().Trim()).Distinct().ToList();
            DataTable chki = new DataTable();
            DataTable remiti = new DataTable();
            DataTable temp = new DataTable();
            string str = "";
            reno.ForEach(r => str += "N'" + r + "',");
            str = str.Substring(0, str.Length - 1);
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString.Replace("Initial Catalog=" + Common.logOnInfo.ConnectionInfo.DatabaseName, "Initial Catalog=" + Common.pathC.Trim())))
                {
                    cn.Open();
                    SqlDataAdapter dd;
                    string sql = "select * from chki where chstnum=N'" + Common.CoNo + "' and chstno in (" + str + ")";
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(chki);

                    sql = "select * from remiti where chstnum=N'" + Common.CoNo + "' and restno in (" + str + ")";
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(remiti);
                }
                if (t.Columns["類別"] == null)
                    t.Columns.Add("類別", typeof(string));
                if (t.Columns["憑證"] == null)
                    t.Columns.Add("憑證", typeof(string));
                if (t.Columns["到期日"] == null)
                    t.Columns.Add("到期日", typeof(string));
                if (t.Columns["帳戶簡稱"] == null)
                    t.Columns.Add("帳戶簡稱", typeof(string));
                if (t.Columns["金額"] == null)
                    t.Columns.Add("金額", typeof(decimal));
                t.AcceptChanges();

                DataRow row;
                List<string> record = new List<string>();
                bool repeat = false;
                bool no = false;
                temp = t.Copy();
                temp.Clear();

                for (int i = 0; i < t.Rows.Count; i++)
                {
                    foreach (string element in record)
                    {
                        if (element == t.Rows[i]["reno"].ToString())
                        {
                            repeat = true;
                        }
                    }
                    if (repeat)
                    {
                        repeat = false;
                        continue;
                    }
                    record.Add(t.Rows[i]["reno"].ToString());
                    no = false;
                    if (chki.AsEnumerable().ToList().Find(r => r["chstno"].ToString().Trim() == t.Rows[i]["reno"].ToString()) != null)
                    {

                        no = true;
                        var c = chki.AsEnumerable().Where(r => r["chstno"].ToString().Trim() == t.Rows[i]["reno"].ToString()).CopyToDataTable();
                        for (int j = 0; j < c.Rows.Count; j++)
                        {
                            row = t.Rows[i];
                            row["類別"] = "支票金額";
                            row["憑證"] = c.Rows[j]["chno"].ToString();
                            row["到期日"] = Common.User_DateTime == 1 ? Date.AddLine(c.Rows[j]["chdate2"].ToString()) : Date.AddLine(c.Rows[j]["chdate2_1"].ToString());
                            row["帳戶簡稱"] = c.Rows[j]["acname1"].ToString();
                            row["金額"] = c.Rows[j]["chmny"].ToString();
                            temp.ImportRow(row);
                            temp.AcceptChanges();
                        }
                    }
                    if (remiti.AsEnumerable().ToList().Find(r => r["restno"].ToString().Trim() == t.Rows[i]["reno"].ToString()) != null)
                    {
                        no = true;
                        var R = remiti.AsEnumerable().Where(r => r["restno"].ToString().Trim() == t.Rows[i]["reno"].ToString()).CopyToDataTable();
                        for (int j = 0; j < R.Rows.Count; j++)
                        {
                            row = t.Rows[i];
                            row["類別"] = "匯入金額";
                            row["憑證"] = R.Rows[j]["reno"].ToString();
                            row["到期日"] = Common.User_DateTime == 1 ? Date.AddLine(R.Rows[j]["redate"].ToString()) : Date.AddLine(R.Rows[j]["redate"].ToString());
                            row["帳戶簡稱"] = R.Rows[j]["acname1"].ToString();
                            row["金額"] = R.Rows[j]["remny"].ToString();
                            temp.ImportRow(row);
                            temp.AcceptChanges();
                        }
                    }
                    if (!no)
                    {
                        row = t.Rows[i];
                        temp.ImportRow(row);
                        temp.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return temp;
        }

        void dataintodocument()
        {
            Common.FrmReport = new Report.Frmreport();

            ReportDocument oRpt = new ReportDocument();

            if (pVar.FrmCust_Receivd.dt.Rows.Count > 0 || pVar.FrmCust_Receivd.radio2.Checked || pVar.FrmCust_Receivd.radio3.Checked)
            {
                if (pVar.FrmCust_Receivd.radio1.Checked)
                {
                    if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_明細表.rpt");
                    else if (radio2.Checked)
                    {
                        if (Common.CoNo != "" && Common.pathC != "")
                            oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_簡要表(含支票).rpt");
                        else
                            oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_簡要表.rpt");
                    }
                    else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自定一.rpt");
                }
                else if (pVar.FrmCust_Receivd.radio2.Checked)
                {
                    oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_內定表.rpt");
                }
                else if (pVar.FrmCust_Receivd.radio3.Checked)
                {
                    oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_內定表.rpt");
                }
            }

            if (pVar.FrmCust_Receivd.radio1.Checked)
            {
                if (radio2.Checked && Common.pathC != "")
                    oRpt.SetDataSource(撈支票與匯入資料(pVar.FrmCust_Receivd.dt));
                else
                    oRpt.SetDataSource(pVar.FrmCust_Receivd.dt);
            }
            else
            {
                oRpt.SetDataSource(pVar.FrmCust_Receivd.dt_Main);
            }



            oRpt.Database.Tables[0].ApplyLogOnInfo(Common.logOnInfo);
            TextObject myFieldTitleName;
            //公司抬頭
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();
            }
            catch { }

            //帳款區間
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtrange"];
                myFieldTitleName.Text = pVar.FrmCust_Receivd.ReDateAcs.Text.ToString() + "~" + pVar.FrmCust_Receivd.ReDateAcs_1.Text.ToString();
            }
            catch { }
            //製表日期
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttoday"];
                switch (Common.User_DateTime)
                {
                    case 1:
                        myFieldTitleName.Text = Date.GetDateTime(1, true);
                        break;
                    case 2:
                        myFieldTitleName.Text = Date.GetDateTime(2, true);
                        break;
                }
            }
            catch { }
            //地址、電話傳真、三行註腳
            if (pVar.FrmCust_Receivd.radio1.Checked)
            {
                try
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                    myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcAddr1"].ToString();
                }
                catch { }
                try
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttel"];
                    myFieldTitleName.Text = "TEL：" + Common.dtSysSettings.Rows[0]["StcTel"].ToString()
                       + " FAX：" + Common.dtSysSettings.Rows[0]["StcTel"].ToString();
                }
                catch { }

                try
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (radio15.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                    else if (radio16.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                    else if (radio17.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                    else if (radio18.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                    else if (radio19.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                catch { }

            }
            if (pVar.FrmCust_Receivd.radio2.Checked)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                if (radio26.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (radio27.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (radio28.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (radio29.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (radio30.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else myFieldTitleName.Text = "";
            }
            else if (pVar.FrmCust_Receivd.radio3.Checked)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                if (radio37.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (radio38.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (radio39.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (radio40.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (radio41.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else myFieldTitleName.Text = "";
            }

            List<ParameterField> num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
            if (num.Find(p => p.Name == "顯示千分位") != null)
            {
                if (pVar.TRS != "")
                    pVar.ShowTRS = true;
                oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
            }
            if (num.Find(p => p.Name == "千分位") != null)
                oRpt.SetParameterValue("千分位", pVar.TRS);
            if (num.Find(p => p.Name == "銷貨單價小數") != null)
            {
                oRpt.SetParameterValue("銷貨單價小數", 銷貨單價小數.ToString());//銷貨單價小數
            }
            if (num.Find(p => p.Name == "銷貨單據小數") != null)
            {
                oRpt.SetParameterValue("銷貨單據小數", 銷貨單據小數.ToString());//銷貨單據小數
            }
            if (num.Find(p => p.Name == "銷項稅額小數") != null)
            {
                oRpt.SetParameterValue("銷項稅額小數", 銷項稅額小數.ToString());//銷項稅額小數
            }
            if (num.Find(p => p.Name == "本幣金額小數") != null)
            {
                oRpt.SetParameterValue("本幣金額小數", 本幣金額小數.ToString());//本幣金額小數
            }
            if (num.Find(p => p.Name == "庫存數量小數") != null)
            {
                oRpt.SetParameterValue("庫存數量小數", 庫存數量小數.ToString());//庫存數量小數
            }
            if (num.Find(p => p.Name == "日期") != null)
            {
                switch (Common.User_DateTime)
                {
                    case 1:
                        oRpt.SetParameterValue("日期", "民國");
                        break;
                    case 2:
                        oRpt.SetParameterValue("日期", "西元");
                        break;
                }
            }
            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument();
            Common.FrmReport.button1_Click(null, null);
            Common.FrmReport.Dispose();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            dataintodocument();
            Common.FrmReport.ShowDialog();
        }

        private void bteExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument();
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".doc");
            Process.Start(Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".doc");
            Common.FrmReport.Dispose();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument();
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls");
            Process.Start(Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls");
            Common.FrmReport.Dispose();
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {
            this.tabControl1.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabControl1.Width, this.tabControl1.Height));
        }
          
        private void tabControl1_Resize_1(object sender, EventArgs e)
        {
            this.tabControl1.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabControl1.Width, this.tabControl1.Height));
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                dr = list.First();
                writeToDatagridview(dr);
            }
            btnTop.Enabled = false;
            btnPrior.Enabled = false;
            btnNext.Enabled = true;
            btnBottom.Enabled = true;
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            current = 客戶編號.Text.Trim();
            dr = getCurrentDataRow();
            temp = list.IndexOf(dr);
            if (list.Count > 0)
            {
                dr = getCurrentDataRow(current);
                int i = list.IndexOf(dr);
                if (i == -1)
                {
                    if (temp == 0)
                    {
                        dr = list.First();
                        writeToDatagridview(dr);

                        MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnTop.Enabled = false;
                        btnPrior.Enabled = false;
                        btnNext.Enabled = true;
                        btnBottom.Enabled = true;
                        return;
                    }
                    else
                    {
                        dr = list[--temp];
                        writeToDatagridview(dr);
                        btnNext.Enabled = true;
                        btnBottom.Enabled = true;
                        return;
                    }
                }
                if (i > 0)
                {
                    dr = list[--i];
                    writeToDatagridview(dr);
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
                else
                {
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnTop.Enabled = false;
                    btnPrior.Enabled = false;
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            current = 客戶編號.Text.Trim();
            dr = getCurrentDataRow();
            temp = list.IndexOf(dr);
            if (list.Count > 0)
            {
                dr = getCurrentDataRow(current);
                int i = list.IndexOf(dr);
                if (i == -1)
                {
                    if (temp >= list.Count)
                    {
                        dr = list.Last();
                        writeToDatagridview(dr);
                        MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnTop.Enabled = true;
                        btnPrior.Enabled = true;
                        btnNext.Enabled = false;
                        btnBottom.Enabled = false;
                        return;
                    }
                    else
                    {
                        dr = list[++i];
                        writeToDatagridview(dr);
                        btnTop.Enabled = true;
                        btnPrior.Enabled = true;
                        return;
                    }
                }
                if (i < list.Count - 1)
                {
                    dr = list[++i];
                    writeToDatagridview(dr);
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                }
                else
                {
                    MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                    btnNext.Enabled = false;
                    btnBottom.Enabled = false;
                }
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                dr = list.Last();
                writeToDatagridview(dr);
            }
            btnTop.Enabled = true;
            btnPrior.Enabled = true;
            btnNext.Enabled = false;
            btnBottom.Enabled = false;
        }

        public void writeToDatagridview(DataRow tempdr)
        {
            IEnumerable<DataRow> query =
            from dt in pVar.FrmCust_Receivd.dt_Detail.AsEnumerable()
            where dt.Field<string>("客戶編號") == tempdr["客戶編號"].ToString()
            select dt;
            // Create a table from the query.
            if (query.Count() > 0)
            {
                dt_datagrid = query.CopyToDataTable<DataRow>();
                dataGridViewT1.DataSource = dt_datagrid;
            }
            else
            {
                dataGridViewT1.DataSource = null;
            }
            writeToText(tabPage1, "");
        }

        public DataRow getCurrentDataRow()
        {
            return list.Find(o => o.Field<string>("客戶編號") == (客戶編號.Text.Trim()));
        }

        public DataRow getCurrentDataRow(string s)
        {
            return list.Find(o => o.Field<string>("客戶編號") == (s));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridViewT2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (Control tb in tabPage2.Controls)
            {
                if (tb is TextBox)
                {
                    if (dataGridViewT2.SelectedRows.Count > 0)
                    {
                        foreach (DataRow dr in list)
                        {
                            if (dr["客戶編號"].ToString() == dataGridViewT2["客戶編號d2", dataGridViewT2.CurrentCell.RowIndex].Value.ToString())
                            {
                                tb.Text = dr[tb.Name.Substring(0, tb.Name.Length - 1)].ToString();
                            }
                        }

                    }
                }
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox5);
            pnlist.Add(groupBox10);
            pnlist.Add(groupBox7);
            pVar.SaveRadioUdf(pnlist, this.Name);
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox5);
            pnlist.Add(groupBox10);
            pnlist.Add(groupBox7);
            pVar.SetRadioUdf(pnlist, this.Name);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();
            
            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt_datagrid.Excel匯出並開啟(this.Text);
            }
            else if (keyData == Keys.Escape)
            {
                this.Dispose(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
         

    }
}
