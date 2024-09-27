using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmBuyGrad : Formbase
    {
        JBS.JS.xEvents xe;
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        SqlTransaction tran;
        string tempNo = "";
        string btnState = "";
        int max = 0;
        List<TextBoxbase> list;

        public FrmBuyGrad()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();
             
            BuPrs.FirstNum = 1;
            BuPrs.LastNum = 3;
        }

        private void FrmBuyGrad_Load(object sender, EventArgs e)
        { 
            GetMaxID();
            writeToTxt(Common.load("Top", "BuyGrad", "gradid"));
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
                    cmd.CommandText = "select top 1 gradid from BuyGrad order by gradid DESC ";
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
                KiNo.Text = row["KiNo"].ToString();
                KiName.Text = row["KiName"].ToString();
                X12No.Text = row["X12No"].ToString();
                X12Name.Text = row["X12Name"].ToString();
                BuPrs.Text = row["BuPrs"].ToString();

            }
            else
            { 
                PK.Text = ""; 
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            } 
        } 

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Top", "BuyGrad", "gradid")); 
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "BuyGrad", "gradid", PK.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "BuyGrad", "gradid", PK.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row); 
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var row = Common.load("Next", "BuyGrad", "gradid", PK.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "BuyGrad", "gradid", PK.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Bottom", "BuyGrad", "gradid"));
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = PK.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            KiNo.ReadOnly = false;
            X12No.ReadOnly = false;
            BuPrs.ReadOnly = false; 
             
            KiNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (KiNo.Text == string.Empty) return;

            tempNo = PK.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            KiNo.ReadOnly = false;
            X12No.ReadOnly = false;
            BuPrs.ReadOnly = false;
             
            KiNo.Focus();
            KiNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (KiNo.Text == string.Empty) return; 
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

                    cmd.CommandText = "delete from BuyGrad where gradid=@PK COLLATE Chinese_Taiwan_Stroke_CS_AS";
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

            var row = Common.load("Next", "BuyGrad", "gradid", PK.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "BuyGrad", "gradid", PK.Text.Trim());
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
                using (var frm = new FrmBuyGradBrow())
                {
                    frm.TSeekNo = PK.Text;
                    frm.ShowDialog(); 
                    writeToTxt(Common.load("Check", "BuyGrad", "gradid", frm.TResult));
                }
            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (KiNo.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                KiNo.Focus();
                return;
            }
            if (X12No.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                X12No.Focus();
                return;
            }
            if (BuPrs.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BuPrs.Focus();
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
                    SqlConnection conn = new SqlConnection(Common.sqlConnString);
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@KiName", KiName.Text.Trim());
                    cmd.Parameters.AddWithValue("@X12No", X12No.Text.Trim());
                    cmd.Parameters.AddWithValue("@X12Name", X12Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@BuPrs", BuPrs.Text.Trim());

                    cmd.CommandText = "insert into buyGrad"
                         + "(KiNo,KiName,X12No,X12Name,BuPrs) VALUES ("
                         + "(@KiNo)"
                         + ",(@KiName)"
                         + ",(@X12No)"
                         + ",(@X12Name)"
                         + ",(@BuPrs))";
                    cmd.ExecuteNonQuery();

                    tran.Commit(); tran.Dispose();
                    cmd.Dispose();
                    conn.Close(); conn.Dispose();

                    //新增成功
                    this.tempNo = (max += 1).ToString();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                     
                    KiNo.Focus();
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
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@gradid", PK.Text.Trim());

                        cmd.CommandText = "select * from buygrad where gradid=@gradid"
                             + " COLLATE Chinese_Taiwan_Stroke_CS_AS";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("資料已被其它人刪除，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@KiName", KiName.Text.Trim());
                        cmd.Parameters.AddWithValue("@X12No", X12No.Text.Trim());
                        cmd.Parameters.AddWithValue("@X12Name", X12Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@BuPrs", BuPrs.Text.Trim());

                        cmd.CommandText = "update buyGrad set "
                            + "KiNo=@KiNo"
                            + ",KiName=@KiName"
                            + ",X12No=@X12No"
                            + ",X12Name=@X12Name"
                            + ",BuPrs=@BuPrs"
                            + " where gradid=@gradid"; 
                        cmd.ExecuteNonQuery();

                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }
                    this.tempNo = PK.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                     
                    KiNo.Focus();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = "";
            writeToTxt(Common.load("Cancel", "buyGrad", "gradid", tempNo));
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
                    btnSave.PerformClick();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender, row =>
            {
                KiNo.Text = row["KiNo"].ToString().Trim();
                KiName.Text = row["KiName"].ToString().Trim();
            });
        }

        private void X12No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX12>(sender, row =>
            {
                X12No.Text = row["X12No"].ToString().Trim();
                X12Name.Text = row["X12Name"].ToString().Trim();
            });
        }

        private void KiNo_Validating(object sender, CancelEventArgs e)
        {
            if (KiNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (KiNo.Text.Trim() == "")
            {
                e.Cancel = true;
                KiNo.Text = "";
                KiName.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.Kind>(sender, e, row =>
            {
                KiNo.Text = row["KiNo"].ToString().Trim();
                KiName.Text = row["KiName"].ToString().Trim();
            });

            if (btnState == "Append")
            {
                if (X12No.Text.Trim() != "")
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

        private void X12No_Validating(object sender, CancelEventArgs e)
        {
            if (X12No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (X12No.Text.Trim() == "")
            {
                e.Cancel = true;
                X12No.Text = "";
                X12Name.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.XX12>(sender, e, row =>
            {
                X12No.Text = row["X12No"].ToString().Trim();
                X12Name.Text = row["X12Name"].ToString().Trim();
            });

            if (btnState == "Append")
            {
                if (KiNo.Text.Trim() != "")
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

        private void BuPrs_Validating(object sender, CancelEventArgs e)
        {
            if (BuPrs.ReadOnly) return;
            if (BuPrs.Text.Trim() == "")
            {
                e.Cancel = true;
                int i = 1;
                BuPrs.Text = i.ToString("f" + BuPrs.LastNum);
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            BuPrs.Text = string.Format("{0:F" + BuPrs.LastNum + "}", decimal.Parse(BuPrs.Text));
        }

        bool IsRepeat()
        {
            bool isrepeat = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("x12no", X12No.Text.Trim());
                        cmd.Parameters.AddWithValue("gradid", PK.Text.Trim());
                        cmd.CommandText = "select * from buygrad where '0'='0'"
                            + " and kino=@kino "
                            + " and x12no=@x12no ";
                        if (btnState == "Modify")
                            cmd.CommandText += " and gradid !=@gradid";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            isrepeat = reader.HasRows;
                        }
                    }
                }
                return isrepeat;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                isrepeat = true;
                return isrepeat;
            }
        }
          
    }
}
