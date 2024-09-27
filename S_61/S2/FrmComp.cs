using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmComp : Formbase
    {
        JBS.JS.xEvents xe;
        List<TextBoxbase> list;
        List<DataRow> li = new List<DataRow>();
        DataTable dt = new DataTable();
        DataRow dr;
        string tempNo;
        int temp = 0;
        string BeforeText;

        public FrmComp()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();
        }

        private void FrmComp_Load(object sender, EventArgs e)
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
                    string str = "select cono,coname1 from Comp";
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
                MessageBox.Show(ex.ToString());
            }
        }

        public void reLoadDB(string strBrow)
        {
            dr = li.Find(r => r.Field<string>("CoNo") == strBrow);
            if (dr != null)
            {
                writeToTxt(dr);
            }
        }

        private void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                tempNo = row["CoNo"].ToString().Trim();

                CoNo.Text = row["CoNo"].ToString().Trim();
                CoName1.Text = row["CoName1"].ToString();
            }
            else
            {
                tempNo = "";
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
        }

        private DataRow getCurrentDataRow()
        {
            return li.Find(o => o.Field<string>("CoNo") == (CoNo.Text.Trim()));
        }

        private DataRow getCurrentDataRow(string s)
        {
            return li.Find(o => o.Field<string>("CoNo") == (s));
        }



        //功能按鈕
        private void btnTop_Click(object sender, EventArgs e)
        {
            loadDB();
            if (li.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            tempNo = CoNo.Text.Trim();
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
                        return;
                    }
                    else
                    {
                        dr = li[--temp];
                        writeToTxt(dr);
                        return;
                    }
                }
                if (i > 0)
                {
                    dr = li[--i];
                    writeToTxt(dr);
                }
                else
                {

                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tempNo = CoNo.Text.Trim();
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

                        return;
                    }
                    else
                    {
                        dr = li[++i];
                        writeToTxt(dr);

                        return;
                    }
                }
                if (i < li.Count - 1)
                {
                    dr = li[++i];
                    writeToTxt(dr);

                }
                else
                {

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
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = CoNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            CoNo.ReadOnly = false;
            CoNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (CoNo.Text == string.Empty)
                return;

            tempNo = CoNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            CoNo.ReadOnly = false;
            CoNo.Focus();
            CoNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CoNo.Text.Trim() == "T")
            {
                MessageBox.Show(
                    "T公司為預設公司, 無法刪除!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (CoNo.Text == string.Empty)
                return;

            string confirm = "請確定是否刪除此筆記錄?";
            if (MessageBox.Show(confirm, "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
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
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CoNo", CoNo.Text.Trim());

                        cmd.CommandText = "delete from Comp where CoNo=@CoNo";

                        cmd.ExecuteNonQuery();
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
                    MessageBox.Show("DelError:\n" + ex.ToString());
                }
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

            using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Comp>())
            {
                frm.TSeekNo = CoNo.Text.Trim();
                var dl = frm.ShowDialog(this);
                if (dl == DialogResult.OK)
                {
                    xe.Validate<JBS.JS.Comp>(frm.TResult, row =>
                    {
                        CoNo.Text = row["CoNo"].ToString();
                        CoName1.Text = row["CoName1"].ToString();
                    });
                }
            }  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (CoNo.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CoNo.Focus();
                return;
            }

            if (FormState == FormEditState.Append)
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    MessageBox.Show("此公司編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CoNo.Text = string.Empty;
                    CoNo.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CoNo", CoNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@CoName1", CoName1.Text.Trim());

                        cmd.CommandText = "insert into Comp (CoNo,CoName1) values("
                            + "(@CoNo)" + "," + "@CoName1" + ")";

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    tempNo = CoNo.Text.Trim();
                    btnAppend_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AppendError:\n" + ex.ToString());
                }
            }
            if (FormState == FormEditState.Modify)
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CoNo.Text = string.Empty;
                    CoNo.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CoNo", CoNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@CoName1", CoName1.Text.Trim());

                        cmd.CommandText = "update Comp set CoName1="
                            + "@CoName1" + " where CoNo =" + "@CoNo";

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    tempNo = CoNo.Text.Trim();
                    btnAppend_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
            Common.SetTextState(FormState = FormEditState.None, ref list);
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
                    if (btnAppend.Enabled)
                        btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (btnModify.Enabled)
                        btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (btnDelete.Enabled)
                        btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    if (btnBrow.Enabled)
                        btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    if (btnExit.Enabled)
                        btnExit.PerformClick();
                    break;
                case Keys.Home:
                    if (btnTop.Enabled)
                        btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    if (btnPrior.Enabled)
                        btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    if (btnNext.Enabled)
                        btnNext.PerformClick();
                    break;
                case Keys.End:
                    if (btnBottom.Enabled)
                        btnBottom.PerformClick();
                    break;
                case Keys.F9:
                    if (btnSave.Enabled)
                        btnSave.PerformClick();
                    break;
                case Keys.F4:
                    if (btnCancel.Enabled)
                        btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CoNo_Enter(object sender, EventArgs e)
        {
            BeforeText = CoNo.Text;
        }

        private void CoNo_Validating(object sender, CancelEventArgs e)
        {
            if (CoNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CoNo.Text.Trim() == "")
            {
                e.Cancel = true;
                CoNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (FormState == FormEditState.Append)
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    e.Cancel = true;
                    CoNo.Text = "";
                    MessageBox.Show("此公司編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            if (FormState == FormEditState.Modify)
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    if (CoNo.Text.Trim() != BeforeText)
                        writeToTxt(dr);
                }
                else
                {
                    e.Cancel = true;
                    CoNo.SelectAll();
                    dr = getCurrentDataRow(tempNo);

                    xe.Open<JBS.JS.Comp>(sender, row =>
                    {
                        CoNo.Text = row["CoNo"].ToString().Trim();
                        CoName1.Text = row["CoName1"].ToString().Trim();
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
