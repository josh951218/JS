using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
 
namespace S_61.subMenuFm_6
{
    public partial class FrmInBilling_1 : Formbase
    {
        public string stno = "";
        public string stname = "";
        public string st;

        SqlTransaction tran;
        DataTable dt = new DataTable();
        List<int> list = new List<int>();
        string sttype;
        string itno = "";
        bool 是否執行 = true;

        public FrmInBilling_1()
        {
            InitializeComponent();
             
            this.期初庫存量.Set庫存數量小數();
            this.期初單位成本.Set進貨單價小數();

            this.期初庫存量.DefaultCellStyle.Format = "f" + Common.Q;
            this.期初單位成本.DefaultCellStyle.Format = "f" + Common.MF;
        }

        private void FrmInBilling_1_Load(object sender, EventArgs e)
        {
            StNo.Text = stno;
            StName.Text = stname;
            sttype = st;
             
            dataGridViewT1.DataSource = dt;
            loadDB();

            計算庫存總金額();
             
        }

        public void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@stno", StNo.Text.Trim());
                    string sql = "select Item.ItNo as 產品編號,Item.ItName as 品名規格,Item.ItUnit as 單位,CASE WHEN Item.ItTrait = '1' THEN '組合品' WHEN Item.ItTrait = '2' THEN '組裝品'WHEN Item.ItTrait = '3' THEN '單一商品' END AS 產品組成,ISNULL(Stock.ItQtyF,0)AS 期初庫存量,Stock.StNo,Item.ItFirCost as 期初單位成本 from item left join stock on stock.itno=item.itno and stock.stno = @StNo";//where item.ItTrait not in (1)";
                    cmd.CommandText = sql;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        da.Fill(dt);
                    }
                    cmd.Dispose();
                }
                if (itno != "" && dt.Rows.Count > 0)
                {
                    int index = dt.AsEnumerable().ToList().FindIndex(r => r["產品編號"].ToString().Trim() == itno);
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                    dataGridViewT1.Rows[index].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void 計算庫存總金額()
        {
            decimal tempcount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tempcount += dt.Rows[i]["期初庫存量"].ToDecimal() * dt.Rows[i]["期初單位成本"].ToDecimal();
            }
            totcount.Text = tempcount.ToString("f" + Common.MF);
        }
         
        private void btnModify_Click(object sender, EventArgs e)
        {
            是否執行 = false;
            list.Clear();
            loadDB();
            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["產品編號"].ReadOnly = true;
            dataGridViewT1.Columns["品名規格"].ReadOnly = true;
            dataGridViewT1.Columns["單位"].ReadOnly = true;
            dataGridViewT1.Columns["產品組成"].ReadOnly = true;
            btnSave.Enabled = true;
            btnModify.Enabled = false;
            是否執行 = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;


                    for (int i = 0; i < list.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@StName", StName.Text.Trim());
                        cmd.Parameters.AddWithValue("@sttype", sttype);
                        cmd.Parameters.AddWithValue("@ItName", dataGridViewT1["品名規格", list[i]].Value.ToString());
                        cmd.Parameters.AddWithValue("@ItQtyF", dataGridViewT1["期初庫存量", list[i]].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("@ItQty", dataGridViewT1["期初庫存量", list[i]].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("@ItNo", dataGridViewT1["產品編號", list[i]].Value);
                        cmd.Parameters.AddWithValue("@StNo1", dataGridViewT1["StNo1", list[i]].Value);
                        cmd.Parameters.AddWithValue("@Qty", dataGridViewT1["期初庫存量", list[i]].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("@Cost", dataGridViewT1["期初單位成本", list[i]].Value.ToDecimal());

                        //Temp
                        cmd.CommandText = "select ItQty,ItQtyF from Stock where ItNo=@ItNo"
                                 + " COLLATE Chinese_Taiwan_Stroke_BIN and StNo=@StNo1"
                                 + " COLLATE Chinese_Taiwan_Stroke_BIN";

                        decimal _Qty = 0, _QtyFStart = 0, _QtyFEnd = 0;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //decimal.TryParse(reader["ItQty"].ToString(), out _Qty);
                                //decimal.TryParse(reader["ItQtyF"].ToString(), out _QtyFStart);
                                _Qty = reader["ItQty"].ToDecimal();
                                _QtyFStart = reader["ItQtyF"].ToDecimal();
                            }
                        }
                        //decimal.TryParse(dataGridViewT1["期初庫存量", list[i]].EditedFormattedValue.ToString(), out _QtyFEnd);
                        _QtyFEnd = dataGridViewT1["期初庫存量", list[i]].EditedFormattedValue.ToDecimal();

                        //item
                        cmd.CommandText = "select * from Stock where StNo=@StNo and ItNo=@ItNo";
                        SqlDataReader dr = cmd.ExecuteReader();
                        decimal invqty = 0;//增加的數量
                        decimal cost;//成本
                        dr.Read();
                        try
                        {
                            //invqty = decimal.Parse(dataGridViewT1["期初庫存量", list[i]].Value.ToString()) - decimal.Parse(dr["ItQtyF"].ToString());
                            invqty = dataGridViewT1["期初庫存量", list[i]].Value.ToDecimal() - dr["ItQtyF"].ToDecimal();
                        }
                        catch
                        {
                            //invqty = decimal.Parse(dataGridViewT1["期初庫存量", list[i]].Value.ToString());
                            invqty = dataGridViewT1["期初庫存量", list[i]].Value.ToDecimal();
                        }

                        //cost = decimal.Parse(dataGridViewT1["期初單位成本", list[i]].Value.ToString());
                        cost = dataGridViewT1["期初單位成本", list[i]].Value.ToDecimal();
                        dr.Dispose();

                        cmd.CommandText = "Update item set "
                        + "ItFirTQty=ItFirTQty+" + invqty + ","
                        + "ItStockQty=ItStockQty+" + invqty + ","
                        + "itfircost=" + cost + ","
                        + "itfirtcost=" + cost + "*(ItFirTQty+" + invqty + ")" + " where ItNo=@ItNo";
                        cmd.ExecuteNonQuery();


                        //BeStock
                        cmd.CommandText = "select count(*) from BeStock where StNo=@StNo and ItNo=@ItNo";
                        if (cmd.ExecuteScalar().ToString() != "0")
                        {
                            cmd.CommandText = "Update BeStock set "
                            + "Qty=@Qty,"
                            + "Cost=@Cost where StNo =@StNo and ItNo=@ItNo";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO BeStock "
                            + "(StNo,ItNo,Qty,Cost) VALUES ((@StNo),(@ItNo),(@Qty),(@Cost))";
                            cmd.ExecuteNonQuery();
                        }


                        //Stock
                        cmd.Parameters.AddWithValue("@QItQtyF", (_QtyFEnd).ToDecimal());
                        cmd.Parameters.AddWithValue("@QItQty", (_Qty - _QtyFStart + _QtyFEnd).ToDecimal());
                         
                        cmd.CommandText = "select count(*) from Stock where StNo=@StNo and ItNo=@ItNo";
                        if (cmd.ExecuteScalar().ToString() != "0")
                        {
                            cmd.CommandText = "Update Stock set "
                            + "ItQtyF=@QItQtyF,"
                            + "ItQty=@QItQty"
                            + " where StNo =@StNo"
                            + " COLLATE Chinese_Taiwan_Stroke_BIN and ItNo=@ItNo";
                             
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO Stock "
                            + "(StNo,StName,StTrait,ItName,ItNo,ItQtyF,ItQty) VALUES ("
                            + "(@StNo),"
                            + "(@StName),"
                            + "(@sttype),"
                            + "(@ItName),"
                            + "(@ItNo),"
                            + "(@ItQtyF),"
                            + "(@ItQty))";

                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    tran.Dispose();

                    MessageBox.Show(
                        "儲存成功!",
                        "訊息視窗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dataGridViewT1.ReadOnly = true;
                    btnSave.Enabled = false;
                    btnModify.Enabled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
         
        private void ItNo_TextChanged(object sender, EventArgs e)
        {

            dataGridViewT1.Search("產品編號", ItNo.Text.Trim());
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                //case Keys.D2:
                //case Keys.NumPad2: 
                //    btnModify_Click(null, null);
                //    break;
                //case Keys.D0:
                //case Keys.NumPad0: 
                //    btnExit_Click(null, null);
                //    break;
                case Keys.F11:
                    btnExit.Focus();
                    btnExit.PerformClick();
                    break;
                case Keys.F9:
                    btnSave.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
         
        private void dataGridViewT1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (list.IndexOf(cell.RowIndex) == -1) list.Add(cell.RowIndex);
        }

        DataGridViewCell cell;
        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cell = null;
            if (e.ColumnIndex > -1 && e.RowIndex > -1 && e.RowIndex < dataGridViewT1.Rows.Count)
            {
                cell = dataGridViewT1[e.ColumnIndex, e.RowIndex];
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "期初庫存量" || dataGridViewT1.Columns[e.ColumnIndex].Name == "期初單位成本")
            {
                dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                計算庫存總金額();
            }
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count > 0 && 是否執行)
            {
                itno = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
            }
        }
    }
}
