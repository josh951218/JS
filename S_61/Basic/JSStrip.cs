using System;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;
using S_61.FoodCloud;

namespace S_61.JS
{
    public partial class JSStrip : Form
    {
        public JSStrip()
        {
            InitializeComponent();

            if (Common.Sys_BookNo.Contains("JS") && Common.Sys_BookNo.EndsWith("M"))
            {
            }
            else if (Common.Sys_BookNo.Contains("JS"))
            {
                百貨設定toolStripMenuItem.Visible = false;
            }
            else if (Common.Sys_BookNo.Contains("JM"))
            {
                foreach (ToolStripItem sp in menuStrip1.Items)
                {
                    sp.Visible = false;
                }
            }
            if (Common.Sys_UsingBatch == 1)
            {
                this.批號管理ToolStripMenuItem.Visible = false;
            }
            if (Common.Sys_WebOrder == 1)
            {
                this.網路訂單toolStripMenuItem1.Visible = false;
            }
            if (Common.Sys_InvUsed == 2)
            {
                this.發票外掛.Visible = false;
            }
        }
        private void menu1_MouseEnter(object sender, EventArgs e)
        {
            if (Common.Series == "74" || Common.Series == "72")
            {
                itemOrder.Visible = false;
                itemOrderInfo.Visible = false;
                itemFord.Visible = false;
                itemFordInfo.Visible = false;

                toolStripSeparator11.Visible = false;
                toolStripSeparator13.Visible = false;

                itemOrderSaled.Visible = false;
                itemCustNotOut.Visible = false;
                itemItemNotOut.Visible = false;
                itemEmplNotOut.Visible = false;
                itemOrderToFord.Visible = false;

                itemFordBshopd.Visible = false;
                itemFactNotin.Visible = false;
                itemItemNotIn.Visible = false;
                itemEmplNotin.Visible = false;
                網路訂單管理系統ToolStripMenuItem.Visible = false;
            }
        }
        private void menu2_MouseEnter(object sender, EventArgs e)
        {
            if (Common.Series == "73" || Common.Series == "74")
            {
                itemlend.Visible = false;
                itemrlend.Visible = false;
                itemlendinfo.Visible = false;

                itemborr.Visible = false;
                itemrborr.Visible = false;
                itemborrinfo.Visible = false;

                itemFrmAllot.Visible = false;
                調撥資料瀏覽ToolStripMenuItem.Visible = false;
            }
        }


        private void JSStrip_Load(object sender, EventArgs e)
        {
        }
        private void menu7_DropDownOpening(object sender, EventArgs e)
        {
            for (int i = 0; i < menu7.DropDownItems.Count; i++)
            {
                var name = menu7.DropDownItems[i].Name;
                if (MainForm.main.MdiChildren.Any(fm => fm.Name == name) == false)
                {
                    menu7.DropDownItems.RemoveAt(i);
                }
            }
        }
        //第一頁
        private void itemQuote_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnQuote_Click(null, null);
        }
        private void itemQuoteInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnQuoteInfo_Click(null, null);
        }
        private void itemOrder_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnOrder_Click(null, null);
        }
        private void itemOrderInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnOrderInfo_Click(null, null);
        }

        private void itemFquot_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFQuot_Click(null, null);
        }
        private void itemFquotInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFQuotInfo_Click(null, null);
        }
        private void itemFord_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFord_Click(null, null);
        }
        private void itemFordInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFordInfo_Click(null, null);
        }

        private void itemOrderSaled_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmOrderSaled_Click(null, null);
        }
        private void itemCustNotOut_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmCustNotOut_Click(null, null);
        }
        private void itemItemNotOut_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmItemNotOut_Click(null, null);
        }
        private void itemEmplNotOut_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmEmplNotOut_Click(null, null);
        }
        private void itemOrderToFord_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnOrderToFord_Click(null, null);
        }

        private void itemFordBshopd_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmFordBshopd_Click(null, null);
        }
        private void itemFactNotin_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmFactNotIn_Click(null, null);
        }
        private void itemItemNotIn_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmItemNotIn_Click(null, null);
        }
        private void itemEmplNotin_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmEmplNotIn_Click(null, null);
        }


        //第二頁
        private void itemFrmSale_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSale_Click(null, null);
        }
        private void itemFrmRSale_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnRSale_Click(null, null);
        }
        private void itemFrmSaleInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSaleInfo_Click(null, null);
        }
        private void itemFrmShop_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnShop_Click(null, null);
        }
        private void itemFrmRShop_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnRShop_Click(null, null);
        }
        private void itemFrmShopInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnShopInfo_Click(null, null);
        }
        private void itemFrmDraw_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnDraw_Click(null, null);
        }
        private void itemFrmDrawInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnDrawInfo_Click(null, null);
        }
        private void itemFrmAdjust_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnAdjust_Click(null, null);
        }
        private void itemFrmGarner_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnGarner_Click(null, null);
        }
        private void itemFrmGarnerInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnGarnerBrow_Click(null, null);
        }
        private void itemFrmSpecial_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSpecial_Click(null, null);
        }
        private void itemFrmAllot_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnAllot_Click(null, null);
        }
        private void itemFrmLend_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnLend_Click(null, null);
        }
        private void itemFrmOutStk_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnOutStk_Click(null, null);
        }
        private void itemFrmInStkInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnInStkInfo_Click(null, null);
        }
        private void itemFrmItemOut_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemOut_Click(null, null);
        }
        private void itemFrmItemInv_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemInv_Click(null, null);
        }





        //第三頁
        private void itemFrmReceiv_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnReceivd_Click(null, null);
        }
        private void itemFrmCustAcc_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmCust_Accs_Click(null, null);
        }
        private void itemFrmCustRec_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmCust_Receivd_Click(null, null);
        }
        private void itemFrmPayable_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnPayabld_Click(null, null);
        }
        private void itemFrmFactAcc_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmFact_Accs_Click(null, null);
        }
        private void itemFrmFactRec_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmFact_Payabld_Click(null, null);
        }
        private void itemFrmCustDateRec_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmDate_Receivd_Click(null, null);
        }
        private void itemFrmEmplRec_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmEmpl_Receivd_Click(null, null);
        }
        private void itemFrmFordRec_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmFord_Payabld_Click(null, null);
        }
        private void itemFrmDatePay_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmDate_Payabld_Click(null, null);
        }
        private void itemFrmEmplAcc_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnEmpl_Accs_Click(null, null);
        }
        private void itemFrmAgeRec_Click(object sender, EventArgs e)
        {
            MainForm.menu.FrmAge_Receiv_Click(null, null);
        }




        //第四頁
        private void FrmSaleRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSale_Rpt_Click(null, null);
        }
        private void itemSaleRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemSale_Rpt_Click(null, null);
        }
        private void itemFrmEmplRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnEmpSale_rpt_Click(null, null);
        }
        private void itemFrmKindRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnKind_rpt_Click(null, null);
        }
        private void itemFrmCrossRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSaleCross_Click(null, null);
        }

        private void FrmShopRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnBShop_Rpt_Click(null, null);
        }
        private void itemShopRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemShop_Rpt_Click(null, null);
        }
        private void itemstockRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemStock_Rpt_Click(null, null);
        }
        private void itemFrmItemChangeRpt_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemChange_Rpt_Click(null, null);
        }






        //第五頁
        private void itemFrmCust1_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnCust1_Click(null, null);
        }
        private void itemFrmXX01_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnXX01_Click(null, null);
        }
        private void itemCustInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnCustInfo_Click(null, null);
        }
        private void itemFrmPrint_C_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnPrint_C_Click(null, null);
        }
        private void itemFrmFact_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFact_Click(null, null);
        }
        private void itemFrmXX12_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnXX12_Click(null, null);
        }
        private void itemFactInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFactInfo_Click(null, null);
        }
        private void itemFrmPrint_F_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnPrint_F_Click(null, null);
        }
        private void itemFrmEmpl_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnEmpl_Click(null, null);
        }
        private void itemFrmSaleClass_Click(object sender, EventArgs e)
        {
            MainForm.menu.lblFrmSaleClass_Click(null, null);
        }

        //5-2
        private void itemFrmitem_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItem_Click(null, null);
        }
        private void itemFrmKind_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnKind_Click(null, null);
        }
        private void itemFrmInfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemInfo_Click(null, null);
        }
        private void itemFrmItemLevel_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmItemLevel_Click(null, null);
        }
        private void itemFrm_Inventory_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnInventory_Click(null, null);
        }
        private void itemFrmSalegrad_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSalGrad_Click(null, null);
        }
        private void itemFrmBuygrad_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnBuyGrad_Click(null, null);
        }
        private void itemFrmCost_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnItemCost_Click(null, null);
        }
        private void itemFrmBom_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnBom_Click(null, null);
        }
        private void itemBarCode_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnBarCode_Click(null, null);
        }

        //5-3
        private void itemFrmDept_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnDept_Click(null, null);
        }
        private void itemFrmStkRoom_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnStock_Click(null, null);
        }
        private void itemFrmXX06_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnXX06_Click(null, null);
        }
        private void itemFrmXa01_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnXa01_Click(null, null);
        }
        private void itemFrmSpec_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSpec_Click(null, null);
        }
        private void itemFrmXX02_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnXX02_Click(null, null);
        }
        private void itemFrmXX04_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnXX04_Click(null, null);
        }
        private void itemFrmSend_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSend_Click(null, null);
        }
        private void itemFrmTrade_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnTrade_Click(null, null);
        }
        private void itemFrmPhrase_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmPhrase_Click(null, null);
        }
        //第六頁
        private void itemCustBilling_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmCustBilling_Click(null, null);
        }
        private void itemFactBilling_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmFactBilling_Click(null, null);
        }
        private void itemFrmInBilling_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmInBilling_Click(null, null);
        }
        private void itemFrmScrit_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnScrit_Click(null, null);
        }
        private void itemSystemSet_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnSystemSet_Click(null, null);
        }
        private void itemFrmCompare_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnCompare_Click(null, null);
        }


        private void itemRegist_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<FrmRegist>("");
        }
        private void itemBackUp_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<FrmDataBackUP>("資料庫備份");
        }
        private void itemFrmInvoSet_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S6.FrmInvoSet>("");
        }


        private void posStoreSet_Click(object sender, EventArgs e)
        {
            //if (MainForm.main.MdiChildren.Any(f => f.Name != "FrmMenu"))
            //{
            //    MessageBox.Show("請先關閉其它正在作業中的視窗", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    MainForm.main.MdiChildren.ToList().ForEach(f => Basic.SetParameter.RefreshFormLocation(f));
            //    return;
            //}

            //MainForm.FrmMenu.OpenForm("", () =>
            //{
            //    var form = MainForm.main.MdiChildren.FirstOrDefault(fm => fm.GetType().Name == "PosStoreSet");
            //    if (form == null)
            //        return new subMenuFm_6.PosStoreSet();
            //    return form;
            //}); 
        }
        private void posFastItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_6.PosFastItem>("");
        }
        private void posShift_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<JBS.JS.FrmXxBasic<JBS.JS.Shift>>("");
        }
        private void posDownLoad_Click(object sender, EventArgs e)
        {
            //if (MainForm.main.MdiChildren.Any(f => f.Name != "FrmMenu"))
            //{
            //    MessageBox.Show("請先關閉其它正在作業中的視窗", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    MainForm.main.MdiChildren.ToList().ForEach(f => Basic.SetParameter.RefreshFormLocation(f));
            //    return;
            //}
            //MainForm.FrmMenu.OpenForm("", () =>
            //{
            //    var form = MainForm.main.MdiChildren.FirstOrDefault(fm => fm.GetType().Name == "PosDownLoad");
            //    if (form == null)
            //        return new subMenuFm_6.PosDownLoad();
            //    return form;
            //}); 
        }
        private void posMessage_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_6.PosMessage>("");
        }

        //外掛
        private void 批次修改作業_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_6.批次修改作業>("");
        }

        private void itemFrmFixSQLIndex_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<JBS.S6.FrmFixSQLIndex>("");
        }

        private void itemlend_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnLend_Click(null, null);
        }

        private void itemrlend_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnRLend_Click(null, null);
        }

        private void itemlendinfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnLendInfo_Click(null, null);
        }

        private void itemborr_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmBorr_Click(null, null);
        }

        private void itemrborr_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmRBorr_Click(null, null);
        }

        private void itemborrinfo_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnBorrowNew_Info_Click(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.FrmSaleInv>("");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.FrmComp>("");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.nFrmSaleInvInfo>("");
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.FrmBShopInv>("");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.nFrmBShopInvInfo>("");
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.FrmSaleDis>("");
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.FrmBShopDis>("");
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.nFrmSaleDisInfo>("");
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S2.nFrmBShopDisInfo>("");
        }

        private void 機台設定作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmMachineSet>("");
        }

        private void 條碼列印作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmPrintBarCode>("");
        }

        private void 錢櫃開啟記錄表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmMoneyBoxLog>("");
        }

        private void 郵件設定作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmSendMail>("");
        }

        private void 收銀人員業績表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNEmpl_Rpt>("");
        }

        private void 會員銷售排行表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNSale_Rpt>("");
        }

        private void 客戶單價分析表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNSaleAvg>("");
        }

        private void 產品銷售排行表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNItem_Rpt>("");
        }

        private void 類別銷售排行表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNKind_Rpt>("");
        }

        private void 時段銷售排行表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNTime_Rpt>("");
        }

        private void 機台刷卡統計表toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.Frmswipe_statistics>("");
        }

        private void 班別設定作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<JBS.JS.FrmXxBasic<JBS.JS.Shift>>("");
        }

        private void 快速商品設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_6.PosFastItem>("");
        }

        private void 最新消息設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_6.PosMessage>("");
        }

        private void 網路訂單管理系統ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S1.FrmNetOrder>("");
        }

        private void 調撥資料瀏覽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_2.FrmAllot_Info>("");
        }

        private void 調整資料瀏覽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_2.FrmAdjustBrowse_Info>("");
        }

        private void 採購員應付帳款ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_3.FrmEmpl_Acc>("業務別-應付帳款");
        }

        private void 帳齡分析應付帳款ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_3.FrmAge_Payabld>("帳齡分析應付帳款");
        }

        private void 庫存低餘安全存量表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_4.FrmLowSafety_Rpt>("庫存低于安全存量表");
        }

        private void 郵件設定作業ToolStripMenuItem1_Click(object sender, EventArgs e)
        { 
            MainForm.menu.OpenForm<S_61.S0.FrmSendMail>("");
        }

        private void 條碼列印作業ToolStripMenuItem1_Click(object sender, EventArgs e)
        { 
            MainForm.menu.OpenForm<S_61.S0.FrmPrintBarCode>("");
        }

        private void 產品批次修改作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MainForm.menu.OpenForm<S_61.subMenuFm_6.批次修改作業>("");
        }

        private void 寄售廠商對帳表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNFactCMS_Rpt>("");
        }

        private void 機台業績統計表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNMachineSale_Rpt>("");
        }

        private void 二聯收銀發票明細表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmInv401_Rpt>("");
        }

        private void 批號管理作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<BatchMangement>("");
        }

        private void 批號資料瀏覽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<BatchInfo>("");
        }

        private void 電子發票設定作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.SOther.Einvsetup>("");
        }

        private void 批號調整作業toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.subMenuFm_2.批號調整作業>("");
        }

        private void 網路訂單管理作業_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnFrmNetOrder_Click(null, null);
        }

        private void 網路訂單撿貨報表_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnCheckItem_Click(null, null);
        }

        private void 網訂出貨覆核作業_Click(object sender, EventArgs e)
        {
            MainForm.menu.btnBarCodeToorder_Click(null, null);
        }

        private void 批號現有庫存toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.FoodCloud.BatchStockBrowse>("");
        }


         
    }
}
