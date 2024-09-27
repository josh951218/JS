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

namespace S_61.subMenuFm_2
{
    public partial class FrmGarner : Formbase
    {
        JBS.JS.Garner jGarner;

        List<TextBoxbase> list;
        DataTable GaD = new DataTable();
        DataTable tempGaD = new DataTable();

        DataTable GaBom = new DataTable();
        DataTable tempBom = new DataTable();

        #region 批次資料
        BatchProcess BatchF = new BatchProcess();           //批次存資料異動修改
        BatchSave BatchSave = new BatchSave();              //批次資料存檔用
        DataTable dt_BatchProcess = new DataTable();        //批次異動
        DataTable dt_TempBatchProcess = new DataTable();    //批次異動暫存檔

        DataTable dt_Bom_BatchProcess = new DataTable();       //bom批次異動
        DataTable dt_Bom_TempBatchProcess = new DataTable();   //bom批次異動暫存檔
        string old_StNoI = "";
        string old_StNoO = "";
        #endregion


        decimal BomRec = 0;
        string ItNoBegin = "";
        string UdfNoBegin = "";
        string TextBefore = "";
        string Memo1 = "";

        public FrmGarner()
        {
            InitializeComponent();
            this.jGarner = new JBS.JS.Garner();
            this.list = this.getEnumMember();
            this.說明.HeaderText = Common.Sys_MemoUdf;

            this.入庫數量.Set庫存數量小數();
            this.成本.Set本幣金額小數();
            this.金額.Set本幣金額小數();
            this.包裝數量.Set庫存數量小數();

            TotMnyB.FirstNum = 15;
            TotMnyB.LastNum = Common.M;

            if (Common.Series == "74" || Common.Series == "72")
            {
                OrNo.Visible = false;
                lblT7.Visible = false;
            }

            this.成本.Visible = Common.User_ShopPrice;
            this.金額.Visible = Common.User_ShopPrice;
            TotMnyB.Visible = Common.User_ShopPrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
            dataGridViewT1.DataSource = GaD;

            if (Common.Sys_UsingBatch == 1)
            {
                this.gridbatch.Visible = false;
            }

        }

        private void FrmGarner_Load(object sender, EventArgs e)
        {
            GaDate.SetDateLength();

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
                        end
                        ,ItNoUdf='',* from garnerd where 1=0 ";

                    da.Fill(GaD);
                    da.Fill(tempGaD);
                }
            };
            InitdtD.Invoke();

            BatchF.建立結構(dt_BatchProcess);
            dt_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_TempBatchProcess = dt_BatchProcess.Clone();
            dataGridView1.DataSource = dt_BatchProcess;
            dataGridView2.DataSource = dt_Bom_BatchProcess;

            var gano = jGarner.Bottom();
            writeToTxt(gano);
        }

        private void FrmGarner_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string gano)
        {
            BomRec = 0;

            var result = jGarner.LoadData(gano, row =>
            {
                GaNo.Text = row["gano"].ToString();

                if (Common.User_DateTime == 1)
                    GaDate.Text = row["gadate"].ToString();
                else
                    GaDate.Text = row["gadate1"].ToString();
                OrNo.Text = row["orno"].ToString();
                StNoI.Text = row["stnoi"].ToString();
                StNameI.Text = row["stnamei"].ToString();
                StNoO.Text = row["stnoo"].ToString();
                StNameO.Text = row["stnameo"].ToString();
                EmNo.Text = row["emno"].ToString();
                EmName.Text = row["emname"].ToString();
                GaMemo.Text = row["gamemo"].ToString();
                TotMnyB.Text = row["totmnyb"].ToDecimal().ToString("f" + Common.M);

                loadD();

                this.Memo1 = row["gamemo1"].ToString();
                jGarner.keyMan.Set(row);

            });

            if (!result)
            {
                Common.SetTextState(this.FormState = FormEditState.Clear, ref list);
                GaD.Clear();
                tempGaD.Clear();
                GaBom.Clear();
                tempBom.Clear();

                this.Memo1 = "";
                jGarner.keyMan.Clear();
            }
            BatchF.雙倉上下頁dt資料修改("Garnerd", gano, dt_BatchProcess, dt_TempBatchProcess);
            BatchF.BOM雙倉上下頁dt資料修改("Garnerd", "GarnerBom", gano, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
            old_StNoI = StNoI.Text.Trim();
            old_StNoO = StNoO.Text.Trim();
        }

        void loadD()
        {
            GaD.Clear();
            tempGaD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select 產品組成="
                        + "case"
                        + " when ittrait=1 then '組合品'"
                        + " when ittrait=2 then '組裝品'"
                        + " when ittrait=3 then '單一商品'"
                        + " end"
                        + " ,ItNoUdf= (select top 1 itnoudf from item where item.itno = garnerd.itno),* from garnerd where gano=@gano ORDER BY recordno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gano", GaNo.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(GaD);
                            dd.Fill(tempGaD);
                        }
                    }
                }
                dataGridViewT1.DataSource = GaD;

                if (GaD.Rows.Count > 0) BomRec = GaD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadSaleBom()
        {
            GaBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select top 1 * from garnbom where 1=0";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from garnbom where GaNo=@GaNo ";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from garnbom where GaNo=@GaNo ";
                     
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@GaNo", jGarner.GetCurrent());
                    cmd.CommandText = sql;
                     
                    da.Fill(GaBom);
                    da.Fill(tempBom);
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void AddRow()
        {
            DataRow row = GaD.NewRow();
            row["itno"] = "";
            row["itname"] = "";
            row["qty"] = 0;
            row["itunit"] = "";
            row["costb"] = 0;
            row["mnyb"] = 0;
            row["itpkgqty"] = 1;
            row["產品組成"] = "";
            row["orno"] = "";
            row["memo"] = "";
            row["BomRec"] = GetBomRec();
            GaD.Rows.Add(row);
            GaD.AcceptChanges();
        }

        void AddRow(int index)
        {
            var row = GaD.NewRow();
            row["itno"] = "";
            row["itname"] = "";
            row["qty"] = 0;
            row["itunit"] = "";
            row["costb"] = 0;
            row["mnyb"] = 0;
            row["itpkgqty"] = 1;
            row["產品組成"] = "";
            row["orno"] = "";
            row["memo"] = "";
            row["BomRec"] = GetBomRec();
            GaD.Rows.InsertAt(row, index);
            GaD.AcceptChanges();
        }

        void CkeckMny()
        {
            decimal d = 0, q = 0;
            decimal totle = 0;
            for (int i = 0; i < GaD.Rows.Count; i++)
            {
                q = GaD.Rows[i]["qty"].ToDecimal();
                d = GaD.Rows[i]["costb"].ToDecimal();
                GaD.Rows[i]["mnyb"] = (q * d).ToDecimal("f" + Common.M);
                totle += GaD.Rows[i]["mnyb"].ToDecimal();
                dataGridViewT1.InvalidateRow(i);
            }
            TotMnyB.Text = totle.ToString("f" + Common.M);
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var gano = jGarner.Top();
            writeToTxt(gano);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var gano = jGarner.Prior();
            writeToTxt(gano);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var gano = jGarner.Next();
            writeToTxt(gano);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var gano = jGarner.Bottom();
            writeToTxt(gano);
        }

        void ThisFormState()
        {
            #region//新增時,各種歸零,然後設定某些預設值
            if (this.FormState == FormEditState.Append)
            {
                this.GaNo.Clear();
                GaD.Clear();
                loadSaleBom();

                this.Memo1 = "";
                this.BomRec = 0;

                this.序號.ReadOnly = true;
                this.金額.ReadOnly = true;
                this.包裝數量.ReadOnly = true;
                this.產品組成.ReadOnly = true;
                this.訂單編號.ReadOnly = true;

                StNoO.Clear();
                StNameO.Clear();

                StNoI.Text = Common.User_StkNo;
                jGarner.Validate<JBS.JS.Stkroom>(StNoI.Text, row =>
                {
                    StNoI.Text = row["stno"].ToString().Trim();
                    StNameI.Text = row["stname"].ToString();

                }, () =>
                {
                    StNoI.Text = StNameI.Text = "";
                });

                GaDate.Text = Date.GetDateTime(Common.User_DateTime, false);

                GaDate.Focus();
            }
            #endregion

            #region//複製時,值不變  ,但要設定某些預設值
            if (this.FormState == FormEditState.Duplicate)
            {
                loadSaleBom();

                this.序號.ReadOnly = true;
                this.金額.ReadOnly = true;
                this.包裝數量.ReadOnly = true;
                this.產品組成.ReadOnly = true;
                this.訂單編號.ReadOnly = true;

                GaNo.Clear();
                GaDate.Text = Date.GetDateTime(Common.User_DateTime, false);

                GaDate.Focus();
                GaDate.SelectAll();
            }
            #endregion

            #region//修改時,值不變
            if (this.FormState == FormEditState.Modify)
            {                
                loadSaleBom();
                this.序號.ReadOnly = true;
                this.金額.ReadOnly = true;
                this.包裝數量.ReadOnly = true;
                this.產品組成.ReadOnly = true;
                this.訂單編號.ReadOnly = true;

                GaDate.Focus();
                GaDate.SelectAll();
            }
            #endregion
            this.自定編號.ReadOnly = true;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            ThisFormState();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBottom_Click(null, null);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            ThisFormState();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBottom_Click(null, null);
                return;
            }
            
            if (jGarner.IsEditInCloseDay(GaDate.Text.Trim()) == false)
                return;

            if (jGarner.IsModify<JBS.JS.Garner>(GaNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jGarner.upModify1<JBS.JS.Garner>(GaNo.Text.Trim());//更新修改狀態1
                var pk = jGarner.Renew();//刷新資料
                writeToTxt(pk);
            }
            
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            ThisFormState();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBottom_Click(null, null);
                return;
            }
            if (jGarner.IsModify<JBS.JS.Garner>(GaNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jGarner.IsEditInCloseDay(GaDate.Text.Trim()) == false)
                return;
            jGarner.GetTempBomOnDeleting("garnbom", GaNo.Text.Trim(), ref tempBom);

            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    //回寫明細-訂單入庫數量
                    for (int i = 0; i < GaD.Rows.Count; i++)
                    {
                        if (GaD.Rows[i]["orid"].ToString() != "")
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("bomid", GaD.Rows[i]["orid"].ToString());
                            cmd.Parameters.AddWithValue("orno", GaD.Rows[i]["orno"].ToString());
                            cmd.Parameters.AddWithValue("tempqty", tempGaD.Rows[i]["qty"].ToString());
                            cmd.CommandText = @"
                            update orderd set 
                            qtyin = qtyin-@tempqty,
                            qtyNotInStk = qtyNotInStk+@tempqty
                            where bomid=@bomid and orno=@orno";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("gano", GaNo.Text.Trim());

                    cmd.CommandText = @"
                        delete from garner where gano=@gano;
                        delete from garnerd where gano=@gano;
                        delete from garnbom where gano=@gano;";
                    cmd.ExecuteNonQuery();

                    jGarner.刪除扣庫存(cmd, tempGaD, tempBom, "stnoi");
                    jGarner.刪除加庫存(cmd, tempGaD, tempBom, "stnoo");

                    BatchSave.進貨_Delete(dt_TempBatchProcess.Copy(), cmd, "Garnerd", GaNo.Text.Trim(), StNoI.Text.Trim());
                    DataTable 過濾組合品之dt = BatchSave.刪除相對應之組合品明細(GaD, dt_TempBatchProcess);
                    BatchSave.進退_Delete(過濾組合品之dt, cmd, "Garnerd", GaNo.Text.Trim(), StNoO.Text.Trim());
                    BatchSave.進退_Delete(dt_Bom_TempBatchProcess, cmd, "GarnerBom", GaNo.Text.Trim(), StNoO.Text.Trim());

                    tn.Commit();

                    jGarner.UpdateItemItStockQty(tempGaD, tempBom);

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
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmGarner_Print())
            {
                frm.PK = GaNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmGarnerBrow())
            { 
                frm.TSeekNo = GaNo.Text.Trim();

                frm.ShowDialog();
                var gano = frm.TResult.Trim();

                writeToTxt(gano);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (jGarner.IsEditInCloseDay(GaDate.Text) == false)
                return;
            //if (string.CompareOrdinal(StNoI.Text, StNoO.Text) == 0)
            //{
            //    MessageBox.Show("入庫倉庫與扣料倉庫不可相同", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    StNoO.Focus();
            //    return;
            //}

            //移除空行
            jGarner.RemoveEmptyRowOnSaving(dataGridViewT1, ref GaD, ref GaBom, CkeckMny);
            //上一方法刪除空值後，刪除對應該筆空值之批號
            BatchF.刪除無明細對應之批號資料(GaD, dt_BatchProcess);
            BatchF.刪除無明細對應之bom批號資料(GaD, dt_Bom_BatchProcess);


            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("入庫明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < GaD.Rows.Count; i++)
            {
                GaD.Rows[i]["StNoO"] = StNoO.Text.Trim();
                GaD.Rows[i]["StNoI"] = StNoI.Text.Trim();
            }

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                #region Append、Duplicate
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

                        var result = jGarner.GetPkNumber<JBS.JS.Garner>(cmd, GaDate.Text, ref GaNo);
                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        //表頭
                        AppendMasterOnSaving(cmd);

                        //明細
                        for (int i = 0; i < GaD.Rows.Count; i++)
                        {
                            AppendDetailOnSaving(cmd, i);

                            if (GaD.Rows[i]["orid"].ToString() != "")
                            {
                                AnsyOrderdQtyOnSaving(cmd, i);
                            }
                        }

                        AppendBomOnSaving(cmd);

                        jGarner.新增加庫存(cmd, GaD, GaBom, "stnoi");
                        jGarner.新增扣庫存(cmd, GaD, GaBom, "stnoo");
                        
                        ////批次資料
                        if (StNoO.Text.Trim().Length > 0)
                        {
                            DataTable 過濾組合品之dt = BatchSave.刪除相對應之組合品明細(GaD, dt_BatchProcess);
                            BatchSave.進退_Append(過濾組合品之dt, cmd, "Garnerd", GaNo.Text.Trim(), false, "", StNoO.Text.Trim());
                            BatchSave.進退_Append(dt_Bom_BatchProcess, cmd, "Garnerbom", GaNo.Text.Trim(), true, "", StNoO.Text.Trim());
                        }

                        if (StNoI.Text.Trim().Length > 0)
                        {
                            BatchSave.進貨_Append(dt_BatchProcess, cmd, "Garnerd", GaNo.Text.Trim(), false, "", StNoI.Text.Trim());
                        }


                        tn.Commit();

                        jGarner.Save(GaNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jGarner.UpdateItemItStockQty(GaD, GaBom);
                        });
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    using (var frm = new FrmGarner_Print())
                    {
                        frm.PK = jGarner.GetCurrent();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                btnAppend_Click(null, null);


            #endregion
            }

            if (this.FormState == FormEditState.Modify)
            {
                #region Modify
                if (jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
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

                        //刪除表頭 表頭
                        UpdateMasterOnSaving(cmd);

                        //刪除明細
                        DelteDetail(cmd);

                        //明細
                        for (int i = 0; i < GaD.Rows.Count; i++)
                        {
                            UpdateDetail(cmd, i);

                            if (GaD.Rows[i]["orid"].ToString() != "")
                            {
                                updateOrderdQtyOnSaving(cmd, i);
                            }
                        }

                        UpdateBom(cmd);

                        jGarner.刪除扣庫存(cmd, tempGaD, tempBom, "stnoi");
                        jGarner.刪除加庫存(cmd, tempGaD, tempBom, "stnoo");

                        jGarner.新增加庫存(cmd, GaD, GaBom, "stnoi");
                        jGarner.新增扣庫存(cmd, GaD, GaBom, "stnoo");
                        
                        //////批次資料
                        BatchSave.進貨_Modify(dt_TempBatchProcess.Copy(), dt_BatchProcess.Copy(), cmd, "Garnerd", GaNo.Text.Trim(), false, "", StNoI.Text.Trim(), old_StNoI);
                        DataTable 過濾組合品之dt = BatchSave.刪除相對應之組合品明細(GaD, dt_BatchProcess);
                        DataTable 過濾組合品之dt_2 = BatchSave.刪除相對應之組合品明細(GaD, dt_TempBatchProcess);
                        BatchSave.進退_Modify(過濾組合品之dt_2, 過濾組合品之dt, cmd, "Garnerd", GaNo.Text.Trim(), false, "", StNoO.Text.Trim(), old_StNoO);
                        BatchSave.進退_Modify(dt_Bom_TempBatchProcess, dt_Bom_BatchProcess, cmd, "GarnerBom", GaNo.Text.Trim(), true, "", StNoO.Text.Trim(), old_StNoO);
               
                        tn.Commit();

                        jGarner.Save(GaNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jGarner.UpdateItemItStockQty(tempGaD, tempBom, GaD, GaBom);
                        });

                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    using (var frm = new FrmGarner_Print())
                    {
                        frm.PK = jGarner.GetCurrent();
                        frm.ShowDialog();
                    }
                }
                jGarner.upModify0<JBS.JS.Garner>(GaNo.Text.Trim());//改回0為無修改狀態
                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                ThisFormState();


                #endregion
            }
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("gano", GaNo.Text);
            cmd.Parameters.AddWithValue("gadate", Date.ToTWDate(GaDate.Text));
            cmd.Parameters.AddWithValue("gadate1", Date.ToUSDate(GaDate.Text));
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text);
            cmd.Parameters.AddWithValue("stnameo", StNameO.Text);
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text);
            cmd.Parameters.AddWithValue("stnamei", StNameI.Text);
            cmd.Parameters.AddWithValue("orno", OrNo.Text);
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("totmnyb", TotMnyB.Text);
            cmd.Parameters.AddWithValue("gamemo", GaMemo.Text);
            cmd.Parameters.AddWithValue("gamemo1", Memo1);
            cmd.Parameters.AddWithValue("bracket", "入庫+");
            cmd.Parameters.AddWithValue("recordno", GaD.Rows.Count);
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);

            cmd.CommandText = "insert into garner ("
                + "gano,gadate,gadate1,stnoo,stnameo,stnoi,stnamei,"
                + "orno,emno,emname,totmnyb,"
                + "gamemo,gamemo1,bracket,recordno,appdate,edtdate,appscno,edtscno ) values "
                + " (@gano,@gadate,@gadate1,@stnoo,@stnameo,@stnoi,@stnamei"
                + ",@orno,@emno,@emname,@totmnyb"
                + ",@gamemo,@gamemo1,@bracket,@recordno,@appdate,@edtdate,@appscno,@edtscno) ";

            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("gano", GaNo.Text);
            cmd.Parameters.AddWithValue("gadate", Date.ToTWDate(GaDate.Text));
            cmd.Parameters.AddWithValue("gadate1", Date.ToUSDate(GaDate.Text));
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text);
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text);
            cmd.Parameters.AddWithValue("orno", GaD.Rows[i]["orno"].ToString());
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("itno", GaD.Rows[i]["itno"].ToString());
            cmd.Parameters.AddWithValue("itname", GaD.Rows[i]["itname"].ToString());
            cmd.Parameters.AddWithValue("ittrait", GaD.Rows[i]["ittrait"].ToString());
            cmd.Parameters.AddWithValue("itunit", GaD.Rows[i]["itunit"].ToString());
            cmd.Parameters.AddWithValue("itpkgqty", GaD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qty", GaD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("costb", GaD.Rows[i]["costb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("mnyb", GaD.Rows[i]["mnyb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("memo", GaD.Rows[i]["memo"].ToString());
            cmd.Parameters.AddWithValue("bomid", GaNo.Text.ToString().Trim() + GaD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", GaD.Rows[i]["BomRec"].ToString());
            cmd.Parameters.AddWithValue("recordno", (i + 1));
            cmd.Parameters.AddWithValue("bracket", "入庫+");
            cmd.Parameters.AddWithValue("itdesp1", GaD.Rows[i]["itdesp1"].ToString());
            cmd.Parameters.AddWithValue("itdesp2", GaD.Rows[i]["itdesp2"].ToString());
            cmd.Parameters.AddWithValue("itdesp3", GaD.Rows[i]["itdesp3"].ToString());
            cmd.Parameters.AddWithValue("itdesp4", GaD.Rows[i]["itdesp4"].ToString());
            cmd.Parameters.AddWithValue("itdesp5", GaD.Rows[i]["itdesp5"].ToString());
            cmd.Parameters.AddWithValue("itdesp6", GaD.Rows[i]["itdesp6"].ToString());
            cmd.Parameters.AddWithValue("itdesp7", GaD.Rows[i]["itdesp7"].ToString());
            cmd.Parameters.AddWithValue("itdesp8", GaD.Rows[i]["itdesp8"].ToString());
            cmd.Parameters.AddWithValue("itdesp9", GaD.Rows[i]["itdesp9"].ToString());
            cmd.Parameters.AddWithValue("itdesp10", GaD.Rows[i]["itdesp10"].ToString());
            cmd.Parameters.AddWithValue("orid", GaD.Rows[i]["orid"].ToString());

            cmd.CommandText = "insert into garnerd ("
                + "gano,gadate,gadate1,stnoi,stnoo,orno,emno,itno,itname,"
                + "ittrait,itunit,itpkgqty,qty,costb,mnyb,memo,bomid,bomrec,"
                + "recordno,bracket,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5,itdesp6,itdesp7,itdesp8,"
                + "itdesp9,itdesp10,orid) values "
                + " (@gano,@gadate,@gadate1,@stnoi,@stnoo,@orno,@emno,@itno,@itname,"
                + "@ittrait,@itunit,@itpkgqty,@qty,@costb,@mnyb,@memo,@bomid,@bomrec,"
                + "@recordno,@bracket,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5,@itdesp6,@itdesp7,@itdesp8,"
                + "@itdesp9,@itdesp10,@orid) ";

            cmd.ExecuteNonQuery();
        }
        private void AnsyOrderdQtyOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qtyin", GaD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("qtyNotInStk", GaD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("bomid", GaD.Rows[i]["orid"].ToString());
            cmd.Parameters.AddWithValue("orno", GaD.Rows[i]["orno"].ToString());
            cmd.CommandText = @"
                update orderd set 
                qtyin = qtyin+@qtyin,
                qtyNotInStk = qtyNotInStk-@qtyNotInStk
                where bomid=@bomid and orno=@orno";
            cmd.ExecuteNonQuery();
        }

        private void updateOrderdQtyOnSaving(SqlCommand cmd, int i)//0712改
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("qty", GaD.Rows[i]["qty"].ToString());
            cmd.Parameters.AddWithValue("bomid", GaD.Rows[i]["orid"].ToString());
            cmd.Parameters.AddWithValue("orno", GaD.Rows[i]["orno"].ToString());
            cmd.Parameters.AddWithValue("tempqty", tempGaD.Rows[i]["qty"].ToString());
            cmd.CommandText = @"
                update orderd set 
                qtyin = qtyin+@qty-@tempqty,
                qtyNotInStk = qtyNotInStk-@qty+@tempqty
                where bomid=@bomid and orno=@orno";
            cmd.ExecuteNonQuery();
        }

        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < GaBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("gano", GaNo.Text);
                cmd.Parameters.AddWithValue("bomid", GaNo.Text + GaBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", GaBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", GaBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", GaBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", GaBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", GaBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", GaBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", GaBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", GaBom.Rows[i]["itrec"].ToDecimal());
                cmd.Parameters.AddWithValue("itprice", GaBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", 1);
                cmd.Parameters.AddWithValue("itmny", GaBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", GaBom.Rows[i]["itnote"]);
                cmd.CommandText = "insert into garnbom ("
                    + "gano,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                    + "itprice,itprs,itmny,itnote) values "
                    + " (@gano,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@"
                    + "itprice,@itprs,@itmny,@itnote) ";

                cmd.ExecuteNonQuery();
            }
        }
        private void UpdateMasterOnSaving(SqlCommand cmd)
        {
            //刪除表頭
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("gano", GaNo.Text.Trim());
            cmd.CommandText = "delete from garner where gano=@gano";
            cmd.ExecuteNonQuery();

            //表頭
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("gano", GaNo.Text);
            cmd.Parameters.AddWithValue("gadate", Date.ToTWDate(GaDate.Text));
            cmd.Parameters.AddWithValue("gadate1", Date.ToUSDate(GaDate.Text));
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text);
            cmd.Parameters.AddWithValue("stnameo", StNameO.Text);
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text);
            cmd.Parameters.AddWithValue("stnamei", StNameI.Text);
            cmd.Parameters.AddWithValue("orno", OrNo.Text);
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("totmnyb", TotMnyB.Text);
            cmd.Parameters.AddWithValue("gamemo", GaMemo.Text);
            cmd.Parameters.AddWithValue("gamemo1", Memo1);
            cmd.Parameters.AddWithValue("bracket", "入庫+");
            cmd.Parameters.AddWithValue("recordno", GaD.Rows.Count);
            cmd.Parameters.AddWithValue("appdate", jGarner.keyMan.AppendTime);
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", jGarner.keyMan.AppendMan);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);

            cmd.CommandText = "insert into garner ("
                + "gano,gadate,gadate1,stnoo,stnameo,stnoi,stnamei,"
                + "orno,emno,emname,totmnyb,"
                + "gamemo,gamemo1,bracket,recordno,appdate,edtdate,appscno,edtscno ) values "
                + " (@gano,@gadate,@gadate1,@stnoo,@stnameo,@stnoi,@stnamei"
                + ",@orno,@emno,@emname,@totmnyb"
                + ",@gamemo,@gamemo1,@bracket,@recordno,@appdate,@edtdate,@appscno,@edtscno) ";

            cmd.ExecuteNonQuery();
        }
        private void DelteDetail(SqlCommand cmd)
        {
            //刪除明細GaNo
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("gano", GaNo.Text.Trim());
            cmd.CommandText = "delete from garnerd where gano=@gano";
            cmd.ExecuteNonQuery();
        }
        private void UpdateDetail(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("gano", GaNo.Text);
            cmd.Parameters.AddWithValue("gadate", Date.ToTWDate(GaDate.Text));
            cmd.Parameters.AddWithValue("gadate1", Date.ToUSDate(GaDate.Text));
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text);
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text);
            cmd.Parameters.AddWithValue("orno", GaD.Rows[i]["orno"].ToString());
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("itno", GaD.Rows[i]["itno"].ToString());
            cmd.Parameters.AddWithValue("itname", GaD.Rows[i]["itname"].ToString());
            cmd.Parameters.AddWithValue("ittrait", GaD.Rows[i]["ittrait"].ToString());
            cmd.Parameters.AddWithValue("itunit", GaD.Rows[i]["itunit"].ToString());
            cmd.Parameters.AddWithValue("itpkgqty", GaD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qty", GaD.Rows[i]["qty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("costb", GaD.Rows[i]["costb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("mnyb", GaD.Rows[i]["mnyb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("memo", GaD.Rows[i]["memo"].ToString());
            cmd.Parameters.AddWithValue("bomid", GaNo.Text.ToString().Trim() + GaD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", GaD.Rows[i]["BomRec"].ToString());
            cmd.Parameters.AddWithValue("recordno", (i + 1));
            cmd.Parameters.AddWithValue("bracket", "入庫+");
            cmd.Parameters.AddWithValue("itdesp1", GaD.Rows[i]["itdesp1"].ToString());
            cmd.Parameters.AddWithValue("itdesp2", GaD.Rows[i]["itdesp2"].ToString());
            cmd.Parameters.AddWithValue("itdesp3", GaD.Rows[i]["itdesp3"].ToString());
            cmd.Parameters.AddWithValue("itdesp4", GaD.Rows[i]["itdesp4"].ToString());
            cmd.Parameters.AddWithValue("itdesp5", GaD.Rows[i]["itdesp5"].ToString());
            cmd.Parameters.AddWithValue("itdesp6", GaD.Rows[i]["itdesp6"].ToString());
            cmd.Parameters.AddWithValue("itdesp7", GaD.Rows[i]["itdesp7"].ToString());
            cmd.Parameters.AddWithValue("itdesp8", GaD.Rows[i]["itdesp8"].ToString());
            cmd.Parameters.AddWithValue("itdesp9", GaD.Rows[i]["itdesp9"].ToString());
            cmd.Parameters.AddWithValue("itdesp10", GaD.Rows[i]["itdesp10"].ToString());
            cmd.Parameters.AddWithValue("orid", GaD.Rows[i]["orid"].ToString());

            cmd.CommandText = "insert into garnerd ("
                + "gano,gadate,gadate1,stnoi,stnoo,orno,emno,itno,itname,"
                + "ittrait,itunit,itpkgqty,qty,costb,mnyb,memo,bomid,bomrec,"
                + "recordno,bracket,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5,itdesp6,itdesp7,itdesp8,"
                + "itdesp9,itdesp10,orid) values "
                + " (@gano,@gadate,@gadate1,@stnoi,@stnoo,@orno,@emno,@itno,@itname,"
                + "@ittrait,@itunit,@itpkgqty,@qty,@costb,@mnyb,@memo,@bomid,@bomrec,"
                + "@recordno,@bracket,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5,@itdesp6,@itdesp7,@itdesp8,"
                + "@itdesp9,@itdesp10,@orid) ";

            cmd.ExecuteNonQuery();
        }
        private void UpdateBom(SqlCommand cmd)
        {
            //
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("gano", GaNo.Text.Trim());
            cmd.CommandText = "delete from garnbom where gano=@gano";
            cmd.ExecuteNonQuery();

            //
            for (int i = 0; i < GaBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("gano", GaNo.Text);
                cmd.Parameters.AddWithValue("bomid", GaNo.Text + GaBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", GaBom.Rows[i]["BomRec"].ToString());
                cmd.Parameters.AddWithValue("itno", GaBom.Rows[i]["itno"].ToString());
                cmd.Parameters.AddWithValue("itname", GaBom.Rows[i]["itname"].ToString());
                cmd.Parameters.AddWithValue("itunit", GaBom.Rows[i]["itunit"].ToString());
                cmd.Parameters.AddWithValue("itqty", GaBom.Rows[i]["itqty"].ToString());
                cmd.Parameters.AddWithValue("itpareprs", GaBom.Rows[i]["itpareprs"].ToString());
                cmd.Parameters.AddWithValue("itpkgqty", GaBom.Rows[i]["itpkgqty"].ToString());
                cmd.Parameters.AddWithValue("itrec", GaBom.Rows[i]["itrec"].ToDecimal());
                cmd.Parameters.AddWithValue("itprice", GaBom.Rows[i]["itprice"].ToString());
                cmd.Parameters.AddWithValue("itprs", "1");
                cmd.Parameters.AddWithValue("itmny", GaBom.Rows[i]["itmny"].ToString());
                cmd.Parameters.AddWithValue("itnote", GaBom.Rows[i]["itnote"].ToString());
                cmd.CommandText = "insert into garnbom ("
                    + "gano,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                    + "itprice,itprs,itmny,itnote) values "
                    + " (@gano,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@"
                    + "itprice,@itprs,@itmny,@itnote) ";

                cmd.ExecuteNonQuery();

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tClear();
            var gano = jGarner.Cancel();
            writeToTxt(gano);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            jGarner.upModify0<JBS.JS.Garner>(GaNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            tClear();
            this.Dispose();
        }

        void tClear()
        {
            GaD.Clear();
            tempGaD.Clear();
            GaBom.Clear();
            tempBom.Clear();
        }

        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                dataGridViewT1["序號", e.RowIndex].Value = (e.RowIndex + 1).ToString();
            }
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                AddRow();
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
                int index = dataGridViewT1.SelectedRows[0].Index;
                var rec = GaD.Rows[index]["BomRec"].ToString();
                jGarner.RemoveBom(rec, ref GaBom);

                GaD.Rows.RemoveAt(index);
                GaD.AcceptChanges();

                //刪除批次異動資訊
                BatchF.刪除批次異動(dt_BatchProcess, rec);
                BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", index];
                    dataGridViewT1.Rows[index].Selected = true;
                }
            }
            dataGridViewT1.Focus();
        }

        private void gridInsert_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridAppend.Focus();
                if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    AddRow(index);
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells["產品編號"];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridBomD.Focus();
                if (dataGridViewT1.SelectedRows[0].Cells["產品組成"].Value.ToString() != "組裝品")
                {
                    MessageBox.Show("只有組合品與組裝品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.Focus();
                    return;
                }
                string rec = dataGridViewT1.SelectedRows[0].Cells["結構編號"].Value.ToString();
                DataTable table = GaBom.Clone();

                for (int i = 0; i < GaBom.Rows.Count; i++)
                {
                    if (GaBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                    {
                        table.ImportRow(GaBom.Rows[i]);
                        GaBom.Rows.RemoveAt(i--);
                    }
                }

                table.AcceptChanges();
                GaBom.AcceptChanges();

                using (var frm = new FrmAdjust_Bom())
                { 
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.table = table.Copy();
                    frm.pkey = rec;
                    frm.grid = dataGridViewT1;
                    frm.FormName = "GarnerBom";
                    #region 批次資料傳遞

                    int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
                    //勾稽明細
                    string BomRec = GaD.Rows[Index_gv]["BomRec"].ToString();
                    DataTable temp = dt_Bom_BatchProcess.Clone();
                    for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
                    {
                        if (dt_Bom_BatchProcess.Rows[i]["BomRec"].ToString() == BomRec)
                        {
                            //dt_Bom_BatchProcess.Rows[i]["BomRec"] = BomRec;
                            temp.ImportRow(dt_Bom_BatchProcess.Rows[i]);
                        }
                    }

                    frm.上層Row = GaD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                    frm.上層StnoO = StNoO.Text.Trim();
                    frm.dt_Bom_BatchProcess = temp;
                    frm.FormName = this.Name;
                    #endregion
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            #region 批次資料傳遞
                            //先刪除
                            for (int i = dt_Bom_BatchProcess.Rows.Count - 1; i >= 0; i--)
                            {
                                if (dt_Bom_BatchProcess.Rows[i]["BomRec"].ToString() == BomRec)
                                    dt_Bom_BatchProcess.Rows.RemoveAt(i);
                            }
                            //後加入
                            dt_Bom_BatchProcess.Merge(frm.dt_Bom_BatchProcess);
                            #endregion
                            if (frm.CallBack.ToString() == "Money")
                            {
                                dataGridViewT1.SelectedRows[0].Cells["成本"].Value = frm.Money;
                                dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                dataGridViewT1.Focus();
                                GaBom.Merge(frm.table);
                                GaBom.AcceptChanges();
                                CkeckMny();
                            }
                            else
                            {
                                GaBom.Merge(frm.table);
                                GaBom.AcceptChanges();
                            }
                            break;
                        case DialogResult.Cancel:
                            GaBom.Merge(table);
                            GaBom.AcceptChanges();
                            break;
                    }
                }
                dataGridViewT1.Focus();
            }
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

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            gridItDesp.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            using (var frm = new FrmDesp(true, FormStyle.Mini))
            {
                frm.dr = GaD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
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
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridBshopHistory_Click(object sender, EventArgs e)
        {
            gridBshopHistory.Focus();

            if (jGarner.EnableBShopPrice() == false)
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
            var itno = GaD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void GaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            jGarner.DateValidate(sender, e);
        }

        private void OrNo_DoubleClick(object sender, EventArgs e)
        {
            if (OrNo.ReadOnly) return;
            OrNo.CausesValidation = false;
            using (var frm = new FrmOrderToGarner())
            { 
                frm.TSeekNo = OrNo.Text.Trim();
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        jGarner.RemoveEmptyRowOnSaving(dataGridViewT1, ref GaD, ref GaBom, CkeckMny);

                        if (frm.MasterRow == null)
                            return;
                        Memo1 = frm.MasterRow["ormemo1"].ToString();
                        //明細部分&組件
                        DataRow row1, row2;
                        try
                        {
                            for (int i = 0; i < frm.dtDetail.Rows.Count; i++)
                            {
                                if (frm.dtDetail.Rows[i]["ittrait"].ToString() == "1")
                                    continue;

                                row1 = GaD.NewRow();
                                row2 = frm.dtDetail.Rows[i];
                                if (row2["ittrait"].ToString() == "2")
                                {
                                    row1["ittrait"] = row2["ittrait"].ToString();
                                    row1["產品組成"] = "組裝品";
                                }
                                else
                                {
                                    row1["ittrait"] = row2["ittrait"].ToString();
                                    row1["產品組成"] = "單一商品";
                                }
                                row1["orid"] = row2["bomid"].ToString();
                                row1["itno"] = row2["itno"].ToString();
                                row1["itname"] = row2["itname"].ToString();
                                row1["itunit"] = row2["itunit"].ToString();
                                row1["qty"] = row2["qty"].ToDecimal("f" + Common.Q);
                                row1["costb"] = 最近一次_單一組裝(row2["itno"].ToString(), row2["itunit"].ToString(), row2["itpkgqty"].ToDecimal(), 1);
                                row1["itpkgqty"] = row2["itpkgqty"].ToString();
                                row1["orno"] = frm.OrNo;
                                row1["memo"] = row2["memo"].ToString();
                                for (int j = 1; j <= 10; j++)
                                {
                                    row1["itdesp" + j] = row2["itdesp" + j].ToString();
                                }
                                row1["BomRec"] = GetBomRec();
                                GaD.Rows.InsertAt(row1, GaD.Rows.Count);
                                GaD.AcceptChanges();

                                //組件部分
                                if (row2["ittrait"].ToString().Trim() == "3")
                                    continue;

                                var bomid = row2["bomid"].ToString().Trim();
                                var rec = row1["BomRec"].ToString().Trim();

                                jGarner.GetTBom<JBS.JS.Order>(bomid, rec, ref GaBom);
                                dataGridViewT1.InvalidateRow(i);

                            }
                            CkeckMny();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        break;
                }
                OrNo.Text = "";
            }
        }

        private void OrNo_Validating(object sender, CancelEventArgs e)
        {
            if (OrNo.ReadOnly) return;
            if (OrNo.Text.Trim() == "") return;
            using (var frm = new FrmOrderToGarner())
            { 
                frm.TSeekNo = OrNo.Text.Trim();
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        jGarner.RemoveEmptyRowOnSaving(dataGridViewT1, ref GaD, ref GaBom, CkeckMny);

                        if (frm.MasterRow == null)
                            return;

                        Memo1 = frm.MasterRow["ormemo1"].ToString();
                        //明細部分&組件
                        DataRow row1, row2;
                        try
                        {
                            for (int i = 0; i < frm.dtDetail.Rows.Count; i++)
                            {
                                if (frm.dtDetail.Rows[i]["ittrait"].ToString() == "1") continue;
                                row1 = GaD.NewRow();
                                row2 = frm.dtDetail.Rows[i];
                                if (row2["ittrait"].ToString() == "2")
                                {
                                    row1["ittrait"] = row2["ittrait"].ToString();
                                    row1["產品組成"] = "組裝品";
                                }
                                else
                                {
                                    row1["ittrait"] = row2["ittrait"].ToString();
                                    row1["產品組成"] = "單一商品";
                                }
                                row1["orid"] = row2["bomid"].ToString();
                                row1["itno"] = row2["itno"].ToString();
                                row1["itname"] = row2["itname"].ToString();
                                row1["itunit"] = row2["itunit"].ToString();
                                row1["qty"] = row2["qty"].ToDecimal("f" + Common.Q);
                                row1["costb"] = 最近一次_單一組裝(row2["itno"].ToString(), row2["itunit"].ToString(), row2["itpkgqty"].ToDecimal(), 1);
                                row1["costb"] = 0;
                                row1["mnyb"] = 0;
                                row1["itpkgqty"] = row2["itpkgqty"].ToString();
                                row1["orno"] = frm.OrNo;
                                row1["memo"] = row2["memo"].ToString();
                                for (int j = 1; j <= 10; j++)
                                {
                                    row1["itdesp" + j] = row2["itdesp" + j].ToString();
                                }
                                row1["BomRec"] = GetBomRec();
                                GaD.Rows.InsertAt(row1, GaD.Rows.Count);
                                GaD.AcceptChanges();

                                //組件部分
                                if (row2["ittrait"].ToString().Trim() == "3")
                                    continue;

                                var bomid = row2["bomid"].ToString().Trim();
                                var rec = row1["BomRec"].ToString().Trim();

                                jGarner.GetTBom<JBS.JS.Order>(bomid, rec, ref GaBom);
                                dataGridViewT1.InvalidateRow(i);

                            }
                            CkeckMny();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        break;
                }
                OrNo.Text = "";
            }
        }

        private void StNoI_DoubleClick(object sender, EventArgs e)
        { 
            if (sender.Equals(StNoI))
            {
                jGarner.Open<JBS.JS.Stkroom>(sender, reader =>
                {
                    StNoI.Text = reader["StNo"].ToString().Trim();
                    StNameI.Text = reader["StName"].ToString().Trim();
                });
            }
            else
            {
                jGarner.Open<JBS.JS.Stkroom>(sender, reader =>
                {
                    StNoO.Text = reader["StNo"].ToString().Trim();
                    StNameO.Text = reader["StName"].ToString().Trim();
                });
            }

        }

        private void StNoI_Validating(object sender, CancelEventArgs e)
        {
            if (StNoI.ReadOnly || StNoO.ReadOnly || btnCancel.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Name == "StNoO")
            {
                if (Common.keyData != Keys.Up)
                {
                    if (((TextBox)sender).Name == "StNoO")
                        if (dataGridViewT1.Rows.Count == 0)
                            if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
                }
                if (tb.Text == "")
                {
                    StNoO.Text = StNameO.Text = "";
                    return;
                }
            }
            else
            {
                if (tb.Text == "")
                {
                    tb.Text = StNameI.Text = "";
                    MessageBox.Show("倉庫編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }

            if (sender.Equals(StNoI))
            {
                jGarner.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
                {
                    StNoI.Text = reader["StNo"].ToString().Trim();
                    StNameI.Text = reader["StName"].ToString().Trim();
                });
            }
            else
            {
                jGarner.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
                {
                    StNoO.Text = reader["StNo"].ToString().Trim();
                    StNameO.Text = reader["StName"].ToString().Trim();
                });
            }
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jGarner.Open<JBS.JS.Empl>(sender, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();

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
                return;
            }

            jGarner.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();

            }, true);
        }

        private void GaMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(GaMemo, 20);
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
                if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name.ToString() == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name.ToString() == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jGarner.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GaNo.ReadOnly) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                #region 產品編號
                if (dataGridViewT1["訂單編號", e.RowIndex].EditedFormattedValue.ToString() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jGarner.DataGridViewOpen<JBS.JS.Item>(sender, e, GaD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {
                #region 單位
                if (dataGridViewT1["訂單編號", e.RowIndex].EditedFormattedValue.ToString() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    string itno = GaD.Rows[e.RowIndex]["itno"].ToString().Trim();
                    decimal qty = 1;
                    if (GaD.Rows[e.RowIndex]["qty"].ToDecimal() > 1)
                        qty = GaD.Rows[e.RowIndex]["qty"].ToDecimal();

                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", itno);
                    cmd.CommandText = "select itunit,itunitp,itbuyprip,itbuypri,itpkgqty from item where itno=@itno";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == false)
                            return;

                        if (unit == reader["itunit"].ToString().Trim() && reader["itunitp"].ToString().Trim() != "")
                        {
                            var unitp = reader["itunitp"].ToString().Trim();
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = unitp;
                            GaD.Rows[e.RowIndex]["itunit"] = unitp;

                            var itpkgqty = reader["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            GaD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;

                            GaD.Rows[e.RowIndex]["costb"] = 最近一次_單一組裝(itno, unitp, itpkgqty, 1);
                        }
                        else
                        {
                            unit = reader["itunit"].ToString().Trim();
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = unit;
                            GaD.Rows[e.RowIndex]["itunit"] = unit;
                            GaD.Rows[e.RowIndex]["itpkgqty"] = 1;
                            GaD.Rows[e.RowIndex]["costb"] = 最近一次_單一組裝(itno, unit, 1, 1);
                        }
                    }
                }
                CkeckMny();
                dataGridViewT1.InvalidateRow(e.RowIndex);
                if (dataGridViewT1.EditingControl != null)
                    ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.說明))
            {
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        GaD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.入庫數量))
            {
                #region 入庫數量
                GaD.Rows[e.RowIndex]["qty"] = dataGridViewT1["入庫數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                //BatchF.同步批次異動數量(dt_BatchProcess, GaD, e.RowIndex);
                //BatchF.BOM同步批次異動數量(dt_Bom_BatchProcess, GaD, GaBom, e.RowIndex);
                CkeckMny();
                dataGridViewT1.InvalidateRow(e.RowIndex);

                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.成本))
            {
                #region 成本
                GaD.Rows[e.RowIndex]["costb"] = dataGridViewT1["成本", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.M);
                CkeckMny();
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {
                #region 單位
                var itno = GaD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                if (dataGridViewT1["訂單編號", e.RowIndex].EditedFormattedValue.ToString() != "")
                {
                    e.Cancel = true;
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GaD.Rows[e.RowIndex]["itunit"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jGarner.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        GaD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;

                        GaD.Rows[e.RowIndex]["costb"] = 最近一次_單一組裝(itno, unit, itpkgqty, 1);
                    }
                    else
                    {
                        GaD.Rows[e.RowIndex]["itpkgqty"] = 1;

                        GaD.Rows[e.RowIndex]["costb"] = 最近一次_單一組裝(itno, unit, 1, 1);
                    }
                });

                GaD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                CkeckMny();

                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                #region 產品編號
                var ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (dataGridViewT1["訂單編號", e.RowIndex].EditedFormattedValue.ToString() != "")
                {
                    if (ItNoBegin == ItNoNow)
                        return;

                    e.Cancel = true;
                    MessageBox.Show("此筆資料由訂單轉入，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GaD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (ItNoNow.Length == 0)
                {
                    GaD.Rows[e.RowIndex]["itno"] = "";
                    GaD.Rows[e.RowIndex]["itname"] = "";
                    GaD.Rows[e.RowIndex]["qty"] = 0;
                    GaD.Rows[e.RowIndex]["itunit"] = "";
                    GaD.Rows[e.RowIndex]["costb"] = 0;
                    GaD.Rows[e.RowIndex]["mnyb"] = 0;
                    GaD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    GaD.Rows[e.RowIndex]["產品組成"] = "";
                    GaD.Rows[e.RowIndex]["orno"] = "";
                    GaD.Rows[e.RowIndex]["orid"] = "";
                    GaD.Rows[e.RowIndex]["memo"] = "";

                    var rec = GaD.Rows[e.RowIndex]["BomRec"].ToString();
                    jGarner.RemoveBom(rec, ref GaBom);

                    //刪除批次異動資訊
                    BatchF.刪除批次異動(dt_BatchProcess, rec);
                    BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    CkeckMny();
                    return;
                }

                if (ItNoNow == ItNoBegin)
                    return;

                if (ItNoNow != ItNoBegin)
                {
                    if (ItNoNow == UdfNoBegin)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = ItNoBegin;

                        GaD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)
                {
                    jGarner.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, GaD, row =>
                    {
                        e.Cancel = !FillItem(row, e.RowIndex);
                    });
                }
                #endregion
            }
        }

        bool FillItem(SqlDataReader row, int index)
        {
            var itno = row["itno"].ToString().Trim();

            var ittrait = row["ittrait"].ToString().Trim();
            if (ittrait == "1")
            {
                MessageBox.Show("組合品無法入庫", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (ittrait == "2")
            {
                GaD.Rows[index]["產品組成"] = "組裝品";
                GaD.Rows[index]["ittrait"] = "2";
            }
            else
            {
                GaD.Rows[index]["產品組成"] = "單一商品";
                GaD.Rows[index]["ittrait"] = "3";
            }

            this.ItNoBegin = itno;

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;

            GaD.Rows[index]["ItNo"] = itno;
            GaD.Rows[index]["ItName"] = row["ItName"].ToString();
            GaD.Rows[index]["ItNoUdf"] = row["ItNoUdf"].ToString();
            if (row["itsalunit"].ToDecimal() == 2)//銷貨常用單位 單位
            {
                var unit = row["itunit"].ToString().Trim();
                GaD.Rows[index]["itunit"] = unit;
                GaD.Rows[index]["itpkgqty"] = 1;

                GaD.Rows[index]["costb"] = 最近一次_單一組裝(itno, unit, 1, 1);
            }
            else
            {
                var unitp = row["itunitp"].ToString().Trim();
                GaD.Rows[index]["itunit"] = unitp;

                var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                GaD.Rows[index]["itpkgqty"] = itpkgqty;

                GaD.Rows[index]["costb"] = 最近一次_單一組裝(itno, unitp, itpkgqty, 1);
            }

            GaD.Rows[index]["orno"] = "";
            GaD.Rows[index]["orid"] = "";

            for (int i = 1; i < 10; i++)
            {
                GaD.Rows[index]["itdesp" + i] = row["itdesp" + i].ToString();
            }

            //BOM
            var rec = GaD.Rows[index]["BomRec"].ToString().Trim();
            jGarner.RemoveBom(rec, ref GaBom);

            if (ittrait == "2")
            {
                jGarner.GetItemBom(itno, rec, ref GaBom);
            }
            CkeckMny();

            BatchF.刪除批次異動(dt_BatchProcess, rec);
            BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);

            return true;
        }

        private void GaNo_Enter(object sender, EventArgs e)
        {
            GaNo.Tag = GaNo.Text.Trim();
        }

        private void GaNo_DoubleClick(object sender, EventArgs e)
        {
            if (GaNo.ReadOnly)
                return;

            using (var frm = new FrmGarner_Print_GaNo())
            {
                frm.TSeekNo = GaNo.Text.Trim();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GaNo.Text = frm.TResult;
                }
            }
        }

        private void GaNo_Validating(object sender, CancelEventArgs e)
        {
            if (GaNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (GaNo.Text.Length > 0 && GaNo.Text.Trim() == "")
            {
                e.Cancel = true;
                GaNo.Text = "";
                GaNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim()))
                {
                    if (GaNo.Text.Trim() == ((GaNo.Tag) ?? "").ToString())
                        return;

                    writeToTxt(GaNo.Text.Trim());
                    loadSaleBom();
                }
                else
                {
                    e.Cancel = true;
                    GaNo_DoubleClick(GaNo, null);
                    GaNo.SelectAll();

                    var exist = jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim());
                    if (exist)
                    {
                        writeToTxt(GaNo.Text.Trim());
                        loadSaleBom();
                    }
                }
            }
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ToolTip tip = new ToolTip();
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "說明")
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

        void t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                dataGridViewT1_CellDoubleClick(dataGridViewT1, new DataGridViewCellEventArgs(dataGridViewT1.CurrentCell.ColumnIndex, dataGridViewT1.CurrentCell.RowIndex));
            }
        }

        private void gridKeyMan_Click(object sender, EventArgs e)
        {
            if (GaNo.Text.Trim() == "")
                return;

            using (var frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jGarner.keyMan.AppendMan;
                frm.ATime = jGarner.keyMan.AppendTime;
                //修改人員
                frm.EName = jGarner.keyMan.EditMan;
                frm.ETime = jGarner.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = GaNo.ReadOnly ? false : true;
                frm.memo1 = Memo1;

                if (frm.ShowDialog() == DialogResult.OK) Memo1 = frm.memo1;
            }
        }

        decimal 最近一次_單一組裝(string itno, string itunit, decimal itpkgqty, decimal qty)
        {
            decimal d = 0;
            string bunit = "";
            decimal bpkgqty = 0;
            decimal btaxprice = 0;
            decimal bavgprice = 0;

            using (DataTable dt = new DataTable())
            using (DataTable dt1 = new DataTable())
            {
                DataRow row;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("itno", itno.Trim());
                    cmd.Parameters.AddWithValue("bsdate", Date.ToTWDate(GaDate.Text));
                    cmd.CommandText = " Select top 1 * from bshopd where itno = @itno and bsdate <=@bsdate order by bsdate desc ";

                    da.Fill(dt);
                    row = dt.AsEnumerable().FirstOrDefault();


                    //沒最近一次取建檔進價
                    if (row == null)
                    {
                        cmd.CommandText = "Select top 1 * from item where itno = @itno";
                        da.Fill(dt1);
                        row = dt1.AsEnumerable().First();
                        d = row["ItBuyPri"].ToDecimal();
                        return (qty * itpkgqty * d).ToDecimal("f" + Common.M);
                    }
                }

                //進貨
                bunit = row["itunit"].ToString();
                bpkgqty = row["itpkgqty"].ToDecimal();
                btaxprice = row["realcost"].ToDecimal();
                bavgprice = 0;

                if (bpkgqty != 0)
                    bavgprice = btaxprice / bpkgqty;

                //銷貨
                if (itunit == bunit && itpkgqty == bpkgqty)
                {
                    return (qty * btaxprice).ToDecimal("f" + Common.M);
                }
                else
                {
                    return (qty * itpkgqty * bavgprice).ToDecimal("f" + Common.M);
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
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift") && gridBshopHistory.Enabled)
            {
                gridBshopHistory_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            var flag = btnAppend.Enabled;

            dataGridViewT1.ReadOnly = flag;

            flag = !flag;
            gridAppend.Enabled = flag;
            gridDelete.Enabled = flag;
            gridInsert.Enabled = flag;
            gridPicture.Enabled = flag;
            gridStock.Enabled = true;
            gridItDesp.Enabled = flag;
            gridBshopHistory.Enabled = true;
            gridBomD.Enabled = flag;
        }

        private void gridbatch_Click(object sender, EventArgs e)
        { 
            BatchF.WhenGridBadch_click(this.Name, dataGridViewT1, GaD, dt_BatchProcess, null, null, null, null, StNoO, true,btnSave.Enabled == true);
        }


    }
}
