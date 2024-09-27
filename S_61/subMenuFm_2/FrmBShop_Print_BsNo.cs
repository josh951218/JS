using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmBShop_Print_BsNo : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; } 
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>();
         
        public FrmBShop_Print_BsNo()
        {
            InitializeComponent();

            this.進貨總額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單價.DefaultCellStyle.Format = "f" + Common.MF;

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }

            this.進貨總額.Visible = Common.User_ShopPrice;
            this.單價.Visible = Common.User_ShopPrice;
        }

        private void FrmBShop_Print_BsNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            if (Common.User_DateTime == 1)
                this.進貨日期.DataPropertyName = "BsDate";
            else
                this.進貨日期.DataPropertyName = "BsDate1";

            loadDB();
            if (Master.Rows.Count > 0)
            {
                writeToTxt();

                int row = list.FindIndex(r => r["BsNo"].ToString() == this.TSeekNo);
                if (row == -1)
                    row = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["BsNo"].ToString()) > 0);
                row = (row == -1) ? 0 : row;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = row;
                dataGridViewT1.Rows[row].Selected = true;
            }
            BsNo.Focus();
        }
         
        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select x3name = "
                        + " case "
                        + " when x3no=1 then '外加稅'"
                        + " when x3no=2 then '內含稅'"
                        + " when x3no=3 then '零稅'"
                        + " when x3no=4 then '免稅'"
                        + " end,* from BShop order by BsNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        Master.Clear(); list.Clear();

                        da.Fill(Master);
                        if (Master.Rows.Count > 0)
                        {
                            list = Master.AsEnumerable().ToList(); 
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void writeToTxt()
        {
            if (Master.Rows.Count > 0)
            {
                dataGridViewT1.DataSource = Master;
                dataGridViewT2.DataSource = Detail;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "" && FaNo.Text.Trim() == "") return;

            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select x3name = "
                        + " case "
                        + " when x3no=1 then '外加稅'"
                        + " when x3no=2 then '內含稅'"
                        + " when x3no=3 then '零稅'"
                        + " when x3no=4 then '免稅'"
                        + " end,* from BShop where '0'='0'";
                    if (BsNo.Text.Trim() != "") str += " AND BsNo LIKE '%' + @bsno + '%' ";
                    if (FaNo.Text.Trim() != "") str += " AND FaNo LIKE '%' + @fano + '%' ";
                    foreach (Control tb in this.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).Enter += new EventHandler(txtonenter);
                        }
                    }
                    str += " order by BsNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (BsNo.Text.Trim() != "") cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                        if (FaNo.Text.Trim() != "") cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.CommandText = str;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        Master.Clear();
                        da.Fill(Master);
                        if (Master.Rows.Count > 0)
                        {
                            list.Clear();
                            list = Master.AsEnumerable().ToList();
                        }
                        else
                        {
                            list.Clear();
                        }
                        da.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                var TempID = dataGridViewT1.SelectedRows[0].Cells["進貨單號"].Value.ToString();
                 
                this.TResult = TempID.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtonenter(object sender, EventArgs e)
        {
            loadDB();
            foreach (Control tb in (sender as Control).Parent.Controls)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Text = "";
                    (tb as TextBox).Enter -= txtonenter;
                }
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6) btnQuery.PerformClick();
            else if (keyData == Keys.F9) btnGet.PerformClick();
            else if (keyData == Keys.F11) btnExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BsNo_TextChanged(object sender, EventArgs e)
        { 
            if (BsNo.Text.Trim() == "" && FaNo.Text.Trim() == "") return;

            loadDB();
            if (Master.Rows.Count < 1) return;
            writeToTxt();
            int i;
            if (list.Count > 0)
            {
                if (BsNo.Text.Trim() != "" && FaNo.Text.Trim() != "")
                {
                    i = list.FindIndex(r => r.Field<string>("BsNo").StartsWith(BsNo.Text.Trim()) && r.Field<string>("FaNo").StartsWith(FaNo.Text.Trim()));
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                    else
                    {
                        i = list.FindLastIndex(r => string.CompareOrdinal(BsNo.Text, r.Field<string>("BsNo")) > 0);
                        if (i == -1) i = 0;
                        var t = list.Find(r => r.Field<string>("BsNo") == BsNo.Text.Trim());
                        if (t != null) i = list.IndexOf(t);
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                }
                else if (BsNo.Text.Trim() != "" && FaNo.Text.Trim() == "")
                {
                    i = list.FindIndex(r => r.Field<string>("BsNo").StartsWith(BsNo.Text.Trim()));
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                    else
                    {
                        i = list.FindLastIndex(r => string.CompareOrdinal(BsNo.Text, r.Field<string>("BsNo")) > 0);
                        if (i == -1) i = 0;
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                }
                else if (BsNo.Text.Trim() == "" && FaNo.Text.Trim() != "")
                {
                    i = list.FindIndex(r => r.Field<string>("FaNo").StartsWith(FaNo.Text.Trim()));
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                    else
                    {
                        var v = list.FindAll(r => string.CompareOrdinal(FaNo.Text, r.Field<string>("FaNo")) > 0)
                            .OrderBy(r => r.Field<string>("FaNo"));
                        if (v.Count() > 0) i = list.IndexOf(v.Last());
                        if (i == -1) i = 0;
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                }
                else
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = list.Count - 1;
                    dataGridViewT1.Rows[list.Count - 1].Cells[0].Selected = true;
                }
            }
        }

        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.Rows.Count > 0)
                btnGet_Click(null, null);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                Detail.Clear();
                return;
            }
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                try
                {
                    string bsno = dataGridViewT1["進貨單號", e.Row.Index].Value.ToString();
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        string sql = "select * from BShopD where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("bsno", bsno.Trim());
                            cmd.CommandText = sql;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                Detail.Clear();
                                da.Fill(Detail);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
         





































































































    }
}
