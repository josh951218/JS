using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmOrderToFordbrow : Formbase
    {
        public DataTable ordertable = new DataTable();

        public FrmOrderToFordbrow()
        {
            InitializeComponent();
             
            Button bt = new Button();
            bt.Text = "確定";
            bt.Font = new System.Drawing.Font("細明體", 14F);

            btnOk.Controls.Add(bt);
            bt.Dock = DockStyle.Fill;
            bt.Click -= new EventHandler(btnBrow_Click);
            bt.Click += new EventHandler(btnBrow_Click);

            this.訂單總額.Visible = this.本幣總額.Visible = Common.User_SalePrice;
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["點選"].Value = "V";
            }
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["點選"].Value = "";
            }
        }

        private void FrmOrderToFordbrow_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = ordertable;

            this.訂單總額.DefaultCellStyle.Format = "f" + Common.M;
            this.本幣總額.DefaultCellStyle.Format = "f" + Common.M;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dataGridViewT1.CurrentCell.OwningColumn.Name != "點選") return;

            if (dataGridViewT1["點選", e.RowIndex].Value.ToString().Trim() == "V")
                dataGridViewT1["點選", e.RowIndex].Value = "";
            else
                dataGridViewT1["點選", e.RowIndex].Value = "V";
            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    allok.Focus();
                    allok.PerformClick();
                    break;
                case Keys.F3:
                    allcancel.Focus();
                    allcancel.PerformClick();
                    break;
                case Keys.F9:
                    btnOk.Focus();
                    btnOk.PerformClick();
                    break;
                case Keys.F11:
                    btnExit.Focus();
                    btnExit.PerformClick();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            var check = ordertable.AsEnumerable().Where(r => r["點選"].ToString().Trim() == "V");
            if (check.Count() > 0)
            {
                DataTable temp = new DataTable();
                temp = check.CopyToDataTable();
                foreach (DataRow i in temp.Rows)
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("orno", i["訂單編號"].ToString().Trim());
                        cmd.CommandText = "update [order] set ortrnflag='1' where orno=@orno";
                        cmd.ExecuteNonQuery();
                        dataGridViewT1.DataSource = null;

                    }
                }

            }
            var nocheck = ordertable.AsEnumerable().ToList().Where(r => r["點選"].ToString().Trim() == "");
            if (nocheck.Count() > 0)
            {
                ordertable = nocheck.CopyToDataTable();
                dataGridViewT1.DataSource = ordertable;
            }
            foreach (DataRow dr in ordertable.Rows)
            {
                dr["點選"] = "V";
            }

            this.DialogResult = DialogResult.Cancel;
        }
    }
}
