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
using JE.S5;

namespace S_61.SOther
{
    public partial class FrmItem : Formbase
    {
        JBS.JS.Item jItem;
        JBS.JS.xEvents xe;
        JBS.JS.Cust jCust;
        DataTable dtStock = new DataTable();
        DataTable dtcustandard = new DataTable();
        DataTable dtfastandard = new DataTable();

        string currentUdf = "";
        string BeforeText = "";
        public string newItno = "";
        string bar1 = "";
        string bar2 = "";

        List<Control> Writes;
        List<TextBoxbase> list;

        public FrmItem()
        {
            InitializeComponent();
            this.jItem = new JBS.JS.Item();
            this.list = this.getEnumMember();
            this.jCust = new JBS.JS.Cust();
            this.xe = new JBS.JS.xEvents();
            Writes = new List<Control>()
            {
                ItNo, ItName, ItIme, ItNoUdf, KiNo,Punit,
                ItUnit, ItNW, ItBuyPri, ItPrice, ItCost, ItSafeQty,
                ItUnitP, ItPkgQty, ItBuyPriP, ItPriceP, ItCostP, ItLastQty, ItFirCost,
                ItDesp1, ItDesp2, ItDesp3, ItDesp4, ItDesp5, ItDesp6,
                ItDesp7, ItDesp8, ItDesp9, ItDesp10, ItNote,
                ItUdf1, ItUdf2, ItUdf3, ItUdf4, ItUdf5, ItPicture,
                sItPrice, sItPrice1, sItPrice2, sItPrice3, sItPrice4, sItPrice5,
                sItPriceP, sItPriceP1, sItPriceP2, sItPriceP3, sItPriceP4, sItPriceP5 , FaNo,
                ItDate, ItBuyDate, ItSalDate,
                ItFirTQty, ItFirTCost, ItStockQty, ScNo, ItDisc
            };

            //數量
            ItFirTQty.Set庫存數量小數();
            ItStockQty.Set庫存數量小數();
            ItSafeQty.Set庫存數量小數();
            ItPkgQty.Set庫存數量小數();
            ItLastQty.Set庫存數量小數();
            ItNW.Set庫存數量小數();
            ItDisc.FirstNum = 3;
            ItDisc.LastNum = 0;


            //金額
            ItPrice.Set銷貨單價小數();
            ItFirTCost.Set銷貨單價小數();
            ItFirCost.Set銷貨單價小數();
            ItBuyPri.Set進貨單價小數();
            ItCost.Set銷貨單價小數();
            ItBuyPriP.Set銷貨單價小數();
            ItPriceP.Set銷貨單價小數();
            ItCostP.Set銷貨單價小數();

            sItPrice.Set銷貨單價小數();
            sItPrice1.Set銷貨單價小數();
            sItPrice2.Set銷貨單價小數();
            sItPrice3.Set銷貨單價小數();
            sItPrice4.Set銷貨單價小數();
            sItPrice5.Set銷貨單價小數();

            sItPriceP.Set銷貨單價小數();
            sItPriceP1.Set銷貨單價小數();
            sItPriceP2.Set銷貨單價小數();
            sItPriceP3.Set銷貨單價小數();
            sItPriceP4.Set銷貨單價小數();
            sItPriceP5.Set銷貨單價小數();

            this.庫存量.Set庫存數量小數();
            this.期初數量.Set庫存數量小數();

            if (Common.Sys_DBqty == 1)
            {
                lblPunit.Visible = Punit.Visible = false;
            }
            ItCost.Visible = ItCostP.Visible = Common.User_ShopPrice;
            ItBuyPri.Visible = ItBuyPriP.Visible = Common.User_ShopPrice;

            ItPrice.Visible = ItPriceP.Visible = Common.User_SalePrice;
            sItPrice.Visible = Common.User_SalePrice;
            sItPrice1.Visible = Common.User_SalePrice;
            sItPrice2.Visible = Common.User_SalePrice;
            sItPrice3.Visible = Common.User_SalePrice;
            sItPrice4.Visible = Common.User_SalePrice;
            sItPrice5.Visible = Common.User_SalePrice;

            sItPriceP.Visible = Common.User_SalePrice;
            sItPriceP1.Visible = Common.User_SalePrice;
            sItPriceP2.Visible = Common.User_SalePrice;
            sItPriceP3.Visible = Common.User_SalePrice;
            sItPriceP4.Visible = Common.User_SalePrice;
            sItPriceP5.Visible = Common.User_SalePrice;

            ItBuyDate.Visible = Common.User_ShopPrice;
            ItSalDate.Visible = Common.User_SalePrice;

            ItFirTCost.Visible = Common.User_ShopPrice;
            ItFirCost.Visible = Common.User_ShopPrice;


            


            var li2 = tabPage2.Controls.OfType<LabelT>().ToList();
            for (int i = 0; i < li2.Count; i++)
            {
                if (li2[i].Name.Contains("Desp") == false) continue;
                var name = li2[i].Name.skipString(3);
                var text = Common.dtSysSettings.Rows[0][name].ToString();
                if (text.Trim().Length == 0) continue;
                li2[i].Text = text;
            }

            var li3 = new List<LabelT> { lblItUdf1, lblItUdf2, lblItUdf3, lblItUdf4, lblItUdf5 };
            for (int i = 0; i < li3.Count; i++)
            {
                var text = Common.dtSysSettings.Rows[0]["ItUdfC" + li3[i].Name.Last()].ToString();
                if (text.Trim().Length == 0) continue;
                li3[i].Text = text;
            }

            var ime = Common.dtSysSettings.Rows[0]["ItIME"].ToString();
            if (ime.Trim().Length > 0) lblItIme.Text = ime;
            #region 品名規格 與 自訂編號，InuptLenth會跟著系統參數改變，但文字框長度永遠不變。(雅真提出)
            //先設定MaxLenth
            this.ItName.MaxLength  = Common.Sys_ItNameLenth;
            this.ItNoUdf.MaxLength = Common.Sys_ItNoUdfLenth;
            //再設定Width
            this.ItName.Width  = (30 * S_61.Basic.JEInitialize.CharWidth) + 7;
            this.ItNoUdf.Width = (20 * S_61.Basic.JEInitialize.CharWidth) + 7;
            #endregion
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void FrmItem_Load(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.IsBalloon = true;
            tp.BackColor = Color.White;
            tp.SetToolTip(ItTrait1, "扣子階");

            dataGridViewT1.DataSource = dtStock;
            dataGridViewT3.SuspendLayout();
            dataGridViewT2.SuspendLayout();
            dataGridViewT3.AllowUserToAddRows = true;
            dataGridViewT2.AllowUserToAddRows = true;

            var pk = jItem.Top();
            writeToTxt(pk);
            btnAppend.Focus();
            
            if (newItno.Length > 0)
            {
                btnAppend_Click(null, null);
            }
        }

        private void loadStock()
        {
            try
            {
                dtStock.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itno", ItNo.Text.Trim());
                    cmd.CommandText = " select itno=(@itno),stkroom.stno,stkroom.stname,ISNULL(stock.itqtyf,0) itqtyf,ISNULL(stock.itqty,0) itqty "
                                    + " from stkroom left join stock on stkroom.stno = stock.stno and stock.itno =(@itno)";
                    da.Fill(dtStock);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void writeToTxt(string itno)
        {
            var result = false;
            jItem.LoadData(itno, row =>
            {
                result = true;

                currentUdf = row["ItNoUdf"].ToString().Trim();
                Writes.ForEach(r =>
                {
                    if (!r.Name.StartsWith("sItPrice"))
                    {
                        if (r is TextBoxNumberT)
                        {
                            r.Text = row[r.Name].ToDecimal().ToString("f" + (r as TextBoxNumberT).LastNum);
                        }
                        else
                        {
                            r.Text = row[r.Name].ToString();
                        }

                        if (Common.User_DateTime == 1)
                        {
                            ItDate.Text = row["ItDate"].ToString();
                            ItBuyDate.Text = row["ItBuyDate"].ToString();
                            ItSalDate.Text = row["ItSalDate"].ToString();
                        }
                        else
                        {
                            ItDate.Text = row["ItDate1"].ToString();
                            ItBuyDate.Text = row["ItBuyDate1"].ToString();
                            ItSalDate.Text = row["ItSalDate1"].ToString();
                        }
                    }
                });

                pictureBoxT1.Clear();
                if (row["pic"] != DBNull.Value) pictureBoxT1.LoadImage((byte[])row["pic"]);

                if (ScNo.TrimTextLenth() > 0)
                {
                    jItem.Validate<JBS.JS.SaleClass>(ScNo.Text, rw => ScName.Text = rw["ScName"].ToString());
                }
                else ScName.Clear();

                if (KiNo.TrimTextLenth() > 0)
                {
                    jItem.Validate<JBS.JS.Kind>(KiNo.Text, rw => KiName.Text = rw["KiName"].ToString());
                }
                else KiName.Clear();

                if (FaNo.TrimTextLenth() > 0)
                {
                    jItem.Validate<JBS.JS.Fact>(FaNo.Text, rw => FaName1.Text = rw["FaName1"].ToString());
                }
                else FaName1.Clear();

                if (row["ItTrait"].ToInteger() == 1) ItTrait1.Checked = true;
                else if (row["ItTrait"].ToInteger() == 2) ItTrait2.Checked = true;
                else if (row["ItTrait"].ToInteger() == 3) ItTrait3.Checked = true;
                else ItTrait3.Checked = true;

                if (row["IsEnable"].ToInteger() == 1) IsEnable1.Checked = true;
                else if (row["IsEnable"].ToInteger() == 3) IsEnable2.Checked = true;
                else IsEnable1.Checked = true;

                if (row["ItCostSlt"].ToInteger() == 1) ItCostSlt1.Checked = true;
                else if (row["ItCostSlt"].ToInteger() == 2) ItCostSlt2.Checked = true;
                else if (row["ItCostSlt"].ToInteger() == 3) ItCostSlt3.Checked = true;
                else ItCostSlt1.Checked = true;

                if (row["ItCodeNo"].ToInteger() == 1) ItCodeNo1.Checked = true;
                else if (row["ItCodeNo"].ToInteger() == 2) ItCodeNo2.Checked = true;
                else if (row["ItCodeNo"].ToInteger() == 3) ItCodeNo3.Checked = true;
                else ItCodeNo3.Checked = true;

                if (row["ItCodeSlt"].ToInteger() == 1) ItCodeSlt1.Checked = true;
                else if (row["ItCodeSlt"].ToInteger() == 2) ItCodeSlt2.Checked = true;
                else ItCodeSlt1.Checked = true;

                if (row["ItBuyUnit"].ToInteger() == 1) ItBuyUnit1.Checked = true;
                else if (row["ItBuyUnit"].ToInteger() == 2) ItBuyUnit2.Checked = true;
                else ItBuyUnit2.Checked = true;

                if (row["ItSalUnit"].ToInteger() == 1) ItSalUnit1.Checked = true;
                else if (row["ItSalUnit"].ToInteger() == 2) ItSalUnit2.Checked = true;
                else ItSalUnit2.Checked = true;

                sItPrice.Text  = row["ItPrice"].ToDecimal().ToString("f" + Common.MS);
                sItPrice1.Text = row["ItPrice1"].ToDecimal().ToString("f" + Common.MS);
                sItPrice2.Text = row["ItPrice2"].ToDecimal().ToString("f" + Common.MS);
                sItPrice3.Text = row["ItPrice3"].ToDecimal().ToString("f" + Common.MS);
                sItPrice4.Text = row["ItPrice4"].ToDecimal().ToString("f" + Common.MS);
                sItPrice5.Text = row["ItPrice5"].ToDecimal().ToString("f" + Common.MS);

                sItPriceP.Text  = row["ItPriceP"].ToDecimal().ToString("f" + Common.MS);
                sItPriceP1.Text = row["ItPriceP1"].ToDecimal().ToString("f" + Common.MS);
                sItPriceP2.Text = row["ItPriceP2"].ToDecimal().ToString("f" + Common.MS);
                sItPriceP3.Text = row["ItPriceP3"].ToDecimal().ToString("f" + Common.MS);
                sItPriceP4.Text = row["ItPriceP4"].ToDecimal().ToString("f" + Common.MS);
                sItPriceP5.Text = row["ItPriceP5"].ToDecimal().ToString("f" + Common.MS);

                ItBarName1.Text = row["ItBarName1"].ToString();
                ItBarName2.Text = row["ItBarName2"].ToString();
                StNo.Text = row["StNo"].ToString();
                StName.Text = SQL.ExecuteScalar("select top 1 stname from stkroom where stno = @stno", new parameters("stno", row["StNo"].ToString()));
                if (ItNo.TrimTextLenth() > 0) loadStock();

                writeotherno(ItNo.Text.ToString());
            });

            if (!result)
            {
                //this.tempNo = "";
                currentUdf = "";
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                ItNote.ReadOnly = true;
                ItNote.Clear();

                pictureBoxT1.Clear();
                dtStock.Clear();
            }
        }

        private void writeotherno(string p)//客戶和廠商型號
        {
            dataGridViewT2.DataSource = null;
            dataGridViewT3.DataSource = null;
            dtcustandard.Clear();
            dtfastandard.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {

                cmd.Parameters.AddWithValue("itno", p);
                cmd.CommandText = @"select standard.*,cust.cuname1 from 
                                    (select * from standard where itno=@itno and kind='cust')standard 
                                    left join cust on standard.cfno=cust.cuno order by cfno ";
                da.Fill(dtcustandard);
                cmd.CommandText = @"select standard.*,fact.faname1 from 
                                    (select * from standard where itno=@itno and kind='fact')standard
                                    left join fact on standard.cfno=fact.fano order by cfno ";
                da.Fill(dtfastandard);
                dataGridViewT2.DataSource = dtfastandard;
                dataGridViewT3.DataSource = dtcustandard;
            }
            dataGridViewT3.ResumeLayout();
            dataGridViewT2.ResumeLayout();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jItem.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jItem.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jItem.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jItem.Bottom();
            writeToTxt(pk);
        }

        void setTxtWhenAppend()
        {
            dataGridViewT3.ReadOnly = false;
            dataGridViewT2.ReadOnly = false;
            dtcustandard.Clear();
            dtfastandard.Clear();
            dtStock.Clear();

            ItDisc.Text = "100";
            ItFirTQty.Text = "0";
            ItStockQty.Text = "0";
            ItSafeQty.Text = "0";
            ItPkgQty.Text = "0";
            ItLastQty.Text = "0";
            ItNW.Text = "0";

            ItPrice.Text = "0";
            ItFirTCost.Text = "0";
            ItFirCost.Text = "0";
            ItBuyPri.Text = "0";
            ItCost.Text = "0";
            ItBuyPriP.Text = "0";
            ItPriceP.Text = "0";
            ItCostP.Text = "0";

            sItPrice.Text = "0";
            sItPrice1.Text = "0";
            sItPrice2.Text = "0";
            sItPrice3.Text = "0";
            sItPrice4.Text = "0";
            sItPrice5.Text = "0";

            sItPriceP.Text = "0";
            sItPriceP1.Text = "0";
            sItPriceP2.Text = "0";
            sItPriceP3.Text = "0";
            sItPriceP4.Text = "0";
            sItPriceP5.Text = "0";

            ItTrait3.Checked = true;
            IsEnable1.Checked = true;
            ItCostSlt1.Checked = true;
            ItCodeNo3.Checked = true;
            ItCodeSlt1.Checked = true;
            ItBuyUnit2.Checked = true;
            ItSalUnit2.Checked = true;

            pictureBoxT1.Clear();
            ItDate.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            ItNote.ReadOnly = false;
            ItNote.Clear();

            ItNo.Focus();
            setTxtWhenAppend();

            if (newItno.Length > 0)
                ItNo.Text = newItno;
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            ItNote.ReadOnly = false;
            dataGridViewT3.ReadOnly = false;
            dataGridViewT2.ReadOnly = false;
            dtcustandard.Clear();
            dtfastandard.Clear();
            ItBuyDate.Text = "";
            ItSalDate.Text = "";
            ItDate.Text = Date.GetDateTime(Common.User_DateTime);

            dtStock.Clear();
            ItBuyDate.Text = "";
            ItSalDate.Text = "";
            ItFirTQty.Text = "0";
            ItFirTCost.Text = "0";
            ItStockQty.Text = "0";

            ItNo.SelectAll();
            ItNoUdf.Text = "";
            ItNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            ItNote.ReadOnly = false;
            dataGridViewT3.ReadOnly = false;
            dataGridViewT2.ReadOnly = false;
            currentUdf = ItNoUdf.Text.Trim();
            IsBom();

            ItNo.ReadOnly = false;
            ItNo.Focus();
            ItNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ItNo.TrimTextLenth() == 0) return;

            if (ItNo.Text.Trim() == "Z$1")
            {
                MessageBox.Show("程式專用之特殊產品，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (ItNo.Text.Trim() == "Z$2")
            {
                MessageBox.Show("程式專用之特殊產品，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (ItNo.Text.Trim() == "Z$3")
            {
                MessageBox.Show("程式專用之特殊產品，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itno", ItNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@boitno", ItNo.Text.Trim());

                    cmd.CommandText = "Select Count(*) from Saled where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.CommandText = "Select Count(*) from rSaled where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.CommandText = "Select Count(*) from Bshopd where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.CommandText = "Select Count(*) from Rshopd where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.CommandText = "Select Count(*) from Adjustd where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.CommandText = "Select Count(*) from drawd where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.CommandText = "Select Count(*) from garnerd where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.CommandText = "Select Count(*) from allotd where itno = (@itno)";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("此產品有使用記錄，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cmd.CommandText = "select itqty from stock where itqty !=0 and itno=(@itno) ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            MessageBox.Show("此產品有庫存，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    cmd.CommandText = "select boitno from bom where boitno=(@boitno) ";
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.HasRows)
                        {
                            MessageBox.Show("此產品已定義為『組合組裝品』，禁止刪除\n", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itno", ItNo.Text.Trim());

                    cmd.CommandText = "delete from item where ItNo=(@itno) ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from stock where ItNo=(@itno) ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from Itemcost where ItNo=(@itno) ";//0622
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from stkcost where ItNo=(@itno) ";//0622
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from bestock where ItNo=(@itno) ";
                    cmd.ExecuteNonQuery();

                    tn.Commit();

                    btnNext_Click(null, null);
                }
                catch (Exception ex)
                {
                    if (tn != null)
                        tn.Rollback();

                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmItemPrint())
            {
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (ItNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmItemb())
            {
                frm.TSeekNo = ItNo.Text.Trim();
                var dl = frm.ShowDialog(this);

                if (dl == DialogResult.OK)
                {
                    writeToTxt(frm.TResult);
                }
                else if (dl == DialogResult.Yes)
                {
                    btnTop_Click(null, null);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            if (jItem.IsRegisted() == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！\n";
                msg += "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ItNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                #region Append
                if (jItem.IsExist(ItNo.Text.Trim()))
                {
                    MessageBox.Show("此產品編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ItNo.Text = string.Empty;
                    ItNo.Focus();
                    return;
                }

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@itno", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@itnoudf", ItNoUdf.Text.Trim());
                        cmd.Parameters.AddWithValue("@itname", ItName.Text);
                        cmd.Parameters.AddWithValue("@kino", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@itime", ItIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItPrice1", sItPrice1.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice2", sItPrice2.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice3", sItPrice3.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice4", sItPrice4.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice5", sItPrice5.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP1", sItPriceP1.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP2", sItPriceP2.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP3", sItPriceP3.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP4", sItPriceP4.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP5", sItPriceP5.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ittrait", getRadioNumber(pTrait));//numeric
                        cmd.Parameters.AddWithValue("@itunit", ItUnit.Text.Trim());
                        cmd.Parameters.AddWithValue("@itunitp", ItUnitP.Text.Trim());
                        cmd.Parameters.AddWithValue("@punit", Punit.Text.Trim());
                        cmd.Parameters.AddWithValue("@itpkgqty", ItPkgQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuypri", ItBuyPri.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itprice", ItPrice.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcost", ItCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuyprip", ItBuyPriP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itpricep", ItPriceP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcostp", ItCostP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuyunit", getRadioNumber(pBuySlt));
                        cmd.Parameters.AddWithValue("@itsalunit", getRadioNumber(pSaleSlt));
                        cmd.Parameters.AddWithValue("@itsafeqty", ItSafeQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itlastqty", ItLastQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itnw", ItNW.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcostslt", getRadioNumber(pCost));
                        cmd.Parameters.AddWithValue("@itcodeslt", getRadioNumber(pCodeSlt));
                        cmd.Parameters.AddWithValue("@itcodeno", getRadioNumber(pCodeNo));
                        cmd.Parameters.AddWithValue("@itdesp1", ItDesp1.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp2", ItDesp2.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp3", ItDesp3.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp4", ItDesp4.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp5", ItDesp5.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp6", ItDesp6.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp7", ItDesp7.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp8", ItDesp8.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp9", ItDesp9.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp10", ItDesp10.Text.Trim());
                        cmd.Parameters.AddWithValue("@itpicture", "");
                        cmd.Parameters.AddWithValue("@itdate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@itdate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@itbuydate", ItBuyDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itbuydate1", ItBuyDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itsaldate", ItSalDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itsaldate1", ItSalDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itfircost", ItFirCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itfirtqty", ItFirTQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itfirtcost", ItFirTCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itstockqty", ItStockQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itnote", ItNote.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf1", ItUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf2", ItUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf3", ItUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf4", ItUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf5", ItUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsEnable", getRadioNumber(pEnable));
                        cmd.Parameters.AddWithValue("@fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@scno", ScNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@pic", pictureBoxT1.ImageToByte());
                        cmd.Parameters.AddWithValue("@ItBarName1", ItBarName1.Text);
                        cmd.Parameters.AddWithValue("@ItBarName2", ItBarName2.Text);
                        cmd.Parameters.AddWithValue("@ItDisc", ItDisc.Text.ToDecimal().ToInteger());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());

                        cmd.CommandText = @"
                        INSERT INTO item
                        (
                             [itno],[itnoudf],[itname],[kino],[itime]
                            ,[ItPrice1],[ItPrice2],[ItPrice3],[ItPrice4],[ItPrice5]
                            ,[ItPriceP1],[ItPriceP2],[ItPriceP3],[ItPriceP4],[ItPriceP5]
                            ,[ittrait],[itunit],[itunitp],[punit],[itpkgqty],[itbuypri]
                            ,[itprice],[itcost],[itbuyprip],[itpricep],[itcostp]
                            ,[itbuyunit],[itsalunit],[itsafeqty],[itlastqty],[itnw]
                            ,[itcostslt],[itcodeslt],[itcodeno],[itdesp1]
                            ,[itdesp2],[itdesp3],[itdesp4],[itdesp5],[itdesp6]
                            ,[itdesp7],[itdesp8],[itdesp9],[itdesp10],[itpicture]
                            ,[itdate],[itdate1],[itbuydate],[itbuydate1],[itsaldate]
                            ,[itsaldate1],[itfircost],[itfirtqty],[itfirtcost],[itstockqty]
                            ,[itnote],[itudf1],[itudf2],[itudf3],[itudf4]
                            ,[itudf5],[IsEnable],[pic],[fano],[scno]
                            ,[ItBarName1],[ItBarName2],[ItDisc],[StNo]
                        ) 
                        VALUES
                        (
                             (@itno),(@itnoudf),(@itname),(@kino),(@itime)
                            ,(@ItPrice1),(@ItPrice2),(@ItPrice3),(@ItPrice4),(@ItPrice5)
                            ,(@ItPriceP1),(@ItPriceP2),(@ItPriceP3),(@ItPriceP4),(@ItPriceP5)
                            ,(@ittrait),(@itunit),(@itunitp),(@punit),(@itpkgqty),(@itbuypri)
                            ,(@itprice),(@itcost),(@itbuyprip),(@itpricep),(@itcostp)
                            ,(@itbuyunit),(@itsalunit),(@itsafeqty),(@itlastqty),(@itnw)
                            ,(@itcostslt),(@itcodeslt),(@itcodeno),(@itdesp1)
                            ,(@itdesp2),(@itdesp3),(@itdesp4),(@itdesp5),(@itdesp6)
                            ,(@itdesp7),(@itdesp8),(@itdesp9),(@itdesp10),(@itpicture)
                            ,(@itdate),(@itdate1),(@itbuydate),(@itbuydate1),(@itsaldate)
                            ,(@itsaldate1),(@itfircost),(@itfirtqty),(@itfirtcost),(@itstockqty)
                            ,(@itnote),(@itudf1),(@itudf2),(@itudf3),(@itudf4)
                            ,(@itudf5),(@IsEnable),(@pic),(@fano),(@scno)
                            ,(@ItBarName1),(@ItBarName2),(@ItDisc),(@StNo)
                            )";
                        cmd.ExecuteNonQuery();
                        SaveStandard(cmd);
                        tn.Commit();

                        jItem.Save(ItNo.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                if (newItno.Length > 0) this.Dispose();

                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                ItNote.ReadOnly = false;
                ItNote.Clear();
                FormState = FormEditState.Append;

                pictureBoxT1.Clear();
                setTxtWhenAppend();
                ItNo.Focus();
                #endregion
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                #region Duplicate
                if (jItem.IsExist(ItNo.Text.Trim()))
                {
                    MessageBox.Show("此產品編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ItNo.Text = string.Empty;
                    ItNo.Focus();
                    return;
                }

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@itno", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@itnoudf", ItNoUdf.Text.Trim());
                        cmd.Parameters.AddWithValue("@itname", ItName.Text);
                        cmd.Parameters.AddWithValue("@kino", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@itime", ItIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItPrice1", sItPrice1.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice2", sItPrice2.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice3", sItPrice3.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice4", sItPrice4.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice5", sItPrice5.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP1", sItPriceP1.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP2", sItPriceP2.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP3", sItPriceP3.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP4", sItPriceP4.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP5", sItPriceP5.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ittrait", getRadioNumber(pTrait));
                        cmd.Parameters.AddWithValue("@itunit", ItUnit.Text.Trim());
                        cmd.Parameters.AddWithValue("@itunitp", ItUnitP.Text.Trim());
                        cmd.Parameters.AddWithValue("@punit", Punit.Text.Trim());
                        cmd.Parameters.AddWithValue("@itpkgqty", ItPkgQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuypri", ItBuyPri.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itprice", ItPrice.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcost", ItCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuyprip", ItBuyPriP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itpricep", ItPriceP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcostp", ItCostP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuyunit", getRadioNumber(pBuySlt));
                        cmd.Parameters.AddWithValue("@itsalunit", getRadioNumber(pSaleSlt));
                        cmd.Parameters.AddWithValue("@itsafeqty", ItSafeQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itlastqty", ItLastQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itnw", ItNW.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcostslt", getRadioNumber(pCost));
                        cmd.Parameters.AddWithValue("@itcodeslt", getRadioNumber(pCodeSlt));
                        cmd.Parameters.AddWithValue("@itcodeno", getRadioNumber(pCodeNo));
                        cmd.Parameters.AddWithValue("@itdesp1", ItDesp1.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp2", ItDesp2.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp3", ItDesp3.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp4", ItDesp4.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp5", ItDesp5.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp6", ItDesp6.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp7", ItDesp7.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp8", ItDesp8.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp9", ItDesp9.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp10", ItDesp10.Text.Trim());
                        cmd.Parameters.AddWithValue("@itpicture", "");
                        cmd.Parameters.AddWithValue("@itdate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@itdate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@itbuydate", ItBuyDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itbuydate1", ItBuyDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itsaldate", ItSalDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itsaldate1", ItSalDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itfircost", ItFirCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itfirtqty", 0);
                        cmd.Parameters.AddWithValue("@itfirtcost", ItFirTCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itstockqty", 0);
                        cmd.Parameters.AddWithValue("@itnote", ItNote.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf1", ItUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf2", ItUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf3", ItUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf4", ItUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf5", ItUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsEnable", getRadioNumber(pEnable));
                        cmd.Parameters.AddWithValue("@fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@scno", ScNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@pic", pictureBoxT1.ImageToByte());
                        cmd.Parameters.AddWithValue("@ItBarName1", ItBarName1.Text);
                        cmd.Parameters.AddWithValue("@ItBarName2", ItBarName2.Text);
                        cmd.Parameters.AddWithValue("@ItDisc", ItDisc.Text.ToDecimal().ToInteger());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());

                        cmd.CommandText = @"
                        INSERT INTO item
                        (
                             [itno],[itnoudf],[itname],[kino],[itime]
                            ,[ItPrice1],[ItPrice2],[ItPrice3],[ItPrice4],[ItPrice5]
                            ,[ItPriceP1],[ItPriceP2],[ItPriceP3],[ItPriceP4],[ItPriceP5]
                            ,[ittrait],[itunit],[itunitp],[punit],[itpkgqty],[itbuypri]
                            ,[itprice],[itcost],[itbuyprip],[itpricep],[itcostp]
                            ,[itbuyunit],[itsalunit],[itsafeqty],[itlastqty],[itnw]
                            ,[itcostslt],[itcodeslt],[itcodeno],[itdesp1]
                            ,[itdesp2],[itdesp3],[itdesp4],[itdesp5],[itdesp6]
                            ,[itdesp7],[itdesp8],[itdesp9],[itdesp10],[itpicture]
                            ,[itdate],[itdate1],[itbuydate],[itbuydate1],[itsaldate]
                            ,[itsaldate1],[itfircost],[itfirtqty],[itfirtcost],[itstockqty]
                            ,[itnote],[itudf1],[itudf2],[itudf3],[itudf4]
                            ,[itudf5],[IsEnable],[pic],[fano],[scno]
                            ,[ItBarName1],[ItBarName2],[ItDisc],[StNo]
                        ) 
                        VALUES
                        (
                             (@itno),(@itnoudf),(@itname),(@kino),(@itime),(@ItPrice1),(@ItPrice2)
                            ,(@ItPrice3),(@ItPrice4),(@ItPrice5),(@ItPriceP1),(@ItPriceP2),(@ItPriceP3)
                            ,(@ItPriceP4),(@ItPriceP5),(@ittrait),(@itunit),(@itunitp),(@punit),(@itpkgqty)
                            ,(@itbuypri),(@itprice),(@itcost),(@itbuyprip),(@itpricep),(@itcostp)
                            ,(@itbuyunit),(@itsalunit),(@itsafeqty),(@itlastqty),(@itnw),(@itcostslt)
                            ,(@itcodeslt),(@itcodeno),(@itdesp1),(@itdesp2),(@itdesp3),(@itdesp4)
                            ,(@itdesp5),(@itdesp6),(@itdesp7),(@itdesp8),(@itdesp9),(@itdesp10)
                            ,(@itpicture),(@itdate),(@itdate1),(@itbuydate),(@itbuydate1),(@itsaldate)
                            ,(@itsaldate1),(@itfircost),(@itfirtqty),(@itfirtcost),(@itstockqty)
                            ,(@itnote),(@itudf1),(@itudf2),(@itudf3),(@itudf4),(@itudf5),(@IsEnable)
                            ,(@pic),(@fano),(@scno),(@ItBarName1),(@ItBarName2),(@ItDisc),(@StNo)
                        )";

                        cmd.ExecuteNonQuery();
                        SaveStandard(cmd);
                        tn.Commit();

                        jItem.Save(ItNo.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                ItNote.ReadOnly = false;
                ItNote.Clear();
                FormState = FormEditState.Append;

                pictureBoxT1.Clear();
                setTxtWhenAppend();
                ItNo.Focus();
                #endregion
            }

            if (this.FormState == FormEditState.Modify)
            {
                #region Modify
                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@itno", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@itnoudf", ItNoUdf.Text.Trim());
                        cmd.Parameters.AddWithValue("@itname", ItName.Text);
                        cmd.Parameters.AddWithValue("@kino", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@itime", ItIme.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItPrice1", sItPrice1.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice2", sItPrice2.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice3", sItPrice3.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice4", sItPrice4.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPrice5", sItPrice5.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP1", sItPriceP1.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP2", sItPriceP2.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP3", sItPriceP3.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP4", sItPriceP4.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ItPriceP5", sItPriceP5.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@ittrait", getRadioNumber(pTrait));
                        cmd.Parameters.AddWithValue("@itunit", ItUnit.Text.Trim());
                        cmd.Parameters.AddWithValue("@itunitp", ItUnitP.Text.Trim());
                        cmd.Parameters.AddWithValue("@punit", Punit.Text.Trim());
                        cmd.Parameters.AddWithValue("@itpkgqty", ItPkgQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuypri", ItBuyPri.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itprice", ItPrice.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcost", ItCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuyprip", ItBuyPriP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itpricep", ItPriceP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcostp", ItCostP.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itbuyunit", getRadioNumber(pBuySlt));
                        cmd.Parameters.AddWithValue("@itsalunit", getRadioNumber(pSaleSlt));
                        cmd.Parameters.AddWithValue("@itsafeqty", ItSafeQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itlastqty", ItLastQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itnw", ItNW.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itcostslt", getRadioNumber(pCost));
                        cmd.Parameters.AddWithValue("@itcodeslt", getRadioNumber(pCodeSlt));
                        cmd.Parameters.AddWithValue("@itcodeno", getRadioNumber(pCodeNo));
                        cmd.Parameters.AddWithValue("@itdesp1", ItDesp1.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp2", ItDesp2.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp3", ItDesp3.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp4", ItDesp4.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp5", ItDesp5.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp6", ItDesp6.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp7", ItDesp7.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp8", ItDesp8.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp9", ItDesp9.Text.Trim());
                        cmd.Parameters.AddWithValue("@itdesp10", ItDesp10.Text.Trim());
                        cmd.Parameters.AddWithValue("@itpicture", "");
                        cmd.Parameters.AddWithValue("@itdate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@itdate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@itbuydate", ItBuyDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itbuydate1", ItBuyDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itsaldate", ItSalDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itsaldate1", ItSalDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@itfircost", ItFirCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itfirtqty", ItFirTQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itfirtcost", ItFirTCost.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itstockqty", ItStockQty.Text.Trim().ToDecimal());
                        cmd.Parameters.AddWithValue("@itnote", ItNote.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf1", ItUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf2", ItUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf3", ItUdf3.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf4", ItUdf4.Text.Trim());
                        cmd.Parameters.AddWithValue("@itudf5", ItUdf5.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsEnable", getRadioNumber(pEnable));
                        cmd.Parameters.AddWithValue("@fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@scno", ScNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItBarName1", ItBarName1.Text);
                        cmd.Parameters.AddWithValue("@ItBarName2", ItBarName2.Text);
                        cmd.Parameters.AddWithValue("@ItDisc", ItDisc.Text.ToDecimal().ToInteger());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());

                        cmd.CommandText = "UPDATE item SET "
                        + "[itnoudf] =@itnoudf"
                        + ",[itname] =@itname"
                        + ",[kino] =@kino"
                        + ",[itime] =@itime"
                        + ",[ittrait] = @ittrait"
                        + ",[IsEnable] =@IsEnable"
                        + ",[itunit] =@itunit"
                        + ",[itunitp] =@itunitp"
                        + ",[Punit] =@Punit"
                        + ",[itpkgqty] = @itpkgqty"
                        + ",[itbuypri] = @itbuypri"
                        + ",[itprice] = @itprice"
                        + ",[itcost] =@itcost"
                        + ",[itbuyprip] = @itbuyprip"
                        + ",[itpricep] = @itpricep"
                        + ",[itcostp] = @itcostp"
                        + ",[itbuyunit] = @itbuyunit"
                        + ",[itsalunit] = @itsalunit"
                        + ",[itsafeqty] = @itsafeqty"
                        + ",[itlastqty] = @itlastqty"
                        + ",[itnw] = @itnw"
                        + ",[itcostslt] = @itcostslt"
                        + ",[itcodeslt] = @itcodeslt"
                        + ",[itcodeno] = @itcodeno"
                        + ",[itdesp1] =@itdesp1"
                        + ",[itdesp2] =@itdesp2"
                        + ",[itdesp3] =@itdesp3"
                        + ",[itdesp4] =@itdesp4"
                        + ",[itdesp5] =@itdesp5"
                        + ",[itdesp6] =@itdesp6"
                        + ",[itdesp7] =@itdesp7"
                        + ",[itdesp8] =@itdesp8"
                        + ",[itdesp9] =@itdesp9"
                        + ",[itdesp10] =@itdesp10"
                        + ",[itpicture] =@itpicture"
                        + ",[itfircost] = @itfircost"
                        + ",[ItPrice1] = @ItPrice1"
                        + ",[ItPrice2] = @ItPrice2"
                        + ",[ItPrice3] = @ItPrice3"
                        + ",[ItPrice4] = @ItPrice4"
                        + ",[ItPrice5] = @ItPrice5"
                        + ",[ItPricep1] = @ItPricep1"
                        + ",[ItPricep2] = @ItPricep2"
                        + ",[ItPricep3] = @ItPricep3"
                        + ",[ItPricep4] = @ItPricep4"
                        + ",[ItPricep5] = @ItPricep5"
                        + ",[itnote] =@itnote"
                        + ",[itudf1] =@itudf1"
                        + ",[itudf2] =@itudf2"
                        + ",[itudf3] =@itudf3"
                        + ",[itudf4] =@itudf4"
                        + ",[itudf5] =@itudf5"
                        + ",[fano] =@fano"
                        + ",[pic] = @pic "
                        + ",[scno] = @scno "
                        + ",[ItBarName1] =@ItBarName1 "
                        + ",[ItBarName2] =@ItBarName2 "
                        + ",[ItDisc] =@ItDisc "
                        + ",[StNo] =@StNo "
                        + " where itno=(@itno) ";

                        cmd.Parameters.Add("@pic", SqlDbType.Image);
                        cmd.Parameters["@pic"].Value = pictureBoxT1.ImageToByte();
                        cmd.ExecuteNonQuery();
                        SaveStandard(cmd);
                        tn.Commit();

                        jItem.Save(ItNo.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                    finally
                    {
                        if (tn != null)
                            tn.Dispose();
                    }
                }

                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                ItNote.ReadOnly = false;
                ItNote.Clear();
                FormState = FormEditState.Append;

                pictureBoxT1.Clear();
                setTxtWhenAppend();
                ItNo.Focus();
                #endregion
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (newItno.Length > 0) this.Dispose();

            var pk = jItem.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            ItNote.ReadOnly = true;
            btnAppend.Focus();
            dataGridViewT3.ReadOnly = true;
            dataGridViewT2.ReadOnly = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            if (ItNo.ReadOnly)
                return;

            using (var frm = new FrmItemb())
            {
                frm.TSeekNo = ItNo.Text.Trim();

                if (DialogResult.OK == frm.ShowDialog())
                    writeToTxt(frm.TResult);
            }
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            jItem.Open<JBS.JS.Kind>(sender, row =>
            {
                KiNo.Text = row["KiNo"].ToString().Trim();
                KiName.Text = row["KiName"].ToString().Trim();
            });
        }
        private void KiNo_Validating(object sender, CancelEventArgs e)
        {
            if (KiNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (KiNo.TrimTextLenth() == 0)
            {
                KiNo.Text = KiName.Text = "";
                return;
            }

            jItem.ValidateOpen<JBS.JS.Kind>(sender, e, row =>
            {
                KiNo.Text = row["KiNo"].ToString().Trim();
                KiName.Text = row["KiName"].ToString().Trim();
            });
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    btnBrow.PerformClick();
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

        decimal getRadioNumber(PanelNT pnl)
        {
            var LastNum = pnl.Controls.OfType<RadioT>().Where(r => r.Checked).FirstOrDefault().Name.Trim().Last().ToDecimal();
            return LastNum == 0 ? 1 : LastNum;
        }

        private void btnPic_Click(object sender, EventArgs e)
        {
            pictureBoxT1.LoadImage();
        }

        private void btnPicClear_Click(object sender, EventArgs e)
        {
            pictureBoxT1.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxT1.SizeMode = checkBox1.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;
        }

        void IsBom()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@boitno", ItNo.Text.Trim());

                    cmd.CommandText = "select boitno from bom where boitno =@boitno" + " COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        ItTrait3.Enabled = rd.HasRows ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ItNo_Enter(object sender, EventArgs e)
        {
            BeforeText = ItNo.Text;
        }

        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            if (ItNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (ItNo.Text.Trim() == "")
            {
                e.Cancel = true;
                ItNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jItem.IsExist(ItNo.Text.Trim()))
                {
                    e.Cancel = true;

                    using (var frm = new FrmItemb())
                    {
                        frm.TSeekNo = ItNo.Text.Trim();
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                writeToTxt(frm.TResult);
                                break;
                        }
                    }
                }
                else
                {
                    if (ItNo.Text.Trim() == ItNoUdf.Text.Trim())
                    {
                        e.Cancel = true;

                        using (var frm = new FrmItemb())
                        {
                            frm.TSeekNo = ItNo.Text.Trim();
                            switch (frm.ShowDialog())
                            {
                                case DialogResult.OK:
                                    writeToTxt(frm.TResult);
                                    break;
                            }
                        }
                        return;
                    }

                    if (jItem.IsExist(ItNo.Text.Trim()))
                    {
                        e.Cancel = true;

                        using (var frm = new FrmItemb())
                        {
                            frm.TSeekNo = ItNo.Text.Trim();
                            switch (frm.ShowDialog())
                            {
                                case DialogResult.OK:
                                    writeToTxt(frm.TResult);
                                    break;
                            }
                        }
                    }
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jItem.IsExist(ItNo.Text.Trim()))
                {
                    e.Cancel = true;

                    using (var frm = new FrmItemb())
                    {
                        frm.TSeekNo = ItNo.Text.Trim();
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                writeToTxt(frm.TResult);
                                break;
                        }
                    }
                }
                else
                {
                    if (ItNo.Text.Trim() == ItNoUdf.Text.Trim())
                    {
                        e.Cancel = true;

                        using (var frm = new FrmItemb())
                        {
                            frm.TSeekNo = ItNo.Text.Trim();
                            switch (frm.ShowDialog())
                            {
                                case DialogResult.OK:
                                    writeToTxt(frm.TResult);
                                    break;
                            }
                        }
                        return;
                    }

                    if (jItem.IsExist(ItNo.Text.Trim()))
                    {
                        e.Cancel = true;

                        using (var frm = new FrmItemb())
                        {
                            frm.TSeekNo = ItNo.Text.Trim();
                            switch (frm.ShowDialog())
                            {
                                case DialogResult.OK:
                                    writeToTxt(frm.TResult);
                                    break;
                            }
                        }
                    }
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jItem.IsExist(ItNo.Text.Trim()))
                {
                    if (ItNo.Text.Trim() == BeforeText)
                        return;

                    writeToTxt(ItNo.Text.Trim());
                }
                else
                {
                    e.Cancel = true;
                    ItNo.SelectAll();
                    using (var frm = new FrmItemb())
                    {
                        frm.TSeekNo = ItNo.Text.Trim();
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                writeToTxt(frm.TResult);
                                break;
                        }
                    }
                }
            }
        }

        private void ItNoUdf_Validating(object sender, CancelEventArgs e)
        {
            if (ItNoUdf.ReadOnly) return;

            if (ItNoUdf.TrimTextLenth() == 0)
            {
                ItNoUdf.Text = "";
                return;
            }

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate || this.FormState == FormEditState.Modify)
            {
                if (Common.load("Check", "item", "itno", ItNoUdf.Text.Trim()) != null || Common.load("Check", "item", "ItNoUdf", ItNoUdf.Text.Trim()) != null)
                {
                    if (this.FormState == FormEditState.Modify)
                    {
                        if (currentUdf == ItNoUdf.Text.Trim()) return;
                    }

                    if (btnCancel.Focused) return;
                    e.Cancel = true;
                    ItNoUdf.Text = "";
                    MessageBox.Show("此自定編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (ItNoUdf.Text.Trim() == ItNo.Text.Trim())
                    {
                        if (btnCancel.Focused) return;
                        e.Cancel = true;
                        ItNoUdf.Text = "";
                        MessageBox.Show("此自定編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }


        private void ItUnit_Validating(object sender, CancelEventArgs e)
        {
            if (ItUnit.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (ItUnit.TrimTextLenth() > 0)
            {
                if (ItUnit.Text.Trim() == ItUnitP.Text.Trim())
                {
                    e.Cancel = true;
                    ItUnit.SelectAll();
                    MessageBox.Show("『單位』與『包裝單位』不可相同", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                ItUnit.Text = ItUnit.Text.Trim();
            }
        }

        private void ItUnitP_Validating(object sender, CancelEventArgs e)
        {
            if (ItUnitP.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (ItUnitP.TrimTextLenth() > 0)
            {
                if (ItUnit.Text.Trim() == ItUnitP.Text.Trim())
                {
                    e.Cancel = true;
                    ItUnitP.SelectAll();
                    MessageBox.Show("『單位』與『包裝單位』不可相同", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                ItUnitP.Text = ItUnitP.Text.Trim();
            }
        }

        private void sItPrice_Validating(object sender, CancelEventArgs e)
        {
            ItPrice.Text = sItPrice.Text.ToDecimal().ToString("f" + ItPrice.LastNum);
        }

        private void sItPriceP_Validating(object sender, CancelEventArgs e)
        {
            ItPriceP.Text = sItPriceP.Text.ToDecimal().ToString("f" + ItPriceP.LastNum);
        }

        private void ItPrice_Validating(object sender, CancelEventArgs e)
        {
            sItPrice.Text = ItPrice.Text.ToDecimal().ToString("f" + sItPrice.LastNum);
        }

        private void ItPriceP_Validating(object sender, CancelEventArgs e)
        {
            sItPriceP.Text = ItPriceP.Text.ToDecimal().ToString("f" + sItPriceP.LastNum);
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            jItem.Open<JBS.JS.Fact>(sender, row =>
            {
                FaNo.Text = row["FaNo"].ToString();
                FaName1.Text = row["FaName1"].ToString();
            });
        }

        private void FaNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            if (FaNo.TrimTextLenth() == 0)
            {
                FaNo.Text = FaName1.Text = "";
                return;
            }

            jItem.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                FaNo.Text = row["FaNo"].ToString();
                FaName1.Text = row["FaName1"].ToString();
            });

        }

        private void ItUnit_DoubleClick(object sender, EventArgs e)
        {
            if (ItUnit.ReadOnly) return;

            using (var frm = new FrmUnit())
            {
                frm.Kid = 1;
                if (DialogResult.OK == frm.ShowDialog()) ItUnit.Text = frm.Result;
            }
        }

        private void ItUnitP_DoubleClick(object sender, EventArgs e)
        {
            if (ItUnitP.ReadOnly) return;

            using (var frm = new FrmUnit())
            {
                frm.Kid = 2;
                if (DialogResult.OK == frm.ShowDialog()) ItUnitP.Text = frm.Result;
            }
        }

        private void ItTrait3_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@boitno", ItNo.Text.Trim());
                cmd.CommandText = "select boitno from bom where boitno =(@boitno) ";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) MessageBox.Show("此產品為『組合組裝品』禁止變更為『單一商品』", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            pTrait.Enabled = pEnable.Enabled = pCost.Enabled = pCodeNo.Enabled = pCodeSlt.Enabled = pSaleSlt.Enabled = pBuySlt.Enabled = !btnAppend.Enabled;
            btnPic.Enabled = btnPicClear.Enabled = !btnAppend.Enabled;
        }

        private void ScNo_DoubleClick(object sender, EventArgs e)
        {
            if (ScNo.ReadOnly)
                return;

            jItem.Open<JBS.JS.SaleClass>(sender, row =>
            {
                ScNo.Text = row["scno"].ToString().Trim();
                ScName.Text = row["scname"].ToString().Trim();
            });
        }

        private void ScNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            if (ScNo.TrimTextLenth() == 0)
            {
                ScNo.Text = ScName.Text = "";
                return;
            }

            jItem.ValidateOpen<JBS.JS.SaleClass>(sender, e, row =>
            {
                ScNo.Text = row["scno"].ToString().Trim();
                ScName.Text = row["scname"].ToString().Trim();
            });
        }

        private void Punit_DoubleClick(object sender, EventArgs e)
        {
            if (Punit.ReadOnly) return;

            using (var frm = new FrmUnit())
            {
                frm.Kid = 1;
                if (DialogResult.OK == frm.ShowDialog()) Punit.Text = frm.Result;
            }
        }

        private void ItName_Validating(object sender, CancelEventArgs e)
        {
            if (ItName.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (ItBarName1.TrimTextLenth() == 0)
            {
                ItBarName1.Text = ItName.Text.GetUTF8(16);
                ItBarName2.Text = new string(ItName.Text.Skip(ItBarName1.Text.Length).ToArray()).GetUTF8(16);
            }
        }

        private void ItBarName1_Validating(object sender, CancelEventArgs e)
        {
            bar1 = ItBarName1.Text;
        }

        private void ItBarName1_Validated(object sender, EventArgs e)
        {
            ItBarName1.Text = bar1.GetUTF8(16);
        }

        private void ItBarName2_Validating(object sender, CancelEventArgs e)
        {
            bar2 = ItBarName2.Text;
        }

        private void ItBarName2_Validated(object sender, EventArgs e)
        {
            ItBarName2.Text = bar2.GetUTF8(16);
        }

        public DialogResult ShowDialog(out string itno)
        {
            var dl = this.ShowDialog();

            itno = jItem.GetCurrent();
            return dl;
        }

        private void ItDisc_Validating(object sender, CancelEventArgs e)
        {
            if (ItDisc.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            if (ItDisc.Text.ToDecimal() > 100)
            {
                e.Cancel = true;
                MessageBox.Show("抽成比不可超過100%");
                ItDisc.SelectAll();
                return;
            }
        }

        private void dataGridViewT3_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT3.ReadOnly || btnCancel.Focused) return;
            if (dataGridViewT3.Columns[e.ColumnIndex].Name == "客戶編號")
            {
                if (dataGridViewT3["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim() == "")
                {
                    dataGridViewT3["客戶編號", e.RowIndex].Value = "";
                    dataGridViewT3["客戶簡稱", e.RowIndex].Value = "";
                    return;
                }
                var r = Common.load("Check", "cust", "cuno", dataGridViewT3["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim());
                if (r != null)
                {
                    if (dataGridViewT3.Rows.OfType<DataGridViewRow>().Count(t => t.Cells["客戶編號"].EditedFormattedValue.ToString().Trim() == r["cuno"].ToString().Trim()) > 1)
                    {
                        MessageBox.Show("此客戶已有此產品型號無法重複", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        ((TextBox)dataGridViewT3.EditingControl).SelectAll();
                        return;
                    }
                    dataGridViewT3.EditingControl.Text = r["cuno"].ToString();
                    dataGridViewT3["客戶編號", e.RowIndex].Value = r["cuno"].ToString();
                    dataGridViewT3["客戶簡稱", e.RowIndex].Value = r["cuname1"].ToString();
                    dataGridViewT3.InvalidateRow(e.RowIndex);
                }
                else
                {
                    e.Cancel = true;
                    using (FrmCustBrow frm = new FrmCustBrow(true, FormStyle.Mini))
                    {
                        frm.SeekNo = dataGridViewT3["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dataGridViewT3.EditingControl.Text = frm.SeekNo;
                            dataGridViewT3["客戶簡稱", e.RowIndex].Value = frm.Result["cuname1"].ToString();
                        }
                    }
                }

            }
            else if (dataGridViewT3.Columns[e.ColumnIndex].Name == "型號")
            {
                dataGridViewT3["型號", e.RowIndex].Value = dataGridViewT3["型號", e.RowIndex].EditedFormattedValue.ToString();
                dataGridViewT3.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridViewT3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT3.ReadOnly) return;
            if (dataGridViewT3.Columns[e.ColumnIndex].Name == "客戶編號")
            {
                using (FrmCustBrow frm = new FrmCustBrow(true, FormStyle.Mini))
                {
                    frm.SeekNo = dataGridViewT3["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewT3.EditingControl.Text = frm.SeekNo;
                        dataGridViewT3["客戶簡稱", e.RowIndex].Value = frm.Result["cuname1"].ToString();
                    }
                }
            }

        }

        private void dataGridViewT2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT2.ReadOnly || btnCancel.Focused) return;
            if (dataGridViewT2.Columns[e.ColumnIndex].Name == "廠商編號")
            {
                if (dataGridViewT2["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim() == "")
                {
                    dataGridViewT2["廠商編號", e.RowIndex].Value = "";
                    dataGridViewT2["廠商簡稱", e.RowIndex].Value = "";
                    return;
                }
                var r = Common.load("Check", "fact", "fano", dataGridViewT2["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim());
                if (r != null)
                {
                    if (dataGridViewT2.Rows.OfType<DataGridViewRow>().Count(t => t.Cells["廠商編號"].EditedFormattedValue.ToString().Trim() == r["fano"].ToString().Trim()) > 1)
                    {
                        MessageBox.Show("此廠商已有此產品型號無法重複", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        ((TextBox)dataGridViewT2.EditingControl).SelectAll();
                        return;
                    }
                    dataGridViewT2.EditingControl.Text = r["fano"].ToString();
                    dataGridViewT2["廠商編號", e.RowIndex].Value = r["fano"].ToString();
                    dataGridViewT2["廠商簡稱", e.RowIndex].Value = r["faname1"].ToString();
                    dataGridViewT2.InvalidateRow(e.RowIndex);
                }
                else
                {
                    e.Cancel = true;
                    using (FrmFactBrow frm = new FrmFactBrow(true, FormStyle.Mini))
                    {
                        frm.SeekNo = dataGridViewT2["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dataGridViewT2.EditingControl.Text = frm.SeekNo;
                            dataGridViewT2["廠商簡稱", e.RowIndex].Value = frm.Result["faname1"].ToString();
                        }
                    }
                }

            }
            else if (dataGridViewT2.Columns[e.ColumnIndex].Name == "型號1")
            {
                dataGridViewT2["型號1", e.RowIndex].Value = dataGridViewT2["型號1", e.RowIndex].EditedFormattedValue.ToString();
                dataGridViewT2.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.ReadOnly) return;
            if (dataGridViewT2.Columns[e.ColumnIndex].Name == "廠商編號")
            {
                using (FrmFactBrow frm = new FrmFactBrow(true, FormStyle.Mini))
                {
                    frm.SeekNo = dataGridViewT2["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewT2.EditingControl.Text = frm.SeekNo;
                        dataGridViewT2["廠商簡稱", e.RowIndex].Value = frm.Result["faname1"].ToString();
                    }
                }
            }
            
        }

        private void SaveStandard(SqlCommand cmd)//存客戶和廠商的型號
        {
            try
            {
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                cmd.CommandText = "delete standard where itno=@itno";
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("price", "");
                cmd.Parameters.AddWithValue("cfno", "");
                cmd.Parameters.AddWithValue("standard", "");

                for (int i = 0; i < dataGridViewT3.Rows.Count-1; i++)
                {
                    if ((bool)dataGridViewT3.Rows[i].Cells["客戶刪除"].EditedFormattedValue == true) continue;
                    cmd.Parameters["cfno"].Value = dataGridViewT3.Rows[i].Cells["客戶編號"].Value.ToString();
                    cmd.Parameters["standard"].Value = dataGridViewT3.Rows[i].Cells["型號"].Value.ToString();
                    cmd.CommandText = "insert into standard (kind,cfno,itno,standard) values ('cust',@cfno,@itno,@standard)";
                    cmd.ExecuteNonQuery();
                }
                for (int i = 0; i < dataGridViewT2.Rows.Count-1; i++)
                {
                    if ((bool)dataGridViewT2.Rows[i].Cells["廠商刪除"].EditedFormattedValue == true) continue;
                    cmd.Parameters["cfno"].Value = dataGridViewT2.Rows[i].Cells["廠商編號"].Value.ToString();
                    cmd.Parameters["standard"].Value = dataGridViewT2.Rows[i].Cells["型號1"].Value.ToString();
                    cmd.CommandText = "insert into standard (kind,cfno,itno,standard) values ('fact',@cfno,@itno,@standard)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jItem.Open<JBS.JS.Stkroom>(sender, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Text = StNo.Text = "";
                return;
            }

            jItem.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }


    }
}
