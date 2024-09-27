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
    public partial class FrmXa01 : Formbase
    {
        JBS.JS.xEvents xe;
        List<TextBoxbase> list;
        SqlTransaction tran;
        List<DataRow> li = new List<DataRow>();
        DataTable dt = new DataTable();
        DataRow dr;
        string tempNo = "";

        int temp = 0;
        string btnState = string.Empty;
        string BeforeText;

        public FrmXa01()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();
        }

        private void FrmXa01_Load(object sender, EventArgs e)
        {
            loadDB();
            if (dt.Rows.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
            btnAppend.Focus();
        }

        public void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select * from Xa01 order by Xa1No COLLATE Chinese_Taiwan_Stroke_BIN";
                    SqlDataAdapter da = new SqlDataAdapter(str, conn);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        li.Clear();
                        li = dt.AsEnumerable().ToList();
                    }
                    else
                    {
                        li.Clear();
                    }
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadDBError:\n" + ex.ToString());
            }
        }

        public void reLoadDB(string strBrow)
        {
            dr = li.Find(r => r.Field<string>("Xa1No") == strBrow);
            if (dr != null)
            {
                writeToTxt(dr);
            }
        }

        private void writeToTxt(DataRow dr)
        {
            if (dr != null)
            {
                Xa1No.Text = dr["xa1no"].ToString();
                Xa1Name.Text = dr["xa1name"].ToString();
                Xa1Coun2.Text = dr["xa1coun2"].ToString();
                Xa1Coun1.Text = dr["xa1coun1"].ToString();
                Xa1Cent.Text = dr["xa1cent"].ToString();
            }
            else
            {
                Xa1No.Text = "";
                Xa1Name.Text = "";
                Xa1Coun2.Text = "";
                Xa1Coun1.Text = "";
                Xa1Cent.Text = "";
            }
        }

        private DataRow getCurrentDataRow()
        {
            return li.Find(o => o.Field<string>("Xa1No") == (Xa1No.Text.Trim()));
        }

        private DataRow getCurrentDataRow(string s)
        {
            return li.Find(o => o.Field<string>("Xa1No") == (s));
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            loadDB();
            if (li.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
            btnTop.Enabled = false;
            btnPrior.Enabled = false;
            btnNext.Enabled = true;
            btnBottom.Enabled = true;
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            tempNo = Xa1No.Text.Trim();
            dr = getCurrentDataRow();
            temp = li.IndexOf(dr);
            loadDB();
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(tempNo);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (temp == 0)
                    {
                        dr = li.First();
                        writeToTxt(dr);
                        MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnTop.Enabled = false;
                        btnPrior.Enabled = false;
                        btnNext.Enabled = true;
                        btnBottom.Enabled = true;
                        return;
                    }
                    else
                    {
                        dr = li[--temp];
                        writeToTxt(dr);
                        btnNext.Enabled = true;
                        btnBottom.Enabled = true;
                        return;
                    }
                }
                if (i > 0)
                {
                    dr = li[--i];
                    writeToTxt(dr);
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
                else
                {
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnTop.Enabled = false;
                    btnPrior.Enabled = false;
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tempNo = Xa1No.Text.Trim();
            dr = getCurrentDataRow();
            temp = li.IndexOf(dr);
            loadDB();
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(tempNo);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (temp >= li.Count)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                        MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnTop.Enabled = true;
                        btnPrior.Enabled = true;
                        btnNext.Enabled = false;
                        btnBottom.Enabled = false;
                        return;
                    }
                    else
                    {
                        dr = li[++i];
                        writeToTxt(dr);
                        btnTop.Enabled = true;
                        btnPrior.Enabled = true;
                        return;
                    }
                }
                if (i < li.Count - 1)
                {
                    dr = li[++i];
                    writeToTxt(dr);
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                }
                else
                {
                    MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                    btnNext.Enabled = false;
                    btnBottom.Enabled = false;
                }
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            loadDB();
            if (li.Count > 0)
            {
                dr = li.Last();
                writeToTxt(dr);
            }
            btnTop.Enabled = true;
            btnPrior.Enabled = true;
            btnNext.Enabled = false;
            btnBottom.Enabled = false;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            this.tempNo = Xa1No.Text.Trim();

            Xa1No.ReadOnly = false;
            Xa1No.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (Xa1No.Text == string.Empty) return;//Xa1No沒有值，就不執行下面的指令

            Common.SetTextState(this.FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            this.tempNo = Xa1No.Text.Trim();

            Xa1No.ReadOnly = false;
            Xa1No.Focus();
            Xa1No.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Xa1No.Text == string.Empty) return;//Xa1No沒有值，就不執行下面的指令

            //執行刪除指令前，檢查此筆資料是否已被別人刪除
            loadDB();
            dr = getCurrentDataRow();
            if (li.IndexOf(dr) == -1)
            {
                if (li.Count > 0)
                {
                    dr = li.Last();
                    writeToTxt(dr);
                }
                else
                {
                    dr = null;
                    writeToTxt(dr);
                }
                return;//資料已被刪除，以下程式碼不執行
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Xa1No", Xa1No.Text.Trim());

                    cmd.CommandText = "delete from Xa01 where Xa1No=@Xa1No";

                    cmd.ExecuteNonQuery();
                    tran.Commit(); tran.Dispose();
                    cmd.Dispose();
                }

                loadDB();
                if (li.Count > 0)
                {
                    dr = li.Last();
                    writeToTxt(dr);
                }
                else
                {
                    dr = null;
                    writeToTxt(dr);
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show("DelError:\n" + ex.ToString());
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            loadDB();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Xa01>(
                5,
                " * ",
                new string[] { "使用國家", "國家簡稱", "單位" },
                new string[] { "Xa1Coun2", "Xa1Coun1", "Xa1Cent" }))
            {
                frm.TSeekNo = Xa1No.Text.Trim();
                var dl = frm.ShowDialog(this);

                if (dl == DialogResult.OK)
                {
                    xe.Validate<JBS.JS.Xa01>(frm.TResult, row =>
                    {
                        Xa1No.Text = row["xa1no"].ToString();
                        Xa1Name.Text = row["xa1name"].ToString();
                        Xa1Coun2.Text = row["xa1coun2"].ToString();
                        Xa1Coun1.Text = row["xa1coun1"].ToString();
                        Xa1Cent.Text = row["xa1cent"].ToString();
                    });
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Xa1No.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Xa1No.Focus();
                return;
            }

            if (btnState == "Append")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    MessageBox.Show("此幣別編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Xa1No.Text = "";
                    Xa1Name.Text = "";
                    Xa1Coun2.Text = "";
                    Xa1Coun1.Text = "";
                    Xa1Cent.Text = "";
                    Xa1No.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Xa1No", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Coun2", Xa1Coun2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Coun1", Xa1Coun1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Cent", Xa1Cent.Text.Trim());

                        cmd.CommandText = "insert into Xa01 (Xa1No,Xa1Name,Xa1Coun2,Xa1Coun1,Xa1Cent) values(@Xa1No,@Xa1Name,@Xa1Coun2,@Xa1Coun1,@Xa1Cent)";

                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }

                    this.tempNo = Xa1No.Text.Trim();
                    Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                    Xa1No.Text = "";
                    Xa1Name.Text = "";
                    Xa1Coun2.Text = "";
                    Xa1Coun1.Text = "";
                    Xa1Cent.Text = "";
                    Xa1No.Focus();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("AppendError:\n" + ex.ToString());
                }
            }
            if (btnState == "Modify")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Xa1No.Text = "";
                    Xa1Name.Text = "";
                    Xa1Coun2.Text = "";
                    Xa1Coun1.Text = "";
                    Xa1Cent.Text = "";
                    Xa1No.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Xa1No", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Coun2", Xa1Coun2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Coun1", Xa1Coun1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Xa1Cent", Xa1Cent.Text.Trim());

                        cmd.CommandText = "update Xa01 set Xa1Name=@Xa1Name"
                            + ",Xa1Coun2=@Xa1Coun2"
                            + ",Xa1Coun1=@Xa1Coun1"
                            + ",Xa1Cent=@Xa1Cent"
                            + " where Xa1No =@Xa1No";

                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }

                    this.tempNo = Xa1No.Text.Trim();
                    Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                    Xa1No.Text = "";
                    Xa1Name.Text = "";
                    Xa1Coun2.Text = "";
                    Xa1Coun1.Text = "";
                    Xa1Cent.Text = "";
                    Xa1No.Focus();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("ModifyError:\n" + ex.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnState = string.Empty;
            //取消時，檢查預載回的資料是否已被別人刪除
            //已被刪除，改顯示最後一筆
            //若再沒資料，清空欄位
            //沒被刪除，則載回預存資料
            loadDB();
            dr = getCurrentDataRow(tempNo);
            if (li.IndexOf(dr) == -1)
            {
                if (li.Count > 0)
                {
                    dr = li.Last();
                    writeToTxt(dr);
                }
                else
                {
                    dr = null;
                    writeToTxt(dr);
                }
            }
            else
            {
                writeToTxt(dr);
            }
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

        private void Xa1No_Enter(object sender, EventArgs e)
        {
            BeforeText = Xa1No.Text;
        }

        private void Xa1No_Validating(object sender, CancelEventArgs e)
        {
            if (Xa1No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (Xa1No.Text.Trim() == "")
            {
                e.Cancel = true;
                Xa1No.Text = "";
                Xa1Name.Text = "";
                Xa1Coun2.Text = "";
                Xa1Coun1.Text = "";
                Xa1Cent.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnState == "Append")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    e.Cancel = true;
                    Xa1No.Text = "";
                    Xa1Name.Text = "";
                    Xa1Coun2.Text = "";
                    Xa1Coun1.Text = "";
                    Xa1Cent.Text = "";
                    MessageBox.Show("此幣別編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (btnState == "Modify")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    if (Xa1No.Text.Trim() != BeforeText)
                        writeToTxt(dr);
                }
                else
                {
                    e.Cancel = true;
                    Xa1No.SelectAll();
                    dr = getCurrentDataRow(tempNo);

                    xe.Open<JBS.JS.Xa01>(sender, row =>
                    {
                        Xa1No.Text = row["xa1no"].ToString();
                        Xa1Name.Text = row["xa1name"].ToString();
                        Xa1Coun2.Text = row["xa1coun2"].ToString();
                        Xa1Coun1.Text = row["xa1coun1"].ToString();
                        Xa1Cent.Text = row["xa1cent"].ToString();
                    });
                }
            }

        }

        public DialogResult ShowDialog(out string no)
        {
            var dl = this.ShowDialog();

            no = this.tempNo ?? "";
            return dl;
        }




    }
}
