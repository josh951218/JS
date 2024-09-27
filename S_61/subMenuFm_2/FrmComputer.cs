using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmComputer : Form
    {
        public decimal resultCount { get; set; }
        public decimal w1 = 0;
        public decimal w2 = 0;
        public decimal w3 = 0;
        public decimal w4 = 0;
        List<string> str = new List<string> { "＋", "－", "×", "÷" };
        public string Pformula = "";
        public decimal qty = 0;
        public string lbTxt = "";

        public FrmComputer()
        {
            InitializeComponent();
            textBoxT1.FirstNum = 8;
            textBoxT2.FirstNum = 8;
            textBoxT3.FirstNum = 8;
            textBoxT4.FirstNum = 8;
            textBoxT1.LastNum = Common.Q;
            textBoxT2.LastNum = Common.Q;
            textBoxT3.LastNum = Common.Q;
            textBoxT4.LastNum = Common.Q;
            Qty.FirstNum = 11;
            Qty.LastNum = Common.Q;

            if (Common.Sys_DBqty == 1)
            {
                qty = 1;
                lbForm.Visible = Qty.Visible = false;
            }

        }

        private void FrmComputer_Load(object sender, EventArgs e)
        {
            resultCount = 0;

            textBoxT1.ReadOnly = false;
            textBoxT2.ReadOnly = false;
            textBoxT3.ReadOnly = false;
            textBoxT4.ReadOnly = false;
            Qty.ReadOnly = false;

            if (Pformula.Length == 3)
            {
                var f = Pformula.ElementAt(0).ToString();
                button1.Text = str[(int)f.ToDecimal()];

                f = Pformula.ElementAt(1).ToString();
                button2.Text = str[(int)f.ToDecimal()];

                f = Pformula.ElementAt(2).ToString();
                button3.Text = str[(int)f.ToDecimal()];
            }
            else if (Common.User_Formula.Length == 3)
            {
                var f = Common.User_Formula.ElementAt(0).ToString();
                button1.Text = str[(int)f.ToDecimal()];

                f = Common.User_Formula.ElementAt(1).ToString();
                button2.Text = str[(int)f.ToDecimal()];

                f = Common.User_Formula.ElementAt(2).ToString();
                button3.Text = str[(int)f.ToDecimal()];
            }

            textBoxT1.Text = w1.ToString("f" + Common.Q);
            textBoxT2.Text = w2.ToString("f" + Common.Q);
            textBoxT3.Text = w3.ToString("f" + Common.Q);
            textBoxT4.Text = w4.ToString("f" + Common.Q);
            qty = qty == 0 ? 1 : qty;
            Qty.Text = qty.ToString("f" + Common.Q);
            lbForm.Text = lbTxt;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btns_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            var index = str.IndexOf(btn.Text);
            index = index == 3 ? 0 : index + 1;
            btn.Text = str[index];
        }

        private void tbs_Validating(object sender, CancelEventArgs e)
        {
            Count();
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            Count();
        }

        bool Count()
        {
            resultCount = 0;

            decimal t1 = textBoxT1.Text.ToDecimal();
            decimal t2 = textBoxT2.Text.ToDecimal();
            decimal t3 = textBoxT3.Text.ToDecimal();
            decimal t4 = textBoxT4.Text.ToDecimal();
            decimal q = Qty.Text.ToDecimal();

            int b1 = str.IndexOf(button1.Text);
            int b2 = str.IndexOf(button2.Text);
            int b3 = str.IndexOf(button3.Text);

            if (b1 == 3 && t2 == 0) goto End;
            if (b2 == 3 && t3 == 0) goto End;
            if (b3 == 3 && t4 == 0) goto End;

            if (b1 == 0) resultCount = t1 + t2;
            else if (b1 == 1) resultCount = t1 - t2;
            else if (b1 == 2) resultCount = t1 * t2;
            else if (b1 == 3) resultCount = t1 / t2;

            if (b2 == 0) resultCount += t3;
            else if (b2 == 1) resultCount -= t3;
            else if (b2 == 2) resultCount *= t3;
            else if (b2 == 3) resultCount /= t3;

            if (b3 == 0) resultCount += t4;
            else if (b3 == 1) resultCount -= t4;
            else if (b3 == 2) resultCount *= t4;
            else if (b3 == 3) resultCount /= t4;

            resultCount *= q;

            textBoxT5.Text = resultCount.ToString("f" + Common.Q);
            return true;
        //===***===
        End:
            MessageBox.Show("公式錯誤,嘗試除以0或是空值！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxT5.Clear();
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Count()) return;

            Pformula = "";
            Pformula += str.IndexOf(button1.Text);
            Pformula += str.IndexOf(button2.Text);
            Pformula += str.IndexOf(button3.Text);
            w1 = textBoxT1.Text.ToDecimal("f" + Common.Q);
            w2 = textBoxT2.Text.ToDecimal("f" + Common.Q);
            w3 = textBoxT3.Text.ToDecimal("f" + Common.Q);
            w4 = textBoxT4.Text.ToDecimal("f" + Common.Q);
            qty = Qty.Text.ToDecimal("f" + Common.Q);

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            string formula = "";
            formula += str.IndexOf(button1.Text);
            formula += str.IndexOf(button2.Text);
            formula += str.IndexOf(button3.Text);

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("formula", formula);
                        cmd.Parameters.AddWithValue("ScName", Common.User_Name);
                        cmd.CommandText = "update scrit set formula=@formula where ScName=@ScName";
                        cmd.ExecuteNonQuery();
                    }
                }
                MainForm.main.loadUserSetting();
                MessageBox.Show("預設值設定完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F9:
                    btnSave.Focus();
                    btnSave.PerformClick();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
