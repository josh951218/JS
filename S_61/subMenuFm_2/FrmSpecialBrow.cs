using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSpecialBrow : Formbase
    {
        JBS.JS.xEvents xe;
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable dtD = new DataTable();
        List<ButtonSmallT> list;
        string SQL = "";
        decimal No = 0;
        decimal TotalCount = 0;

        public FrmSpecialBrow()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            cn.ConnectionString = Common.sqlConnString;
            list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6 };
            SpMemo.ReadOnly = tMemo.ReadOnly = tSpNo.ReadOnly = tSDate.ReadOnly = tEDate.ReadOnly = tItNo.ReadOnly = false;

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            lblT11.Text = Common.Sys_MemoUdf;

            this.數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.金額.Set銷貨單據小數();
            this.折數.FirstNum = 10;
            this.折數.LastNum = 0;
            this.紅利.DefaultCellStyle.Format = "f0";

            tSDate.SetDateLength();
            tEDate.SetDateLength();
            this.起始日.DataPropertyName = Common.User_DateTime == 1 ? "起始日期" : "起始日期1";
            this.終止日.DataPropertyName = Common.User_DateTime == 1 ? "終止日期" : "終止日期1";

            dataGridViewT1.DataSource = dtD.DefaultView;
        }

        private void FrmSpecialBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
+ " LEFT(SpecialD.SDate,3)+'/'+SUBSTRING(SpecialD.SDate,4,2)+'/'+RIGHT(SpecialD.SDate,2) 起始日期,"
+ " LEFT(SpecialD.SDate1,4)+'/'+SUBSTRING(SpecialD.SDate1,4,2)+'/'+RIGHT(SpecialD.SDate1,2) 起始日期1,"
+ " LEFT(SpecialD.EDate,3)+'/'+SUBSTRING(SpecialD.EDate,4,2)+'/'+RIGHT(SpecialD.EDate,2) 終止日期,"
+ " LEFT(SpecialD.EDate1,4)+'/'+SUBSTRING(SpecialD.EDate1,4,2)+'/'+RIGHT(SpecialD.EDate1,2) 終止日期1,"
                //+ " 產品組成= case when saled.ittrait=1 then '組合品' when saled.ittrait=2 then '組裝品' when saled.ittrait=3 then '單一商品' end,"
+ " Speciald.SpID, Speciald.SpNo, Speciald.SDate, Speciald.SDate1, Speciald.SDate2, Speciald.EDate, Speciald.EDate1, "
+ " Speciald.EDate2, Speciald.SpTrait, Speciald.SpTraitName, Speciald.ItUnit, Speciald.Itpkgqty, Speciald.Qty, "
+ " Speciald.Price, Speciald.ItPrice, Speciald.Point, Speciald.Prs, Speciald.Memo, Speciald.BomID, Speciald.BomRec, "
+ " Speciald.ReCordNo, Speciald.Sltflag, Speciald.Extflag, Speciald.ItDesp1, Speciald.ItDesp2, Speciald.ItDesp3, "
+ " Speciald.ItDesp4, Speciald.ItDesp5, Speciald.ItDesp6, Speciald.ItDesp7, Speciald.ItDesp8, Speciald.ItDesp9, "
+ " Speciald.ItDesp10, Speciald.Reason, Speciald.GroupID, Special.EmNo, Special.EmName, Special.SpMemo, item.itno, "
+ " item.itnoudf, item.itname, item.itstockqty"
+ " FROM              Speciald LEFT OUTER JOIN"
+ " Special ON Speciald.SpNo = Special.SpNo LEFT OUTER JOIN"
+ " item ON Speciald.ItNo = item.itno"
+ " WHERE (0 = 0)";

            dataGridViewT1.SuspendLayout();

            tSpNo.Text = this.TSeekNo;
            btnQuery_Click(null, null);
            tSpNo.Clear();

            dataGridViewT1.PerformLayout();

            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from Speciald ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["SpID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("SpID = " + No);

            if (index == -1) return;
            if (index >= dtD.Rows.Count) index = dtD.Rows.Count - 1;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        private void Q2_Click(object sender, EventArgs e)
        {
            GridSort("SpNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q2.ForeColor = Color.Red;
        }

        private void Q3_Click(object sender, EventArgs e)
        {
            GridSort("SDate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q3.ForeColor = Color.Red;
        }

        private void Q4_Click(object sender, EventArgs e)
        {
            GridSort("EDate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q4.ForeColor = Color.Red;
        }

        private void Q5_Click(object sender, EventArgs e)
        {
            GridSort("ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q5.ForeColor = Color.Red;
        }

        private void Q6_Click(object sender, EventArgs e)
        {
            GridSort("Memo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q6.ForeColor = Color.Red;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Enabled = false;
                if (tSpNo.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Cload(dtD, SQL, "Special.SpNo", tSpNo.Text.Trim());
                    }
                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "SpNo", tSpNo.Text.Trim());
                }
                else if (tSDate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(tSDate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "SpecialD.SDate", date);
                    }
                    Q3_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "SDate", date);
                }
                else if (tEDate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(tEDate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "SpecialD.EDate", date);
                    }
                    Q4_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EDate", date);
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "SpecialD.ItNo", tItNo.Text.Trim(), "SpecialD.SDate");
                    }
                    Q5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItNo", tItNo.Text.Trim());
                }
                else if (tMemo.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "SpecialD.Memo", tMemo.Text.Trim(), "SpecialD.SDate");
                    }
                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "Memo", tMemo.Text.Trim());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnQuery.Enabled = true;
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (e.Row.Index == dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected))
                {
                    var index = e.Row.Index;
                    ItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                    EmNo.Text = dtD.DefaultView[index]["EmNo"].ToString();
                    EmName.Text = dtD.DefaultView[index]["EmName"].ToString();
                    Stock.Text = dtD.DefaultView[index]["ItStockQty"].ToDecimal("f" + Common.Q).ToString();
                    SpMemo.Text = dtD.DefaultView[index]["SpMemo"].ToString();
                }
            }
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (var frm = new SOther.FrmPicture())
            {
                var row = Common.load("Check", "item", "itno", dtD.DefaultView[index]["ItNo"].ToString().Trim());
                frm.image = row["pic"];
                frm.ShowDialog();
            }
        }

        private void btnDesp_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(false, FormStyle.Mini))
            {
                frm.dr = dtD.DefaultView[index].Row;
                frm.ShowDialog();
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                btnStock.Focus();
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
                dataGridViewT1.Focus();
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0)
            {
                this.TResult = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) this.TResult = "";
                else
                {
                    this.TResult = dtD.DefaultView[index]["SpNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Sdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused) 
                return;

            xe.DateValidate(sender, e, true);
        }
         
        private void tItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void Memo_Validating(object sender, CancelEventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus(); return;
            }
            //更新資料庫
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@SpMemo", SpMemo.Text.Trim());
                    cmd.Parameters.AddWithValue("@SpNo", dtD.DefaultView[index]["SpNo"].ToString().Trim());
                    cmd.CommandText = " Update Special Set SpMemo = (@SpMemo) where SpNo = (@SpNo)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                btnQuery_Click(null, null);
            }
            else if (keyData == Keys.F6)
            {
                btnDesp_Click(null, null);
            }
            else if (keyData == Keys.F8)
            {
                btnStock_Click(null, null);
            }
            else if (keyData == Keys.F9)
            {
                btnGet.Focus();
                btnGet_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                Q2_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                Q3_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                Q4_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                Q5_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F6") && keyData.ToString().EndsWith("Shift"))
            {
                Q6_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
