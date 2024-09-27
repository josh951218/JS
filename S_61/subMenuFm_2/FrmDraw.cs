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
    public partial class FrmDraw : Formbase
    {
        JBS.JS.Draw jDraw;
        List<TextBoxbase> list;
        List<Button> GridButton;

        DataTable DrD = new DataTable(); //明細
        DataTable DrBom = new DataTable();//組件

        DataTable tempD = new DataTable();//用來記錄 修改&複製狀態一進來原始的明細
        DataTable tempBom = new DataTable();//用來記錄 修改狀態&複製一進來原始的組件明細

        #region 批次資料
        BatchProcess BatchF = new BatchProcess();              //批次存資料異動修改
        BatchSave BatchSave = new BatchSave();              //批次資料存檔用
        DataTable dt_BatchProcess = new DataTable();        //批次異動
        DataTable dt_TempBatchProcess = new DataTable();    //批次異動暫存檔

        DataTable dt_Bom_BatchProcess = new DataTable();       //bom批次異動
        DataTable dt_Bom_TempBatchProcess = new DataTable();   //bom批次異動暫存檔
        string old_StNoO = "", old_StNoI = "";
        #endregion

        decimal BomRec = 0;
        string ItNoBegin = "";//紀錄編輯格上一次的編號
        string UdfNoBegin = "";
        string TextBefore = "";
        string Memo1 = "";//詳細備註


        public FrmDraw()
        {
            InitializeComponent();
            this.jDraw = new JBS.JS.Draw();
            this.list = this.getEnumMember();

            pVar.SetMemoUdf(this.說明);

            GridButton = new List<Button> { gridAppend, gridDelete, gridPicture, gridInsert, gridItDesp, gridBomD  };

            this.包裝數量.Set庫存數量小數();
            this.領料數量.Set庫存數量小數();
            this.成本.Set本幣金額小數();
            this.金額.Set本幣金額小數();

            //this.成本.Visible = Common.User_ShopPrice;
            this.金額.Visible = Common.User_ShopPrice;
            TotMnyB.Visible = Common.User_ShopPrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
            dataGridViewT1.DataSource = DrD;
            
            if (Common.Sys_UsingBatch == 1)
            {
                this.gridbatch.Visible = false;
            }

        }

        private void FrmDraw_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
                DrDate.MaxLength = 7;
            else
                DrDate.MaxLength = 8;

            GridButton.ForEach(r => r.Enabled = false);

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("drno", DrNo.Text.Trim());
                cmd.CommandText = "select 產品組成=case"
                        + " when ittrait=1 then '組合品'"
                        + " when ittrait=2 then '組裝品'"
                        + " when ittrait=3 then '單一商品' end,"
                        + " ItNoUdf='',* from drawd where 1=0 ";

                dd.Fill(DrD);
                dd.Fill(tempD);
            }

            BatchF.建立結構(dt_BatchProcess);
            dt_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_BatchProcess = dt_BatchProcess.Clone();
            dt_Bom_TempBatchProcess = dt_BatchProcess.Clone();
            dataGridView1.DataSource = dt_BatchProcess;
            dataGridView2.DataSource = dt_Bom_BatchProcess;

            var pk = jDraw.Bottom();
            writeToTxt(pk);
        }

        private void FrmDraw_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string drno)
        {
            BomRec = 0;

            var result = jDraw.LoadData(drno, reader =>
            {
                DrNo.Text = reader["drno"].ToString();
                if (Common.User_DateTime == 1)
                    DrDate.Text = reader["drdate"].ToString();
                else
                    DrDate.Text = reader["drdate1"].ToString();
                StNoI.Text = reader["stnoi"].ToString();
                StNameI.Text = reader["stnamei"].ToString();
                StNoO.Text = reader["stnoo"].ToString();
                StNameO.Text = reader["stnameo"].ToString();
                EmNo.Text = reader["emno"].ToString();
                EmName.Text = reader["emname"].ToString();
                DrMemo.Text = reader["drmemo"].ToString();

                TotMnyB.Text = reader["TotMnyB"].ToDecimal().ToString("f" + Common.M);

                load_DrD();

                Memo1 = reader["drmemo1"].ToString();
                jDraw.keyMan.Set(reader);
            });

            if (!result)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                DrD.Clear();
                tempD.Clear();
                DrBom.Clear();
                tempBom.Clear();

                this.Memo1 = "";
                jDraw.keyMan.Clear();
            }

            BatchF.雙倉上下頁dt資料修改("Drawd", drno, dt_BatchProcess, dt_TempBatchProcess);
            BatchF.BOM雙倉上下頁dt資料修改("Drawd", "DrawBom", drno, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
            old_StNoI = StNoI.Text.Trim();
            old_StNoO = StNoO.Text.Trim();
        }

        void load_DrD()
        {
            DrD.Clear();
            tempD.Clear();

            string sql = "select 產品組成=case"
                        + " when ittrait=1 then '組合品'"
                        + " when ittrait=2 then '組裝品'"
                        + " when ittrait=3 then '單一商品' end,"
                        + " ItNoUdf= (select top 1 itnoudf from item where item.itno = drawd.itno),* from drawd where drno=@drno ORDER BY recordno";

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("drno", DrNo.Text.Trim());
                cmd.CommandText = sql;

                dd.Fill(DrD);
                dd.Fill(tempD);
            }
            dataGridViewT1.DataSource = DrD;
            if (DrD.Rows.Count > 0) BomRec = DrD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
        }

        void load_DrBom()
        {
            DrBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select * from drawbom where 1=0";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from drawbom where drno=@drno";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from drawbom where drno=@drno";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@drno", jDraw.GetCurrent());
                    cmd.CommandText = sql;

                    da.Fill(DrBom);
                    da.Fill(tempBom);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void SetStName()
        {
            DrDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            string sql = "select stname from stkroom where stno=@stnoo";
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("stnoo", StNoO.Text.Trim());
                        if (cmd.ExecuteScalar().ToString() != "")
                            StNameO.Text = cmd.ExecuteScalar().ToString();
                        else
                            StNameO.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void AddRow()
        {
            DataRow dr = DrD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["itunit"] = "";
            dr["qty"] = 0;
            dr["itpkgqty"] = 1;
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            dr["ittrait"] = 0;
            dr["costb"] = 0;
            dr["mnyb"] = 0;
            DrD.Rows.Add(dr);
            DrD.AcceptChanges();
        }

        void AddRow(int index)
        {
            DataRow dr = DrD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["itunit"] = "";
            dr["qty"] = 0;
            dr["itpkgqty"] = 1;
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            dr["ittrait"] = 0;
            dr["costb"] = 0;
            dr["mnyb"] = 0;
            DrD.Rows.InsertAt(dr, index);
            DrD.AcceptChanges();
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jDraw.Top();
            writeToTxt(pk);

        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jDraw.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jDraw.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jDraw.Bottom();
            writeToTxt(pk);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            DrD.Clear();
            load_DrBom();

            this.Memo1 = "";
            this.BomRec = 0;

            GridButton.ForEach(r => r.Enabled = true);
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            this.金額.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            StNoO.Text = Common.User_StkNo;
            SetStName();

            DrDate.Focus();

            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
  
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (DrD.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jDraw.IsExistDocument<JBS.JS.Draw>(DrNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            load_DrBom();

            DrNo.Text = "";
            DrDate.Text = Date.GetDateTime(Common.User_DateTime, false);

            GridButton.ForEach(r => r.Enabled = true);
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            this.金額.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess, dt_Bom_BatchProcess, dt_Bom_TempBatchProcess);
            DrDate.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (DrD.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jDraw.IsExistDocument<JBS.JS.Draw>(DrNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            if (jDraw.IsEditInCloseDay(DrDate.Text) == false)
                return;

            if (jDraw.IsModify<JBS.JS.Draw>(DrNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jDraw.upModify1<JBS.JS.Draw>(DrNo.Text.Trim());//更新修改狀態1
                var pk = jDraw.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            load_DrBom();

            GridButton.ForEach(r => r.Enabled = true);
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            this.金額.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            DrDate.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jDraw.IsExistDocument<JBS.JS.Draw>(DrNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jDraw.IsModify<JBS.JS.Draw>(DrNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jDraw.IsEditInCloseDay(DrDate.Text) == false)
                return;

           
            jDraw.GetTempBomOnDeleting("drawbom", DrNo.Text.Trim(), ref tempBom);

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
                    cmd.Parameters.AddWithValue("drno", DrNo.Text.Trim());

                    cmd.CommandText = @"
                        delete from draw    where drno=@drno;
                        delete from drawd   where drno=@drno;
                        delete from drawbom where drno=@drno; ";
                    cmd.ExecuteNonQuery();

                    this.加庫存(cmd, tempD, tempBom, "stnoo");
                    this.扣庫存(cmd, tempD, tempBom, "stnoi");

                    BatchSave.進貨_Delete(dt_TempBatchProcess.Copy(), cmd, "Drawd", DrNo.Text.Trim(),StNoI.Text.Trim());
                    BatchSave.進貨_Delete(dt_Bom_TempBatchProcess.Copy(), cmd, "DrawBom", DrNo.Text.Trim(), StNoI.Text.Trim());
                    BatchSave.進退_Delete(dt_TempBatchProcess.Copy(), cmd, "Drawd", DrNo.Text.Trim(), StNoO.Text.Trim());
                    BatchSave.進退_Delete(dt_Bom_TempBatchProcess.Copy(), cmd, "DrawBom", DrNo.Text.Trim(), StNoO.Text.Trim());

                    tn.Commit();

                    jDraw.UpdateItemItStockQty(tempD, tempBom);

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
            using (var frm = new FrmDraw_Print())
            {
                frm.PK = DrNo.Text.Trim();
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
            using (var frm = new FrmDrawBrow())
            {
                frm.TSeekNo = DrNo.Text;

                frm.ShowDialog();
                writeToTxt(frm.TResult.Trim());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (jDraw.IsEditInCloseDay(DrDate.Text) == false)
                return;

            if (StNoI.Text == StNoO.Text)
            {
                MessageBox.Show("入料與發料倉庫不可相同", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jDraw.RemoveEmptyRowOnSaving(dataGridViewT1, ref DrD, ref DrBom, CkeckMny);
            //上一方法刪除空值後，刪除對應該筆空值之批號
            BatchF.刪除無明細對應之批號資料(DrD, dt_BatchProcess);
            BatchF.刪除無明細對應之bom批號資料(DrD, dt_Bom_BatchProcess);

            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < DrD.Rows.Count; i++)
            {
                DrD.Rows[i]["stnoo"] = StNoO.Text.Trim();
                DrD.Rows[i]["stnoi"] = StNoI.Text.Trim();
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

                        var result = jDraw.GetPkNumber<JBS.JS.Draw>(cmd, DrDate.Text, ref DrNo);

                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        AppendMasterOnSaving(cmd);

                        for (int i = 0; i < DrD.Rows.Count; i++)
                        {
                            AppendDetailOnSaving(cmd, i);
                        }

                        AppendBomOnSaving(cmd);

                        this.加庫存(cmd, DrD, DrBom, "stnoi");
                        this.扣庫存(cmd, DrD, DrBom, "stnoo");
                        
                        //批次資料

                        //DataTable A = new DataTable();
                        //A = dt_BatchProcess.Copy();
                        //DataTable B = new DataTable();
                        //B = dt_Bom_BatchProcess.Copy();

                        if (StNoI.Text.Trim().Length > 0)
                        {
                            BatchSave.進貨_Append(dt_BatchProcess.Copy(), cmd, "Drawd", DrNo.Text.Trim(), false, "", StNoI.Text.Trim());
                            BatchSave.進貨_Append(dt_Bom_BatchProcess.Copy(), cmd, "Drawbom", DrNo.Text.Trim(), true, "", StNoI.Text.Trim());
                        }
                        if (StNoO.Text.Trim().Length > 0)
                        {
                            BatchSave.進退_Append(dt_BatchProcess.Copy(), cmd, "Drawd", DrNo.Text.Trim(), false, "", StNoO.Text.Trim());
                            BatchSave.進退_Append(dt_Bom_BatchProcess.Copy(), cmd, "Drawbom", DrNo.Text.Trim(), true, "", StNoO.Text.Trim());
                            //BatchSave.進退_Append(A, cmd, "Drawd", DrNo.Text.Trim(), false, "", StNoO.Text.Trim());
                            //BatchSave.進退_Append(B, cmd, "Drawbom", DrNo.Text.Trim(), true, "", StNoO.Text.Trim());
                        }
                        tn.Commit();

                        jDraw.Save(DrNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jDraw.UpdateItemItStockQty(DrD, DrBom);
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
                    using (var frm = new FrmDraw_Print())
                    {
                        frm.PK = jDraw.GetCurrent();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
                #endregion
            }

            if (this.FormState == FormEditState.Modify)
            {
                #region Modify
                if (jDraw.IsExistDocument<JBS.JS.Draw>(DrNo.Text.Trim()) == false)
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

                        //刪除主檔
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drno", DrNo.Text.Trim());
                        cmd.CommandText = "delete from draw where drno=@drno";
                        cmd.ExecuteNonQuery();

                        AppendMasterOnSaving(cmd);

                        //刪除明細
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drno", DrNo.Text.Trim());
                        cmd.CommandText = "delete from drawd where drno=@drno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < DrD.Rows.Count; i++)
                        {
                            AppendDetailOnSaving(cmd, i);
                        }

                        //刪除組件
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drno", DrNo.Text.Trim());
                        cmd.CommandText = "delete from drawbom where drno=@drno";
                        cmd.ExecuteNonQuery();

                        AppendBomOnSaving(cmd);

                        this.加庫存(cmd, tempD, tempBom, "stnoo");
                        this.扣庫存(cmd, tempD, tempBom, "stnoi");

                        this.加庫存(cmd, DrD, DrBom, "stnoi");
                        this.扣庫存(cmd, DrD, DrBom, "stnoo");
                        ////批次資料

                        //DataTable A = new DataTable();
                        //A = dt_BatchProcess.Copy();
                        //DataTable TA = new DataTable();
                        //TA = dt_TempBatchProcess.Copy();

                        //DataTable B = new DataTable();
                        //B = dt_Bom_BatchProcess.Copy();
                        //DataTable TB = new DataTable();
                        //TB = dt_Bom_TempBatchProcess.Copy();



                        BatchSave.進貨_Modify(dt_TempBatchProcess.Copy(), dt_BatchProcess.Copy(), cmd, "Drawd", DrNo.Text.Trim(), false, "", StNoI.Text.Trim(), old_StNoI);
                        BatchSave.進貨_Modify(dt_Bom_TempBatchProcess.Copy(), dt_Bom_BatchProcess.Copy(), cmd, "DrawBom", DrNo.Text.Trim(), true, "", StNoI.Text.Trim(), old_StNoI);

                        //BatchSave.進退_Modify(TA, A, cmd, "Drawd", DrNo.Text.Trim(), false, "", StNoO.Text.Trim(), old_StNoO);
                        //BatchSave.進退_Modify(TB, B, cmd, "DrawBom", DrNo.Text.Trim(), true, "", StNoO.Text.Trim(), old_StNoO); 

                        BatchSave.進退_Modify(dt_TempBatchProcess.Copy(), dt_BatchProcess.Copy(), cmd, "Drawd", DrNo.Text.Trim(), false, "", StNoO.Text.Trim(), old_StNoO);
                        BatchSave.進退_Modify(dt_Bom_TempBatchProcess.Copy(), dt_Bom_BatchProcess.Copy(), cmd, "DrawBom", DrNo.Text.Trim(), true, "", StNoO.Text.Trim(), old_StNoO); 
                        tn.Commit();

                        jDraw.Save(DrNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jDraw.UpdateItemItStockQty(tempD, tempBom, DrD, DrBom);
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
                    using (var frm = new FrmDraw_Print())
                    {
                        frm.PK = jDraw.GetCurrent();
                        frm.ShowDialog();
                    }
                }
                jDraw.upModify0<JBS.JS.Draw>(DrNo.Text.Trim());//改回0為無修改狀態

                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
                #endregion
            }
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("drno", DrNo.Text);
            cmd.Parameters.AddWithValue("drdate", Date.ToTWDate(DrDate.Text));
            cmd.Parameters.AddWithValue("drdate1", Date.ToUSDate(DrDate.Text));
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text);
            cmd.Parameters.AddWithValue("stnameo", StNameO.Text);
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text);
            cmd.Parameters.AddWithValue("stnamei", StNameI.Text);
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("drmemo", DrMemo.Text);
            cmd.Parameters.AddWithValue("bracket", "領料");
            cmd.Parameters.AddWithValue("drmemo1", Memo1);
            cmd.Parameters.AddWithValue("totmnyb", TotMnyB.Text.ToDecimal());
            cmd.Parameters.AddWithValue("recordno", DrD.Rows.Count);
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);

            cmd.CommandText = "insert into draw"
                + "(drno,drdate,drdate1,stnoo,stnameo,stnoi,stnamei"
                + ",emno,emname,drmemo,bracket,drmemo1,totmnyb,recordno,appdate,edtdate,appscno,edtscno) values "
                + " (@drno,@drdate,@drdate1,@stnoo,@stnameo,@stnoi,@stnamei"
                + ",@emno,@emname,@drmemo,@bracket,@drmemo1,@totmnyb,@recordno,@appdate,@edtdate,@appscno,@edtscno) ";
            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("drno", DrNo.Text);
            cmd.Parameters.AddWithValue("drdate", Date.ToTWDate(DrDate.Text));
            cmd.Parameters.AddWithValue("drdate1", Date.ToUSDate(DrDate.Text));
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text);
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text);
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("itno", DrD.Rows[i]["itno"]);
            cmd.Parameters.AddWithValue("itname", DrD.Rows[i]["itname"]);
            cmd.Parameters.AddWithValue("ittrait", DrD.Rows[i]["ittrait"]);
            cmd.Parameters.AddWithValue("itunit", DrD.Rows[i]["itunit"]);
            cmd.Parameters.AddWithValue("itpkgqty", DrD.Rows[i]["itpkgqty"]);
            cmd.Parameters.AddWithValue("qty", DrD.Rows[i]["qty"]);
            cmd.Parameters.AddWithValue("memo", DrD.Rows[i]["memo"]);
            cmd.Parameters.AddWithValue("bomid", DrNo.Text + DrD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", DrD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1));
            cmd.Parameters.AddWithValue("bracket", "領料");
            cmd.Parameters.AddWithValue("itdesp1", DrD.Rows[i]["itdesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", DrD.Rows[i]["itdesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", DrD.Rows[i]["itdesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", DrD.Rows[i]["itdesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", DrD.Rows[i]["itdesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", DrD.Rows[i]["itdesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", DrD.Rows[i]["itdesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", DrD.Rows[i]["itdesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", DrD.Rows[i]["itdesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", DrD.Rows[i]["itdesp10"]);
            cmd.Parameters.AddWithValue("costb", DrD.Rows[i]["costb"]);
            cmd.Parameters.AddWithValue("mnyb", DrD.Rows[i]["mnyb"]);
            cmd.Parameters.AddWithValue("stnameo", StNameO.Text);
            cmd.Parameters.AddWithValue("stnamei", StNameI.Text);
            cmd.CommandText = "insert into drawd"
                + "(drno,drdate,drdate1,stnoo,stnoi,emno,itno,itname,ittrait"
                + ",itunit,itpkgqty,qty,memo"
                + ",bomid,bomrec,recordno"
                + ",bracket"
                + ",itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                + ",itdesp6,itdesp7,itdesp8,itdesp9,itdesp10"
                + ",costb,mnyb"
                + ",stnameo,stnamei) values "
                + "(@drno,@drdate,@drdate1,@stnoo,@stnoi,@emno,@itno,@itname,@ittrait"
                + ",@itunit,@itpkgqty,@qty,@memo"
                + ",@bomid,@bomrec,@recordno"
                + ",@bracket"
                + ",@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                + ",@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10"
                + ",@costb,@mnyb"
                + ",@stnameo,@stnamei) ";

            cmd.ExecuteNonQuery();
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < DrBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("drno", DrNo.Text);
                cmd.Parameters.AddWithValue("bomid", DrNo.Text + DrBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", DrBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", DrBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", DrBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", DrBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", DrBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", DrBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", DrBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", DrBom.Rows[i]["itrec"]);
                cmd.Parameters.AddWithValue("itprice", DrBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", DrBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", DrBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", DrBom.Rows[i]["itnote"]);
                cmd.CommandText = "insert into drawbom"
                    + "(drno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty"
                    + ",itrec,itprice,itprs,itmny,itnote"
                    + ") values "
                    + "(@drno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty"
                    + ",@itrec,@itprice,@itprs,@itmny,@itnote) ";

                cmd.ExecuteNonQuery();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jDraw.Cancel();
            writeToTxt(pk);

            GridButton.ForEach(r => r.Enabled = false);
            dataGridViewT1.ReadOnly = true;

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            jDraw.upModify0<JBS.JS.Draw>(DrNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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
                var index = dataGridViewT1.SelectedRows[0].Index;
                var rec = DrD.Rows[index]["BomRec"].ToString();

                jDraw.RemoveBom(rec, ref DrBom);

                //刪除明細
                DrD.Rows.RemoveAt(index);
                DrD.AcceptChanges();

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
                frm.dr = DrD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows[0].Cells["ittrait"].EditedFormattedValue.ToString() == "3")
            {
                MessageBox.Show("只有組合品與組裝品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridViewT1.Focus();
                return;
            }

            string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString();
            DataTable table = DrBom.Clone();

            for (int i = 0; i < DrBom.Rows.Count; i++)
            {
                if (DrBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                {
                    table.ImportRow(DrBom.Rows[i]);
                    DrBom.Rows.RemoveAt(i--);
                }
            }

            table.AcceptChanges();
            DrBom.AcceptChanges();

            

            using (var frm = new FrmAdjust_Bom())
            { 
                frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                frm.table = table.Copy();
                frm.pkey = rec;
                frm.grid = dataGridViewT1;
                frm.FormName = "DrawBom";
                #region 批次資料傳遞
                
                int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
                //勾稽明細
                string BomRec = DrD.Rows[Index_gv]["BomRec"].ToString();
                DataTable temp = dt_Bom_BatchProcess.Clone();
                for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
                {
                    if (dt_Bom_BatchProcess.Rows[i]["BomRec"].ToString() == BomRec)
                    {
                        dt_Bom_BatchProcess.Rows[i]["BomRec"] = BomRec;
                        temp.ImportRow(dt_Bom_BatchProcess.Rows[i]);
                    }
                }

                frm.上層Row = DrD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                frm.上層StnoO = StNoO.Text.Trim();
                frm.dt_Bom_BatchProcess = temp;
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
                            dataGridViewT1.Focus();
                            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            DrBom.Merge(frm.table);
                            DrBom.AcceptChanges();
                            CkeckMny();
                        }
                        else
                        {
                            DrBom.Merge(frm.table);
                            DrBom.AcceptChanges();
                        }
                        break;
                    case DialogResult.Cancel:
                        DrBom.Merge(table);
                        DrBom.AcceptChanges();
                        break;
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

        private void gridBshopHistory_Click(object sender, EventArgs e)
        {
            gridBshopHistory.Focus();

            if (jDraw.EnableBShopPrice() == false)
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
            var itno = DrD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
                if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewT1["序號", e.RowIndex].Value = e.RowIndex + 1;
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

                jDraw.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                jDraw.DataGridViewOpen<JBS.JS.Item>(sender, e, DrD, row => FillItem(row, e.RowIndex));
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "說明")
            {
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        DrD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                var itno = DrD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jDraw.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit == row["itunit"].ToString())
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = row["itunitp"].ToString();

                        DrD.Rows[e.RowIndex]["itunit"] = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal();
                        if (itpkgqty == 0)
                            itpkgqty = 1;

                        DrD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        DrD.Rows[e.RowIndex]["costb"] = row["itbuyprip"].ToDecimal();
                    }
                    else if (unit == row["itunitp"].ToString() && unit.Length > 0)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = row["itunit"].ToString();

                        DrD.Rows[e.RowIndex]["itunit"] = row["itunit"].ToString();
                        DrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        DrD.Rows[e.RowIndex]["costb"] = row["itbuypri"].ToDecimal();
                    }
                    else
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = row["itunit"].ToString();

                        DrD.Rows[e.RowIndex]["itunit"] = row["itunit"].ToString();
                        DrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        DrD.Rows[e.RowIndex]["costb"] = row["itbuypri"].ToDecimal();
                    }
                });
                dataGridViewT1.InvalidateRow(e.RowIndex);

                CkeckMny();

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品組成")
            {
                var ittrait = DrD.Rows[e.RowIndex]["ittrait"].ToDecimal();
                if (ittrait == 1)
                {
                    DrD.Rows[e.RowIndex]["ittrait"] = 2;
                    DrD.Rows[e.RowIndex]["產品組成"] = "組裝品";
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                }
                else if (ittrait == 2)
                {
                    DrD.Rows[e.RowIndex]["ittrait"] = 1;
                    DrD.Rows[e.RowIndex]["產品組成"] = "組合品";
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                }
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                #region 產品編號
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoNow.Length == 0)
                {
                    DrD.Rows[e.RowIndex]["itno"] = "";
                    DrD.Rows[e.RowIndex]["itname"] = "";
                    DrD.Rows[e.RowIndex]["itunit"] = "";
                    DrD.Rows[e.RowIndex]["qty"] = 0;
                    DrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    DrD.Rows[e.RowIndex]["memo"] = "";
                    DrD.Rows[e.RowIndex]["產品組成"] = "";

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    CkeckMny();

                    var rec = DrD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jDraw.RemoveBom(rec, ref DrBom);
                    //刪除批次異動資訊
                    BatchF.刪除批次異動(dt_BatchProcess, rec);
                    BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);
                    return;
                }

                if (ItNoNow == ItNoBegin)
                    return;

                if (ItNoNow == UdfNoBegin && ItNoNow.Length > 0)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    DrD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    //刪除批次異動資訊
                    var rec = DrD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    BatchF.刪除批次異動(dt_BatchProcess, rec);
                    BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);
                    return;
                }

                jDraw.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, DrD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                #region 單位
                var itno = DrD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                if (itno.Length == 0)
                    return;

                jDraw.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        DrD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        DrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                DrD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                CkeckMny();
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "領料數量")
            {
                #region 領料數量
                var qty = dataGridViewT1["領料數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                DrD.Rows[e.RowIndex]["qty"] = qty;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CkeckMny();


                var itno = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var stno = StNoO.Text;
                var ItTrait = dataGridViewT1["ItTrait", e.RowIndex].Value.ToString().Trim();
                var BomRec = DrD.Rows[e.RowIndex]["BomRec"].ToString().Trim();
 
                using (DataTable Bom     = new JBS.JS.Sale().GetBomQtyTable(DrD, DrBom))
                using (DataTable TempBom = new JBS.JS.Sale().GetBomQtyTable(tempD, tempBom))
                {
                    new JBS.JS.Sale().IsLowStock(itno, stno, ItTrait, BomRec, DrD, DrBom, Bom, tempD, TempBom, this.FormState);
                }

                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "成本")
            {
                #region 成本
                var costb = dataGridViewT1["成本", e.RowIndex].EditedFormattedValue.ToDecimal();
                DrD.Rows[e.RowIndex]["costb"] = costb;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CkeckMny();
                #endregion
            }
        }

        private void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["itno"].ToString().Trim();

            this.ItNoBegin = itno;

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;

            DrD.Rows[index]["itno"] = itno;
            DrD.Rows[index]["itname"] = reader["itname"].ToString();
            DrD.Rows[index]["itunit"] = reader["itunit"].ToString();
            DrD.Rows[index]["itpkgqty"] = 1;
            DrD.Rows[index]["costb"] = reader["itbuypri"].ToDecimal().ToString("f" + Common.M);
            DrD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            for (int i = 1; i <= 10; i++)
            {
                DrD.Rows[index]["itdesp" + i] = reader["itdesp" + i].ToString();
            }

            var ittrait = reader["ittrait"].ToDecimal();
            if (ittrait == 1 || ittrait == 2)
            {
                //組裝or組合,預設值帶組合
                ittrait = 1;
                DrD.Rows[index]["ittrait"] = ittrait;
                DrD.Rows[index]["產品組成"] = "組合品";
            }
            else
            {
                //單一商品
                ittrait = 3;
                DrD.Rows[index]["ittrait"] = ittrait;
                DrD.Rows[index]["產品組成"] = "單一商品";
            }
            dataGridViewT1.InvalidateRow(index);



            var rec = DrD.Rows[index]["BomRec"].ToString();
            jDraw.RemoveBom(rec, ref DrBom);

            if (ittrait != 3)
                jDraw.GetItemBom(itno, rec, ref DrBom);

            //刪除批次異動資訊
            BatchF.刪除批次異動(dt_BatchProcess, rec);
            BatchF.BOM刪除批次異動(dt_Bom_BatchProcess, rec);
        }

        private void StNoI_Validating(object sender, CancelEventArgs e)
        {
            if (StNoI.ReadOnly) return;
            if (btnCancel.Focused || btnExit.Focused) return;

            if (sender.Equals(StNoI))
            {
                if (StNoI.TrimTextLenth() == 0)
                {
                    StNoI.Clear();
                    StNameI.Clear();
                    return;
                }

                jDraw.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
                {
                    StNoI.Text = reader["StNo"].ToString().Trim();
                    StNameI.Text = reader["StName"].ToString().Trim();
                });
            }
            else
            {
                if (StNoO.TrimTextLenth() == 0)
                {
                    StNoO.Clear();
                    StNameO.Clear();

                    e.Cancel = true;
                    MessageBox.Show("倉庫編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    StNoO.Focus();
                    return;
                }

                jDraw.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
                {
                    StNoO.Text = reader["StNo"].ToString().Trim();
                    StNameO.Text = reader["StName"].ToString().Trim();
                });
            }


            if (Common.keyData != Keys.Up)
            {
                if (sender.Equals(StNoO))
                    if (dataGridViewT1.Rows.Count == 0)
                        if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
        }

        private void DrDate_Validating(object sender, CancelEventArgs e)
        {
            if (DrDate.ReadOnly) return;
            if (btnCancel.Focused) return;

            jDraw.DateValidate(sender, e);

        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (EmNo.ReadOnly == true) return;
            if (btnCancel.Focused || btnExit.Focused) return;

            if (EmNo.TrimTextLenth() == 0)
            {
                EmNo.Clear();
                EmName.Clear();
                return;
            }

            jDraw.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
            }, true);
        }

        private void DrMemo_DoubleClick(object sender, EventArgs e)
        {
            if (DrMemo.ReadOnly == true)
                return;

            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DrMemo.Text = frm.Memo.GetUTF8(60);
                }
            }
        }

        private void StNoI_DoubleClick(object sender, EventArgs e)
        {
            if (sender.Equals(StNoI))
            {
                jDraw.Open<JBS.JS.Stkroom>(sender, reader =>
                {
                    StNoI.Text = reader["StNo"].ToString().Trim();
                    StNameI.Text = reader["StName"].ToString().Trim();
                });
            }
            else
            {
                jDraw.Open<JBS.JS.Stkroom>(sender, reader =>
                {
                    StNoO.Text = reader["StNo"].ToString().Trim();
                    StNameO.Text = reader["StName"].ToString().Trim();
                });
            }
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jDraw.Open<JBS.JS.Empl>(sender, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
            });
        }

        private void DrNo_Enter(object sender, EventArgs e)
        {
            TextBefore = DrNo.Text;
        }

        private void DrNo_DoubleClick(object sender, EventArgs e)
        {
            if (DrNo.ReadOnly) 
                return;

            using (var frm = new FrmDraw_Print_DrNo())
            {
                frm.TSeekNo = DrNo.Text.Trim();
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        DrNo.Text = frm.TResult.Trim();
                        break;
                }
            }
        }

        private void DrNo_Validating(object sender, CancelEventArgs e)
        {
            if (DrNo.ReadOnly || btnCancel.Focused) return;

            if (DrNo.Text.Length > 0 && DrNo.Text.Trim() == "")
            {
                e.Cancel = true;
                DrNo.Text = "";
                DrNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jDraw.IsExistDocument<JBS.JS.Draw>(DrNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jDraw.IsExistDocument<JBS.JS.Draw>(DrNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jDraw.IsExistDocument<JBS.JS.Draw>(DrNo.Text.Trim()))
                {
                    if (TextBefore == DrNo.Text.Trim())
                        return;

                    writeToTxt(DrNo.Text.Trim());
                    load_DrBom();
                }
                else
                {
                    e.Cancel = true;
                    DrNo.SelectAll();

                    using (var frm = new FrmDraw_Print_DrNo())
                    {
                        frm.TSeekNo = DrNo.Text.Trim();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            DrNo.Text = frm.TResult.Trim();
                            writeToTxt(frm.TResult.Trim());
                            load_DrBom();
                        }
                    }
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

        ToolTip tip = new ToolTip();
        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "備註說明")
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
            if (DrNo.Text.Trim() == "")
                return;

            using (FrmSale_AppScNo frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jDraw.keyMan.AppendMan;
                frm.ATime = jDraw.keyMan.AppendTime;
                //修改人員
                frm.EName = jDraw.keyMan.EditMan;
                frm.ETime = jDraw.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = DrNo.ReadOnly ? false : true;
                frm.memo1 = Memo1;

                if (frm.ShowDialog() == DialogResult.OK) Memo1 = frm.memo1;
            }
        }

        void CkeckMny()
        {
            decimal d = 0, q = 0;
            decimal totle = 0;
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                decimal.TryParse(dataGridViewT1["領料數量", i].Value.ToString(), out q);
                decimal.TryParse(dataGridViewT1["成本", i].Value.ToString(), out d);
                dataGridViewT1["金額", i].Value = (q * d).ToString();
            }
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                decimal.TryParse(dataGridViewT1["金額", i].Value.ToString(), out d);
                totle += d;
            }
            TotMnyB.Text = totle.ToString("f" + Common.M);
        }

        bool 加庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false) return true;

                var stno = "";
                var itno = "";
                var qty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (qty * itpkgqty).ToDecimal("f" + Common.Q);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StNo", stno);
                    cmd.Parameters.AddWithValue("@ItNo", itno);
                    cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) + (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + TotalQty + ");";
                        cmd.ExecuteNonQuery();
                    }

                    //cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    //TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    //cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    //cmd.ExecuteNonQuery();
                }
                foreach (DataRow row in dt.Select("ItTrait = '1'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (qty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StNo", stno);
                        cmd.Parameters.AddWithValue("@ItNo", itno);
                        cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) + (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + TotalQty + ");";
                            cmd.ExecuteNonQuery();
                        }

                        //cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        //TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        //cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                        //cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool 扣庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false) return true;

                var stno = "";
                var itno = "";
                var qty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (qty * itpkgqty).ToDecimal("f" + Common.Q);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StNo", stno);
                    cmd.Parameters.AddWithValue("@ItNo", itno);
                    cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) - (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + (-1 * TotalQty) + ");";
                        cmd.ExecuteNonQuery();
                    }

                    //cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    //TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    //cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    //cmd.ExecuteNonQuery();
                }
                foreach (DataRow row in dt.Select("ItTrait = '1'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (qty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StNo", stno);
                        cmd.Parameters.AddWithValue("@ItNo", itno);
                        cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) - (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + (-1 * TotalQty) + ");";
                            cmd.ExecuteNonQuery();
                        }

                        //cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        //TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        //cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                        //cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void gridbatch_Click(object sender, EventArgs e)
        {
            BatchF.WhenGridBadch_click(this.Name, dataGridViewT1, DrD, dt_BatchProcess, null, null, null, null,StNoO,true,btnSave.Enabled == true);
        }
    }
}
