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
using System.IO;
using System.Diagnostics;

namespace S_61.subMenuFm_2
{
    public partial class FrmRSale : Formbase
    {
        JBS.JS.RSale jRSale;
        JBS.JS.xEvents xe = new JBS.JS.xEvents();
        DataTable dtRSaleD = new DataTable();
        DataTable tempD = new DataTable();

        DataTable dtRSaleBom = new DataTable();
        DataTable tempBom = new DataTable();

        DataTable DaFile = new DataTable();//附件檔案
        DataTable Einvdt = new DataTable();

        #region 批次資料
        BatchProcess BatchF = new BatchProcess();              //批次存資料異動修改
        BatchSave BatchSave = new BatchSave();              //批次資料存檔用
        DataTable dt_BatchProcess = new DataTable();        //批次異動
        DataTable dt_TempBatchProcess = new DataTable();    //批次異動暫存檔

        DataTable dt_Bom_BatchProcess = new DataTable();       //bom批次異動
        DataTable dt_Bom_TempBatchProcess = new DataTable();   //bom批次異動暫存檔
        #endregion
        DataTable dtstandard = new DataTable();

        string TextBefore;
        string ItNoBegin;
        string UdfNoBegin;
        string Memo1 = "";
        string offline = "";
        string online = "";
        decimal BomRec = 0;
        decimal Disc = 1;
        string OldCust = "";
        string Oldpayerno = "";
        string tempinvno = "";

        List<Control> Writes;
        List<Button> AllGridButtons;
        List<TextBoxNumberT> NumsInit_Zero;
        List<TextBoxbase> list;

        //發票日期、發票地址
        string InvDate = "";
        string InvAddr1 = "";
        string SaPayment = "";//付款條件
        string PhotoPath = "";// 13.7c  

        //媒體申報
        string invkind = "";
        string specialtax = "";
        string passmode = "";

        //發票防伪碼錯誤
        string invrandom = "";
        public FrmRSale()
        {
            InitializeComponent();
            this.jRSale = new JBS.JS.RSale();
            this.list = this.getEnumMember();
            this.dataGridViewT1.tableName = "RSaled";

            SaDate.SetDateLength();
            SaDateAc.SetDateLength();
            pVar.SetMemoUdf(this.備註說明);

            Writes = new List<Control> { 
                SaNo, SaDate, SaDateAc, FromSale, CuNo, CuName11, CuPer11, CuTel11, StNo, StName, Xa1No, Xa1Name, Xa1Par, Bracket, 
                TaxMnyB, TaxMny, X3No, Rate, Tax, TotMny, Discount, CollectMny, GetPrvAcc, AcctMny, X4No, X4Name, EmNo, EmName, SeNo, SeName, SpNo, SpName, SaMemo, 
                TaxMny1, X3No1, Rate1, Tax1, TotMny1, X5No, InvNo, Discount1, CashMny, CardMny, CardNo, InvName, Ticket, CollectMny1, GetPrvAcc1, AcctMny1, InvTaxNo ,DeNo,DeName  
            };

            AllGridButtons = new List<Button> { gridAppend, gridDelete, gridPicture, gridInsert, gridItDesp, gridBomD, gridAddress, gridInvNo, gridInvMode };

            NumsInit_Zero = new List<TextBoxNumberT> { TaxMnyB, TaxMny, Tax, TotMny, Discount, CollectMny, GetPrvAcc, AcctMny, TaxMny1, Tax1, TotMny1, Discount1, CashMny, CardMny, Ticket, CollectMny1, GetPrvAcc1, AcctMny1 };

            //金額權限
            TaxMnyB.Visible = Common.User_SalePrice;
            TaxMny.Visible = Common.User_SalePrice;
            Tax.Visible = Common.User_SalePrice;
            TotMny.Visible = Common.User_SalePrice;
            Discount.Visible = Common.User_SalePrice;
            CollectMny.Visible = Common.User_SalePrice;

            AcctMny.Visible = Common.User_SalePrice;
            TaxMny1.Visible = Common.User_SalePrice;
            Tax1.Visible = Common.User_SalePrice;
            TotMny1.Visible = Common.User_SalePrice;
            Discount1.Visible = Common.User_SalePrice;
            CashMny.Visible = Common.User_SalePrice;
            CardMny.Visible = Common.User_SalePrice;
            Ticket.Visible = Common.User_SalePrice;
            CollectMny1.Visible = Common.User_SalePrice;

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

            this.售價.Visible = Common.User_SalePrice;
            this.稅前售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.本幣單價.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;

            //小數位數設定
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
            this.銷退數量.Set庫存數量小數();
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

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }
            if (Common.Sys_UsingBatch == 1)
            {
                this.GridBatch.Visible = false;
            }

            if (Common.Series == "74")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                CuTel11.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
                this.訂單憑證.Visible = false;
            }
            else if (Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                this.訂單憑證.Visible = true;
            }
            else if (Common.Series == "72")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
                StNo.Enabled = true;
                StName.Enabled = true;
                CuTel11.Validating += new CancelEventHandler(Xa1Par_Validating);
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

            if (Common.Sys_LendToSaleMode == 2)
            {
                Xa1Par.AllowGrayBackColor = true;
                Xa1Par.ReadOnly = false;
                Xa1Par.ReadOnly = true;
                Xa1Par.Enabled = false;

                CuTel11.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
            }
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
            dataGridViewT1.DataSource = dtRSaleD;
        }

        private void FrmRSale_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = @"
                        select 產品組成=
                        case
                        when ittrait=1 then '組合品'
                        when ittrait=2 then '組裝品'
                        when ittrait=3 then '單一商品'
                        end,ItNoUdf='',*
                        from RSaleD where 1=0 ";

                da.Fill(dtRSaleD);
                da.Fill(tempD);
            }

            #region 批次初始
            BatchF.建立結構(dt_BatchProcess);
            dt_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_TempBatchProcess = dt_BatchProcess.Clone();
            dataGridView1.DataSource = dt_BatchProcess;
            dataGridView2.DataSource = dt_Bom_BatchProcess;
            #endregion

            var pk = jRSale.Bottom();
            writeToTxt(pk);

        }

        private void FrmRSale_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string sano)
        {
            BomRec = 0;

            var result = jRSale.LoadData(sano, row =>
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

                //使用電子發票
                if (row["einv"].ToString() == "1")
                    EInv1.Checked = true;
                else
                    EInv2.Checked = true;

                EInvChange.Text = row["einvchange"].ToString();
                EInvState.Text = row["einvstate"].ToString();
                User_Einv.Text = row["User_Einv"].ToString();
                iTitle.Text = row["iTitle"].ToString();
                //退款客戶
                //payerno.Text = row["payerno"].ToString();
                pVar.CuPareValidate(row["payerno"].ToString(), payerno, payername);
                //填入發票日期與地址
                var date = (Common.User_DateTime == 1) ? "" : "1";
                SaDate.Text = row["SaDate" + date].ToString();
                SaDateAc.Text = row["SaDateAc" + date].ToString();
                InvDate = row["InvDate" + date].ToString();
                InvAddr1 = row["InvAddr1"].ToString();
                CuPer11.Text = CuPer11.Text.GetUTF8(10);
                DeNo.Text = row["DeNo"].ToString();
                DeName.Text = row["DeName"].ToString();

                //媒體申報
                invkind = row["invkind"].ToString();
                specialtax = row["specialtax"].ToString();
                passmode = row["passmode"].ToString();

                //載入稅別名稱與發票名稱
                pVar.XX05Validate(row["X5No"].ToString(), X5No, X5Name);
                pVar.XX03Validate(row["X3No"].ToString(), X3No, X3Name);
                pVar.XX03Validate(row["X3No"].ToString(), X3No1, X3Name1);
                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                Rate1.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                SaPayment = row["SaPayment"].ToString();
                gridInvMode.Text = row["invbatch"].ToInteger() == 1 ? "批開" : "";
                PhotoPath = row["PhotoPath"].ToString();

                //載入明細與暫存檔
                loadRSaleD();
                loadAdvAmt();

                this.OldCust = CuNo.Text.Trim();
                this.Oldpayerno = payerno.Text.Trim();
                this.Memo1 = row["samemo1"].ToString();
                this.offline = row["offline"].ToString().Trim();
                this.online = row["online"].ToString().Trim();
                jRSale.keyMan.Set(row);
            });

            if (!result)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtRSaleD.Clear();
                tempD.Clear();
                dtRSaleBom.Clear();
                tempBom.Clear();

                this.OldCust = "";
                this.Oldpayerno = "";
                this.Memo1 = "";
                this.offline = "";
                this.online = "";
                jRSale.keyMan.Clear();
            }
            BatchF.上下頁dt資料修改("rSaleD", sano, dt_BatchProcess, dt_TempBatchProcess);
            BatchF.BOM上下頁dt資料修改("rSaleD", "rSaleBom", sano, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
        }

        void loadRSaleD()
        {
            dtRSaleD.Clear();
            tempD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.CommandText = @"
                        select 產品組成=
                        case
                        when ittrait=1 then '組合品'
                        when ittrait=2 then '組裝品'
                        when ittrait=3 then '單一商品'
                        end,ItNoUdf= (select top 1 itnoudf from item where item.itno = RSaleD.itno),*
                        from RSaleD where SaNo=@sano order by recordno";

                    da.Fill(dtRSaleD);
                    da.Fill(tempD);
                }
                dataGridViewT1.DataSource = dtRSaleD;
                if (dtRSaleD.Rows.Count > 0) BomRec = dtRSaleD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadRSaleBom()
        {
            dtRSaleBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = conn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select top 1 * from RSaleBom where 1=0";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from RSaleBom where SaNo=@sano ";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from RSaleBom where SaNo=@sano ";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("sano", jRSale.GetCurrent());
                    cmd.CommandText = sql;

                    da.Fill(dtRSaleBom);
                    da.Fill(tempBom);
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

        void setTxtWhenAppend()
        {
            CuAdvamt.Clear();

            BomRec = 0;
            decimal d = 0;
            NumsInit_Zero.ForEach(t => t.Text = d.ToString("F" + t.LastNum));

            //倉庫
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

            SaDate.Text = Date.GetDateTime(Common.User_DateTime);
            SaDateAc.Text = Date.GetDateTime(Common.User_DateTime);
            InvDate = Date.GetDateTime(Common.User_DateTime);

            gridInvMode.Text = Common.User_ScInvBat == 1 ? "批開" : "";

            //媒體申報
            invkind = "";
            specialtax = "";
            passmode = "";

            //電子發票
            EInv2.Checked = true;
            lblEInvChange.Visible = lblEInvState.Visible = EInvChange.Visible = EInvState.Visible = false;
            SaDate.Focus();

            invrandom = "";
        }

        string ToTaiwanMny(TextBox tx)
        {
            decimal d = 0, p = 0;
            decimal.TryParse(tx.Text, out d);
            decimal.TryParse(Xa1Par.Text, out p);
            if (tx.Name == Tax.Name)//外幣營業稅轉本幣
                return (d * p).ToString("f" + Common.TS);
            else if (tx.Name == TotMny.Name)//外幣應退轉本幣應退
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
            var pk = jRSale.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jRSale.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jRSale.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jRSale.Bottom();
            writeToTxt(pk);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            SaPayment = "";
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            dtRSaleD.Clear();
            loadRSaleBom();
            this.Memo1 = "";
            this.offline = "";
            this.online = "";
            this.BomRec = 0;

            payerno.Enabled = true;

            SaDate.Focus();
            setTxtWhenAppend();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
            this.自定編號.ReadOnly = true;
            if (X5No.Text == "7" || X5No.Text == "8")
            {
                User_Einv.Text = Common.User_Einv;
                iTitle.Text = Common.iTitle;
            }
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            SaPayment = "";
            if (SaNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            switch (Common.User_DateTime)
            {
                case 1:
                    SaDate.Text = Date.GetDateTime(1, false);
                    SaDateAc.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    SaDate.Text = Date.GetDateTime(2, false);
                    SaDateAc.Text = Date.GetDateTime(2, false);
                    break;
            }

            //電子發票
            EInvState.Text = "未上傳";

            loadRSaleBom();

            //媒體申報
            invkind = "";
            specialtax = "";
            passmode = "";

            InvNo.Text = "";
            InvDate = Date.GetDateTime(Common.User_DateTime);

            CardNo.Clear();
            Bracket.Clear();

            Discount.Text = "0";
            Discount1.Text = "0";
            GetPrvAcc.Text = "0";
            GetPrvAcc1.Text = "0";
            CashMny.Text = "0";
            CardMny.Text = "0";
            Ticket.Text = "0";
            SetAcctMny();

            SaNo.Text = "";
            SaDate.Focus();
            SaDate.SelectAll();
            payerno.Enabled = true;
            this.自定編號.ReadOnly = true;
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
            if (X5No.Text == "7" || X5No.Text == "8")
            {
                User_Einv.Text = Common.User_Einv;
                iTitle.Text = Common.iTitle;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (SaNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jRSale.IsExistDocument<JBS.JS.RSale>(SaNo.Text.Trim()) == false)
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
            if (jRSale.IsSendToEINV(SaNo.Text) == false || jRSale.IsPassToBatch(InvNo.Text) == true )////上傳 電子雲&發票 可改部分
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
                this.自定編號.ReadOnly = true;
                return;
            }


            //if (jRSale.IsSendToEINV(SaNo.Text) == false)//上傳電子雲可改
            //    return;
            //
            //if (jRSale.IsPassToBatch(InvNo.Text) == true)//批次可改
            //    return;

            if (jRSale.IsEditInCloseDay(SaDate.Text) == false)//都不能改
                return;

            if (jRSale.IsPassToAcc(SaNo.Text.Trim()) == true)//都不能改
                return;

            if (jRSale.IsPassToReceiv(SaNo.Text.Trim()) == true)//只有銷退單的沖款可以改
                return;

            if (jRSale.IsModify<JBS.JS.RSale>(SaNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jRSale.upModify1<JBS.JS.RSale>(SaNo.Text.Trim());//更新修改狀態1
                var pk = jRSale.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            loadRSaleBom();

            SaNo.Focus();
            SaNo.SelectAll();

            //紀錄發票號碼
            if (InvNo.Text.Trim() != "")
            {
                tempinvno = InvNo.Text.Trim();
            }
            payerno.Enabled = true;
            this.自定編號.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SaNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jRSale.IsExistDocument<JBS.JS.RSale>(SaNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jRSale.IsModify<JBS.JS.RSale>(SaNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Common.User_CanEditPOS == 2 && Bracket.Text.Trim() == "前台")
            {
                MessageBox.Show("前台收銀單據，無法異動！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Bracket.Text.Trim() == "前台")
            {
                var dl = MessageBox.Show("前台收銀單據,請確定是否刪除?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dl != DialogResult.OK)
                    return;
            }

            //有折讓紀錄不可修改
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

            if (jRSale.IsSendToEINV(SaNo.Text) == false)
                return;

            if (jRSale.IsPassToBatch(InvNo.Text) == true)
                return;

            if (jRSale.IsEditInCloseDay(SaDate.Text) == false)
                return;

            if (jRSale.IsPassToAcc(SaDate.Text.Trim()) == true)
                return;

            if (jRSale.IsPassToReceiv(SaNo.Text.Trim()) == true)
                return;

            

            var reno = jRSale.GetThisPassToReceiv(SaNo.Text.Trim());
            jRSale.GetTempBomOnDeleting("RSaleBom", SaNo.Text.Trim(), ref tempBom);

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
                    jRSale.GetOldAcctMnyOnDeleting(SaNo.Text.Trim(), out acctmny, out getprvacc);

                    for (int i = 0; i < tempD.Rows.Count; i++)
                    {
                        //若此筆明細有訂單資料 加回此筆數量
                        if (tempD.Rows[i]["orid"].ToString() != "")
                        {
                            BackOrderdQty(cmd, i);
                        }
                    }

                    //前台有開發票,要取消作廢
                    if (Bracket.Text.Trim() == "前台" && InvNo.Text.Trim().Length == 10)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("invno", InvNo.Text.Trim());

                        cmd.CommandText = @"
                            update
                            posinv
                            set Memo=''
                            where posinv.sano =
                            (
	                            select posinv.sano 
	                            from rsale left join posinv on rsale.invno=posinv.invno
	                            where rsale.invno = @invno
                            )";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                             delete from nullify
                             where invno in
                             (
	                             select posinv.invno from posinv
	                             inner join
	                             (
 		                            select posinv.sano 
		                            from rsale left join posinv on rsale.invno=posinv.invno
		                            where rsale.invno = @invno
	                             )B on posinv.sano=B.sano
                             )";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("reno", reno);
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.CommandText = @"
                        delete from receiv   where ExtFlag =N'銷退' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from receivd  where ExtFlag =N'銷退' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from RSaleBom where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from RSaleD   where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN;
                        delete from RSale    where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN; ";
                    cmd.ExecuteNonQuery();

                    jRSale.扣庫存(cmd, tempD, tempBom, "stno", "qty");

                    FrmAffixFile.FileDelete_單據刪除(cmd, SaNo.Text.Trim(), "銷退單");

                    BatchSave.進貨_Delete(dt_TempBatchProcess, cmd, "rSaleD", SaNo.Text.Trim());
                    BatchSave.進貨_Delete(dt_Bom_TempBatchProcess, cmd, "rSaleBom", SaNo.Text.Trim());

                    tn.Commit();

                    jRSale.BackOldCustReceiv(this.Oldpayerno.Trim(), acctmny, getprvacc);

                    jRSale.UpdateItemItStockQty(tempD, tempBom);

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
            using (var frm = new FrmRSale_Print())
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

            using (var frm = new S2.FrmRSaleBrowNew())
            {
                frm.TSeekNo = SaNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult.Trim());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (jRSale.IsEditInCloseDay(SaDate.Text) == false)
                return;

            if (jRSale.IsRegisted() == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！\n";
                msg += "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (StNo.Text.Trim() == "")
            {
                CuNo.Focus();
                MessageBox.Show("倉庫編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CuNo.Text.Trim() == "")//客戶編號不能空值
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
                            cmd.CommandText = "select * from discountd where invno = '" + tempinvno + "'";
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

            if (InvNo.Text.Trim() != "")
            {
                if (tempinvno != InvNo.Text.Trim())
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        try
                        {
                            //作廢
                            cmd.CommandText = "select * from nullify where invno = '" + InvNo.Text.Trim() + "'";
                            if (!cmd.ExecuteScalar().IsNullOrEmpty())
                            {
                                MessageBox.Show("此發票編號已有作廢紀錄，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                InvNo.SelectAll();
                                return;
                            }
                        }
                        catch (Exception EX)
                        {
                            MessageBox.Show(EX.ToString());
                        }
                    }
                }
            }

            jRSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRSaleD, ref dtRSaleBom, SetAllMny);
            //上一方法刪除空值後，刪除對應該筆空值之批號
            BatchF.刪除無明細對應之批號資料(dtRSaleD, dt_BatchProcess);
            BatchF.刪除無明細對應之bom批號資料(dtRSaleD, dt_Bom_BatchProcess);

            if (Common.TPS == Common.MST && X3No.Text.ToDecimal() != 2)
            {
                var checktaxmny = dtRSaleD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPS));
                if (TaxMny.Text.ToDecimal() != checktaxmny)
                {
                    MessageBox.Show("稅前合計金額有誤！無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
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

                        var result = jRSale.GetPkNumber<JBS.JS.RSale>(cmd, SaDate.Text, ref SaNo);
                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        //發票檢查
                        if (!InvNoCheck(cmd)) return;

                        //寫主檔
                        AppendMasterOnSaving(cmd);

                        //寫明細 
                        for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                        {
                            AppendDetailOnSaving(cmd, i);

                            if (dtRSaleD.Rows[i]["orid"].ToString() != "")
                            {
                                AnsyOrderdQtyOnSaving(cmd, i);
                            }
                        }

                        //寫組件明細
                        this.AppendBomOnSaving(cmd);

                        this.PassToReceivOnSaving(cmd);

                        jRSale.加庫存(cmd, dtRSaleD, dtRSaleBom, "stno", "qty");

                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, SaNo.Text, "銷退單");

                        //批次資料
                        BatchSave.進貨_Append(dt_BatchProcess, cmd, "rSaleD", SaNo.Text.Trim(), false, CuNo.Text.Trim());
                        BatchSave.進貨_Append(dt_Bom_BatchProcess, cmd, "rSaleBom", SaNo.Text.Trim(), true, CuNo.Text.Trim());

                        tn.Commit();

                        jRSale.Save(SaNo.Text.Trim());
                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新客戶應收帳款
                            jRSale.UpdateNewCustReceiv(payerno.Text.Trim(), AcctMny.Text, GetPrvAcc.Text);

                            //更新產品檔庫存量
                            jRSale.UpdateItemItStockQty(dtRSaleD, dtRSaleBom);
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
                    using (var frm = new FrmRSale_Print())
                    {
                        frm.PK = SaNo.Text.Trim();
                        frm.CuNo = CuNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);

            }

            //修改
            if (this.FormState == FormEditState.Modify)
            {
                if (jRSale.IsExistDocument<JBS.JS.RSale>(SaNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

                if (jRSale.IsPassToReceiv(SaNo.Text.Trim()) == true)
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

                        var acctmny = 0M;
                        var getprvacc = 0M;
                        jRSale.GetOldAcctMnyOnDeleting(SaNo.Text.Trim(), out acctmny, out getprvacc);

                        UpdateMasterOnSaving(cmd);//更新主檔

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                        cmd.CommandText = "delete from RSaleD where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < tempD.Rows.Count; i++)
                        {
                            if (tempD.Rows[i]["orid"].ToString() != "")
                            {
                                BackOrderdQty(cmd, i);
                            }
                        }

                        for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                        {
                            this.AppendDetailOnSaving(cmd, i);

                            if (dtRSaleD.Rows[i]["orid"].ToString() != "")
                            {
                                AnsyOrderdQtyOnSaving(cmd, i);
                            }
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                        cmd.CommandText = "delete from RSaleBom where SaNo=@sano COLLATE Chinese_Taiwan_Stroke_BIN";
                        cmd.ExecuteNonQuery();

                        this.AppendBomOnSaving(cmd);

                        this.PassToReceivOnSaving(cmd);//沖款有關??

                        jRSale.扣庫存(cmd, tempD, tempBom, "stno", "qty");
                        jRSale.加庫存(cmd, dtRSaleD, dtRSaleBom, "stno", "qty");

                        //批次資料
                        BatchSave.進貨_Modify(dt_TempBatchProcess, dt_BatchProcess, cmd, "rSaleD", SaNo.Text.Trim(), false, CuNo.Text.Trim());
                        BatchSave.進貨_Modify(dt_Bom_TempBatchProcess, dt_Bom_BatchProcess, cmd, "rSaleBom", SaNo.Text.Trim(), true, CuNo.Text.Trim());

                        tn.Commit();

                        jRSale.Save(SaNo.Text.Trim());
                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新客戶應付帳款
                            jRSale.BackOldCustReceiv(this.Oldpayerno, acctmny, getprvacc);
                            jRSale.UpdateNewCustReceiv(payerno.Text.Trim(), AcctMny.Text, GetPrvAcc.Text);

                            //更新產品檔庫存量
                            jRSale.UpdateItemItStockQty(tempD, tempBom, dtRSaleD, dtRSaleBom);
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
                    using (var frm = new FrmRSale_Print())
                    {
                        frm.PK = SaNo.Text.Trim();
                        frm.CuNo = CuNo.Text.Trim();
                        frm.ShowDialog();
                    }
                }
                jRSale.upModify0<JBS.JS.RSale>(SaNo.Text.Trim());//改回0為無修改狀態

                if (tk != null)
                    tk.Wait();
                btnAppend_Click(null, null);

                //更新current值
                //tempNo = SaNo.Text.Trim();
                //CuAdvamt.Clear();
                //Basic.SetParameter.TextBoxClear(Writes);
                //dtRSaleD.Clear();
                //dtRSaleBom.Clear();
                //setTxtWhenAppend();
                ////※新增或是複製結構編號歸零
                ////※編輯時，結構編號疊加
                //BomRec = 0;
                ////btnState = "Append";
                //SaDate.Focus();
            }
        }

        private void BackOrderdQty(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyout", tempD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("qtyNotout", tempD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("bomid", tempD.Rows[i]["orid"].ToString());
            cmd.Parameters.AddWithValue("orno", tempD.Rows[i]["orno"].ToString());
            cmd.CommandText = "update orderd set "
            + " qtyout = qtyout+@qtyout, "
            + " qtyNotout = qtyNotout-@qtyNotout "
            + " where bomid=@bomid "
            + " and orno=@orno ";
            cmd.ExecuteNonQuery();

            //檢查訂單是否結案
            cmd.CommandText = "select sum(qtyNotout) from orderd where orno=@orno and qtyNotout > 0";
            if (cmd.ExecuteScalar().ToDecimal() <= 0)
            {
                cmd.CommandText = "update [order] set "
                + " oroverflag = '" + 1 + "'"
                + " where orno=@orno";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText = "update [order] set "
                + " oroverflag = '" + 0 + "'"
                + " where orno=@orno";
                cmd.ExecuteNonQuery();
            }
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
            cmd.Parameters.AddWithValue("quno", FromSale.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName11.Text.Trim());
            cmd.Parameters.AddWithValue("cutel1", CuTel11.Text.Trim());
            cmd.Parameters.AddWithValue("cuper1", CuPer11.Text.Trim());
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
            cmd.Parameters.AddWithValue("getprvacc", 0);
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
            cmd.Parameters.AddWithValue("fromsale", FromSale.Text.Trim());
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("samemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("deno", DeNo.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("dename", DeName.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("SaPayment", SaPayment);
            cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));

            cmd.Parameters.AddWithValue("einv", EInv1.Checked ? "1" : "2");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvstate", EInv1.Checked ? EInvState.Text.Trim() : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvchange", EInv1.Checked ? EInvChange.Text : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("invrandom", invrandom);//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("payerno", payerno.Text.ToString().Trim());//帳款歸屬
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


            cmd.CommandText = "INSERT INTO RSale "
                + "(sano,sadate1,sadate2,sadateac1,sadateac2"
                + ",quno,cuno,cuname1,cutel1,cuper1,emno,emname,spno,spname,stno,stname"
                + ",xa1no,xa1name,xa1par,taxmnyb,taxmny,x3no,rate,x5no,seno,sename"
                + ",x4no,x4name,tax,totmny,taxb,totmnyb,discount,cashmny,cardmny"
                + ",cardno,ticket,collectmny,getprvacc,acctmny,samemo,bracket,recordno,invno"
                + ",invdate,invdate1,invname,invtaxno,invaddr1,invbatch"
                + ",appdate,edtdate,appscno,edtscno"
                + ",fromsale,sadate,sadateac,samemo1,deno,dename,SaPayment,PhotoPath,payerno"
                + ",einv,einvstate,einvchange,invkind,passmode,specialtax,invrandom,User_Einv,iTitle)"
                + " VALUES "
                + "(@sano,@sadate1,@sadate2,@sadateac1,@sadateac2"
                + ",@quno,@cuno,@cuname1,@cutel1,@cuper1,@emno,@emname,@spno,@spname,@stno,@stname"
                + ",@xa1no,@xa1name,@xa1par,@taxmnyb,@taxmny,@x3no,@rate,@x5no,@seno,@sename"
                + ",@x4no,@x4name,@tax,@totmny,@taxb,@totmnyb,@discount,@cashmny,@cardmny"
                + ",@cardno,@ticket,@collectmny,@getprvacc,@acctmny,@samemo,@bracket,@recordno,@invno"
                + ",@invdate,@invdate1,@invname,@invtaxno,@invaddr1,@invbatch"
                + ",@appdate,@edtdate,@appscno,@edtscno"
                + ",@fromsale,@sadate,@sadateac,@samemo1,@deno,@dename,@SaPayment,@PhotoPath,@payerno"
                + ",@einv,@einvstate,@einvchange,@invkind,@passmode,@specialtax,@invrandom,@User_Einv,@iTitle) ";
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
            cmd.Parameters.AddWithValue("quno", FromSale.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
            cmd.Parameters.AddWithValue("stno", dtRSaleD.Rows[i]["stno"].ToString().Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
            cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
            cmd.Parameters.AddWithValue("sename", SeName.Text.Trim());
            cmd.Parameters.AddWithValue("x4no", X4No.Text.Trim());
            cmd.Parameters.AddWithValue("x4name", X4Name.Text.Trim());
            cmd.Parameters.AddWithValue("itno", dtRSaleD.Rows[i]["itno"].ToString().Trim());
            cmd.Parameters.AddWithValue("itname", dtRSaleD.Rows[i]["itname"].ToString());
            cmd.Parameters.AddWithValue("ittrait", dtRSaleD.Rows[i]["ittrait"].ToString().Trim());
            cmd.Parameters.AddWithValue("itunit", dtRSaleD.Rows[i]["itunit"].ToString().Trim());
            cmd.Parameters.AddWithValue("itpkgqty", dtRSaleD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qty", dtRSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("price", dtRSaleD.Rows[i]["price"].ToDecimal("f" + Common.MS));
            cmd.Parameters.AddWithValue("prs", dtRSaleD.Rows[i]["prs"].ToDecimal("f3"));
            cmd.Parameters.AddWithValue("rate", RATEToRate(Rate));
            cmd.Parameters.AddWithValue("taxprice", dtRSaleD.Rows[i]["taxprice"].ToDecimal("f6"));
            cmd.Parameters.AddWithValue("mny", dtRSaleD.Rows[i]["mny"].ToDecimal("f" + Common.TPS));
            cmd.Parameters.AddWithValue("priceb", dtRSaleD.Rows[i]["priceb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("taxpriceb", dtRSaleD.Rows[i]["taxpriceb"].ToDecimal("f6"));
            cmd.Parameters.AddWithValue("mnyb", dtRSaleD.Rows[i]["mnyb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("memo", dtRSaleD.Rows[i]["Memo"].ToString().Trim());
            cmd.Parameters.AddWithValue("bomid", SaNo.Text + dtRSaleD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", dtRSaleD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1).ToString());
            cmd.Parameters.AddWithValue("bracket", Bracket.Text.Trim());
            cmd.Parameters.AddWithValue("itdesp1", dtRSaleD.Rows[i]["ItDesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", dtRSaleD.Rows[i]["ItDesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", dtRSaleD.Rows[i]["ItDesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", dtRSaleD.Rows[i]["ItDesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", dtRSaleD.Rows[i]["ItDesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", dtRSaleD.Rows[i]["ItDesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", dtRSaleD.Rows[i]["ItDesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", dtRSaleD.Rows[i]["ItDesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", dtRSaleD.Rows[i]["ItDesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", dtRSaleD.Rows[i]["ItDesp10"]);
            cmd.Parameters.AddWithValue("stName", dtRSaleD.Rows[i]["stname"].ToString().Trim());
            cmd.Parameters.AddWithValue("orid", dtRSaleD.Rows[i]["orid"]);
            cmd.Parameters.AddWithValue("orno", dtRSaleD.Rows[i]["orno"]);
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("mwidth1", dtRSaleD.Rows[i]["mwidth1"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth2", dtRSaleD.Rows[i]["mwidth2"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth3", dtRSaleD.Rows[i]["mwidth3"].ToDecimal());
            cmd.Parameters.AddWithValue("mwidth4", dtRSaleD.Rows[i]["mwidth4"].ToDecimal());
            cmd.Parameters.AddWithValue("pqty", dtRSaleD.Rows[i]["pqty"].ToDecimal());
            cmd.Parameters.AddWithValue("punit", dtRSaleD.Rows[i]["punit"].ToString());
            cmd.Parameters.AddWithValue("standard", dtRSaleD.Rows[i]["standard"].ToString());

            cmd.CommandText = "INSERT INTO RSaleD "
            + "(sano,sadate1,sadate2,sadateac1,sadateac2"
            + ",quno"
            + ",cuno,emno,spno"
            + ",stno,xa1no,xa1par,seno,sename"
            + ",x4no,x4name"
            + ",itno,itname"
            + ",ittrait,itunit,itpkgqty,qty,price"
            + ",prs,rate,taxprice,mny,priceb"
            + ",taxpriceb,mnyb,memo"
            + ",bomid,bomrec,recordno"
            + ",bracket"
            + ",itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
            + ",itdesp6,itdesp7,itdesp8,itdesp9,itdesp10"
            + ",stName,orid,orno,sadate,sadateac,mwidth1,mwidth2,mwidth3,mwidth4,pqty,punit,standard) "
            + " VALUES "
            + " (@sano,@sadate1,@sadate2,@sadateac1,@sadateac2"
            + ",@quno"
            + ",@cuno,@emno,@spno"
            + ",@stno,@xa1no,@xa1par,@seno,@sename"
            + ",@x4no,@x4name"
            + ",@itno,@itname"
            + ",@ittrait,@itunit,@itpkgqty,@qty,@price"
            + ",@prs,@rate,@taxprice,@mny,@priceb"
            + ",@taxpriceb,@mnyb,@memo"
            + ",@bomid,@bomrec,@recordno"
            + ",@bracket"
            + ",@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
            + ",@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10"
            + ",@stName,@orid,@orno,@sadate,@sadateac,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pqty,@punit,@standard) ";

            cmd.ExecuteNonQuery();
        }
        private void AnsyOrderdQtyOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyout", dtRSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qtyNotout", dtRSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("bomid", dtRSaleD.Rows[i]["orid"].ToString());
            cmd.Parameters.AddWithValue("orno", dtRSaleD.Rows[i]["orno"].ToString());
            cmd.CommandText = "update orderd set "
            + " qtyout = qtyout-@qtyout,"
            + " qtyNotout = qtyNotout+@qtyNotout"
            + " where bomid=@bomid"
            + " and orno=@orno";
            cmd.ExecuteNonQuery();

            //檢查訂單是否結案
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("orno", dtRSaleD.Rows[i]["orno"].ToString());
            cmd.CommandText = "select sum(qtyNotout) from orderd where orno=@orno and qtyNotout > 0";
            if (cmd.ExecuteScalar().ToDecimal() <= 0)
            {
                cmd.CommandText = "update [order] set "
                + " oroverflag = '" + 1 + "'"
                + " where orno=@orno";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText = "update [order] set "
                + " oroverflag = '" + 0 + "'"
                + " where orno=@orno";
                cmd.ExecuteNonQuery();
            }
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < dtRSaleBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("SaNo", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("BomID", SaNo.Text + dtRSaleBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("BomRec", dtRSaleBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", dtRSaleBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", dtRSaleBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", dtRSaleBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", dtRSaleBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", dtRSaleBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", dtRSaleBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", dtRSaleBom.Rows[i]["itrec"]);
                cmd.Parameters.AddWithValue("itprice", dtRSaleBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", dtRSaleBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", dtRSaleBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", dtRSaleBom.Rows[i]["itnote"]);

                cmd.CommandText = "INSERT INTO RSaleBom"
                + "(SaNo,BomID,BomRec,itno,itname"
                + ",itunit,itqty,itpareprs,itpkgqty,itrec"
                + ",itprice,itprs,itmny,itnote) VALUES "
                + "(@SaNo,@BomID,@BomRec,@itno,@itname"
                + ",@itunit,@itqty,@itpareprs,@itpkgqty,@itrec"
                + ",@itprice,@itprs,@itmny,@itnote)";
                cmd.ExecuteNonQuery();
            }
        }
        private void PassToReceivOnSaving(SqlCommand cmd)
        {
            //儲存時檢查『應退總計』與『未退金額』是否相等
            //若相等時，刪除沖款與沖款明細
            //若不等時，沖款
            decimal totmny = 0; //應退總額
            decimal acctmny = 0;//未退總額
            decimal.TryParse(TotMny.Text, out totmny);
            decimal.TryParse(AcctMny.Text, out acctmny);
            decimal collectmny = 0;//已退金額
            decimal.TryParse(CollectMny.Text, out collectmny);
            //沖款總額
            decimal _Total = 0;
            _Total = collectmny;
            //本幣總額
            decimal xa1par = 0;
            decimal _TotalB = 0;
            decimal.TryParse(Xa1Par.Text, out xa1par);
            _TotalB = _Total * xa1par;//沖款總額 * 匯率

            //刪除沖款
            string reno = "";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.CommandText = "select reno from receivd where ExtFlag =N'銷退' and SaNo =@sano COLLATE Chinese_Taiwan_Stroke_BIN";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    if (reader.Read())
                        reno = reader["reno"].ToString();
            }
            cmd.Parameters.AddWithValue("reno", reno);
            cmd.CommandText = "delete from receiv where ExtFlag =N'銷退' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from receivd where ExtFlag =N'銷退' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();


            //沖款
            if (totmny != acctmny)
            {
                //沖款主檔
                TextBoxT ReNo = new TextBoxT();
                jRSale.GetPkNumber<JBS.JS.Receiv>(cmd, SaDate.Text, ref ReNo);
                reno = ReNo.Text.Trim();

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("reno", reno);
                cmd.Parameters.AddWithValue("redate", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("redate1", Date.ToUSDate(SaDate.Text));
                cmd.Parameters.AddWithValue("cuno", payerno.Text.Trim());
                cmd.Parameters.AddWithValue("cuname1", payername.Text.Trim());
                cmd.Parameters.AddWithValue("cutel1", payerno.Text != CuNo.Text ? "" : CuTel11.Text);
                cmd.Parameters.AddWithValue("emno", EmNo.Text);
                cmd.Parameters.AddWithValue("emname", EmName.Text);
                cmd.Parameters.AddWithValue("cashmny", (-1) * CashMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("cardmny", (-1) * CardMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("cardno", CardNo.Text);
                cmd.Parameters.AddWithValue("ticket", (-1) * Ticket.Text.ToDecimal());
                cmd.Parameters.AddWithValue("getprvacc", (-1) * GetPrvAcc.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totmny", (-1) * _Total);
                cmd.Parameters.AddWithValue("actslt", 1);
                cmd.Parameters.AddWithValue("totdisc", (-1) * Discount.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totreve", (-1) * _Total);
                cmd.Parameters.AddWithValue("memo1", Bracket.Text.Trim());
                cmd.Parameters.AddWithValue("memo2", "銷退單轉入");
                cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
                cmd.Parameters.AddWithValue("Bracket", "銷退");
                cmd.Parameters.AddWithValue("recordno", 1);
                cmd.Parameters.AddWithValue("ExtFlag", "銷退");
                cmd.Parameters.AddWithValue("TotMny1", 0);
                cmd.Parameters.AddWithValue("TotExgDiff", 0);
                cmd.Parameters.AddWithValue("CheckMny", 0);
                cmd.Parameters.AddWithValue("RemitMny", 0);
                cmd.Parameters.AddWithValue("OtherMny", 0);
                cmd.Parameters.AddWithValue("AddPrvAcc", 0);
                cmd.Parameters.AddWithValue("xa1par1", Xa1Par.Text.Trim());
                cmd.Parameters.AddWithValue("offline", offline);
                cmd.Parameters.AddWithValue("online", online);
                cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                cmd.CommandText = "INSERT INTO receiv"
                + "(reno,redate,redate1"
                + ",cuno,cuname1,cutel1,emno,emname"
                + ",cashmny,cardmny,cardno,ticket"
                + ",getprvacc,totmny,actslt,totdisc,totreve"
                + ",memo1,memo2,sano,seno,Bracket"
                + ",recordno,ExtFlag,TotMny1,TotExgDiff"
                + ",CheckMny,RemitMny,OtherMny,AddPrvAcc,xa1par,offline,online,spno) VALUES "
                + "(@reno,@redate,@redate1"
                + ",@cuno,@cuname1,@cutel1,@emno,@emname"
                + ",@cashmny,@cardmny,@cardno,@ticket"
                + ",@getprvacc,@totmny,@actslt,@totdisc,@totreve"
                + ",@memo1,@memo2,@sano,@seno,@Bracket"
                + ",@recordno,@ExtFlag,@TotMny1,@TotExgDiff"
                + ",@CheckMny,@RemitMny,@OtherMny,@AddPrvAcc,@xa1par1,@offline,@online,@spno) ";
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
                cmd.Parameters.AddWithValue("bracket", "銷退");
                cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text);
                cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
                cmd.Parameters.AddWithValue("totmny", (-1) * TotMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("acctmny", (-1) * AcctMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("invno", InvNo.Text);
                cmd.Parameters.AddWithValue("discount", (-1) * Discount.Text.ToDecimal());
                cmd.Parameters.AddWithValue("reverse", (-1) * _Total);
                cmd.Parameters.AddWithValue("xa1par1", Xa1Par.Text);
                cmd.Parameters.AddWithValue("reverseb", (-1) * _TotalB);
                cmd.Parameters.AddWithValue("exgstat", "匯兌收益");
                cmd.Parameters.AddWithValue("exgdiff", 0);
                cmd.Parameters.AddWithValue("extflag", "銷退");
                cmd.Parameters.AddWithValue("payerno", payerno.Text != CuNo.Text ? payername.Text.Trim() : "");
                cmd.CommandText = "INSERT INTO receivd"
                + "(reno,redate,redate1"
                + ",cuno,emno,emname,recordno"
                + ",sadateac,sadateac1,sano,bracket"
                + ",xa1no,xa1name,xa1par"
                + ",totmny,acctmny,invno,discount"
                + ",reverse,xa1par1,reverseb,exgstat"
                + ",exgdiff,extflag,payerno) VALUES "
                + "(@reno,@redate,@redate1"
                + ",@cuno,@emno,@emname,@recordno"
                + ",@sadateac,@sadateac1,@sano,@bracket"
                + ",@xa1no,@xa1name,@xa1par"
                + ",@totmny,@acctmny,@invno,@discount"
                + ",@reverse,@xa1par1,@reverseb,@exgstat"
                + ",@exgdiff,@extflag,@payerno)";
                cmd.ExecuteNonQuery();

            }
        }
        private void UpdateMasterOnSaving(SqlCommand cmd)
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
            cmd.Parameters.AddWithValue("quno", FromSale.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName11.Text.Trim());
            cmd.Parameters.AddWithValue("cutel1", CuTel11.Text.Trim());
            cmd.Parameters.AddWithValue("cuper1", CuPer11.Text.Trim());
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
            cmd.Parameters.AddWithValue("fromsale", FromSale.Text.Trim());
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDateAc.Text));
            cmd.Parameters.AddWithValue("samemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("deno", DeNo.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("dename", DeName.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("GetPrvAcc", GetPrvAcc.Text.ToDecimal());
            cmd.Parameters.AddWithValue("offline", offline);
            cmd.Parameters.AddWithValue("online", online);
            cmd.Parameters.AddWithValue("SaPayment", SaPayment);
            cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));

            cmd.Parameters.AddWithValue("einvchange", EInv1.Checked ? EInvChange.Text : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einv", EInv1.Checked ? "1" : "2");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvstate", EInv1.Checked ? EInvState.Text.Trim() : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("payerno", payerno.Text.Trim());
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

            cmd.CommandText = "UPDATE RSale set "
            + "sano=@sano"
            + ",sadate=@sadate"
            + ",sadate1=@sadate1"
            + ",sadate2=@sadate2"
            + ",sadateac=@sadateac"
            + ",sadateac1=@sadateac1"
            + ",sadateac2=@sadateac2"
            + ",fromsale=@fromsale"
            + ",cuno=@cuno"
            + ",cuname1=@cuname1"
            + ",cutel1=@cutel1"
            + ",cuper1=@cuper1"
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
            + ",samemo=@samemo"
            + ",bracket=@bracket"
            + ",recordno=@recordno"
            + ",deno=@deno"
            + ",dename=@dename"
            + ",invno=@invno"
            + ",invdate=@invdate"
            + ",invdate1=@invdate1"
            + ",invname=@invname"
            + ",invtaxno=@invtaxno"
            + ",invaddr1=@invaddr1"
            + ",samemo1=@samemo1"
            + ",invbatch=@invbatch"
            + ",edtdate=@edtdate"
            + ",edtscno=@edtscno"
            + ",GetPrvAcc=@GetPrvAcc"
            + ",offline=@offline"
            + ",online=@online"
            + ",SaPayment=@SaPayment"
            + ",PhotoPath=@PhotoPath"
            + ",einvchange=@einvchange,einv=@einv,einvstate=@einvstate"
            + ",invkind=@invkind,specialtax=@specialtax,passmode=@passmode"
            + ",payerno=@payerno,User_Einv=@User_Einv,iTitle=@iTitle "
            + " WHERE SaNo =@sano COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
        }

        private bool InvNoCheck(SqlCommand cmd)
        {
            if (InvNo.TrimTextLenth() == 0) return true;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("InvNo", InvNo.Text.Trim());

            //銷退發票 只檢查是否有作廢紀錄
            cmd.CommandText = "select invno from nullify where invno = @InvNo";
            if (!cmd.ExecuteScalar().IsNullOrEmpty())
            {
                MessageBox.Show("此發票編號已有作廢紀錄，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InvNo.SelectAll();
                return false;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jRSale.Cancel();
            writeToTxt(pk);
            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            payerno.Enabled = false;
            jRSale.upModify0<JBS.JS.RSale>(SaNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        void GetSpecialPrice(DataRow row, int index)
        {
            var prs = 1M;
            var price = 0M;
            var SpTrait = 0M;

            var itno = row["itno"].ToString().Trim();
            var itname = row["itname"].ToString();
            var unit = row["itunit"].ToString().Trim();
            var qty = row["qty"].ToDecimal("f" + Common.Q);
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
                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);

                        cmd.CommandText = " select top 1 * from Speciald "
                                          + " where 0=0"
                                          + " and ItNo=(@itno)"
                                          + " and ItUnit=(@unit)"
                                          + " and ItPkgqty=(@itpkgqty)"
                                          + " and EDate >= '" + Date.ToTWDate(SaDate.Text) + "'"
                                          + " and SDate <= '" + Date.ToTWDate(SaDate.Text) + "'"
                                          + " order by SDate DESC,RecordNo DESC";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                SpTrait = reader["SpTrait"].ToDecimal();
                                prs = reader["prs"].ToDecimal("f3");
                                price = reader["price"].ToDecimal("f" + Common.MS);
                            }
                        }
                    }
                }
                if (SpTrait == 1)
                {
                    row["price"] = price;
                    row["prs"] = 1.000;
                    dataGridViewT1.InvalidateRow(index);
                }
                else if (SpTrait == 2)
                {
                    jRSale.Validate<JBS.JS.Item>(itno, r => price = r["itprice"].ToDecimal());

                    row["price"] = price;
                    row["prs"] = prs;
                    dataGridViewT1.InvalidateRow(index);
                }
                else
                {
                    GetSystemPrice(row, index);
                    dataGridViewT1.InvalidateRow(index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            var sum = dtRSaleD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f"+Common.TPS)).ToDecimal("f" + Common.MST);

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
                var totmny = dtRSaleD.AsEnumerable().Sum(r => r["Pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MS)).ToDecimal("f" + Common.MST);
                tax = (totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate;

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



        void ReLoadCust(SqlDataReader row)
        {
            if (row != null)
            {
                CuNo.Text = row["CuNo"].ToString().Trim();
                CuName11.Text = row["CuName1"].ToString();
                CuPer11.Text = row["CuPer1"].ToString().GetUTF8(10);
                CuTel11.Text = row["CuTel1"].ToString();
                Xa1No.Text = row["CuXa1no"].ToString();
                X3No.Text = row["CuX3no"].ToString();
                X3No1.Text = row["CuX3no"].ToString();
                X4No.Text = row["CuX4no"].ToString();
                X5No.Text = row["CuX5no"].ToString();
                SpNo.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["SpName"].ToString().Trim();
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.XX03Validate(X3No1.Text, X3No1, X3Name1, Rate1);
                pVar.XX04Validate(X4No.Text, X4No, X4Name);
                pVar.XX05Validate(X5No.Text, X5No, X5Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);
                payerno.Text = row["payerno"].ToString().Trim() == "" ? row["CuNo"].ToString() : row["payerno"].ToString();
                pVar.CuPareValidate(payerno.Text, payerno, payername);

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

                InvName.Text = row["CuInvoName"].ToString();
                if (InvName.Text.Trim() == "") InvName.Text = row["CuName2"].ToString();
                EmNo.Text = row["CuEmno1"].ToString();
                InvTaxNo.Text = row["CuUno"].ToString();

                CuAdvamt.Text = string.Format("{0:F" + CuAdvamt.LastNum + "}", row["CuAdvamt"]);
                decimal.TryParse(row["CuDisc"].ToString(), out Disc);

                for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                {
                    GetSpecialPrice(dtRSaleD.Rows[i], i);
                    SetRow_TaxPrice(dtRSaleD.Rows[i]);
                    SetRow_Mny(dtRSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                getDeNo();

                this.TextBefore = row["CuNo"].ToString().Trim();
            }
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Cust>(sender, row => ReLoadCust(row));
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

            jRSale.ValidateOpen<JBS.JS.Cust>(sender, e, row => { fillcust(row); });
        }

        private void fillcust(SqlDataReader row)
        {
            if (CuNo.Text.Trim() == TextBefore)
                return;

            CuNo.Text = row["CuNo"].ToString().Trim();
            CuName11.Text = row["CuName1"].ToString().Trim();
            CuPer11.Text = row["CuPer1"].ToString().GetUTF8(10);
            CuTel11.Text = row["CuTel1"].ToString();
            Xa1No.Text = row["CuXa1no"].ToString();
            X3No.Text = row["CuX3no"].ToString();
            X3No1.Text = row["CuX3no"].ToString();
            X4No.Text = row["CuX4no"].ToString();
            X5No.Text = row["CuX5no"].ToString();
            EmNo.Text = row["CuEmno1"].ToString();
            InvName.Text = row["CuInvoName"].ToString();
            if (InvName.Text.Trim() == "") InvName.Text = row["CuName2"].ToString();
            InvTaxNo.Text = row["CuUno"].ToString();
            SpNo.Text = row["SpNo"].ToString().Trim();
            SpName.Text = row["SpName"].ToString().Trim();
            pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
            pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
            pVar.XX03Validate(X3No1.Text, X3No1, X3Name1, Rate1);
            pVar.XX04Validate(X4No.Text, X4No, X4Name);
            pVar.XX05Validate(X5No.Text, X5No, X5Name);
            pVar.EmplValidate(EmNo.Text, EmNo, EmName);

            payerno.Text = row["payerno"].ToString().Trim() == "" ? row["CuNo"].ToString() : row["payerno"].ToString();
            pVar.CuPareValidate(payerno.Text, payerno, payername);
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

            CuAdvamt.Text = row["CuAdvamt"].ToDecimal("f" + CuAdvamt.LastNum).ToString();
            Disc = row["CuDisc"].ToDecimal();
            if (this.FormState != FormEditState.Modify)
            {
                for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                {
                    GetSpecialPrice(dtRSaleD.Rows[i], i);
                    SetRow_TaxPrice(dtRSaleD.Rows[i]);
                    SetRow_Mny(dtRSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }
            getDeNo();

            this.TextBefore = row["CuNo"].ToString().Trim();
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Stkroom>(sender, row => DoubleToGetStNo(row));
        }
        void DoubleToGetStNo(SqlDataReader reader)
        {
            StNo.Text = reader["StNo"].ToString().Trim();
            StName.Text = reader["StName"].ToString().Trim();

            if (Common.Sys_StNoMode == 1)
            {
                for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                {
                    dtRSaleD.Rows[i]["stno"] = StNo.Text;
                    dtRSaleD.Rows[i]["StName"] = StName.Text;
                    dataGridViewT1.InvalidateRow(i);
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtRSaleD, i, StNo.Text.Trim(), StName.Text.Trim());
                    BatchF.BOM同步批次異動倉庫(dt_Bom_BatchProcess, dtRSaleD, dtRSaleBom, i);
                }
            }

            this.TextBefore = reader["StNo"].ToString().Trim();
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

            jRSale.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                if (TextBefore == StNo.Text.Trim())
                    return;

                DoubleToGetStNo(row);
            });
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.XX03>(sender, row =>
            {
                X3No.Text = row["X3No"].ToString().Trim();
                X3No1.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                X3Name1.Text = row["X3Name"].ToString();
                decimal rate = row["X3Rate"].ToString().ToDecimal() * 100;
                Rate.Text = rate.ToString();
                Rate1.Text = rate.ToString();

                //內含稅=>二聯發票
                if (X3No.Text == "2")
                {
                    X5No.Text = "2";
                    pVar.XX05Validate("2", X5No, X5Name);
                }

                //完成稅別設定，重新計算金額
                for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtRSaleD.Rows[i]);
                    SetRow_Mny(dtRSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["X3No"].ToString().Trim();
            });
        }
        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (X3No.ReadOnly) return;
            if (btnCancel.Focused) return;

            TextBox tx = sender as TextBox;
            if (tx.TrimTextLenth() == 0 || tx.Text.ToDecimal() == 0)
            {
                e.Cancel = true;
                MessageBox.Show("稅別編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tx.SelectAll();
                return;
            }

            jRSale.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                if (tx.Text.Trim() == TextBefore)
                    return;

                X3No.Text = row["X3No"].ToString().Trim();
                X3No1.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                X3Name1.Text = row["X3Name"].ToString();
                decimal rate = row["X3Rate"].ToString().ToDecimal() * 100;
                Rate.Text = rate.ToString("f" + Rate.LastNum);
                Rate1.Text = rate.ToString("f" + Rate.LastNum);

                //內含稅=>二聯發票
                if (X3No.Text == "2")
                {
                    X5No.Text = "2";
                    pVar.XX05Validate("2", X5No, X5Name);
                }

                //完成稅別設定，重新計算金額
                for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtRSaleD.Rows[i]);
                    SetRow_Mny(dtRSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["X3No"].ToString().Trim();
            });
        }

        private void X4No_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.XX04>(sender, reader =>
            {
                X4No.Text = reader["X4No"].ToString().Trim();
                X4Name.Text = reader["X4Name"].ToString().Trim();
            });
        }
        private void X4No_Validating(object sender, CancelEventArgs e)
        {
            if (X4No.ReadOnly || btnCancel.Focused) return;
            if (X4No.TrimTextLenth() == 0)
            {
                X4No.Clear();
                X4Name.Clear();
                return;
            }

            jRSale.ValidateOpen<JBS.JS.XX04>(sender, e, reader =>
            {
                X4No.Text = reader["X4No"].ToString().Trim();
                X4Name.Text = reader["X4Name"].ToString().Trim();
            }, true);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Empl>(sender, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
                DeNo.Text = reader["emdeno"].ToString().Trim();

                jRSale.Validate<JBS.JS.Dept>(DeNo.Text, row => DeName.Text = row["dename1"].ToString(), () => DeName.Clear());
            });
        }
        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (EmNo.ReadOnly || btnCancel.Focused) return;
            if (EmNo.TrimTextLenth() == 0)
            {
                EmNo.Clear();
                EmName.Clear();
                DeNo.Clear();
                DeName.Clear();
                return;
            }

            jRSale.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
                DeNo.Text = reader["emdeno"].ToString().Trim();

                jRSale.Validate<JBS.JS.Dept>(DeNo.Text, row => DeName.Text = row["dename1"].ToString(), () => DeName.Clear());

            }, true);
        }

        private void SeNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Send>(sender, reader =>
            {
                SeNo.Text = reader["SeNo"].ToString().Trim();
                SeName.Text = reader["SeName"].ToString().Trim();
            });
        }
        private void SeNo_Validating(object sender, CancelEventArgs e)
        {
            if (SeNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SeNo.Text.Trim() == "")
            {
                SeNo.Clear();
                SeName.Clear();
                return;
            }

            jRSale.ValidateOpen<JBS.JS.Send>(sender, e, reader =>
            {
                SeNo.Text = reader["SeNo"].ToString().Trim();
                SeName.Text = reader["SeName"].ToString().Trim();
            }, true);
        }

        private void X5No_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.XX05>(sender, reader =>
            {
                X5No.Text = reader["X5No"].ToString().Trim();
                X5Name.Text = reader["X5Name"].ToString().Trim();
            });
        }
        private void X5No_Validating(object sender, CancelEventArgs e)
        {
            if (X5No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (X5No.TrimTextLenth() == 0)
            {
                X5No.Clear();
                X5Name.Clear();
                e.Cancel = true;
                MessageBox.Show("發票類別編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jRSale.ValidateOpen<JBS.JS.XX05>(sender, e, reader =>
            {
                X5No.Text = reader["X5No"].ToString().Trim();
                X5Name.Text = reader["X5Name"].ToString().Trim();
            });

            if (X5No.Text == "7" || X5No.Text == "8")//使用
                EInv1.Checked = true;
            else
                EInv2.Checked = true;
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Spec>(sender, reader =>
            {
                SpNo.Text = reader["SpNo"].ToString().Trim();
                SpName.Text = reader["SpName"].ToString().Trim();
            });
        }
        private void SpNo_Validating(object sender, CancelEventArgs e)
        {
            if (SpNo.ReadOnly || btnCancel.Focused) return;
            if (SpNo.TrimTextLenth() == 0)
            {
                SpNo.Clear();
                SpName.Clear();
                return;
            }

            jRSale.ValidateOpen<JBS.JS.Spec>(sender, e, reader =>
            {
                SpNo.Text = reader["SpNo"].ToString().Trim();
                SpName.Text = reader["SpName"].ToString().Trim();
            }, true);
        }

        private void Text_Enter(object sender, EventArgs e)
        {
            //SaNo,StNo,CuNo,X3No,X3No1
            TextBefore = (sender as TextBox).Text;
        }


        private void SaNo_DoubleClick(object sender, EventArgs e)
        {
            if (SaNo.ReadOnly)
                return;

            using (var frm = new FrmRSale_Print_SaNo())
            {
                frm.TSeekNo = (sender as TextBoxT).Text;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    SaNo.Text = frm.TResult;
                }
            }

        }

        private void SaNo_Validating(object sender, CancelEventArgs e)
        {
            if (SaNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SaNo.Text.Length > 0 && SaNo.Text.Trim() == "")
            {
                e.Cancel = true;
                SaNo.Text = "";
                SaNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jRSale.IsExistDocument<JBS.JS.RSale>(SaNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jRSale.IsExistDocument<JBS.JS.RSale>(SaNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jRSale.IsExistDocument<JBS.JS.RSale>(SaNo.Text.Trim()))
                {
                    if (TextBefore == SaNo.Text.Trim())
                        return;

                    writeToTxt(SaNo.Text.Trim());
                    loadRSaleBom();
                }
                else
                {
                    e.Cancel = true;
                    SaNo.SelectAll();

                    using (var frm = new FrmRSale_Print_SaNo())
                    {
                        frm.TSeekNo = (sender as TextBoxT).Text;

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            SaNo.Text = frm.TResult;
                            writeToTxt(frm.TResult);
                            loadRSaleBom();
                        }
                    }

                }
            }
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
        }


        private void Xa1Par_Validating(object sender, CancelEventArgs e)
        {
            if (Xa1Par.ReadOnly != true && Xa1Par.Text.Trim() != "" && dataGridViewT1.Rows.Count > 0)
            {
                //離開匯率設定，重新計算本幣金額
                for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                {
                    SetRow_Mny(dtRSaleD.Rows[i]);
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
            if (Discount.ReadOnly) return;
            if (sender.Equals(Discount)) Discount1.Text = Discount.Text;
            if (sender.Equals(Discount1)) Discount.Text = Discount1.Text;
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

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() != "")
                if (dataGridViewT1.Rows.Count == 0)
                    if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        void GridSaleDAddRows()
        {
            DataRow dRow = dtRSaleD.NewRow();
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

            dtRSaleD.Rows.Add(dRow);
            dtRSaleD.AcceptChanges();
        }

        void DeleteEmptyRow(int index)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                //刪除明細前，先刪除明細的『組件明細』
                string rec = dataGridViewT1.Rows[index].Cells["結構編號"].EditedFormattedValue.ToString().Trim();
                jRSale.RemoveBom(rec, ref dtRSaleBom);

                //刪除明細
                dataGridViewT1.Rows.Remove(dataGridViewT1.Rows[index]);
                dtRSaleD.AcceptChanges();

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
            DataRow dRow = dtRSaleD.NewRow();
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

            dtRSaleD.Rows.InsertAt(dRow, index);
            dtRSaleD.AcceptChanges();
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

                jRSale.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SaNo.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                if (dataGridViewT1["訂單憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jRSale.DataGridViewOpen<JBS.JS.Item>(sender, e, dtRSaleD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (CurrentColumnName == "出貨倉庫")
            {
                #region 出貨倉庫
                if (dataGridViewT1.Columns["出貨倉庫"].ReadOnly)
                    return;

                jRSale.DataGridViewOpen<JBS.JS.Stkroom>(sender, e, dtRSaleD, row =>
                {
                    dtRSaleD.Rows[e.RowIndex]["stno"] = row["StNo"].ToString();
                    dtRSaleD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtRSaleD, e.RowIndex, row["StNo"].ToString(), row["StName"].ToString());
                    BatchF.BOM同步批次異動倉庫(dt_Bom_BatchProcess, dtRSaleD, dtRSaleBom, e.RowIndex);
                });
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                if (dataGridViewT1["訂單憑證", e.RowIndex].Value.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itno = dtRSaleD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jRSale.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (row != null && unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        dtRSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            dtRSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            dtRSaleD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;

                dtRSaleD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)//1代表一般進銷存
                {
                    GetSpecialPrice(dtRSaleD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtRSaleD.Rows[e.RowIndex]);
                    SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);

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

                        dtRSaleD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
            else if (CurrentColumnName == "銷退數量")
            {
                #region 銷退數量
                if (Common.Sys_DBqty == 1)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = dtRSaleD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtRSaleD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtRSaleD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtRSaleD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtRSaleD.Rows[e.RowIndex]["Pformula"].ToString();

                        var tb = (TextBox)dataGridViewT1.EditingControl;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtRSaleD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtRSaleD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtRSaleD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtRSaleD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtRSaleD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                            tb.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtRSaleD.Rows[e.RowIndex]["Qty"] = frm.resultCount.ToString("f" + Common.Q);
                            dtRSaleD.Rows[e.RowIndex]["PQty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);
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
                        frm.w1 = dtRSaleD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtRSaleD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtRSaleD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtRSaleD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtRSaleD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = dtRSaleD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "銷退數量";

                        var tb = (TextBox)dataGridViewT1.EditingControl;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtRSaleD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtRSaleD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtRSaleD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtRSaleD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtRSaleD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;
                            dtRSaleD.Rows[e.RowIndex]["qty"] = frm.qty;

                            tb.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtRSaleD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);
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
                if (dtRSaleD.Rows[e.RowIndex]["itno"].ToString().Trim() == "") return;
                using (var frm = new FrmItemLevelb())
                {
                    frm.TSeekNo = dtRSaleD.Rows[e.RowIndex]["itno"].ToString().Trim();
                    frm.itunit = dtRSaleD.Rows[e.RowIndex]["itunit"].ToString().Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewT1.EditingControl.Text = frm.Result.ToDecimal().ToString("f" + Common.MS);
                        dtRSaleD.Rows[e.RowIndex]["price"] = frm.Result.ToDecimal("f" + Common.MS);
                        SetRow_TaxPrice(dtRSaleD.Rows[e.RowIndex]);
                        SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
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
                            dtRSaleD.Rows[e.RowIndex]["punit"] = frm.Result;
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
            if (CurrentColumnName == "訂單憑證")
            {
                #region 訂單憑證
                if (dataGridViewT1.EditingControl.IsNotNull() && dataGridViewT1.EditingControl.Text.Trim() == "")
                {
                    dtRSaleD.Rows[e.RowIndex]["orid"] = "";
                    dtRSaleD.Rows[e.RowIndex]["orno"] = "";
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
            else if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                //空值->清空
                if (ItNoNow == "" || ItNoNow.Trim().Length == 0)
                {
                    //if (btnSave.Focused) return;
                    dtRSaleD.Rows[e.RowIndex]["orno"] = "";
                    dtRSaleD.Rows[e.RowIndex]["orid"] = "";
                    dtRSaleD.Rows[e.RowIndex]["itno"] = "";
                    dtRSaleD.Rows[e.RowIndex]["ItNoUdf"] = "";
                    dtRSaleD.Rows[e.RowIndex]["itname"] = "";
                    dtRSaleD.Rows[e.RowIndex]["itunit"] = "";
                    dtRSaleD.Rows[e.RowIndex]["Punit"] = "";
                    dtRSaleD.Rows[e.RowIndex]["qty"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["Pqty"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["Price"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["TaxPrice"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["Mny"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["ItPkgQty"] = 1;
                    dtRSaleD.Rows[e.RowIndex]["ItTrait"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["產品組成"] = "";
                    dtRSaleD.Rows[e.RowIndex]["Memo"] = "";
                    dtRSaleD.Rows[e.RowIndex]["PriceB"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["TaxPriceB"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["MnyB"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["StNo"] = StNo.Text;
                    dtRSaleD.Rows[e.RowIndex]["StName"] = StName.Text;
                    dtRSaleD.Rows[e.RowIndex]["mwidth1"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["mwidth2"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["mwidth3"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["mwidth4"] = 0;
                    dtRSaleD.Rows[e.RowIndex]["Pformula"] = "";
                    //折數
                    dtRSaleD.Rows[e.RowIndex]["Prs"] = (Common.Sys_SalePrice == 2) ? Disc : 1;

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = dtRSaleD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jRSale.RemoveBom(rec, ref dtRSaleBom);
                    //刪除批次異動資訊
                    BatchF.刪除批次異動(dt_BatchProcess, rec);
                    BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);
                    return;
                }
                //值沒變->離開
                if (ItNoNow == ItNoBegin)
                    return;

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    dtRSaleD.Rows[e.RowIndex]["itno"] = ItNoBegin;
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

                        dtRSaleD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                //值變了，跟產品編號和自定編號都不一樣,帶值出來                //若找不到這筆資料則開窗
                if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)
                {
                    jRSale.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtRSaleD, row => FillItem(row, e.RowIndex));
                }
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                string itno = dtRSaleD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    if (dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim() != TextBefore)
                    {
                        e.Cancel = true;
                        MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;

                        dtRSaleD.Rows[e.RowIndex]["itunit"] = TextBefore;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    return;
                }

                jRSale.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        dtRSaleD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        dtRSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                dtRSaleD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    GetSpecialPrice(dtRSaleD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtRSaleD.Rows[e.RowIndex]);
                    SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
                #endregion
            }
            else if (CurrentColumnName == "銷退數量")
            {
                #region 銷退數量
                var qty = dataGridViewT1["銷退數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (Common.Sys_DBqty == 1)
                {
                    dtRSaleD.Rows[e.RowIndex]["Qty"] = qty;
                    dtRSaleD.Rows[e.RowIndex]["PQty"] = qty;
                }
                else if (Common.Sys_DBqty == 2)
                {
                    dtRSaleD.Rows[e.RowIndex]["Qty"] = qty;
                }
                SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "計價數量")
            {
                #region 計價數量
                dtRSaleD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);

                SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "售價")
            {
                #region 售價
                dtRSaleD.Rows[e.RowIndex]["Price"] = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);
                if (Common.Sys_LowCost != 3 && dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() != "")
                    pVar.CheckLowCost(dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["單位", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS));

                SetRow_TaxPrice(dtRSaleD.Rows[e.RowIndex]);
                SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "折數")
            {
                #region 折數
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                dtRSaleD.Rows[e.RowIndex]["Prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue;

                SetRow_TaxPrice(dtRSaleD.Rows[e.RowIndex]);
                SetRow_Mny(dtRSaleD.Rows[e.RowIndex]);
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

                dtRSaleD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MS);
                        dtRSaleD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MS);
                        dtRSaleD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(dtRSaleD.Rows[e.RowIndex]);

                taxprice = dtRSaleD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                dtRSaleD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                dtRSaleD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                dtRSaleD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
                #endregion
            }
            else if (CurrentColumnName == "出貨倉庫")
            {
                #region 出貨倉庫
                if (dataGridViewT1.Columns["出貨倉庫"].ReadOnly)
                    return;

                jRSale.DataGridViewValidateOpen<JBS.JS.Stkroom>(sender, e, dtRSaleD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["stno"].ToString().Trim();

                    dtRSaleD.Rows[e.RowIndex]["StNo"] = row["StNo"].ToString();
                    dtRSaleD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();
                    BatchF.同步批次異動倉庫(dt_BatchProcess, dtRSaleD, e.RowIndex, row["StNo"].ToString(), row["StName"].ToString());
                    BatchF.BOM同步批次異動倉庫(dt_Bom_BatchProcess, dtRSaleD, dtRSaleBom, e.RowIndex);
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
                        MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = TextBefore;
                        dtRSaleD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                    }
                }
                else
                {
                    dtRSaleD.Rows[e.RowIndex]["itpkgqty"] = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
        }

        void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["ItNo"].ToString().Trim();

            this.ItNoBegin = itno;
            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;
            dtRSaleD.Rows[index]["ItNo"] = itno;
            dtRSaleD.Rows[index]["ItName"] = reader["ItName"];
            dtRSaleD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"];
            dtRSaleD.Rows[index]["Punit"] = reader["Punit"];
            //帶入產品常用倉庫
            if (reader["stno"].ToString().Trim().Length > 0)
            {
                dtRSaleD.Rows[index]["stno"] = reader["stno"].ToString();
                dtRSaleD.Rows[index]["stname"] = SQL.ExecuteScalar("select TOP 1 stname from stkroom where stno = @stno", new parameters("stno", reader["stno"].ToString()));
            }
            //銷貨常用單位
            string utype = reader["ItSalUnit"].ToString().Trim();
            string unit;
            //預設帶包裝單位或是單位
            if (utype == "1")
            {
                unit = reader["ItUnitp"].ToString();
                dtRSaleD.Rows[index]["ItUnit"] = unit;

                var itpkgqty = reader["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                dtRSaleD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                unit = reader["ItUnit"].ToString();
                dtRSaleD.Rows[index]["ItUnit"] = unit;
                dtRSaleD.Rows[index]["itpkgqty"] = 1;
            }

            GetSpecialPrice(dtRSaleD.Rows[index], index);
            SetRow_TaxPrice(dtRSaleD.Rows[index]);
            SetRow_Mny(dtRSaleD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            //組合組裝品
            string trait = reader["ItTrait"].ToString();
            dtRSaleD.Rows[index]["ItTrait"] = trait;

            if (trait == "1") trait = "組合品";
            else if (trait == "2") trait = "組裝品";
            else if (trait == "3") trait = "單一商品";
            dataGridViewT1["產品組成", index].Value = trait;

            //產品規格說明
            for (int x = 1; x <= 10; x++)
            {
                dtRSaleD.Rows[index]["ItDesp" + x] = reader["ItDesp" + x];
            }

            //BOM
            var rec = dataGridViewT1["結構編號", index].EditedFormattedValue.ToString().Trim();
            jRSale.RemoveBom(rec, ref dtRSaleBom);
            jRSale.GetItemBom(itno, rec, ref dtRSaleBom);
            //刪除批次異動資訊
            BatchF.刪除批次異動(dt_BatchProcess, rec);
            BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);
            //回寫對應的客戶型號
            rowstandard(itno, index);
        }

        private void rowstandard(string itno, int index)
        {
            try
            {
                dtstandard.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", itno);
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim().ToString());
                    if (itno == null)
                    {
                        for (int i = 0; i < dataGridViewT1.RowCount; i++)
                        {
                            dtstandard.Clear();
                            cmd.Parameters["itno"].Value = dataGridViewT1.Rows[i].Cells["產品編號"].Value.ToString();
                            cmd.CommandText = " select * from standard where itno=@itno and cfno=@cuno";
                            da.Fill(dtstandard);
                            if (dtstandard.Rows.Count != 0)
                                dtRSaleD.Rows[i]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                            else
                                dtRSaleD.Rows[i]["standard"] = "";
                        }
                    }
                    else
                    {
                        cmd.CommandText = " select * from standard where itno=@itno and cfno=@cuno";
                        da.Fill(dtstandard);
                        if (dtstandard.Rows.Count != 0)
                            dtRSaleD.Rows[index]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                        else
                            dtRSaleD.Rows[index]["standard"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = true;
            }
            else if (Common.Series == "72")
            {
                dataGridViewT1.Columns["出貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = true;
            }
            else if (Common.Series == "71")
            {
                dataGridViewT1.Columns["出貨倉庫"].ReadOnly = false;
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = true;
            }

            pEInv.Enabled = EInvChange.Enabled = !CuNo.ReadOnly;//電子發票
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
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells["產品編號"];
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
                string rec = dataGridViewT1.CurrentRow.Cells["結構編號"].EditedFormattedValue.ToString().Trim();
                jRSale.RemoveBom(rec, ref dtRSaleBom);

                //刪除明細
                int index = dataGridViewT1.SelectedRows[0].Index;
                dtRSaleD.Rows.RemoveAt(index);
                dtRSaleD.AcceptChanges();

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
            gridItDesp.Focus();
            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(true, JE.MyControl.FormStyle.Mini))
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }
                frm.dr = dtRSaleD.Rows[index];
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
                    string rec = dataGridViewT1.SelectedRows[0].Cells["結構編號"].EditedFormattedValue.ToString().Trim();
                    DataTable table = dtRSaleBom.Clone();
                    frm.FormName = "RSaleBom";
                    for (int i = 0; i < dtRSaleBom.Rows.Count; i++)
                    {
                        if (dtRSaleBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                        {
                            table.ImportRow(dtRSaleBom.Rows[i]);
                            dtRSaleBom.Rows.RemoveAt(i--);
                        }
                    }

                    table.AcceptChanges();
                    dtRSaleBom.AcceptChanges();
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    frm.dtD = table.Copy();
                    frm.BomRec = rec;
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.grid = dataGridViewT1;

                    #region 批次資料傳遞
                    int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
                    //勾稽明細
                    string BomRec = dtRSaleD.Rows[Index_gv]["BomRec"].ToString();
                    DataTable temp = dt_Bom_BatchProcess.Clone();
                    for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
                    {
                        if (dt_Bom_BatchProcess.Rows[i]["BomRec"].ToString() == BomRec)
                        {
                            dt_Bom_BatchProcess.Rows[i]["BomRec"] = BomRec;
                            temp.ImportRow(dt_Bom_BatchProcess.Rows[i]);
                        }
                    }

                    frm.上層Row = dtRSaleD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                    frm.dt_Bom_BatchProcess = temp;
                    #endregion


                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            #region 批次資料取回
                            //先刪除
                            for (int i = dt_Bom_BatchProcess.Rows.Count - 1; i >= 0; i--)
                            {
                                if (dt_Bom_BatchProcess.Rows[i]["BomRec"].ToString() == BomRec)
                                    dt_Bom_BatchProcess.Rows.RemoveAt(i);
                            }
                            //後加入
                            dt_Bom_BatchProcess.Merge(frm.dt_Bom_BatchProcess);
                            #endregion
                            if (frm.CallBack == "Money")
                            {
                                dtRSaleBom.Merge(frm.dtD);
                                dtRSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MS);
                                dataGridViewT1.Focus();
                                SetRow_TaxPrice(dtRSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetRow_Mny(dtRSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetAllMny();
                                break;
                            }
                            else
                            {
                                dtRSaleBom.Merge(frm.dtD);
                                dtRSaleBom.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            dtRSaleBom.Merge(table);
                            dtRSaleBom.AcceptChanges();
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
            var itno = dtRSaleD.Rows[index]["itno"].ToString().Trim();
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
            var itno = dtRSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶此產品交易 frm = new S2.Frm所有客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
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
            var itno = (index == -1) ? "" : dtRSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史交易 frm = new S2.Frm該客戶歷史交易())
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

            if (jRSale.EnableBShopPrice() == false)
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
            var itno = dtRSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridCost_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dtRSaleD.AsEnumerable().Any(r => r["itno"].ToString().Trim() == ""))
            {
                MessageBox.Show("明細尚有產品編號未輸入，請登打完畢後再進行查詢");
                return;
            }

            if (btnAppend.Enabled)
            {
                dtRSaleBom.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@SaNo", SaNo.Text.Trim());
                    cmd.CommandText = "select * from rSaleBom where SaNo=@SaNo COLLATE Chinese_Taiwan_Stroke_BIN";

                    da.Fill(dtRSaleBom);
                }
            }

            gridCost.Focus();
            if (dataGridViewT1.SelectedRows.Count > 0)
            {
                using (S2.Frm成本與毛利分析 frm = new S2.Frm成本與毛利分析(SaDate.Text))
                {
                    frm.dtD = dtRSaleD.Copy();
                    frm.bom = dtRSaleBom.Copy();
                    frm.date = Date.ToTWDate(SaDate.Text);
                    frm.ShowDialog();
                }
            }
            dataGridViewT1.Focus();
        }

        private void gridAddress_Click(object sender, EventArgs e)
        {
            jRSale.Open<JBS.JS.Cust>(CuNo, reader =>
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

            using (var frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jRSale.keyMan.AppendMan;
                frm.ATime = jRSale.keyMan.AppendTime;
                //修改人員
                frm.EName = jRSale.keyMan.EditMan;
                frm.ETime = jRSale.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void gridInvNo_Click(object sender, EventArgs e)
        {
            var sano = SaNo.Text.Trim();
            var IsPOS = (Bracket.Text.Trim() == "前台") ? true : false;
            using (FrmSale_Inv frm = new FrmSale_Inv("RSale", sano, IsPOS))
            {
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


        private void FromSale_DoubleClick(object sender, EventArgs e)
        {
            if (FromSale.ReadOnly)
                return;

            using (var frm = new FrmSaleToRSale())
            {
                frm.CuNo = CuNo.Text.Trim();
                frm.SeekNo = FromSale.Text.Trim();

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                jRSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRSaleD, ref dtRSaleBom, SetAcctMny);

                PassSaleM(frm);

                PassSaleD(frm);

                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                    dataGridViewT1.Focus();
                }
            }

            FromSale.Tag = FromSale.Text.Trim();
            SetAllMny();
        }
        private void FromSale_Validating(object sender, CancelEventArgs e)
        {
            if (FromSale.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FromSale.TrimTextLenth() == 0)
            {
                FromSale.Tag = "";
                return;
            }

            if (FromSale.Tag == null)
                FromSale.Tag = "";

            if (jRSale.IsExistDocument<JBS.JS.Sale>(FromSale.Text.Trim()))
            {
                if (FromSale.Tag.ToString() == FromSale.Text.Trim())
                    return;

                using (var frm = new FrmSaleToRSale())
                {
                    frm.CuNo = CuNo.Text.Trim();
                    frm.SeekNo = FromSale.Text.Trim();


                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jRSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRSaleD, ref dtRSaleBom, SetAcctMny);

                    PassSaleM(frm);

                    PassSaleD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                FromSale.Tag = FromSale.Text.Trim();
                SetAllMny();
            }
            else
            {
                e.Cancel = true;
                using (var frm = new FrmSaleToRSale())
                {
                    frm.CuNo = CuNo.Text.Trim();
                    frm.SeekNo = FromSale.Text.Trim();


                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jRSale.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtRSaleD, ref dtRSaleBom, SetAcctMny);

                    PassSaleM(frm);

                    PassSaleD(frm);

                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", dataGridViewT1.Rows.Count - 1];
                        dataGridViewT1.Focus();
                    }
                }
                FromSale.Tag = FromSale.Text.Trim();
                SetAllMny();
            }
        }
        private void PassSaleM(FrmSaleToRSale frm)
        {
            FromSale.Text = frm.SaNo;
            FromSale.Tag = frm.SaNo;
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
            SaMemo.Text = frm.MasterRow["SaMemo"].ToString();
            X4No.Text = frm.MasterRow["X4No"].ToString();
            X4Name.Text = frm.MasterRow["X4Name"].ToString();
            SpNo.Text = frm.MasterRow["SpNo"].ToString();
            SpName.Text = frm.MasterRow["SpName"].ToString();
            SeNo.Text = frm.MasterRow["SeNo"].ToString();
            SeName.Text = frm.MasterRow["SeName"].ToString();
            InvName.Text = frm.MasterRow["InvName"].ToString();
            InvTaxNo.Text = frm.MasterRow["InvTaxNo"].ToString();
            //InvNo.Text = frm.MasterRow["InvNo"].ToString(); //銷貨單轉入消退單時不需要帶入發票號碼 20160127 開會討論通過
            InvDate = frm.MasterRow["InvDate"].ToString();
            InvAddr1 = frm.MasterRow["InvAddr1"].ToString();
            X5No.Text = frm.MasterRow["X5no"].ToString();
            pVar.XX05Validate(X5No.Text, X5No, X5Name);
            pVar.XX03Validate(X3No.Text, X3No, X3Name);
            pVar.XX03Validate(X3No1.Text, X3No1, X3Name1);
            //請款客戶
            payerno.Text = frm.MasterRow["payerno"].ToString();
            User_Einv.Text = frm.MasterRow["User_Einv"].ToString();
            iTitle.Text = frm.MasterRow["iTitle"].ToString();
            jRSale.Validate<JBS.JS.Cust>(payerno.Text.ToString(), r => { payername.Text = r["cuname1"].ToString(); }, () => { payername.Clear(); });

            this.Memo1 = frm.MasterRow["samemo1"].ToString();
            SaPayment = frm.MasterRow["SaPayment"].ToString();
            CardNo.Text = frm.MasterRow["CardNo"].ToString();
            PhotoPath = frm.MasterRow["PhotoPath"].ToString();
            if (CuNo.Text.Trim() == "")
            {
                CuNo.Text = frm.MasterRow["CuNo"].ToString();
                CuName11.Text = frm.MasterRow["CuName1"].ToString();
                CuPer11.Text = frm.MasterRow["CuPer1"].ToString().GetUTF8(10);
                CuTel11.Text = frm.MasterRow["CuTel1"].ToString();

                jRSale.Validate<JBS.JS.Cust>(CuNo.Text, row =>
                {
                    CuAdvamt.Text = row["CuAdvamt"].ToDecimal().ToString("f" + CuAdvamt.LastNum);
                    this.Disc = row["CuDisc"].ToDecimal("f4");
                });
            }

            if (frm.MasterRow["einv"].ToDecimal() == 1)
                EInv1.Checked = true;
            else
                EInv2.Checked = true;
            EInvChange.Text = frm.MasterRow["einvchange"].ToString();
            EInvState.Text = "未上傳";

            invkind = frm.MasterRow["invkind"].ToString();
            specialtax = frm.MasterRow["specialtax"].ToString();
            passmode = frm.MasterRow["passmode"].ToString();
            invrandom = frm.MasterRow["invrandom"].ToString();
        }
        private void PassSaleD(FrmSaleToRSale frm)
        {
            DataRow row = null;
            DataTable dtD = frm.dtDetail;

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                var rec = GetBomRec();
                row = dtRSaleD.NewRow();
                row["itno"] = dtD.Rows[i]["itno"];
                row["itname"] = dtD.Rows[i]["itname"];
                row["stno"] = dtD.Rows[i]["stno"];
                row["stname"] = dtD.Rows[i]["stname"];
                row["ittrait"] = dtD.Rows[i]["ittrait"];
                row["ItNoUdf"] = dtD.Rows[i]["ItNoUdf"];
                if (dtD.Rows[i]["ittrait"].ToString() == "1") row["產品組成"] = "組合品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "2") row["產品組成"] = "組裝品";
                else if (dtD.Rows[i]["ittrait"].ToString() == "3") row["產品組成"] = "單一商品";
                row["itunit"] = dtD.Rows[i]["itunit"];
                row["qty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["Punit"] = dtD.Rows[i]["Punit"];
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
                row["orid"] = dtD.Rows[i]["orid"];
                row["orno"] = dtD.Rows[i]["orno"];
                row["mwidth1"] = dtD.Rows[i]["mwidth1"];
                row["mwidth2"] = dtD.Rows[i]["mwidth2"];
                row["mwidth3"] = dtD.Rows[i]["mwidth3"];
                row["mwidth4"] = dtD.Rows[i]["mwidth4"];
                row["Pformula"] = dtD.Rows[i]["Pformula"];
                row["standard"] = dtD.Rows[i]["standard"];
                for (int j = 1; j < 11; j++)
                {
                    row["itdesp" + j] = dtD.Rows[i]["itdesp" + j];
                }
                dtRSaleD.Rows.Add(row);
                dtRSaleD.AcceptChanges();
                dataGridViewT1.InvalidateRow(dtRSaleD.Rows.Count - 1);

                if (dtD.Rows[i]["ittrait"].ToString().Trim() == "3")
                    continue;

                var sabomid = dtD.Rows[i]["bomid"].ToString().Trim();
                jRSale.GetTBom<JBS.JS.Sale>(sabomid, rec.ToString(), ref dtRSaleBom);

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
                    cmd.Parameters.Clear();
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
            else if (keyData == Keys.F2)
            {
                if (gridAppend.Enabled) gridAppend_Click(null, null);
            }
            else if (keyData == Keys.F3)
            {
                if (gridDelete.Enabled) gridDelete_Click(null, null);
            }
            else if (keyData == Keys.F5)
            {
                if (gridInsert.Enabled) gridInsert_Click(null, null);
            }
            else if (keyData == Keys.F6)
            {
                if (gridItDesp.Enabled) gridItDesp_Click(null, null);
            }
            else if (keyData == Keys.F7)
            {
                if (gridBomD.Enabled) gridBomD_Click(null, null);
            }
            else if (keyData == Keys.F8)
            {
                if (gridStock.Enabled) gridStock_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift"))
            {
                if (gridTran.Enabled) gridTran_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift"))
            {
                if (gridAllTrans.Enabled) gridAllTrans_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F11") && keyData.ToString().EndsWith("Shift"))
            {
                if (gridItBuyPrice.Enabled) gridItBuyPrice_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        ToolTip tip = new ToolTip();
        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "出貨倉庫" || str == "備註說明" || str == "售價")
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
            else if (str == "銷退數量")
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
            jRSale.Open<JBS.JS.Dept>(sender, reader =>
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

            jRSale.ValidateOpen<JBS.JS.Dept>(sender, e, reader =>
            {
                DeNo.Text = reader["DeNo"].ToString().Trim();
                DeName.Text = reader["DeName1"].ToString().Trim();
            }, true);
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

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "銷退數量")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "select itstockqty from item where itno=N'" + dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() + "'";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        toolStripStatusLabel1.Text = "現有庫存量：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);
                }
            }
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                if (dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "select itunit,itunitp,itpkgqty from item where itno=N'" + dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() + "'";
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

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "銷退數量" || dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
                toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.瀏覽 0.結束";
        }

        private void gridInvMode_Click(object sender, EventArgs e)
        {
            gridInvMode.Text = gridInvMode.Text == "批開" ? "" : "批開";
        }

        int GetGridInvMode()
        {
            return (gridInvMode.Text == "批開") ? 1 : 2;
        }

        //選擇檔頭檔案路徑 13.7C
        private void ShowORModify_PhotoPath_Click(object sender, EventArgs e)
        {
            using (FrmAffixFile frm = new FrmAffixFile())
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dt_訂單關聯報價單 = new DataTable())
                {
                    string sqlcommand = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號 , * from AffixFile where (DaType ='銷退單' and Datano=@Datano) or (Datano=@Datano2 and DaType='銷貨單')";
                    //撈出訂單關聯的報價單
                    string sqlcommand_ = "SELECT distinct(quote.quno) FROM quote INNER JOIN [order] on quote.quno = [order].quno where 1=0  ";
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Datano", SaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Datano2", FromSale.Text.Trim());

                    if (dtRSaleD.Rows.Count > 0)
                    {
                        List<string> ornolist = new List<string>();
                        for (int i = 0; i < dtRSaleD.Rows.Count; i++)
                        {
                            if (ornolist.IndexOf(dtRSaleD.Rows[i]["orno"].ToString()) == -1)
                            {
                                var ornoG = "Datano3" + i.ToString();
                                cmd.Parameters.AddWithValue(ornoG, dtRSaleD.Rows[i]["orno"].ToString());
                                ornolist.Add(dtRSaleD.Rows[i]["orno"].ToString());
                                sqlcommand += " or (DaType ='訂貨單' and Datano=@" + ornoG + ")";
                                sqlcommand_ += " or [order].orno =@" + ornoG;
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

                    frm.CMD = cmd;
                    frm.DtFile = DaFile;
                    frm.Datano = SaNo.Text.Trim();
                    frm.DaType = "銷退單";
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
                iTitle.Text = Common.iTitle;
                User_Einv.Visible = true;
                iTitle.Visible = true;
                labelT1.Visible = true;
            }
            else
            {
                lblEInvChange.Visible = lblEInvState.Visible = EInvChange.Visible = EInvState.Visible = false;
                if (CuNo.ReadOnly) return;
                EInvChange.Text = "";
                EInvState.Text = "";
                pVar.XX05Validate("1", X5No, X5Name);
                User_Einv.Text = Common.User_Einv;
                iTitle.Text = Common.iTitle;
                User_Einv.Visible = false;
                iTitle.Visible = false;
                labelT1.Visible = false;
            }
        }

        private void GridBatch_Click(object sender, EventArgs e)
        {
            BatchF.WhenGridBadch_click(this.Name, dataGridViewT1, dtRSaleD, dt_BatchProcess, null, null, CuNo, CuName11, null, true, btnSave.Enabled == true);
        }

        private void payerno_Validating_1(object sender, CancelEventArgs e)
        {
            if (payerno.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (payerno.Text.Trim() == "")
            {
                e.Cancel = true;
                payerno.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            jRSale.ValidateOpen<JBS.JS.Cust>(sender, e, row => { payerno.Text = row["cuno"].ToString(); payername.Text = row["cuname1"].ToString(); });

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

        private void payerno_DoubleClick(object sender, EventArgs e)
        {
            if (payerno.ReadOnly || btnCancel.Focused)
                return;
            jRSale.Open<JBS.JS.Cust>(sender, reader =>
            {
                payerno.Text = reader["cuno"].ToString();
                payername.Text = reader["cuname1"].ToString();
            });
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

            jRSale.ValidateOpen<JBS.JS.Cust>(sender, e, row =>
            {
                payerno.Text = row["cuno"].ToString();
                payername.Text = row["cuname1"].ToString();
            });
        }
    }
}

