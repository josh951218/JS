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
    public partial class 應收票據建檔 : Formbase
    {
        List<TextBoxbase> list;

        public DataTable tbM = new DataTable();
        public DataRow chksys;
        public DataRow scrit;
        public string 票據連線字串 = "";
        public string 客戶編號 = "";
        public bool 開窗模式 = false;
        List<DataRow> li = new List<DataRow>();
         
        List<Control> UnderTxt;
        List<TextBoxNumberT> NumTxt;
        DataRow dr;

        bool 是否驗證 = false;
        string btnState = "";
        string CurrentRow = "";
        string BeforeText = "";

        public 應收票據建檔()
        {
            InitializeComponent();
            this.list = this.getEnumMember();
            //SC = new List<btnT> { btnSave, btnCancel };
            //Others = new List<btnT> { btnTop, btnPrior, btnNext, btnBottom, btnAppend, btnModify, btnDelete, btnExit, btnDuplicate };
            UnderTxt = new List<Control> { ChNo, BaNo, BaName, ChAct, ChLine, ChDis, ChDate1, ChDate2, ChDate3, ChMny, ChDate, ChMemo, CoNo, CuName2 };
            NumTxt = new List<TextBoxNumberT> { ChMny, ChOMny1, ChOMny2, ChOMny3, ChTMny1, ChTMny2, ChTMny3 };
            if (Common.User_DateTime == 1) ChDate.MaxLength = ChDate1.MaxLength = ChDate2.MaxLength = ChDate3.MaxLength = ChTDate1.MaxLength = 7;
            else ChDate.MaxLength = ChDate1.MaxLength = ChDate2.MaxLength = ChDate3.MaxLength = ChTDate1.MaxLength = 8;
            //ChDate.Init();
            //ChDate1.Init();
            //ChDate2.Init();
            //ChDate3.Init();
            //ChTDate1.Init();
        }

        private void 應收票據建檔_Load(object sender, EventArgs e)
        {
            NumTxt.ForEach(r =>
            {
                r.MarkThousand = true;
                r.LastNum = Convert.ToInt32(chksys["deci"].ToString());
                r.FirstNum = 12;
                r.MaxLength = 20;
                //r.NumThousands = true;
                //r.NumLast = Convert.ToInt32(chksys["deci"].ToString());
                //r.NumFirst = (20 - 1 - r.NumLast);
            });
            //groupBoxT1.BackColor = groupBoxT2.BackColor = Color.FromArgb(215, 227, 239);
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
            //UnderTxt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = true;
            //    if (r is CheckBox) ((CheckBox)r).Enabled = false;
            //});
            if (scrit["scsuchk"].ToString() == "2") CoNo.Enabled = false;
            loadM();
            if (li.Count > 0)
                WriteToTxt(li.First());
        }

        DataRow GetDataRow()
        {
            return li.Find(r => r["ChNo"].ToString() == ChNo.Text.Trim());
        }

        DataRow GetDataRow(string str)
        {
            return li.Find(r => r["ChNo"].ToString() == str.Trim());
        }

        void loadM()
        {
            li = tbM.AsEnumerable().OrderBy(r => r["chno"].ToString()).ToList();
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null)
            {
                UnderTxt.ForEach(r =>
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
                CuNo.Text = dr["CuNo"].ToString();
                CuName1.Text = dr["CuName1"].ToString();
                CuName2.Text = dr["CuName2"].ToString();
                BaNo.Text = dr["BaNo"].ToString();
                BaName.Text = dr["BaName"].ToString();
                ChAct.Text = dr["ChAct"].ToString();
                ChMny.Text = dr["ChMny"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                ChStatus.Text = dr["ChStatus"].ToString();
                AcNo.Text = dr["AcNo"].ToString();
                AcName1.Text = dr["AcName1"].ToString();
                FaNo.Text = dr["FaNo"].ToString();
                FaName1.Text = dr["FaName1"].ToString();
                FaName2.Text = dr["FaName2"].ToString();
                ChOMny1.Text = dr["ChOMny1"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                ChOMny2.Text = dr["ChOMny2"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                ChOMny3.Text = dr["ChOMny3"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                ChTMny1.Text = dr["ChTMny1"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                ChTMny2.Text = dr["ChTMny2"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                ChTMny3.Text = dr["ChTMny3"].ToDecimal().ToString("N" + chksys["deci"].ToString());
                TacNo.Text = dr["TacNo"].ToString();
                TacName1.Text = dr["TacName1"].ToString();
                ChMemo.Text = dr["ChMemo"].ToString();
                if (Common.User_DateTime == 1)
                {
                    ChDate.Text = dr["ChDate"].ToString();
                    ChDate1.Text = dr["ChDate1"].ToString();
                    ChDate2.Text = dr["ChDate2"].ToString();
                    ChDate3.Text = dr["ChDate3"].ToString();
                    ChTDate1.Text = dr["ChTDate1"].ToString();
                }
                else
                {
                    ChDate.Text = dr["ChDate_1"].ToString();
                    ChDate1.Text = dr["ChDate1_1"].ToString();
                    ChDate2.Text = dr["ChDate2_1"].ToString();
                    ChDate3.Text = dr["ChDate3_1"].ToString();
                    ChTDate1.Text = dr["ChTDate1_1"].ToString();
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
            //UnderTxt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = false;
            //    if (r is TextBox) ((TextBox)r).Text = "";
            //    if (r is CheckBox) ((CheckBox)r).Enabled = true;
            //    if (r is CheckBox) ((CheckBox)r).CheckState = CheckState.Unchecked;
            //});

            ChLine.Enabled = ChDis.Enabled = true;

            if (scrit["scsuchk"].ToString() == "2") 
                CoNo.ReadOnly = true;

            CoNo.Text = scrit["cono"].ToString();
            取得公司名稱(CoNo.Text.Trim());
            CuNo.Text = 客戶編號;
            取得客戶名稱(CuNo.Text.Trim());
            帶出上次交易帳戶();

            ChDate.Text = ChDate1.Text = ChDate2.Text = ChDate3.Text = Date.GetDateTime(Common.User_DateTime, false);
            ChStatus.Text = "1";
            Lb1.BackColor = Color.MistyRose;
            decimal d = 0;
            ChMny.Text = ChOMny1.Text = ChOMny2.Text = ChOMny3.Text = ChTMny1.Text = ChTMny2.Text = ChTMny3.Text = d.ToString("N" + chksys["deci"].ToString());
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
            //UnderTxt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = false;
            //    if (r is CheckBox) ((CheckBox)r).Enabled = true;
            //});

            ChLine.Enabled = ChDis.Enabled = true;

            if (scrit["scsuchk"].ToString() == "2") 
                CoNo.ReadOnly = true;

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
            //UnderTxt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = false;
            //    if (r is CheckBox) ((CheckBox)r).Enabled = true;
            //});

            ChLine.Enabled = ChDis.Enabled = true;

            if (scrit["scsuchk"].ToString() == "2") 
                CoNo.ReadOnly = true;

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
            if (ChNo.Text.Trim() == "")
            {
                MessageBox.Show("『支票號碼』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ChNo.Focus();
                return;
            }
            if (BaNo.Text.Trim() == "")
            {
                MessageBox.Show("『付款銀行』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BaNo.Focus();
                return;
            }
            if (ChMny.Text.ToDecimal() == 0)
            {
                MessageBox.Show("『票面金額』不可為零，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ChMny.Focus();
                return;
            }
            if (!確定公司可新增(CoNo.Text.Trim(), CuNo.Text.Trim()))
            {
                return;
            }
            if (btnState == "Append" || btnState == "Duplicate")
            {

                try
                {
                    dr = tbM.NewRow();
                    dr["chno"] = ChNo.Text.Trim();
                    dr["cono"] = CoNo.Text.Trim();
                    dr["coname1"] = CoName1.Text.Trim();
                    dr["cuno"] = CuNo.Text.Trim();
                    dr["cuname1"] = CuName1.Text.Trim();
                    dr["cuname2"] = CuName2.Text.Trim();
                    dr["bano"] = BaNo.Text.Trim();
                    dr["baname"] = BaName.Text.Trim();
                    dr["chact"] = ChAct.Text.Trim();
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
                ChNo.Text = ChMemo.Text = "";
                decimal d = 0;
                ChMny.Text = d.ToString("N" + chksys["deci"].ToString());
                ChDate.Text = ChDate1.Text = ChDate2.Text = ChDate3.Text = Date.GetDateTime(Common.User_DateTime, false);
                ChNo.Focus();
            }
            if (btnState == "Modify")
            {
                try
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

                    dr = tbM.NewRow();
                    dr["chno"] = ChNo.Text.Trim();
                    dr["cono"] = CoNo.Text.Trim();
                    dr["coname1"] = CoName1.Text.Trim();
                    dr["cuno"] = CuNo.Text.Trim();
                    dr["cuname1"] = CuName1.Text.Trim();
                    dr["cuname2"] = CuName2.Text.Trim();
                    dr["bano"] = BaNo.Text.Trim();
                    dr["baname"] = BaName.Text.Trim();
                    dr["chact"] = ChAct.Text.Trim();
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
                UnderTxt.ForEach(r =>
                {
                    if (r is TextBox) ((TextBox)r).ReadOnly = false;
                    if (r is TextBox) ((TextBox)r).Text = "";
                    if (r is CheckBox) ((CheckBox)r).Checked = false;
                });
                decimal d = 0;
                ChMny.Text = d.ToString("N" + chksys["deci"].ToString());
                ChNo.Focus();
                btnState = "Modify";

            }
            btnCancel_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnState = "";
            //SC.ForEach(r => r.Enabled = false);
            //Others.ForEach(r => r.Enabled = true);
            //UnderTxt.ForEach(r =>
            //{
            //    if (r is TextBox) ((TextBox)r).ReadOnly = false;
            //    if (r is TextBoxT) ((TextBoxT)r).GrayView = false;
            //    if (r is txtNumber) ((txtNumber)r).GrayView = false;
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
                pVar.FrmReceivd.CheckMny.Text = tot.ToString("f" + pVar.FrmReceivd.CheckMny.LastNum);
                pVar.FrmReceivd.CHKi = tbM.Copy();
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

        private void BaNo_DoubleClick(object sender, EventArgs e)
        {
            if (BaNo.ReadOnly) return;

            using (var frm = new 全省銀行建檔_瀏覽())
            { 
                frm.SeekNo = BaNo.Text.Trim();
                frm.CanAppend = true;
                frm.開窗模式 = true;
                frm.票據連線字串 = 票據連線字串;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    BaNo.Text = frm.Result["BaNo"].ToString();
                    BaName.Text = frm.Result["BaName"].ToString();
                }
            }
        }

        private void BaNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused || BaNo.ReadOnly) return;
            if (BaNo.Text.Trim() == "")
            {
                MessageBox.Show("『付款銀行』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (BaNo.Text.Trim() == BeforeText && !是否驗證) return;
            using (SqlConnection cn = new SqlConnection(票據連線字串))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("bano", BaNo.Text.Trim());
                cmd.CommandText = "select * from bank where bano=@bano";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    是否驗證 = false;
                    BaNo.Text = reader["BaNo"].ToString();
                    BaName.Text = reader["BaName"].ToString();
                }
                else
                {
                    是否驗證 = true;
                    e.Cancel = true;
                    using (var frm = new 全省銀行建檔_瀏覽())
                    { 
                        frm.SeekNo = BaNo.Text.Trim();
                        frm.CanAppend = true;
                        frm.開窗模式 = true;
                        frm.票據連線字串 = 票據連線字串;
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            BaNo.Text = frm.Result["BaNo"].ToString();
                            BaName.Text = frm.Result["BaName"].ToString();
                        }
                    }
                }
            }
        }

        private void ChDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;

            TextBox tb = (TextBox)sender;
            if (tb.ReadOnly) return;
            if (tb.Text.Trim() == "")
            {
                if (tb.Name == "ChTDate1") return;
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

            if (ChNo.Text.Trim() == "")
            {
                e.Cancel = true;
                ChNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable chno = new DataTable();
            using (SqlConnection cn = new SqlConnection(票據連線字串))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("chno", ChNo.Text.Trim());
                    cmd.CommandText = "select chno from chki where chno=@chno";
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
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BaNo_Enter(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb.ReadOnly) 
                return;
             
            BeforeText = tb.Text.Trim();
        }

        void 帶出上次交易帳戶()
        {
            if (CuNo.Text.Trim() != "")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(票據連線字串))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.CommandText = "select BaNo,BaName,ChAct from chki where cuno=@cuno order by ChDate1 desc";
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows && reader.Read())
                        {
                            BaNo.Text = reader["BaNo"].ToString();
                            BaName.Text = reader["BaName"].ToString();
                            ChAct.Text = reader["ChAct"].ToString();
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
                        cmd.CommandText = "update acct set acmny1-=@chmny where acno=@acno";
                        cmd.ExecuteNonQuery();
                    }
                }
                //if (dr["chstatus"].ToDecimal() == 5)
                //{
                //    using (SqlConnection cn = new SqlConnection(Common.浮動連線字串))
                //    {
                //        cn.Open();
                //        SqlCommand cmd = cn.CreateCommand();
                //        cmd.CommandText = "update fact set FaPayAmt-='" + dr["ChOMny3"].ToDecimal() + "' where FaNo='" + dr["FaNo"].ToString().Trim() + "'";
                //        cmd.ExecuteNonQuery();
                //    }
                //}
                if (dr["chstatus"].ToDecimal() == 6)
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("chtmny1", dr["ChTMny1"].ToDecimal());
                        cmd.Parameters.AddWithValue("chtmny3", dr["ChTMny3"].ToDecimal());
                        cmd.Parameters.AddWithValue("acno", dr["AcNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("tacno", dr["TacNo"].ToString().Trim());
                        cmd.CommandText = "update acct set acmny1-=@chtmny1 where acno=@acno;";
                        cmd.CommandText += "update acct set acmny1-=@chtmny3 where acno=@tacno;";
                        cmd.ExecuteNonQuery();
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
                        cmd.CommandText = "update acct set acmny1+=@chmny where acno=@acno";
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
                    }
                }
                //if (ChStatus.Text.Trim() == "5")
                //{
                //    using (SqlConnection cn = new SqlConnection(Common.浮動連線字串))
                //    {
                //        cn.Open();
                //        SqlCommand cmd = cn.CreateCommand();
                //        cmd.CommandText = "update fact set FaPayAmt+='" + ChOMny3.Text.ToDecimal() + "' where FaNo='" + FaNo.Text.ToString().Trim() + "'";
                //        cmd.ExecuteNonQuery();
                //    }
                //}
                if (ChStatus.Text.Trim() == "6")
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("chtmny1", ChTMny1.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("chtmny3", ChTMny3.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("acno", AcNo.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("tacno", TacNo.Text.ToString().Trim());

                        cmd.CommandText = "update acct set acmny1+=@chtmny1 where acno=@acno;";
                        cmd.CommandText += "update acct set acmny1+=@chtmny3 where acno=@tacno;";
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
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
        void 取得客戶名稱(string cuno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("cuno", cuno);
                cmd.CommandText = "select * from cust where cuno=@cuno";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    CuName1.Text = reader["CuName1"].ToString();
                    CuName2.Text = reader["CuName2"].ToString();
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
        bool 確定公司可新增(string cono, string cuno)
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
                    cmd.Parameters.AddWithValue("cuno", cuno);
                    cmd.CommandText = "select cuno from cust where cuno=@cuno";
                    if (cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        MessageBox.Show("此公司沒有此客戶，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CuNo.Focus();
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
