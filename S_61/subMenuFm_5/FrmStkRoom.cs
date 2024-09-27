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
    public partial class FrmStkRoom : Formbase
    {
        JBS.JS.xEvents xe;
        List<TextBoxbase> list = new List<TextBoxbase>();
        SqlTransaction tran;
        List<DataRow> li = new List<DataRow>();
        DataTable dt = new DataTable();
        DataTable dtItem = new DataTable();
        DataRow dr;
        string tempNo = "";
        int temp = 0;
        string btnState = string.Empty;
        string BeforeText;

        public FrmStkRoom()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();
            cn.ConnectionString = Common.sqlConnString;
        }

        private void FrmStkRoom_Load(object sender, EventArgs e)
        {
            loadDB();
            if (dt.Rows.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
            btnAppend.Focus();
        }

        void LoadItem()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from item";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        dtItem.Clear();
                        da.Fill(dtItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select * from StkRoom";
                    using (SqlDataAdapter da = new SqlDataAdapter(str, conn))
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void reLoadDB(string strBrow)
        {
            dr = li.Find(r => r.Field<string>("StNo") == strBrow);
            if (dr != null)
            {
                writeToTxt(dr);
            }
        }

        private void writeToTxt(DataRow dr)
        {
            if (dr != null)
            {
                StNo.Text = dr["StNo"].ToString();
                StName.Text = dr["StName"].ToString();
                switch (dr["StTrait"].ToString())
                {
                    case "1": StTrait1.Checked = true; break;
                    case "2": StTrait2.Checked = true; break;
                    case "3": StTrait3.Checked = true; break;
                    case "4": StTrait4.Checked = true; break;
                    default: break;
                }
            }
            else
            {
                StNo.Text = "";
                StName.Text = "";
                foreach (Control c in panelNT1.Controls)
                {
                    if (c is RadioButton) (c as RadioButton).Checked = false; 
                }
            }
        }

        private DataRow getCurrentDataRow()
        {
            return li.Find(o => o.Field<string>("StNo") == (StNo.Text.Trim()));
        }

        private DataRow getCurrentDataRow(string s)
        {
            return li.Find(o => o.Field<string>("StNo") == (s));
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
            tempNo = StNo.Text.Trim();
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
            tempNo = StNo.Text.Trim();
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
            if (Common.Series == "74" || Common.Series == "73")
            {
                DataTable dt = new DataTable();
                da1.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("此版本為單倉系統，無法新增倉庫。", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Text = "倉庫建檔作業";
                    return;
                }
            }

            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            tempNo = StNo.Text.Trim();

            StTrait1.Checked = true;

            //新增時，清空欄位,焦點於StNo
            StNo.ReadOnly = false;
            StNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (StNo.TrimTextLenth() == 0)
                return;

            if (StNo.Text.Trim() == "BIN" || StNo.Text.Trim() == "BOUT")
            {
                MessageBox.Show("此倉庫為預設倉庫，禁止修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(this.FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            tempNo = StNo.Text.Trim();

            //修改時,焦點於StNo
            StNo.ReadOnly = false;
            StNo.Focus();
            StNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnState = ((Button)sender).Name.Substring(3);
            if (StNo.Text == string.Empty) return;//StNo沒有值，就不執行下面的指令

            if (!checkStock())
            {
                MessageBox.Show("此倉庫為預設倉庫，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (StNo.Text.Trim() == "BIN" || StNo.Text.Trim() == "BOUT")
            {
                MessageBox.Show("此倉庫為預設倉庫，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());

                    string sql = "select * from stock where stno=@StNo and itqty !=0";
                    cmd.CommandText = sql;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("此倉庫已有異動庫存資料，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
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
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("StNo", StNo.Text);
                        cmd.CommandText = "delete from StkRoom where StNo=@StNo;";
                        cmd.CommandText += "delete from stock where stno=@StNo";

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

            using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>(
                    3,
                @" stno,stname,倉庫類別 = 
                   case 
	                   when sttrait=1 then '庫存倉'
	                   when sttrait=2 then '借出倉'
	                   when sttrait=3 then '加工倉'
	                   when sttrait=4 then '借入倉'
                   end ",
                new string[] { "倉庫類別" },
                new string[] { "倉庫類別" }))
            {
                frm.TSeekNo = StNo.Text.Trim();
                var dl = frm.ShowDialog(this);
                if (dl == DialogResult.OK)
                {
                    xe.Validate<JBS.JS.Stkroom>(frm.TResult, row =>
                    {
                        StNo.Text = row["stno"].ToString().Trim();
                        StName.Text = row["stname"].ToString().Trim();

                        switch (row["StTrait"].ToString())
                        {
                            case "1": StTrait1.Checked = true; break;
                            case "2": StTrait2.Checked = true; break;
                            case "3": StTrait3.Checked = true; break;
                            case "4": StTrait4.Checked = true; break;
                            default: break;
                        }
                    });
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (StNo.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.Focus();
                return;
            }
            if (StNo.Text.Trim() == "BIN" || StNo.Text.Trim() == "BOUT")
            {
                MessageBox.Show("此倉庫為預設倉庫，禁止修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnState == "Append")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    MessageBox.Show("此倉庫編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StNo.Text = string.Empty;
                    StNo.Focus();
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
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@StName", StName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pnlStTrait", getRadioNumber(panelNT1));

                        cmd.CommandText = "insert into StkRoom"
                             + "(StNo,StName,StTrait) VALUES (@StNo,@StName,@pnlStTrait)";
                        cmd.ExecuteNonQuery();
                         
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }

                    //更新current值
                    tempNo = StNo.Text.Trim();
                    StNo.Text = string.Empty;
                    StName.Text = string.Empty;
                    StNo.Focus();
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
                    StNo.Text = string.Empty;
                    StNo.Focus();
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
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@StName", StName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pnlStTrait", getRadioNumber(panelNT1));

                        cmd.CommandText = ""
                            + " update StkRoom set StName=@StName,StTrait=@pnlStTrait where StNo=@StNo ;"
                            + " update stock set StName=@StName,StTrait=@pnlStTrait where StNo =@StNo ;";
                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }

                    //更新current值
                    tempNo = StNo.Text.Trim();
                    StNo.Text = string.Empty;
                    StName.Text = string.Empty;
                    StNo.Focus();
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

        bool checkStock()
        {
            bool CanDelete = false;
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());

                cmd.CommandText = "select count(stno) from scrit where StNo=@StNo";
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    rd.Read();
                    if (rd[0].ToString() == "0")
                    {
                        CanDelete = true;
                    }
                    else
                    {
                        CanDelete = false;
                    }
                }

                rd.Close(); rd.Dispose();
                tran.Commit(); tran.Dispose();
                cmd.Dispose();
                conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                CanDelete = false;
                MessageBox.Show(ex.ToString());
            }
            return CanDelete;
        }

        private void StNo_Enter(object sender, EventArgs e)
        {
            BeforeText = StNo.Text;
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.Text.Trim() == "")
            {
                e.Cancel = true;
                StNo.Text = "";
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
                    StNo.Text = "";
                    MessageBox.Show("此倉庫編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (btnState == "Modify")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    if (StNo.Text.Trim() != BeforeText)
                        writeToTxt(dr);
                }
                else
                {
                    e.Cancel = true;
                    StNo.SelectAll();
                    dr = getCurrentDataRow(tempNo);

                    xe.Open<JBS.JS.Stkroom>(sender, row =>
                    {
                        StNo.Text = row["stno"].ToString().Trim();
                        StName.Text = row["stname"].ToString().Trim();

                        switch (row["StTrait"].ToString())
                        {
                            case "1": StTrait1.Checked = true; break;
                            case "2": StTrait2.Checked = true; break;
                            case "3": StTrait3.Checked = true; break;
                            case "4": StTrait4.Checked = true; break;
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
