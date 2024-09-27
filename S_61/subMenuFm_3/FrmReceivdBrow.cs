using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmReceivdBrow : Formbase
    {
        JBS.JS.xEvents xe;
        public string Result { get; set; }
        DataTable table = new DataTable();
        List<DataRow> list = new List<DataRow>();
        DataRow dr;
        List<Button> query;
        string NO = "";

        DataTable CHKi = new DataTable();
        DataTable RemitI = new DataTable();
        public bool 票據錯誤 = false;
        public string 票據錯誤文字 = "";
        public string 票據連線字串 = "";
        public DataRow chksys;
        public DataRow scrit;

        public FrmReceivdBrow()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            #region 票據部分
            支票按鈕.Visible = 匯入按鈕.Visible = Common.pathC == "" ? false : true;
            #endregion

            pVar.SetMemoUdf(this.備註);
            query6.Text = "f6:" + Common.Sys_MemoUdf;

            query = new List<Button> { query2, query3, query4, query5, query6, queryD };

            foreach (Control i in this.Controls)
            {
                if (i is TextBoxNumberT)
                {
                    (i as TextBoxNumberT).FirstNum = (13 - 1 - Common.MST);
                    (i as TextBoxNumberT).LastNum = Common.MST;
                    //(i as TextBoxNumberT).NumNegative = true;
                    TotExgDiff.LastNum = Common.M;
                    (i as TextBoxNumberT).Text = "";
                    (i as TextBoxNumberT).Text = "0";
                }
            }

            this.折讓金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.沖帳金額.DefaultCellStyle.Format = "f" + Common.MST;
        }

        private void FrmCust_ReceivBrow_Load(object sender, EventArgs e)
        {

            if (Common.User_DateTime == 1)
                ReDate.MaxLength = 7;
            else
                ReDate.MaxLength = 8;

            load("reno,d.sano");
            if (list.Count > 0)
            {
                dr = list.Find(r => r["reno"].ToString() == SeekNo);
                if (dr != null)
                {
                    WriteToTxt(dr);
                    NO = dr["序號"].ToString().Trim();
                }
                else
                    NO = "1";
            }
            else
            {
                WriteToTxt(null);
            }

            SetButtonColor();
            SetSelectRow(NO);
            query2.ForeColor = Color.Red;

            ReNo.Focus();

        }

        void load(string str)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    int i = 0;
                    string sql = "select 日期="
                                           + " case"
                                           + " when " + Common.User_DateTime + "=1 then a.redate"
                                           + " when " + Common.User_DateTime + "=2 then a.redate1"
                                           + " end,序號='',a.cuname1 cname1, a.reno,a.cuno,"
                                           + " d.*,a.cashmny,a.cardmny,a.ticket,a.CheckMny,a.remitmny,a.othermny,a.GetPrvAcc,a.TotMny 沖帳合計,a.AddPrvAcc,a.TotReve 沖抵帳款,a.TotDisc,a.cardno,a.memo2 主檔備註"
                                           + " from receiv a left join receivd d on a.reno=d.reno order by d." + str + "";
                    SqlDataAdapter dd = new SqlDataAdapter(sql, cn);
                    table.Clear();
                    list.Clear();
                    dd.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        table.AsEnumerable().ToList().ForEach(r =>
                        {
                            r["日期"] = Date.AddLine(r["日期"].ToString());
                            r["序號"] = ++i;
                        });
                        list = table.AsEnumerable().ToList();
                    }
                    dataGridViewT1.DataSource = null;
                    dataGridViewT1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null) return;
            CashMny.Text = dr["CashMny"].ToDecimal().ToString("f" + Common.MST);
            CardMny.Text = dr["CardMny"].ToDecimal().ToString("f" + Common.MST);
            Ticket.Text = dr["Ticket"].ToDecimal().ToString("f" + Common.MST);
            CheckMny.Text = dr["CheckMny"].ToDecimal().ToString("f" + Common.MST);
            RemitMny.Text = dr["RemitMny"].ToDecimal().ToString("f" + Common.MST);
            OtherMny.Text = dr["OtherMny"].ToDecimal().ToString("f" + Common.MST);
            GetPrvAcc.Text = dr["GetPrvAcc"].ToDecimal().ToString("f" + Common.MST);
            TotMny.Text = dr["沖帳合計"].ToDecimal().ToString("f" + Common.MST);
            AddPrvAcc.Text = dr["AddPrvAcc"].ToDecimal().ToString("f" + Common.MST);
            TotReve.Text = dr["沖抵帳款"].ToDecimal().ToString("f" + Common.MST);
            TotDisc.Text = dr["TotDisc"].ToDecimal().ToString("f" + Common.MST);
            InvNo.Text = dr["InvNo"].ToString();
            應收總計.Text = dr["totmny"].ToDecimal().ToString("f" + Common.MST);
            未收金額.Text = dr["AcctMny"].ToDecimal().ToString("f" + Common.MST);
            TotExgDiff.Text = dr["ExgDiff"].ToDecimal().ToString("f" + Common.MST);
            ReMemo.Text = dr["主檔備註"].ToString();
            CardNo.Text = dr["CardNo"].ToString();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("cuno", dr["cuno"].ToString().Trim());
                    cmd.CommandText = "select CuAdvamt from cust where cuno=@cuno";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        CuAdvamt.Text = cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.MST);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void SetButtonColor()
        {
            query.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow(string NO)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == NO)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void queryD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            if (dataGridViewT1.SelectedRows[0].Cells["單據"].Value.ToString().Trim() == "期初") return;
            using (var frm = new FrmReceivdBrow_Detail())
            {
                frm.No = dataGridViewT1.SelectedRows[0].Cells["單據憑證"].Value.ToString().Trim();
                frm.單據 = dataGridViewT1.SelectedRows[0].Cells["單據"].Value.ToString().Trim();
                frm.ShowDialog();
            }
        }

        private void query2_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "reno,sano";
            SetButtonColor();
            query2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query3_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "日期,reno";
            SetButtonColor();
            query3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query4_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "sano";
            SetButtonColor();
            query4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query5_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "cuno asc ,日期 DESC,reno asc";
            SetButtonColor();
            query5.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query6_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "Memo1";
            SetButtonColor();
            query6.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnQuery.Enabled = false;
            if (CuNo.Text != "" && ReDate.Text != "")
            {
                load("CuNo,d.ReDate");
                table.DefaultView.Sort = "CuNo,ReDate";
                if (Common.User_DateTime == 1)
                {
                    dataGridViewT1.Search("客戶編號", CuNo.Text, "民國日期", ReDate.Text);
                }
                else
                {
                    dataGridViewT1.Search("客戶編號", CuNo.Text, "西元日期", ReDate.Text);
                }
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                query5.ForeColor = Color.Red;
                WriteToTxt(dr);
                table.DefaultView.Sort = "cuno asc ,redate DESC,reno asc";
                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }

            if (CuNo.Text != "")
            {
                load("CuNo");
                table.DefaultView.Sort = "CuNo";
                dataGridViewT1.Search("客戶編號", CuNo.Text);
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                WriteToTxt(dr);
                table.DefaultView.Sort = "cuno asc ,redate DESC,reno asc";
                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }

            if (ReDate.Text != "")
            {
                load("ReDate");
                table.DefaultView.Sort = "ReDate";
                if (Common.User_DateTime == 1)
                {
                    dataGridViewT1.Search("民國日期", ReDate.Text);
                }
                else
                {
                    dataGridViewT1.Search("西元日期", ReDate.Text);
                }
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                query3.ForeColor = Color.Red;
                WriteToTxt(dr);

                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }

            if (ReNo.Text != "")
            {
                load("ReNo");
                table.DefaultView.Sort = "ReNo";
                dataGridViewT1.Search("收款單號", ReNo.Text);
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                query2.ForeColor = Color.Red;
                WriteToTxt(dr);

                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }

            if (SaNo.Text != "")
            {
                load("SaNo");
                table.DefaultView.Sort = "SaNo";
                dataGridViewT1.Search("單據憑證", SaNo.Text);
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                query4.ForeColor = Color.Red;
                WriteToTxt(dr);

                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }
            btnQuery.Enabled = true;
            btnQuery.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) Result = "";
            else
                Result = dataGridViewT1.SelectedRows[0].Cells["收款單號"].Value.ToString().Trim();
            this.Dispose();
            this.Close();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            string num = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dr = list.Find(r => r["序號"].ToString() == num);
            WriteToTxt(dr);
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                query2.Focus();
                query2.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                query3.Focus();
                query3.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                query4.Focus();
                query4.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                query5.Focus();
                query5.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F6") && keyData.ToString().EndsWith("Shift"))
            {
                query6.Focus();
                query6.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F8") && keyData.ToString().EndsWith("Shift"))
            {
                queryD.Focus();
                queryD.PerformClick();
            }
            else if (keyData == Keys.F3)
            {
                btnQuery.Focus();
                btnQuery.PerformClick();
            }
            else if (keyData == Keys.F4)
            {
                btnExit.Focus();
                btnExit.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void ReDate_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == "" || btnExit.Focused) return;
            if (!tb.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void 支票按鈕_Click(object sender, EventArgs e)
        {
            if (票據錯誤)
            {
                MessageBox.Show(票據錯誤文字, "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                CHKi.Clear();
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    string sql = "select * from chki where chstnum=@chstnum and chstno=@chstno order by chno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("chstnum", Common.CoNo);
                        cmd.Parameters.AddWithValue("chstno", dataGridViewT1.SelectedRows[0].Cells["收款單號"].Value.ToString().Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(CHKi);
                        }
                    }
                }
                using (var frm = new 應收票據建檔())
                {
                    frm.tbM = CHKi.Copy();
                    frm.開窗模式 = true;
                    frm.chksys = chksys;
                    frm.scrit = scrit;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 匯入按鈕_Click(object sender, EventArgs e)
        {
            if (票據錯誤)
            {
                MessageBox.Show(票據錯誤文字, "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                RemitI.Clear();
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    string sql = "select * from remiti where chstnum=@chstnum and restno=@restno order by reno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("chstnum", Common.CoNo);
                        cmd.Parameters.AddWithValue("restno", dataGridViewT1.SelectedRows[0].Cells["收款單號"].Value.ToString().Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(RemitI);
                        }
                    }
                }
                using (var frm = new 帳款匯入作業())
                {
                    frm.tbM = RemitI.Copy();
                    frm.開窗模式 = true;
                    frm.chksys = chksys;
                    frm.scrit = scrit;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblT1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            var row = Common.load("Check", "receiv", "reno", dataGridViewT1.SelectedRows[0].Cells["收款單號"].Value.ToString().Trim());
            if (row != null)
            {
                using (SOther.會計編號開窗 frm = new SOther.會計編號開窗())
                {
                    frm.acno = row["acno"].ToString();
                    frm.accono = row["accono"].ToString();
                    frm.ShowDialog();
                }
            }
        }
    }
}
