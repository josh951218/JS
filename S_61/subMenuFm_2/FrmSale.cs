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
    public partial class FrmSale : Formbase
    {
        JBS.JS.Sale jSale;
        JBS.JS.xEvents xe = new JBS.JS.xEvents();
        JE.MyControl.TextBoxT LeNo = new JE.MyControl.TextBoxT();
        BatchFunction batch = new BatchFunction();
        DataTable dtSaleD = new DataTable();  //明細檔
        DataTable tempSaleD = new DataTable();//明細暫存檔

        DataTable dtSaleBom = new DataTable(); //組件檔
        DataTable tempBom = new DataTable();   //所有組件暫存檔

        #region 批次資料
        BatchProcess BatchF = new BatchProcess();           //批次資料異動修改
        BatchSave BatchSave = new BatchSave();              //批次資料存檔用
        DataTable dt_BatchProcess = new DataTable();        //批次異動
        DataTable dt_TempBatchProcess = new DataTable();    //批次異動暫存檔

        DataTable dt_Bom_BatchProcess = new DataTable();       //bom批次異動
        DataTable dt_Bom_TempBatchProcess = new DataTable();   //bom批次異動暫存檔
        #endregion

        DataTable lendtemp = new DataTable();
        DataTable lend = new DataTable();
        DataTable dtstandard = new DataTable();//型號
        DataTable DaFile = new DataTable();//附件檔案
        DataTable Einvdt = new DataTable();

        string TextBefore;
        string ItNoBegin;
        string UdfNoBegin;
        string Memo1 = "";
        string offline = "";
        string online = "";
        string die = "";
        decimal Disc = 1;
        decimal BomRec = 0;
        string tempinvrandom;

        string OldCust = "";//修改前->客戶編號
        string Oldpayerno = "";//修改前->請款客戶編號
        string CuPer1 = "", CuTel1 = "", CuAddr = "", AdName = "";//AdName = 指送公司名稱        //13.6a
        string PhotoPath = "";// 13.7c  

        List<Control> Writes;
        List<Button> AllGridButtons;
        List<TextBoxNumberT> NumsInit_Zero;
        List<TextBoxbase> list;

        //發票明細
        string InvDate = "";
        string InvAddr1 = "";
        bool 此銷貨單有寄庫 = false;

        //媒體申報
        string invkind = "";
        string specialtax = "";
        string passmode = "";

        string tempinvno = "";

        public FrmSale()
        {
            InitializeComponent();
            this.jSale = new JBS.JS.Sale();
            this.list = this.getEnumMember();
            this.dataGridViewT1.tableName = "Saled";

            SaDate.SetDateLength();
            SaDateAc.SetDateLength();
            pVar.SetMemoUdf(this.備註說明);

            Writes = new List<Control> { 
                SaNo, SaDate, SaDateAc, QuNo, CuNo, CuName11, AdPer11 , AdTel1 ,AdAddr1 , StNo, StName, Xa1No, Xa1Name, Xa1Par, Bracket, 
                TaxMnyB, TaxMny, X3No, Rate, Tax, TotMny, Discount, CollectMny, GetPrvAcc, AcctMny, X4No, X4Name, EmNo, EmName, SeNo, SeName, SpNo, SpName, SaMemo, 
                TaxMny1, X3No1, Rate1, Tax1, TotMny1, X5No, InvNo, Discount1, CashMny, CardMny, CardNo, InvName, Ticket, CollectMny1, GetPrvAcc1, AcctMny1, InvTaxNo,DeNo,DeName,payerno
            };

            AllGridButtons = new List<Button> { gridAppend, gridDelete, gridPicture, gridInsert, gridItDesp, gridBomD, gridAddress, gridInvNo, gridInvMode, gridAutoInv };

            NumsInit_Zero = new List<TextBoxNumberT> { TaxMnyB, TaxMny, Tax, TotMny, Discount, CollectMny, GetPrvAcc, AcctMny, TaxMny1, Tax1, TotMny1, Discount1, CashMny, CardMny, Ticket, CollectMny1, GetPrvAcc1, AcctMny1 };

            gridAutoInv.Text = Common.User_ScInvSlt == 1 ? "手動" : "自動";

            //小數位數
            foreach (TextBoxNumberT item in NumsInit_Zero)
            {
                item.Set銷貨單據小數();
            }
            Tax.Set銷項稅額小數();
            Tax1.Set銷項稅額小數();
            X3No.FirstNum = X3No1.FirstNum = 1;
            X3No.LastNum = X3No1.LastNum = 0;
            Rate.FirstNum = Rate1.FirstNum = 1;
            Rate.LastNum = Rate1.LastNum = 0;
            TaxMnyB.FirstNum = 20;
            TaxMnyB.LastNum = Common.M;
            CuAdvamt.FirstNum = 12;
            CuAdvamt.LastNum = Common.MST;
            Xa1Par.LastNum = 4;
            Xa1Par.FirstNum = 4;

            this.計價數量.Set庫存數量小數();
            this.銷貨數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.售價.Set銷貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前售價.FirstNum = 9;
            this.稅前售價.LastNum = 6;
            this.稅前售價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set銷項金額小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前單價.FirstNum = 9;
            this.本幣稅前單價.LastNum = 6;
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";
            this.本幣稅前金額.Set本幣金額小數();
            if (!dtSaleD.Columns.Contains("stnoi")) dtSaleD.Columns.Add("stnoi", typeof(String));
            if (!dtSaleD.Columns.Contains("stnamei")) dtSaleD.Columns.Add("stnamei", typeof(String));

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }
            if (Common.Sys_UsingBatch == 1)
                this.gridbatch.Visible = false;

            if (Common.Series == "74")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                AdTel1.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
                this.訂單憑證.Visible = false;
                btnLetoSale.Visible = false;
            }
            else if (Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                this.訂單憑證.Visible = true;
                btnLetoSale.Visible = false;
            }
            else if (Common.Series == "72")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
                StNo.Enabled = true;
                StName.Enabled = true;
                AdTel1.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
                this.訂單憑證.Visible = false;
            }
            else if (Common.Series == "71")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
                StNo.Enabled = true;
                StName.Enabled = true;
                this.訂單憑證.Visible = true;
            }
            dataGridViewT1.DataSource = dtSaleD;

            //金額權限
            TaxMnyB.Visible = Common.User_SalePrice;
            TaxMny.Visible = Common.User_SalePrice;
            Tax.Visible = Common.User_SalePrice;
            TotMny.Visible = Common.User_SalePrice;
            Discount.Visible = Common.User_SalePrice;
            CollectMny.Visible = Common.User_SalePrice;
            GetPrvAcc.Visible = Common.User_SalePrice;
            AcctMny.Visible = Common.User_SalePrice;
            TaxMny1.Visible = Common.User_SalePrice;
            Tax1.Visible = Common.User_SalePrice;
            TotMny1.Visible = Common.User_SalePrice;
            Discount1.Visible = Common.User_SalePrice;
            CashMny.Visible = Common.User_SalePrice;
            CardMny.Visible = Common.User_SalePrice;
            Ticket.Visible = Common.User_SalePrice;
            CollectMny1.Visible = Common.User_SalePrice;
            GetPrvAcc1.Visible = Common.User_SalePrice;
            AcctMny1.Visible = Common.User_SalePrice;
            X3No.Visible = Common.User_SalePrice;
            X3Name.Visible = Common.User_SalePrice;
            Rate.Visible = Common.User_SalePrice;
            X3No1.Visible = Common.User_SalePrice;
            X3Name1.Visible = Common.User_SalePrice;
            Rate1.Visible = Common.User_SalePrice;
            X5No.Visible = Common.User_SalePrice;
            X5Name.Visible = Common.User_SalePrice;
            InvNo.Visible = Common.User_SalePrice;
            CardNo.Visible = Common.User_SalePrice;
            gridInvNo.Visible = Common.User_SalePrice;
            gridInvMode.Visible = Common.User_SalePrice;
            gridAutoInv.Visible = Common.User_SalePrice;
            this.售價.Visible = Common.User_SalePrice;
            this.稅前售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.本幣單價.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmSale_Load(object sender, EventArgs e)
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
                        end,ItNoUdf='',*
                        from SaleD where 1=0 ";

                    da.Fill(dtSaleD);
                    da.Fill(tempSaleD);
                }
            };
            InitdtD.Invoke();


            #region 批次初始
            BatchF.建立結構(dt_BatchProcess);
            dt_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_TempBatchProcess = dt_BatchProcess.Clone();
            dataGridView1.DataSource = dt_BatchProcess;
            dataGridView2.DataSource = dt_Bom_BatchProcess;
            #endregion 
            var pk = jSale.Bottom();
            writeToTxt(pk);
 
        }

        private void FrmSale_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string sano)
        {
            labelT1.Text = "送貨地址";
            var result = jSale.LoadData(sano, reader =>
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

                //使用電子發票
                if (reader["einv"].ToString() == "1")
                    EInv1.Checked = true;
                else
                    EInv2.Checked = true;

                EInvChange.Text = reader["einvchange"].ToString();
                EInvState.Text = reader["einvstate"].ToString();
                User_Einv.Text = reader["User_Einv"].ToString();
                iTitle.Text = reader["iTitle"].ToString();

                //填入發票日期與地址
                var date = (Common.User_DateTime == 1) ? "" : "1";
                SaDate.Text = reader["SaDate" + date].ToString();
                SaDateAc.Text = reader["SaDateAc" + date].ToString();
                InvDate = reader["InvDate" + date].ToString();
                InvAddr1 = reader["InvAddr1"].ToString();
                tempinvrandom = reader["invrandom"].ToString();
                AdPer11.Text = AdPer11.Text.GetUTF8(10);
                DeNo.Text = reader["DeNo"].ToString();
                DeName.Text = reader["DeName"].ToString();
                SaPayment.Text = reader["SaPayment"].ToString();
                PhotoPath = reader["PhotoPath"].ToString();//13.7c
                //載入稅別名稱與發票名稱
                jSale.Validate<JBS.JS.XX03>(X3No.Text, r =>
                {
                    X3Name.Text = r["x3name"].ToString();
                    X3Name1.Text = r["x3name"].ToString();
                }, () =>
                {
                    X3Name.Clear();
                    X3Name1.Clear();
                });
                Rate.Text = (reader["Rate"].ToDecimal() * 100).ToString("f0");
                Rate1.Text = (reader["Rate"].ToDecimal() * 100).ToString("f0");
                jSale.Validate<JBS.JS.XX05>(X5No.Text, r => X5Name.Text = r["x5name"].ToString(), () => X5Name.Clear());
                jSale.Validate<JBS.JS.Cust>(payerno.Text, r => payername.Text = r["cuname1"].ToString(), () => payername.Clear());
                gridInvMode.Text = reader["invbatch"].ToInteger() == 1 ? "批開" : "";
                
                //由於發票許用方式沒有紀錄資料庫，所以顯示時可能為之前的取用方式，非此張銷貨單之取用方式，故隱藏不顯示，以免造成使用者誤會
                gridAutoInv.Visible = false;


                //媒體申報
                invkind = reader["invkind"].ToString();
                specialtax = reader["specialtax"].ToString();
                passmode = reader["passmode"].ToString();

                //載入明細與暫存檔
                loadSaleD();
                loadAdvAmt();

                this.OldCust = CuNo.Text.Trim();
                this.Oldpayerno = payerno.Text.Trim();
                this.Memo1 = reader["samemo1"].ToString();
                this.offline = reader["offline"].ToString().Trim();
                this.online = reader["online"].ToString().Trim();
                this.die = reader["die"].ToString().Trim();
                jSale.keyMan.Set(reader);
            });

            if (result == false)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtSaleD.Clear();
                tempSaleD.Clear();
                dtSaleBom.Clear();
                tempBom.Clear();

                this.OldCust = "";
                this.Oldpayerno = "";
                this.Memo1 = "";
                this.offline = "";
                this.online = "";
                this.die = "";
                jSale.keyMan.Clear();
            }
            #region  帶入公司名稱變數值
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                AdName = "";
                try
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    cmd.CommandText = "select ISNULL(name, '') as 公司名稱  from DeliveryAddress where cuno=@cuno and  DefaultPrint='V'";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            AdName = dr["公司名稱"].ToString();       //指送公司名稱
                        }
                    }
                    if (AdName == "")
                    {
                        cmd.CommandText = "select cuname2 from cust where cuno = @cuno ";
                        object 公司名稱 = cmd.ExecuteScalar();
                        if (公司名稱 != null)
                            AdName = 公司名稱.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
            }
            #endregion
            BatchF.上下頁dt資料修改("Saled", sano, dt_BatchProcess, dt_TempBatchProcess);
            BatchF.BOM上下頁dt資料修改("Saled", "SaleBom", sano, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
        }

        void loadSaleD()
        {
            dtSaleD.Clear();
            tempSaleD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("SaNo", SaNo.Text.Trim());
                    cmd.CommandText = "select 產品組成="
                                   + " case"
                                   + " when ittrait=1 then '組合品'"
                                   + " when ittrait=2 then '組裝品'"
                                   + " when ittrait=3 then '單一商品'"
                                   + " end,ItNoUdf= (select top 1 itnoudf from item where item.itno = SaleD.itno),*"
                                   + " from SaleD where SaNo=(@SaNo) ORDER BY recordno ";

                    da.Fill(dtSaleD);
                    da.Fill(tempSaleD);
                }
                dataGridViewT1.DataSource = dtSaleD;
                if (dtSaleD.Rows.Count > 0) BomRec = dtSaleD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadSaleBom()
        {
            dtSaleBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select top 1 * from SaleBom where 1=0";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from SaleBom where SaNo=@SaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from SaleBom where SaNo=@SaNo COLLATE Chinese_Taiwan_Stroke_BIN";

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SaNo", jSale.GetCurrent());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtSaleBom);
                            da.Fill(tempBom);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadAdvAmt()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    //載入客戶預收餘額
                    if (conn.State != ConnectionState.Open) conn.Open();
                    var sql = "select CuAdvamt from Cust where CuNo=(@CuNo) ";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CuNo", CuNo.Text.Trim());
                        CuAdvamt.Text = cmd.ExecuteScalar().ToDecimal().ToString("f" + CuAdvamt.LastNum);
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
                return (d * p).ToString("f" + Common.TS);
            else if (tx.Name == TotMny.Name)//外幣應收轉本幣應收
                return (d * p).ToString("f" + Common.MST);
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
            var pk = jSale.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jSale.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jSale.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jSale.Bottom();
            writeToTxt(pk);
        }

        void ThisFormState()
        {
            #region//新增時,各種歸零,然後設定某些預設值
            if (this.FormState == FormEditState.Append)
            {
                this.LeNo.Clear();
                dtSaleD.Clear();
                loadSaleBom();

                this.Memo1 = "";
                this.offline = "";
                this.online = "";
                this.die = "";
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

                gridInvMode.Text = Common.User_ScInvBat == 1 ? "批開" : "";
                gridAutoInv.Text = Common.User_ScInvSlt == 1 ? "手動" : "自動";
                if (gridInvMode.Text == "批開")
                    gridAutoInv.Visible = false;
                else
                    gridAutoInv.Visible = true;

                SaDate.Text = Date.GetDateTime(Common.User_DateTime);
                SaDateAc.Text = Date.GetDateTime(Common.User_DateTime);

                //媒體申報
                invkind = "";
                specialtax = "";
                passmode = "";

                //電子發票
                EInv2.Checked = true;
                lblEInvChange.Visible = lblEInvState.Visible = EInvChange.Visible = EInvState.Visible = false;
                SaDate.Focus();

                此銷貨單有寄庫 = false;
            }
            #endregion

            #region//複製時,值不變  ,但要設定某些預設值
            if (this.FormState == FormEditState.Duplicate)
            {
                loadSaleBom();

                foreach (DataRow row in dtSaleD.Rows)
                {
                    row["orno"] = "";
                    row["leno"] = "";
                    row["leid"] = "";
                    row["NetNo"] = "";
                }

                SaNo.Clear();
                CardNo.Clear();
                Bracket.Clear();

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

                //媒體申報
                invkind = "";
                passmode = "";
                specialtax = "";

                //電子發票
                EInvState.Text = "未上傳";

                gridAutoInv.Text = Common.User_ScInvSlt == 1 ? "手動" : "自動";
                if (gridInvMode.Text == "批開")
                    gridAutoInv.Visible = false;
                else
                    gridAutoInv.Visible = true;

                SaDate.Text = Date.GetDateTime(Common.User_DateTime);
                SaDateAc.Text = Date.GetDateTime(Common.User_DateTime);

                SaDate.Focus();
                CuNo.SelectAll();

                此銷貨單有寄庫 = false;
            }
            #endregion

            #region//修改時,值不變
            if (this.FormState == FormEditState.Modify)
            {
                loadSaleBom();               
                SaNo.Focus();
                CuNo.SelectAll();
            }
            #endregion

            this.自定編號.ReadOnly = true;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            //SaPayment.Text = "";
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            ThisFormState();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
            btnLetoSale.Enabled = true;
            if (X5No.Text == "7" || X5No.Text == "8")
            {
                User_Einv.Text = Common.User_Einv;
                iTitle.Text = Common.iTitle;
            }
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            //SaPayment = "";
            if (SaNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jSale.IsModify<JBS.JS.Sale>(SaNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            ThisFormState();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (SaNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jSale.IsExistDocument<JBS.JS.Sale>(SaNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            
            if (Common.User_CanEditPOS == 2 && Bracket.Text.Trim() == "前台")
            {
                MessageBox.Show("前台收銀單據，無法異動！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jSale.IsSendToEINV(SaNo.Text) == false || jSale.IsPassToBatch(InvNo.Text) == true)//上傳 電子雲&發票 可改部分
            {
                payerno.ReadOnly = false;
                payername.ReadOnly = false;   
                X4No.ReadOnly = false;
                EmNo.ReadOnly = false;
                DeNo.ReadOnly = false;
                SeNo.ReadOnly = false;
                DeName.ReadOnly = false;
                SpNo.ReadOnly = false;
                SaDateAc.ReadOnly = false;
                dataGridViewT1.ReadOnly = true;
                for (int i = 0; i < panelT1.Controls.Count; i++)
                    panelT1.Controls[i].Enabled = false;
                btnCancel.Enabled = btnSave.Enabled = true;
                this.FormState = FormEditState.Modify;
                return;
            }

            if (jSale.IsEditInCloseDay(SaDate.Text) == false)//都不能改
                return;

            if (jSale.IsPassToAcc(SaNo.Text.Trim()) == true)//都不能改
                return;

            此銷貨單有寄庫 = false;
            if (jSale.IsPassToInStk(SaNo.Text.Trim(), false) == true)
                此銷貨單有寄庫 = true;

            if (jSale.IsPassToReceiv(SaNo.Text.Trim()) == true)
                return;

            if (jSale.IsPassToRLend(tempSaleD) == true)
                MessageBox.Show("此單據已有還入單！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (jSale.IsModify<JBS.JS.Sale>(SaNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jSale.upModify1<JBS.JS.Sale>(SaNo.Text.Trim());//更新修改狀態1
                var pk = jSale.Renew();
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
            if (SaNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jSale.IsExistDocument<JBS.JS.Sale>(SaNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被其他使用者刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (Common.User_CanEditPOS == 2 && Bracket.Text.Trim() == "前台")
            {
                MessageBox.Show("前台收銀單據，無法異動！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (EInvState.Text.Trim() == "已上傳")
            {
                MessageBox.Show("此發票已上傳平台，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jSale.IsModify<JBS.JS.Sale>(SaNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Bracket.Text.Trim() == "前台")
            {
                var dl = MessageBox.Show("前台收銀單據,請確定是否刪除?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dl != DialogResult.OK)
                    return;
            }
            
            //有折讓紀錄不可修改或刪除
            if (InvNo.Text.Trim() != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    try
                    {
                        cmd.CommandText = "select * from discountd where invno = '" + InvNo.Text.Trim() + "'";
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

            if (jSale.IsPassToBatch(InvNo.Text) == true)
                return;

            if (jSale.IsEditInCloseDay(SaDate.Text) == false)
                return;

            if (jSale.IsPassToAcc(SaNo.Text.Trim()) == true)
                return;

            if (jSale.IsPassToInStk(SaNo.Text.Trim()) == true)
                return;

            if (jSale.IsPassToReceiv(SaNo.Text.Trim()) == true)
                return;

            if (jSale.IsPassToRLend(tempSaleD) == true)
            {
                var dl = MessageBox.Show("此單據已有還出單,是否要刪除", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dl != DialogResult.OK)
                    return;
            }

            var reno = jSale.GetThisPassToReceiv(SaNo.Text.Trim());
            jSale.GetTempBomOnDeleting("SaleBom", SaNo.Text.Trim(), ref tempBom);

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
                    jSale.GetOldAcctMnyOnDeleting(SaNo.Text.Trim(), out acctmny, out getprvacc);

                    for (int i = 0; i < tempSaleD.Rows.Count; i++)
                    {
                        //若此筆明細為訂單轉入 加回此筆數量
                        if (tempSaleD.Rows[i]["orid"].ToString().Trim().Length > 0)
                        {
                            BackOrderdQty(cmd, i);
                        }
                        if (tempSaleD.Rows[i]["NetNo"].ToString().Trim().Length > 0)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("NetNo", tempSaleD.Rows[i]["NetNo"].ToString().Trim());
                            cmd.CommandText = @" update weborder SET orderState='1' WHERE orno = @NetNo";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("reno", reno);
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.CommandText = @"
                        Delete from receiv  where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from receivd where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from SaleBom where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from SaleD   where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN;
                        Delete from Sale    where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN;
                        ";
                    if (Bracket.Text.Trim() == "前台")
                        cmd.CommandText += "Delete from posinv where SaNo=(@sano);";

                    cmd.ExecuteNonQuery();

                    jSale.加庫存(cmd, tempSaleD, tempBom);

                    FrmAffixFile.FileDelete_單據刪除(cmd, SaNo.Text.Trim(), "銷貨單");
                    
                    BatchSave.進退_Delete(dt_TempBatchProcess, cmd, "Saled", SaNo.Text.Trim());
                    BatchSave.進退_Delete(dt_Bom_TempBatchProcess, cmd, "SaleBom", SaNo.Text.Trim());
                    tn.Commit();

                    jSale.BackOldCustReceiv(this.Oldpayerno.Trim(), acctmny, getprvacc);

                    jSale.UpdateItemItStockQty(tempSaleD, tempBom);

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
            if (SaNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmSale_Print())
            {
                frm.PK = SaNo.Text.Trim();
                frm.CuNo = CuNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (SaNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new S2.FrmSaleBrowNew())
            {
                frm.TSeekNo = SaNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            this.CuPer1 = AdPer11.Text.Trim();
            this.CuTel1 = AdTel1.Text.Trim();

            if (jSale.IsEditInCloseDay(SaDate.Text) == false)
                return;

            if (jSale.IsRegisted() == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！" + Environment.NewLine + "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (X5No.Text.ToDecimal() == 7 && EInvChange.SelectedIndex == 0 && InvTaxNo.TrimTextLenth() == 0)
            {
                InvTaxNo.Focus();
                MessageBox.Show("電子發票若要使用『交換發票』，必須輸入統編", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Focus();
                MessageBox.Show("倉庫編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CuNo.Text.Trim() == "")
            {
                CuNo.Focus();
                MessageBox.Show("客戶編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Writes.ForEach(r =>
                {
                    if (r is TextBoxNumberT && r.Text.Trim() == "") r.Text = "0";
                });
            }
            if (payerno.Text.Trim() == "")
            {
                payerno.Focus();
                MessageBox.Show("請款客戶編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var acctmny = 0M;
            var getprvacc = 0M;
            jSale.GetOldAcctMnyOnDeleting(SaNo.Text.Trim(), out acctmny, out getprvacc);

            if (jSale.IsOverCredit(CuNo.Text.Trim(), acctmny, AcctMny.Text.Trim().ToDecimal()) == false)
                return;

            //明細不能是空值
            jSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtSaleD, ref dtSaleBom, SetAllMny);
            //上一方法刪除空值後，刪除對應該筆空值之批號
            BatchF.刪除無明細對應之批號資料(dtSaleD, dt_BatchProcess);
            BatchF.刪除無明細對應之bom批號資料(dtSaleD, dt_Bom_BatchProcess);


            //先檢查明細[稅前金額]是否正確
            //因為朝聖發生 數量 * 稅前單價 != 稅前金額的狀況 E.G. 數量(1) * 稅前單價(50) = 稅前金額(0)
            //所已在bug沒找到之前，使用已下辦法檢查
            var qty = 0M;
            var taxprice = 0M;
            var mny = 0M;
            var difference = 0M;
            foreach (DataRow item in dtSaleD.Rows)
            {
                qty = item["Pqty"].ToDecimal("f" + Common.Q);
                taxprice = item["taxprice"].ToDecimal("f6");
                mny = (qty * taxprice).ToDecimal("f" + Common.TPS);
                difference = mny - item["mny"].ToDecimal();
                if (difference > 1 || difference < -1)
                {
                    MessageBox.Show("明細稅前金額有誤！無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (Common.TPS == Common.MST && X3No.Text.ToDecimal() != 2)
            {
                var checktaxmny = dtSaleD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPS));
                if (TaxMny.Text.ToDecimal() != checktaxmny)
                {
                    MessageBox.Show("稅前合計金額有誤！無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var einvchange = EInvChange.Text.Trim();

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                #region Append 、 Duplicate
                //前置作業
                string cuname2 = "";
                jSale.Validate<JBS.JS.Cust>(CuNo.Text.Trim(), reader =>
                {
                    cuname2 = reader["cuname2"].ToString().Trim();
                });


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

                        //明細借轉銷
                        if (dtSaleD.AsEnumerable().Any(r => r["leno"].ToString().Trim() == "批借轉銷") && this.FormState == FormEditState.Append)
                        {
                            //還入單號
                            result &= jSale.GetPkNumber<JBS.JS.RLend>(cmd, SaDate.Text, ref LeNo);

                            gotoBatRlend(cmd, cuname2);
                        }
                        else if (dtSaleD.AsEnumerable().Any(r => r["leno"].ToString().Trim().Length > 0) && this.FormState == FormEditState.Append)
                        {
                            gotoRlend(cmd);
                        }

                        //銷貨單號
                        result &= jSale.GetPkNumber<JBS.JS.Sale>(cmd, SaDate.Text, ref SaNo);

                        //銷貨單號-還入單號是否有出錯
                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        //自動取得發票號碼
                        bool repeat;
                        this.AutoGetInvNo(cmd, out repeat);
                        if (repeat)
                            return;
                        //檢查發票
                        if (!InvNoCheck(cmd)) return;

                        //儲存主檔
                        this.AppendMasterOnSaving(cmd);

                        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                        {
                            //儲存明細
                            this.AppendDetailOnSaving(cmd, i);

                            //若此筆明細為訂單轉入
                            if (dtSaleD.Rows[i]["orid"].ToString().Trim().Length > 0)
                            {
                                this.AnsyOrderdQtyOnSaving(cmd, i);
                            }
                        }

                        //儲存組件
                        this.AppendBomOnSaving(cmd);

                        //處理沖款
                        this.PassToReceivOnSaving(cmd);

                        //處理庫存
                        jSale.扣庫存(cmd, dtSaleD, dtSaleBom);

                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, SaNo.Text, "銷貨單");
                       
                        //批次資料
                        BatchSave.進退_Append(dt_BatchProcess, cmd, "Saled", SaNo.Text.Trim(), false, CuNo.Text.Trim());
                        BatchSave.進退_Append(dt_Bom_BatchProcess, cmd, "SaleBom", SaNo.Text.Trim(), true, CuNo.Text.Trim());
                        
                        //完成重要資料存檔, 確認交易
                        tn.Commit();

                        //儲存完成
                        jSale.Save(SaNo.Text.Trim());
                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新客戶應收帳款
                            jSale.UpdateNewCustReceiv(payerno.Text.Trim(), AcctMny.Text, GetPrvAcc.Text);

                            //更新客戶交易日期
                            jSale.UpdateCustLastDay(SaDate.Text.Trim(), CuNo.Text.Trim());

                            //更新最近進貨日期
                            jSale.UpdateItemDate(SaDate.Text, ref dtSaleD);

                            //更新產品檔庫存量
                            jSale.UpdateItemItStockQty(dtSaleD, dtSaleBom);

                            //使用電子發票
                            if (EInv1.Checked) ChangeCustEInv(CuNo.Text.Trim(), einvchange);
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
                    using (var frm = new FrmSale_Print())
                    {
                        frm.PK = jSale.GetCurrent();
                        frm.CuNo = CuNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }

                if (LeNo.Text.Trim().Length > 0)
                {
                    using (var frm = new S2.FrmSaleAndRLend())
                    {
                        frm.No = jSale.GetCurrent();
                        frm.RNo = LeNo.Text.Trim();
                        frm.me = S2.FrmSaleAndRLend.MyEnum.Sale;

                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                ThisFormState();
                #endregion
            }

            if (this.FormState == FormEditState.Modify)
            {
                #region Modify
                if (jSale.IsExistDocument<JBS.JS.Sale>(SaNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

                if (jSale.IsPassToReceiv(SaNo.Text.Trim()) == true)
                    return;


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

                        //自動取得發票號碼
                        bool repeat = false;
                        this.AutoGetInvNo(cmd, out repeat);
                        if (repeat)
                            return;
                        //檢查發票
                        if (!InvNoCheck(cmd)) return;

                        if (!提示是否清空這張單據的發票號碼(cmd)) return;

                        //儲存主檔
                        UpdateMasterOnSaving(cmd);

                        //刪除舊檔
                        DelteOldOnSaving(cmd);

                        for (int i = 0; i < tempSaleD.Rows.Count; i++)
                        {
                            //NetNo
                            if (tempSaleD.Rows[i]["NetNo"].ToString().Length > 0)
                            {
                                if (tempSaleD.Rows[i]["NetNo"].ToString().Length > 0)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("NetNo", tempSaleD.Rows[i]["NetNo"].ToString());
                                    cmd.CommandText = @"update weborder SET orderState='1' WHERE orno = @NetNo";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            //若此筆明細為訂單轉入先扣掉訂單的交貨數量
                            if (tempSaleD.Rows[i]["orid"].ToString().Trim().Length > 0)
                            {
                                BackOrderdQty(cmd, i);
                            }
                        }

                        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                        {
                            //儲存明細
                            this.AppendDetailOnSaving(cmd, i);

                            //若此筆明細為訂單轉入
                            if (dtSaleD.Rows[i]["orid"].ToString().Trim().Length > 0)
                            {
                                this.AnsyOrderdQtyOnSaving(cmd, i);
                            }
                        }

                        //儲存組件
                        this.AppendBomOnSaving(cmd);

                        //處理沖款
                        this.Update_PassToReceivOnSaving(cmd);

                        //處理庫存
                        jSale.加庫存(cmd, tempSaleD, tempBom);
                        jSale.扣庫存(cmd, dtSaleD, dtSaleBom);
                        
                        //批次資料
                        BatchSave.進退_Modify(dt_TempBatchProcess, dt_BatchProcess, cmd, "Saled", SaNo.Text.Trim(), false, CuNo.Text.Trim());
                        BatchSave.進退_Modify(dt_Bom_TempBatchProcess, dt_Bom_BatchProcess, cmd, "SaleBom", SaNo.Text.Trim(), true, CuNo.Text.Trim());

                        //完成重要資料存檔, 確認交易
                        tn.Commit();

                        //儲存完成
                        jSale.Save(SaNo.Text.Trim());
                        string tepayerno = payerno.Text.ToString().Trim();
                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新客戶應付帳款
                            jSale.BackOldCustReceiv( this.Oldpayerno, acctmny, getprvacc);
                            jSale.UpdateNewCustReceiv(tepayerno, AcctMny.Text, GetPrvAcc.Text);

                            //更新客戶交易日期
                            jSale.UpdateCustLastDay(SaDate.Text.Trim(), CuNo.Text.Trim());

                            //更新最近銷貨日期
                            jSale.UpdateItemDate(SaDate.Text.Trim(), ref dtSaleD);

                            //更新產品檔庫存量
                            jSale.UpdateItemItStockQty(tempSaleD, tempBom, dtSaleD, dtSaleBom);

                            //使用電子發票
                            if (EInv1.Checked) ChangeCustEInv(CuNo.Text.Trim(), einvchange);
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
                    using (var frm = new FrmSale_Print())
                    {
                        frm.PK = jSale.GetCurrent();
                        frm.CuNo = CuNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }
                jSale.upModify0<JBS.JS.Sale>(SaNo.Text.Trim());//修改狀態改回0可供他人編輯
                if (tk != null)
                    tk.Wait();
                //Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                //ThisFormState();
                btnAppend_Click(null, null);
                #endregion
            }
            
        }

        private bool 提示是否清空這張單據的發票號碼(SqlCommand cmd)
        {
            if (InvNo.Text.Trim().Length == 0)
            {
                string OldInNo = SQL.ExecuteScalar("select top 1 invno from sale where sano = @sano", new parameters("sano", SaNo.Text.Trim()), cmd);
                if (OldInNo.Trim().Length > 0) //如果發票號碼是空的，且在這次存檔前有發票號碼
                {
                    if (MessageBox.Show("單據編號: [" + SaNo.Text + "] 目前發票號碼為: [" + OldInNo + "]\n存檔後將會清空發票號碼，是否存檔?","", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        InvNo.Text = OldInNo;
                        InvNo.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private void Update_PassToReceivOnSaving(SqlCommand cmd)
        {
            //儲存時檢查『應收總計』與『未收金額』是否相等
            //若相等時，刪除沖款與沖款明細
            //若不等時，沖款
            decimal totmny = 0;//應收總額
            decimal acctmny = 0;//未收總額
            decimal.TryParse(TotMny.Text, out totmny);
            decimal.TryParse(AcctMny.Text, out acctmny);
            decimal collectmny = 0;//已收金額
            decimal getprvacc = 0;//取用預收
            decimal.TryParse(CollectMny.Text, out collectmny);
            decimal.TryParse(GetPrvAcc.Text, out getprvacc);
            //沖款總額
            decimal _Total = 0;
            _Total = collectmny + getprvacc;//已收金額 + 取用預收
            //本幣總額
            decimal xa1par = 0;
            decimal _TotalB = 0;
            decimal.TryParse(Xa1Par.Text, out xa1par);
            _TotalB = _Total * xa1par;//沖款總額 * 匯率

            string reno = "";

            //刪除沖款
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.CommandText = "select reno from receivd where ExtFlag =N'銷貨' and SaNo =@sano COLLATE Chinese_Taiwan_Stroke_BIN";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    if (reader.Read())
                        reno = reader["reno"].ToString();
            }
            if (totmny == acctmny)   //獲得此銷貨單沖帳編號
            {
                cmd.Parameters.AddWithValue("reno", reno);
                cmd.CommandText = "delete from receiv where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from receivd where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
                cmd.ExecuteNonQuery();
            }
            else if (totmny != acctmny)//判斷此銷貨單是否有沖帳收入
            {
                if (reno != "")//判斷是否有此銷貨單是否有沖帳紀錄
                {
                    #region Update receiv 、receivd
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("reno", reno);
                    cmd.Parameters.AddWithValue("redate", Date.ToTWDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("redate1", Date.ToUSDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("cuno", payerno.Text.Trim());
                    cmd.Parameters.AddWithValue("cuname1", payername.Text.Trim());
                    cmd.Parameters.AddWithValue("cutel1", payername.Text != CuNo.Text ? "" : AdTel1.Text);
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
                    cmd.Parameters.AddWithValue("memo1", Bracket.Text.Trim());
                    cmd.Parameters.AddWithValue("memo2", "銷貨單轉入");
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Bracket", "銷貨");
                    cmd.Parameters.AddWithValue("recordno", 1);
                    cmd.Parameters.AddWithValue("ExtFlag", "銷貨");
                    cmd.Parameters.AddWithValue("TotMny1", 0);
                    cmd.Parameters.AddWithValue("TotExgDiff", 0);
                    cmd.Parameters.AddWithValue("CheckMny", 0);
                    cmd.Parameters.AddWithValue("RemitMny", 0);
                    cmd.Parameters.AddWithValue("OtherMny", 0);
                    cmd.Parameters.AddWithValue("AddPrvAcc", 0);
                    cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
                    cmd.Parameters.AddWithValue("offline", offline);
                    cmd.Parameters.AddWithValue("online", online);
                    cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                    cmd.CommandText = @"
update receiv set 
reno = @reno,redate = @redate,redate1 = @redate1,cuno = @cuno,cuname1 = @cuname1,cutel1 = @cutel1,emno = @emno,emname = @emname,cashmny = @cashmny,cardmny = @cardmny,cardno = @cardno,ticket = @ticket
,getprvacc = @getprvacc,totmny = @totmny,actslt = @actslt,totdisc = @totdisc,totreve = @totreve,memo1 = @memo1,memo2 = @memo2,sano = @sano,seno = @seno,Bracket = @Bracket,recordno = @recordno,ExtFlag = @ExtFlag,TotMny1 = @TotMny1
,TotExgDiff = @TotExgDiff,CheckMny = @CheckMny,RemitMny = @RemitMny,OtherMny = @OtherMny,AddPrvAcc = @AddPrvAcc,xa1par = @xa1par,offline = @offline,online = @online,spno = @spno
where reno = @reno  ";

                    cmd.ExecuteNonQuery();

                    //沖款明細
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("reno", reno);
                    cmd.Parameters.AddWithValue("redate", Date.ToTWDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("redate1", Date.ToUSDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("cuno", payerno.Text.Trim());
                    cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    cmd.Parameters.AddWithValue("emname", EmName.Text);
                    cmd.Parameters.AddWithValue("recordno", 1);
                    cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("sadateac1", Date.ToUSDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("bracket", "銷貨");
                    cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
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
                    cmd.Parameters.AddWithValue("exgdiff", 0);
                    cmd.Parameters.AddWithValue("extflag", "銷貨");
                    cmd.Parameters.AddWithValue("payerno", payerno.Text != CuNo.Text ? payername.Text.Trim() : "");
                    cmd.CommandText = @"
update receivd set 
reno = @reno,redate = @redate,redate1 = @redate1,cuno = @cuno,emno = @emno,emname = @emname,recordno = @recordno,sadateac = @sadateac,sadateac1 = @sadateac1,sano = @sano,bracket = @bracket
,xa1no = @xa1no,xa1name = @xa1name,xa1par = @xa1par,totmny = @totmny,acctmny = @acctmny,invno = @invno,discount = @discount,reverse = @reverse,xa1par1 = @xa1par1,reverseb = @reverseb
,exgstat = @exgstat,exgdiff = @exgdiff,extflag = @extflag , payerno = @payerno 
where reno = @reno ";
                    cmd.ExecuteNonQuery();
                    #endregion
                }
                else
                    PassToReceivOnSaving(cmd);
            }
        }  //13.7C
        private void AutoGetInvNo(SqlCommand cmd, out bool repeat)
        {
            //自動取得發票，純自動取得發票
            //檢查部分，由另一函式執行
            repeat = false;

            //批開不自動取得發票
            if (gridInvMode.Text == "批開")
            {
                if (Common.User_ScInvSlt == 2)
                    MessageBox.Show("批開發票，將不自動取得發票號碼!!!");
                return;
            }

            //修改-沒有更改發票取得方式,不自動取得發票
            if (gridInvMode.Text == "" && gridAutoInv.Visible == false)
            {
                if (Common.User_ScInvSlt == 2)
                    MessageBox.Show("未選自動開立發票，將不自動取得發票號碼!!!");
                return;
            }

            //單據金額0，不自動取得發票
            if (TotMny.Text.Trim().ToDecimal() == 0)
            {
                if (Common.User_ScInvSlt == 2)
                    MessageBox.Show("單據金額為零，將不自動取得發票號碼!!!");
                return;
            }

            //選手動，但沒有打發票，不自動取得
            //選手動，有打發票，也不自動取得
            if (gridAutoInv.Text == "手動")
            {
                if (Common.User_ScInvSlt == 2)
                    MessageBox.Show("選擇手動開立發票，將不自動取得發票號碼!!!");
                return;
            }

            //剩下狀況
            //1.自動，有手動打發票
            //2.自動，沒手動打發票

            //若為true，代表自動，手打發票
            bool IsUserInput = InvNo.Text.Length == 10;

            var scvno = "";
            var invno = "";
            var inv = "";
            var no = 0M;
            object obj;
            bool exist = false;

            if (IsUserInput)
                invno = InvNo.Text;
            else
                InvNo.Clear();

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InvNo", "");
            cmd.Parameters.AddWithValue("@User", Common.User_Name);
            cmd.Parameters.AddWithValue("@Einvid", User_Einv.Text.ToString().Trim());//電子發票 改這
            if (X5No.Text.Trim().ToInteger() == 2)//二聯
            {
                #region 自動二聯式發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic2 from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();
                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定二聯式發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoic2e ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoic2 = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToInteger() == 1)//三聯
            {
                #region 自動三聯式發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic3 from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();

                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定三聯式發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoic3e ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoic3 = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToInteger() == 7)//電子發票取號
            {
                #region 電子發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic7 from Einvsetup where Einvid = (@Einvid)";
                    obj = cmd.ExecuteScalar();

                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)//取得發票不足10碼，可能電子發票設定有誤
                    {
                        MessageBox.Show("沒有設定正確的電子發票號碼!(電子發票自動取號)");
                        repeat = true;
                        return;
                    }
                }

                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = "Select Count(*) from Einvsetup where Einvid = (@Einvid) And (@InvNo) > ScInvoic7e";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Einvsetup Set ScInvoic7 = (@InvNo) where Einvid = (@Einvid)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToInteger() == 8)//電子發票取號
            {
                #region 電子發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic8 from Einvsetup where Einvid = (@Einvid)";
                    obj = cmd.ExecuteScalar();

                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)//取得發票不足10碼，可能電子發票設定有誤
                    {
                        MessageBox.Show("沒有設定正確的電子發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = "Select Count(*) from Einvsetup where Einvid = (@Einvid) And (@InvNo) > ScInvoic8e";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Einvsetup Set ScInvoic8 = (@InvNo) where Einvid = (@Einvid)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToDecimal() == 3)//二聯收銀機
            {
                #region 自動二聯收銀機發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoicA from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();
                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定二聯收銀機發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoicAe ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoicA = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToDecimal() == 4)//三聯收銀機
            {
                #region 自動三聯收銀機發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoicA3 from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();
                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定三聯收銀機發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoicA3e ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoicA3 = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
        }
        private bool InvNoCheck(SqlCommand cmd)
        {
            if (InvNo.TrimTextLenth() == 0) return true;
            //新增狀態 或 修改狀態發票號碼異動下，檢查發票號碼是否重複
            if (this.FormState != FormEditState.Modify || (this.FormState == FormEditState.Modify && tempinvno != InvNo.Text))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@InvNo", InvNo.Text.Trim());
                cmd.CommandText = @"Select count(*) from
                (
                 (select invno from Sale )
                 union all	
                 (select invno from posinv)
                 union all	
                 (select invno from sinv)
                ) POS與進銷存銷貨單以及銷項發票登入
                 where InvNo = @InvNo";

                if (cmd.ExecuteScalar().ToDecimal() >= 1)
                {
                    MessageBox.Show("此發票編號已存在，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    InvNo.SelectAll();
                    return false;
                }
                //作廢
                cmd.CommandText = "select invno from nullify where invno = @InvNo";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                {
                    MessageBox.Show("此發票編號已有作廢紀錄，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    InvNo.SelectAll();
                    return false;
                }
            }

            //修改狀態下，發票已折讓，無法修改
            if (this.FormState == FormEditState.Modify)
            {
                cmd.CommandText = "select count(invno) from discountd where invno = N'" + tempinvno + "'";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    MessageBox.Show("此單據已有折讓紀錄不可修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();

            if ((X5No.Text.ToDecimal() == 3 || X5No.Text.ToDecimal() == 4) && Common.sc_MachineSet.Trim().Length > 0)
            {
                SeNo.Text = Common.sc_MachineSet;
                SeName.Text = Common.sc_MachineSet + "號機";
            }

            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.Parameters.AddWithValue("sadate1", Date.ToUSDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadate2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("sadateac1", Date.ToUSDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("sadateac2", SaDateAc.Text.Trim());
            cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName11.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
            cmd.Parameters.AddWithValue("spname", SpName.Text.Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
            cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", RATEToRate(Rate));
            cmd.Parameters.AddWithValue("x5no", X5No.Text.Trim());
            cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
            cmd.Parameters.AddWithValue("sename", SeName.Text.Trim());
            cmd.Parameters.AddWithValue("x4no", X4No.Text.Trim());
            cmd.Parameters.AddWithValue("x4name", X4Name.Text.Trim());
            cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
            cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
            cmd.Parameters.AddWithValue("taxb", ToTaiwanMny(Tax));//本幣營業稅額 = 外幣營業稅額*匯率
            cmd.Parameters.AddWithValue("totmnyb", ToTaiwanMny(TotMny));//本幣總額 = 外幣總額*匯率
            cmd.Parameters.AddWithValue("discount", Discount.Text.Trim());
            cmd.Parameters.AddWithValue("cashmny", CashMny.Text.Trim());
            cmd.Parameters.AddWithValue("cardmny", CardMny.Text.Trim());
            cmd.Parameters.AddWithValue("cardno", CardNo.Text.Trim());
            cmd.Parameters.AddWithValue("ticket", Ticket.Text.Trim());
            cmd.Parameters.AddWithValue("collectmny", CollectMny.Text.Trim());
            cmd.Parameters.AddWithValue("getprvacc", GetPrvAcc.Text.Trim());
            cmd.Parameters.AddWithValue("acctmny", AcctMny.Text.Trim());
            cmd.Parameters.AddWithValue("samemo", SaMemo.Text);
            cmd.Parameters.AddWithValue("bracket", Bracket.Text.Trim());
            cmd.Parameters.AddWithValue("recordno", dataGridViewT1.Rows.Count);
            cmd.Parameters.AddWithValue("invno", InvNo.Text.Trim());
            cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(InvDate));
            cmd.Parameters.AddWithValue("invdate1", Date.ToUSDate(InvDate));
            cmd.Parameters.AddWithValue("invname", InvName.Text.Trim());
            cmd.Parameters.AddWithValue("invtaxno", InvTaxNo.Text.Trim());
            cmd.Parameters.AddWithValue("invaddr1", InvAddr1.Trim());
            cmd.Parameters.AddWithValue("invbatch", GetGridInvMode());//發票批開選定
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("samemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("deno", DeNo.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("dename", DeName.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("cutel1", CuTel1);
            cmd.Parameters.AddWithValue("cuper1", CuPer1.GetUTF8(10));
            cmd.Parameters.AddWithValue("CuAddr", CuAddr);
            cmd.Parameters.AddWithValue("Adper1", AdPer11.Text.ToString().Trim().GetUTF8(10));//指送負責人
            cmd.Parameters.AddWithValue("Adtel", AdTel1.Text.ToString().Trim());//指送電話
            cmd.Parameters.AddWithValue("AdAddr", AdAddr1.Text.ToString().Trim());//指送地址
            cmd.Parameters.AddWithValue("SaPayment", SaPayment.Text.Trim());
            cmd.Parameters.AddWithValue("PhotoPath", PhotoPath); //13.7a
            cmd.Parameters.AddWithValue("einv", EInv1.Checked ? "1" : "2");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvstate", EInv1.Checked ? EInvState.Text.Trim() : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvchange", EInv1.Checked ? EInvChange.Text : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("invrandom", jSale.GetInvoiceRandom());//發票隨機防伪碼 4碼
            cmd.Parameters.AddWithValue("payerno", payerno.Text.ToString());
            cmd.Parameters.AddWithValue("User_Einv", User_Einv.Text);//電子發票設定
            cmd.Parameters.AddWithValue("iTitle", iTitle.Text);//電子發票設定

            //媒體申報
            if (InvNo.Text.Trim() != "")
            {
                if (invkind == "")
                {
                    if (X5No.Text == "1")
                    {
                        invkind = "31 銷項三聯式".GetUTF8(20);
                    }
                    else if (X5No.Text == "2")
                    {
                        invkind = "32 銷項二聯式".GetUTF8(20);
                    }
                    else if (X5No.Text == "3")
                    {
                        invkind = "32 銷項二聯式收銀機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "4")
                    {
                        invkind = "35 銷項三聯式收銀機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "5")
                    {
                        invkind = "36 銷項免用統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "7")
                    {
                        invkind = "35 銷項一般稅額計算之電子發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "8")
                    {
                        invkind = "37 特種稅額之銷項憑證(含特種稅額計算之電子發票)".GetUTF8(20);
                    }
                }

                if (X3No.Text.Trim() != "3")
                    passmode = "";
                else
                {
                    if (passmode == "")
                        passmode = "1";
                }

                if (invkind.Substring(0, 2) != "37")
                    specialtax = "";
            }
            else
            {
                if (X5No.Text.Trim() == "5")
                {
                    if(invkind == "")
                        invkind = "36 銷項免用統一發票".GetUTF8(20);

                    specialtax = "";

                    if (X3No.Text.Trim() != "3")
                        passmode = "";
                    else
                    {
                        if (passmode == "")
                            passmode = "1";
                    }
                }
                else
                {
                    invkind = "";
                    specialtax = "";
                    passmode = "";
                }
            }

            cmd.Parameters.AddWithValue("invkind", invkind);
            cmd.Parameters.AddWithValue("passmode", passmode);
            cmd.Parameters.AddWithValue("specialtax", specialtax);

            cmd.CommandText = @"
            INSERT INTO Sale (
            sano,sadate1,sadate2,sadateac1,sadateac2,quno,cuno,cuname1,cutel1,cuper1,CuAddr
            ,emno,emname,spno,spname,stno,stname,xa1no,xa1name,xa1par
            ,taxmnyb,taxmny,x3no,rate,x5no,seno,sename,x4no,x4name,tax,totmny
            ,taxb,totmnyb,discount,cashmny,cardmny,cardno,ticket,collectmny
            ,getprvacc,acctmny,samemo,bracket,recordno,invno
            ,invdate,invdate1,invname,invtaxno,invaddr1,invbatch
            ,appdate,edtdate,appscno,edtscno,sadate,sadateac,samemo1,deno,dename,Adper1,Adtel,AdAddr,SaPayment,PhotoPath
            ,invkind,passmode,specialtax,einvstate,einvchange,einv,invrandom,payerno,User_Einv,iTitle 
            ) VALUES (
            @sano,@sadate1,@sadate2,@sadateac1,@sadateac2,@quno,@cuno,@cuname1,@cutel1,@cuper1,@CuAddr
            ,@emno,@emname,@spno,@spname,@stno,@stname,@xa1no,@xa1name,@xa1par
            ,@taxmnyb,@taxmny,@x3no,@rate,@x5no,@seno,@sename,@x4no,@x4name,@tax,@totmny
            ,@taxb,@totmnyb,@discount,@cashmny,@cardmny,@cardno,@ticket,@collectmny
            ,@getprvacc,@acctmny,@samemo,@bracket,@recordno,@invno
            ,@invdate,@invdate1,@invname,@invtaxno,@invaddr1,@invbatch
            ,@appdate,@edtdate,@appscno,@edtscno,@sadate,@sadateac,@samemo1,@deno,@dename,@Adper1,@Adtel,@AdAddr,@SaPayment,@PhotoPath
            ,@invkind,@passmode,@specialtax,@einvstate,@einvchange,@einv,@invrandom,@payerno,@User_Einv,@iTitle)";
            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.Parameters.AddWithValue("sadate1", Date.ToUSDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadate2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("sadateac1", Date.ToUSDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("sadateac2", SaDateAc.Text.Trim());
            cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
            cmd.Parameters.AddWithValue("stno", dtSaleD.Rows[i]["stno"].ToString().Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
            cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
            cmd.Parameters.AddWithValue("sename", SeName.Text.Trim());
            cmd.Parameters.AddWithValue("x4no", X4No.Text.Trim());
            cmd.Parameters.AddWithValue("x4name", X4Name.Text.Trim());
            cmd.Parameters.AddWithValue("itno", dtSaleD.Rows[i]["itno"].ToString().Trim());
            cmd.Parameters.AddWithValue("itname", dtSaleD.Rows[i]["itname"].ToString());
            cmd.Parameters.AddWithValue("ittrait", dtSaleD.Rows[i]["ittrait"].ToString().Trim());
            cmd.Parameters.AddWithValue("itunit", dtSaleD.Rows[i]["itunit"].ToString().Trim());
            cmd.Parameters.AddWithValue("itpkgqty", dtSaleD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qty", dtSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("price", dtSaleD.Rows[i]["price"].ToDecimal("f" + Common.MS));
            cmd.Parameters.AddWithValue("prs", dtSaleD.Rows[i]["prs"].ToDecimal("f3"));
            cmd.Parameters.AddWithValue("rate", RATEToRate(Rate));
            cmd.Parameters.AddWithValue("taxprice", dtSaleD.Rows[i]["taxprice"].ToDecimal("f6"));
            cmd.Parameters.AddWithValue("mny", dtSaleD.Rows[i]["mny"].ToDecimal("f" + Common.TPS));
            cmd.Parameters.AddWithValue("priceb", dtSaleD.Rows[i]["priceb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("taxpriceb", dtSaleD.Rows[i]["taxpriceb"].ToDecimal("f6"));
            cmd.Parameters.AddWithValue("mnyb", dtSaleD.Rows[i]["mnyb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("memo", dtSaleD.Rows[i]["Memo"].ToString().Trim().GetUTF8(20));
            cmd.Parameters.AddWithValue("bomid", SaNo.Text + dtSaleD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", dtSaleD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1).ToString());
            cmd.Parameters.AddWithValue("bracket", Bracket.Text.Trim());
            cmd.Parameters.AddWithValue("itdesp1", dtSaleD.Rows[i]["ItDesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", dtSaleD.Rows[i]["ItDesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", dtSaleD.Rows[i]["ItDesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", dtSaleD.Rows[i]["ItDesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", dtSaleD.Rows[i]["ItDesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", dtSaleD.Rows[i]["ItDesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", dtSaleD.Rows[i]["ItDesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", dtSaleD.Rows[i]["ItDesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", dtSaleD.Rows[i]["ItDesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", dtSaleD.Rows[i]["ItDesp10"]);
            cmd.Parameters.AddWithValue("stName", dtSaleD.Rows[i]["stname"].ToString().Trim());
            cmd.Parameters.AddWithValue("orid", dtSaleD.Rows[i]["orid"]);
            cmd.Parameters.AddWithValue("orno", dtSaleD.Rows[i]["orno"]);
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("mwidth1", dtSaleD.Rows[i]["mwidth1"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth2", dtSaleD.Rows[i]["mwidth2"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth3", dtSaleD.Rows[i]["mwidth3"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth4", dtSaleD.Rows[i]["mwidth4"].ToDecimal());
            cmd.Parameters.AddWithValue("pqty", dtSaleD.Rows[i]["pqty"].ToDecimal());
            cmd.Parameters.AddWithValue("punit", dtSaleD.Rows[i]["punit"].ToString());
            cmd.Parameters.AddWithValue("pformula", dtSaleD.Rows[i]["pformula"].ToString());
            cmd.Parameters.AddWithValue("leno", dtSaleD.Rows[i]["leno"].ToString().Trim());
            cmd.Parameters.AddWithValue("leid", dtSaleD.Rows[i]["leid"]);
            cmd.Parameters.AddWithValue("NetNo", dtSaleD.Rows[i]["NetNo"]);
            cmd.Parameters.AddWithValue("standard", dtSaleD.Rows[i]["standard"]);

            if (dtSaleD.Rows[i]["leno"].ToString().Trim().Length > 0)
                cmd.Parameters.AddWithValue("cyno", LeNo.Text.Trim());
            else
                cmd.Parameters.AddWithValue("cyno", "");

            cmd.Parameters.AddWithValue("Adper1", dtSaleD.Rows[i]["Adper1"].ToString().Trim().GetUTF8(10));//指送負責人
            cmd.Parameters.AddWithValue("Adtel", dtSaleD.Rows[i]["Adtel"].ToString().Trim().GetUTF8(20));//指送電話
            if (dtSaleD.Rows[i]["AdAddr"].ToString().Trim().GetUTF8(60) == "")
                cmd.Parameters.AddWithValue("AdAddr", AdAddr1.Text.Trim().GetUTF8(60));//指送地址
            else
                cmd.Parameters.AddWithValue("AdAddr", dtSaleD.Rows[i]["AdAddr"].ToString().Trim().GetUTF8(60));//指送地址
            cmd.Parameters.AddWithValue("AdName", dtSaleD.Rows[i]["AdName"].ToString().Trim().GetUTF8(50));

            cmd.CommandText = @"
            INSERT INTO Saled (
            sano,sadate1,sadate2,sadateac1,sadateac2,quno,cuno,emno,spno
            ,stno,xa1no,xa1par,seno,sename,x4no,x4name,itno,itname
            ,ittrait,itunit,itpkgqty,qty,price,prs,rate,taxprice,mny,priceb
            ,taxpriceb,mnyb,memo,bomid,bomrec,recordno,bracket
            ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5
            ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10
            ,stName,orid,orno,sadate,sadateac,mwidth1,mwidth2,mwidth3,mwidth4
            ,pqty,punit,pformula,leno,leid,cyno,Adper1,Adtel,AdAddr,AdName,NetNo,standard
            )  VALUES (
            @sano,@sadate1,@sadate2,@sadateac1,@sadateac2,@quno,@cuno,@emno,@spno
            ,@stno,@xa1no,@xa1par,@seno,@sename,@x4no,@x4name,@itno,@itname
            ,@ittrait,@itunit,@itpkgqty,@qty,@price,@prs,@rate,@taxprice,@mny,@priceb
            ,@taxpriceb,@mnyb,@memo,@bomid,@bomrec,@recordno,@bracket
            ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5
            ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10
            ,@stName,@orid,@orno,@sadate,@sadateac,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pqty,@punit
            ,@pformula,@leno,@leid,@cyno,@Adper1,@Adtel,@AdAddr,@AdName,@NetNo,@standard) ";
            cmd.ExecuteNonQuery();

            if (dtSaleD.Rows[i]["NetNo"].ToString().Length > 0)
            {
                cmd.CommandText = @"update weborder SET orderState='2' WHERE orno = @NetNo";
                cmd.ExecuteNonQuery();
            }

        }
        private void AnsyOrderdQtyOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyout", dtSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qtyNotout", dtSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("bomid", dtSaleD.Rows[i]["orid"].ToString());
            cmd.Parameters.AddWithValue("orno", dtSaleD.Rows[i]["orno"].ToString());
            cmd.CommandText = @"
                Update orderd set qtyout = qtyout+@qtyout, qtyNotout = qtyNotout-@qtyNotout
                Where bomid=@bomid and orno=@orno";
            cmd.ExecuteNonQuery();

            //檢查訂單是否結案
            cmd.Parameters.AddWithValue("oroverflag0", "0");
            cmd.Parameters.AddWithValue("oroverflag1", "1");
            cmd.CommandText = " Update [order] set oroverflag =@oroverflag0 where orno = @orno and (Select Count(*) from orderd where qtyNotout > 0 and orno = @orno) > 0;";
            cmd.CommandText += "Update [order] set oroverflag =@oroverflag1 where orno = @orno and (Select Count(*) from orderd where qtyNotout > 0 and orno = @orno) = 0;";
            cmd.ExecuteNonQuery();
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < dtSaleBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("SaNo", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("BomID", SaNo.Text + dtSaleBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("BomRec", dtSaleBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", dtSaleBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", dtSaleBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", dtSaleBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", dtSaleBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", dtSaleBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", dtSaleBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", dtSaleBom.Rows[i]["itrec"]);
                cmd.Parameters.AddWithValue("itprice", dtSaleBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", dtSaleBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", dtSaleBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", dtSaleBom.Rows[i]["itnote"]);

                cmd.CommandText = @"
                    INSERT INTO SaleBom
                    (SaNo,BomID,BomRec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,itprs,itmny,itnote) VALUES 
                    (@SaNo,@BomID,@BomRec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote)";
                cmd.ExecuteNonQuery();
            }
        }
        private void PassToReceivOnSaving(SqlCommand cmd)
        {
            //儲存時檢查『應收總計』與『未收金額』是否相等
            //若相等時，刪除沖款與沖款明細
            //若不等時，沖款
            decimal totmny = 0;//應收總額
            decimal acctmny = 0;//未收總額
            decimal.TryParse(TotMny.Text, out totmny);
            decimal.TryParse(AcctMny.Text, out acctmny);
            decimal collectmny = 0;//已收金額
            decimal getprvacc = 0;//取用預收
            decimal.TryParse(CollectMny.Text, out collectmny);
            decimal.TryParse(GetPrvAcc.Text, out getprvacc);
            //沖款總額
            decimal _Total = 0;
            _Total = collectmny + getprvacc;//已收金額 + 取用預收
            //本幣總額
            decimal xa1par = 0;
            decimal _TotalB = 0;
            decimal.TryParse(Xa1Par.Text, out xa1par);
            _TotalB = _Total * xa1par;//沖款總額 * 匯率

            string reno = "";

            //刪除沖款
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.CommandText = "select reno from receivd where ExtFlag =N'銷貨' and SaNo =@sano COLLATE Chinese_Taiwan_Stroke_BIN";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    if (reader.Read())
                        reno = reader["reno"].ToString();
            }
            cmd.Parameters.AddWithValue("reno", reno);
            cmd.CommandText = "delete from receiv where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
            //
            cmd.CommandText = "delete from receivd where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
            //
            if (totmny != acctmny)
            {
                //沖款主檔
                JE.MyControl.TextBoxT ReNo = new JE.MyControl.TextBoxT();
                ReNo.Name = "ReNo";
                Common.JESetSSID(SqlTable.Receiv, ref SaDate, ref ReNo, cmd);
                reno = ReNo.Text.Trim();

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("reno", reno);
                cmd.Parameters.AddWithValue("redate", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("redate1", Date.ToUSDate(SaDate.Text));

                cmd.Parameters.AddWithValue("cuno", payerno.Text.Trim());
                cmd.Parameters.AddWithValue("cuname1", payername.Text.Trim());
                cmd.Parameters.AddWithValue("cutel1", CuNo.Text.Trim() == payerno.Text.Trim()?AdTel1.Text.Trim():"");

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
                cmd.Parameters.AddWithValue("memo1", Bracket.Text.Trim());
                cmd.Parameters.AddWithValue("memo2", "銷貨單轉入");
                cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
                cmd.Parameters.AddWithValue("Bracket", "銷貨");
                cmd.Parameters.AddWithValue("recordno", 1);
                cmd.Parameters.AddWithValue("ExtFlag", "銷貨");
                cmd.Parameters.AddWithValue("TotMny1", 0);
                cmd.Parameters.AddWithValue("TotExgDiff", 0);
                cmd.Parameters.AddWithValue("CheckMny", 0);
                cmd.Parameters.AddWithValue("RemitMny", 0);
                cmd.Parameters.AddWithValue("OtherMny", 0);
                cmd.Parameters.AddWithValue("AddPrvAcc", 0);
                cmd.Parameters.AddWithValue("xa1par1", Xa1Par.Text);
                cmd.Parameters.AddWithValue("offline", offline);
                cmd.Parameters.AddWithValue("online", online);
                cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                cmd.CommandText = @"
                INSERT INTO receiv (
                reno,redate,redate1,cuno,cuname1,cutel1,emno,emname,cashmny,cardmny,cardno,ticket
                ,getprvacc,totmny,actslt,totdisc,totreve,memo1,memo2,sano,seno,Bracket,recordno,ExtFlag,TotMny1
                ,TotExgDiff,CheckMny,RemitMny,OtherMny,AddPrvAcc,xa1par,offline,online,spno
                ) VALUES (
                @reno,@redate,@redate1,@cuno,@cuname1,@cutel1,@emno,@emname,@cashmny,@cardmny,@cardno,@ticket
                ,@getprvacc,@totmny,@actslt,@totdisc,@totreve,@memo1,@memo2,@sano,@seno,@Bracket,@recordno,@ExtFlag,@TotMny1
                ,@TotExgDiff,@CheckMny,@RemitMny,@OtherMny,@AddPrvAcc,@xa1par1,@offline,@online,@spno) ";
                cmd.ExecuteNonQuery();

                //沖款明細
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("reno", reno);
                cmd.Parameters.AddWithValue("redate", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("redate1", Date.ToUSDate(SaDate.Text));
                cmd.Parameters.AddWithValue("cuno", payerno.Text.Trim());
                cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                cmd.Parameters.AddWithValue("emname", EmName.Text);
                cmd.Parameters.AddWithValue("recordno", 1);
                cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("sadateac1", Date.ToUSDate(SaDate.Text));
                cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("bracket", "銷貨");
                cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
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
                cmd.Parameters.AddWithValue("exgdiff", 0);
                cmd.Parameters.AddWithValue("extflag", "銷貨");
                cmd.Parameters.AddWithValue("payerno", payerno.Text != CuNo.Text ?payername.Text.Trim() : "");//注意!!此為[出貨客戶]
                cmd.CommandText = @"
                INSERT INTO receivd (
                reno,redate,redate1,cuno,emno,emname,recordno,sadateac,sadateac1,sano,bracket
                ,xa1no,xa1name,xa1par,totmny,acctmny,invno,discount,reverse,xa1par1,reverseb
                ,exgstat,exgdiff,extflag,payerno 
                ) VALUES (
                @reno,@redate,@redate1,@cuno,@emno,@emname,@recordno,@sadateac,@sadateac1,@sano
                ,@bracket,@xa1no,@xa1name,@xa1par,@totmny,@acctmny,@invno,@discount,@reverse
                ,@xa1par1,@reverseb,@exgstat,@exgdiff,@extflag,@payerno )";
                cmd.ExecuteNonQuery();
            }
        }
        private void UpdateMasterOnSaving(SqlCommand cmd)//修改-更新主檔
        {
            cmd.Parameters.Clear();

            if ((X5No.Text.ToDecimal() == 3 || X5No.Text.ToDecimal() == 4) && Common.sc_MachineSet.Trim().Length > 0)
            {
                SeNo.Text = Common.sc_MachineSet;
                SeName.Text = Common.sc_MachineSet + "號機";
            }

            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.Parameters.AddWithValue("sadate1", Date.ToUSDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadate2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("sadateac1", Date.ToUSDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("sadateac2", SaDateAc.Text.Trim());
            cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName11.Text.Trim());
            cmd.Parameters.AddWithValue("cutel1", AdTel1.Text.Trim());
            cmd.Parameters.AddWithValue("cuper1", AdPer11.Text.Trim().GetUTF8(10));
            //cmd.Parameters.AddWithValue("cuaddr", CuAddr);
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
            cmd.Parameters.AddWithValue("spname", SpName.Text.Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
            cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", RATEToRate(Rate));
            cmd.Parameters.AddWithValue("x5no", X5No.Text.Trim());
            cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
            cmd.Parameters.AddWithValue("sename", SeName.Text.Trim());
            cmd.Parameters.AddWithValue("x4no", X4No.Text.Trim());
            cmd.Parameters.AddWithValue("x4name", X4Name.Text.Trim());
            cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
            cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
            cmd.Parameters.AddWithValue("taxb", ToTaiwanMny(Tax));//本幣營業稅額 = 外幣營業稅額*匯率
            cmd.Parameters.AddWithValue("totmnyb", ToTaiwanMny(TotMny));//本幣總額 = 外幣總額*匯率
            cmd.Parameters.AddWithValue("discount", Discount.Text.Trim());
            cmd.Parameters.AddWithValue("cashmny", CashMny.Text.Trim());
            cmd.Parameters.AddWithValue("cardmny", CardMny.Text.Trim());
            cmd.Parameters.AddWithValue("cardno", CardNo.Text.Trim());
            cmd.Parameters.AddWithValue("ticket", Ticket.Text.Trim());
            cmd.Parameters.AddWithValue("collectmny", CollectMny.Text.Trim());
            cmd.Parameters.AddWithValue("getprvacc", GetPrvAcc.Text.Trim());
            cmd.Parameters.AddWithValue("acctmny", AcctMny.Text.Trim());
            cmd.Parameters.AddWithValue("samemo", SaMemo.Text);
            cmd.Parameters.AddWithValue("bracket", Bracket.Text.Trim());
            cmd.Parameters.AddWithValue("recordno", dataGridViewT1.Rows.Count);
            cmd.Parameters.AddWithValue("invno", InvNo.Text.Trim());
            cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(InvDate));
            cmd.Parameters.AddWithValue("invdate1", Date.ToUSDate(InvDate));
            cmd.Parameters.AddWithValue("invname", InvName.Text.Trim());
            cmd.Parameters.AddWithValue("invtaxno", InvTaxNo.Text.Trim());
            cmd.Parameters.AddWithValue("invaddr1", InvAddr1.Trim());
            cmd.Parameters.AddWithValue("invbatch", GetGridInvMode());//發票批開選定
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("samemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("deno", DeNo.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("dename", DeName.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("offline", offline);
            cmd.Parameters.AddWithValue("online", online);
            cmd.Parameters.AddWithValue("die", die);
            cmd.Parameters.AddWithValue("Adper1", AdPer11.Text.ToString().Trim().GetUTF8(10));//指送負責人
            cmd.Parameters.AddWithValue("Adtel", AdTel1.Text.ToString().Trim());//指送電話
            cmd.Parameters.AddWithValue("AdAddr", AdAddr1.Text.ToString().Trim().GetUTF8(60));//指送地址
            cmd.Parameters.AddWithValue("SaPayment", SaPayment.Text.Trim());
            cmd.Parameters.AddWithValue("PhotoPath", PhotoPath);
            cmd.Parameters.AddWithValue("einvchange", EInv1.Checked ? EInvChange.Text : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einv", EInv1.Checked ? "1" : "2");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvstate", EInv1.Checked ? EInvState.Text.Trim() : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("payerno", payerno.Text.ToString());
            cmd.Parameters.AddWithValue("User_Einv", User_Einv.Text.ToString());
            cmd.Parameters.AddWithValue("iTitle", iTitle.Text);//電子發票設定
            cmd.Parameters.AddWithValue("invrandom",tempinvrandom.Trim().ToString()==""? jSale.GetInvoiceRandom():tempinvrandom);//1229若沒有隨機碼會補

            //媒體申報
            if (InvNo.Text.Trim() != "")
            {
                if (invkind == "")
                {
                    if (X5No.Text == "1")
                    {
                        invkind = "31 銷項三聯式".GetUTF8(20);
                    }
                    else if (X5No.Text == "2")
                    {
                        invkind = "32 銷項二聯式".GetUTF8(20);
                    }
                    else if (X5No.Text == "3")
                    {
                        invkind = "32 銷項二聯式收銀機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "4")
                    {
                        invkind = "35 銷項三聯式收銀機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "5")
                    {
                        invkind = "36 銷項免用統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "7")
                    {
                        invkind = "35 銷項一般稅額計算之電子發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "8")
                    {
                        invkind = "37 特種稅額之銷項憑證(含特種稅額計算之電子發票)".GetUTF8(20);
                    }
                }

                if (X3No.Text.Trim() != "3")
                    passmode = "";
                else
                {
                    if (passmode == "")
                        passmode = "1";
                }

                if (invkind.Substring(0, 2) != "37")
                    specialtax = "";
            }
            else
            {
                if (X5No.Text.Trim() == "5")
                {
                    if (invkind == "")
                        invkind = "36 銷項免用統一發票".GetUTF8(20);

                    specialtax = "";

                    if (X3No.Text.Trim() != "3")
                        passmode = "";
                    else
                    {
                        if (passmode == "")
                            passmode = "1";
                    }
                }
                else
                {
                    invkind = "";
                    specialtax = "";
                    passmode = "";
                }
            }

            cmd.Parameters.AddWithValue("invkind", invkind);
            cmd.Parameters.AddWithValue("passmode", passmode);
            cmd.Parameters.AddWithValue("specialtax", specialtax);

            cmd.CommandText = @" 
            UPDATE Sale set 
            sano=@sano,sadate=@sadate,sadate1=@sadate1,sadate2=@sadate2,sadateac=@sadateac,sadateac1=@sadateac1,sadateac2=@sadateac2
            ,quno=@quno,cuno=@cuno,emno=@emno,emname=@emname,spno=@spno,cuname1=@cuname1,cutel1=@cutel1,cuper1=@cuper1
            ,spname=@spname,stno=@stno,stname=@stname,xa1no=@xa1no,Xa1Name=@Xa1Name,xa1par=@xa1par,taxmnyb=@taxmnyb
            ,taxmny=@taxmny,x3no=@x3no,rate=@rate,x5no=@x5no,seno=@seno,sename=@sename,x4no=@x4no,x4name=@x4name
            ,tax=@tax,totmny=@totmny,taxb=@taxb,totmnyb=@totmnyb,discount=@discount,cashmny=@cashmny,cardmny=@cardmny
            ,cardno=@cardno,ticket=@ticket,collectmny=@collectmny,getprvacc=@getprvacc,acctmny=@acctmny,samemo=@samemo
            ,bracket=@bracket,recordno=@recordno,deno=@deno,dename=@dename,invno=@invno,invdate=@invdate,invdate1=@invdate1
            ,invname=@invname,invtaxno=@invtaxno,invaddr1=@invaddr1,samemo1=@samemo1,invbatch=@invbatch,edtdate=@edtdate
            ,edtscno=@edtscno,offline=@offline,online=@online,die=@die,Adper1=@Adper1,Adtel=@Adtel,AdAddr=@AdAddr,SaPayment=@SaPayment
            ,PhotoPath=@PhotoPath,invkind=@invkind,specialtax=@specialtax,passmode=@passmode,einvchange=@einvchange,einv=@einv
            ,einvstate=@einvstate,payerno=@payerno ,invrandom=@invrandom,User_Einv=@User_Einv,iTitle=@iTitle 
             WHERE SaNo =@sano COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
        }
        private void DelteOldOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.CommandText = " Delete from SaleD   where sano=@sano COLLATE Chinese_Taiwan_Stroke_BIN;";
            cmd.CommandText += "Delete from SaleBom where sano=@sano COLLATE Chinese_Taiwan_Stroke_BIN;";
            cmd.ExecuteNonQuery();
        }
        private void BackOrderdQty(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyout", tempSaleD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("qtyNotout", tempSaleD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("bomid", tempSaleD.Rows[i]["orid"].ToString());
            cmd.Parameters.AddWithValue("orno", tempSaleD.Rows[i]["orno"].ToString());
            cmd.CommandText = @"
                Update orderd set qtyout = qtyout-@qtyout, qtyNotout = qtyNotout+@qtyNotout
                Where bomid=@bomid and orno=@orno";
            cmd.ExecuteNonQuery();

            //檢查訂單是否結案
            cmd.Parameters.AddWithValue("oroverflag0", "0");
            cmd.Parameters.AddWithValue("oroverflag1", "1");
            cmd.CommandText = " Update [order] set oroverflag =@oroverflag0 where orno = @orno and (Select Count(*) from orderd where qtyNotout > 0 and orno = @orno) > 0;";
            cmd.CommandText += "Update [order] set oroverflag =@oroverflag1 where orno = @orno and (Select Count(*) from orderd where qtyNotout > 0 and orno = @orno) = 0;";
            cmd.ExecuteNonQuery();
        }
        private void ChangeCustEInv(string cuno, string einvchange)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("cuno", cuno);
                cmd.Parameters.AddWithValue("einvchange", einvchange);
                cmd.CommandText = @"update cust set 
                einv=1,einvchange=@einvchange,cux5no=7 where cuno=@cuno";
                cmd.ExecuteNonQuery();
            }
        }
        //弘恩批次借轉還
        void gotoBatRlend(SqlCommand cmd, string cuname2)
        {
            // 主檔參數
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("leno", LeNo.Text.Trim());
            cmd.Parameters.AddWithValue("ledate", Date.ToTWDate(SaDate.Text.Trim()));
            cmd.Parameters.AddWithValue("ledate1", Date.ToUSDate(SaDate.Text.Trim()));
            cmd.Parameters.AddWithValue("ledata2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("stnoi", "BOUT");
            cmd.Parameters.AddWithValue("stnamei", "借出倉庫");
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName11.Text.Trim());
            cmd.Parameters.AddWithValue("cuname2", cuname2.Trim());
            cmd.Parameters.AddWithValue("cutel", AdTel1.Text.GetUTF8(20).Trim());
            cmd.Parameters.AddWithValue("cuper1", AdPer11.Text.GetUTF8(10).Trim());
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
            cmd.Parameters.AddWithValue("lememo", SaMemo.Text.Trim());
            cmd.Parameters.AddWithValue("lememo1", Memo1.Trim());
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
            cmd.Parameters.AddWithValue("OrNo", "");
            cmd.Parameters.AddWithValue("isFromSale", "賣出");
            //
            cmd.Parameters.AddWithValue("LendNo", "");
            cmd.Parameters.AddWithValue("LendBid", "");
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

            DataTable lenoBom = dtSaleBom.Clone();
            DataTable boms = dtSaleBom.Clone();
            var rec = 1;
            var itrec = 1;
            var bomid = "";
            var qty = 0M;

            lendtemp.AcceptChanges();
            var rws = lendtemp.AsEnumerable().Where(r => r["勾選"].ToString().Trim().Length > 0);
            if (rws.Count() == 0) lendtemp.Clear();
            else
            {
                var tsort = lendtemp.Clone();
                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    if (dtSaleD.Rows[i]["leno"].ToString().Trim().Length == 0)
                        continue;

                    var itno = dtSaleD.Rows[i]["itno"].ToString().Trim();
                    var trows = rws.Where(r => r["itno"].ToString().Trim() == itno);
                    if (trows.Count() > 0)
                    {
                        for (int j = 0; j < trows.Count(); j++)
                        {
                            tsort.ImportRow(trows.ElementAt(j));
                        }
                    }
                }
                lendtemp = tsort.Copy();
                tsort.Clear();
                tsort.Dispose();
            }

            for (int i = 0; i < lendtemp.Rows.Count; i++, rec++)
            {
                //還入數量 = 未還量 - tempqty
                qty = lendtemp.Rows[i]["qtynotout"].ToDecimal("f" + Common.Q) - lendtemp.Rows[i]["tempqty"].ToDecimal("f" + Common.Q);
                lendtemp.Rows[i]["qty"] = qty.ToDecimal("f" + Common.Q);
                cmd.Parameters["qty"].Value = qty.ToDecimal("f" + Common.Q);
                //先異動未還量
                cmd.Parameters["LendNo"].Value = lendtemp.Rows[i]["leno"].ToString().Trim();
                cmd.Parameters["LendBid"].Value = lendtemp.Rows[i]["bomid"].ToString().Trim();
                cmd.Parameters["qtynotout"].Value = lendtemp.Rows[i]["tempqty"].ToDecimal("f" + Common.Q);
                cmd.CommandText = "update lendd set qtynotout=(@qtynotout) where leno=(@LendNo) and bomid=(@LendBid)";
                cmd.ExecuteNonQuery();
                //先取得組件
                boms.Clear();
                cmd.CommandText = "Select * from lendbom where bomid=(@LendBid)";
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(boms);
                }
                //再洗掉bomid
                cmd.Parameters["cono"].Value = lendtemp.Rows[i]["cono"].ToString().Trim();
                cmd.Parameters["stno"].Value = lendtemp.Rows[i]["stno"].ToString().Trim();
                cmd.Parameters["itno"].Value = lendtemp.Rows[i]["itno"].ToString().Trim();
                cmd.Parameters["itname"].Value = lendtemp.Rows[i]["itname"].ToString();
                cmd.Parameters["ittrait"].Value = lendtemp.Rows[i]["ittrait"].ToString().Trim();
                cmd.Parameters["itunit"].Value = lendtemp.Rows[i]["itunit"].ToString().Trim();
                cmd.Parameters["itpkgqty"].Value = lendtemp.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                //
                SetRow_TaxPrice1(lendtemp.Rows[i]);
                SetRow_Mny1(lendtemp.Rows[i]);
                // 
                cmd.Parameters["price"].Value = lendtemp.Rows[i]["price"].ToDecimal("f" + Common.MS);
                cmd.Parameters["prs"].Value = lendtemp.Rows[i]["prs"].ToDecimal("f3");
                cmd.Parameters["rate"].Value = lendtemp.Rows[i]["rate"].ToDecimal("f2");
                cmd.Parameters["taxprice"].Value = lendtemp.Rows[i]["taxprice"].ToDecimal("f6");
                cmd.Parameters["mny"].Value = lendtemp.Rows[i]["mny"].ToDecimal("f" + Common.TPS);
                cmd.Parameters["priceb"].Value = lendtemp.Rows[i]["priceb"].ToDecimal("f" + Common.M);
                cmd.Parameters["taxpriceb"].Value = lendtemp.Rows[i]["taxpriceb"].ToDecimal("f6");
                cmd.Parameters["mnyb"].Value = lendtemp.Rows[i]["mnyb"].ToDecimal("f" + Common.M);
                cmd.Parameters["memo"].Value = lendtemp.Rows[i]["memo"].ToString().Trim();
                cmd.Parameters["lowzero"].Value = lendtemp.Rows[i]["lowzero"].ToString().Trim();
                bomid = LeNo.Text.Trim() + rec.ToString().PadLeft(10, '0');
                lendtemp.Rows[i]["bomid"] = bomid;
                lendtemp.Rows[i]["bomrec"] = rec;
                lendtemp.Rows[i]["recordno"] = rec;
                cmd.Parameters["bomid"].Value = bomid;
                cmd.Parameters["bomrec"].Value = rec;
                cmd.Parameters["recordno"].Value = rec;
                cmd.Parameters["sltflag"].Value = lendtemp.Rows[i]["sltflag"].ToString().Trim();
                cmd.Parameters["extflag"].Value = lendtemp.Rows[i]["extflag"].ToString().Trim();
                cmd.Parameters["itdesp1"].Value = lendtemp.Rows[i]["itdesp1"].ToString();
                cmd.Parameters["itdesp2"].Value = lendtemp.Rows[i]["itdesp2"].ToString();
                cmd.Parameters["itdesp3"].Value = lendtemp.Rows[i]["itdesp3"].ToString();
                cmd.Parameters["itdesp4"].Value = lendtemp.Rows[i]["itdesp4"].ToString();
                cmd.Parameters["itdesp5"].Value = lendtemp.Rows[i]["itdesp5"].ToString();
                cmd.Parameters["itdesp6"].Value = lendtemp.Rows[i]["itdesp6"].ToString();
                cmd.Parameters["itdesp7"].Value = lendtemp.Rows[i]["itdesp7"].ToString();
                cmd.Parameters["itdesp8"].Value = lendtemp.Rows[i]["itdesp8"].ToString();
                cmd.Parameters["itdesp9"].Value = lendtemp.Rows[i]["itdesp9"].ToString();
                cmd.Parameters["itdesp10"].Value = lendtemp.Rows[i]["itdesp10"].ToString();
                cmd.Parameters["stname"].Value = lendtemp.Rows[i]["stname"].ToString().Trim();

                //
                cmd.CommandText = @"INSERT INTO [dbo].[rlendd] 
                ([leno],[ledate],[ledate1],[cono],[cuno],[stno],[emno],[xa1no],[xa1par],[itno],[itname],[ittrait],[itunit],[itpkgqty],[qty],[price],[prs]
                ,[rate],[taxprice],[mny],[priceb],[taxpriceb],[mnyb],[memo],[lowzero],[bomid],[bomrec],[recordno],[sltflag],[extflag],[itdesp1],[itdesp2],[itdesp3]
                ,[itdesp4],[itdesp5],[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10],[stname],[stnoi],[stnamei],[LendNo],[lenoid],[isFromSale])
                VALUES (@leno,@ledate,@ledate1,@cono,@cuno,@stno,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@itpkgqty,@qty,@price,@prs
                ,@rate,@taxprice,@mny,@priceb,@taxpriceb,@mnyb,@memo,@lowzero,@bomid,@bomrec,@recordno,@sltflag,@extflag,@itdesp1,@itdesp2,@itdesp3
                ,@itdesp4,@itdesp5,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@stname,@stnoi,@stnamei,@LendNo,@LendBid,@isFromSale)";
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

                    cmd.CommandText = @"INSERT INTO [dbo].[rlendbom]
                    ([leno],[bomid],[bomrec],[itno],[itname],[itunit],[itqty],[itpareprs],[itpkgqty],[itrec],[itprice],[itprs],[itmny],[itnote])
                    VALUES(@leno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote)";
                    cmd.ExecuteNonQuery();
                    lenoBom.ImportRow(boms.Rows[j]);
                    lenoBom.AcceptChanges();
                }
            }

            //主檔
            decimal[] mnys = SetRlendAllMny(ref lendtemp);
            cmd.Parameters["TaxMnyb"].Value = (mnys[0] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M);
            cmd.Parameters["TaxMny"].Value = mnys[0].ToDecimal("f" + Common.MST);
            cmd.Parameters["Tax"].Value = mnys[1].ToDecimal("f" + Common.TS);
            cmd.Parameters["TotMny"].Value = mnys[2].ToDecimal("f" + Common.MST);
            cmd.Parameters["Taxb"].Value = (mnys[1] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["TotMnyb"].Value = (mnys[2] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["recordno"].Value = lendtemp.Rows.Count;

            cmd.CommandText = @"insert into rlend (
            leno,ledate,ledate1,stno,stname,stnoi,stnamei
            ,cuno,cuname1,cuname2,cutel,cuper1,emno,emname,xa1no,xa1name,xa1par
            ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,lememo,lememo1,appdate,edtdate,appscno,edtscno
            ,recordno) values 
            (@leno,@ledate,@ledate1,@stno,@stname,@stnoi,@stnamei
            ,@cuno,@cuname1,@cuname2,@cutel,@cuper1,@emno,@emname,@xa1no,@xa1name,@xa1par
            ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@lememo,@lememo1,@appdate,@edtdate,@appscno,@edtscno
            ,@recordno) ";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"
                UPDATE lend
                SET lend.leoverflag = 1
                FROM (
	                    select lend.leno 
	                    from lendd 
	                    left join lend on lendd.leno=lend.leno group by lend.leno having MAX(lendd.qtynotout) <= 0
	                    ) A
                where lend.leno = A.leno ";
            cmd.ExecuteNonQuery();

            JBS.JS.RLend jRLend = new JBS.JS.RLend();
            jRLend.加庫存(cmd, lendtemp, lenoBom, "stno");
            jRLend.扣庫存(cmd, lendtemp, lenoBom, "stnoi");
        }
        void SetRow_TaxPrice1(DataRow row)
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
        void SetRow_Mny1(DataRow row)
        {
            var qty = row["qty"].ToDecimal("f" + Common.Q);
            var price = row["price"].ToDecimal("f" + Common.MS);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPS);

            var par = Xa1Par.Text.Trim().ToDecimal();
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPS).ToDecimal("f" + Common.M);
        }
        decimal[] SetRlendAllMny(ref DataTable temp)
        {
            var taxmny = 0M;
            var tax = 0M;
            var totmny = 0M;
            var sum = 0M;

            sum = temp.AsEnumerable().Sum(r => r["Mny"].ToDecimal("f" + Common.TPS)).ToDecimal("f" + Common.MST);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TS);
                taxmny = sum;
                totmny = (sum + tax).ToDecimal("f" + Common.MST);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                totmny = temp.AsEnumerable().Sum(r => r["qty"].ToDecimal("f" + Common.Q) * r["price"].ToDecimal("f" + Common.MS) * r["prs"].ToDecimal()).ToDecimal("f" + Common.MST);
                tax = ((totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate).ToDecimal("f" + Common.TS);
                taxmny = (totmny - tax).ToDecimal("f" + Common.MST);
            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                tax = 0;
                totmny = taxmny = sum.ToDecimal("f" + Common.MST);
            }
            return new decimal[] { taxmny, tax, totmny };
        }
        void tClear()
        {
            dtSaleD.Clear();
            tempSaleD.Clear();
            dtSaleBom.Clear();
            tempBom.Clear();
            //temprowBom.Clear();
            lendtemp.Clear();
            lend.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tClear();
            var pk = jSale.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            btnLetoSale.Enabled = false;
            jSale.upModify0<JBS.JS.Sale>(SaNo.Text.Trim());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            tClear();
            this.Close();
            this.Dispose();
        }

        void GetSystemPrice(DataRow row, int index)
        {
            string sql = "";
            var itno = row["itno"].ToString().Trim();
            var unit = row["itunit"].ToString().Trim();
            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
            string itunit = "";
            string itunitp = "";
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
                                itunit = reader["itunit"].ToString().Trim();
                                itunitp = reader["itunitp"].ToString().Trim();
                                if (unit == itunitp && unit != "")
                                {
                                    row["price"] = reader["itpricep"].ToDecimal("f" + Common.MS);
                                }
                                else
                                {
                                    row["price"] = reader["itprice"].ToDecimal("f" + Common.MS);
                                }
                                row["prs"] = "1.000";
                            }
                        }
                    }
                    if (Common.Sys_SalePrice == 2)
                    {
                        sql = " select 售價 = "
                            + " case "
                            + " when c.cuslevel = 1 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep1"
                            + " when c.cuslevel = 2 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep2"
                            + " when c.cuslevel = 3 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep3"
                            + " when c.cuslevel = 4 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep4"
                            + " when c.cuslevel = 5 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep5"
                            + " when c.cuslevel = 6 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep"
                            + " when c.cuslevel = 1  then i.itprice1"
                            + " when c.cuslevel = 2  then i.itprice2"
                            + " when c.cuslevel = 3  then i.itprice3"
                            + " when c.cuslevel = 4  then i.itprice4"
                            + " when c.cuslevel = 5  then i.itprice5"
                            + " when c.cuslevel = 6  then i.itprice"
                            + " end ,c.cudisc 折數"
                            + " from cust as c,item as i"
                            + " where itno=(@itno)"
                            + " and cuno=(@cuno)";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("@itunit", unit.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["折數"].ToDecimal("f3");
                                    row["price"] = reader["售價"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 3)
                    {
                        //類別折數(取產品建檔售價/包裝售價，折數取售價等級建檔裡的折數)
                        sql = " select s.*,i.itno,c.cuno from salgrad as s "
                            + " inner join item as i on s.kino=i.scno and itno=(@itno) "
                            + " inner join cust as c on s.x1no=c.cux1no and cuno=(@cuno) ";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) row["prs"] = reader["reprs"].ToDecimal("f3");
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 4)
                    {
                        //歷史售價(最後一次交易售價)
                        sql = " select * from saled where itno=(@itno)"
                            + " and itunit=(@itunit)"
                            + " and itpkgqty=(@itpkgqty)"
                            + " and cuno=(@cuno)"
                            + " order by sadate desc,said desc";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@itunit", unit);
                            cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["prs"].ToDecimal("f3");
                                    row["price"] = reader["price"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 5)
                    {
                        if (itunitp.Trim().Length > 0 && itunitp.Trim() == itunit)
                        {
                            sql = " select s.*,i.itno,售價="
                                + " case"
                                + " when s.regrade = 6 and i.itpkgqty = " + itpkgqty + " then i.itpricep"
                                + " when s.regrade = 1 and i.itpkgqty = " + itpkgqty + " then i.itpricep1"
                                + " when s.regrade = 2 and i.itpkgqty = " + itpkgqty + " then i.itpricep2"
                                + " when s.regrade = 3 and i.itpkgqty = " + itpkgqty + " then i.itpricep3"
                                + " when s.regrade = 4 and i.itpkgqty = " + itpkgqty + " then i.itpricep4"
                                + " when s.regrade = 5 and i.itpkgqty = " + itpkgqty + " then i.itpricep5"
                                + " end,c.cuno from salgrad as s"
                                + " inner join item as i on s.kino=i.scno and itno=(@itno)"
                                + " inner join cust as c on s.x1no=c.cux1no and cuno=(@cuno)";
                        }
                        else
                        {
                            sql = " select s.*,i.itno,售價="
                                + " case"
                                + " when s.regrade = 6  then i.itprice"
                                + " when s.regrade = 1  then i.itprice1"
                                + " when s.regrade = 2  then i.itprice2"
                                + " when s.regrade = 3  then i.itprice3"
                                + " when s.regrade = 4  then i.itprice4"
                                + " when s.regrade = 5  then i.itprice5"
                                + " end,c.cuno from salgrad as s"
                                + " inner join item as i on s.kino=i.scno and itno=(@itno)"
                                + " inner join cust as c on s.x1no=c.cux1no and cuno=(@cuno)";
                        }
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["reprs"].ToDecimal("f3");
                                    row["price"] = reader["售價"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 7)
                    {
                        //歷史報價
                        sql = " select * from quoted where itno=(@itno)"
                            + " and itunit=(@itunit)"
                            + " and itpkgqty=(@itpkgqty)"
                            + " and cuno=(@cuno)"
                            + " order by qudate desc,quid desc";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@itunit", unit);
                            cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["prs"].ToDecimal("f3");
                                    row["price"] = reader["price"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 6)
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
            var price = row["price"].ToDecimal("f" + Common.MS);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPS);

            var par = Xa1Par.Text.Trim().ToDecimal();
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPS).ToDecimal("f" + Common.M);
        }
        void SetAllMny()
        {
            var tax = 0M;
            var par = Xa1Par.Text.ToDecimal();
            var sum = dtSaleD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f"+Common.TPS)).ToDecimal("f" + Common.MST);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TS);
                TaxMny.Text = TaxMny1.Text = sum.ToString("f" + Common.MST);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = Tax1.Text = tax.ToString("f" + Common.TS);
                TotMny.Text = TotMny1.Text = (sum + tax).ToString("f" + Common.MST);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                var totmny = dtSaleD.AsEnumerable().Sum(r => r["Pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MS)).ToDecimal("f"+Common.MST);
                tax = totmny / (1 + Common.Sys_Rate) * Common.Sys_Rate;

                TotMny.Text = TotMny1.Text = totmny.ToString("f" + Common.MST);
                tax = tax.ToDecimal("f" + Common.TS);
                Tax.Text = Tax1.Text = tax.ToString();
                TaxMny.Text = TaxMny1.Text = (totmny - tax).ToString("f" + Common.MST);
                TaxMnyB.Text = (TaxMny.Text.ToDecimal() * par).ToString("f" + Common.M);
            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                TaxMny.Text = TaxMny1.Text = sum.ToString("f" + Common.MST);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = Tax1.Text = tax.ToString("f" + Common.TS);
                TotMny.Text = TotMny1.Text = sum.ToString("f" + Common.MST);
            }
            SetAcctMny();
        }
        void SetAcctMny()
        {
            //未收金額 = 應收總額-折扣金額-取用預收-已收金額
            //                                => 已收金額 = 現金金額 + 刷卡金額 + 禮卷金額
            decimal acctmny = 0;   //未收金額
            decimal totmny = 0;    //應收總額
            decimal discount = 0;  //折扣金額
            decimal getprvacc = 0; //取用預收
            decimal collectmny = 0;//已收金額
            decimal cashmny = 0;   //現金金額
            decimal cardmny = 0;   //刷卡金額
            decimal ticket = 0;    //禮卷金額
            //計算『已收金額』
            decimal.TryParse(CashMny.Text, out cashmny);
            decimal.TryParse(CardMny.Text, out cardmny);
            decimal.TryParse(Ticket.Text, out ticket);
            collectmny = cashmny + cardmny + ticket;
            CollectMny.Text = collectmny.ToString("f" + Common.MST);
            CollectMny1.Text = collectmny.ToString("f" + Common.MST);
            //計算『未收金額』
            decimal.TryParse(TotMny.Text, out totmny);
            decimal.TryParse(Discount.Text, out discount);
            decimal.TryParse(GetPrvAcc.Text, out getprvacc);
            acctmny = totmny - discount - getprvacc - collectmny;
            AcctMny.Text = acctmny.ToString("f" + Common.MST);
            AcctMny1.Text = acctmny.ToString("f" + Common.MST);
        }



        void gotoRlend(SqlCommand cmd)
        {
            DataTable lenoD = new DataTable();//借出轉銷貨檔
            DataTable lenoBom = new DataTable();//借出轉銷貨暫存檔

            string leno = "";
            JE.MyControl.TextBoxT LeNo = new JE.MyControl.TextBoxT();
            LeNo.Name = "LeNo";
            Common.JESetSSID(SqlTable.RLend, ref SaDate, ref LeNo, cmd);
            leno = LeNo.Text.Trim();

            // 主檔參數
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("leno", leno.Trim());
            cmd.Parameters.AddWithValue("ledate", Date.ToTWDate(SaDate.Text.Trim()));
            cmd.Parameters.AddWithValue("ledate1", Date.ToUSDate(SaDate.Text.Trim()));
            cmd.Parameters.AddWithValue("ledata2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("stnoi", "BOUT");
            cmd.Parameters.AddWithValue("stnamei", "借出倉庫");
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName11.Text.Trim());
            var cuname2 = "";
            jSale.Validate<JBS.JS.Cust>(CuNo.Text.Trim(), row => cuname2 = row["cuname2"].ToString());

            cmd.Parameters.AddWithValue("cuname2", cuname2.Trim());
            cmd.Parameters.AddWithValue("cutel", AdTel1.Text.GetUTF8(20).Trim());
            cmd.Parameters.AddWithValue("cuper1", AdPer11.Text.GetUTF8(10).Trim());
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
            cmd.Parameters.AddWithValue("lememo", SaMemo.Text.Trim());
            cmd.Parameters.AddWithValue("lememo1", Memo1.Trim());
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
            cmd.Parameters.AddWithValue("OrNo", "");
            cmd.Parameters.AddWithValue("LendNo", "");
            cmd.Parameters.AddWithValue("leid", "");
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
            var rows = dtSaleD.AsEnumerable().Where(r => r["leno"].ToString().Trim().Length > 0);
            if (rows.Count() > 0)
                lenoD = rows.CopyToDataTable();

            lenoBom = dtSaleBom.Clone();
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
                var bomid = leno.Trim() + rec.PadLeft(10, '0');
                cmd.Parameters["bomid"].Value = bomid;
                cmd.Parameters["bomrec"].Value = rec;
                cmd.Parameters["recordno"].Value = recordno;
                recordno += 1;
                cmd.Parameters["sltflag"].Value = rows.ElementAt(i)["sltflag"].ToString().Trim();
                cmd.Parameters["extflag"].Value = rows.ElementAt(i)["extflag"].ToString().Trim();
                cmd.Parameters["itdesp1"].Value = rows.ElementAt(i)["itdesp1"].ToString();
                cmd.Parameters["itdesp2"].Value = rows.ElementAt(i)["itdesp2"].ToString();
                cmd.Parameters["itdesp3"].Value = rows.ElementAt(i)["itdesp3"].ToString();
                cmd.Parameters["itdesp4"].Value = rows.ElementAt(i)["itdesp4"].ToString();
                cmd.Parameters["itdesp5"].Value = rows.ElementAt(i)["itdesp5"].ToString();
                cmd.Parameters["itdesp6"].Value = rows.ElementAt(i)["itdesp6"].ToString();
                cmd.Parameters["itdesp7"].Value = rows.ElementAt(i)["itdesp7"].ToString();
                cmd.Parameters["itdesp8"].Value = rows.ElementAt(i)["itdesp8"].ToString();
                cmd.Parameters["itdesp9"].Value = rows.ElementAt(i)["itdesp9"].ToString();
                cmd.Parameters["itdesp10"].Value = rows.ElementAt(i)["itdesp10"].ToString();
                cmd.Parameters["stname"].Value = rows.ElementAt(i)["stname"].ToString().Trim();
                cmd.Parameters["LendNo"].Value = rows.ElementAt(i)["leno"].ToString().Trim();
                cmd.Parameters["leid"].Value = rows.ElementAt(i)["leid"];

                cmd.CommandText = "INSERT INTO [dbo].[rlendd] "
                   + "([leno],[ledate],[ledate1],[cono],[cuno],[stno],[emno],[xa1no],[xa1par],[itno],[itname],[ittrait],[itunit],[itpkgqty],[qty],[price],[prs]"
                   + ",[rate],[taxprice],[mny],[priceb],[taxpriceb],[mnyb],[memo],[lowzero],[bomid],[bomrec],[recordno],[sltflag],[extflag],[itdesp1],[itdesp2],[itdesp3]"
                   + ",[itdesp4],[itdesp5],[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10],[stname],[stnoi],[stnamei],[LendNo],[lenoid])"
                   + "VALUES (@leno,@ledate,@ledate1,@cono,@cuno,@stno,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@itpkgqty,@qty,@price,@prs"
                   + ",@rate,@taxprice,@mny,@priceb,@taxpriceb,@mnyb,@memo,@lowzero,@bomid,@bomrec,@recordno,@sltflag,@extflag,@itdesp1,@itdesp2,@itdesp3"
                   + ",@itdesp4,@itdesp5,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@stname,@stnoi,@stnamei,@LendNo,@leid)";
                cmd.ExecuteNonQuery();

                if (rows.ElementAt(i)["leid"].ToString().Trim() != "" && rows.ElementAt(i)["leno"].ToString().Trim() != "")
                {
                    cmd.Parameters["qtynotout"].Value = rows.ElementAt(i)["qty"].ToDecimal();
                    cmd.CommandText = "update lendd set qtynotout=qtynotout-@qtynotout where leno=@LendNo and bomid=@leid";
                    cmd.ExecuteNonQuery();
                }

                //組件
                var Brows = dtSaleBom.AsEnumerable().Where(p => p["bomrec"].ToString().Trim() == rec);
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

                    cmd.CommandText = "INSERT INTO [dbo].[rlendbom]"
                    + "([leno],[bomid],[bomrec],[itno],[itname],[itunit],[itqty],[itpareprs],[itpkgqty],[itrec],[itprice],[itprs],[itmny],[itnote])"
                    + "VALUES(@leno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote)";
                    cmd.ExecuteNonQuery();
                    lenoBom.ImportRow(Brows.ElementAt(j));
                    lenoBom.AcceptChanges();
                }
            }

            //主檔 
            decimal[] mnys = SetRlendAllMny(ref lenoD);
            cmd.Parameters["TaxMnyb"].Value = (mnys[0] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M);
            cmd.Parameters["TaxMny"].Value = mnys[0].ToDecimal("f" + Common.MST);
            cmd.Parameters["Tax"].Value = mnys[1].ToDecimal("f" + Common.TS);
            cmd.Parameters["TotMny"].Value = mnys[2].ToDecimal("f" + Common.MST);
            cmd.Parameters["Taxb"].Value = (mnys[1] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["TotMnyb"].Value = (mnys[2] * Xa1Par.Text.ToDecimal()).ToDecimal("f" + Common.M); ;
            cmd.Parameters["recordno"].Value = lenoD.Rows.Count;

            cmd.CommandText = "insert into rlend ("
            + " leno,ledate,ledate1,stno,stname,stnoi,stnamei"
            + " ,cuno,cuname1,cuname2,cutel,cuper1,emno,emname,xa1no,xa1name,xa1par"
            + " ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,lememo,lememo1,appdate,edtdate,appscno,edtscno"
            + " ,recordno) values "
            + " (@leno,@ledate,@ledate1,@stno,@stname,@stnoi,@stnamei"
            + " ,@cuno,@cuname1,@cuname2,@cutel,@cuper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
            + " ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@lememo,@lememo1,@appdate,@edtdate,@appscno,@edtscno"
            + " ,@recordno) ";
            cmd.ExecuteNonQuery();

            Common.加庫存(cmd, lenoD, lenoBom, "stno");
            Common.扣庫存(cmd, lenoD, lenoBom, "stnoi");

            lenoD.Dispose();
            lenoBom.Dispose();
        }

        private void Text_Enter(object sender, EventArgs e)
        {
            //SaNo,CuNo,StNo,X3No,X3No1
            SaNo.Tag = SaNo.Text.Trim();
            CuNo.Tag = CuNo.Text.Trim();
            StNo.Tag = StNo.Text.Trim();

            if (sender.Equals(X3No) || sender.Equals(X3No1))
            {
                X3No.Tag = X3No1.Tag = (sender as TextBox).Text.Trim();
            }
        }

        private void SaNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Sale>(sender);
        }

        private void SaNo_Validating(object sender, CancelEventArgs e)
        {
            if (SaNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SaNo.TrimTextLenth() == 0 && SaNo.TextLength > 0)
            {
                e.Cancel = true;
                SaNo.Text = "";
                SaNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jSale.IsExistDocument<JBS.JS.Sale>(SaNo.Text.Trim()) == true)
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jSale.IsExistDocument<JBS.JS.Sale>(SaNo.Text.Trim()) == true)
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jSale.IsExistDocument<JBS.JS.Sale>(SaNo.Text.Trim()) == true)
                {
                    if (SaNo.Text.Trim() == (SaNo.Tag ?? "").ToString())
                        return;

                    writeToTxt(SaNo.Text.Trim());
                    loadSaleBom();
                }
                else
                {
                    e.Cancel = true;
                    jSale.Open<JBS.JS.Sale>(sender);
                    SaNo.SelectAll();

                    if (jSale.IsExistDocument<JBS.JS.Sale>(SaNo.Text.Trim()) == true)
                    {
                        writeToTxt(SaNo.Text.Trim());
                        loadSaleBom();
                    }
                }
            }
        }

        void FillCust(SqlDataReader row)
        {
            CuNo.Text = row["CuNo"].ToString().Trim();
            CuName11.Text = row["CuName1"].ToString();
            Xa1No.Text = row["CuXa1no"].ToString();
            X3No.Text = row["CuX3no"].ToString();
            X3No1.Text = row["CuX3no"].ToString();
            X4No.Text = row["CuX4no"].ToString();
            X5No.Text = row["CuX5no"].ToString();
            EmNo.Text = row["CuEmno1"].ToString();
            payerno.Text = row["payerno"].ToString().Trim() == "" ? row["CuNo"].ToString() : row["payerno"].ToString();
            jSale.Validate<JBS.JS.Cust>(payerno.Text, r => payername.Text = r["cuname1"].ToString(), () => payername.Clear());
            InvName.Text = row["CuInvoName"].ToString();

            CuAddr = row["cuaddr1"].ToString();
            CuTel1 = row["cutel1"].ToString();
            CuPer1 = row["cuper1"].ToString().GetUTF8(10);
            SpNo.Text = row["SpNo"].ToString().Trim();
            SpName.Text = row["SpName"].ToString().Trim();

            switch (row["einv"].ToString())//是否使用電子發票
            {
                case "1": 
                    EInv1.Checked = true;
                    EInvChange.Text = row["einvchange"].ToString();
                    break;
                case "2": 
                    EInv2.Checked = true;
                    EInvChange.Text = "";
                    break;
                default: break;
            }

            if (InvName.Text.Trim() == "")
                InvName.Text = row["CuName2"].ToString();
            InvTaxNo.Text = row["CuUno"].ToString();
            InvAddr1 = row["Cuaddr2"].ToString();
            CuAdvamt.Text = row["CuAdvamt"].ToDecimal("f" + CuAdvamt.LastNum).ToString();
            this.Disc = row["CuDisc"].ToDecimal();

            jSale.Validate<JBS.JS.XX03>(X3No.Text, reader =>
            {
                X3No.Text = X3No1.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = X3Name1.Text = reader["X3Name"].ToString().Trim();

                var rate = reader["X3rate"].ToDecimal() * 100;
                Rate.Text = Rate1.Text = rate.ToString("f0");
            });

            jSale.Validate<JBS.JS.Empl>(EmNo.Text, reader =>
            {
                EmNo.Text = reader["EmNo"].ToString().Trim();
                EmName.Text = reader["EmName"].ToString().Trim();
                DeNo.Text = reader["Emdeno"].ToString().Trim();
            }, () =>
            {
                EmNo.Text = "";
                EmName.Text = "";
                DeNo.Text = "";
            });

            jSale.Validate<JBS.JS.Dept>(DeNo.Text, r => DeName.Text = r["DeName1"].ToString(), () => DeName.Clear());
            jSale.Validate<JBS.JS.XX04>(X4No.Text, r => X4Name.Text = r["X4Name"].ToString(), () => X4Name.Clear());
            jSale.Validate<JBS.JS.XX05>(X5No.Text, r => X5Name.Text = r["X5Name"].ToString(), () => X5Name.Clear());
            jSale.Validate<JBS.JS.Xa01>(Xa1No.Text, r => Xa1Name.Text = r["Xa1Name"].ToString(), () => Xa1Name.Clear());
   
            if (this.FormState != FormEditState.Modify)
            {
                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    Common.GetSpecialPrice(dtSaleD.Rows[i], i, CuNo, SaDate, dataGridViewT1, GetSystemPrice);
                    SetRow_TaxPrice(dtSaleD.Rows[i]);
                    SetRow_Mny(dtSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }

            CuNo.Tag = row["CuNo"].ToString().Trim();

            填入指送地址資訊(row);//CuPer1  CuTel1 cuaddr1 AdName = 公司地址 
            rowstandard(null, 0);
        }


        private void 填入指送地址資訊(SqlDataReader row = null, string cuno = "")//如果有預設列印，即列印指派地址;否則列印CUST之資料
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                if (row != null)
                    cmd.Parameters.AddWithValue("cuno", row["cuno"]);
                else if (cuno != "")
                    cmd.Parameters.AddWithValue("cuno", cuno);
                cmd.CommandText = "select * from DeliveryAddress where cuno=@cuno and DefaultPrint = 'V'";

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        AdPer11.Text = dr["per1"].ToString().GetUTF8(10);
                        AdTel1.Text = dr["Tel"].ToString();
                        AdAddr1.Text = dr["Addr"].ToString();
                        AdName = dr["name"].ToString();       //指送公司名稱
                        //labelT1.Text = "指送地址";
                        return;
                    }
                    else if (row != null)
                    {
                        AdPer11.Text = row["CuPer1"].ToString().GetUTF8(10);
                        AdTel1.Text = row["CuTel1"].ToString();
                        AdName = row["cuname2"].ToString();
                        var addr = "cuaddr" + Common.Sys_DefaultAddr;
                        AdAddr1.Text = row[addr].ToString().Trim();
                        return;
                    }
                }

                if (cuno != "")
                {
                    cmd.CommandText = "select * from cust where cuno=@cuno";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            AdPer11.Text = dr["CuPer1"].ToString().GetUTF8(10);
                            AdTel1.Text = dr["CuTel1"].ToString();
                            AdName = dr["cuname2"].ToString();
                            var addr = "cuaddr" + Common.Sys_DefaultAddr;
                            AdAddr1.Text = dr[addr].ToString().Trim();
                        }
                    }
                }


            }
        }

        private void 填入saleD指送地址欄位資料(DataRow dRow) //小按鈕(新增，或插入)明細時
        {
            dRow["AdAddr"] = AdAddr1.Text.Trim();
            dRow["Adper1"] = AdPer11.Text.Trim();
            dRow["Adtel"] = AdTel1.Text.Trim();
            dRow["AdName"] = AdName;                //指送公司名稱
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Cust>(sender, reader =>
            {
                FillCust(reader);
            });
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly || btnCancel.Focused)
                return;

            if (CuNo.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("請先輸入客戶編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuName11.Text = AdPer11.Text = AdTel1.Text = EmNo.Text = EmName.Text = "";
                return;
            }

            jSale.ValidateOpen<JBS.JS.Cust>(sender, e, row =>
            {
                if (CuNo.Text.Trim() == (CuNo.Tag ?? "").ToString())
                    return;

                if (dtSaleD.AsEnumerable().Any(r => r["leno"].ToString().Trim().Length > 0))
                {
                    dtSaleD.Clear();
                    dtSaleBom.Clear();
                }

                FillCust(row);
            });
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Stkroom>(sender, reader => DoubleToGetStNo(reader));
        }
        void DoubleToGetStNo(SqlDataReader reader)
        {
            StNo.Text = reader["StNo"].ToString().Trim();
            StName.Text = reader["StName"].ToString().Trim();

            if (Common.Sys_StNoMode == 1)
            {
                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    dtSaleD.Rows[i]["stno"] = StNo.Text;
                    dtSaleD.Rows[i]["StName"] = StName.Text;
                    dataGridViewT1.InvalidateRow(i);
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtSaleD, i, StNo.Text.Trim(), StName.Text.Trim());
                    BatchF.BOM同步批次異動倉庫(dt_Bom_BatchProcess, dtSaleD, dtSaleBom, i);
                }
            }

            StNo.Tag = reader["StNo"].ToString().Trim();
        }
        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly || btnCancel.Focused)
                return;

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Clear();
                StName.Clear();
                e.Cancel = true;
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jSale.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
            {
                if (StNo.Text.Trim() == (StNo.Tag ?? "").ToString())
                    return;

                DoubleToGetStNo(reader);
            });
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.XX03>(sender, reader =>
            {
                X3No.Text = X3No1.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = X3Name1.Text = reader["X3Name"].ToString();

                X3No.Tag = X3No.Text;
                X3No1.Tag = X3No1.Text;

                var rate = reader["X3rate"].ToDecimal() * 100;
                Rate.Text = Rate1.Text = rate.ToString("f0");

                //完成稅別設定，重新計算金額
                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtSaleD.Rows[i]);
                    SetRow_Mny(dtSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                X3No.Tag = X3No1.Tag = reader["X3No"].ToString().Trim();
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

            jSale.ValidateOpen<JBS.JS.XX03>(sender, e, reader =>
            {
                var tb = sender as TextBox;
                if (tb.Tag != null)
                {
                    if (tb.Tag.ToString().Trim() == reader["X3No"].ToString().Trim())
                        return;
                }

                X3No.Text = X3No1.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = X3Name1.Text = reader["X3Name"].ToString();

                var rate = reader["X3rate"].ToDecimal() * 100;
                Rate.Text = Rate1.Text = rate.ToString("f0");

                //完成稅別設定，重新計算金額
                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtSaleD.Rows[i]);
                    SetRow_Mny(dtSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                X3No.Tag = X3No1.Tag = reader["X3No"].ToString().Trim();
            });
        }

        private void X5No_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.XX05>(sender, reader =>
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

            jSale.ValidateOpen<JBS.JS.XX05>(sender, e, reader =>
            {
                X5No.Text = reader["X5No"].ToString().Trim();
                X5Name.Text = reader["X5Name"].ToString().Trim();
            });


            if (X5No.Text == "7" || X5No.Text == "8")//使用
                EInv1.Checked = true;
            else
                EInv2.Checked = true;
        }

        private void X4No_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.XX04>(sender, reader =>
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

            jSale.ValidateOpen<JBS.JS.XX04>(sender, e, reader =>
            {
                X4No.Text = reader["X4No"].ToString().Trim();
                X4Name.Text = reader["X4Name"].ToString().Trim();
            }, true);

        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Empl>(sender, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
                DeNo.Text = reader["emdeno"].ToString().Trim();

                jSale.Validate<JBS.JS.Dept>(DeNo.Text, row => DeName.Text = row["dename1"].ToString(), () => DeName.Clear());
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

            jSale.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
                DeNo.Text = reader["emdeno"].ToString().Trim();

                jSale.Validate<JBS.JS.Dept>(DeNo.Text, row => DeName.Text = row["dename1"].ToString(), () => DeName.Clear());
            }, true);

        }

        private void DeNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Dept>(sender, reader =>
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

            jSale.ValidateOpen<JBS.JS.Dept>(sender, e, reader =>
            {
                DeNo.Text = reader["DeNo"].ToString().Trim();
                DeName.Text = reader["DeName1"].ToString().Trim();
            }, true);

        }

        private void SeNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Send>(sender, reader =>
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

            jSale.ValidateOpen<JBS.JS.Send>(sender, e, reader =>
            {
                SeNo.Text = reader["SeNo"].ToString().Trim();
                SeName.Text = reader["SeName"].ToString().Trim();
            }, true);

        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Spec>(sender, reader =>
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

            jSale.ValidateOpen<JBS.JS.Spec>(sender, e, reader =>
            {
                SpNo.Text = reader["SpNo"].ToString().Trim();
                SpName.Text = reader["SpName"].ToString().Trim();
            }, true);

        }

        private void SaDate_Validating(object sender, CancelEventArgs e)
        {
            if (SaDate.ReadOnly) return;
            if (btnCancel.Focused) return;
            TextBox tx = ((TextBox)sender);

            if (tx.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("日期不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!tx.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tx.Name == SaDate.Name)
            {
                if (string.CompareOrdinal(SaDate.Text, SaDateAc.Text) > 0)
                {
                    SaDateAc.Text = SaDate.Text;
                }
                else
                {
                    if (SaDateAc.Text.Trim() == "")
                        SaDateAc.Text = SaDate.Text;
                }
            }
            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                //過去的日子，以今天為準，未來的日子，以未來的銷貨日為準
                string now;
                if (Common.User_DateTime == 1)
                    now = Date.GetDateTime(1, false);
                else
                    now = Date.GetDateTime(2, false);

                if (string.CompareOrdinal(now, SaDate.Text) > 0)
                {
                    InvDate = now;
                }
            }
            //if (tx.Name == SaDate.Name)
            //{
                //if (this.FormState != FormEditState.Modify)
                //{
                //    if (CuNo.Text.Trim().Length > 0)
                //    {
                //        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                //        {
                //            Common.GetSpecialPrice(dtSaleD.Rows[i], i, CuNo, SaDate, dataGridViewT1, GetSystemPrice);
                //            SetRow_TaxPrice(dtSaleD.Rows[i]);
                //            SetRow_Mny(dtSaleD.Rows[i]);
                //            dataGridViewT1.InvalidateRow(i);
                //        }
                //        SetAllMny();
                //    }
                //}
            //}

        }

        private void Xa1Par_Validating(object sender, CancelEventArgs e)
        {
            if (Xa1Par.ReadOnly != true && Xa1Par.Text.Trim() != "" && dataGridViewT1.Rows.Count > 0)
            {
                //離開匯率設定，重新計算本幣金額
                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    SetRow_Mny(dtSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }

            if (Common.keyData != Keys.Up)
            {
                if (CuNo.Text.Trim() != "")
                    if (dataGridViewT1.Rows.Count == 0)
                        if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
        }

        private void Discount_Validating(object sender, CancelEventArgs e)
        {
            if (Discount.ReadOnly)
                return;

            if (sender.Equals(Discount))
                Discount1.Text = Discount.Text;
            if (sender.Equals(Discount1))
                Discount.Text = Discount1.Text;
            SetAcctMny();
        }

        private void GetPrvAcc_Validating(object sender, CancelEventArgs e)
        {
            if (GetPrvAcc.ReadOnly) return;
            if (btnCancel.Focused) return;

            decimal d = CuAdvamt.Text.ToDecimal();
            decimal d1 = (sender as TextBox).Text.ToDecimal();

            //if (d1 == 0)
            //    return;

            if (d1>0 && d1 > d)
            {
                e.Cancel = true;
                (sender as TextBox).Text = (0M).ToString("F" + GetPrvAcc.LastNum);
                MessageBox.Show("取用金額超出預收餘額額度", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sender.Equals(GetPrvAcc)) GetPrvAcc1.Text = GetPrvAcc.Text;
            if (sender.Equals(GetPrvAcc1)) GetPrvAcc.Text = GetPrvAcc1.Text;
            SetAcctMny();
        }

        private void CashMny_Validating(object sender, CancelEventArgs e)
        {
            if (CashMny.ReadOnly == false) SetAcctMny();
        }

        private void Tax_Validating(object sender, CancelEventArgs e)
        {
            if (Tax.ReadOnly) return;
            //營業稅額離開，可能手動調整營業稅
            //計算方法：『稅前合計』+『營業稅額』
            if (sender.Equals(Tax)) Tax1.Text = Tax.Text;
            if (sender.Equals(Tax1)) Tax.Text = Tax1.Text;

            decimal taxmny = TaxMny.Text.ToDecimal();
            decimal tax = Tax.Text.ToDecimal();
            decimal totmny = TotMny.Text.ToDecimal();

            if (Common.Sys_X3Forward == 1 && X3No.Text.Trim() == "2")
            {
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MST);
                TaxMny1.Text = TaxMny.Text;
            }
            else
            {
                TotMny.Text = (taxmny + tax).ToString("f" + Common.MST);
                TotMny1.Text = TotMny.Text;
            }

            SetAcctMny();
        }

        private void InvNo_Validating(object sender, CancelEventArgs e)
        {
            if (InvNo.ReadOnly || btnCancel.Focused) return;
            if (InvNo.TrimTextLenth() == 0)
            {
                InvNo.Clear();
                return;
            }

            if (InvNo.TrimTextLenth() != 10)
            {
                e.Cancel = true;
                MessageBox.Show("發票號碼輸入錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InvNo.SelectAll();
                return;
            }
            else
            {
                var str = InvNo.Text.Trim().ToUpper();
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

        private void SaMemo_DoubleClick(object sender, EventArgs e)
        {
            if (SaMemo.ReadOnly)
                return;

            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    SaMemo.Text = frm.Memo.GetUTF8(60);
                SaMemo.SelectAll();
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() != "")
                if (dataGridViewT1.Rows.Count == 0)
                    if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }
        //明細
        void GridSaleDAddRows()
        {
            DataRow dRow = dtSaleD.NewRow();
            dRow["itno"] = "";
            dRow["ItNoUdf"] = "";
            dRow["itname"] = "";
            dRow["PQty"] = 0;
            dRow["Punit"] = "";
            dRow["Qty"] = 0;
            dRow["itunit"] = "";
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
            //折數設定, 讀『系統檔』與『客戶檔』
            //售價取用方式若是設定『客戶等級折數』，帶客戶『折數』欄位
            //其餘帶1.00
            if (Common.Sys_SalePrice == 2)
                dRow["Prs"] = Disc;
            else dRow["Prs"] = "1.00";
            //結構編號
            dRow["BomRec"] = GetBomRec();
            //倉庫帶預設倉
            dRow["StNo"] = StNo.Text;
            dRow["StName"] = StName.Text;
            dRow["leno"] = "";
            dRow["leid"] = "";
            dRow["cyno"] = "";


            填入saleD指送地址欄位資料(dRow);

            dtSaleD.Rows.Add(dRow);
            dtSaleD.AcceptChanges();
        }

        void DeleteEmptyRow(int index)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                //刪除明細前，先刪除明細的『組件明細』
                var rec = dataGridViewT1.Rows[index].Cells["結構編號"].Value.ToString().Trim();
                jSale.RemoveBom(rec, ref dtSaleBom);

                //刪除明細
                dataGridViewT1.Rows.Remove(dataGridViewT1.Rows[index]);
                dtSaleD.AcceptChanges();

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
            DataRow dRow = dtSaleD.NewRow();
            dRow["itno"] = "";
            dRow["ItNoUdf"] = "";
            dRow["itname"] = "";
            dRow["PQty"] = 0;
            dRow["Punit"] = "";
            dRow["Qty"] = 0;
            dRow["itunit"] = "";
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
            //折數設定, 讀『系統檔』與『客戶檔』
            //售價取用方式若是設定『客戶等級折數』，帶客戶『折數』欄位
            //其餘帶1.00
            if (Common.Sys_SalePrice == 2)
                dRow["Prs"] = Disc;
            else dRow["Prs"] = "1.00";
            //結構編號
            dRow["BomRec"] = GetBomRec();
            //倉庫帶預設倉
            dRow["StNo"] = StNo.Text;
            dRow["StName"] = StName.Text;
            dRow["leno"] = "";
            dRow["leid"] = "";
            dRow["cyno"] = "";

            填入saleD指送地址欄位資料(dRow);

            dtSaleD.Rows.InsertAt(dRow, index);
            dtSaleD.AcceptChanges();
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        private void CuNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            if (CuNo.ReadOnly)
            {
                AllGridButtons.ForEach(b => b.Enabled = false);
            }
            else
            {
                AllGridButtons.ForEach(b => b.Enabled = true);
            }

            //設定唯讀欄位, 有些欄位須要讀系統參數設定
            dataGridViewT1.ReadOnly = CuNo.ReadOnly;
            dataGridViewT1.Columns["序號"].ReadOnly = true;
            dataGridViewT1.Columns["稅前售價"].ReadOnly = true;
            dataGridViewT1.Columns["產品組成"].ReadOnly = true;
            dataGridViewT1.Columns["倉庫名稱"].ReadOnly = true;
            dataGridViewT1.Columns["本幣單價"].ReadOnly = true;
            dataGridViewT1.Columns["本幣稅前單價"].ReadOnly = true;
            dataGridViewT1.Columns["本幣稅前金額"].ReadOnly = true;
            dataGridViewT1.Columns["借出憑證"].ReadOnly = true;
            dataGridViewT1.Columns["型號"].ReadOnly = true;
            //折數設定
            if (Common.Sys_KeyPrs == 1)
                dataGridViewT1.Columns["折數"].ReadOnly = false;
            else
                dataGridViewT1.Columns["折數"].ReadOnly = true;

            //74.73版單倉
            if (Common.Series == "74")
            {
                dataGridViewT1.Columns["出貨倉庫"].ReadOnly = true;
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = true;
            }
            else if (Common.Series == "73")
            {
                dataGridViewT1.Columns["出貨倉庫"].ReadOnly = true;
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = false;
            }
            else if (Common.Series == "72")
            {
                dataGridViewT1.Columns["出貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = true;
            }
            else if (Common.Series == "71")
            {
                dataGridViewT1.Columns["出貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = false;
            }

            pEInv.Enabled = EInvChange.Enabled = !CuNo.ReadOnly;//電子發票

        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["itno"].ToString().Trim();

            this.ItNoBegin = itno;
            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;
            dtSaleD.Rows[index]["ItNo"] = itno;
            dtSaleD.Rows[index]["ItName"] = reader["ItName"].ToString();
            dtSaleD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            dtSaleD.Rows[index]["Punit"] = reader["Punit"].ToString();
            dtSaleD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            //帶入產品常用倉庫
            if (reader["stno"].ToString().Trim().Length > 0)
            {
                dtSaleD.Rows[index]["stno"] = reader["stno"].ToString();
                dtSaleD.Rows[index]["stname"] = SQL.ExecuteScalar("select TOP 1 stname from stkroom where stno = @stno", new parameters("stno", reader["stno"].ToString()));
            }
            //銷貨常用單位
            var utype = reader["ItSalUnit"].ToString().Trim();
            var unit = "";
            //預設帶包裝單位或是單位
            if (utype == "1")
            {
                unit = reader["ItUnitp"].ToString();
                dtSaleD.Rows[index]["ItUnit"] = unit;

                var itpkgqty = reader["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;

                dtSaleD.Rows[index]["ItPkgQty"] = itpkgqty;
            }
            else
            {
                unit = reader["ItUnit"].ToString();
                dtSaleD.Rows[index]["ItUnit"] = unit;
                dtSaleD.Rows[index]["ItPkgQty"] = 1;
            }

            Common.GetSpecialPrice(dtSaleD.Rows[index], index, CuNo, SaDate, dataGridViewT1, GetSystemPrice);
            SetRow_TaxPrice(dtSaleD.Rows[index]);
            SetRow_Mny(dtSaleD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            //組合組裝品
            string trait = reader["ItTrait"].ToString();
            dtSaleD.Rows[index]["ItTrait"] = trait;

            if (trait == "1")
                trait = "組合品";
            else if (trait == "2")
                trait = "組裝品";
            else if (trait == "3")
                trait = "單一商品";
            dtSaleD.Rows[index]["產品組成"] = trait;
            //
            for (int x = 1; x <= 10; x++)
            {
                dtSaleD.Rows[index]["ItDesp" + x] = reader["ItDesp" + x];
            }

            //BOM
            var rec = dataGridViewT1["結構編號", index].EditedFormattedValue.ToString().Trim();
            jSale.RemoveBom(rec, ref dtSaleBom);
            jSale.GetItemBom(itno, rec, ref dtSaleBom);



            SetRow_TaxPrice(dtSaleD.Rows[index]);
            SetRow_Mny(dtSaleD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();
            rowstandard(reader["itno"].ToString().Trim(), index);//回寫對應的客戶型號
            //刪除批次異動資訊
            BatchF.刪除批次異動(dt_BatchProcess, rec);
            BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);

        }
        bool FillItemByOder(int DGVFocusRow)
        {
            using (var frm = new FrmOrderToSale())
            {
                //刪除批次異動資訊
                var Bomrec = dataGridViewT1.CurrentRow.Cells["結構編號"].EditedFormattedValue.ToString().Trim();
                frm.TSeekNo = dataGridViewT1["訂單憑證", DGVFocusRow].EditedFormattedValue.ToString().Trim();
                frm.CuNo = CuNo.Text.Trim();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.MasterRow == null || frm.dtDetail.Rows.Count == 0) return false;

                    BatchF.刪除批次異動(dt_BatchProcess, Bomrec);
                    BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, Bomrec);

                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = frm.OrNo.Trim();
                    Memo1 = frm.MasterRow["ormemo1"].ToString();
                    Xa1No.Text = frm.MasterRow["Xa1no"].ToString();
                    Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
                    Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
                    X3No.Text = frm.MasterRow["X3no"].ToString();
                    X3No1.Text = frm.MasterRow["X3no"].ToString();
                    Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
                    Rate1.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
                    SaMemo.Text = frm.MasterRow["ormemo"].ToString();
                    EmNo.Text = frm.MasterRow["EmNo"].ToString();
                    EmName.Text = frm.MasterRow["EmName"].ToString();
                    getDeNo();
                    SpNo.Text = frm.MasterRow["SpNo"].ToString();
                    SpName.Text = frm.MasterRow["SpName"].ToString();
                    pVar.XX03Validate(X3No.Text, X3No, X3Name);
                    pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);
                    AdAddr1.Text = frm.MasterRow["AdAddr"].ToString();
                    AdTel1.Text = frm.MasterRow["cutel1"].ToString();
                    AdPer11.Text = frm.MasterRow["cuper1"].ToString();
                    SaPayment.Text = frm.MasterRow["orpayment"].ToString();
                    CardNo.Text = frm.MasterRow["CardNo"].ToString();
                    PhotoPath = frm.MasterRow["PhotoPath"].ToString();//13.7c
                    CuName11.Text = frm.MasterRow["CuName1"].ToString();

                    jSale.RemoveBom(dtSaleD.Rows[DGVFocusRow]["bomrec"].ToString().Trim(), ref dtSaleBom);
                    dtSaleD.Rows[DGVFocusRow].Delete();
                    dtSaleD.AcceptChanges();

                    //明細部分&組件
                    DataRow row1, row2;

                    for (int i = 0; i < frm.dtDetail.Rows.Count; i++)
                    {
                        row1 = dtSaleD.NewRow();
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
                        row1["orno"] = frm.OrNo.Trim();
                        row1["orid"] = row2["bomid"].ToString();
                        row1["itno"] = row2["itno"].ToString();
                        row1["itname"] = row2["itname"].ToString();
                        row1["itunit"] = row2["itunit"].ToString();
                        row1["itnoudf"] = row2["itnoudf"].ToString();
                        row1["Punit"] = row2["Punit"].ToString();
                        row1["qty"] = row2["qtynotout"].ToDecimal("f" + Common.Q);
                        if (Common.Sys_DBqty == 1)
                            row1["Pqty"] = row2["qtynotout"].ToDecimal("f" + Common.Q);
                        else
                            row1["Pqty"] = row2["pqty"].ToDecimal("f" + Common.Q);
                        row1["price"] = row2["price"].ToDecimal("f" + Common.MS);
                        row1["prs"] = row2["prs"].ToDecimal("f3");
                        row1["taxprice"] = row2["taxprice"].ToDecimal("f6");
                        row1["memo"] = row2["memo"].ToString();
                        row1["priceb"] = row2["priceb"].ToDecimal("f" + Common.M);
                        row1["taxpriceb"] = row2["taxpriceb"].ToDecimal("f6");
                        row1["itpkgqty"] = row2["itpkgqty"].ToDecimal("f" + Common.Q);
                        帶入倉庫資訊(row1, row2);
                        row1["mwidth1"] = row2["mwidth1"].ToDecimal("f" + Common.Q);
                        row1["mwidth2"] = row2["mwidth2"].ToDecimal("f" + Common.Q);
                        row1["mwidth3"] = row2["mwidth3"].ToDecimal("f" + Common.Q);
                        row1["mwidth4"] = row2["mwidth4"].ToDecimal("f" + Common.Q);
                        row1["pformula"] = row2["pformula"].ToString();
                        row1["standard"] = row2["standard"].ToString();//訂單憑證-型號
                        row1["AdAddr"] = row2["AdAddr"].ToString();
                        row1["Adper1"] = row2["Adper1"].ToString();
                        row1["Adtel"] = row2["Adtel"].ToString();
                        row1["AdName"] = row2["AdName"].ToString();
                        row1["NetNo"] = frm.MasterRow["NetNo"].ToString();

                        for (int j = 1; j <= 10; j++)
                        {
                            row1["itdesp" + j] = row2["itdesp" + j].ToString();
                        }
                        row1["BomRec"] = GetBomRec();
                        SetRow_Mny(row1);
                        dtSaleD.Rows.InsertAt(row1, dtSaleD.Rows.Count);
                        dtSaleD.AcceptChanges();


                        //組件部分
                        if (row2["ittrait"].ToDecimal() == 3)
                            continue;

                        var bomid = row2["bomid"].ToString().Trim();
                        var rec = row1["BomRec"].ToString().Trim();

                        jSale.GetTBom<JBS.JS.Order>(bomid, rec, ref dtSaleBom);
                        dataGridViewT1.InvalidateRow(i);
                    }

                    dataGridViewT1.CurrentCell = dataGridViewT1["訂單憑證", DGVFocusRow];
                    dataGridViewT1.CurrentRow.Selected = true;
                    SetAllMny();
                    OrderToSale庫存與成本控管(dtSaleD);
                    return true;
                }
                else
                    return false;
            }
        }
        private void rowstandard(string p, int index)
        {
            try
            {
                dtstandard.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", p);
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim().ToString());
                    if (p == null)
                    {
                        for (int i = 0; i < dataGridViewT1.RowCount; i++)
                        {
                            dtstandard.Clear();
                            cmd.Parameters["itno"].Value = dataGridViewT1.Rows[i].Cells["產品編號"].Value.ToString();
                            cmd.CommandText = " select * from standard where itno=@itno and cfno=@cuno";
                            da.Fill(dtstandard);
                            if (dtstandard.Rows.Count != 0)
                                dtSaleD.Rows[i]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                            else
                                dtSaleD.Rows[i]["standard"] = "";
                        }
                    }
                    else
                    {
                        cmd.CommandText = " select * from standard where itno=@itno and cfno=@cuno";
                        da.Fill(dtstandard);
                        if (dtstandard.Rows.Count != 0)
                            dtSaleD.Rows[index]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                        else
                            dtSaleD.Rows[index]["standard"] = "";
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SaNo.ReadOnly) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0 ) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;

            if (稽核寄庫_修改(CurrentColumnName, e.RowIndex, e.ColumnIndex)) return;

            if (CurrentColumnName == "訂單憑證")
            {
                #region 訂單憑證
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改 
                    if (dtSaleD.Rows[e.RowIndex]["leno"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借出轉入, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                FillItemByOder(e.RowIndex);
                #endregion
            }
            else if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改 
                    if (dtSaleD.Rows[e.RowIndex]["leno"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借出轉入, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jSale.DataGridView_ItNoMultipleOpen(sender, e.RowIndex,
                    () =>
                    {
                        gridDelete.Focus();
                        gridDelete_Click(null, null);
                    },
                    add =>
                    {
                        GridSaleDInsertRows(add);
                    },
                    (sqlreader, index) => FillItem(sqlreader, index));
                #endregion
            }
            else if (CurrentColumnName == "出貨倉庫")
            {
                #region 出貨倉庫
                jSale.DataGridViewOpen<JBS.JS.Stkroom>(sender, e, dtSaleD, row =>
                {
                    dtSaleD.Rows[e.RowIndex]["stno"] = row["stno"].ToString();
                    dtSaleD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtSaleD, e.RowIndex, row["StNo"].ToString(), row["StName"].ToString());
                    BatchF.BOM同步批次異動倉庫(dt_Bom_BatchProcess, dtSaleD, dtSaleBom, e.RowIndex);
                });
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改 
                    if (dtSaleD.Rows[e.RowIndex]["leno"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借出轉入, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridViewT1["借出憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由借出轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itno = dtSaleD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jSale.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            dtSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            dtSaleD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;
                dtSaleD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)//1代表一般進銷存
                {
                    Common.GetSpecialPrice(dtSaleD.Rows[e.RowIndex], e.RowIndex, CuNo, SaDate, dataGridViewT1, GetSystemPrice);
                    SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                    SetRow_Mny(dtSaleD.Rows[e.RowIndex]);

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

                        dtSaleD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
            else if (CurrentColumnName == "銷貨數量")
            {
                #region 銷貨數量
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改 
                    if (dtSaleD.Rows[e.RowIndex]["leno"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借出轉入, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (Common.Sys_DBqty == 1)
                {
                    using (var frm = new FrmComputer())
                    {
                        frm.w1 = dtSaleD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtSaleD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtSaleD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtSaleD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtSaleD.Rows[e.RowIndex]["Pformula"].ToString();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtSaleD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtSaleD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtSaleD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtSaleD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtSaleD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtSaleD.Rows[e.RowIndex]["Qty"] = frm.resultCount.ToString("f" + Common.Q);
                            dtSaleD.Rows[e.RowIndex]["PQty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
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
                        frm.w1 = dtSaleD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtSaleD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtSaleD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtSaleD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtSaleD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = dtSaleD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "銷貨數量";

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtSaleD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtSaleD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtSaleD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtSaleD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtSaleD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;
                            dtSaleD.Rows[e.RowIndex]["qty"] = frm.qty;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtSaleD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            SetAllMny();
                        }
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "售價")
            {
                #region 售價
                if (dtSaleD.Rows[e.RowIndex]["itno"].ToString().Trim() == "") return;
                using (var frm = new FrmItemLevelb())
                {
                    frm.TSeekNo = dtSaleD.Rows[e.RowIndex]["itno"].ToString().Trim();
                    frm.itunit = dtSaleD.Rows[e.RowIndex]["itunit"].ToString().Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Result.ToDecimal().ToString("f" + Common.MS);
                        dtSaleD.Rows[e.RowIndex]["price"] = frm.Result.ToDecimal("f" + Common.MS);
                        SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                        SetRow_Mny(dtSaleD.Rows[e.RowIndex]);

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
                    if (dataGridViewT1.EditingControl != null)
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
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
                            dtSaleD.Rows[e.RowIndex]["punit"] = frm.Result;
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "地址")
            {
                #region 指送地址
                using (指送地址 frm = new 指送地址())
                {
                    frm.cuno = CuNo.Text.Trim();
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            dtSaleD.Rows[dataGridViewT1.CurrentCell.RowIndex]["AdAddr"] = frm.addr.ToString().Trim().GetUTF8(60);
                            dtSaleD.Rows[dataGridViewT1.CurrentCell.RowIndex]["Adper1"] = frm.per.ToString().Trim().GetUTF8(10);
                            dtSaleD.Rows[dataGridViewT1.CurrentCell.RowIndex]["Adtel"] = frm.tel.ToString().Trim().GetUTF8(20);
                            dtSaleD.Rows[dataGridViewT1.CurrentCell.RowIndex]["AdName"] = frm.name.ToString().Trim().GetUTF8(50);
                            dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
                            break;
                        case DialogResult.Cancel: break;
                    }
                }
                #endregion
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
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "訂單憑證")
            {
                TextBefore = dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "銷貨數量")
            {
                TextBefore = dataGridViewT1["銷貨數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
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

                jSale.Validate<JBS.JS.Item>(ItNoBegin, reader =>
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

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;

            var flag = 稽核寄庫_修改(CurrentColumnName, e.RowIndex, e.ColumnIndex);
            if (flag)
            {
                e.Cancel = true;
                return;
            }

            if (CurrentColumnName == "訂單憑證")
            {
                #region 訂單憑證
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改
                    var orrrno = dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    if (dtSaleD.Rows[e.RowIndex]["leno"].ToString().Trim().Length > 0 && orrrno != TextBefore.ToString().Trim())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借出轉入, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtSaleD.Rows[e.RowIndex]["orno"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                //

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() == TextBefore) return;
                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() == "")
                {
                    dtSaleD.Rows[e.RowIndex]["orid"] = "";
                    dtSaleD.Rows[e.RowIndex]["orno"] = "";
                    dtSaleD.Rows[e.RowIndex]["NetNo"] = "";
                    return;
                }

                if (FillItemByOder(e.RowIndex) == false)
                    e.Cancel = true;

                #endregion
            }
            else if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,產品不能改 
                    if (dtSaleD.Rows[e.RowIndex]["leno"].ToString().Trim().Length > 0 && ItNoNow != TextBefore.ToString().Trim())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借出轉入, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtSaleD.Rows[e.RowIndex]["itno"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim().Length > 0 && ItNoNow != TextBefore.ToString().Trim())
                {
                    if (ItNoNow == ItNoBegin)
                        return;

                    e.Cancel = true;
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    dtSaleD.Rows[e.RowIndex]["itno"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (ItNoNow == "" || ItNoNow.Trim().Length == 0)
                {
                    if (btnSave.Focused) return;
                    dtSaleD.Rows[e.RowIndex]["orno"] = "";
                    dtSaleD.Rows[e.RowIndex]["orid"] = "";
                    dtSaleD.Rows[e.RowIndex]["itno"] = "";
                    dtSaleD.Rows[e.RowIndex]["ItNoUdf"] = "";
                    dtSaleD.Rows[e.RowIndex]["itname"] = "";
                    dtSaleD.Rows[e.RowIndex]["itunit"] = "";
                    dtSaleD.Rows[e.RowIndex]["Punit"] = "";
                    dtSaleD.Rows[e.RowIndex]["qty"] = 0;
                    dtSaleD.Rows[e.RowIndex]["Pqty"] = 0;
                    dtSaleD.Rows[e.RowIndex]["Price"] = 0;
                    dtSaleD.Rows[e.RowIndex]["TaxPrice"] = 0;
                    dtSaleD.Rows[e.RowIndex]["Mny"] = 0;
                    dtSaleD.Rows[e.RowIndex]["ItPkgQty"] = 1;
                    dtSaleD.Rows[e.RowIndex]["ItTrait"] = 0;
                    dtSaleD.Rows[e.RowIndex]["產品組成"] = "";
                    dtSaleD.Rows[e.RowIndex]["Memo"] = "";
                    dtSaleD.Rows[e.RowIndex]["PriceB"] = 0;
                    dtSaleD.Rows[e.RowIndex]["TaxPriceB"] = 0;
                    dtSaleD.Rows[e.RowIndex]["MnyB"] = 0;
                    dtSaleD.Rows[e.RowIndex]["StNo"] = StNo.Text;
                    dtSaleD.Rows[e.RowIndex]["StName"] = StName.Text;
                    dtSaleD.Rows[e.RowIndex]["mwidth1"] = 0;
                    dtSaleD.Rows[e.RowIndex]["mwidth2"] = 0;
                    dtSaleD.Rows[e.RowIndex]["mwidth3"] = 0;
                    dtSaleD.Rows[e.RowIndex]["mwidth4"] = 0;
                    dtSaleD.Rows[e.RowIndex]["Pformula"] = "";
                    dtSaleD.Rows[e.RowIndex]["leno"] = "";
                    dtSaleD.Rows[e.RowIndex]["leid"] = "";
                    dtSaleD.Rows[e.RowIndex]["cyno"] = "";
                    //折數
                    dtSaleD.Rows[e.RowIndex]["Prs"] = (Common.Sys_SalePrice == 2) ? Disc : 1;

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = dtSaleD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jSale.RemoveBom(rec, ref dtSaleBom);
                    //刪除批次異動資訊
                    BatchF.刪除批次異動(dt_BatchProcess, rec);
                    BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);
                    return;
                }

                //值沒變->離開
                if (ItNoNow == ItNoBegin)
                    return;

                //值有變，但是跟自定編號一樣，視同沒變->離開 //把『自定編號』改成『產品編號』
                if (ItNoNow != ItNoBegin)
                {
                    if (ItNoNow == UdfNoBegin)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = ItNoBegin;

                        dtSaleD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                //值變了，跟產品編號和自定編號都不一樣,帶值出來 //若找不到這筆資料則開窗
                if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)
                {
                    jSale.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtSaleD, row => FillItem(row, e.RowIndex));
                }
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    if (dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim() != TextBefore)
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtSaleD.Rows[e.RowIndex]["itunit"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    return;
                }

                if (dataGridViewT1["借出憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    if (dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim() != TextBefore)
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借出轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtSaleD.Rows[e.RowIndex]["itunit"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    return;
                }

                string itno = dtSaleD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                jSale.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                dtSaleD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    Common.GetSpecialPrice(dtSaleD.Rows[e.RowIndex], e.RowIndex, CuNo, SaDate, dataGridViewT1, GetSystemPrice);
                    SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                    SetRow_Mny(dtSaleD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
                #endregion
            }
            else if (CurrentColumnName == "銷貨數量")
            {
                #region 銷貨數量
                var qty = dataGridViewT1["銷貨數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷,數量不能改
                    if (dtSaleD.Rows[e.RowIndex]["leno"].ToString().Trim().Length > 0 && qty != TextBefore.ToDecimal())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借出轉入, 無法修改!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtSaleD.Rows[e.RowIndex]["Qty"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                if (Common.Sys_DBqty == 1)
                {
                    dtSaleD.Rows[e.RowIndex]["Qty"] = qty;
                    dtSaleD.Rows[e.RowIndex]["PQty"] = qty;
                }
                else if (Common.Sys_DBqty == 2)
                {
                    dtSaleD.Rows[e.RowIndex]["Qty"] = qty;
                }

                var itno = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var stno = dataGridViewT1["出貨倉庫", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var ItTrait = dataGridViewT1["ItTrait", e.RowIndex].Value.ToString().Trim();
                var BomRec = dtSaleD.Rows[e.RowIndex]["BomRec"].ToString().Trim();
                
                using (DataTable Bom = jSale.GetBomQtyTable(dtSaleD, dtSaleBom))
                using (DataTable TempBom = jSale.GetBomQtyTable(tempSaleD, tempBom))
                {
                    jSale.IsLowStock(itno, stno, ItTrait, BomRec, dtSaleD, dtSaleBom, Bom, tempSaleD, TempBom, this.FormState );
                }
                
                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "計價數量")
            {
                #region 計價數量
                dtSaleD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "售價")
            {
                #region 售價
                dtSaleD.Rows[e.RowIndex]["Price"] = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);
                dataGridViewT1.InvalidateRow(e.RowIndex);

                if (Common.Sys_LowCost != 3 && dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() != "")
                    pVar.CheckLowCost(dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["單位", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS));

                SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "折數")
            {
                #region 折數
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                dtSaleD.Rows[e.RowIndex]["Prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue;

                SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "稅前金額")
            {
                #region 稅前金額
                //正常情形『稅前金額』是由『售價』帶出來的
                //下面處理的情形是手動打上『稅前金額』
                //所以須往前推算『售價』金額。
                decimal price = 0;
                decimal qty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前售價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f" + Common.TPS);
                decimal prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToString().ToDecimal();
                qty = (qty == 0) ? 1 : qty;
                if (TextBefore.ToDecimal() == mny) return;

                dtSaleD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MS);
                        dtSaleD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MS);
                        dtSaleD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);

                taxprice = dtSaleD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                dtSaleD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                dtSaleD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                dtSaleD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "出貨倉庫")
            {
                #region 出貨倉庫
                jSale.DataGridViewValidateOpen<JBS.JS.Stkroom>(sender, e, dtSaleD, reader =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = reader["stno"].ToString().Trim();

                    dtSaleD.Rows[e.RowIndex]["stno"] = reader["stno"].ToString().Trim();
                    dtSaleD.Rows[e.RowIndex]["StName"] = reader["StName"].ToString().Trim();
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtSaleD, e.RowIndex, dtSaleD.Rows[e.RowIndex]["StNo"].ToString(), dtSaleD.Rows[e.RowIndex]["StName"].ToString());
                    BatchF.BOM同步批次異動倉庫(dt_Bom_BatchProcess, dtSaleD, dtSaleBom, e.RowIndex);
                });
                #endregion
            }
            else if (CurrentColumnName == "包裝數量")
            {
                #region 包裝數量
                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    decimal itpkgqty = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                    if (itpkgqty != TextBefore.ToDecimal())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                else if (dataGridViewT1["借出憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    decimal itpkgqty = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                    if (itpkgqty != TextBefore.ToDecimal())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由借出轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                else
                {
                    dtSaleD.Rows[e.RowIndex]["itpkgqty"] = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
        }

        private void OrderToSale庫存與成本控管(DataTable dtSaleD)
        {
            using (DataTable Bom = jSale.GetBomQtyTable(dtSaleD, dtSaleBom))
            using (DataTable TempBom = jSale.GetBomQtyTable(tempSaleD, tempBom))
            {
                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    var itno = dtSaleD.Rows[i]["itno"].ToString().Trim();
                    var stno = dtSaleD.Rows[i]["stno"].ToString().Trim();
                    var Price = dtSaleD.Rows[i]["Price"].ToDecimal("f" + Common.MS);
                    var ItUnit = dtSaleD.Rows[i]["ItUnit"].ToString().Trim();
                    var ItTrait = dtSaleD.Rows[i]["ItTrait"].ToString().Trim();
                    var BomRec = dtSaleD.Rows[i]["BomRec"].ToString().Trim();
                    if (itno.Length > 0)
                    {
                        //庫存控管
                        jSale.IsLowStock(itno, stno, ItTrait, BomRec, dtSaleD, dtSaleBom, Bom, tempSaleD, TempBom, this.FormState);

                        //成本控管  
                        if (Common.Sys_LowCost != 3)
                            pVar.CheckLowCost(itno, ItUnit, Price);
                    }
                }
            }
        }

        private bool 稽核寄庫_修改(string CurrentColumnName, int RowIndex, int ColumnIndex)
        {
            bool 此銷貨明細有寄庫 = false;
            #region 判斷該項目是否有寄庫　inqty!=0
            if (此銷貨單有寄庫)
            {
                var BomId = dtSaleD.Rows[RowIndex]["BomId"].ToString();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Cancel();
                    cmd.Parameters.AddWithValue("BomId", BomId);
                    cmd.CommandText = "select count(*) from InStkD where BomId =@BomId and inqty != 0 and BomId!=''";
                    if (cmd.ExecuteScalar().ToInteger() >= 1)
                        此銷貨明細有寄庫 = true;
                }
            }
            #endregion
            #region 稽核欄位修改
            if (此銷貨明細有寄庫)
            {
                if (CurrentColumnName == "產品編號" || CurrentColumnName == "單位" || CurrentColumnName == "銷貨數量" || CurrentColumnName == "包裝數量")
                {
                    MessageBox.Show("此列明細有轉寄庫，" + CurrentColumnName + "無法修改。");
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    string columnStr = "";
                    switch (CurrentColumnName)
                    {
                        case "產品編號": columnStr = "itno"; break;
                        case "單位": columnStr = "itunit"; break;
                        case "銷貨數量": columnStr = "qty"; break;
                        case "包裝數量": columnStr = "itpkgqty"; break;
                    }

                    if (CurrentColumnName == "銷貨數量")
                        dtSaleD.Rows[RowIndex][columnStr] = TextBefore.ToDecimal();
                    else
                        dtSaleD.Rows[RowIndex][columnStr] = TextBefore;
                    dataGridViewT1.InvalidateRow(RowIndex);
                    return true;
                }
            }
            #endregion

            return false;
        }



        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() == "")
            {
                CuNo.Focus();
                MessageBox.Show("客戶編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridSaleDAddRows();
                string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "訂單憑證";
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
                #region 稽核是否可刪除該項目，如此銷貨單曾經寄庫過即無法刪除此項目。
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("SaNo", SaNo.Text);
                    cmd.CommandText = "select count(*) from InStkD where SaNo =@SaNo";
                    if (cmd.ExecuteScalar().ToInteger() >= 1)
                    {
                        MessageBox.Show("此銷貨單存在寄庫紀錄，無法刪除。");
                        return;
                    }
                }
                #endregion

                if (Common.Sys_LendToSaleMode == 2)
                {
                    //非一般模式借轉銷
                    var idx = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (idx == -1)
                    {
                        dataGridViewT1.Focus();
                        return;
                    }

                    if (dtSaleD.Rows[idx]["leno"].ToString().Trim().Length > 0)
                    {
                        MessageBox.Show("此筆資料由借出轉入, 無法刪除!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //刪除明細前，先刪除明細的『組件明細』
                string rec = dataGridViewT1.CurrentRow.Cells["結構編號"].EditedFormattedValue.ToString().Trim();
                jSale.RemoveBom(rec, ref dtSaleBom);

                //刪除明細
                int index = dataGridViewT1.SelectedRows[0].Index;
                dtSaleD.Rows.RemoveAt(index);
                dtSaleD.AcceptChanges();

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                }
                SetAllMny();

                //刪除批次異動資訊
                BatchF.刪除批次異動(dt_BatchProcess, rec);
                BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);
                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "訂單憑證";
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
                if (dtSaleD.AsEnumerable().All(r => r["itno"].ToString().Trim().Length >= 0))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    GridSaleDInsertRows(index);
                    string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "訂單憑證";
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
                frm.dr = dtSaleD.Rows[index];
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
                    frm.FormName = "SaleBom";
                    string rec = dataGridViewT1.SelectedRows[0].Cells["結構編號"].Value.ToString();
                    DataTable table = dtSaleBom.Clone();

                    for (int i = 0; i < dtSaleBom.Rows.Count; i++)
                    {
                        if (dtSaleBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                        {
                            table.ImportRow(dtSaleBom.Rows[i]);
                            dtSaleBom.Rows.RemoveAt(i--);
                        }
                    }

                    table.AcceptChanges();
                    dtSaleBom.AcceptChanges();

                    frm.dtD = table.Copy();
                    frm.BomRec = rec;
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.grid = dataGridViewT1;

                    #region 批次資料傳遞
                    int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
                    //勾稽明細
                    string BomRec = dtSaleD.Rows[Index_gv]["BomRec"].ToString();
                    DataTable temp = dt_Bom_BatchProcess.Clone();
                    for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
                    {
                        if (dt_Bom_BatchProcess.Rows[i]["BomRec"].ToString() == BomRec)
                        {
                            dt_Bom_BatchProcess.Rows[i]["BomRec"] = BomRec;
                            temp.ImportRow(dt_Bom_BatchProcess.Rows[i]);
                        }
                    }

                    frm.上層Row = dtSaleD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                    frm.dt_Bom_BatchProcess = temp;
                    #endregion

                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:

                            //先刪除
                            for (int i = dt_Bom_BatchProcess.Rows.Count - 1; i >= 0; i--)
                            {
                                if (dt_Bom_BatchProcess.Rows[i]["BomRec"].ToString() == BomRec)
                                    dt_Bom_BatchProcess.Rows.RemoveAt(i);
                            }
                            //後加入
                            dt_Bom_BatchProcess.Merge(frm.dt_Bom_BatchProcess);


                            if (frm.CallBack == "Money")
                            {
                                dtSaleBom.Merge(frm.dtD);
                                dtSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MS);
                                dataGridViewT1.InvalidateRow(dataGridViewT1.SelectedRows[0].Index);
                                dataGridViewT1.Focus();
                                SetRow_TaxPrice(dtSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetRow_Mny(dtSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetAllMny();
                                break;
                            }
                            else
                            {
                                dtSaleBom.Merge(frm.dtD);
                                dtSaleBom.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            dtSaleBom.Merge(table);
                            dtSaleBom.AcceptChanges();
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
                using (FrmSale_Stock frm = new FrmSale_Stock())
                {
                    frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                    frm.ShowDialog();
                    dataGridViewT1.Focus();
                }
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
            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶此產品交易 frm = new S2.Frm該客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
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
            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶此產品交易 frm = new S2.Frm所有客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridItBuyPrice_Click(object sender, EventArgs e)
        {
            gridItBuyPrice.Focus();

            if (jSale.EnableBShopPrice() == false)
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
            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridCustSale_Click(object sender, EventArgs e)
        {
            gridCustSale.Focus();
            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("客戶編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuNo.Focus();
                return;
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var itno = (index == -1) ? "" : dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史交易 frm = new S2.Frm該客戶歷史交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridItemQuote_Click(object sender, EventArgs e)
        {
            gridItemQuote.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史報價 frm = new S2.Frm該客戶歷史報價())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridAllTransTrans_Click(object sender, EventArgs e)
        {
            gridAllTransTrans.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶所有產品 frm = new S2.Frm所有客戶所有產品())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridCost_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dtSaleD.AsEnumerable().Any(r => r["itno"].ToString().Trim() == ""))
            {
                MessageBox.Show("明細尚有產品編號未輸入，請登打完畢後再進行查詢");
                return;
            }

            if (btnAppend.Enabled)
            {
                dtSaleBom.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@SaNo", SaNo.Text.Trim());
                    cmd.CommandText = "select * from SaleBom where SaNo=@SaNo COLLATE Chinese_Taiwan_Stroke_BIN";

                    da.Fill(dtSaleBom);
                }
            }

            gridCost.Focus();
            if (dataGridViewT1.SelectedRows.Count > 0)
            {
                using (S2.Frm成本與毛利分析 frm = new S2.Frm成本與毛利分析(SaDate.Text))
                {
                    
                    frm.dtD = dtSaleD.Copy();
                    frm.bom = dtSaleBom.Copy();
                    frm.date = Date.ToTWDate(SaDate.Text);
                    frm.ShowDialog();
                }
            }
            dataGridViewT1.Focus();
        }

        private void gridAddress_Click(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Cust>(CuNo, reader =>
            {
                SaMemo.Text = reader["CuNo"].ToString() + " ";
                SaMemo.Text += reader["CuName1"].ToString() + " ";
                SaMemo.Text += reader["CuAddr3"].ToString();
            });
        }

        private void gridKeyMan_Click(object sender, EventArgs e)
        {
            if (SaNo.Text.Trim() == "")
                return;

            using (FrmSale_AppScNo frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jSale.keyMan.AppendMan;
                frm.ATime = jSale.keyMan.AppendTime;
                //修改人員
                frm.EName = jSale.keyMan.EditMan;
                frm.ETime = jSale.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void gridInvNo_Click(object sender, EventArgs e)
        {
            var sano = SaNo.Text.Trim();
            var IsPOS = (Bracket.Text.Trim() == "前台") ? true : false;
            using (FrmSale_Inv frm = new FrmSale_Inv("Sale", sano, IsPOS))
            {
                frm.ivno = InvNo.Text.Trim();
                frm.Result[0] = InvDate;
                frm.Result[1] = InvName.Text.Trim();
                frm.Result[2] = InvTaxNo.Text.Trim();
                frm.Result[3] = InvAddr1;

                //媒體申報
                if (invkind != "")
                    frm.InvKind.SelectedIndex = frm.InvKind.FindString(invkind);
                else
                {
                    if (X5No.Text.Trim() == "1")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("31 銷項三聯式");
                    else if (X5No.Text.Trim() == "2")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("32 銷項二聯式");
                    else if (X5No.Text.Trim() == "3")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("32 銷項二聯式收銀機統一發票");
                    else if (X5No.Text.Trim() == "4")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("35 銷項三聯式收銀機統一發票");
                    else if (X5No.Text.Trim() == "5")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("36 銷項免用統一發票");
                    else if (X5No.Text.Trim() == "7")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("35 銷項一般稅額計算之電子發票");
                    else if (X5No.Text.Trim() == "8")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString("37 特種稅額之銷項憑證(含特種稅額計算之電子發票");
                }

                if (specialtax != "")
                    frm.SpecialTax.SelectedIndex = frm.SpecialTax.FindString(specialtax);

                if (passmode != "")
                    frm.PassMode.SelectedIndex = frm.PassMode.FindString(passmode);

                if (X3No.Text.Trim() != "3")
                    frm.PassMode.Enabled = false;
                else
                {
                    frm.PassMode.Enabled = true;
                    if (passmode == "")
                        frm.PassMode.SelectedIndex = 0;
                }

                if (DialogResult.OK == frm.ShowDialog())
                {
                    InvNo.Text = frm.ivno;
                    InvDate = frm.Result[0];
                    InvName.Text = frm.Result[1];
                    InvTaxNo.Text = frm.Result[2];
                    InvAddr1 = frm.Result[3];

                    //媒體申報
                    invkind = frm.InvKind.Text.GetUTF8(20);

                    if (invkind.Substring(0, 2) != "37")
                        specialtax = "";
                    else
                        specialtax = frm.SpecialTax.Text.Substring(0, 1);

                    if (frm.PassMode.Text != "")
                        passmode = frm.PassMode.Text.Substring(0, 1);
                    else
                        passmode = "";
                }
            }
        }

        private void gridInvMode_Click(object sender, EventArgs e)
        {
            gridInvMode.Text = gridInvMode.Text == "批開" ? "" : "批開";

            gridAutoInv.Visible = gridInvMode.Text != "批開";
        }

        private void gridAutoInv_Click(object sender, EventArgs e)
        {
            gridAutoInv.Text = gridAutoInv.Text == "手動" ? "自動" : "手動";
            if (gridAutoInv.Text == "自動") gridInvMode.Text = "";
        }

        int GetGridInvMode()
        {
            return (gridInvMode.Text == "批開") ? 1 : 2;
        }

        private void QuNo_DoubleClick(object sender, EventArgs e)
        {
            if (QuNo.ReadOnly)
                return;

            QuNo.CausesValidation = false;

            using (var frm = new subMenuFm_1.FrmQuoteToSale())
            {
                frm.CuNo = CuNo.Text.Trim();
                frm.SeekNo = QuNo.Text.Trim();

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                jSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtSaleD, ref dtSaleBom, SetAllMny);

                this.PassQuoteM(frm);

                this.PassQuoteD(frm);

                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                    dataGridViewT1.Focus();
                }
            }

            QuNo.Tag = QuNo.Text.Trim();
            SetAllMny();

            QuNo.CausesValidation = true;
        }

        private void QuNo_Validating(object sender, CancelEventArgs e)
        {
            if (QuNo.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            if (QuNo.TrimTextLenth() == 0)
            {
                QuNo.Tag = "";
                return;
            }

            if (QuNo.Tag == null)
                QuNo.Tag = "";

            if (jSale.IsExistDocument<JBS.JS.Quote>(QuNo.Text.Trim()) == true)
            {
                //已經開過窗了, 之後就不開
                if (QuNo.Tag.ToString() == QuNo.Text.Trim())
                    return;

                using (var frm = new subMenuFm_1.FrmQuoteToSale())
                {
                    frm.CuNo = CuNo.Text.Trim();
                    frm.SeekNo = QuNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtSaleD, ref dtSaleBom, SetAllMny);

                    this.PassQuoteM(frm);

                    this.PassQuoteD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                QuNo.Tag = QuNo.Text.Trim();
                SetAllMny();
            }
            else
            {
                e.Cancel = true;
                using (var frm = new subMenuFm_1.FrmQuoteToSale())
                {
                    frm.CuNo = CuNo.Text.Trim();
                    frm.SeekNo = QuNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtSaleD, ref dtSaleBom, SetAllMny);

                    this.PassQuoteM(frm);

                    this.PassQuoteD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                QuNo.Tag = QuNo.Text.Trim();
                SetAllMny();
            }
        }
        private void PassQuoteM(FrmQuoteToSale frm)
        {
            QuNo.Text = frm.QuNo.Trim();
            Xa1No.Text = frm.MasterRow["Xa1no"].ToString();
            Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
            Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
            X3No.Text = frm.MasterRow["X3no"].ToString();
            X3No1.Text = frm.MasterRow["X3no"].ToString();
            Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            Rate1.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            EmNo.Text = frm.MasterRow["EmNo"].ToString();
            EmName.Text = frm.MasterRow["EmName"].ToString();
            SaMemo.Text = frm.MasterRow["qumemo"].ToString();
            PhotoPath = frm.MasterRow["PhotoPath"].ToString();
            getDeNo();
            pVar.XX03Validate(X3No.Text, X3No, X3Name);
            pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);
            this.Memo1 = frm.MasterRow["qumemo1"].ToString();

            CuNo.Text = frm.MasterRow["CuNo"].ToString();
            CuName11.Text = frm.MasterRow["CuName1"].ToString();
            AdPer11.Text = frm.MasterRow["CuPer1"].ToString().GetUTF8(10);
            AdTel1.Text = frm.MasterRow["CuTel1"].ToString();
            AdAddr1.Text = SQL.ExecuteScalar("select cuaddr" + Common.Sys_DefaultAddr +"  from cust where cuno = @cuno ",new parameters("cuno",CuNo.Text));
            CuAdvamt.Text = frm.MasterRow["CuAdvamt"].ToDecimal("f" + CuAdvamt.LastNum).ToString();
            Disc = frm.MasterRow["CuDisc"].ToDecimal("f4");
            InvTaxNo.Text = frm.MasterRow["CuUno"].ToString();

            var CuInvoName = frm.MasterRow["CuInvoName"].ToString();
            if (CuInvoName.Trim().Length == 0) CuInvoName = frm.MasterRow["CuName2"].ToString();
            InvName.Text = CuInvoName;
            填入請款客戶(CuNo.Text.ToString());
            jSale.Validate<JBS.JS.Cust>(payerno.Text, r => payername.Text = r["cuname1"].ToString(), () => payername.Clear());
            SaPayment.Text = frm.MasterRow["qupayment"].ToString();
            //填入指送地址資訊(null, frm.MasterRow["CuNo"].ToString());//CuPer1  CuTel1 cuaddr1 AdName = 公司地址 

        }

        private void 填入請款客戶(string p)
        {
             using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
             using (SqlCommand cmd = cn.CreateCommand())
             {
                 cn.Open();
                 cmd.Parameters.AddWithValue("cuno", p);
                 cmd.CommandText = "select cuno,payerno from cust where cuno=@cuno ";
                 using (SqlDataReader dr = cmd.ExecuteReader())
                 {
                     if (dr.HasRows)
                     {
                         dr.Read();
                         payerno.Text = dr["payerno"].ToString().Trim() == "" ? p : dr["payerno"].ToString();
                     }
                 }
             }
        }
        private void PassQuoteD(FrmQuoteToSale frm)
        {
            DataRow row = null;
            DataTable dtD = frm.dtDetail;

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                var rec = GetBomRec();
                row = dtSaleD.NewRow();
                row["itno"] = dtD.Rows[i]["itno"];
                row["itname"] = dtD.Rows[i]["itname"];
                row["itnoudf"] = dtD.Rows[i]["itnoudf"];
                帶入倉庫資訊(row, dtD.Rows[i]);
                row["ittrait"] = dtD.Rows[i]["ittrait"];
                if (dtD.Rows[i]["ittrait"].ToString() == "1") row["產品組成"] = "組合品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "2") row["產品組成"] = "組裝品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "3") row["產品組成"] = "單一商品";
                row["itunit"] = dtD.Rows[i]["itunit"];
                row["Punit"] = dtD.Rows[i]["Punit"];
                row["qty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["Pqty"] = dtD.Rows[i]["Pqty"].ToDecimal("f" + Common.Q);
                row["itpkgqty"] = dtD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                row["price"] = dtD.Rows[i]["price"].ToDecimal("f" + Common.MS);
                row["prs"] = dtD.Rows[i]["prs"].ToDecimal("f3");
                row["taxprice"] = dtD.Rows[i]["taxprice"].ToDecimal("f6");
                row["mny"] = dtD.Rows[i]["mny"].ToDecimal("f" + Common.TPS);
                row["priceb"] = dtD.Rows[i]["priceb"].ToDecimal("f" + Common.M);
                row["taxpriceb"] = dtD.Rows[i]["taxpriceb"].ToDecimal("f6");
                row["mnyb"] = dtD.Rows[i]["mnyb"].ToDecimal("f" + Common.M);
                row["bomrec"] = rec;
                row["memo"] = dtD.Rows[i]["memo"];
                row["mwidth1"] = dtD.Rows[i]["mwidth1"].ToDecimal("f" + Common.Q);
                row["mwidth2"] = dtD.Rows[i]["mwidth2"].ToDecimal("f" + Common.Q);
                row["mwidth3"] = dtD.Rows[i]["mwidth3"].ToDecimal("f" + Common.Q);
                row["mwidth4"] = dtD.Rows[i]["mwidth4"].ToDecimal("f" + Common.Q);
                row["pformula"] = dtD.Rows[i]["pformula"].ToString();
                row["standard"] = dtD.Rows[i]["standard"].ToString();
                for (int j = 1; j < 11; j++)
                {
                    row["itdesp" + j] = dtD.Rows[i]["itdesp" + j];
                }
                填入saleD指送地址欄位資料(row);
                dtSaleD.Rows.Add(row);
                dtSaleD.AcceptChanges();
                dataGridViewT1.InvalidateRow(dtSaleD.Rows.Count - 1);

                if (dtD.Rows[i]["ittrait"].ToString().Trim() == "3")
                    continue;

                var qubomid = dtD.Rows[i]["bomid"].ToString().Trim();
                jSale.GetTBom<JBS.JS.Quote>(qubomid, rec.ToString(), ref dtSaleBom);
            }
        }

        private void 帶入倉庫資訊(DataRow row1, DataRow row2)//row1 為回傳值 row2 為參數
        {
            #region 帶入倉庫資訊
            //先帶入預設
            row1["StNo"] = StNo.Text.Trim();
            row1["StName"] = StName.Text;
            //如果有設定產品常用倉庫，則覆蓋
            SQL.ExecuteReader("select stno,stname = (select top 1 stname from stkroom where stkroom.stno = item.StNo) from item where itno = @itno and len(stno) > 0", new parameters("itno", row2["itno"].ToString()),
               r =>
               {
                   row1["stno"] = r["stno"].ToString();
                   row1["stname"] = r["stname"].ToString();
               });
            #endregion
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
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift") && gridTran.Enabled)
            {
                gridTran_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift") && gridAllTrans.Enabled)
            {
                gridAllTrans_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F11") && keyData.ToString().EndsWith("Shift") && gridItBuyPrice.Enabled)
            {
                gridItBuyPrice_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ToolTip tip = new ToolTip();
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "出貨倉庫" || str == "備註說明" || str == "訂單憑證" || str == "售價")
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
            else if (str == "銷貨數量")
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
                frm.CanEdt = CuNo.ReadOnly ? false : true;
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

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "銷貨數量")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim());
                    cmd.CommandText = "select itstockqty from item where itno=@itno ";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        toolStripStatusLabel1.Text = "現有庫存量：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);
                }
            }
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "售價")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                if (CuNo.Text.Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim());
                    cmd.Parameters.AddWithValue("itunit", dataGridViewT1["單位", e.RowIndex].Value.ToString().Trim());
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));

                    cmd.CommandText = "select top(1)taxprice from saled where "
                                    + "itno=@itno "
                                    + "and itunit=@itunit "
                                    + "and cuno=@cuno "
                                    + "and sadate<=@sadate "
                                    + "order by sadate desc,bomid desc";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        toolStripStatusLabel1.Text = "最後一次交易稅前售價：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.MS);
                    else
                        toolStripStatusLabel1.Text = "最後一次交易稅前售價：0";
                }
            }
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
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

        private void dataGridViewT1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "銷貨數量" || dataGridViewT1.Columns[e.ColumnIndex].Name == "售價" || dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
                toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.瀏覽 0.結束";
        }

        private void SaNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            btnInStk.Enabled = SaNo.ReadOnly;
        }

        private void btnInStk_Click(object sender, EventArgs e)
        {
            if (jSale.IsEditInCloseDay(SaDate.Text) == false)
                return;

            if (SaNo.Text.Trim().Length == 0) return;
            using (var frm = new FrmInStkb())
            {
                frm.SaNo = SaNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnLetoSale_Click(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() == "")
            {
                MessageBox.Show("客戶編號不能為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuNo.Focus();
                return;
            }

            if (Common.Sys_LendToSaleMode == 2)
            {
                #region 宏恩模式借轉銷
                using (S2.frmLendToSaleBAT frm = new S2.frmLendToSaleBAT())
                {
                    frm.cuno = CuNo.Text.Trim();
                    frm.disc = this.Disc;
                    frm.sadate = SaDate.Text;
                    frm.x3no = X3No.Text.Trim();
                    frm.xa1par = Xa1Par.Text.ToDecimal();
                    frm.stno = StNo.Text.Trim();
                    frm.stname = StName.Text.Trim();
                    frm.maxrec = BomRec.ToInteger();

                    //判斷銷貨明細是否有借出明細(leid='V'),都沒有的話,表示首次開窗
                    var rows = dtSaleD.AsEnumerable().Where(r => r["leid"].ToString().Trim().Length > 0);
                    if (rows.Count() == 0)
                    {
                        frm.dtSaleD.Clear();
                        if (frm.dtSaleD.Columns.Count == 0) frm.dtSaleD = dtSaleD.Clone();
                    }
                    else
                    {
                        frm.dtSaleD = rows.CopyToDataTable();

                        this.lendtemp.AcceptChanges();//<=重要
                        frm.lendtemp = this.lendtemp;
                        frm.lendtempNOedit = this.lendtemp.Copy();
                    }

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.lendtemp = frm.lendtemp;
                        //先刪除有借出單據的明細,在import新取回的row
                        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                        {
                            if (dtSaleD.Rows[i]["leid"].ToString().Trim().Length > 0)
                            {
                                dtSaleD.Rows.RemoveAt(i--);
                            }
                        }
                        dtSaleD.AcceptChanges();

                        //再刪除借出單據的bom,在取得bom表
                        var boms = dtSaleBom.AsEnumerable().Where(r => r["ItSource"].ToDecimal() != 1);
                        if (boms.Count() == 0) dtSaleBom.Clear();
                        else
                        {
                            dtSaleBom = boms.CopyToDataTable();
                        }
                        dtSaleBom.AcceptChanges();

                        //import明細 and 取得bom表
                        DataTable dtgetbom = new DataTable();
                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        using (SqlCommand cmd = cn.CreateCommand())
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("boitno", "");
                            cmd.Parameters.AddWithValue("BomRec", "");
                            for (int i = 0; i < frm.dtSaleD.Rows.Count; i++)
                            {
                                this.dtSaleD.ImportRow(frm.dtSaleD.Rows[i]);

                                cmd.Parameters["boitno"].Value = frm.dtSaleD.Rows[i]["itno"].ToString().Trim();
                                cmd.Parameters["bomrec"].Value = frm.dtSaleD.Rows[i]["bomrec"].ToString().Trim();
                                cmd.CommandText = " Select BomRec=(@BomRec),ItSource=1.0,[itno],[itname],[itunit],[itqty],[itpareprs],[itprice],[itprs],[itmny],[itpkgqty],[itnote],[itrec] from bomd where boitno=(@boitno) ";
                                da.Fill(dtgetbom);
                            }
                        }
                        dtgetbom.AcceptChanges();
                        //
                        dtSaleBom.Merge(dtgetbom);
                        dtSaleBom.AcceptChanges();
                        //
                        dtSaleD.AcceptChanges();
                        SetAllMny();
                        //
                        this.BomRec = frm.maxrec;
                    }
                    else //表示frm.Cancel
                    {
                        this.lendtemp.Dispose();
                        this.lendtemp = frm.lendtempNOedit;
                    }
                }
                #endregion
            }
            else
            {
                #region 一般模式借轉銷
                using (var frm = new FrmLendToSale())
                {
                    frm.TSeekNo = "";
                    frm.CuNo = CuNo.Text.Trim();
                    lendtemp.Clear();
                    lendtemp = dtSaleD.Copy();
                    lend = dtSaleD.Clone();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.MasterRow == null || frm.dtDetail.Rows.Count == 0) return;

                        Memo1 = frm.MasterRow["lememo1"].ToString();
                        Xa1No.Text = frm.MasterRow["Xa1no"].ToString();
                        Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
                        Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
                        X3No.Text = frm.MasterRow["X3no"].ToString();
                        X3No1.Text = frm.MasterRow["X3no"].ToString();
                        Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
                        Rate1.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");

                        SaMemo.Text = frm.MasterRow["lememo"].ToString();
                        EmNo.Text = frm.MasterRow["EmNo"].ToString();
                        EmName.Text = frm.MasterRow["EmName"].ToString();
                        getDeNo();

                        pVar.XX03Validate(X3No.Text, X3No, X3Name);
                        pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);

                        if (lendtemp.Rows.Count > 0)
                        {
                            for (int i = 0; i < lendtemp.Rows.Count; i++)
                            {
                                if (lendtemp.Rows[i]["itno"].ToString().Trim() == "")
                                {
                                    lendtemp.Rows[i].Delete();
                                    lendtemp.AcceptChanges();
                                }
                            }
                        }
                        if (dtSaleD.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtSaleD.Rows.Count; i++)
                            {
                                if (dtSaleD.Rows[i]["itno"].ToString().Trim() == "")
                                {
                                    dtSaleD.Rows[i].Delete();
                                    dtSaleD.AcceptChanges();
                                }
                            }
                        }
                        //明細部分&組件
                        DataRow row1, row2;

                        for (int i = 0; i < frm.dtDetail.Rows.Count; i++)
                        {
                            row1 = dtSaleD.NewRow();
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
                            row1["leno"] = row2["leno"].ToString();

                            row1["itno"] = row2["itno"].ToString();
                            row1["itname"] = row2["itname"].ToString();
                            row1["itunit"] = row2["itunit"].ToString();
                            row1["Punit"] = row2["itunit"].ToString();
                            row1["qty"] = row2["qtynotout"].ToDecimal("f" + Common.Q);
                            row1["Pqty"] = row2["qtynotout"].ToDecimal("f" + Common.Q);

                            row1["price"] = row2["price"].ToDecimal("f" + Common.MS);
                            row1["prs"] = row2["prs"].ToDecimal("f3");
                            row1["taxprice"] = row2["taxprice"].ToDecimal("f6");
                            row1["memo"] = row2["memo"].ToString();
                            row1["priceb"] = row2["priceb"].ToDecimal("f" + Common.M);
                            row1["taxpriceb"] = row2["taxpriceb"].ToDecimal("f6");
                            row1["itpkgqty"] = row2["itpkgqty"].ToDecimal("f" + Common.Q);
                            row1["StNo"] = StNo.Text.Trim();
                            row1["StName"] = StName.Text;
                            row1["stnoi"] = row2["stnoi"].ToString().Trim();
                            row1["stnamei"] = row2["stnamei"].ToString().Trim();
                            row1["mwidth1"] = 0;
                            row1["mwidth2"] = 0;
                            row1["mwidth3"] = 0;
                            row1["mwidth4"] = 0;
                            row1["leid"] = row2["bomid"].ToString();

                            for (int j = 1; j <= 10; j++)
                            {
                                row1["itdesp" + j] = row2["itdesp" + j].ToString();
                            }
                            row1["BomRec"] = GetBomRec();
                            SetRow_Mny(row1);
                            dtSaleD.Rows.InsertAt(row1, dtSaleD.Rows.Count);
                            dtSaleD.AcceptChanges();


                            //組件部分
                            if (row2["ittrait"].ToDecimal() == 3) continue;
                            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                            {
                                cn.Open();
                                string sql = "select * from lendbom where bomid=@bomid";
                                using (SqlCommand cmd = cn.CreateCommand())
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("bomid", row2["bomid"].ToString().Trim());
                                    cmd.CommandText = sql;
                                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                                    {
                                        DataTable temprowBom = new DataTable();

                                        temprowBom.Clear();
                                        dd.Fill(temprowBom);
                                        for (int j = 0; j < temprowBom.Rows.Count; j++)
                                        {
                                            temprowBom.Rows[j]["BomRec"] = row1["BomRec"].ToString().Trim();
                                        }
                                        dtSaleBom.Merge(temprowBom);
                                        dtSaleBom.AcceptChanges();
                                    }
                                }
                            }
                            dataGridViewT1.InvalidateRow(i);
                        }
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.CurrentRow.Index];
                        dataGridViewT1.CurrentRow.Selected = true;
                        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                        {
                            SetRow_TaxPrice(dtSaleD.Rows[i]);
                            SetRow_Mny(dtSaleD.Rows[i]);

                        }
                        SetAllMny();

                    }
                }
                //避免重複撈單據
                if (lendtemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSaleD.Rows.Count; i++)
                    {
                        bool repeat = false;
                        for (int j = 0; j < lendtemp.Rows.Count; j++)
                        {
                            if (dtSaleD.Rows[i]["leid"].ToDecimal() == lendtemp.Rows[j]["leid"].ToDecimal())
                            {
                                repeat = true;
                                break;
                            }


                        }
                        if (!repeat)
                        {
                            lend.ImportRow(dtSaleD.Rows[i]);
                            lend.AcceptChanges();
                        }
                    }
                    if (lendtemp.Rows.Count > 0)
                    {
                        if (lend.Rows.Count > 0)
                        {
                            lendtemp.Merge(lend);
                            dtSaleD.Clear();
                            dtSaleD.Merge(lendtemp);
                        }
                        else
                        {
                            dtSaleD.Clear();
                            dtSaleD.Merge(lendtemp);
                        }
                    }
                }
                #endregion
            }
        }

        private void InvNo_Enter(object sender, EventArgs e)
        {
            if (gridAutoInv.Text == "自動")
            {
                if (X5No.Text.ToString().Trim() == "7" || X5No.Text.ToString().Trim() == "8")
                    toolStripStatusLabel1.Text = "目前發票號碼：" + jSale.GetNextInvNo78(X5No.Text.Trim(), User_Einv.Text.Trim());
                else
                    toolStripStatusLabel1.Text = "目前發票號碼：" + jSale.GetNextInvNo(X5No.Text.Trim());
            }
        }

        private void InvNo_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.瀏覽 0.結束";
        }

        private void AdAddr1_DoubleClick(object sender, EventArgs e)
        {
            if (((TextBox)sender).ReadOnly) return;

            using (指送地址 frm = new 指送地址())
            {
                frm.cuno = CuNo.Text.Trim();
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        AdAddr1.Text = frm.addr; //公司住址
                        AdPer11.Text = frm.per.GetUTF8(10);;  //公司連絡人
                        AdTel1.Text = frm.tel;  //公司電話
                        AdName = frm.name;       //公司名稱
                        break;
                    case DialogResult.Cancel: break;
                }
            }
        }

        //13.7c
        private void ShowORModify_PhotoPath_Click(object sender, EventArgs e)
        {
            using (FrmAffixFile frm = new FrmAffixFile())
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dt_訂單關聯報價單 = new DataTable())
                {
                    string sqlcommand = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號, * from AffixFile where (DaType ='銷貨單' and Datano=@Datano) or (Datano=@Datano2 and DaType='報價單')";
                    //撈出訂單關聯的報價單
                    string sqlcommand_ = "SELECT distinct(quote.quno) FROM quote INNER JOIN [order] on quote.quno = [order].quno where 1=0  ";
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Datano", SaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Datano2", QuNo.Text.Trim());

                    if (dtSaleD.Rows.Count > 0)
                    {
                        List<string> ornolist = new List<string>();
                        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                        {
                            if (ornolist.IndexOf(dtSaleD.Rows[i]["orno"].ToString()) == -1)
                            {
                                var ornoG = "Datano3" + i.ToString();
                                cmd.Parameters.AddWithValue(ornoG, dtSaleD.Rows[i]["orno"].ToString());
                                ornolist.Add(dtSaleD.Rows[i]["orno"].ToString());
                                sqlcommand  += " or (DaType ='訂貨單' and Datano=@" + ornoG + ")";
                                sqlcommand_ += " or [order].orno =@" + ornoG ;
                            }
                        }
                        cmd.CommandText = sqlcommand_;
                        da.Fill(dt_訂單關聯報價單);
                        for (int i = 0; i < dt_訂單關聯報價單.Rows.Count; i++)
                        {
                            var ornoG = "Datano4" + i.ToString();
                            cmd.Parameters.AddWithValue(ornoG, dt_訂單關聯報價單.Rows[i]["quno"].ToString());
                            sqlcommand += " or (DaType ='報價單' and Datano=@" + ornoG + ")";
                        }
                    }

                    cmd.CommandText = sqlcommand;
                    DaFile.Clear();
                    da.Fill(DaFile);

                    frm.DtFile = DaFile;
                    frm.Datano = SaNo.Text.Trim();
                    frm.CMD = cmd;
                    frm.DaType = "銷貨單";
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

        private void EInv1_CheckedChanged(object sender, EventArgs e)
        {
            //是否使用電子發票
            if (EInv1.Checked)//使用
            {
                lblEInvChange.Visible = lblEInvState.Visible = EInvChange.Visible = EInvState.Visible = true;
                if (CuNo.ReadOnly) return;
                EInvChange.Text = "存證";
                EInvState.Text = "未上傳";
                pVar.XX05Validate("7", X5No, X5Name);
                User_Einv.Text = Common.User_Einv;
                User_Einv_Validating(null, null);
                iTitle.Text = Common.iTitle;
                User_Einv.Visible = true;
                iTitle.Visible = true;
                labelT5.Visible = true;
            }
            else
            {
                lblEInvChange.Visible = lblEInvState.Visible = EInvChange.Visible = EInvState.Visible = false;
                if (CuNo.ReadOnly) return;
                EInvChange.Text = "";
                EInvState.Text = "";
                pVar.XX05Validate("1", X5No, X5Name);
                User_Einv.Text = "";
                iTitle.Text = "";
                User_Einv.Visible = false;
                iTitle.Visible = false;
                labelT5.Visible = false;
            }
        }

        private void gridbatch_Click(object sender, EventArgs e)
        {
            BatchF.WhenGridBadch_click(this.Name, dataGridViewT1, dtSaleD, dt_BatchProcess, null, null, CuNo, CuName11, null, false, btnSave.Enabled == true);
        }

        private void payerno_Validating(object sender, CancelEventArgs e)
        {
            if (payerno.ReadOnly || btnCancel.Focused)
                return;

            if (payerno.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("請款客戶編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jSale.ValidateOpen<JBS.JS.Cust>(sender, e, row =>
            {
                payerno.Text = row["cuno"].ToString();
                payername.Text = row["cuname1"].ToString();
            });
        }

        private void payerno_DoubleClick(object sender, EventArgs e)
        {
            if (payerno.ReadOnly || btnCancel.Focused)
                return;
            jSale.Open<JBS.JS.Cust>(sender, reader =>
            {
                payerno.Text = reader["cuno"].ToString();
                payername.Text = reader["cuname1"].ToString();
            });

        }

        private void User_Einv_DoubleClick(object sender, EventArgs e)
        {
            if (User_Einv.ReadOnly || btnCancel.Focused)
                return;
            xe.Open<JBS.JS.EINV>(sender, reader =>
            {
                User_Einv.Text = reader["Einvid"].ToString();
                iTitle.Text = reader["EinvTitle"].ToString();
            });
        }

        private void User_Einv_Validating(object sender, CancelEventArgs e)
        {
            if (User_Einv.Text.Trim().Length == 0)
            { iTitle.Text = ""; }
            else
            {
                if (User_Einv.ReadOnly) return;
                if (btnCancel.Focused) return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandText = "select * from Einvsetup where Einvid='" + User_Einv.Text + "'";
                    if (cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        User_Einv_DoubleClick(sender, null);
                        e.Cancel = true;
                    }
                    else
                    {
                        Einvdt.Clear();
                        da.Fill(Einvdt);
                        da.Dispose();
                        iTitle.Text = Einvdt.Rows[0]["EinvTitle"].ToString();
                    }
                }
            }
        }
    }
}
