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

namespace S_61.S2
{
    public partial class FrmRBorrowBrowNew : Formbase
    {
        JBS.JS.xEvents xe;

        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }

        DataTable dtD = new DataTable();
        List<ButtonSmallT> list;
        string SQL = "";
        decimal No = 0;
        decimal TotalCount = 0;
        bool IsQuery;
        decimal Query = 0;

        public FrmRBorrowBrowNew()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT19.Text = Common.Sys_MemoUdf;

            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.FirstNum = 9;
            this.本幣稅前單價.LastNum = 6;
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";
            this.進價.Set進貨單價小數();
            this.稅前單價.FirstNum = 9;
            this.稅前單價.LastNum = 6;
            this.稅前單價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set銷項金額小數();
            this.還出數量.Set庫存數量小數();
            this.還出日期.DataPropertyName = Common.User_DateTime == 1 ? "還出日期" : "還出日期1";
            this.tBodate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

            dataGridViewT1.ReadOnly = false;
            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name != this.備註說明.Name) dataGridViewT1.Columns[i].ReadOnly = true;
            }

            dataGridViewT1.DataSource = dtD.DefaultView;

            this.本幣單價.Visible = Common.User_ShopPrice;
            this.本幣稅前金額.Visible = Common.User_ShopPrice;
            this.本幣稅前單價.Visible = Common.User_ShopPrice;
            this.進價.Visible = Common.User_ShopPrice;
            this.稅前單價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
            textBoxT1.Visible = textBoxT2.Visible = textBoxT3.Visible = textBoxT5.Visible = Common.User_ShopPrice;
        }

        private void FrmBorrowBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
+ " LEFT(RBorrD.bodate,3)+'/'+SUBSTRING(RBorrD.bodate,4,2)+'/'+RIGHT(RBorrD.bodate,2) 還出日期,"
+ " LEFT(RBorrD.bodate1,4)+'/'+SUBSTRING(RBorrD.bodate1,5,2)+'/'+RIGHT(RBorrD.bodate1,2) 還出日期1,"
+ " 產品組成= case when RBorrD.ittrait=1 then '組合品' when RBorrD.ittrait=2 then '組裝品' when RBorrD.ittrait=3 then '單一商品' end,"
+ " RBorrD.boid, RBorrD.bono, RBorrD.bodate, RBorrD.bodate1, RBorrD.bodate2, RBorrD.cono, RBorrD.fano, RBorrD.stno, RBorrD.emno, "
+ " RBorrD.xa1no, RBorrD.xa1par, RBorrD.itno, RBorrD.itname, RBorrD.ittrait, RBorrD.itunit, RBorrD.itpkgqty, RBorrD.qty, RBorrD.price, "
+ " RBorrD.prs, RBorrD.rate, RBorrD.taxprice, RBorrD.mny, RBorrD.priceb, RBorrD.taxpriceb, RBorrD.mnyb, RBorrD.memo, RBorrD.lowzero,"
+ " RBorrD.bomid, RBorrD.bomrec, RBorrD.recordno AS recordno1, RBorrD.sltflag, RBorrD.extflag, RBorrD.itdesp1, RBorrD.itdesp2, "
+ " RBorrD.itdesp3, RBorrD.itdesp4, RBorrD.itdesp5, RBorrD.itdesp6, RBorrD.itdesp7, RBorrD.itdesp8, RBorrD.itdesp9, RBorrD.itdesp10, "
+ " RBorrD.stname, RBorrD.stnoo, RBorrD.stnameo, RBorr.faname1, RBorr.faname2, RBorr.fatel1, RBorr.faper1, RBorr.emname, "
+ " RBorr.xa1name, RBorr.taxmnyf, RBorr.taxmny, RBorr.taxmnyb, RBorr.x3no, RBorr.tax, RBorr.totmny, RBorr.taxb, RBorr.totmnyb, "
+ " RBorr.recordno, RBorr.bomemo, RBorr.bomemo1, RBorr.AppScNo, RBorr.AppDate, RBorr.EdtScNo, RBorr.EdtDate, item.itnoudf, "
+ " item.itstockqty "
+ " FROM  RBorrD LEFT OUTER JOIN RBorr ON RBorrD.bono = RBorr.bono "
+ "             LEFT OUTER JOIN item ON RBorrD.itno = item.itno "
+ " WHERE (0 = 0)";

            tBoNo.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            if (Query == 0) IsQuery = false;
            Query++;
            tBoNo.Clear();

            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from RBorrD ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["BoID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("BoID = " + No);

            if (index == -1) return;
            if (index >= dtD.Rows.Count) index = dtD.Rows.Count - 1;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        private void Q2_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q2.Enabled = false;
                tBoNo.Text = dtD.DefaultView[index]["BoNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tBoNo.Clear();
            }
            GridSort("BoNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q2.ForeColor = Color.Red;
            Q2.Enabled = true;
        }

        private void Q3_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q3.Enabled = false;
                tFaNo.Text = dtD.DefaultView[index]["FaNo"].ToString();
                tBodate.Text = dtD.DefaultView[index]["bodate"].ToString();
                btnQuery_Click(btnQuery, null);
                tFaNo.Clear(); tBodate.Clear();
            }
            GridSort("bodate DESC,FaNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q3.ForeColor = Color.Red;
            Q3.Enabled = true;
        }

        private void Q4_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q4.Enabled = false;
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tItNo.Clear();
            }
            GridSort("ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q4.ForeColor = Color.Red;
            Q4.Enabled = true;
        }

        private void Q5_Click(object sender, EventArgs e)
        {
            if (tItNo.TrimTextLenth() == 0 && tFaNo.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q5.Enabled = false;
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                tFaNo.Text = dtD.DefaultView[index]["FaNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tItNo.Clear(); tFaNo.Clear();
            }
            GridSort("FaNo ASC,ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q5.ForeColor = Color.Red;
            Q5.Enabled = true;
        }

        private void Q6_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q6.Enabled = false;
                tStNo.Text = dtD.DefaultView[index]["StNo"].ToString();
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tStNo.Clear(); tItNo.Clear();
            }
            GridSort("StNo ASC,ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q6.ForeColor = Color.Red;
            Q6.Enabled = true;
        }

        private void Q7_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q7.Enabled = false;
                tEmNo.Text = dtD.DefaultView[index]["EmNo"].ToString();
                tBodate.Text = dtD.DefaultView[index]["bodate"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear(); tBodate.Clear();
            }
            GridSort("EmNo ASC,bodate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q7.ForeColor = Color.Red;
            Q7.Enabled = true;
        }

        private void Q8_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q8.Enabled = false;
                tMemo.Text = dtD.DefaultView[index]["Memo"].ToString();
                btnQuery_Click(btnQuery, null);
                tMemo.Clear();
            }
            GridSort("Memo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q8.ForeColor = Color.Red;
            Q8.Enabled = true;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                IsQuery = true;
                btnQuery.Enabled = false;

                if (tFaNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorrD.FaNo", tFaNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q5_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tStNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorrD.StNo", tStNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", tStNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0 && tBodate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tBodate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorr.EmNo", tEmNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "Bodate", date);
                }
                else if (tBoNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Eload(dtD, SQL, "RBorr.BoNo", tBoNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "BoNo", tBoNo.Text.Trim());
                }
                else if (tBodate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tBodate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "RBorrD.Bodate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q3_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "Bodate", date);
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorrD.ItNo", tItNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q4_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItNo", tItNo.Text.Trim());
                }
                else if (tMemo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorrD.Memo", tMemo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q8_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "Memo", tMemo.Text.Trim());
                }
                else if (tStNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorrD.StNo", tStNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", tStNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorr.EmNo", tEmNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim());
                }
                else if (tFaNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorr.FaNo", tFaNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim());
                }
                else if (tUnit.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RBorr.FaNo", tFaNo.Text.Trim(), "RBorrD.Bodate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    GridSort("ItUnit ASC,ItNo ASC");
                    list.ForEach(t => t.ForeColor = Color.Black);
                    Q4.ForeColor = Color.Red;
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItUnit", tUnit.Text.Trim());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsQuery = false;
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

                    textBoxT1.Text = dtD.DefaultView[index]["TaxMny"].ToDecimal().ToString("f" + Common.MFT);
                    textBoxT2.Text = dtD.DefaultView[index]["Tax"].ToDecimal().ToString("f" + Common.TF);
                    textBoxT3.Text = dtD.DefaultView[index]["TotMny"].ToDecimal().ToString("f" + Common.MFT);
                    textBoxT4.Text = dtD.DefaultView[index]["BoMemo"].ToString();
                    pVar.XX03Validate(dtD.DefaultView[index]["X3No"].ToString(), new TextBox(), textBoxT5);
                    textBoxT6.Text = dtD.DefaultView[index]["Xa1No"].ToString();
                    textBoxT7.Text = dtD.DefaultView[index]["Xa1Name"].ToString();
                    textBoxT8.Text = dtD.DefaultView[index]["Xa1Par"].ToDecimal().ToString("f4");
                    textBoxT9.Text = dtD.DefaultView[index]["ItNo"].ToString();
                    textBoxT10.Text = dtD.DefaultView[index]["ItNoUdf"].ToString();
                    textBoxT11.Text = dtD.DefaultView[index]["ItStockQty"].ToDecimal().ToString("f" + Common.Q);
                    textBoxT12.Text = dtD.DefaultView[index]["FaNo"].ToString();
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
                var boid = dtD.DefaultView[index]["BoID"].ToString().Trim();
                frm.dr = Common.load("Check", "RBorrD", "BoID", boid);
                frm.ShowDialog();
            }
        }

        private void btnBom_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var trait = dtD.DefaultView[index]["ItTrait"].ToDecimal();
            if (trait != 1 && trait != 2)
            {
                MessageBox.Show("只有組合品或組裝品有組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridViewT1.Focus();
                return;
            }

            using (subMenuFm_2.FrmSale_Bom frm = new subMenuFm_2.FrmSale_Bom())
            {
                frm.BoItNo1 = dtD.DefaultView[index]["ItNo"].ToString().Trim();
                frm.BoItName1 = dtD.DefaultView[index]["itName"].ToString().Trim();
                frm.JustForBrow = true;
                frm.TTable = "rborrbom";
                frm.TKey = dtD.DefaultView[index]["BomID"].ToString().Trim();
                frm.ShowDialog();
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                btnStock.Focus();
                subMenuFm_2.FrmSale_Stock frm = new subMenuFm_2.FrmSale_Stock();
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
                    this.TResult = dtD.DefaultView[index]["BoNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != this.備註說明.Name) btnGet_Click(null, null);
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != this.備註說明.Name) return;
            try
            {
                var bomid = dtD.DefaultView[e.RowIndex]["Bomid"].ToString().Trim();
                var memo = dataGridViewT1[this.備註說明.Name, e.RowIndex].EditedFormattedValue.ToString();
                if (memo.Length == 0) return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@memo", memo);
                    cmd.Parameters.AddWithValue("@Bomid", bomid);

                    cmd.CommandText = "Update RBorrD Set memo = @memo where Bomid = @Bomid";
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
            else if (keyData == Keys.F7)
            {
                btnBom_Click(null, null);
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
            else if (keyData.ToString().StartsWith("F7") && keyData.ToString().EndsWith("Shift"))
            {
                Q7_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F8") && keyData.ToString().EndsWith("Shift"))
            {
                Q8_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tFaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void tItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void tStNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void tEmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void tBodate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused) 
                return;

            xe.DateValidate(sender, e, true); 
        }

        private void FrmBorrowBrowNew_Shown(object sender, EventArgs e)
        {
            tBoNo.Focus();
        }

        private void FrmBorrowBrowNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.TResult.Length > 0) return;

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
                    this.TResult = dtD.DefaultView[index]["BoNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
