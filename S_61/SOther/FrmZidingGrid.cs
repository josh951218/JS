using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Data.SqlClient;
using System.Reflection;

namespace S_61.SOther
{
    public partial class FrmZidingGrid : Formbase
    {
        public string frmName = "";                     //來源表單名稱
        public DataGridViewT grid = new DataGridViewT();//來源grid
        public DataTable dtD = new DataTable();         //來源資料表
        public string tableName = "";                   //來源資料表名稱


        DataTable dtDB = new DataTable();
        List<string> list = new List<string>();

        public FrmZidingGrid(DataGridViewT gd)
        {
            InitializeComponent();
            this.grid = gd;
        }

        private void FrmZidingGrid_Load(object sender, EventArgs e)
        {
            if (CorrespondTableName(frmName) == "不得更新")
            {
                labelT12.Visible = Modify品名規格_textBoxT.Visible = false;
            }
            labelT1.Text = frmName + "『DatagridView』自定欄位";
            labelT2.Text = frmName + "『資料庫』中可使用的欄位";

            //來源資料表,顯示於grid2
            dtDB = dtD.Clone();
            var row = dtDB.NewRow();
            dtDB.Rows.Add(row);

            dataGridViewbaseTwo2.AutoGenerateColumns = true;
            dataGridViewbaseTwo2.DataSource = dtDB;
            for (int i = 0; i < dtDB.Columns.Count; i++)
            {
                dataGridViewbaseTwo2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewbaseTwo1.AllowUserToOrderColumns = true;
            dataGridViewbaseTwo2.AllowUserToOrderColumns = false;

            //來源datagrid,顯示於grid1
            if (dtD.Rows.Count == 0)
            {
                var rw = dtD.NewRow();
                dtD.Rows.Add(rw);
            }
            dataGridViewbaseTwo1.DataSource = dtD;

            //檢查有無使用者自定記錄
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@表單名稱", frmName);
                    cmd.CommandText = " Select Count(*) from tablestyle where 表單名稱=(@表單名稱)";
                    if (cmd.ExecuteScalar().ToDecimal() == 0)
                    {
                        #region  沒有自定記錄,儲存程式設計師的表的來源datagrid預設值
                        cmd.Parameters.AddWithValue("使用者名稱", Common.User_Name);
                        cmd.Parameters.AddWithValue("資料行名稱", "");
                        cmd.Parameters.AddWithValue("綁定值名稱", "");
                        cmd.Parameters.AddWithValue("初始順序", 0);
                        cmd.Parameters.AddWithValue("自定順序", 0);
                        cmd.Parameters.AddWithValue("初始文字", "");
                        cmd.Parameters.AddWithValue("自定文字", "");
                        cmd.Parameters.AddWithValue("初始寬度", 0);
                        cmd.Parameters.AddWithValue("自定寬度", 0);
                        cmd.Parameters.AddWithValue("自定顯示", "");
                        //
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;
                        for (int i = 0; i < grid.Columns.Count; i++)
                        {
                            DataGridViewTextBoxColumn t = new DataGridViewTextBoxColumn();
                            t.Name = grid.Columns[i].Name;
                            t.DataPropertyName = grid.Columns[i].DataPropertyName.ToLower();

                            string[] tag = new string[3];//初始文字//初始寬度//初始顯示or隱藏
                            tag[0] = grid.Columns[i].HeaderText;
                            tag[1] = ((DataGridViewTextBoxColumn)grid.Columns[i]).MaxInputLength.ToString();
                            tag[2] = grid.Columns[i].Visible.ToString();

                            t.DisplayIndex = grid.Columns[i].DisplayIndex;
                            t.HeaderText = grid.Columns[i].HeaderText;
                            t.MaxInputLength = ((DataGridViewTextBoxColumn)grid.Columns[i]).MaxInputLength;
                            if (t.MaxInputLength >= 150) t.MaxInputLength = 150;
                            t.Width = (int)((t.MaxInputLength * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));

                            t.Tag = tag;
                            t.SortMode = DataGridViewColumnSortMode.NotSortable;
                            list.Add(t.DataPropertyName.ToLower());
                            dataGridViewbaseTwo1.Columns.Add(t);

                            cmd.Parameters["資料行名稱"].Value = grid.Columns[i].Name;
                            cmd.Parameters["綁定值名稱"].Value = grid.Columns[i].DataPropertyName.ToLower();
                            cmd.Parameters["初始順序"].Value = grid.Columns[i].Index;
                            cmd.Parameters["自定順序"].Value = t.DisplayIndex;
                            cmd.Parameters["初始文字"].Value = tag.ElementAt(0);
                            cmd.Parameters["自定文字"].Value = t.HeaderText;
                            cmd.Parameters["初始寬度"].Value = tag.ElementAt(1);
                            cmd.Parameters["自定寬度"].Value = t.MaxInputLength;
                            cmd.Parameters["自定顯示"].Value = tag.ElementAt(2);
                            cmd.CommandText = "Insert into tablestyle (表單名稱,使用者名稱,資料行名稱,綁定值名稱,初始順序,自定順序,初始文字,自定文字,初始寬度,自定寬度,自定顯示) values (@表單名稱,@使用者名稱,@資料行名稱,@綁定值名稱,@初始順序,@自定順序,@初始文字,@自定文字,@初始寬度,@自定寬度,@自定顯示)";
                            cmd.ExecuteNonQuery();
                        }
                        tn.Commit();
                        #endregion
                    }
                    else
                    {
                        #region 有自定記錄
                        cmd.CommandText = " Select * from tablestyle where 表單名稱=(@表單名稱) order by 初始順序";
                        DataTable temp = new DataTable();
                        da.Fill(temp);

                        for (int i = 0; i < temp.Rows.Count; i++)
                        {
                            string[] tag = new string[3];
                            tag[0] = temp.Rows[i]["初始文字"].ToString();
                            tag[1] = temp.Rows[i]["初始寬度"].ToString();
                            tag[2] = temp.Rows[i]["自定顯示"].ToString();

                            DataGridViewTextBoxColumn t = new DataGridViewTextBoxColumn();
                            t.Name = temp.Rows[i]["資料行名稱"].ToString();
                            t.DataPropertyName = temp.Rows[i]["綁定值名稱"].ToString().ToLower();

                            t.HeaderText = temp.Rows[i]["自定文字"].ToString().Trim();
                            t.MaxInputLength = temp.Rows[i]["自定寬度"].ToInteger();
                            t.Tag = tag;

                            t.SortMode = DataGridViewColumnSortMode.NotSortable;
                            t.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                            list.Add(t.DataPropertyName.ToLower());
                            dataGridViewbaseTwo1.Columns.Add(t);
                        }
                        var temp1 = temp.AsEnumerable().OrderBy(r => r["自定順序"].ToInteger()).CopyToDataTable();
                        for (int i = 0; i < temp1.Rows.Count; i++)
                        {
                            dataGridViewbaseTwo1.Columns[temp1.Rows[i]["資料行名稱"].ToString()].DisplayIndex = i;
                        }
                        #endregion
                    }
                    #region 拿掉已經使用的欄位
                    for (int i = 0; i < dtDB.Columns.Count; i++)
                    {
                        if (list.IndexOf(dtDB.Columns[i].ColumnName.ToLower()) == -1) continue;
                        else
                        {
                            dtDB.Columns.RemoveAt(i);
                            i--;
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    this.Dispose();
                }
            }
        }

        void writeToText(DataGridViewColumn column)
        {
            if (column.Tag != null && column.Tag.ToString().Length > 0)
            {
                oName.Text = column.Name;
                oTable.Text = column.DataPropertyName.ToLower();

                oIndex.Text = column.Index.ToString();
                uIndex.Text = column.DisplayIndex.ToString();

                string[] tag = (string[])column.Tag;
                oHeaderText.Text = tag.ElementAt(0);
                oWidth.Text = tag.ElementAt(1);
                if (tag.ElementAt(2) == bool.TrueString) rT.Checked = true;
                else rF.Checked = true;

                uHeaderText.Text = column.HeaderText;
                uWidth.Text = ((DataGridViewTextBoxColumn)column).MaxInputLength.ToString();
            }
        }

        private void dataGridViewbaseTwo1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (dataGridViewbaseTwo1.SelectedCells != null && dataGridViewbaseTwo1.SelectedCells.Count > 0)
            {
                var column = dataGridViewbaseTwo1.SelectedCells[0].OwningColumn;
                if (column.Equals(e.Cell.OwningColumn))
                {
                    writeToText(column);
                }
            }
        }

        private void dataGridViewbaseTwo1_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (dataGridViewbaseTwo1.SelectedCells != null && dataGridViewbaseTwo1.SelectedCells.Count > 0)
            {
                var column = dataGridViewbaseTwo1.SelectedCells[0].OwningColumn;
                if (column.Equals(e.Column))
                {
                    writeToText(column);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridViewbaseTwo2.SelectedCells != null && dataGridViewbaseTwo2.SelectedCells.Count > 0)
            {
                btnAdd.Focus();
                var cell = dataGridViewbaseTwo2.SelectedCells;

                DataGridViewTextBoxColumn t = new DataGridViewTextBoxColumn();
                DataGridViewColumn c = ((DataGridViewColumn)cell[0].OwningColumn);

                t.Name = c.Name;
                t.DataPropertyName = c.DataPropertyName.ToLower();

                var len = GetLength(t.DataPropertyName);

                t.HeaderText = c.HeaderText;
                t.MaxInputLength = len;
                t.Visible = true;

                string[] str = new string[] { c.HeaderText, len.ToString(), bool.TrueString };
                t.Tag = str;

                t.Width = (int)((t.MaxInputLength * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
                t.SortMode = DataGridViewColumnSortMode.NotSortable;
                t.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewbaseTwo1.Columns.Add(t);
                dataGridViewbaseTwo2.Columns.Remove(c);
                t.DisplayIndex = 0;
                dataGridViewbaseTwo1.CurrentCell = dataGridViewbaseTwo1[t.Name, 0];
                dataGridViewbaseTwo1.CurrentCell.Selected = true;
                dataGridViewbaseTwo1.Focus();
            }
            else
            {
                if (dataGridViewbaseTwo2.Columns.Count == 0)
                {
                    MessageBox.Show("沒有欄位可以新增了！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("請選擇要加入的欄位！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void rT_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            if (btnDefault.Focused) return;
            if (((RadioT)sender).Checked == false) return;
            if (dataGridViewbaseTwo1.SelectedCells != null && dataGridViewbaseTwo1.SelectedCells.Count > 0)
            {
                var flag = rT.Checked ? bool.TrueString : bool.FalseString;
                var cell = dataGridViewbaseTwo1.SelectedCells[0];
                if (cell.OwningColumn.Name == "序號" && flag == bool.FalseString)
                {
                    MessageBox.Show("隱藏序號會造成系統錯誤,請勿隱藏！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rT.Checked = true;
                    return;
                }
                var tag = ((string[])cell.OwningColumn.Tag);
                tag[2] = flag;
                cell.OwningColumn.Tag = tag;
            }
        }

        private void uHeaderText_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            if (btnDefault.Focused) return;
            if (dataGridViewbaseTwo1.SelectedCells != null && dataGridViewbaseTwo1.SelectedCells.Count > 0)
            {
                var cell = dataGridViewbaseTwo1.SelectedCells[0];
                cell.OwningColumn.HeaderText = uHeaderText.Text.Trim();
            }
        }

        private void uWidth_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            if (btnDefault.Focused) return;
            if (uWidth.Text.ToInteger() <= 0 || uWidth.Text.ToInteger() > 150)
            {
                e.Cancel = true;
                MessageBox.Show("寬度不可以小於/等於0或是大於150！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                uWidth.SelectAll();
                return;
            }
            if (dataGridViewbaseTwo1.SelectedCells != null && dataGridViewbaseTwo1.SelectedCells.Count > 0)
            {
                var cell = dataGridViewbaseTwo1.SelectedCells[0];
                var maxlength = uWidth.Text.ToInteger();
                ((DataGridViewTextBoxColumn)cell.OwningColumn).MaxInputLength = maxlength;
                dataGridViewbaseTwo1.Columns[cell.OwningColumn.Name].Width = (int)((maxlength * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            }
        }

        private void uIndex_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            if (btnDefault.Focused) return;
            if (uIndex.Text.ToInteger() < 0)
            {
                e.Cancel = true;
                MessageBox.Show("寬度不可以小於零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                uIndex.SelectAll();
                return;
            }
            if (dataGridViewbaseTwo1.SelectedCells != null && dataGridViewbaseTwo1.SelectedCells.Count > 0)
            {
                var index = uIndex.Text.ToInteger();
                if (index >= dataGridViewbaseTwo1.Columns.Count) index = dataGridViewbaseTwo1.Columns.Count - 1;

                var cell = dataGridViewbaseTwo1.SelectedCells[0];
                ((DataGridViewTextBoxColumn)cell.OwningColumn).DisplayIndex = index;
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("請確定是否要回復預設值?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@表單名稱", frmName);
                cmd.CommandText = " Delete from tablestyle where 表單名稱=(@表單名稱)";
                cmd.ExecuteNonQuery();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //檢查有無使用者自定記錄
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@表單名稱", frmName);
                    cmd.Parameters.AddWithValue("使用者名稱", Common.User_Name);
                    cmd.Parameters.AddWithValue("資料行名稱", "");
                    cmd.Parameters.AddWithValue("綁定值名稱", "");
                    cmd.Parameters.AddWithValue("初始順序", 0);
                    cmd.Parameters.AddWithValue("自定順序", 0);
                    cmd.Parameters.AddWithValue("初始文字", "");
                    cmd.Parameters.AddWithValue("自定文字", "");
                    cmd.Parameters.AddWithValue("初始寬度", 0);
                    cmd.Parameters.AddWithValue("自定寬度", 0);
                    cmd.Parameters.AddWithValue("自定顯示", "");
                    //
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.CommandText = " Delete from tablestyle where 表單名稱=(@表單名稱)";
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < dataGridViewbaseTwo1.Columns.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        string[] tag = ((string[])dataGridViewbaseTwo1.Columns[i].Tag);
                        if (Modify品名規格_textBoxT.Text.Trim().Length > 0 && tag.ElementAt(0).ToString() == "品名規格")
                        {
                            string tableName_ =  CorrespondTableName(frmName);
                            if (tableName_ == "不得更新")
                            {
                                tn.Rollback();
                                Modify品名規格_textBoxT.Text = "";
                                MessageBox.Show("此視窗品名規格不得修改!!!");
                                return;
                                
                            }
                            SQL.ExecuteNonQuery(" ALTER TABLE " + tableName_ + " ALTER Column itname  nvarchar(" + Modify品名規格_textBoxT.Text + ") ", null, null, cmd);
                            cmd.Parameters.AddWithValue("初始寬度", Modify品名規格_textBoxT.Text.Trim());
                        }
                        else 
                        {
                            cmd.Parameters.AddWithValue("初始寬度",tag.ElementAt(1));
                        }
                        cmd.Parameters.AddWithValue("表單名稱", frmName);
                        cmd.Parameters.AddWithValue("使用者名稱", Common.User_Name);
                        cmd.Parameters.AddWithValue("資料行名稱", dataGridViewbaseTwo1.Columns[i].Name);
                        cmd.Parameters.AddWithValue("綁定值名稱", dataGridViewbaseTwo1.Columns[i].DataPropertyName.ToLower());
                        cmd.Parameters.AddWithValue("初始順序", dataGridViewbaseTwo1.Columns[i].Index);
                        cmd.Parameters.AddWithValue("自定順序", dataGridViewbaseTwo1.Columns[i].DisplayIndex);
                        cmd.Parameters.AddWithValue("初始文字", tag.ElementAt(0));
                        cmd.Parameters.AddWithValue("自定文字", dataGridViewbaseTwo1.Columns[i].HeaderText);
                        cmd.Parameters.AddWithValue("自定寬度", ((DataGridViewTextBoxColumn)dataGridViewbaseTwo1.Columns[i]).MaxInputLength);
                        cmd.Parameters.AddWithValue("自定顯示", tag.ElementAt(2));
                        cmd.CommandText = "Insert into tablestyle (表單名稱,使用者名稱,資料行名稱,綁定值名稱,初始順序,自定順序,初始文字,自定文字,初始寬度,自定寬度,自定顯示) values (@表單名稱,@使用者名稱,@資料行名稱,@綁定值名稱,@初始順序,@自定順序,@初始文字,@自定文字,@初始寬度,@自定寬度,@自定顯示)";
                        cmd.ExecuteNonQuery();
                    }
                    tn.Commit();
                    MessageBox.Show("儲存成功！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private string CorrespondTableName(string frmName)
        {
            switch (frmName)
            {
                case "FrmQuote":
                    return "Quoted";
                case "FrmOrder" :
                    return "Orderd";
                case "FrmSale":
                    return "Saled";
                case "FrmRSale":
                    return "RSaled";

                case "FrmFQuot":
                    return "FQuotd";
                case "FrmFord":
                    return "Fordd";
                case "FrmBShop":
                    return "BShopd";
                case "FrmRShop":
                    return "RShopd";      
            }
            return "不得更新";
        }

        int GetLength(string column)
        {
            if (this.tableName.Trim().Length == 0)
                return 4;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            using (DataTable temp = new DataTable())
            {
                cmd.Parameters.AddWithValue("column", column);
                cmd.Parameters.AddWithValue("tb", this.tableName);
                cmd.CommandText = @"
                Select column_name,data_type,character_maximum_length,intf=NUMERIC_PRECISION-NUMERIC_SCALE,itne=NUMERIC_SCALE
                From information_schema.columns
                Where table_name = @tb and COLUMN_NAME = @column ";

                da.Fill(temp);
                if (temp.Rows.Count == 0)
                    return 4;
                else
                {
                    var type = temp.Rows[0]["data_type"].ToString().Trim();

                    if (type.ToLower() == "nvarchar")
                        return temp.Rows[0]["character_maximum_length"].ToInteger();
                    if (type.ToLower() == "numeric")
                        return temp.Rows[0]["intf"].ToInteger();
                    if (type.ToLower() == "int")
                        return temp.Rows[0]["intf"].ToInteger();
                    if (type.ToLower() == "bit")
                        return 1;
                }
            }

            return 4;
        }
    }
}
