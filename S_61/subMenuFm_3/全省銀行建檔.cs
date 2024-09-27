using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.SOther;

namespace S_61.subMenuFm_3
{
    public partial class 全省銀行建檔 : Formbase
    {
        List<TextBoxbase> list;

        public String 票據連線字串 = "";
        DataTable tbM = new DataTable();
        List<DataRow> li = new List<DataRow>();
         
        List<TextBox> Txt;
        DataRow dr;
        string btnState;
        string BeforeText;
        string CurrentRow = "";
        SqlTransaction tran;
        public 全省銀行建檔()
        {
            InitializeComponent();
            this.list = this.getEnumMember();
            //pVar.全省銀行建檔 = this;
            //SC = new List<btnT> { btnSave, btnCancel };
            //Others = new List<btnT> { btnTop, btnPrior, btnNext, btnBottom, btnAppend, btnModify, btnDelete, btnExit, btnDuplicate, btnBrow };
            Txt = new List<TextBox> { BaNo, BaName, BaTel, BaAddr1, BaR1, BaMemo };
        }

        private void 全省銀行建檔_Load(object sender, EventArgs e)
        {
            loadM();
            if (li.Count > 0)
                WriteToTxt(li.First());
        }

        void loadM()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    tbM.Clear();
                    li.Clear();
                    SqlDataAdapter dd = new SqlDataAdapter("select * from bank order by bano", cn);
                    dd.Fill(tbM);
                    if (tbM.Rows.Count > 0) li = tbM.AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        DataRow GetDataRow()
        {
            return li.Find(r => r["BaNo"].ToString() == BaNo.Text.Trim());
        }

        DataRow GetDataRow(string str)
        {
            return li.Find(r => r["BaNo"].ToString() == str.Trim());
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null)
            {
                Txt.ForEach(r => r.Text = "");
            }
            else
            {
                Txt.ForEach(r =>
                {
                    r.Text = dr[r.Name.ToString()].ToString().Trim();
                });
                CurrentRow = dr["BaNo"].ToString().Trim();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            loadM();
            if (li.Count > 0)
            {
                dr = li.First();
                WriteToTxt(dr);
            }
            btnTop.Enabled = btnPrior.Enabled = false;
            btnNext.Enabled = btnBottom.Enabled = true;
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            dr = GetDataRow();
            int temp = li.IndexOf(dr);
            loadM();
            if (li.Count > 0)
            {
                dr = GetDataRow(CurrentRow);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (temp == 0)
                    {
                        MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        WriteToTxt(li.First());
                        btnTop.Enabled = btnPrior.Enabled = false;
                        btnNext.Enabled = btnBottom.Enabled = true;
                    }
                    else
                    {
                        WriteToTxt(li[--temp]);
                        btnTop.Enabled = btnPrior.Enabled = btnNext.Enabled = btnBottom.Enabled = true;
                    }
                }
                if (i > 0)
                {
                    WriteToTxt(li[--i]);
                    btnTop.Enabled = btnPrior.Enabled = btnNext.Enabled = btnBottom.Enabled = true;
                }
                if (i == 0)
                {
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WriteToTxt(li.First());
                    btnTop.Enabled = btnPrior.Enabled = false;
                    btnNext.Enabled = btnBottom.Enabled = true;
                }
            }
            else
            {
                WriteToTxt(null);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dr = GetDataRow();
            int temp = li.LastIndexOf(dr);
            loadM();
            if (li.Count > 0)
            {
                dr = GetDataRow(CurrentRow);
                int i = li.LastIndexOf(dr);
                if (i == -1)
                {
                    if (temp >= li.Count - 1)
                    {
                        MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        WriteToTxt(li.Last());
                        btnTop.Enabled = btnPrior.Enabled = true;
                        btnNext.Enabled = btnBottom.Enabled = false;
                    }
                    else
                    {
                        WriteToTxt(li[++temp]);
                        btnTop.Enabled = btnPrior.Enabled = btnNext.Enabled = btnBottom.Enabled = true;
                    }
                }
                if (i < li.Count - 1)
                {
                    WriteToTxt(li[++i]);
                    btnTop.Enabled = btnPrior.Enabled = btnNext.Enabled = btnBottom.Enabled = true;
                }
                else
                {
                    MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WriteToTxt(li.Last());
                    btnTop.Enabled = btnPrior.Enabled = true;
                    btnNext.Enabled = btnBottom.Enabled = false;
                }
            }
            else
            {
                WriteToTxt(null);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            loadM();
            if (li.Count > 0)
            {
                dr = li.Last();
                WriteToTxt(dr);
            }
            btnNext.Enabled = btnBottom.Enabled = false;
            btnTop.Enabled = btnPrior.Enabled = true;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            btnState = "Append";
            //SC.ForEach(r => r.Enabled = true);
            //Others.ForEach(r => r.Enabled = false);
            //Txt.ForEach(r => r.ReadOnly = false);
            //Txt.ForEach(r => r.Text = "");
            BaNo.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (tbM.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(this.FormState = FormEditState.Duplicate, ref list);
            btnState = "Duplicate";
            //SC.ForEach(r => r.Enabled = true);
            //Others.ForEach(r => r.Enabled = false);
            //Txt.ForEach(r => r.ReadOnly = false);
            BaNo.Text = "";
            BaNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (tbM.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(this.FormState = FormEditState.Modify, ref list);
            btnState = "Modify";
            //SC.ForEach(r => r.Enabled = true);
            //Others.ForEach(r => r.Enabled = false);
            //Txt.ForEach(r => r.ReadOnly = false);
            BaNo.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbM.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnState = "Delete";
            if (MessageBox.Show("請確定是否刪除此筆記錄?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                //執行刪除指令前，檢查此筆資料是否已被別人刪除
                loadM();
                dr = GetDataRow();
                if (li.IndexOf(dr) == -1)
                {
                    if (li.Count > 0)
                    {
                        dr = li.Last();
                        WriteToTxt(dr);
                    }
                    else
                    {
                        dr = null;
                        WriteToTxt(dr);
                    }
                    return;//資料已被刪除，以下程式碼不執行
                }
                try
                {
                    using (SqlConnection cn = new SqlConnection(票據連線字串))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();

                        tran = cn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.Parameters.AddWithValue("bano", BaNo.Text.Trim());
                        cmd.CommandText = "delete from bank where bano=@bano COLLATE Chinese_Taiwan_Stroke_BIN";
                        cmd.ExecuteNonQuery();

                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                        dr = GetDataRow(CurrentRow);
                        int i = li.IndexOf(dr);
                        loadM();
                        if (li.Count > 0)
                        {
                            if (i >= li.Count - 1)
                            {
                                WriteToTxt(li.Last());
                                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                WriteToTxt(li[i]);
                            }
                        }
                        else
                        {
                            WriteToTxt(null);
                        }
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
            if (tbM.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new 全省銀行建檔_瀏覽())
            { 
                frm.票據連線字串 = 票據連線字串;
                frm.SeekNo = BaNo.Text.Trim();
                frm.ShowDialog();
                switch (frm.DialogResult)
                {
                    case DialogResult.OK:
                        if (frm.result != "")
                        {
                            loadM();
                            if (li.Count > 0)
                            {
                                dr = GetDataRow(frm.result);
                                WriteToTxt(dr);
                            }
                            else
                            {
                                WriteToTxt(null);
                            }
                        }
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaNo.Text.Trim() == "")
            {
                MessageBox.Show("『銀行編號』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BaNo.Focus();
                return;
            }
            if (btnState == "Append" || btnState == "Duplicate")
            {
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    tran = cn.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "insert into bank (";
                        Txt.ForEach(r =>
                        {
                            if (r.Name.ToString() != "BaMemo")
                                cmd.CommandText += r.Name.ToString() + ",";
                            else
                                cmd.CommandText += r.Name.ToString() + ")values(";
                        });
                        Txt.ForEach(r =>
                        {
                            if (r.Name.ToString() != "BaMemo")
                                cmd.CommandText += "@" + r.Name.ToString() + ",";
                            else
                                cmd.CommandText += "@" + r.Name.ToString() + ")";
                            cmd.Parameters.AddWithValue(r.Name.ToString(), r.Text.ToString());
                        });
                        cmd.ExecuteNonQuery();
                        tran.Commit();
                        tran.Dispose();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                CurrentRow = BaNo.Text.Trim();
                btnAppend_Click(null, null);
            }
            if (btnState == "Modify")
            {
                loadM();
                dr = GetDataRow(CurrentRow);
                int index = li.IndexOf(dr);
                if (index == -1)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BaNo.Focus();
                    return;
                }
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    tran = cn.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.Parameters.AddWithValue("bano", CurrentRow);
                        cmd.CommandText = "delete bank where BaNo=@bano";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "insert into bank (";
                        Txt.ForEach(r =>
                        {
                            if (r.Name.ToString() != "BaMemo")
                                cmd.CommandText += r.Name.ToString() + ",";
                            else
                                cmd.CommandText += r.Name.ToString() + ")values(";
                        });
                        Txt.ForEach(r =>
                        {
                            if (r.Name.ToString() != "BaMemo")
                                cmd.CommandText += "@" + r.Name.ToString() + ",";
                            else
                                cmd.CommandText += "@" + r.Name.ToString() + ")";
                            cmd.Parameters.AddWithValue(r.Name.ToString(), r.Text.ToString());
                        });
                        cmd.ExecuteNonQuery();
                        tran.Commit();
                        tran.Dispose();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                CurrentRow = BaNo.Text.Trim();
                Txt.ForEach(r => r.Text = "");
                BaNo.Focus();
                btnState = "Modify";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnState = "";
            //SC.ForEach(r => r.Enabled = false);
            //Others.ForEach(r => r.Enabled = true);
            //Txt.ForEach(r => r.ReadOnly = true);
            loadM();
            if (li.Count > 0)
            {
                dr = GetDataRow(CurrentRow);
                WriteToTxt(dr);
            }
            else
            {
                WriteToTxt(null);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void BaNo_DoubleClick(object sender, EventArgs e)
        {
            if (BaNo.ReadOnly != true)
            {
                using (var frm = new 全省銀行建檔_瀏覽())
                { 
                    frm.票據連線字串 = 票據連線字串;
                    frm.CanAppend = false;
                    frm.SeekNo = BaNo.Text.Trim();
                    frm.開窗模式 = true;
                    frm.ShowDialog();
                    switch (frm.DialogResult)
                    {
                        case DialogResult.OK:
                            dr = GetDataRow(frm.Result["BaNo"].ToString().Trim());
                            WriteToTxt(dr);
                            break;
                    }
                }
            }
        }

        private void BaNo_Enter(object sender, EventArgs e)
        {
            BeforeText = BaNo.Text.Trim();
        }

        private void BaNo_Validating(object sender, CancelEventArgs e)
        {
            if (BaNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (BaNo.Text.Trim() == "")
            {
                e.Cancel = true;
                BaNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                loadM();
                dr = GetDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    e.Cancel = true;
                    BaNo.Text = "";
                    MessageBox.Show("此銀行編號已重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (btnState == "Duplicate")
            {
                loadM();
                dr = GetDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    e.Cancel = true;
                    BaNo.Text = "";
                    MessageBox.Show("此銀行編號已重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (btnState == "Modify")
            {
                loadM();
                dr = GetDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    if (BaNo.Text.Trim() != BeforeText)
                        WriteToTxt(dr);
                }
                else
                {
                    e.Cancel = true;
                    BaNo.SelectAll();
                    dr = GetDataRow(CurrentRow);
                    //開瀏覽視窗
                    using (var frm = new 全省銀行建檔_瀏覽())
                    { 
                        frm.票據連線字串 = 票據連線字串;
                        frm.CanAppend = false;
                        frm.SeekNo = BaNo.Text.Trim();
                        frm.開窗模式 = true;
                        frm.ShowDialog();
                        switch (frm.DialogResult)
                        {
                            case DialogResult.OK:
                                dr = GetDataRow(frm.Result["BaNo"].ToString().Trim());
                                WriteToTxt(dr);
                                break;
                        }
                    }

                }
            }
        }

        private void BaAddr1_DoubleClick(object sender, EventArgs e)
        {
            if (BaAddr1.ReadOnly != true)
            {
                using (var frm = new FrmSaddr())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.BaAddr1.Text = frm.TAddr;
                        this.BaR1.Text = frm.TZip;
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    btnAppend.Focus();
                    btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.Focus();
                    btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btnDelete.Focus();
                    btnDelete.PerformClick();
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



    }
}
