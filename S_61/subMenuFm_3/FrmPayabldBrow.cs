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
    public partial class FrmPayabldBrow : Formbase
    {
        JBS.JS.xEvents xe;
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo { get; set; }

        DataTable table = new DataTable();
        List<DataRow> list = new List<DataRow>();
        DataRow dr;
        List<Button> query;
        string NO = "";

        DataTable CHKo = new DataTable();
        DataTable RemitO = new DataTable();
        public bool 票據錯誤 = false;
        public string 票據錯誤文字 = "";
        public string 票據連線字串 = "";
        public DataRow chksys;
        public DataRow scrit;

        public FrmPayabldBrow()
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
                    (i as TextBoxNumberT).LastNum = Common.MFT;
                    //(i as TextBoxNumberT).NumNegative = true;
                    TotExgDiff.LastNum = Common.MF;
                    (i as TextBoxNumberT).Text = "";
                    (i as TextBoxNumberT).Text = "0";
                }
            }

            this.折讓金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.沖帳金額.DefaultCellStyle.Format = "f" + Common.MFT;
        }

        private void FrmPayabldBrow_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
                PaDate.MaxLength = 7;
            else
                PaDate.MaxLength = 8;

            load("pano,d.bsno");
            if (list.Count > 0)
            {
                dr = list.Find(r => r["pano"].ToString() == this.TSeekNo);
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

            PaNo.Focus();
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
                        + " when " + Common.User_DateTime + "=1 then a.padate"
                        + " when " + Common.User_DateTime + "=2 then a.padate1"
                        + " end,序號='',a.faname1 fname1,a.pano ,a.fano ,"
                        + " d.*,a.cashmny,a.cardmny,a.ticket,a.CheckMny,a.remitmny,a.othermny,a.GetPrvAcc,a.TotMny 沖帳合計,a.AddPrvAcc,a.TotReve 沖抵帳款,a.TotDisc,a.cardno,a.memo2 主檔備註"
                        + " from payabl a left join payabld d on a.pano=d.pano order by d." + str + "";
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
            CashMny.Text = dr["CashMny"].ToDecimal().ToString("f" + Common.MFT);
            CardMny.Text = dr["CardMny"].ToDecimal().ToString("f" + Common.MFT);
            Ticket.Text = dr["Ticket"].ToDecimal().ToString("f" + Common.MFT);
            CheckMny.Text = dr["CheckMny"].ToDecimal().ToString("f" + Common.MFT);
            RemitMny.Text = dr["RemitMny"].ToDecimal().ToString("f" + Common.MFT);
            OtherMny.Text = dr["OtherMny"].ToDecimal().ToString("f" + Common.MFT);
            GetPrvAcc.Text = dr["GetPrvAcc"].ToDecimal().ToString("f" + Common.MFT);
            TotMny.Text = dr["沖帳合計"].ToDecimal().ToString("f" + Common.MFT);
            AddPrvAcc.Text = dr["AddPrvAcc"].ToDecimal().ToString("f" + Common.MFT);
            TotReve.Text = dr["沖抵帳款"].ToDecimal().ToString("f" + Common.MFT);
            TotDisc.Text = dr["TotDisc"].ToDecimal().ToString("f" + Common.MFT);
            InvNo.Text = dr["InvNo"].ToString();
            應收總計.Text = dr["totmny"].ToDecimal().ToString("f" + Common.MFT);
            未收金額.Text = dr["AcctMny"].ToDecimal().ToString("f" + Common.MFT);
            TotExgDiff.Text = dr["ExgDiff"].ToDecimal().ToString("f" + Common.MFT);
            ReMemo.Text = dr["主檔備註"].ToString();
            CardNo.Text = dr["CardNo"].ToString();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("fano", dr["fano"].ToString().Trim());
                    cmd.CommandText = "select FaPayAmt from fact where fano=@fano";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        CuAdvamt.Text = cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.MFT);
                    cmd.Dispose();
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
            table.DefaultView.Sort = "pano,bsno";
            SetButtonColor();
            query2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query3_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "日期,pano ";
            SetButtonColor();
            query3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query4_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "bsno";
            SetButtonColor();
            query4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query5_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "fano asc ,日期 DESC,pano asc";
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
            if (FaNo.Text != "" && PaDate.Text != "")
            {
                load("FaNo,d.PaDate");
                table.DefaultView.Sort = "FaNo,PaDate";
                if (Common.User_DateTime == 1)
                {
                    dataGridViewT1.Search("廠商編號", FaNo.Text, "民國日期", PaDate.Text);
                }
                else
                {
                    dataGridViewT1.Search("廠商編號", FaNo.Text, "西元日期", PaDate.Text);
                }
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                query5.ForeColor = Color.Red;
                WriteToTxt(dr);

                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }

            if (FaNo.Text != "")
            {
                load("FaNo");
                table.DefaultView.Sort = "FaNo";
                dataGridViewT1.Search("廠商編號", FaNo.Text);
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                WriteToTxt(dr);
                table.DefaultView.Sort = "fano asc ,padate DESC,pano asc";
                btnQuery.Enabled = true;
                btnQuery.Focus();

                return;
            }

            if (PaDate.Text != "")
            {
                load("PaDate");
                table.DefaultView.Sort = "PaDate";
                if (Common.User_DateTime == 1)
                {
                    dataGridViewT1.Search("民國日期", PaDate.Text);
                }
                else
                {
                    dataGridViewT1.Search("西元日期", PaDate.Text);
                }
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                query3.ForeColor = Color.Red;
                WriteToTxt(dr);

                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }

            if (PaNo.Text != "")
            {
                load("PaNo");
                table.DefaultView.Sort = "PaNo";
                dataGridViewT1.Search("付款單號", PaNo.Text);
                dr = list.Find(r => r["序號"].ToString() == dataGridViewT1.SelectedRows[0].Cells["序號"].ToString());
                SetButtonColor();
                query2.ForeColor = Color.Red;
                WriteToTxt(dr);

                btnQuery.Enabled = true;
                btnQuery.Focus();
                return;
            }

            if (BsNo.Text != "")
            {
                load("BsNo");
                table.DefaultView.Sort = "BsNo";
                dataGridViewT1.Search("單據憑證", BsNo.Text);
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
            if (dataGridViewT1.Rows.Count == 0) this.TResult = "";
            else
                this.TResult = dataGridViewT1.SelectedRows[0].Cells["付款單號"].Value.ToString().Trim();
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

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
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


        private void PaDate_Validating(object sender, CancelEventArgs e)
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
                CHKo.Clear();
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    string sql = "select * from chko where chstnum=@chstnum and chstno=@chstno order by chno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("chstnum", Common.CoNo);
                        cmd.Parameters.AddWithValue("chstno", dataGridViewT1.SelectedRows[0].Cells["付款單號"].Value.ToString().Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(CHKo);
                        }
                    }
                }
                using (var frm = new 應付票據建檔())
                {
                    frm.tbM = CHKo.Copy();
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
                RemitO.Clear();
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    string sql = "select * from remito where chstnum=@chstnum and restno=@restno order by reno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("chstnum", Common.CoNo);
                        cmd.Parameters.AddWithValue("restno", dataGridViewT1.SelectedRows[0].Cells["付款單號"].Value.ToString().Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(RemitO);
                        }
                    }
                }
                using (var frm = new 帳款匯出作業())
                {
                    frm.tbM = RemitO.Copy();
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
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var row = Common.load("Check", "payabl", "pano", dataGridViewT1.SelectedRows[0].Cells["付款單號"].Value.ToString().Trim());
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
