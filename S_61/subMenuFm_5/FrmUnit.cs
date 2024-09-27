using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmUnit : Formbase
    {
        public int Kid;
        public string Result = "";
        DataTable tb = new DataTable();
        DataTable tmep = new DataTable();
        int DelIndex = 0;

        public FrmUnit()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }

        private void FrmItUnit_Load(object sender, EventArgs e)
        {
            this.單位.HeaderText = Kid == 1 ? "單位" : "包裝單位";
            lblT1.Text = Kid == 1 ? "輸入新增單位" : "輸入新增包裝單位";
            load();
            itunit.Focus();
        }

        void load()
        {
            dataGridViewT1.DataSource = null;
            tb.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("kid", Kid);
                cmd.CommandText = "select * from unit where kid=@Kid order by unid";
                SqlDataAdapter dd = new SqlDataAdapter(cmd);
                dd.Fill(tb);
                dataGridViewT1.DataSource = tb;
                dd.Dispose();
                cmd.Dispose();
            }
        }

        private void Append_Click(object sender, EventArgs e)
        {
            if (itunit.Text.Trim() != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itunit", itunit.Text.Trim());
                    cmd.Parameters.AddWithValue("@Kid", Kid);

                    cmd.CommandText = "insert into unit(itunit,kid)values(@itunit,@Kid)";
                    cmd.ExecuteNonQuery();
                }
                load();
                dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Selected = true;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = dataGridViewT1.Rows.Count - 1;
            }
            itunit.Focus();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count < 1) return;
            DelIndex = dataGridViewT1.SelectedRows[0].Index;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@unid", dataGridViewT1.SelectedRows[0].Cells["unid"].Value.ToString().Trim());
                cmd.CommandText = "delete unit where unid=@unid";
                cmd.ExecuteNonQuery();
            }
            load();
            if (DelIndex > dataGridViewT1.Rows.Count - 1)
            {
                if (dataGridViewT1.Rows.Count == 0) return;
                dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Selected = true;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = dataGridViewT1.Rows.Count - 1;
            }
            else
            {
                dataGridViewT1.Rows[DelIndex].Selected = true;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = DelIndex;
            }
        }

        private void Get_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count > 0)
                Result = dataGridViewT1.SelectedRows[0].Cells["單位"].Value.ToString().Trim();
            else
                Result = "";
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            Get_Click(null, null);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    Append.Focus();
                    Append.PerformClick();
                    break;
                case Keys.F3:
                    Delete.Focus();
                    Delete.PerformClick();
                    break;
                case Keys.F9:
                    Get.Focus();
                    Get.PerformClick();
                    break;
                case Keys.F11:
                    Exit.Focus();
                    Exit.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
