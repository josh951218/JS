using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_1;
using S_61.SOther;
using System.IO;
using System.Diagnostics;

namespace S_61.subMenuFm_2
{
    public partial class FrmBShop : Formbase
    {
        JBS.JS.BShop jBShop;
        JE.MyControl.TextBoxT BoNo = new JE.MyControl.TextBoxT();
        BatchFunction Batch = new BatchFunction();
        DataTable dtBShopD = new DataTable();  //明細檔
        DataTable tempBShopD = new DataTable();//明細暫存檔

        DataTable dtBShopBom = new DataTable(); //組件檔
        DataTable tempBom = new DataTable();   //所有組件暫存檔

        #region 批次資料
        BatchProcess BatchF = new BatchProcess();          //批次存資料異動修改
        BatchSave BatchSave = new BatchSave();             //批次資料存檔用
        DataTable dt_BatchProcess = new DataTable();       //批次異動
        DataTable dt_TempBatchProcess = new DataTable();   //批次異動暫存檔
        #endregion
        DataTable borrtemp = new DataTable();//borrtemp
        DataTable borr = new DataTable();//borr

        DataTable DaFile = new DataTable();//附件檔案

        string TextBefore;
        string ItNoBegin;
        string UdfNoBegin;
        string Memo1 = "";
        decimal BomRec = 0;

        //修改前->廠商編號
        string OldFact = "";

        List<Control> Writes;
        List<Button> AllGridButtons;
        List<TextBoxNumberT> NumsInit_Zero;
        List<TextBox> CostControl;
        List<TextBoxbase> list;

        //發票日期、發票地址
        string InvDate = "";
        string InvAddr1 = "";
        string PhotoPath = "";// 13.7c  

        //媒體申報
        string invkind = "";
        string sub = "";
        string customsno = "";

        string tempinvno = "";

        public FrmBShop()
        {
            InitializeComponent();
            this.jBShop = new JBS.JS.BShop();
            this.list = this.getEnumMember();
            this.dataGridViewT1.tableName = "BShopd";

            BsDate.SetDateLength();
            BsDateAc.SetDateLength();
            pVar.SetMemoUdf(this.備註說明);
            
            Writes = new List<Control> { 
                BsNo, BsDate, BsDateAc, FqNo, FaNo, FaName11, FaPer11, FaTel11, StNo, StName, Xa1No, Xa1Name, Xa1Par, 
                TaxMnyB, TaxMny, X3No, Rate, Tax, TotMny, Discount, CollectMny, GetPrvAcc, AcctMny, X4No, X4Name, EmNo, EmName, SeNo, SeName, SpNo, SpName, BsMemo, 
                TaxMny1, X3No1, Rate1, Tax1, TotMny1, X5No, InvNo, Discount1, CashMny, CardMny, CardNo, InvName, Ticket, CollectMny1, GetPrvAcc1, AcctMny1, InvTaxNo,
                Transport, TranPar, TransportB, Premium, PremPar, PremiumB, Tariff, TariPar, TariffB, Postal, ProcFee, Certif, Clearance, OtherFee, TotFee,DeNo,DeName,EInvState,EInvChange,invrandom  
            };

            //
            AllGridButtons = new List<Button> { gridAppend, gridDelete, gridPicture, gridInsert, gridItDesp, gridBomD, gridAddress, gridInvNo, gridInvMode };

            NumsInit_Zero = new List<TextBoxNumberT> { TaxMnyB, TaxMny, Tax, TotMny, Discount, CollectMny, GetPrvAcc, AcctMny, TaxMny1, Tax1, TotMny1, Discount1, CashMny, CardMny, Ticket, CollectMny1, GetPrvAcc1, AcctMny1, Transport, TranPar, TransportB, Premium, PremPar, PremiumB, Tariff, TariPar, TariffB, Postal, ProcFee, Certif, Clearance, OtherFee, TotFee };
            CostControl = new List<TextBox> { TaxMnyB, TaxMny, Tax, TotMny, Discount, CollectMny, GetPrvAcc, AcctMny, TaxMny1, Tax1, TotMny1, Discount1, CashMny, CardMny, Ticket, CollectMny1, GetPrvAcc1, AcctMny1, X3No, X3Name, Rate, X3No1, X3Name1, Rate1, X5No, X5Name, InvNo, CardNo };
            
            //權限設定
            CostControl.ForEach(t => t.Visible = Common.User_ShopPrice);
            gridInvNo.Visible = Common.User_ShopPrice;
            this.進價.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.實際成本.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
            this.本幣單價.Visible = Common.User_ShopPrice;
            this.本幣稅前單價.Visible = Common.User_ShopPrice;
            this.本幣稅前金額.Visible = Common.User_ShopPrice;

            //第一頁
            Xa1Par.FirstNum = 4;
            Xa1Par.LastNum = 4;
            TaxMnyB.FirstNum = 20;
            TaxMnyB.LastNum = Common.M;
            FaPayAmt.LastNum = Common.M; FaPayAmt.FirstNum = Common.nFirst;
            TaxMny.LastNum = Common.MFT; TaxMny.FirstNum = Common.nFirst;
            Tax.LastNum = Common.TF; Tax.FirstNum = Common.nFirst;
            TotMny.LastNum = Common.MFT; TotMny.FirstNum = Common.nFirst;
            Discount.LastNum = Common.MFT; Discount.FirstNum = Common.nFirst;
            CollectMny.LastNum = Common.MFT; CollectMny.FirstNum = Common.nFirst;
            GetPrvAcc.LastNum = Common.MFT; GetPrvAcc.FirstNum = Common.nFirst;
            AcctMny.LastNum = Common.MFT; AcctMny.FirstNum = Common.nFirst;
            //第二頁
            TaxMny1.LastNum = Common.MFT; TaxMny1.FirstNum = Common.nFirst;
            Tax1.LastNum = Common.TF; Tax1.FirstNum = Common.nFirst;
            TotMny1.LastNum = Common.MFT; TotMny1.FirstNum = Common.nFirst;
            Discount1.LastNum = Common.MFT; Discount1.FirstNum = Common.nFirst;
            CashMny.LastNum = Common.MFT; CashMny.FirstNum = Common.nFirst;
            CardMny.LastNum = Common.MFT; CardMny.FirstNum = Common.nFirst;
            Ticket.LastNum = Common.MFT; Ticket.FirstNum = Common.nFirst;
            CollectMny1.LastNum = Common.MFT; CollectMny1.FirstNum = Common.nFirst;
            GetPrvAcc1.LastNum = Common.MFT; GetPrvAcc1.FirstNum = Common.nFirst;
            AcctMny1.LastNum = Common.MFT; AcctMny1.FirstNum = Common.nFirst;
            //第三頁
            Transport.LastNum = Common.MF; Transport.FirstNum = Common.nFirst;
            TransportB.LastNum = Common.MF; TransportB.FirstNum = Common.nFirst;
            Premium.LastNum = Common.MF; Premium.FirstNum = Common.nFirst;
            PremiumB.LastNum = Common.MF; PremiumB.FirstNum = Common.nFirst;
            Tariff.LastNum = Common.MF; Tariff.FirstNum = Common.nFirst;
            TariffB.LastNum = Common.MF; TariffB.FirstNum = Common.nFirst;
            Postal.LastNum = Common.MF; Postal.FirstNum = Common.nFirst;
            ProcFee.LastNum = Common.MF; ProcFee.FirstNum = Common.nFirst;
            Certif.LastNum = Common.MF; Certif.FirstNum = Common.nFirst;
            Clearance.LastNum = Common.MF; Clearance.FirstNum = Common.nFirst;
            OtherFee.LastNum = Common.MF; OtherFee.FirstNum = Common.nFirst;
            TotFee.LastNum = Common.MF; TotFee.FirstNum = Common.nFirst;
            TranPar.LastNum = 4;
            PremPar.LastNum = 4;
            TariPar.LastNum = 4;
            TranPar.FirstNum = 11;
            PremPar.FirstNum = 11;
            TariPar.FirstNum = 11;

            this.計價數量.Set庫存數量小數();
            this.進貨數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.進價.Set進貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前進價.FirstNum = 9;
            this.稅前進價.LastNum = 6;
            this.稅前進價.DefaultCellStyle.Format = "f6";
            this.實際成本.Set本幣金額小數();
            this.稅前金額.Set進項金額小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前單價.FirstNum = 9;
            this.本幣稅前單價.LastNum = 6;
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";
            this.本幣稅前金額.Set本幣金額小數();

            if (!dtBShopD.Columns.Contains("stnoo")) dtBShopD.Columns.Add("stnoo", typeof(String));
            if (!dtBShopD.Columns.Contains("stnameo")) dtBShopD.Columns.Add("stnameo", typeof(String));

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }
            if (Common.Sys_UsingBatch == 1)
            {
                this.gridbatch.Visible = false;
            }


            if (Common.Series == "74")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                FaTel11.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
                this.採購憑證.Visible = false;
            }
            else if (Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                this.採購憑證.Visible = true;
            }
            else if (Common.Series == "72")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
                StNo.Enabled = true;
                StName.Enabled = true;
                FaTel11.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
                this.採購憑證.Visible = false;
            }
            else if (Common.Series == "71")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
                StNo.Enabled = true;
                StName.Enabled = true;
                this.採購憑證.Visible = true;
            }
            dataGridViewT1.DataSource = dtBShopD;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmShop_Load(object sender, EventArgs e)
        {
            Action InitdtD = () =>
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = @"
                        Select 產品組成=
                        case
                        when ittrait=1 then '組合品'
                        when ittrait=2 then '組裝品'
                        when ittrait=3 then '單一商品'
                        end,ItNoUdf = '' ,*
                        from BShopD where 1=0 ";

                    da.Fill(dtBShopD);
                    da.Fill(tempBShopD);
                }
            };
            InitdtD.Invoke();
            BatchF.建立結構(dt_BatchProcess, dt_TempBatchProcess);
            dataGridView1.DataSource = dt_BatchProcess;
            var pk = jBShop.Bottom();
            writeToTxt(pk);
            
        }

        private void FrmBShop_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string bsno)
        {
            var result = jBShop.LoadData(bsno, reader =>
            {
                foreach (var tb in Writes)
                {
                    if (tb.Name.EndsWith("1"))
                    {
                        var len = tb.Name.Length - 1;
                        var name = tb.Name.Substring(0, len);
                        tb.Text = reader[name].ToString();
                    }
                    else
                    {
                        tb.Text = reader[tb.Name].ToString();
                    }

                    if (tb is TextBoxNumberT)
                    {
                        tb.Text = tb.Text.ToDecimal().ToString("f" + ((TextBoxNumberT)tb).LastNum);
                    }
                }

                EInvChange.Text = reader["einvchange"].ToString();
                EInvState.Text = reader["einvstate"].ToString();
                invrandom.Text = reader["invrandom"].ToString();

                //填入發票日期與地址
                var date = (Common.User_DateTime == 1) ? "" : "1";
                BsDate.Text = reader["BsDate" + date].ToString();
                BsDateAc.Text = reader["BsDateAc" + date].ToString();
                InvDate = reader["InvDate" + date].ToString();
                InvAddr1 = reader["InvAddr1"].ToString();

                FaPer11.Text = FaPer11.Text.GetUTF8(10);
                DeNo.Text = reader["DeNo"].ToString();
                DeName.Text = reader["DeName"].ToString();

                //費用
                var ck1 = (reader["BsShare"].ToString() == "2");
                var ck2 = (reader["BsShsel"].ToString() == "2");
                BsShare1.Checked = !ck1;
                BsShare2.Checked = ck1;
                BsShsel1.Checked = !ck2;
                BsShsel2.Checked = ck2;

                //載入稅別名稱與發票名稱
                pVar.XX05Validate(reader["X5No"].ToString(), X5No, X5Name);
                pVar.XX03Validate(reader["X3No"].ToString(), X3No, X3Name);
                pVar.XX03Validate(reader["X3No"].ToString(), X3No1, X3Name1);
                Rate.Text = (reader["Rate"].ToDecimal() * 100).ToString("f0");
                Rate1.Text = (reader["Rate"].ToDecimal() * 100).ToString("f0");
                PhotoPath = reader["PhotoPath"].ToString();//13.7c

                //媒體申報
                invkind = reader["invkind"].ToString();
                customsno = reader["customsno"].ToString();
                sub = reader["sub"].ToString();

                gridInvMode.Text = reader["invbatch"].ToInteger() == 1 ? "批開" : "";

                //載入明細與暫存檔
                loadBShopD();
                loadPayAmt();

                this.OldFact = FaNo.Text.Trim();
                this.Memo1 = reader["bsmemo1"].ToString();
                jBShop.keyMan.Set(reader);
            });

            if (result == false)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtBShopD.Clear();
                tempBShopD.Clear();
                dtBShopBom.Clear();
                tempBom.Clear();

                this.OldFact = "";
                this.Memo1 = "";
                jBShop.keyMan.Clear();
            }
            BatchF.上下頁dt資料修改("bshopD", bsno, dt_BatchProcess, dt_TempBatchProcess);
        }

        void loadBShopD()
        {
            dtBShopD.Clear();
            tempBShopD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                    cmd.CommandText = @"
                        Select 產品組成=
                        case
                        when ittrait=1 then '組合品'
                        when ittrait=2 then '組裝品'
                        when ittrait=3 then '單一商品'
                        end,ItNoUdf = (select top 1 itnoudf from item where item.itno = BShopD.itno),*
                        from BShopD where BsNo=@bsno ORDER BY recordno ";

                    da.Fill(dtBShopD);
                    da.Fill(tempBShopD);
                }
                if (dtBShopD.Rows.Count > 0) BomRec = dtBShopD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadBShopBom()
        {
            dtBShopBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select top 1 * from bshopbom where 1=0";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from bshopbom where BsNo=@BsNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from bshopbom where BsNo=@BsNo COLLATE Chinese_Taiwan_Stroke_BIN";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@BsNo", jBShop.GetCurrent());
                    cmd.CommandText = sql;

                    da.Fill(dtBShopBom);
                    da.Fill(tempBom);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadPayAmt()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    //載入廠商預付餘額
                    if (conn.State != ConnectionState.Open) conn.Open();
                    var sql = "select FaPayAmt from Fact where FaNo=(@FaNo) ";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                        FaPayAmt.Text = cmd.ExecuteScalar().ToDecimal().ToString("f" + FaPayAmt.LastNum);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string ToTaiwanMny(TextBox tx)
        {
            decimal d = 0, p = 0;
            decimal.TryParse(tx.Text, out d);
            decimal.TryParse(Xa1Par.Text, out p);
            if (tx.Name == Tax.Name)//外幣營業稅轉本幣
                return (d * p).ToString("f" + Common.TF);
            else if (tx.Name == TotMny.Name)//外幣應退轉本幣應退
                return (d * p).ToString("f" + Common.MFT);
            else return "0";
        }

        string RATEToRate(TextBox tx)
        {
            decimal d = 0;
            decimal.TryParse(tx.Text, out d);
            d = d / 100;
            return d.ToString("F3");
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jBShop.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jBShop.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jBShop.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jBShop.Bottom();
            writeToTxt(pk);
        }

        void ThisFormState()
        {
            #region//新增時,各種歸零,然後設定某些預設值
            if (this.FormState == FormEditState.Append)
            {
                this.BoNo.Clear();
                dtBShopD.Clear();
                loadBShopBom();

                this.Memo1 = "";
                this.BomRec = 0;

                NumsInit_Zero.ForEach(t => t.Text = (0M).ToString("F" + t.LastNum));

                StNo.Text = Common.User_StkNo;
                pVar.StkValidate(Common.User_StkNo, StNo, StName);

                Xa1No.Text = "TWD";
                Xa1Par.Text = "1.000";
                pVar.Xa01Validate("TWD", Xa1No, Xa1Name);

                X3No.Text = "1";
                X3No1.Text = "1";
                X5No.Text = "1";
                pVar.XX03Validate("1", X3No, X3Name, Rate);
                pVar.XX03Validate("1", X3No1, X3Name1, Rate1);
                pVar.XX05Validate("1", X5No, X5Name);

                this.InvAddr1 = "";
                this.InvDate = Date.GetDateTime(Common.User_DateTime);

                BsDate.Text = Date.GetDateTime(Common.User_DateTime);
                BsDateAc.Text = Date.GetDateTime(Common.User_DateTime);

                BsShare2.Checked = true;
                BsShsel1.Checked = true;

                //媒體申報
                invkind = "";
                sub = "";
                customsno = "";
                //電子發票預設
                invrandom.Visible = lblinvrandom.Visible = lblEInvChange.Visible = lblEInvState.Visible = EInvChange.Visible = EInvState.Visible = false;
                EInvState.Text = "未上傳";
                invrandom.Text = "";

                BsDate.Focus();
                //進項批開
                gridInvMode.Text = Common.User_ScInvBat == 1 ? "批開" : "";
            }
            #endregion

            #region//複製時,值不變  ,但要設定某些預設值
            if (this.FormState == FormEditState.Duplicate)
            {
                loadBShopBom();

                foreach (DataRow row in dtBShopD.Rows)
                {
                    row["fono"] = "";
                    row["bono"] = "";
                    row["boid"] = "";
                }
                BsNo.Text = "";
                CardNo.Text = "";

                Discount.Text = "0";
                Discount1.Text = "0";
                GetPrvAcc.Text = "0";
                GetPrvAcc1.Text = "0";
                CashMny.Text = "0";
                CardMny.Text = "0";
                Ticket.Text = "0";
                this.SetAcctMny();

                InvNo.Text = "";
                InvDate = Date.GetDateTime(Common.User_DateTime);

                BsDate.Text = Date.GetDateTime(Common.User_DateTime);
                BsDateAc.Text = Date.GetDateTime(Common.User_DateTime);

                //媒體申報
                invkind = "";
                sub = "";
                customsno = "";

                BsDate.Focus();
                FaNo.SelectAll();
            }
            #endregion

            #region//修改時,值不變
            if (this.FormState == FormEditState.Modify)
            {
                loadBShopBom();

                BsNo.Focus();
                FaNo.SelectAll();
            }
            #endregion
            this.自定編號.ReadOnly = true;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            ThisFormState();
            btnBotoBshop.Enabled = true;
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess);
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(this.FormState = FormEditState.Duplicate, ref list);
            ThisFormState();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess);
           
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jBShop.IsExistDocument<JBS.JS.BShop>(BsNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            //進項批開
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = "select * from binvbatch where invno = '" + InvNo.Text.Trim() + "'";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                {
                    MessageBox.Show("此單據在發票系統已有批開發票，\n請刪除『已改明細發票』內中該筆發票，才可修改刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //有折讓紀錄不可修改
                cmd.CommandText = "select * from bdiscountd where invno = '" + InvNo.Text.Trim() + "'";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                {
                    MessageBox.Show("此發票編號已有折讓紀錄不可異動", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (jBShop.IsEditInCloseDay(BsDate.Text) == false)
                return;

            if (jBShop.IsPassToAcc(BsNo.Text.Trim()) == true)
                return;

            if (jBShop.IsPassToPayabl(BsNo.Text.Trim()) == true)
                return;

            if (jBShop.IsPassToRBorrow(tempBShopD) == true)
            {
                MessageBox.Show("此單據已有還出單！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (jBShop.IsModify<JBS.JS.BShop>(BsNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jBShop.upModify1<JBS.JS.BShop>(BsNo.Text.Trim());//更新修改狀態1
                var pk = jBShop.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            ThisFormState();

            //紀錄發票號碼
            if (InvNo.Text.Trim() != "")
            {
                tempinvno = InvNo.Text.Trim();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jBShop.IsModify<JBS.JS.BShop>(BsNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //進項批開
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = "select * from binvbatch where invno = '" + InvNo.Text.Trim() + "'";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                {
                    MessageBox.Show("此單據在發票系統已有批開發票，\n請刪除『已改明細發票』內中該筆發票，才可修改刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //有折讓紀錄不可刪除
            if (InvNo.Text.Trim() != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    try
                    {
                        cmd.CommandText = "select * from bdiscountd where invno = '" + InvNo.Text.Trim() + "'";
                        if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        {
                            MessageBox.Show("此發票編號已有折讓紀錄不可異動", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            if (jBShop.IsEditInCloseDay(BsDate.Text) == false)
                return;

            if (jBShop.IsPassToAcc(BsNo.Text.Trim()) == true)
                return;

            if (jBShop.IsPassToPayabl(BsNo.Text.Trim()) == true)
                return;

            if (jBShop.IsPassToRBorrow(tempBShopD) == true)
            {
                var dl = MessageBox.Show("此單據已有還出單,是否要刪除", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dl != DialogResult.OK)
                    return;
            }

            if (jBShop.IsExistDocument<JBS.JS.BShop>(BsNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            var pano = jBShop.GetThisPassToPayabl(BsNo.Text.Trim());
            jBShop.GetTempBomOnDeleting("BShopBom", BsNo.Text.Trim(), ref tempBom);

            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    var acctmny = 0M;
                    var getprvacc = 0M;
                    jBShop.GetOldAcctMnyOnDeleting(BsNo.Text.Trim(), out acctmny, out getprvacc);

                    for (int i = 0; i < tempBShopD.Rows.Count; i++)
                    {
                        //若此筆明細為採購轉入 扣掉此筆數量
                        if (tempBShopD.Rows[i]["foid"].ToString().Trim().Length > 0)
                        {
                            BackForddQty(cmd, i);
                        }
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("pano", pano);
                    cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                    cmd.CommandText = @" 
                        Delete from payabl   where ExtFlag =N'進貨' and pano = @pano COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from payabld  where ExtFlag =N'進貨' and pano = @pano COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from BShopBom where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from BShopD   where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from BShop    where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN;";
                    cmd.ExecuteNonQuery();

                    jBShop.扣庫存(cmd, tempBShopD, tempBom);

                    FrmAffixFile.FileDelete_單據刪除(cmd, BsNo.Text.Trim(), "進貨單");

                    BatchSave.進貨_Delete(dt_TempBatchProcess, cmd, "bshopD", BsNo.Text.Trim());

                    tn.Commit();

                    jBShop.BackOldFactPayabl(this.OldFact.Trim(), acctmny, getprvacc);

                    jBShop.UpdateItemItStockQty(tempBShopD, tempBom);

                    btnNext_Click(null, null);

                    MessageBox.Show("刪除完成!");
                }
                catch (Exception ex)
                {
                    if (tn != null)
                        tn.Rollback();

                    throw ex;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FrmBShop_Print frm = new FrmBShop_Print())
            {
                frm.PK = BsNo.Text.Trim();
                frm.FaNo = FaNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (BsNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new S2.FrmBShopBrowNew())
            {
                frm.TSeekNo = BsNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (jBShop.IsEditInCloseDay(BsDate.Text) == false)
                return;

            if (jBShop.IsRegisted() == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！" + Environment.NewLine + "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Focus();
                MessageBox.Show("倉庫編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (X5No.Text.ToDecimal() == 7 && EInvChange.SelectedIndex == 0 && InvTaxNo.TrimTextLenth() == 0)
            {
                InvTaxNo.Focus();
                MessageBox.Show("電子發票若要使用『交換發票』，必須輸入廠商統編", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (FaNo.TrimTextLenth() == 0)
            {
                FaNo.Focus();
                MessageBox.Show("廠商編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Writes.ForEach(r =>
                {
                    if (r is TextBoxNumberT && r.Text.Trim() == "") r.Text = "0";
                });
            }

            //檢查發票號碼是否重複
            if (InvNo.Text.Trim() != "")
            {
                if (FormState == FormEditState.Modify && tempinvno == InvNo.Text.Trim())
                {

                }
                else
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        try
                        {
                            cmd.CommandText = "select * from bshop where invno = '" + InvNo.Text.Trim() + "'";
                            if (!cmd.ExecuteScalar().IsNullOrEmpty())
                            {
                                MessageBox.Show("你輸入的發票號碼已經重複，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            cmd.CommandText = "select * from binv where invno = '" + InvNo.Text.Trim() + "'";
                            if (!cmd.ExecuteScalar().IsNullOrEmpty())
                            {
                                MessageBox.Show("你輸入的發票號碼已經重複，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }

            //發票號碼在發票系統折讓作業中有記錄則無法修改
            if (FormState == FormEditState.Modify)
            {
                if (tempinvno != "")
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        try
                        {
                            cmd.CommandText = "select * from bdiscountd where invno = '" + tempinvno + "'";
                            if (!cmd.ExecuteScalar().IsNullOrEmpty())
                            {
                                MessageBox.Show("此單據已有折讓紀錄不可修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            throw;
                        }
                    }
                }
            }

            //明細不能是空值
            jBShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtBShopD, ref dtBShopBom, SetAllMny);
            //上一方法刪除空值後，刪除對應該筆空值之批號
            BatchF.刪除無明細對應之批號資料(dtBShopD, dt_BatchProcess);

            if (Common.TPF == Common.MFT && X3No.Text.ToDecimal() != 2)
            {
                var checktaxmny = dtBShopD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPF));
                if (TaxMny.Text.ToDecimal() != checktaxmny)
                {
                    MessageBox.Show("稅前合計金額有誤！無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                //前置作業
                string faname2 = "";
                jBShop.Validate<JBS.JS.Fact>(FaNo.Text.Trim(), reader =>
                {
                    faname2 = reader["faname2"].ToString().Trim();
                });


                //先分攤費用，計算成本，之後才存明細，因為明細裡有成本欄位
                if (BsShare2.Checked)
                {
                    this.FeeShare();
                }
                else
                {
                    for (int i = 0; i < dtBShopD.Rows.Count; i++)
                    {
                        if (dtBShopD.Rows[i]["realcost"].ToDecimal() == 0)
                        {
                            dtBShopD.Rows[i]["realcost"] = dtBShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
                            dataGridViewT1.InvalidateRow(i);
                        }
                    }
                }

                System.Threading.Tasks.Task tk;
                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        var result = true;

                        //明細借轉進
                        if (dtBShopD.AsEnumerable().Any(r => r["bono"].ToString().Trim() == "批借轉進") && this.FormState == FormEditState.Append)
                        {
                            //還入單號
                            result &= jBShop.GetPkNumber<JBS.JS.RBorr>(cmd, BsDate.Text, ref BoNo);

                            PassToRBorr(cmd, faname2);
                        }
                        else if (dtBShopD.AsEnumerable().Any(r => r["bono"].ToString().Trim().Length > 0) && this.FormState == FormEditState.Append)
                        {
                            gotoRBorr(cmd);
                        }

                        //進貨單號
                        result &= jBShop.GetPkNumber<JBS.JS.BShop>(cmd, BsDate.Text, ref BsNo);

                        //進貨單號-還入單號是否有出錯
                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        //儲存主檔
                        this.AppendMasterOnSaving(cmd);

                        for (int i = 0; i < dtBShopD.Rows.Count; i++)
                        {
                            //儲存明細
                            this.AppendDetailOnSaving(cmd, i);

                            //若此筆明細為採購轉入
                            if (dtBShopD.Rows[i]["foid"].ToString().Trim().Length > 0)
                            {
                                this.AnsyForddQtyOnSaving(cmd, i);
                            }
                        }

                        //儲存組件
                        this.AppendBomOnSaving(cmd);

                        //處理沖款
                        this.PassToPayablOnSaving(cmd);

                        //處理庫存
                        jBShop.加庫存(cmd, dtBShopD, dtBShopBom);

                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, BsNo.Text.Trim(), "進貨單");
                        //批次資料
                        BatchSave.進貨_Append(dt_BatchProcess, cmd, "bshopd", BsNo.Text.Trim());

                        //完成重要資料存檔, 確認交易
                        tn.Commit();

                        //儲存完成
                        jBShop.Save(BsNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新廠商應付帳款
                            jBShop.UpdateNewFactPayabl(FaNo.Text, AcctMny.Text, GetPrvAcc.Text);

                            //更新廠商交易日期
                            jBShop.UpdateFactLastDay(BsDate.Text.Trim(), FaNo.Text.Trim());

                            //更新最近進貨日期 //若系統參數"產品進價異動"選擇"進貨自動更新" 刷新產品建檔"進價"
                            jBShop.UpdateItemDatePrice(BsDate.Text, ref dtBShopD);

                            //更新產品檔庫存量
                            jBShop.UpdateItemItStockQty(dtBShopD, dtBShopBom);
                        });
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }
                var dl = MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl == DialogResult.Yes)
                {
                    using (var frm = new FrmBShop_Print())
                    {
                        frm.PK = jBShop.GetCurrent();
                        frm.FaNo = FaNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }

                if (BoNo.Text.Trim().Length > 0)
                {
                    using (var frm = new S2.FrmSaleAndRLend())
                    {
                        frm.No = jBShop.GetCurrent();
                        frm.RNo = BoNo.Text.Trim();
                        frm.me = S2.FrmSaleAndRLend.MyEnum.BShop;

                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                ThisFormState();
               
            }


            if (this.FormState == FormEditState.Modify)
            {
                if (jBShop.IsExistDocument<JBS.JS.BShop>(BsNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

                if (jBShop.IsPassToPayabl(BsNo.Text.Trim()) == true)
                    return;

                //先分攤費用，計算成本，之後才存明細，因為明細裡有成本欄位
                if (BsShare2.Checked)
                {
                    this.FeeShare();
                }
                else
                {
                    for (int i = 0; i < dtBShopD.Rows.Count; i++)
                    {
                        if (dtBShopD.Rows[i]["realcost"].ToDecimal() == 0)
                        {
                            dtBShopD.Rows[i]["realcost"] = dtBShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
                            dataGridViewT1.InvalidateRow(i);
                        }
                    }
                }

                System.Threading.Tasks.Task tk;
                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        var acctmny = 0M;
                        var getprvacc = 0M;
                        jBShop.GetOldAcctMnyOnDeleting(BsNo.Text.Trim(), out acctmny, out getprvacc);

                        //儲存主檔
                        UpdateMasterOnSaving(cmd);

                        //刪除舊檔
                        DelteOldOnSaving(cmd);

                        for (int i = 0; i < tempBShopD.Rows.Count; i++)
                        {
                            //若此筆明細為採購轉入先扣掉採購的交貨數量
                            if (tempBShopD.Rows[i]["foid"].ToString().Trim().Length > 0)
                            {
                                BackForddQty(cmd, i);
                            }
                        }

                        for (int i = 0; i < dtBShopD.Rows.Count; i++)
                        {
                            //儲存明細
                            this.AppendDetailOnSaving(cmd, i);

                            //若此筆明細為採購轉入
                            if (dtBShopD.Rows[i]["foid"].ToString().Trim().Length > 0)
                            {
                                this.AnsyForddQtyOnSaving(cmd, i);
                            }
                        }

                        //儲存組件
                        this.AppendBomOnSaving(cmd);

                        //處理沖款
                        this.PassToPayablOnSaving(cmd);

                        //處理庫存
                        jBShop.扣庫存(cmd, tempBShopD, tempBom);
                        jBShop.加庫存(cmd, dtBShopD, dtBShopBom);

                        //批次資料
                        BatchSave.進貨_Modify(dt_TempBatchProcess, dt_BatchProcess, cmd, "bshopd", BsNo.Text.Trim());

                        //完成重要資料存檔, 確認交易
                        tn.Commit();

                        //儲存完成
                        jBShop.Save(BsNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新廠商應付帳款
                            jBShop.BackOldFactPayabl(this.OldFact, acctmny, getprvacc);
                            jBShop.UpdateNewFactPayabl(FaNo.Text.Trim(), AcctMny.Text, GetPrvAcc.Text);

                            //更新廠商交易日期
                            jBShop.UpdateFactLastDay(BsDate.Text.Trim(), FaNo.Text.Trim());

                            //更新最近進貨日期 //若系統參數"產品進價異動"選擇"進貨自動更新" 刷新產品建檔"進價"
                            jBShop.UpdateItemDatePrice(BsDate.Text.Trim(), ref dtBShopD);

                            //更新產品檔庫存量
                            jBShop.UpdateItemItStockQty(tempBShopD, tempBom, dtBShopD, dtBShopBom);
                        });
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                var dl = MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl == DialogResult.Yes)
                {
                    using (var frm = new FrmBShop_Print())
                    {
                        frm.PK = jBShop.GetCurrent();
                        frm.FaNo = FaNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }
                jBShop.upModify0<JBS.JS.BShop>(BsNo.Text.Trim());//改回0為無修改狀態
                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                ThisFormState();
            }
            
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
            cmd.Parameters.AddWithValue("bsdate1", Date.ToUSDate(BsDate.Text));
            cmd.Parameters.AddWithValue("bsdate2", BsDate.Text);
            cmd.Parameters.AddWithValue("bsdateac1", Date.ToUSDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("bsdateac2", BsDateAc.Text);
            cmd.Parameters.AddWithValue("FqNo", FqNo.Text);
            cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
            cmd.Parameters.AddWithValue("faname1", FaName11.Text);
            cmd.Parameters.AddWithValue("fatel1", FaTel11.Text);
            cmd.Parameters.AddWithValue("faper1", FaPer11.Text);
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("spno", SpNo.Text);
            cmd.Parameters.AddWithValue("spname", SpName.Text);
            cmd.Parameters.AddWithValue("stno", StNo.Text);
            cmd.Parameters.AddWithValue("stname", StName.Text);
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text);
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text);
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
            cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text);
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text);
            cmd.Parameters.AddWithValue("x3no", X3No.Text);
            cmd.Parameters.AddWithValue("rate", RATEToRate(Rate));
            cmd.Parameters.AddWithValue("x5no", X5No.Text);
            cmd.Parameters.AddWithValue("seno", SeNo.Text);
            cmd.Parameters.AddWithValue("sename", SeName.Text);
            cmd.Parameters.AddWithValue("x4no", X4No.Text);
            cmd.Parameters.AddWithValue("x4name", X4Name.Text);
            cmd.Parameters.AddWithValue("tax", Tax.Text);
            cmd.Parameters.AddWithValue("totmny", TotMny.Text);
            cmd.Parameters.AddWithValue("taxb", ToTaiwanMny(Tax));//本幣營業稅額 = 外幣營業稅額*匯率
            cmd.Parameters.AddWithValue("totmnyb", ToTaiwanMny(TotMny));//本幣總額 = 外幣總額*匯率
            cmd.Parameters.AddWithValue("discount", Discount.Text);
            cmd.Parameters.AddWithValue("cashmny", CashMny.Text);
            cmd.Parameters.AddWithValue("cardmny", CardMny.Text);
            cmd.Parameters.AddWithValue("cardno", CardNo.Text);
            cmd.Parameters.AddWithValue("ticket", Ticket.Text);
            cmd.Parameters.AddWithValue("collectmny", CollectMny.Text);
            cmd.Parameters.AddWithValue("getprvacc", GetPrvAcc.Text);
            cmd.Parameters.AddWithValue("acctmny", AcctMny.Text);
            cmd.Parameters.AddWithValue("BsMemo", BsMemo.Text);
            cmd.Parameters.AddWithValue("recordno", dataGridViewT1.Rows.Count);
            cmd.Parameters.AddWithValue("invno", InvNo.Text);
            cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(InvDate));
            cmd.Parameters.AddWithValue("invdate1", Date.ToUSDate(InvDate));
            cmd.Parameters.AddWithValue("invname", InvName.Text);
            cmd.Parameters.AddWithValue("invtaxno", InvTaxNo.Text);
            cmd.Parameters.AddWithValue("invaddr1", InvAddr1);
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("Transport", Transport.Text);
            cmd.Parameters.AddWithValue("tranpar", TranPar.Text);
            cmd.Parameters.AddWithValue("Transportb", TransportB.Text);
            cmd.Parameters.AddWithValue("Premium", Premium.Text);
            cmd.Parameters.AddWithValue("PremPar", PremPar.Text);
            cmd.Parameters.AddWithValue("Premiumb", PremiumB.Text);
            cmd.Parameters.AddWithValue("Tariff", Tariff.Text);
            cmd.Parameters.AddWithValue("TariPar", TariPar.Text);
            cmd.Parameters.AddWithValue("Tariffb", TariffB.Text);
            cmd.Parameters.AddWithValue("Postal", Postal.Text);
            cmd.Parameters.AddWithValue("ProcFee", ProcFee.Text);
            cmd.Parameters.AddWithValue("Certif", Certif.Text);
            cmd.Parameters.AddWithValue("Clearance", Clearance.Text);
            cmd.Parameters.AddWithValue("OtherFee", OtherFee.Text);
            cmd.Parameters.AddWithValue("TotFee", TotFee.Text);
            cmd.Parameters.AddWithValue("BsDate", Date.ToTWDate(BsDate.Text));
            cmd.Parameters.AddWithValue("BsDateAc", Date.ToTWDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("BsShare", getRadio(pnlBsShare));
            cmd.Parameters.AddWithValue("BsShsel", getRadio(pnlBsShsel));
            cmd.Parameters.AddWithValue("bsmemo1", Memo1);
            cmd.Parameters.AddWithValue("deno", DeNo.Text.Trim());
            cmd.Parameters.AddWithValue("dename", DeName.Text.Trim());
            cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));
            cmd.Parameters.AddWithValue("einvstate", EInvState.Text.Trim());//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvchange", EInvChange.Text.Trim());//發票傳遞方式 
            cmd.Parameters.AddWithValue("invrandom", invrandom.Text.Trim());//發票隨機防伪碼 4碼

            //媒體申報
            if (InvNo.Text.Trim() != "" && X5No.Text.Trim() != "5")
            {
                if (invkind == "")
                {
                    if (X5No.Text.Trim() == "1")
                    {
                        invkind = "21 進項三聯式、電子計算機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text.Trim() == "2")
                    {
                        invkind = "22 進項二聯式收銀機統一發票、載有稅額之其他憑證".GetUTF8(20);
                    }
                    else if (X5No.Text.Trim() == "3")
                    {
                        invkind = "22 進項二聯式收銀機統一發票、載有稅額之其他憑證".GetUTF8(20);
                    }
                    else if (X5No.Text.Trim() == "4" || X5No.Text.Trim() == "7" || X5No.Text.Trim() == "8" )
                    {
                        invkind = "25 進項三聯式收銀機統一發票及一般稅額計算之電子發票".GetUTF8(20);
                    }
                }

                if (sub == "")
                {
                    sub = "1";
                }
                if (invkind.ToString().Trim() != "")
                {
                    if (invkind.Substring(0, 2) != "28")
                        customsno = "";
                } 
                else { MessageBox.Show("進項媒體申報種類為空沒有分類到"); }
            }
            else
            {
                invkind = "";
                sub = "";
                customsno = "";
            }

            cmd.Parameters.AddWithValue("invkind", invkind);
            cmd.Parameters.AddWithValue("sub", sub);
            cmd.Parameters.AddWithValue("customsno", customsno);


            //進項批開
            if (gridInvMode.Text == "批開")
                cmd.Parameters.AddWithValue("invbatch", 1);
            else
                cmd.Parameters.AddWithValue("invbatch", 2);

            cmd.CommandText = @"
                INSERT INTO BShop (
                BsNo,bsdate1,bsdate2,bsdateac1,bsdateac2
                ,FqNo,FaNo,faname1,fatel1,faper1,emno,emname,spno,spname,stno,stname
                ,xa1no,xa1name,xa1par,taxmnyb,taxmny,x3no,rate,x5no,seno,sename
                ,x4no,x4name,tax,totmny,taxb,totmnyb,discount,cashmny
                ,cardmny,cardno,ticket,collectmny,getprvacc,acctmny,BsMemo
                ,recordno,invno,invdate,invdate1,invname,invtaxno,invaddr1
                ,appdate,edtdate,appscno,edtscno,Transport,tranpar,Transportb
                ,Premium,PremPar,Premiumb,Tariff,TariPar,Tariffb
                ,Postal,ProcFee,Certif,Clearance,OtherFee,TotFee
                ,BsDate,BsDateAc,BsShare,BsShsel,bsmemo1,deno,dename,PhotoPath 
                ,invkind,sub,customsno,invbatch,einvstate,einvchange,invrandom)
                VALUES (
                @BsNo,@bsdate1,@bsdate2,@bsdateac1,@bsdateac2
                ,@FqNo,@FaNo,@faname1,@fatel1,@faper1,@emno,@emname,@spno,@spname,@stno,@stname
                ,@xa1no,@xa1name,@xa1par,@taxmnyb,@taxmny,@x3no,@rate,@x5no,@seno,@sename
                ,@x4no,@x4name,@tax,@totmny,@taxb,@totmnyb,@discount,@cashmny
                ,@cardmny,@cardno,@ticket,@collectmny,@getprvacc,@acctmny,@BsMemo
                ,@recordno,@invno,@invdate,@invdate1,@invname,@invtaxno,@invaddr1
                ,@appdate,@edtdate,@appscno,@edtscno,@Transport,@tranpar,@Transportb
                ,@Premium,@PremPar,@Premiumb,@Tariff,@TariPar,@Tariffb
                ,@Postal,@ProcFee,@Certif,@Clearance,@OtherFee,@TotFee
                ,@BsDate,@BsDateAc,@BsShare,@BsShsel,@bsmemo1,@deno,@dename,@PhotoPath
                ,@invkind,@sub,@customsno,@invbatch,@einvstate,@einvchange,@invrandom)";
            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
            cmd.Parameters.AddWithValue("bsdate1", Date.ToUSDate(BsDate.Text));
            cmd.Parameters.AddWithValue("bsdate2", BsDate.Text);
            cmd.Parameters.AddWithValue("bsdateac1", Date.ToUSDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("bsdateac2", BsDateAc.Text);
            cmd.Parameters.AddWithValue("FqNo", FqNo.Text.Trim());
            cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("spno", SpNo.Text);
            cmd.Parameters.AddWithValue("stno", dataGridViewT1["進貨倉庫", i].Value);
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text);
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
            cmd.Parameters.AddWithValue("seno", SeNo.Text);
            cmd.Parameters.AddWithValue("sename", SeName.Text);
            cmd.Parameters.AddWithValue("x4no", X4No.Text);
            cmd.Parameters.AddWithValue("x4name", X4Name.Text);
            cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", i].Value);
            cmd.Parameters.AddWithValue("itname", dataGridViewT1["品名規格", i].Value);
            cmd.Parameters.AddWithValue("ittrait", dataGridViewT1["ittrait", i].Value);
            cmd.Parameters.AddWithValue("itunit", dataGridViewT1["單位", i].Value);
            cmd.Parameters.AddWithValue("punit", dataGridViewT1["計位", i].Value);
            cmd.Parameters.AddWithValue("itpkgqty", dataGridViewT1["包裝數量", i].Value);
            cmd.Parameters.AddWithValue("qty", dataGridViewT1["進貨數量", i].Value);
            cmd.Parameters.AddWithValue("pqty", dataGridViewT1["計價數量", i].Value);
            cmd.Parameters.AddWithValue("price", dataGridViewT1["進價", i].Value);
            cmd.Parameters.AddWithValue("prs", dataGridViewT1["折數", i].Value);
            cmd.Parameters.AddWithValue("rate", RATEToRate(Rate));
            cmd.Parameters.AddWithValue("taxprice", dataGridViewT1["稅前進價", i].Value);
            cmd.Parameters.AddWithValue("realcost", dataGridViewT1["實際成本", i].Value);
            cmd.Parameters.AddWithValue("mny", dataGridViewT1["稅前金額", i].Value);
            cmd.Parameters.AddWithValue("priceb", dataGridViewT1["本幣單價", i].Value);
            cmd.Parameters.AddWithValue("taxpriceb", dataGridViewT1["本幣稅前單價", i].Value);
            cmd.Parameters.AddWithValue("mnyb", dataGridViewT1["本幣稅前金額", i].Value);
            cmd.Parameters.AddWithValue("memo", dataGridViewT1["備註說明", i].Value);
            cmd.Parameters.AddWithValue("bomid", BsNo.Text + dtBShopD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", dtBShopD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1).ToString());
            cmd.Parameters.AddWithValue("itdesp1", dtBShopD.Rows[i]["ItDesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", dtBShopD.Rows[i]["ItDesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", dtBShopD.Rows[i]["ItDesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", dtBShopD.Rows[i]["ItDesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", dtBShopD.Rows[i]["ItDesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", dtBShopD.Rows[i]["ItDesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", dtBShopD.Rows[i]["ItDesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", dtBShopD.Rows[i]["ItDesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", dtBShopD.Rows[i]["ItDesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", dtBShopD.Rows[i]["ItDesp10"]);
            cmd.Parameters.AddWithValue("stName", dtBShopD.Rows[i]["stname"].ToString().Trim());
            cmd.Parameters.AddWithValue("BsDate", Date.ToTWDate(BsDate.Text));
            cmd.Parameters.AddWithValue("BsDateAc", Date.ToTWDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("fono", dtBShopD.Rows[i]["fono"]);
            cmd.Parameters.AddWithValue("foid", dtBShopD.Rows[i]["foid"]);
            cmd.Parameters.AddWithValue("mwidth1", dtBShopD.Rows[i]["mwidth1"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth2", dtBShopD.Rows[i]["mwidth2"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth3", dtBShopD.Rows[i]["mwidth3"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth4", dtBShopD.Rows[i]["mwidth4"].ToDecimal());
            cmd.Parameters.AddWithValue("Pformula", dtBShopD.Rows[i]["Pformula"].ToString());
            cmd.Parameters.AddWithValue("bono", dtBShopD.Rows[i]["bono"].ToString().Trim());
            cmd.Parameters.AddWithValue("boid", dtBShopD.Rows[i]["boid"]);
            if (dtBShopD.Rows[i]["bono"].ToString().Trim().Length > 0)
                cmd.Parameters.AddWithValue("cyno", BoNo.Text.Trim());
            else
                cmd.Parameters.AddWithValue("cyno", "");

            cmd.CommandText = @"
                INSERT INTO BShopD (
                BsNo,bsdate1,bsdate2,bsdateac1,bsdateac2
                ,FqNo,FaNo,emno,spno,stno,xa1no,xa1par,seno,sename
                ,x4no,x4name,itno,itname,ittrait,itunit,punit,itpkgqty,qty,pqty,price
                ,prs,rate,taxprice,realcost,mny,priceb,taxpriceb,mnyb,memo
                ,bomid,bomrec,recordno,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5
                ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10
                ,stName,BsDate,BsDateAc,fono,foid,mwidth1,mwidth2,mwidth3,mwidth4,Pformula,bono,boid,cyno ) 
                VALUES (
                @BsNo,@bsdate1,@bsdate2,@bsdateac1,@bsdateac2
                ,@FqNo,@FaNo,@emno,@spno,@stno,@xa1no,@xa1par,@seno,@sename
                ,@x4no,@x4name,@itno,@itname,@ittrait,@itunit,@punit,@itpkgqty,@qty,@pqty,@price
                ,@prs,@rate,@taxprice,@realcost,@mny,@priceb,@taxpriceb,@mnyb,@memo
                ,@bomid,@bomrec,@recordno,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5
                ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10
                ,@stName,@BsDate,@BsDateAc,@fono,@foid,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@Pformula,@bono,@boid,@cyno ) ";
            cmd.ExecuteNonQuery();
        }
        private void AnsyForddQtyOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyin", dtBShopD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qtyNotin", dtBShopD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("bomid", dtBShopD.Rows[i]["foid"].ToString());
            cmd.Parameters.AddWithValue("fono", dtBShopD.Rows[i]["fono"].ToString());
            cmd.CommandText = @"
                Update fordd set qtyin = qtyin+@qtyin, qtyNotin = qtyNotin-@qtyNotin
                Where bomid=@bomid and fono=@fono";
            cmd.ExecuteNonQuery();

            //檢查採購是否結案
            cmd.Parameters.AddWithValue("fooverflag0", "0");
            cmd.Parameters.AddWithValue("fooverflag1", "1");
            cmd.CommandText = " Update ford set fooverflag =@fooverflag0 where fono = @fono and (Select Count(*) from fordd where qtynotin > 0 and fono = @fono) > 0;";
            cmd.CommandText += "Update ford set fooverflag =@fooverflag1 where fono = @fono and (Select Count(*) from fordd where qtynotin > 0 and fono = @fono) = 0;";
            cmd.ExecuteNonQuery();
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < dtBShopBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
                cmd.Parameters.AddWithValue("BomID", BsNo.Text + dtBShopBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("BomRec", dtBShopBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", dtBShopBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", dtBShopBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", dtBShopBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", dtBShopBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", dtBShopBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", dtBShopBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", dtBShopBom.Rows[i]["itrec"]);
                cmd.Parameters.AddWithValue("itprice", dtBShopBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", dtBShopBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", dtBShopBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", dtBShopBom.Rows[i]["itnote"]);

                cmd.CommandText = @"
                    INSERT INTO BShopBom(
                    BsNo,BomID,BomRec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,itprs,itmny,itnote) VALUES (
                    @BsNo,@BomID,@BomRec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote) ";
                cmd.ExecuteNonQuery();
            }
        }
        private void PassToPayablOnSaving(SqlCommand cmd)
        {
            //儲存時檢查『應付總計』與『未付金額』是否相等
            //若相等時，刪除沖款與沖款明細
            //若不等時，沖款
            decimal totmny = 0; //應付總計
            decimal acctmny = 0;//未付金額
            decimal.TryParse(TotMny.Text, out totmny);
            decimal.TryParse(AcctMny.Text, out acctmny);
            decimal collectmny = 0;//已付金額
            decimal getprvacc = 0; //取用預付
            decimal.TryParse(CollectMny.Text, out collectmny);
            decimal.TryParse(GetPrvAcc.Text, out getprvacc);
            //沖款總額
            decimal _Total = 0;
            _Total = collectmny + getprvacc;
            //本幣總額
            decimal xa1par = 0;
            decimal _TotalB = 0;
            decimal.TryParse(Xa1Par.Text, out xa1par);
            _TotalB = _Total * xa1par;//沖款總額 * 匯率

            string pano = "";

            //刪除沖款
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());

            cmd.CommandText = "select pano from payabld where ExtFlag =N'進貨' and BsNo =@bsno COLLATE Chinese_Taiwan_Stroke_BIN";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    if (reader.Read())
                        pano = reader["pano"].ToString();
            }
            cmd.Parameters.AddWithValue("pano", pano);
            cmd.CommandText = "delete from payabl where ExtFlag =N'進貨' and PaNo =@pano COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from payabld where ExtFlag =N'進貨' and PaNo =@pano COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
            //
            if (totmny != acctmny)
            {
                //沖款主檔
                JE.MyControl.TextBoxT PaNo = new JE.MyControl.TextBoxT();
                PaNo.Name = "PaNo";
                Common.JESetSSID(SqlTable.Payabl, ref BsDate, ref PaNo, cmd);
                pano = PaNo.Text.Trim();

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("PaNo", pano);
                cmd.Parameters.AddWithValue("padate", Date.ToTWDate(BsDate.Text));
                cmd.Parameters.AddWithValue("padate1", Date.ToUSDate(BsDate.Text));
                cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                cmd.Parameters.AddWithValue("faname1", FaName11.Text);
                cmd.Parameters.AddWithValue("fatel1", FaTel11.Text);
                cmd.Parameters.AddWithValue("emno", EmNo.Text);
                cmd.Parameters.AddWithValue("emname", EmName.Text);
                cmd.Parameters.AddWithValue("cashmny", CashMny.Text);
                cmd.Parameters.AddWithValue("cardmny", CardMny.Text);
                cmd.Parameters.AddWithValue("cardno", CardNo.Text);
                cmd.Parameters.AddWithValue("ticket", Ticket.Text);
                cmd.Parameters.AddWithValue("getprvacc", GetPrvAcc.Text);
                cmd.Parameters.AddWithValue("totmny", _Total);
                cmd.Parameters.AddWithValue("actslt", 1);
                cmd.Parameters.AddWithValue("totdisc", Discount.Text);
                cmd.Parameters.AddWithValue("totreve", _Total);
                cmd.Parameters.AddWithValue("memo2", "進貨單轉入");
                cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
                cmd.Parameters.AddWithValue("Bracket", "進貨");
                cmd.Parameters.AddWithValue("recordno", 1);
                cmd.Parameters.AddWithValue("ExtFlag", "進貨");
                cmd.Parameters.AddWithValue("TotMny1", 0);
                cmd.Parameters.AddWithValue("TotExgDiff", 0);
                cmd.Parameters.AddWithValue("CheckMny", 0);
                cmd.Parameters.AddWithValue("RemitMny", 0);
                cmd.Parameters.AddWithValue("OtherMny", 0);
                cmd.Parameters.AddWithValue("AddPrvAcc", 0);
                cmd.Parameters.AddWithValue("xa1par1", Xa1Par.Text);

                cmd.CommandText = @"
                    INSERT INTO payabl (
                    PaNo,padate,padate1,FaNo,faname1,fatel1,emno,emname,cashmny,cardmny,cardno,ticket
                    ,getprvacc,totmny,actslt,totdisc,totreve,memo2,BsNo,Bracket
                    ,recordno,ExtFlag,TotMny1,TotExgDiff,CheckMny,RemitMny,OtherMny,AddPrvAcc,xa1par
                    ) VALUES (
                    @PaNo,@padate,@padate1,@FaNo,@faname1,@fatel1,@emno,@emname,@cashmny,@cardmny,@cardno,@ticket
                    ,@getprvacc,@totmny,@actslt,@totdisc,@totreve,@memo2,@BsNo,@Bracket
                    ,@recordno,@ExtFlag,@TotMny1,@TotExgDiff,@CheckMny,@RemitMny,@OtherMny,@AddPrvAcc,@xa1par1) ";
                cmd.ExecuteNonQuery();

                //沖款明細
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("PaNo", pano);
                cmd.Parameters.AddWithValue("padate", Date.ToTWDate(BsDate.Text));
                cmd.Parameters.AddWithValue("padate1", Date.ToUSDate(BsDate.Text));
                cmd.Parameters.AddWithValue("FaNo", FaNo.Text);
                cmd.Parameters.AddWithValue("emno", EmNo.Text);
                cmd.Parameters.AddWithValue("emname", EmName.Text);
                cmd.Parameters.AddWithValue("recordno", 1);
                cmd.Parameters.AddWithValue("bsdateac", Date.ToTWDate(BsDate.Text));
                cmd.Parameters.AddWithValue("bsdateac1", Date.ToUSDate(BsDate.Text));
                cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
                cmd.Parameters.AddWithValue("bracket", "進貨");
                cmd.Parameters.AddWithValue("xa1no", Xa1No.Text);
                cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text);
                cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
                cmd.Parameters.AddWithValue("totmny", TotMny.Text);
                cmd.Parameters.AddWithValue("acctmny", AcctMny.Text);
                cmd.Parameters.AddWithValue("invno", InvNo.Text);
                cmd.Parameters.AddWithValue("discount", Discount.Text);
                cmd.Parameters.AddWithValue("reverse", _Total);
                cmd.Parameters.AddWithValue("xa1par1", Xa1Par.Text);
                cmd.Parameters.AddWithValue("reverseb", _TotalB);
                cmd.Parameters.AddWithValue("exgstat", "匯兌收益");
                cmd.Parameters.AddWithValue("exgdiff", 0);//匯差
                cmd.Parameters.AddWithValue("extflag", "進貨");

                cmd.CommandText = @"
                    INSERT INTO payabld(
                    PaNo,padate,padate1,FaNo,emno,emname,recordno,bsdateac,bsdateac1,BsNo,bracket
                    ,xa1no,xa1name,xa1par,totmny,acctmny,invno,discount
                    ,reverse,xa1par1,reverseb,exgstat,exgdiff,extflag
                    ) VALUES (
                    @PaNo,@padate,@padate1,@FaNo,@emno,@emname,@recordno,@bsdateac,@bsdateac1,@BsNo,@bracket
                    ,@xa1no,@xa1name,@xa1par,@totmny,@acctmny,@invno,@discount
                    ,@reverse,@xa1par1,@reverseb,@exgstat,@exgdiff,@extflag) ";
                cmd.ExecuteNonQuery();
            }
        }
        private void UpdateMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
            cmd.Parameters.AddWithValue("BsDate", Date.ToTWDate(BsDate.Text));
            cmd.Parameters.AddWithValue("bsdate1", Date.ToUSDate(BsDate.Text));
            cmd.Parameters.AddWithValue("bsdate2", BsDate.Text);
            cmd.Parameters.AddWithValue("BsDateAc", Date.ToTWDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("bsdateac1", Date.ToUSDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("bsdateac2", BsDateAc.Text);
            cmd.Parameters.AddWithValue("FqNo", FqNo.Text);
            cmd.Parameters.AddWithValue("FaNo", FaNo.Text);
            cmd.Parameters.AddWithValue("faname1", FaName11.Text);
            cmd.Parameters.AddWithValue("fatel1", FaTel11.Text);
            cmd.Parameters.AddWithValue("faper1", FaPer11.Text);
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("spno", SpNo.Text);
            cmd.Parameters.AddWithValue("spname", SpName.Text);
            cmd.Parameters.AddWithValue("stno", StNo.Text);
            cmd.Parameters.AddWithValue("stname", StName.Text);
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text);
            cmd.Parameters.AddWithValue("Xa1Name", Xa1Name.Text);
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
            cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text);
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text);
            cmd.Parameters.AddWithValue("x3no", X3No.Text);
            cmd.Parameters.AddWithValue("rate", RATEToRate(Rate));
            cmd.Parameters.AddWithValue("x5no", X5No.Text);
            cmd.Parameters.AddWithValue("seno", SeNo.Text);
            cmd.Parameters.AddWithValue("sename", SeName.Text);
            cmd.Parameters.AddWithValue("x4no", X4No.Text);
            cmd.Parameters.AddWithValue("x4name", X4Name.Text);
            cmd.Parameters.AddWithValue("tax", Tax.Text);
            cmd.Parameters.AddWithValue("totmny", TotMny.Text);
            cmd.Parameters.AddWithValue("taxb", ToTaiwanMny(Tax));
            cmd.Parameters.AddWithValue("totmnyb", ToTaiwanMny(TotMny));
            cmd.Parameters.AddWithValue("discount", Discount.Text);
            cmd.Parameters.AddWithValue("cashmny", CashMny.Text);
            cmd.Parameters.AddWithValue("cardmny", CardMny.Text);
            cmd.Parameters.AddWithValue("cardno", CardNo.Text);
            cmd.Parameters.AddWithValue("ticket", Ticket.Text);
            cmd.Parameters.AddWithValue("collectmny", CollectMny.Text);
            cmd.Parameters.AddWithValue("getprvacc", GetPrvAcc.Text);
            cmd.Parameters.AddWithValue("acctmny", AcctMny.Text);
            cmd.Parameters.AddWithValue("BsMemo", BsMemo.Text);
            cmd.Parameters.AddWithValue("recordno", dataGridViewT1.Rows.Count);
            cmd.Parameters.AddWithValue("deno", DeNo.Text.Trim());
            cmd.Parameters.AddWithValue("dename", DeName.Text.Trim());
            cmd.Parameters.AddWithValue("invno", InvNo.Text);
            cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(InvDate));
            cmd.Parameters.AddWithValue("invdate1", Date.ToUSDate(InvDate));
            cmd.Parameters.AddWithValue("invname", InvName.Text);
            cmd.Parameters.AddWithValue("invtaxno", InvTaxNo.Text);
            cmd.Parameters.AddWithValue("invaddr1", InvAddr1);
            cmd.Parameters.AddWithValue("bsmemo1", Memo1);
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("Transport", Transport.Text);
            cmd.Parameters.AddWithValue("TranPar", TranPar.Text);
            cmd.Parameters.AddWithValue("TransportB", TransportB.Text);
            cmd.Parameters.AddWithValue("Premium", Premium.Text);
            cmd.Parameters.AddWithValue("PremPar", PremPar.Text);
            cmd.Parameters.AddWithValue("PremiumB", PremiumB.Text);
            cmd.Parameters.AddWithValue("Tariff", Tariff.Text);
            cmd.Parameters.AddWithValue("TariPar", TariPar.Text);
            cmd.Parameters.AddWithValue("TariffB", TariffB.Text);
            cmd.Parameters.AddWithValue("Postal", Postal.Text);
            cmd.Parameters.AddWithValue("ProcFee", ProcFee.Text);
            cmd.Parameters.AddWithValue("Certif", Certif.Text);
            cmd.Parameters.AddWithValue("Clearance", Clearance.Text);
            cmd.Parameters.AddWithValue("OtherFee", OtherFee.Text);
            cmd.Parameters.AddWithValue("TotFee", TotFee.Text);
            cmd.Parameters.AddWithValue("BsShare", getRadio(pnlBsShare));
            cmd.Parameters.AddWithValue("BsShsel", getRadio(pnlBsShsel));
            cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));
            cmd.Parameters.AddWithValue("einvstate", EInvState.Text.Trim());//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvchange", EInvChange.Text.Trim());//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("invrandom", invrandom.Text.Trim());//發票隨機防伪碼 4碼

            //媒體申報
            if (InvNo.Text.Trim() != "" && X5No.Text != "5")
            {
                if (invkind == "")
                {
                    if (X5No.Text.Trim() == "1")
                    {
                        invkind = "21 進項三聯式、電子計算機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text.Trim() == "2")
                    {
                        invkind = "22 進項二聯式收銀機統一發票、載有稅額之其他憑證".GetUTF8(20);
                    }
                    else if (X5No.Text.Trim() == "3")
                    {
                        invkind = "22 進項二聯式收銀機統一發票、載有稅額之其他憑證".GetUTF8(20);
                    }
                    else if (X5No.Text.Trim() == "4" || X5No.Text.Trim() == "7" || X5No.Text.Trim() == "8")
                    {
                        invkind = "25 進項三聯式收銀機統一發票及一般稅額計算之電子發票".GetUTF8(20);
                    }
                }

                if (sub == "")
                {
                    sub = "1";
                }

                if (invkind.ToString().Trim() != "")
                {
                    if (invkind.Substring(0, 2) != "28")
                        customsno = "";
                }
                else { MessageBox.Show("進項媒體申報種類為空沒有分類到"); }
            }
            else
            {
                invkind = "";
                sub = "";
                customsno = "";
            }

            cmd.Parameters.AddWithValue("invkind", invkind);
            cmd.Parameters.AddWithValue("sub", sub);
            cmd.Parameters.AddWithValue("customsno", customsno);

            //進項批開
            if (gridInvMode.Text == "批開")
                cmd.Parameters.AddWithValue("invbatch", 1);
            else
                cmd.Parameters.AddWithValue("invbatch", 2);


            cmd.CommandText = @" UPDATE BShop set 
                BsNo=@Bsno,BsDate=@BsDate,bsdate1=@bsdate1,bsdate2=@bsdate2,BsDateAc=@BsDateAc,bsdateac1=@bsdateac1,bsdateac2=@bsdateac2
                ,FqNo=@FqNo,FaNo=@FaNo,faname1=@faname1,fatel1=@fatel1,faper1=@faper1,emno=@emno,emname=@emname,spno=@spno,spname=@spname
                ,stno=@stno,stname=@stname,xa1no=@xa1no,Xa1Name=@Xa1Name,xa1par=@xa1par,taxmnyb=@taxmnyb,taxmny=@taxmny,x3no=@x3no,rate=@rate
                ,x5no=@x5no,seno=@seno,sename=@sename,x4no=@x4no,x4name=@x4name,tax=@tax,totmny=@totmny,taxb=@taxb,totmnyb=@totmnyb
                ,discount=@discount,cashmny=@cashmny,cardmny=@cardmny,cardno=@cardno,ticket=@ticket,collectmny=@collectmny,getprvacc=@getprvacc
                ,acctmny=@acctmny,BsMemo=@BsMemo,recordno=@recordno,deno=@deno,dename=@dename,invno=@invno,invdate=@invdate,invdate1=@invdate1
                ,invname=@invname,invtaxno=@invtaxno,invaddr1=@invaddr1,bsmemo1=@bsmemo1,edtdate=@edtdate,edtscno=@edtscno,Transport=@Transport
                ,TranPar=@TranPar,TransportB=@TransportB,Premium=@Premium,PremPar=@PremPar,PremiumB=@PremiumB,Tariff=@Tariff,TariPar=@TariPar
                ,TariffB=@TariffB,Postal=@Postal,ProcFee=@ProcFee,Certif=@Certif,Clearance=@Clearance,OtherFee=@OtherFee,TotFee=@TotFee
                ,BsShare=@BsShare,BsShsel=@BsShsel,PhotoPath=@PhotoPath,invkind=@invkind,sub=@sub,customsno=@customsno,invbatch=@invbatch 
                ,einvstate=@einvstate,einvchange=@einvchange,invrandom=@invrandom 
                WHERE BsNo = @BsNo ";
            cmd.ExecuteNonQuery();
        }
        private void DelteOldOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
            cmd.CommandText = " Delete from BShopD   where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN;";
            cmd.CommandText += "Delete from BShopBom where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN;";
            cmd.ExecuteNonQuery();
        }
        private void BackForddQty(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyin", tempBShopD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("qtyNotin", tempBShopD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("bomid", tempBShopD.Rows[i]["foid"].ToString());
            cmd.Parameters.AddWithValue("fono", tempBShopD.Rows[i]["fono"].ToString());
            cmd.CommandText = @"
                Update fordd set qtyin = qtyin-@qtyin, qtyNotin = qtyNotin+@qtyNotin
                Where bomid=@bomid and fono=@fono";
            cmd.ExecuteNonQuery();

            //檢查採購是否結案
            cmd.Parameters.AddWithValue("fooverflag0", "0");
            cmd.Parameters.AddWithValue("fooverflag1", "1");
            cmd.CommandText = " Update ford set fooverflag =@fooverflag0 where fono = @fono and (Select Count(*) from fordd where qtynotin > 0 and fono = @fono) > 0;";
            cmd.CommandText += "Update ford set fooverflag =@fooverflag1 where fono = @fono and (Select Count(*) from fordd where qtynotin > 0 and fono = @fono) = 0;";
            cmd.ExecuteNonQuery();
        }
        //弘恩批次借轉還
        private void PassToRBorr(SqlCommand cmd, string faname2)
        {
            // 主檔參數
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("bono", BoNo.Text.Trim());
            cmd.Parameters.AddWithValue("bodate", Date.ToTWDate(BsDate.Text.Trim()));
            cmd.Parameters.AddWithValue("bodate1", Date.ToUSDate(BsDate.Text.Trim()));
            cmd.Parameters.AddWithValue("bodata2", BsDate.Text.Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("stnoo", "BIN");
            cmd.Parameters.AddWithValue("stnameo", "借入倉庫");
            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
            cmd.Parameters.AddWithValue("faname1", FaName11.Text.Trim());
            cmd.Parameters.AddWithValue("faname2", faname2);
            cmd.Parameters.AddWithValue("fatel1", FaTel11.Text.GetUTF8(20).Trim());
            cmd.Parameters.AddWithValue("faper1", FaPer11.Text.GetUTF8(10).Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxmnyb", 0.0);
            cmd.Parameters.AddWithValue("taxmny", 0.0);
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
            cmd.Parameters.AddWithValue("tax", 0.0);
            cmd.Parameters.AddWithValue("taxb", 0.0);
            cmd.Parameters.AddWithValue("totmny", 0.0);
            cmd.Parameters.AddWithValue("totmnyb", 0.0);
            cmd.Parameters.AddWithValue("bomemo", BsMemo.Text.Trim());
            cmd.Parameters.AddWithValue("bomemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("recordno", 0);

            //明細參數
            cmd.Parameters.AddWithValue("cono", "");
            cmd.Parameters.AddWithValue("itno", "");
            cmd.Parameters.AddWithValue("itname", "");
            cmd.Parameters.AddWithValue("ittrait", "");
            cmd.Parameters.AddWithValue("itunit", "");
            cmd.Parameters.AddWithValue("itpkgqty", 1.0);
            cmd.Parameters.AddWithValue("qty", 0.0);
            cmd.Parameters.AddWithValue("price", 0.0);
            cmd.Parameters.AddWithValue("prs", 1.0);
            cmd.Parameters.AddWithValue("taxprice", 0.0);
            cmd.Parameters.AddWithValue("mny", 0.0);
            cmd.Parameters.AddWithValue("priceb", 0.0);
            cmd.Parameters.AddWithValue("taxpriceb", 0.0);
            cmd.Parameters.AddWithValue("mnyb", 0.0);
            cmd.Parameters.AddWithValue("memo", "");
            cmd.Parameters.AddWithValue("lowzero", "");
            cmd.Parameters.AddWithValue("bomid", "");
            cmd.Parameters.AddWithValue("bomrec", "");
            cmd.Parameters.AddWithValue("sltflag", "");
            cmd.Parameters.AddWithValue("extflag", "");
            cmd.Parameters.AddWithValue("itdesp1", "");
            cmd.Parameters.AddWithValue("itdesp2", "");
            cmd.Parameters.AddWithValue("itdesp3", "");
            cmd.Parameters.AddWithValue("itdesp4", "");
            cmd.Parameters.AddWithValue("itdesp5", "");
            cmd.Parameters.AddWithValue("itdesp6", "");
            cmd.Parameters.AddWithValue("itdesp7", "");
            cmd.Parameters.AddWithValue("itdesp8", "");
            cmd.Parameters.AddWithValue("itdesp9", "");
            cmd.Parameters.AddWithValue("itdesp10", "");
            cmd.Parameters.AddWithValue("BorrNo", "");
            cmd.Parameters.AddWithValue("BorrBid", "");
            cmd.Parameters.AddWithValue("qtynotout", 0.0);

            //組件參數
            cmd.Parameters.AddWithValue("itqty", 0.0);
            cmd.Parameters.AddWithValue("itpareprs", 1.0);
            cmd.Parameters.AddWithValue("itrec", 0.0);
            cmd.Parameters.AddWithValue("itprice", 0.0);
            cmd.Parameters.AddWithValue("itprs", 1.0);
            cmd.Parameters.AddWithValue("itmny", 0.0);
            cmd.Parameters.AddWithValue("itnote", "");
            cmd.Parameters.AddWithValue("itsource", "");
            cmd.Parameters.AddWithValue("itbuypri", "");
            cmd.Parameters.AddWithValue("itbuymny", "");

            DataTable bonoBom = dtBShopBom.Clone();
            DataTable boms = dtBShopBom.Clone();
            var rec = 1;
            var itrec = 1;
            var bomid = "";
            var qty = 0M;

            //排序借轉進明細, 和進貨明細順序一樣
            borrtemp.AcceptChanges();
            var rws = borrtemp.AsEnumerable().Where(r => r["勾選"].ToString().Trim().Length > 0);
            if (rws.Count() == 0) borrtemp.Clear();
            else
            {
                var tsort = borrtemp.Clone();
                for (int i = 0; i < dtBShopD.Rows.Count; i++)
                {
                    if (dtBShopD.Rows[i]["bono"].ToString().Trim().Length == 0)
                        continue;

                    var itno = dtBShopD.Rows[i]["itno"].ToString().Trim();
                    var trows = rws.Where(r => r["itno"].ToString().Trim() == itno);
                    if (trows.Count() > 0)
                    {
                        for (int j = 0; j < trows.Count(); j++)
                        {
                            tsort.ImportRow(trows.ElementAt(j));
                        }
                    }
                }
                borrtemp = tsort.Copy();
                tsort.Clear();
                tsort.Dispose();
            }

            for (int i = 0; i < borrtemp.Rows.Count; i++, rec++)
            {
                //還入數量 = 未還量 - tempqty
                qty = borrtemp.Rows[i]["qtynotout"].ToDecimal("f" + Common.Q) - borrtemp.Rows[i]["tempqty"].ToDecimal("f" + Common.Q);
                borrtemp.Rows[i]["qty"] = qty.ToDecimal("f" + Common.Q);
                cmd.Parameters["qty"].Value = qty.ToDecimal("f" + Common.Q);
                //先異動未還量
                cmd.Parameters["BorrNo"].Value = borrtemp.Rows[i]["bono"].ToString().Trim();
                cmd.Parameters["BorrBid"].Value = borrtemp.Rows[i]["bomid"].ToString().Trim();
                cmd.Parameters["qtynotout"].Value = borrtemp.Rows[i]["tempqty"].ToDecimal("f" + Common.Q);
                cmd.CommandText = "update borrd set qtynotout=(@qtynotout) where bono=(@BorrNo) and bomid=(@BorrBid)";
                cmd.ExecuteNonQuery();
                //先取得組件
                boms.Clear();
                cmd.CommandText = "Select * from borrbom where bomid=(@BorrBid)";
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(boms);
                }
                //再洗掉bomid
                cmd.Parameters["cono"].Value = borrtemp.Rows[i]["cono"].ToString().Trim();
                cmd.Parameters["stno"].Value = borrtemp.Rows[i]["stno"].ToString().Trim();
                cmd.Parameters["itno"].Value = borrtemp.Rows[i]["itno"].ToString().Trim();
                cmd.Parameters["itname"].Value = borrtemp.Rows[i]["itname"].ToString();
                cmd.Parameters["ittrait"].Value = borrtemp.Rows[i]["ittrait"].ToString().Trim();
                cmd.Parameters["itunit"].Value = borrtemp.Rows[i]["itunit"].ToString().Trim();
                cmd.Parameters["itpkgqty"].Value = borrtemp.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                //
                SetRow_TaxPrice1(borrtemp.Rows[i]);
                SetRow_Mny1(borrtemp.Rows[i]);
                // 
                cmd.Parameters["price"].Value = borrtemp.Rows[i]["price"].ToDecimal("f" + Common.MF);
                cmd.Parameters["prs"].Value = borrtemp.Rows[i]["prs"].ToDecimal("f3");
                cmd.Parameters["rate"].Value = borrtemp.Rows[i]["rate"].ToDecimal("f2");
                cmd.Parameters["taxprice"].Value = borrtemp.Rows[i]["taxprice"].ToDecimal("f6");
                cmd.Parameters["mny"].Value = borrtemp.Rows[i]["mny"].ToDecimal("f" + Common.TPS);
                cmd.Parameters["priceb"].Value = borrtemp.Rows[i]["priceb"].ToDecimal("f" + Common.M);
                cmd.Parameters["taxpriceb"].Value = borrtemp.Rows[i]["taxpriceb"].ToDecimal("f6");
                cmd.Parameters["mnyb"].Value = borrtemp.Rows[i]["mnyb"].ToDecimal("f" + Common.M);
                cmd.Parameters["memo"].Value = borrtemp.Rows[i]["memo"].ToString().Trim();
                cmd.Parameters["lowzero"].Value = borrtemp.Rows[i]["lowzero"].ToString().Trim();
                bomid = BoNo.Text.Trim() + rec.ToString().PadLeft(10, '0');
                borrtemp.Rows[i]["bomid"] = bomid;
                borrtemp.Rows[i]["bomrec"] = rec;
                borrtemp.Rows[i]["recordno"] = rec;
                cmd.Parameters["bomid"].Value = bomid;
                cmd.Parameters["bomrec"].Value = rec;
                cmd.Parameters["recordno"].Value = rec;
                cmd.Parameters["sltflag"].Value = borrtemp.Rows[i]["sltflag"].ToString().Trim();
                cmd.Parameters["extflag"].Value = borrtemp.Rows[i]["extflag"].ToString().Trim();
                cmd.Parameters["itdesp1"].Value = borrtemp.Rows[i]["itdesp1"].ToString().Trim();
                cmd.Parameters["itdesp2"].Value = borrtemp.Rows[i]["itdesp2"].ToString().Trim();
                cmd.Parameters["itdesp3"].Value = borrtemp.Rows[i]["itdesp3"].ToString().Trim();
                cmd.Parameters["itdesp4"].Value = borrtemp.Rows[i]["itdesp4"].ToString().Trim();
                cmd.Parameters["itdesp5"].Value = borrtemp.Rows[i]["itdesp5"].ToString().Trim();
                cmd.Parameters["itdesp6"].Value = borrtemp.Rows[i]["itdesp6"].ToString().Trim();
                cmd.Parameters["itdesp7"].Value = borrtemp.Rows[i]["itdesp7"].ToString().Trim();
                cmd.Parameters["itdesp8"].Value = borrtemp.Rows[i]["itdesp8"].ToString().Trim();
                cmd.Parameters["itdesp9"].Value = borrtemp.Rows[i]["itdesp9"].ToString().Trim();
                cmd.Parameters["itdesp10"].Value = borrtemp.Rows[i]["itdesp10"].ToString().Trim();
                cmd.Parameters["stname"].Value = borrtemp.Rows[i]["stname"].ToString().Trim();

                //
                cmd.CommandText = @"INSERT INTO [dbo].[rborrd] 
                ([bono],[bodate],[bodate1],[cono],[fano],[stno],[emno],[xa1no],[xa1par],[itno],[itname],[ittrait],[itunit],[itpkgqty],[qty],[price],[prs]
                ,[rate],[taxprice],[mny],[priceb],[taxpriceb],[mnyb],[memo],[lowzero],[bomid],[bomrec],[recordno],[sltflag],[extflag],[itdesp1],[itdesp2],[itdesp3]
                ,[itdesp4],[itdesp5],[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10],[stname],[stnoo],[stnameo],[BorrNo],[borrid])
                VALUES (@bono,@bodate,@bodate1,@cono,@fano,@stno,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@itpkgqty,@qty,@price,@prs
                ,@rate,@taxprice,@mny,@priceb,@taxpriceb,@mnyb,@memo,@lowzero,@bomid,@bomrec,@recordno,@sltflag,@extflag,@itdesp1,@itdesp2,@itdesp3
                ,@itdesp4,@itdesp5,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@stname,@stnoo,@stnameo,@BorrNo,@BorrBid)";
                cmd.ExecuteNonQuery();

                for (int j = 0; j < boms.Rows.Count; j++, itrec++)
                {
                    boms.Rows[j]["bomrec"] = rec;
                    boms.Rows[j]["bomid"] = bomid;
                    cmd.Parameters["bomrec"].Value = rec;
                    cmd.Parameters["bomid"].Value = bomid;
                    cmd.Parameters["itno"].Value = boms.Rows[j]["itno"].ToString().Trim();
                    cmd.Parameters["itname"].Value = boms.Rows[j]["itname"].ToString();
                    cmd.Parameters["itunit"].Value = boms.Rows[j]["itunit"].ToString().Trim();
                    cmd.Parameters["itqty"].Value = boms.Rows[j]["itqty"].ToDecimal();
                    cmd.Parameters["itpareprs"].Value = boms.Rows[j]["itpareprs"].ToDecimal();
                    cmd.Parameters["itpkgqty"].Value = boms.Rows[j]["itpkgqty"].ToDecimal();
                    cmd.Parameters["itrec"].Value = itrec.ToString();
                    cmd.Parameters["itprice"].Value = boms.Rows[j]["itprice"].ToDecimal();
                    cmd.Parameters["itprs"].Value = boms.Rows[j]["itprs"].ToDecimal();
                    cmd.Parameters["itmny"].Value = boms.Rows[j]["itmny"].ToDecimal();
                    cmd.Parameters["itnote"].Value = boms.Rows[j]["itnote"].ToString().Trim();

                    cmd.CommandText = @"INSERT INTO [dbo].[rBorrbom]
                    ([bono],[bomid],[bomrec],[itno],[itname],[itunit],[itqty],[itpareprs],[itpkgqty],[itrec],[itprice],[itprs],[itmny],[itnote])
                    VALUES(@bono,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote)";
                    cmd.ExecuteNonQuery();
                    bonoBom.ImportRow(boms.Rows[j]);
                    bonoBom.AcceptChanges();
                }
            }

            //主檔
            decimal[] mnys = SetRlendAllMny(ref borrtemp);
            cmd.Parameters["TaxMnyb"].Value = (mnys[0] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M);
            cmd.Parameters["TaxMny"].Value = mnys[0].ToDecimal("f" + Common.MFT);
            cmd.Parameters["Tax"].Value = mnys[1].ToDecimal("f" + Common.TF);
            cmd.Parameters["TotMny"].Value = mnys[2].ToDecimal("f" + Common.MFT);
            cmd.Parameters["Taxb"].Value = (mnys[1] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["TotMnyb"].Value = (mnys[2] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["recordno"].Value = borrtemp.Rows.Count;

            cmd.CommandText = @"insert into rBorr (
            bono,bodate,bodate1,stno,stname,stnoo,stnameo
            ,fano,faname1,faname2,fatel1,faper1,emno,emname,xa1no,xa1name,xa1par
            ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,bomemo,bomemo1,appdate,edtdate,appscno,edtscno
            ,recordno) values 
            (@bono,@bodate,@bodate1,@stno,@stname,@stnoo,@stnameo
            ,@fano,@faname1,@faname2,@fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par
            ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@bomemo,@bomemo1,@appdate,@edtdate,@appscno,@edtscno
            ,@recordno) ";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"
            UPDATE Borr
            SET Borr.booverflag = 1
            FROM (
	                select Borr.bono 
	                from borrd 
	                left join Borr on borrd.bono=Borr.bono group by Borr.bono having MAX(borrd.qtynotout) <= 0
	                ) A
            where Borr.bono = A.bono ";
            cmd.ExecuteNonQuery();

            JBS.JS.RBorr jRBorr = new JBS.JS.RBorr();
            jRBorr.加庫存(cmd, borrtemp, bonoBom, "stnoo");
            jRBorr.扣庫存(cmd, borrtemp, bonoBom, "stno");
        }
        private void SetRow_TaxPrice1(DataRow row)
        {
            var taxprice = row["price"].ToDecimal() * row["prs"].ToDecimal();
            switch (X3No.Text.Trim())
            {
                case "1":
                case "3":
                case "4":
                    row["taxprice"] = taxprice.ToDecimal("f6");
                    break;
                case "2":
                    row["taxprice"] = (taxprice / (1 + Common.Sys_Rate)).ToDecimal("f6");
                    break;
            }
        }
        private void SetRow_Mny1(DataRow row)
        {
            var qty = row["qty"].ToDecimal("f" + Common.Q);
            var price = row["price"].ToDecimal("f" + Common.MF);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPF);

            var par = Xa1Par.Text.Trim().ToDecimal();
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPS).ToDecimal("f" + Common.M);
        }
        private decimal[] SetRlendAllMny(ref DataTable temp)
        {
            var taxmny = 0M;
            var tax = 0M;
            var totmny = 0M;
            var sum = 0M;

            sum = temp.AsEnumerable().Sum(r => r["Mny"].ToDecimal("f" + Common.TPF)).ToDecimal("f" + Common.MFT);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TF);
                taxmny = sum;
                totmny = (sum + tax).ToDecimal("f" + Common.MFT);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                totmny = temp.AsEnumerable().Sum(r => r["qty"].ToDecimal("f" + Common.Q) * r["price"].ToDecimal("f" + Common.MF) * r["prs"].ToDecimal()).ToDecimal("f" + Common.MFT);
                tax = ((totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate).ToDecimal("f" + Common.TF);
                taxmny = (totmny - tax).ToDecimal("f" + Common.MFT);
            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                tax = 0;
                totmny = taxmny = sum.ToDecimal("f" + Common.MFT);
            }
            return new decimal[] { taxmny, tax, totmny };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jBShop.Cancel();
            writeToTxt(pk);

            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            btnBotoBshop.Enabled = false;
            jBShop.upModify0<JBS.JS.BShop>(BsNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        void GetSystemPrice(DataRow row, int index)
        {
            string sql = "";
            var itno = row["itno"].ToString().Trim();
            var unit = row["itunit"].ToString().Trim();
            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@itno", itno);
                        cmd.CommandText = "select * from item where itno=(@itno)";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string itunit = reader["itunit"].ToString().Trim();
                                string itunitp = reader["itunitp"].ToString().Trim();
                                if (unit == itunitp && unit != "")
                                {
                                    row["price"] = reader["itbuyprip"].ToDecimal("f" + Common.MF);
                                }
                                else
                                {
                                    row["price"] = reader["itbuypri"].ToDecimal("f" + Common.MF);
                                }
                                row["prs"] = "1.000";
                            }
                        }
                    }
                    if (Common.Sys_BuyPrice == 2)
                    {
                        //歷史進價(最後一次交易進價)
                        sql = " select * from bshopd where itno=(@itno)"
                            + " and itunit=(@itunit)"
                            + " and itpkgqty=(@itpkgqty)"
                            + " and fano=(@fano)"
                            + " order by bsdate desc";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@itunit", unit);
                            cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);
                            cmd.Parameters.AddWithValue("@fano", FaNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["prs"].ToDecimal("f3");
                                    row["price"] = reader["price"].ToDecimal("f" + Common.MF);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_BuyPrice == 3)
                    {
                        //類別折數(取產品建檔進價/包裝進價，折數取進價等級建檔裡的折數)
                        sql = " select b.*,i.itno,f.fano from buygrad as b "
                            + " inner join item as i on b.kino=i.kino and itno=(@itno) "
                            + " inner join fact as f on b.x12no=f.fax12no and f.fano=(@fano) ";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@fano", FaNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) row["prs"] = reader["buprs"].ToDecimal("f3");
                            }
                        }
                    }
                    else if (Common.Sys_BuyPrice == 4)
                    {
                        row["price"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void SetRow_TaxPrice(DataRow row)
        {
            var taxprice = row["price"].ToDecimal() * row["prs"].ToDecimal();
            switch (X3No.Text.Trim())
            {
                case "1":
                case "3":
                case "4":
                    row["taxprice"] = taxprice.ToDecimal("f6");
                    break;
                case "2":
                    row["taxprice"] = (taxprice / (1 + Common.Sys_Rate)).ToDecimal("f6");
                    break;
            }
        }
        void SetRow_Mny(DataRow row)
        {
            var qty = row["Pqty"].ToDecimal("f" + Common.Q);
            var price = row["price"].ToDecimal("f" + Common.MF);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPF);

            var par = Xa1Par.Text.Trim().ToDecimal();
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPF).ToDecimal("f" + Common.M);
        }
        void SetAllMny()
        {
            var tax = 0M;
            var par = Xa1Par.Text.ToDecimal();
            var sum = dtBShopD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f"+Common.TPF)).ToDecimal("f" + Common.MFT);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TF);
                TaxMny.Text = TaxMny1.Text = sum.ToString("f" + Common.MFT);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = Tax1.Text = tax.ToString("f" + Common.TF);
                TotMny.Text = TotMny1.Text = (sum + tax).ToString("f" + Common.MFT);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                var totmny = dtBShopD.AsEnumerable().Sum(r => r["Pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MF)).ToDecimal("f" + Common.MFT);
                tax = (totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate;

                TotMny.Text = TotMny1.Text = totmny.ToString("f" + Common.MFT);
                tax = tax.ToDecimal("f" + Common.TF);
                Tax.Text = Tax1.Text = tax.ToString();
                TaxMny.Text = TaxMny1.Text = (totmny - tax).ToString("f" + Common.MFT);
                TaxMnyB.Text = (TaxMny.Text.ToDecimal() * par).ToString("f" + Common.M);
            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                TaxMny.Text = TaxMny1.Text = sum.ToString("f" + Common.MFT);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = Tax1.Text = tax.ToString("f" + Common.TF);
                TotMny.Text = TotMny1.Text = sum.ToString("f" + Common.MFT);
            }
            SetAcctMny();
        }
        void SetAcctMny()
        {
            //未付金額 = 應退總額-折扣金額-取用預付-已退金額
            //                                => 已退金額 = 現金金額 + 刷卡金額 + 禮卷金額
            decimal acctmny = 0;   //未付金額
            decimal totmny = 0;    //應退總額
            decimal discount = 0;  //折扣金額
            decimal getprvacc = 0; //取用預付
            decimal collectmny = 0;//已退金額
            decimal cashmny = 0;   //現金金額
            decimal cardmny = 0;   //刷卡金額
            decimal ticket = 0;    //禮卷金額
            //計算『已退金額』
            decimal.TryParse(CashMny.Text, out cashmny);
            decimal.TryParse(CardMny.Text, out cardmny);
            decimal.TryParse(Ticket.Text, out ticket);
            collectmny = cashmny + cardmny + ticket;
            CollectMny.Text = collectmny.ToString("f" + Common.MFT);
            CollectMny1.Text = collectmny.ToString("f" + Common.MFT);
            //計算『未付金額』
            decimal.TryParse(TotMny.Text, out totmny);
            decimal.TryParse(Discount.Text, out discount);
            decimal.TryParse(GetPrvAcc.Text, out getprvacc);
            acctmny = totmny - discount - getprvacc - collectmny;
            AcctMny.Text = acctmny.ToString("f" + Common.MFT);
            AcctMny1.Text = acctmny.ToString("f" + Common.MFT);
        }


        void gotoRBorr(SqlCommand cmd)
        {
            DataTable bonoD = new DataTable();//借入轉進貨檔
            DataTable bonoBom = new DataTable();//借入轉進貨暫存檔

            string bono = "";
            JE.MyControl.TextBoxT BoNo = new JE.MyControl.TextBoxT();
            BoNo.Name = "BoNo";
            Common.JESetSSID(SqlTable.RBorr, ref BsDate, ref BoNo, cmd);
            bono = BoNo.Text.Trim();

            // 主檔參數
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("bono", bono.Trim());
            cmd.Parameters.AddWithValue("bodate", Date.ToTWDate(BsDate.Text.Trim()));
            cmd.Parameters.AddWithValue("bodate1", Date.ToUSDate(BsDate.Text.Trim()));
            cmd.Parameters.AddWithValue("bodata2", BsDate.Text.Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("stnoo", "BIN");
            cmd.Parameters.AddWithValue("stnameo", "借入倉庫");
            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
            cmd.Parameters.AddWithValue("faname1", FaName11.Text.Trim());
            var faname2 = "";
            jBShop.Validate<JBS.JS.Fact>(FaNo.Text.Trim(), row => faname2 = row["faname2"].ToString());

            cmd.Parameters.AddWithValue("faname2", faname2);
            cmd.Parameters.AddWithValue("fatel1", FaTel11.Text.GetUTF8(20).Trim());
            cmd.Parameters.AddWithValue("faper1", FaPer11.Text.GetUTF8(10).Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxmnyb", 0.0);
            cmd.Parameters.AddWithValue("taxmny", 0.0);
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
            cmd.Parameters.AddWithValue("tax", 0.0);
            cmd.Parameters.AddWithValue("taxb", 0.0);
            cmd.Parameters.AddWithValue("totmny", 0.0);
            cmd.Parameters.AddWithValue("totmnyb", 0.0);
            cmd.Parameters.AddWithValue("bomemo", BsMemo.Text.Trim());
            cmd.Parameters.AddWithValue("bomemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("recordno", 0);

            //明細參數
            cmd.Parameters.AddWithValue("cono", "");
            cmd.Parameters.AddWithValue("itno", "");
            cmd.Parameters.AddWithValue("itname", "");
            cmd.Parameters.AddWithValue("ittrait", "");
            cmd.Parameters.AddWithValue("itunit", "");
            cmd.Parameters.AddWithValue("itpkgqty", 1);
            cmd.Parameters.AddWithValue("qty", 0);
            cmd.Parameters.AddWithValue("price", 0);
            cmd.Parameters.AddWithValue("prs", 1);
            cmd.Parameters.AddWithValue("taxprice", 0);
            cmd.Parameters.AddWithValue("mny", 0);
            cmd.Parameters.AddWithValue("priceb", 0);
            cmd.Parameters.AddWithValue("taxpriceb", 0);
            cmd.Parameters.AddWithValue("mnyb", 0);
            cmd.Parameters.AddWithValue("memo", "");
            cmd.Parameters.AddWithValue("lowzero", "");
            cmd.Parameters.AddWithValue("bomid", "");
            cmd.Parameters.AddWithValue("bomrec", "");
            cmd.Parameters.AddWithValue("sltflag", "");
            cmd.Parameters.AddWithValue("extflag", "");
            cmd.Parameters.AddWithValue("itdesp1", "");
            cmd.Parameters.AddWithValue("itdesp2", "");
            cmd.Parameters.AddWithValue("itdesp3", "");
            cmd.Parameters.AddWithValue("itdesp4", "");
            cmd.Parameters.AddWithValue("itdesp5", "");
            cmd.Parameters.AddWithValue("itdesp6", "");
            cmd.Parameters.AddWithValue("itdesp7", "");
            cmd.Parameters.AddWithValue("itdesp8", "");
            cmd.Parameters.AddWithValue("itdesp9", "");
            cmd.Parameters.AddWithValue("itdesp10", "");
            cmd.Parameters.AddWithValue("BorrNo", "");
            cmd.Parameters.AddWithValue("boid", "");
            cmd.Parameters.AddWithValue("qtynotout", 0);

            //組件參數
            cmd.Parameters.AddWithValue("itqty", 0);
            cmd.Parameters.AddWithValue("itpareprs", 1);
            cmd.Parameters.AddWithValue("itrec", 0);
            cmd.Parameters.AddWithValue("itprice", 0);
            cmd.Parameters.AddWithValue("itprs", 1);
            cmd.Parameters.AddWithValue("itmny", 0);
            cmd.Parameters.AddWithValue("itnote", "");
            cmd.Parameters.AddWithValue("itsource", "");
            cmd.Parameters.AddWithValue("itbuypri", "");
            cmd.Parameters.AddWithValue("itbuymny", "");

            //明細
            var rows = dtBShopD.AsEnumerable().Where(r => r["bono"].ToString().Trim().Length > 0);
            if (rows.Count() > 0)
                bonoD = rows.CopyToDataTable();

            bonoBom = dtBShopBom.Clone();
            var recordno = 1;
            for (int i = 0; i < rows.Count(); i++)
            {

                cmd.Parameters["cono"].Value = rows.ElementAt(i)["cono"].ToString().Trim();
                cmd.Parameters["stno"].Value = rows.ElementAt(i)["stno"].ToString().Trim();
                cmd.Parameters["itno"].Value = rows.ElementAt(i)["itno"].ToString().Trim();
                cmd.Parameters["itname"].Value = rows.ElementAt(i)["itname"].ToString();
                cmd.Parameters["ittrait"].Value = rows.ElementAt(i)["ittrait"].ToString().Trim();
                cmd.Parameters["itunit"].Value = rows.ElementAt(i)["itunit"].ToString().Trim();
                cmd.Parameters["itpkgqty"].Value = rows.ElementAt(i)["itpkgqty"].ToDecimal();
                cmd.Parameters["qty"].Value = rows.ElementAt(i)["qty"].ToDecimal();
                cmd.Parameters["price"].Value = rows.ElementAt(i)["price"].ToDecimal();
                cmd.Parameters["prs"].Value = rows.ElementAt(i)["prs"].ToDecimal();
                cmd.Parameters["rate"].Value = rows.ElementAt(i)["rate"].ToDecimal();
                cmd.Parameters["taxprice"].Value = rows.ElementAt(i)["taxprice"].ToDecimal();
                cmd.Parameters["mny"].Value = rows.ElementAt(i)["mny"].ToDecimal();
                cmd.Parameters["priceb"].Value = rows.ElementAt(i)["priceb"].ToDecimal();
                cmd.Parameters["taxpriceb"].Value = rows.ElementAt(i)["taxpriceb"].ToDecimal();
                cmd.Parameters["mnyb"].Value = rows.ElementAt(i)["mnyb"].ToDecimal();
                cmd.Parameters["memo"].Value = rows.ElementAt(i)["memo"].ToString().Trim();
                cmd.Parameters["lowzero"].Value = rows.ElementAt(i)["lowzero"].ToString().Trim();
                var rec = rows.ElementAt(i)["bomrec"].ToString().Trim();
                var bomid = bono.Trim() + rec.PadLeft(10, '0');
                cmd.Parameters["bomid"].Value = bomid;
                cmd.Parameters["bomrec"].Value = rec;
                cmd.Parameters["recordno"].Value = recordno;
                recordno += 1;
                cmd.Parameters["sltflag"].Value = rows.ElementAt(i)["sltflag"].ToString().Trim();
                cmd.Parameters["extflag"].Value = rows.ElementAt(i)["extflag"].ToString().Trim();
                cmd.Parameters["itdesp1"].Value = rows.ElementAt(i)["itdesp1"].ToString().Trim();
                cmd.Parameters["itdesp2"].Value = rows.ElementAt(i)["itdesp2"].ToString().Trim();
                cmd.Parameters["itdesp3"].Value = rows.ElementAt(i)["itdesp3"].ToString().Trim();
                cmd.Parameters["itdesp4"].Value = rows.ElementAt(i)["itdesp4"].ToString().Trim();
                cmd.Parameters["itdesp5"].Value = rows.ElementAt(i)["itdesp5"].ToString().Trim();
                cmd.Parameters["itdesp6"].Value = rows.ElementAt(i)["itdesp6"].ToString().Trim();
                cmd.Parameters["itdesp7"].Value = rows.ElementAt(i)["itdesp7"].ToString().Trim();
                cmd.Parameters["itdesp8"].Value = rows.ElementAt(i)["itdesp8"].ToString().Trim();
                cmd.Parameters["itdesp9"].Value = rows.ElementAt(i)["itdesp9"].ToString().Trim();
                cmd.Parameters["itdesp10"].Value = rows.ElementAt(i)["itdesp10"].ToString().Trim();
                cmd.Parameters["stname"].Value = rows.ElementAt(i)["stname"].ToString().Trim();
                cmd.Parameters["BorrNo"].Value = rows.ElementAt(i)["bono"].ToString().Trim();
                cmd.Parameters["boid"].Value = rows.ElementAt(i)["boid"];

                cmd.CommandText = "INSERT INTO [dbo].[rborrd] "
                   + "([bono],[bodate],[bodate1],[cono],[fano],[stno],[emno],[xa1no],[xa1par],[itno],[itname],[ittrait],[itunit],[itpkgqty],[qty],[price],[prs]"
                   + ",[rate],[taxprice],[mny],[priceb],[taxpriceb],[mnyb],[memo],[lowzero],[bomid],[bomrec],[recordno],[sltflag],[extflag],[itdesp1],[itdesp2],[itdesp3]"
                   + ",[itdesp4],[itdesp5],[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10],[stname],[stnoo],[stnameo],[BorrNo],[borrid])"
                   + "VALUES (@bono,@bodate,@bodate1,@cono,@fano,@stno,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@itpkgqty,@qty,@price,@prs"
                   + ",@rate,@taxprice,@mny,@priceb,@taxpriceb,@mnyb,@memo,@lowzero,@bomid,@bomrec,@recordno,@sltflag,@extflag,@itdesp1,@itdesp2,@itdesp3"
                   + ",@itdesp4,@itdesp5,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@stname,@stnoo,@stnameo,@BorrNo,@boid)";
                cmd.ExecuteNonQuery();

                if (rows.ElementAt(i)["boid"].ToString().Trim() != "" && rows.ElementAt(i)["bono"].ToString().Trim() != "")
                {
                    cmd.Parameters["qtynotout"].Value = rows.ElementAt(i)["qty"].ToDecimal();
                    cmd.CommandText = "update borrd set qtynotout=qtynotout-@qtynotout where bono=@BorrNo and bomid=@boid";
                    cmd.ExecuteNonQuery();
                }

                //組件
                var Brows = dtBShopBom.AsEnumerable().Where(p => p["bomrec"].ToString().Trim() == rec);
                for (int j = 0; j < Brows.Count(); j++)
                {
                    cmd.Parameters["bomid"].Value = bomid;
                    cmd.Parameters["bomrec"].Value = rec;
                    cmd.Parameters["itno"].Value = Brows.ElementAt(j)["itno"].ToString().Trim();
                    cmd.Parameters["itname"].Value = Brows.ElementAt(j)["itname"].ToString();
                    cmd.Parameters["itunit"].Value = Brows.ElementAt(j)["itunit"].ToString().Trim();
                    cmd.Parameters["itqty"].Value = Brows.ElementAt(j)["itqty"].ToDecimal();
                    cmd.Parameters["itpareprs"].Value = Brows.ElementAt(j)["itpareprs"].ToDecimal();
                    cmd.Parameters["itpkgqty"].Value = Brows.ElementAt(j)["itpkgqty"].ToDecimal();
                    cmd.Parameters["itrec"].Value = (j + 1).ToString();
                    cmd.Parameters["itprice"].Value = Brows.ElementAt(j)["itprice"].ToDecimal();
                    cmd.Parameters["itprs"].Value = Brows.ElementAt(j)["itprs"].ToDecimal();
                    cmd.Parameters["itmny"].Value = Brows.ElementAt(j)["itmny"].ToDecimal();
                    cmd.Parameters["itnote"].Value = Brows.ElementAt(j)["itnote"].ToString().Trim();

                    cmd.CommandText = "INSERT INTO [dbo].[rborrbom]"
                    + "([bono],[bomid],[bomrec],[itno],[itname],[itunit],[itqty],[itpareprs],[itpkgqty],[itrec],[itprice],[itprs],[itmny],[itnote])"
                    + "VALUES(@bono,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote)";
                    cmd.ExecuteNonQuery();
                    bonoBom.ImportRow(Brows.ElementAt(j));
                    bonoBom.AcceptChanges();
                }
            }

            //主檔
            decimal[] mnys = SetRlendAllMny(ref bonoD);
            cmd.Parameters["TaxMnyb"].Value = (mnys[0] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M);
            cmd.Parameters["TaxMny"].Value = mnys[0].ToDecimal("f" + Common.MFT);
            cmd.Parameters["Tax"].Value = mnys[1].ToDecimal("f" + Common.TF);
            cmd.Parameters["TotMny"].Value = mnys[2].ToDecimal("f" + Common.MFT);
            cmd.Parameters["Taxb"].Value = (mnys[1] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["TotMnyb"].Value = (mnys[2] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["recordno"].Value = bonoD.Rows.Count;

            cmd.CommandText = "insert into rborr ("
            + " bono,bodate,bodate1,stno,stname,stnoo,stnameo"
            + " ,fano,faname1,faname2,fatel1,faper1,emno,emname,xa1no,xa1name,xa1par"
            + " ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,bomemo,bomemo1,appdate,edtdate,appscno,edtscno"
            + " ,recordno) values "
            + " (@bono,@bodate,@bodate1,@stno,@stname,@stnoo,@stnameo"
            + " ,@fano,@faname1,@faname2,@fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
            + " ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@bomemo,@bomemo1,@appdate,@edtdate,@appscno,@edtscno"
            + " ,@recordno) ";
            cmd.ExecuteNonQuery();

            Common.加庫存(cmd, bonoD, bonoBom, "stnoo");
            Common.扣庫存(cmd, bonoD, bonoBom, "stno");

            bonoD.Dispose();
            bonoBom.Dispose();

        }

        private void Text_Enter(object sender, EventArgs e)
        {
            //BsNo,FaNo,StNo,X3No,X3No1
            BsNo.Tag = BsNo.Text.Trim();
            FaNo.Tag = FaNo.Text.Trim();
            StNo.Tag = StNo.Text.Trim();

            if (sender.Equals(X3No) || sender.Equals(X3No1))
            {
                X3No.Tag = X3No1.Tag = (sender as TextBox).Text.Trim();
            }
        }



        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.Fact>(sender, reader => FillFact(reader));
        }
        void FillFact(SqlDataReader row)
        {
            FaNo.Text = row["FaNo"].ToString().Trim();
            FaName11.Text = row["FaName1"].ToString();
            FaPer11.Text = row["FaPer1"].ToString().GetUTF8(10);
            FaTel11.Text = row["FaTel1"].ToString();
            Xa1No.Text = row["FaXa1no"].ToString();
            X3No.Text = row["FaX3no"].ToString();
            X3No1.Text = row["FaX3no"].ToString();
            X4No.Text = row["FaX4no"].ToString();
            X5No.Text = row["FaX5no"].ToString();
            EmNo.Text = row["FaEmno1"].ToString();
            InvName.Text = row["FaName2"].ToString();
            InvTaxNo.Text = row["FaUno"].ToString();

            pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
            pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
            pVar.XX03Validate(X3No1.Text, X3No1, X3Name1, Rate1);
            pVar.XX04Validate(X4No.Text, X4No, X4Name);
            pVar.XX05Validate(X5No.Text, X5No, X5Name);
            pVar.EmplValidate(EmNo.Text, EmNo, EmName);
            FaPayAmt.Text = string.Format("{0:F" + FaPayAmt.LastNum + "}", row["FaPayAmt"]);
            getDeNo();

            for (int i = 0; i < dtBShopD.Rows.Count; i++)
            {
                GetSystemPrice(dtBShopD.Rows[i], i);
                SetRow_TaxPrice(dtBShopD.Rows[i]);
                SetRow_Mny(dtBShopD.Rows[i]);
                dataGridViewT1.InvalidateRow(i);
            }
            SetAllMny();

            FaNo.Tag = row["FaNo"].ToString().Trim();
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

            jBShop.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                if (FaNo.Text.Trim() == (FaNo.Tag ?? "").ToString())
                    return;

                if (dtBShopD.AsEnumerable().Any(r => r["BoNo"].ToString().Trim().Length > 0))
                {
                    dtBShopD.Clear();
                    dtBShopBom.Clear();
                }

                FillFact(row);
            });
        }


        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.Stkroom>(sender, reader => DoubleToGetStNo(reader));
        }
        void DoubleToGetStNo(SqlDataReader reader)
        {
            StNo.Text = reader["StNo"].ToString().Trim();
            StName.Text = reader["StName"].ToString().Trim();

            if (Common.Sys_StNoMode == 1)
            {
                for (int i = 0; i < dtBShopD.Rows.Count; i++)
                {
                    dtBShopD.Rows[i]["stno"] = StNo.Text;
                    dtBShopD.Rows[i]["StName"] = StName.Text;
                    dataGridViewT1.InvalidateRow(i);
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtBShopD, i, StNo.Text.Trim(), StName.Text.Trim());
                }
            }

            StNo.Tag = reader["StNo"].ToString().Trim();
        }
        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Clear();
                StName.Clear();
                e.Cancel = true;
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jBShop.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
            {
                if (StNo.Text.Trim() == (StNo.Tag ?? "").ToString())
                    return;
                DoubleToGetStNo(reader);
            });
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.XX03>(sender, reader =>
            {
                X3No.Text = X3No1.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = X3Name1.Text = reader["X3Name"].ToString();

                var rate = reader["X3rate"].ToDecimal() * 100;
                Rate.Text = Rate1.Text = rate.ToString("f0");

                //完成稅別設定，重新計算金額
                for (int i = 0; i < dtBShopD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtBShopD.Rows[i]);
                    SetRow_Mny(dtBShopD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                X3No.Tag = X3No.Text;
                X3No1.Tag = X3No1.Text;
            });
        }

        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            if (X3No.TrimTextLenth() == 0 || X3No1.TrimTextLenth() == 0)
            {
                e.Cancel = true;

                X3No.Clear();
                X3Name.Clear();

                X3No1.Clear();
                X3Name1.Clear();

                MessageBox.Show("稅別編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jBShop.ValidateOpen<JBS.JS.XX03>(sender, e, reader =>
            {
                var tb = sender as TextBox;
                if (reader["X3No"].ToString().Trim() == (tb.Tag ?? "").ToString())
                    return;

                X3No.Text = X3No1.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = X3Name1.Text = reader["X3Name"].ToString();

                var rate = reader["X3rate"].ToDecimal() * 100;
                Rate.Text = Rate1.Text = rate.ToString("f0");

                //完成稅別設定，重新計算金額
                for (int i = 0; i < dtBShopD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtBShopD.Rows[i]);
                    SetRow_Mny(dtBShopD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                X3No.Tag = X3No1.Tag = reader["X3No"].ToString().Trim();
            });
        }

        private void X5No_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.XX05>(sender, reader =>
            {
                X5No.Text = reader["X5No"].ToString().Trim();
                X5Name.Text = reader["X5Name"].ToString().Trim();
            });
        }

        private void X5No_Validating(object sender, CancelEventArgs e)
        {
            if (X5No.ReadOnly || btnCancel.Focused)
                return;

            if (X5No.TrimTextLenth() == 0)
            {
                X5No.Clear();
                X5Name.Clear();
                e.Cancel = true;
                MessageBox.Show("發票類別編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jBShop.ValidateOpen<JBS.JS.XX05>(sender, e, reader =>
            {
                X5No.Text = reader["X5No"].ToString().Trim();
                X5Name.Text = reader["X5Name"].ToString().Trim();
            });
        }

        private void X4No_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.XX04>(sender, reader =>
            {
                X4No.Text = reader["X4No"].ToString().Trim();
                X4Name.Text = reader["X4Name"].ToString().Trim();
            });
        }

        private void X4No_Validating(object sender, CancelEventArgs e)
        {
            if (X4No.ReadOnly || btnCancel.Focused)
                return;

            if (X4No.TrimTextLenth() == 0)
            {
                X4No.Clear();
                X4Name.Clear();
                return;
            }

            jBShop.ValidateOpen<JBS.JS.XX04>(sender, e, reader =>
            {
                X4No.Text = reader["X4No"].ToString().Trim();
                X4Name.Text = reader["X4Name"].ToString().Trim();
            }, true);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.Empl>(sender, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
                DeNo.Text = reader["emdeno"].ToString().Trim();

                jBShop.Validate<JBS.JS.Dept>(DeNo.Text, row => DeName.Text = row["dename1"].ToString(), () => DeName.Clear());
            });
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            if (EmNo.TrimTextLenth() == 0)
            {
                EmNo.Clear();
                EmName.Clear();
                DeNo.Clear();
                DeName.Clear();
                return;
            }

            jBShop.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
                DeNo.Text = reader["emdeno"].ToString().Trim();

                jBShop.Validate<JBS.JS.Dept>(DeNo.Text, row => DeName.Text = row["dename1"].ToString(), () => DeName.Clear());
            }, true);
        }

        private void DeNo_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.Dept>(sender, reader =>
            {
                DeNo.Text = reader["DeNo"].ToString().Trim();
                DeName.Text = reader["DeName1"].ToString().Trim();
            });
        }

        private void DeNo_Validating(object sender, CancelEventArgs e)
        {
            if (DeNo.ReadOnly || btnCancel.Focused)
                return;

            if (DeNo.TrimTextLenth() == 0)
            {
                DeNo.Clear();
                DeName.Clear();
                return;
            }

            jBShop.ValidateOpen<JBS.JS.Dept>(sender, e, reader =>
            {
                DeNo.Text = reader["DeNo"].ToString().Trim();
                DeName.Text = reader["DeName1"].ToString().Trim();
            }, true);
        }

        private void SeNo_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.Send>(sender, reader =>
            {
                SeNo.Text = reader["SeNo"].ToString().Trim();
                SeName.Text = reader["SeName"].ToString().Trim();
            });
        }

        private void SeNo_Validating(object sender, CancelEventArgs e)
        {
            if (SeNo.ReadOnly || btnCancel.Focused)
                return;

            if (SeNo.TrimTextLenth() == 0)
            {
                SeNo.Clear();
                SeName.Clear();
                return;
            }

            jBShop.ValidateOpen<JBS.JS.Send>(sender, e, reader =>
            {
                SeNo.Text = reader["SeNo"].ToString().Trim();
                SeName.Text = reader["SeName"].ToString().Trim();
            }, true);
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.Spec>(sender, reader =>
            {
                SpNo.Text = reader["SpNo"].ToString().Trim();
                SpName.Text = reader["SpName"].ToString().Trim();
            });
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

            jBShop.ValidateOpen<JBS.JS.Spec>(sender, e, reader =>
            {
                SpNo.Text = reader["SpNo"].ToString().Trim();
                SpName.Text = reader["SpName"].ToString().Trim();
            }, true);
        }

        private void BsNo_DoubleClick(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.BShop>(sender);
        }

        private void BsNo_Validating(object sender, CancelEventArgs e)
        {
            if (BsNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (BsNo.TrimTextLenth() == 0 && BsNo.TextLength > 0)
            {
                e.Cancel = true;
                BsNo.Text = "";
                BsNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jBShop.IsExistDocument<JBS.JS.BShop>(BsNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jBShop.IsExistDocument<JBS.JS.BShop>(BsNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jBShop.IsExistDocument<JBS.JS.BShop>(BsNo.Text.Trim()))
                {
                    if (BsNo.Text.Trim() == (BsNo.Tag ?? "").ToString())
                        return;

                    writeToTxt(BsNo.Text.Trim());
                    loadBShopBom();
                }
                else
                {
                    e.Cancel = true;
                    jBShop.Open<JBS.JS.BShop>(sender);
                    BsNo.SelectAll();

                    if (jBShop.IsExistDocument<JBS.JS.BShop>(BsNo.Text.Trim()) == true)
                    {
                        writeToTxt(BsNo.Text.Trim());
                        loadBShopBom();
                    }
                }
            }
        }

        private void BsDate_Validating(object sender, CancelEventArgs e)
        {
            if (BsDate.ReadOnly) return;
            if (btnCancel.Focused) return;
            TextBox tb = sender as TextBox;

            if (tb.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("日期不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!tb.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tb.Name == BsDate.Name)
            {
                if (string.CompareOrdinal(BsDate.Text, BsDateAc.Text) > 0)
                {
                    BsDateAc.Text = BsDate.Text;
                }
                else
                {
                    if (BsDateAc.Text.Trim() == "")
                        BsDateAc.Text = BsDate.Text;
                }
            }
            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                //過去的日子，以今天為準，未來的日子，以未來的進貨日為準
                string now;
                if (Common.User_DateTime == 1)
                    now = Date.GetDateTime(1, false);
                else
                    now = Date.GetDateTime(2, false);

                if (string.CompareOrdinal(now, BsDate.Text) > 0)
                {
                    InvDate = now;
                }
            }
        }

        private void Xa1Par_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (Xa1Par.ReadOnly) return;

            if (Xa1Par.ReadOnly != true && Xa1Par.Text.Trim() != "" && dataGridViewT1.Rows.Count > 0)
            {
                if (Xa1Par.Text.ToDecimal() == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("匯率不可為零");
                    return;
                }
                //離開匯率設定，重新計算本幣金額
                for (int i = 0; i < dtBShopD.Rows.Count; i++)
                {
                    SetRow_Mny(dtBShopD.Rows[i]);
                    FeeShare();
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }

            if (Common.keyData != Keys.Up)
            {
                if (FaNo.Text.Trim() != "")
                    if (dataGridViewT1.Rows.Count == 0)
                        if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
        }

        private void Discount_Validating(object sender, CancelEventArgs e)
        {
            if (Discount.ReadOnly) return;

            Discount.Text = ((TextBox)sender).Text;
            Discount1.Text = ((TextBox)sender).Text;
            SetAcctMny();
        }

        private void GetPrvAcc_Validating(object sender, CancelEventArgs e)
        {
            if (GetPrvAcc.ReadOnly) return;
            if (btnCancel.Focused) return;

            decimal d = 0, d1 = 0;
            decimal.TryParse(FaPayAmt.Text, out d);
            decimal.TryParse((sender as TextBox).Text, out d1);

            //if (d1 == 0)
            //    return;

            if (d1 > 0 && d1 > d)
            {
                e.Cancel = true;
                d1 = 0;
                (sender as TextBox).Text = d1.ToString("F" + GetPrvAcc.LastNum);
                MessageBox.Show("取用金額超出預付餘額額度", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((sender as TextBox).Name == GetPrvAcc.Name)
                GetPrvAcc1.Text = GetPrvAcc.Text;
            else
                GetPrvAcc.Text = GetPrvAcc1.Text;
            SetAcctMny();
        }

        private void CashMny_Validating(object sender, CancelEventArgs e)
        {
            if (CashMny.ReadOnly) return;
            SetAcctMny();
        }

        private void Tax_Validating(object sender, CancelEventArgs e)
        {
            //營業稅額離開，可能手動調整營業稅
            //計算方法：『稅前合計』+『營業稅額』
            if (Tax.ReadOnly) return;
            if ((sender as TextBox).Name == Tax.Name)
                Tax1.Text = Tax.Text;
            else
                Tax.Text = Tax1.Text;

            decimal taxmny = TaxMny.Text.ToDecimal();
            decimal tax = Tax.Text.ToDecimal();
            decimal totmny = TotMny.Text.ToDecimal();

            if (Common.Sys_X3Forward == 1 && X3No.Text.Trim() == "2")
            {
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MFT);
                TaxMny1.Text = TaxMny.Text;
            }
            else
            {
                TotMny.Text = (taxmny + tax).ToString("f" + Common.MFT);
                TotMny1.Text = TotMny.Text;
            }

            SetAcctMny();
        }

        private void InvNo_Validating(object sender, CancelEventArgs e)
        {
            if (InvNo.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (InvNo.TextLength == 0) return;

            if (InvNo.TextLength != 10)
            {
                e.Cancel = true;
                if (InvNo.Text.Trim() == "")
                    InvNo.Text = "";
                MessageBox.Show("發票號碼輸入錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string str = InvNo.Text.Trim().ToUpper();
                for (int i = 0; i < 10; i++)
                {
                    if (i < 2)
                    {
                        if (!char.IsUpper(str[i]))
                        {
                            e.Cancel = true;
                            MessageBox.Show("發票號碼輸入錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                    else
                    {
                        if (!char.IsDigit(str[i]))
                        {
                            e.Cancel = true;
                            MessageBox.Show("發票號碼輸入錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                }
                InvNo.Text = str;
            }
            EInvState.Text = "已上傳";
        }

        int getRadio(PanelNT pnl)
        {
            int n = 1;
            if (pnl.Name == pnlBsShare.Name)
            {
                n = BsShare1.Checked ? 1 : 2;
            }
            if (pnl.Name == pnlBsShsel.Name)
            {
                n = BsShsel1.Checked ? 1 : 2;
            }
            return n;
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() != "")
                if (dataGridViewT1.Rows.Count == 0)
                    if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        void GridSaleDAddRows()
        {
            DataRow dRow = dtBShopD.NewRow();
            dRow["itno"] = "";
            dRow["ItNoUdf"] = "";
            dRow["itname"] = "";
            dRow["Qty"] = 0;
            dRow["PQty"] = 0;
            dRow["itunit"] = "";
            dRow["Punit"] = "";
            dRow["Price"] = 0;
            dRow["TaxPrice"] = 0;
            dRow["Mny"] = 0;
            dRow["ItPkgQty"] = 1;
            dRow["ItTrait"] = 0;
            dRow["產品組成"] = "";
            dRow["Memo"] = "";
            dRow["PriceB"] = 0;
            dRow["TaxPriceB"] = 0;
            dRow["MnyB"] = 0;
            //折數
            dRow["Prs"] = "1.00";
            //結構編號
            dRow["BomRec"] = GetBomRec();
            //倉庫帶預設倉
            dRow["StNo"] = StNo.Text;
            dRow["StName"] = StName.Text;
            dRow["bono"] = "";
            dRow["boid"] = "";
            dRow["cyno"] = "";

            dtBShopD.Rows.Add(dRow);
            dtBShopD.AcceptChanges();
        }

        void DeleteEmptyRow(int index)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                //刪除明細前，先刪除明細的『組件明細』
                var rec = dataGridViewT1.Rows[index].Cells["結構編號"].EditedFormattedValue.ToString().Trim();
                jBShop.RemoveBom(rec, ref dtBShopBom);

                //刪除明細
                dataGridViewT1.Rows.Remove(dataGridViewT1.Rows[index]);
                dtBShopD.AcceptChanges();
                dataGridViewT1.Focus();
                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentRow.Selected = true;
                    for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                    {
                        dataGridViewT1["序號", i].Value = (i + 1).ToString();
                    }
                }
                //刪除列後，重新計算帳款
                SetAllMny();
            }
        }

        void GridSaleDInsertRows(int index)
        {
            DataRow dRow = dtBShopD.NewRow();
            dRow["itno"] = "";
            dRow["ItNoUdf"] = "";
            dRow["itname"] = "";
            dRow["Qty"] = 0;
            dRow["PQty"] = 0;
            dRow["itunit"] = "";
            dRow["Punit"] = "";
            dRow["Price"] = 0;
            dRow["TaxPrice"] = 0;
            dRow["Mny"] = 0;
            dRow["ItPkgQty"] = 1;
            dRow["ItTrait"] = 0;
            dRow["產品組成"] = "";
            dRow["Memo"] = "";
            dRow["PriceB"] = 0;
            dRow["TaxPriceB"] = 0;
            dRow["MnyB"] = 0;
            //折數
            dRow["Prs"] = "1.00";
            //結構編號
            dRow["BomRec"] = GetBomRec();
            //倉庫帶預設倉
            dRow["StNo"] = StNo.Text;
            dRow["StName"] = StName.Text;
            dRow["bono"] = "";
            dRow["boid"] = "";
            dRow["cyno"] = "";

            dtBShopD.Rows.InsertAt(dRow, index);
            dtBShopD.AcceptChanges();
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "稅前金額")
            {
                TextBefore = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "採購憑證")
            {
                TextBefore = dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "進貨數量")
            {
                TextBefore = dataGridViewT1["進貨數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "計價數量")
            {
                TextBefore = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "進價")
            {
                TextBefore = dataGridViewT1["進價", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "折數")
            {
                TextBefore = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToString().Trim();
            } 
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "包裝數量")
            {
                TextBefore = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jBShop.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["ItNo"].ToString().Trim();

            this.ItNoBegin = itno;
            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;
            dtBShopD.Rows[index]["itno"] = itno;
            dtBShopD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"];
            dtBShopD.Rows[index]["itname"] = reader["ItName"];
            dtBShopD.Rows[index]["PUnit"] = reader["PUnit"];
            dtBShopD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            //帶入產品常用倉庫
            if (reader["stno"].ToString().Trim().Length > 0)
            {
                dtBShopD.Rows[index]["stno"] = reader["stno"].ToString();
                dtBShopD.Rows[index]["stname"] = SQL.ExecuteScalar("select TOP 1 stname from stkroom where stno = @stno", new parameters("stno", reader["stno"].ToString()));
            }
            //進貨常用單位
            var utype = reader["ItBuyUnit"].ToString().Trim();
            var unit = "";

            //預設帶包裝單位或是單位
            if (utype == "1")
            {
                unit = reader["ItUnitp"].ToString();
                dtBShopD.Rows[index]["ItUnit"] = unit;

                var itpkgqty = reader["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                dtBShopD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                unit = reader["ItUnit"].ToString();
                dtBShopD.Rows[index]["ItUnit"] = unit;
                dtBShopD.Rows[index]["itpkgqty"] = 1;
            }

            GetSystemPrice(dtBShopD.Rows[index], index);
            SetRow_TaxPrice(dtBShopD.Rows[index]);
            SetRow_Mny(dtBShopD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            //組合組裝品
            string trait = reader["ItTrait"].ToString().Trim();
            if (trait.Length == 0)
                trait = "3";
            dtBShopD.Rows[index]["ItTrait"] = trait;

            if (trait == "1") trait = "組合品";
            else if (trait == "2") trait = "組裝品";
            else if (trait == "3") trait = "單一商品";
            else trait = "";
            dtBShopD.Rows[index]["產品組成"] = trait;

            //規格說明
            for (int x = 1; x <= 10; x++)
            {
                dtBShopD.Rows[index]["ItDesp" + x] = reader["ItDesp" + x];
            }

            //BOM
            var rec = dtBShopD.Rows[index]["BomRec"].ToString().Trim();
            jBShop.RemoveBom(rec, ref dtBShopBom);
            jBShop.GetItemBom(itno, rec, ref dtBShopBom);

            //刪除批次異動資訊
            BatchF.刪除批次異動(dt_BatchProcess, rec);
        }
        void FillItemByFord(string FoNo, int DGVFocusRow, DataRow MasterRow, DataTable dtDetail)
        {
            //刪除批次異動資訊
            var Bomrec = dataGridViewT1.CurrentRow.Cells["結構編號"].EditedFormattedValue.ToString().Trim();
            BatchF.刪除批次異動(dt_BatchProcess, Bomrec);

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = FoNo.Trim();
            Memo1 = MasterRow["fomemo1"].ToString();
            Xa1No.Text = MasterRow["Xa1no"].ToString();
            Xa1Name.Text = MasterRow["Xa1Name"].ToString();
            Xa1Par.Text = MasterRow["Xa1Par"].ToString();
            X3No.Text = MasterRow["X3no"].ToString();
            X3No1.Text = MasterRow["X3no"].ToString();
            Rate.Text = (MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            Rate1.Text = (MasterRow["Rate"].ToDecimal() * 100).ToString("f0");

            BsMemo.Text = MasterRow["fomemo"].ToString();
            EmNo.Text = MasterRow["EmNo"].ToString();
            EmName.Text = MasterRow["EmName"].ToString();
            getDeNo();
            SpNo.Text = MasterRow["SpNo"].ToString();
            SpName.Text = MasterRow["SpName"].ToString();
            pVar.XX03Validate(X3No.Text, X3No, X3Name);
            pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);
            PhotoPath = MasterRow["PhotoPath"].ToString();//13.7c
            jBShop.RemoveBom(dtBShopD.Rows[DGVFocusRow]["bomrec"].ToString().Trim(), ref dtBShopBom);
            dtBShopD.Rows[DGVFocusRow].Delete();
            dtBShopD.AcceptChanges();

            //明細部分&組件
            DataRow row1, row2;

            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                row1 = dtBShopD.NewRow();
                row2 = dtDetail.Rows[i];
                if (row2["ittrait"].ToString() == "2")
                {
                    row1["ittrait"] = row2["ittrait"].ToString();
                    row1["產品組成"] = "組裝品";
                }
                else if (row2["ittrait"].ToString() == "3")
                {
                    row1["ittrait"] = row2["ittrait"].ToString();
                    row1["產品組成"] = "單一商品";
                }
                else
                {
                    row1["ittrait"] = row2["ittrait"].ToString();
                    row1["產品組成"] = "組合品";
                }
                row1["fono"] = FoNo;
                row1["foid"] = row2["bomid"].ToString();
                row1["itno"] = row2["itno"].ToString();
                row1["ItNoUdf"] = row2["ItNoUdf"].ToString();
                row1["itname"] = row2["itname"].ToString();
                row1["itunit"] = row2["itunit"].ToString();
                row1["Punit"] = row2["Punit"].ToString();
                row1["qty"] = row2["qtynotin"].ToDecimal("f" + Common.Q);
                if (Common.Sys_DBqty == 1)
                    row1["pqty"] = row2["qtynotin"].ToDecimal("f" + Common.Q);
                else
                    row1["pqty"] = row2["pqty"].ToDecimal("f" + Common.Q);
                row1["price"] = row2["price"].ToDecimal("f" + Common.MF);
                row1["prs"] = row2["prs"].ToDecimal("f3");
                row1["taxprice"] = row2["taxprice"].ToDecimal("f6");
                row1["memo"] = row2["memo"].ToString();
                row1["priceb"] = row2["priceb"].ToDecimal("f" + Common.M);
                row1["taxpriceb"] = row2["taxpriceb"].ToDecimal("f6");
                row1["itpkgqty"] = row2["itpkgqty"].ToDecimal("f" + Common.Q);
                row1["StNo"] = StNo.Text;
                row1["StName"] = StName.Text;
                row1["mwidth1"] = row2["mwidth1"].ToDecimal();
                row1["mwidth2"] = row2["mwidth2"].ToDecimal();
                row1["mwidth3"] = row2["mwidth3"].ToDecimal();
                row1["mwidth4"] = row2["mwidth4"].ToDecimal();
                row1["Pformula"] = row2["Pformula"].ToString();
                for (int j = 1; j <= 10; j++)
                {
                    row1["itdesp" + j] = row2["itdesp" + j].ToString();
                }
                row1["BomRec"] = GetBomRec();
                SetRow_Mny(row1);
                dtBShopD.Rows.InsertAt(row1, dtBShopD.Rows.Count);
                dtBShopD.AcceptChanges();


                //組件部分
                if (row2["ittrait"].ToString().Trim() == "3")
                    continue;

                var bomid = row2["bomid"].ToString().Trim();
                var rec = row1["BomRec"].ToString().Trim();

                jBShop.GetTBom<JBS.JS.Ford>(bomid, rec, ref dtBShopBom);
                dataGridViewT1.InvalidateRow(i);
            }

            dataGridViewT1.CurrentCell = dataGridViewT1["採購憑證", DGVFocusRow];
            dataGridViewT1.CurrentRow.Selected = true;
            SetAllMny();
        }
        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BsNo.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改 
                    if (dtBShopD.Rows[e.RowIndex]["bono"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借入轉單, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jBShop.DataGridViewOpen<JBS.JS.Item>(sender, e, dtBShopD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (CurrentColumnName == "進貨倉庫")
            {               
                #region 進貨倉庫
                if (dataGridViewT1.Columns["進貨倉庫"].ReadOnly)
                    return;

                jBShop.DataGridViewOpen<JBS.JS.Stkroom>(sender, e, dtBShopD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["StNo"].ToString();

                    dtBShopD.Rows[e.RowIndex]["stno"] = row["StNo"].ToString();
                    dtBShopD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtBShopD, e.RowIndex, row["StNo"].ToString(), row["StName"].ToString());
                });
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region  單位
                if (Common.Sys_LendToSaleMode == 2)
                {
                    if (dtBShopD.Rows[e.RowIndex]["bono"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借入轉單, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dataGridViewT1["採購憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridViewT1["借入憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由借入轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itno = dtBShopD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jBShop.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        dtBShopD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            dtBShopD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            dtBShopD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;
                dtBShopD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)//1代表一般進銷存
                {
                    GetSystemPrice(dtBShopD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtBShopD.Rows[e.RowIndex]);
                    SetRow_Mny(dtBShopD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
                #endregion
            }
            else if (CurrentColumnName == "備註說明")
            {
                #region 備註說明
                using (FrmSale_Memo frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        dtBShopD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
            else if (CurrentColumnName == "採購憑證")
            {
                #region 採購憑證
                if (Common.Sys_LendToSaleMode == 2)
                {
                    if (dtBShopD.Rows[e.RowIndex]["bono"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借入轉單, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                using (var frm = new FrmFordToShop())
                {
                    frm.TSeekNo = dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    frm.FaNo = FaNo.Text.Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.MasterRow == null || frm.dtDetail.Rows.Count == 0) return;

                        FillItemByFord(frm.FoNo, e.RowIndex, frm.MasterRow, frm.dtDetail);
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "進貨數量")
            {
                #region 進貨數量
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改 
                    if (dtBShopD.Rows[e.RowIndex]["bono"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借入轉單, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (Common.Sys_DBqty == 1)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = dtBShopD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtBShopD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtBShopD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtBShopD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtBShopD.Rows[e.RowIndex]["Pformula"].ToString();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtBShopD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtBShopD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtBShopD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtBShopD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtBShopD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtBShopD.Rows[e.RowIndex]["qty"] = frm.resultCount.ToString("f" + Common.Q);
                            dtBShopD.Rows[e.RowIndex]["pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtBShopD.Rows[e.RowIndex]);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            SetAllMny();
                        }
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "計價數量")
            {
                #region 計價數量
                if (Common.Sys_DBqty == 2)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = dtBShopD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtBShopD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtBShopD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtBShopD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtBShopD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = dtBShopD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "進貨數量";


                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtBShopD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtBShopD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtBShopD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtBShopD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtBShopD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;
                            dtBShopD.Rows[e.RowIndex]["qty"] = frm.qty;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtBShopD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtBShopD.Rows[e.RowIndex]);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            SetAllMny();
                        }
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "計位")
            {
                #region 計位
                if (Common.Sys_DBqty == 2)
                {
                    using (var frm = new FrmUnit())
                    {
                        frm.Kid = 1;
                        if (DialogResult.OK == frm.ShowDialog())
                            dtBShopD.Rows[e.RowIndex]["punit"] = frm.Result;
                    }
                }
                #endregion
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "採購憑證")
            {
                #region 採購憑證
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改
                    var orrrno = dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    if (dtBShopD.Rows[e.RowIndex]["bono"].ToString().Trim().Length > 0 && orrrno != TextBefore.ToString().Trim())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借入轉單, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtBShopD.Rows[e.RowIndex]["fono"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                if (dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() == TextBefore) return;
                if (dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() == "")
                {
                    dtBShopD.Rows[e.RowIndex]["foid"] = "";
                    dtBShopD.Rows[e.RowIndex]["fono"] = "";
                    return;
                }
                using (var frm = new FrmFordToShop())
                {
                    frm.TSeekNo = dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    frm.FaNo = FaNo.Text.Trim();
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (frm.MasterRow == null || frm.dtDetail.Rows.Count == 0)
                            {
                                e.Cancel = true;
                                return;
                            }

                            FillItemByFord(frm.FoNo, e.RowIndex, frm.MasterRow, frm.dtDetail);
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                var itnoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString();

                //非一般模式借轉銷,產品不能改 
                if (Common.Sys_LendToSaleMode == 2)
                {
                    if (dtBShopD.Rows[e.RowIndex]["bono"].ToString().Trim().Length > 0 && itnoNow != TextBefore.ToString().Trim())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借入轉單, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtBShopD.Rows[e.RowIndex]["itno"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                //空值->清空
                if (itnoNow == "" || itnoNow.Trim().Length == 0)
                {
                    if (btnSave.Focused)
                        return;

                    dtBShopD.Rows[e.RowIndex]["fono"] = "";
                    dtBShopD.Rows[e.RowIndex]["foid"] = "";
                    dtBShopD.Rows[e.RowIndex]["itno"] = "";
                    dtBShopD.Rows[e.RowIndex]["ItNoUdf"] = "";
                    dtBShopD.Rows[e.RowIndex]["itname"] = "";
                    dtBShopD.Rows[e.RowIndex]["itunit"] = "";
                    dtBShopD.Rows[e.RowIndex]["Punit"] = "";
                    dtBShopD.Rows[e.RowIndex]["Pqty"] = 0;
                    dtBShopD.Rows[e.RowIndex]["qty"] = 0;
                    dtBShopD.Rows[e.RowIndex]["Price"] = 0;
                    dtBShopD.Rows[e.RowIndex]["TaxPrice"] = 0;
                    dtBShopD.Rows[e.RowIndex]["Mny"] = 0;
                    dtBShopD.Rows[e.RowIndex]["ItPkgQty"] = 1;
                    dtBShopD.Rows[e.RowIndex]["ItTrait"] = 0;
                    dtBShopD.Rows[e.RowIndex]["產品組成"] = "";
                    dtBShopD.Rows[e.RowIndex]["Memo"] = "";
                    dtBShopD.Rows[e.RowIndex]["PriceB"] = 0;
                    dtBShopD.Rows[e.RowIndex]["TaxPriceB"] = 0;
                    dtBShopD.Rows[e.RowIndex]["MnyB"] = 0;
                    dtBShopD.Rows[e.RowIndex]["StNo"] = StNo.Text;
                    dtBShopD.Rows[e.RowIndex]["StName"] = StName.Text;
                    dtBShopD.Rows[e.RowIndex]["mwidth1"] = 0;
                    dtBShopD.Rows[e.RowIndex]["mwidth2"] = 0;
                    dtBShopD.Rows[e.RowIndex]["mwidth3"] = 0;
                    dtBShopD.Rows[e.RowIndex]["mwidth4"] = 0;
                    dtBShopD.Rows[e.RowIndex]["Pformula"] = "";
                    dtBShopD.Rows[e.RowIndex]["bono"] = "";
                    dtBShopD.Rows[e.RowIndex]["boid"] = "";
                    dtBShopD.Rows[e.RowIndex]["cyno"] = "";
                    dtBShopD.Rows[e.RowIndex]["Prs"] = "1.00";

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = dtBShopD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jBShop.RemoveBom(rec, ref dtBShopBom);

                    //刪除批次異動資訊
                    BatchF.刪除批次異動(dt_BatchProcess, rec);
                    return;
                }

                //值沒變->離開
                if (itnoNow == ItNoBegin)
                    return;

                if (dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    e.Cancel = true;
                    MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    dtBShopD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                //值有變，但是跟自定編號一樣，視同沒變->離開                //把『自定編號』改成『產品編號』
                if (itnoNow != ItNoBegin)
                {
                    if (itnoNow == UdfNoBegin)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = ItNoBegin;

                        dtBShopD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                //值變了, 跟產品編號和自定編號都不一樣,帶值出來, 若找不到這筆資料則開窗
                if (itnoNow != ItNoBegin && itnoNow != UdfNoBegin)
                {
                    jBShop.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtBShopD, row => FillItem(row, e.RowIndex));
                }
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                string itno = dtBShopD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                if (dataGridViewT1["採購憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    if (dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim() != TextBefore)
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtBShopD.Rows[e.RowIndex]["itunit"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    return;
                }
                if (dataGridViewT1["借入憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    if (dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim() != TextBefore)
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借入轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtBShopD.Rows[e.RowIndex]["itunit"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    return;
                }

                jBShop.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        dtBShopD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        dtBShopD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                dtBShopD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    GetSystemPrice(dtBShopD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtBShopD.Rows[e.RowIndex]);
                    SetRow_Mny(dtBShopD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
                #endregion
            }
            else if (CurrentColumnName == "進貨數量")
            {
                #region 進貨數量
                var qty = dataGridViewT1["進貨數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                if (qty == TextBefore.ToDecimal())
                    return;

                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,數量不能改
                    if (dtBShopD.Rows[e.RowIndex]["bono"].ToString().Trim().Length > 0 && qty != TextBefore.ToDecimal())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借入轉單, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtBShopD.Rows[e.RowIndex]["Qty"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                if (Common.Sys_DBqty == 1)
                {
                    dtBShopD.Rows[e.RowIndex]["Qty"] = qty;
                    dtBShopD.Rows[e.RowIndex]["PQty"] = qty;
                }
                else if (Common.Sys_DBqty == 2)
                {
                    dtBShopD.Rows[e.RowIndex]["Qty"] = qty;
                }

                SetRow_Mny(dtBShopD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "計價數量")
            {
                #region 計價數量
                var pqty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (pqty == TextBefore.ToDecimal())
                    return;

                dtBShopD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                SetRow_Mny(dtBShopD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "進價")
            {
                #region 進價
                var price = dataGridViewT1["進價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MF);
                if (price == TextBefore.ToDecimal())
                    return;

                dtBShopD.Rows[e.RowIndex]["Price"] = dataGridViewT1["進價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MF);

                SetRow_TaxPrice(dtBShopD.Rows[e.RowIndex]);
                SetRow_Mny(dtBShopD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "折數")
            {
                #region 折數
                var prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");
                if (prs == TextBefore.ToDecimal())
                    return;

                if (dataGridViewT1.Columns["折數"].ReadOnly) 
                    return;

                dtBShopD.Rows[e.RowIndex]["Prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");

                SetRow_TaxPrice(dtBShopD.Rows[e.RowIndex]);
                SetRow_Mny(dtBShopD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "稅前金額")
            {
                #region 稅前金額
                //正常情形『稅前金額』是由『進價』帶出來的
                //下面處理的情形是手動打上『稅前金額』
                //所以須往前推算『進價』金額。
                decimal price = 0;
                decimal qty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前進價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPF);
                decimal prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");
                qty = (qty == 0) ? 1 : qty;

                if (mny == TextBefore.ToDecimal())
                    return;

                dtBShopD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MF);
                        dtBShopD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MF);
                        dtBShopD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(dtBShopD.Rows[e.RowIndex]);

                taxprice = dtBShopD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                dtBShopD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                dtBShopD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                dtBShopD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "進貨倉庫")
            {
                #region 進貨倉庫
                try
                {
                    jBShop.DataGridViewValidateOpen<JBS.JS.Stkroom>(sender, e, dtBShopD, reader =>
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = reader["stno"].ToString().Trim();

                        dtBShopD.Rows[e.RowIndex]["StNo"] = reader["StNo"].ToString();
                        dtBShopD.Rows[e.RowIndex]["StName"] = reader["StName"].ToString();
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        BatchF.同步批次異動倉庫(dt_BatchProcess, dtBShopD, e.RowIndex, reader["StNo"].ToString(), reader["StName"].ToString());
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
                #endregion
            }
            else if (CurrentColumnName == "包裝數量")
            {
                #region 包裝數量
                var pkgqty = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                if (pkgqty == TextBefore.ToDecimal())
                    return;

                if (dataGridViewT1["採購憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    decimal itpkgqty = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                    if (itpkgqty != TextBefore.ToDecimal())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtBShopD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                else if (dataGridViewT1["借入憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    decimal itpkgqty = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                    if (itpkgqty != TextBefore.ToDecimal())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借入轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtBShopD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                else
                {
                    dtBShopD.Rows[e.RowIndex]["itpkgqty"] = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
             #endregion
            }
        }


        private void FaNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            if (FaNo.ReadOnly)
            {
                AllGridButtons.ForEach(b => b.Enabled = false);
            }
            else
            {
                AllGridButtons.ForEach(b => b.Enabled = true);
            }

            //設定唯讀欄位, 有些欄位須要讀系統參數設定
            dataGridViewT1.ReadOnly = FaNo.ReadOnly;
            dataGridViewT1.Columns["序號"].ReadOnly = true;
            dataGridViewT1.Columns["稅前進價"].ReadOnly = true;
            dataGridViewT1.Columns["產品組成"].ReadOnly = true;
            dataGridViewT1.Columns["倉庫名稱"].ReadOnly = true;
            dataGridViewT1.Columns["本幣單價"].ReadOnly = true;
            dataGridViewT1.Columns["本幣稅前單價"].ReadOnly = true;
            dataGridViewT1.Columns["本幣稅前金額"].ReadOnly = true;
            dataGridViewT1.Columns["借入憑證"].ReadOnly = true;
            //折數設定
            if (Common.Sys_KeyPrs == 1)
                dataGridViewT1.Columns["折數"].ReadOnly = false;
            else
                dataGridViewT1.Columns["折數"].ReadOnly = true;

            //74.73版單倉
            if (Common.Series == "74")
            {
                dataGridViewT1.Columns["進貨倉庫"].ReadOnly = true;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = true;
            }
            else if (Common.Series == "73")
            {
                dataGridViewT1.Columns["進貨倉庫"].ReadOnly = true;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = false;
            }
            else if (Common.Series == "72")
            {
                dataGridViewT1.Columns["進貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = true;
            }
            else if (Common.Series == "71")
            {
                dataGridViewT1.Columns["進貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = false;
            }

            EInvChange.Enabled = !FaNo.ReadOnly;
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() == "")
            {
                FaNo.Focus();
                MessageBox.Show("廠商編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridSaleDAddRows();
                string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "採購憑證";
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells[str];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            gridDelete.Focus();
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷
                    var idx = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (idx == -1)
                    {
                        dataGridViewT1.Focus();
                        return;
                    }

                    if (dtBShopD.Rows[idx]["bono"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借入轉單, 無法刪除!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //刪除明細前，先刪除明細的『組件明細』
                var rec = dataGridViewT1.CurrentRow.Cells["結構編號"].EditedFormattedValue.ToString().Trim();
                jBShop.RemoveBom(rec, ref dtBShopBom);

                //刪除明細
                int index = dataGridViewT1.SelectedRows[0].Index;
                dtBShopD.Rows.RemoveAt(index);
                dtBShopD.AcceptChanges();

                //刪除批次異動資訊
                BatchF.刪除批次異動(dt_BatchProcess, rec);

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                }
                SetAllMny();//刪除列後，重新計算帳款

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "採購憑證";
                    dataGridViewT1.CurrentCell = dataGridViewT1[str, index];
                    dataGridViewT1.Rows[index].Selected = true;
                }
            }
            dataGridViewT1.Focus();
        }

        private void gridPicture_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows.Count > 0)
                {
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
                    dataGridViewT1.Focus();
                }
            }
        }

        private void gridInsert_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridInsert.Focus();
                if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    GridSaleDInsertRows(index);
                    string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "採購憑證";
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells[str];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            gridItDesp.Focus();
            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(true, FormStyle.Mini))
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }
                frm.dr = dtBShopD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridBomD.Focus();
                string _trait = dataGridViewT1["產品組成", dataGridViewT1.CurrentRow.Index].Value.ToString();
                if (_trait != "組合品" && _trait != "組裝品")
                {
                    MessageBox.Show("只有組合品或組裝品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.Focus();
                    return;
                }

                using (FrmSale_Bom frm = new FrmSale_Bom())
                {
                    string rec = dataGridViewT1.SelectedRows[0].Cells["結構編號"].Value.ToString();
                    DataTable table = dtBShopBom.Clone();

                    for (int i = 0; i < dtBShopBom.Rows.Count; i++)
                    {
                        if (dtBShopBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                        {
                            table.ImportRow(dtBShopBom.Rows[i]);
                            dtBShopBom.Rows.RemoveAt(i--);
                        }
                    }

                    table.AcceptChanges();
                    dtBShopBom.AcceptChanges();
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    frm.dtD = table.Copy();
                    frm.BomRec = rec;
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.grid = dataGridViewT1;

                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (frm.CallBack == "Money")
                            {
                                dtBShopBom.Merge(frm.dtD);
                                dtBShopD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MF);
                                dataGridViewT1.Focus();
                                SetRow_TaxPrice(dtBShopD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetRow_Mny(dtBShopD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetAllMny();
                                break;
                            }
                            else
                            {
                                dtBShopBom.Merge(frm.dtD);
                                dtBShopBom.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            dtBShopBom.Merge(table);
                            dtBShopBom.AcceptChanges();
                            dataGridViewT1.Focus();
                            break;
                    }
                }
            }
        }

        private void gridStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridStock.Focus();
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
                dataGridViewT1.Focus();
            }
        }

        private void gridTran_Click(object sender, EventArgs e)
        {
            gridTran.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtBShopD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該廠商此產品交易 frm = new S2.Frm該廠商此產品交易())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridFactBShop_Click(object sender, EventArgs e)
        {
            gridFactBShop.Focus();
            if (FaNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("廠商編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var itno = (index == -1) ? "" : dtBShopD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該廠商歷史交易 frm = new S2.Frm該廠商歷史交易())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridAllTrans_Click(object sender, EventArgs e)
        {
            gridAllTrans.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtBShopD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有廠商此產品交易 frm = new S2.Frm所有廠商此產品交易())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridSaleHistory_Click(object sender, EventArgs e)
        {
            gridSaleHistory.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtBShopD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm售價查詢 frm = new S2.Frm售價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridAddress_Click(object sender, EventArgs e)
        {
            jBShop.Open<JBS.JS.Fact>(FaNo, row =>
            {
                BsMemo.Text = row["FaNo"].ToString() + " ";
                BsMemo.Text += row["FaName1"].ToString() + " ";
                BsMemo.Text += row["FaAddr3"].ToString();
            });
        }

        private void gridKeyMan_Click_1(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
                return;

            using (FrmSale_AppScNo frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jBShop.keyMan.AppendMan;
                frm.ATime = jBShop.keyMan.AppendTime;
                //修改人員
                frm.EName = jBShop.keyMan.EditMan;
                frm.ETime = jBShop.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void gridInvNo_Click(object sender, EventArgs e)
        {
            var bsno = BsNo.Text.Trim();
            var IsPOS = false;　
            using (FrmBShop_Inv frm = new FrmBShop_Inv("BShop", bsno, IsPOS))
            {
                frm.Result[0] = InvDate;
                frm.Result[1] = InvName.Text.Trim();
                frm.Result[2] = InvTaxNo.Text.Trim();
                frm.Result[3] = InvAddr1;

                //媒體申報
                if (invkind == "")
                {
                    if (X5No.Text.Trim() == "1")
                    {
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("21 進項三聯式、電子計算機統一發票");
                    }
                    else if (X5No.Text.Trim() == "2")
                    {
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("22 進項二聯式收銀機統一發票、載有稅額之其他憑證");
                    }
                    else if (X5No.Text.Trim() == "3")
                    {
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("22 進項二聯式收銀機統一發票、載有稅額之其他憑證");
                    }
                    else if (X5No.Text.Trim() == "4")
                    {
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("25 進項三聯式收銀機統一發票及一般稅額計算之電子發票");
                    }
                }
                else
                    frm.InvKind.SelectedIndex = frm.InvKind.FindString(invkind);

                if (sub == "")
                    frm.Sub.SelectedIndex = 0;
                else
                    frm.Sub.SelectedIndex = frm.Sub.FindString(sub);

                frm.CustomsNo.Text = customsno;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InvDate = frm.Result[0];
                    InvName.Text = frm.Result[1];
                    InvTaxNo.Text = frm.Result[2];
                    InvAddr1 = frm.Result[3];

                    //媒體申報
                    invkind = frm.InvKind.Text.GetUTF8(20);
                    sub = frm.Sub.Text.Substring(0, 1);
                    customsno = frm.CustomsNo.Text;
                }
            }
        }

        private void FqNo_DoubleClick(object sender, EventArgs e)
        {
            if (FqNo.ReadOnly)
                return;

            FqNo.CausesValidation = false;

            using (var frm = new subMenuFm_1.FrmFQuotToShop())
            {
                frm.FaNo = FaNo.Text.Trim();
                frm.SeekNo = FqNo.Text.Trim();

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                jBShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtBShopD, ref dtBShopBom, SetAllMny);

                this.PassFQuotM(frm);

                this.PassFQuotD(frm);

                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                    dataGridViewT1.Focus();
                }
            }
            FqNo.Tag = FqNo.Text.Trim();
            SetAllMny();

            FqNo.CausesValidation = true;
        }
        private void FqNo_Validating(object sender, CancelEventArgs e)
        {
            if (FqNo.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            if (FqNo.TrimTextLenth() == 0)
            {
                FqNo.Tag = "";
                return;
            }

            if (FqNo.Tag == null)
            {
                FqNo.Tag = "";
            }

            if (jBShop.IsExistDocument<JBS.JS.FQuot>(FqNo.Text.Trim()) == true)
            {
                //已經開過窗了, 之後就不開
                if (FqNo.Tag.ToString() == FqNo.Text.Trim())
                    return;

                using (var frm = new subMenuFm_1.FrmFQuotToShop())
                {
                    frm.FaNo = FaNo.Text.Trim();
                    frm.SeekNo = FqNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jBShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtBShopD, ref dtBShopBom, SetAllMny);

                    this.PassFQuotM(frm);

                    this.PassFQuotD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                FqNo.Tag = FqNo.Text.Trim();
                SetAllMny();
            }
            else
            {
                e.Cancel = true;
                using (var frm = new subMenuFm_1.FrmFQuotToShop())
                {
                    frm.FaNo = FaNo.Text.Trim();
                    frm.SeekNo = FqNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jBShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtBShopD, ref dtBShopBom, SetAllMny);

                    this.PassFQuotM(frm);

                    this.PassFQuotD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                FqNo.Tag = FqNo.Text.Trim();
                SetAllMny();
            }
        }
        private void PassFQuotM(FrmFQuotToShop frm)
        {
            FqNo.Text = frm.FqNo.Trim();
            Xa1No.Text = frm.MasterRow["Xa1no"].ToString();
            Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
            Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
            X3No.Text = frm.MasterRow["X3no"].ToString();
            X3No1.Text = frm.MasterRow["X3no"].ToString();
            Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            Rate1.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            EmNo.Text = frm.MasterRow["EmNo"].ToString();
            EmName.Text = frm.MasterRow["EmName"].ToString();
            PhotoPath = frm.MasterRow["PhotoPath"].ToString();
            getDeNo();
            BsMemo.Text = frm.MasterRow["FQMemo"].ToString();
            pVar.XX03Validate(X3No.Text, X3No, X3Name);
            pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);
            this.Memo1 = frm.MasterRow["fqmemo1"].ToString();

            if (FaNo.Text.Trim() == "")
            {
                FaNo.Text = frm.MasterRow["FaNo"].ToString();
                FaName11.Text = frm.MasterRow["FaName1"].ToString();
                FaPer11.Text = frm.MasterRow["FaPer1"].ToString().GetUTF8(10);
                FaTel11.Text = frm.MasterRow["FaTel1"].ToString();

                jBShop.Validate<JBS.JS.Fact>(FaNo.Text.Trim(), reader =>
                {
                    InvTaxNo.Text = reader["FaUno"].ToString();
                    InvName.Text = reader["FaName2"].ToString();
                    FaPayAmt.Text = reader["FaPayAmt"].ToDecimal().ToString("f" + FaPayAmt.LastNum);
                });
            }
        }
        private void PassFQuotD(FrmFQuotToShop frm)
        {
            DataRow row = null;
            DataTable dtD = frm.dtFQuotD;

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                var rec = GetBomRec();
                row = dtBShopD.NewRow();
                row["itno"] = dtD.Rows[i]["itno"];
                row["itname"] = dtD.Rows[i]["itname"];
                row["stno"] = StNo.Text.Trim();
                row["stname"] = StName.Text.Trim();
                row["ittrait"] = dtD.Rows[i]["ittrait"];
                row["ItNoUdf"] = dtD.Rows[i]["ItNoUdf"];
                if (dtD.Rows[i]["ittrait"].ToString() == "1") row["產品組成"] = "組合品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "2") row["產品組成"] = "組裝品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "3") row["產品組成"] = "單一商品";
                row["itunit"] = dtD.Rows[i]["itunit"];
                row["punit"] = getPUnit(dtD.Rows[i]["itno"].ToString().Trim());
                row["qty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["pqty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["itpkgqty"] = dtD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                row["price"] = dtD.Rows[i]["price"].ToDecimal("f" + Common.MF);
                row["prs"] = dtD.Rows[i]["prs"].ToDecimal("f3");
                row["taxprice"] = dtD.Rows[i]["taxprice"].ToDecimal("f6");
                row["mny"] = dtD.Rows[i]["mny"].ToDecimal("f" + Common.TPF);
                row["priceb"] = dtD.Rows[i]["priceb"].ToDecimal("f" + Common.M);
                row["taxpriceb"] = dtD.Rows[i]["taxpriceb"].ToDecimal("f6");
                row["mnyb"] = dtD.Rows[i]["mnyb"].ToDecimal("f" + Common.M);
                row["bomrec"] = rec;
                row["memo"] = dtD.Rows[i]["memo"];
                for (int j = 1; j < 11; j++)
                {
                    row["itdesp" + j] = dtD.Rows[i]["itdesp" + j];
                }
                dtBShopD.Rows.Add(row);
                dtBShopD.AcceptChanges();
                dataGridViewT1.InvalidateRow(dtBShopD.Rows.Count - 1);

                if (dtD.Rows[i]["ittrait"].ToString().Trim() == "3")
                    continue;

                var fqbomid = dtD.Rows[i]["bomid"].ToString();
                jBShop.GetTBom<JBS.JS.FQuot>(fqbomid, rec.ToString(), ref dtBShopBom);
            }
        }
        void getDeNo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@emno", EmNo.Text.Trim());
                        cmd.CommandText = "select d.deno,d.dename1 from empl as e left join dept as d on e.emdeno=d.deno where e.emno=@emno";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                DeNo.Text = reader["deno"].ToString().Trim();
                                DeName.Text = reader["dename1"].ToString().Trim();
                            }
                            else
                            {
                                DeNo.Text = "";
                                DeName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeNo.Text = "";
                DeName.Text = "";
                MessageBox.Show(ex.ToString());
            }
        }

        string getPUnit(string itno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itno", itno);
                    cmd.CommandText = "select punit from item where itno=@itno";
                    var unit = cmd.ExecuteScalar();
                    return unit.IsNullOrEmpty() ? "" : unit.ToString();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D1 | keyData == Keys.NumPad1 && btnAppend.Enabled)
            {
                btnAppend.PerformClick();
                return true;
            }
            else if (keyData == Keys.D2 | keyData == Keys.NumPad2 && btnModify.Enabled)
            {
                btnModify.PerformClick();
            }
            else if (keyData == Keys.D3 | keyData == Keys.NumPad3 && btnDelete.Enabled)
            {
                btnDelete.PerformClick();
            }
            else if (keyData == Keys.D4 | keyData == Keys.NumPad4 && btnBrow.Enabled)
            {
                btnBrow.PerformClick();
            }
            else if (keyData == Keys.D0 | keyData == Keys.NumPad0 | keyData == Keys.F11 && btnBrow.Enabled)
            {
                btnExit.PerformClick();
            }
            else if (keyData == Keys.Home && btnTop.Enabled)
            {
                btnTop.PerformClick();
            }
            else if (keyData == Keys.PageUp && btnPrior.Enabled)
            {
                btnPrior.PerformClick();
            }
            else if (keyData == Keys.PageDown && btnNext.Enabled)
            {
                btnNext.PerformClick();
            }
            else if (keyData == Keys.End && btnBottom.Enabled)
            {
                btnBottom.PerformClick();
            }
            else if (keyData == Keys.F9 && btnSave.Enabled)
            {
                btnSave.PerformClick();
            }
            else if (keyData == Keys.F4 && btnCancel.Enabled)
            {
                btnCancel.Focus();
                btnCancel.PerformClick();
            }
            else if (keyData == Keys.F2 && gridAppend.Enabled)
            {
                gridAppend_Click(null, null);
            }
            else if (keyData == Keys.F3 && gridDelete.Enabled)
            {
                gridDelete_Click(null, null);
            }
            else if (keyData == Keys.F5 && gridInsert.Enabled)
            {
                gridInsert_Click(null, null);
            }
            else if (keyData == Keys.F6 && gridItDesp.Enabled)
            {
                gridItDesp_Click(null, null);
            }
            else if (keyData == Keys.F7 && gridBomD.Enabled)
            {
                gridBomD_Click(null, null);
            }
            else if (keyData == Keys.F8 && gridStock.Enabled)
            {
                gridStock_Click(null, null);
            }
            else if (keyData.ToString() == "F9, Shift" && gridTran.Enabled)
            {
                gridTran_Click(null, null);
            }
            else if (keyData.ToString() == "F10, Shift" && gridAllTrans.Enabled)
            {
                gridAllTrans_Click(null, null);
            }
            else if (keyData.ToString() == "F11, Shift" && gridSaleHistory.Enabled)
            {
                gridSaleHistory_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BsMemo_DoubleClick(object sender, EventArgs e)
        {
            if (BsMemo.ReadOnly) return;
            using (FrmSale_Memo frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK) BsMemo.Text = frm.Memo.GetUTF8(60);
                BsMemo.SelectAll();
            }
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ToolTip tip = new ToolTip();
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "進貨倉庫" || str == "備註說明" || str == "採購憑證")
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
            else if (str == "進貨數量")
            {
                if (Common.Sys_DBqty == 1)
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    t.KeyDown += new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
                }
                else
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "");
                }
            }
            else if (str == "計價數量")
            {
                if (Common.Sys_DBqty == 2)
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    t.KeyDown += new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
                }
                else
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "");
                }
            }
            else
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "");
            }
        }

        void t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                dataGridViewT1_CellDoubleClick(dataGridViewT1, new DataGridViewCellEventArgs(dataGridViewT1.CurrentCell.ColumnIndex, dataGridViewT1.CurrentCell.RowIndex));
            }
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = FaNo.ReadOnly ? false : true;
                frm.memo1 = Memo1;

                if (frm.ShowDialog() == DialogResult.OK) Memo1 = frm.memo1;
            }
        }

        private void dataGridViewT1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "進貨數量")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim());
                    cmd.CommandText = "select itstockqty from item where itno=@itno";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        toolStripStatusLabel1.Text = "現有庫存量：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);
                }
            }
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "進價")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                if (FaNo.Text.Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim());
                    cmd.Parameters.AddWithValue("itunit", dataGridViewT1["單位", e.RowIndex].Value.ToString().Trim());
                    cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("bsdate", Date.ToTWDate(BsDate.Text));
                    cmd.CommandText = "select top(1)taxprice from bshopd where "
                        + " itno=@itno "
                        + " and itunit=@itunit "
                        + " and fano=@fano "
                        + " and bsdate<=@bsdate "
                        + " order by bsdate desc,bomid desc";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        toolStripStatusLabel1.Text = "最後一次交易稅前進價：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.MF);
                    else
                        toolStripStatusLabel1.Text = "最後一次交易稅前進價：0";
                }
            }
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim());
                    cmd.CommandText = "select itunit,itunitp,itpkgqty from item where itno=@itno ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            toolStripStatusLabel1.Text = reader["itpkgqty"].ToDecimal() == 0 ? "" : reader["itpkgqty"].ToDecimal().ToString("f" + Common.Q);
                            toolStripStatusLabel1.Text += reader["itunit"].ToString() == "" ? " / " : reader["itunit"].ToString() + " / ";
                            toolStripStatusLabel1.Text += reader["itunitp"].ToString() == "" ? "未建檔" : reader["itunitp"].ToString();
                            toolStripStatusLabel1.Text += ",滑鼠左鍵快點兩下切換單位或自行輸入";
                        }
                    }
                }
            }
        }

        private void dataGridViewT1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "進貨數量" || dataGridViewT1.Columns[e.ColumnIndex].Name == "進價" || dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
                toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.瀏覽 0.結束";
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            pnlBsShare.Enabled = pnlBsShsel.Enabled = !btnAppend.Enabled;
        }

        private void btnBotoBshop_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() == "")
            {
                MessageBox.Show("廠商編號不能為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }

            if (Common.Sys_LendToSaleMode == 2)
            {
                #region 宏恩模式借轉銷
                using (S2.FrmBorrToBShopBAT frm = new S2.FrmBorrToBShopBAT())
                {
                    frm.fano = FaNo.Text.Trim();
                    frm.bsdate = BsDate.Text;
                    frm.x3no = X3No.Text.Trim();
                    frm.xa1par = Xa1Par.Text.ToDecimal();
                    frm.stno = StNo.Text.Trim();
                    frm.stname = StName.Text.Trim();
                    frm.maxrec = BomRec.ToInteger();

                    //判斷銷貨明細是否有借入明細(boid='V'),都沒有的話,表示首次開窗
                    var rows = dtBShopD.AsEnumerable().Where(r => r["boid"].ToString().Trim().Length > 0);
                    if (rows.Count() == 0)
                    {
                        frm.dtBShopD.Clear();
                        if (frm.dtBShopD.Columns.Count == 0) frm.dtBShopD = dtBShopD.Clone();
                    }
                    else
                    {
                        frm.dtBShopD = rows.CopyToDataTable();

                        this.borrtemp.AcceptChanges();//<=重要
                        frm.borrtemp = this.borrtemp;
                        frm.borrtempNOedit = this.borrtemp.Copy();
                    }

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.borrtemp = frm.borrtemp;
                        //先刪除有借入單據的明細,在import新取回的row
                        for (int i = 0; i < dtBShopD.Rows.Count; i++)
                        {
                            if (dtBShopD.Rows[i]["boid"].ToString().Trim().Length > 0)
                            {
                                dtBShopD.Rows.RemoveAt(i--);
                            }
                        }
                        dtBShopD.AcceptChanges();

                        //再刪除借入單據的bom,在取得bom表
                        var boms = dtBShopBom.AsEnumerable().Where(r => r["ItSource"].ToDecimal() != 1);
                        if (boms.Count() == 0) dtBShopBom.Clear();
                        else
                        {
                            dtBShopBom = boms.CopyToDataTable();
                        }
                        dtBShopBom.AcceptChanges();

                        //import明細 and 取得bom表
                        DataTable dtgetbom = new DataTable();
                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        using (SqlCommand cmd = cn.CreateCommand())
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("boitno", "");
                            cmd.Parameters.AddWithValue("BomRec", "");
                            for (int i = 0; i < frm.dtBShopD.Rows.Count; i++)
                            {
                                this.dtBShopD.ImportRow(frm.dtBShopD.Rows[i]);

                                cmd.Parameters["boitno"].Value = frm.dtBShopD.Rows[i]["itno"].ToString().Trim();
                                cmd.Parameters["bomrec"].Value = frm.dtBShopD.Rows[i]["bomrec"].ToString().Trim();
                                cmd.CommandText = " Select BomRec=(@BomRec),ItSource=1.0,[itno],[itname],[itunit],[itqty],[itpareprs],[itprice],[itprs],[itmny],[itpkgqty],[itnote],[itrec] from bomd where boitno=(@boitno) ";
                                da.Fill(dtgetbom);
                            }
                        }
                        dtgetbom.AcceptChanges();
                        //
                        dtBShopBom.Merge(dtgetbom);
                        dtBShopBom.AcceptChanges();
                        //
                        dtBShopD.AcceptChanges();
                        SetAllMny();
                        //
                        this.BomRec = frm.maxrec;
                    }
                    else //表示frm.Cancel
                    {
                        this.borrtemp.Dispose();
                        this.borrtemp = frm.borrtempNOedit;
                    }
                }
                #endregion
            }
            else
            {
                #region 一般模式借轉銷
                using (var frm = new FrmBorrToBShop())
                {
                    frm.TSeekNo = "";
                    frm.FaNo = FaNo.Text.Trim();
                    borrtemp.Clear();
                    borrtemp = dtBShopD.Copy();
                    borr = dtBShopD.Clone();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.MasterRow == null || frm.dtDetail.Rows.Count == 0) return;

                        Memo1 = frm.MasterRow["bomemo1"].ToString();
                        Xa1No.Text = frm.MasterRow["Xa1no"].ToString();
                        Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
                        Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
                        X3No.Text = frm.MasterRow["X3no"].ToString();
                        X3No1.Text = frm.MasterRow["X3no"].ToString();
                        Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
                        Rate1.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");

                        BsMemo.Text = frm.MasterRow["bomemo"].ToString();
                        EmNo.Text = frm.MasterRow["EmNo"].ToString();
                        EmName.Text = frm.MasterRow["EmName"].ToString();
                        getDeNo();

                        pVar.XX03Validate(X3No.Text, X3No, X3Name);
                        pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);

                        if (borrtemp.Rows.Count > 0)
                        {
                            for (int i = 0; i < borrtemp.Rows.Count; i++)
                            {
                                if (borrtemp.Rows[i]["itno"].ToString().Trim() == "")
                                {
                                    borrtemp.Rows[i].Delete();
                                    borrtemp.AcceptChanges();
                                }
                            }
                        }
                        if (dtBShopD.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtBShopD.Rows.Count; i++)
                            {
                                if (dtBShopD.Rows[i]["itno"].ToString().Trim() == "")
                                {
                                    dtBShopD.Rows[i].Delete();
                                    dtBShopD.AcceptChanges();
                                }
                            }
                        }
                        //明細部分&組件
                        DataRow row1, row2;

                        for (int i = 0; i < frm.dtDetail.Rows.Count; i++)
                        {
                            row1 = dtBShopD.NewRow();
                            row2 = frm.dtDetail.Rows[i];
                            if (row2["ittrait"].ToString() == "2")
                            {
                                row1["ittrait"] = row2["ittrait"].ToString();
                                row1["產品組成"] = "組裝品";
                            }
                            else if (row2["ittrait"].ToString() == "3")
                            {
                                row1["ittrait"] = row2["ittrait"].ToString();
                                row1["產品組成"] = "單一商品";
                            }
                            else
                            {
                                row1["ittrait"] = row2["ittrait"].ToString();
                                row1["產品組成"] = "組合品";
                            }
                            row1["bono"] = row2["bono"].ToString();

                            row1["itno"] = row2["itno"].ToString();
                            row1["itname"] = row2["itname"].ToString();
                            row1["itunit"] = row2["itunit"].ToString();
                            row1["Punit"] = row2["itunit"].ToString();
                            row1["qty"] = row2["qtynotout"].ToDecimal("f" + Common.Q);
                            row1["Pqty"] = row2["qtynotout"].ToDecimal("f" + Common.Q);

                            row1["price"] = row2["price"].ToDecimal("f" + Common.MF);
                            row1["prs"] = row2["prs"].ToDecimal("f3");
                            row1["taxprice"] = row2["taxprice"].ToDecimal("f6");
                            row1["memo"] = row2["memo"].ToString();
                            row1["priceb"] = row2["priceb"].ToDecimal("f" + Common.M);
                            row1["taxpriceb"] = row2["taxpriceb"].ToDecimal("f6");
                            row1["itpkgqty"] = row2["itpkgqty"].ToDecimal("f" + Common.Q);
                            row1["StNo"] = StNo.Text.Trim();
                            row1["StName"] = StName.Text;
                            row1["stnoo"] = row2["stnoo"].ToString().Trim();
                            row1["stnameo"] = row2["stnameo"].ToString().Trim();
                            row1["mwidth1"] = 0;
                            row1["mwidth2"] = 0;
                            row1["mwidth3"] = 0;
                            row1["mwidth4"] = 0;
                            row1["boid"] = row2["bomid"].ToString();

                            for (int j = 1; j <= 10; j++)
                            {
                                row1["itdesp" + j] = row2["itdesp" + j].ToString();
                            }
                            row1["BomRec"] = GetBomRec();
                            SetRow_Mny(row1);
                            dtBShopD.Rows.InsertAt(row1, dtBShopD.Rows.Count);
                            dtBShopD.AcceptChanges();


                            //組件部分
                            if (row2["ittrait"].ToDecimal() == 3) continue;
                            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                            {
                                cn.Open();
                                string sql = "select * from borrbom where bomid=@bomid";
                                using (SqlCommand cmd = cn.CreateCommand())
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("bomid", row2["bomid"].ToString().Trim());
                                    cmd.CommandText = sql;
                                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                                    {
                                        tempBom.Clear();
                                        dd.Fill(tempBom);
                                        for (int j = 0; j < tempBom.Rows.Count; j++)
                                        {
                                            tempBom.Rows[j]["BomRec"] = row1["BomRec"].ToString().Trim();
                                        }
                                        dtBShopBom.Merge(tempBom);
                                        dtBShopBom.AcceptChanges();
                                    }
                                }
                            }
                            dataGridViewT1.InvalidateRow(i);
                        }
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.CurrentRow.Index];
                        dataGridViewT1.CurrentRow.Selected = true;
                        for (int i = 0; i < dtBShopD.Rows.Count; i++)
                        {
                            SetRow_TaxPrice(dtBShopD.Rows[i]);
                            SetRow_Mny(dtBShopD.Rows[i]);

                        }
                        SetAllMny();

                    }
                }
                //避免重複撈單據
                if (borrtemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBShopD.Rows.Count; i++)
                    {
                        bool repeat = false;
                        for (int j = 0; j < borrtemp.Rows.Count; j++)
                        {
                            if (dtBShopD.Rows[i]["boid"].ToDecimal() == borrtemp.Rows[j]["boid"].ToDecimal())
                            {
                                repeat = true;
                                break;
                            }


                        }
                        if (!repeat)
                        {
                            borr.ImportRow(dtBShopD.Rows[i]);
                            borr.AcceptChanges();
                        }
                    }
                    if (borrtemp.Rows.Count > 0)
                    {
                        if (borr.Rows.Count > 0)
                        {
                            borrtemp.Merge(borr);
                            dtBShopD.Clear();
                            dtBShopD.Merge(borrtemp);
                        }
                        else
                        {
                            dtBShopD.Clear();
                            dtBShopD.Merge(borrtemp);
                        }
                        for (int i = 0; i < dtBShopD.Rows.Count; i++)
                        {
                            SetRow_TaxPrice(dtBShopD.Rows[i]);
                            SetRow_Mny(dtBShopD.Rows[i]);

                        }
                        SetAllMny();
                    }
                }
                #endregion
            }
        }


        //選擇檔頭檔案路徑 13.7C
        private void ShowORModify_PhotoPath_Click(object sender, EventArgs e)
        {
            using (FrmAffixFile frm = new FrmAffixFile())
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dt_採購關聯進貨 = new DataTable())
                {
                    string sqlcommand = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號, * from AffixFile where (DaType ='進貨單' and Datano=@Datano) or (Datano=@Datano2 and DaType='詢價單')";
                    //撈出訂單關聯的報價單
                    string sqlcommand_ = "SELECT distinct(FQuot.fqno) FROM FQuot INNER JOIN ford on FQuot.fqno = ford.fqno  where 1=0  ";
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Datano", BsNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Datano2", FqNo.Text.Trim());
                   
                    if (dtBShopD.Rows.Count > 0)
                    {
                        List<string> DistinctList = new List<string>();
                        for (int i = 0; i < dtBShopD.Rows.Count; i++)
                        {
                            if (DistinctList.IndexOf(dtBShopD.Rows[i]["fono"].ToString()) == -1)
                            {
                                var Datano = "Datano3" + i.ToString();
                                cmd.Parameters.AddWithValue(Datano, dtBShopD.Rows[i]["fono"].ToString());
                                DistinctList.Add(dtBShopD.Rows[i]["fono"].ToString());
                                sqlcommand += " or (DaType ='採購單' and Datano=@" + Datano + ")";
                                sqlcommand_ += " or [ford].fono =@" + Datano;
                            }
                        }
                        cmd.CommandText = sqlcommand_;
                        da.Fill(dt_採購關聯進貨);
                        for (int i = 0; i < dt_採購關聯進貨.Rows.Count; i++)
                        {
                            var Datano = "Datano4" + i.ToString();
                            cmd.Parameters.AddWithValue(Datano, dt_採購關聯進貨.Rows[i]["fqno"].ToString());
                            sqlcommand += " or (DaType ='詢價單' and Datano=@" + Datano + ")";
                        }
                    }

                    cmd.CommandText = sqlcommand;
                    DaFile.Clear();
                    da.Fill(DaFile);
                
                    frm.CMD = cmd;
                    frm.DtFile = DaFile;
                    frm.Datano = BsNo.Text.Trim();
                    frm.DaType = "進貨單";
                    if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
                    {
                        frm.編輯狀態 = "新增";
                    }
                    else
                    {
                        frm.編輯狀態 = "修改";
                    } 
                    frm.ShowDialog();
                }
            }
        }
        private void 選擇檔頭檔案路徑()
        {
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {
                //OpenFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                OpenFileDialog.Title = "請選擇檔案";
                OpenFileDialog.ShowDialog();
                if (OpenFileDialog.FileName != "")
                    PhotoPath = OpenFileDialog.FileName;
            }
            if (File.Exists(PhotoPath))
            {
                Process.Start(PhotoPath);
            }
        }
       
        private void gridInvMode_Click(object sender, EventArgs e)
        {
            gridInvMode.Text = gridInvMode.Text == "批開" ? "" : "批開";
        }

        private void X5No_TextChanged(object sender, EventArgs e)
        {
            if (X5No.Text.ToString() == "7" || X5No.Text.ToString() == "8")
            {
                lblEInvChange.Visible = true;
                EInvChange.Visible = true;
                EInvState.Visible = true;
                lblEInvState.Visible = true;
                invrandom.Visible = true;
                lblinvrandom.Visible = true;
                if (InvNo.Text.Trim() != "")
                    EInvState.Text = "已上傳";
                else
                    EInvState.Text = "未上傳";
            }
            else
            {
                lblEInvChange.Visible = false;
                EInvChange.Visible = false;
                EInvState.Visible = false;
                lblEInvState.Visible = false;
                invrandom.Visible = false;
                lblinvrandom.Visible = false;
            }
        }

        private void gridbatch_Click(object sender, EventArgs e)
        {
            this.Validate();
            BatchF.WhenGridBadch_click(this.Name, dataGridViewT1, dtBShopD, dt_BatchProcess, FaNo, FaName11, null, null, null, false, btnSave.Enabled);
        }

        private void buttonGridT1_Click(object sender, EventArgs e)
        {
            gridAllTrans.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtBShopD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有廠商所有產品 frm = new S2.Frm所有廠商所有產品())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }




        //實際成本
        void FeeSummary()
        {
            decimal fee = 0, total = 0;
            decimal.TryParse(TransportB.Text, out fee);
            total += fee; fee = 0;
            decimal.TryParse(PremiumB.Text, out fee);
            total += fee; fee = 0;
            decimal.TryParse(TariffB.Text, out fee);
            total += fee; fee = 0;
            decimal.TryParse(Postal.Text, out fee);
            total += fee; fee = 0;
            decimal.TryParse(ProcFee.Text, out fee);
            total += fee; fee = 0;
            decimal.TryParse(Certif.Text, out fee);
            total += fee; fee = 0;
            decimal.TryParse(Clearance.Text, out fee);
            total += fee; fee = 0;
            decimal.TryParse(OtherFee.Text, out fee);
            total += fee;
            TotFee.Text = total.ToString("f" + TotFee.LastNum);
           // FeeShare();
        }
        void FeeShare()
        {
            if (dataGridViewT1.ReadOnly) return;

            var par = Xa1Par.Text.ToDecimal();
            if (BsShare1.Checked)
            {
                for (int i = 0; i < dtBShopD.Rows.Count; i++)
                {
                    dtBShopD.Rows[i]["realcost"] = (par * dtBShopD.Rows[i]["taxprice"].ToDecimal()).ToDecimal("f" + Common.M);
                    dataGridViewT1.InvalidateRow(i);
                }
            }
            else if (BsShare2.Checked)
            {
                if (BsShsel1.Checked)
                {
                    //依數量
                    decimal qty = 0, totqty = 0, taxprice = 0, totfee = 0;
                    string fee = "";
                    for (int i = 0; i < dtBShopD.Rows.Count; i++)
                    {
                        if (!dtBShopD.Rows[i]["qty"].IsNullOrEmpty())
                        {
                            qty = dtBShopD.Rows[i]["qty"].ToDecimal();
                            totqty += qty;
                        }
                    }
                    //攤費：費用/數量
                    decimal.TryParse(TotFee.Text, out totfee);
                    if (totqty == 0)
                    {
                        fee = "0.0000";
                    }
                    else
                    {
                        fee = (totfee / totqty).ToString("f" + Common.M);
                    }

                    for (int i = 0; i < dtBShopD.Rows.Count; i++)
                    {
                        if (dtBShopD.Rows[i]["taxprice"].IsNullOrEmpty())
                        {
                            dtBShopD.Rows[i]["taxprice"] = 0;
                            dtBShopD.Rows[i]["realcost"] = fee;
                        }
                        else
                        {
                            taxprice = dtBShopD.Rows[i]["taxprice"].ToDecimal() * par;
                            decimal tempfee = 0;
                            decimal.TryParse(fee, out tempfee);
                            dtBShopD.Rows[i]["realcost"] = (taxprice + tempfee).ToString("f" + Common.M);
                        }
                        dataGridViewT1.InvalidateRow(i);
                    }
                }
                else if (BsShsel2.Checked)
                {
                    //依金額
                    decimal totfee = 0, taxmnyb = 0, taxprice = 0;

                    decimal.TryParse(TotFee.Text, out totfee);
                    decimal.TryParse(TaxMnyB.Text, out taxmnyb);

                    for (int i = 0; i < dtBShopD.Rows.Count; i++)
                    {
                        if (dtBShopD.Rows[i]["taxprice"].IsNullOrEmpty())
                        {
                            dtBShopD.Rows[i]["taxprice"] = 0;
                            dtBShopD.Rows[i]["realcost"] = 0;
                        }
                        else
                        {
                            if (taxmnyb == 0)
                            {
                                dtBShopD.Rows[i]["realcost"] = (par * dtBShopD.Rows[i]["taxprice"].ToDecimal()).ToDecimal("f" + Common.M);
                            }
                            else
                            {
                                taxprice = dtBShopD.Rows[i]["taxprice"].ToDecimal() * par;
                                dtBShopD.Rows[i]["realcost"] = (taxprice + (totfee * (taxprice / taxmnyb))).ToDecimal("f" + Common.M);
                            }
                        }
                        dataGridViewT1.InvalidateRow(i);
                    }
                }
            }
        }
        void BsShare_CheckedChanged(object sender, EventArgs e)
        {
            if (BsShare1.Checked)
            {
                BsShsel1.Enabled = false;
                BsShsel2.Enabled = false;
                for (int i = 0; i < dtBShopD.Rows.Count; i++)
                {
                    dtBShopD.Rows[i]["realcost"] = dtBShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
                    dataGridViewT1.InvalidateRow(i);
                }
            }
            else
            {
                BsShsel1.Enabled = true;
                BsShsel2.Enabled = true;
                FeeShare();
            }
        }
        void BsShsel_CheckedChanged(object sender, EventArgs e)
        {
            FeeShare();
        }
        private void Transport_Validating(object sender, CancelEventArgs e)
        {
            if (Transport.ReadOnly) return;
            decimal fee = 0, par = 0;
            decimal.TryParse(Transport.Text, out fee);
            decimal.TryParse(TranPar.Text, out par);
            TransportB.Text = (fee * par).ToString("f" + TransportB.LastNum);
            FeeSummary();
        }
        private void Premium_Validating(object sender, CancelEventArgs e)
        {
            if (Premium.ReadOnly) return;
            decimal fee = 0, par = 0;
            decimal.TryParse(Premium.Text, out fee);
            decimal.TryParse(PremPar.Text, out par);
            PremiumB.Text = (fee * par).ToString("f" + PremiumB.LastNum);
            FeeSummary();
        }
        private void Tariff_Validating(object sender, CancelEventArgs e)
        {
            if (Tariff.ReadOnly) return;
            decimal fee = 0, par = 0;
            decimal.TryParse(Tariff.Text, out fee);
            decimal.TryParse(TariPar.Text, out par);
            TariffB.Text = (fee * par).ToString("f" + TariffB.LastNum);
            FeeSummary();
        }
        private void Postal_Validating(object sender, CancelEventArgs e)
        {
            FeeSummary();
        }




























    }
}