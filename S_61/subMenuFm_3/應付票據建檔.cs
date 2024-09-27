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

namespace S_61.subMenuFm_3
{
    public partial class 應付票據建檔 : Formbase
    {
        List<TextBoxbase> list;
        public DataTable tbM = new DataTable();
        public DataRow chksys;
        public DataRow scrit;
        public string 票據連線字串 = "";
        public string 廠商編號 = "";
        public bool 開窗模式 = false;
        List<DataRow> li = new List<DataRow>();

        List<Control> Txt;
        List<Label> Lb;
        DataRow dr;
        string btnState = "";
        string CurrentRow = "";
        string BeforeText;

        public 應付票據建檔()
        {
            InitializeComponent();
            this.list = this.getEnumMember();
            //SC = new List<btnT> { btnSave, btnCancel };
            //Others = new List<btnT> { btnTop, btnPrior, btnNext, btnBottom, btnAppend, btnModify, btnDelete, btnExit, btnDuplicate };
            Lb = new List<Label> { Lb1, Lb2, Lb3, Lb4, Lb5, Lb6 };
            Txt = new List<Control> { ChNo, CoNo, CoName1, FaNo, FaName1, FaName2, AcNo, AcName, AcName1, ChLine, ChDis, ChDate, ChDate1, ChDate2, ChDate3, ChMny, ChStatus, ChMemo };

            if (Common.User_DateTime == 1) ChDate.MaxLength = ChDate1.MaxLength = ChDate2.MaxLength = ChDate3.MaxLength = 7;
            else ChDate.MaxLength = ChDate1.MaxLength = ChDate2.MaxLength = ChDate3.MaxLength = 8;
            //ChDate.Init();
            //ChDate1.Init();
            //ChDate2.Init();
            //ChDate3.Init();
        }

        private void 應付票據建檔_Load(object sender, EventArgs e)
        {
            //ChMny.NumThousands = true;
            //ChMny.NumLast = Convert.ToInt32(chksys["deci"].ToString());
            //ChMny.NumFirst = (20 - 1 - ChMny.NumLast);

            ChMny.MarkThousand = true;
            ChMny.LastNum = Convert.ToInt32(chksys["deci"].ToString());
            ChMny.FirstNum = 12;
            ChMny.MaxLength = 20;

            //ChLine.BackColor = ChDis.BackColor = Color.FromArgb(215, 227, 239);
            if (!開窗模式)
            {
                //SC.ForEach(r => r.Enabled = false);
                //Others.ForEach(r => r.Enabled = true);
            }
            else
            {
                //SC.ForEach(r => r.Enabled = false);
                btnAppend.Enabled = btnModify.Enabled = btnDelete.Enabled = btnDuplicate.Enabled = false;
            }
            //Txt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = true;
            //    if (r is CheckBox) ((CheckBox)r).Enabled = false;
            //});

            loadM();
            if (li.Count > 0)
                WriteToTxt(li.First());
        }

        void loadM()
        {
            li = tbM.AsEnumerable().OrderBy(r => r["chno"].ToString()).ToList();
        }

        DataRow GetDataRow()
        {
            return li.Find(r => r["ChNo"].ToString() == ChNo.Text.Trim());
        }

        DataRow GetDataRow(string str)
        {
            return li.Find(r => r["ChNo"].ToString() == str.Trim());
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null)
            {
                Txt.ForEach(r =>
                {
                    if (r is TextBox) ((TextBox)r).Text = "";
                    if (r is CheckBox) ((CheckBox)r).Checked = false;
                });
                Lb1.BackColor = Color.Transparent;
            }
            else
            {
                ChNo.Text = dr["ChNo"].ToString();
                CoNo.Text = dr["CoNo"].ToString();
                CoName1.Text = dr["CoName1"].ToString();
                ChMny.Text = dr["ChMny"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                ChStatus.Text = dr["ChStatus"].ToString();
                AcNo.Text = dr["AcNo"].ToString();
                AcName1.Text = dr["AcName1"].ToString();
                AcName.Text = dr["AcName"].ToString();
                FaNo.Text = dr["FaNo"].ToString();
                FaName1.Text = dr["FaName1"].ToString();
                FaName2.Text = dr["FaName2"].ToString();
                ChMemo.Text = dr["ChMemo"].ToString();
                if (Common.User_DateTime == 1)
                {
                    ChDate.Text = dr["ChDate"].ToString();
                    ChDate1.Text = dr["ChDate1"].ToString();
                    ChDate2.Text = dr["ChDate2"].ToString();
                    ChDate3.Text = dr["ChDate3"].ToString();
                }
                else
                {
                    ChDate.Text = dr["ChDate_1"].ToString();
                    ChDate1.Text = dr["ChDate1_1"].ToString();
                    ChDate2.Text = dr["ChDate2_1"].ToString();
                    ChDate3.Text = dr["ChDate3_1"].ToString();
                }
                ChLine.Checked = dr["ChLine"].ToDecimal() == 1 ? true : false;
                ChDis.Checked = dr["ChDis"].ToDecimal() == 1 ? true : false;
                Lb1.BackColor = Color.MistyRose;
                CurrentRow = dr["ChNo"].ToString();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
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
            Txt.ForEach(r =>
            {
                //if (r is TextBox) ((TextBox)r).ReadOnly = false;
                //if (r is TextBox) ((TextBox)r).Text = "";
                if (r is CheckBox)
                {
                    ((CheckBox)r).Enabled = true;
                    if (r.Name == "ChLine")
                    {
                        if (chksys["chkline"].ToDecimal() == 0) ((CheckBox)r).CheckState = CheckState.Unchecked;
                        else ((CheckBox)r).CheckState = CheckState.Checked;
                    }
                    if (r.Name == "ChDis")
                    {
                        if (chksys["chkdis"].ToDecimal() == 0) ((CheckBox)r).CheckState = CheckState.Unchecked;
                        else ((CheckBox)r).CheckState = CheckState.Checked;
                    }
                }
            });
            //CoName1.ReadOnly = FaNo.ReadOnly = FaName1.ReadOnly = AcName1.ReadOnly = ChStatus.ReadOnly = true;
            if (scrit["scsuchk"].ToString() == "2")
                CoNo.ReadOnly = true;
            CoNo.Text = scrit["cono"].ToString();
            取得公司名稱(CoNo.Text.Trim());
            FaNo.Text = 廠商編號;
            取得廠商名稱(FaNo.Text.Trim());
            帶出上次交易帳戶();

            ChDate.Text = ChDate1.Text = ChDate2.Text = ChDate3.Text = Date.GetDateTime(Common.User_DateTime, false);
            ChStatus.Text = "1";
            Lb1.BackColor = Color.Red;
            decimal d = 0;
            ChMny.Text = d.ToString("N" + chksys["deci"].ToString());
            ChNo.Focus();
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
            //Txt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = false;
            //    if (r is CheckBox) ((CheckBox)r).Enabled = true;
            //});

            ChLine.Enabled = ChDis.Enabled = true;

            if (scrit["scsuchk"].ToString() == "2") 
                CoNo.ReadOnly = true;
            //CoName1.ReadOnly = FaNo.ReadOnly = FaName1.ReadOnly = AcName1.ReadOnly = ChStatus.ReadOnly = true;
            ChDate.Text = Date.GetDateTime(Common.User_DateTime);
            ChNo.Text = "";
            ChNo.Focus();
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
            //Txt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = false;
            //    if (r is CheckBox) ((CheckBox)r).Enabled = true;
            //});

            ChLine.Enabled = ChDis.Enabled = true;

            if (scrit["scsuchk"].ToString() == "2") 
                CoNo.ReadOnly = true;
            //CoName1.ReadOnly = FaNo.ReadOnly = FaName1.ReadOnly = AcName1.ReadOnly = ChStatus.ReadOnly = true;
            ChDate.Text = Date.GetDateTime(Common.User_DateTime);
            ChNo.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbM.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("請確定是否刪除此筆記錄?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                try
                {
                    dr = GetDataRow(CurrentRow);
                    int i = li.IndexOf(dr);
                    for (int j = 0; j < tbM.Rows.Count; j++)
                    {
                        if (tbM.Rows[j]["ChNo"].ToString().Trim() == dr["ChNo"].ToString().Trim())
                        {
                            tbM.Rows[j].Delete();
                            tbM.AcceptChanges();
                        }
                    }
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
                catch (Exception ex)
                {
                    MessageBox.Show("DelError:\n" + ex.ToString());
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (AcNo.Text.Trim() == "")
            {
                if (ChStatus.Text.ToDecimal() == 2 || ChStatus.Text.ToDecimal() == 3 || ChStatus.Text.ToDecimal() == 6)
                {
                    MessageBox.Show("『開票帳戶』不可為空，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AcNo.Focus();
                    return;
                }
            }
            if (ChMny.Text.ToDecimal() == 0)
            {
                MessageBox.Show("『票面金額』不可為零，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ChMny.Focus();
                return;
            }
            if (!確定公司可新增(CoNo.Text.Trim(), FaNo.Text.Trim()))
            {
                return;
            }
            if (btnState == "Append" || btnState == "Duplicate")
            {
                if (!自動產生編號())
                {
                    MessageBox.Show("『支票號碼』重複", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ChNo.Focus();
                    return;
                }
                try
                {
                    dr = tbM.NewRow();
                    dr["chno"] = ChNo.Text.Trim();
                    dr["cono"] = CoNo.Text.Trim();
                    dr["coname1"] = CoName1.Text.Trim();
                    dr["fano"] = FaNo.Text.Trim();
                    dr["faname1"] = FaName1.Text.Trim();
                    dr["faname2"] = FaName2.Text.Trim();
                    dr["acno"] = AcNo.Text.Trim();
                    dr["acname"] = AcName.Text.Trim();
                    dr["acname1"] = AcName1.Text.Trim();
                    dr["chline"] = ChLine.Checked == true ? "1" : "0";
                    dr["chdis"] = ChDis.Checked == true ? "1" : "0";
                    dr["chdate"] = Date.ToTWDate(ChDate.Text.ToString());
                    dr["chdate_1"] = Date.ToUSDate(ChDate.Text.ToString());
                    dr["chdate1"] = Date.ToTWDate(ChDate1.Text.ToString());
                    dr["chdate1_1"] = Date.ToUSDate(ChDate1.Text.ToString());
                    dr["chdate2"] = Date.ToTWDate(ChDate2.Text.ToString());
                    dr["chdate2_1"] = Date.ToUSDate(ChDate2.Text.ToString());
                    dr["chdate3"] = Date.ToTWDate(ChDate3.Text.ToString());
                    dr["chdate3_1"] = Date.ToUSDate(ChDate3.Text.ToString());
                    dr["chmny"] = ChMny.Text.ToDecimal();
                    dr["chmemo"] = ChMemo.Text;
                    dr["chstatus"] = "1";
                    dr["chstname"] = "未 處 理";
                    tbM.Rows.Add(dr);
                    tbM.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                CurrentRow = ChNo.Text.Trim();
                btnAppend_Click(null, null);

            }
            if (btnState == "Modify")
            {
                dr = GetDataRow(CurrentRow);
                for (int j = 0; j < tbM.Rows.Count; j++)
                {
                    if (tbM.Rows[j]["ChNo"].ToString().Trim() == dr["ChNo"].ToString().Trim())
                    {
                        tbM.Rows[j].Delete();
                        tbM.AcceptChanges();
                        break;
                    }
                }
                try
                {
                    dr = tbM.NewRow();
                    dr["chno"] = ChNo.Text.Trim();
                    dr["cono"] = CoNo.Text.Trim();
                    dr["coname1"] = CoName1.Text.Trim();
                    dr["fano"] = FaNo.Text.Trim();
                    dr["faname1"] = FaName1.Text.Trim();
                    dr["faname2"] = FaName2.Text.Trim();
                    dr["acno"] = AcNo.Text.Trim();
                    dr["acname"] = AcName.Text.Trim();
                    dr["acname1"] = AcName1.Text.Trim();
                    dr["chline"] = ChLine.Checked == true ? "1" : "0";
                    dr["chdis"] = ChDis.Checked == true ? "1" : "0";
                    dr["chdate"] = Date.ToTWDate(ChDate.Text.ToString());
                    dr["chdate_1"] = Date.ToUSDate(ChDate.Text.ToString());
                    dr["chdate1"] = Date.ToTWDate(ChDate1.Text.ToString());
                    dr["chdate1_1"] = Date.ToUSDate(ChDate1.Text.ToString());
                    dr["chdate2"] = Date.ToTWDate(ChDate2.Text.ToString());
                    dr["chdate2_1"] = Date.ToUSDate(ChDate2.Text.ToString());
                    dr["chdate3"] = Date.ToTWDate(ChDate3.Text.ToString());
                    dr["chdate3_1"] = Date.ToUSDate(ChDate3.Text.ToString());
                    dr["chmny"] = ChMny.Text.ToDecimal();
                    dr["chmemo"] = ChMemo.Text;
                    dr["chstatus"] = "1";
                    dr["chstname"] = "未 處 理";
                    tbM.Rows.Add(dr);
                    tbM.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                CurrentRow = ChNo.Text.Trim();
                Txt.ForEach(r =>
                {
                    if (r is TextBox) ((TextBox)r).Text = "";
                    if (r is CheckBox) ((CheckBox)r).CheckState = CheckState.Unchecked;
                });
                decimal d = 0;
                ChMny.Text = d.ToString("N" + chksys["deci"].ToString());
                ChNo.Focus();
                btnState = "Modify";

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnState = "";
            //SC.ForEach(r => r.Enabled = false);
            //Others.ForEach(r => r.Enabled = true);
            //Txt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = true;
            //    if (r is CheckBox) ((CheckBox)r).Enabled = false;
            //});

            ChLine.Enabled = ChDis.Enabled = false;

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
            if (!開窗模式)
            {
                decimal tot = 0;
                for (int i = 0; i < tbM.Rows.Count; i++)
                {
                    tot += tbM.Rows[i]["ChMny"].ToDecimal();
                }
                pVar.FrmPayabld.CheckMny.Text = tot.ToString("f" + pVar.FrmPayabld.CheckMny.LastNum);
                pVar.FrmPayabld.CHKo = tbM.Copy();
            }
            this.Dispose();
            this.Close();
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

        private void CoNo_DoubleClick(object sender, EventArgs e)
        {
            if (CoNo.ReadOnly) return;

            using (var frm = new 公司開窗())
            {
                frm.TSeekNo = CoNo.Text.Trim();
                frm.票據連線字串 = 票據連線字串;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    CoNo.Text = frm.TResult["cono"].ToString();
                    CoName1.Text = frm.TResult["CoName1"].ToString();
                }
            }
        }

        private void CoNo_Validating(object sender, CancelEventArgs e)
        {
            if (CoNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CoNo.Text.Trim() == "")
            {
                MessageBox.Show("『公司編號』不可為空，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            try
            {
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("cono", CoNo.Text.Trim());
                    cmd.CommandText = "select * from comp where cono not in ('df') and copaths != '' and cono=@cono";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        CoNo.Text = reader["CoNo"].ToString();
                        CoName1.Text = reader["CoName1"].ToString();
                    }
                    else
                    {
                        e.Cancel = true;
                        using (var frm = new 公司開窗())
                        {
                            frm.TSeekNo = CoNo.Text.Trim();
                            frm.票據連線字串 = 票據連線字串;
                            frm.ShowDialog();
                            if (frm.DialogResult == DialogResult.OK)
                            {
                                CoNo.Text = frm.TResult["cono"].ToString();
                                CoName1.Text = frm.TResult["CoName1"].ToString();
                            }
                        }
                    }
                    reader.Close(); reader.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AcNo_DoubleClick(object sender, EventArgs e)
        {
            if (AcNo.ReadOnly) return;
            using (var frm = new 銀行帳號建檔_瀏覽())
            { 
                frm.chksys = chksys;
                frm.scrit = scrit;
                frm.票據連線字串 = 票據連線字串; 
                frm.開窗模式 = true;
                frm.去除外幣帳戶 = true;
                frm.CoNo = CoNo.Text.Trim();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    if (frm.TResult != null)
                    {
                        AcNo.Text = frm.TResult["AcNo"].ToString();
                        AcName.Text = frm.TResult["AcName"].ToString();
                        AcName1.Text = frm.TResult["AcName1"].ToString();
                    }
                    else
                    {
                        AcNo.Text = "";
                        AcName.Text = "";
                        AcName1.Text = "";
                    }
                }
            }
        }

        private void AcNo_Validating(object sender, CancelEventArgs e)
        {
            if (AcNo.ReadOnly || btnCancel.Focused) return;
            if (AcNo.Text.Trim() == "") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("acno", AcNo.Text.Trim());
                    cmd.Parameters.AddWithValue("cono", CoNo.Text.Trim());
                    cmd.CommandText = "select * from acct where acno=@acno and cono=@cono and ackind=1";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        AcNo.Text = reader["AcNo"].ToString();
                        AcName.Text = reader["AcName"].ToString();
                        AcName1.Text = reader["AcName1"].ToString();
                        reader.Dispose(); reader.Close();
                    }
                    else
                    {
                        reader.Dispose(); reader.Close();
                        e.Cancel = true;
                        using (var frm = new 銀行帳號建檔_瀏覽())
                        { 
                            frm.chksys = chksys;
                            frm.scrit = scrit;
                            frm.票據連線字串 = 票據連線字串; 
                            frm.開窗模式 = true;
                            frm.去除外幣帳戶 = true;
                            frm.CoNo = CoNo.Text.Trim();
                            frm.ShowDialog();
                            if (frm.DialogResult == DialogResult.OK)
                            {
                                if (frm.TResult != null)
                                {
                                    AcNo.Text = frm.TResult["AcNo"].ToString();
                                    AcName.Text = frm.TResult["AcName"].ToString();
                                    AcName1.Text = frm.TResult["AcName1"].ToString();
                                }
                                else
                                {
                                    AcNo.Text = "";
                                    AcName.Text = "";
                                    AcName1.Text = "";
                                }
                            }
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ChDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            TextBox tb = (TextBox)sender;
            if (tb.ReadOnly) return;
            if (tb.Text.Trim() == "")
            {
                MessageBox.Show("『日期』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (!tb.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (!稽核會計年度(tb.Text.Trim()))
            {
                e.Cancel = true;
                return;
            }
            if (tb.Name == "ChDate1")
            {
                if (ChDate1.Text.ToDecimal() > ChDate2.Text.ToDecimal())
                    ChDate2.Text = ChDate1.Text;
                if (ChDate1.Text.ToDecimal() > ChDate3.Text.ToDecimal())
                    ChDate3.Text = ChDate1.Text;
            }
            if (tb.Name == "ChDate2")
            {
                if (ChDate2.Text.ToDecimal() > ChDate3.Text.ToDecimal())
                    ChDate3.Text = ChDate2.Text;
            }
        }

        private void ChMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(ChMemo, 50);
        }

        private void ChNo_Validating(object sender, CancelEventArgs e)
        {
            if (ChNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            DataTable chno = new DataTable();
            using (SqlConnection cn = new SqlConnection(票據連線字串))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("chno", ChNo.Text.Trim());
                    cmd.CommandText = "select chno from chko where chno=@chno";
                    SqlDataAdapter dd = new SqlDataAdapter(cmd);
                    dd.Fill(chno);
                    dd.Dispose();
                }
            }

            if (btnState == "Append")
            {
                loadM();
                dr = GetDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    e.Cancel = true;
                    ChNo.Text = "";
                    MessageBox.Show("此支票編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (chno.AsEnumerable().ToList().Find(r => r["chno"].ToString() == ChNo.Text.Trim()) != null)
                {
                    e.Cancel = true;
                    ChNo.Text = "";
                    MessageBox.Show("此支票編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
                    ChNo.Text = "";
                    MessageBox.Show("此支票編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (chno.AsEnumerable().ToList().Find(r => r["chno"].ToString() == ChNo.Text.Trim()) != null)
                {
                    e.Cancel = true;
                    ChNo.Text = "";
                    MessageBox.Show("此支票編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Modify")
            {
                loadM();
                dr = GetDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    if (ChNo.Text.Trim() != BeforeText)
                    {
                        WriteToTxt(dr);
                        ChDate.Text = Date.GetDateTime(Common.User_DateTime);
                        CoNo.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("無此支票編號，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }

            }
        }

        private void ChNo_Enter(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb.ReadOnly) return;
            BeforeText = tb.Text.Trim();
        }

        void 帶出上次交易帳戶()
        {
            if (FaNo.Text.Trim() != "")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(票據連線字串))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.CommandText = "select acno,acname1,acname from chko where fano=@fano order by ChDate1 desc";
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows && reader.Read())
                        {
                            AcNo.Text = reader["AcNo"].ToString();
                            AcName.Text = reader["AcName"].ToString();
                            AcName1.Text = reader["AcName1"].ToString();
                        }
                        reader.Dispose(); reader.Close();
                        cmd.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        bool 刪除之前帳戶資料(DataRow Row)
        {
            try
            {
                if (dr["chstatus"].ToDecimal() == 3)
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("chmny", dr["ChMny"].ToDecimal());
                        cmd.Parameters.AddWithValue("acno", dr["AcNo"].ToString().Trim());
                        cmd.CommandText = "update acct set acmny1+=@chmny where acno=@acno";
                        cmd.ExecuteNonQuery();

                        cmd.Cancel(); cmd.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        bool 新增目前帳戶資料()
        {
            try
            {
                if (ChStatus.Text.Trim() == "3")
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("chmny", ChMny.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("acno", AcNo.Text.ToString().Trim());
                        cmd.CommandText = "update acct set acmny1-=@chmny where acno=@acno";
                        cmd.ExecuteNonQuery();

                        cmd.Cancel(); cmd.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        bool 自動產生編號()
        {
            DataTable table = new DataTable();
            string date = "", count = "1";
            if (chksys["noadd"].ToDecimal() == 1)//民國年月日+流水號
            {
                date = Date.GetDateTime(1);
                count = count.PadLeft(3, '0');
            }
            else if (chksys["noadd"].ToDecimal() == 2)//民國年月+流水號
            {
                date = Date.GetDateTime(1);
                date = date.Substring(0, 5);
                count = count.PadLeft(5, '0');
            }
            else if (chksys["noadd"].ToDecimal() == 3)//西元年月日+流水號
            {
                date = Date.GetDateTime(2);
                count = count.PadLeft(2, '0');
            }
            else
            {
                date = Date.GetDateTime(2);
                date = date.Substring(0, 6);
                count = count.PadLeft(4, '0');
            }
            decimal No = (date + count).ToDecimal();
            try
            {
                if (ChNo.Text.Trim() == "")
                {
                    using (SqlConnection cn = new SqlConnection(票據連線字串))
                    {
                        cn.Open();
                        string sql = "";
                        if (chksys["noadd"].ToDecimal() == 1)
                            sql = "select chno from chko where left(chno,7)='" + Date.GetDateTime(1) + "' order by chno desc";
                        else if (chksys["noadd"].ToDecimal() == 2)
                            sql = "select chno from chko where left(chno,5)='" + Date.GetDateTime(1).Substring(0, 5) + "' order by chno desc";
                        else if (chksys["noadd"].ToDecimal() == 3)
                            sql = "select chno from chko where left(chno,8)='" + Date.GetDateTime(2) + "' order by chno desc";
                        else
                            sql = "select chno from chko where left(chno,6)='" + Date.GetDateTime(2).Substring(0, 6) + "' order by chno desc";
                        SqlDataAdapter dd = new SqlDataAdapter(sql, cn);
                        dd.Fill(table);
                        table.Merge(tbM);
                        if (table.Rows.Count > 0)
                        {
                            table = table.AsEnumerable().OrderByDescending(r => r["chno"].ToString()).CopyToDataTable();
                            No = table.Rows[0]["chno"].ToDecimal() + 1;
                            ChNo.Text = No.ToString().Trim();
                            return true;
                        }
                        else
                        {
                            ChNo.Text = No.ToString().Trim();
                            return true;
                        }
                    }
                }
                else
                {
                    using (SqlConnection cn = new SqlConnection(票據連線字串))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("chno", ChNo.Text.Trim());
                        cmd.CommandText = "select chno from chko where chno=@chno";
                        if (!cmd.ExecuteScalar().IsNullOrEmpty()) return true;
                        if (li.Find(r => r["chno"].ToString().Trim() == ChNo.Text.Trim()) != null) return false;

                        cmd.Dispose();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        void 取得公司名稱(string cono)
        {
            using (SqlConnection cn = new SqlConnection(票據連線字串))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("cono", cono);
                cmd.CommandText = "select coname1 from comp where cono=@cono";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                {
                    CoName1.Text = cmd.ExecuteScalar().ToString().Trim();
                }
                cmd.Dispose();
            }
        }
        void 取得廠商名稱(string fano)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("fano", fano);
                cmd.CommandText = "select * from fact where fano=@fano";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    FaName1.Text = reader["FaName1"].ToString();
                    if (reader["FaChkName"].ToString().Trim() == "")
                        FaName2.Text = reader["FaName2"].ToString();
                    else
                        FaName2.Text = reader["FaChkName"].ToString();
                }
                cmd.Dispose();
            }
        }
        bool 稽核會計年度(string str)
        {
            if (str.Length == 7)
            {
                if (str.Substring(0, 3).ToDecimal() < chksys["chkyear1"].ToDecimal())
                {
                    MessageBox.Show("輸入日期不可小於『系統票據年度』", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                    return true;
            }
            else if (str.Length == 8)
            {
                if (str.Substring(0, 4).ToDecimal() < chksys["chkyear2"].ToDecimal())
                {
                    MessageBox.Show("輸入日期不可小於『系統票據年度』", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                    return true;
            }
            return false;
        }
        bool 確定公司可新增(string cono, string fano)
        {
            try
            {
                string path = "";
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("cono", CoNo.Text.Trim());
                    cmd.CommandText = "select copaths from comp where cono=@cono";
                    if (cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        MessageBox.Show("此公司沒跟進銷存系統連結，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CoNo.Focus();
                        return false;
                    }
                    else
                    {
                        path = cmd.ExecuteScalar().ToString();
                    }
                    cmd.Dispose();
                }
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString.Replace("Initial Catalog=" + Common.logOnInfo.ConnectionInfo.DatabaseName, "Initial Catalog=" + path)))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("fano", fano);
                    cmd.CommandText = "select fano from fact where fano=@fano";
                    if (cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        MessageBox.Show("此公司沒有此廠商，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FaNo.Focus();
                        return false;
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

         
    }
}
