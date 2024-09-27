using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Collections.Generic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSale_Bom : Formbase
    {
        public string CallBack { get; set; }
        public DataTable dtD = new DataTable();
        public string BomRec { get; set; }
        public string BoItNo1 { get; set; }
        public string BoItName1 { get; set; }

        public string TTable;
        public string TKey;
        public decimal Money;
        public bool JustForBrow = false;

        public DataGridView grid;

        string ItNoBegin = "";

        JBS.JS.xEvents xe;

        #region 批次宣告
        BatchProcess BatchF = new BatchProcess();  //批次存資料異動修改
        public DataTable dt_Bom_BatchProcess;
        public DataRow 上層Row;
        public string FormName;
        #endregion
        public FrmSale_Bom()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            pVar.SetMemoUdf(this.說明);

            BoTotQty.FirstNum = Common.nFirst;
            BoTotQty.LastNum = Common.Q;
            BoTotMny.FirstNum = Common.nFirst;
            BoTotMny.LastNum = Common.MS;

            this.標準用量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.母件比例.FirstNum = Common.nFirst;
            this.母件比例.LastNum = 4;
            this.母件比例.DefaultCellStyle.Format = "f4";
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.單價.Set銷貨單價小數();
            this.金額.Set銷貨單價小數();

            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            this.BoItName.MaxLength = Common.Sys_ItNameLenth;

        }

        private void FrmSale_Bom_Load(object sender, EventArgs e)
        {
            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["序號"].ReadOnly = true;
            dataGridViewT1.Columns["金額"].ReadOnly = true;

            //權限設定
            if (!Common.User_SalePrice || !Common.User_ShopPrice)
            {
                dataGridViewT1.Columns["單價"].Visible = false;
                dataGridViewT1.Columns["金額"].Visible = false;
                BoTotMny.Visible = false;
            }

            if (JustForBrow)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridInsert.Enabled = false;
                gridGetMny.Enabled = false;
                gridGetBomD.Enabled = false;
                dataGridViewT1.ReadOnly = true;
                //載入表頭
                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;

                LoadViewState();
            }
            else
            {
                //載入表頭
                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;
                //載入明細
                dataGridViewT1.DataSource = dtD;
                Summary();
            }

            #region 批次頁面開啟 
            if (Common.Sys_UsingBatch == 2 && dt_Bom_BatchProcess != null)
                this.gridbatch.Visible = true;
            else
                this.gridbatch.Visible = false;


            //dtD 是bom表 SALEBOM
            //dt_Bom_BatchProcess BOM表組件批號

            if (dt_Bom_BatchProcess != null)
            {
                //dt_Bom_BatchProcess's value of column of row according to DataGridView's serial No. rewriteing again.
                for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
                {
                    int index = dtD.MultiThreadFindIndex("itrec", dt_Bom_BatchProcess.Rows[i]["rec"].ToString());
                    dt_Bom_BatchProcess.Rows[i]["rec"] = index + 1;
                }
            }
            #endregion
            dataGridView1.DataSource = dt_Bom_BatchProcess;
        }

        void LoadViewState()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = conn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                    cmd.CommandText = "Select * from " + TTable.Trim() + " where BomID=@TKey";

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewT1.DataSource = dt;
                        Summary();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //明細
        private void GridSaleDAddRows()
        {
            DataRow row = dtD.NewRow();
            row["BomID"] = TKey;
            row["BomRec"] = BomRec;
            row["itno"] = "";
            row["itname"] = "";
            row["itunit"] = "";
            row["itqty"] = 0;
            row["itpareprs"] = 1;
            row["itprice"] = 0;
            row["itprs"] = 1;
            row["itmny"] = 0;
            row["itpkgqty"] = 0;
            row["itnote"] = "";

            dtD.Rows.Add(row);
            dataGridViewT1.DataSource = dtD;
        }

        private void GridSaleDInsertRows(int index)
        {
            DataRow row = dtD.NewRow();
            row["BomID"] = TKey;
            row["BomRec"] = BomRec;
            row["itno"] = "";
            row["itname"] = "";
            row["itunit"] = "";
            row["itqty"] = 0;
            row["itpareprs"] = 1;
            row["itprice"] = 0;
            row["itprs"] = 1;
            row["itmny"] = 0;
            row["itpkgqty"] = 0;
            row["itnote"] = "";

            dtD.Rows.InsertAt(row, index);
            dataGridViewT1.DataSource = dtD;
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0 && dataGridViewT1.ReadOnly == false)
            {
                GridSaleDAddRows();
                return;
            }
        }

        void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["itno"].ToString().Trim();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;

            dtD.Rows[index]["itno"] = itno;
            dtD.Rows[index]["itname"] = reader["itname"];
            dtD.Rows[index]["ItUnit"] = reader["ItUnit"];
            dtD.Rows[index]["itpkgqty"] = 1;
            dtD.Rows[index]["ItPrice"] = reader["ItPrice"];
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                xe.DataGridViewOpen<JBS.JS.Item>(sender, e, dtD, row => FillItem(row, e.RowIndex));

                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                var itno = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                xe.Validate<JBS.JS.Item>(itno, reader =>
                {
                    if (unit == reader["itunit"].ToString())
                    {
                        unit = reader["itunitp"].ToString();
                        dtD.Rows[e.RowIndex]["itpkgqty"] = reader["itpkgqty"];
                        dtD.Rows[e.RowIndex]["itprice"] = reader["itpricep"];
                    }
                    else
                    {
                        unit = reader["itunit"].ToString();
                        dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        dtD.Rows[e.RowIndex]["itprice"] = reader["itprice"];
                    }
                });

                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;

            if (gridDelete.Focused || gridExit.Focused)
                return;

            var cellValue = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim();
            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                try
                {
                    #region 產品編號
                    if (cellValue == ItNoBegin)
                        return;

                    if (cellValue == BoItNo.Text.Trim() && BoItNo.Text.Trim() != "")
                    {
                        e.Cancel = true;
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = "";
                        for (int i = 0; i < dtD.Columns.Count; i++)
                        {
                            dtD.Rows[e.RowIndex][i] = DBNull.Value;
                        }
                        dtD.Rows[e.RowIndex]["BomID"] = TKey;
                        dtD.Rows[e.RowIndex]["BomRec"] = BomRec;
                        dtD.Rows[e.RowIndex]["ItQty"] = 0;
                        dtD.Rows[e.RowIndex]["ItParePrs"] = 1;
                        dtD.Rows[e.RowIndex]["ItPrice"] = 0;
                        dtD.Rows[e.RowIndex]["ItPrs"] = 1;
                        dtD.Rows[e.RowIndex]["ItMny"] = 0;
                        dtD.Rows[e.RowIndex]["ItPkgQty"] = 1;

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        MessageBox.Show(
                            "組件明細不可與組件編號相同",
                            "訊息視窗",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (cellValue == "")
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = "";
                        for (int i = 0; i < dtD.Columns.Count; i++)
                        {
                            dtD.Rows[e.RowIndex][i] = DBNull.Value;
                        }
                        dtD.Rows[e.RowIndex]["BomID"] = TKey;
                        dtD.Rows[e.RowIndex]["BomRec"] = BomRec;
                        dtD.Rows[e.RowIndex]["ItQty"] = 0;
                        dtD.Rows[e.RowIndex]["ItParePrs"] = 1;
                        dtD.Rows[e.RowIndex]["ItPrice"] = 0;
                        dtD.Rows[e.RowIndex]["ItPrs"] = 1;
                        dtD.Rows[e.RowIndex]["ItMny"] = 0;
                        dtD.Rows[e.RowIndex]["ItPkgQty"] = 1;

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }

                    xe.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtD, row => FillItem(row, e.RowIndex));
                    #endregion
                }
                finally
                {
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    CheckMny(e.RowIndex);
                }
                return;
            }

            switch (dataGridViewT1.Columns[e.ColumnIndex].Name)
            {
                case "標準用量":
                    dataGridViewT1["標準用量", e.RowIndex].Value = dataGridViewT1["標準用量", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    CheckMny(e.RowIndex);
                    break;
                case "母件比例":
                    dataGridViewT1["母件比例", e.RowIndex].Value = dataGridViewT1["母件比例", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (cellValue.IsZeroOrEmpty())
                    {
                        e.Cancel = true;
                        decimal d = 0;
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = d.ToString();
                        dataGridViewT1["母件比例", e.RowIndex].Value = d;
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                        MessageBox.Show("母件比例不可為0或是空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    CheckMny(e.RowIndex);
                    break;
                case "單價":
                    dataGridViewT1["單價", e.RowIndex].Value = dataGridViewT1["單價", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    CheckMny(e.RowIndex);
                    break;
                case "折數":
                    dataGridViewT1["折數", e.RowIndex].Value = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    CheckMny(e.RowIndex);
                    break;
                case "包裝數量":
                    dataGridViewT1["包裝數量", e.RowIndex].Value = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (cellValue.IsZeroOrEmpty())
                    {
                        e.Cancel = true;
                        decimal d = 0;
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = d.ToString();
                        dataGridViewT1["包裝數量", e.RowIndex].Value = d;
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                        MessageBox.Show("包裝數量不可為0或是空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }
            //#region 批次頁面開啟
            //if (dt_Bom_BatchProcess != null)
            //{
            //    if (dataGridViewT1.Columns[e.ColumnIndex].Name == "標準用量"
            //     || dataGridViewT1.Columns[e.ColumnIndex].Name == "母件比例"
            //     || dataGridViewT1.Columns[e.ColumnIndex].Name == "包裝數量")
            //    {
            //        int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
            //        if (dataGridViewT1.Rows[Index_gv].Cells["產品編號"].Value.ToString().Trim().Length == 0) return;
            //        //銷貨退回時，只有組合品可以勾稽批次資料
            //        if (上層Row["ItTrait"].ToString() != "1") return;
            //        //勾稽明細
            //        string BomRec = 上層Row["BomRec"].ToString();
            //        //勾稽bom，因為Bom的itrec會跟著序號動態改變所以這邊是抓序號；原本有table資料的話，他的itrec一開始就會重新編輯
            //        string rec = dataGridViewT1.Rows[Index_gv].Cells["序號"].Value.ToString();
            //        int index = dt_Bom_BatchProcess.MultiThreadFindIndex("BomRec", BomRec, "rec", rec);
            //        if (index > -1)
            //            dt_Bom_BatchProcess.Rows[index]["qty"] = (上層Row["qty"].ToDecimal() * 上層Row["itpkgqty"].ToDecimal() * (dtD.Rows[Index_gv]["itqty"].ToDecimal() * dtD.Rows[Index_gv]["itpkgqty"].ToDecimal() / dtD.Rows[Index_gv]["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q).ToString();
            //    }
            //}
            //#endregion
        }

        void CheckMny(int Index)
        {
            decimal _ItQty = 0;//標準用量
            decimal _ItPrice = 0;//單價
            decimal _ItDisc = 0;//折數
            decimal _ItPrs = 1;//母件比例
            decimal _Mny = 0;
            decimal.TryParse(dataGridViewT1["標準用量", Index].Value.ToString(), out _ItQty);
            decimal.TryParse(dataGridViewT1["單價", Index].Value.ToString(), out _ItPrice);
            decimal.TryParse(dataGridViewT1["折數", Index].Value.ToString(), out _ItDisc);
            decimal.TryParse(dataGridViewT1["母件比例", Index].Value.ToString(), out _ItPrs);

            _Mny = Math.Round((_ItQty * _ItPrice * _ItDisc) / _ItPrs, 4, MidpointRounding.AwayFromZero);
            dataGridViewT1["金額", Index].Value = _Mny.ToString();
            Summary();
        }

        void Summary()
        {
            decimal qty = 0;
            decimal total = 0;

            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                qty += dataGridViewT1["標準用量", i].EditedFormattedValue.ToDecimal("f" + Common.Q);
                total += dataGridViewT1["金額", i].EditedFormattedValue.ToDecimal("f" + Common.MS);
            }

            BoTotQty.Text = qty.ToString("f" + Common.Q);
            BoTotMny.Text = total.ToString("f" + Common.MS);
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
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
                int index = dataGridViewT1.SelectedRows[0].Index;
                #region 批次頁面開啟
                if (dt_Bom_BatchProcess != null)
                {
                    //刪除批次異動資訊
                    BatchF.WhenFormBomDeleteRow(dt_Bom_BatchProcess, dataGridViewT1.Rows[index].Cells["序號"].Value.ToInteger());
                }
                #endregion
                //刪除明細
                dtD.Rows.RemoveAt(index);
                dtD.AcceptChanges();

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                }

                Summary();//刪除列後，重新計算帳款

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", index];
                    dataGridViewT1.Rows[index].Selected = true;
                }

            }
            dataGridViewT1.Focus();
        }

        private void gridPic_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows.Count > 0)
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
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
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells["產品編號"];
                    dataGridViewT1.CurrentRow.Selected = true;
                    #region 批次頁面開啟
                    if (dt_Bom_BatchProcess != null)
                    {
                        //插入批次異動資訊
                        BatchF.WhenFormBomInsertRow(dt_Bom_BatchProcess, dataGridViewT1.Rows[index].Cells["序號"].Value.ToInteger());
                    }
                    #endregion
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridStk_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                using (FrmSale_Stock frm = new FrmSale_Stock())
                {
                    frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                    frm.ShowDialog();
                }
            }
        }

        private void gridGetMny_Click(object sender, EventArgs e)
        {
            //零件不能是空值
            if (dt_Bom_BatchProcess != null)
            {
                #region 批次頁面開啟
                int 刪除次數 = 0;
                for (int i = 0; i < dtD.Rows.Count; i++)
                {

                    if (dtD.Rows[i]["itno"].ToString().Trim().Length == 0)
                    {
                        BatchF.WhenFormBomDeleteRow(dt_Bom_BatchProcess, dataGridViewT1.Rows[i].Cells["序號"].Value.ToInteger() - 刪除次數);
                        dtD.Rows.RemoveAt(i--);
                        刪除次數++;
                    }

                }
                #endregion
            }
            else
            {
                #region 一般頁面
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    if (dtD.Rows[i]["itno"].ToString().Trim().Length == 0)
                    {
                        dtD.Rows.RemoveAt(i--);
                    }
                }
                #endregion
            }
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
                dtD.Rows[i]["ItRec"] = (i + 1).ToString();
            }
            Summary();
            dtD.AcceptChanges();

            CallBack = "Money";
            Money = 0;
            decimal.TryParse(BoTotMny.Text.Trim(), out Money);
            int index = grid.SelectedRows[0].Index;
            if (grid.Columns["售價"] == null)
                grid.CurrentCell = grid["進價", index];
            else
                grid.CurrentCell = grid["售價", index];
            this.DialogResult = DialogResult.OK;
        }

        private void gridGetBomD_Click(object sender, EventArgs e)
        {
            //零件不能是空值
            if (dt_Bom_BatchProcess != null)
            {
                #region 批次頁面開啟
                int 刪除次數 = 0;
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                   
                    if (dtD.Rows[i]["itno"].ToString().Trim().Length == 0)
                    {
                        BatchF.WhenFormBomDeleteRow(dt_Bom_BatchProcess, dataGridViewT1.Rows[i].Cells["序號"].Value.ToInteger() - 刪除次數);
                        dtD.Rows.RemoveAt(i--);
                        刪除次數++;
                    }

                }
                #endregion
            }
            else
            {
                #region 一般頁面
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    if (dtD.Rows[i]["itno"].ToString().Trim().Length == 0)
                    {
                        dtD.Rows.RemoveAt(i--);
                    }
                }
                #endregion
            }
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
                dtD.Rows[i]["ItRec"] = (i + 1).ToString();
            }
            Summary();
            dtD.AcceptChanges();

            CallBack = "Bom";
            this.DialogResult = DialogResult.OK;
        }

        private void gridExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.Equals(Keys.F6) && gridStk.Enabled)
            {
                gridStk.Focus();
                gridStk.PerformClick();
            }
            else if (keyData.Equals(Keys.F7) && gridGetMny.Enabled)
            {
                gridGetMny.Focus();
                gridGetMny.PerformClick();
            }
            else if (keyData.Equals(Keys.F8) && gridGetBomD.Enabled)
            {
                gridGetBomD.Focus();
                gridGetBomD.PerformClick();
            }
            else if (keyData.Equals(Keys.F4) && gridExit.Enabled)
            {
                gridExit.Focus();
                gridExit.PerformClick();
            }
            else if (keyData.Equals(Keys.F2) && gridAppend.Enabled)
            {
                gridAppend.Focus();
                gridAppend_Click(null, null);
            }
            else if (keyData.Equals(Keys.F3) && gridDelete.Enabled)
            {
                gridDelete.Focus();
                gridDelete_Click(null, null);
            }
            else if (keyData.Equals(Keys.F5) && gridInsert.Enabled)
            {
                gridInsert.Focus();
                gridInsert_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void gridbatch_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.CurrentCell == null) return;
            int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
            if (dataGridViewT1.Rows[Index_gv].Cells["產品編號"].Value.ToString().Trim().Length == 0) return;
            //銷/退或作業，只有組合品可以勾稽批次資料
            if (上層Row["ItTrait"].ToString() != "1") return;
            //勾稽明細
            string BomRec = 上層Row["BomRec"].ToString();
            //勾稽bom，因為Bom的itrec會跟著序號動態改變所以這邊是抓序號；原本table有資料的話，它的itrec一開始就會重新編輯
            string rec = dataGridViewT1.Rows[Index_gv].Cells["序號"].Value.ToString();

            DataTable temp = dt_Bom_BatchProcess.Clone();
            var list = dt_Bom_BatchProcess.FindIndexToList("BomRec", BomRec, "rec", rec);
            for (int k = 0; k < list.Count; k++)
            {
                int Index = list[k];
                DataRow dr = temp.NewRow();
                for (int i = 0; i < dt_Bom_BatchProcess.Columns.Count; i++)
                {
                    string column = dt_Bom_BatchProcess.Columns[i].ColumnName.ToString();
                    string value = dt_Bom_BatchProcess.Rows[Index][column].ToString();
                    dr[column] = value;
                }
                dr["序號"] = (k + 1).ToString();
                dr["修改批號"] = "刪除";
                dr["StNO"] = 上層Row["StNO"].ToString();
                dr["ItName"] = 上層Row["ItName"].ToString();
                dr["ItUnit"] = 上層Row["ItUnit"].ToString();
                dr["FaName"] = SQL.ExecuteScalar("select top 1 faname1 from fact where fano = '" + dr["fano"].ToString() + "'");
                string 庫存量 = SQL.ExecuteScalar("SELECT top 1 StNoQty FROM BatchStock where stno ='" + 上層Row["StNo"].ToString() + "' and bno ='" + dt_Bom_BatchProcess.Rows[Index]["bno"].ToString() + "'");
                if (庫存量 == "") 庫存量 = "0";
                dr["StNoQty"] = 庫存量;
                dr["rec"] = rec;
                dr["BomRec"] = 上層Row["BomRec"].ToString();
                temp.Rows.Add(dr);
            }

            using (Batch frm = new Batch(this.FormName, temp,true))
            {
                frm._Qty = (上層Row["qty"].ToDecimal() * 上層Row["itpkgqty"].ToDecimal() * (dtD.Rows[Index_gv]["itqty"].ToDecimal() * dtD.Rows[Index_gv]["itpkgqty"].ToDecimal() / dtD.Rows[Index_gv]["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q).ToString();
                frm._itno = dtD.Rows[Index_gv]["itno"].ToString();
                frm._itname = dtD.Rows[Index_gv]["itname"].ToString();
                //BOM的倉庫是帶入單據明細的倉庫
                frm._stno = 上層Row["stno"].ToString();
                frm._stname = 上層Row["stname"].ToString();
                frm._bomid = dtD.Rows[Index_gv]["bomid"].ToString();
                frm._rec = rec;
                frm._BomRec = BomRec;
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Yes)
                {
                    //更新時先刪除ROW
                    //if (index > -1)
                    //    dt_Bom_BatchProcess.Rows.RemoveAt(index);
                    //更新時先刪除之前的ROW
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        int Index = list[i];
                        dt_Bom_BatchProcess.Rows.RemoveAt(Index);
                    }

                    //再插入ROW
                    dt_Bom_BatchProcess.Merge(temp);

                    //寫回DataGridView
                    //dtD.Rows[Index_gv]["stno"] = temp.Rows[0]["stno"].ToString();
                    //dtD.Rows[Index_gv]["stname"] = temp.Rows[0]["stname"].ToString();
                }
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
                else
                {
                    string 上層ItNo = 上層Row["itno"].ToString();
                    string CurrentItNo = dtD.Rows[index]["itno"].ToString().Trim();
                    if (CurrentItNo.Length > 0)
                    {
                        parameters par = new parameters();
                        par.AddWithValue("明細Itno", 上層ItNo);
                        par.AddWithValue("子階Itno", CurrentItNo);

                        using (DataTable TempDt = new DataTable())
                        {
                            SQL.ExecuteNonQuery("SELECT top 1 * FROM BOMD WHERE BoItno = @明細Itno and Itno = @子階Itno", par, TempDt);
                            if (TempDt.Rows.Count > 0)
                            {
                                frm.dr = TempDt.Rows[0];
                            }
                            else
                            {
                                //加一條空的
                                DataRow dr = TempDt.NewRow();
                                TempDt.Rows.Add(dr);
                            }
                            frm.ShowDialog();
                        }
                    }
                }
            }
            dataGridViewT1.Focus();
        }

    }
}
