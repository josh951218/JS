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
    public partial class FrmRSale_Print_SaNo : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>(); 

        public FrmRSale_Print_SaNo()
        {
            InitializeComponent();

            this.銷退總額.DefaultCellStyle.Format = "f" + Common.MST;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單價.DefaultCellStyle.Format = "f" + Common.MS;

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }

            this.銷退總額.Visible = Common.User_SalePrice;
            this.單價.Visible = Common.User_SalePrice;
        }

        private void FrmRSale_Print_SaNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            if (Common.User_DateTime == 1)
                this.銷退日期.DataPropertyName = "SaDate";
            else
                this.銷退日期.DataPropertyName = "SaDate1";

            loadDB();
            if (Master.Rows.Count > 0)
            {
                writeToTxt();

                int row = list.FindIndex(r => r["SaNo"].ToString() == this.TSeekNo);
                if (row == -1)
                    row = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["SaNo"].ToString()) > 0);
                row = (row == -1) ? 0 : row;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = row;
                dataGridViewT1.Rows[row].Selected = true;
            }
            SaNo.Focus();
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
                        + " end,* from RSale order by SaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        Master.Clear(); list.Clear();

                        da.Fill(Master);
                        if (Master.Rows.Count > 0)
                        {
                            list = Master.AsEnumerable().ToList();
                            //lblT1.Text = "總共有 " + list.Count.ToString() + " 筆資料";
                        }
                        //else
                        //    lblT1.Text = "";
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
            if (SaNo.Text.Trim() == "" && CuNo.Text.Trim() == "") return;

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
                       + " end,* from RSale WHERE 1=1";
                    if (SaNo.Text.Trim() != "") str += " AND SaNo LIKE '%' + @sano + '%' ";
                    if (CuNo.Text.Trim() != "") str += " AND CuNo LIKE '%' + @cuno + '%' ";
                    foreach (Control tb in this.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).Enter += new EventHandler(txtonenter);
                        }
                    }
                    str += " order by SaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (SaNo.Text.Trim() != "") cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                        if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
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
                var TempID = dataGridViewT1.SelectedRows[0].Cells["銷退單號"].Value.ToString();
                  
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

        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.Rows.Count > 0)
                btnGet_Click(null, null);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count < 1)
            {
                Detail.Clear();
                return;
            }
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                try
                {
                    string sano = dataGridViewT1.SelectedRows[0].Cells["銷退單號"].Value.ToString();
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        string sql = "select * from RSaleD where sano=@sano COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("sano", sano.Trim());
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

        private void SaNo_TextChanged(object sender, EventArgs e)
        {
            if (SaNo.Text.Trim() == "" && CuNo.Text.Trim() == "") return;

            loadDB();
            if (Master.Rows.Count < 1) return;
            writeToTxt();
            int i;
            if (list.Count > 0)
            {
                if (SaNo.Text.Trim() != "" && CuNo.Text.Trim() != "")
                {
                    i = list.FindIndex(r => r.Field<string>("SaNo").StartsWith(SaNo.Text.Trim()) && r.Field<string>("CuNo").StartsWith(CuNo.Text.Trim()));
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                    else
                    {
                        i = list.FindLastIndex(r => string.CompareOrdinal(SaNo.Text, r.Field<string>("SaNo")) > 0);
                        if (i == -1) i = 0;
                        var t = list.Find(r => r.Field<string>("SaNo") == SaNo.Text.Trim());
                        if (t != null) i = list.IndexOf(t);
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                }
                else if (SaNo.Text.Trim() != "" && CuNo.Text.Trim() == "")
                {
                    i = list.FindIndex(r => r.Field<string>("SaNo").StartsWith(SaNo.Text.Trim()));
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                    else
                    {
                        i = list.FindLastIndex(r => string.CompareOrdinal(SaNo.Text, r.Field<string>("SaNo")) > 0);
                        if (i == -1) i = 0;
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                }
                else if (SaNo.Text.Trim() == "" && CuNo.Text.Trim() != "")
                {
                    i = list.FindIndex(r => r.Field<string>("CuNo").StartsWith(CuNo.Text.Trim()));
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    }
                    else
                    {
                        var v = list.FindAll(r => string.CompareOrdinal(CuNo.Text, r.Field<string>("CuNo")) > 0)
                            .OrderBy(r => r.Field<string>("CuNo"));
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























    }
}
