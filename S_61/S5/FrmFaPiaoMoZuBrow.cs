using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;
using JE.MyControl;
using System.Data.SqlClient;

namespace S_61.S5
{
    public partial class FrmFaPiaoMoZuBrow : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }

        public DataRow result = null;
        DataTable dt = new DataTable();

        public FrmFaPiaoMoZuBrow(FormStyle style = FormStyle.Mini, bool AllowAppend = false)
        {
            InitializeComponent();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            this.Style = style;
        }

        private void FrmStkRoomBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            LoadDB();
            dataGridViewT1.DataSource = dt;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.發票模組編號.Width = Screen.PrimaryScreen.Bounds.Width / 5 - 13;
            this.發票模組名稱.Width = Screen.PrimaryScreen.Bounds.Width / 5 - 13;
            this.起始發票號碼.Width = Screen.PrimaryScreen.Bounds.Width / 5 - 13;
            this.終止發票號碼.Width = Screen.PrimaryScreen.Bounds.Width / 5 - 13;
            this.目前發票號碼.Width = Screen.PrimaryScreen.Bounds.Width / 5 - 13;

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            dataGridViewT1.Search("發票模組編號", this.TSeekNo ?? "");
            ImNo.Focus();
        }

        void LoadDB()
        {
            try
            {
                dt.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from InvMode order by ImNo ", cn))
                    {
                        da.Fill(dt);
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
            this.Dispose();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index >= 0)
            {
                result = dt.Rows[index];
                this.DialogResult = DialogResult.OK;
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void StNo_TextChanged(object sender, EventArgs e)
        {
            dataGridViewT1.Search("發票模組編號", ImNo.Text.Trim(), "發票模組名稱", ImName.Text.Trim());
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (ImNo.TrimTextLenth() == 0 && ImName.TrimTextLenth() == 0) return;

            try
            {
                dataGridViewT1.SuspendLayout();
                dt.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "Select * From InvMode where 0=0 ";

                        if (ImNo.TrimTextLenth() > 0)
                        {
                            cmd.Parameters.AddWithValue("@tno", ImNo.Text.Trim());
                            cmd.CommandText += " And ImNo like '%'+@tno+'%' ";
                        }
                        if (ImName.TrimTextLenth() > 0)
                        {
                            cmd.Parameters.AddWithValue("@tname", ImName.Text.Trim());
                            cmd.CommandText += " And ImName like '%'+@tname+'%' ";
                        }
                        cmd.CommandText += " Order by 1";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                ImNo.Enter += new EventHandler(Text_OnEnter);
                ImName.Enter += new EventHandler(Text_OnEnter);
                dataGridViewT1.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            dataGridViewT1.SuspendLayout();

            ImNo.Clear();
            ImName.Clear();
            ImNo.Enter -= new EventHandler(Text_OnEnter);
            ImName.Enter -= new EventHandler(Text_OnEnter);

            LoadDB();
            dataGridViewT1.ResumeLayout();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                btnQuery.Focus();
                btnQuery.PerformClick();
            }
            else if (keyData == Keys.F9)
            {
                dataGridViewT1.Focus();
                btnGet.PerformClick();
            }
            else if (keyData == Keys.F11)
            {
                this.Dispose();
            }
            else if (keyData == Keys.Escape)
            {
                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}
