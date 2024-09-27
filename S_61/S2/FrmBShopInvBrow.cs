using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmBShopInvBrow : Formbase, JBS.JS.IxOpen
    {
        JBS.JS.BShopInv jBShopInv;
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        public bool StateChange { get; set; }
        public string SQL { get; set; }
        public string X3No { get; set; }
        DataTable dtD = new DataTable();
        List<ButtonSmallT> list;
        decimal No = 0;

        public FrmBShopInvBrow()
        {
            InitializeComponent();
            this.jBShopInv = new JBS.JS.BShopInv();
            list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7 };

            tInDate.SetDateLength();

            this.數量.Set庫存數量小數();
            this.計量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();

            this.本幣單價.Set本幣金額小數();
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";
            this.本幣稅前金額.Set本幣金額小數();

            this.售價.Set進貨單價小數();
            this.稅前單價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set進項金額小數();

            dataGridViewT1.ReadOnly = false;
            foreach (DataGridViewColumn c in dataGridViewT1.Columns)
            {
                if (c.Equals(this.備註說明))
                    c.ReadOnly = false;
                else
                    c.ReadOnly = true;
            }
            dataGridViewT1.DataSource = dtD.DefaultView;

            textBoxNumberT1.Visible = Common.User_ShopPrice;
            textBoxNumberT2.Visible = Common.User_ShopPrice;
            textBoxNumberT3.Visible = Common.User_ShopPrice;

            Q2.EnabledChanged -= new EventHandler(Q_EnabledChanged);
            Q3.EnabledChanged -= new EventHandler(Q_EnabledChanged);
            Q4.EnabledChanged -= new EventHandler(Q_EnabledChanged);
            Q5.EnabledChanged -= new EventHandler(Q_EnabledChanged);
            Q6.EnabledChanged -= new EventHandler(Q_EnabledChanged);
            Q7.EnabledChanged -= new EventHandler(Q_EnabledChanged);
            btnQuery.EnabledChanged -= new EventHandler(Q_EnabledChanged);

            Q2.EnabledChanged += new EventHandler(Q_EnabledChanged);
            Q3.EnabledChanged += new EventHandler(Q_EnabledChanged);
            Q4.EnabledChanged += new EventHandler(Q_EnabledChanged);
            Q5.EnabledChanged += new EventHandler(Q_EnabledChanged);
            Q6.EnabledChanged += new EventHandler(Q_EnabledChanged);
            Q7.EnabledChanged += new EventHandler(Q_EnabledChanged);
            btnQuery.EnabledChanged += new EventHandler(Q_EnabledChanged);

            dataGridViewT1.RowStateChanged -= new DataGridViewRowStateChangedEventHandler(dataGridViewT1_RowStateChanged);
            dataGridViewT1.RowStateChanged += new DataGridViewRowStateChangedEventHandler(dataGridViewT1_RowStateChanged);
        }

        private void FrmBShopInvBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = "";
            if (Common.User_DateTime == 1)
                SQL = "LEFT(bshopinvd.indate,3)+'/'+SUBSTRING(bshopinvd.indate,4,2)+'/'+RIGHT(bshopinvd.indate,2) 發票日期,";
            else
                SQL = "LEFT(bshopinvd.indate1,4)+'/'+SUBSTRING(bshopinvd.indate1,5,2)+'/'+RIGHT(bshopinvd.indate1,2) 發票日期,";

            SQL += @"
                 bshopinvd.inid,bshopinvd.inno,bshopinvd.indate,bshopinvd.indate1,bshopinvd.cono,bshopinvd.fano,bshopinvd.itno,bshopinvd.itname,bshopinvd.itunit
                ,bshopinvd.punit,bshopinvd.bomid,bshopinvd.bomrec,bshopinvd.ittrait,bshopinvd.itpkgqty,bshopinvd.pqty,bshopinvd.qty,bshopinvd.price
                ,bshopinvd.prs,bshopinvd.rate,bshopinvd.xa1par,bshopinvd.taxprice,bshopinvd.mny,bshopinvd.priceb,bshopinvd.taxpriceb,bshopinvd.mnyb
                ,bshopinvd.memo,bshopinvd.recordno,bshopinv.faname1,bshopinv.coname1,inputday
                From bshopinvd
                Left join bshopinv on bshopinvd.inno = bshopinv.inno
                Where 0=0 ";

            try
            {
                btnQuery.Enabled = false;
                GetData("bshopinvd", "InNo", this.TSeekNo.Trim(), "InNo ASC", Q2);
            }
            finally
            {
                btnQuery.Enabled = true;
            }
        }

        private void FrmBShopInvBrow_Shown(object sender, EventArgs e)
        {
            tInNo.Focus();
        }

        private void Q2_Click(object sender, EventArgs e)
        {
            try
            {
                Q2.Enabled = false;
                GetData("bshopinvd", "InNo", "InNo ASC", Q2);
            }
            finally
            {
                Q2.Enabled = true;
            }
        }

        private void Q3_Click(object sender, EventArgs e)
        {
            try
            {
                Q3.Enabled = false;
                GetData("bshopinvd", "InDate", "InDate DESC", Q3, "DESC");
            }
            finally
            {
                Q3.Enabled = true;
            }
        }

        private void Q4_Click(object sender, EventArgs e)
        {
            try
            {
                Q4.Enabled = false;
                GetData("bshopinvd", "FaNo", "FaNo ASC", Q4);
            }
            finally
            {
                Q4.Enabled = true;
            }
        }

        private void Q5_Click(object sender, EventArgs e)
        {
            try
            {
                Q5.Enabled = false;
                GetData("bshopinvd", "ItNo", "ItNo ASC", Q5);
            }
            finally
            {
                Q5.Enabled = true;
            }
        }

        private void Q6_Click(object sender, EventArgs e)
        {
            try
            {
                Q6.Enabled = false;
                GetData("bshopinvd", "Memo", "Memo ASC", Q6);
            }
            finally
            {
                Q6.Enabled = true;
            }
        }

        private void Q7_Click(object sender, EventArgs e)
        {
            try
            {
                Q7.Enabled = false;
                GetData("bshopinvd", "CoNo", "CoNo ASC", Q7);
            }
            finally
            {
                Q7.Enabled = true;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Enabled = false;

                if (tInNo.TrimTextLenth() > 0)
                {
                    GetData("bshopinvd", "InNo", tInNo.Text.Trim(), "InNo ASC", Q2);
                }
                else if (tInDate.TrimTextLenth() > 0)
                {
                    GetData("bshopinvd", "InDate", tInDate.Text.Trim(), "InDate ASC", Q3);
                }
                else if (tCoNo.TrimTextLenth() > 0)
                {
                    GetData("bshopinvd", "CoNo", tCoNo.Text.Trim(), "CoNo ASC", Q7);
                }
                else if (tFaNo.TrimTextLenth() > 0)
                {
                    GetData("bshopinvd", "FaNo", tFaNo.Text.Trim(), "FaNo ASC", Q4);
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    GetData("bshopinvd", "ItNo", tItNo.Text.Trim(), "ItNo ASC", Q5);
                }
                else if (tMemo.TrimTextLenth() > 0)
                {
                    GetData("bshopinvd", "Memo", tMemo.Text.Trim(), "Memo ASC", Q6);
                }

            }
            finally
            {
                btnQuery.Enabled = true;
            }
        }

        /// <summary>
        /// ※排序按鈕函式※
        /// 1 找到定位點No
        /// 2 以pk撈資料
        /// 3 若No還在, 定位No
        /// 4 若No不在, 定位pk
        /// 5 定位完畢後, 撈表頭資料
        /// </summary>
        void GetData(string table, string pk, string sorter, ButtonSmallT btn)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            No = dtD.DefaultView[index]["InID"].ToDecimal();
            var vl = dtD.DefaultView[index][pk].ToString().Trim();

            dataGridViewT1.DataSource = null;
            Common.NewLoad(ref dtD, SQL, table, pk, vl);
            dataGridViewT1.DataSource = dtD.DefaultView;

            dtD.DefaultView.Sort = sorter;

            var InIDflag = dtD.AsEnumerable().Any(r => r["InID"].ToDecimal() == No);
            if (InIDflag)
                dtD.DefaultView.Search(ref dataGridViewT1, "InID", No.ToString());
            else
                dtD.DefaultView.Search(ref dataGridViewT1, pk, vl);

            list.ForEach(t => t.ForeColor = Color.Black);
            btn.ForeColor = Color.Red;

            index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                Clear();
            else
                Fill(index);
        }

        void GetData(string table, string pk, string sorter, ButtonSmallT btn, string desc = "DESC")
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            No = dtD.DefaultView[index]["InID"].ToDecimal();
            var vl = dtD.DefaultView[index][pk].ToString().Trim();

            dataGridViewT1.DataSource = null;
            Common.NewLoad(ref dtD, SQL, table, pk, vl, "DESC");
            dataGridViewT1.DataSource = dtD.DefaultView;

            dtD.DefaultView.Sort = sorter;

            var InIDflag = dtD.AsEnumerable().Any(r => r["InID"].ToDecimal() == No);
            if (InIDflag)
                dtD.DefaultView.Search(ref dataGridViewT1, "InID", No.ToString());
            else
                dtD.DefaultView.Search(ref dataGridViewT1, pk, vl);

            list.ForEach(t => t.ForeColor = Color.Black);
            btn.ForeColor = Color.Red;

            index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                Clear();
            else
                Fill(index);
        }

        /// <summary>
        /// ※搜尋按鈕函式※
        /// 1 以pk撈資料
        /// 2 以pk定位
        /// 3 定位完畢後, 撈表頭資料
        /// </summary>
        void GetData(string table, string pk, string pkValue, string sorter, ButtonSmallT btn)
        {
            dataGridViewT1.DataSource = null;
            Common.NewLoad(ref dtD, SQL, table, pk, pkValue);
            dataGridViewT1.DataSource = dtD.DefaultView;

            dtD.DefaultView.Sort = sorter;
            dtD.DefaultView.Search(ref dataGridViewT1, pk, pkValue);

            list.ForEach(t => t.ForeColor = Color.Black);
            btn.ForeColor = Color.Red;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                Clear();
            else
                Fill(index);
        }

        void GetData(string table, string pk, string pkValue, string sorter, ButtonSmallT btn, string desc = "DESC")
        {
            dataGridViewT1.DataSource = null;
            Common.NewLoad(ref dtD, SQL, table, pk, pkValue, "DESC");
            dataGridViewT1.DataSource = dtD.DefaultView;

            dtD.DefaultView.Sort = sorter;
            dtD.DefaultView.Search(ref dataGridViewT1, pk, pkValue);

            list.ForEach(t => t.ForeColor = Color.Black);
            btn.ForeColor = Color.Red;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                Clear();
            else
                Fill(index);
        }

        void Fill(int index)
        {
            Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                var inno = dtD.DefaultView[index]["inno"].ToString().Trim();
                cmd.Parameters.AddWithValue("inno", inno);

                cn.Open();
                cmd.CommandText = " Select * from bshopinv where inno = @inno ";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == false)
                        return;

                    reader.Read();
                    textBoxNumberT1.Text = reader["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                    textBoxNumberT2.Text = reader["Tax"].ToDecimal().ToString("f" + Common.TS);
                    textBoxNumberT3.Text = reader["TotMny"].ToDecimal().ToString("f" + Common.MST);
                    textBoxNumberT4.Text = reader["Xa1Par"].ToDecimal().ToString("f3");
                    X3No = reader["X3No"].ToString().Trim();
                    textBoxT7.Text = reader["CoNo"].ToString().Trim();
                    textBoxT2.Text = reader["FaNo"].ToString().Trim();
                    textBoxT3.Text = reader["InvTaxNo"].ToString().Trim();
                    textBoxT4.Text = reader["InvName"].ToString().Trim();
                    textBoxT5.Text = reader["InvAddr1"].ToString().Trim();
                    textBoxT6.Text = reader["InMemo"].ToString().Trim();
                    checkBoxT1.Checked = (reader["invalid"].ToDecimal() == 1);
                    this.TSeekNo = inno;
                }
            }
            pVar.XX03Validate(X3No, textBoxT1, textBoxT1);
        }

        void Clear()
        {
            this.TSeekNo = string.Empty;
            this.X3No = string.Empty;
            textBoxNumberT1.Clear();
            textBoxNumberT2.Clear();
            textBoxNumberT3.Clear();
            textBoxNumberT4.Clear();
            textBoxT1.Clear();
            textBoxT2.Clear();
            textBoxT3.Clear();
            textBoxT4.Clear();
            textBoxT5.Clear();
            textBoxT6.Clear();
            textBoxT7.Clear();
            checkBoxT1.Checked = false;
        }

        private void Q_EnabledChanged(object sender, EventArgs e)
        {
            dataGridViewT1.RowStateChanged -= new DataGridViewRowStateChangedEventHandler(dataGridViewT1_RowStateChanged);

            if ((sender as ButtonSmallT).Enabled)
                dataGridViewT1.RowStateChanged += new DataGridViewRowStateChangedEventHandler(dataGridViewT1_RowStateChanged);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (e.Row.Index != index)
                return;

            Fill(index);
        }

        private void tFaNo_DoubleClick(object sender, EventArgs e)
        {
            jBShopInv.Open<JBS.JS.Fact>(sender);
        }

        private void tItNo_DoubleClick(object sender, EventArgs e)
        {
            jBShopInv.Open<JBS.JS.Item>(sender);
        }

        private void tInDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jBShopInv.DateValidate(sender, e, true);
        }

        private void tCoNo_DoubleClick(object sender, EventArgs e)
        {
            jBShopInv.Open<JBS.JS.Comp>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            this.TResult = dtD.Rows[index]["InNo"].ToString().Trim();

            this.Dispose();
        }

        private void FrmSaleInvBrow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.TResult != null && this.TResult.Length > 0)
                return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            this.TResult = dtD.Rows[index]["InNo"].ToString().Trim();
        }

        private void btnInputDay_Click(object sender, EventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            No = dtD.DefaultView[index]["InID"].ToDecimal();
            var vl = dtD.DefaultView[index]["InputDay"].ToString().Trim();

            dataGridViewT1.DataSource = null;
            Common.Cload(dtD, SQL, "bshopinv.InputDay", Date.GetDateTime(2));
            dataGridViewT1.DataSource = dtD.DefaultView;

            dtD.DefaultView.Sort = "InputDay ASC";

            var InIDflag = dtD.AsEnumerable().Any(r => r["InID"].ToDecimal() == No);
            if (InIDflag)
                dtD.DefaultView.Search(ref dataGridViewT1, "InID", No.ToString());
            else
                dtD.DefaultView.Search(ref dataGridViewT1, "InputDay", vl);

            list.ForEach(t => t.ForeColor = Color.Black);
            btnInputDay.ForeColor = Color.Red;

            index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                Clear();
            else
                Fill(index);
        }
    }
}
