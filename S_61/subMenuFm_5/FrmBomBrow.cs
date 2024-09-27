using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmBomBrow : Formbase
    {
        public string Result = "";

        DataTable dt = new DataTable();
        int index = 0;

        public FrmBomBrow()
        {
            InitializeComponent();
            cn.ConnectionString = Common.sqlConnString;

            this.標準使用量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.單價.Set本幣金額小數();
            this.金額.Set本幣金額小數();
            this.母件比例.FirstNum = 5;
            this.母件比例.LastNum = 4;
            this.母件比例.DefaultCellStyle.Format = "f4";
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";

            this.單價.Visible = this.折數.Visible = this.金額.Visible = Common.User_ShopPrice;
            textBoxT5.Visible = Common.User_ShopPrice;
        }

        private void FrmBomBrow_Load(object sender, EventArgs e)
        {
            dt.Clear();
            da.Fill(dt);
            dataGridViewT1.DataSource = dt.DefaultView;
            WriteToText();
        }

        private void WriteToText()
        {
            if (dt.Rows.Count > 0)
                if (dataGridViewT1.SelectedRows != null)
                    if (dataGridViewT1.SelectedRows.Count > 0)
                    {
                        index = dataGridViewT1.SelectedRows[0].Index;
                        textBoxT1.Text = dt.Rows[index]["boitno"].ToString();
                        textBoxT2.Text = dt.Rows[index]["itno"].ToString();
                        textBoxT3.Text = dt.Rows[index]["itstockqty"].ToDecimal().ToString("f" + Common.Q);
                        textBoxT4.Text = dt.Rows[index]["bototqty"].ToDecimal().ToString("f" + Common.Q);
                        textBoxT5.Text = dt.Rows[index]["bototmny"].ToDecimal().ToString("f" + Common.M);
                        textBoxT6.Text = dt.Rows[index]["bomemo"].ToString();
                        this.Result = dt.Rows[index]["boitno"].ToString();
                    }
        }

        private void btnQurry_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0) dt.DefaultView.Search(ref dataGridViewT1, "BoItNo", textBoxT7.Text.Trim());
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected) WriteToText();
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F3:
                    btnQurry.Focus();
                    btnQurry.PerformClick();
                    break;
                case Keys.F4:
                    btnGet.Focus();
                    btnGet.PerformClick();
                    break;
                case Keys.F11:
                    btnExit.Focus();
                    btnExit.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
