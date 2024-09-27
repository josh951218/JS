using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmItemLVbat : Formbase
    {
        public FrmItemLVbat()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }

        private void FrmItemLVbat_Load(object sender, EventArgs e)
        { 
            foreach (var item in this.Controls.OfType<TextBoxNumberT>())
            {
                item.FirstNum = 1;
                item.LastNum = 3;
                item.ReadOnly = false;
                item.Text = "";
            }
        }

        private void txtNumber1_Validating(object sender, CancelEventArgs e)
        {
            TextBoxNumberT tb = sender as TextBoxNumberT;
            if (tb.Text.Trim().Length > 0 && tb.Text.Trim().ToDecimal() > 1)
            {
                e.Cancel = true;
                tb.SelectAll();
                MessageBox.Show("折數設定不能大於1！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExitl_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@txtNumber1", txtNumber1.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber2", txtNumber2.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber3", txtNumber3.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber4", txtNumber4.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber5", txtNumber5.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber6", txtNumber6.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber7", txtNumber7.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber8", txtNumber8.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber9", txtNumber9.Text.Trim());
                        cmd.Parameters.AddWithValue("@txtNumber10", txtNumber10.Text.Trim());

                        cmd.CommandText = " update item set itprice = itprice ";
                        if (txtNumber1.Text.Trim().Length > 0) cmd.CommandText += " ,itprice1 = itprice * @txtNumber1";
                        if (txtNumber2.Text.Trim().Length > 0) cmd.CommandText += " ,itprice2 = itprice * @txtNumber2";
                        if (txtNumber3.Text.Trim().Length > 0) cmd.CommandText += " ,itprice3 = itprice * @txtNumber3";
                        if (txtNumber4.Text.Trim().Length > 0) cmd.CommandText += " ,itprice4 = itprice * @txtNumber4";
                        if (txtNumber5.Text.Trim().Length > 0) cmd.CommandText += " ,itprice5 = itprice * @txtNumber5";

                        if (txtNumber6.Text.Trim().Length > 0) cmd.CommandText += " ,itpricep1 = itpricep * @txtNumber6";
                        if (txtNumber7.Text.Trim().Length > 0) cmd.CommandText += " ,itpricep2 = itpricep * @txtNumber7";
                        if (txtNumber8.Text.Trim().Length > 0) cmd.CommandText += " ,itpricep3 = itpricep * @txtNumber8";
                        if (txtNumber9.Text.Trim().Length > 0) cmd.CommandText += " ,itpricep4 = itpricep * @txtNumber9";
                        if (txtNumber10.Text.Trim().Length > 0) cmd.CommandText += " ,itpricep5 = itpricep * @txtNumber10";
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("修改完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
