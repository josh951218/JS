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
    public partial class FrmFact : Formbase
    {
        JBS.JS.Fact jFact;
        JBS.JS.xEvents xe;
        List<TextBoxbase> list = new List<TextBoxbase>();
        SqlTransaction tn;

        string tempNo = "";
        string btnState = string.Empty;
        string BeforeText = "";
        DataTable DataGridView_dt = new DataTable();

        public FrmFact()
        {
            InitializeComponent();
            this.jFact = new JBS.JS.Fact();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();

            FaCredit.FirstNum = Common.nFirst;
            FaXbh.FirstNum = Common.nFirst;
            FaPayAmt.FirstNum = Common.nFirst;

            FaCredit.LastNum = Common.MF;
            FaXbh.LastNum = Common.MF;
            FaPayAmt.LastNum = Common.MF;

            if (Common.Series == "74" || Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
            }
            loadSystemSettings();
        }

        private void FrmFact_Load(object sender, EventArgs e)
        {
            FaUno.TextAlign = HorizontalAlignment.Left;

            writeToTxt(Common.load("Top", "Fact", "FaNo"));
            btnAppend.Focus();
        }

        private void loadSystemSettings()
        {
            //載入系統資訊
            //自訂欄位,資料庫有值才改,沒值則預設
            var v = Common.listSysSettings.First().Field<string>("faudfc1");
            if (v != "" && v != null) lblFaUdf1.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("faudfc2");
            if (v != "" && v != null) lblFaUdf2.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("faudfc3");
            if (v != "" && v != null) lblFaUdf3.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("faudfc4");
            if (v != "" && v != null) lblFaUdf4.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("faudfc5");
            if (v != "" && v != null) lblFaUdf5.Text = v.Trim();
        }

        private void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                //表頭欄位
                FaNo.Text = row["FaNo"].ToString();
                FaIme.Text = row["FaIme"].ToString();
                FaName2.Text = row["FaName2"].ToString();
                FaName1.Text = row["FaName1"].ToString();
                //幣別編號不是空值,帶出幣別名稱
                Xa1No.Text = row["FaXa1no"].ToString();
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);

                //類別編號不是空值,帶出類別名稱
                pVar.XX12Validate(row["FaX12no"].ToString(), FaX12No, FaX12Name);
                //業務人員編號不是空值,帶出業務名稱
                FaEmNo1.Text = row["FaEmno1"].ToString();
                pVar.EmplValidate(FaEmNo1.Text, FaEmNo1, FaEmName);

                //
                //分頁一
                //
                FaPer1.Text = row["FaPer1"].ToString();
                FaPer2.Text = row["FaPer2"].ToString();
                FaPer.Text = row["FaPer"].ToString();
                FaTel1.Text = row["FaTel1"].ToString();
                FaTel2.Text = row["FaTel2"].ToString();
                FaFax1.Text = row["FaFax1"].ToString();
                FaAtel1.Text = row["FaAtel1"].ToString();
                FaAtel2.Text = row["FaAtel2"].ToString();
                FaTel3.Text = row["FaTel3"].ToString();
                FaAddr1.Text = row["FaAddr1"].ToString();
                FaR1.Text = row["FaR1"].ToString();
                FaAddr2.Text = row["FaAddr2"].ToString();
                FaR2.Text = row["FaR2"].ToString();
                FaAddr3.Text = row["FaAddr3"].ToString();
                FaR3.Text = row["FaR3"].ToString();
                //現有應付帳款:dt["FaPayable"]

                //現有預付餘額:dt["FaPayAmt"]
                FaPayAmt.Text = row["FaPayAmt"].ToString();
                decimal d = 0;
                decimal.TryParse(FaPayAmt.Text, out d);
                FaPayAmt.Text = string.Format("{0:F" + FaPayAmt.LastNum + "}", d);

                //信用額度
                FaCredit.Text = row["FaCredit"].ToString();
                d = 0;
                decimal.TryParse(FaCredit.Text, out d);
                FaCredit.Text = string.Format("{0:F" + FaCredit.LastNum + "}", d);

                //計算信用額度的餘額：
                //餘額[FaXbh] = 信用額度[FaCredit]-現有應付帳款["FaPayable"]
                decimal d_credit = 0, d_receiv = 0;
                d = 0;
                decimal.TryParse(row["FaCredit"].ToString(), out d_credit);
                decimal.TryParse(row["FaPayable"].ToString(), out d_receiv);
                d = d_credit - d_receiv;
                FaXbh.Text = string.Format("{0:F" + FaXbh.LastNum + "}", d);


                FaEmail.Text = row["FaEmail"].ToString();
                FaWww.Text = row["FaWww"].ToString();
                FaChkName.Text = row["FaChkName"].ToString();
                //統一編號
                FaUno.Text = row["FaUno"].ToString();
                //營業稅編號不是空值,帶出營業稅名稱
                FaX3No.Text = row["FaX3no"].ToString();
                pVar.XX03Validate(FaX3No.Text, FaX3No, FaX3Name);

                //結帳類別編號不是空值,帶出結帳類別名稱
                FaX4No.Text = row["FaX4no"].ToString();
                pVar.XX04Validate(FaX4No.Text, FaX4No, FaX4Name);

                //發票模式編號不是空值,帶出發票模式名稱
                FaX5No.Text = row["FaX5no"].ToString();
                pVar.XX05Validate(FaX5No.Text, FaX5No, FaX5Name);

                //
                //分頁二
                //
                FaEngname.Text = row["FaEngname"].ToString();
                FaEngaddr.Text = row["FaEngaddr"].ToString();
                FaEngr1.Text = row["FaEngr1"].ToString();
                FaMemo1.Text = row["FaMemo1"].ToString();
                FaWork.Text = row["FaWork"].ToString();
                FaArea.Text = row["FaArea"].ToString();
                //區域編號不是空值,帶出區域名稱
                FaX2No.Text = row["FaX2no"].ToString();
                pVar.XX02Validate(FaX2No.Text, FaX2No, FaX2Name);

                FaUdf1.Text = row["FaUdf1"].ToString();
                FaUdf2.Text = row["FaUdf2"].ToString();
                FaUdf3.Text = row["FaUdf3"].ToString();
                FaUdf4.Text = row["FaUdf4"].ToString();
                FaUdf5.Text = row["FaUdf5"].ToString();
                FaLastday.Text = row["FaLastday"].ToString();
                if (Common.User_DateTime == 1)
                {
                    FaDate.Text = row["FaDate"].ToString();
                }
                else
                {
                    FaDate.Text = row["FaDate1"].ToString();
                }
                WebID.Text = row["WebID"].ToString();
                WebPassWord.Text = row["WebPassWord"].ToString();
                SeekNo = FaNo.Text;

                DetailMemo.Text = row["DetailMemo"].ToString();
            }
            else
            {
                SeekNo = "";
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                DetailMemo.Clear();
            }
            Radio_7_8_9_CheckedChanged(null, null);
        }

        private void writeToTxt(SqlDataReader row)
        {
            if (row != null)
            {
                //表頭欄位
                FaNo.Text = row["FaNo"].ToString();
                FaIme.Text = row["FaIme"].ToString();
                FaName2.Text = row["FaName2"].ToString();
                FaName1.Text = row["FaName1"].ToString();
                //幣別編號不是空值,帶出幣別名稱
                Xa1No.Text = row["FaXa1no"].ToString();
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);

                //類別編號不是空值,帶出類別名稱
                pVar.XX12Validate(row["FaX12no"].ToString(), FaX12No, FaX12Name);
                //業務人員編號不是空值,帶出業務名稱
                FaEmNo1.Text = row["FaEmno1"].ToString();
                pVar.EmplValidate(FaEmNo1.Text, FaEmNo1, FaEmName);

                //
                //分頁一
                //
                FaPer1.Text = row["FaPer1"].ToString();
                FaPer2.Text = row["FaPer2"].ToString();
                FaPer.Text = row["FaPer"].ToString();
                FaTel1.Text = row["FaTel1"].ToString();
                FaTel2.Text = row["FaTel2"].ToString();
                FaFax1.Text = row["FaFax1"].ToString();
                FaAtel1.Text = row["FaAtel1"].ToString();
                FaAtel2.Text = row["FaAtel2"].ToString();
                FaTel3.Text = row["FaTel3"].ToString();
                FaAddr1.Text = row["FaAddr1"].ToString();
                FaR1.Text = row["FaR1"].ToString();
                FaAddr2.Text = row["FaAddr2"].ToString();
                FaR2.Text = row["FaR2"].ToString();
                FaAddr3.Text = row["FaAddr3"].ToString();
                FaR3.Text = row["FaR3"].ToString();
                //現有應付帳款:dt["FaPayable"]

                //現有預付餘額:dt["FaPayAmt"]
                FaPayAmt.Text = row["FaPayAmt"].ToString();
                decimal d = 0;
                decimal.TryParse(FaPayAmt.Text, out d);
                FaPayAmt.Text = string.Format("{0:F" + FaPayAmt.LastNum + "}", d);

                //信用額度
                FaCredit.Text = row["FaCredit"].ToString();
                d = 0;
                decimal.TryParse(FaCredit.Text, out d);
                FaCredit.Text = string.Format("{0:F" + FaCredit.LastNum + "}", d);

                //計算信用額度的餘額：
                //餘額[FaXbh] = 信用額度[FaCredit]-現有應付帳款["FaPayable"]
                decimal d_credit = 0, d_receiv = 0;
                d = 0;
                decimal.TryParse(row["FaCredit"].ToString(), out d_credit);
                decimal.TryParse(row["FaPayable"].ToString(), out d_receiv);
                d = d_credit - d_receiv;
                FaXbh.Text = string.Format("{0:F" + FaXbh.LastNum + "}", d);


                FaEmail.Text = row["FaEmail"].ToString();
                FaWww.Text = row["FaWww"].ToString();
                FaChkName.Text = row["FaChkName"].ToString();
                //統一編號
                FaUno.Text = row["FaUno"].ToString();
                //營業稅編號不是空值,帶出營業稅名稱
                FaX3No.Text = row["FaX3no"].ToString();
                pVar.XX03Validate(FaX3No.Text, FaX3No, FaX3Name);

                //結帳類別編號不是空值,帶出結帳類別名稱
                FaX4No.Text = row["FaX4no"].ToString();
                pVar.XX04Validate(FaX4No.Text, FaX4No, FaX4Name);

                //發票模式編號不是空值,帶出發票模式名稱
                FaX5No.Text = row["FaX5no"].ToString();
                pVar.XX05Validate(FaX5No.Text, FaX5No, FaX5Name);

                //
                //分頁二
                //
                FaEngname.Text = row["FaEngname"].ToString();
                FaEngaddr.Text = row["FaEngaddr"].ToString();
                FaEngr1.Text = row["FaEngr1"].ToString();
                FaMemo1.Text = row["FaMemo1"].ToString();
                FaWork.Text = row["FaWork"].ToString();
                FaArea.Text = row["FaArea"].ToString();
                //區域編號不是空值,帶出區域名稱
                FaX2No.Text = row["FaX2no"].ToString();
                pVar.XX02Validate(FaX2No.Text, FaX2No, FaX2Name);

                FaUdf1.Text = row["FaUdf1"].ToString();
                FaUdf2.Text = row["FaUdf2"].ToString();
                FaUdf3.Text = row["FaUdf3"].ToString();
                FaUdf4.Text = row["FaUdf4"].ToString();
                FaUdf5.Text = row["FaUdf5"].ToString();
                FaLastday.Text = row["FaLastday"].ToString();
                if (Common.User_DateTime == 1)
                {
                    FaDate.Text = row["FaDate"].ToString();
                }
                else
                {
                    FaDate.Text = row["FaDate1"].ToString();
                }
                WebID.Text = row["WebID"].ToString();
                WebPassWord.Text = row["WebPassWord"].ToString();
                SeekNo = FaNo.Text;
                DetailMemo.Text = row["DetailMemo"].ToString();
            }
            else
            {
                SeekNo = "";
                DetailMemo.Clear();
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
            Radio_7_8_9_CheckedChanged(null, null);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Top", "Fact", "FaNo"));
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "Fact", "FaNo", FaNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "Fact", "FaNo", FaNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            writeToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() == "") return;
            var row = Common.load("Next", "Fact", "FaNo", FaNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "Fact", "FaNo", FaNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Bottom", "Fact", "FaNo", FaNo.Text.Trim()));
        }

        void setTxtWhenAppend()
        {
            //新增一筆資料時,載入欄位預設值
            Xa1No.Text = "TWD";
            Xa1Name.Text = "新臺幣";
            FaX3No.Text = "1";
            FaX3Name.Text = "外加稅";
            FaX5No.Text = "1";
            FaX5Name.Text = "三聯式";
            FaArea.Text = "1";

            switch (Common.User_DateTime)
            {
                case 1:
                    FaDate.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    FaDate.Text = Date.GetDateTime(2, false);
                    break;
            }
            FaCredit.Text = (0M).ToString("f" + Common.MF);
            FaXbh.Text = (0M).ToString("f" + Common.MF);
            FaPayAmt.Text = (0M).ToString("f" + Common.MF);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = FaNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            DetailMemo.Clear();
            btnState = ((Button)sender).Name.Substring(3);

            setTxtWhenAppend();
            FaNo.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            tempNo = FaNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            switch (Common.User_DateTime)
            {
                case 1:
                    FaDate.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    FaDate.Text = Date.GetDateTime(2, false);
                    break;
            }
            WebID.Clear();
            WebPassWord.Clear();

            FaNo.SelectAll();
            FaNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            tempNo = FaNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            if (!稽核此廠商是否能刪除或者修改幣別("修改幣別"))
                Xa1No.ReadOnly = true;

            FaNo.Focus();
            FaNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (FaNo.Text == string.Empty) return;
            btnState = "Delete";
            try
            {
                if (!稽核此廠商是否能刪除或者修改幣別("刪除廠商"))
                    return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                    cmd.CommandText = "delete from Fact where FaNo=@FaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();
                }
                btnNext_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool 稽核此廠商是否能刪除或者修改幣別(string state = "")
        {   
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                cmd.CommandText = "select count(*) from bshop where FaNo=@FaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    if (state == "刪除廠商")
                    MessageBox.Show("此廠商已有帳款資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; 
                }
                cmd.CommandText = "select count(*) from rshop where FaNo=@FaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    if (state == "刪除廠商")
                    MessageBox.Show("此廠商已有帳款資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; 
                }
                cmd.CommandText = "select count(*) from Payabl where FaNo=@FaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    if (state == "刪除廠商")
                    MessageBox.Show("此廠商已有帳款資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; 
                }
                cmd.CommandText = "select count(*) from borr where FaNo=@FaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    if (state == "刪除廠商")
                    MessageBox.Show("此廠商已有借入還出資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; 
                }
                cmd.CommandText = "select count(*) from RBorr where FaNo=@FaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    if (state == "刪除廠商")
                    MessageBox.Show("此廠商已有借入還出資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; 
                }
                return true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmfactPrint())
            {
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (FaNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            using (var frm = new FrmFactb())
            {
                frm.TSeekNo = FaNo.Text.Trim();
                var dl = frm.ShowDialog(this);
                if (dl == DialogResult.OK)
                {
                    xe.Validate<JBS.JS.Fact>(frm.TResult, reader => writeToTxt(reader));
                }
                else if (dl == DialogResult.Yes)
                {
                    btnTop_Click(null, null);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (xe.IsRegisted("fact") == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！\n";
                msg += "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (FaNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("廠商編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }
            else
            {
                if (FaCredit.TrimTextLenth() == 0) FaCredit.Text = (0M).ToString("f" + Common.MF);
            }
            if (Xa1No.Text.Trim() == "")
            {
                MessageBox.Show("幣別編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Xa1No.Focus();
                return;
            }
            if (FaX3No.Text.Trim() == "")
            {
                MessageBox.Show("營業稅不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaX3No.Focus();
                return;
            }
            if (FaX5No.Text.Trim() == "")
            {
                MessageBox.Show("發票模式不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaX5No.Focus();
                return;
            }
            if (WebID.TrimTextLenth() > 0 && WebPassWord.TrimTextLenth() == 0)
            {
                MessageBox.Show("網路密碼不可為空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WebPassWord.Focus();
                return;
            }
            if (WebID.TrimTextLenth() == 0 && WebPassWord.TrimTextLenth() > 0)
            {
                MessageBox.Show("網路帳號不可為空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WebID.Focus();
                return;
            }

            if (btnState == "Append")
            {
                if (Common.load("Check", "Fact", "FaNo", FaNo.Text.Trim()) != null)
                {
                    MessageBox.Show("此廠商編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaNo.Text = string.Empty;
                    FaNo.Focus();
                    return;
                }
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaName2", FaName2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaName1", FaName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaWork", FaWork.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaXa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaCono", "T");
                        cmd.Parameters.AddWithValue("@FaIme", FaIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX12no", FaX12No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEmno1", FaEmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer1", FaPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer2", FaPer2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer", FaPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel1", FaTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel2", FaTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaFax1", FaFax1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAtel1", FaAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAtel2", FaAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel3", FaTel3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaBbc", "BBC");
                        cmd.Parameters.AddWithValue("@FaAddr1", FaAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR1", FaR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAddr2", FaAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR2", FaR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAddr3", FaAddr3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR3", FaR3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEmail", FaEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaWww", FaWww.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX2no", FaX2No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUno", FaUno.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX3no", FaX3No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX4no", FaX4No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaCredit", FaCredit.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngname", FaEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngaddr", FaEngaddr.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngr1", FaEngr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaMemo1", FaMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX5no", FaX5No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaArea", FaArea.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf1", FaUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf2", FaUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf3", FaUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf4", FaUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf5", FaUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf6", "保留");
                        cmd.Parameters.AddWithValue("@FaDate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@FaDate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@FaDate2", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@FaLastday", "");
                        cmd.Parameters.AddWithValue("@FaLastday1", "");
                        cmd.Parameters.AddWithValue("@FaLastday2", "");
                        cmd.Parameters.AddWithValue("@FaFirPayPar", "1");
                        cmd.Parameters.AddWithValue("@FaChkName", FaChkName.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebPassWord", WebPassWord.Text.Trim());
                        cmd.Parameters.AddWithValue("@detailmemo", DetailMemo.Text);


                        cmd.CommandText = "INSERT INTO Fact"
                            + "(FaNo,FaName2,FaName1,FaWork"
                            + ",FaXa1no,FaCono,FaIme,FaX12no"
                            + ",FaEmno1,FaPer1,FaPer2,FaPer,FaTel1"
                            + ",FaTel2,FaFax1,FaAtel1,FaAtel2,FaTel3"
                            + ",FaBbc,FaAddr1,FaR1,FaAddr2,FaR2"
                            + ",FaAddr3,FaR3,FaEmail"
                            + ",FaWww,FaX2no,FaUno,FaX3no,FaX4no"
                            + ",FaCredit,FaEngname,FaEngaddr,FaEngr1,FaMemo1"
                            + ",FaX5no,FaArea,FaUdf1,FaUdf2"
                            + ",FaUdf3,FaUdf4,FaUdf5,FaUdf6,FaDate"
                            + ",FaDate1,FaDate2,FaLastday,FaLastday1,FaLastday2"
                            + ",FaFirPayPar"
                            //+ ",FaFirPayabl,FaSparePay,FaFirPayPar,FaFirPayAmt,cuadvamt"
                            + ",FaChkName,WebID,WebPassWord,detailmemo"
                            + ") VALUES ("
                            //+ FaNo.Text.Trim() + "',N'" + FaName2.Text + "',N'" + FaName1.Text + "',N'" + FaWork.Text + "',N'"
                            //+ Xa1No.Text + "','T',N'" + FaIme.Text + "',N'" + FaX12No.Text + "',N'"
                            //+ FaEmNo1.Text + "',N'" + FaPer1.Text + "',N'" + FaPer2.Text + "',N'" + FaPer.Text + "',N'" + FaTel1.Text + "',N'"
                            //+ FaTel2.Text + "',N'" + FaFax1.Text + "',N'" + FaAtel1.Text + "',N'" + FaAtel2.Text + "',N'" + FaTel3.Text
                            //+ "','BBC',N'" + FaAddr1.Text + "',N'" + FaR1.Text + "',N'" + FaAddr2.Text + "',N'" + FaR2.Text + "',N'"
                            //+ FaAddr3.Text + "',N'" + FaR3.Text + "',N'" + FaEmail.Text + "',N'"
                            //+ FaWww.Text + "',N'" + FaX2No.Text + "',N'" + FaUno.Text + "',N'" + FaX3No.Text + "',N'" + FaX4No.Text + "',"
                            //+ FaCredit.Text + ",N'" + FaEngname.Text + "',N'" + FaEngaddr.Text + "',N'" + FaEngr1.Text + "',N'" + FaMemo1.Text + "',N'"
                            //+ FaX5No.Text + "',N'" + FaArea.Text + "',N'" + FaUdf1.Text + "',N'" + FaUdf2.Text + "',N'"
                            //+ FaUdf3.Text + "',N'" + FaUdf4.Text + "',N'" + FaUdf5.Text + "','保留',N'" + Model.Date.GetDateTime(1, false)
                            //+ "',N'" + Model.Date.GetDateTime(2, false) + "',N'" + Model.Date.GetDateTime(2, false) + "','','','',1"
                            ////+ ",0,0,0,0,0"
                            //+ ",'" + FaChkName.Text.Trim() + "')";

                            + "(@FaNo),(@FaName2),(@FaName1),(@FaWork),(@FaXa1no),(@FaCono),(@FaIme),(@FaX12no),(@FaEmno1),(@FaPer1),(@FaPer2),(@FaPer)"
                            + ",(@FaTel1),(@FaTel2),(@FaFax1),(@FaAtel1),(@FaAtel2),(@FaTel3),(@FaBbc),(@FaAddr1),(@FaR1),(@FaAddr2),(@FaR2),(@FaAddr3)"
                            + ",(@FaR3),(@FaEmail),(@FaWww),(@FaX2no),(@FaUno),(@FaX3no),(@FaX4no),(@FaCredit),(@FaEngname),(@FaEngaddr),(@FaEngr1),(@FaMemo1)"
                            + ",(@FaX5no),(@FaArea),(@FaUdf1),(@FaUdf2),(@FaUdf3),(@FaUdf4),(@FaUdf5),(@FaUdf6),(@FaDate),(@FaDate1),(@FaDate2),(@FaLastday)"
                            + ",(@FaLastday1),(@FaLastday2),(@FaFirPayPar),(@FaChkName),(@WebID),(@WebPassWord),(@detailmemo)"
                            + ")";

                        cmd.ExecuteNonQuery();
                        tn.Commit();

                        this.SeekNo = tempNo = FaNo.Text.Trim();
                        Common.SetTextState(FormState = FormEditState.Clear, ref list);
                        DetailMemo.Clear();
                        FormState = FormEditState.Append;
                        FaNo.Focus();
                        setTxtWhenAppend();
                    }
                    catch (Exception ex)
                    {
                        if (tn != null) tn.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            if (btnState == "Duplicate")
            {
                if (Common.load("Check", "Fact", "FaNo", FaNo.Text.Trim()) != null)
                {
                    MessageBox.Show("此廠商編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaNo.Text = string.Empty;
                    FaNo.Focus();
                    return;
                }
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaName2", FaName2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaName1", FaName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaWork", FaWork.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaXa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaCono", "T");
                        cmd.Parameters.AddWithValue("@FaIme", FaIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX12no", FaX12No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEmno1", FaEmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer1", FaPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer2", FaPer2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer", FaPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel1", FaTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel2", FaTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaFax1", FaFax1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAtel1", FaAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAtel2", FaAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel3", FaTel3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaBbc", "BBC");
                        cmd.Parameters.AddWithValue("@FaAddr1", FaAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR1", FaR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAddr2", FaAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR2", FaR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAddr3", FaAddr3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR3", FaR3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEmail", FaEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaWww", FaWww.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX2no", FaX2No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUno", FaUno.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX3no", FaX3No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX4no", FaX4No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaCredit", FaCredit.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngname", FaEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngaddr", FaEngaddr.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngr1", FaEngr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaMemo1", FaMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX5no", FaX5No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaArea", FaArea.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf1", FaUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf2", FaUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf3", FaUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf4", FaUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf5", FaUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf6", "保留");
                        cmd.Parameters.AddWithValue("@FaDate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@FaDate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@FaDate2", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@FaLastday", "");
                        cmd.Parameters.AddWithValue("@FaLastday1", "");
                        cmd.Parameters.AddWithValue("@FaLastday2", "");
                        cmd.Parameters.AddWithValue("@FaFirPayPar", "1");
                        cmd.Parameters.AddWithValue("@FaChkName", FaChkName.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebPassWord", WebPassWord.Text.Trim());
                        cmd.Parameters.AddWithValue("@detailmemo", DetailMemo.Text);


                        cmd.CommandText = "INSERT INTO Fact"
                            + "(FaNo,FaName2,FaName1,FaWork"
                            + ",FaXa1no,FaCono,FaIme,FaX12no"
                            + ",FaEmno1,FaPer1,FaPer2,FaPer,FaTel1"
                            + ",FaTel2,FaFax1,FaAtel1,FaAtel2,FaTel3"
                            + ",FaBbc,FaAddr1,FaR1,FaAddr2,FaR2"
                            + ",FaAddr3,FaR3,FaEmail"
                            + ",FaWww,FaX2no,FaUno,FaX3no,FaX4no"
                            + ",FaCredit,FaEngname,FaEngaddr,FaEngr1,FaMemo1"
                            + ",FaX5no,FaArea,FaUdf1,FaUdf2"
                            + ",FaUdf3,FaUdf4,FaUdf5,FaUdf6,FaDate"
                            + ",FaDate1,FaDate2,FaLastday,FaLastday1,FaLastday2"
                            + ",FaFirPayPar"
                            //+ ",FaFirPayabl,FaSparePay,FaFirPayPar,FaFirPayAmt,cuadvamt"
                            + ",FaChkName,WebID,WebPassWord,detailmemo"
                            + ") VALUES ("
                            + "(@FaNo),(@FaName2),(@FaName1),(@FaWork),(@FaXa1no),(@FaCono),(@FaIme),(@FaX12no),(@FaEmno1),(@FaPer1),(@FaPer2),(@FaPer)"
                            + ",(@FaTel1),(@FaTel2),(@FaFax1),(@FaAtel1),(@FaAtel2),(@FaTel3),(@FaBbc),(@FaAddr1),(@FaR1),(@FaAddr2),(@FaR2),(@FaAddr3)"
                            + ",(@FaR3),(@FaEmail),(@FaWww),(@FaX2no),(@FaUno),(@FaX3no),(@FaX4no),(@FaCredit),(@FaEngname),(@FaEngaddr),(@FaEngr1),(@FaMemo1)"
                            + ",(@FaX5no),(@FaArea),(@FaUdf1),(@FaUdf2),(@FaUdf3),(@FaUdf4),(@FaUdf5),(@FaUdf6),(@FaDate),(@FaDate1),(@FaDate2),(@FaLastday)"
                            + ",(@FaLastday1),(@FaLastday2),(@FaFirPayPar),(@FaChkName),(@WebID),(@WebPassWord),(@detailmemo)"
                            + ")";

                        cmd.ExecuteNonQuery();
                        tn.Commit();

                        this.SeekNo = tempNo = FaNo.Text.Trim();
                        Common.SetTextState(FormState = FormEditState.Clear, ref list);
                        DetailMemo.Clear();
                        FormState = FormEditState.Append;
                        FaNo.Focus();
                        setTxtWhenAppend();
                    }
                    catch (Exception ex)
                    {
                        if (tn != null) tn.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            if (btnState == "Modify")
            {
                if (Common.load("Check", "Fact", "FaNo", FaNo.Text.Trim()) == null)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaNo.Text = string.Empty;
                    FaNo.Focus();
                    return;
                }
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaName2", FaName2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaName1", FaName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaWork", FaWork.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaXa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaIme", FaIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX12no", FaX12No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEmno1", FaEmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer1", FaPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer2", FaPer2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaPer", FaPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel1", FaTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel2", FaTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaFax1", FaFax1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAtel1", FaAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAtel2", FaAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaTel3", FaTel3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAddr1", FaAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR1", FaR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAddr2", FaAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR2", FaR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaAddr3", FaAddr3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaR3", FaR3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEmail", FaEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaWww", FaWww.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX2no", FaX2No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUno", FaUno.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX3no", FaX3No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX4no", FaX4No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaCredit", FaCredit.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngname", FaEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngaddr", FaEngaddr.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaEngr1", FaEngr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaMemo1", FaMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaX5no", FaX5No.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaArea", FaArea.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf1", FaUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf2", FaUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf3", FaUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf4", FaUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdf5", FaUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaDate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@FaDate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@FaDate2", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@FaLastday", "");
                        cmd.Parameters.AddWithValue("@FaLastday1", "");
                        cmd.Parameters.AddWithValue("@FaLastday2", "");
                        cmd.Parameters.AddWithValue("@FaFirPayPar", "1");
                        cmd.Parameters.AddWithValue("@FaChkName", FaChkName.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebPassWord", WebPassWord.Text.Trim());
                        cmd.Parameters.AddWithValue("@detailmemo", DetailMemo.Text);


                        cmd.CommandText = "Update Fact set "
                          + "FaName2 =@FaName2"
                          + ",FaName1 =@FaName1"
                          + ",FaWork =@FaWork"
                          + ",FaXa1no =@FaXa1no"
                          + ",FaIme =@FaIme"
                          + ",FaX12no =@FaX12no"
                          + ",FaEmno1 =@FaEmno1"
                          + ",FaPer1 =@FaPer1"
                          + ",FaPer2 =@FaPer2"
                          + ",FaPer =@FaPer"
                          + ",FaTel1 =@FaTel1"
                          + ",FaTel2 =@FaTel2"
                          + ",FaFax1 =@FaFax1"
                          + ",FaAtel1 =@FaAtel1"
                          + ",FaAtel2 =@FaAtel2"
                          + ",FaTel3 =@FaTel3"
                          + ",FaAddr1 =@FaAddr1"
                          + ",FaR1 =@FaR1"
                          + ",FaAddr2 =@FaAddr2"
                          + ",FaR2 =@FaR2"
                          + ",FaAddr3 =@FaAddr3"
                          + ",FaR3 =@FaR3"
                          + ",FaEmail =@FaEmail"
                          + ",FaWww =@FaWww"
                          + ",FaX2no =@FaX2no"
                          + ",FaUno =@FaUno"
                          + ",FaX3no =@FaX3no"
                          + ",FaX4no =@FaX4no"
                          + ",FaCredit =@FaCredit"
                          + ",FaEngname =@FaEngname"
                          + ",FaEngaddr =@FaEngaddr"
                          + ",FaEngr1 =@FaEngr1"
                          + ",FaChkName =@FaChkName"
                          + ",FaMemo1 =@FaMemo1"
                          + ",FaX5no =@FaX5no"
                          + ",FaArea =@FaArea"
                          + ",FaUdf1 =@FaUdf1"
                          + ",FaUdf2 =@FaUdf2"
                          + ",FaUdf3 =@FaUdf3"
                          + ",FaUdf4 =@FaUdf4"
                          + ",FaUdf5 =@FaUdf5"
                          + ",WebID =@WebID"
                          + ",detailmemo =@detailmemo"
                          + ",WebPassWord =@WebPassWord"
                          + " where FaNo =@FaNo"
                          + " COLLATE Chinese_Taiwan_Stroke_BIN";

                        cmd.ExecuteNonQuery();
                        tn.Commit();

                        this.SeekNo = tempNo = FaNo.Text.Trim();
                        Common.SetTextState(FormState = FormEditState.Clear, ref list);
                        DetailMemo.Clear();
                        FormState = FormEditState.Append;
                        FaNo.Focus();
                    }
                    catch (Exception ex)
                    {
                        if (tn != null) tn.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            DetailMemo.Clear();
            writeToTxt(Common.load("Cancel", "fact", "fano", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }






        //表頭欄位
        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender, reader => writeToTxt(reader));
        }

        private void Xa1No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Xa01>(sender, row =>
            {
                Xa1No.Text = row["Xa1No"].ToString().Trim();
                Xa1Name.Text = row["Xa1Name"].ToString().Trim();
            });
        }
        private void Xa1No_Validating(object sender, CancelEventArgs e)
        {
            if (Xa1No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (Xa1No.Text.Trim() == "")
            {
                e.Cancel = true;
                Xa1No.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.Xa01>(sender, e, row =>
            {
                Xa1No.Text = row["Xa1No"].ToString().Trim();
                Xa1Name.Text = row["Xa1Name"].ToString().Trim();
            });
        }

        private void FaX12no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX12>(sender, row =>
            {
                FaX12No.Text = row["X12No"].ToString().Trim();
                FaX12Name.Text = row["X12Name"].ToString().Trim();
            });
        }

        private void FaEmno1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender, row =>
            {
                FaEmNo1.Text = row["EmNo"].ToString().Trim();
                FaEmName.Text = row["EmName"].ToString().Trim();
            });
        }
        private void FaEmno1_Validating(object sender, CancelEventArgs e)
        {
            if (FaEmNo1.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaEmNo1.Text.Trim() == "")
            {
                FaEmNo1.Text = "";
                FaEmName.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                FaEmNo1.Text = row["EmNo"].ToString().Trim();
                FaEmName.Text = row["EmName"].ToString().Trim();
            });
        }


        private void FaAddr1_Leave(object sender, EventArgs e)
        {
            if (FaAddr2.Text.Trim() == "") FaAddr2.Text = FaAddr1.Text;
            if (FaAddr3.Text.Trim() == "") FaAddr3.Text = FaAddr1.Text;
        }

        private void FaAddr1_DoubleClick(object sender, EventArgs e)
        {
            if (FaAddr1.ReadOnly != true)
            {
                using (var frm = new FrmSaddr())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (sender.Equals(FaAddr1))
                        {
                            this.FaAddr1.Text = frm.TAddr;
                            if (this.FaAddr2.Text.Trim() == "")
                                this.FaAddr2.Text = frm.TAddr;
                            if (this.FaAddr3.Text.Trim() == "")
                                this.FaAddr3.Text = frm.TAddr;

                            this.FaR1.Text = frm.TZip;
                            if (this.FaR2.Text.Trim() == "")
                                this.FaR2.Text = frm.TZip;
                            if (this.FaR3.Text.Trim() == "")
                                this.FaR3.Text = frm.TZip;
                        }

                        if (sender.Equals(FaAddr2))
                        {
                            this.FaAddr2.Text = frm.TAddr;
                            this.FaR2.Text = frm.TZip;
                        }

                        if (sender.Equals(FaAddr3))
                        {
                            this.FaAddr3.Text = frm.TAddr;
                            this.FaR3.Text = frm.TZip;
                        }
                    }
                }
            }
        }

        private void FaR1_Leave(object sender, EventArgs e)
        {
            if (FaR2.Text.Trim() == "") FaR2.Text = FaR1.Text;
            if (FaR3.Text.Trim() == "") FaR3.Text = FaR1.Text;
        }

        private void FaCredit_Leave(object sender, EventArgs e)
        {
            if (FaCredit.ReadOnly) return;
            if (FaCredit.Text.Trim() == "") return;
            try
            {
                FaCredit.Text = string.Format("{0:F" + FaCredit.LastNum + "}", Convert.ToDecimal(FaCredit.Text));
                if (btnState == "Append" || btnState == "Duplicate")
                {
                    FaXbh.Text = FaCredit.Text;
                }
                if (btnState == "Modify")
                {
                    var v = Common.load("Check", "Fact", "FaNo", FaNo.Text.Trim());
                    decimal d = 0, d_CuCredit = 0, d_Cureceiv = 0;
                    decimal.TryParse(FaCredit.Text.Trim(), out d_CuCredit);
                    decimal.TryParse(v["FaPayable"].ToString(), out d_Cureceiv);
                    d = d_CuCredit - d_Cureceiv;
                    FaXbh.Text = string.Format("{0:F" + FaCredit.LastNum + "}", d);
                }
            }
            catch
            {
                MessageBox.Show("只能輸入數字", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaCredit.Focus();
                FaCredit.SelectAll();
            }
        }

        private void FaX3no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX03>(sender, row =>
            {
                FaX3No.Text = row["X3No"].ToString();
                FaX3Name.Text = row["X3Name"].ToString();
            });
        }

        private void FaX4no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX04>(sender, row =>
            {
                FaX4No.Text = row["X4No"].ToString().Trim();
                FaX4Name.Text = row["X4Name"].ToString().Trim();
            });
        }
        private void FaX4no_Validating(object sender, CancelEventArgs e)
        {
            if (FaX4No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaX4No.Text.Trim() == "")
            {
                FaX4No.Text = "";
                FaX4Name.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX04>(sender, e, row =>
            {
                FaX4No.Text = row["X4No"].ToString().Trim();
                FaX4Name.Text = row["X4Name"].ToString().Trim();
            });
        }

        private void FaX5no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX05>(sender, row =>
            {
                FaX5No.Text = row["X5No"].ToString().Trim();
                FaX5Name.Text = row["X5Name"].ToString().Trim();
            });
        }
        private void FaX5no_Validating(object sender, CancelEventArgs e)
        {
            if (FaX5No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaX5No.Text.Trim() == "")
            {
                e.Cancel = true;
                FaX5No.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.XX05>(sender, e, row =>
            {
                FaX5No.Text = row["X5No"].ToString().Trim();
                FaX5Name.Text = row["X5Name"].ToString().Trim();
            });
        }



        private void FaArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)49 || e.KeyChar == (char)50 || e.KeyChar == (char)51 || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void FaX2no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX02>(sender, row =>
            {
                FaX2No.Text = row["X2No"].ToString().Trim();
                FaX2Name.Text = row["X2Name"].ToString().Trim();
            });
        }
        private void FaX2no_Validating(object sender, CancelEventArgs e)
        {
            if (FaX2No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaX2No.Text.Trim() == "")
            {
                FaX2No.Text = "";
                FaX2Name.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX02>(sender, e, row =>
            {
                FaX2No.Text = row["X2No"].ToString().Trim();
                FaX2Name.Text = row["X2Name"].ToString().Trim();
            });
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    if (btnAppend.Enabled) btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (btnModify.Enabled) btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (btnDelete.Enabled) btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    if (btnBrow.Enabled) btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    if (btnExit.Enabled) btnExit.PerformClick();
                    break;
                case Keys.Home:
                    if (btnTop.Enabled) btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    if (btnPrior.Enabled) btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    if (btnNext.Enabled) btnNext.PerformClick();
                    break;
                case Keys.End:
                    if (btnBottom.Enabled) btnBottom.PerformClick();
                    break;
                case Keys.F9:
                    if (btnSave.Enabled) btnSave.PerformClick();
                    break;
                case Keys.F4:
                    if (btnCancel.Enabled)
                    {
                        btnCancel.Focus();
                        btnCancel.PerformClick();
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void FaNo_Enter(object sender, EventArgs e)
        {
            BeforeText = FaNo.Text;
        }

        private void FaNo_Validating(object sender, CancelEventArgs e)
        {
            if (FaNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaNo.Text.Trim() == "")
            {
                e.Cancel = true;
                FaNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                if (jFact.IsExist(FaNo.Text.Trim()))
                {
                    e.Cancel = true;
                    xe.Open<JBS.JS.Fact>(sender, reader => writeToTxt(reader));
                }
            }

            if (btnState == "Duplicate")
            {
                if (jFact.IsExist(FaNo.Text.Trim()))
                {
                    e.Cancel = true;
                    xe.Open<JBS.JS.Fact>(sender, reader => writeToTxt(reader));
                }
            }

            if (btnState == "Modify")
            {
                if (jFact.IsExist(FaNo.Text.Trim()))
                {
                    if (FaNo.Text.Trim() != BeforeText)
                    {
                        xe.Validate<JBS.JS.Fact>(FaNo.Text.Trim(), reader => writeToTxt(reader));
                    }
                }
                else
                {
                    e.Cancel = true;
                    xe.Open<JBS.JS.Fact>(sender, reader => writeToTxt(reader));
                }
            }
        }

        private void FaName2_Validating(object sender, CancelEventArgs e)
        {
            if (FaName2.ReadOnly) return;
            if (FaName2.Text.Trim() != "" && FaName1.Text.Trim() == "")
            {
                if (FaName2.Text.Length > 4)
                    FaName1.Text = FaName2.Text.Substring(0, 4);
                else
                    FaName1.Text = FaName2.Text;
            }
        }


        private void FaX12no_Validating(object sender, CancelEventArgs e)
        {
            if (FaX12No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaX12No.Text.Trim() == "")
            {
                FaX12No.Text = "";
                FaX12Name.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX12>(sender, e, row =>
            {
                FaX12No.Text = row["X12No"].ToString().Trim();
                FaX12Name.Text = row["X12Name"].ToString().Trim();
            });
        }


        private void FaX3no_Validating(object sender, CancelEventArgs e)
        {
            if (FaX3No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaX3No.Text.Trim() == "")
            {
                e.Cancel = true;
                FaX3No.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                FaX3No.Text = row["X3No"].ToString();
                FaX3Name.Text = row["X3Name"].ToString();
            });
        }



        private void FaArea_Validating(object sender, CancelEventArgs e)
        {
            if (FaArea.ReadOnly) return;
            if (!(FaArea.Text.Trim() == "1" || FaArea.Text == "2" || FaArea.Text == "3" || FaArea.Text.Trim() == ""))
            {
                if (btnCancel.Focused) return;
                e.Cancel = true;
                FaArea.SelectAll();
                MessageBox.Show("只能輸入:\t\n\n  1 國內\n  2 國外\n  3 國內外", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void WebID_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (WebID.ReadOnly) return;
            if (WebID.TrimTextLenth() == 0)
            {
                WebID.Clear();
                WebPassWord.Clear();
                return;
            }
            try
            {
                var IsExist = false;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                    cmd.CommandText = "Select Count(*) from Cust where WebID = (@WebID)";
                    IsExist = cmd.ExecuteScalar().ToDecimal() > 0;
                }
                if (IsExist)
                {
                    e.Cancel = true;
                    MessageBox.Show("此網路帳號已被使用，請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WebID.SelectAll();
                    return;
                }

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@FaNo", FaNo.Text);
                    cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                    if (btnState == "Append" || btnState == "Duplicate")
                        cmd.CommandText = "Select Count(*) from Fact where WebID = (@WebID)";
                    if (btnState == "Modify")
                        cmd.CommandText = "Select Count(*) from Fact where FaNo != (@FaNo) And WebID = (@WebID)";
                    IsExist = cmd.ExecuteScalar().ToDecimal() > 0;
                }
                if (IsExist)
                {
                    e.Cancel = true;
                    MessageBox.Show("此網路帳號已被使用，請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WebID.SelectAll();
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FaUno_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (FaUno.ReadOnly) return;

            if (FaUno.TrimTextLenth() == 0)
            {
                FaUno.Clear();
                return;
            }

            if (FaUno.TrimTextLenth() > 0 && FaUno.TrimTextLenth() != 8)
            {
                e.Cancel = true;
                MessageBox.Show("統一編號輸入錯誤!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaUno.SelectAll();
                return;
            }

            if (FaUno.TrimTextLenth() == 8)
            {
                if (FaUno.Text.All(c => Char.IsDigit(c)) == false)
                {
                    e.Cancel = true;
                    MessageBox.Show("統一編號輸入錯誤!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaUno.SelectAll();
                    return;
                }
            }
        }

        public DialogResult ShowDialog(out string cuno)
        {
            var dl = this.ShowDialog();

            cuno = this.tempNo;
            return dl;
        }


        private void Radio_7_8_9_CheckedChanged(object sender, EventArgs e)
        {
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.日期.DataPropertyName = Common.User_DateTime == 1 ? "民國" : "西元";

            string SqlStr = "", SqlStrr = "";

            if (radio7.Checked)
            {
                SqlStr = @"select 來源='詢價',民國=SUBSTRING(fqdate,1,3) + '/'+SUBSTRING(fqdate,4,2)+ '/'+SUBSTRING(fqdate,6,2),西元= SUBSTRING(fqdate1,1,4) + '/'+SUBSTRING(fqdate1,5,2)+ '/'+SUBSTRING(fqdate1,7,2)
                          ,fqno as 單據憑證,itno as 產品編號,itname as 品名規格,qty as 數量,itunit as 單位,price as 進價,prs as 折數 ,mny as 稅前金額  from fquotd   where FaNo = @FaNo order by fqdate desc,fqno desc";
            }
            else if (radio8.Checked)
            {
                SqlStr = @" select 來源='採購',民國=SUBSTRING(fodate,1,3) + '/'+SUBSTRING(fodate,4,2)+ '/'+SUBSTRING(fodate,6,2),西元= SUBSTRING(fodate1,1,4) + '/'+SUBSTRING(fodate1,5,2)+ '/'+SUBSTRING(fodate1,7,2)
                           ,fono as 單據憑證,itno as 產品編號,itname as 品名規格,qty as 數量,itunit as 單位,price as 進價,prs as 折數 ,mny as 稅前金額  from fordd    where FaNo = @FaNo order by fodate desc,fono desc";
            }
            else if (radio9.Checked)
            {
                SqlStr = @" select 來源='進貨',民國=SUBSTRING(bsdate,1,3) + '/'+SUBSTRING(bsdate,4,2)+ '/'+SUBSTRING(bsdate,6,2),西元= SUBSTRING(bsdate1,1,4) + '/'+SUBSTRING(bsdate1,5,2)+ '/'+SUBSTRING(bsdate1,7,2)
                           ,bsno as 單據憑證,itno as 產品編號,itname as 品名規格,qty as 數量,itunit as 單位,price as 進價,prs as 折數 ,mny as 稅前金額  from BShopD   where FaNo = @FaNo order by bsdate desc,bsno desc";
                SqlStrr = @" select 來源='進退',民國=SUBSTRING(bsdate,1,3) + '/'+SUBSTRING(bsdate,4,2)+ '/'+SUBSTRING(bsdate,6,2),西元= SUBSTRING(bsdate1,1,4) + '/'+SUBSTRING(bsdate1,5,2)+ '/'+SUBSTRING(bsdate1,7,2)
                           ,bsno as 單據憑證,itno as 產品編號,itname as 品名規格,qty*-1 as 數量,itunit as 單位,price*-1 as 進價,prs as 折數 ,mny*-1 as 稅前金額  from rShopD   where FaNo = @FaNo order by bsdate desc,bsno desc";
            }
            dataGridViewT1.DataSource = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataGridView_dt.Clear();
                cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                cmd.CommandText = SqlStr;
                da.Fill(DataGridView_dt);
                if (SqlStrr.Length > 0)
                {
                    cmd.CommandText = SqlStrr;
                    da.Fill(DataGridView_dt);
                }
            }
            dataGridViewT1.DataSource = DataGridView_dt;
        }    



    }
}