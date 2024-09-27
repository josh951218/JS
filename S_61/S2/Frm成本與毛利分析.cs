using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class Frm成本與毛利分析 : Formbase
    {
        JBS.JS.xEvents xe;
        public DataTable dtD = new DataTable();
        public DataTable bom = new DataTable();
        bool 可各倉計算成本 = false;
        public string date = "";
        private string 單據日期 ="";

        public Frm成本與毛利分析(string _單據日期)
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            this.數量.Set庫存數量小數();
            this.稅前金額.Set本幣金額小數();
            this.成本.Set本幣金額小數();
            this.毛利.Set本幣金額小數();

            this.稅前金額.Visible = this.成本.Visible = this.毛利.Visible = Common.User_ShopPrice;
            textBoxT1.Visible = textBoxT2.Visible = textBoxT3.Visible = textBoxT4.Visible = Common.User_ShopPrice;
            this.單據日期 = _單據日期;
        }

        private void Frm成本與毛利分析_Load(object sender, EventArgs e)
        {
            //if (dtD.Columns.Contains("stno")) 可各倉計算成本 = true;
            //rdAvgByOneStk.Enabled = 可各倉計算成本;
            dataGridViewT1.DataSource = dtD;
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            if (rdAvgByAllStk.Checked)
            {
                //月平均成本
                設定月平均成本();
            }
            else if (rdAvgByOneStk.Checked)
            {
                //單倉庫存成本
                getCostOneStk();
            }
            else if (radioT2.Checked)
            {
                //最近一次進貨成本  
                設定最後一次進貨成本();
            }
            else if (radioT3.Checked)
            {
                //標準成本
                設定標準成本();
            }
            else if (radioT4.Checked)
            {
                //建檔
                string itno = "";
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    itno = dataGridViewT1["產品編號", i].Value.ToString();
                    DataRow row = Common.load("Check", "item", "itno", itno);

                    if (row["ItCostSlt"].ToDecimal() == 1) 取得月平均成本(i);
                    else if (row["ItCostSlt"].ToDecimal() == 2) 取得最後一次進貨成本(i);
                    else if (row["ItCostSlt"].ToDecimal() == 3) 取得標準成本(i);
                }
            }

            decimal totprice = 0;
            decimal totmny = 0;
            decimal fit = 0;
            decimal percent = 0;
            totprice = dataGridViewT1.Rows.OfType<DataGridViewRow>().Sum(r => r.Cells["稅前金額"].Value.ToDecimal());
            totmny = dataGridViewT1.Rows.OfType<DataGridViewRow>().Sum(r => r.Cells["成本"].Value.ToDecimal());
            fit = totprice - totmny;
            if (totprice == 0) percent = 0;
            else percent = (fit / totprice) * 100;

            textBoxT1.Text = totprice.ToString("f" + Common.MS);
            textBoxT2.Text = totmny.ToString("f" + Common.MS);
            textBoxT3.Text = fit.ToString("f" + Common.MS);
            textBoxT4.Text = percent.ToString("f2");
        }

        void 設定標準成本()
        {
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                取得標準成本(i);
            }
        }
        void 取得標準成本(int rowindex)
        {
            DataRow row = dtD.Rows[rowindex];
            var ittrait = row["ittrait"].ToDecimal();
            if (ittrait == 1)
            {
                var cost = 0M;
                var rec = row["bomrec"].ToString().Trim();
                bom.AsEnumerable()
                    .Where(r => r["bomrec"].ToString().Trim() == rec)
                    .Aggregate(0, (x, bomrow) =>
                    {
                        var itno = bomrow["itno"].ToString().Trim();
                        var itunit = bomrow["itunit"].ToString().Trim();
                        var qty = bomrow["itqty"].ToDecimal();
                        var itpkgqty = bomrow["itpkgqty"].ToDecimal();
                        var bitpareprs = bomrow["itpareprs"].ToDecimal();

                        xe.Validate<JBS.JS.Item>(itno, rw =>
                        {
                            if (rw["itunitp"].ToString() == itunit && rw["itpkgqty"].ToDecimal() == itpkgqty)
                            {
                                cost += rw["ItCostP"].ToDecimal() * qty;
                            }
                            else
                            {
                                cost += rw["ItCost"].ToDecimal() * (qty * itpkgqty / bitpareprs);
                            }
                        });
                        return x;
                    });
                dataGridViewT1["成本", rowindex].Value = row["qty"].ToDecimal() * row["itpkgqty"].ToDecimal() * cost;
                dataGridViewT1["毛利", rowindex].Value = dataGridViewT1["稅前金額", rowindex].Value.ToDecimal() - dataGridViewT1["成本", rowindex].Value.ToDecimal();
            }
            else
            {
                var itno = row["itno"].ToString().Trim();// dataGridViewT1["產品編號", i].Value.ToString();
                var itunit = row["itunit"].ToString();
                var qty = row["qty"].ToDecimal();
                var itpkgqty = row["itpkgqty"].ToDecimal();
                DataRow itrow = Common.load("Check", "item", "itno", itno);
                if (itrow["itunitp"].ToString() == itunit && itrow["itpkgqty"].ToDecimal() == itpkgqty)
                {
                    dataGridViewT1["成本", rowindex].Value = qty * itrow["ItCostP"].ToDecimal();
                }
                else
                {
                    dataGridViewT1["成本", rowindex].Value = itrow["ItCost"].ToDecimal() * qty * itpkgqty;
                }
                dataGridViewT1["毛利", rowindex].Value = dataGridViewT1["稅前金額", rowindex].Value.ToDecimal() - dataGridViewT1["成本", rowindex].Value.ToDecimal();
            }
        }

        void 設定最後一次進貨成本()
        {
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                取得最後一次進貨成本(i);
            }
        }
        void 取得最後一次進貨成本(int rowindex)
        {
            string bomid = "";
            decimal cost = 0;

            string bitno = "";
            string bitunit = "";
            decimal bitqty = 0;
            decimal bitpkgqty = 0;
            decimal bitpareprs = 0;
            DataRow dr = null;
            string str_SQL最後一次進價 = "select top 1 * from bshopd where itno =@itno and bsdate1 <= @bsdate order by bsdate1 desc";
            parameters par_SQL最後一次進價 = new parameters();

            cost = 0;
            if (dataGridViewT1["ittrait", rowindex].Value.ToDecimal() == 1)
            {
                //組合
                bomid = dataGridViewT1["bomid", rowindex].Value.ToString();
                var rows = bom.AsEnumerable().Where(r => r["bomid"].ToString() == bomid);
                if (rows.Count() > 0)
                {
                    DataTable tbom = rows.CopyToDataTable();
                    for (int j = 0; j < tbom.Rows.Count; j++)
                    {
                        bitno = tbom.Rows[j]["itno"].ToString();
                        bitunit = tbom.Rows[j]["itunit"].ToString();
                        bitqty = tbom.Rows[j]["itqty"].ToDecimal();
                        bitpkgqty = tbom.Rows[j]["itpkgqty"].ToDecimal();
                        bitpareprs = tbom.Rows[j]["itpareprs"].ToDecimal();

                        par_SQL最後一次進價.Clear();
                        par_SQL最後一次進價.AddWithValue("itno", bitno);
                        par_SQL最後一次進價.AddWithValue("bsdate", Date.ToUSDate(this.單據日期));
                        DataTable TempDt = new DataTable();
                        SQL.ExecuteNonQuery(str_SQL最後一次進價, par_SQL最後一次進價, TempDt);

                        if (TempDt.Rows.Count == 0)
                        {
                            //沒最近一次進貨，帶建檔進價
                            DataRow row = Common.load("Check", "item", "itno", bitno);
                            cost += bitqty / bitpareprs * bitpkgqty * row["ItBuyPri"].ToDecimal();
                        }
                        else//TempDt.Rows.Count > 0
                        {
                            dr = TempDt.Rows[0];
                            //有最近一次進貨。
                            if (bitunit == dr["itunit"].ToString() && bitpkgqty == dr["itpkgqty"].ToDecimal())
                            {
                                cost += (bitqty * dr["realcost"].ToDecimal()) / bitpareprs;
                            }
                            else
                            {
                                cost += (bitqty * bitpkgqty * (dr["realcost"].ToDecimal() / dr["itpkgqty"].ToDecimal())) / bitpareprs;
                            }
                        }
                    }
                }
                else cost += 0;
                cost = cost * dataGridViewT1["數量", rowindex].Value.ToDecimal() * dataGridViewT1["包裝數量", rowindex].Value.ToDecimal();
            }
            else
            {
                //單一組裝
                bitno = dataGridViewT1["產品編號", rowindex].Value.ToString();
                bitunit = dataGridViewT1["單位", rowindex].Value.ToString();
                bitqty = dataGridViewT1["數量", rowindex].Value.ToDecimal();
                bitpkgqty = dataGridViewT1["包裝數量", rowindex].Value.ToDecimal();
                par_SQL最後一次進價.Clear();
                par_SQL最後一次進價.AddWithValue("itno", bitno);
                par_SQL最後一次進價.AddWithValue("bsdate", Date.ToUSDate(this.單據日期));
                DataTable TempDt = new DataTable();
                SQL.ExecuteNonQuery(str_SQL最後一次進價, par_SQL最後一次進價, TempDt);
                if (TempDt.Rows.Count == 0)
                {
                    //沒有最近一次進貨
                    DataRow row = Common.load("Check", "item", "itno", bitno);
                    cost = bitqty * bitpkgqty * row["ItBuyPri"].ToDecimal();
                }
                else //TempDt.Rows.Count > 0
                {
                    dr = TempDt.Rows[0];
                    //有最近一次進貨。
                    if (bitunit == dr["itunit"].ToString() && bitpkgqty == dr["itpkgqty"].ToDecimal())
                    {
                        cost = (bitqty * dr["realcost"].ToDecimal());
                    }
                    else
                    {
                        cost = (bitqty * bitpkgqty * (dr["realcost"].ToDecimal() / dr["itpkgqty"].ToDecimal()));
                    }
                }
            }
            dataGridViewT1["成本", rowindex].Value = cost;
            dataGridViewT1["毛利", rowindex].Value = dataGridViewT1["稅前金額", rowindex].Value.ToDecimal() - dataGridViewT1["成本", rowindex].Value.ToDecimal();
        }

        void 設定月平均成本()
        {
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                取得月平均成本(i);
            }
        }
        void 取得月平均成本(int rowindex)
        {
            List<string> ltdate = new List<string>();
            for (int i = 1; i <= 12; i++)
                ltdate.Add(Common.Sys_StkYear1 + (i.ToString().PadLeft(2, '0')));
            for (int i = 1; i <= 12; i++)
                ltdate.Add(Common.Sys_StkYear1 + 1 + (i.ToString().PadLeft(2, '0')));

            string d = new string(Date.ToTWDate(date).Take(5).ToArray());
            var inYear = ltdate.IndexOf(d);
            if (inYear == -1)
            {
                MessageBox.Show("超過或低於庫存年度");
                return;
            }
            d = (ltdate.IndexOf(d) + 1).ToString().PadLeft(2, '0');

            DataTable dtCost = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                string sql = "select itno,avgcost" + d + " from itemcost ";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.Fill(dtCost);
                }
            }
            string itno = "";
            DataRow dr;
            if (dtD.Rows[rowindex]["ittrait"].ToDecimal() == 1)
            {
                var cost = 0M;
                var rec = dtD.Rows[rowindex]["bomrec"].ToString().Trim();
                var bitqty = 0M;
                var bitpkgqty = 0M;
                var bitpareprs = 0M;

                bom.AsEnumerable().Where(r => r["bomrec"].ToString().Trim() == rec).ToList().ForEach(r =>
                {
                    bitqty = r["itqty"].ToDecimal();
                    bitpkgqty = r["itpkgqty"].ToDecimal();
                    bitpareprs = r["itpareprs"].ToDecimal();
                    dr = dtCost.AsEnumerable().ToList().Find(t => t["itno"].ToString() == r["itno"].ToString().Trim());
                    if (dr != null)
                        cost += bitqty * bitpkgqty / bitpareprs * dr["avgcost" + d].ToDecimal();
                });

                dataGridViewT1["成本", rowindex].Value = dataGridViewT1["數量", rowindex].Value.ToDecimal()
                    * dataGridViewT1["包裝數量", rowindex].Value.ToDecimal()
                    * cost;

                dataGridViewT1["毛利", rowindex].Value = dataGridViewT1["稅前金額", rowindex].Value.ToDecimal() - dataGridViewT1["成本", rowindex].Value.ToDecimal();
            }
            else
            {
                itno = dataGridViewT1["產品編號", rowindex].Value.ToString();
                dr = dtCost.AsEnumerable().ToList().Find(r => r["itno"].ToString() == itno);
                if (dr != null)
                {
                    dataGridViewT1["成本", rowindex].Value = dataGridViewT1["數量", rowindex].Value.ToDecimal()
                        * dataGridViewT1["包裝數量", rowindex].Value.ToDecimal()
                        * dr["avgcost" + d].ToDecimal();
                }
                else
                {
                    dataGridViewT1["成本", rowindex].Value = 0;
                }
                dataGridViewT1["毛利", rowindex].Value = dataGridViewT1["稅前金額", rowindex].Value.ToDecimal() - dataGridViewT1["成本", rowindex].Value.ToDecimal();
            }
        }

        private void getCostOneStk()
        {
            List<string> ltdate = new List<string>();
            for (int i = 1; i <= 12; i++)
                ltdate.Add(Common.Sys_StkYear1 + (i.ToString().PadLeft(2, '0')));
            for (int i = 1; i <= 12; i++)
                ltdate.Add(Common.Sys_StkYear1 + 1 + (i.ToString().PadLeft(2, '0')));

            string d = new string(Date.ToTWDate(date).Take(5).ToArray());
            d = (ltdate.IndexOf(d) + 1).ToString().PadLeft(2, '0');

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("itno", "");
                cmd.Parameters.AddWithValue("stno", "");
                cmd.CommandText = "select avgcost" + d + " from stkcost where itno=@itno and stno=@stno";

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    cmd.Parameters["itno"].Value =  dataGridViewT1["產品編號", i].Value.ToString();
                    cmd.Parameters["stno"].Value = dtD.Rows[i]["stno"].ToString();

                    decimal cost = cmd.ExecuteScalar().ToDecimal();

                    dataGridViewT1["成本", i].Value = dataGridViewT1["數量", i].Value.ToDecimal() * cost;
                    dataGridViewT1["毛利", i].Value = dataGridViewT1["稅前金額", i].Value.ToDecimal() - dataGridViewT1["成本", i].Value.ToDecimal();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dtD.Clear();
            bom.Clear();

            base.OnFormClosing(e);
        }

        private void ClearEmptyItno(DataTable dataTable)
        {
            for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
            {
                if (dataTable.Rows[i]["itno"].ToString().Trim().Length == 0)
                    dataTable.Rows.RemoveAt(i);
            }
        }

    }
}
