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
    public partial class FrmOutStkBrow : Formbase
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

        public FrmOutStkBrow()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;


            this.包裝數量.Set庫存數量小數();
            this.領出數量.Set庫存數量小數();
            this.寄庫未領數量.Set庫存數量小數();
            this.領出日期.DataPropertyName = Common.User_DateTime == 1 ? "領出日期" : "領出日期1";
            this.寄庫日期.DataPropertyName = Common.User_DateTime == 1 ? "indate" : "indate1";
            this.toudate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

            dataGridViewT1.ReadOnly = false;
            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name != this.備註說明.Name) dataGridViewT1.Columns[i].ReadOnly = true;
            }

            dataGridViewT1.DataSource = dtD.DefaultView;
        }

        private void FrmOutStkBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = ""
              + " LEFT(OuStkd.oudate,3)+'/'+SUBSTRING(OuStkd.oudate,4,2)+'/'+RIGHT(OuStkd.oudate,2) 領出日期,"
              + " LEFT(OuStkd.oudate1,4)+'/'+SUBSTRING(OuStkd.oudate1,5,2)+'/'+RIGHT(OuStkd.oudate1,2) 領出日期1,"
              + " 產品組成= case when OuStkd.ittrait=1 then '組合品' when OuStkd.ittrait=2 then '組裝品' when OuStkd.ittrait=3 then '單一商品' end,"
              + " OuStkD.ouID,OuStkD.ouno,OuStkD.inno,OuStkD.sano,OuStkD.oudate,OuStkD.OUDATE1,OuStkD.oudateac,OuStkD.oudateac1,OuStkD.oudateac2,OuStkD.quno,OuStkD.stno,OuStkD.stname, "
              + " OUSTKD.ORNO,OUSTKD.ITNO,OUSTKD.ITNAME,OUSTKD.ittrait,OuStkD.itunit,OuStkD.itpkgqty,OuStkD.ouqty,OuStkD.inqty,OuStkD.qty,OuStkD.price,OuStkD.prs,"
              + " OuStkD.taxprice,OuStkD.mny,OuStkD.priceb,OuStkD.taxpriceb,OuStkD.mnyb,OuStkD.memo,OuStkD.bomid,OuStkD.bomrec,OuStkD.indate,OuStkD.indate1,"
              + " OuStk.cuno,OuStk.cuname1,OuStk.cutel1,OuStk.cuper1,OuStk.emno,OuStk.emname,OuStk.xa1no,OuStk.Xa1Name,OuStk.xa1par,OuStk.taxmnyf,OuStk.taxmnyb,OuStk.taxmny,OuStk.x3no, "
              + " OUSTK.rate,OuStk.x5no,OuStk.seno,OuStk.sename,OuStk.x4no,OuStk.x4name,OuStk.tax,OuStk.totmny,OuStk.taxb,OuStk.totmnyb,OuStk.acctmny,OuStk.oumemo,OuStk.oumemo1,item.itnoudf, item.itstockqty"
              + " FROM OuStkD LEFT OUTER JOIN"
              + " OuStk ON OuStkD.ouno = OuStk.ouno LEFT OUTER JOIN"
              + " item ON OuStkD.itno = item.itno"
              + " WHERE (0 = 0)";

            touno.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            touno.Clear();

            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }

        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from OuStkD ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }

        void GridSort(string sorter)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["ouID"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("ouID = " + No);

            if (index == -1) return;
            if (index >= dtD.Rows.Count) index = dtD.Rows.Count - 1;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        private void Q2_Click(object sender, EventArgs e)
        {
            GridSort("OuNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q2.ForeColor = Color.Red;
        }

        private void Q3_Click(object sender, EventArgs e)
        {
            GridSort("Oudate DESC,OuNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q3.ForeColor = Color.Red;
        }

        private void Q4_Click(object sender, EventArgs e)
        {
            GridSort("Itno ASC,Oudate ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q4.ForeColor = Color.Red;
        }

        private void Q5_Click(object sender, EventArgs e)
        {
            GridSort("CuNo ASC,Oudate DESC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q5.ForeColor = Color.Red;
        }

        private void Q6_Click(object sender, EventArgs e)
        {
            GridSort("InNo ASC,Oudate ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q6.ForeColor = Color.Red;
        }

        private void Q7_Click(object sender, EventArgs e)
        {
            GridSort("CuNo ASC,ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q7.ForeColor = Color.Red;
        }

        private void Q8_Click(object sender, EventArgs e)
        {
            GridSort("StNo ASC,ItNo ASC");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q8.ForeColor = Color.Red;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Enabled = false;
                if (tcuno.TrimTextLenth() > 0 && toudate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(toudate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "OuStkD.CuNo", tcuno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q5_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "CuNo", tcuno.Text.Trim(), "Oudate", date);
                }
                else if (tinno.TrimTextLenth() > 0 && toudate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(toudate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Cload(dtD, SQL, "OuStkD.InNo", tcuno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q6_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "InNo", tcuno.Text.Trim(), "Oudate", date);

                }
                else if (tcuno.TrimTextLenth() > 0 && titno.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "OuStkD.CuNo", tcuno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q7_Click(null, null);
                    dtD.DefaultView.SearchForDate(ref dataGridViewT1, "CuNo", tcuno.Text.Trim(), "ItNo", titno.Text.Trim());
                }
                else if (tstno.TrimTextLenth() > 0 && titno.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "OuStkD.StNo", tstno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q8_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", tstno.Text.Trim(), "ItNo", titno.Text.Trim());
                }
                else if (touno.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount || TotalCount == 0)
                    {
                        Common.Cload(dtD, SQL, "OuStkD.OuNo", touno.Text.Trim());
                    }
                    Q2_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "OuNo", touno.Text.Trim());
                }
                else if (toudate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(toudate.Text.Trim());
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Dload(dtD, SQL, "OuStkD.Oudate", date);
                    }
                    Q3_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "Oudate", date);
                }
                else if (titno.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "OuStkD.ItNo", titno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q4_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "ItNo", titno.Text.Trim());
                }
                else if (tcuno.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "OuStkD.CuNo", tcuno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q5_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", tcuno.Text.Trim());
                }
                else if (tinno.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "OuStkD.InNo", tinno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q6_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "InNo", tinno.Text.Trim());
                }
                else if (tstno.TrimTextLenth() > 0)
                {
                    if (dtD.Rows.Count < TotalCount)
                    {
                        Common.Eload(dtD, SQL, "OuStkD.StNo", tstno.Text.Trim(), "OuStkD.Oudate");
                    }
                    Q8_Click(null, null);
                    dtD.DefaultView.Search(ref dataGridViewT1, "StNo", tstno.Text.Trim());
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
            if (dataGridViewT1.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (var frm = new JE.SOther.FrmDesp(false, FormStyle.Mini))
            {
                var ouid = dtD.DefaultView[index]["ouID"].ToString();
                frm.dr = Common.load("Check", "Oustkd", "ouID", ouid);
                frm.SeekNo = ouid;
                frm.ShowDialog();
            }
        }

        private void btnBom_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var trait = dtD.DefaultView[index]["ittrait"].ToDecimal();

            if (trait != 1 && trait != 2)
            {
                MessageBox.Show("只有組合品或組裝品可編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridViewT1.Focus();
                return;
            }

            using (var frm = new subMenuFm_2.FrmSale_Bom())
            {
                frm.BoItNo1 = dtD.DefaultView[index]["itno"].ToString().Trim();
                frm.BoItName1 = dtD.DefaultView[index]["itname"].ToString().Trim();
                frm.JustForBrow = true;
                frm.TTable = "oustkbom";
                frm.TKey = "BomID";
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
                    this.TResult = dtD.DefaultView[index]["OuNo"].ToString();
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

                    textBoxT1.Text = dtD.DefaultView[index]["itno"].ToString();
                    textBoxT2.Text = dtD.DefaultView[index]["stno"].ToString();
                    textBoxT3.Text = dtD.DefaultView[index]["stname"].ToString();
                    textBoxT4.Text = dtD.DefaultView[index]["cuno"].ToString();
                    textBoxT5.Text = dtD.DefaultView[index]["cutel1"].ToString();
                    textBoxT6.Text = dtD.DefaultView[index]["cuper1"].ToString();
                }
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void toudate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused)
                return;

            xe.DateValidate(sender, e, true);

        }

        private void tcuno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void titno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void tstno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
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
            else if (keyData == Keys.F4)
            {
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

        private void FrmOutStkBrow_FormClosing(object sender, FormClosingEventArgs e)
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
                    this.TResult = dtD.DefaultView[index]["OuNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FrmOutStkBrow_Shown(object sender, EventArgs e)
        {
            touno.Focus();
        }



    }
}
