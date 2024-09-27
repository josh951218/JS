using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace JBS.S6
{
    public partial class FrmItnoChange : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmItnoChange()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmItnoChange_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BItNo.Text.Trim() == "" || NItNo.Text.Trim() == "")
            {
                MessageBox.Show("產品編號不可為空", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("請確定是否更新編號", "訊息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;
            BItNo.Enabled = NItNo.Enabled = false;
            try
            {
                using (TransactionScope tn = new TransactionScope())
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.AddWithValue("@BItNo", BItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@NItNo", NItNo.Text.Trim());
                        cmd.CommandText = "";

                        cmd.CommandText += " Update RShopBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update AdjuBom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update AdjuBomTemp          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update adjustd              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update AlloBom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update AlloBomTemp          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update allotd               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update Bdiscountd           set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update bestock              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update bom                set boitno=@NItNo where boitno=@BItNo;";
                        cmd.CommandText += " Update bomd               set boitno=@NItNo where boitno=@BItNo;";
                        cmd.CommandText += " Update bomd                 set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update Bomdd                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update borrbom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update borrd                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update BShopBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update BShopBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update bshopd               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update discountd            set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update DrawBom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update DrawBomTemp          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update drawd                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update FOrdBom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update FordBomTemp          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update fordd                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update FQuotBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update FQuotBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update fquotd               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update GarnBom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update GarnBomTemp          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update garnerd              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update InOutBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update InOutBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update InOutD               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update InStkBOM             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update InStkD               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update item                 set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update Itemcost             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update itemcost_temp        set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update ItemSNo              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update IvBom                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update IvD                  set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update lendbom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update lendd                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update LSItem               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update OrderBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update OrderBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update orderd               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update OuStkBOM             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update OuStkD               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update OutInBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update OutInBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update OutInD               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update PosFastItem          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update PosPLU               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update Proded               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update QuoteBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update QuoteBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update quoted               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update RBorrBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update RBorrD               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update rlendbom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update rlendd               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update RSaleBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update RSaleBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update rsaled               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update RShopBom             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update RShopBomTemp         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update rshopd               set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update SaleBom              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update salebomTemp          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update saled                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update saledTemp            set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update SaveStockBom         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update SaveStockD           set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update SNo                  set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update Speciald             set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update stkcost              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update StkDetail            set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update stock                set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update stock_D              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update StocktakingBom       set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update StocktakingBomTemp   set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update Stocktakingd         set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update TempOrderD           set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update TempOrderDetail      set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update TempQuoteD           set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update TempRsaleD           set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update TempSaleD            set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update Tempstock            set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update turshop              set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update WebOrderbom          set itno=@NItNo where itno=@BItNo;";
                        cmd.CommandText += " Update weborderd            set itno=@NItNo where itno=@BItNo;";


                        ////報價
                        //cmd.CommandText += "Update quoted set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update quotebom set itno=@NItNo where itno=@BItNo;";
                        ////訂單
                        //cmd.CommandText += "Update orderd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update orderbom set itno=@NItNo where itno=@BItNo;";
                        ////詢價
                        //cmd.CommandText += "Update fquotd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update fquotbom set itno=@NItNo where itno=@BItNo;";
                        ////採購
                        //cmd.CommandText += "Update fordd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update fordbom set itno=@NItNo where itno=@BItNo;";
                        ////銷貨
                        //cmd.CommandText += "Update saled set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update saled set tempitno=@NItNo where tempitno=@BItNo;";
                        //cmd.CommandText += "Update salebom set itno=@NItNo where itno=@BItNo;";
                        ////銷退
                        //cmd.CommandText += "Update rsaled set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update rsalebom set itno=@NItNo where itno=@BItNo;";
                        ////進貨
                        //cmd.CommandText += "Update bshopd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update bshopbom set itno=@NItNo where itno=@BItNo;";
                        ////進退
                        //cmd.CommandText += "Update rshopd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update rshopbom set itno=@NItNo where itno=@BItNo;";
                        ////領料
                        //cmd.CommandText += "Update drawd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update drawbom set itno=@NItNo where itno=@BItNo;";
                        ////入庫
                        //cmd.CommandText += "Update garnerd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update garnbom set itno=@NItNo where itno=@BItNo;";
                        ////調撥
                        //cmd.CommandText += "Update allotd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update allobom set itno=@NItNo where itno=@BItNo;";
                        ////調整
                        //cmd.CommandText += "Update adjustd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update adjubom set itno=@NItNo where itno=@BItNo;";
                        ////寄庫
                        //cmd.CommandText += "Update instkd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update instkbom set itno=@NItNo where itno=@BItNo;";
                        ////寄庫領出
                        //cmd.CommandText += "Update oustkd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update oustkbom set itno=@NItNo where itno=@BItNo;";
                        ////特價區間
                        //cmd.CommandText += "Update speciald set itno=@NItNo where itno=@BItNo;";
                        ////盤點
                        //cmd.CommandText += "Update ivd set itno=@NItNo where itno=@BItNo;";
                        //cmd.CommandText += "Update ivbom set itno=@NItNo where itno=@BItNo;";
                        ////產品建檔
                        //cmd.CommandText += "Update item set itno=@NItNo where itno=@BItNo;";
                        ////組合品建檔
                        //cmd.CommandText += "Update bom set boitno=@NItNo where boitno=@BItNo;";
                        //cmd.CommandText += "Update bomd set boitno=@NItNo where boitno=@BItNo;";
                        ////bestock 期初開帳
                        //cmd.CommandText += "Update bestock set itno=@NItNo where itno=@BItNo;";
                        ////stock 庫存明細
                        //cmd.CommandText += "Update stock set itno=@NItNo where itno=@BItNo;";
                        ////tempstock pos用
                        //cmd.CommandText += "Update tempstock set itno=@NItNo where itno=@BItNo;";
                        ////itemcost 成本計算 上表
                        //cmd.CommandText += "Update itemcost set itno=@NItNo where itno=@BItNo;";
                        ////stkcost 成本計算 下表
                        //cmd.CommandText += "Update stkcost set itno=@NItNo where itno=@BItNo;";
                        ////posplu pos快速商品PLU
                        //cmd.CommandText += "Update posplu set itno=@NItNo where itno=@BItNo;";
                        ////posfastitem pos快速商品
                        //cmd.CommandText += "Update posfastitem set itno=@NItNo where itno=@BItNo;";

                        cmd.ExecuteNonQuery();
                        tn.Complete();
                        MessageBox.Show("編號修改完成", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BItNo.Text = ItName.Text = ItUnit.Text = ItUnitP.Text = NItNo.Text = "";
                        BItNo.Focus();
                        cmd.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            BItNo.Enabled = NItNo.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void BItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender, row =>
            {
                BItNo.Text = row["ItNo"].ToString();
                ItName.Text = row["ItName"].ToString();
                ItUnit.Text = row["ItUnit"].ToString();
                ItUnitP.Text = row["ItUnitP"].ToString();
            });
        }

        private void BItNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (BItNo.Text.Trim() == "")
            {
                BItNo.Text = "";
                ItName.Text = "";
                ItUnit.Text = "";
                ItUnitP.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                BItNo.Text = row["ItNo"].ToString();
                ItName.Text = row["ItName"].ToString();
                ItUnit.Text = row["ItUnit"].ToString();
                ItUnitP.Text = row["ItUnitP"].ToString();
            });
        }

        private void NItNo_Validating(object sender, CancelEventArgs e)
        {
            if (NItNo.Text.Trim() == "" || btnCancel.Focused)
                return;

            if (pVar.ItemValidate(NItNo.Text))
            {
                e.Cancel = true;
                MessageBox.Show("此產品編號已經存在", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("@itnoudf", NItNo.Text.Trim());
                cmd.CommandText = "select itnoudf from item where itnoudf=@itnoudf";
                if (cmd.ExecuteScalar().IsNotNull())
                {
                    e.Cancel = true;
                    MessageBox.Show("此編號已有自定編號存在", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
        }

    }
}
