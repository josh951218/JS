using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class Einvsetup : Formbase
    {
        public Einvsetup()
        {
            InitializeComponent();
        }
        //JBS.JS.xEvents xe;

        List<TextBoxbase> list = new List<TextBoxbase>(); 
        SqlTransaction tran;
        List<DataRow> li = new List<DataRow>();
        DataTable dt = new DataTable();
        DataRow dr;
        string tempNo = "";
        int temp = 0;
        string btnState = "";
        List<ButtonT> SC = new List<ButtonT>();
        List<ButtonT> Others = new List<ButtonT>();
        //string BeforeText;

        private void Einvsetup_Load(object sender, EventArgs e)
        {
            loadDB();
            if (dt.Rows.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
            btnAppend.Focus();
        }

        private void writeToTxt(DataRow dr)
        {
            if (dr != null)
            {
                Einvid.Text = dr["Einvid"].ToString();
                iTitle.Text = dr["EinvTitle"].ToString();
                iStore.Text = dr["EinvStore"].ToString();
                iUnno.Text = dr["EinvUnno"].ToString();
                iTaxNo.Text = dr["EinvTaxNo"].ToString();
                iTel.Text = dr["EinvTel"].ToString();
                iAddress.Text = dr["EinvAddress"].ToString();
                iMemo1.Text = dr["EinvMemo1"].ToString();
                iMemo2.Text = dr["EinvMemo2"].ToString();
                ScInvoic7b.Text = dr["ScInvoic7b"].ToString();
                ScInvoic7e.Text = dr["ScInvoic7e"].ToString();
                ScInvoic8b.Text = dr["ScInvoic8b"].ToString();
                ScInvoic8e.Text = dr["ScInvoic8e"].ToString();
                ScInvoic7.Text = dr["ScInvoic7"].ToString();
                ScInvoic8.Text = dr["ScInvoic8"].ToString();
            }
            else
            {
                Einvid.Text ="";
                iTitle.Text ="";
                iStore.Text ="";
                iUnno.Text = "";
                iTaxNo.Text ="";
                iTel.Text = "";
                iAddress.Text = "";
                iMemo1.Text = "";
                iMemo2.Text = "";
                ScInvoic7b.Text = "";
                ScInvoic7e.Text ="";
                ScInvoic8b.Text = "";
                ScInvoic8e.Text = "";
                ScInvoic7.Text = "";
                ScInvoic8.Text = "";
            }
        }
        public void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select * from Einvsetup";
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
        private void btnTop_Click(object sender, EventArgs e)
        {
            loadDB();
            if (li.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
        }
        private DataRow getCurrentDataRow()
        {
            return li.Find(o => o.Field<string>("Einvid") == (Einvid.Text.Trim()));
        }

        private DataRow getCurrentDataRow(string s)
        {
            return li.Find(o => o.Field<string>("Einvid") == (s));
        }
        private void btnPrior_Click(object sender, EventArgs e)
        {
            tempNo = Einvid.Text.Trim();
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
            tempNo = Einvid.Text.Trim();
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
            if (dt.Rows.Count >= Common.Sys_Einvusen)
            {
                MessageBox.Show("您新增的家數已超過,請聯絡華越擴增使用統編數!");
                return;
            }
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            Einvid.ReadOnly = iTitle.ReadOnly = iStore.ReadOnly = iUnno.ReadOnly = iTaxNo.ReadOnly =
            iTel.ReadOnly = iAddress.ReadOnly = iMemo1.ReadOnly = iMemo2.ReadOnly =
            ScInvoic7b.ReadOnly = ScInvoic7e.ReadOnly = ScInvoic8b.ReadOnly = ScInvoic8e.ReadOnly = false;
            btnState = ((Button)sender).Name.Substring(3);
            tempNo = Einvid.Text.Trim();
            Einvid.Focus();
            whenappend();
        }

        private void whenappend()
        {
            Einvid.Text = "";
            iTitle.Text = "";
            iStore.Text = "";
            iUnno.Text = "";
            iTaxNo.Text = "";
            iTel.Text = "";
            iAddress.Text = "";
            iMemo1.Text = "";
            iMemo2.Text = "";
            ScInvoic7b.Text = "";
            ScInvoic7e.Text = "";
            ScInvoic8b.Text = "";
            ScInvoic8e.Text = "";
            ScInvoic7.Text = "";
            ScInvoic8.Text = "";
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (Einvid.TrimTextLenth() == 0)
                return;

            Common.SetTextState(this.FormState = FormEditState.Modify, ref list);
            iStore.ReadOnly = iTel.ReadOnly = iAddress.ReadOnly = iMemo1.ReadOnly = iMemo2.ReadOnly =
            ScInvoic7b.ReadOnly = ScInvoic7e.ReadOnly = ScInvoic8b.ReadOnly = ScInvoic8e.ReadOnly = 
            ScInvoic7.ReadOnly = ScInvoic8.ReadOnly = false;
            btnState = ((Button)sender).Name.Substring(3);
            tempNo = Einvid.Text.Trim();

            iTitle.Focus();
            iTitle.SelectAll();
            Einvid.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Einvid.TrimTextLenth() == 0)
                return;
            var dl = MessageBox.Show("提醒請確認此設定無相關單據使用後再進行刪除動作，請確定是否刪除？", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dl == DialogResult.Yes)
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
                        cmd.Parameters.AddWithValue("@Einvid", Einvid.Text.Trim());
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.CommandText = "delete from Einvsetup where Einvid=@Einvid";

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("電子發票統編與匯入檔案不合，請確認!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //MessageBox.Show("電子發票設定號碼區間與匯入檔案不合，請確認!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //MessageBox.Show("電子發票設定字軌與匯入檔案不合，請確認!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //MessageBox.Show("系統時間與匯入檔案的期別不合，請確認!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            #region 各種空白提醒
            if (Einvid.Text.Trim() == "")
            {
                MessageBox.Show("電子發票設定編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Einvid.Focus();
                return;
            }
            if (ScInvoic7b.Text.Trim() == "" && ScInvoic8b.Text.Trim() == "" )
            {
                MessageBox.Show("電子發票起始發票號碼不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Einvid.Focus();
                return;
            }
            if (iUnno.Text.Trim()=="")
            {
                MessageBox.Show("公司統一編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Einvid.Focus();
                return;
            }
            if (ScInvoic7e.Text.Trim() == "" && ScInvoic8e.Text.Trim() == "")
            {
                MessageBox.Show("電子發票截止發票號碼不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Einvid.Focus();
                return;
            }
            #endregion
            if (btnState == "Append")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    MessageBox.Show("此編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Einvid.Text = string.Empty;
                    Einvid.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Einvid", Einvid.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvTitle", iTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvStore", iStore.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvUnno", iUnno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvTaxNo", iTaxNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvTel", iTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvAddress", iAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvMemo1", iMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvMemo2", iMemo2.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic7", ScInvoic7.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic7b", ScInvoic7b.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic7e", ScInvoic7e.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic8", ScInvoic8.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic8b", ScInvoic8b.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic8e", ScInvoic8e.Text.Trim());
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.CommandText = @"insert into Einvsetup (Einvid, EinvTitle,EinvStore,EinvUnno,
                        EinvTaxNo,EinvTel,EinvAddress,EinvMemo1,EinvMemo2,ScInvoic7,ScInvoic7e,ScInvoic8,ScInvoic8e,ScInvoic7b,ScInvoic8b) 
                        values (@Einvid, @EinvTitle,@EinvStore,@EinvUnno,@EinvTaxNo,@EinvTel,@EinvAddress,
                        @EinvMemo1,@EinvMemo2,@ScInvoic7,@ScInvoic7e,@ScInvoic8,@ScInvoic8e,@ScInvoic7b,@ScInvoic8b)";

                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }

                    //更新current值
                    tempNo = Einvid.Text.Trim();
                    Einvid.Text = string.Empty;
                    Einvid.Focus();
                    btnCancel_Click(null, null);
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
                    MessageBox.Show("在修改狀態底下找不到此設定編碼資料,已被其他使用者刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Einvid.Text = string.Empty;
                    Einvid.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Einvid", Einvid.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvTitle", iTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvStore", iStore.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvUnno", iUnno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvTaxNo", iTaxNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvTel", iTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvAddress", iAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvMemo1", iMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EinvMemo2", iMemo2.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic7", ScInvoic7.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic7b", ScInvoic7b.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic7e", ScInvoic7e.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic8", ScInvoic8.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic8b", ScInvoic8b.Text.Trim());
                        cmd.Parameters.AddWithValue("@ScInvoic8e", ScInvoic8e.Text.Trim());
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.CommandText = @"update Einvsetup set EinvTitle=@EinvTitle,EinvStore=@EinvStore,
                       EinvUnno=@EinvUnno,EinvTaxNo=@EinvTaxNo,EinvTel=@EinvTel,EinvAddress=@EinvAddress,
                       EinvMemo1=@EinvMemo1,EinvMemo2=@EinvMemo2,ScInvoic7=@ScInvoic7,ScInvoic7e=@ScInvoic7e,
                      ScInvoic7b=@ScInvoic7b,ScInvoic8b=@ScInvoic8b,
                       ScInvoic8=@ScInvoic8,ScInvoic8e=@ScInvoic8e where Einvid=@Einvid";

                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();
                    }
                    //更新current值
                    tempNo = Einvid.Text.Trim();
                    Einvid.Text = string.Empty;
                    Einvid.Focus();
                    btnCancel_Click(null, null);
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
            Einvid.ReadOnly = iTitle.ReadOnly = iStore.ReadOnly = iUnno.ReadOnly = iTaxNo.ReadOnly =
            iTel.ReadOnly = iAddress.ReadOnly = iMemo1.ReadOnly = iMemo2.ReadOnly =
            ScInvoic7b.ReadOnly = ScInvoic7e.ReadOnly = ScInvoic8b.ReadOnly = ScInvoic8e.ReadOnly =  ScInvoic7.ReadOnly = ScInvoic8.ReadOnly=true;
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

        private void iUnno_Validated(object sender, EventArgs e)
        {
            if (btnCancel.Focused || iUnno.TrimTextLenth() == 0) return;
            if(iUnno.Text.Length<8)
            {
                MessageBox.Show("輸入有誤，統一編號格式為8碼，請檢查");
                iUnno.Focus();
            }
        }

        private void ScInvoic7b_Validated(object sender, EventArgs e)
        {
            if (btnState == "Append")
            {
                ScInvoic7.Text = ScInvoic7b.Text;
                ScInvoic8.Text = ScInvoic8b.Text;
            }
        }
    }
}
