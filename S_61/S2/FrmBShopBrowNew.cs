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
    public partial class FrmBShopBrowNew : Formbase
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

        public FrmBShopBrowNew()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT21.Text = Common.Sys_MemoUdf;

            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.Set本幣金額小數();
            this.進價.Set進貨單價小數();
            this.稅前金額.Set進項金額小數();
            this.進貨數量.Set庫存數量小數();
            this.進貨日期.DataPropertyName = Common.User_DateTime == 1 ? "進貨日期" : "進貨日期1";
            tBsdate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

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

            textBoxT1.Visible = textBoxT2.Visible = textBoxT5.Visible = textBoxT6.Visible = Common.User_ShopPrice;
            btnInv.Enabled = Common.User_ShopPrice;
        }

        private void FrmBShopBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
+ " LEFT(bshopd.bsdate,3)+'/'+SUBSTRING(bshopd.bsdate,4,2)+'/'+RIGHT(bshopd.bsdate,2) 進貨日期,"
+ " LEFT(bshopd.bsdate1,4)+'/'+SUBSTRING(bshopd.bsdate1,5,2)+'/'+RIGHT(bshopd.bsdate1,2) 進貨日期1,"
+ " 產品組成= case when bshopd.ittrait=1 then '組合品' when bshopd.ittrait=2 then '組裝品' when bshopd.ittrait=3 then '單一商品' end,"
+ " bshopd.bsid, bshopd.bsno, bshopd.bsdate, bshopd.bsdate1, bshopd.bsdateac, bshopd.bsdateac1, bshopd.fqno,"
+ " bshopd.fano, bshopd.stno, bshopd.StName, bshopd.fono, bshopd.itno, bshopd.itname, bshopd.ittrait, bshopd.itunit,"
+ " bshopd.itpkgqty, bshopd.qty, bshopd.price, bshopd.prs, bshopd.taxprice, bshopd.mny, bshopd.priceb,"
+ " bshopd.taxpriceb, bshopd.mnyb, bshopd.memo, bshopd.bomid, bshopd.serno, bshopd.RealCost, bshopd.foid,"
+ " bshopd.Punit, bshopd.Pqty, bshopd.Pformula, bshop.faname2, bshop.faname1, bshop.fatel1, bshop.faper1,"
+ " bshop.emno, bshop.emname, bshop.spno, bshop.spname, bshop.xa1no, bshop.xa1name, bshop.xa1par,"
+ " bshop.taxmnyb, bshop.taxmny, bshop.x3no, bshop.x5no, bshop.seno, bshop.sename, bshop.x4no, bshop.x4name,"
+ " bshop.tax, bshop.totmny, bshop.taxb, bshop.totmnyb, bshop.bsmemo, bshop.bsmemo1, bshop.invno, bshop.invdate,"
+ " bshop.invdate1, bshop.invname, bshop.invtaxno, bshop.invaddr1, item.itnoudf, item.itstockqty,bshop.invkind,bshop.sub,bshop.customsno"
+ " FROM  bshopd LEFT OUTER JOIN"
+ " bshop ON bshopd.bsno = bshop.bsno LEFT OUTER JOIN"
+ " item ON bshopd.itno = item.itno"
+ " WHERE  (0 = 0)";


            tBsNo.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            if (Query == 0) IsQuery = false;
            Query++;
            tBsNo.Clear();


            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from bshopd ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["BsID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("BsID = " + No);

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
                tBsNo.Text = dtD.DefaultView[index]["BsNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tBsNo.Clear();
            }
            GridSort("BsNo ASC");
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
                tBsNo.Text = dtD.DefaultView[index]["BsNo"].ToString();
                tBsdate.Text = dtD.DefaultView[index]["bsdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tBsNo.Clear();
                tBsdate.Clear();
            }
            GridSort("bsdate DESC,BsNo ASC");
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
                tBsdate.Text = dtD.DefaultView[index]["bsdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tItNo.Clear();
                tBsdate.Clear();
            }
            GridSort("ItNo ASC,bsdate DESC");
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
                tFaNo.Text = dtD.DefaultView[index]["FaNo"].ToString();
                tBsdate.Text = dtD.DefaultView[index]["bsdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tFaNo.Clear();
                tBsdate.Clear();
            }
            GridSort("FaNo ASC,bsdate DESC");
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
                tFoNo.Text = dtD.DefaultView[index]["FoNo"].ToString();
                tBsdate.Text = dtD.DefaultView[index]["bsdate"].ToString();
                btnQuery_Click(btnQuery, null);
                tFoNo.Clear();
                tBsdate.Clear();
            }
            GridSort("FoNo ASC,bsdate ASC");
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
                tFaNo.Clear();
                tItNo.Clear();
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
                tFoNo.Text = dtD.DefaultView[index]["FoNo"].ToString();
                tItNo.Text = dtD.DefaultView[index]["ItNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tFoNo.Clear();
                tItNo.Clear();
            }
            GridSort("FoNo ASC,ItNo ASC");
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
                tStNo.Clear();
                tItNo.Clear();
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
                tFaNo.Text = dtD.DefaultView[index]["FaNo"].ToString();
                btnQuery_Click(btnQuery, null);
                tEmNo.Clear();
                tFaNo.Clear();
            }
            GridSort("EmNo ASC,FaNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q11.ForeColor = Color.Red;
            Q11.Enabled = true;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                IsQuery = true;
                btnQuery.Enabled = false;

                if (tFaNo.TrimTextLenth() > 0 && tBsdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tBsdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.FaNo", tFaNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q5_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim(), "Bsdate", date);
                }
                else if (tFoNo.TrimTextLenth() > 0 && tBsdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tBsdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.FoNo", tFoNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "FoNo", tFoNo.Text.Trim(), "Bsdate", date);
                }
                else if (tFaNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.FaNo", tFaNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q7_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tFoNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.FoNo", tFoNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q8_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FoNo", tFoNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tStNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.StNo", tStNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q9_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", tStNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0 && tFaNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.EmNo", tEmNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q11_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim(), "FaNo", tFaNo.Text.Trim());
                }
                else if (tBsNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Eload(dtD, SQL, "BShopd.BsNo", tBsNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "BsNo", tBsNo.Text.Trim());
                }
                else if (tFoNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.FoNo", tFoNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FoNo", tFoNo.Text.Trim());
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.ItNo", tItNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q4_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItNo", tItNo.Text.Trim());
                }
                else if (tBsdate.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    var date = Date.ToTWDate(tBsdate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "BShopd.Bsdate", date);
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q3_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "Bsdate", date);
                }
                else if (tMemo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.Memo", tMemo.Text.Trim(), "BShopd.Bsdate");
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
                        Common.Eload(dtD, SQL, "BShopd.StNo", tStNo.Text.Trim(), "BShopd.Bsdate");
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
                        Common.Cload(dtD, SQL, "BShopd.InvNo", tInvNo.Text.Trim());
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
                        Common.Eload(dtD, SQL, "BShopd.EmNo", tEmNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q11_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "EmNo", tEmNo.Text.Trim());
                }
                else if (tFaNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "BShopd.FaNo", tFaNo.Text.Trim(), "BShopd.Bsdate");
                    }
                    dataGridViewT1.DataSource = dtD.DefaultView;

                    Q5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "FaNo", tFaNo.Text.Trim());
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
                    textBoxT3.Text = dtD.DefaultView[index]["BsMemo"].ToString();
                    textBoxT4.Text = dtD.DefaultView[index]["InvNo"].ToString();

                    pVar.XX03Validate(dtD.DefaultView[index]["X3No"].ToString(), new TextBox(), textBoxT5);
                    textBoxT6.Text = dtD.DefaultView[index]["TotMny"].ToDecimal().ToString("f" + Common.MST);
                    textBoxT17.Text = dtD.DefaultView[index]["ItStockQty"].ToDecimal().ToString("f" + Common.Q);

                    textBoxT9.Text = dtD.DefaultView[index]["ItNo"].ToString();
                    textBoxT10.Text = dtD.DefaultView[index]["ItNoUdf"].ToString();
                    textBoxT15.Text = dtD.DefaultView[index]["StNo"].ToString();
                    textBoxT16.Text = dtD.DefaultView[index]["StName"].ToString();

                    textBoxT12.Text = dtD.DefaultView[index]["FaNo"].ToString();
                    textBoxT13.Text = dtD.DefaultView[index]["FaPer1"].ToString();
                    textBoxT14.Text = dtD.DefaultView[index]["FaTel1"].ToString();

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
                var bsid = dtD.DefaultView[index]["BsID"].ToString().Trim();
                frm.dr = Common.load("Check", "BShopD", "BsID", bsid);
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
                frm.TTable = "BShopBom";
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
                    this.TResult = dtD.DefaultView[index]["BsNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnInv_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var bsno = dtD.DefaultView[index]["BsNo"].ToString().Trim();
            var IsPOS = false;

            using (subMenuFm_2.FrmBShop_Inv frm = new subMenuFm_2.FrmBShop_Inv("BShop", bsno, IsPOS))
            {
                frm.Result[0] = Common.User_DateTime == 1 ? dtD.DefaultView[index]["InvDate"].ToString() : dtD.DefaultView[index]["InvDate1"].ToString();
                frm.Result[1] = dtD.DefaultView[index]["InvName"].ToString();
                frm.Result[2] = dtD.DefaultView[index]["InvTaxNo"].ToString();
                frm.Result[3] = dtD.DefaultView[index]["InvAddr1"].ToString();

                //媒體申報
                if (dtD.DefaultView[index]["invkind"].ToString() != "")
                    frm.InvKind.SelectedIndex = frm.InvKind.FindString(dtD.DefaultView[index]["invkind"].ToString());
                if (dtD.DefaultView[index]["customsno"].ToString() != "")
                    frm.CustomsNo.Text = dtD.DefaultView[index]["customsno"].ToString();
                if (dtD.DefaultView[index]["sub"].ToString() != "")
                    frm.Sub.SelectedIndex = frm.Sub.FindString(dtD.DefaultView[index]["sub"].ToString());

                //媒體申報
                frm.媒體申報.Enabled = false;

                if (frm.ShowDialog() == DialogResult.OK) textBoxT4.Text = frm.ivno;
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
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

                    cmd.CommandText = "Update BShopd Set memo = @memo where Bomid = @Bomid";
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
                    cmd.Parameters.AddWithValue("@BsMemo", textBoxT3.Text.Trim());
                    cmd.Parameters.AddWithValue("@BsNo", dtD.DefaultView[index]["BsNo"].ToString().Trim());

                    cmd.CommandText = "Update BShop Set BsMemo=@BsMemo where BsNo=@BsNo ";
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

        private void tStNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void tEmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void tBsdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused) 
                return;

            xe.DateValidate(sender, e, true); 
        }

        private void FrmBShopBrowNew_FormClosing(object sender, FormClosingEventArgs e)
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
                    this.TResult = dtD.DefaultView[index]["BsNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FrmBShopBrowNew_Shown(object sender, EventArgs e)
        {
            tBsNo.Focus();
        }

        private void labelT1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var row = Common.load("Check", "bshop", "BsNo", dtD.DefaultView[index]["BsNo"].ToString());
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
