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
using S_61.subMenuFm_2;


namespace S_61.S2
{
    public partial class FrmRLendBrow : Formbase
    {
        JBS.JS.RLend jRLend;

        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 
         
        List<ButtonGridT> list;
       
        DataTable dtD = new DataTable();
        string SQL = "";
        decimal No = 0;
        decimal TotalCount = 0;
        bool IsQuery;
        decimal Query = 0;

        public FrmRLendBrow()
        {
            InitializeComponent();
            this.jRLend = new JBS.JS.RLend();
            list = new List<ButtonGridT> { query2, query3, query4, query5, query6, query7, query8 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            query8.Text = "f8:" + Common.Sys_MemoUdf;
            labelT19.Text = Common.Sys_MemoUdf;

            TaxMny.FirstNum = Common.nFirst;
            Tax.FirstNum = Common.nFirst;
            TotMny.FirstNum = Common.nFirst;
            StockQty.FirstNum = Common.iFirst;
            Xa1Par.FirstNum = 4;

            TaxMny.LastNum = Common.MST;
            Tax.LastNum = Common.TS;
            TotMny.LastNum = Common.MST;
            StockQty.LastNum = Common.DQ;
            Xa1Par.LastNum = 4;

            dataGridViewT1.ReadOnly = false;
            this.還入單號.ReadOnly = true;
            this.還入日期.ReadOnly = true;
            this.還入倉庫.ReadOnly = true;
            this.客戶簡稱.ReadOnly = true;
            this.品名規格.ReadOnly = true;
            this.單位.ReadOnly = true;
            this.還入數量.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.還入人員.ReadOnly = true;
            this.產品編號.ReadOnly = true;
            this.售價.ReadOnly = true;
            this.折數.ReadOnly = true;
            this.稅前單價.ReadOnly = true;
            this.稅前金額.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.客戶編號.ReadOnly = true;

            this.還入數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前單價.DefaultCellStyle.Format = "f6";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.折數.DefaultCellStyle.Format = "f3";
            this.本幣稅前單價.DefaultCellStyle.Format = "f" + Common.M;
            this.本幣單價.DefaultCellStyle.Format = "f" + Common.M;
            this.本幣稅前金額.DefaultCellStyle.Format = "f" + Common.MS;

            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name != this.備註說明.Name) dataGridViewT1.Columns[i].ReadOnly = true;
            }

            dataGridViewT1.DataSource = dtD.DefaultView;

            this.售價.Visible = Common.User_SalePrice;
            this.稅前單價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;
            this.本幣單價.Visible = Common.User_SalePrice;
            TaxMny.Visible = Tax.Visible = TotMny.Visible = Common.User_SalePrice;
        }

        private void FrmLendBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";
            //日期格式
            switch (Common.User_DateTime)
            {
                case 1:
                    qDate.MaxLength = 7;
                    break;
                case 2:
                    qDate.MaxLength = 8;
                    break;
            }
            SQL = " 序號='',"
                + " LEFT(rlendd.ledate,3)+'/'+SUBSTRING(rlendd.ledate,4,2)+'/'+RIGHT(rlendd.ledate,2) 還入日期,"
                + " LEFT(rlendd.ledate1,4)+'/'+SUBSTRING(rlendd.ledate1,5,2)+'/'+RIGHT(rlendd.ledate1,2) 還入日期1,"
                + " 產品組成= case when rlendd.ittrait=1 then '組合品' when rlendd.ittrait=2 then '組裝品' when rlendd.ittrait=3 then '單一商品' end,"
                + " rlendd.*,rlend.emname,rlend.lememo,rlend.taxmny,rlend.tax,rlend.totmny,rlend.xa1name,rlend.x3no,rlend.cuname1,item.itstockqty,item.itnoudf from rlendd left outer join rlend "
                + " on rlendd.leno = rlend.leno left outer join item on rlendd.itno=item.itno where '0'='0'";

            qLeno.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            if (Query == 0) IsQuery = false;
            Query++;
            qLeno.Clear();

            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["leid"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("leid = " + No);

            if (index == -1) return;
            if (index >= dtD.Rows.Count) index = dtD.Rows.Count - 1;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from rlendd ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        private void query2_Click(object sender, EventArgs e)
        {
            if (qLeno.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                query2.Enabled = false;
                qLeno.Text = dtD.DefaultView[index]["Leno"].ToString();
                btnQuery_Click(btnQuery, null);
                qLeno.Clear();
            }
            GridSort("Leno ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            query2.ForeColor = Color.Red;
            query2.Enabled = true;
        }

        private void query3_Click(object sender, EventArgs e)
        {
            if (qCuNo.TrimTextLenth() == 0 && qDate.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                query3.Enabled = false;
                qCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                qDate.Text = dtD.DefaultView[index]["ledate"].ToString();
                btnQuery_Click(btnQuery, null);
                qCuNo.Clear(); qDate.Clear();
            }
            GridSort("ledate DESC,CuNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            query3.ForeColor = Color.Red;
            query3.Enabled = true;
        }

        private void query4_Click(object sender, EventArgs e)
        {
            if (qItNo.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                query4.Enabled = false;
                qItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                qItNo.Clear();
            }
            GridSort("ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            query4.ForeColor = Color.Red;
            query4.Enabled = true;
        }

        private void query5_Click(object sender, EventArgs e)
        {
            if (qItNo.TrimTextLenth() == 0 && qCuNo.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                query5.Enabled = false;
                qItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                qCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                btnQuery_Click(btnQuery, null);
                qItNo.Clear(); qCuNo.Clear();
            }
            GridSort("CuNo ASC,ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            query5.ForeColor = Color.Red;
            query5.Enabled = true;
        }

        private void query6_Click(object sender, EventArgs e)
        {
            if (qStNo.TrimTextLenth() == 0 && qItNo.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                query6.Enabled = false;
                qStNo.Text = dtD.DefaultView[index]["StNo"].ToString();
                qItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                qStNo.Clear(); qItNo.Clear();
            }
            GridSort("StNo ASC,ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            query6.ForeColor = Color.Red;
            query6.Enabled = true;
        }

        private void query7_Click(object sender, EventArgs e)
        {
            if (qEmName.TrimTextLenth() == 0 && qDate.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                query7.Enabled = false;
                qEmName.Text = dtD.DefaultView[index]["EmName"].ToString();
                qDate.Text = dtD.DefaultView[index]["ledate"].ToString();
                btnQuery_Click(btnQuery, null);
                qEmName.Clear(); qDate.Clear();
            }
            GridSort("EmName ASC,ledate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
            query7.ForeColor = Color.Red;
            query7.Enabled = true;
        }

        private void query8_Click(object sender, EventArgs e)
        {
            if (qMeNo.TrimTextLenth() == 0 && !IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                query8.Enabled = false;
                qMeNo.Text = dtD.DefaultView[index]["Memo"].ToString();
                btnQuery_Click(btnQuery, null);
                qMeNo.Clear();
            }
            GridSort("Memo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            query8.ForeColor = Color.Red;
            query8.Enabled = true;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                IsQuery = true;
                btnQuery.Enabled = false;

                if (qCuNo.TrimTextLenth() > 0 && qItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "rlend.CuNo", qCuNo.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", qCuNo.Text.Trim(), "ItNo", qItNo.Text.Trim());
                }
                else if (qStNo.TrimTextLenth() > 0 && qItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "rlend.StNo", qStNo.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", qStNo.Text.Trim(), "ItNo", qItNo.Text.Trim());
                }
                else if (qEmName.TrimTextLenth() > 0 && qDate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(qDate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "rlend.EmNo", qEmName.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "EmNo", qEmName.Text.Trim(), "Ledate", date);

                }
                else if (qLeno.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Eload(dtD, SQL, "rlend.LeNo", qLeno.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "LeNo", qLeno.Text.Trim());
                }
                else if (qDate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(qDate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "rlendd.Ledate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query3_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "Ledate", date);
                }
                else if (qItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "rlendd.ItNo", qItNo.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query4_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItNo", qItNo.Text.Trim());
                }
                else if (qMeNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "rlendd.MeNo", qMeNo.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query8_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "MeNo", qMeNo.Text.Trim());
                }
                else if (qUnit.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "rlendd.itunit", qUnit.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    GridSort("itunit ASC,ItNo ASC");
                    list.ForEach(t => t.ForeColor = Color.Black);
                    query4.ForeColor = Color.Red;
                    dtD.DefaultView.Search(ref dataGridViewT1, "itunit", qUnit.Text.Trim());
                }
                else if (qCuNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "rlend.CuNo", qCuNo.Text.Trim(), "rlendd.Ledate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    query5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", qCuNo.Text.Trim());
                }

            }
            catch
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
                var itno = dtD.DefaultView[index]["ItNo"].ToString().Trim();
                jRLend.Validate<JBS.JS.Item>(itno, row =>
                {
                    frm.image = row["pic"];
                });

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
                var leid = dtD.DefaultView[index]["leid"].ToString().Trim();
                frm.dr = Common.load("Check", "RLendD", "leid", leid);
                frm.ShowDialog();
            }
        }

        private void btnBom_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                string trait = dataGridViewT1["產品組成", dataGridViewT1.CurrentRow.Index].Value.ToString();
                if (trait != "組合品" && trait != "組裝品")
                {
                    MessageBox.Show("只有組合品或組裝品有組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.Focus();
                    return;
                }

                using (FrmSale_Bom frm = new FrmSale_Bom())
                {
                    frm.BoItNo1 = ItNo.Text.Trim();
                    frm.BoItName1 = dataGridViewT1["品名規格", dataGridViewT1.CurrentRow.Index].Value.ToString();
                    frm.JustForBrow = true;
                    frm.TTable = "rlendbom";
                    frm.TKey = dataGridViewT1["BomID", dataGridViewT1.CurrentRow.Index].Value.ToString();
                    frm.ShowDialog();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0)
            {
                TResult = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) TResult = "";
                else
                {
                    TResult = dtD.DefaultView[index]["LeNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }


        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != this.備註說明.Name) btnExit_Click(null, null);
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

                    cmd.CommandText = "Update RLendd Set memo = @memo where Bomid = @Bomid";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (e.Row.Index == dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected))
                {
                    var index = e.Row.Index;

                    TaxMny.Text = dtD.DefaultView[index]["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                    Tax.Text = dtD.DefaultView[index]["Tax"].ToDecimal().ToString("f" + Common.TS);
                    LeMemo.Text = dtD.DefaultView[index]["LeMemo"].ToString();

                    pVar.XX03Validate(dtD.DefaultView[index]["X3No"].ToString(), new TextBox(), X3Name);
                    TotMny.Text = dtD.DefaultView[index]["TotMny"].ToDecimal().ToString("f" + Common.MST);
                    StockQty.Text = dtD.DefaultView[index]["itstockqty"].ToDecimal().ToString("f" + Common.Q);

                    ItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                    ItNoUdf.Text = dtD.DefaultView[index]["ItNoUdf"].ToString();


                    CuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();

                    Xa1No.Text = dtD.DefaultView[index]["Xa1No"].ToString();
                    Xa1Name.Text = dtD.DefaultView[index]["Xa1Name"].ToString();
                    Xa1Par.Text = dtD.DefaultView[index]["Xa1Par"].ToDecimal().ToString("f4");
                }
            }
        }

        private void LeMemo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        string leno = dataGridViewT1.SelectedRows[0].Cells["還入單號"].Value.ToString();
                        string memo = LeMemo.Text.Trim();
                        cmd.Parameters.AddWithValue("lememo", memo);
                        cmd.Parameters.AddWithValue("leno", leno);
                        cmd.CommandText = "update rlend set lememo=@lememo where leno=@leno ";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void qCuNo_DoubleClick(object sender, EventArgs e)
        {
            jRLend.Open<JBS.JS.Cust>(sender);
        }

        private void qStNo_DoubleClick(object sender, EventArgs e)
        {
            jRLend.Open<JBS.JS.Stkroom>(sender);
        }

        private void qItNo_DoubleClick(object sender, EventArgs e)
        {
            jRLend.Open<JBS.JS.Item>(sender);
        }

        private void qEmName_DoubleClick(object sender, EventArgs e)
        {
            jRLend.Open<JBS.JS.Empl>(sender);
        }

        private void qMeNo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(qMeNo, 20);
        }

        private void qDate_Validating(object sender, CancelEventArgs e)
        { 
            if (btnExit.Focused) 
                return;

            jRLend.DateValidate(sender, e, true); 
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                btnQuery_Click(null, null);
            }
            else if (keyData == Keys.F5)
            {
                btnDesp_Click(null, null);
            }
            else if (keyData == Keys.F6)
            {
                btnBom_Click(null, null);
            }
            else if (keyData == Keys.F7)
            {
                btnStock_Click(null, null);
            }
            else if (keyData == Keys.F9)
            {
                btnExit_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                query2_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                query3_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                query4_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                query5_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F6") && keyData.ToString().EndsWith("Shift"))
            {
                query6_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F7") && keyData.ToString().EndsWith("Shift"))
            {
                query7_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F8") && keyData.ToString().EndsWith("Shift"))
            {
                query8_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmLendBrowNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TResult.Length > 0) return;

            if (dtD.Rows.Count == 0)
            {
                TResult = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) TResult = "";
                else
                {
                    TResult = dtD.DefaultView[index]["LeNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FrmRLendBrow_Shown(object sender, EventArgs e)
        {
            qLeno.Focus();
        }











    }
}
