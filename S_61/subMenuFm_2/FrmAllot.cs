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
    public partial class FrmAllot : Formbase
    {
        JBS.JS.Allot jAllot;
        List<ButtonGridT> GridButton;

        DataTable AlD = new DataTable(); //明細
        DataTable AlBom = new DataTable();//組件
        DataTable tempBom = new DataTable();//組件暫存
        DataTable tempAlD = new DataTable();//用來記錄 修改&複製狀態一進來原始的明細

        List<TextBoxbase> list;

        decimal BomRec = 0;
        string ItNoBegin = "";//紀錄編輯格上一次的編號
        string UdfNoBegin = "";
        string Memo1 = "";//詳細備註

        public FrmAllot()
        {
            InitializeComponent();
            this.jAllot = new JBS.JS.Allot();
            this.list = this.getEnumMember();

            pVar.SetMemoUdf(this.備註說明);

            this.調撥數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            GridButton = new List<ButtonGridT> { gridAppend, gridDelete, gridPicture, gridInsert, gridItDesp, gridBomD};

            if (Common.User_DateTime == 1) AlDate.MaxLength = 7;
            else AlDate.MaxLength = 8;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
            dataGridViewT1.DataSource = AlD;
        }

        private void FrmAllot_Load(object sender, EventArgs e)
        {
            GridButton.ForEach(r => r.Enabled = false);

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
                cmd.CommandText = "select *,ItNoUdf = '' from allotd where 1=0";

                dd.Fill(AlD);
                dd.Fill(tempAlD);
            }

            var pk = jAllot.Bottom();
            writeToTxt(pk);
        }

        private void FrmAllot_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string alno)
        {
            BomRec = 0;

            var result = jAllot.LoadData(alno, row =>
            {
                AlNo.Text = row["Alno"].ToString();

                if (Common.User_DateTime == 1)
                    AlDate.Text = row["Aldate"].ToString().Trim();
                else
                    AlDate.Text = row["Aldate1"].ToString().Trim();

                StNoI.Text = row["stnoi"].ToString().Trim();
                StNameI.Text = row["stnamei"].ToString().Trim();
                StNoO.Text = row["stnoo"].ToString().Trim();
                StNameO.Text = row["stnameo"].ToString().Trim();
                EmNo.Text = row["emno"].ToString().Trim();
                EmName.Text = row["emname"].ToString().Trim();
                AlMemo.Text = row["Almemo"].ToString().Trim();

                loadD();

                this.Memo1 = row["Almemo1"].ToString();
                jAllot.keyMan.Set(row);

            });

            if (!result)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                AlD.Clear();
                AlBom.Clear();
                tempAlD.Clear();
                tempBom.Clear();

                this.Memo1 = "";
                jAllot.keyMan.Clear();
            }
        }

        void loadD()
        {
            AlD.Clear();
            tempAlD.Clear();
            string sql = "select *,ItNoUdf= (select top 1 itnoudf from item where item.itno = allotd.itno) from allotd where alno=@alno ORDER BY recordno";

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
                cmd.CommandText = sql;

                dd.Fill(AlD);
                dd.Fill(tempAlD);

                dataGridViewT1.DataSource = AlD;

                if (AlD.Rows.Count > 0)
                    BomRec = AlD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
            }
        }

        void loadBom()
        {
            AlBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select * from allobom where 1=0 ";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from allobom where alno=@alno";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from allobom where alno=@alno";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("alno", jAllot.GetCurrent());
                    cmd.CommandText = sql;

                    da.Fill(AlBom);
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
            AlDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            string sql = "select stname from stkroom where stno=@stno";
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("stno", StNoO.Text.Trim());
                        cmd.CommandText = sql;
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
            DataRow dr = AlD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["itunit"] = "";
            dr["qty"] = 0;
            dr["itpkgqty"] = 1;
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            dr["ittrait"] = 0;
            AlD.Rows.Add(dr);
            AlD.AcceptChanges();
        }

        void AddRow(int index)
        {
            DataRow dr = AlD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["itunit"] = "";
            dr["qty"] = 0;
            dr["itpkgqty"] = 1;
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            dr["ittrait"] = 0;
            AlD.Rows.InsertAt(dr, index);
            AlD.AcceptChanges();
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
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
                //刪除組件明細
                var rec = AlD.Rows[index]["BomRec"].ToString();
                jAllot.RemoveBom(rec, ref AlBom);

                //刪除明細
                AlD.Rows[index].Delete();
                AlD.AcceptChanges();

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
                frm.dr = AlD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            var ittrait = dataGridViewT1.SelectedRows[0].Cells["ittrait"].EditedFormattedValue.ToString().Trim();
            if (ittrait != "1" && ittrait != "2")
            {
                MessageBox.Show("只有組合品與組裝品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridViewT1.Focus();
                return;
            }
            string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString();
            DataTable table = AlBom.Clone();

            for (int i = 0; i < AlBom.Rows.Count; i++)
            {
                if (AlBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                {
                    table.ImportRow(AlBom.Rows[i]);
                    AlBom.Rows.RemoveAt(i--);
                }
            }

            AlBom.AcceptChanges();
            table.AcceptChanges();

            using (var frm = new FrmDraw_Bom())
            { 
                frm.table = table.Copy();
                frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                frm.BomRec = rec;
                frm.grid = dataGridViewT1;
                frm.上層Row = AlD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        if (frm.CallBack == "Bom")
                        {
                            AlBom.Merge(frm.table);
                            AlBom.AcceptChanges();
                            dataGridViewT1.Focus();
                            table.Clear();
                        }
                        break;
                    case DialogResult.Cancel:
                        AlBom.Merge(table);
                        AlBom.AcceptChanges();
                        dataGridViewT1.Focus();
                        table.Clear();
                        break;
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

        private void gridBshopHistory_Click(object sender, EventArgs e)
        {
            gridBshopHistory.Focus();

            if (jAllot.EnableBShopPrice() == false)
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
            var itno = AlD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }



        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jAllot.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jAllot.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jAllot.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jAllot.Bottom();
            writeToTxt(pk);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            GridButton.ForEach(r => r.Enabled = true);
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            AlD.Clear();
            loadBom();

            this.Memo1 = "";
            this.BomRec = 0;

            StNoO.Text = Common.User_StkNo;
            SetStName();

            AlNo.Text = "";
            AlDate.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            loadBom();

            GridButton.ForEach(r => r.Enabled = true);
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            AlDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            AlNo.Text = "";
            AlDate.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jAllot.IsExistDocument<JBS.JS.Allot>(AlNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            
            if (jAllot.IsEditInCloseDay(AlDate.Text) == false)
                return;

            if (jAllot.IsModify<JBS.JS.Allot>(AlNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jAllot.upModify1<JBS.JS.Allot>(AlNo.Text.Trim());//更新修改狀態1
                var pk = jAllot.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            loadBom();

            GridButton.ForEach(r => r.Enabled = true);
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            AlDate.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jAllot.IsExistDocument<JBS.JS.Allot>(AlNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jAllot.IsModify<JBS.JS.Allot>(AlNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jAllot.IsEditInCloseDay(AlDate.Text) == false)
                return;
            jAllot.GetTempBomOnDeleting("Allobom", AlNo.Text.Trim(), ref tempBom);
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
                    cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
                    cmd.CommandText = @"
                        delete from allot where alno=@alno;
                        delete from allotd where alno=@alno;
                        delete from Allobom where alno=@alno;";
                    cmd.ExecuteNonQuery();

                    jAllot.加庫存(cmd, tempAlD, tempBom, "stnoo");
                    jAllot.扣庫存(cmd, tempAlD, tempBom, "stnoi");

                    tn.Commit();

                    jAllot.UpdateItemItStockQty(tempAlD, tempBom);

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
            using (var frm = new FrmAllot_Print())
            {
                frm.PK = AlNo.Text.Trim();
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
            using (var frm = new FrmAllotBrow())
            {  
                frm.TSeekNo = AlNo.Text;
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (jAllot.IsEditInCloseDay(AlDate.Text) == false)
                return;

            if (StNoI.Text.Trim() == "")
            {
                MessageBox.Show("撥入倉庫不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNoI.Focus();
                return;
            }

            if (StNoI.Text == StNoO.Text)
            {
                MessageBox.Show("撥入與撥出倉庫不可相同", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNoI.Focus();
                return;
            }

            jAllot.RemoveEmptyRowOnSaving(dataGridViewT1, ref AlD, ref AlBom, () => { });

            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < AlD.Rows.Count; i++)
            {
                AlD.Rows[i]["StNoO"] = StNoO.Text.Trim();
                AlD.Rows[i]["StNoI"] = StNoI.Text.Trim();
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

                        var result = true;
                        result &= jAllot.GetPkNumber<JBS.JS.Allot>(cmd, AlDate.Text, ref AlNo);

                        if (!result)
                        {
                            if (tn != null)
                                tn.Rollback();

                            MessageBox.Show("單號取得失敗!");
                            return;
                        }

                        AppendMasterOnSaving(cmd);

                        for (int i = 0; i < AlD.Rows.Count; i++)
                        {
                            AppendDetailOnSaving(cmd, i);
                        }

                        AppendBomOnSaving(cmd);

                        jAllot.加庫存(cmd, AlD, AlBom, "stnoi");
                        jAllot.扣庫存(cmd, AlD, AlBom, "stnoo");

                        tn.Commit();

                        jAllot.Save(AlNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jAllot.UpdateItemItStockQty(AlD, AlBom);
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
                    using (var frm = new FrmAllot_Print())
                    {
                        frm.PK = jAllot.GetCurrent();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jAllot.IsExistDocument<JBS.JS.Allot>(AlNo.Text.Trim()) == false)
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

                        //刪除主檔 重新寫回主檔
                        UpdateMasterOnSaving(cmd);

                        //刪除明細 重新寫回明細
                        UpdateDetail(cmd);

                        //刪除組件明細 重新寫回組件明細
                        UpdateBom(cmd);

                        //處理庫存 
                        jAllot.加庫存(cmd, tempAlD, tempBom, "stnoo");
                        jAllot.扣庫存(cmd, tempAlD, tempBom, "stnoi");

                        jAllot.加庫存(cmd, AlD, AlBom, "stnoi");
                        jAllot.扣庫存(cmd, AlD, AlBom, "stnoo");

                        //完成重要資料存檔, 確認交易
                        tn.Commit();

                        //儲存完成
                        jAllot.Save(AlNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jAllot.UpdateItemItStockQty(tempAlD, tempBom, AlD, AlBom);
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
                    using (var frm = new FrmAllot_Print())
                    {
                        frm.PK = jAllot.GetCurrent();
                        frm.ShowDialog();
                    }
                }
                jAllot.upModify0<JBS.JS.Allot>(AlNo.Text.Trim());//改回0為無修改狀態
                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
            }
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
            cmd.Parameters.AddWithValue("aldate", Date.ToTWDate(AlDate.Text));
            cmd.Parameters.AddWithValue("aldate1", Date.ToUSDate(AlDate.Text));
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text.Trim());
            cmd.Parameters.AddWithValue("stnameo", StNameO.Text.Trim());
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text.Trim());
            cmd.Parameters.AddWithValue("stnamei", StNameI.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("almemo", AlMemo.Text);
            cmd.Parameters.AddWithValue("bracket", "調撥");
            cmd.Parameters.AddWithValue("almemo1", Memo1);
            cmd.Parameters.AddWithValue("recordno", AlD.Rows.Count);
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.CommandText = "insert into allot"
                + "(alno,aldate,aldate1,stnoo,stnameo,stnoi,stnamei"
                + ",emno,emname,almemo,bracket,almemo1,recordno,appdate,edtdate,appscno,edtscno) values "
                + "(@alno,@aldate,@aldate1,@stnoo,@stnameo,@stnoi,@stnamei"
                + ",@emno,@emname,@almemo,@bracket,@almemo1,@recordno,@appdate,@edtdate,@appscno,@edtscno)";
            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("Alno", AlNo.Text.Trim());
            cmd.Parameters.AddWithValue("Aldate", Date.ToTWDate(AlDate.Text));
            cmd.Parameters.AddWithValue("Aldate1", Date.ToUSDate(AlDate.Text));
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text.Trim());
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("itno", AlD.Rows[i]["itno"]);
            cmd.Parameters.AddWithValue("itname", AlD.Rows[i]["itname"]);
            cmd.Parameters.AddWithValue("ittrait", AlD.Rows[i]["ittrait"]);
            cmd.Parameters.AddWithValue("itunit", AlD.Rows[i]["itunit"]);
            cmd.Parameters.AddWithValue("itpkgqty", AlD.Rows[i]["itpkgqty"]);
            cmd.Parameters.AddWithValue("qty", AlD.Rows[i]["qty"]);
            cmd.Parameters.AddWithValue("memo", AlD.Rows[i]["memo"]);
            cmd.Parameters.AddWithValue("bomid", AlNo.Text + AlD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", AlD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1));
            cmd.Parameters.AddWithValue("bracket", "調撥");
            cmd.Parameters.AddWithValue("itdesp1", AlD.Rows[i]["itdesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", AlD.Rows[i]["itdesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", AlD.Rows[i]["itdesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", AlD.Rows[i]["itdesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", AlD.Rows[i]["itdesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", AlD.Rows[i]["itdesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", AlD.Rows[i]["itdesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", AlD.Rows[i]["itdesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", AlD.Rows[i]["itdesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", AlD.Rows[i]["itdesp10"]);
            cmd.CommandText = "insert into allotd"
                + "(Alno,Aldate,Aldate1,stnoo,stnoi,emno,itno,itname,ittrait"
                + ",itunit,itpkgqty,qty,memo"
                + ",bomid,bomrec,recordno"
                + ",bracket"
                + ",itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                + ",itdesp6,itdesp7,itdesp8,itdesp9,itdesp10"
                + ") values "
                + " (@Alno,@Aldate,@Aldate1,@stnoo,@stnoi,@emno,@itno,@itname,@ittrait"
                + ",@itunit,@itpkgqty,@qty,@memo"
                + ",@bomid,@bomrec,@recordno"
                + ",@bracket"
                + ",@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                + ",@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10) ";

            cmd.ExecuteNonQuery();
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < AlBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("alno", AlNo.Text);
                cmd.Parameters.AddWithValue("bomid", AlNo.Text + AlBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", AlBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", AlBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", AlBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", AlBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", AlBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", AlBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", AlBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", (i + 1));
                cmd.Parameters.AddWithValue("itprice", AlBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", AlBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", AlBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", AlBom.Rows[i]["itnote"]);
                cmd.CommandText = "insert into allobom"
                    + "(alno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty"
                    + ",itrec,itprice,itprs,itmny,itnote"
                    + ") values "
                    + " (@alno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty"
                    + ",@itrec,@itprice,@itprs,@itmny,@itnote) ";
                cmd.ExecuteNonQuery();
            }
        }
        private void UpdateMasterOnSaving(SqlCommand cmd)
        {
            //刪除主檔 重新寫回主檔
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
            cmd.CommandText = "delete from allot where alno=@alno";
            cmd.ExecuteNonQuery();

            //刪除主檔 重新寫回主檔
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
            cmd.Parameters.AddWithValue("aldate", Date.ToTWDate(AlDate.Text));
            cmd.Parameters.AddWithValue("aldate1", Date.ToUSDate(AlDate.Text));
            cmd.Parameters.AddWithValue("stnoo", StNoO.Text.Trim());
            cmd.Parameters.AddWithValue("stnameo", StNameO.Text.Trim());
            cmd.Parameters.AddWithValue("stnoi", StNoI.Text.Trim());
            cmd.Parameters.AddWithValue("stnamei", StNameI.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text);
            cmd.Parameters.AddWithValue("almemo", AlMemo.Text);
            cmd.Parameters.AddWithValue("bracket", "調撥");
            cmd.Parameters.AddWithValue("almemo1", Memo1);
            cmd.Parameters.AddWithValue("recordno", AlD.Rows.Count);
            cmd.Parameters.AddWithValue("appdate", jAllot.keyMan.AppendTime);
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", jAllot.keyMan.AppendMan);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);

            cmd.CommandText = "insert into allot"
                + "(alno,aldate,aldate1,stnoo,stnameo,stnoi,stnamei"
                + ",emno,emname,almemo,bracket,almemo1,recordno,appdate,edtdate,appscno,edtscno) values "
                + "(@alno,@aldate,@aldate1,@stnoo,@stnameo,@stnoi,@stnamei"
                + ",@emno,@emname,@almemo,@bracket,@almemo1,@recordno,@appdate,@edtdate,@appscno,@edtscno)";
            cmd.ExecuteNonQuery();
        }
        private void UpdateDetail(SqlCommand cmd)
        {
            //刪除明細
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
            cmd.CommandText = "delete from allotd where alno=@alno";
            cmd.ExecuteNonQuery();
            //重新寫回明細
            for (int i = 0; i < AlD.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Alno", AlNo.Text.Trim());
                cmd.Parameters.AddWithValue("Aldate", Date.ToTWDate(AlDate.Text));
                cmd.Parameters.AddWithValue("Aldate1", Date.ToUSDate(AlDate.Text));
                cmd.Parameters.AddWithValue("stnoo", StNoO.Text.Trim());
                cmd.Parameters.AddWithValue("stnoi", StNoI.Text.Trim());
                cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                cmd.Parameters.AddWithValue("itno", AlD.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", AlD.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("ittrait", AlD.Rows[i]["ittrait"]);
                cmd.Parameters.AddWithValue("itunit", AlD.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itpkgqty", AlD.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("qty", AlD.Rows[i]["qty"]);
                cmd.Parameters.AddWithValue("memo", AlD.Rows[i]["memo"]);
                cmd.Parameters.AddWithValue("bomid", AlNo.Text + AlD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", AlD.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("recordno", (i + 1));
                cmd.Parameters.AddWithValue("bracket", "調撥");
                cmd.Parameters.AddWithValue("itdesp1", AlD.Rows[i]["itdesp1"]);
                cmd.Parameters.AddWithValue("itdesp2", AlD.Rows[i]["itdesp2"]);
                cmd.Parameters.AddWithValue("itdesp3", AlD.Rows[i]["itdesp3"]);
                cmd.Parameters.AddWithValue("itdesp4", AlD.Rows[i]["itdesp4"]);
                cmd.Parameters.AddWithValue("itdesp5", AlD.Rows[i]["itdesp5"]);
                cmd.Parameters.AddWithValue("itdesp6", AlD.Rows[i]["itdesp6"]);
                cmd.Parameters.AddWithValue("itdesp7", AlD.Rows[i]["itdesp7"]);
                cmd.Parameters.AddWithValue("itdesp8", AlD.Rows[i]["itdesp8"]);
                cmd.Parameters.AddWithValue("itdesp9", AlD.Rows[i]["itdesp9"]);
                cmd.Parameters.AddWithValue("itdesp10", AlD.Rows[i]["itdesp10"]);

                cmd.CommandText = "insert into allotd"
                    + "(Alno,Aldate,Aldate1,stnoo,stnoi,emno,itno,itname,ittrait"
                    + ",itunit,itpkgqty,qty,memo"
                    + ",bomid,bomrec,recordno"
                    + ",bracket"
                    + ",itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                    + ",itdesp6,itdesp7,itdesp8,itdesp9,itdesp10"
                    + ") values "
                    + " (@Alno,@Aldate,@Aldate1,@stnoo,@stnoi,@emno,@itno,@itname,@ittrait"
                    + ",@itunit,@itpkgqty,@qty,@memo"
                    + ",@bomid,@bomrec,@recordno"
                    + ",@bracket"
                    + ",@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                    + ",@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10) ";

                cmd.ExecuteNonQuery();

            }
        }
        private void UpdateBom(SqlCommand cmd)
        {
            //刪除組件明細
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
            cmd.CommandText = "delete from allobom where alno=@alno";
            cmd.ExecuteNonQuery();
            //重新寫回組件明細
            for (int i = 0; i < AlBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("alno", AlNo.Text);
                cmd.Parameters.AddWithValue("bomid", AlNo.Text + AlBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", AlBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", AlBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", AlBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", AlBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", AlBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", AlBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", AlBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", (i + 1));
                cmd.Parameters.AddWithValue("itprice", AlBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", AlBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", AlBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", AlBom.Rows[i]["itnote"]);
                cmd.CommandText = "insert into allobom"
                    + "(alno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty"
                    + ",itrec,itprice,itprs,itmny,itnote"
                    + ") values "
                    + " (@alno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty"
                    + ",@itrec,@itprice,@itprs,@itmny,@itnote) ";
                cmd.ExecuteNonQuery();
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jAllot.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            GridButton.ForEach(r => r.Enabled = false);
            dataGridViewT1.ReadOnly = true;
            btnAppend.Focus();
            jAllot.upModify0<JBS.JS.Allot>(AlNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewT1["序號", e.RowIndex].Value = e.RowIndex + 1;
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
                if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name.ToString() != "產品編號")
                return;

            ItNoBegin = UdfNoBegin = "";
            ItNoBegin = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

            if (ItNoBegin == "")
                return;

            jAllot.Validate<JBS.JS.Item>(ItNoBegin, reader =>
            {
                ItNoBegin = reader["itno"].ToString().Trim();
                UdfNoBegin = reader["itnoudf"].ToString().Trim();
            });
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                jAllot.DataGridViewOpen<JBS.JS.Item>(sender, e, AlD, reader =>
                {
                    //if (row["ittrait"].ToDecimal() == 1)
                    //{
                    //    MessageBox.Show("組合品無法調撥", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    //this.ItNoBegin = row["itno"].ToString().Trim();

                    //if (dataGridViewT1.EditingControl != null)
                    //    dataGridViewT1.EditingControl.Text = row["itno"].ToString().Trim();

                    //AlD.Rows[e.RowIndex]["itno"] = row["itno"].ToString().Trim();
                    //AlD.Rows[e.RowIndex]["itname"] = row["itname"].ToString();
                    //AlD.Rows[e.RowIndex]["itunit"] = row["itunit"].ToString().Trim();
                    //AlD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    //dataGridViewT1["ittrait", e.RowIndex].Value = row["ittrait"].ToString().Trim();
                    //for (int i = 1; i <= 10; i++)
                    //{
                    //    AlD.Rows[e.RowIndex]["itdesp" + i] = row["itdesp" + i].ToString();
                    //}

                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = reader["itno"].ToString().Trim();

                    AlD.Rows[e.RowIndex]["itno"] = reader["itno"].ToString().Trim();
                    AlD.Rows[e.RowIndex]["itname"] = reader["itname"].ToString();
                    AlD.Rows[e.RowIndex]["itunit"] = reader["itunit"].ToString();
                    AlD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    AlD.Rows[e.RowIndex]["ittrait"] = reader["ittrait"].ToString();

                    for (int i = 1; i <= 10; i++)
                    {
                        AlD.Rows[e.RowIndex]["itdesp" + i] = reader["itdesp" + i].ToString();
                    }

                    var rec = AlD.Rows[e.RowIndex]["BomRec"].ToString();
                    jAllot.RemoveBom(rec, ref AlBom);

                    if (dataGridViewT1["ittrait", e.RowIndex].Value.ToString() != "3")
                        jAllot.GetItemBom(reader["itno"].ToString().Trim(), rec, ref AlBom);
                   
                });
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "備註說明")
            {
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        AlD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                var itno = AlD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jAllot.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        AlD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            AlD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            AlD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;

                AlD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoNow.Length == 0)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = "";

                    AlD.Rows[e.RowIndex]["itno"] = "";
                    AlD.Rows[e.RowIndex]["itname"] = "";
                    AlD.Rows[e.RowIndex]["itunit"] = "";
                    AlD.Rows[e.RowIndex]["qty"] = 0;
                    AlD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    AlD.Rows[e.RowIndex]["memo"] = "";
                    AlD.Rows[e.RowIndex]["ittrait"] = 0;

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    ItNoBegin = UdfNoBegin = "";


                    var rec = AlD.Rows[e.RowIndex]["BomRec"].ToString();
                    jAllot.RemoveBom(rec, ref AlBom);
                    return;
                }

                if (ItNoNow == ItNoBegin)
                    return;

                if (ItNoNow == UdfNoBegin && ItNoNow.Length > 0)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    AlD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jAllot.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, AlD, reader => FillItem(reader, e));

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                string itno = dataGridViewT1["產品編號", e.RowIndex].Value.ToString();

                jAllot.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        AlD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        AlD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                AlD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void FillItem(SqlDataReader reader, DataGridViewCellValidatingEventArgs e)
        {
            //if (reader["ittrait"].ToDecimal() == 1)
            //{
            //    e.Cancel = true;
            //    MessageBox.Show("組合品無法調撥", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    if (dataGridViewT1.EditingControl != null)
            //        dataGridViewT1.EditingControl.Text = "";

            //    AlD.Rows[e.RowIndex]["itno"] = "";
            //    return;
            //}


            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = reader["itno"].ToString().Trim();

            AlD.Rows[e.RowIndex]["itno"] = reader["itno"].ToString().Trim();
            AlD.Rows[e.RowIndex]["itname"] = reader["itname"].ToString();
            AlD.Rows[e.RowIndex]["itunit"] = reader["itunit"].ToString();
            AlD.Rows[e.RowIndex]["itpkgqty"] = 1;
            AlD.Rows[e.RowIndex]["ittrait"] = reader["ittrait"].ToString();
            AlD.Rows[e.RowIndex]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            for (int i = 1; i <= 10; i++)
            {
                AlD.Rows[e.RowIndex]["itdesp" + i] = reader["itdesp" + i].ToString();
            }

            var rec = AlD.Rows[e.RowIndex]["BomRec"].ToString();
            jAllot.RemoveBom(rec, ref AlBom);

            if (dataGridViewT1["ittrait", e.RowIndex].Value.ToString() != "3")
                jAllot.GetItemBom(reader["itno"].ToString().Trim(), rec, ref AlBom);
        }

        private void AlDate_Validating(object sender, CancelEventArgs e)
        {
            if (AlDate.ReadOnly == true)
                return;

            if (btnCancel.Focused)
                return;

            jAllot.DateValidate(sender, e);
        }

        private void StNoI_DoubleClick(object sender, EventArgs e)
        {
            if (sender.Equals(StNoI))
            {
                jAllot.Open<JBS.JS.Stkroom>(sender, reader =>
                {
                    StNoI.Text = reader["StNo"].ToString().Trim();
                    StNameI.Text = reader["StName"].ToString().Trim();
                });
            }
            else
            {
                jAllot.Open<JBS.JS.Stkroom>(sender, reader =>
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

            string str = ((TextBox)sender).Text.Trim();
            if (str == "")
            {
                if (((TextBox)sender).Name == "StNoI")
                {
                    StNoI.Text = "";
                    StNameI.Text = "";
                    return;
                }
                else
                {
                    StNameO.Text = "";
                    MessageBox.Show("倉庫編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    ((TextBox)sender).Focus();
                    return;
                }
            }

            if (sender.Equals(StNoI))
            {
                jAllot.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
                {
                    StNoI.Text = reader["StNo"].ToString().Trim();
                    StNameI.Text = reader["StName"].ToString().Trim();
                });
            }
            else
            {
                jAllot.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
                {
                    StNoO.Text = reader["StNo"].ToString().Trim();
                    StNameO.Text = reader["StName"].ToString().Trim();
                });
            }

            if (Common.keyData != Keys.Up)
            {
                if (((TextBox)sender).Name == "StNoO")
                    if (dataGridViewT1.Rows.Count == 0)
                        if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jAllot.Open<JBS.JS.Empl>(sender, reader =>
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

            jAllot.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();

            }, true);
        }

        private void AlMemo_DoubleClick(object sender, EventArgs e)
        {
            if (AlMemo.ReadOnly == true)
                return;

            using (var frm = new FrmSale_Memo())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        AlMemo.Text = frm.Memo.GetUTF8(60);
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
        }

        private void AlNo_Enter(object sender, EventArgs e)
        {
            AlNo.Tag = AlNo.Text.Trim();
        }

        private void AlNo_DoubleClick(object sender, EventArgs e)
        {
            if (AlNo.ReadOnly)
                return;

            using (var frm = new FrmAllot_Print_AlNo())
            { 
                frm.TSeekNo = AlNo.Text.Trim();

                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        AlNo.Text = frm.TResult.Trim();
                        break;
                }
            }
        }

        private void AlNo_Validating(object sender, CancelEventArgs e)
        {
            if (AlNo.ReadOnly || btnCancel.Focused) return;

            if (AlNo.Text.Length > 0 && AlNo.Text.Trim() == "")
            {
                e.Cancel = true;
                AlNo.Text = "";
                AlNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jAllot.IsExistDocument<JBS.JS.Allot>(AlNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jAllot.IsExistDocument<JBS.JS.Allot>(AlNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jAllot.IsExistDocument<JBS.JS.Allot>(AlNo.Text.Trim()))
                {
                    if (AlNo.Text.Trim() == (AlNo.Tag ?? "").ToString())
                        return;

                    writeToTxt(AlNo.Text.Trim());
                    loadBom();
                }
                else
                {
                    e.Cancel = true;
                    AlNo.SelectAll();

                    using (var frm = new FrmAllot_Print_AlNo())
                    { 
                        frm.TSeekNo = AlNo.Text.Trim();

                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                AlNo.Text = frm.TResult.Trim();
                                writeToTxt(AlNo.Text.Trim());
                                loadBom();
                                break; 
                        }
                    }
                }
            }
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
            if (AlNo.Text.Trim() == "")
                return;

            using (FrmSale_AppScNo frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jAllot.keyMan.AppendMan;
                frm.ATime = jAllot.keyMan.AppendTime;
                //修改人員
                frm.EName = jAllot.keyMan.EditMan;
                frm.ETime = jAllot.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (var frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = AlNo.ReadOnly ? false : true;
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

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
