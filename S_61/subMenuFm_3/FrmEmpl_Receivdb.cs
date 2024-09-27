using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Data;
using System.Data.SqlClient;

namespace S_61.subMenuFm_3
{
    public partial class FrmEmpl_Receivdb : Formbase
    {
        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();
        IEnumerable<IGrouping<string, DataRow>> GroupbyDate = null;
        IGrouping<string, DataRow> g = null;
        public string DateRange = "";

        public FrmEmpl_Receivdb()
        {
            InitializeComponent();

            this.折讓金額.DefaultCellStyle.Format = "f" + Common.M;
            this.現金金額.DefaultCellStyle.Format = "f" + Common.M;
            this.刷卡金額.DefaultCellStyle.Format = "f" + Common.M;
            this.禮卷金額.DefaultCellStyle.Format = "f" + Common.M;
            this.支票金額.DefaultCellStyle.Format = "f" + Common.M;
            this.匯入金額.DefaultCellStyle.Format = "f" + Common.M;
            this.其它金額.DefaultCellStyle.Format = "f" + Common.M;
            this.取用預收.DefaultCellStyle.Format = "f" + Common.M;
            this.沖帳合計.DefaultCellStyle.Format = "f" + Common.M;
            this.累入預收.DefaultCellStyle.Format = "f" + Common.M;
            this.沖抵帳款.DefaultCellStyle.Format = "f" + Common.M;
        }

        private void FrmEmpl_Receivdb_Load(object sender, EventArgs e)
        {
            radioT1.Checked = radioT4.Checked = true;

            this.收款日期.DataPropertyName = Common.User_DateTime == 1 ? "redate" : "redate1";

            radioT2.SetUserDefineRpt("業務別已收帳款自定一.rpt");

            GroupbyDate = from r in dt.AsEnumerable()
                          group r by r["EmNo"].ToString();

            g = GroupbyDate.FirstOrDefault();
            WriteToTitle(g);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void WriteToTitle(IGrouping<string, DataRow> grow)
        {
            if (grow.IsNotNull())
            {
                temp = grow.CopyToDataTable();
                dataGridViewT1.DataSource = temp;

                var p = temp.AsEnumerable();
                textBoxT1.Text = grow.Key;
                textBoxT2.Text = temp.Rows[0]["emname"].ToString();
                textBoxT3.Text = temp.Rows.Count.ToString();
                textBoxT4.Text = p.Sum(r => r["TotDisc"].ToDecimal()).ToString("f" + Common.M);

                textBoxT5.Text = p.Sum(r => r["CashMny"].ToDecimal()).ToString("f" + Common.M);
                textBoxT6.Text = p.Sum(r => r["CardMny"].ToDecimal()).ToString("f" + Common.M);
                textBoxT7.Text = p.Sum(r => r["Ticket"].ToDecimal()).ToString("f" + Common.M);
                textBoxT8.Text = p.Sum(r => r["CheckMny"].ToDecimal()).ToString("f" + Common.M);
                textBoxT9.Text = p.Sum(r => r["RemitMny"].ToDecimal()).ToString("f" + Common.M);

                textBoxT10.Text = p.Sum(r => r["OtherMny"].ToDecimal()).ToString("f" + Common.M);
                textBoxT11.Text = p.Sum(r => r["GetPrvAcc"].ToDecimal()).ToString("f" + Common.M);
                textBoxT12.Text = p.Sum(r => r["TotReve"].ToDecimal()).ToString("f" + Common.M);
                textBoxT13.Text = p.Sum(r => r["AddPrvAcc"].ToDecimal()).ToString("f" + Common.M);
                textBoxT14.Text = p.Sum(r => r["TotMny"].ToDecimal()).ToString("f" + Common.M);
            }
            else temp.Clear();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            g = GroupbyDate.FirstOrDefault();
            WriteToTitle(g);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == 0)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[--index];
                WriteToTitle(g);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == GroupbyDate.Count() - 1)
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[++index];
                WriteToTitle(g);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            g = GroupbyDate.LastOrDefault();
            WriteToTitle(g);
        }

        void OutReport(RptMode mode)
        {
            string path = "";
            if (radioT1.Checked)
            {
                if (Common.pathC != "")
                    path = Common.reportaddress + "Report\\業務別已收帳款簡要表(含支票).rpt";
                else
                    path = Common.reportaddress + "Report\\業務別已收帳款簡要表.rpt";
            }
            else if (radioT3.Checked)
            {
                path = Common.reportaddress + "Report\\業務別已收帳款總表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\業務別已收帳款自定一.rpt";
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

            RPT rp;
            if (radioT3.Checked)
            {
                DataTable temp = dt.Clone();
                var tempg = from r in dt.AsEnumerable()
                            group r by r["emno"].ToString().Trim();
                foreach (var em in tempg)
                {
                    DataRow rw = temp.NewRow();
                    rw["emno"] = em.Key;
                    rw["emname"] = em.FirstOrDefault()["emname"];

                    var emnos = dt.AsEnumerable().Where(r => r["emno"].ToString().Trim() == em.Key);

                    rw["TotDisc"] = emnos.Sum(r => r["TotDisc"].ToDecimal()).ToString("f" + Common.M);
                    rw["CashMny"] = emnos.Sum(r => r["CashMny"].ToDecimal()).ToString("f" + Common.M);
                    rw["CardMny"] = emnos.Sum(r => r["CardMny"].ToDecimal()).ToString("f" + Common.M);
                    rw["Ticket"] = emnos.Sum(r => r["Ticket"].ToDecimal()).ToString("f" + Common.M);
                    rw["CheckMny"] = emnos.Sum(r => r["CheckMny"].ToDecimal()).ToString("f" + Common.M);
                    rw["RemitMny"] = emnos.Sum(r => r["RemitMny"].ToDecimal()).ToString("f" + Common.M);
                    rw["OtherMny"] = emnos.Sum(r => r["OtherMny"].ToDecimal()).ToString("f" + Common.M);
                    rw["GetPrvAcc"] = emnos.Sum(r => r["GetPrvAcc"].ToDecimal()).ToString("f" + Common.M);
                    rw["TotReve"] = emnos.Sum(r => r["TotReve"].ToDecimal()).ToString("f" + Common.M);
                    rw["AddPrvAcc"] = emnos.Sum(r => r["AddPrvAcc"].ToDecimal()).ToString("f" + Common.M);
                    rw["TotMny"] = emnos.Sum(r => r["TotMny"].ToDecimal()).ToString("f" + Common.M);

                    temp.Rows.Add(rw);
                }
                rp = new RPT(temp, path);
            }
            else
            {
                if (Common.CoNo != "" && Common.pathC != "")
                    rp = new RPT(撈支票與匯入資料(dt), path);
                else
                    rp = new RPT(dt, path);
            }
            rp.office = "業務別已收帳款";
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
                rp.Mail("業務別已收帳款");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Mail);
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




































    }
}
