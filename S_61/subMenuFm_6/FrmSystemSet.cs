using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class FrmSystemSet : Formbase
    {
        SqlTransaction tran;
        List<DataRow> li = new List<DataRow>();
        DataTable Comp = new DataTable();
        DataTable dt = new DataTable();
        DataTable tCode = new DataTable();
        DataRow dr;
        string btnState = string.Empty;
        List<TextBox> HideTxts;
        List<TextBoxbase> list;

        public FrmSystemSet()
        {
            InitializeComponent();
            //pVar.FrmSystemSet = this;
            cn1.ConnectionString = Common.sqlConnString;
            list = this.getEnumMember();

            HideTxts = new List<TextBox> 
            { 
                MnyDeciS, MnyDeciF, MnyDeciSt, 
                MnyDeciFt, TaxDeciS, TaxDeciF, MnyDeciBS, QtyDeci,
                TaxPriceS, TaxPriceF 
            };

            this.長度.FirstNum = 4;
            this.長度.LastNum = 0;
            this.長度.DefaultCellStyle.Format = "f0";

            this.小數.FirstNum = 4;
            this.小數.LastNum = 0;
            this.小數.DefaultCellStyle.Format = "f0";

            CloseDate.SetDateLength();
        }

        private void FrmSystemSet_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = tCode;

            loadDB();
            if (dt.Rows.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
            MnyDeciS.FirstNum = 1;
            MnyDeciF.FirstNum = 1;
            MnyDeciSt.FirstNum = 1;
            MnyDeciFt.FirstNum = 1;
            TaxDeciS.FirstNum = 1;
            TaxDeciF.FirstNum = 1;
            MnyDeciBS.FirstNum = 1;
            QtyDeci.FirstNum = 1;
            TaxPriceS.FirstNum = TaxPriceF.FirstNum = 1;

            MnyDeciS.LastNum = 0;
            MnyDeciF.LastNum = 0;
            MnyDeciSt.LastNum = 0;
            MnyDeciFt.LastNum = 0;
            TaxDeciS.LastNum = 0;
            TaxDeciF.LastNum = 0;
            MnyDeciBS.LastNum = 0;
            QtyDeci.LastNum = 0;
            TaxPriceS.LastNum = TaxPriceF.LastNum = 0;

            CuSaleMny.FirstNum = CuPoint.FirstNum = 11;
            CuSaleMny.LastNum = CuPoint.LastNum = 0;

            btnModify.Focus();
        }

        public void loadDB()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from systemset", cn))
                {
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
                }
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from comp", cn))
                {
                    Comp.Clear();
                    da.Fill(Comp);
                }
                using (cn1 = new SqlConnection(Common.sqlConnString))
                {
                    cn1.Open();
                    tCode.Clear();
                    da1.Fill(tCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                //表頭欄位
                StcName.Text = row["StcName"].ToString();
                StcAddr1.Text = row["StcAddr1"].ToString();
                StcTel.Text = row["StcTel"].ToString();
                StcFax.Text = row["StcFax"].ToString();
                StcPnName.Text = row["StcPnName"].ToString();
                StcCo.Text = row["StcCo"].ToString();
                StcTaxNo.Text = row["StcTaxNo"].ToString();
                Stc080Tel.Text = row["Stc080Tel"].ToString();
                StcTaxNo1.Text = row["StcTaxNo1"].ToString();

                //分頁一
                //庫存年度
                StkYear2.Text = row["StkYear2"].ToString();
                StkYear1.Text = row["StkYear1"].ToString();
                //單價小數
                MnyDeciS.Text = row["MnyDeciS"].ToString();
                MnyDeciF.Text = row["MnyDeciF"].ToString();
                //單據小數
                MnyDeciSt.Text = row["MnyDeciSt"].ToString();
                MnyDeciFt.Text = row["MnyDeciFt"].ToString();
                //稅項小數
                TaxDeciS.Text = row["TaxDeciS"].ToString();
                TaxDeciF.Text = row["TaxDeciF"].ToString();
                //本幣金額
                MnyDeciBS.Text = row["MnyDeciBS"].ToString();
                //庫存小數
                QtyDeci.Text = row["QtyDeci"].ToString();
                //銷項金額小數
                TaxPriceS.Text = row["TaxPriceS"].ToString();
                //進項金額小數
                TaxPriceF.Text = row["TaxPriceF"].ToString();

                PathC.Text = row["PathC"].ToString();
                switch (row["NoAdd"].ToString())
                {
                    case "1": NoAdd1.Checked = true; break;
                    case "2": NoAdd2.Checked = true; break;
                    case "3": NoAdd3.Checked = true; break;
                    case "4": NoAdd4.Checked = true; break;
                    default: NoAdd1.Checked = true; break;
                }
                CuSaleMny.Text = row["CuSaleMny"].ToDecimal().ToString("f0");
                CuPoint.Text = row["CuPoint"].ToDecimal().ToString("f0");
                //
                uip.Text = row["Update_IP"].ToString().Trim();
                ufolder.Text = row["Update_Folder"].ToString().Trim();
                uid.Text = row["Update_Account"].ToString().Trim();
                upw.Text = row["Update_Pwd"].ToString().Trim();
                //
                if (row["LendToSaleMode"].ToDecimal() == 2) rdLeSale2.Checked = true;
                else rdLeSale1.Checked = true;

                //分頁二
                CuUdfC1.Text = row["CuUdfC1"].ToString();
                CuUdfC2.Text = row["CuUdfC2"].ToString();
                CuUdfC3.Text = row["CuUdfC3"].ToString();
                CuUdfC4.Text = row["CuUdfC4"].ToString();
                CuUdfC5.Text = row["CuUdfC5"].ToString();

                FaUdfC1.Text = row["FaUdfC1"].ToString();
                FaUdfC2.Text = row["FaUdfC2"].ToString();
                FaUdfC3.Text = row["FaUdfC3"].ToString();
                FaUdfC4.Text = row["FaUdfC4"].ToString();
                FaUdfC5.Text = row["FaUdfC5"].ToString();

                ItUdfC1.Text = row["ItUdfC1"].ToString();
                ItUdfC2.Text = row["ItUdfC2"].ToString();
                ItUdfC3.Text = row["ItUdfC3"].ToString();
                ItUdfC4.Text = row["ItUdfC4"].ToString();
                ItUdfC5.Text = row["ItUdfC5"].ToString();

                ItNoUdfC.Text = row["ItNoUdfC"].ToString();
                ItIME.Text = row["ItIME"].ToString();

                ItDesp1.Text = row["ItDesp1"].ToString();
                ItDesp2.Text = row["ItDesp2"].ToString();
                ItDesp3.Text = row["ItDesp3"].ToString();
                ItDesp4.Text = row["ItDesp4"].ToString();
                ItDesp5.Text = row["ItDesp5"].ToString();
                ItDesp6.Text = row["ItDesp6"].ToString();
                ItDesp7.Text = row["ItDesp7"].ToString();
                ItDesp8.Text = row["ItDesp8"].ToString();
                ItDesp9.Text = row["ItDesp9"].ToString();
                ItDesp10.Text = row["ItDesp10"].ToString();


                //分頁三
                CloseDate.Text = row["CloseDate"].ToString();
                SaleHead.Text = row["SaleHead"].ToString();
                MemoUdf.Text = row["MemoUdf"].ToString();
                switch (row["KeyPrs"].ToString())
                {
                    case "1": KeyPrs1.Checked = true; break;
                    case "2": KeyPrs2.Checked = true; break;
                    default: KeyPrs1.Checked = true; break;
                }
                switch (row["LowStockMode"].ToString())
                {
                    case "1": LowStockMode1.Checked = true; break;
                    case "2": LowStockMode2.Checked = true; break;
                    default: LowStockMode1.Checked = true; break;
                }
                switch (row["LowStock"].ToString())
                {
                    case "1": LowStock1.Checked = true; break;
                    case "2": LowStock2.Checked = true; break;
                    default: LowStock2.Checked = true; break;
                }
                switch (row["LowStkSlt"].ToString())
                {
                    case "1": LowStkSlt1.Checked = true; break;
                    case "2": LowStkSlt2.Checked = true; break;
                    default: LowStkSlt2.Checked = true; break;
                }
                switch (row["LowCost"].ToString())
                {
                    case "1": LowCost1.Checked = true; break;
                    case "2": LowCost2.Checked = true; break;
                    case "3": LowCost3.Checked = true; break;
                    default: LowCost3.Checked = true; break;
                }
                switch (row["UpCredit"].ToString())
                {
                    case "1": UpCredit1.Checked = true; break;
                    case "2": UpCredit2.Checked = true; break;
                    case "3": UpCredit3.Checked = true; break;
                    default: UpCredit3.Checked = true; break;
                }
                switch (row["SalePrice"].ToString())
                {
                    case "1": SalePrice1.Checked = true; break;
                    case "2": SalePrice2.Checked = true; break;
                    case "3": SalePrice3.Checked = true; break;
                    case "4": SalePrice4.Checked = true; break;
                    case "5": SalePrice5.Checked = true; break;
                    case "6": SalePrice6.Checked = true; break;
                    case "7": SalePrice7.Checked = true; break;
                    default: SalePrice1.Checked = true; break;
                }
                switch (row["FrontSale"].ToString())
                {
                    case "1": FrontSale1.Checked = true; break;
                    case "2": FrontSale2.Checked = true; break;
                    default: FrontSale2.Checked = true; break;
                }
                switch (row["BuyPrice"].ToString())
                {
                    case "1": BuyPrice1.Checked = true; break;
                    case "2": BuyPrice2.Checked = true; break;
                    case "3": BuyPrice3.Checked = true; break;
                    case "4": BuyPrice4.Checked = true; break;
                    case "5": BuyPrice5.Checked = true; break;
                    default: BuyPrice1.Checked = true; break;
                }
                switch (row["AutoBuyp"].ToString())
                {
                    case "1": AutoBuyp1.Checked = true; break;
                    case "2": AutoBuyp2.Checked = true; break;
                    default: AutoBuyp1.Checked = true; break;
                }
                switch (row["OrderFlg"].ToString())
                {
                    case "1": OrderFlg1.Checked = true; break;
                    case "2": OrderFlg2.Checked = true; break;
                    default: OrderFlg1.Checked = true; break;
                }
                switch (row["StNoMode"].ToString())
                {
                    case "1": StNoMode1.Checked = true; break;
                    case "2": StNoMode2.Checked = true; break;
                    default: StNoMode1.Checked = true; break;
                }
                switch (row["StockKind"].ToString())
                {
                    case "1": StockKind1.Checked = true; break;
                    case "2": StockKind2.Checked = true; break;
                    default: StockKind1.Checked = true; break;
                }
                switch (row["DBqty"].ToString())
                {
                    case "1": DBqty_1.Checked = true; break;
                    case "2": DBqty_2.Checked = true; break;
                    default: DBqty_1.Checked = true; break;
                }
                switch (row["defaultAddr"].ToString())
                {
                    case "1": Addr1.Checked = true; break;
                    case "2": Addr2.Checked = true; break;
                    case "3": Addr3.Checked = true; break;
                    default: Addr1.Checked = true; break;
                }
                switch (row["UsingBatch"].ToString())
                {
                    case "1": UsingBatch_1.Checked = true; break;
                    case "2": UsingBatch_2.Checked = true; break;
                    default: UsingBatch_1.Checked = true; break;
                }
                switch (row["weborder"].ToString())
                {
                    case "1": rdWebOrder1.Checked = true; break;
                    case "2": rdWebOrder2.Checked = true; break;
                    default: rdWebOrder1.Checked = true; break;
                }
                ItName.Text = row["ItNameLenth"].ToString();
                ItNoUdf.Text = row["ItNoUdfLenth"].ToString();
                //分頁四

                PosMPW.Text = row["PosMPW"].ToString();
                //分頁五
                CoNo.Text = Comp.Rows[0]["CoNo"].ToString();
                CoName1.Text = Comp.Rows[0]["CoName1"].ToString();
                CoName2.Text = Comp.Rows[0]["CoName2"].ToString();
                PathC.Text = row["PathC"].ToString();
            }
            else
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            //StcName沒有值，就不執行下面的指令
            if (StcName.Text == string.Empty) return;

            btnState = ((Button)sender).Name.Substring(3);
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            HideTxts.ForEach(t => t.ReadOnly = true);
            //有單據則不能修改庫存年度
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Common.sqlConnString;
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                tran = cn.BeginTransaction();
                cmd.Transaction = tran;

                SqlDataReader rd = null;
                bool IsHaveDocs = false;

                cmd.CommandText = "select Count(*) from sale";
                rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    rd.Close(); rd.Dispose();
                    IsHaveDocs = true;
                    goto Finish;
                }
                cmd.CommandText = "select Count(*) from Bshop";
                rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    rd.Close(); rd.Dispose();
                    IsHaveDocs = true;
                    goto Finish;
                }

            Finish:
                tran.Commit(); tran.Dispose();
                cmd.Dispose();
                cn.Close(); cn.Dispose();

                if (IsHaveDocs)
                {
                    StkYear1.ReadOnly = true;
                    StkYear2.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show(ex.ToString());
            }

            StcName.Focus();
            StcName.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (StcName.Text.Trim() == string.Empty)//儲存或修改時，StcName不能為空值
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StcName.Focus();
                return;
            }
            if (btnState == "Modify")
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;


                        cmd.Parameters.AddWithValue("@code", "");
                        cmd.Parameters.AddWithValue("@stcname", StcName.Text.Trim());
                        cmd.Parameters.AddWithValue("@stcco", StcCo.Text.Trim());
                        cmd.Parameters.AddWithValue("@stcaddr1", StcAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@stctaxno", StcTaxNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@stctel", StcTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@stcfax", StcFax.Text.Trim());
                        cmd.Parameters.AddWithValue("@stc080tel", Stc080Tel.Text.Trim());
                        cmd.Parameters.AddWithValue("@stcpnname", StcPnName.Text.Trim());
                        cmd.Parameters.AddWithValue("@stctaxno1", StcTaxNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@stkyear1", StkYear1.Text.Trim());
                        cmd.Parameters.AddWithValue("@stkyear2", StkYear2.Text.Trim());
                        cmd.Parameters.AddWithValue("@mnydecis", MnyDeciS.Text.Trim());
                        cmd.Parameters.AddWithValue("@MnyDeciSt", MnyDeciSt.Text.Trim());
                        cmd.Parameters.AddWithValue("@MnyDeciF", MnyDeciF.Text.Trim());
                        cmd.Parameters.AddWithValue("@MnyDeciFt", MnyDeciFt.Text.Trim());
                        cmd.Parameters.AddWithValue("@MnyDeciBS", MnyDeciBS.Text.Trim());
                        cmd.Parameters.AddWithValue("@TaxDeciS", TaxDeciS.Text.Trim());
                        cmd.Parameters.AddWithValue("@TaxDeciF", TaxDeciF.Text.Trim());
                        cmd.Parameters.AddWithValue("@TaxPriceS", TaxPriceS.Text.Trim());
                        cmd.Parameters.AddWithValue("@TaxPriceF", TaxPriceF.Text.Trim());
                        cmd.Parameters.AddWithValue("@QtyDeci", QtyDeci.Text.Trim());
                        cmd.Parameters.AddWithValue("@noadd", getRadioNumber(pCodeNo));
                        cmd.Parameters.AddWithValue("@CuUdfC1", CuUdfC1.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuUdfC2", CuUdfC2.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuUdfC3", CuUdfC3.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuUdfC4", CuUdfC4.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuUdfC5", CuUdfC5.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdfC1", FaUdfC1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdfC2", FaUdfC2.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdfC3", FaUdfC3.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdfC4", FaUdfC4.Text.Trim());
                        cmd.Parameters.AddWithValue("@FaUdfC5", FaUdfC5.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItUdfC1", ItUdfC1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItUdfC2", ItUdfC2.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItUdfC3", ItUdfC3.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItUdfC4", ItUdfC4.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItUdfC5", ItUdfC5.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp1", ItDesp1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp2", ItDesp2.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp3", ItDesp3.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp4", ItDesp4.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp5", ItDesp5.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp6", ItDesp6.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp7", ItDesp7.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp8", ItDesp8.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp9", ItDesp9.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItDesp10", ItDesp10.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItNoUdfC", ItNoUdfC.Text.Trim());
                        cmd.Parameters.AddWithValue("@SaleHead", SaleHead.Text.Trim());
                        cmd.Parameters.AddWithValue("@MemoUdf", MemoUdf.Text.Trim());
                        cmd.Parameters.AddWithValue("@keyprs", getRadioNumber(pPrs));
                        cmd.Parameters.AddWithValue("@lowstock", getRadioNumber(pLowStock));
                        cmd.Parameters.AddWithValue("@lowstkslt", getRadioNumber(pLowStockSlt));
                        cmd.Parameters.AddWithValue("@lowcost", getRadioNumber(pLowCost));
                        cmd.Parameters.AddWithValue("@upcredit", getRadioNumber(pCredit));
                        cmd.Parameters.AddWithValue("@saleprice", getRadioNumber(pSale));
                        cmd.Parameters.AddWithValue("@frontsale", getRadioNumber(pPrint));
                        cmd.Parameters.AddWithValue("@buyprice", getRadioNumber(pBShop));
                        cmd.Parameters.AddWithValue("@PathC", PathC.Text.Trim());
                        cmd.Parameters.AddWithValue("@AutoBuyp", getRadioNumber(pItemPrice));
                        cmd.Parameters.AddWithValue("@OrderFlg", getRadioNumber(pOrder));
                        cmd.Parameters.AddWithValue("@StNoMode", getRadioNumber(pStockMode));
                        cmd.Parameters.AddWithValue("@StockKind", getRadioNumber(pSystem));
                        cmd.Parameters.AddWithValue("@DBqty", getRadioNumber(pMula));
                        cmd.Parameters.AddWithValue("@CuSaleMny", CuSaleMny.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("@CuPoint", CuPoint.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("@PosMPW", PosMPW.Text.Trim());
                        cmd.Parameters.AddWithValue("@usrno", dt.Rows[0]["usrno"]);
                        cmd.Parameters.AddWithValue("@cono", CoNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cono1", Comp.Rows[0]["cono"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@coname1", CoName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@coname2", CoName2.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItIME", ItIME.Text.Trim());
                        cmd.Parameters.AddWithValue("@CloseDate", Date.ToTWDate(CloseDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("@CloseDate1", Date.ToUSDate(CloseDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Update_IP", uip.Text.Trim());
                        cmd.Parameters.AddWithValue("@Update_Folder", ufolder.Text.Trim());
                        cmd.Parameters.AddWithValue("@Update_Account", uid.Text.Trim());
                        cmd.Parameters.AddWithValue("@Update_Pwd", upw.Text.Trim());
                        cmd.Parameters.AddWithValue("@LendToSaleMode", getRadioNumber(pLend2Sale));
                        cmd.Parameters.AddWithValue("@LowStockMode", getRadioNumber(pLowStockMode));
                        cmd.Parameters.AddWithValue("@defaultAddr", getRadioNumber(pDefaultAddr));
                        cmd.Parameters.AddWithValue("@UsingBatch", getRadioNumber(pUsingBatch));
                        cmd.Parameters.AddWithValue("@weborder", getRadioNumber(pWebOrder));
                        cmd.Parameters.AddWithValue("@ItNameLenth", ItName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItNoUdfLenth", ItNoUdf.Text.Trim());
                        cmd.CommandText = "UPDATE systemset set "
                                + "[stcname] =@stcname"
                                + ",[stcco] = @stcco"
                                + ",[stcaddr1] =@stcaddr1"
                                + ",[stctaxno] =@stctaxno"
                                + ",[stctel] =@stctel"
                                + ",[stcfax] =@stcfax"
                                + ",[stc080tel] =@stc080tel"
                                + ",[stcpnname] =@stcpnname"
                                + ",[stctaxno1] =@stctaxno1"
                                + ",[stkyear1] =@stkyear1"
                                + ",[stkyear2] =@stkyear2"
                                + ",[mnydecis] =@mnydecis"
                                + ",[MnyDeciSt] =@MnyDeciSt"
                                + ",[MnyDeciF] =@MnyDeciF"
                                + ",[MnyDeciFt] =@MnyDeciFt"
                                + ",[MnyDeciBS] =@MnyDeciBS"
                                + ",[TaxDeciS] =@TaxDeciS"
                                + ",[TaxDeciF] =@TaxDeciF"
                                + ",[TaxPriceS] =@TaxPriceS"
                                + ",[TaxPriceF] =@TaxPriceF"
                                + ",[QtyDeci] =@QtyDeci"
                                + ",[noadd] = @noadd"
                                + ",[CuUdfC1] = @CuUdfC1"
                                + ",[CuUdfC2] = @CuUdfC2"
                                + ",[CuUdfC3] = @CuUdfC3"
                                + ",[CuUdfC4] = @CuUdfC4"
                                + ",[CuUdfC5] = @CuUdfC5"
                                + ",[FaUdfC1] = @FaUdfC1"
                                + ",[FaUdfC2] = @FaUdfC2"
                                + ",[FaUdfC3] = @FaUdfC3"
                                + ",[FaUdfC4] = @FaUdfC4"
                                + ",[FaUdfC5] = @FaUdfC5"
                                + ",[ItUdfC1] = @ItUdfC1"
                                + ",[ItUdfC2] = @ItUdfC2"
                                + ",[ItUdfC3] = @ItUdfC3"
                                + ",[ItUdfC4] = @ItUdfC4"
                                + ",[ItUdfC5] = @ItUdfC5"

                                + ",[ItDesp1] = @ItDesp1"
                                + ",[ItDesp2] = @ItDesp2"
                                + ",[ItDesp3] = @ItDesp3"
                                + ",[ItDesp4] = @ItDesp4"
                                + ",[ItDesp5] = @ItDesp5"
                                + ",[ItDesp6] = @ItDesp6"
                                + ",[ItDesp7] = @ItDesp7"
                                + ",[ItDesp8] = @ItDesp8"
                                + ",[ItDesp9] = @ItDesp9"
                                + ",[ItDesp10] = @ItDesp10"
                                + ",[CloseDate] = @CloseDate"
                                + ",[CloseDate1] = @CloseDate1"
                                + ",[ItNoUdfC] = @ItNoUdfC"
                                + ",[SaleHead] = @SaleHead"
                                + ",[MemoUdf] = @MemoUdf"
                                + ",[keyprs] = @keyprs"

                                + ",[LowStockMode] = @LowStockMode"
                                + ",[lowstock] = @lowstock"
                                + ",[lowstkslt] = @lowstkslt"
                                + ",[lowcost] = @lowcost"
                                + ",[upcredit] = @upcredit"
                                + ",[saleprice] = @saleprice"
                                + ",[frontsale] = @frontsale"
                                + ",[buyprice] = @buyprice"
                                + ",[PathC] = @PathC"
                                + ",[AutoBuyp] = @AutoBuyp"
                                + ",[OrderFlg] = @OrderFlg"
                                + ",[StNoMode] = @StNoMode"
                                + ",[StockKind] = @StockKind"
                                + ",[DBqty] = @DBqty"
                                + ",[CodeStr] = @code"
                                + ",[CuSaleMny] = @CuSaleMny"
                                + ",[CuPoint] = @CuPoint"
                                + ",[PosMPW] = @PosMPW"
                                + ",[ItIME]=@ItIME"
                                + ",[Update_IP]=@Update_IP"
                                + ",[Update_Folder]=@Update_Folder"
                                + ",[Update_Account]=@Update_Account"
                                + ",[Update_Pwd]=@Update_Pwd"
                                + ",[LendToSaleMode]=@LendToSaleMode"
                                + ",[defaultAddr]=@defaultAddr"
                                + ",[UsingBatch] = @UsingBatch"
                                + ",ItNameLenth  =@ItNameLenth"
                                + ",ItNoUdfLenth =@ItNoUdfLenth"
                                + ",weborder=@weborder"
                                + "  where usrno =@usrno";
                                
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update comp set"
                                        + " [cono] =@cono"
                                        + ",[coname1] =@coname1"
                                        + ",[coname2] =@coname2"
                                        + " where cono =@cono1";
                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();

                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (!BMIDP.ReadOnly)
                        {
                            BMIDP.Visible = false;
                            pHidden.Visible = false;
                            HideTxts.ForEach(t => t.ReadOnly = true);
                        }
                        MainForm.main.loadSystemSetting();
                        MainForm.main.loadComp();
                    }
                }

                try
                {
                    using (cn1 = new SqlConnection(Common.sqlConnString))
                    {
                        cn1.Open();
                        using (SqlCommand cmd = cn1.CreateCommand())
                        {
                            cmd.CommandText = "delete from systemcode where userno='T01'";
                            cmd.ExecuteNonQuery();
                        }
                        foreach (DataRow row in tCode.Rows)
                        {
                            row["columnName"] = row["columnName"].ToString().Trim();
                            row.AcceptChanges();
                            if (row["columnName"].ToString().Trim().Length == 0) continue;
                            else
                            {
                                row.SetAdded();
                            }
                        }
                        da1.Update(tCode);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            btnCancel_Click(null, null);
            btnExit.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!BMIDP.ReadOnly)
            {
                pHidden.Visible = false;
                BMIDP.Visible = false;
                HideTxts.ForEach(t => t.ReadOnly = true);
            }

            btnState = string.Empty;
            Common.SetTextState(FormState = FormEditState.None, ref list);

            loadDB();
            dr = li.First();
            writeToTxt(dr);
            btnModify.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            //OpenFileDialog fd = new OpenFileDialog();
            //fd.ShowDialog();
            //PathC.Text = fd.FileName;
        }

        private string getRadioNumber(GroupBoxT pnl)
        {
            return pnl.Controls.OfType<RadioT>().FirstOrDefault(r => r.Checked).Name.Last().ToString();
        }

        private string getRadioNumber(PanelNT pnl)
        {
            return pnl.Controls.OfType<RadioT>().FirstOrDefault(r => r.Checked).Name.Last().ToString();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.PerformClick();
                    break;

                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    btnExit.PerformClick();
                    break;
                case Keys.F9:
                    btnSave.Focus();
                    btnSave.PerformClick();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        //隱藏區
        private void lblMnyDeciS_Click(object sender, EventArgs e)
        {
            if (btnState == "Modify")
            {
                BMIDP.Text = "";
                BMIDP.Visible = true;
                BMIDP.ReadOnly = false;
                BMIDP.Focus();
            }
        }

        private void btnX3Change_Click(object sender, EventArgs e)
        {
            //還沒開發
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            DataRow row = tCode.NewRow();
            row["userNo"] = "T01";
            row["cLen"] = 1;
            row["cDec"] = 0;
            row["frontStr"] = "";
            row["backStr"] = "";
            tCode.Rows.Add(row);
            tCode.AcceptChanges();
            dataGridViewT1.DataSource = tCode;
        }

        private void btnBrowT3_Click(object sender, EventArgs e)
        {
            DataRow row = tCode.NewRow();
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var index = p.Index;
            tCode.Rows.InsertAt(row, index);
            tCode.AcceptChanges();
            dataGridViewT1.DataSource = tCode;
        }

        private void btnBrowT4_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            dataGridViewT1.Rows.Remove(p);
            tCode.AcceptChanges();
            dataGridViewT1.DataSource = tCode;
        }


        private void StkYear2_Validating(object sender, CancelEventArgs e)
        {
            if (StkYear2.ReadOnly) return;
            var y = StkYear2.Text.ToDecimal() - 1911;
            StkYear1.Text = y.ToString("f0");
        }

        private void StkYear1_Validating(object sender, CancelEventArgs e)
        {
            if (StkYear1.ReadOnly) return;
            var y = StkYear1.Text.ToDecimal() + 1911;
            StkYear2.Text = y.ToString("f0");
        }

        public object GetValue(string table, string only_a_Result_youWant, string pkColumn, string pkValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@pkno", pkValue == null ? "" : pkValue);
                        cmd.CommandText = " select " + only_a_Result_youWant + " from " + table.Trim() + " where " + pkColumn + " = @pkno ";
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void BMIDP_TextChanged(object sender, EventArgs e)
        {
            if (btnModify.Enabled) return;
            if (BMIDP.Text.Trim().ToUpper() == "BMIDP")
            {
                bool HasDocts = false;
                var c = GetValue("Sale", "Count(*)", "0", "0").ToDecimal();
                HasDocts = HasDocts || c > 0;
                c = GetValue("BShop", "Count(*)", "0", "0").ToDecimal();
                HasDocts = HasDocts || c > 0;

                StkYear1.ReadOnly = HasDocts;
                StkYear2.ReadOnly = HasDocts;

                HideTxts.ForEach(t => t.ReadOnly = false);

                pHidden.Visible = true;
                MnyDeciS.Focus();
            }
        }

        private void btnModify_EnabledChanged(object sender, EventArgs e)
        {
            pCodeNo.Enabled = pMula.Enabled = pSystem.Enabled = !btnModify.Enabled;
            pPrs.Enabled = pLowStock.Enabled = pLowStockSlt.Enabled = pLowCost.Enabled = pCredit.Enabled = pSale.Enabled = pBShop.Enabled = pPrint.Enabled = pItemPrice.Enabled = pOrder.Enabled = pStockMode.Enabled = !btnModify.Enabled;
            pLowStockMode.Enabled = btnX3Change.Enabled = btnBrowT1.Enabled = btnBrowT3.Enabled = btnBrowT4.Enabled = !btnModify.Enabled;
            pDefaultAddr.Enabled = !btnModify.Enabled;
            dataGridViewT1.ReadOnly = btnModify.Enabled;

            if (Common.Series == "74") pStockMode.Enabled = false;
            if (Common.Series == "73") pStockMode.Enabled = false;
            if (Common.Series == "74") pLowStockMode.Enabled = false;
            if (Common.Series == "73") pLowStockMode.Enabled = false;
        }

        private void MnyDeciBS_Enter(object sender, EventArgs e)
        {
            ((TextBoxNumberT)sender).SelectAll();
        }

        private void CloseDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (CloseDate.TrimTextLenth() == 0)
            {
                CloseDate.Clear();
                return;
            }
            if (!CloseDate.IsDateTime())
            {
                CloseDate.Focus();
                MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void lblStcTaxNo1_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmSystemSetmore())
            {
                frm.ShowDialog();
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            using (var frm = new SubFrmItemColumnSelect())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == -1)
                        return;

                    //dataGridViewT1.SelectedRows[0].Cells["列印欄位"].Value = frm.TResult.Trim();
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = frm.TResult.Trim();
                    tCode.Rows[index]["columnName"] = frm.TResult.Trim();

                    dataGridViewT1.InvalidateRow(index);
                }
            }
        }

        private void UsingBatch_CheckedChanged(object sender, MouseEventArgs e)
        {
            string str = ((RadioButton)sender).Text;
            if (str == "不啟用")
                MessageBox.Show("批號管理，必須重新啟用軟體，才會[關閉]!!");
            else
                MessageBox.Show("批號管理，必須重新啟用軟體，才會[啟用]!!");
        }

        private void ItNameORItNoUdf_Lenth_Validating(object sender, CancelEventArgs e)
        {
            string Column_Name = ((TextBoxNumberT)sender).Name;
            int Now_Value = ((TextBoxNumberT)sender).Text.Trim().ToInteger();
            #region 限制條件:資料庫結構長度只能變大不能變小 如:等於 或 小於則 return
            string sqr_sql = 
@"    select  character_maximum_length as 長度     
	  from information_schema.columns  
	  where table_name = 'item' and column_name ='" + Column_Name + "'";
            int oldValue = SQL.ExecuteScalar(sqr_sql).ToInteger();

            if (oldValue == Now_Value)
                return;
            if (oldValue > Now_Value) 
            {
                ((TextBoxNumberT)sender).Text = oldValue.ToString();
                MessageBox.Show("結構長度不得變小! 目前長度為:[" + oldValue+"]");
                e.Cancel = true;
                return;
            }
            #endregion
            #region SQL 改變結構長度
            DialogResult dialogResult = MessageBox.Show("是否修改產品基本檔(" + ((TextBoxNumberT)sender).Tag + ")資料庫欄位長度? 請選擇 [是] 或 [否]", "請選擇 [是] 或 [否]", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                using (SqlConnection cn = new SqlConnection(S_61.Basic.Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    using (SqlTransaction tran = cn.BeginTransaction("Transaction"))
                    {
                        cmd.Connection = cn;
                        cmd.Transaction = tran;
                        try
                        {
                            
                            
                            if (Column_Name == "ItNoUdf")//如果是品名規格才會編動的部分
                            {
                                ChangeColumnType(cmd, "item", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                SQL.ExecuteNonQuery("UPDATE SYSTEMSET SET ItNoUdfLenth = '" + Now_Value + "'");
                                Common.Sys_ItNoUdfLenth = Now_Value;
                            }
                            else if (Column_Name == "ItName")
                            {
                                ChangeColumnType(cmd, "item", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "saled", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "salebom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "orderd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "OrderBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Rsaled", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Rsalebom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Quoted", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "QuoteBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "lendd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "lendbom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "speciald", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Instkd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "InstkBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "fquotd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "fquotBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Fordd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "FordBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Bshopd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "BshopBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "rshopd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "RShopBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Rlendd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Rlendbom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Oustkd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "OuStkBOM", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "drawd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "DrawBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Garnerd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "GarnBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Allotd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "AlloBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Adjustd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "AdjuBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Borrd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "BorrBom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Rborrd", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "Rborrbom", Column_Name, "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "bomD", Column_Name , "NVARCHAR(" + Now_Value + ")");
                                ChangeColumnType(cmd, "bom", "boitname", "NVARCHAR(" + Now_Value + ")");
                                SQL.ExecuteNonQuery("UPDATE SYSTEMSET SET ItNameLenth = '" + Now_Value + "'");
                                Common.Sys_ItNameLenth = Now_Value;
                            }
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            e.Cancel = true;
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
 
            }
            else if (dialogResult == DialogResult.No)
            {
                ((TextBoxNumberT)sender).Text = oldValue.ToString();
            }
            #endregion
        }


        void ChangeColumnType(SqlCommand cmd, string TTable, string TColumn, string TType)
        {
            cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'" + TTable.Replace('[', ' ').Replace(']', ' ').Trim() + "') and name=N'" + TColumn + "'";
            if (cmd.ExecuteScalar().ToString() == "1")
            {
                cmd.CommandText = "ALTER TABLE " + TTable + " ALTER COLUMN " + TColumn + " " + TType;
                if (TType.Contains("NVARCHAR"))
                    cmd.CommandText += " COLLATE Chinese_Taiwan_Stroke_BIN";
                cmd.ExecuteNonQuery();
            }
        }






    }
}
