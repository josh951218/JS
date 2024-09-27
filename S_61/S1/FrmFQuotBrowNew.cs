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

namespace S_61.S1
{
    public partial class FrmFQuotBrowNew : Formbase
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
        bool IsQuery;
        decimal Query = 0;

        public FrmFQuotBrowNew()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT23.Text = Common.Sys_MemoUdf;

            this.詢價數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.Set本幣金額小數();
            this.進價.Set進貨單價小數();
            this.稅前金額.Set進項金額小數();
            this.詢價日期.DataPropertyName = Common.User_DateTime == 1 ? "詢價日期" : "詢價日期1";
            this.預計交期.DataPropertyName = Common.User_DateTime == 1 ? "預計交期" : "預計交期1";
            this.tFqdate.MaxLength = this.tFqdateS.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

            dataGridViewT1.ReadOnly = false;
            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name != this.備註說明.Name) dataGridViewT1.Columns[i].ReadOnly = true;
            }

            dataGridViewT1.DataSource = dtD.DefaultView;

            this.進價.Visible = Common.User_ShopPrice;
            this.稅前單價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
            this.本幣單價.Visible = Common.User_ShopPrice;
            this.本幣稅前單價.Visible = Common.User_ShopPrice;
            this.本幣稅前金額.Visible = Common.User_ShopPrice;

            textBoxT1.Visible = textBoxT2.Visible = textBoxT8.Visible = textBoxT7.Visible = Common.User_ShopPrice;
        }

        private void FrmQuoteBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
+ " LEFT(Fquotd.Fqdate,3)+'/'+SUBSTRING(Fquotd.Fqdate,4,2)+'/'+RIGHT(Fquotd.Fqdate,2) 詢價日期,"
+ " LEFT(Fquotd.Fqdate1,4)+'/'+SUBSTRING(Fquotd.Fqdate1,5,2)+'/'+RIGHT(Fquotd.Fqdate1,2) 詢價日期1,"
+ " LEFT(Fquotd.FqdateS,3)+'/'+SUBSTRING(Fquotd.FqdateS,4,2)+'/'+RIGHT(Fquotd.FqdateS,2) 預計交期,"
+ " LEFT(Fquotd.FqdateS1,4)+'/'+SUBSTRING(Fquotd.FqdateS1,5,2)+'/'+RIGHT(Fquotd.FqdateS1,2) 預計交期1,"
+ " 產品組成= case when Fquotd.ittrait=1 then '組合品' when Fquotd.ittrait=2 then '組裝品' when Fquotd.ittrait=3 then '單一商品' end,"
+ " Fquotd.FqID, Fquotd.FqNo, Fquotd.Fqdate, Fquotd.Fqdate1, Fquotd.itno, Fquotd.itname, Fquotd.ittrait, "
+ " Fquotd.itunit, Fquotd.itpkgqty, Fquotd.qty, Fquotd.price, Fquotd.prs, Fquotd.rate, Fquotd.taxprice, Fquotd.mny, "
+ " Fquotd.priceb, Fquotd.taxpriceb, Fquotd.mnyb, Fquotd.FqdateS, Fquotd.FqdateS1, "
+ " Fquotd.memo, Fquotd.lowzero, Fquotd.bomid, Fquotd.bomrec, Fquotd.recordno, "
+ " Fquotd.sltflag, Fquotd.extflag, Fquotd.itdesp1, Fquotd.itdesp2, Fquotd.itdesp3, Fquotd.itdesp4, Fquotd.itdesp5, "
+ " Fquotd.itdesp6, Fquotd.itdesp7, Fquotd.itdesp8, Fquotd.itdesp9, Fquotd.itdesp10, Fquotd.stName, "
+ " [Fquot].FaNo, [Fquot].faname2, [Fquot].faname1, [Fquot].FaTel1, [Fquot].FaPer1, [Fquot].emno, [Fquot].emname, "
+ " [Fquot].xa1no, [Fquot].xa1name, [Fquot].xa1par, [Fquot].taxmnyf, [Fquot].taxmnyb, "
+ " [Fquot].taxmny, [Fquot].x3no, [Fquot].tax, [Fquot].totmny, [Fquot].taxb, [Fquot].totmnyb, [Fquot].FqPayment, "
+ " [Fquot].FqPeriod, [Fquot].FqMemo, [Fquot].fqmemo1, [Fquot].recordno AS rdno, "
+ " item.itnoudf, item.itstockqty"
+ " FROM              Fquotd LEFT OUTER JOIN"
+ " [Fquot] ON Fquotd.FqNo = [Fquot].FqNo LEFT OUTER JOIN"
+ " item ON Fquotd.itno = item.itno"
+ " WHERE          (0 = 0)";

            tFqNo.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            if (Query == 0) IsQuery = false;
            Query++;
            tFqNo.Clear();


            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from Fquotd ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["FqID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("FqID = " + No);

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
                tFqNo.Text = dtD.DefaultView[index]["FqNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tFqNo.Clear();
            }

            GridSort("FqNo ASC");
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
                tFqdate.Text = dtD.DefaultView[index]["Fqdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tFaNo.Clear(); tFqdate.Clear();
            }

            GridSort("Fqdate DESC,FaNo ASC");
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
                tFaNo.Text = dtD.DefaultView[index]["FaNo"].ToString();
                tFqdateS.Text = dtD.DefaultView[index]["FqdateS"].ToString();
                btnQuery_Click(btnQuery, null);
                tFaNo.Clear(); tFqdateS.Clear();
            }

            GridSort("FqdateS DESC,FaNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q4.ForeColor = Color.Red;
            Q4.Enabled = true;
        }

        private void Q5_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q5.Enabled = false;
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tItNo.Clear();

            }

            GridSort("ItNo ASC");
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
                tFaNo.Text = dtD.DefaultView[index]["FaNo"].ToString();
                tFqdate.Text = dtD.DefaultView[index]["Fqdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tFaNo.Clear(); tFqdate.Clear();
            }

            GridSort("FaNo ASC,Fqdate DESC");
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
                tFaNo.Text = dtD.DefaultView[index]["FaNo"].ToString();
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tFaNo.Clear(); tItNo.Clear();
            }

            GridSort("FaNo ASC,ItNo ASC");
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
                tEmNo.Text = dtD.DefaultView[index]["EmNo"].ToString();
                tFqdate.Text = dtD.DefaultView[index]["Fqdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear(); tFqdate.Clear();
            }

            GridSort("EmNo ASC,Fqdate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q8.ForeColor = Color.Red;
            Q8.Enabled = true;
        }

        private void Q9_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q9.Enabled = false;
                tEmNo.Text = dtD.DefaultView[index]["EmNo"].ToString();
                tFqdateS.Text = dtD.DefaultView[index]["FqdateS"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear(); tFqdateS.Clear();
            }

            GridSort("EmNo ASC,FqdateS DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q9.ForeColor = Color.Red;
            Q9.Enabled = true;
        }

        private void Q10_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                Q10.Enabled = false;
                tMemo.Text = dtD.DefaultView[index]["Memo"].ToString();
                btnQuery_Click(btnQuery, null);
                tMemo.Clear();
            }

            GridSort("Memo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q10.ForeColor = Color.Red;
            Q10.Enabled = true;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                IsQuery = true;
                btnQuery.Enabled = false;

                if (tFaNo.TrimTextLenth() > 0 && tFqdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tFqdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].FaNo", tFaNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim(), "Fqdate", date);
                }
                else if (tFaNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].FaNo", tFaNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0 && tFqdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tFqdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].EmNo", tEmNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q8_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "Fqdate", date);
                }
                else if (tEmNo.TrimTextLenth() > 0 && tFqdateS.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tFqdateS.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].EmNo", tEmNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "Fqdate", date);
                }
                else if (tFqNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].FqNo", tFqNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FqNo", tFqNo.Text.Trim());
                }
                else if (tFqdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tFqdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "[Fquotd].Fqdate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q3_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "Fqdate", date);
                }
                else if (tFqdateS.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tFqdateS.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "[Fquotd].FqdateS", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q4_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "FqdateS", date);
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].ItNo", tItNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItNo", tItNo.Text.Trim());
                }
                else if (tMemo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].Memo", tMemo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q10_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "Memo", tMemo.Text.Trim());
                }
                else if (tFaNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].FaNo", tFaNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.GetDateTime(Common.User_DateTime);
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Fquotd].EmNo", tEmNo.Text.Trim(), "[Fquotd].Fqdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "Fqdate", date);
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

        private void btnItDesp_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(false, FormStyle.Mini))
            {
                var FqID = dtD.DefaultView[index]["FqID"].ToString().Trim();
                frm.dr = Common.load("Check", "Fquotd", "FqID", FqID);
                frm.ShowDialog();
            }
        }

        private void btnBom_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows[0].Cells["產品組成"].Value.ToString() == "單一商品")
            {
                MessageBox.Show("只有組合品或組裝品有組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                using (var frm = new subMenuFm_2.FrmDraw_Bom())
                { 
                    var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == -1) return;
                    else
                    {
                        frm.BoItNo1 = dataGridViewT1["產品編號", index].Value.ToString();
                        frm.BoItName1 = dataGridViewT1["品名規格", index].Value.ToString();
                        frm.DrNo = dataGridViewT1["詢價編號", index].Value.ToString();
                        frm.BomID = dtD.DefaultView[index]["BomID"].ToString();
                        frm.together = true;
                        frm.str = new string[] { "Fquotbom", "FqNo" };
                        frm.ShowDialog();
                    }
                }
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
                    this.TResult = dtD.DefaultView[index]["FqNo"].ToString();
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

                    cmd.CommandText = "Update Fquotd Set memo = @memo where Bomid = @Bomid";
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
                btnItDesp_Click(null, null);
            }
            else if (keyData == Keys.F8)
            {
                btnStock_Click(null, null);
            }
            else if (keyData == Keys.F7)
            {
                btnBom_Click(null, null);
            }
            else if (keyData == Keys.F4)
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
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift"))
            {
                Q9_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift"))
            {
                Q10_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textBoxT16_Validating(object sender, CancelEventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FqMemo", textBoxT16.Text.Trim());
                    cmd.Parameters.AddWithValue("@FqNo", dtD.DefaultView[index]["FqNo"].ToString().Trim());

                    cmd.CommandText = "Update [Fquot] Set FqMemo=@FqMemo where FqNo=@FqNo ";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tFaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void tItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void tEmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void tQudate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused)
                return;

            xe.DateValidate(sender, e, true);
        }

        private void tQuDateS_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused)
                return;

            xe.DateValidate(sender, e, true);
        }

        private void FrmQuoteBrowNew_FormClosing(object sender, FormClosingEventArgs e)
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
                    this.TResult = dtD.DefaultView[index]["FqNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (e.Row.Index == dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected))
                {
                    var index = e.Row.Index;

                    textBoxT1.Text = dtD.DefaultView[index]["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                    textBoxT2.Text = dtD.DefaultView[index]["Tax"].ToDecimal().ToString("f" + Common.TS);
                    textBoxT8.Text = dtD.DefaultView[index]["TotMny"].ToDecimal().ToString("f" + Common.MST);
                    pVar.XX03Validate(dtD.DefaultView[index]["X3No"].ToString(), new TextBox(), textBoxT7);

                    textBoxT10.Text = dtD.DefaultView[index]["ItNo"].ToString();
                    textBoxT9.Text = dtD.DefaultView[index]["ItNoUdf"].ToString();
                    textBoxT11.Text = dtD.DefaultView[index]["ItStockQty"].ToDecimal().ToString("f" + Common.Q);

                    textBoxT13.Text = dtD.DefaultView[index]["FaNo"].ToString();
                    textBoxT14.Text = dtD.DefaultView[index]["FaPer1"].ToString();
                    textBoxT15.Text = dtD.DefaultView[index]["FaTel1"].ToString();
                    textBoxT5.Text = dtD.DefaultView[index]["Xa1No"].ToString();
                    textBoxT6.Text = dtD.DefaultView[index]["Xa1Name"].ToString();
                    textBoxT12.Text = dtD.DefaultView[index]["Xa1Par"].ToDecimal().ToString("f4");


                    textBoxT3.Text = dtD.DefaultView[index]["FqPayment"].ToString();
                    textBoxT4.Text = dtD.DefaultView[index]["FqPeriod"].ToString();
                    textBoxT16.Text = dtD.DefaultView[index]["FqMemo"].ToString();
                }
            }
        }

        private void FrmQuoteBrowNew_Shown(object sender, EventArgs e)
        {
            tFqNo.Focus();
        }
    }
}
