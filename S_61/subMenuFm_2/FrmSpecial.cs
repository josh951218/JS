using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSpecial : Formbase
    {
        JBS.JS.Special jSpecial;
        JBS.JS.xEvents xe;
        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();
        DataTable dtTemp = new DataTable();

        List<TextBoxbase> list;

        DataRow dr = null;
        string tempNo;
        string TextBefore;
        string ItNoBegin;
        string UdfNoBegin;
        string btnState = "";

        public FrmSpecial()
        {
            InitializeComponent();
            this.jSpecial = new JBS.JS.Special();
            this.xe = new JBS.JS.xEvents();
            cn.ConnectionString = Common.sqlConnString;
            list = this.getEnumMember();

            gridGroupID.Enabled = true;
            this.數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.特價.Set本幣金額小數();
            this.紅利.FirstNum = 10;
            this.紅利.LastNum = 0;
            this.紅利.DefaultCellStyle.Format = "f0";
            this.原價.Set本幣金額小數();

            SpSDate.SetDateLength();
            SpEDate.SetDateLength();
            dataGridViewT1.DataSource = dtD;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmSpecial_Load(object sender, EventArgs e)
        {
            da.Fill(dtM);
            dtM.Clear();

            da1.SelectCommand.Parameters["@SpNo"].Value = SpNo.Text.Trim();
            da1.Fill(dtD);
            dtD.Clear();
            dataGridViewT1.DataSource = dtD;

            btnBottom_Click(null, null);
            btnAppend.Focus();
        }

        void WriteToText(DataRow row)
        {
            dr = row;
            if (row.IsNotNull())
            {
                SpNo.Text = row["SpNo"].ToString();
                if (Common.User_DateTime == 1)
                {
                    SpSDate.Text = row["SDate"].ToString();
                    SpEDate.Text = row["EDate"].ToString();
                }
                else
                {
                    SpSDate.Text = row["SDate1"].ToString();
                    SpEDate.Text = row["EDate1"].ToString();
                }
                EmNo.Text = row["EmNo"].ToString();
                EmName.Text = row["EmName"].ToString();
                SpMemo.Text = row["SpMemo"].ToString();
                CuX1No.Text = row["CuX1No"].ToString();
                CuNo.Text = row["CuNo"].ToString();
                parameters par = new parameters("CuNo", CuNo.Text.Trim());
                par.AddWithValue("X1No", CuX1No.Text.Trim());
                CuName1.Text  = SQL.ExecuteScalar("select top 1 cuname1 from cust where cuno =@CuNo", par);
                CuX1Name.Text = SQL.ExecuteScalar("Select top 1 X1Name from xx01 where X1No = @X1No", par);
                LoadD();
            }
            else
            {
                dtD.Clear();
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
        }

        void LoadD()
        {
            da1.SelectCommand.Parameters["@SpNo"].Value = SpNo.Text.Trim();

            dtD.Clear();
            da1.Fill(dtD);

            dtTemp.Clear();
            dtTemp = dtD.Copy();
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridAdjustDAddRows();
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
                int index = dataGridViewT1.CurrentRow.Index;
                dtD.Rows.RemoveAt(index);

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
            gridPicture.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus(); return;
            }
            using (var frm = new SOther.FrmPicture())
            {
                var row = Common.load("Check", "item", "itno", dtD.Rows[index]["ItNo"].ToString().Trim());
                frm.image = row["pic"];
                frm.ShowDialog();
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
                    GridAdjustDAddRows(index);
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells["產品編號"];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            gridItDesp.Focus();
            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(false, FormStyle.Mini))
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }
                frm.dr = dtD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridGroupID_Click(object sender, EventArgs e)
        {
            gridGroupID.Focus();
            using (var frm = new SOther.FrmSpecialGroup())
            {
                frm.ShowDialog();

                Common.SetTextState(FormState = FormEditState.None, ref list);
                WriteToText(Common.load("Check", "special", "spno", SpNo.Text.Trim()));
            }
            dataGridViewT1.Focus();
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

        private void gridBshopHistory_Click(object sender, EventArgs e)
        {
            gridBshopHistory.Focus();

            if (jSpecial.EnableBShopPrice() == false)
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
            var itno = dtD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }



        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToText(Common.load("Top", "special", "SpNo"));
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            if (SpNo.Text.Trim() == "") return;
            dr = Common.load("Prior", "special", "SpNo", SpNo.Text.Trim());
            if (dr == null)
            {
                dr = Common.load("CPrior", "special", "SpNo", SpNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnTop.Enabled = false;
                btnPrior.Enabled = false;
                btnNext.Enabled = true;
                btnBottom.Enabled = true;
            }
            else
            {
                btnNext.Enabled = true;
                btnBottom.Enabled = true;
            }
            WriteToText(dr);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (SpNo.Text.Trim() == "") return;
            dr = Common.load("Next", "special", "SpNo", SpNo.Text.Trim());
            if (dr == null)
            {
                dr = Common.load("CNext", "special", "SpNo", SpNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnTop.Enabled = true;
                btnPrior.Enabled = true;
                btnNext.Enabled = false;
                btnBottom.Enabled = false;
            }
            else
            {
                btnTop.Enabled = true;
                btnPrior.Enabled = true;
            }
            WriteToText(dr);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToText(Common.load("Bottom", "special", "SpNo"));
        }



        void SetTextWhenAppend()
        {
            SpSDate.Text = Common.User_DateTime == 1 ? Date.GetDateTime(1) : Date.GetDateTime(2);
            SpEDate.Text = Common.User_DateTime == 1 ? Date.GetDateTime(1) : Date.GetDateTime(2);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = SpNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            this.btnState = "Append";

            dtD.Clear();
            SetTextWhenAppend();
            SpSDate.Focus();
            this.自定編號.ReadOnly = true;
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (SpNo.Text.Trim() == "")
            {
                MessageBox.Show("無資料，請使用新增功能", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tempNo = SpNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            this.btnState = "Duplicate";

            SpNo.Clear();
            SpSDate.Focus();
            this.自定編號.ReadOnly = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (SpNo.Text.Trim() == "") return;

            tempNo = SpNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            this.btnState = "Modify";

            dtTemp = dtD.Copy();
            SpNo.Focus();
            SpNo.SelectAll();
            this.自定編號.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SpNo.TrimTextLenth() == 0) return;

            SqlTransaction tn = null;
            tempNo = SpNo.Text.Trim();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.Parameters.AddWithValue("@SpNo", tempNo);
                    cmd.CommandText = " Delete from Special where SpNo = (@SpNo);";
                    cmd.CommandText += " Delete from SpecialD where SpNo = (@SpNo);";
                    cmd.ExecuteNonQuery();
                    tn.Commit();

                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
            WriteToText(Common.load("CNext", "special", "SpNo", tempNo.Trim()));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (var frm = new FrmSpecialPrint())
                {
                    frm.ShowDialog();
                }
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (SpNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmSpecialBrow())
            {
                frm.TSeekNo = SpNo.Text.Trim();
                frm.ShowDialog();
                WriteToText(Common.load("Check", "SPECIAL", "SPNO", frm.TResult));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FormState == FormEditState.Append || FormState == FormEditState.Duplicate)
            {
                if (!Common.JESetSSID(SqlTable.Special, ref SpSDate, ref SpNo)) return;
            }
            if (SpNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SpNo.Focus();
                return;
            }

            if (dataGridViewT1.Rows.Count > 0)
            {
                //if (dataGridViewT1.Rows.OfType<DataGridViewRow>().Count(r => r.Cells["產品編號"].EditedFormattedValue.IsNullOrEmpty()) > 0)
                //{
                //    var rows = dtD.AsEnumerable().Where(r => !r["itno"].ToString().IsNullOrEmpty());

                //    if (rows.Count() == 0) dtD.Clear();
                //    else dtD = rows.CopyToDataTable();

                //    dtD.AcceptChanges();
                //    dataGridViewT1.DataSource = dtD;
                //}
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    if (dtD.Rows[i]["ItNo"].ToString().Trim().Length == 0)
                    {
                        dtD.Rows.Remove(dtD.Rows[i]);
                        i--;
                    }
                }
                dtD.AcceptChanges();

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    if (dtD.Rows[i]["SpTrait"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("特價方式未設定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.CurrentCell = dataGridViewT1["特價取用方式", i];
                        dataGridViewT1.Rows[i].Selected = true;
                        dataGridViewT1.Focus();
                        return;
                    }
                }
            }
            //if (dataGridViewT1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            //    {
            //        if (dataGridViewT1["SpTrait", i].Value.IsNullOrEmpty())
            //        {
            //            MessageBox.Show("特價方式未設定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
            //            dataGridViewT1.CurrentCell = dataGridViewT1["特價取用方式", i];
            //            dataGridViewT1.Rows[i].Selected = true;
            //            dataGridViewT1.Focus();
            //            return;
            //        }
            //    }
            //}

            if (FormState == FormEditState.Append || FormState == FormEditState.Duplicate)
            {
                SqlTransaction trans = null;
                try
                {
                    if (cn.State != ConnectionState.Open) cn.Open();
                    trans = cn.BeginTransaction();

                    dtM.Clear();
                    SqlCommand cmd = new SqlCommand(@"
INSERT INTO [Special] (CuNo,CuX1No,[SpNo], [SDate], [SDate1], [EDate], [EDate1], [EmNo], [EmName], [SpMemo], [RecordNo]) 
 VALUES (@CuNo,@CuX1No,@SpNo, @SDate, @SDate1, @EDate, @EDate1, @EmNo, @EmName, @SpMemo, @RecordNo)", cn, trans);
                    cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());
                    cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                    cmd.Parameters.AddWithValue("CuX1No", CuX1No.Text.Trim());
                    cmd.Parameters.AddWithValue("SDate", Date.ToTWDate(SpSDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("SDate1", Date.ToUSDate(SpSDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("EDate", Date.ToTWDate(SpEDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("EDate1", Date.ToUSDate(SpEDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("EmNo", EmNo.Text.Trim());
                    cmd.Parameters.AddWithValue("EmName", EmName.Text.Trim());
                    cmd.Parameters.AddWithValue("SpMemo", SpMemo.Text.Trim());
                    cmd.Parameters.AddWithValue("ReCordNo", dtD.Rows.Count.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["SpNo"] = SpNo.Text.Trim();
                        dtD.Rows[i]["SDate"] = Date.ToTWDate(SpSDate.Text.Trim());
                        dtD.Rows[i]["SDate1"] = Date.ToUSDate(SpSDate.Text.Trim());
                        dtD.Rows[i]["EDate"] = Date.ToTWDate(SpEDate.Text.Trim());
                        dtD.Rows[i]["EDate1"] = Date.ToUSDate(SpEDate.Text.Trim());
                        dtD.Rows[i]["EmNo"] = EmNo.Text.Trim();
                        dtD.Rows[i]["ReCordNo"] = (i + 1);
                        dtD.Rows[i].AcceptChanges();
                        dtD.Rows[i].SetAdded();
                    }
                    da1.InsertCommand.Transaction = trans;
                    da1.Update(dtD);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    if (trans != null) trans.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (FormState == FormEditState.Modify)
            {
                SqlTransaction trans = null;
                try
                {
                    if (cn.State != ConnectionState.Open) cn.Open();
                    trans = cn.BeginTransaction();

                    dtM.Clear();
                    dr.AcceptChanges();      
                    SqlCommand cmd = new SqlCommand(@"
UPDATE [Special] SET [SpNo] = @SpNo, [SDate] = @SDate, [SDate1] = @SDate1, [EDate] = @EDate, [EDate1] = @EDate1, [EmNo] = @EmNo, [EmName] = @EmName, [SpMemo] = @SpMemo, [RecordNo] = @RecordNo ,CuNo = @CuNo ,CuX1No = @CuX1No
WHERE [SpNo] = @SpNo", cn, trans);
                    cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());
                    cmd.Parameters.AddWithValue("CuNo",CuNo.Text.Trim());
                    cmd.Parameters.AddWithValue("CuX1No",CuX1No.Text.Trim());
                    cmd.Parameters.AddWithValue("SDate",Date.ToTWDate(SpSDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("SDate1",Date.ToUSDate(SpSDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("EDate",Date.ToTWDate(SpEDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("EDate1",Date.ToUSDate(SpEDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("EmNo",EmNo.Text.Trim());
                    cmd.Parameters.AddWithValue("EmName",EmName.Text.Trim());
                    cmd.Parameters.AddWithValue("SpMemo",SpMemo.Text.Trim());
                    cmd.Parameters.AddWithValue("ReCordNo",dtD.Rows.Count.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    //先刪除
                    dtTemp.AcceptChanges();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        dtTemp.Rows[i].Delete();
                    }
                    da1.DeleteCommand.Transaction = trans;
                    da1.Update(dtTemp);
                    //再新增
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["SpNo"] = SpNo.Text.Trim();
                        dtD.Rows[i]["SDate"] = Date.ToTWDate(SpSDate.Text.Trim());
                        dtD.Rows[i]["SDate1"] = Date.ToUSDate(SpSDate.Text.Trim());
                        dtD.Rows[i]["EDate"] = Date.ToTWDate(SpEDate.Text.Trim());
                        dtD.Rows[i]["EDate1"] = Date.ToUSDate(SpEDate.Text.Trim());
                        dtD.Rows[i]["EmNo"] = EmNo.Text.Trim();
                        dtD.Rows[i]["ReCordNo"] = (i + 1);
                        dtD.Rows[i].AcceptChanges();
                        dtD.Rows[i].SetAdded();
                    }
                    da1.InsertCommand.Transaction = trans;
                    da1.Update(dtD);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    if (trans != null) trans.Rollback();
                    MessageBox.Show(ex.ToString());
                }

            }
            tempNo = SpNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            dtD.Clear();
            SpNo.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnState = "";
            Common.SetTextState(FormState = FormEditState.None, ref list);
            WriteToText(Common.load("Check", "special", "spno", tempNo));
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void SpNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            gridAppend.Enabled = gridDelete.Enabled = gridPicture.Enabled = gridInsert.Enabled = gridItDesp.Enabled = !SpNo.ReadOnly;
            gridGroupID.Enabled = SpNo.ReadOnly;

            dataGridViewT1.ReadOnly = SpNo.ReadOnly;
            dataGridViewT1.Columns["序號"].ReadOnly = true;

            this.Reason.ReadOnly = true;
            this.數量.ReadOnly = true;
            this.折數.ReadOnly = true;
            this.特價.ReadOnly = true;
            this.紅利.ReadOnly = true;
            this.原價.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jSpecial.Open<JBS.JS.Empl>(sender, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }
        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (EmNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (EmNo.Text.Trim().Length == 0)
            {
                EmNo.Clear();
                EmName.Clear();
                return;
            }

            jSpecial.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void SpSDate_Validating(object sender, CancelEventArgs e)
        {
            if (SpEDate.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SpSDate.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                SpSDate.Clear();
            }
            else
            {
                if (!SpSDate.IsDateTime())
                {
                    e.Cancel = true;
                    MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SpSDate.Text = Date.GetDateTime(Common.User_DateTime);
                    SpSDate.SelectAll();
                }
                else
                {
                    //if (string.CompareOrdinal(SpEDate.Text, SpSDate.Text) < 0)
                    //{
                    //    e.Cancel = true;
                    //    MessageBox.Show("起始日期大於終止日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    SpEDate.Text = Date.GetDateTime(Common.User_DateTime);
                    //    SpEDate.SelectAll();
                    //}
                }
            }
        }

        private void SpEDate_Validating(object sender, CancelEventArgs e)
        {
            if (SpEDate.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SpEDate.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                SpEDate.Clear();
            }
            else
            {
                if (!SpEDate.IsDateTime())
                {
                    e.Cancel = true;
                    MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SpEDate.Text = Date.GetDateTime(Common.User_DateTime);
                    SpEDate.SelectAll();
                }
                else
                {
                    if (string.CompareOrdinal(SpEDate.Text, SpSDate.Text) < 0)
                    {
                        e.Cancel = true;
                        MessageBox.Show("起始日期大於終止日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SpEDate.Text = Date.GetDateTime(Common.User_DateTime);
                        SpEDate.SelectAll();
                    }
                    if (Common.keyData != Keys.Up)
                    {
                        if (dataGridViewT1.Rows.Count == 0)
                            if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
                    }
                }
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
                if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        private void dataGridViewT1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        void GridAdjustDAddRows()
        {
            DataRow dr = dtD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 1;
            dr["prs"] = 1;
            dr["price"] = 0;
            dr["point"] = 0;
            dr["spTrait"] = "";
            dr["spTraitName"] = "";
            dr["itprice"] = 0;
            dr["itunit"] = "";
            dr["itpkgqty"] = 1;
            dr["ittrait"] = 0;
            dr["memo"] = "";
            dtD.Rows.Add(dr);
            dtD.AcceptChanges();
        }

        void GridAdjustDAddRows(int index)
        {
            DataRow dr = dtD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 1;
            dr["prs"] = 1;
            dr["price"] = 0;
            dr["point"] = 0;
            dr["spTrait"] = "";
            dr["spTraitName"] = "";
            dr["itprice"] = 0;
            dr["itunit"] = "";
            dr["itpkgqty"] = 1;
            dr["ittrait"] = 0;
            dr["memo"] = "";
            dtD.Rows.InsertAt(dr, index);
            dtD.AcceptChanges();
        }

        DataRow GetUnit(string no)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from item where itno=@itno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("itno", no);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable t = new DataTable();
                            da.Fill(t);
                            return t.Rows[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }




        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "特價取用方式")
            {
                TextBefore = dtD.Rows[e.RowIndex]["SpTraitName"].ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jSpecial.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SpNo.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                jSpecial.DataGridViewOpen<JBS.JS.Item>(sender, e, dtD, row => FillItem(row, e.RowIndex));
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                string tempitno = dtD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string tempunit = dataGridViewT1.EditingControl.Text.Trim();
                var p = GetUnit(tempitno);
                if (p != null)
                {
                    if (tempunit == p["ItUnit"].ToString())
                    {
                        dataGridViewT1.EditingControl.Text = p["ItUnitP"].ToString();
                        dtD.Rows[e.RowIndex]["ItUnit"] = p["ItUnitP"].ToString();
                        decimal d = p["itpkgqty"].ToDecimal();
                        dtD.Rows[e.RowIndex]["itpkgqty"] = d.ToString("f" + Common.Q);
                        dtD.Rows[e.RowIndex]["ItPrice"] = p["ItPriceP"];
                    }
                    else
                    {
                        dataGridViewT1.EditingControl.Text = p["ItUnit"].ToString();
                        dtD.Rows[e.RowIndex]["ItUnit"] = p["ItUnit"].ToString();
                        decimal d = 1;
                        dtD.Rows[e.RowIndex]["itpkgqty"] = d.ToString("f" + Common.Q);
                        dtD.Rows[e.RowIndex]["ItPrice"] = p["ItPrice"];
                    }
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "備註說明")
            {
                using (FrmSale_Memo frm = new FrmSale_Memo())
                {
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);
                            dtD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            break;
                        case DialogResult.Cancel: break;
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "特價取用方式")
            {
                if (dtD.Rows[e.RowIndex]["ItNo"].ToString().Length == 0) return;

                var itno = dtD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var itname = dtD.Rows[e.RowIndex]["itname"].ToString();
                var itprice = dtD.Rows[e.RowIndex]["itprice"].ToDecimal();

                using (FrmSpecOption Option = new FrmSpecOption(itno, itname, itprice))
                {
                    if ((Option.ShowDialog() == DialogResult.OK))
                    {
                        if (dataGridViewT1.EditingControl.IsNotNull()) dataGridViewT1.EditingControl.Text = Option.SpTraitName;
                        dtD.Rows[e.RowIndex]["SpTraitName"] = Option.SpTraitName;
                        dtD.Rows[e.RowIndex]["SpTrait"] = Option.SpTrait;
                        TextBefore = Option.SpTraitName;

                        dtD.Rows[e.RowIndex]["qty"] = Option.qty;
                        dtD.Rows[e.RowIndex]["prs"] = Option.prs;
                        dtD.Rows[e.RowIndex]["price"] = Option.price;
                        dtD.Rows[e.RowIndex]["point"] = Option.point;
                        dtD.Rows[e.RowIndex]["reason"] = Option.reason;
                        dtD.Rows[e.RowIndex]["GroupID"] = Option.groupid;
                        dtD.Rows[e.RowIndex]["singleprice"] = Option.singleprice;


                        if (Option.SpTrait.ToDecimal() == 1)
                        {
                            dtD.Rows[e.RowIndex]["qty"] = 1;
                            dtD.Rows[e.RowIndex]["Prs"] = 1;
                            dtD.Rows[e.RowIndex]["point"] = 0;
                        }
                        else if (Option.SpTrait.ToDecimal() == 2)
                        {
                            dtD.Rows[e.RowIndex]["qty"] = 1;
                            dtD.Rows[e.RowIndex]["Price"] = 0;
                            dtD.Rows[e.RowIndex]["point"] = 0;
                        }
                        else if (Option.SpTrait.ToDecimal() == 3)
                        {
                            dtD.Rows[e.RowIndex]["Price"] = 0;
                            dtD.Rows[e.RowIndex]["point"] = 0;
                        }
                        else if (Option.SpTrait.ToDecimal() == 4)
                        {
                            dtD.Rows[e.RowIndex]["Price"] = 0;
                            dtD.Rows[e.RowIndex]["point"] = 0;
                        }
                        else if (Option.SpTrait.ToDecimal() == 5)
                        {
                            dtD.Rows[e.RowIndex]["qty"] = 1;
                            dtD.Rows[e.RowIndex]["Prs"] = 1;
                        }
                        else if (Option.SpTrait.ToDecimal() == 6)
                        {
                            dtD.Rows[e.RowIndex]["qty"] = 1;
                            dtD.Rows[e.RowIndex]["Prs"] = 1;
                            dtD.Rows[e.RowIndex]["point"] = 0;
                        }
                        else if (Option.SpTrait.ToDecimal() == 7)
                        {
                            dtD.Rows[e.RowIndex]["price"] = 0;
                            dtD.Rows[e.RowIndex]["Prs"] = 1;
                            dtD.Rows[e.RowIndex]["point"] = 0;
                        }
                        else if (Option.SpTrait.ToDecimal() == 8)
                        {
                            dtD.Rows[e.RowIndex]["Prs"] = 1;
                            dtD.Rows[e.RowIndex]["point"] = 0;
                        }
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                }
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            int index = e.RowIndex;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoNow == "" || ItNoNow.Trim().Length == 0)  //空值->清空
                {
                    if (btnSave.Focused) return;
                    dtD.Rows[index]["itno"] = "";
                    dtD.Rows[index]["itnoudf"] = "";
                    dtD.Rows[index]["itname"] = "";
                    dtD.Rows[index]["qty"] = 1;
                    dtD.Rows[index]["prs"] = 1;
                    dtD.Rows[index]["price"] = 0;
                    dtD.Rows[index]["point"] = 0;
                    dtD.Rows[index]["spTrait"] = "";
                    dtD.Rows[index]["itprice"] = 0;
                    dtD.Rows[index]["itunit"] = "";
                    dtD.Rows[index]["itpkgqty"] = 1;
                    dtD.Rows[index]["ittrait"] = 0;
                    dtD.Rows[index]["memo"] = "";

                    ItNoBegin = "";
                    dataGridViewT1.InvalidateRow(index);
                    return;
                }

                if (ItNoNow == ItNoBegin)
                    return; //值沒變->離開

                if (ItNoNow != ItNoBegin)//值有變，但是跟自定編號一樣，視同沒變->離開  //把『自定編號』改成『產品編號』
                {
                    if (ItNoNow == UdfNoBegin)
                    {
                        dataGridViewT1["產品編號", e.RowIndex].Value = ItNoBegin;
                        return;
                    }
                }

                if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)  //值變了，跟產品編號和自定編號都不一樣,帶值出來  //若找不到這筆資料則開窗
                {
                    jSpecial.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtD, row => FillItem(row, e.RowIndex));
                    dataGridViewT1.InvalidateRow(index);
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                string tempitno = dtD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string tempunit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                if (tempunit == TextBefore) return;
                var p = GetUnit(tempitno);
                if (p != null)
                {
                    if (tempunit == p["ItUnit"].ToString())
                    {
                        dataGridViewT1.EditingControl.Text = p["ItUnit"].ToString();
                        dtD.Rows[e.RowIndex]["ItUnit"] = p["ItUnit"].ToString();
                        dtD.Rows[e.RowIndex]["Price"] = p["ItPrice"].ToString();
                        decimal d = 1;
                        dtD.Rows[e.RowIndex]["itpkgqty"] = d.ToString("f" + Common.Q);
                        dtD.Rows[e.RowIndex]["ItPrice"] = p["ItPrice"].ToString();
                    }
                    else
                    {
                        dataGridViewT1.EditingControl.Text = p["ItunitP"].ToString();
                        dtD.Rows[e.RowIndex]["ItUnit"] = p["ItunitP"].ToString();
                        dtD.Rows[e.RowIndex]["Price"] = p["ItPriceP"].ToString();
                        decimal d = p["itpkgqty"].ToDecimal();
                        dtD.Rows[e.RowIndex]["itpkgqty"] = d.ToString("f" + Common.Q);
                        dtD.Rows[e.RowIndex]["ItPrice"] = p["ItPriceP"].ToString();
                    }
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "特價取用方式")
            {
                if (dataGridViewT1["特價取用方式", e.RowIndex].EditedFormattedValue.ToString().Trim().Length == 0)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = "";
                    dtD.Rows[e.RowIndex]["SpTrait"] = "";
                    dtD.Rows[e.RowIndex]["SpTraitName"] = "";
                }
                else
                {
                    dtD.Rows[e.RowIndex]["SpTraitName"] = TextBefore;
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void FillItem(SqlDataReader reader, int index)
        {
            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = reader["ItNo"].ToString();

            dtD.Rows[index]["SpTrait"] = "";
            dtD.Rows[index]["SpTraitName"] = "";

            dtD.Rows[index]["ItNo"] = reader["ItNo"].ToString();
            dtD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            dtD.Rows[index]["ItName"] = reader["ItName"].ToString();
            dtD.Rows[index]["ItTrait"] = reader["ItTrait"].ToString();
            dtD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            string unitset = reader["ItSalUnit"].ToString().Trim();
            if (unitset == "1")
            {
                dtD.Rows[index]["Price"] = reader["ItPriceP"].ToString();
                dtD.Rows[index]["ItUnit"] = reader["ItUnitp"].ToString();
                dtD.Rows[index]["ItPkgqty"] = reader["ItPkgqty"].ToString();
                dtD.Rows[index]["ItPrice"] = reader["ItPriceP"].ToString();
            }
            else
            {
                dtD.Rows[index]["Price"] = reader["ItPrice"].ToString();
                dtD.Rows[index]["ItUnit"] = reader["ItUnit"].ToString();
                decimal d = 1;
                dtD.Rows[index]["ItPkgqty"] = d.ToString("f" + Common.Q);
                dtD.Rows[index]["ItPrice"] = reader["ItPrice"].ToString();
            }

            for (int x = 1; x <= 10; x++)
            {
                dtD.Rows[index]["ItDesp" + x] = reader["ItDesp" + x];
            }
        }

        ToolTip tip = new ToolTip();
        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "備註說明" || str == "特價取用方式")
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

        private void FrmSpecial_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D1 || keyData == Keys.NumPad1)
            {
                btnAppend.Focus();
                btnAppend.PerformClick();
            }
            else if (keyData == Keys.D2 || keyData == Keys.NumPad2)
            {
                btnModify.Focus();
                btnModify.PerformClick();
            }
            else if (keyData == Keys.D3 || keyData == Keys.NumPad3)
            {
                btnDelete.Focus();
                btnDelete.PerformClick();
            }
            else if (keyData == Keys.D4 || keyData == Keys.NumPad4)
            {
                btnBrow.Focus();
                btnBrow.PerformClick();
            }
            else if (keyData == Keys.F9)
            {
                btnSave.Focus();
                btnSave.PerformClick();
                return true;
            }
            else if (keyData == Keys.F4)
            {
                btnCancel.Focus();
                btnCancel.PerformClick();
            }
            else if (keyData == Keys.F2)
            {
                gridAppend.Focus();
                gridAppend.PerformClick();
            }
            else if (keyData == Keys.F3)
            {
                gridDelete.Focus();
                gridDelete.PerformClick();
            }
            else if (keyData == Keys.F5)
            {
                gridInsert.Focus();
                gridInsert.PerformClick();
            }
            else if (keyData == Keys.F6)
            {
                gridItDesp.Focus();
                gridItDesp.PerformClick();
            }
            else if (keyData == Keys.F8)
            {
                gridStock.Focus();
                gridStock.PerformClick();
            }
            else if (keyData == Keys.F10)
            {
                gridBshopHistory.Focus();
                gridBshopHistory.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SpNo_Validating(object sender, CancelEventArgs e)
        {
            if (SpNo.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            if (SpNo.TrimTextLenth() == 0 && SpNo.TextLength > 0)
            {
                e.Cancel = true;
                MessageBox.Show("編號不可輸入空白");
                return;
            }

            if (this.btnState == "Append" || this.btnState == "Duplicate")
            {
                if (jSpecial.IsExistDocument<JBS.JS.Special>(SpNo.Text.Trim()) == true)
                {
                    e.Cancel = true;
                    MessageBox.Show("編號重複!");
                    return;
                }
            }

            if (this.btnState == "Modify")
            {
                var tag = SpNo.Tag ?? "";
                if (SpNo.Text != tag.ToString())
                {
                    e.Cancel = true;
                    SpNo.Text = tag.ToString();
                    MessageBox.Show("編號不能修改!"); 
                    return;
                }
            }
        }

        private void SpNo_Enter(object sender, EventArgs e)
        {
            SpNo.Tag = SpNo.Text;
        }

        private void CuX1No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX01>(sender, row =>
            {
                CuX1No.Text = row["x1no"].ToString().Trim();
                CuX1Name.Text = row["x1name"].ToString().Trim();
            });
        }

        private void CuX1No_Validating(object sender, CancelEventArgs e)
        {
            if (CuX1No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuX1No.Text.Trim() == "")
            {
                CuX1No.Text = "";
                CuX1Name.Text = "";
                return;
            }
            
            xe.ValidateOpen<JBS.JS.XX01>(sender, e, row =>
            {
                CuX1No.Text = row["x1no"].ToString().Trim();
                CuX1Name.Text = row["x1name"].ToString().Trim();
            });
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender, reader =>        
            {
                CuNo.Text = reader["CuNo"].ToString();
                CuName1.Text = reader["cuname1"].ToString();
            });
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.Text.Trim().Length > 0)
            {
                string count = SQL.ExecuteScalar("select count(*) from cust where cuno = @cuno", new parameters("cuno", CuNo.Text.Trim()));
                if (count == "0")
                {
                    e.Cancel = true;
                    xe.Open<JBS.JS.Cust>(sender, reader =>
                    {
                        CuNo.Text = reader["CuNo"].ToString();
                        CuName1.Text = reader["cuname1"].ToString();
                    });
                }
            }
        }

    }
}
