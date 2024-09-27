using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmReceivdBrow_Detail : Formbase
    {
        public string 單據;
        public string No;

        public FrmReceivdBrow_Detail()
        {
            InitializeComponent();
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.稅前單價.DefaultCellStyle.Format = "f6";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;
            pVar.SetMemoUdf(this.備註說明);
        }

        private void FrmCust_DetailBrow_Load(object sender, EventArgs e)
        { 
            if (單據 == "進貨" || 單據 == "進退")
            {
                this.訂單編號.HeaderText = "採購單號";
                this.訂單編號.DataPropertyName = "fono";
            }
            string sql = "";
            switch (單據)
            {
                case "銷貨":
                    sql = "select recordno 序號,* from saled where sano='" + No + "' order by recordno";
                    break;
                case "銷退":
                    sql = "select recordno 序號,* from raled where sano='" + No + "' order by recordno";
                    break;
                case "進貨":
                    sql = "select recordno 序號,* from bshopd where bsno='" + No + "' order by recordno";
                    break;
                case "進退":
                    sql = "select recordno 序號,* from rshopd where bsno='" + No + "' order by recordno";
                    break;
            }
            if (sql == "") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlDataAdapter dd = new SqlDataAdapter(sql, cn);
                    DataTable tb = new DataTable();
                    dd.Fill(tb);
                    dataGridViewT1.DataSource = tb;
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
            this.Close();
        }
    }
}
