using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using JE.SOther;
using S_61.Basic;
using S_61.SOther;
using System.Diagnostics;
using System.IO;

namespace S_61.subMenuFm_2
{
    public partial class FrmRShop : Formbase
    {
        JBS.JS.RShop jRShop;

        DataTable dtRShopD = new DataTable();
        DataTable tempD = new DataTable();

        DataTable dtRShopBom = new DataTable();
        DataTable tempBom = new DataTable();

        DataTable DaFile = new DataTable();//附件檔案

        #region 批次資料
        BatchProcess BatchF = new BatchProcess();          //批次存資料異動修改
        BatchSave BatchSave = new BatchSave();             //批次資料存檔用
        DataTable dt_BatchProcess = new DataTable();       //批次異動
        DataTable dt_TempBatchProcess = new DataTable();   //批次異動暫存檔
        #endregion

        string TextBefore;
        string ItNoBegin;
        string UdfNoBegin;
        string Memo1 = "";
        decimal BomRec = 0;
        string OldFact = "";

        string tempinvno = "";

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

        public FrmRShop()
        {
            InitializeComponent();
            this.jRShop = new JBS.JS.RShop();
            this.list = this.getEnumMember();
            this.cn1.ConnectionString = Common.sqlConnString;
            this.dataGridViewT1.tableName = "RShopd";

            BsDate.SetDateLength();
            BsDateAc.SetDateLength();
            pVar.SetMemoUdf(this.備註說明);

            Writes = new List<Control> { 
                BsNo, BsDate, BsDateAc, FromBShop, FaNo, FaName11, FaPer11, FaTel11, StNo, StName, Xa1No, Xa1Name, Xa1Par, 
                TaxMnyB, TaxMny, X3No, Rate, Tax, TotMny, Discount, CollectMny,  AcctMny, X4No, X4Name, EmNo, EmName, SeNo, SeName, SpNo, SpName, BsMemo, 
                TaxMny1, X3No1, Rate1, Tax1, TotMny1, X5No, InvNo, Discount1, CashMny, CardMny, CardNo, InvName, Ticket, CollectMny1,  AcctMny1, InvTaxNo,
                Transport, TranPar, TransportB, Premium, PremPar, PremiumB, Tariff, TariPar, TariffB, Postal, ProcFee, Certif, Clearance, OtherFee, TotFee,DeNo,DeName,EInvState,EInvChange,invrandom    
            };

            AllGridButtons = new List<Button> { gridAppend, gridDelete, gridPicture, gridInsert, gridItDesp, gridBomD, gridAddress, gridInvNo, gridInvMode };

            NumsInit_Zero = new List<TextBoxNumberT> { TaxMnyB, TaxMny, Tax, TotMny, Discount, CollectMny, AcctMny, TaxMny1, Tax1, TotMny1, Discount1, CashMny, CardMny, Ticket, CollectMny1, AcctMny1, Transport, TranPar, TransportB, Premium, PremPar, PremiumB, Tariff, TariPar, TariffB, Postal, ProcFee, Certif, Clearance, OtherFee, TotFee };
            CostControl = new List<TextBox> { TaxMnyB, TaxMny, Tax, TotMny, Discount, CollectMny, AcctMny, TaxMny1, Tax1, TotMny1, Discount1, CashMny, CardMny, Ticket, CollectMny1, AcctMny1, X3No, X3Name, Rate, X3No1, X3Name1, Rate1, X5No, X5Name, InvNo, CardNo };

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
            this.退貨數量.Set庫存數量小數();
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
            dataGridViewT1.DataSource = dtRShopD;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmRShop_Load(object sender, EventArgs e)
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
                end,ItNoUdf='',*
                from RShopD where 1=0 ";

                da.Fill(dtRShopD);
                da.Fill(tempD);
            }

            BatchF.建立結構(dt_BatchProcess, dt_TempBatchProcess);
            dataGridView1.DataSource = dt_BatchProcess;

            var pk = jRShop.Bottom();
            writeToTxt(pk);
        }

        private void FrmRShop_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string bsno)
        {
            BomRec = 0;

            var result = jRShop.LoadData(bsno, row =>
            {
                Writes.ForEach(r =>
                {
                    if (r.Name.EndsWith("1"))
                    {
                        r.Text = row[r.Name.Substring(0, r.Name.Length - 1)].ToString();
                    }
                    else
                    {
                        r.Text = row[r.Name].ToString();
                    }
                    if (r is TextBoxNumberT)
                    {
                        r.Text = r.Text.ToDecimal().ToString("f" + ((TextBoxNumberT)r).LastNum);
                    }
                });
                EInvChange.Text = row["einvchange"].ToString();
                EInvState.Text = row["einvstate"].ToString();
                invrandom.Text = row["invrandom"].ToString();

                //填入發票日期與地址
                var date = (Common.User_DateTime == 1) ? "" : "1";
                BsDate.Text = row["BsDate" + date].ToString();
                BsDateAc.Text = row["BsDateAc" + date].ToString();
                InvDate = row["InvDate" + date].ToString();
                InvAddr1 = row["InvAddr1"].ToString();

                FaPer11.Text = FaPer11.Text.GetUTF8(10);
                DeNo.Text = row["deno"].ToString().Trim();
                DeName.Text = row["dename"].ToString().Trim();

                //費用
                BsShare1.Checked = true;
                BsShare2.Checked = (row["BsShare"].ToString() == "2");
                BsShsel1.Checked = true;
                BsShsel2.Checked = (row["BsShsel"].ToString() == "2");

                //載入稅別名稱與發票名稱
                pVar.XX05Validate(row["X5No"].ToString(), X5No, X5Name);
                pVar.XX03Validate(row["X3No"].ToString(), X3No, X3Name);
                pVar.XX03Validate(row["X3No"].ToString(), X3No1, X3Name1);
                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                Rate1.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");

                PhotoPath = row["PhotoPath"].ToString();

                //進項批開
                if (row["invbatch"].ToDecimal() == 1)
                    gridInvMode.Text = "批開";
                else
                    gridInvMode.Text = "";

                //媒體申報
                invkind = row["invkind"].ToString();
                customsno = row["customsno"].ToString();
                sub = row["sub"].ToString();

                gridInvMode.Text = row["invbatch"].ToInteger() == 1 ? "批開" : "";

                //載入明細與暫存檔
                loadRShopD();
                loadPayAmt();

                this.OldFact = FaNo.Text.Trim();
                this.Memo1 = row["bsmemo1"].ToString();
                jRShop.keyMan.Set(row);
            });

            if (!result)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtRShopD.Clear();
                tempD.Clear();
                dtRShopBom.Clear();
                tempBom.Clear();

                this.OldFact = "";
                this.Memo1 = "";
                jRShop.keyMan.Clear();
            }
            BatchF.上下頁dt資料修改("rShopD", bsno, dt_BatchProcess, dt_TempBatchProcess);
        }

        void loadRShopD()
        {
            dtRShopD.Clear();
            tempD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                    cmd.CommandText = "select 產品組成="
                                     + " case"
                                     + " when ittrait=1 then '組合品'"
                                     + " when ittrait=2 then '組裝品'"
                                     + " when ittrait=3 then '單一商品'"
                                     + " end,ItNoUdf= (select top 1 itnoudf from item where item.itno = RShopD.itno),*"
                                     + " from RShopD where BsNo=@bsno order by recordno";

                    da.Fill(dtRShopD);
                    da.Fill(tempD);
                }
                if (dtRShopD.Rows.Count > 0) BomRec = dtRShopD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadBShopBom()
        {
            dtRShopBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select top 1 * from rshopbom where 1=0";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from rshopbom where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from rshopbom where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("bsno", jRShop.GetCurrent());
                    cmd.CommandText = sql;

                    da.Fill(dtRShopBom);
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
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    //載入廠商預付餘額
                    if (cn.State != ConnectionState.Open) cn.Open();
                    var sql = "select FaPayAmt from Fact where FaNo=(@FaNo) ";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
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
            var pk = jRShop.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jRShop.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jRShop.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jRShop.Bottom();
            writeToTxt(pk);
        }

        private void setTxtWhenAppend()
        {
            BomRec = 0;
            decimal d = 0;
            NumsInit_Zero.ForEach(t => t.Text = d.ToString("F" + t.LastNum));

            //新增一筆資料時,載入欄位預設值
            BsShare2.Checked = true;
            BsShsel1.Checked = true;

            StNo.Text = Common.User_StkNo;
            pVar.StkValidate(Common.User_StkNo, StNo, StName);

            Xa1No.Text = "TWD";
            pVar.Xa01Validate("TWD", Xa1No, Xa1Name);
            Xa1Par.Text = "1.000";

            InvAddr1 = "";
            X3No.Text = "1";
            X3No1.Text = "1";
            X5No.Text = "1";
            pVar.XX03Validate("1", X3No, X3Name, Rate);
            pVar.XX03Validate("1", X3No1, X3Name1, Rate1);
            pVar.XX05Validate("1", X5No, X5Name);

            BsDate.Text = Date.GetDateTime(Common.User_DateTime);
            BsDateAc.Text = Date.GetDateTime(Common.User_DateTime);
            InvDate = Date.GetDateTime(Common.User_DateTime);

            //電子發票預設
            invrandom.Visible = lblinvrandom.Visible = lblEInvChange.Visible = lblEInvState.Visible = EInvChange.Visible = EInvState.Visible = false;
            EInvState.Text = "未上傳";
            invrandom.Text = "";

            //媒體申報
            invkind = "";
            sub = "";
            customsno = "";

            //進項批開
            gridInvMode.Text = Common.User_ScInvBat == 1 ? "批開" : "";
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            dtRShopD.Clear();
            loadBShopBom();

            this.Memo1 = "";
            this.BomRec = 0;

            setTxtWhenAppend();
            BsDate.Focus();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess);
            this.自定編號.ReadOnly = true;
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            loadBShopBom();

            dtRShopD.AsEnumerable().ToList().ForEach(r => r["fono"] = "");

            switch (Common.User_DateTime)
            {
                case 1:
                    BsDate.Text = Date.GetDateTime(1, false);
                    BsDateAc.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    BsDate.Text = Date.GetDateTime(2, false);
                    BsDateAc.Text = Date.GetDateTime(2, false);
                    break;
            }

            CardNo.Text = "";
            BsNo.Text = "";
            //電子發票
            invrandom.Text = "";
            EInvState.Text = "未上傳";

            //媒體申報
            invkind = "";
            sub = "";
            customsno = "";

            InvNo.Text = "";
            InvDate = Date.GetDateTime(Common.User_DateTime);

            Discount.Text = "0";
            Discount1.Text = "0";
            CashMny.Text = "0";
            CardMny.Text = "0";
            Ticket.Text = "0";
            this.SetAcctMny();

            BsDate.Focus();
            FaNo.SelectAll();
            //電子發票預設
            EInvState.Text = "未上傳";
            invrandom.Text = "";
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess);
            this.自定編號.ReadOnly = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jRShop.IsExistDocument<JBS.JS.RShop>(BsNo.Text.Trim()) == false)
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
            }

            if (jRShop.IsEditInCloseDay(BsDate.Text) == false)
                return;

            if (jRShop.IsPassToAcc(BsNo.Text.Trim()) == true)
                return;

            if (jRShop.IsPassToPayabl(BsNo.Text.Trim()) == true)
                return;

            if (jRShop.IsModify<JBS.JS.RShop>(BsNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jRShop.upModify1<JBS.JS.RShop>(BsNo.Text.Trim());//更新修改狀態1
                var pk = jRShop.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            loadBShopBom();

            BsNo.Focus();
            FaNo.SelectAll();

            //紀錄發票號碼
            if (InvNo.Text.Trim() != "")
            {
                tempinvno = InvNo.Text.Trim();
            }
            this.自定編號.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jRShop.IsExistDocument<JBS.JS.RShop>(BsNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jRShop.IsModify<JBS.JS.RShop>(BsNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            //有折讓紀錄不可修改
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

            if (jRShop.IsEditInCloseDay(BsDate.Text) == false)
                return;

            if (jRShop.IsPassToAcc(BsNo.Text.Trim()) == true)
                return;

            if (jRShop.IsPassToPayabl(BsNo.Text.Trim()) == true)
                return;

            
            var pano = jRShop.GetThisPassToPayabl(BsNo.Text.Trim());
            jRShop.GetTempBomOnDeleting("RShopBom", BsNo.Text.Trim(), ref tempBom);

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
                    jRShop.GetOldAcctMnyOnDeleting(BsNo.Text.Trim(), out acctmny, out getprvacc);

                    for (int i = 0; i < tempD.Rows.Count; i++)
                    {
                        if (tempD.Rows[i]["foid"].ToString().Trim().Length > 0)
                        {
                            BackForddQty(cmd, i);
                        }
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("pano", pano);
                    cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                    cmd.CommandText = @"
                        delete from payabl   where ExtFlag =N'退貨' and pano =@pano COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from payabld  where ExtFlag =N'退貨' and pano =@pano COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from RShopBom where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from RShopD   where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from RShop    where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN; ";
                    cmd.ExecuteNonQuery();

                    jRShop.加庫存(cmd, tempD, tempBom);

                    FrmAffixFile.FileDelete_單據刪除(cmd, BsNo.Text.Trim(), "進退單");

                    BatchSave.進退_Delete(dt_TempBatchProcess, cmd, "rShopD", BsNo.Text.Trim());

                    tn.Commit();

                    jRShop.BackOldFactPayabl(this.OldFact.Trim(), acctmny, getprvacc);

                    jRShop.UpdateItemItStockQty(tempD, tempBom);

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
            using (var frm = new FrmRShop_Print())
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

            using (var frm = new S2.FrmRShopBrowNew())
            {
                frm.TSeekNo = BsNo.Text;
                frm.ShowDialog();

                writeToTxt(frm.TResult.Trim());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (jRShop.IsEditInCloseDay(BsDate.Text) == false)
                return;

            if (jRShop.IsRegisted() == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！\n";
                msg += "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (StNo.Text.Trim() == "")
            {
                FaNo.Focus();
                MessageBox.Show("倉庫編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            jRShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRShopD, ref dtRShopBom, SetAllMny);
            //上一方法刪除空值後，刪除對應該筆空值之批號
            BatchF.刪除無明細對應之批號資料(dtRShopD, dt_BatchProcess);

            if (Common.TPF == Common.MFT && X3No.Text.ToDecimal() != 2)
            {
                var checktaxmny = dtRShopD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPF));
                if (TaxMny.Text.ToDecimal() != checktaxmny)
                {
                    MessageBox.Show("稅前合計金額有誤！無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                //先分攤費用，計算成本，之後才存明細，因為明細裡有成本欄位
                if (BsShare2.Checked)
                {
                    this.FeeShare();
                }
                else
                {
                    for (int i = 0; i < dtRShopD.Rows.Count; i++)
                    {
                        if (dtRShopD.Rows[i]["realcost"].ToDecimal() == 0)
                        {
                            dtRShopD.Rows[i]["realcost"] = dtRShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
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

                        var result = jRShop.GetPkNumber<JBS.JS.RShop>(cmd, BsDate.Text, ref BsNo);
                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        this.AppendMasterOnSaving(cmd);

                        for (int i = 0; i < dtRShopD.Rows.Count; i++)
                        {
                            this.AppendDetailOnSaving(cmd, i);

                            if (dtRShopD.Rows[i]["foid"].ToString().Trim().Length > 0)
                            {
                                this.AnsyForddQtyOnSaving(cmd, i);
                            }
                        }

                        this.AppendBomOnSaving(cmd);

                        this.PassToPayablOnSaving(cmd);

                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, BsNo.Text.Trim(), "進退單");

                        jRShop.扣庫存(cmd, dtRShopD, dtRShopBom);
                       
                        //批次資料
                        BatchSave.進退_Append(dt_BatchProcess, cmd, "rShopD", BsNo.Text.Trim());

                        tn.Commit();

                        jRShop.Save(BsNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新廠商應付帳款
                            jRShop.UpdateNewFactPayabl(FaNo.Text, AcctMny.Text, "0");

                            //更新產品檔庫存量
                            jRShop.UpdateItemItStockQty(dtRShopD, dtRShopBom);
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
                    using (var frm = new FrmRShop_Print())
                    {
                        frm.PK = BsNo.Text.Trim();
                        frm.FaNo = FaNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jRShop.IsExistDocument<JBS.JS.RShop>(BsNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

                if (jRShop.IsPassToPayabl(BsNo.Text.Trim()) == true)
                    return;

                //先分攤費用，計算成本，之後才存明細，因為明細裡有成本欄位
                if (BsShare2.Checked)
                {
                    this.FeeShare();
                }
                else
                {
                    for (int i = 0; i < dtRShopD.Rows.Count; i++)
                    {
                        if (dtRShopD.Rows[i]["realcost"].ToDecimal() == 0)
                        {
                            dtRShopD.Rows[i]["realcost"] = dtRShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
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
                        jRShop.GetOldAcctMnyOnDeleting(BsNo.Text.Trim(), out acctmny, out getprvacc);

                        //儲存主檔
                        UpdateMasterOnSaving(cmd);

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                        cmd.CommandText = "delete from RShopD where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < tempD.Rows.Count; i++)
                        {
                            if (tempD.Rows[i]["foid"].ToString().Trim().Length > 0)
                            {
                                BackForddQty(cmd, i);
                            }
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                        cmd.CommandText = "delete from RShopBom where BsNo=@bsno COLLATE Chinese_Taiwan_Stroke_BIN";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < dtRShopD.Rows.Count; i++)
                        {
                            this.AppendDetailOnSaving(cmd, i);

                            if (dtRShopD.Rows[i]["foid"].ToString().Trim().Length > 0)
                            {
                                this.AnsyForddQtyOnSaving(cmd, i);
                            }
                        }

                        this.AppendBomOnSaving(cmd);

                        this.PassToPayablOnSaving(cmd);

                        jRShop.加庫存(cmd, tempD, tempBom);
                        jRShop.扣庫存(cmd, dtRShopD, dtRShopBom);

                        //批次資料
                        BatchSave.進退_Modify(dt_TempBatchProcess, dt_BatchProcess, cmd, "rShopD", BsNo.Text.Trim());

                        tn.Commit();

                        jRShop.Save(BsNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新廠商應付帳款
                            jRShop.BackOldFactPayabl(this.OldFact, acctmny, getprvacc);
                            jRShop.UpdateNewFactPayabl(FaNo.Text.Trim(), AcctMny.Text, "0");

                            //更新產品檔庫存量
                            jRShop.UpdateItemItStockQty(tempD, tempBom, dtRShopD, dtRShopBom);
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
                    using (var frm = new FrmRShop_Print())
                    {
                        frm.PK = BsNo.Text.Trim();
                        frm.FaNo = FaNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }
                jRShop.upModify0<JBS.JS.RShop>(BsNo.Text.Trim());//改回0為無修改狀態
                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
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
            cmd.Parameters.AddWithValue("FqNo", FromBShop.Text.Trim());
            cmd.Parameters.AddWithValue("FromBShop", FromBShop.Text.Trim());
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
            cmd.Parameters.AddWithValue("einvstate", EInvState.Text.Trim());//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvchange", EInvChange.Text.Trim());//發票傳遞方式 
            cmd.Parameters.AddWithValue("invrandom", invrandom.Text.Trim());//發票隨機防伪碼 4碼
            //cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.ToString().GetUTF8(100));

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
            if (gridInvMode.Text.Trim() == "批開")
                cmd.Parameters.AddWithValue("invbatch", 1);
            else
                cmd.Parameters.AddWithValue("invbatch", 2);
            
            cmd.CommandText = "UPDATE RShop set "
            + "BsNo=@Bsno"
            + ",BsDate=@BsDate"
            + ",bsdate1=@bsdate1"
            + ",bsdate2=@bsdate2"
            + ",BsDateAc=@BsDateAc"
            + ",bsdateac1=@bsdateac1"
            + ",bsdateac2=@bsdateac2"
            + ",FqNo=@FqNo"
            + ",FromBShop=@FromBShop"
            + ",FaNo=@FaNo"
            + ",faname1=@faname1"
            + ",fatel1=@fatel1"
            + ",faper1=@faper1"
            + ",emno=@emno"
            + ",emname=@emname"
            + ",spno=@spno"
            + ",spname=@spname"
            + ",stno=@stno"
            + ",stname=@stname"
            + ",xa1no=@xa1no"
            + ",Xa1Name=@Xa1Name"
            + ",xa1par=@xa1par"
            + ",taxmnyb=@taxmnyb"
            + ",taxmny=@taxmny"
            + ",x3no=@x3no"
            + ",rate=@rate"
            + ",x5no=@x5no"
            + ",seno=@seno"
            + ",sename=@sename"
            + ",x4no=@x4no"
            + ",x4name=@x4name"
            + ",tax=@tax"
            + ",totmny=@totmny"
            + ",taxb=@taxb"
            + ",totmnyb=@totmnyb"
            + ",discount=@discount"
            + ",cashmny=@cashmny"
            + ",cardmny=@cardmny"
            + ",cardno=@cardno"
            + ",ticket=@ticket"
            + ",collectmny=@collectmny"
            + ",acctmny=@acctmny"
            + ",BsMemo=@BsMemo"
            + ",recordno=@recordno"
            + ",deno=@deno"
            + ",dename=@dename"
            + ",invno=@invno"
            + ",invdate=@invdate"
            + ",invdate1=@invdate1"
            + ",invname=@invname"
            + ",invtaxno=@invtaxno"
            + ",invaddr1=@invaddr1"
            + ",bsmemo1=@bsmemo1"
            + ",edtdate=@edtdate"
            + ",edtscno=@edtscno"
            + ",Transport=@Transport"
            + ",TranPar=@TranPar"
            + ",TransportB=@TransportB"
            + ",Premium=@Premium"
            + ",PremPar=@PremPar"
            + ",PremiumB=@PremiumB"
            + ",Tariff=@Tariff"
            + ",TariPar=@TariPar"
            + ",TariffB=@TariffB"
            + ",Postal=@Postal"
            + ",ProcFee=@ProcFee"
            + ",Certif=@Certif"
            + ",Clearance=@Clearance"
            + ",OtherFee=@OtherFee"
            + ",TotFee=@TotFee"
            + ",BsShare=@BsShare"
            + ",BsShsel=@BsShsel"
            //+ ",PhotoPath=@PhotoPath"
            + ",invbatch=@invbatch"
            + ",invkind=@invkind"
            + ",sub=@sub"
            + ",customsno=@customsno"
            + ",einvstate=@einvstate"
            + ",einvchange=@einvchange"
            + ",invrandom=@invrandom"
            + " WHERE BsNo =@bsno COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
            cmd.Parameters.AddWithValue("bsdate1", Date.ToUSDate(BsDate.Text));
            cmd.Parameters.AddWithValue("bsdate2", BsDate.Text);
            cmd.Parameters.AddWithValue("bsdateac1", Date.ToUSDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("bsdateac2", BsDateAc.Text);
            cmd.Parameters.AddWithValue("FqNo", FromBShop.Text.Trim());
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
            cmd.Parameters.AddWithValue("getprvacc", 0);
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
            cmd.Parameters.AddWithValue("frombshop", FromBShop.Text.Trim());
            cmd.Parameters.AddWithValue("einvstate", EInvState.Text.Trim());//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvchange", EInvChange.Text.Trim());//發票傳遞方式 
            cmd.Parameters.AddWithValue("invrandom", invrandom.Text.Trim());//發票隨機防伪碼 4碼
            //cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.ToString().GetUTF8(100));

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


            cmd.CommandText = "INSERT INTO RShop "
                + "(BsNo,bsdate1,bsdate2,bsdateac1,bsdateac2"
                + ",FqNo"
                + ",FaNo"
                + ",faname1,fatel1,faper1"
                + ",emno,emname,spno,spname,stno,stname"
                + ",xa1no,xa1name,xa1par"
                + ",taxmnyb,taxmny"
                + ",x3no,rate,x5no,seno,sename"
                + ",x4no,x4name,tax,totmny"
                + ",taxb,totmnyb,discount,cashmny"
                + ",cardmny,cardno,ticket,collectmny"
                + ",getprvacc,acctmny,BsMemo"
                + ",recordno,invno"
                + ",invdate,invdate1,invname,invtaxno,invaddr1"
                + ",appdate,edtdate,appscno,edtscno"
                + ",Transport,tranpar,Transportb"
                + ",Premium,PremPar,Premiumb"
                + ",Tariff,TariPar,Tariffb"
                + ",Postal,ProcFee,Certif"
                + ",Clearance,OtherFee,TotFee"
                + ",BsDate,BsDateAc,BsShare,BsShsel,bsmemo1,deno,dename,frombshop,invbatch,invkind,sub,customsno"
                + ",einvstate,einvchange,invrandom )"
                + " VALUES "
               + " (@BsNo,@bsdate1,@bsdate2,@bsdateac1,@bsdateac2"
               + ",@FqNo"
               + ",@FaNo"
               + ",@faname1,@fatel1,@faper1"
               + ",@emno,@emname,@spno,@spname,@stno,@stname"
               + ",@xa1no,@xa1name,@xa1par"
               + ",@taxmnyb,@taxmny"
               + ",@x3no,@rate,@x5no,@seno,@sename"
               + ",@x4no,@x4name,@tax,@totmny"
               + ",@taxb,@totmnyb,@discount,@cashmny"
               + ",@cardmny,@cardno,@ticket,@collectmny"
               + ",@getprvacc,@acctmny,@BsMemo"
               + ",@recordno,@invno"
               + ",@invdate,@invdate1,@invname,@invtaxno,@invaddr1"
               + ",@appdate,@edtdate,@appscno,@edtscno"
               + ",@Transport,@tranpar,@Transportb"
               + ",@Premium,@PremPar,@Premiumb"
               + ",@Tariff,@TariPar,@Tariffb"
               + ",@Postal,@ProcFee,@Certif"
               + ",@Clearance,@OtherFee,@TotFee"
               + ",@BsDate,@BsDateAc,@BsShare,@BsShsel,@bsmemo1,@deno,@dename,@frombshop,@invbatch,@invkind,@sub,@customsno"
               + ",@einvstate,@einvchange,@invrandom )";
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
            cmd.Parameters.AddWithValue("FqNo", FromBShop.Text.Trim());
            cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("spno", SpNo.Text);
            cmd.Parameters.AddWithValue("stno", dataGridViewT1["退貨倉庫", i].Value);
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
            cmd.Parameters.AddWithValue("qty", dataGridViewT1["退貨數量", i].Value);
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
            cmd.Parameters.AddWithValue("bomid", BsNo.Text + dtRShopD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", dtRShopD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1).ToString());
            cmd.Parameters.AddWithValue("itdesp1", dtRShopD.Rows[i]["ItDesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", dtRShopD.Rows[i]["ItDesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", dtRShopD.Rows[i]["ItDesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", dtRShopD.Rows[i]["ItDesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", dtRShopD.Rows[i]["ItDesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", dtRShopD.Rows[i]["ItDesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", dtRShopD.Rows[i]["ItDesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", dtRShopD.Rows[i]["ItDesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", dtRShopD.Rows[i]["ItDesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", dtRShopD.Rows[i]["ItDesp10"]);
            cmd.Parameters.AddWithValue("stName", dtRShopD.Rows[i]["stname"].ToString().Trim());
            cmd.Parameters.AddWithValue("BsDate", Date.ToTWDate(BsDate.Text));
            cmd.Parameters.AddWithValue("BsDateAc", Date.ToTWDate(BsDateAc.Text));
            cmd.Parameters.AddWithValue("fono", dtRShopD.Rows[i]["fono"]);
            cmd.Parameters.AddWithValue("foid", dtRShopD.Rows[i]["foid"]);
            cmd.Parameters.AddWithValue("mwidth1", dtRShopD.Rows[i]["mwidth1"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth2", dtRShopD.Rows[i]["mwidth2"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth3", dtRShopD.Rows[i]["mwidth3"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth4", dtRShopD.Rows[i]["mwidth4"].ToDecimal());
            cmd.Parameters.AddWithValue("Pformula", dtRShopD.Rows[i]["Pformula"].ToString());
            cmd.CommandText = "INSERT INTO RShopD "
            + "(BsNo,bsdate1,bsdate2,bsdateac1,bsdateac2"
            + ",FqNo"
            + ",FaNo,emno,spno"
            + ",stno,xa1no,xa1par,seno,sename"
            + ",x4no,x4name"
            + ",itno,itname"
            + ",ittrait,itunit,punit,itpkgqty,qty,pqty,price"
            + ",prs,rate,taxprice,realcost,mny,priceb"
            + ",taxpriceb,mnyb,memo"
            + ",bomid,bomrec,recordno"
            + ",itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
            + ",itdesp6,itdesp7,itdesp8,itdesp9,itdesp10"
            + ",stName,BsDate,BsDateAc,fono,foid,mwidth1,mwidth2,mwidth3,mwidth4,Pformula"
            + " ) VALUES "
            + "(@BsNo,@bsdate1,@bsdate2,@bsdateac1,@bsdateac2"
            + ",@FqNo"
            + ",@FaNo,@emno,@spno"
            + ",@stno,@xa1no,@xa1par,@seno,@sename"
            + ",@x4no,@x4name"
            + ",@itno,@itname"
            + ",@ittrait,@itunit,@punit,@itpkgqty,@qty,@pqty,@price"
            + ",@prs,@rate,@taxprice,@realcost,@mny,@priceb"
            + ",@taxpriceb,@mnyb,@memo"
            + ",@bomid,@bomrec,@recordno"
            + ",@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
            + ",@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10"
            + ",@stName,@BsDate,@BsDateAc,@fono,@foid,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@Pformula) ";
            cmd.ExecuteNonQuery();
        }
        private void AnsyForddQtyOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyin", dtRShopD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qtyNotin", dtRShopD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("bomid", dtRShopD.Rows[i]["foid"].ToString());
            cmd.Parameters.AddWithValue("fono", dtRShopD.Rows[i]["fono"].ToString());
            cmd.CommandText = @"
                update fordd set qtyin = qtyin-@qtyin, qtyNotin = qtyNotin+@qtyNotin
                where bomid=@bomid and fono=@fono";
            cmd.ExecuteNonQuery();

            //檢查採購是否結案
            cmd.CommandText = "select sum(qtyNotin) from fordd where fono=@fono and qtyNotin>0";
            if (cmd.ExecuteScalar().ToDecimal() <= 0)
            {
                cmd.CommandText = "update ford set "
                + " fooverflag = '" + 1 + "'"
                + " where fono=@fono";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText = "update ford set "
                + " fooverflag = '" + 0 + "'"
                + " where fono=@fono";
                cmd.ExecuteNonQuery();
            }
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < dtRShopBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
                cmd.Parameters.AddWithValue("BomID", BsNo.Text + dtRShopBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("BomRec", dtRShopBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", dtRShopBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", dtRShopBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", dtRShopBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", dtRShopBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", dtRShopBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", dtRShopBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", dtRShopBom.Rows[i]["itrec"]);
                cmd.Parameters.AddWithValue("itprice", dtRShopBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", dtRShopBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", dtRShopBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", dtRShopBom.Rows[i]["itnote"]);
                //cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.ToString().GetUTF8(100));
                
                cmd.CommandText = "INSERT INTO RShopBom"
                + "(BsNo,BomID,BomRec,itno,itname"
                + ",itunit,itqty,itpareprs,itpkgqty,itrec"
                + ",itprice,itprs,itmny,itnote) VALUES "
                + " (@BsNo,@BomID,@BomRec,@itno,@itname"
                + ",@itunit,@itqty,@itpareprs,@itpkgqty,@itrec"
                + ",@itprice,@itprs,@itmny,@itnote) ";
                cmd.ExecuteNonQuery();
            }
        }
        private void BackForddQty(SqlCommand cmd, int i)
        {
            if (tempD.Rows[i]["foid"].ToString() != "")
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("qtyin", tempD.Rows[i]["qty"].ToString());
                cmd.Parameters.AddWithValue("qtyNotin", tempD.Rows[i]["qty"].ToString());
                cmd.Parameters.AddWithValue("bomid", tempD.Rows[i]["foid"].ToString());
                cmd.Parameters.AddWithValue("fono", tempD.Rows[i]["fono"].ToString());
                cmd.CommandText = "update fordd set "
                + " qtyin = qtyin+@qtyin,"
                + " qtyNotin = qtyNotin-@qtyNotin"
                + " where bomid=@bomid"
                + " and fono=@fono";
                cmd.ExecuteNonQuery();
            }
            //檢查採購是否結案
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("fono", tempD.Rows[i]["fono"].ToString());
            cmd.CommandText = "select sum(qtyNotin) from fordd where fono=@fono";
            if (cmd.ExecuteScalar().ToDecimal() <= 0)
            {
                cmd.CommandText = "update ford set "
                + " fooverflag = '" + 1 + "'"
                + " where fono=@fono";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText = "update ford set "
                + " fooverflag = '" + 0 + "'"
                + " where fono=@fono";
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
            decimal.TryParse(CollectMny.Text, out collectmny);
            //沖款總額
            decimal _Total = 0;
            _Total = collectmny;
            //本幣總額
            decimal xa1par = 0;
            decimal _TotalB = 0;
            decimal.TryParse(Xa1Par.Text, out xa1par);
            _TotalB = _Total * xa1par;//沖款總額 * 匯率

            string pano = "";

            //刪除沖款
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
            cmd.CommandText = "select pano from payabld where ExtFlag =N'退貨' and BsNo =@bsno COLLATE Chinese_Taiwan_Stroke_BIN";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    if (reader.Read())
                        pano = reader["pano"].ToString();
            }

            cmd.Parameters.AddWithValue("pano", pano);
            cmd.CommandText = "delete from payabl where ExtFlag =N'退貨' and PaNo =@pano COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
            //
            cmd.CommandText = "delete from payabld where ExtFlag =N'退貨' and PaNo =@pano COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();



            //沖款
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
                cmd.Parameters.AddWithValue("cashmny", (-1) * CashMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("cardmny", (-1) * CardMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("cardno", CardNo.Text);
                cmd.Parameters.AddWithValue("ticket", (-1) * Ticket.Text.ToDecimal());
                cmd.Parameters.AddWithValue("getprvacc", 0);
                cmd.Parameters.AddWithValue("totmny", (-1) * _Total);
                cmd.Parameters.AddWithValue("actslt", 1);
                cmd.Parameters.AddWithValue("totdisc", (-1) * Discount.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totreve", (-1) * _Total);
                cmd.Parameters.AddWithValue("memo2", "退貨單轉入");
                cmd.Parameters.AddWithValue("BsNo", BsNo.Text.Trim());
                cmd.Parameters.AddWithValue("Bracket", "退貨");
                cmd.Parameters.AddWithValue("recordno", 1);
                cmd.Parameters.AddWithValue("ExtFlag", "退貨");
                cmd.Parameters.AddWithValue("TotMny1", 0);
                cmd.Parameters.AddWithValue("TotExgDiff", 0);
                cmd.Parameters.AddWithValue("CheckMny", 0);
                cmd.Parameters.AddWithValue("RemitMny", 0);
                cmd.Parameters.AddWithValue("OtherMny", 0);
                cmd.Parameters.AddWithValue("AddPrvAcc", 0);
                cmd.Parameters.AddWithValue("xa1par1", Xa1Par.Text);


                cmd.CommandText = "INSERT INTO payabl"
                + "(PaNo,padate,padate1"
                + ",FaNo,faname1,fatel1,emno,emname"
                + ",cashmny,cardmny,cardno,ticket"
                + ",getprvacc,totmny,actslt,totdisc,totreve"
                + ",memo2,BsNo,Bracket"
                + ",recordno,ExtFlag,TotMny1,TotExgDiff"
                + ",CheckMny,RemitMny,OtherMny,AddPrvAcc,xa1par) VALUES "
                + "(@PaNo,@padate,@padate1"
                + ",@FaNo,@faname1,@fatel1,@emno,@emname"
                + ",@cashmny,@cardmny,@cardno,@ticket"
                + ",@getprvacc,@totmny,@actslt,@totdisc,@totreve"
                + ",@memo2,@BsNo,@Bracket"
                + ",@recordno,@ExtFlag,@TotMny1,@TotExgDiff"
                + ",@CheckMny,@RemitMny,@OtherMny,@AddPrvAcc,@xa1par1) ";
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
                cmd.Parameters.AddWithValue("bracket", "退貨");
                cmd.Parameters.AddWithValue("xa1no", Xa1No.Text);
                cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text);
                cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
                cmd.Parameters.AddWithValue("totmny", (-1) * TotMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("acctmny", (-1) * AcctMny.Text.ToDecimal());//未付金額 沖款顯示全部金額
                cmd.Parameters.AddWithValue("invno", InvNo.Text);
                cmd.Parameters.AddWithValue("discount", (-1) * Discount.Text.ToDecimal());
                cmd.Parameters.AddWithValue("reverse", (-1) * _Total);
                cmd.Parameters.AddWithValue("xa1par1", Xa1Par.Text);
                cmd.Parameters.AddWithValue("reverseb", (-1) * _TotalB);
                cmd.Parameters.AddWithValue("exgstat", "匯兌收益");
                cmd.Parameters.AddWithValue("exgdiff", 0);//匯差
                cmd.Parameters.AddWithValue("extflag", "退貨");

                cmd.CommandText = "INSERT INTO payabld"
                + "(PaNo,padate,padate1"
                + ",FaNo,emno,emname,recordno"
                + ",bsdateac,bsdateac1,BsNo,bracket"
                + ",xa1no,xa1name,xa1par"
                + ",totmny,acctmny,invno,discount"
                + ",reverse,xa1par1,reverseb,exgstat"
                + ",exgdiff,extflag) VALUES "
                + "(@PaNo,@padate,@padate1"
                + ",@FaNo,@emno,@emname,@recordno"
                + ",@bsdateac,@bsdateac1,@BsNo,@bracket"
                + ",@xa1no,@xa1name,@xa1par"
                + ",@totmny,@acctmny,@invno,@discount"
                + ",@reverse,@xa1par1,@reverseb,@exgstat"
                + ",@exgdiff,@extflag) ";
                cmd.ExecuteNonQuery();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jRShop.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            jRShop.upModify0<JBS.JS.RShop>(BsNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        void GetSystemPrice(DataRow row, int index)
        {
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
                        var sql = " select * from bshopd where itno=(@itno)"
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
                        var sql = " select b.*,i.itno,f.fano from buygrad as b "
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
            var sum = dtRShopD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f"+Common.TPF)).ToDecimal("f" + Common.MFT);

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
                var totmny = dtRShopD.AsEnumerable().Sum(r => r["Pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MF)).ToDecimal("f" + Common.MFT);
                tax = (totmny / (1 + Common.Sys_Rate))* Common.Sys_Rate;

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
            acctmny = totmny - discount - getprvacc - collectmny;
            AcctMny.Text = acctmny.ToString("f" + Common.MFT);
            AcctMny1.Text = acctmny.ToString("f" + Common.MFT);
        }

        void ReLoadFact(SqlDataReader row)
        {
            if (row != null)
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

                for (int i = 0; i < dtRShopD.Rows.Count; i++)
                {
                    GetSystemPrice(dtRShopD.Rows[i], i);
                    SetRow_TaxPrice(dtRShopD.Rows[i]);
                    SetRow_Mny(dtRShopD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["FaNo"].ToString().Trim();
            }
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.Fact>(sender, row =>
            {
                ReLoadFact(row);
                getDeNo();
            });
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

            jRShop.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                if (TextBefore == FaNo.Text.Trim())
                    return;

                ReLoadFact(row);
                getDeNo();
            });
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.Stkroom>(sender, reader => DoubleToGetStNo(reader));
        }
        void DoubleToGetStNo(SqlDataReader reader)
        {
            StNo.Text = reader["StNo"].ToString().Trim();
            StName.Text = reader["StName"].ToString().Trim();

            if (Common.Sys_StNoMode == 1)
            {
                for (int i = 0; i < dtRShopD.Rows.Count; i++)
                {
                    dtRShopD.Rows[i]["stno"] = StNo.Text;
                    dtRShopD.Rows[i]["StName"] = StName.Text;
                    dataGridViewT1.InvalidateRow(i);
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtRShopD, i, StNo.Text.Trim(), StName.Text.Trim());
                }
            }

            this.TextBefore = reader["StNo"].ToString().Trim();
            StNo.Tag = reader["StNo"].ToString().Trim();
        }
        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.Text.Trim() == "")
            {
                e.Cancel = true;
                StNo.Text = "";
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jRShop.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
            {
                if (StNo.Text.Trim() == TextBefore)
                    return;

                DoubleToGetStNo(reader);
            });
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.XX03>(sender, reader =>
            {
                X3No.Text = X3No1.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = X3Name1.Text = reader["X3Name"].ToString();

                var rate = reader["X3rate"].ToDecimal() * 100;
                Rate.Text = Rate1.Text = rate.ToString("f0");

                for (int i = 0; i < dtRShopD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtRShopD.Rows[i]);
                    SetRow_Mny(dtRShopD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = reader["X3No"].ToString().Trim();
            });
        }
        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (X3No.ReadOnly || btnCancel.Focused)
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

            jRShop.ValidateOpen<JBS.JS.XX03>(sender, e, reader =>
            {
                if (TextBefore == reader["X3No"].ToString().Trim())
                    return;

                X3No.Text = X3No1.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = X3Name1.Text = reader["X3Name"].ToString();

                var rate = reader["X3rate"].ToDecimal() * 100;
                Rate.Text = Rate1.Text = rate.ToString("f0");

                for (int i = 0; i < dtRShopD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtRShopD.Rows[i]);
                    SetRow_Mny(dtRShopD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = reader["X3No"].ToString().Trim();
            });
        }

        private void X4No_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.XX04>(sender, row =>
            {
                X4No.Text = row["X4No"].ToString().Trim();
                X4Name.Text = row["X4Name"].ToString().Trim();
            });
        }
        private void X4No_Validating(object sender, CancelEventArgs e)
        {
            if (X4No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (X4No.Text.Trim() == "")
            {
                X4No.Text = "";
                X4Name.Text = "";
                return;
            }

            jRShop.ValidateOpen<JBS.JS.XX04>(sender, e, row =>
            {
                X4No.Text = row["X4No"].ToString().Trim();
                X4Name.Text = row["X4Name"].ToString().Trim();
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.Empl>(sender, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
            getDeNo();
        }
        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (EmNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (EmNo.Text.Trim() == "")
            {
                EmNo.Text = "";
                EmName.Text = "";
                getDeNo();
                return;
            }

            jRShop.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
            getDeNo();
        }

        private void SeNo_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.Send>(sender, row =>
            {
                SeNo.Text = row["SeNo"].ToString().Trim();
                SeName.Text = row["SeName"].ToString().Trim();
            });
        }
        private void SeNo_Validating(object sender, CancelEventArgs e)
        {
            if (SeNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SeNo.TrimTextLenth() == 0)
            {
                SeNo.Text = "";
                SeName.Text = "";
                return;
            }

            jRShop.ValidateOpen<JBS.JS.Send>(sender, e, row =>
            {
                SeNo.Text = row["SeNo"].ToString().Trim();
                SeName.Text = row["SeName"].ToString().Trim();
            });
        }

        private void X5No_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.XX05>(sender, row =>
            {
                X5No.Text = row["X5No"].ToString().Trim();
                X5Name.Text = row["X5Name"].ToString().Trim();
            });
        }
        private void X5No_Validating(object sender, CancelEventArgs e)
        {
            if (X5No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (X5No.Text.Trim() == "")
            {
                e.Cancel = true;
                X5No.Text = "";
                MessageBox.Show("發票類別編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jRShop.ValidateOpen<JBS.JS.XX05>(sender, e, row =>
            {
                X5No.Text = row["X5No"].ToString().Trim();
                X5Name.Text = row["X5Name"].ToString().Trim();
            });
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.Spec>(sender, row =>
            {
                SpNo.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["spname"].ToString().Trim();
            });
        }
        private void SpNo_Validating(object sender, CancelEventArgs e)
        {
            if (SpNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SpNo.Text.Trim() == "")
            {
                SpNo.Text = "";
                SpName.Text = "";
                return;
            }

            jRShop.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
            {
                SpNo.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["spname"].ToString().Trim();
            });
        }

        private void FaNo_Enter(object sender, EventArgs e)
        {
            //BsNo,FaNo,X3No,X3No1
            TextBefore = (sender as TextBox).Text;
        }

        private void BsNo_DoubleClick(object sender, EventArgs e)
        {
            if (BsNo.ReadOnly)
                return;

            using (var frm = new FrmRShop_Print_BsNo())
            { 
                frm.TSeekNo = BsNo.Text.Trim();

                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        BsNo.Text = frm.TResult;
                        break;
                }
            }
        }

        private void BsNo_Validating(object sender, CancelEventArgs e)
        {
            if (BsNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (BsNo.Text.Length > 0 && BsNo.Text.Trim() == "")
            {
                e.Cancel = true;
                BsNo.Text = "";
                BsNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowPhotoPath.Enabled = false;//沒有表單單號不可以附加檔案
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jRShop.IsExistDocument<JBS.JS.RShop>(BsNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jRShop.IsExistDocument<JBS.JS.RShop>(BsNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jRShop.IsExistDocument<JBS.JS.RShop>(BsNo.Text.Trim()))
                {
                    if (TextBefore == BsNo.Text.Trim())
                        return;

                    writeToTxt(BsNo.Text.Trim());
                    loadBShopBom();
                }
                else
                {
                    e.Cancel = true;
                    BsNo.SelectAll();

                    using (var frm = new FrmRShop_Print_BsNo())
                    { 
                        frm.TSeekNo = BsNo.Text.Trim();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            writeToTxt(frm.TResult);
                            loadBShopBom();
                        }
                    }
                }
            }
            ShowPhotoPath.Enabled = true;
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
                //過去的日子，以今天為準，未來的日子，以未來的退貨日為準
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
                for (int i = 0; i < dtRShopD.Rows.Count; i++)
                {
                    SetRow_Mny(dtRShopD.Rows[i]);
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
            DataRow dRow = dtRShopD.NewRow();
            dRow["itno"] = "";
            dRow["itnoudf"] = "";
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

            dtRShopD.Rows.Add(dRow);
            dtRShopD.AcceptChanges();
        }

        void DeleteEmptyRow(int index)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                //刪除明細前，先刪除明細的『組件明細』
                var rec = dataGridViewT1.Rows[index].Cells["結構編號"].Value.ToString();
                jRShop.RemoveBom(rec, ref dtRShopBom);

                //刪除明細
                dataGridViewT1.Rows.Remove(dataGridViewT1.Rows[index]);
                dtRShopD.AcceptChanges();

                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentRow.Selected = true;
                    for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                    {
                        dataGridViewT1["序號", i].Value = (i + 1).ToString();
                        dataGridViewT1.InvalidateRow(i);
                    }
                }
                //刪除列後，重新計算帳款
                SetAllMny();
            }
            dataGridViewT1.Focus();
        }

        void GridSaleDInsertRows(int index)
        {
            DataRow dRow = dtRShopD.NewRow();
            dRow["itno"] = "";
            dRow["itnoudf"] = "";
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

            dtRShopD.Rows.InsertAt(dRow, index);
            dtRShopD.AcceptChanges();
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

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BsNo.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                if (dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jRShop.DataGridViewOpen<JBS.JS.Item>(sender, e, dtRShopD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (CurrentColumnName == "退貨倉庫")
            {
                #region 退貨倉庫
                if (dataGridViewT1.Columns["退貨倉庫"].ReadOnly)
                    return;

                jRShop.DataGridViewOpen<JBS.JS.Stkroom>(sender, e, dtRShopD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["StNo"].ToString();

                    dtRShopD.Rows[e.RowIndex]["stno"] = row["StNo"].ToString();
                    dtRShopD.Rows[e.RowIndex]["stname"] = row["StName"].ToString();
                  
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtRShopD, e.RowIndex, row["StNo"].ToString(), row["StName"].ToString());
                });
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                if (dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itno = dtRShopD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jRShop.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (row != null && unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        dtRShopD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            dtRShopD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            dtRShopD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;

                dtRShopD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)//1代表一般進銷存
                {
                    GetSystemPrice(dtRShopD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtRShopD.Rows[e.RowIndex]);
                    SetRow_Mny(dtRShopD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
                #endregion
            }
            else if (CurrentColumnName == "備註說明")
            {
                #region 備註說明
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        dtRShopD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
            else if (CurrentColumnName == "退貨數量")
            {
                #region 退貨數量
                if (Common.Sys_DBqty == 1)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = dtRShopD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtRShopD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtRShopD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtRShopD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtRShopD.Rows[e.RowIndex]["Pformula"].ToString();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtRShopD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtRShopD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtRShopD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtRShopD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtRShopD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtRShopD.Rows[e.RowIndex]["qty"] = frm.resultCount.ToString("f" + Common.Q);
                            dtRShopD.Rows[e.RowIndex]["pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtRShopD.Rows[e.RowIndex]);
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
                        frm.w1 = dtRShopD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtRShopD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtRShopD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtRShopD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtRShopD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = dtRShopD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "退貨數量";

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtRShopD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtRShopD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtRShopD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtRShopD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtRShopD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;
                            dtRShopD.Rows[e.RowIndex]["qty"] = frm.qty;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtRShopD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtRShopD.Rows[e.RowIndex]);
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
                            dtRShopD.Rows[e.RowIndex]["punit"] = frm.Result;
                    }
                }
                #endregion 
            }
        }

        private void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["ItNo"].ToString().Trim();

            this.ItNoBegin = itno;
            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;
            dtRShopD.Rows[index]["itno"] = itno;
            dtRShopD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"];
            dtRShopD.Rows[index]["itname"] = reader["ItName"].ToString();
            dtRShopD.Rows[index]["PUnit"] = reader["PUnit"].ToString();
            dtRShopD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            //帶入產品常用倉庫
            if (reader["stno"].ToString().Trim().Length > 0)
            {
                dtRShopD.Rows[index]["stno"] = reader["stno"].ToString();
                dtRShopD.Rows[index]["stname"] = SQL.ExecuteScalar("select TOP 1 stname from stkroom where stno = @stno", new parameters("stno", reader["stno"].ToString()));
            }
            //退貨常用單位
            var utype = reader["ItBuyUnit"].ToString().Trim();
            var unit = "";
            //預設帶包裝單位或是單位
            if (utype == "1")
            {
                unit = reader["ItUnitp"].ToString();
                dtRShopD.Rows[index]["ItUnit"] = unit;

                var itpkgqty = reader["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                dtRShopD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                unit = reader["ItUnit"].ToString();
                dtRShopD.Rows[index]["ItUnit"] = unit;
                dtRShopD.Rows[index]["itpkgqty"] = 1;
            }

            GetSystemPrice(dtRShopD.Rows[index], index);
            SetRow_TaxPrice(dtRShopD.Rows[index]);
            SetRow_Mny(dtRShopD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            //組合組裝品
            string trait = reader["ItTrait"].ToString();
            dtRShopD.Rows[index]["ItTrait"] = trait;
            if (trait == "1") trait = "組合品";
            else if (trait == "2") trait = "組裝品";
            else if (trait == "3") trait = "單一商品";
            dtRShopD.Rows[index]["產品組成"] = trait;

            //產品規格說明
            for (int x = 1; x <= 10; x++)
            {
                dtRShopD.Rows[index]["ItDesp" + x] = reader["ItDesp" + x];
            }

            var rec = dataGridViewT1["結構編號", index].Value.ToString();
            jRShop.RemoveBom(rec, ref dtRShopBom);
            jRShop.GetItemBom(itno, rec, ref dtRShopBom);

            //刪除批次異動資訊
            BatchF.刪除批次異動(dt_BatchProcess, rec);
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

                jRShop.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dataGridViewT1.EndEdit();

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString();
                //空值->清空
                if (ItNoNow == "" || ItNoNow.Trim().Length == 0)
                {
                    if (btnSave.Focused) return;
                    dtRShopD.Rows[e.RowIndex]["foid"] = "";
                    dtRShopD.Rows[e.RowIndex]["fono"] = "";
                    dtRShopD.Rows[e.RowIndex]["itno"] = "";
                    dtRShopD.Rows[e.RowIndex]["ItNoUdf"] = "";
                    dtRShopD.Rows[e.RowIndex]["itname"] = "";
                    dtRShopD.Rows[e.RowIndex]["itunit"] = "";
                    dtRShopD.Rows[e.RowIndex]["punit"] = "";
                    dtRShopD.Rows[e.RowIndex]["qty"] = 0;
                    dtRShopD.Rows[e.RowIndex]["pqty"] = 0;
                    dtRShopD.Rows[e.RowIndex]["Price"] = 0;
                    dtRShopD.Rows[e.RowIndex]["TaxPrice"] = 0;
                    dtRShopD.Rows[e.RowIndex]["Mny"] = 0;
                    dtRShopD.Rows[e.RowIndex]["ItPkgQty"] = 1;
                    dtRShopD.Rows[e.RowIndex]["ItTrait"] = 0;
                    dtRShopD.Rows[e.RowIndex]["產品組成"] = "";
                    dtRShopD.Rows[e.RowIndex]["Memo"] = "";
                    dtRShopD.Rows[e.RowIndex]["PriceB"] = 0;
                    dtRShopD.Rows[e.RowIndex]["TaxPriceB"] = 0;
                    dtRShopD.Rows[e.RowIndex]["MnyB"] = 0;
                    dtRShopD.Rows[e.RowIndex]["StNo"] = StNo.Text;
                    dtRShopD.Rows[e.RowIndex]["StName"] = StName.Text;
                    dtRShopD.Rows[e.RowIndex]["mwidth1"] = 0;
                    dtRShopD.Rows[e.RowIndex]["mwidth2"] = 0;
                    dtRShopD.Rows[e.RowIndex]["mwidth3"] = 0;
                    dtRShopD.Rows[e.RowIndex]["mwidth4"] = 0;
                    dtRShopD.Rows[e.RowIndex]["Pformula"] = "";
                    //折數
                    dtRShopD.Rows[e.RowIndex]["Prs"] = "1.00";

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = dtRShopD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jRShop.RemoveBom(rec, ref dtRShopBom);

                    //刪除批次異動資訊
                    BatchF.刪除批次異動(dt_BatchProcess, rec);
                    return;
                }
                //值沒變->離開
                if (ItNoNow == ItNoBegin)
                    return;

                if (dataGridViewT1["採購憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    dtRShopD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                //值有變，但是跟自定編號一樣，視同沒變->離開                //把『自定編號』改成『產品編號』
                if (ItNoNow != ItNoBegin)
                {
                    if (ItNoNow == UdfNoBegin)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = ItNoBegin;

                        dtRShopD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                //值變了，跟產品編號和自定編號都不一樣,帶值出來                //若找不到這筆資料則開窗
                if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)
                {
                    jRShop.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtRShopD, row => FillItem(row, e.RowIndex));
                }
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                string itno = dtRShopD.Rows[e.RowIndex]["itno"].ToString();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString();

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

                        dtRShopD.Rows[e.RowIndex]["itunit"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    return;
                }

                jRShop.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        dtRShopD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        dtRShopD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                dtRShopD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    GetSystemPrice(dtRShopD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtRShopD.Rows[e.RowIndex]);
                    SetRow_Mny(dtRShopD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
                #endregion
            }
            else if (CurrentColumnName == "退貨數量")
            {
                #region 退貨數量
                var qty = dataGridViewT1["退貨數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                if (Common.Sys_DBqty == 1)
                {
                    dtRShopD.Rows[e.RowIndex]["Qty"] = qty;
                    dtRShopD.Rows[e.RowIndex]["PQty"] = qty;
                }
                else if (Common.Sys_DBqty == 2)
                {
                    dtRShopD.Rows[e.RowIndex]["Qty"] = qty;
                }

                SetRow_Mny(dtRShopD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "計價數量")
            {
                #region 計價數量
                dtRShopD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                SetRow_Mny(dtRShopD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "進價")
            {
                #region 進價
                dtRShopD.Rows[e.RowIndex]["Price"] = dataGridViewT1["進價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MF);

                SetRow_TaxPrice(dtRShopD.Rows[e.RowIndex]);
                SetRow_Mny(dtRShopD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "折數")
            {
                #region 折數
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                dtRShopD.Rows[e.RowIndex]["Prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");

                SetRow_TaxPrice(dtRShopD.Rows[e.RowIndex]);
                SetRow_Mny(dtRShopD.Rows[e.RowIndex]);
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
                if (TextBefore.ToDecimal() == mny) return;

                dtRShopD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MF);
                        dtRShopD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MF);
                        dtRShopD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(dtRShopD.Rows[e.RowIndex]);

                taxprice = dtRShopD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                dtRShopD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                dtRShopD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                dtRShopD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "退貨倉庫")
            {
                #region 退貨倉庫
                if (dataGridViewT1.Columns["退貨倉庫"].ReadOnly)
                    return;

                jRShop.DataGridViewValidateOpen<JBS.JS.Stkroom>(sender, e, dtRShopD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["StNo"].ToString();

                    dtRShopD.Rows[e.RowIndex]["StNo"] = row["StNo"].ToString();
                    dtRShopD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtRShopD, e.RowIndex, row["StNo"].ToString(), row["StName"].ToString());
                });
                #endregion
            }
            else if (CurrentColumnName == "包裝數量")
            {
                #region 包裝數量
                if (dataGridViewT1["採購憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    decimal num = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                    if (num != TextBefore.ToDecimal())
                    {
                        MessageBox.Show("此筆資料由採購轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtRShopD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                    }
                }
                else
                {
                    dtRShopD.Rows[e.RowIndex]["itpkgqty"] = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
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
            dataGridViewT1.Columns["採購憑證"].ReadOnly = true;
            dataGridViewT1.Columns["稅前進價"].ReadOnly = true;
            dataGridViewT1.Columns["產品組成"].ReadOnly = true;
            dataGridViewT1.Columns["倉庫名稱"].ReadOnly = true;
            dataGridViewT1.Columns["本幣單價"].ReadOnly = true;
            dataGridViewT1.Columns["本幣稅前單價"].ReadOnly = true;
            dataGridViewT1.Columns["本幣稅前金額"].ReadOnly = true;
            //折數設定
            if (Common.Sys_KeyPrs == 1)
                dataGridViewT1.Columns["折數"].ReadOnly = false;
            else
                dataGridViewT1.Columns["折數"].ReadOnly = true;

            //74.73版單倉
            if (Common.Series == "74")
            {
                dataGridViewT1.Columns["退貨倉庫"].ReadOnly = true;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = true;
            }
            else if (Common.Series == "73")
            {
                dataGridViewT1.Columns["退貨倉庫"].ReadOnly = true;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = true;
            }
            else if (Common.Series == "72")
            {
                dataGridViewT1.Columns["退貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = true;
            }
            else if (Common.Series == "71")
            {
                dataGridViewT1.Columns["退貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["採購憑證"].ReadOnly = true;
            }
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
                string str = "產品編號";
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
                //刪除明細前，先刪除明細的『組件明細』
                var rec = dataGridViewT1.CurrentRow.Cells["結構編號"].Value.ToString();
                jRShop.RemoveBom(rec, ref dtRShopBom);

                //刪除明細
                int index = dataGridViewT1.SelectedRows[0].Index;
                dtRShopD.Rows.RemoveAt(index);
                dtRShopD.AcceptChanges();

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                }
                SetAllMny();//刪除列後，重新計算帳款

                //刪除批次異動資訊
                BatchF.刪除批次異動(dt_BatchProcess, rec);

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    string str = (Common.Series == "74") ? "產品編號" : "產品編號";
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
                    string str = "產品編號";
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells[str];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridItDesp.Focus();

                if (dataGridViewT1["產品編號", dataGridViewT1.CurrentRow.Index].EditedFormattedValue.ToString().Trim() == "")
                {
                    dataGridViewT1.Focus();
                    return;
                }

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }
                using (var frm = new FrmDesp(true, FormStyle.Mini))
                {
                    frm.dr = dtRShopD.Rows[index];
                    frm.ShowDialog();
                }
                dataGridViewT1.Focus();
            }
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
                    DataTable table = dtRShopBom.Clone();

                    for (int i = 0; i < dtRShopBom.Rows.Count; i++)
                    {
                        if (dtRShopBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                        {
                            table.ImportRow(dtRShopBom.Rows[i]);
                            dtRShopBom.Rows.RemoveAt(i--);
                        }
                    }

                    table.AcceptChanges();
                    dtRShopBom.AcceptChanges();
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
                                dtRShopBom.Merge(frm.dtD);
                                dtRShopD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MF);
                                dataGridViewT1.Focus();
                                SetRow_TaxPrice(dtRShopD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetRow_Mny(dtRShopD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetAllMny();
                                break;
                            }
                            else
                            {
                                dtRShopBom.Merge(frm.dtD);
                                dtRShopBom.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            dtRShopBom.Merge(table);
                            dtRShopBom.AcceptChanges();
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
                frm.ItNo = dtRShopD.Rows[dataGridViewT1.CurrentRow.Index]["ItNo"].ToString();
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
            var itno = dtRShopD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該廠商此產品交易 frm = new S2.Frm該廠商此產品交易())
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
            var itno = dtRShopD.Rows[index]["itno"].ToString().Trim();
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
            var itno = dtRShopD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm售價查詢 frm = new S2.Frm售價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void Transport_Validating(object sender, CancelEventArgs e)
        {
            decimal fee = 0, par = 0;
            decimal.TryParse(Transport.Text, out fee);
            decimal.TryParse(TranPar.Text, out par);
            TransportB.Text = (fee * par).ToString("f" + TransportB.LastNum);
            FeeSummary();
        }

        private void Premium_Validating(object sender, CancelEventArgs e)
        {
            decimal fee = 0, par = 0;
            decimal.TryParse(Premium.Text, out fee);
            decimal.TryParse(PremPar.Text, out par);
            PremiumB.Text = (fee * par).ToString("f" + PremiumB.LastNum);
            FeeSummary();
        }

        private void Tariff_Validating(object sender, CancelEventArgs e)
        {
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
            //FeeShare();
        }

        void FeeShare()
        {
            if (dataGridViewT1.ReadOnly) return;

            if (BsShare1.Checked)
            {
                for (int i = 0; i < dtRShopD.Rows.Count; i++)
                {
                    dtRShopD.Rows[i]["realcost"] = dtRShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
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
                    for (int i = 0; i < dtRShopD.Rows.Count; i++)
                    {
                        if (!dtRShopD.Rows[i]["qty"].IsNullOrEmpty())
                        {
                            qty = dtRShopD.Rows[i]["qty"].ToDecimal();
                            totqty += qty;
                        }
                    }
                    //攤費：費用/數量
                    decimal.TryParse(TotFee.Text, out totfee);
                    if (totqty == 0)
                    {
                        fee = "0";
                    }
                    else
                    {
                        fee = (totfee / totqty).ToString("f" + Common.M);
                    }

                    for (int i = 0; i < dtRShopD.Rows.Count; i++)
                    {
                        if (dtRShopD.Rows[i]["taxprice"].IsNullOrEmpty())
                        {
                            dtRShopD.Rows[i]["taxprice"] = 0;
                            dtRShopD.Rows[i]["realcost"] = fee;
                        }
                        else
                        {
                            taxprice = dtRShopD.Rows[i]["taxprice"].ToDecimal();
                            decimal tempfee = 0;
                            decimal.TryParse(fee, out tempfee);
                            dtRShopD.Rows[i]["realcost"] = (taxprice + tempfee).ToString("f" + Common.M);
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

                    for (int i = 0; i < dtRShopD.Rows.Count; i++)
                    {
                        if (dtRShopD.Rows[i]["taxprice"].IsNullOrEmpty())
                        {
                            dtRShopD.Rows[i]["taxprice"] = 0;
                            dtRShopD.Rows[i]["realcost"] = 0;
                        }
                        else
                        {
                            if (taxmnyb == 0)
                            {
                                dtRShopD.Rows[i]["realcost"] = dtRShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
                            }
                            else
                            {
                                taxprice = dtRShopD.Rows[i]["taxprice"].ToDecimal();
                                dtRShopD.Rows[i]["realcost"] = (taxprice + (totfee * (taxprice / taxmnyb))).ToDecimal("f" + Common.M);
                            }
                        }
                        dataGridViewT1.InvalidateRow(i);
                    }
                }
            }
        }

        //void BsShare_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (BsShare1.Checked)
        //    {
        //        BsShsel1.Enabled = false;
        //        BsShsel2.Enabled = false;
        //        for (int i = 0; i < dtRShopD.Rows.Count; i++)
        //        {
        //            dtRShopD.Rows[i]["realcost"] = dtRShopD.Rows[i]["taxprice"].ToDecimal("f" + Common.M);
        //            dataGridViewT1.InvalidateRow(i);
        //        }
        //    }
        //    else
        //    {
        //        BsShsel1.Enabled = true;
        //        BsShsel2.Enabled = true;
        //        FeeShare();
        //    }
        //}

        //void BsShsel_CheckedChanged(object sender, EventArgs e)
        //{
        //    FeeShare();
        //}

        private void gridAddress_Click(object sender, EventArgs e)
        {
            jRShop.Open<JBS.JS.Fact>(FaNo, row =>
            {
                BsMemo.Text = row["FaNo"].ToString() + " ";
                BsMemo.Text += row["FaName1"].ToString() + " ";
                BsMemo.Text += row["FaAddr3"].ToString();
            });
        }

        private void gridKeyMan_Click(object sender, EventArgs e)
        {
            if (BsNo.Text.Trim() == "")
                return;

            using (var frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jRShop.keyMan.AppendMan;
                frm.ATime = jRShop.keyMan.AppendTime;
                //修改人員
                frm.EName = jRShop.keyMan.EditMan;
                frm.ETime = jRShop.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void gridInvNo_Click(object sender, EventArgs e)
        {
            var bsno = BsNo.Text.Trim();
            var IsPOS = false;
            using (FrmBShop_Inv frm = new FrmBShop_Inv("RShop", bsno, IsPOS))
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

        private void FromBshop_DoubleClick(object sender, EventArgs e)
        {
            if (FromBShop.ReadOnly)
                return;

            FromBShop.CausesValidation = false;

            using (var frm = new FrmBShopToRShop())
            { 
                frm.FaNo = FaNo.Text.Trim();
                frm.SeekNo = FromBShop.Text.Trim();

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                jRShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRShopD, ref dtRShopBom, SetAllMny);

                this.PassFQuotM(frm);

                this.PassFQuotD(frm);

                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                    dataGridViewT1.Focus();
                }
            }
            FromBShop.Tag = FromBShop.Text.Trim();
            SetAllMny();

            FromBShop.CausesValidation = true;
        }
        private void FromBshop_Validating(object sender, CancelEventArgs e)
        {
            if (FromBShop.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FromBShop.TrimTextLenth() == 0)
            {
                FromBShop.Tag = "";
                return;
            }

            if (FromBShop.Tag == null)
            {
                FromBShop.Tag = "";
            }

            if (jRShop.IsExistDocument<JBS.JS.BShop>(FromBShop.Text.Trim()) == true)
            {
                if (FromBShop.Tag.ToString() == FromBShop.Text.Trim())
                    return;

                using (var frm = new FrmBShopToRShop())
                { 
                    frm.FaNo = FaNo.Text.Trim();
                    frm.SeekNo = FromBShop.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jRShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRShopD, ref dtRShopBom, SetAllMny);

                    this.PassFQuotM(frm);

                    this.PassFQuotD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                FromBShop.Tag = FromBShop.Text.Trim();
                SetAllMny();
            }
            else
            {
                e.Cancel = true;
                using (var frm = new FrmBShopToRShop())
                { 
                    frm.FaNo = FaNo.Text.Trim();
                    frm.SeekNo = FromBShop.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jRShop.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRShopD, ref dtRShopBom, SetAllMny);

                    this.PassFQuotM(frm);

                    this.PassFQuotD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                FromBShop.Tag = FromBShop.Text.Trim();
                SetAllMny();
            }
        }
        private void PassFQuotM(FrmBShopToRShop frm)
        {
            FromBShop.Text = frm.BsNo;
            Memo1 = frm.MasterRow["bsmemo1"].ToString();
            Xa1No.Text = frm.MasterRow["Xa1no"].ToString();
            Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
            Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
            X3No.Text = frm.MasterRow["X3no"].ToString();
            X3No1.Text = frm.MasterRow["X3no"].ToString();
            Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            Rate1.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            EmNo.Text = frm.MasterRow["EmNo"].ToString();
            EmName.Text = frm.MasterRow["EmName"].ToString();
            getDeNo();
            BsMemo.Text = frm.MasterRow["BsMemo"].ToString();
            X4No.Text = frm.MasterRow["X4No"].ToString();
            X4Name.Text = frm.MasterRow["X4Name"].ToString();
            SpNo.Text = frm.MasterRow["SpNo"].ToString();
            SpName.Text = frm.MasterRow["SpName"].ToString();
            SeNo.Text = frm.MasterRow["SeNo"].ToString();
            SeName.Text = frm.MasterRow["SeName"].ToString();
            InvName.Text = frm.MasterRow["InvName"].ToString();
            InvTaxNo.Text = frm.MasterRow["InvTaxNo"].ToString();
            InvNo.Text = frm.MasterRow["InvNo"].ToString();
            InvDate = frm.MasterRow["InvDate"].ToString();
            InvAddr1 = frm.MasterRow["InvAddr1"].ToString();
            PhotoPath = frm.MasterRow["PhotoPath"].ToString();
            X5No.Text = frm.MasterRow["X5no"].ToString();
            pVar.XX05Validate(X5No.Text, X5No, X5Name);
            pVar.XX03Validate(X3No.Text, X3No, X3Name);
            pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);
            this.Memo1 = frm.MasterRow["bsmemo1"].ToString();
            EInvChange.Text = frm.MasterRow["EInvChange"].ToString();
            invrandom.Text = frm.MasterRow["invrandom"].ToString();
            EInvState.Text = frm.MasterRow["EInvState"].ToString();
            if (FaNo.Text.Trim() == "")
            {
                FaNo.Text = frm.MasterRow["FaNo"].ToString();
                FaName11.Text = frm.MasterRow["FaName1"].ToString();
                FaPer11.Text = frm.MasterRow["FaPer1"].ToString().GetUTF8(10);
                FaTel11.Text = frm.MasterRow["FaTel1"].ToString();
                
                jRShop.Validate<JBS.JS.Fact>(FaNo.Text, row =>
                {
                    FaPayAmt.Text = row["FaPayAmt"].ToDecimal().ToString("f" + FaPayAmt.LastNum);
                });
            }
        }
        private void PassFQuotD(FrmBShopToRShop frm)
        {
            DataRow row = null;
            DataTable dtD = frm.dtDetail;

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                var rec = GetBomRec();
                row = dtRShopD.NewRow();

                row["itno"] = dtD.Rows[i]["itno"];
                row["ItNoUdf"] = dtD.Rows[i]["ItNoUdf"];
                row["itname"] = dtD.Rows[i]["itname"];
                row["stno"] = dtD.Rows[i]["stno"];
                row["stname"] = dtD.Rows[i]["stname"];
                row["ittrait"] = dtD.Rows[i]["ittrait"];
                if (dtD.Rows[i]["ittrait"].ToString() == "1") row["產品組成"] = "組合品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "2") row["產品組成"] = "組裝品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "3") row["產品組成"] = "單一商品";
                row["itunit"] = dtD.Rows[i]["itunit"];
                row["punit"] = dtD.Rows[i]["punit"];
                row["qty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["pqty"] = dtD.Rows[i]["pqty"].ToDecimal("f" + Common.Q);
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
                row["foid"] = dtD.Rows[i]["foid"];
                row["fono"] = dtD.Rows[i]["fono"];
                row["mwidth1"] = dtD.Rows[i]["mwidth1"];
                row["mwidth2"] = dtD.Rows[i]["mwidth2"];
                row["mwidth3"] = dtD.Rows[i]["mwidth3"];
                row["mwidth4"] = dtD.Rows[i]["mwidth4"];
                row["pformula"] = dtD.Rows[i]["pformula"];
                for (int j = 1; j < 11; j++)
                {
                    row["itdesp" + j] = dtD.Rows[i]["itdesp" + j];
                }
                dtRShopD.Rows.Add(row);
                dtRShopD.AcceptChanges();
                dataGridViewT1.InvalidateRow(dtRShopD.Rows.Count - 1);

                if (dtD.Rows[i]["ittrait"].ToString().Trim() == "3")
                    continue;

                var bsbomid = dtD.Rows[i]["bomid"].ToString().Trim();
                jRShop.GetTBom<JBS.JS.BShop>(bsbomid, rec.ToString(), ref dtRShopBom);

            }
        }
        void getDeNo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    cmd.CommandText = "select d.deno,d.dename1 from empl as e left join dept as d on e.emdeno=d.deno where e.emno=@emno";
                    SqlDataReader reader = cmd.ExecuteReader();
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
                    reader.Dispose(); reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            using (var frm = new FrmSale_Memo())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        BsMemo.Text = frm.Memo.GetUTF8(60);
                        break;
                }
                BsMemo.SelectAll();
            }
        }

        ToolTip tip = new ToolTip();
        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "退貨倉庫" || str == "備註說明")
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
            else if (str == "退貨數量")
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

        private void DeNo_DoubleClick(object sender, EventArgs e)
        {
            if (DeNo.ReadOnly)
                return;

            jRShop.Open<JBS.JS.Dept>(sender, row =>
            {
                DeNo.Text = row["deno"].ToString();
                DeName.Text = row["dename1"].ToString();
            });
        }

        private void DeNo_Validating(object sender, CancelEventArgs e)
        {
            if (DeNo.ReadOnly || btnCancel.Focused) return;

            if (DeNo.TrimTextLenth() == 0)
            {
                DeNo.Clear();
                DeName.Clear();
                return;
            }

            jRShop.ValidateOpen<JBS.JS.Dept>(sender, e, row =>
            {
                DeNo.Text = row["deno"].ToString();
                DeName.Text = row["dename1"].ToString();
            });
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (var frm = new S1.Frm詳細備註())
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

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "退貨數量")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim());
                        cmd.CommandText = "select itstockqty from item where itno=@itno";
                        if (!cmd.ExecuteScalar().IsNullOrEmpty())
                            toolStripStatusLabel1.Text = "現有庫存量：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);
                    }
                }
            }
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim());
                        cmd.CommandText = "select itunit,itunitp,itpkgqty from item where itno=@itno";
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
        }

        private void dataGridViewT1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "退貨數量" || dataGridViewT1.Columns[e.ColumnIndex].Name == "進價" || dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
                toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.瀏覽 0.結束";
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            pnlBsShare.Enabled = pnlBsShsel.Enabled = !btnAppend.Enabled;
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
            var itno = (index == -1) ? "" : dtRShopD.Rows[index]["itno"].ToString().Trim();
            using (var frm = new S2.Frm該廠商歷史交易())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
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
                    string sqlcommand = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號 , * from AffixFile where (DaType ='進退單' and Datano=@Datano) or (Datano=@Datano2 and DaType='進貨單')";
                    //撈出訂單關聯的報價單
                    string sqlcommand_ = "SELECT distinct(FQuot.fqno) FROM FQuot INNER JOIN ford on FQuot.fqno = ford.fqno  where 1=0  ";
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Datano", BsNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Datano2", FromBShop.Text.Trim());
                   
                    if (dtRShopD.Rows.Count > 0)
                    {
                        List<string> DistinctList = new List<string>();
                        for (int i = 0; i < dtRShopD.Rows.Count; i++)
                        {
                            if (DistinctList.IndexOf(dtRShopD.Rows[i]["fono"].ToString()) == -1)
                            {
                                var Datano = "Datano3" + i.ToString();
                                cmd.Parameters.AddWithValue(Datano, dtRShopD.Rows[i]["fono"].ToString());
                                DistinctList.Add(dtRShopD.Rows[i]["fono"].ToString());
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
                
                    frm.DtFile = DaFile;
                    frm.CMD = cmd;
                    frm.Datano = BsNo.Text.Trim();
                    frm.DaType = "進退單";
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

        private void gridInvMode_Click(object sender, EventArgs e)
        {
            //進項批開
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
            BatchF.WhenGridBadch_click(this.Name, dataGridViewT1, dtRShopD, dt_BatchProcess, FaNo, FaName11, null, null, null, false, btnSave.Enabled == true);
        }



    }
}