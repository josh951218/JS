using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmBorrow_Print_BoNo : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable dt = new DataTable();
        DataTable dtD = new DataTable();

        public FrmBorrow_Print_BoNo()
        {
            InitializeComponent();

            this.借入日期.DataPropertyName = Common.User_DateTime == 1 ? "bodate" : "bodate1";
            this.進價.Set進貨單價小數();
            this.數量.Set庫存數量小數();

            this.進價.Visible = Common.User_ShopPrice;
        }

        private void FrmBorrow_Print_BoNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            loadM();
            dt.DefaultView.Search(ref dataGridViewT1, "BoNo", this.TSeekNo ?? "");
        }

        void loadM()
        {
            dt.Clear();
            dtD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = "select * from borr order by BoNo";
                    da.Fill(dt);
                    dataGridViewT1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("BoNo", dt.Rows[index]["BoNo"].ToString().Trim());
                    cmd.CommandText = "select 序號='',* from borrd where BoNo = (@BoNo) ";
                    da.Fill(dtD);
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["序號"] = (i + 1).ToString();
                    }
                    dataGridViewT2.DataSource = dtD;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnEnter(object sender, EventArgs e)
        {
            BoNo.Clear();
            loadM();
            BoNo.Enter -= OnEnter;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            BoNo.Enter += new EventHandler(OnEnter);

            if (BoNo.Text == "") return;
            dt.Clear();
            dtD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("BoNo", BoNo.Text.Trim());
                    cmd.CommandText = "select * from borr where BoNo like '%' + @BoNo + '%' order by BoNo";
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = 0;
                        dataGridViewT1.Rows[0].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BoNo_TextChanged(object sender, EventArgs e)
        {
            if (BoNo.TrimTextLenth() == 0) return;
            dt.DefaultView.Search(ref dataGridViewT1, "BoNo", BoNo.Text.Trim());
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            this.TResult = dt.Rows[index]["BoNo"].ToString().Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
                btnQuery.PerformClick();

            if (keyData == Keys.F9)
                btnGet.PerformClick();

            if (keyData == Keys.F11)
                btnExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }
    }
}
