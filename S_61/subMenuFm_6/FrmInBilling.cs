using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class FrmInBilling : Formbase
    {
        JBS.JS.xEvents xe;

        List<DataRow> list = new List<DataRow>();
        DataTable dt = new DataTable();
        public string st;

        public FrmInBilling()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmInBilling_Load(object sender, EventArgs e)
        {
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.CommandText = "select count(*) from StkRoom where StNo=@StNo";

                        if (cmd.ExecuteScalar().ToString() != "0")
                        {
                            using (var frm = new FrmInBilling_1())
                            { 
                                frm.stno = StNo.Text.Trim();
                                frm.stname = StName.Text.Trim();
                                frm.st = st;
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            StNo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();

                this.st = row["StTrait"].ToString();
            });
        }
         
        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if (StNo.Text.Trim() == "")
            {
                e.Cancel = true;
                StNo.Text = "";
                //MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();

                this.st = row["StTrait"].ToString();
            }); 
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    btnExit.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 
    }
}
