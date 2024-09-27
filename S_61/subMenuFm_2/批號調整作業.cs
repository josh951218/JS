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

namespace S_61.subMenuFm_2
{
    public partial class 批號調整作業 : Formbase
    {
        JBS.JS.BatchProcess_adjust jBatch_adjust;
        JBS.JS.xEvents xe = new JBS.JS.xEvents();

        List<TextBoxbase> list;

        #region 批次資料
        BatchProcess BatchF = new BatchProcess();           //批次資料異動修改
        BatchSave BatchSave = new BatchSave();              //批次資料存檔用
        DataTable dt_BatchProcess = new DataTable();        //批次異動
        DataTable dt_TempBatchProcess = new DataTable();    //批次異動暫存檔
        #endregion

        string Memo1 = "";
        string old_StNo = "";

        public 批號調整作業()
        {
            InitializeComponent();
            this.jBatch_adjust = new JBS.JS.BatchProcess_adjust();
            this.list = this.getEnumMember();
            this.備註說明.HeaderText = Common.Sys_MemoUdf;

            this.帳上庫存量.Set庫存數量小數();
            this.盤點數量.Set庫存數量小數();
            this.盤盈虧數量.Set庫存數量小數();

            AdDate.SetDateLength();

            dataGridViewT1.DataSource = dt_BatchProcess;
        }

        private void 批號調整作業_Load(object sender, EventArgs e)
        {
            writeToTxt(jBatch_adjust.Bottom());
        }

        void writeToTxt(string adno)
        {
            var result = jBatch_adjust.LoadData(adno, row =>
            {
                AdNo.Text = row["adno"].ToString();

                if (Common.User_DateTime == 1)
                    AdDate.Text = row["addate"].ToString();
                else
                    AdDate.Text = row["addate1"].ToString();
                StNo.Text = row["stno"].ToString();
                StNo.Tag = row["stno"].ToString();
                old_StNo = row["stno"].ToString();

                StName.Text = row["stname"].ToString();

                EmNo.Text = row["emno"].ToString();
                EmName.Text = row["emname"].ToString();
                AdMemo.Text = row["admemo"].ToString();

                this.Memo1 = row["admemo1"].ToString();
                jBatch_adjust.keyMan.Set(row);

            });

            if (!result)
            {
                Common.SetTextState(this.FormState = FormEditState.Clear, ref list);
                StNo.Tag = "";
                this.Memo1 = "";
                old_StNo = "";
                jBatch_adjust.keyMan.Clear();
            }


            string str = @"select b.*,a.batchNo,a.[Date],a.Date1,
            faname1=(select faname1 from fact where fano=b.fano)
            from 
	            (SELECT * FROM BatchProcess_adjustd WHERE bomid like '" + adno + @"'+'__________')   as b
            Left join 
	            BatchInformation as a
            on a.bno = b.bno";

            dt_BatchProcess.Clear();
            dt_TempBatchProcess.Clear();
            SQL.ExecuteNonQuery(str, null, dt_BatchProcess);
            SQL.ExecuteNonQuery(str, null, dt_TempBatchProcess);
        }




        void AddRow()
        {
            DataRow row = dt_BatchProcess.NewRow();
            row["Bno"] = -1;
            row["Batchno"] = "";
            row["itno"] = "";
            row["itname"] = "";
            row["stkqty"] = 0;
            row["qty"] = 0;
            row["realqty"] = 0;
            row["memo"] = "";
            row["fano"] = "";
            row["faname1"] = "";
            row["Date"] = "";
            row["Date1"] = "";
            row["stno"] = "";
            row["rec"] = dt_BatchProcess.Rows.Count > 0 ? dt_BatchProcess.AsEnumerable().Max(r => r["rec"].ToDecimal()) + 1 : 1;
            dt_BatchProcess.Rows.Add(row);
            dt_BatchProcess.AcceptChanges();
        }
        void AddRow(int index)
        {
            var row = dt_BatchProcess.NewRow();
            row["Bno"] = -1;
            row["Batchno"] = "";
            row["itno"] = "";
            row["itname"] = "";
            row["stkqty"] = 0;
            row["qty"] = 0;
            row["realqty"] = 0;
            row["memo"] = "";
            row["fano"] = "";
            row["faname1"] = "";
            row["Date"] = "";
            row["Date1"] = "";
            row["stno"] = "";
            row["rec"] = dt_BatchProcess.Rows.Count > 0 ? dt_BatchProcess.AsEnumerable().Max(r => r["rec"].ToDecimal()) + 1 : 1;
            dt_BatchProcess.Rows.InsertAt(row, index);
            dt_BatchProcess.AcceptChanges();
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
                var rec = dt_BatchProcess.Rows[index]["Rec"].ToString();

                dt_BatchProcess.Rows.RemoveAt(index);
                dt_BatchProcess.AcceptChanges();

                //刪除批次異動資訊
                BatchF.刪除批次異動(dt_BatchProcess, rec);

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


        private void btnTop_Click(object sender, EventArgs e)
        {
            var gano = jBatch_adjust.Top();
            writeToTxt(gano);
        }
        private void btnPrior_Click(object sender, EventArgs e)
        {
            var gano = jBatch_adjust.Prior();
            writeToTxt(gano);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            var gano = jBatch_adjust.Next();
            writeToTxt(gano);
        }
        private void btnBottom_Click(object sender, EventArgs e)
        {
            var gano = jBatch_adjust.Bottom();
            writeToTxt(gano);
        }




        void ThisFormState()
        {
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            this.產品名稱.ReadOnly = true;
            this.帳上庫存量.ReadOnly = true;
            this.盤盈虧數量.ReadOnly = true;
            this.製造日期.ReadOnly = true;
            this.有效日期.ReadOnly = true;
            this.製造廠商.ReadOnly = true;

            gridAppend.Enabled = gridDelete.Enabled = gridInsert.Enabled = true;

            #region//新增時,各種歸零,然後設定某些預設值
            if (this.FormState == FormEditState.Append)
            {
                this.Memo1 = "";

                AdDate.Text = Date.GetDateTime(Common.User_DateTime, false);

                StNo.Text = Common.User_StkNo;
                jBatch_adjust.Validate<JBS.JS.Stkroom>(StNo.Text, row =>
                {
                    StNo.Tag = StNo.Text = row["stno"].ToString().Trim();
                    StName.Text = row["stname"].ToString();

                });
            }
            #endregion

            #region//複製時,值不變  ,但要設定某些預設值
            if (this.FormState == FormEditState.Duplicate)
            {
                AdNo.Clear();
                AdDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            }
            #endregion

            #region//修改時,值不變
            if (this.FormState == FormEditState.Modify)
            {
                
            }
            #endregion
            AdDate.Focus();
        }
        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            ThisFormState();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess);
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            ThisFormState();
            BatchF.WhenAppendOrDuplicate(dt_BatchProcess, dt_TempBatchProcess);
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jBatch_adjust.IsExistDocument<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBottom_Click(null, null);
                return;
            }

            if (jBatch_adjust.IsEditInCloseDay(AdDate.Text.Trim()) == false)
                return;

            if (jBatch_adjust.IsModify<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jBatch_adjust.upModify1<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim());//更新修改狀態1
                var pk = jBatch_adjust.Renew();//刷新資料
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
            if (jBatch_adjust.IsExistDocument<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBottom_Click(null, null);
                return;
            }
            if (jBatch_adjust.IsModify<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jBatch_adjust.IsEditInCloseDay(AdDate.Text.Trim()) == false)
                return;

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
                    cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());

                    cmd.CommandText = @"delete from BatchProcess_adjust where adno=@adno;";
                    cmd.ExecuteNonQuery();

                    BatchSave.進貨_Delete(dt_TempBatchProcess, cmd, "adjustd", AdNo.Text.Trim(), StNo.Text.Trim());

                    tn.Commit();

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
            //using (var frm = new FrmGarner_Print())
            //{
            //    frm.PK = GaNo.Text.Trim();
            //    frm.ShowDialog();
            //}
        }
        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (AdNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("空資料庫，請先新增！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new 批號調整瀏覽())
            {
                frm.TSeekNo = AdNo.Text.Trim();
                frm.ShowDialog();
                writeToTxt(frm.TResult);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (jBatch_adjust.IsEditInCloseDay(AdDate.Text) == false)
                return;

            //移除空行，無產品編號&無批號
            for (int i = 0; i < dt_BatchProcess.Rows.Count; i++)
            {
                if (dt_BatchProcess.Rows[i]["itno"].ToString().Trim() == "" || (dt_BatchProcess.Rows[i]["batchno"].ToString().Trim() == ""))
                    dt_BatchProcess.Rows[i].Delete();
            }
            dt_BatchProcess.AcceptChanges();

            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("入庫明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < dt_BatchProcess.Rows.Count; i++)
            {
                dt_BatchProcess.Rows[i]["StNo"] = StNo.Text.Trim();
            }

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                #region Append、Duplicate
                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        var result = jBatch_adjust.GetPkNumber<JBS.JS.BatchProcess_adjust>(cmd, AdDate.Text, ref AdNo);
                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        //表頭
                        AppendMasterOnSaving(cmd);
                        //批次資料
                        BatchSave.進貨_Append(dt_BatchProcess.Copy(), cmd, "adjustd", AdNo.Text.Trim(), false, "", StNo.Text.Trim());
                        //明細
                        UpdateDetailOnSaving(cmd, ref dt_BatchProcess);

                        tn.Commit();

                        jBatch_adjust.Save(AdNo.Text.Trim());

                        dt_TempBatchProcess.Clear();
                        dt_BatchProcess.Clear();
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }

                //if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //{
                //    using (var frm = new FrmGarner_Print())
                //    {
                //        frm.PK = jBatch_adjust.GetCurrent();
                //        frm.ShowDialog();
                //    }
                //}

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                btnAppend_Click(null, null);


                #endregion
            }

            if (this.FormState == FormEditState.Modify)
            {
                #region Modify
                if (jBatch_adjust.IsExistDocument<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
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

                        //刪除表頭 表頭
                        UpdateMasterOnSaving(cmd);
                        //批次資料
                        BatchSave.進貨_Modify(dt_TempBatchProcess.Copy(), dt_BatchProcess.Copy(), cmd, "adjustd", AdNo.Text.Trim(), false, "", StNo.Text.Trim(), old_StNo);
                        //明細
                        UpdateDetailOnSaving(cmd, ref dt_BatchProcess);

                        tn.Commit();

                        jBatch_adjust.Save(AdNo.Text.Trim());

                        dt_TempBatchProcess.Clear();
                        dt_BatchProcess.Clear();
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                //if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //{
                //    using (var frm = new FrmGarner_Print())
                //    {
                //        frm.PK = jBatch_adjust.GetCurrent();
                //        frm.ShowDialog();
                //    }
                //}

                btnCancel_Click(null, null);


                #endregion
            }
        }
        void SetParameters(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("adno", AdNo.Text);
            cmd.Parameters.AddWithValue("addate", Date.ToTWDate(AdDate.Text));
            cmd.Parameters.AddWithValue("addate1", Date.ToUSDate(AdDate.Text));
            cmd.Parameters.AddWithValue("stno", StNo.Text);
            cmd.Parameters.AddWithValue("stname", StName.Text);
            cmd.Parameters.AddWithValue("emno", EmNo.Text);
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("admemo", AdMemo.Text);
            cmd.Parameters.AddWithValue("admemo1", Memo1);
            cmd.Parameters.AddWithValue("recordno", dt_BatchProcess.Rows.Count);
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            SetParameters(cmd);
            cmd.CommandText = @"INSERT INTO [dbo].[BatchProcess_adjust]
            ([adno],[addate],[addate1],[stno],[stname],[emno],[emname],[admemo],
            [appscno],[appdate],[edtscno],[edtdate],[recordno],[admemo1])
            VALUES
            (@adno,@addate,@addate1,@stno,@stname,@emno,@emname,@admemo,
            @appscno,@appdate,@edtscno,@edtdate,@recordno,@admemo1)";

            cmd.ExecuteNonQuery();
        }
        private void UpdateMasterOnSaving(SqlCommand cmd)
        {
            //刪除表頭
            SetParameters(cmd);
            //表頭
            cmd.CommandText = @"UPDATE [dbo].[BatchProcess_adjust]
               SET [addate] = @addate
                  ,[addate1] = @addate1
                  ,[stno] = @stno
                  ,[stname] = @stname
                  ,[emno] = @emno
                  ,[emname] = @emname
                  ,[admemo] = @admemo
                  ,[edtscno] = @edtscno
                  ,[edtdate] = @edtdate
                  ,[recordno] = @recordno
                  ,[admemo1] = @admemo1
             WHERE adno=@adno";
            cmd.ExecuteNonQuery();
        }
        private void UpdateDetailOnSaving(SqlCommand cmd,ref DataTable dtD)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("adno", AdNo.Text);
            cmd.Parameters.AddWithValue("addate", Date.ToTWDate(AdDate.Text));
            cmd.Parameters.AddWithValue("addate1", Date.ToUSDate(AdDate.Text));
            cmd.Parameters.AddWithValue("stno", StNo.Text);

            cmd.Parameters.AddWithValue("itno","");
            cmd.Parameters.AddWithValue("itname", "");
            cmd.Parameters.AddWithValue("stkqty", "");
            cmd.Parameters.AddWithValue("realqty", "");
            cmd.Parameters.AddWithValue("memo", "");
            cmd.Parameters.AddWithValue("recordno", "");
            cmd.Parameters.AddWithValue("fano", "");

            cmd.Parameters.AddWithValue("bomid", "");

            cmd.CommandText = @"
            UPDATE [dbo].[BatchProcess_adjustd]
               SET [adno] = @adno
                  ,[addate] = @addate
                  ,[addate1] = @addate1
                  ,[stno] = @stno
                  ,[itno] = @itno
                  ,[itname] = @itname
                  ,[stkqty] = @stkqty
                  ,[realqty] = @realqty
                  ,[memo] = @memo
                  ,[recordno] = @recordno
                  ,[fano]=@fano
             WHERE bomid=@bomid";

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                cmd.Parameters["bomid"].Value = AdNo.Text.Trim() + dtD.Rows[i]["rec"].ToString().PadLeft(10, '0');
                cmd.Parameters["itno"].Value = dtD.Rows[i]["itno"].ToString().Trim();
                cmd.Parameters["itname"].Value = dtD.Rows[i]["itname"].ToString().Trim();
                cmd.Parameters["stkqty"].Value = dtD.Rows[i]["stkqty"].ToString().Trim();
                cmd.Parameters["realqty"].Value = dtD.Rows[i]["realqty"].ToString().Trim();
                cmd.Parameters["memo"].Value = dtD.Rows[i]["memo"].ToString().Trim();
                cmd.Parameters["recordno"].Value = (i + 1);
                cmd.Parameters["fano"].Value = dtD.Rows[i]["fano"].ToString().Trim();
                cmd.ExecuteNonQuery();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.None, ref list);
            var gano = jBatch_adjust.Cancel();
            writeToTxt(gano);
            
            gridAppend.Enabled = gridDelete.Enabled = gridInsert.Enabled = true;
            dataGridViewT1.ReadOnly = true;
            btnAppend.Focus();
            jBatch_adjust.upModify0<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim());//改回0為無修改狀態
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        private void AdNo_Enter(object sender, EventArgs e)
        {
            AdNo.Tag = AdNo.Text.Trim();
        }
        private void AdNo_Validating(object sender, CancelEventArgs e)
        {
            if (AdNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (AdNo.Text.Length > 0 && AdNo.Text.Trim() == "")
            {
                e.Cancel = true;
                AdNo.Text = "";
                AdNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jBatch_adjust.IsExistDocument<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jBatch_adjust.IsExistDocument<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jBatch_adjust.IsExistDocument<JBS.JS.BatchProcess_adjust>(AdNo.Text.Trim()))
                {
                    if (AdNo.Text.Trim() == ((AdNo.Tag) ?? "").ToString())
                        return;

                    writeToTxt(AdNo.Text.Trim());
                }
                else
                {
                    e.Cancel = true;
                    //GaNo_DoubleClick(GaNo, null);
                    //GaNo.SelectAll();

                    //var exist = jGarner.IsExistDocument<JBS.JS.Garner>(GaNo.Text.Trim());
                    //if (exist)
                    //{
                    //    writeToTxt(GaNo.Text.Trim());
                    //    loadSaleBom();
                    //}
                }
            }
        }
        private void GaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            jBatch_adjust.DateValidate(sender, e);
        }
        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jBatch_adjust.Open<JBS.JS.Stkroom>(sender, reader => FillStNo(reader));
        }
        void FillStNo(SqlDataReader reader)
        {
            StNo.Text = reader["StNo"].ToString().Trim();
            if (StNo.Tag.ToString().Trim() == StNo.Text.Trim()) return;
            StNo.Tag = reader["StNo"].ToString().Trim();
            StName.Text = reader["StName"].ToString().Trim();
            if (dt_BatchProcess.Rows.Count == 0)
                gridAppend_Click(null, null);
            else
            {
                for (int i = 0; i < dt_BatchProcess.Rows.Count; i++)
                {
                    GetStkQty(dt_BatchProcess.Rows[i]);
                    ComputeRealQty(dt_BatchProcess.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
            }
        }
        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly || btnCancel.Focused)
                return;

            jBatch_adjust.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader => FillStNo(reader));
        }
        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jBatch_adjust.Open<JBS.JS.Empl>(sender, reader =>
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

            jBatch_adjust.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();

            }, true);
        }
        private void AdMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(AdMemo, 68);
        }



        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0 && dataGridViewT1.ReadOnly == false)
                gridAppend_Click(null, null);
        }
        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                dataGridViewT1["序號", e.RowIndex].Value = (e.RowIndex + 1).ToString();
            }
        }
        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0 ) return;
            if (dataGridViewT1.EditingControl == null) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.批號))
            {
                if (dt_BatchProcess.Rows[e.RowIndex]["itno"].ToString().Trim() == "")
                {
                    MessageBox.Show("請先輸入產品編號後，再輸入批號");
                    return;
                }
                using (批號開窗 frm = new 批號開窗())
                {
                    frm.itno = dt_BatchProcess.Rows[e.RowIndex]["itno"].ToString().Trim();
                    frm.batchno = dataGridViewT1.EditingControl.Text.Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewT1.EditingControl.Text = frm.result["batchno"].ToString().Trim();
                        FillBatchNo(frm.result,e.RowIndex);
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.備註說明))
            {
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);
                        dt_BatchProcess.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                jBatch_adjust.DataGridViewOpen<JBS.JS.Item>(sender, e, dt_BatchProcess, row => FillItem(row, e.RowIndex));
            }
        }
        bool FillItem(SqlDataReader row, int index)
        {
            var itno = row["itno"].ToString().Trim();

            dataGridViewT1.EditingControl.Text = itno;
            dt_BatchProcess.Rows[index]["itno"] = itno;
            dt_BatchProcess.Rows[index]["itname"] = row["itname"].ToString();
            
            //輸入產品後，批號清空
            dt_BatchProcess.Rows[index]["Bno"] = -1;
            dt_BatchProcess.Rows[index]["batchno"] = "";
            dt_BatchProcess.Rows[index]["fano"] = "";
            dt_BatchProcess.Rows[index]["faname1"] = "";
            dt_BatchProcess.Rows[index]["date"] = "";
            dt_BatchProcess.Rows[index]["date1"] = "";
            dt_BatchProcess.Rows[index]["stkqty"] = 0;
            dt_BatchProcess.Rows[index]["realqty"] = 0;
            dt_BatchProcess.Rows[index]["qty"] = 0;

            //搜尋該產品是否有批號
            DataTable temp = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("itno", itno);
                cmd.CommandText = @"select *,faname1=(select faname1 from fact where fano=batchInformation.Fano)  
                                    from batchInformation where Itno=@itno";
                dd.Fill(temp);
            }
            //如果批號大於2筆，不自動帶出
            if (temp.Rows.Count == 1)
            {
                dt_BatchProcess.Rows[index]["Bno"] = temp.Rows[0]["Bno"].ToString().Trim();
                dt_BatchProcess.Rows[index]["batchno"] = temp.Rows[0]["batchno"].ToString().Trim();
                dt_BatchProcess.Rows[index]["fano"] = temp.Rows[0]["fano"].ToString().Trim();
                dt_BatchProcess.Rows[index]["faname1"] = temp.Rows[0]["faname1"].ToString().Trim();
                if (Common.User_DateTime == 1)
                {
                    dt_BatchProcess.Rows[index]["date"] = Date.ToTWDate(temp.Rows[0]["date"].ToString().Trim());
                    dt_BatchProcess.Rows[index]["date1"] = Date.ToTWDate(temp.Rows[0]["date1"].ToString().Trim());
                }
                else
                {
                    dt_BatchProcess.Rows[index]["date"] = Date.ToUSDate(temp.Rows[0]["date"].ToString().Trim());
                    dt_BatchProcess.Rows[index]["date1"] = Date.ToUSDate(temp.Rows[0]["date1"].ToString().Trim());
                }
                GetStkQty(dt_BatchProcess.Rows[index]);
                ComputeRealQty(dt_BatchProcess.Rows[index]);
            }
            temp.Clear();
            return true;
        }
        void FillBatchNo(DataRow row,int index)
        {
            dt_BatchProcess.Rows[index]["Bno"] = row["Bno"].ToString().Trim();
            dt_BatchProcess.Rows[index]["batchno"] = row["batchno"].ToString().Trim();
            dt_BatchProcess.Rows[index]["fano"] = row["fano"].ToString().Trim();
            dt_BatchProcess.Rows[index]["faname1"] = row["faname1"].ToString().Trim();

            if (Common.User_DateTime == 1)
            {
                dt_BatchProcess.Rows[index]["date"] = Date.ToTWDate(row["date"].ToString().Trim());
                dt_BatchProcess.Rows[index]["date1"] = Date.ToTWDate(row["date1"].ToString().Trim());
            }
            else
            {
                dt_BatchProcess.Rows[index]["date"] = Date.ToUSDate(row["date"].ToString().Trim());
                dt_BatchProcess.Rows[index]["date1"] = Date.ToUSDate(row["date1"].ToString().Trim());
            }

            GetStkQty(dt_BatchProcess.Rows[index]);
            ComputeRealQty(dt_BatchProcess.Rows[index]);
            dataGridViewT1.InvalidateRow(index);
        }
        void GetStkQty(DataRow row)
        {
            parameters par = new parameters();
            par.AddWithValue("bno", row["Bno"].ToString().Trim());
            par.AddWithValue("itno", row["itno"].ToString().Trim());
            par.AddWithValue("stno", StNo.Text.Trim());
            row["stkqty"] = SQL.ExecuteScalar(@"select stnoqty from BatchStock where bno=@bno and itno=@itno and stno=@stno", par).ToDecimal();
        }
        void ComputeRealQty(DataRow row)
        {
            row["qty"] = row["realqty"].ToDecimal() - row["stkqty"].ToDecimal();
        }
        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly || btnCancel.Focused || gridDelete.Focused) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (dataGridViewT1.EditingControl == null) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.批號))
            {
                if (dataGridViewT1.EditingControl.Text.Trim() == "")
                {
                    dt_BatchProcess.Rows[e.RowIndex]["Bno"] = -1;
                    dt_BatchProcess.Rows[e.RowIndex]["batchno"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["fano"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["faname1"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["date"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["date1"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["stkqty"] = 0;
                    dt_BatchProcess.Rows[e.RowIndex]["realqty"] = 0;
                    dt_BatchProcess.Rows[e.RowIndex]["qty"] = 0;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (dt_BatchProcess.Rows[e.RowIndex]["itno"].ToString().Trim() == "")
                {
                    MessageBox.Show("請先輸入產品編號後，再輸入批號");
                    e.Cancel = true;
                    return;
                }

                var NBatchNo = dataGridViewT1.EditingControl.Text.Trim();
                var BBatchNo = dt_BatchProcess.Rows[e.RowIndex]["batchno"].ToString().Trim();
                if (NBatchNo.Equals(BBatchNo)) return;

                //搜尋該產品是否有此批號
                DataTable temp = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("itno", dt_BatchProcess.Rows[e.RowIndex]["itno"].ToString().Trim());
                    cmd.Parameters.AddWithValue("batchno", dataGridViewT1.EditingControl.Text.Trim());
                    cmd.CommandText = @"select *,faname1=(select faname1 from fact where fano=batchInformation.fano)  
                                    from batchInformation where Itno=@itno and batchno=@batchno";
                    dd.Fill(temp);
                }
                if (temp.Rows.Count == 1)
                {
                    FillBatchNo(temp.Rows[0], e.RowIndex);
                    temp.Clear();
                }
                else
                {
                    //跑來這代表以下狀況；1.該產品無此批號 2.該產品有2個相同批號，但製造廠商不同
                    temp.Clear();
                    e.Cancel = true;
                    dataGridViewT1_CellDoubleClick(dataGridViewT1, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.盤點數量))
            {
                var NRealQty = dataGridViewT1.EditingControl.Text.ToDecimal();
                var BRealQty = dt_BatchProcess.Rows[e.RowIndex]["realqty"].ToDecimal();
                if (NRealQty == BRealQty) return;

                dt_BatchProcess.Rows[e.RowIndex]["realqty"] = NRealQty;
                ComputeRealQty(dt_BatchProcess.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                var ItNoNow = dataGridViewT1.EditingControl.Text.ToString().Trim();
                if (ItNoNow == "")
                {
                    dt_BatchProcess.Rows[e.RowIndex]["Bno"] = -1;
                    dt_BatchProcess.Rows[e.RowIndex]["batchno"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["itno"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["itname"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["fano"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["faname1"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["date"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["date1"] = "";
                    dt_BatchProcess.Rows[e.RowIndex]["stkqty"] = 0;
                    dt_BatchProcess.Rows[e.RowIndex]["realqty"] = 0;
                    dt_BatchProcess.Rows[e.RowIndex]["qty"] = 0;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                var ItNoBefore = dt_BatchProcess.Rows[e.RowIndex]["itno"].ToString().Trim();
                if (ItNoNow.Equals(ItNoBefore)) return;

                jBatch_adjust.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dt_BatchProcess, row =>
                {
                    e.Cancel = !FillItem(row, e.RowIndex);
                });
            }
        }



        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            if (AdNo.Text.Trim() == "")
                return;

            using (var frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jBatch_adjust.keyMan.AppendMan;
                frm.ATime = jBatch_adjust.keyMan.AppendTime;
                //修改人員
                frm.EName = jBatch_adjust.keyMan.EditMan;
                frm.ETime = jBatch_adjust.keyMan.EditTime;
                frm.ShowDialog();
            }
        }
        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = AdNo.ReadOnly ? false : true;
                frm.memo1 = Memo1;

                if (frm.ShowDialog() == DialogResult.OK) Memo1 = frm.memo1;
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
            return base.ProcessCmdKey(ref msg, keyData);
        }



    }
}
