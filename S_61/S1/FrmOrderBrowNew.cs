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
    public partial class FrmOrderBrowNew : Formbase
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

        public FrmOrderBrowNew()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT23.Text = Common.Sys_MemoUdf;

            this.訂單數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.Set本幣金額小數();
            this.售價.Set銷貨單價小數();
            this.稅前金額.Set銷項金額小數();
            this.訂單日期.DataPropertyName = Common.User_DateTime == 1 ? "訂單日期" : "訂單日期1";
            this.交貨日期.DataPropertyName = Common.User_DateTime == 1 ? "交貨日期" : "交貨日期1";
            this.tOrdate.MaxLength = this.tEsDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

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

        private void FrmOrderBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
+ " LEFT(orderd.ordate,3)+'/'+SUBSTRING(orderd.ordate,4,2)+'/'+RIGHT(orderd.ordate,2) 訂單日期,"
+ " LEFT(orderd.ordate1,4)+'/'+SUBSTRING(orderd.ordate1,5,2)+'/'+RIGHT(orderd.ordate1,2) 訂單日期1,"
+ " LEFT(orderd.esdate,3)+'/'+SUBSTRING(orderd.esdate,4,2)+'/'+RIGHT(orderd.esdate,2) 交貨日期,"
+ " LEFT(orderd.esdate1,4)+'/'+SUBSTRING(orderd.esdate1,5,2)+'/'+RIGHT(orderd.esdate1,2) 交貨日期1,"
+ " 產品組成= case when orderd.ittrait=1 then '組合品' when orderd.ittrait=2 then '組裝品' when orderd.ittrait=3 then '單一商品' end,"
+ " orderd.OrID, orderd.orno, orderd.ordate, orderd.ordate1, orderd.ortrnflag, orderd.itno, orderd.itname, orderd.ittrait, "
+ " orderd.itunit, orderd.itpkgqty, orderd.qty, orderd.price, orderd.prs, orderd.rate, orderd.taxprice, orderd.mny, "
+ " orderd.priceb, orderd.taxpriceb, orderd.mnyb, orderd.qtyout, orderd.qtyin, orderd.esdate, orderd.esdate1, "
+ " orderd.esdate2, orderd.stkqtyflag, orderd.memo, orderd.lowzero, orderd.bomid, orderd.bomrec, orderd.recordno, "
+ " orderd.sltflag, orderd.extflag, orderd.itdesp1, orderd.itdesp2, orderd.itdesp3, orderd.itdesp4, orderd.itdesp5, "
+ " orderd.itdesp6, orderd.itdesp7, orderd.itdesp8, orderd.itdesp9, orderd.itdesp10, orderd.stName, orderd.qtyNotOut, "
+ " orderd.qtyNotInStk, orderd.Punit, orderd.Pqty, orderd.mqty, orderd.munit, orderd.mlong, orderd.mwidth1, "
+ " orderd.mwidth2, orderd.mwidth3, orderd.mwidth4, orderd.mformula, orderd.Pformula, [order].quno, [order].oroverflag, "
+ " [order].cuno, [order].cuname2, [order].cuname1, [order].cutel1, [order].cuper1, [order].emno, [order].emname, "
+ " [order].xa1no, [order].xa1name, [order].xa1par, [order].trno, [order].trname, [order].taxmnyf, [order].taxmnyb, "
+ " [order].taxmny, [order].x3no, [order].tax, [order].totmny, [order].taxb, [order].totmnyb, [order].orpayment, "
+ " [order].orperiod, [order].ormemo, [order].ormemo1, [order].recordno AS rdno, [order].spno, [order].spname, "
+ " item.itnoudf, item.itstockqty,[orderd].standard "
+ " FROM              orderd LEFT OUTER JOIN"
+ " [order] ON orderd.orno = [order].orno LEFT OUTER JOIN"
+ " item ON orderd.itno = item.itno"
+ " WHERE          (0 = 0)";


            tOrNo.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            if (Query == 0) IsQuery = false;
            Query++;
            tOrNo.Clear();

            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from orderd ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["OrID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("OrID = " + No);

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
                tOrNo.Text = dtD.DefaultView[index]["OrNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tOrNo.Clear();
            }
            GridSort("OrNo ASC");
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
                tOrdate.Text = dtD.DefaultView[index]["ordate"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear(); tOrdate.Clear();
            }
            GridSort("ordate DESC,CuNo ASC");
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
                tEsDate.Text = dtD.DefaultView[index]["esdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear(); tEsDate.Clear();
            }
            GridSort("esdate DESC,CuNo ASC");
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
                tOrdate.Text = dtD.DefaultView[index]["ordate"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear(); tOrdate.Clear();
            }
            GridSort("CuNo ASC,ordate DESC");
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
                tCuNo.Clear(); tItNo.Clear();
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
                tOrdate.Text = dtD.DefaultView[index]["ordate"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear(); tOrdate.Clear();
            }
            GridSort("EmNo ASC,ordate DESC");
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
                tEsDate.Text = dtD.DefaultView[index]["esdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear(); tEsDate.Clear();
            }
            GridSort("EmNo ASC,esdate DESC");
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

        private void Q11_Click(object sender, EventArgs e)//型號排序+日期逆排
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                tstandard.Text = dtD.DefaultView[index]["standard"].ToString();
                tOrdate.Text = dtD.DefaultView[index]["ordate"].ToString();
                btnQuery_Click(btnQuery, null);
                tstandard.Clear(); tOrdate.Clear();
            }
            GridSort("standard ASC,ordate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                IsQuery = true;
                btnQuery.Enabled = false;

                if (tCuNo.TrimTextLenth() > 0 && tOrdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tOrdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Orderd].CuNo", tCuNo.Text.Trim(), "[Orderd].Ordate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim(), "Ordate", date);
                }
                else if (tCuNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Orderd].CuNo", tCuNo.Text.Trim(), "[Orderd].Ordate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0 && tOrdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tOrdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Orderd].EmNo", tEmNo.Text.Trim(), "[Orderd].Ordate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q8_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "Ordate", date);
                }
                else if (tEmNo.TrimTextLenth() > 0 && tEsDate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tEsDate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Orderd].EmNo", tEmNo.Text.Trim(), "[Orderd].Ordate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "Esdate", date);
                }
                else if (tOrNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Eload(dtD, SQL, "[Orderd].OrNo", tOrNo.Text.Trim(), "[Orderd].Ordate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "OrNo", tOrNo.Text.Trim());
                }
                else if (tOrdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tOrdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "[Orderd].Ordate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q3_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "Ordate", date);
                }
                else if (tEsDate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tEsDate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "[Orderd].EsDate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q4_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EsDate", date);
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Orderd].ItNo", tItNo.Text.Trim(), "[Orderd].Ordate");
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
                        Common.Eload(dtD, SQL, "[Orderd].Memo", tMemo.Text.Trim(), "[Orderd].Ordate");
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
                        Common.Eload(dtD, SQL, "[Orderd].CuNo", tCuNo.Text.Trim(), "[Orderd].Ordate");
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
                        Common.Eload(dtD, SQL, "[Orderd].EmNo", tEmNo.Text.Trim(), "[Orderd].Ordate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "Esdate", date);
                }
                else if (tstandard.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.GetDateTime(Common.User_DateTime);
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "[Orderd].standard", tstandard.Text.Trim(), "[Orderd].Ordate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;
                    Q11_Click(null, null);//standard ASC,ordate DESC
                    dtD.DefaultView.Search(ref dataGridViewT1, "standard", tstandard.Text.Trim());
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
                var orid = dtD.DefaultView[index]["OrID"].ToString().Trim();
                frm.dr = Common.load("Check", "OrderD", "OrID", orid);
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
                        frm.DrNo = dataGridViewT1["訂單編號", index].Value.ToString();
                        frm.BomID = dtD.DefaultView[index]["BomID"].ToString();
                        frm.together = true;
                        frm.str = new string[] { "orderbom", "orno" };
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
                    this.TResult = dtD.DefaultView[index]["OrNo"].ToString();
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

                    cmd.CommandText = "Update OrderD Set memo = @memo where Bomid = @Bomid";
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
                    cmd.Parameters.AddWithValue("@OrMemo", textBoxT16.Text.Trim());
                    cmd.Parameters.AddWithValue("@OrNo", dtD.DefaultView[index]["OrNo"].ToString().Trim());

                    cmd.CommandText = "Update [Order] Set OrMemo=@OrMemo where OrNo=@OrNo ";
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

        private void tOrdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused)
                return;

            xe.DateValidate(sender, e, true);
        }

        private void tEsDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused)
                return;

            xe.DateValidate(sender, e, true);
        }

        private void FrmOrderBrowNew_FormClosing(object sender, FormClosingEventArgs e)
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
                    this.TResult = dtD.DefaultView[index]["OrNo"].ToString();
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
                    checkBoxT1.Checked = dtD.DefaultView[index]["OrOverFlag"].ToString() == Boolean.TrueString;

                    textBoxT3.Text = dtD.DefaultView[index]["OrPayment"].ToString();
                    textBoxT4.Text = dtD.DefaultView[index]["OrPeriod"].ToString();
                    textBoxT16.Text = dtD.DefaultView[index]["ormemo"].ToString();
                }
            }
        }

        private void FrmOrderBrowNew_Shown(object sender, EventArgs e)
        {
            tOrNo.Focus();
        }
    }
}
