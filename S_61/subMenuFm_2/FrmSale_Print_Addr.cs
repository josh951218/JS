using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSale_Print_Addr : Formbase
    {
        public string TResult { get; set; }

        public FrmSale_Print_Addr()
        {
            InitializeComponent();
        }

        private void FrmSale_Print_Addr_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            LoadDB();
        }

        void LoadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from Addr";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        dataGridViewT1.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                int i = e.RowIndex;
                if (i == -1)
                    i = dataGridViewT1.SelectedRows[0].Index;
                this.TResult = dataGridViewT1["常用地址", i].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
