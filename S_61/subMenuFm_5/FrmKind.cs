using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmKind : Formbase
    {
        JBS.JS.xEvents xe;

        List<TextBoxbase> list;
        SqlTransaction tran;
        List<DataRow> li = new List<DataRow>();
        DataTable dt = new DataTable();
        DataRow dr;
        string tempNo = "";
        int temp = 0;
        string btnState = "";
        List<ButtonT> SC = new List<ButtonT>();
        List<ButtonT> Others = new List<ButtonT>();
        string BeforeText;

        public FrmKind()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();
        }

        private void FrmKind_Load(object sender, EventArgs e)
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
                    string str = "select * from Kind";
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
            dr = li.Find(r => r.Field<string>("KiNo") == strBrow);
            if (dr != null)
            {
                writeToTxt(dr);
            }
        }

        private void writeToTxt(DataRow dr)
        {
            if (dr != null)
            {
                KiNo.Text = dr["KiNo"].ToString();
                KiName.Text = dr["KiName"].ToString();
                switch (dr["KiTax"].ToString())
                {
                    case "1": KiTax1.Checked = true; break;
                    case "2": KiTax2.Checked = true; break;
                    default: break;
                }
            }
            else
            {
                KiNo.Text = "";
                KiName.Text = "";
                foreach (Control c in panelNT1.Controls)
                {
                    if (c is RadioButton) (c as RadioButton).Checked = false;
                    if (c is Label) c.BackColor = Color.Transparent;
                }
            }
        }

        private DataRow getCurrentDataRow()
        {
            return li.Find(o => o.Field<string>("KiNo") == (KiNo.Text.Trim()));
        }

        private DataRow getCurrentDataRow(string s)
        {
            return li.Find(o => o.Field<string>("KiNo") == (s));
        }

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
            tempNo = KiNo.Text.Trim();
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
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tempNo = KiNo.Text.Trim();
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
                    MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            tempNo = KiNo.Text.Trim();

            KiTax1.Checked = true;
            KiNo.ReadOnly = false;
            KiNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (KiNo.TrimTextLenth() == 0)
                return;

            Common.SetTextState(this.FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            tempNo = KiNo.Text.Trim();

            KiNo.ReadOnly = false;
            KiNo.Focus();
            KiNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (KiNo.TrimTextLenth() == 0)
                return;

            {
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
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.CommandText = "delete from Kind where KiNo=@KiNo";

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
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            loadDB();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Kind>(
                    3,
                @" kino,kiname,稅別 = 
                   case 
	                   when kitax=1 then '應稅'
	                   when kitax=2 then '免稅'
                   end ",
                new string[] { "稅別" },
                new string[] { "稅別" }))
            {
                frm.TSeekNo = KiNo.Text.Trim();
                var dl = frm.ShowDialog(this);
                if (dl == DialogResult.OK)
                {
                    xe.Validate<JBS.JS.Kind>(frm.TResult, row =>
                    {
                        KiNo.Text = row["KiNo"].ToString().Trim();
                        KiName.Text = row["KiName"].ToString().Trim();

                        switch (row["KiTax"].ToString())
                        {
                            case "1": KiTax1.Checked = true; break;
                            case "2": KiTax2.Checked = true; break;
                            default: break;
                        }
                    },()=>MessageBox.Show("Test"));
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

            if (btnState == "Append")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    MessageBox.Show("此產品編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    KiNo.Text = string.Empty;
                    KiNo.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@KiName", KiName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pnlKiTax", getRadioNumber(panelNT1));
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.CommandText = "insert into Kind values (@KiNo,@KiName,@pnlKiTax,0)";


                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }

                    //更新current值
                    tempNo = KiNo.Text.Trim();
                    KiNo.Text = string.Empty;
                    KiName.Text = string.Empty;
                    KiNo.Focus();
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
                    KiNo.Text = string.Empty;
                    KiNo.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@KiName", KiName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pnlKiTax", getRadioNumber(panelNT1));
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.CommandText = "update Kind set KiName=@KiName,KiTax=@pnlKiTax where KiNo=@KiNo";

                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }
                    //更新current值
                    tempNo = KiNo.Text.Trim();
                    KiNo.Text = string.Empty;
                    KiName.Text = string.Empty;
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
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnState = string.Empty;

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

        private void labelsColorToControl(Label lb)
        {
            lb.Parent.Controls.OfType<RadioButton>().ToList().ForEach(r =>
            {
                if (r.Name == lb.Name.Substring(3))
                    r.Checked = true;
            });
        }

        private void labelsColorToControl(RadioButton rd)
        {
            rd.Parent.Controls.OfType<Label>().ToList().ForEach(l =>
            {
                if (l.Name.EndsWith(rd.Name)) l.BackColor = Color.LightBlue;
                else
                    l.BackColor = Color.Transparent;
            });
        }

        private void labels_Click(object sender, EventArgs e)
        {
            if (KiNo.ReadOnly) return;
            labelsColorToControl(sender as Label);
        }

        private void radios_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                labelsColorToControl(sender as RadioButton);
        }

        private string getRadioNumber(Panel pnl)
        {
            string str = "";
            foreach (RadioButton rd in pnl.Controls.OfType<RadioButton>())
            {
                if (rd.Checked)
                    str = rd.Name.Last().ToString();
            }
            return str;
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

        private void KiNo_Enter(object sender, EventArgs e)
        {
            BeforeText = KiNo.Text;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmKindPrint())
            { 
                frm.ShowDialog();
            }
        }

        private void KiNo_Validating(object sender, CancelEventArgs e)
        {
            if (KiNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (KiNo.Text.Trim() == "")
            {
                e.Cancel = true;
                KiNo.Text = "";
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
                    KiNo.Text = "";
                    MessageBox.Show("此產品編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (btnState == "Modify")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    if (KiNo.Text.Trim() != BeforeText)
                        writeToTxt(dr);
                }
                else
                {
                    e.Cancel = true;
                    KiNo.SelectAll();
                    dr = getCurrentDataRow(tempNo);

                    xe.Open<JBS.JS.Kind>(sender, row =>
                    {
                        KiNo.Text = row["KiNo"].ToString().Trim();
                        KiName.Text = row["KiName"].ToString().Trim();

                        switch (row["KiTax"].ToString())
                        {
                            case "1": KiTax1.Checked = true; break;
                            case "2": KiTax2.Checked = true; break;
                            default: break;
                        }
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

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            panelNT1.Enabled = !btnAppend.Enabled;
        }
    }
}
