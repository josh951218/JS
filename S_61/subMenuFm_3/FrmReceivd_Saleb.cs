using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmReceivd_Saleb : Formbase
    {
        DataTable dt = new DataTable();
        public string callType;

        public FrmReceivd_Saleb()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;

            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.稅前單價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }
            pVar.SetMemoUdf(this.備註說明);
        }

        private void FrmReceivd_Saleb_Load(object sender, EventArgs e)
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
                using (SqlDataAdapter da = new SqlDataAdapter("select * from " + callType, conn))
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
 














    }
}
