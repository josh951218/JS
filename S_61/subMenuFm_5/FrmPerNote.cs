using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmPerNote : Formbase
    {
        public string TResult { get; set; }
        DataTable tb = new DataTable();

        public FrmPerNote()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }

        private void FrmPerNote_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            load();
        }

        void load()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                string sql = "select * from pernote ";
                SqlDataAdapter dd = new SqlDataAdapter(sql, cn);
                dd.Fill(tb);
                dataGridViewT1.DataSource = tb;
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridViewT1.SelectedRows[0].Index;
            if (index > dataGridViewT1.Rows.Count - 1 || index < 0) return;

            this.TResult = dataGridViewT1.SelectedRows[0].Cells["有效期限"].Value.ToString().Trim();
            this.DialogResult = DialogResult.OK;
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Dispose();
                this.Close();
            } 
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
