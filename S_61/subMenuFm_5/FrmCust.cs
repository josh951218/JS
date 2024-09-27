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
    public partial class FrmCust : Formbase
    {
        JBS.JS.Cust jCust;
        JBS.JS.xEvents xe;
        List<TextBoxbase> list = new List<TextBoxbase>();
        SqlTransaction tn;
        string tempNo = "";
        string btnState = string.Empty;
        string BeforeText = "";

        //13.6
        DataTable 相關單據dt = new DataTable();       //相關單據DataSource
        DataTable 指送地址dt = new DataTable();       //指送地址DataSource(新增用)
        string BeforeText_指送地址 = "";
        List<string> Update指送地址 = new List<string>();
        List<string> Insert指送地址 = new List<string>();

        public FrmCust()
        {
            InitializeComponent();
            this.jCust = new JBS.JS.Cust();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();

            CuCredit.FirstNum = Common.nFirst;
            CuXbh.FirstNum = Common.nFirst;
            CuAdvamt.FirstNum = Common.nFirst;

            CuCredit.LastNum = Common.MS;
            CuXbh.LastNum = Common.MS;
            CuAdvamt.LastNum = Common.MS;

            if (Common.Series == "74" || Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
            }
            loadSystemSettings();
        }

        private void FrmCust1_Load(object sender, EventArgs e)
        {
            CuUno.TextAlign = HorizontalAlignment.Left;

            if (Common.Sys_StockKind == 2) tabControl1.SelectTab(2);
            else tabControl1.SelectTab(0);

            //日期格式
            switch (Common.User_DateTime)
            {
                case 1:
                    CuBirth.MaxLength = 7;
                    CuLastday.MaxLength = 7;
                    CuDate.MaxLength = 7;
                    CuLastday_1.MaxLength = 7;
                    CuDate_1.MaxLength = 7;
                    CuDatep.MaxLength = 7;
                    break;
                case 2:
                    CuBirth.MaxLength = 8;
                    CuBirth.MaxLength = 8;
                    CuLastday.MaxLength = 8;
                    CuDate.MaxLength = 8;
                    CuLastday_1.MaxLength = 8;
                    CuDate_1.MaxLength = 8;
                    CuDatep.MaxLength = 8;
                    break;
            }

            writeToTxt(Common.load("Top", "Cust", "Cuno"));

            btnAppend.Focus();
        }

        private void loadSystemSettings()
        {
            //載入系統資訊
            //自定欄位,資料庫有值才改,沒值則預設
            var v = Common.listSysSettings.First().Field<string>("cuudfc1");
            if (v != "" && v != null) lblCuUdf1.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("cuudfc2");
            if (v != "" && v != null) lblCuUdf2.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("cuudfc3");
            if (v != "" && v != null) lblCuUdf3.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("cuudfc4");
            if (v != "" && v != null) lblCuUdf4.Text = v.Trim();
            v = Common.listSysSettings.First().Field<string>("cuudfc5");
            if (v != "" && v != null) lblCuUdf5.Text = v.Trim();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            地址.Width = (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            公司名稱.Width = (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            聯絡人.Width = (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            電話.Width = (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
        }

        private void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                ImNo.Text = row["ImNo"].ToString();
                pVar.InvModeValidate(row["ImNo"].ToString(), ImNo, ImName);
                //表頭
                CuNo.Text = row["cuno"].ToString();
                CuIme.Text = row["cuime"].ToString();
                CuName2.Text = row["cuname2"].ToString();
                CuName1.Text = row["cuname1"].ToString();
                pVar.Xa01Validate(row["cuxa1no"].ToString(), Xa1No, Xa1Name);
                pVar.XX01Validate(row["cux1no"].ToString(), CuX1No, CuX1Name);
                pVar.CuPareValidate(row["cupareno"].ToString(), CuPareNo, CuPareName);
                pVar.EmplValidate(row["cuemno1"].ToString(), CuEmNo1, CuEmName);
                pVar.CuPareValidate(row["payerno"].ToString(), payerno, payername);//請款客戶
                switch (row["einv"].ToString())//是否使用電子發票
                {
                    case "1": 
                        EInv1.Checked = true;
                        lblEInvChange.Visible = CBEInvChange.Visible = true;
                        break;
                    case "2":
                        EInv2.Checked = true;
                        lblEInvChange.Visible = CBEInvChange.Visible = false;
                        break;
                    default: break;
                }
                CBEInvChange.Text = row["einvchange"].ToString();
                SpNo.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["SpName"].ToString().Trim();

                //分頁一
                CuPer1.Text = row["cuper1"].ToString();
                CuPer2.Text = row["cuper2"].ToString();
                CuPer.Text = row["cuper"].ToString();
                CuTel1.Text = row["cutel1"].ToString();
                CuTel2.Text = row["cutel2"].ToString();
                CuFax1.Text = row["cufax1"].ToString();
                CuAtel1.Text = row["cuatel1"].ToString();
                CuAtel2.Text = row["cuatel2"].ToString();
                CuAtel3.Text = row["cuatel3"].ToString();
                CuAddr1.Text = row["cuaddr1"].ToString();
                CuR1.Text = row["cur1"].ToString();
                CuAddr2.Text = row["cuaddr2"].ToString();
                CuR2.Text = row["cur2"].ToString();
                CuAddr3.Text = row["cuaddr3"].ToString();
                CuR3.Text = row["cur3"].ToString();
                //現有預收餘額:dt["CuAdvamt"]
                decimal dtemp = 0;
                CuAdvamt.Text = row["CuAdvamt"].ToString();
                decimal.TryParse(CuAdvamt.Text, out dtemp);
                CuAdvamt.Text = dtemp.ToString("f" + CuAdvamt.LastNum);
                //信用額度
                dtemp = 0;
                CuCredit.Text = row["CuCredit"].ToString();
                decimal.TryParse(CuCredit.Text, out dtemp);
                CuCredit.Text = dtemp.ToString("f" + CuCredit.LastNum);
                //計算信用額度的餘額：
                //餘額[CuXbh] = 信用額度[CuCredit]-現有應收帳款["Cureceiv"]
                dtemp = 0;
                decimal dcredit = 0, dreceiv = 0;
                decimal.TryParse(row["CuCredit"].ToString(), out dcredit);
                decimal.TryParse(row["Cureceiv"].ToString(), out dreceiv);
                dtemp = dcredit - dreceiv;
                CuXbh.Text = dtemp.ToString("f" + CuXbh.LastNum);
                Nopay.Text = dreceiv.ToString("f"+CuXbh.LastNum);
                //建議售價
                switch (row["cuslevel"].ToString())
                {
                    case "1":
                        radioT1.Checked = true;
                        radioT11.Checked = true; break;
                    case "2":
                        radioT2.Checked = true;
                        radioT12.Checked = true; break;
                    case "3":
                        radioT3.Checked = true;
                        radioT13.Checked = true; break;
                    case "4":
                        radioT4.Checked = true;
                        radioT14.Checked = true; break;
                    case "5":
                        radioT5.Checked = true;
                        radioT15.Checked = true; break;
                    case "6":
                        radioT6.Checked = true;
                        radioT16.Checked = true; break;
                    default:
                        radioT6.Checked = true;
                        radioT16.Checked = true; break;
                }
                CuDisc.Text = row["cudisc"].ToString();
                CuDisc1.Text = row["cudisc"].ToString();
                CuEmail.Text = row["cuemail"].ToString();
                CuWww.Text = row["cuwww"].ToString();
                CuUno.Text = row["cuuno"].ToString();
                pVar.XX03Validate(row["CuX3No"].ToString(), CuX3No, CuX3Name);
                pVar.XX03Validate(row["CuX3No"].ToString(), CuX3No1, CuX3Name1);
                pVar.XX04Validate(row["CuX4No"].ToString(), CuX4No, CuX4Name);
                pVar.XX04Validate(row["CuX4No"].ToString(), CuX4No1, CuX4Name1);
                pVar.XX05Validate(row["CuX5No"].ToString(), CuX5No, CuX5Name);
                pVar.XX05Validate(row["CuX5No"].ToString(), CuX5No1, CuX5Name1);
                //分頁二
                CuEngname.Text = row["cuengname"].ToString();
                CuEngaddr.Text = row["cuengaddr"].ToString();
                CuEngr1.Text = row["cuengr1"].ToString();
                CuMemo1.Text = row["cumemo1"].ToString();
                CuInvoName.Text = row["cuinvoname"].ToString();
                CuArea.Text = row["cuarea"].ToString();
                CuUdf1.Text = row["cuudf1"].ToString();
                CuUdf2.Text = row["cuudf2"].ToString();
                CuUdf3.Text = row["cuudf3"].ToString();
                CuUdf4.Text = row["cuudf4"].ToString();
                CuUdf5.Text = row["cuudf5"].ToString();
                
                pVar.XX02Validate(row["cux2no"].ToString(), CuX2No, CuX2Name);
                if (Common.User_DateTime == 1)
                {
                    CuDate.Text = row["CuDate"].ToString();
                    CuDate_1.Text = row["CuDate"].ToString();
                    CuLastday.Text = row["CuLastday"].ToString();
                    CuLastday_1.Text = row["CuLastday"].ToString();
                    CuBirth.Text = row["cubirth"].ToString();
                }
                else
                {
                    CuDate.Text = row["CuDate1"].ToString();
                    CuDate_1.Text = row["CuDate1"].ToString();
                    CuLastday.Text = row["CuLastday1"].ToString();
                    CuLastday_1.Text = row["CuLastday1"].ToString();
                    CuBirth.Text = row["cubirth1"].ToString();
                }
                //分頁三
                CuIdNo.Text = row["cuidno"].ToString();
                CuSex.Text = row["cusex"].ToString();
                CuPer1_1.Text = row["cuper1"].ToString();
                CuBlood.Text = row["cublood"].ToString();
                CuTel1_1.Text = row["cutel1"].ToString();
                CuAtel1_1.Text = row["cuatel1"].ToString();
                CuFax1_1.Text = row["cufax1"].ToString();
                CuAddr1_1.Text = row["cuaddr1"].ToString();
                CuR1_1.Text = row["cur1"].ToString();
                CuEmail_1.Text = row["cuemail"].ToString();
                CuWww_1.Text = row["cuwww"].ToString();
                CuMemo1_1.Text = row["cumemo1"].ToString();
                CuMemo2_1.Text = row["cumemo2"].ToString();
                CuDatep.Text = row["CuDatep"].ToString();
                CuPoint.Text = row["CuPoint"].ToDecimal().ToString("f0");
                WebID.Text = row["WebID"].ToString();
                WebPassWord.Text = row["WebPassWord"].ToString();
                SeekNo = CuNo.Text;
                //分頁四
                DetailMemo.Text = row["DetailMemo"].ToString();
            }
            else
            {
                SeekNo = "";
                DetailMemo.Clear();
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
            Radio_7_8_9_CheckedChanged(null,null);
            指送地址Function("load",null,null);
        }

        private void writeToTxt(SqlDataReader row)
        {
            if (row != null)
            {
                ImNo.Text = row["ImNo"].ToString();
                pVar.InvModeValidate(row["ImNo"].ToString(), ImNo, ImName);
                //表頭
                CuNo.Text = row["cuno"].ToString();
                CuIme.Text = row["cuime"].ToString();
                CuName2.Text = row["cuname2"].ToString();
                CuName1.Text = row["cuname1"].ToString();
                pVar.Xa01Validate(row["cuxa1no"].ToString(), Xa1No, Xa1Name);
                pVar.XX01Validate(row["cux1no"].ToString(), CuX1No, CuX1Name);
                pVar.CuPareValidate(row["cupareno"].ToString(), CuPareNo, CuPareName);
                pVar.EmplValidate(row["cuemno1"].ToString(), CuEmNo1, CuEmName);
                switch (row["einv"].ToString())//是否使用電子發票
                {
                    case "1": EInv1.Checked = true; break;
                    case "2": EInv2.Checked = true; break;
                    default: break;
                }
                CBEInvChange.Text = row["einvchange"].ToString();
                //分頁一
                CuPer1.Text = row["cuper1"].ToString();
                CuPer2.Text = row["cuper2"].ToString();
                CuPer.Text = row["cuper"].ToString();
                CuTel1.Text = row["cutel1"].ToString();
                CuTel2.Text = row["cutel2"].ToString();
                CuFax1.Text = row["cufax1"].ToString();
                CuAtel1.Text = row["cuatel1"].ToString();
                CuAtel2.Text = row["cuatel2"].ToString();
                CuAtel3.Text = row["cuatel3"].ToString();
                CuAddr1.Text = row["cuaddr1"].ToString();
                CuR1.Text = row["cur1"].ToString();
                CuAddr2.Text = row["cuaddr2"].ToString();
                CuR2.Text = row["cur2"].ToString();
                CuAddr3.Text = row["cuaddr3"].ToString();
                CuR3.Text = row["cur3"].ToString();
                //現有預收餘額:dt["CuAdvamt"]
                decimal dtemp = 0;
                CuAdvamt.Text = row["CuAdvamt"].ToString();
                decimal.TryParse(CuAdvamt.Text, out dtemp);
                CuAdvamt.Text = dtemp.ToString("f" + CuAdvamt.LastNum);
                //信用額度
                dtemp = 0;
                CuCredit.Text = row["CuCredit"].ToString();
                decimal.TryParse(CuCredit.Text, out dtemp);
                CuCredit.Text = dtemp.ToString("f" + CuCredit.LastNum);
                //計算信用額度的餘額：
                //餘額[CuXbh] = 信用額度[CuCredit]-現有應收帳款["Cureceiv"]
                dtemp = 0;
                decimal dcredit = 0, dreceiv = 0;
                decimal.TryParse(row["CuCredit"].ToString(), out dcredit);
                decimal.TryParse(row["Cureceiv"].ToString(), out dreceiv);
                dtemp = dcredit - dreceiv;
                CuXbh.Text = dtemp.ToString("f" + CuXbh.LastNum);
                //建議售價
                switch (row["cuslevel"].ToString())
                {
                    case "1":
                        radioT1.Checked = true;
                        radioT11.Checked = true; break;
                    case "2":
                        radioT2.Checked = true;
                        radioT12.Checked = true; break;
                    case "3":
                        radioT3.Checked = true;
                        radioT13.Checked = true; break;
                    case "4":
                        radioT4.Checked = true;
                        radioT14.Checked = true; break;
                    case "5":
                        radioT5.Checked = true;
                        radioT15.Checked = true; break;
                    case "6":
                        radioT6.Checked = true;
                        radioT16.Checked = true; break;
                    default:
                        radioT6.Checked = true;
                        radioT16.Checked = true; break;
                }
                CuDisc.Text = row["cudisc"].ToString();
                CuDisc1.Text = row["cudisc"].ToString();
                CuEmail.Text = row["cuemail"].ToString();
                CuWww.Text = row["cuwww"].ToString();
                CuUno.Text = row["cuuno"].ToString();
                pVar.XX03Validate(row["CuX3No"].ToString(), CuX3No, CuX3Name);
                pVar.XX03Validate(row["CuX3No"].ToString(), CuX3No1, CuX3Name1);
                pVar.XX04Validate(row["CuX4No"].ToString(), CuX4No, CuX4Name);
                pVar.XX04Validate(row["CuX4No"].ToString(), CuX4No1, CuX4Name1);
                pVar.XX05Validate(row["CuX5No"].ToString(), CuX5No, CuX5Name);
                pVar.XX05Validate(row["CuX5No"].ToString(), CuX5No1, CuX5Name1);
                //分頁二
                CuEngname.Text = row["cuengname"].ToString();
                CuEngaddr.Text = row["cuengaddr"].ToString();
                CuEngr1.Text = row["cuengr1"].ToString();
                CuMemo1.Text = row["cumemo1"].ToString();
                CuInvoName.Text = row["cuinvoname"].ToString();
                CuArea.Text = row["cuarea"].ToString();
                CuUdf1.Text = row["cuudf1"].ToString();
                CuUdf2.Text = row["cuudf2"].ToString();
                CuUdf3.Text = row["cuudf3"].ToString();
                CuUdf4.Text = row["cuudf4"].ToString();
                CuUdf5.Text = row["cuudf5"].ToString();
                
                pVar.XX02Validate(row["cux2no"].ToString(), CuX2No, CuX2Name);
                if (Common.User_DateTime == 1)
                {
                    CuDate.Text = row["CuDate"].ToString();
                    CuDate_1.Text = row["CuDate"].ToString();
                    CuLastday.Text = row["CuLastday"].ToString();
                    CuLastday_1.Text = row["CuLastday"].ToString();
                    CuBirth.Text = row["cubirth"].ToString();
                }
                else
                {
                    CuDate.Text = row["CuDate1"].ToString();
                    CuDate_1.Text = row["CuDate1"].ToString();
                    CuLastday.Text = row["CuLastday1"].ToString();
                    CuLastday_1.Text = row["CuLastday1"].ToString();
                    CuBirth.Text = row["cubirth1"].ToString();
                }
                //分頁三
                CuIdNo.Text = row["cuidno"].ToString();
                CuSex.Text = row["cusex"].ToString();
                CuPer1_1.Text = row["cuper1"].ToString();
                CuBlood.Text = row["cublood"].ToString();
                CuTel1_1.Text = row["cutel1"].ToString();
                CuAtel1_1.Text = row["cuatel1"].ToString();
                CuFax1_1.Text = row["cufax1"].ToString();
                CuAddr1_1.Text = row["cuaddr1"].ToString();
                CuR1_1.Text = row["cur1"].ToString();
                CuEmail_1.Text = row["cuemail"].ToString();
                CuWww_1.Text = row["cuwww"].ToString();
                CuMemo1_1.Text = row["cumemo1"].ToString();
                CuMemo2_1.Text = row["cumemo2"].ToString();
                CuDatep.Text = row["CuDatep"].ToString();
                CuPoint.Text = row["CuPoint"].ToDecimal().ToString("f0");

                WebID.Text = row["WebID"].ToString();
                WebPassWord.Text = row["WebPassWord"].ToString();

                pVar.CuPareValidate(row["payerno"].ToString(), payerno, payername);//請款客戶

                SeekNo = CuNo.Text;
                SpNo.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["SpName"].ToString().Trim();
                DetailMemo.Text = row["DetailMemo"].ToString();
            }
            else
            {
                SeekNo = "";
                DetailMemo.Clear();
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
            Radio_7_8_9_CheckedChanged(null, null);
            指送地址Function("load", null, null);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Top", "Cust", "CuNo"));
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "Cust", "CuNo", CuNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "Cust", "CuNo", CuNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            var row = Common.load("Next", "Cust", "CuNo", CuNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "Cust", "CuNo", CuNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Bottom", "Cust", "CuNo", CuNo.Text.Trim()));
        }

        void setTxtWhenAppend()
        {
            //新增一筆資料時,載入欄位預設值
            pVar.Xa01Validate("TWD", Xa1No, Xa1Name);
            pVar.XX03Validate("1", CuX3No, CuX3Name);
            pVar.XX03Validate("1", CuX3No1, CuX3Name1);
            pVar.XX05Validate("1", CuX5No, CuX5Name);
            pVar.XX05Validate("1", CuX5No1, CuX5Name1);

            CuArea.Text = "1";
            radioT6.Checked = true;
            radioT16.Checked = true;
            CuDisc.Text = "1.000";
            CuDisc1.Text = "1.000";


            switch (Common.User_DateTime)
            {
                case 1:
                    CuDate.Text = Date.GetDateTime(1, false);
                    CuDate_1.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    CuDate.Text = Date.GetDateTime(2, false);
                    CuDate_1.Text = Date.GetDateTime(2, false);
                    break;
            }
            CuCredit.Text = (0M).ToString("f" + Common.MF);
            CuXbh.Text = (0M).ToString("f" + Common.MF);
            CuAdvamt.Text = (0M).ToString("f" + Common.MF);

            EInv2.Checked = true;//電子發票
            CBEInvChange.Text = "";
            dataGridViewT1.DataSource = null;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = CuNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            DetailMemo.Clear();
            btnState = ((Button)sender).Name.Substring(3);

            setTxtWhenAppend();
            CuNo.Focus();
            指送地址Function("NewCust",null,null);
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            tempNo = CuNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            switch (Common.User_DateTime)
            {
                case 1:
                    CuDate.Text = Date.GetDateTime(1, false);
                    CuDate_1.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    CuDate.Text = Date.GetDateTime(2, false);
                    CuDate_1.Text = Date.GetDateTime(2, false);
                    break;
            }
            CuPoint.Text = "0";
            WebID.Clear();
            WebPassWord.Clear();

            CuNo.SelectAll();
            CuNo.Focus();
            控制指送地址Column編輯狀態(true);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            tempNo = CuNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);
            if(!稽核此客戶是否能刪除或者修改幣別("修改幣別"))
                Xa1No.ReadOnly = true;
            CuNo.Focus();
            CuNo.SelectAll();
            控制指送地址Column編輯狀態(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CuNo.Text == "") return;
            btnState = "Delete";
            try
            {
                if (!稽核此客戶是否能刪除或者修改幣別("刪除客戶"))
                    return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                    cmd.CommandText = "delete from Cust where CuNo=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();
                    指送地址Function("delete",cn,cmd);
                }
                btnNext_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool 稽核此客戶是否能刪除或者修改幣別(string state="")
        {
             using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
             using (SqlCommand cmd = cn.CreateCommand())
             {
                 cn.Open();
                 cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                 cmd.CommandText = "select count(*) from sale where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                 if (cmd.ExecuteScalar().ToString() != "0")
                 {
                     if(state=="刪除客戶")
                        MessageBox.Show("此客戶已有帳款資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
                 cmd.CommandText = "select count(*) from rsale where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                 if (cmd.ExecuteScalar().ToString() != "0")
                 {
                     if (state == "刪除客戶")
                        MessageBox.Show("此客戶已有帳款資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
                 cmd.CommandText = "select count(*) from receiv where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                 if (cmd.ExecuteScalar().ToString() != "0")
                 {
                     if (state == "刪除客戶")
                        MessageBox.Show("此客戶已有帳款資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
                 cmd.CommandText = "select count(*) from instk where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                 if (cmd.ExecuteScalar().ToString() != "0")
                 {
                     if (state == "刪除客戶")
                         MessageBox.Show("此客戶已有寄庫資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
                 cmd.CommandText = "select count(*) from lend where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                 if (cmd.ExecuteScalar().ToString() != "0")
                 {
                     if (state == "刪除客戶")
                         MessageBox.Show("此客戶已有借出還入無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
                 cmd.CommandText = "select count(*) from rlend where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                 if (cmd.ExecuteScalar().ToString() != "0")
                 {
                     if (state == "刪除客戶")
                         MessageBox.Show("此客戶已有借出還入無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
                 return true;
             }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCustPrint())
            {
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            using (var frm = new FrmCustb())
            {
                frm.TSeekNo = CuNo.Text.Trim();
                var dl = frm.ShowDialog(this);
                if (dl == DialogResult.OK)
                {
                    xe.Validate<JBS.JS.Cust>(frm.TResult, reader => writeToTxt(reader));
                }
                else if (dl == DialogResult.Yes)
                {
                    btnTop_Click(null, null);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (xe.IsRegisted("cust") == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！\n";
                msg += "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           if (CuNo.Text == string.Empty)//儲存或修改時，CuNo不能為空值
            {
                MessageBox.Show("客戶編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuNo.Focus();
                return;
            }
            else
            {
                //新增，複製，修改時，折數與信用額度幫填入0值
                try
                {
                    CuDisc1.Text = Convert.ToDecimal(CuDisc1.Text).ToString();
                }
                catch { CuDisc1.Text = "0"; }
                try
                {
                    CuCredit.Text = Convert.ToDecimal(CuCredit.Text).ToString();
                }
                catch { CuCredit.Text = "0"; }
            }

            if (Xa1No.Text.Trim() == "")
            {
                MessageBox.Show("幣別編號不可為空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Xa1No.Focus();
                return;
            }
            if (CuX3No1.Text.Trim() == "")
            {
                MessageBox.Show("營業稅編號不可為空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuX3No1.Focus();
                return;
            }
            if (CuX5No1.Text.Trim() == "")
            {
                MessageBox.Show("發票模式編號不可為空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuX5No1.Focus();
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
            if (EInv1.Checked && CuUno.TrimTextLenth() == 0)//使用電子發票
            {
                CuUno.Text = "";
                MessageBox.Show("客戶使用電子發票\n統一編號資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuUno.Focus();
                return;
            }

            var machine = "";
            //if (Common.Sys_StockKind == 1)
            //{
            //    machine = "";
            //}
            //else
            //{
            //    if (File.Exists(Common.StartUpIniFileName))
            //    {
            //        //唯有pos系統，且是多點情況下，機台號碼存1號機做傳輸用
            //        var SIP = Common.GetPrivateProfileString("POS設定", "SERVER");
            //        machine = "1";
            //        if (SIP.Trim() == "-1") machine = "";
            //        if (Common.Series.ToDecimal() == 71) machine = "";
            //    }
            //    else
            //    {
            //        machine = "";
            //        MessageBox.Show("設定檔遺失！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}

            if (btnState == "Append")
            {
                if (Common.load("Check", "Cust", "Cuno", CuNo.Text.Trim()) != null)
                {
                    MessageBox.Show("此客戶編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuNo.Text = string.Empty;
                    CuNo.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();

                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuname2", CuName2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuname1", CuName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuinvoname", CuInvoName.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuchkname", "支票抬頭");
                        cmd.Parameters.AddWithValue("@cuxa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cupareno", CuPareNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cucono", "T");
                        cmd.Parameters.AddWithValue("@cuime", CuIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux1no", CuX1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuemno1", CuEmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper1", CuPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper2", CuPer2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper", CuPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@cutel1", CuTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cutel2", CuTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cufax1", CuFax1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel1", CuAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel2", CuAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel3", CuAtel3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cubbc", "BBC");
                        cmd.Parameters.AddWithValue("@cuaddr1", CuAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur1", CuR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuaddr2", CuAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur2", CuR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuaddr3", CuAddr3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur3", CuR3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuslevel", getRadioNumber(groupBoxT1));
                        cmd.Parameters.AddWithValue("@cudisc", CuDisc1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuemail", CuEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuwww", CuWww.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux2no", CuX2No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuuno", CuUno.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux3no", CuX3No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux4no", CuX4No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cucredit", CuCredit.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengname", CuEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengaddr", CuEngaddr.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengr1", CuEngr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cumemo1", CuMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cumemo2", CuMemo2_1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux5no", CuX5No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuarea", CuArea.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf1", CuUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf2", CuUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf3", CuUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf4", CuUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf5", CuUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf6", "保留");
                        cmd.Parameters.AddWithValue("@cudate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@cudate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@cudate2", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@culastday", "");
                        cmd.Parameters.AddWithValue("@culastday1", "");
                        cmd.Parameters.AddWithValue("@culastday2", "");
                        cmd.Parameters.AddWithValue("@cufirrcvpar", "1");
                        cmd.Parameters.AddWithValue("@cunote", "備忘錄");
                        cmd.Parameters.AddWithValue("@cubirth", Date.ToTWDate(CuBirth.Text));
                        cmd.Parameters.AddWithValue("@cubirth1", Date.ToUSDate(CuBirth.Text));
                        cmd.Parameters.AddWithValue("@cubirth2", CuBirth.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuDatep", Date.ToTWDate(CuDatep.Text));
                        cmd.Parameters.AddWithValue("@CuDatep1", Date.ToUSDate(CuDatep.Text));
                        cmd.Parameters.AddWithValue("@CuDatep2", CuDatep.Text.Trim());
                        cmd.Parameters.AddWithValue("@cusex", CuSex.Text.Trim());
                        cmd.Parameters.AddWithValue("@cublood", CuBlood.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuidno", CuIdNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsTrans", machine);
                        cmd.Parameters.AddWithValue("@CuPoint", "0");
                        cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebPassWord", WebPassWord.Text.Trim());
                        cmd.Parameters.AddWithValue("@ImNo", ImNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@einv", getRadioNumber(pEInv));//是否有電子發票
                        cmd.Parameters.AddWithValue("@einvchange", EInv1.Checked?CBEInvChange.Text:"");//是否有電子發票
                        cmd.Parameters.AddWithValue("@payerno", payerno.Text.Trim());//請款客戶編號
                        cmd.Parameters.AddWithValue("@SpNo", SpNo.Text.Trim());//專案編號
                        cmd.Parameters.AddWithValue("@SpName", SpName.Text.Trim());//專案編號
                        cmd.Parameters.AddWithValue("@detailmemo", DetailMemo.Text);
                        
                        cmd.CommandText = "INSERT INTO Cust"
                            + "(cuno,cuname2,cuname1,cuinvoname,cuchkname"
                            + ",cuxa1no,cupareno,cucono,cuime,cux1no"
                            + ",cuemno1,cuper1,cuper2,cuper,cutel1"
                            + ",cutel2,cufax1,cuatel1,cuatel2,cuatel3"
                            + ",cubbc,cuaddr1,cur1,cuaddr2,cur2"
                            + ",cuaddr3,cur3,cuslevel,cudisc,cuemail"
                            + ",cuwww,cux2no,cuuno,cux3no,cux4no"
                            + ",cucredit,cuengname,cuengaddr,cuengr1,cumemo1"
                            + ",cumemo2,cux5no,cuarea,cuudf1,cuudf2"
                            + ",cuudf3,cuudf4,cuudf5,cuudf6,cudate"
                            + ",cudate1,cudate2,culastday,culastday1,culastday2"
                            + ",cufirrcvpar"
                            //+ ",cufirreceiv,cusparercv,cureceiv,cufiradvamt,cuadvamt"
                            + ",cunote,cubirth,cubirth1,cubirth2,CuDatep,CuDatep1,CuDatep2"
                            + ",cusex,cublood,cuidno,IsTrans,CuPoint,WebID,WebPassWord,ImNo,einv,einvchange,payerno,SpNo,SpName,detailmemo) VALUES ("
                            //+ CuNo.Text.Trim() + "',N'" + CuName2.Text + "',N'" + CuName1.Text + "',N'" + CuInvoName.Text + "','支票抬頭',N'"
                            //+ Xa1No.Text + "',N'" + CuPareNo.Text + "','T',N'" + CuIme.Text + "',N'" + CuX1No.Text + "',N'"
                            //+ CuEmNo1.Text + "',N'" + CuPer1.Text + "',N'" + CuPer2.Text + "',N'" + CuPer.Text + "',N'" + CuTel1.Text + "',N'"
                            //+ CuTel2.Text + "',N'" + CuFax1.Text + "',N'" + CuAtel1.Text + "',N'" + CuAtel2.Text + "',N'" + CuAtel3.Text
                            //+ "','BBC',N'" + CuAddr1.Text + "',N'" + CuR1.Text + "',N'" + CuAddr2.Text + "',N'" + CuR2.Text + "',N'"
                            //+ CuAddr3.Text + "',N'" + CuR3.Text + "'," + getRadioNumber(pnlCuSlevel1) + "," + CuDisc1.Text + ",N'" + CuEmail.Text + "',N'"
                            //+ CuWww.Text + "',N'" + CuX2No.Text + "',N'" + CuUno.Text + "',N'" + CuX3No1.Text + "',N'" + CuX4No1.Text + "',"
                            //+ CuCredit.Text + ",N'" + CuEngname.Text + "',N'" + CuEngaddr.Text + "',N'" + CuEngr1.Text + "',N'" + CuMemo1.Text + "',N'"
                            //+ CuMemo2_1.Text + "',N'" + CuX5No1.Text + "',N'" + CuArea.Text + "',N'" + CuUdf1.Text + "',N'" + CuUdf2.Text + "',N'"
                            //+ CuUdf3.Text + "',N'" + CuUdf4.Text + "',N'" + CuUdf5.Text + "','保留',N'" + Model.Date.GetDateTime(1, false)
                            //+ "',N'" + Model.Date.GetDateTime(2, false) + "',N'" + Model.Date.GetDateTime(2, false) + "','','','',1"
                            //+ ",0,0,0,0,0"
                            //+ ",'備忘錄',N'" + Date.ToTWDate(CuBirth.Text) + "',N'" + Date.ToUSDate(CuBirth.Text) + "',N'" + CuBirth.Text
                            //+ "',N'" + Date.ToTWDate(CuDatep.Text) + "',N'" + Date.ToUSDate(CuDatep.Text) + "',N'" + CuDatep.Text
                            //+ "',N'" + CuSex.Text + "',N'" + CuBlood.Text + "',N'" + CuIdNo.Text + "',N'" + machine + "',0)";

                            + "(@cuno),(@cuname2),(@cuname1),(@cuinvoname),(@cuchkname),(@cuxa1no),(@cupareno),(@cucono),(@cuime),(@cux1no)"
                            + ",(@cuemno1),(@cuper1),(@cuper2),(@cuper),(@cutel1),(@cutel2),(@cufax1),(@cuatel1),(@cuatel2),(@cuatel3),(@cubbc)"
                            + ",(@cuaddr1),(@cur1),(@cuaddr2),(@cur2),(@cuaddr3),(@cur3),(@cuslevel),(@cudisc),(@cuemail),(@cuwww),(@cux2no),(@cuuno)"
                            + ",(@cux3no),(@cux4no),(@cucredit),(@cuengname),(@cuengaddr),(@cuengr1),(@cumemo1),(@cumemo2),(@cux5no),(@cuarea)"
                            + ",(@cuudf1),(@cuudf2),(@cuudf3),(@cuudf4),(@cuudf5),(@cuudf6),(@cudate),(@cudate1),(@cudate2)"
                            + ",(@culastday),(@culastday1)"
                            + ",(@culastday2),(@cufirrcvpar)"
                            // + "','','','',1'"
                            + ",(@cunote),(@cubirth),(@cubirth1),(@cubirth2),(@CuDatep),(@CuDatep1),(@CuDatep2)"
                            + ",(@cusex),(@cublood),(@cuidno),(@IsTrans),(@CuPoint),(@WebID),(@WebPassWord),(@ImNo),(@einv),(@einvchange),(@payerno)"
                            + ",(@SpNo),(@SpName),(@detailmemo) "
                            + ")";

                        cmd.ExecuteNonQuery();
                        指送地址Function("Save", cn,cmd);
                        tn.Commit(); tn.Dispose();
                        cmd.Dispose();
                    }

                    this.SeekNo = tempNo = CuNo.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                    DetailMemo.Clear();
                    FormState = FormEditState.Append;
                    CuNo.Focus();
                    setTxtWhenAppend();
                }
                catch (Exception ex)
                {
                    tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }

            if (btnState == "Duplicate")
            {
                if (Common.load("Check", "Cust", "Cuno", CuNo.Text.Trim()) != null)
                {
                    MessageBox.Show("此客戶編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuNo.Text = string.Empty;
                    CuNo.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        tn = conn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuname2", CuName2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuname1", CuName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuinvoname", CuInvoName.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuchkname", "支票抬頭");
                        cmd.Parameters.AddWithValue("@cuxa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cupareno", CuPareNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cucono", "T");
                        cmd.Parameters.AddWithValue("@cuime", CuIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux1no", CuX1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuemno1", CuEmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper1", CuPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper2", CuPer2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper", CuPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@cutel1", CuTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cutel2", CuTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cufax1", CuFax1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel1", CuAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel2", CuAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel3", CuAtel3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cubbc", "BBC");
                        cmd.Parameters.AddWithValue("@cuaddr1", CuAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur1", CuR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuaddr2", CuAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur2", CuR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuaddr3", CuAddr3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur3", CuR3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuslevel", getRadioNumber(groupBoxT1));
                        cmd.Parameters.AddWithValue("@cudisc", CuDisc1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuemail", CuEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuwww", CuWww.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux2no", CuX2No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuuno", CuUno.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux3no", CuX3No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux4no", CuX4No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cucredit", CuCredit.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengname", CuEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengaddr", CuEngaddr.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengr1", CuEngr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cumemo1", CuMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cumemo2", CuMemo2_1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux5no", CuX5No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuarea", CuArea.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf1", CuUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf2", CuUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf3", CuUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf4", CuUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf5", CuUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf6", "保留");
                        cmd.Parameters.AddWithValue("@cudate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@cudate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@cudate2", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@culastday", "");
                        cmd.Parameters.AddWithValue("@culastday1", "");
                        cmd.Parameters.AddWithValue("@culastday2", "");
                        cmd.Parameters.AddWithValue("@cufirrcvpar", "1");
                        cmd.Parameters.AddWithValue("@cunote", "備忘錄");
                        cmd.Parameters.AddWithValue("@cubirth", Date.ToTWDate(CuBirth.Text));
                        cmd.Parameters.AddWithValue("@cubirth1", Date.ToUSDate(CuBirth.Text));
                        cmd.Parameters.AddWithValue("@cubirth2", CuBirth.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuDatep", Date.ToTWDate(CuDatep.Text));
                        cmd.Parameters.AddWithValue("@CuDatep1", Date.ToUSDate(CuDatep.Text));
                        cmd.Parameters.AddWithValue("@CuDatep2", CuDatep.Text.Trim());
                        cmd.Parameters.AddWithValue("@cusex", CuSex.Text.Trim());
                        cmd.Parameters.AddWithValue("@cublood", CuBlood.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuidno", CuIdNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsTrans", machine);
                        cmd.Parameters.AddWithValue("@CuPoint", "0");
                        cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebPassWord", WebPassWord.Text.Trim());
                        cmd.Parameters.AddWithValue("@ImNo", ImNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@einv", getRadioNumber(pEInv));//是否有電子發票
                        cmd.Parameters.AddWithValue("@einvchange", EInv1.Checked ? CBEInvChange.Text : "");//是否有電子發票
                        cmd.Parameters.AddWithValue("@payerno", payerno.Text.Trim());//請款客戶編號
                        cmd.Parameters.AddWithValue("@SpNo", SpNo.Text.Trim());//專案編號
                        cmd.Parameters.AddWithValue("@SpName", SpName.Text.Trim());//專案編號
                        cmd.Parameters.AddWithValue("@detailmemo", DetailMemo.Text);
                        cmd.CommandText = "INSERT INTO Cust"
                            + "(cuno,cuname2,cuname1,cuinvoname,cuchkname"
                            + ",cuxa1no,cupareno,cucono,cuime,cux1no"
                            + ",cuemno1,cuper1,cuper2,cuper,cutel1"
                            + ",cutel2,cufax1,cuatel1,cuatel2,cuatel3"
                            + ",cubbc,cuaddr1,cur1,cuaddr2,cur2"
                            + ",cuaddr3,cur3,cuslevel,cudisc,cuemail"
                            + ",cuwww,cux2no,cuuno,cux3no,cux4no"
                            + ",cucredit,cuengname,cuengaddr,cuengr1,cumemo1"
                            + ",cumemo2,cux5no,cuarea,cuudf1,cuudf2"
                            + ",cuudf3,cuudf4,cuudf5,cuudf6,cudate"
                            + ",cudate1,cudate2,culastday,culastday1,culastday2"
                            + ",cufirrcvpar"
                            //+ ",cufirreceiv,cusparercv,cureceiv,cufiradvamt,cuadvamt"
                            + ",cunote,cubirth,cubirth1,cubirth2,CuDatep,CuDatep1,CuDatep2"
                            + ",cusex,cublood,cuidno,IsTrans,CuPoint,WebID,WebPassWord,ImNo,einv,einvchange,payerno,SpNo,SpName,detailmemo) VALUES ("
                            //+ CuNo.Text.Trim() + "',N'" + CuName2.Text + "',N'" + CuName1.Text + "',N'" + CuInvoName.Text + "','支票抬頭',N'"
                            //+ Xa1No.Text + "',N'" + CuPareNo.Text + "','T',N'" + CuIme.Text + "',N'" + CuX1No.Text + "',N'"
                            //+ CuEmNo1.Text + "',N'" + CuPer1.Text + "',N'" + CuPer2.Text + "',N'" + CuPer.Text + "',N'" + CuTel1.Text + "',N'"
                            //+ CuTel2.Text + "',N'" + CuFax1.Text + "',N'" + CuAtel1.Text + "',N'" + CuAtel2.Text + "',N'" + CuAtel3.Text
                            //+ "','BBC',N'" + CuAddr1.Text + "',N'" + CuR1.Text + "',N'" + CuAddr2.Text + "',N'" + CuR2.Text + "',N'"
                            //+ CuAddr3.Text + "',N'" + CuR3.Text + "'," + getRadioNumber(pnlCuSlevel1) + "," + CuDisc1.Text + ",N'" + CuEmail.Text + "',N'"
                            //+ CuWww.Text + "',N'" + CuX2No.Text + "',N'" + CuUno.Text + "',N'" + CuX3No1.Text + "',N'" + CuX4No1.Text + "',"
                            //+ CuCredit.Text + ",N'" + CuEngname.Text + "',N'" + CuEngaddr.Text + "',N'" + CuEngr1.Text + "',N'" + CuMemo1.Text + "',N'"
                            //+ CuMemo2_1.Text + "',N'" + CuX5No1.Text + "',N'" + CuArea.Text + "',N'" + CuUdf1.Text + "',N'" + CuUdf2.Text + "',N'"
                            //+ CuUdf3.Text + "',N'" + CuUdf4.Text + "',N'" + CuUdf5.Text + "','保留',N'" + Model.Date.GetDateTime(1, false)
                            //+ "',N'" + Model.Date.GetDateTime(2, false) + "',N'" + Model.Date.GetDateTime(2, false) + "','','','',1"
                            ////+ ",0,0,0,0,0"
                            //+ ",'備忘錄',N'" + Date.ToTWDate(CuBirth.Text) + "',N'" + Date.ToUSDate(CuBirth.Text) + "',N'" + CuBirth.Text
                            //+ "',N'" + Date.ToTWDate(CuDatep.Text) + "',N'" + Date.ToUSDate(CuDatep.Text) + "',N'" + CuDatep.Text
                            //+ "',N'" + CuSex.Text + "',N'" + CuBlood.Text + "',N'" + CuIdNo.Text + "',N'" + machine + "',0)";

                            + "(@cuno),(@cuname2),(@cuname1),(@cuinvoname),(@cuchkname),(@cuxa1no),(@cupareno),(@cucono),(@cuime),(@cux1no)"
                            + ",(@cuemno1),(@cuper1),(@cuper2),(@cuper),(@cutel1),(@cutel2),(@cufax1),(@cuatel1),(@cuatel2),(@cuatel3),(@cubbc)"
                            + ",(@cuaddr1),(@cur1),(@cuaddr2),(@cur2),(@cuaddr3),(@cur3),(@cuslevel),(@cudisc),(@cuemail),(@cuwww),(@cux2no),(@cuuno)"
                            + ",(@cux3no),(@cux4no),(@cucredit),(@cuengname),(@cuengaddr),(@cuengr1),(@cumemo1),(@cumemo2),(@cux5no),(@cuarea)"
                            + ",(@cuudf1),(@cuudf2),(@cuudf3),(@cuudf4),(@cuudf5),(@cuudf6),(@cudate),(@cudate1),(@cudate2)"
                            + ",(@culastday),(@culastday1)"
                            + ",(@culastday2),(@cufirrcvpar)"
                            // + "','','','',1'"
                            + ",(@cunote),(@cubirth),(@cubirth1),(@cubirth2),(@CuDatep),(@CuDatep1),(@CuDatep2)"
                            + ",(@cusex),(@cublood),(@cuidno),(@IsTrans),(@CuPoint),(@WebID),(@WebPassWord),(@ImNo),(@einv),(@einvchange),(@payerno)"
                            + ",(@SpNo),(@SpName),(@detailmemo)"
                            + ")";

                        cmd.ExecuteNonQuery();
                        指送地址Function("Duplicate_Save", conn, cmd);
                        tn.Commit(); tn.Dispose();
                        cmd.Dispose();
                    }

                    this.SeekNo = tempNo = CuNo.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                    DetailMemo.Clear();
                    FormState = FormEditState.Append;
                    CuNo.Focus();
                    setTxtWhenAppend();
                }
                catch (Exception ex)
                {
                    tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }

            if (btnState == "Modify")
            {
                if (Common.load("Check", "Cust", "Cuno", CuNo.Text.Trim()) == null)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuNo.Text = string.Empty;
                    CuNo.Focus();
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        tn = conn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuname2", CuName2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuname1", CuName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuinvoname", CuInvoName.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuchkname", "支票抬頭");
                        cmd.Parameters.AddWithValue("@cuxa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cupareno", CuPareNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cucono", "T");
                        cmd.Parameters.AddWithValue("@cuime", CuIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux1no", CuX1No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuemno1", CuEmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper1", CuPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper2", CuPer2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuper", CuPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@cutel1", CuTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cutel2", CuTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cufax1", CuFax1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel1", CuAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel2", CuAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuatel3", CuAtel3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cubbc", "BBC");
                        cmd.Parameters.AddWithValue("@cuaddr1", CuAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur1", CuR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuaddr2", CuAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur2", CuR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuaddr3", CuAddr3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cur3", CuR3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuslevel", getRadioNumber(groupBoxT1));
                        cmd.Parameters.AddWithValue("@cudisc", CuDisc1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuemail", CuEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuwww", CuWww.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux2no", CuX2No.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuuno", CuUno.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux3no", CuX3No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux4no", CuX4No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cucredit", CuCredit.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengname", CuEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengaddr", CuEngaddr.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuengr1", CuEngr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cumemo1", CuMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cumemo2", CuMemo2_1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cux5no", CuX5No1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuarea", CuArea.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf1", CuUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf2", CuUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf3", CuUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf4", CuUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf5", CuUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuudf6", "保留");
                        cmd.Parameters.AddWithValue("@cudate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@cudate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@cudate2", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@culastday", "");
                        cmd.Parameters.AddWithValue("@culastday1", "");
                        cmd.Parameters.AddWithValue("@culastday2", "");
                        cmd.Parameters.AddWithValue("@cufirrcvpar", "1");
                        cmd.Parameters.AddWithValue("@cunote", "備忘錄");
                        cmd.Parameters.AddWithValue("@cubirth", Date.ToTWDate(CuBirth.Text));
                        cmd.Parameters.AddWithValue("@cubirth1", Date.ToUSDate(CuBirth.Text));
                        cmd.Parameters.AddWithValue("@cubirth2", CuBirth.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuDatep", Date.ToTWDate(CuDatep.Text));
                        cmd.Parameters.AddWithValue("@CuDatep1", Date.ToUSDate(CuDatep.Text));
                        cmd.Parameters.AddWithValue("@CuDatep2", CuDatep.Text.Trim());
                        cmd.Parameters.AddWithValue("@cusex", CuSex.Text.Trim());
                        cmd.Parameters.AddWithValue("@cublood", CuBlood.Text.Trim());
                        cmd.Parameters.AddWithValue("@cuidno", CuIdNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsTrans", machine);
                        cmd.Parameters.AddWithValue("@CuPoint", "0");
                        cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebPassWord", WebPassWord.Text.Trim());
                        cmd.Parameters.AddWithValue("@ImNo", ImNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@einv", getRadioNumber(pEInv));//是否有電子發票
                        cmd.Parameters.AddWithValue("@einvchange", EInv1.Checked ? CBEInvChange.Text : "");//是否有電子發票
                        cmd.Parameters.AddWithValue("@payerno", payerno.Text.Trim());//請款客戶編號
                        cmd.Parameters.AddWithValue("@SpNo", SpNo.Text.Trim());//專案編號
                        cmd.Parameters.AddWithValue("@SpName", SpName.Text.Trim());//專案編號
                        cmd.Parameters.AddWithValue("@detailmemo", DetailMemo.Text.Trim());//專案編號
                        cmd.CommandText = "Update Cust set "
                          + "cuname2 =@cuname2"
                          + ",cuname1 =@cuname1"
                          + ",cuinvoname =@cuinvoname"
                          + ",cuxa1no =@cuxa1no"
                          + ",cupareno =@cupareno"
                          + ",cuime =@cuime"
                          + ",cux1no =@cux1no"
                          + ",cuemno1 =@cuemno1"
                          + ",cuper1 =@cuper1"
                          + ",cuper2 =@cuper2"
                          + ",cuper =@cuper"
                          + ",cutel1 =@cutel1"
                          + ",cutel2 =@cutel2"
                          + ",cufax1 =@cufax1"
                          + ",cuatel1 =@cuatel1"
                          + ",cuatel2 =@cuatel2"
                          + ",cuatel3 =@cuatel3"
                          + ",cuaddr1 =@cuaddr1"
                          + ",cur1 =@cur1"
                          + ",cuaddr2 =@cuaddr2"
                          + ",cur2 =@cur2"
                          + ",cuaddr3 =@cuaddr3"
                          + ",cur3 =@cur3"
                          + ",cuslevel =@cuslevel"
                          + ",cudisc =@cudisc"
                          + ",cuemail =@cuemail"
                          + ",cuwww =@cuwww"
                          + ",cux2no =@cux2no"
                          + ",cuuno =@cuuno"
                          + ",cux3no =@cux3no"
                          + ",cux4no =@cux4no"
                          + ",cucredit =@cucredit"
                          + ",cuengname =@cuengname"
                          + ",cuengaddr =@cuengaddr"
                          + ",cuengr1 =@cuengr1"
                          + ",cumemo1 =@cumemo1"
                          + ",cumemo2 =@cumemo2"
                          + ",cux5no =@cux5no"
                          + ",cuarea =@cuarea"
                          + ",cuudf1 =@cuudf1"
                          + ",cuudf2 =@cuudf2"
                          + ",cuudf3 =@cuudf3"
                          + ",cuudf4 =@cuudf4"
                          + ",cuudf5 =@cuudf5"
                          + ",cubirth =@cubirth"
                          + ",cubirth1 =@cubirth1"
                          + ",CuDatep =@CuDatep"
                          + ",CuDatep1 =@CuDatep1"
                          + ",cusex =@cusex"
                          + ",cublood =@cublood"
                          + ",CuPoint =@CuPoint"
                          + ",WebID =@WebID"
                          + ",WebPassWord =@WebPassWord"
                          + ",ImNo =@ImNo"
                          + ",IsTrans =@IsTrans"
                          + ",cuidno =@cuidno"
                          + ",einvchange=@einvchange"
                          + ",payerno=@payerno"
                          + ",SpNo=@SpNo"
                          + ",SpName=@SpName"
                          + ",detailmemo=@detailmemo"
                          + ",einv =@einv where CuNo =@cuno"
                          + " COLLATE Chinese_Taiwan_Stroke_BIN";

                        cmd.ExecuteNonQuery();
                        指送地址Function("Save", conn, cmd);
                        tn.Commit(); tn.Dispose();
                        cmd.Dispose();
                    }


                    this.SeekNo = tempNo = CuNo.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                    DetailMemo.Clear();
                    FormState = FormEditState.Append;
                    CuNo.Focus();
                }
                catch (Exception ex)
                {
                    tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
            控制指送地址Column編輯狀態(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            DetailMemo.Clear();
            writeToTxt(Common.load("Cancel", "Cust", "Cuno", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            
            btnAppend.Focus();
            控制指送地址Column編輯狀態(false);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private string getRadioNumber(GroupBoxT gbx)
        {
            string str = "";
            foreach (Control rd in gbx.Controls)
            {
                if (rd is RadioButton)
                {
                    if (((RadioButton)rd).Checked)
                        str = rd.Name.Last().ToString();
                }
            }
            return str;
        }

        //表頭欄位
        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender, reader => writeToTxt(reader));
        }
        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuNo.Text.Trim() == "")
            {
                e.Cancel = true;
                CuNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                if (jCust.IsExist(CuNo.Text.Trim()))
                {
                    e.Cancel = true;
                    xe.Open<JBS.JS.Cust>(sender, reader => writeToTxt(reader));
                }
            }

            if (btnState == "Duplicate")
            {
                if (jCust.IsExist(CuNo.Text.Trim()))
                {
                    e.Cancel = true;
                    xe.Open<JBS.JS.Cust>(sender, reader => writeToTxt(reader));
                }
            }

            if (btnState == "Modify")
            {
                if (jCust.IsExist(CuNo.Text.Trim()))
                {
                    if (CuNo.Text.Trim() != BeforeText)
                    {
                        xe.Validate<JBS.JS.Cust>(CuNo.Text.Trim(), reader => writeToTxt(reader));
                    }
                }
                else
                {
                    e.Cancel = true;
                    xe.Open<JBS.JS.Cust>(sender, reader => writeToTxt(reader));
                }
            }
        }

        private void CuName2_Leave(object sender, EventArgs e)
        {
            if (CuName2.ReadOnly) return;
            if (CuName2.Text.Trim() != "" && CuName1.Text.Trim() == "")
            {
                CuName1.Text = CuName2.Text.GetUTF8(10);
            }
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

        private void CuPareNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender, reader =>
            {
                CuPareNo.Text = reader["CuNo"].ToString();
                CuPareName.Text = reader["CuName1"].ToString();
            });
        }
        private void CuPareNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuPareNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuPareNo.Text.Trim() == "")
            {
                CuPareNo.Text = "";
                CuPareName.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Cust>(sender, e, reader =>
            {
                CuPareNo.Text = reader["CuNo"].ToString();
                CuPareName.Text = reader["CuName1"].ToString();
            }, true);
        }

        private void CuX1No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX01>(sender, row =>
            {
                CuX1No.Text = row["x1no"].ToString().Trim();
                CuX1Name.Text = row["x1name"].ToString().Trim();
            });
        }
        private void CuX1No_Validating(object sender, CancelEventArgs e)
        {
            if (CuX1No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuX1No.Text.Trim() == "")
            {
                CuX1No.Text = "";
                CuX1Name.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX01>(sender, e, row =>
            {
                CuX1No.Text = row["x1no"].ToString().Trim();
                CuX1Name.Text = row["x1name"].ToString().Trim();
            });
        }


        private void CuEmNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender, row =>
            {
                CuEmNo1.Text = row["EmNo"].ToString().Trim();
                CuEmName.Text = row["EmName"].ToString().Trim();
            });
        }
        private void CuEmNo1_Validating(object sender, CancelEventArgs e)
        {
            if (CuEmNo1.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuEmNo1.Text.Trim() == "")
            {
                CuEmNo1.Text = "";
                CuEmName.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                CuEmNo1.Text = row["EmNo"].ToString().Trim();
                CuEmName.Text = row["EmName"].ToString().Trim();
            });

            if (tabControl1.TabIndex == 4) 
            {
                dataGridViewT2.Focus();
            }
        }



        private void CuAddr1_DoubleClick(object sender, EventArgs e)
        {
            if (CuAddr1.ReadOnly != true)
            {
                using (var frm = new FrmSaddr())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (sender.Equals(CuAddr1))
                        {
                            this.CuAddr1.Text = frm.TAddr;
                            if (this.CuAddr2.Text.Trim() == "")
                                this.CuAddr2.Text = frm.TAddr;
                            if (this.CuAddr3.Text.Trim() == "")
                                this.CuAddr3.Text = frm.TAddr;
                            if (this.CuAddr1_1.Text.Trim() == "")
                                this.CuAddr1_1.Text = frm.TAddr;

                            this.CuR1.Text = frm.TZip;
                            if (this.CuR2.Text.Trim() == "")
                                this.CuR2.Text = frm.TZip;
                            if (this.CuR3.Text.Trim() == "")
                                this.CuR3.Text = frm.TZip;
                            if (this.CuR1_1.Text.Trim() == "")
                                this.CuR1_1.Text = frm.TZip;
                        }

                        if (sender.Equals(CuAddr2))
                        {
                            this.CuAddr2.Text = frm.TAddr;
                            this.CuR2.Text = frm.TZip;
                        }

                        if (sender.Equals(CuAddr3))
                        {
                            this.CuAddr3.Text = frm.TAddr;
                            this.CuR3.Text = frm.TZip;
                        }

                        if (sender.Equals(CuAddr1_1))
                        {
                            this.CuAddr1_1.Text = frm.TAddr;
                            this.CuR1_1.Text = frm.TZip;
                        }
                    }
                }
            }
        }

        private void CuX3No_DoubleClick(object sender, EventArgs e)
        {
            if (CuX3No1.ReadOnly)
                return;

            xe.Open<JBS.JS.XX03>(sender, row =>
            {
                CuX3No.Text = row["X3No"].ToString();
                CuX3No1.Text = row["X3No"].ToString();
                CuX3Name.Text = row["X3Name"].ToString();
                CuX3Name1.Text = row["X3Name"].ToString();
            });
        }

        private void CuX4No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX04>(sender, row =>
            {
                CuX4No.Text = row["X4No"].ToString().Trim();
                CuX4Name.Text = row["X4Name"].ToString().Trim();

                CuX4No1.Text = row["X4No"].ToString().Trim();
                CuX4Name1.Text = row["X4Name"].ToString().Trim();
            });
        }
        private void CuX4No_Validating(object sender, CancelEventArgs e)
        {
            if (CuX4No.ReadOnly) return;
            if (btnCancel.Focused) return;

            TextBox tx = ((TextBox)sender);
            if (tx.Text.Trim() == "")
            {
                CuX4No.Text = "";
                CuX4No1.Text = "";
                CuX4Name.Text = "";
                CuX4Name1.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX04>(sender, e, row =>
            {
                CuX4No.Text = row["X4No"].ToString().Trim();
                CuX4Name.Text = row["X4Name"].ToString().Trim();

                CuX4No1.Text = row["X4No"].ToString().Trim();
                CuX4Name1.Text = row["X4Name"].ToString().Trim();
            });
        }

        private void CuX5No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX05>(sender, row =>
            {
                CuX5No.Text = row["X5No"].ToString().Trim();
                CuX5Name.Text = row["X5Name"].ToString().Trim();

                CuX5No1.Text = row["X5No"].ToString().Trim();
                CuX5Name1.Text = row["X5Name"].ToString().Trim();
            });
        }
        private void CuX5No_Validating(object sender, CancelEventArgs e)
        {
            if (CuX5No1.ReadOnly) return;
            if (btnCancel.Focused) return;

            TextBox tx = ((TextBox)sender);
            if (tx.Text.Trim() == "")
            {
                e.Cancel = true;
                tx.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.XX05>(sender, e, row =>
            {
                CuX5No.Text = row["X5No"].ToString().Trim();
                CuX5Name.Text = row["X5Name"].ToString().Trim();

                CuX5No1.Text = row["X5No"].ToString().Trim();
                CuX5Name1.Text = row["X5Name"].ToString().Trim();
            });

            if (CuX5No.Text == "7")//使用
                EInv1.Checked = true;
            else
                EInv2.Checked = true;
        }

        private void CuArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)49 || e.KeyChar == (char)50 || e.KeyChar == (char)51 || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void CuX2No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX02>(sender, row =>
            {
                CuX2No.Text = row["X2No"].ToString().Trim();
                CuX2Name.Text = row["X2Name"].ToString().Trim();
            });
        }
        private void CuX2No_Validating(object sender, CancelEventArgs e)
        {
            if (CuX2No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuX2No.Text.Trim() == "")
            {
                CuX2No.Text = "";
                CuX2Name.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX02>(sender, e, row =>
            {
                CuX2No.Text = row["X2No"].ToString().Trim();
                CuX2Name.Text = row["X2Name"].ToString().Trim();
            });
        }

        private void CuSex_DoubleClick(object sender, EventArgs e)
        {
            if (CuSex.Text.Trim() == "" || CuSex.Text == "女")
                CuSex.Text = "男";
            else
                CuSex.Text = "女";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down :
                    if (!dataGridViewT2.ReadOnly) 
                        AddDataGridView2Row();
                    break;
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

        private void AddDataGridView2Row()
        {
            try
            {
                int max = 指送地址dt.Rows.Count, now = 0;
                if (dataGridViewT2.CurrentCell.RowIndex < max - 1) return;
                for (int i = 0; i < 指送地址dt.Rows.Count; i++)
                {
                    if (指送地址dt.Rows[i]["dano"].ToString().Trim().Length > 0) now += 1; ;
                }
                if (dataGridViewT2["指送編碼", dataGridViewT2.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim().Length > 0) now += 1;
                if (now >= max)
                {
                    DataRow dr = 指送地址dt.NewRow();
                    dr["序號"] = (指送地址dt.Rows.Count + 1).ToString();
                    指送地址dt.Rows.Add(dr);
                    dataGridViewT2.InvalidateRow(dataGridViewT2.CurrentCell.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CuNo_Enter(object sender, EventArgs e)
        {
            BeforeText = CuNo.Text;
        }

        private void CuX3No_Validating(object sender, CancelEventArgs e)
        {
            if (CuX3No1.ReadOnly) return;
            if (btnCancel.Focused) return;

            TextBox tx = ((TextBox)sender);
            if (tx.Text.Trim() == "")
            {
                e.Cancel = true;
                tx.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                CuX3No.Text = row["X3No"].ToString();
                CuX3No1.Text = row["X3No"].ToString();
                CuX3Name.Text = row["X3Name"].ToString();
                CuX3Name1.Text = row["X3Name"].ToString();
            });
        }



        private void CuArea_Validating(object sender, CancelEventArgs e)
        {
            if (CuArea.ReadOnly) return;
            if (!(CuArea.Text.Trim() == "1" || CuArea.Text == "2" || CuArea.Text == "3" || CuArea.Text.Trim() == ""))
            {
                e.Cancel = true;
                CuArea.SelectAll();
                MessageBox.Show("只能輸入:\t\n\n  1 國內\n  2 國外\n  3 國內外", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void CuBirth_Validating(object sender, CancelEventArgs e)
        {
            if (CuBirth.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (CuBirth.Text.Trim() == "")
            {
                CuBirth.Text = "";
                return;
            }

            if (!CuBirth.IsDateTime())
            {
                e.Cancel = true;
                CuBirth.SelectAll();
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Common.User_DateTime == 1)
                    CuBirth.Text = Date.GetDateTime(1, false);
                else
                    CuBirth.Text = Date.GetDateTime(2, false);
                return;
            }
        }

        private void CuMemo1_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuMemo1.Text = str;
            CuMemo1_1.Text = str;
        }

        private void CuWww_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuWww.Text = str;
            CuWww_1.Text = str;
        }

        private void CuEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuEmail.Text = str;
            CuEmail_1.Text = str;
        }

        private void CuR1_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuR1.Text = str;
            CuR1_1.Text = str;
            if (CuR2.Text.Trim() == "") CuR2.Text = str;
            if (CuR3.Text.Trim() == "") CuR3.Text = str;
        }

        private void CuAddr2_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuAddr2.Text.Trim() == "") CuR2.Text = "";
            if (CuAddr3.Text.Trim() == "") CuR3.Text = "";
        }

        private void CuAddr1_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuAddr1.Text = str;
            CuAddr1_1.Text = str;
            if (CuAddr1.Text.Trim() == "") CuAddr1.Text = str;
            if (CuAddr2.Text.Trim() == "") CuAddr2.Text = str;
            if (CuAddr3.Text.Trim() == "") CuAddr3.Text = str;
        }

        private void CuFax1_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuFax1.Text = str;
            CuFax1_1.Text = str;
        }

        private void CuAtel1_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuAtel1.Text = str;
            CuAtel1_1.Text = str;
        }

        private void CuTel1_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuTel1.Text = str;
            CuTel1_1.Text = str;
        }

        private void CuPer1_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = (sender as TextBox).Text;
            CuPer1.Text = str;
            CuPer1_1.Text = str;
        }

        private void CuDisc_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            string str = ((TextBox)sender).Text;
            decimal d = 0;
            decimal.TryParse(str, out d);

            if (d > 1)
            {
                e.Cancel = true;
                ((TextBox)sender).SelectAll();
                MessageBox.Show("折數設定不可大於1.000", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                CuDisc.Text = d.ToString("f" + CuDisc.LastNum);
                CuDisc1.Text = d.ToString("f" + CuDisc1.LastNum);
            }
        }

        private void CuCredit_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (btnState == "Append" || btnState == "Duplicate")
            {
                CuXbh.Text = CuCredit.Text;
            }
            if (btnState == "Modify")
            {
                var row = Common.load("Check", "Cust", "Cuno", CuNo.Text.Trim());

                decimal d = 0, cucredit = 0, cureceiv = 0;
                decimal.TryParse(CuCredit.Text.Trim(), out cucredit);
                decimal.TryParse(row["Cureceiv"].ToString(), out cureceiv);
                d = cucredit - cureceiv;
                CuXbh.Text = d.ToString("f" + CuXbh.LastNum);
            }
        }

        private void CuDatep_Validating(object sender, CancelEventArgs e)
        {
            if (CuDatep.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (CuDatep.Text.Trim().Length == 0)
            {
                CuDatep.Text = "";
                return;
            }
            if (!CuDatep.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuDatep.SelectAll();
                return;
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
                    cmd.CommandText = "Select Count(*) from Fact where WebID = (@WebID)";
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
                    cmd.Parameters.AddWithValue("@CuNo", CuNo.Text);
                    cmd.Parameters.AddWithValue("@WebID", WebID.Text.Trim());
                    if (btnState == "Append" || btnState == "Duplicate")
                    {
                        cmd.CommandText = "Select Count(*) from Cust where WebID = (@WebID)";
                    }
                    if (btnState == "Modify")
                    {
                        cmd.CommandText = "Select Count(*) from Cust where CuNo != (@CuNo) And WebID = (@WebID)";
                    }
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

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            CBEInvChange.Enabled = pEInv.Enabled = groupBoxT1.Enabled = groupBoxT2.Enabled = !btnAppend.Enabled;//電子發票
            if (FormState != FormEditState.None)
            {
                if (EInv2.Checked) CBEInvChange.Enabled = false;
            }
        }

        private void radioT16_Enter(object sender, EventArgs e)
        {
            RadioButton rd = ((RadioButton)sender);
            var index = rd.Name.Last().ToString();
            foreach (RadioButton r in groupBoxT1.Controls.OfType<RadioButton>())
            {
                if (r.Name.EndsWith(index.ToString())) r.Checked = true;
            }
        }

        private void radioT6_Enter(object sender, EventArgs e)
        {
            RadioButton rd = ((RadioButton)sender);
            var index = rd.Name.Last().ToString();
            foreach (RadioButton r in groupBoxT2.Controls.OfType<RadioButton>())
            {
                if (r.Name.EndsWith(index.ToString())) r.Checked = true;
            }
        }

        private void ImNo_DoubleClick(object sender, EventArgs e)
        {
            if (ImNo.ReadOnly) return;
            using (S5.FrmFaPiaoMoZuBrow frm = new S5.FrmFaPiaoMoZuBrow())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ImNo.Text = frm.result["ImNo"].ToString().Trim();
                    ImName.Text = frm.result["ImName"].ToString().Trim();
                }
            }
        }

        private void ImNo_Validating(object sender, CancelEventArgs e)
        {
            if (ImNo.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (ImNo.TrimTextLenth() == 0)
            {
                ImNo.Clear();
                ImName.Clear();
                return;
            }
            if (ImNo.TrimTextLenth() > 0)
            {
                var row = Common.load("Check", "invMode", "ImNo", ImNo.Text.Trim());
                if (row == null)
                {
                    e.Cancel = true;
                    using (S5.FrmFaPiaoMoZuBrow frm = new S5.FrmFaPiaoMoZuBrow())
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            ImNo.Text = frm.result["ImNo"].ToString().Trim();
                            ImName.Text = frm.result["ImName"].ToString().Trim();
                        }
                    }
                }
                else
                {
                    ImNo.Text = row["imno"].ToString().Trim();
                    ImName.Text = row["ImName"].ToString().Trim();
                }

            }
        }

        private void CuUno_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (CuUno.ReadOnly) return;

            //if (CuUno.TrimTextLenth() == 0)
            //{
            //        e.Cancel = true;
            //        CuUno.Text = "";
            //        MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        CuUno.Focus();
            //        return;
            //}
            if (CuUno.TrimTextLenth() > 0 && CuUno.TrimTextLenth() != 8)
            {
                e.Cancel = true;
                MessageBox.Show("統一編號輸入錯誤!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuUno.SelectAll();
                return;
            }

            if (CuUno.TrimTextLenth() == 8)
            {
                if (CuUno.Text.All(c => Char.IsDigit(c)) == false)
                {
                    e.Cancel = true;
                    MessageBox.Show("統一編號輸入錯誤!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuUno.SelectAll();
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
            this.日期.DataPropertyName = Common.User_DateTime == 1 ? "民國" : "西元";
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;
            lblNopay.Visible = false;
            Nopay.Visible = false;
            dtpayerno.Visible = false;
            string SqlStr = "", SqlStrr = "";
            
            if(radio7.Checked)
            {
                SqlStr = @" select 來源='報價',民國=SUBSTRING(QuDate,1,3) + '/'+SUBSTRING(QuDate,4,2)+ '/'+SUBSTRING(QuDate,6,2),西元= SUBSTRING(QuDate1,1,4) + '/'+SUBSTRING(QuDate1,5,2)+ '/'+SUBSTRING(QuDate1,7,2)
                            ,quno as 單據憑證,itno as 產品編號,itname as 品名規格,qty as 數量,itunit as 單位,price as 售價,prs as 折數 ,mny as 稅前金額  from quoted   where cuno = @cuno order by QuDate desc,quno desc";
            }
            else if(radio8.Checked){
                SqlStr = @" select 來源='訂單',民國=SUBSTRING(ordate,1,3) + '/'+SUBSTRING(ordate,4,2)+ '/'+SUBSTRING(ordate,6,2),西元= SUBSTRING(ordate1,1,4) + '/'+SUBSTRING(ordate1,5,2)+ '/'+SUBSTRING(ordate1,7,2)
                            ,orno as 單據憑證,itno as 產品編號,itname as 品名規格,qty as 數量,itunit as 單位,price as 售價,prs as 折數 ,mny as 稅前金額  from orderd  where cuno = @cuno order by ordate desc,orno desc";
            }
            else if (radio9.Checked)
            {
                SqlStr = @" select 來源='銷貨',民國=SUBSTRING(saled.sadate,1,3) + '/'+SUBSTRING(saled.sadate,4,2)+ '/'+SUBSTRING(saled.sadate,6,2),西元= SUBSTRING(saled.sadate1,1,4) + '/'+SUBSTRING(saled.sadate1,5,2)+ '/'+SUBSTRING(saled.sadate1,7,2)
                           ,sale.cuno,sale.payerno ,saled.sano as 單據憑證,itno as 產品編號,itname as 品名規格,qty as 數量,itunit as 單位,price as 售價,prs as 折數 ,mny as 稅前金額  from SALED left join SALE on SALE.sano=saled.sano   
                            where  saled.cuno = @cuno or sale.payerno = @cuno order by  saled.sadate desc, saled.sano desc";
                SqlStrr = @" select 來源='銷退',民國=SUBSTRING(rsaled.sadate,1,3) + '/'+SUBSTRING(rsaled.sadate,4,2)+ '/'+SUBSTRING(rsaled.sadate,6,2),西元= SUBSTRING(rsaled.sadate1,1,4) + '/'+SUBSTRING(rsaled.sadate1,5,2)+ '/'+SUBSTRING(rsaled.sadate1,7,2)
                           ,rsale.cuno,rsale.payerno ,rsaled.sano as 單據憑證,itno as 產品編號,itname as 品名規格,qty*-1 as 數量,itunit as 單位,price *-1 as 售價,prs as 折數 ,mny*-1 as 稅前金額  from RSALED left join RSALE on RSALE.sano=rsaled.sano   
                            where  rsaled.cuno = @cuno or rsale.payerno = @cuno order by  rsaled.sadate desc, rsaled.sano desc";
                //lblNopay.Visible = true;未付累積金額
               // Nopay.Visible = true;
            }
            dataGridViewT1.DataSource = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                相關單據dt.Clear();
                cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                cmd.CommandText = SqlStr;
                da.Fill(相關單據dt);
                if (SqlStrr.Length > 0)
                {
                    cmd.CommandText = SqlStrr;
                    da.Fill(相關單據dt);
                }
                if (radio9.Checked)
                {
                    for (int i = 0; i < 相關單據dt.Rows.Count; i++)
                    {
                        if (CuNo.Text.Trim().ToString() == 相關單據dt.Rows[i]["payerno"].ToString().Trim())
                        {
                            dtpayerno.Visible = true;
                            dtpayerno.HeaderText = "出貨客戶編號";
                            相關單據dt.Rows[i]["payerno"] = 相關單據dt.Rows[i]["cuno"].ToString();
                        }
                        else if (相關單據dt.Rows[i]["payerno"].ToString().Trim() == payerno.Text.ToString().Trim())
                        {
                            相關單據dt.Rows[i]["payerno"] = "";
                        }
                    }
                }
            }
            dataGridViewT1.DataSource = 相關單據dt;
        }

        private void 控制指送地址Column編輯狀態(bool state)
        {
            dataGridViewT2.ReadOnly = !state;
            foreach (var item in new List<DataGridViewTextBoxColumn> { 序號, 預設列印 })
            {
                item.ReadOnly = true;
            }
        }

        private void 指送地址Function(string State,SqlConnection con_,SqlCommand cmd_)
        {
            dataGridViewT2.DataSource = null;

            if (State.ToLower() == "load")
            {
                #region Load
                指送地址dt.Clear();
                Insert指送地址.Clear();
                Update指送地址.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    cmd.CommandText = "Select ROW_NUMBER() OVER(ORDER BY id) AS 序號,* from DeliveryAddress where cuno = @cuno ";
                    da.Fill(指送地址dt);
                }
                
                for (int i = 0; i < 1; i++)
                {
                    DataRow dr = 指送地址dt.NewRow();
                    dr["序號"] = (指送地址dt.Rows.Count+ 1 + i).ToString();
                    指送地址dt.Rows.Add(dr);
                }
                dataGridViewT2.DataSource = 指送地址dt;
                #endregion
            }
            else if (State.ToLower() == "duplicate_save")
            {
                #region  insert
                for (int i = 0; i < 指送地址dt.Rows.Count; i++)
                {
                    cmd_.Parameters.Clear();
                    cmd_.Parameters.AddWithValue("dano", 指送地址dt.Rows[i]["dano"].ToString().Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("cuno", CuNo.Text.Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("addr", 指送地址dt.Rows[i]["addr"].ToString().Trim().GetUTF8(60));
                    cmd_.Parameters.AddWithValue("name", 指送地址dt.Rows[i]["name"].ToString().Trim().GetUTF8(50));
                    cmd_.Parameters.AddWithValue("zip", 指送地址dt.Rows[i]["zip"].ToString().Trim().GetUTF8(3));
                    cmd_.Parameters.AddWithValue("tel", 指送地址dt.Rows[i]["tel"].ToString().Trim().GetUTF8(20));
                    cmd_.Parameters.AddWithValue("per1", 指送地址dt.Rows[i]["per1"].ToString().Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("defaultPrint", 指送地址dt.Rows[i]["defaultPrint"].ToString().Trim().GetUTF8(1));
                    cmd_.CommandText = "insert into DeliveryAddress(dano,cuno,addr,zip,tel,per1,defaultPrint,name) values(@dano,@cuno,@addr,@zip,@tel,@per1,@defaultPrint,@name)";
                    cmd_.ExecuteNonQuery();
                }
                #endregion
            }
            else if (State.ToLower() == "save" )
            {
                #region update
                for (int i = 0; i < Update指送地址.Count ; i++)
                {
                    int index = int.Parse(Update指送地址[i])-1;
                    cmd_.Parameters.Clear();
                    var id_ = 指送地址dt.Rows[index]["id"].ToString().Trim().GetUTF8(10);
                    var name_ = 指送地址dt.Rows[index]["name"].ToString().Trim().GetUTF8(10);
                    cmd_.Parameters.AddWithValue("id", 指送地址dt.Rows[index]["id"].ToString().Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("dano", 指送地址dt.Rows[index]["dano"].ToString().Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("name", 指送地址dt.Rows[index]["name"].ToString().Trim().GetUTF8(50));
                    cmd_.Parameters.AddWithValue("cuno", CuNo.Text.Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("addr", 指送地址dt.Rows[index]["addr"].ToString().Trim().GetUTF8(60));
                    cmd_.Parameters.AddWithValue("zip", 指送地址dt.Rows[index]["zip"].ToString().Trim().GetUTF8(3));
                    cmd_.Parameters.AddWithValue("tel", 指送地址dt.Rows[index]["tel"].ToString().Trim().GetUTF8(20));
                    cmd_.Parameters.AddWithValue("per1", 指送地址dt.Rows[index]["per1"].ToString().Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("defaultPrint", 指送地址dt.Rows[index]["defaultPrint"].ToString().Trim().GetUTF8(1));
                    cmd_.CommandText = @"UPDATE DeliveryAddress SET dano=@dano,cuno=@cuno,addr=@addr,zip=@zip,name=@name,tel=@tel,per1=@per1,defaultPrint= @defaultPrint WHERE id = @id ";
                    var influent = cmd_.ExecuteNonQuery();
                    if (cmd_.ExecuteNonQuery() <= 0)
                    #region  insert
                    {
                        cmd_.CommandText = "insert into DeliveryAddress(dano,cuno,addr,zip,tel,per1,defaultPrint,name) values(@dano,@cuno,@addr,@zip,@tel,@per1,@defaultPrint,@name)";
                        cmd_.ExecuteNonQuery();
                    }
                    #endregion
                }
                #endregion         
                #region  insert
                for (int i = 0; i < Insert指送地址.Count ; i++)
                {
                    int index = int.Parse(Insert指送地址[i]) - 1;
                    if (指送地址dt.Rows[index]["dano"].ToString().Trim() == "") continue;
                    cmd_.Parameters.Clear();
                    cmd_.Parameters.AddWithValue("dano", 指送地址dt.Rows[index]["dano"].ToString().Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("cuno", CuNo.Text.Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("addr", 指送地址dt.Rows[index]["addr"].ToString().Trim().GetUTF8(60));
                    cmd_.Parameters.AddWithValue("zip", 指送地址dt.Rows[index]["zip"].ToString().Trim().GetUTF8(3));
                    cmd_.Parameters.AddWithValue("tel", 指送地址dt.Rows[index]["tel"].ToString().Trim().GetUTF8(20));
                    cmd_.Parameters.AddWithValue("per1", 指送地址dt.Rows[index]["per1"].ToString().Trim().GetUTF8(10));
                    cmd_.Parameters.AddWithValue("name", 指送地址dt.Rows[index]["name"].ToString().Trim().GetUTF8(50));
                    cmd_.Parameters.AddWithValue("defaultPrint", 指送地址dt.Rows[index]["defaultPrint"].ToString().Trim().GetUTF8(1));
                    cmd_.CommandText = "insert into DeliveryAddress(dano,cuno,addr,zip,tel,per1,defaultPrint,name) values(@dano,@cuno,@addr,@zip,@tel,@per1,@defaultPrint,@name)";
                    cmd_.ExecuteNonQuery();
                }
                #endregion
                #region  Delete
                string DefaultId ="";
                for (int i = 0; i < 指送地址dt.Rows.Count; i++)
                {
                    if (指送地址dt.Rows[i]["defaultPrint"].ToString().Trim() == "V")
                        DefaultId = 指送地址dt.Rows[i]["id"].ToString().Trim();

                    var dano = 指送地址dt.Rows[i]["dano"].ToString().Trim();
                    var id = 指送地址dt.Rows[i]["id"].ToString().Trim();
                    if (指送地址dt.Rows[i]["dano"].ToString().Trim() != "" ) 
                        continue;
                    if (指送地址dt.Rows[i]["id"].ToString().Trim() == "")
                        continue;
                    cmd_.Parameters.Clear();
                    cmd_.Parameters.AddWithValue("id", id);
                    cmd_.CommandText = "delete from DeliveryAddress where id = @id";
                    cmd_.ExecuteNonQuery();
                }
                #endregion
                #region  UpDate預設列印
                cmd_.Parameters.Clear();
                cmd_.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                cmd_.Parameters.AddWithValue("id", DefaultId);
                cmd_.CommandText = @"UPDATE DeliveryAddress SET defaultPrint= ''  WHERE cuno=@cuno and defaultPrint = 'V' ";
                cmd_.ExecuteNonQuery();
                cmd_.CommandText = @"UPDATE DeliveryAddress SET defaultPrint= 'V' WHERE id=@id ";
                cmd_.ExecuteNonQuery();
                #endregion
            }
            else if (State.ToLower() == "delete")
            {
                #region Delete
                cmd_.Parameters.Clear();
                cmd_.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                cmd_.CommandText = "delete from DeliveryAddress where cuno = @cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                cmd_.ExecuteNonQuery();
                #endregion
            }
            else if (State.ToLower() == "newcust")
            {
                #region NewCust
                指送地址dt.Clear();
                控制指送地址Column編輯狀態(true);
                DataRow dr = 指送地址dt.NewRow();
                dr["序號"] = (1).ToString();
                指送地址dt.Rows.Add(dr);
                dataGridViewT2.DataSource = 指送地址dt;
                #endregion
            }
        }

        private void dataGridViewT2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT2.ReadOnly) return;
            if (btnDelete.Focused || btnCancel.Focused) return;
            var CurrentColumnName = dataGridViewT2.Columns[e.ColumnIndex].Name;
        }

        private void dataGridViewT2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            BeforeText_指送地址 = dataGridViewT2.CurrentCell.EditedFormattedValue.ToString().Trim();
        }

        private void dataGridViewT2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string 序號 = 指送地址dt.Rows[dataGridViewT2.CurrentCell.RowIndex]["序號"].ToString();
            string LeastText_指送地址 = dataGridViewT2.CurrentCell.EditedFormattedValue.ToString().Trim();
            if (BeforeText_指送地址 != LeastText_指送地址 && Update指送地址.IndexOf(序號) == -1 && Insert指送地址.IndexOf(序號) == -1)
            {
                if (指送地址dt.Rows[dataGridViewT2.CurrentCell.RowIndex]["id"].ToString() != "")
                    Update指送地址.Add(序號);
                else
                    Insert指送地址.Add(序號);
            }
        }
        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.ReadOnly) return;
            if (btnDelete.Focused || btnCancel.Focused) return;
            var CurrentColumnName = dataGridViewT2.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "預設列印") 
            {
                if (指送地址dt.Rows[e.RowIndex]["DefaultPrint"].ToString().Trim().ToUpper() == "V")
                {
                    指送地址dt.Rows[e.RowIndex]["DefaultPrint"] = "";
                }
                else 
                {
                    for (int i = 0; i < 指送地址dt.Rows.Count; i++)
                    {
                        指送地址dt.Rows[i]["DefaultPrint"] = "";
                    }
                    指送地址dt.Rows[e.RowIndex]["DefaultPrint"] = "V";
                }
            }
        }

        private void dataGridViewT2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                string str = dataGridViewT2.CurrentCell.OwningColumn.Name;
                TextBox t = (TextBox)e.Control;
                t.TextChanged -= new EventHandler(t_TextChanged);
                if (str == "郵遞區號")
                {
                    t.TextChanged += new EventHandler(t_TextChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void t_TextChanged(object sender, EventArgs e)
        {
            string zip = dataGridViewT2["郵遞區號", dataGridViewT2.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim();
            if (zip.Length == 3)
            {

                if (指送地址dt.Rows[dataGridViewT2.CurrentCell.RowIndex]["addr"].ToString().IndexOf(get地址(zip)) != -1)
                    return;
                指送地址dt.Rows[dataGridViewT2.CurrentCell.RowIndex]["zip"] = zip;
                指送地址dt.Rows[dataGridViewT2.CurrentCell.RowIndex]["addr"] = get地址(zip);
                //指送地址dt.AcceptChanges();
                dataGridViewT2.InvalidateRow(dataGridViewT2.CurrentCell.RowIndex);
                //dataGridViewT2.CurrentRow.Selected = true;
                //dataGridViewT2.Focus();
            }
        }

        private string get地址(string zip)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("zip", zip);
                cmd.CommandText = "select * from saddr2 where zip=@zip";
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    return dr["city"].ToString().Trim() + dr["area"].ToString().Trim();
                }
            }
            return "";
        }
        private string getRadioNumber(PanelNT pnl)
        {
            var rd = pnl.Controls.OfType<RadioT>().FirstOrDefault(r => r.Checked);
            return rd == null ? "0" : rd.Name.Last().ToString();
        }

        private void EInv1_CheckedChanged(object sender, EventArgs e)
        {
            //是否使用電子發票
            if (CuNo.ReadOnly) return;
            if (EInv1.Checked)//使用
            {
                lblEInvChange.Visible = CBEInvChange.Visible = true;
                CBEInvChange.Enabled = true;
                CBEInvChange.Text = "存證";
                pVar.XX05Validate("7", CuX5No, CuX5Name);
                pVar.XX05Validate("7", CuX5No1, CuX5Name1);
            }
            else
            {
                lblEInvChange.Visible = CBEInvChange.Visible = false;
                CBEInvChange.Enabled = false;
                CBEInvChange.Text = "";
                pVar.XX05Validate("1", CuX5No, CuX5Name);
                pVar.XX05Validate("1", CuX5No1, CuX5Name1);
            }
        }

        private void payerno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender, reader =>
                {
                payerno.Text =reader["cuno"].ToString();
                payername.Text =reader["cuname1"].ToString();
                });
        }
        private void payerno_Validating(object sender, CancelEventArgs e)
        {
            if (jCust.IsExist(payerno.Text.Trim()))
            {
                xe.Validate<JBS.JS.Cust>(payerno.Text.Trim(), reader => payername.Text = reader["cuname1"].ToString());

            }
            else
            {
                e.Cancel = true;
                xe.Open<JBS.JS.Cust>(sender, reader =>
                {
                    payerno.Text = reader["cuno"].ToString();
                    payername.Text = reader["cuname1"].ToString();
                });
            }
        }


        private void SpNo_Validating(object sender, CancelEventArgs e)
        {
            if (SpNo.ReadOnly || btnCancel.Focused)
                return;

            if (SpNo.TrimTextLenth() == 0)
            {
                SpNo.Clear();
                SpName.Clear();
                return;
            }

            jCust.ValidateOpen<JBS.JS.Spec>(sender, e, reader =>
            {
                SpNo.Text = reader["SpNo"].ToString().Trim();
                SpName.Text = reader["SpName"].ToString().Trim();
            }, true);
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            jCust.Open<JBS.JS.Spec>(sender, reader =>
            {
                SpNo.Text = reader["SpNo"].ToString().Trim();
                SpName.Text = reader["SpName"].ToString().Trim();
            });
        }



        
    }
}



