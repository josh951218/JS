using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;


namespace S_61.subMenuFm_3
{
    public partial class FrmPayabld_BShopb : Formbase
    {
        DataTable dt = new DataTable();
        public string callType;

        public FrmPayabld_BShopb()
        {
            InitializeComponent();

            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.稅前單價.DefaultCellStyle.Format = "f" +  Common.MF;
            this.稅前金額.DefaultCellStyle.Format = "f" +  Common.MF;

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }
            pVar.SetMemoUdf(this.備註說明);
        }

        private void FrmPayabld_BShopb_Load(object sender, EventArgs e)
        { 

            loadDB();
            dataGridViewT1.DataSource = dt;
            btnExit.Focus();
        }

        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select * from " + callType;
                    SqlDataAdapter da = new SqlDataAdapter(str, conn);
                    dt.Clear();
                    da.Fill(dt);
                    da.Dispose();
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
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11)
            {
                this.Close();
                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }















    }
}
