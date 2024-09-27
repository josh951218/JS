using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmSaleGrad : Formbase
    {
        JBS.JS.xEvents xe;
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        SqlTransaction tran;
        string tempNo = "";
        string btnState = "";
        int max = 0;
        List<TextBoxbase> list;

        public FrmSaleGrad()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();
        }

        private void FrmSaleGrad_Load(object sender, EventArgs e)
        {
            GetMaxID();
            writeToTxt(Common.load("Top", "SalGrad", "gradid"));
            btnAppend.Focus();
        }

        public void GetMaxID()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "Select Top 1 gradid from SalGrad order by gradid DESC";
                    var obj = cmd.ExecuteScalar();
                    if (obj == null) max = 0;
                    else
                    {
                        max = obj.ToInteger();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                PK.Text = row["gradid"].ToString();
                ScNo.Text = row["KiNo"].ToString();
                ScName.Text = row["KiName"].ToString();
                X1No.Text = row["X1No"].ToString();
                X1Name.Text = row["X1Name"].ToString();
                RePrs.Text = row["RePrs"].ToString();

                switch (row["ReGrade"].ToString())
                {
                    case "1": ReGrade1.Checked = true; break;
                    case "2": ReGrade2.Checked = true; break;
                    case "3": ReGrade3.Checked = true; break;
                    case "4": ReGrade4.Checked = true; break;
                    case "5": ReGrade5.Checked = true; break;
                    case "6": ReGrade6.Checked = true; break;
                    default: break;
                }
            }
            else
            {
                PK.Text = "";
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
        }
         
        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Top", "SalGrad", "gradid"));
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            if (PK.Text.Trim() == "") return;
            var row = Common.load("Prior", "SalGrad", "gradid", PK.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "SalGrad", "gradid", PK.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (PK.Text.Trim() == "") return;
            var row = Common.load("Next", "SalGrad", "gradid", PK.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "SalGrad", "gradid", PK.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Bottom", "SalGrad", "gradid"));
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = PK.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            ReGrade6.Checked = true;
            ScNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (ScNo.Text == string.Empty) return;

            tempNo = PK.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            ScNo.Focus();
            ScNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ScNo.Text == string.Empty) return;
            btnState = ((Button)sender).Name.Substring(3);

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tran = cn.BeginTransaction();
                    cmd.Transaction = tran;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@PK", PK.Text.Trim());

                    cmd.CommandText = "delete from SalGrad where gradid=@PK COLLATE Chinese_Taiwan_Stroke_CS_AS";
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                    tran.Dispose();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }

            var row = Common.load("Next", "SalGrad", "gradid", PK.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "SalGrad", "gradid", PK.Text.Trim());
            }
            writeToTxt(row);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (PK.Text.Trim().Length == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (var frm = new FrmSaleGradBrow())
                {
                    frm.TSeekNo = PK.Text;
                    frm.ShowDialog();
                    writeToTxt(Common.load("Check", "SalGrad", "gradid", frm.TResult));
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ScNo.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ScNo.Focus();
                return;
            }
            if (X1No.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                X1No.Focus();
                return;
            }
            if (RePrs.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RePrs.Focus();
                return;
            }
            if (IsRepeat())
            {
                MessageBox.Show("資料重複，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnState == "Append")
            {
                try
                {
                    SqlConnection cn = new SqlConnection(Common.sqlConnString);
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    tran = cn.BeginTransaction();
                    cmd.Transaction = tran;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@KiNo", ScNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@KiName", ScName.Text.Trim());
                    cmd.Parameters.AddWithValue("@X1No", X1No.Text.Trim());
                    cmd.Parameters.AddWithValue("@X1Name", X1Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@RePrs", RePrs.Text.Trim());
                    cmd.Parameters.AddWithValue("@pnlRadio", getRadioNumber(panelNT1));

                    cmd.CommandText = "insert into SalGrad"
                         + "(KiNo,KiName,X1No,X1Name,RePrs,ReGrade) VALUES ("
                         + "@KiNo,"
                         + "@KiName,"
                         + "@X1No,"
                         + "@X1Name,"
                         + "@RePrs,"
                         + "@pnlRadio)";
                    cmd.ExecuteNonQuery();

                    tran.Commit(); tran.Dispose();
                    cmd.Dispose();
                    cn.Close(); cn.Dispose();

                    //新增成功
                    this.tempNo = (max += 1).ToString();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);

                    int i = 1;
                    RePrs.Text = i.ToString("f" + RePrs.LastNum);
                    ScNo.Focus();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
            if (btnState == "Modify")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        tran = cn.BeginTransaction();
                        cmd.Transaction = tran;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@PK", PK.Text.Trim());
                        cmd.Parameters.AddWithValue("@KiNo", ScNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@KiName", ScName.Text.Trim());
                        cmd.Parameters.AddWithValue("@X1No", X1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@X1Name", X1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@RePrs", RePrs.Text.Trim());
                        cmd.Parameters.AddWithValue("@pnlRadio", getRadioNumber(panelNT1));

                        cmd.CommandText = " update SalGrad set "
                            + " KiNo=@KiNo,"
                            + " KiName=@KiName,"
                            + " X1No=@X1No,"
                            + " X1Name=@X1Name,"
                            + " RePrs=@RePrs,"
                            + " ReGrade=@pnlRadio"
                            + " where gradid=(@PK) ";

                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }
                    this.tempNo = PK.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);

                    ScNo.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tran.Rollback();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = "";
            writeToTxt(Common.load("Cancel", "SalGrad", "gradid", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    btnExit.PerformClick();
                    break;
                case Keys.Home:
                    btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    btnNext.PerformClick();
                    break;
                case Keys.End:
                    btnBottom.PerformClick();
                    break;
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

        private string getRadioNumber(PanelNT pnl)
        {
            return pnl.Controls.OfType<RadioT>().FirstOrDefault(r => r.Checked).Name.Last().ToString();
        }

        private void ScNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.SaleClass>(sender, row =>
            {
                ScNo.Text = row["scno"].ToString().Trim();
                ScName.Text = row["scname"].ToString().Trim();
            });
        }
        private void ScNo_Validating(object sender, CancelEventArgs e)
        {
            if (ScNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (ScNo.Text.Trim() == "")
            {
                e.Cancel = true;
                ScNo.Text = "";
                ScName.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                xe.ValidateOpen<JBS.JS.SaleClass>(sender, e, row =>
                {
                    ScNo.Text = row["scno"].ToString().Trim();
                    ScName.Text = row["scname"].ToString().Trim();
                });

                if (btnState == "Append")
                {
                    if (X1No.Text.Trim() != "")
                    {
                        if (IsRepeat())
                        {
                            e.Cancel = true;
                            MessageBox.Show("資料重複，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                if (btnState == "Modify")
                {
                    if (IsRepeat())
                    {
                        e.Cancel = true;
                        MessageBox.Show("資料重複，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void X1No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX01>(sender, row =>
            {
                X1No.Text = row["x1no"].ToString().Trim();
                X1Name.Text = row["x1name"].ToString().Trim();
            });
        }
        private void X1No_Validating(object sender, CancelEventArgs e)
        {
            if (X1No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (X1No.Text.Trim() == "")
            {
                e.Cancel = true;
                X1No.Text = "";
                X1Name.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                xe.ValidateOpen<JBS.JS.XX01>(sender, e, row =>
                {
                    X1No.Text = row["x1no"].ToString().Trim();
                    X1Name.Text = row["x1name"].ToString().Trim();
                });

                if (btnState == "Append")
                {
                    if (ScNo.Text.Trim() != "")
                    {
                        if (IsRepeat())
                        {
                            e.Cancel = true;
                            MessageBox.Show("資料重複，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                if (btnState == "Modify")
                {
                    if (IsRepeat())
                    {
                        e.Cancel = true;
                        MessageBox.Show("資料重複，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
         
        private void RePrs_Validating(object sender, CancelEventArgs e)
        {
            if (RePrs.ReadOnly) return;
            if (RePrs.Text.Trim() == "")
            {
                e.Cancel = true;
                int i = 1;
                RePrs.Text = i.ToString("f" + RePrs.LastNum);
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            RePrs.Text = string.Format("{0:F" + RePrs.LastNum + "}", decimal.Parse(RePrs.Text));
        }

        bool IsRepeat()
        {
            bool isrepeat = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("KiNo", ScNo.Text.Trim());
                    cmd.Parameters.AddWithValue("X1No", X1No.Text.Trim());
                    cmd.Parameters.AddWithValue("PK", PK.Text.Trim());

                    cmd.CommandText = "select * from salgrad where '0'='0'"
                                    + " and kino=@KiNo"
                                    + " and x1no=@X1No";

                    if (btnState == "Modify") cmd.CommandText += " and gradid !=@PK";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        isrepeat = reader.HasRows;
                    }
                }
                return isrepeat;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return true;
            }
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            panelNT1.Enabled = !btnAppend.Enabled;
        }
























    }
}
