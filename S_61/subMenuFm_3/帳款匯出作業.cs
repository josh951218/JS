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
    public partial class 帳款匯出作業 : Formbase
    {
        List<TextBoxbase> list;
        public DataTable tbM = new DataTable();
        public DataRow chksys;
        public DataRow scrit;
        public string 票據連線字串 = "";
        public string 廠商編號 = "";
        public bool 開窗模式 = false;
        List<DataRow> li = new List<DataRow>();

        List<TextBox> Txt;
        DataRow dr;
        string btnState = "";
        string tempNo = "";
        string BeforeText;

        public 帳款匯出作業()
        {
            InitializeComponent();
            this.list = this.getEnumMember();

            Txt = new List<TextBox> { ReNo, CoNo, CoName1, CoName2, ReDate, AcNo, AcName, AcName1, Xa1Name, ReMny, ReMemo, FaNo, FaName1, FaName2, ChargeMny, };
            ChargeMny.MarkThousand = ReMny.MarkThousand = true;

            //ReMny.NumNegative = true;
            //ChargeMny.NumNegative = true;
        }

        private void 帳款匯出作業_Load(object sender, EventArgs e)
        {
            ChargeMny.LastNum = ReMny.LastNum = Convert.ToInt32(chksys["deci"].ToString());
            ChargeMny.FirstNum = ReMny.FirstNum = 12;
            ChargeMny.MaxLength = ReMny.MaxLength = 20;

            SurCharge.BackColor = Color.FromArgb(215, 227, 239);
            if (Common.User_DateTime == 1) ReDate.MaxLength = 7;
            else ReDate.MaxLength = 8;

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
            if (scrit["scsuchk"].ToString() == "2") CoNo.Enabled = false;
            loadM();
            if (li.Count > 0)
                WriteToTxt(li.First());
        }

        void loadM()
        {
            li = tbM.AsEnumerable().OrderBy(r => r["reno"].ToString()).ToList();
        }

        DataRow GetDataRow()
        {
            return li.Find(r => r["ReNo"].ToString() == ReNo.Text.Trim());
        }

        DataRow GetDataRow(string str)
        {
            return li.Find(r => r["ReNo"].ToString() == str.Trim());
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
                    if (r is TextBox)
                    {
                        if (r.Name.ToString() == "ReMny" || r.Name.ToString() == "ChargeMny")
                            r.Text = dr[r.Name.ToString()].ToDecimal().ToString("N" + chksys["deci"].ToString());
                        else
                        {
                            if (r.Name == "ReDate")
                                r.Text = Common.User_DateTime == 1 ? dr["ReDate"].ToString() : dr["ReDate_1"].ToString();
                            else
                                r.Text = dr[r.Name.ToString()].ToString();
                        }
                    }
                });
                SurCharge.Checked = dr["SurCharge"].ToDecimal() == 1 ? true : false;
                tempNo = dr["ReNo"].ToString();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            if (li.Count > 0)
            {
                dr = li.First();
                WriteToTxt(dr);
            } 
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            dr = GetDataRow();
            int temp = li.IndexOf(dr);
            if (li.Count > 0)
            {
                dr = GetDataRow(tempNo);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (temp == 0)
                    {
                        MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        WriteToTxt(li.First()); 
                    }
                    else
                    {
                        WriteToTxt(li[--temp]); 
                    }
                }
                if (i > 0)
                {
                    WriteToTxt(li[--i]); 
                }
                if (i == 0)
                {
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WriteToTxt(li.First()); 
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
                dr = GetDataRow(tempNo);
                int i = li.LastIndexOf(dr);
                if (i == -1)
                {
                    if (temp >= li.Count - 1)
                    {
                        MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        WriteToTxt(li.Last()); 
                    }
                    else
                    {
                        WriteToTxt(li[++temp]); 
                    }
                }
                if (i < li.Count - 1)
                {
                    WriteToTxt(li[++i]); 
                }
                else
                {
                    MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WriteToTxt(li.Last()); 
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
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            btnState = "Append";
            //SC.ForEach(r => r.Enabled = true);
            //Others.ForEach(r => r.Enabled = false);
            Txt.ForEach(r =>
            {
                if (r is TextBox)
                {
                    r.Text = "";
                    if (r.Name.ToString() != "CoName1")
                        if (r.Name.ToString() != "AcName")
                            if (r.Name.ToString() != "Xa1Name")
                                if (r.Name.ToString() != "FaName2")
                                    if (r.Name.ToString() != "FaNo")
                                        ((TextBox)r).ReadOnly = false;
                    if (r is TextBoxNumberT) r.Text = "0";
                }
            });
            SurCharge.Checked = true;
            SurCharge.Enabled = true;

            CoNo.Text = scrit["cono"].ToString();
            取得公司名稱(CoNo.Text.Trim());
            FaNo.Text = 廠商編號;
            取得廠商名稱(FaNo.Text.Trim());

            ReDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            ReNo.Focus();
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
            Txt.ForEach(r =>
            {
                if (r is TextBox)
                {
                    if (r.Name.ToString() != "CoName1")
                        if (r.Name.ToString() != "AcName")
                            if (r.Name.ToString() != "Xa1Name")
                                if (r.Name.ToString() != "FaName2")
                                    if (r.Name.ToString() != "FaNo")
                                        ((TextBox)r).ReadOnly = false;
                }
            });
            ChargeMny.ReadOnly = SurCharge.Checked == true ? false : true;
            SurCharge.Enabled = true;
            ReDate.Text = Date.GetDateTime(Common.User_DateTime);
            ReNo.Text = "";
            ReNo.Focus();
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
            Txt.ForEach(r =>
            {
                if (r is TextBox)
                {
                    if (r.Name.ToString() != "CoName1")
                        if (r.Name.ToString() != "AcName")
                            if (r.Name.ToString() != "Xa1Name")
                                if (r.Name.ToString() != "FaName2")
                                    if (r.Name.ToString() != "FaNo")
                                        ((TextBox)r).ReadOnly = false;
                }
            });
            ChargeMny.ReadOnly = SurCharge.Checked == true ? false : true;
            SurCharge.Enabled = true;
            CoName1.ReadOnly = AcName.ReadOnly = true;
            ReDate.Text = Date.GetDateTime(Common.User_DateTime);
            ReNo.Focus();
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
                    dr = GetDataRow(tempNo);
                    int i = li.IndexOf(dr);
                    for (int j = 0; j < tbM.Rows.Count; j++)
                    {
                        if (tbM.Rows[j]["ReNo"].ToString().Trim() == dr["ReNo"].ToString().Trim())
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
                MessageBox.Show("『帳戶編號』不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AcNo.Focus();
                return;
            }
            if (FaNo.Text.Trim() == "")
            {
                MessageBox.Show("『廠商編號』不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }
            if (ReMny.Text.ToDecimal() == 0)
            {
                MessageBox.Show("『金額』不可為零", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ReMny.Focus();
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
                    MessageBox.Show("『匯入證號』重複", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ReNo.Focus();
                    return;
                }
                try
                {
                    dr = tbM.NewRow();
                    dr["reno"] = ReNo.Text.Trim();
                    dr["cono"] = CoNo.Text.Trim();
                    dr["coname1"] = CoName1.Text.Trim();
                    dr["coname2"] = CoName2.Text.Trim();
                    dr["redate"] = Date.ToTWDate(ReDate.Text.Trim());
                    dr["redate_1"] = Date.ToUSDate(ReDate.Text.Trim());
                    dr["fano"] = FaNo.Text.Trim();
                    dr["faname1"] = FaName1.Text.Trim();
                    dr["faname2"] = FaName2.Text.Trim();
                    dr["acno"] = AcNo.Text.Trim();
                    dr["acname"] = AcName.Text.Trim();
                    dr["acname1"] = AcName1.Text.Trim();
                    dr["xa1name"] = Xa1Name.Text.Trim();
                    dr["remny"] = ReMny.Text.Trim();
                    dr["rememo"] = ReMemo.Text.Trim();
                    dr["chargemny"] = ChargeMny.Text.Trim();
                    dr["surcharge"] = SurCharge.Checked ? "1" : "0";
                    tbM.Rows.Add(dr);
                    tbM.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                tempNo = ReNo.Text.Trim();
                btnAppend_Click(null, null);
            }
            if (btnState == "Modify")
            {
                try
                {
                    dr = GetDataRow(tempNo);
                    for (int j = 0; j < tbM.Rows.Count; j++)
                    {
                        if (tbM.Rows[j]["ReNo"].ToString().Trim() == dr["ReNo"].ToString().Trim())
                        {
                            tbM.Rows[j].Delete();
                            tbM.AcceptChanges();
                            break;
                        }
                    }
                    dr = tbM.NewRow();
                    dr["reno"] = ReNo.Text.Trim();
                    dr["cono"] = CoNo.Text.Trim();
                    dr["coname1"] = CoName1.Text.Trim();
                    dr["coname2"] = CoName2.Text.Trim();
                    dr["redate"] = Date.ToTWDate(ReDate.Text.Trim());
                    dr["redate_1"] = Date.ToUSDate(ReDate.Text.Trim());
                    dr["fano"] = FaNo.Text.Trim();
                    dr["faname1"] = FaName1.Text.Trim();
                    dr["faname2"] = FaName2.Text.Trim();
                    dr["acno"] = AcNo.Text.Trim();
                    dr["acname"] = AcName.Text.Trim();
                    dr["acname1"] = AcName1.Text.Trim();
                    dr["xa1name"] = Xa1Name.Text.Trim();
                    dr["remny"] = ReMny.Text.Trim();
                    dr["rememo"] = ReMemo.Text.Trim();
                    dr["chargemny"] = ChargeMny.Text.Trim();
                    dr["surcharge"] = SurCharge.Checked ? "1" : "0";
                    tbM.Rows.Add(dr);
                    tbM.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                tempNo = ReNo.Text.Trim();
                Txt.ForEach(r =>
                {
                    if (r is TextBox) ((TextBox)r).Text = "";
                    if (r is TextBoxNumberT) ((TextBox)r).Text = "0";
                });
                ReNo.Focus();
                btnState = "Modify";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnState = "";
            //SC.ForEach(r => r.Enabled = false);
            //Others.ForEach(r => r.Enabled = true);
            SurCharge.Enabled = false;
            Txt.ForEach(r =>
            {
                if (r is TextBox) ((TextBox)r).ReadOnly = true;
            });
            loadM();
            if (li.Count > 0)
            {
                dr = GetDataRow(tempNo);
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
                    tot += tbM.Rows[i]["ReMny"].ToDecimal();
                }
                pVar.FrmPayabld.RemitMny.Text = tbM.AsEnumerable().Sum(r => r["ReMny"].ToDecimal()).ToString("f" + pVar.FrmPayabld.RemitMny.LastNum);//tot.ToString("f" + Common.MST);
                pVar.FrmPayabld.FeeMny.Text = tbM.AsEnumerable().Sum(r => r["ChargeMny"].ToDecimal()).ToString("f" + pVar.FrmPayabld.FeeMny.LastNum);
                pVar.FrmPayabld.RemitO = tbM.Copy();
            }
            this.Dispose();
            this.Close();
        }

        private void CoNo_DoubleClick(object sender, EventArgs e)
        {
            if (CoNo.ReadOnly)
                return;

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
            if (CoNo.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

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
                        if (BeforeText != CoNo.Text.Trim())
                        {
                            AcNo.Text = AcName.Text = AcName1.Text = Xa1Name.Text = "";
                        }
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused || ReDate.ReadOnly) return;
            if (ReDate.Text.Trim() == "")
            {
                MessageBox.Show("『匯出日期』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (!ReDate.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (!稽核會計年度(ReDate.Text.Trim())) e.Cancel = true;
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
                frm.去除外幣帳戶 = false;
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
            using (SqlConnection cn = new SqlConnection(票據連線字串))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("acno", AcNo.Text.Trim());
                cmd.CommandText = "select xa1name from acct where acno=@acno";
                if (!cmd.ExecuteScalar().IsNullOrEmpty()) Xa1Name.Text = cmd.ExecuteScalar().ToString().Trim();
            }
        }

        private void AcNo_Validating(object sender, CancelEventArgs e)
        {
            if (AcNo.ReadOnly || btnCancel.Focused) return;
            if (AcNo.Text.Trim() == "")
            {
                AcName.Text = AcName1.Text = Xa1Name.Text = "";
                e.Cancel = true;
                MessageBox.Show("『帳戶編號』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("acno", AcNo.Text.Trim());
                    cmd.Parameters.AddWithValue("cono", CoNo.Text.Trim());

                    cmd.CommandText = "select * from acct where acno=@acno and cono=@cono";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        AcNo.Text = reader["AcNo"].ToString();
                        AcName.Text = reader["AcName"].ToString();
                        AcName1.Text = reader["AcName1"].ToString();
                    }
                    else
                    {
                        e.Cancel = true;
                        using (var frm = new 銀行帳號建檔_瀏覽())
                        {
                            frm.chksys = chksys;
                            frm.scrit = scrit;
                            frm.票據連線字串 = 票據連線字串;
                            frm.開窗模式 = true;
                            frm.去除外幣帳戶 = false;
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
                    reader.Dispose(); reader.Close();
                    cmd.CommandText = "select xa1name from acct where acno=@acno";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty()) Xa1Name.Text = cmd.ExecuteScalar().ToString().Trim();

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReMemo_DoubleClick(object sender, EventArgs e)
        {
            if (ReMemo.ReadOnly)
                return;

            pVar.MemoMemoOpenForm(ReMemo, 50);
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

        bool 自動產生編號()
        {
            DataTable table = new DataTable();
            string date = "", count = "1";
            if (chksys["noadd"].ToDecimal() == 1)//民國年月日+流水號
            {
                date = Date.ToTWDate(ReDate.Text.Trim());
                count = count.PadLeft(3, '0');
            }
            else if (chksys["noadd"].ToDecimal() == 2)//民國年月+流水號
            {
                date = Date.ToTWDate(ReDate.Text.Trim());
                date = date.Substring(0, 5);
                count = count.PadLeft(5, '0');
            }
            else if (chksys["noadd"].ToDecimal() == 3)//西元年月日+流水號
            {
                date = Date.ToUSDate(ReDate.Text.Trim());
                count = count.PadLeft(2, '0');
            }
            else
            {
                date = Date.ToUSDate(ReDate.Text.Trim());
                date = date.Substring(0, 6);
                count = count.PadLeft(4, '0');
            }
            decimal No = (date + count).ToDecimal();
            try
            {
                if (ReNo.Text.Trim() == "")
                {
                    using (SqlConnection cn = new SqlConnection(票據連線字串))
                    {
                        cn.Open();
                        string sql = "";
                        if (chksys["noadd"].ToDecimal() == 1)
                            sql = "select reno from remito where left(reno,7)='" + Date.ToTWDate(ReDate.Text.ToString()) + "' order by reno desc";
                        else if (chksys["noadd"].ToDecimal() == 2)
                            sql = "select reno from remito where left(reno,5)='" + Date.ToTWDate(ReDate.Text.ToString()).Substring(0, 5) + "' order by reno desc";
                        else if (chksys["noadd"].ToDecimal() == 3)
                            sql = "select reno from remito where left(reno,8)='" + Date.ToUSDate(ReDate.Text.ToString()) + "' order by reno desc";
                        else
                            sql = "select reno from remito where left(reno,6)='" + Date.ToUSDate(ReDate.Text.ToString()).Substring(0, 6) + "' order by reno desc";
                        SqlDataAdapter dd = new SqlDataAdapter(sql, cn);
                        dd.Fill(table);
                        table.Merge(tbM);
                        if (table.Rows.Count > 0)
                        {
                            table = table.AsEnumerable().OrderByDescending(r => r["reno"].ToString()).CopyToDataTable();
                            No = table.Rows[0]["reno"].ToDecimal() + 1;
                            ReNo.Text = No.ToString().Trim();
                            return true;
                        }
                        else
                        {
                            ReNo.Text = No.ToString().Trim();
                            return true;
                        }
                    }
                }
                else
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("reno", ReNo.Text.Trim());
                        cmd.CommandText = "select reno from remito where reno=@reno";
                        if (!cmd.ExecuteScalar().IsNullOrEmpty()) return false;

                        if (li.Find(r => r["reno"].ToString().Trim() == ReNo.Text.Trim()) != null) return false;

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

        private void ReNo_Validating(object sender, CancelEventArgs e)
        {
            if (ReNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (btnState == "Append")
            {
                loadM();
                dr = GetDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    e.Cancel = true;
                    ReNo.Text = "";
                    MessageBox.Show("此匯入證號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ReNo.Text = "";
                    MessageBox.Show("此匯入證號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (btnState == "Modify")
            {
                loadM();
                dr = GetDataRow();
                int i = li.IndexOf(dr);
                if (i != -1)
                {
                    if (ReNo.Text.Trim() != BeforeText)
                    {
                        WriteToTxt(dr);
                        CoNo.Focus();
                    }

                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("無此匯入證號，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ReNo_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.ReadOnly) return;
            BeforeText = tb.Text.Trim();
        }

        private void SurCharge_CheckStateChanged(object sender, EventArgs e)
        {
            if (btnState == "") return;
            if (SurCharge.CheckState == CheckState.Checked) ChargeMny.ReadOnly = false;
            else
            {
                ChargeMny.Text = "0";
                ChargeMny.ReadOnly = true;
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
                    FaName2.Text = reader["FaName2"].ToString();
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
