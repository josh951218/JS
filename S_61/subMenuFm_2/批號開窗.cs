using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_1;
using S_61.SOther;

namespace S_61.subMenuFm_2
{
    public partial class 批號開窗 : Formbase, JBS.JS.IxOpen 
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        DataTable table = new DataTable();
        public string itno = "";
        public string batchno;
        public DataRow result;
        public 批號開窗()
        {
            InitializeComponent();
        }

        private void 批號開窗_Load(object sender, EventArgs e)
        {
            load();
            dataGridViewT1.DataSource = table;
            dataGridViewT1.Search("批次號碼", batchno);
        }

        void load()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("itno", itno);
                    cmd.CommandText = @"
    	                select *,faname1 = (select faname1 from fact where fano = batchInformation.fano),itname 
                        from batchInformation ";
                    cmd.CommandText += " left join item on item.itno = batchInformation.itno";
                    if (itno != "")
                    {
                        cmd.CommandText += " where batchInformation.itno=@itno";
                    }
                    cmd.CommandText += " order by batchInformation.itno";
                    table.Clear();
                    dd.Fill(table);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count > 0)
            {
                this.result = table.Rows[dataGridViewT1.SelectedRows[0].Index];
                TResult = this.result["Batchno"].ToString().Trim();
                this.DialogResult = DialogResult.OK;
            }
            this.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Batchno_TextChanged(object sender, EventArgs e)
        {
            if (Batchno.TrimTextLenth() > 0)
                dataGridViewT1.Search("批次號碼", Batchno.Text.Trim());
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            btnGet_Click(null, null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                btnGet.PerformClick();
            }
            else if (keyData == Keys.F11)
            {
                btnExit.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
