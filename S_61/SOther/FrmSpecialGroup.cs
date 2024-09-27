using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmSpecialGroup : Formbase
    {
        public Decimal singleprice = 0;
        public Decimal qty = 0;
        public Decimal price = 0;
        public string groupid = "";

        DataTable dtD = new DataTable();
        List<String> list = new List<String>();

        public FrmSpecialGroup(bool IsBrow = false)
        {
            InitializeComponent();

            if (IsBrow)//瀏覽
            {
                dataGridViewT1.ReadOnly = true;
                gridAppend.Enabled = gridDelete.Enabled = gridSave.Enabled = false;
                btnGet.Enabled = true;
            }
            else//新增
            {
                dataGridViewT1.ReadOnly = false;
                this.序號.ReadOnly = this.群組代碼.ReadOnly = true;
                gridAppend.Enabled = gridDelete.Enabled = gridSave.Enabled = true;
                btnGet.Enabled = false;
            }

            this.單件售價.FirstNum = 12;
            this.單件售價.LastNum = 0;
            this.單件售價.DefaultCellStyle.Format = "f0";
            this.數量.FirstNum = 12;
            this.數量.LastNum = 0;
            this.數量.DefaultCellStyle.Format = "f0";
            this.售價.FirstNum = 12;
            this.售價.LastNum = 0;
            this.售價.DefaultCellStyle.Format = "f0";

            dataGridViewT1.DataSource = dtD;
        }

        private void FrmSpecialGroup_Load(object sender, EventArgs e)
        {
            LoadDB();
        }

        void LoadDB()
        {
            dtD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = " Select 序號='',* from SpecialGroup";
                    da.Fill(dtD);

                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["序號"] = "特價群組 " + (i + 1).ToString();
                        dtD.Rows[i].AcceptChanges();
                        dataGridViewT1.InvalidateRow(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            gridAppend.Focus();
            dtD.Rows.Add();
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["序號"] = "特價群組 " + (i + 1).ToString();
            }
            if (dtD.Rows.Count > 0)
            {
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells["單件售價"];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            gridDelete.Focus();
            if (dataGridViewT1.Rows.Count > 0)
            {
                int index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus(); return;
                }
                else
                {
                    var groupid = dtD.Rows[index]["groupid"].ToString().Trim();
                    if (groupid.Length > 0) list.Add(groupid);

                    dtD.Rows.RemoveAt(index);

                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["序號"] = "特價群組 " + (i + 1).ToString();
                    }
                    if (dataGridViewT1.Rows.Count > 0)
                    {
                        index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                        dataGridViewT1.CurrentCell = dataGridViewT1["單件售價", index];
                        dataGridViewT1.Rows[index].Selected = true;
                    }
                }
            }
            dataGridViewT1.Focus();
        }

        private void gridSave_Click(object sender, EventArgs e)
        {
            gridSave.Enabled = btnExit.Enabled = false;
            SqlTransaction tn = null;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        //if (dtD.Rows[i]["groupid"].ToString().Length > 0)
                        {
                            if (dtD.Rows[i]["qty"].ToDecimal() == 0 || dtD.Rows[i]["price"].ToDecimal() == 0 || dtD.Rows[i]["singleprice"].ToDecimal() == 0)
                            {
                                MessageBox.Show("數量與價格不可為空白！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        if (dtD.Rows[i]["qty"].ToDecimal() == 0 && dtD.Rows[i]["price"].ToDecimal() == 0 && dtD.Rows[i]["singleprice"].ToDecimal() == 0 && dtD.Rows[i]["groupid"].ToString().Length == 0)
                        {
                            dtD.Rows.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("groupid", "");
                    foreach (var str in list)
                    {
                        cmd.Parameters["groupid"].Value = str;
                        cmd.CommandText = " Delete from SpecialGroup where groupid = (@groupid)";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.AddWithValue("singleprice", 0);
                    cmd.Parameters.AddWithValue("qty", 0);
                    cmd.Parameters.AddWithValue("price", 0);
                    foreach (DataRow row in dtD.Rows)
                    {
                        cmd.Parameters["singleprice"].Value = row["singleprice"].ToDecimal("f0");
                        cmd.Parameters["qty"].Value = row["qty"].ToDecimal("f0");
                        cmd.Parameters["price"].Value = row["price"].ToDecimal("f0");

                        var groupid = row["groupid"].ToString().Trim();
                        cmd.Parameters["groupid"].Value = groupid;

                        var exist = false;
                        if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)
                        {
                            cmd.CommandText = "Select Count(*) from Speciald where groupid = (@groupid)";
                            exist = cmd.ExecuteScalar().ToDecimal() > 0;
                        }

                        var up = false;
                        if (exist)
                        {
                            up = MessageBox.Show("[代碼:" + groupid + "] 此混搭設定已被採用，請問是否一併更新特價區間設定！", "訊息視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes;
                        }

                        if (groupid.Length > 0)
                        {
                            cmd.CommandText = " Update SpecialGroup Set singleprice = (@singleprice), qty = (@qty), price = (@price) where groupid = (@groupid);";
                            if (up) cmd.CommandText += " Update Speciald Set singleprice = (@singleprice), reason = (@qty), qty = (@qty), price = (@price) where groupid = (@groupid);";
                        }
                        else
                        {
                            cmd.CommandText = " Insert into SpecialGroup (singleprice,qty,price) Values (@singleprice,@qty,@price);";
                        }
                        cmd.ExecuteNonQuery();
                    }
                    tn.Commit();
                    MessageBox.Show("儲檔成功！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDB();
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    gridSave.Enabled = btnExit.Enabled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;
            else
            {
                singleprice = dtD.Rows[index]["singleprice"].ToDecimal("f0");
                qty = dtD.Rows[index]["qty"].ToDecimal("f0");
                price = dtD.Rows[index]["price"].ToDecimal("f0");
                groupid = dtD.Rows[index]["groupid"].ToString().Trim();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (btnExit.Focused) return;
            var column = dataGridViewT1.Columns[e.ColumnIndex];
            if (column.Name == this.數量.Name)
            {
                dtD.Rows[e.RowIndex]["qty"] = dataGridViewT1[this.數量.Name, e.RowIndex].EditedFormattedValue.ToString().Trim().ToDecimal();
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (column.Name == this.售價.Name)
            {
                dtD.Rows[e.RowIndex]["price"] = dataGridViewT1[this.售價.Name, e.RowIndex].EditedFormattedValue.ToString().Trim().ToDecimal();
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }
    }
}
