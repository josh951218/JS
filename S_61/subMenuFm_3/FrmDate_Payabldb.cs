using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmDate_Payabldb : Formbase
    {
        public DataTable dt = new DataTable();
        public DataTable rt = new DataTable();
        public DataTable report = new DataTable();
        public string DateRange = "";
        DataRow dr = null;

        public FrmDate_Payabldb()
        {
            InitializeComponent();

            this.折讓金額.DefaultCellStyle.Format = "f" + Common.M;
            this.現金金額.DefaultCellStyle.Format = "f" + Common.M;
            this.刷卡金額.DefaultCellStyle.Format = "f" + Common.M;
            this.禮卷金額.DefaultCellStyle.Format = "f" + Common.M;
            this.支票金額.DefaultCellStyle.Format = "f" + Common.M;
            this.匯出金額.DefaultCellStyle.Format = "f" + Common.M;
            this.其它金額.DefaultCellStyle.Format = "f" + Common.M;
            this.取用預付.DefaultCellStyle.Format = "f" + Common.M;
            this.沖帳合計.DefaultCellStyle.Format = "f" + Common.M;
            this.累入預付.DefaultCellStyle.Format = "f" + Common.M;
            this.沖抵帳款.DefaultCellStyle.Format = "f" + Common.M;
        }

        private void FrmDate_Payabldb_Load(object sender, EventArgs e)
        {
            radioT2.Checked = radioT4.Checked = true;

            this.付款日期.DataPropertyName = Common.User_DateTime == 1 ? "padate" : "padate1";
            dataGridViewT1.DataSource = dt;

            radioT3.SetUserDefineRpt("日期別已付帳款簡要自定一.rpt");
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count > 0)
            {
                if (e.StateChanged == DataGridViewElementStates.Selected)
                {
                    dr = dt.Rows[e.Row.Index];
                    WriteToTitle(dr);
                }
            }
        }

        void WriteToTitle(DataRow dr)
        {
            if (dr.IsNotNull())
            {
                textBoxT1.Text = dt.Rows.Count.ToString();
                textBoxT2.Text = dr["折讓總"].ToDecimal().ToString("f" + Common.M);
                textBoxT3.Text = dr["現金總"].ToDecimal().ToString("f" + Common.M);
                textBoxT4.Text = dr["刷卡總"].ToDecimal().ToString("f" + Common.M);
                textBoxT5.Text = dr["禮卷總"].ToDecimal().ToString("f" + Common.M);
                textBoxT6.Text = dr["支票總"].ToDecimal().ToString("f" + Common.M);
                textBoxT7.Text = dr["匯出總"].ToDecimal().ToString("f" + Common.M);
                textBoxT8.Text = dr["其它總"].ToDecimal().ToString("f" + Common.M);
                textBoxT9.Text = dr["取用總"].ToDecimal().ToString("f" + Common.M);
                textBoxT10.Text = dr["沖帳總"].ToDecimal().ToString("f" + Common.M);
                textBoxT11.Text = dr["累入總"].ToDecimal().ToString("f" + Common.M);
                textBoxT12.Text = dr["沖抵總"].ToDecimal().ToString("f" + Common.M);
            }
            else
            {
                var p = this.Controls.OfType<TextBox>();
                foreach (var item in p)
                {
                    item.Clear();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        DataTable 撈支票與匯出資料(DataTable t)
        {
            var pano = t.AsEnumerable().Select(r => r["pano"].ToString().Trim()).Distinct().ToList();
            DataTable chko = new DataTable();
            DataTable remito = new DataTable();
            DataTable temp = new DataTable();
            string str = "";
            pano.ForEach(r => str += "N'" + r + "',");
            str = str.Substring(0, str.Length - 1);
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString.Replace("Initial Catalog=" + Common.logOnInfo.ConnectionInfo.DatabaseName, "Initial Catalog=" + Common.pathC.Trim())))
                {
                    cn.Open();
                    SqlDataAdapter dd;
                    string sql = "select * from chko where chstnum=N'" + Common.CoNo + "' and chstno in (" + str + ")";
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(chko);

                    sql = "select * from remito where chstnum=N'" + Common.CoNo + "' and restno in (" + str + ")";
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(remito);
                }
                t.Columns.Add("類別", typeof(string));
                t.Columns.Add("憑證", typeof(string));
                t.Columns.Add("到期日", typeof(string));
                t.Columns.Add("帳戶簡稱", typeof(string));
                t.Columns.Add("金額", typeof(decimal));
                t.AcceptChanges();

                DataRow row;
                bool no = false;
                temp = t.Copy();
                temp.Clear();

                for (int i = 0; i < t.Rows.Count; i++)
                {
                    no = false;
                    if (chko.AsEnumerable().ToList().Find(r => r["chstno"].ToString().Trim() == t.Rows[i]["pano"].ToString()) != null)
                    {
                        no = true;
                        var c = chko.AsEnumerable().Where(r => r["chstno"].ToString().Trim() == t.Rows[i]["pano"].ToString()).CopyToDataTable();
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
                    if (remito.AsEnumerable().ToList().Find(r => r["restno"].ToString().Trim() == t.Rows[i]["pano"].ToString()) != null)
                    {
                        no = true;
                        var R = remito.AsEnumerable().Where(r => r["restno"].ToString().Trim() == t.Rows[i]["pano"].ToString()).CopyToDataTable();
                        for (int j = 0; j < R.Rows.Count; j++)
                        {
                            row = t.Rows[i];
                            row["類別"] = "匯出金額";
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

        void OutReport(RptMode mode)
        {
            string path = "";
            if (radioT1.Checked)
            {
                report = rt.Copy();
                path = Common.reportaddress + "Report\\日期別已付帳款明細表.rpt";
            }
            else if (radioT2.Checked)
            {
                if (Common.CoNo != "" && Common.pathC != "")
                {
                    report = 撈支票與匯出資料(dt.Copy());
                    path = Common.reportaddress + "Report\\日期別已付帳款簡要表(含支票).rpt";
                }
                else
                {
                    report = dt.Copy();
                    path = Common.reportaddress + "Report\\日期別已付帳款簡要表.rpt";
                }
            }
            else if (radioT3.Checked)
            {
                report = dt.Copy();
                path = Common.reportaddress + "Report\\日期別已付帳款簡要自定一.rpt";
            }

            string txtend = "";
            if (radioT4.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            var rp = new RPT(report, path);
            rp.office = "日期別已付帳款";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
            rp.lobj.Add(new string[] { "DateCreated", p });
            rp.lobj.Add(new string[] { "Xa1No", "幣    別：新臺幣" });

            if (mode == RptMode.Word)
                rp.Word();
            else if (mode == RptMode.Excel)
                rp.Excel();
            else if (mode == RptMode.PreView)
                rp.PreView();
            else if (mode == RptMode.Print)
                rp.Print();
            else if (mode == RptMode.Mail)
                rp.Mail("日期別已付帳款");
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Mail);
        }
    }
}
