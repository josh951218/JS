using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmBuyGradBrow : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable dt = new DataTable();

        public FrmBuyGradBrow()
        {
            InitializeComponent(); 
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";

        }

        private void FrmBuyGradBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            dataGridViewT1.DataSource = dt.DefaultView;
            loadDB();

            dt.DefaultView.Search(ref dataGridViewT1, "gradid", this.TSeekNo ?? "");

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnModify.Focus();
        }

        void loadDB()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from buygrad order by gradid asc", cn))
                {
                    dt.Clear();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void GridReadOnly(bool isreadonly)
        {
            if (isreadonly)
                dataGridViewT1.ReadOnly = true;
            else
            {
                dataGridViewT1.ReadOnly = false;
                dataGridViewT1.Columns["產品類別"].ReadOnly = true;
                dataGridViewT1.Columns["產品類別名稱"].ReadOnly = true;
                dataGridViewT1.Columns["廠商類別"].ReadOnly = true;
                dataGridViewT1.Columns["廠商類別名稱"].ReadOnly = true;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
                if (dataGridViewT1.SelectedRows.Count > 0)
                    dataGridViewT1.CurrentCell = dataGridViewT1["折數", dataGridViewT1.SelectedRows[0].Index];

            GridReadOnly(false);

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            btnModify.Enabled = false;
            btnExit.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GridReadOnly(true);
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].RowState != DataRowState.Modified)
                        continue;

                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("buprs", dt.Rows[i]["Buprs"].ToDecimal("f3"));
                            cmd.Parameters.AddWithValue("gradid", dt.Rows[i]["gradid"].ToDecimal("f0"));
                            cmd.CommandText = "update buygrad set buprs=@buprs where gradid=@gradid";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                btnModify.Enabled = true;
                btnExit.Enabled = true;

                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnModify.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK; 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GridReadOnly(true);
            loadDB();

            btnModify.Enabled = true;
            btnExit.Enabled = true;

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    btnExit.PerformClick();
                    break;
                case Keys.F9:
                    btnSave.PerformClick();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmSaleGradBrow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dt.Rows.Count == 0) this.TResult = "";

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) this.TResult = "";
            else
            {
                this.TResult = dt.DefaultView[index]["gradid"].ToString();
            }
        }
    }
}
