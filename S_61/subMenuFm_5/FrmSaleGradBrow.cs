using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmSaleGradBrow : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable dt = new DataTable();

        public FrmSaleGradBrow()
        {
            InitializeComponent();

            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";

            this.售價等級.FirstNum = 1;
            this.售價等級.LastNum = 0;
            this.售價等級.DefaultCellStyle.Format = "f0";
        }

        private void FrmSaleGradBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            dataGridViewT1.DataSource = dt.DefaultView;
            loadDB();

            dt.DefaultView.Search(ref dataGridViewT1, "gradid", this.TSeekNo ?? "");

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnModify.Focus();
        }

        private void loadDB()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from salgrad order by gradid asc", cn))
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

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
                if (dataGridViewT1.SelectedRows.Count > 0)
                    dataGridViewT1.CurrentCell = dataGridViewT1["折數", dataGridViewT1.SelectedRows[0].Index];

            GridReadOnly(false);

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            btnModify.Enabled = false;
            btnExit.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GridReadOnly(true);
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].RowState != DataRowState.Modified) continue;
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("RePrs", dt.Rows[i]["RePrs"].ToString());
                        cmd.Parameters.AddWithValue("ReGrade", dt.Rows[i]["ReGrade"].ToString());
                        cmd.Parameters.AddWithValue("gradid", dt.Rows[i]["gradid"].ToString());

                        cmd.CommandText = "update salgrad set "
                        + " RePrs=@RePrs "
                        + ",ReGrade=@ReGrade "
                        + " where gradid=@gradid ";
                        cmd.ExecuteNonQuery();
                    }
                }
                btnModify.Enabled = true;
                btnExit.Enabled = true;

                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnModify.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GridReadOnly(true);
            loadDB();

            btnModify.Enabled = true;
            btnExit.Enabled = true;

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void GridReadOnly(bool isreadonly)
        {
            if (isreadonly)
            {
                dataGridViewT1.ReadOnly = true;
            }
            else
            {
                dataGridViewT1.ReadOnly = false;
                dataGridViewT1.Columns[this.pk.Name].ReadOnly = true;
                dataGridViewT1.Columns[this.銷售類別.Name].ReadOnly = true;
                dataGridViewT1.Columns[this.銷售類別名稱.Name].ReadOnly = true;
                dataGridViewT1.Columns[this.客戶類別.Name].ReadOnly = true;
                dataGridViewT1.Columns[this.客戶類別名稱.Name].ReadOnly = true;
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (dataGridViewT1.Columns[this.折數.Name].ReadOnly) return;

            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.折數.Name)
                {
                    string str = dataGridViewT1[this.折數.Name, e.RowIndex].EditedFormattedValue.ToString();
                    if (str.ToDecimal() == 0)
                    {
                        e.Cancel = true;
                        MessageBox.Show("折數不可小於或等於零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (str.ToDecimal() > 1)
                    {
                        e.Cancel = true;
                        MessageBox.Show("折數設定不可大於一！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.售價等級.Name)
                {
                    string str = dataGridViewT1[this.售價等級.Name, e.RowIndex].EditedFormattedValue.ToString();
                    switch (str)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = str;
                            dt.Rows[e.RowIndex]["ReGrade"] = str;
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            break;
                        default:
                            e.Cancel = true;
                            MessageBox.Show("只能輸入1~6的數字:\n"
                                + "數字1 : 售價一\n"
                                + "數字2 : 售價二\n"
                                + "數字3 : 售價三\n"
                                + "數字4 : 售價四\n"
                                + "數字5 : 售價五\n"
                                + "數字6 : 建檔售價"
                                , "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (dataGridViewT1.EditingControl != null)
                                ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                            break;
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    btnExit.PerformClick();
                    break;
                case Keys.F9:
                    btnSave.PerformClick();
                    btnModify.Focus();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    btnModify.Focus();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmSaleGradBrow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dt.Rows.Count == 0) this.TResult = "";

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) this.TResult = "";
            else
            {
                this.TResult = dt.DefaultView[index]["gradid"].ToString();
            }
        }
    }
}
