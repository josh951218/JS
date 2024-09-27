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
    public partial class FrmQuoteBrowNew : Formbase
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

        public FrmQuoteBrowNew()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT23.Text = Common.Sys_MemoUdf;

            this.報價數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.Set本幣金額小數();
            this.售價.Set銷貨單價小數();
            this.稅前金額.Set銷項金額小數();
            this.報價日期.DataPropertyName = Common.User_DateTime == 1 ? "報價日期" : "報價日期1";
            this.預計交期.DataPropertyName = Common.User_DateTime == 1 ? "預計交期" : "預計交期1";
            this.tQudate.MaxLength = this.tQudateS.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

            dataGridViewT1.ReadOnly = false;
            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name != this.備註說明.Name) dataGridViewT1.Columns[i].ReadOnly = true;
            }

            dataGridViewT1.DataSource = dtD.DefaultView;

            this.售價.Visible = Common.User_SalePrice;
            this.稅前單價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.本幣單價.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;

            textBoxT1.Visible = textBoxT2.Visible = textBoxT8.Visible = textBoxT7.Visible = Common.User_SalePrice;
        }

        private void FrmQuoteBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
+ " LEFT(quoted.qudate,3)+'/'+SUBSTRING(quoted.qudate,4,2)+'/'+RIGHT(quoted.qudate,2) 報價日期,"
+ " LEFT(quoted.qudate1,4)+'/'+SUBSTRING(quoted.qudate1,5,2)+'/'+RIGHT(quoted.qudate1,2) 報價日期1,"
+ " LEFT(quoted.qudates,3)+'/'+SUBSTRING(quoted.qudates,4,2)+'/'+RIGHT(quoted.qudates,2) 預計交期,"
+ " LEFT(quoted.qudates1,4)+'/'+SUBSTRING(quoted.qudates1,5,2)+'/'+RIGHT(quoted.qudates1,2) 預計交期1,"
+ " 產品組成= case when quoted.ittrait=1 then '組合品' when quoted.ittrait=2 then '組裝品' when quoted.ittrait=3 then '單一商品' end,"
+ " quoted.QuID, quoted.QuNo, quoted.qudate, quoted.qudate1, quoted.itno, quoted.itname, quoted.ittrait, "
+ " quoted.itunit, quoted.itpkgqty, quoted.qty, quoted.price, quoted.prs, quoted.rate, quoted.taxprice, quoted.mny, "
+ " quoted.priceb, quoted.taxpriceb, quoted.mnyb, quoted.qudates, quoted.qudates1, "
+ " quoted.memo, quoted.lowzero, quoted.bomid, quoted.bomrec, quoted.recordno, "
+ " quoted.sltflag, quoted.extflag, quoted.itdesp1, quoted.itdesp2, quoted.itdesp3, quoted.itdesp4, quoted.itdesp5, "
+ " quoted.itdesp6, quoted.itdesp7, quoted.itdesp8, quoted.itdesp9, quoted.itdesp10, quoted.stName, "
+ " quoted.Punit, quoted.Pqty, quoted.mwidth1, "
+ " quoted.mwidth2, quoted.mwidth3, quoted.mwidth4, quoted.Pformula, "
+ " [quote].cuno, [quote].cuname2, [quote].cuname1, [quote].cutel1, [quote].cuper1, [quote].emno, [quote].emname, "
+ " [quote].xa1no, [quote].xa1name, [quote].xa1par, [quote].trno, [quote].trname, [quote].taxmnyf, [quote].taxmnyb, "
+ " [quote].taxmny, [quote].x3no, [quote].tax, [quote].totmny, [quote].taxb, [quote].totmnyb, [quote].QuPayment, "
+ " [quote].QuPeriod, [quote].QuMemo, [quote].qumemo1, [quote].recordno AS rdno, "
+ " item.itnoudf, item.itstockqty, [quoted].standard "
+ " FROM              quoted LEFT OUTER JOIN"
+ " [quote] ON quoted.QuNo = [quote].QuNo LEFT OUTER JOIN"
+ " item ON quoted.itno = item.itno"
+ " WHERE          (0 = 0)";


            tQuNo.Text = this.TSeekNo;
            btnQuery_Click(null, null);
            if (Query == 0) IsQuery = false;
            Query++;
            tQuNo.Clear();


            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from quoted ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["QuID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("QuID = " + No);

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
                tQuNo.Text = dtD.DefaultView[index]["quno"].ToString();
                btnQuery_Click(btnQuery, null);
                tQuNo.Clear();
            }
            GridSort("QuNo ASC");
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
                tCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                tQudate.Text = dtD.DefaultView[index]["qudate"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear();
                tQudate.Clear();
            }
            GridSort("qudate DESC,CuNo ASC");
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
                tCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                tQudateS.Text = dtD.DefaultView[index]["qudates"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear();
                tQudateS.Clear();
            }
            GridSort("qudates DESC,CuNo ASC");
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
                tCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                tQudate.Text = dtD.DefaultView[index]["qudate"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear();
                tQudate.Clear();
            }
            GridSort("CuNo ASC,qudate DESC");
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
                tCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear();
                tItNo.Clear();
            }
            GridSort("CuNo ASC,ItNo ASC");
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
                tQudate.Text = dtD.DefaultView[index]["qudate"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear();
                tQudate.Clear();
            }
            GridSort("EmNo ASC,qudate DESC");
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
                tQudateS.Text = dtD.DefaultView[index]["qudates"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear();
                tQudateS.Clear();
            }
            GridSort("EmNo ASC,qudates DESC");
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

        private void Q11_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                tItNo.Text = dtD.DefaultView[index]["standard"].ToString();
                btnQuery_Click(btnQuery, null);
                tstandard.Clear();
            }
            GridSort("standard ASC,qudate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                IsQuery = true;
                btnQuery.Enabled = false;

                if (tCuNo.TrimTextLenth() > 0 && tQudate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tQudate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].CuNo", tCuNo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim(), "qudate", date);
                }
                else if (tCuNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].CuNo", tCuNo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0 && tQudate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tQudate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].EmNo", tEmNo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q8_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "qudate", date);
                }
                else if (tEmNo.TrimTextLenth() > 0 && tQudateS.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tQudateS.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].EmNo", tEmNo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "QudateS", date);
                }
                else if (tQuNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Eload(dtD, SQL, "[quoted].QuNo", tQuNo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "QuNo", tQuNo.Text.Trim());
                    AllCancel();
                }
                else if (tQudate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tQudate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "[quoted].qudate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q3_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "qudate", date);
                }
                else if (tQudateS.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tQudateS.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "[quoted].QudateS", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q4_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "QudateS", date);
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].ItNo", tItNo.Text.Trim(), "[quoted].qudate");
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
                        Common.Eload(dtD, SQL, "[quoted].Memo", tMemo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q10_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "Memo", tMemo.Text.Trim());
                }
                else if (tCuNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].CuNo", tCuNo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.GetDateTime(Common.User_DateTime);
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].EmNo", tEmNo.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "QudateS", date);
                }
                else if (tstandard.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[quoted].standard", tstandard.Text.Trim(), "[quoted].qudate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;
                    Q11_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "standard", tstandard.Text.Trim());
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItNo", tItNo.Text.Trim());
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
                var QuID = dtD.DefaultView[index]["QuID"].ToString().Trim();
                frm.dr = Common.load("Check", "quoted", "QuID", QuID);
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
                        frm.DrNo = dataGridViewT1["報價編號", index].Value.ToString();
                        frm.BomID = dtD.DefaultView[index]["BomID"].ToString();
                        frm.together = true;
                        frm.str = new string[] { "quotebom", "QuNo" };
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
                    this.TResult = dtD.DefaultView[index]["QuNo"].ToString();
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

                    cmd.CommandText = "Update quoted Set memo = @memo where Bomid = @Bomid";
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
                    cmd.Parameters.AddWithValue("@QuMemo", textBoxT16.Text.Trim());
                    cmd.Parameters.AddWithValue("@QuNo", dtD.DefaultView[index]["QuNo"].ToString().Trim());

                    cmd.CommandText = "Update [quote] Set QuMemo=@QuMemo where QuNo=@QuNo ";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tCuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
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
                    this.TResult = dtD.DefaultView[index]["QuNo"].ToString();
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

                    textBoxT13.Text = dtD.DefaultView[index]["CuNo"].ToString();
                    textBoxT14.Text = dtD.DefaultView[index]["CuPer1"].ToString();
                    textBoxT15.Text = dtD.DefaultView[index]["CuTel1"].ToString();
                    textBoxT5.Text = dtD.DefaultView[index]["Xa1No"].ToString();
                    textBoxT6.Text = dtD.DefaultView[index]["Xa1Name"].ToString();
                    textBoxT12.Text = dtD.DefaultView[index]["Xa1Par"].ToDecimal().ToString("f4");


                    textBoxT3.Text = dtD.DefaultView[index]["QuPayment"].ToString();
                    textBoxT4.Text = dtD.DefaultView[index]["QuPeriod"].ToString();
                    textBoxT16.Text = dtD.DefaultView[index]["QuMemo"].ToString();
                }
            }
        }

        private void FrmQuoteBrowNew_Shown(object sender, EventArgs e)
        {
            tQuNo.Focus();
        }

        void AllCancel()
        {
            tQuNo.Clear(); tQudate.Clear();
            tQudateS.Clear(); tItNo.Clear();
            tCuNo.Clear(); tEmNo.Clear();
            tMemo.Clear();
        }

    }
}
