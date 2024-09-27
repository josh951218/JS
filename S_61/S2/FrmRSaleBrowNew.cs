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
    public partial class FrmRSaleBrowNew : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        JBS.JS.RSale jRSale; 
        DataTable dtD = new DataTable();
        List<ButtonSmallT> list;
        string SQL = "";
        decimal No = 0;
        decimal TotalCount = 0;
        bool IsQuery;
        decimal Query = 0;

        public FrmRSaleBrowNew()
        {
            InitializeComponent();
            this.jRSale = new JBS.JS.RSale();
            list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT21.Text = Common.Sys_MemoUdf;

            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.Set本幣金額小數();
            this.售價.Set銷貨單價小數();
            this.稅前金額.Set銷項金額小數();
            this.銷退數量.Set庫存數量小數();
            this.銷退日期.DataPropertyName = Common.User_DateTime == 1 ? "銷退日期" : "銷退日期1";
            this.tSadate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

            dataGridViewT1.ReadOnly = false;
            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name != this.備註說明.Name) dataGridViewT1.Columns[i].ReadOnly = true;
            }

            dataGridViewT1.DataSource = dtD.DefaultView;

            this.本幣單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.稅前單價.Visible = Common.User_SalePrice;

            this.textBoxT1.Visible = Common.User_SalePrice;
            this.textBoxT2.Visible = Common.User_SalePrice;
            this.textBoxT5.Visible = Common.User_SalePrice;
            this.textBoxT6.Visible = Common.User_SalePrice;

        }

        private void FrmSaleBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
+ " LEFT(RSaled.sadate,3)+'/'+SUBSTRING(RSaled.sadate,4,2)+'/'+RIGHT(RSaled.sadate,2) 銷退日期,"
+ " LEFT(RSaled.sadate1,4)+'/'+SUBSTRING(RSaled.sadate1,5,2)+'/'+RIGHT(RSaled.sadate1,2) 銷退日期1,"
+ " 產品組成= case when RSaled.ittrait=1 then '組合品' when RSaled.ittrait=2 then '組裝品' when RSaled.ittrait=3 then '單一商品' end,"
+ " RSaled.saID, RSaled.sano, RSaled.sadate, RSaled.sadate1, RSaled.sadateac, RSaled.sadateac1, RSaled.quno, RSaled.stno,"
+ " RSaled.orno, RSaled.itno, RSaled.itname, RSaled.ittrait, RSaled.itunit, RSaled.itpkgqty, RSaled.qty, RSaled.price, RSaled.prs,"
+ " RSaled.taxprice, RSaled.mny, RSaled.priceb, RSaled.taxpriceb, RSaled.mnyb, RSaled.memo, RSaled.Bomid, RSaled.stName, RSaled.mqty, "
+ " RSaled.munit, RSaled.mlong, RSaled.mwidth1, RSaled.mwidth2, RSaled.mwidth3, RSaled.mwidth4, RSaled.mformula,"
+ " RSaled.Punit, RSaled.Pqty, RSaled.Pformula, RSale.cuno, RSale.cuname2, RSale.cuname1, RSale.cutel1,"
+ " RSale.cuper1, RSale.emno, RSale.emname, RSale.spno, RSale.spname, RSale.xa1no, RSale.Xa1Name, RSale.xa1par,"
+ " RSale.taxmnyf, RSale.taxmnyb, RSale.taxmny, RSale.x3no, RSale.rate, RSale.x5no, RSale.seno, RSale.sename, RSale.x4no,"
+ " RSale.x4name, RSale.tax, RSale.totmny, RSale.taxb, RSale.totmnyb, RSale.acctmny, RSale.samemo, RSale.invno, RSale.invdate,"
+ " RSale.invdate1, RSale.invname, RSale.invtaxno, RSale.invaddr1, RSale.invbatch, RSale.invbatflg, RSale.appdate, RSale.edtdate,"
+ " RSale.appscno, RSale.edtscno, RSale.DeNo, RSale.DeName, RSale.Bracket, item.itnoudf, item.itstockqty , RSaled.standard  "
+ " FROM RSaled LEFT OUTER JOIN"
+ " RSale ON RSaled.sano = RSale.sano LEFT OUTER JOIN"
+ " item ON RSaled.itno = item.itno"
+ " WHERE (0 = 0)";


            tSaNo.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            if (Query == 0) IsQuery = false;
            Query++;
            tSaNo.Clear();


            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from RSaled ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["SaID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("SaID = " + No);

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
                tSaNo.Text = dtD.DefaultView[index]["SaNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tSaNo.Clear();
            }
            GridSort("SaNo ASC");
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
                tSaNo.Text = dtD.DefaultView[index]["SaNo"].ToString();
                tSadate.Text = dtD.DefaultView[index]["sadate"].ToString();
                btnQuery_Click(btnQuery, null);
                tSaNo.Clear(); tSadate.Clear();
            }
            GridSort("sadate DESC,SaNo ASC");
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
                tSadate.Text = dtD.DefaultView[index]["sadate"].ToString();
                btnQuery_Click(btnQuery, null);
                tItNo.Clear(); tSadate.Clear();
            }
            GridSort("ItNo ASC,sadate ASC");
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
                tCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                tSadate.Text = dtD.DefaultView[index]["sadate"].ToString();
                btnQuery_Click(btnQuery, null);
                tCuNo.Clear(); tSadate.Clear();
            }
            GridSort("CuNo ASC,sadate DESC");
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
                tOrNo.Text = dtD.DefaultView[index]["OrNo"].ToString();
                tSadate.Text = dtD.DefaultView[index]["sadate"].ToString();
                btnQuery_Click(btnQuery, null);
                tOrNo.Clear(); tSadate.Clear();
            }
            GridSort("OrNo ASC,sadate ASC");
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
                tOrNo.Text = dtD.DefaultView[index]["OrNo"].ToString();
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tOrNo.Clear(); tItNo.Clear();
            }
            GridSort("OrNo ASC,ItNo ASC");
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
                tStNo.Text = dtD.DefaultView[index]["StNo"].ToString();
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tStNo.Clear(); tItNo.Clear();
            }
            GridSort("StNo ASC,ItNo ASC");
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

                Q11.Enabled = false;
                tEmNo.Text = dtD.DefaultView[index]["EmNo"].ToString();
                tCuNo.Text = dtD.DefaultView[index]["CuNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear(); tCuNo.Clear();
            }
            GridSort("EmNo ASC,CuNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q11.ForeColor = Color.Red;
            Q11.Enabled = true;
        }

        private void Q12_Click(object sender, EventArgs e)
        {
            if (!IsQuery)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                tstandard.Text = dtD.DefaultView[index]["standard"].ToString();
                tSadate.Text = dtD.DefaultView[index]["sadate"].ToString();
                btnQuery_Click(btnQuery, null);
                tItNo.Clear(); tSadate.Clear();
            }
            GridSort("standard ASC,sadate ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                IsQuery = true;
                btnQuery.Enabled = false;

                if (tCuNo.TrimTextLenth() > 0 && tSadate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tSadate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSale.CuNo", tCuNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q5_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim(), "Sadate", date);
                }
                else if (tOrNo.TrimTextLenth() > 0 && tSadate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tSadate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSaled.OrNo", tOrNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "OrNo", tOrNo.Text.Trim(), "Sadate", date);
                }
                else if (tCuNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSale.CuNo", tCuNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tOrNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSaled.OrNo", tOrNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q8_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "OrNo", tOrNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tStNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSaled.StNo", tStNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", tStNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0 && tCuNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSale.EmNo", tEmNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q11_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "CuNo", tCuNo.Text.Trim());
                }
                else if (tSaNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Eload(dtD, SQL, "RSale.SaNo", tSaNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "SaNo", tSaNo.Text.Trim());
                }
                else if (tOrNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSaled.OrNo", tOrNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "OrNo", tOrNo.Text.Trim());
                }
                else if (tSadate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tSadate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "RSaled.Sadate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q3_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "Sadate", date);
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSaled.ItNo", tItNo.Text.Trim(), "RSaled.Sadate");
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
                        Common.Eload(dtD, SQL, "RSaled.Memo", tMemo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q10_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "Memo", tMemo.Text.Trim());
                }
                else if (tStNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSaled.StNo", tStNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", tStNo.Text.Trim());
                }
                else if (tInvNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Cload(dtD, SQL, "RSale.InvNo", tInvNo.Text.Trim());
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    GridSort("InvNo ASC");
                    list.ForEach(t => t.ForeColor = Color.Black);
                    dtD.DefaultView.Search(ref dataGridViewT1, "InvNo", tInvNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSale.EmNo", tEmNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q11_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim());
                }
                else if (tCuNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSale.CuNo", tCuNo.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", tCuNo.Text.Trim());
                }
                else if (tstandard.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "RSaled.standard", tstandard.Text.Trim(), "RSaled.Sadate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q12_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "standard", tstandard.Text.ToString());
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

                    textBoxT1.Text = dtD.DefaultView[index]["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                    textBoxT2.Text = dtD.DefaultView[index]["Tax"].ToDecimal().ToString("f" + Common.TS);
                    textBoxT3.Text = dtD.DefaultView[index]["SaMemo"].ToString();
                    textBoxT4.Text = dtD.DefaultView[index]["InvNo"].ToString();

                    pVar.XX03Validate(dtD.DefaultView[index]["X3No"].ToString(), new TextBox(), textBoxT5);
                    textBoxT6.Text = dtD.DefaultView[index]["TotMny"].ToDecimal().ToString("f" + Common.MST);
                    textBoxT17.Text = dtD.DefaultView[index]["ItStockQty"].ToDecimal().ToString("f" + Common.Q);

                    textBoxT9.Text = dtD.DefaultView[index]["ItNo"].ToString();
                    textBoxT10.Text = dtD.DefaultView[index]["ItNoUdf"].ToString();
                    textBoxT15.Text = dtD.DefaultView[index]["StNo"].ToString();
                    textBoxT16.Text = dtD.DefaultView[index]["StName"].ToString();

                    textBoxT12.Text = dtD.DefaultView[index]["CuNo"].ToString();
                    textBoxT13.Text = dtD.DefaultView[index]["CuPer1"].ToString();
                    textBoxT14.Text = dtD.DefaultView[index]["CuTel1"].ToString();

                    textBoxT7.Text = dtD.DefaultView[index]["Xa1No"].ToString();
                    textBoxT8.Text = dtD.DefaultView[index]["Xa1Name"].ToString();
                    textBoxT11.Text = dtD.DefaultView[index]["Xa1Par"].ToDecimal().ToString("f4");
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
                var said = dtD.DefaultView[index]["SaID"].ToString().Trim();
                frm.dr = Common.load("Check", "RSaled", "SaID", said);
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
                frm.TTable = "RSalebom";
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
                TResult = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) TResult = "";
                else
                {
                    TResult = dtD.DefaultView[index]["SaNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnInv_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var sano = dtD.DefaultView[index]["SaNo"].ToString().Trim();
            var IsPOS = dtD.DefaultView[index]["Bracket"].ToString().Trim() == "前台" ? true : false;

            using (subMenuFm_2.FrmSale_Inv frm = new subMenuFm_2.FrmSale_Inv("RSale", sano, IsPOS))
            {
                frm.Result[0] = Common.User_DateTime == 1 ? dtD.DefaultView[index]["InvDate"].ToString() : dtD.DefaultView[index]["InvDate1"].ToString();
                frm.Result[1] = dtD.DefaultView[index]["InvName"].ToString();
                frm.Result[2] = dtD.DefaultView[index]["InvTaxNo"].ToString();
                frm.Result[3] = dtD.DefaultView[index]["InvAddr1"].ToString();

                if (frm.ShowDialog() == DialogResult.OK) textBoxT4.Text = frm.ivno;
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

                    cmd.CommandText = "Update RSaled Set memo = @memo where Bomid = @Bomid";
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
            else if (keyData.ToString().StartsWith("F11") && keyData.ToString().EndsWith("Shift"))
            {
                Q11_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textBoxT3_Validating(object sender, CancelEventArgs e)
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
                    cmd.Parameters.AddWithValue("@SaMemo", textBoxT3.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaNo", dtD.DefaultView[index]["SaNo"].ToString().Trim());

                    cmd.CommandText = "Update RSale Set SaMemo=@SaMemo where SaNo=@SaNo ";
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
            jRSale.Open<JBS.JS.Cust>(sender);
        }

        private void tItNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Item>(sender);
        }

        private void tStNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Stkroom>(sender);
        }

        private void tEmNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Empl>(sender);
        }

        private void tSadate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused) 
                return;

            jRSale.DateValidate(sender, e, true); 
        }

        private void FrmSaleBrowNew_FormClosing(object sender, FormClosingEventArgs e)
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
                    TResult = dtD.DefaultView[index]["SaNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FrmRSaleBrowNew_Shown(object sender, EventArgs e)
        {
            tSaNo.Focus();
        }

        private void labelT1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var row = Common.load("Check", "rsale", "sano",dtD.DefaultView[index]["SaNo"].ToString());
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
