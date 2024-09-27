using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmAdjust_Bom : Formbase
    {
        JBS.JS.xEvents xe;
        public string CallBack { get; set; }
        public DataTable table { get; set; }
        public string BomRec { get; set; }
        public string BoItNo1 { get; set; }
        public string BoItName1 { get; set; }
        public string adno { get; set; }
        public string rec { get; set; }

        public string pkey;
        public decimal Money;
        public bool JustForBrow = false;
        public bool FromADBrow = false;
        public bool FromQuote = false;
        public bool FromFquot = false;
        public DataGridView grid;
        List<TextBox> CostControl;
        string textBefore = "";

        #region 批次宣告
        BatchProcess BatchF = new BatchProcess();  //批次存資料異動修改
        public DataTable dt_Bom_BatchProcess;
        public DataRow 上層Row;
        public string 上層StnoO;
        public string FormName;
        #endregion

        public FrmAdjust_Bom()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pVar.SetMemoUdf(this.說明);
            CostControl = new List<TextBox> { BoTotMny };

            //數量
            BoTotQty.FirstNum = Common.nFirst;
            BoTotQty.LastNum = Common.Q;
            //金額
            BoTotMny.FirstNum = Common.nFirst;
            BoTotMny.LastNum = Common.MS;

            //string[] Num = new string[5];
            //Num[0] = "標準用量," + Common.iFirst + "," + Common.Q + ",0";
            //Num[1] = "母件比例," + Common.iFirst + ",4,0";
            //Num[2] = "單價," + Common.nFirst + "," + Common.MS + ",0";
            //Num[3] = "金額," + Common.nFirst + "," + Common.MS + ",0";
            //Num[4] = "包裝數量," + Common.nFirst + "," + Common.Q + ",0";
            //dataGridViewT1.限制輸入_列名_整數_小數_空值 = Num;

            this.標準用量.Set庫存數量小數();
            this.單價.Set銷貨單價小數();
            this.金額.Set銷貨單價小數();
            this.包裝數量.Set庫存數量小數();

            this.母件比例.FirstNum = Common.nFirst;
            this.母件比例.LastNum = 4;
            this.母件比例.DefaultCellStyle.Format = "f4";
             
            this.單價.Visible = this.金額.Visible = Common.User_ShopPrice;
            BoTotMny.Visible = Common.User_ShopPrice;

            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            this.BoItName.MaxLength = Common.Sys_ItNameLenth;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            this.BoItName.MaxLength = Common.Sys_ItNameLenth;
            

        }

        private void FrmAdiust_Bom_Load(object sender, EventArgs e)
        {
            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["序號"].ReadOnly = true;
            
            if (FromADBrow)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridInsert.Enabled = false;
                gridGetMny.Enabled = false;
                gridGetBomD.Enabled = false;
                dataGridViewT1.ReadOnly = true;

                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;

                LoadAdjuBom();
            }
            else if (JustForBrow)
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
                dataGridViewT1.DataSource = table;
                if (table.Rows.Count > 0) Summary();
            }

            #region 批次頁面開啟
            if (Common.Sys_UsingBatch == 2 && dt_Bom_BatchProcess != null)
                this.gridbatch.Visible = true;
            else
                this.gridbatch.Visible = false;
            if (dt_Bom_BatchProcess != null)
            {
                //dt_Bom_BatchProcess's value of column of row according to DataGridView's serial No. rewriteing again.
                for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
                {
                    int index = table.MultiThreadFindIndex("itrec", dt_Bom_BatchProcess.Rows[i]["rec"].ToString());
                    dt_Bom_BatchProcess.Rows[i]["rec"] = index + 1;
                }
            }
            #endregion
        }

        void LoadViewState()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "Select * from bomd where boitno=@boitno";
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("boitno", pkey);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            da.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                dataGridViewT1.DataSource = table;
                                Summary();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void LoadAdjuBom()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from adjubom where adno=@adno and bomrec=@bomrec";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("adno", adno);
                        cmd.Parameters.AddWithValue("bomrec", rec);
                        cmd.CommandText = sql;

                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            DataTable td = new DataTable();
                            dd.Fill(td);
                            dataGridViewT1.DataSource = td;
                            Summary();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        void Summary()
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                decimal d = 0, qty = 0, total = 0;

                for (int q = 0; q < dataGridViewT1.Rows.Count; q++)
                {
                    decimal.TryParse(dataGridViewT1["標準用量", q].Value.ToString(), out d);
                    d = Math.Round(d, Common.Q, MidpointRounding.AwayFromZero);
                    qty += d;
                }
                BoTotQty.Text = qty.ToString("f" + Common.Q);

                for (int t = 0; t < dataGridViewT1.Rows.Count; t++)
                {
                    decimal.TryParse(dataGridViewT1["金額", t].Value.ToString(), out d);
                    d = Math.Round(d, Common.MS, MidpointRounding.AwayFromZero);
                    total += d;
                }
                BoTotMny.Text = total.ToString("f" + Common.MS);
            }
            else
            {
                BoTotMny.Text = "0";
                BoTotQty.Text = "0";
            }
        }

        private void GridSaleDAddRows()
        {
            DataRow row = table.NewRow();
            row["bomrec"] = pkey;
            row["itno"] = "";
            row["itname"] = "";
            row["itunit"] = "";
            row["itqty"] = 0;
            row["itpareprs"] = 1;
            row["itprice"] = 0;
            row["itprs"] = 0;
            row["itmny"] = 0;
            row["itpkgqty"] = 0;
            row["itnote"] = "";


            table.Rows.Add(row);
            dataGridViewT1.DataSource = table;
        }

        private void GridSaleDInsertRows(int index)
        {
            DataRow row = table.NewRow();
            row["bomrec"] = pkey;
            row["itno"] = "";
            row["itname"] = "";
            row["itunit"] = "";
            row["itqty"] = 0;
            row["itpareprs"] = 1;
            row["itprice"] = 0;
            row["itprs"] = 0;
            row["itmny"] = 0;
            row["itpkgqty"] = 0;
            row["itnote"] = "";

            table.Rows.InsertAt(row, index);
            dataGridViewT1.DataSource = table;
        }

        void DelEmptyRow()
        {
            //var rows = table.AsEnumerable().Where(r => r["ItNo"].ToString() != "");
            //if (rows.Count() > 0)
            //    table = rows.CopyToDataTable();
            //else
            //    table.Clear();
            //零件不能是空值
            if (dt_Bom_BatchProcess != null)
            {
                #region 批次頁面開啟
                int 刪除次數 = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    if (table.Rows[i]["itno"].ToString().Trim().Length == 0)
                    {
                        BatchF.WhenFormBomDeleteRow(dt_Bom_BatchProcess, dataGridViewT1.Rows[i].Cells["序號"].Value.ToInteger() - 刪除次數);
                        table.Rows.RemoveAt(i--);
                        刪除次數++;
                    }

                }
                #endregion
            }
            else
            {
                #region 一般頁面
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["itno"].ToString().Trim().Length == 0)
                    {
                        table.Rows.RemoveAt(i--);
                    }
                }
                #endregion
            }
            table.AcceptChanges();
            dataGridViewT1.DataSource = table;

            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
                table.Rows[i]["ItRec"] = (i + 1).ToString();
            }
            Summary();
        }

        void CheckMny(int Index)
        {
            decimal _ItQty = 0;//標準用量
            decimal _ItPrice = 0;//單價
            decimal _ItPrs = 1;//母件比例
            decimal _ItPkgQty = 1;//包裝數量
            decimal _Mny = 0;
            decimal.TryParse(dataGridViewT1["標準用量", Index].Value.ToString(), out _ItQty);
            decimal.TryParse(dataGridViewT1["包裝數量", Index].Value.ToString(), out _ItPkgQty);
            decimal.TryParse(dataGridViewT1["單價", Index].Value.ToString(), out _ItPrice);
            decimal.TryParse(dataGridViewT1["母件比例", Index].Value.ToString(), out _ItPrs);

            _Mny = Math.Round((_ItQty * _ItPrice * _ItPkgQty) / _ItPrs, 4, MidpointRounding.AwayFromZero);
            dataGridViewT1["金額", Index].Value = _Mny.ToString();
            Summary();
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
                //刪除明細
                int index = dataGridViewT1.SelectedRows[0].Index;

                #region 批次頁面開啟
                if (dt_Bom_BatchProcess != null)
                {
                    //刪除批次異動資訊
                    BatchF.WhenFormBomDeleteRow(dt_Bom_BatchProcess, dataGridViewT1.Rows[index].Cells["序號"].Value.ToInteger());
                }
                #endregion

                table.Rows.RemoveAt(index);
                table.AcceptChanges();

                

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
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
            }
        }

        private void gridGetMny_Click(object sender, EventArgs e)
        {
            //零件不能是空值
            DelEmptyRow();
            table.AcceptChanges();
            CallBack = "Money";
            Money = 0;
            decimal.TryParse(BoTotMny.Text.Trim(), out Money);
            int index = grid.SelectedRows[0].Index;
            if (FromQuote)
            {
                grid.CurrentCell = grid["售價", index];
            }
            else if (FromFquot)
            {
                grid.CurrentCell = grid["進價", index];
            }
            else
                grid.CurrentCell = grid["成本", index];
            this.DialogResult = DialogResult.OK;
        }

        private void gridGetBomD_Click(object sender, EventArgs e)
        {
            //零件不能是空值
            DelEmptyRow();
            table.AcceptChanges();
            CallBack = "Bom";
            this.DialogResult = DialogResult.OK;
        }

        private void gridExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0 && dataGridViewT1.ReadOnly == false)
            {
                GridSaleDAddRows();
                if (dataGridViewT1.Rows.Count > 0)
                    dataGridViewT1[0, 0].Value = 1;
                return;
            }
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;
            switch (dataGridViewT1.Columns[e.ColumnIndex].Name)
            {
                case "產品編號":
                    var Source = dataGridViewT1.DataSource;
                    var tempD = Source == null ? null : (DataTable)Source;

                    if (tempD != null)
                    {
                        xe.DataGridViewOpen<JBS.JS.Item>(sender, e, tempD, row =>
                        {
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = row["ItNo"].ToString();

                            tempD.Rows[e.RowIndex]["itno"] = row["ItNo"].ToString();
                            tempD.Rows[e.RowIndex]["itname"] = row["ItName"].ToString();
                            tempD.Rows[e.RowIndex]["itunit"] = row["ItUnit"].ToString();
                            tempD.Rows[e.RowIndex]["ItPrice"] = row["ItPrice"].ToDecimal();
                            tempD.Rows[e.RowIndex]["itpkgqty"] = "1";
                            tempD.Rows[e.RowIndex]["ItParePrs"] = "1";
                            BatchF.刪除批次異動(dt_Bom_BatchProcess, dataGridViewT1.Rows[e.RowIndex].Cells["序號"].Value.ToString());
                        });

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    break;
                     

                case "單位":
                    string itno = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    string str = dataGridViewT1.SelectedRows[0].Cells["單位"].Value.ToString();
                    try
                    {
                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        {
                            cn.Open();
                            string sql = "select * from item where itno =@itno";
                            using (SqlCommand cmd = new SqlCommand(sql, cn))
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("itno", itno.Trim());
                                SqlDataReader dr = cmd.ExecuteReader();
                                if (dr.HasRows)
                                {
                                    dr.Read();

                                    if (dr["itunitp"].ToString() != "")
                                    {
                                        if (dr["itunitp"].ToString() != str)
                                        {
                                            if (dataGridViewT1.EditingControl != null)
                                                dataGridViewT1.EditingControl.Text = dr["itunitp"].ToString();
                                            dataGridViewT1.SelectedRows[0].Cells["單位"].Value = dr["itunitp"].ToString();
                                            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                            dataGridViewT1.SelectedRows[0].Cells["包裝數量"].Value = dr["itpkgqty"].ToString();
                                            CheckMny(e.RowIndex);
                                        }
                                        else
                                        {
                                            if (dataGridViewT1.EditingControl != null)
                                                dataGridViewT1.EditingControl.Text = dr["itunit"].ToString();
                                            dataGridViewT1.SelectedRows[0].Cells["單位"].Value = dr["itunit"].ToString();
                                            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                            dataGridViewT1.SelectedRows[0].Cells["包裝數量"].Value = "1";
                                            CheckMny(e.RowIndex);
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }

                                    dr.Close(); dr.Dispose();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    break;
            }
        }

        string CellValue = "";
        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (gridDelete.Focused || gridExit.Focused) return;
            if (dataGridViewT1.ReadOnly == true) return;

            CellValue = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString();

            switch (dataGridViewT1.Columns[e.ColumnIndex].Name)
            {
                case "產品編號":
                    if (CellValue == BoItNo.Text.Trim() && BoItNo.Text.Trim() != "")
                    {
                        e.Cancel = true;
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = "";
                        dataGridViewT1["產品編號", e.RowIndex].Value = "";
                        dataGridViewT1["品名規格", e.RowIndex].Value = "";
                        dataGridViewT1["單位", e.RowIndex].Value = "";
                        dataGridViewT1["標準用量", e.RowIndex].Value = "0";
                        dataGridViewT1["母件比例", e.RowIndex].Value = "1";
                        dataGridViewT1["單價", e.RowIndex].Value = "0";
                        dataGridViewT1["金額", e.RowIndex].Value = "0";
                        dataGridViewT1["包裝數量", e.RowIndex].Value = "1";
                        dataGridViewT1["說明", e.RowIndex].Value = "";
                        MessageBox.Show("組件明細不可與組件編號相同", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (CellValue == "")
                        {
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = "";
                            dataGridViewT1["產品編號", e.RowIndex].Value = "";
                            dataGridViewT1["品名規格", e.RowIndex].Value = "";
                            dataGridViewT1["單位", e.RowIndex].Value = "";
                            dataGridViewT1["標準用量", e.RowIndex].Value = "0";
                            dataGridViewT1["母件比例", e.RowIndex].Value = "1";
                            dataGridViewT1["單價", e.RowIndex].Value = "0";
                            dataGridViewT1["金額", e.RowIndex].Value = "0";
                            dataGridViewT1["包裝數量", e.RowIndex].Value = "1";
                            dataGridViewT1["說明", e.RowIndex].Value = "";
                            BatchF.刪除批次異動(dt_Bom_BatchProcess, dataGridViewT1.Rows[e.RowIndex].Cells["序號"].Value.ToString());
                            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }
                        else
                        {
                            var Source = dataGridViewT1.DataSource;
                            var tempD = Source == null ? null : (DataTable)Source;

                            if (tempD != null)
                            {
                                xe.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, tempD, row =>
                                {
                                    if (dataGridViewT1.EditingControl != null)
                                    {
                                        if (dataGridViewT1.EditingControl.Text.Trim() != textBefore)//如果產品編號改變，就會把批次資料刪掉
                                            BatchF.刪除批次異動(dt_Bom_BatchProcess, dataGridViewT1.Rows[e.RowIndex].Cells["序號"].Value.ToString());
                                        dataGridViewT1.EditingControl.Text = row["ItNo"].ToString();
                                    }
                                    tempD.Rows[e.RowIndex]["itno"] = row["ItNo"].ToString();
                                    tempD.Rows[e.RowIndex]["itname"] = row["ItName"].ToString();
                                    tempD.Rows[e.RowIndex]["itunit"] = row["ItUnit"].ToString();
                                    tempD.Rows[e.RowIndex]["ItPrice"] = row["ItPrice"].ToDecimal();
                                    tempD.Rows[e.RowIndex]["itpkgqty"] = "1";
                                    tempD.Rows[e.RowIndex]["ItParePrs"] = "1";
                                });

                                dataGridViewT1.InvalidateRow(e.RowIndex);
                            }
                            else
                            {
                                e.Cancel = true;
                            }
                             
                        }
                    }
                    CheckMny(e.RowIndex);
                    break;
                case "標準用量":
                    dataGridViewT1["標準用量", e.RowIndex].Value = dataGridViewT1["標準用量", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    CheckMny(e.RowIndex);
                    break;
                case "母件比例":
                    dataGridViewT1["母件比例", e.RowIndex].Value = dataGridViewT1["母件比例", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (CellValue.IsZeroOrEmpty())
                    {
                        e.Cancel = true;
                        decimal d = 0;
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = d.ToString();
                        dataGridViewT1["母件比例", e.RowIndex].Value = d;
                        if (dataGridViewT1.EditingControl != null)
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
                case "金額":
                    dataGridViewT1["金額", e.RowIndex].Value = dataGridViewT1["金額", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    CheckMny(e.RowIndex);
                    break;
                case "包裝數量":
                    dataGridViewT1["包裝數量", e.RowIndex].Value = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (CellValue.IsZeroOrEmpty())
                    {
                        e.Cancel = true;
                        decimal d = 0;
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = d.ToString();
                        dataGridViewT1["包裝數量", e.RowIndex].Value = d;
                        if (dataGridViewT1.EditingControl != null)
                            ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                        MessageBox.Show("包裝數量不可為0或是空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F6:
                    gridStk.Focus();
                    gridStk.PerformClick();
                    break;
                case Keys.F7:
                    gridGetMny.Focus();
                    gridGetMny.PerformClick();
                    break;
                case Keys.F8:
                    gridGetBomD.Focus();
                    gridGetBomD.PerformClick();
                    break;
                case Keys.F11:
                    gridExit.Focus();
                    gridExit.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.產品編號.ReadOnly == false)
            {
                if (this.dataGridViewT1.Columns[e.ColumnIndex].Name == this.產品編號.Name)
                {
                    textBefore = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim();
                }
            }
        }

        private void gridbatch_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.CurrentCell == null) return;
            int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
            if (dataGridViewT1.Rows[Index_gv].Cells["產品編號"].Value.ToString().Trim().Length == 0) return;

            //銷/退/領料之作業，只有組合品可以勾稽批次資料；入庫時都可以
            if (FormName != "FrmGarner" && 上層Row["ItTrait"].ToString() != "1") return;
            //勾稽明細
            string BomRec = 上層Row["BomRec"].ToString();
            //勾稽bom，因為Bom的itrec會跟著序號動態改變所以這邊是抓序號；原本有table資料的話，他的itrec一開始就會重新編輯
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
                dr["StNO"] = 上層StnoO;
                dr["ItName"] = 上層Row["ItName"].ToString();
                dr["ItUnit"] = 上層Row["ItUnit"].ToString();
                string 庫存量 = SQL.ExecuteScalar("SELECT top 1 StNoQty FROM BatchStock where stno ='" + 上層StnoO + "' and bno ='" + dt_Bom_BatchProcess.Rows[Index]["bno"].ToString() + "'");
                if (庫存量 == "") 庫存量 = "0";
                dr["StNoQty"] = 庫存量;
                dr["rec"] = rec;
                dr["BomRec"] = 上層Row["BomRec"].ToString();
                temp.Rows.Add(dr);
            }



            using (Batch frm = new Batch(this.FormName, temp, true))
            {

                frm._Qty = (上層Row["qty"].ToDecimal() * 上層Row["itpkgqty"].ToDecimal() * (table.Rows[Index_gv]["itqty"].ToDecimal() * table.Rows[Index_gv]["itpkgqty"].ToDecimal() / table.Rows[Index_gv]["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q).ToString();
                frm._itno = table.Rows[Index_gv]["itno"].ToString();
                frm._itname = table.Rows[Index_gv]["itname"].ToString();
                //BOM的倉庫是帶入單據明細的倉庫
                frm._stno = 上層StnoO;
                frm._stname = SQL.ExecuteScalar("SELECT stname FROM STKROOM where stno = '" + 上層StnoO + "'");
                frm._bomid = table.Rows[Index_gv]["bomid"].ToString();
                frm._rec = rec;
                frm._BomRec = BomRec;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.Yes)
                {
                    //更新時先刪除ROW
                    //if (index > -1)
                    //    dt_Bom_BatchProcess.Rows.RemoveAt(index);
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        int Index = list[i];
                        dt_Bom_BatchProcess.Rows.RemoveAt(Index);
                    }
                    //再插入ROW
                    dt_Bom_BatchProcess.Merge(temp);
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
                    string CurrentItNo = table.Rows[index]["itno"].ToString().Trim();
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
