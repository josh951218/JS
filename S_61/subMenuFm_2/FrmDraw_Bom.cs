using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmDraw_Bom : Formbase
    {
        JBS.JS.xEvents xe;
        public string CallBack { get; set; }
        public DataTable table { get; set; }
        public string BomRec { get; set; }
        public string BoItNo1 { get; set; }
        public string BoItName1 { get; set; }
        public string BomID { get; set; }
        public string DrNo { get; set; }
        public string[] str;

        public bool JustForBrow = false;
        public bool FormGarnerBrow = false;
        public bool FormQuoteBrow = false;
        public bool FormFQuotBrow = false;
        public bool together = false;
        string textBefore = "";

        public DataGridView grid;
        public DataRow 上層Row;

        public FrmDraw_Bom()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pVar.SetMemoUdf(this.說明);
            //數量
            BoTotQty.FirstNum = Common.nFirst;
            BoTotQty.LastNum = Common.Q;

            //string[] Num = new string[3];
            //Num[0] = "標準用量," + Common.iFirst + "," + Common.Q + ",0";
            //Num[1] = "母件比例," + Common.iFirst + ",4,0";
            //Num[2] = "包裝數量," + Common.nFirst + "," + Common.Q + ",0";
            //dataGridViewT1.限制輸入_列名_整數_小數_空值 = Num;

            this.標準用量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.母件比例.DefaultCellStyle.Format = "f4";
            this.母件比例.FirstNum = 1;
            this.母件比例.LastNum = 4;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            this.BoItName.MaxLength = Common.Sys_ItNameLenth;
        }

        private void FrmDraw_Bom_Load(object sender, EventArgs e)
        {
            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["序號"].ReadOnly = true;
            
            if (JustForBrow)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridInsert.Enabled = false;
                gridGetBomD.Enabled = false;

                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;
                load();
            }
            else if (FormGarnerBrow)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridInsert.Enabled = false;
                gridGetBomD.Enabled = false;

                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;
                loadforgarner();
            }
            else if (FormQuoteBrow)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridInsert.Enabled = false;
                gridGetBomD.Enabled = false;

                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;
                loadforquote();
            }
            else if (FormFQuotBrow)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridInsert.Enabled = false;
                gridGetBomD.Enabled = false;
                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;
                loadforfquot();
            }
            else if (together)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridInsert.Enabled = false;
                gridGetBomD.Enabled = false;
                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;
                loadtogether();
            }
            else
            {
                //載入表頭
                BoItNo.Text = BoItNo1;
                BoItName.Text = BoItName1;
                //載入明細
                dataGridViewT1.DataSource = table;
            }
            Summary();

        }

        void load()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select * from drawbom where DrNo=@DrNo and BomID=@BomID";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("DrNo", DrNo);
                        cmd.Parameters.AddWithValue("BomID", BomID);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            dd.Fill(tb);
                            dataGridViewT1.DataSource = tb;
                            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                            {
                                dataGridViewT1["序號", i].Value = (i + 1).ToString();
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

        void loadforgarner()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select * from garnbom where gano=@gano and BomID=@BomID";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("gano", DrNo);
                        cmd.Parameters.AddWithValue("BomID", BomID);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            dd.Fill(tb);
                            dataGridViewT1.DataSource = tb;
                            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                            {
                                dataGridViewT1["序號", i].Value = (i + 1).ToString();
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

        void loadforquote()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select * from quotebom where quno=@quno and BomID=@BomID ";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("quno", DrNo);
                        cmd.Parameters.AddWithValue("BomID", BomID);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            dd.Fill(tb);
                            dataGridViewT1.DataSource = tb;
                            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                            {
                                dataGridViewT1["序號", i].Value = (i + 1).ToString();
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

        void loadforfquot()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select * from fquotbom where fqno=@fqno and BomID=@BomID";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("fqno", DrNo);
                        cmd.Parameters.AddWithValue("BomID", BomID);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            dd.Fill(tb);
                            dataGridViewT1.DataSource = tb;
                            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                            {
                                dataGridViewT1["序號", i].Value = (i + 1).ToString();
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

        void loadtogether()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select * from " + str[0] + " where " + str[1] + "=@drno and BomID=@BomID";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("drno", DrNo);
                        cmd.Parameters.AddWithValue("BomID", BomID);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            dd.Fill(tb);
                            dataGridViewT1.DataSource = tb;
                            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                            {
                                dataGridViewT1["序號", i].Value = (i + 1).ToString();
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

        void Summary()
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                decimal d = 0, qty = 0;

                for (int q = 0; q < dataGridViewT1.Rows.Count; q++)
                {
                    decimal.TryParse(dataGridViewT1.Rows[q].Cells["標準用量"].Value.ToString(), out d);
                    d = Math.Round(d, Common.Q, MidpointRounding.AwayFromZero);
                    qty += d;
                }
                BoTotQty.Text = qty.ToString("f" + Common.Q);
            }
            else
            {
                BoTotQty.Text = "0";
            }
        }

        private void GridSaleDAddRows()
        {
            DataRow row = table.NewRow();
            row["bomrec"] = BomRec;
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
            row["bomrec"] = BomRec;
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
            var rows = table.AsEnumerable().ToList().FindAll(r => !r["itno"].IsNullOrEmpty());
            if (rows.Count > 0)
                table = rows.CopyToDataTable();
            else
                table.Clear();

            table.AcceptChanges();
            dataGridViewT1.DataSource = table;

            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
                table.Rows[i]["ItRec"] = (i + 1).ToString();
            }
            Summary();
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            gridAppend.Focus();
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["產品編號", i].Value.ToString().Trim() == "")
                {
                    dataGridViewT1.Focus();
                    return;
                }
            }
            GridSaleDAddRows();
            dataGridViewT1.Focus();
            dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells["產品編號"];
            dataGridViewT1.CurrentRow.Selected = true;
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                dataGridViewT1.Rows.Remove(dataGridViewT1.SelectedRows[0]);
                table.AcceptChanges();
                dataGridViewT1.Focus();
                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentRow.Selected = true;
                    for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                    {
                        dataGridViewT1["序號", i].Value = (i + 1).ToString();
                    }
                }
                //刪除列後，重新計算帳款
                Summary();
            }
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
            if (dataGridViewT1.Rows.Count == 0) return;
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["產品編號", i].Value.ToString() == "")
                {
                    dataGridViewT1.Focus();
                    return;
                }
            }
            int InsertIndex = dataGridViewT1.CurrentRow.Index;
            GridSaleDInsertRows(InsertIndex);

            dataGridViewT1.Focus();
            dataGridViewT1.CurrentCell = dataGridViewT1.Rows[InsertIndex].Cells["產品編號"];
            dataGridViewT1.CurrentRow.Selected = true;
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

        private void gridGetBomD_Click(object sender, EventArgs e)
        {
            //零件不能是空值
            gridGetBomD.Focus();
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
                            tempD.Rows[e.RowIndex]["itpkgqty"] = "1";
                            tempD.Rows[e.RowIndex]["ItParePrs"] = "1";
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
                                            dataGridViewT1.EditingControl.Text = dr["itunitp"].ToString();
                                            dataGridViewT1.SelectedRows[0].Cells["單位"].Value = dr["itunitp"].ToString();
                                            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                            dataGridViewT1.SelectedRows[0].Cells["包裝數量"].Value = dr["itpkgqty"].ToString();
                                        }
                                        else
                                        {
                                            dataGridViewT1.EditingControl.Text = dr["itunit"].ToString();
                                            dataGridViewT1.SelectedRows[0].Cells["單位"].Value = dr["itunit"].ToString();
                                            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                            dataGridViewT1.SelectedRows[0].Cells["包裝數量"].Value = "1";
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
                        dataGridViewT1.EditingControl.Text = "";
                        dataGridViewT1["產品編號", e.RowIndex].Value = "";
                        dataGridViewT1["品名規格", e.RowIndex].Value = "";
                        dataGridViewT1["單位", e.RowIndex].Value = "";
                        dataGridViewT1["標準用量", e.RowIndex].Value = "0";
                        dataGridViewT1["母件比例", e.RowIndex].Value = "1";
                        dataGridViewT1["包裝數量", e.RowIndex].Value = "1";
                        dataGridViewT1["說明", e.RowIndex].Value = "";
                        dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
                            dataGridViewT1["包裝數量", e.RowIndex].Value = "1";
                            dataGridViewT1["說明", e.RowIndex].Value = "";
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
                                        dataGridViewT1.EditingControl.Text = row["ItNo"].ToString();

                                    tempD.Rows[e.RowIndex]["itno"] = row["ItNo"].ToString();
                                    tempD.Rows[e.RowIndex]["itname"] = row["ItName"].ToString();
                                    tempD.Rows[e.RowIndex]["itunit"] = row["ItUnit"].ToString();
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
                    break;
                case "標準用量":
                    dataGridViewT1["標準用量", e.RowIndex].Value = dataGridViewT1["標準用量", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    break;
                case "母件比例":
                    dataGridViewT1["母件比例", e.RowIndex].Value = dataGridViewT1["母件比例", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (CellValue.IsZeroOrEmpty())
                    {
                        e.Cancel = true;
                        decimal d = 0;
                        dataGridViewT1.EditingControl.Text = d.ToString();
                        dataGridViewT1["母件比例", e.RowIndex].Value = d;
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                        MessageBox.Show("母件比例不可為0或是空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                case "包裝數量":
                    dataGridViewT1["包裝數量", e.RowIndex].Value = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue;
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (CellValue.IsZeroOrEmpty())
                    {
                        e.Cancel = true;
                        decimal d = 0;
                        dataGridViewT1.EditingControl.Text = d.ToString();
                        dataGridViewT1["包裝數量", e.RowIndex].Value = d;
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
                case Keys.F11:
                    gridExit.Focus();
                    gridExit.PerformClick();
                    break;
                case Keys.F9:
                    gridGetBomD.Focus();
                    gridGetBomD.PerformClick();
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
