using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;


namespace S_61.subMenuFm_2
{
    public partial class FrmSale_Memo : Formbase
    {
        public string Memo { get; set; }
        DataTable dt = new DataTable();

        public FrmSale_Memo()
        {
            InitializeComponent();
            pVar.SetMemoUdf(this.備註說明);

            this.Style = FormStyle.Mini;
        }

        private void FrmSale_Memo_Load(object sender, EventArgs e)
        { 
            dataGridViewT1.DataSource = dt;
            LoadDB();
        }

        void LoadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from memo01";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        dt.Clear();
                        da.Fill(dt);
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
                Memo = "";
                if (dataGridViewT1.SelectedRows.Count > 0)
                {
                    if (!dataGridViewT1["備註說明", dataGridViewT1.SelectedRows[0].Index].Value.IsNullOrEmpty())
                    {
                        Memo = dataGridViewT1["備註說明", dataGridViewT1.SelectedRows[0].Index].Value.ToString();
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Dispose();
            else if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (dataGridViewT1.Rows.Count > 0)
                {
                    Memo = "";
                    if (dataGridViewT1.SelectedRows.Count > 0)
                    {
                        if (!dataGridViewT1["備註說明", dataGridViewT1.SelectedRows[0].Index].Value.IsNullOrEmpty())
                        {
                            Memo = dataGridViewT1["備註說明", dataGridViewT1.SelectedRows[0].Index].Value.ToString();
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dt.Clear();
            base.OnFormClosing(e);
        }
         
    }
}
