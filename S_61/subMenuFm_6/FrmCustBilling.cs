using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class FrmCustBilling : Formbase
    {
        DataTable dt = new DataTable();

        decimal _OldFirMny = 0;
        decimal _OldCuFirAdvamt = 0;
        decimal _OldCuCredit = 0;
        decimal _OldCuFirRcvPar = 0;

        public FrmCustBilling()
        {
            InitializeComponent(); 

            this.期初帳款金額.Set銷貨單價小數();
            this.期初帳款餘額.Set銷貨單價小數();
            this.期初預收金額.Set銷貨單價小數();
            this.預收餘額.Set銷貨單價小數();
            this.應收帳款餘額.Set銷貨單價小數();
            this.信用額度.Set銷貨單價小數();

            this.帳款期初匯率.FirstNum = 12;
            this.帳款期初匯率.LastNum = 4;
            this.帳款期初匯率.DefaultCellStyle.Format = "f4";
        }

        private void FrmCustBilling_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt;

            loadDB();
            //btnSave.Enabled = false;
        }

        private void FrmCustBilling_Shown(object sender, EventArgs e)
        {
            cust.Focus();
        }

        private void cust_TextChanged(object sender, EventArgs e)
        {
            dataGridViewT1.Search("客戶編號", cust.Text.Trim());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D2:
                case Keys.NumPad2:
                    if (dataGridViewT1.EditingControl != null) break;
                    if (cust.Focused) break;
                    btnModify.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    if (dataGridViewT1.EditingControl != null) break;
                    if (cust.Focused) break;
                    btnExit.Focus();
                    btnExit.PerformClick();
                    break;
                case Keys.F11:
                    btnExit.Focus();
                    btnExit.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (Common.Sys_JZOK)
            {
                MessageBox.Show("系統已做過年度結轉，無法再執行開帳作業！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) index = 0;
            dataGridViewT1.CurrentCell = dataGridViewT1["期初帳款金額", index];
            dataGridViewT1.Rows[index].Selected = true;

            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["客戶編號"].ReadOnly = true;
            dataGridViewT1.Columns["客戶簡稱"].ReadOnly = true;
            dataGridViewT1.Columns["期初帳款餘額"].ReadOnly = true;
            dataGridViewT1.Columns["預收餘額"].ReadOnly = true;
            dataGridViewT1.Columns["應收帳款餘額"].ReadOnly = true;
            dataGridViewT1.Columns["幣別"].ReadOnly = true;
             
            btnCancel.Enabled = true;
            btnModify.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.ReadOnly = true;
                loadDB();
            }
            finally
            { 
                btnModify.Enabled = true;
                btnCancel.Enabled = false;
            }
        }

        private void btnModify_EnabledChanged(object sender, EventArgs e)
        {
            btnExit.Enabled = btnModify.Enabled;
        }
         
        public void loadDB()
        {
            try
            {
                dt.Clear();
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select Cust.CuNo as 客戶編號,Cust.CuName1 as 客戶簡稱,Cust.CuFirReceiv as 期初帳款金額,Cust.CuSpareRcv as 期初帳款餘額,Cust.CuFirAdvamt as 期初預收金額,Cust.CuAdvamt as 預收餘額,Cust.CuCredit as 信用額度,Cust.CuReceiv as 應收帳款餘額,Xa01.Xa1Name as 幣別,ISNULL(Cust.CuFirRcvPar,1) AS 帳款期初匯率 from Cust left join Xa01 on Xa01.Xa1no=Cust.CuXa1no order by cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
         
        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        { 
            if (dataGridViewT1.ReadOnly)
                return;

            if (e.ColumnIndex == -1)
                return;

            if (e.RowIndex == -1)
                return;


            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.期初帳款金額))
            {
                _OldFirMny = dataGridViewT1["期初帳款金額", e.RowIndex].EditedFormattedValue.ToDecimal();
            }

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.期初預收金額))
            {
                _OldCuFirAdvamt = dataGridViewT1["期初預收金額", e.RowIndex].EditedFormattedValue.ToDecimal();
            }

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.信用額度))
            {
                _OldCuCredit = dataGridViewT1["信用額度", e.RowIndex].EditedFormattedValue.ToDecimal();
            }

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.帳款期初匯率))
            {
                _OldCuFirRcvPar = dataGridViewT1["帳款期初匯率", e.RowIndex].EditedFormattedValue.ToDecimal();
            }
        }

        private void dataGridViewT1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        { 
            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.期初帳款金額))
            {
                #region 期初帳款金額
                var cuno = dataGridViewT1["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var nowQ = dataGridViewT1["期初帳款金額", e.RowIndex].EditedFormattedValue.ToDecimal();

                if (_OldFirMny == nowQ)
                    return;

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("cuno", cuno);
                            cmd.Parameters.AddWithValue("nowQ", nowQ);
                            cn.Open();
                            tn = cn.BeginTransaction();
                            cmd.Transaction = tn;

                            cmd.CommandText =  " Update cust set CuReceiv = ISNULL(CuReceiv,0)-ISNULL(CuSpareRcv,0) where cuno=@cuno;";
                            cmd.CommandText += " Update cust set CuSpareRcv  = ISNULL(CuSpareRcv,0)-ISNULL(CuFirReceiv,0)+@nowQ where cuno=@cuno;";
                            cmd.CommandText += " Update cust set CuReceiv = ISNULL(CuReceiv,0)+ISNULL(CuSpareRcv,0) where cuno=@cuno;";
                            cmd.CommandText += " Update cust set CuFirReceiv = @nowQ where cuno=@cuno;";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = " Select CuFirReceiv,CuSpareRcv,CuReceiv from cust where cuno=@cuno;";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (dataGridViewT1.EditingControl != null)
                                        dataGridViewT1.EditingControl.Text = reader["CuFirReceiv"].ToString();
                                    dt.Rows[e.RowIndex]["期初帳款金額"] = reader["CuFirReceiv"].ToDecimal();//期初應收帳款金額
                                    dt.Rows[e.RowIndex]["期初帳款餘額"] = reader["CuSpareRcv"].ToDecimal(); //期初應收帳款餘額
                                    dt.Rows[e.RowIndex]["應收帳款餘額"] = reader["CuReceiv"].ToDecimal();   //現有應收帳款
                                }
                            }
                            tn.Commit();
                        }
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (tn != null)
                            tn.Dispose();
                    }
                }
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.期初預收金額))
            {
                #region 期初預收金額
                var cuno = dataGridViewT1["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var nowQ = dataGridViewT1["期初預收金額", e.RowIndex].EditedFormattedValue.ToDecimal();

                if (_OldCuFirAdvamt == nowQ)
                    return;

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("cuno", cuno);
                            cmd.Parameters.AddWithValue("nowQ", nowQ);
                            cn.Open();
                            tn = cn.BeginTransaction();
                            cmd.Transaction = tn;

                            cmd.CommandText = "  Update cust set CuAdvamt  = ISNULL(CuAdvamt,0)-ISNULL(CuFirAdvamt,0)+@nowQ where cuno=@cuno;";
                            cmd.CommandText += " Update cust set CuFirAdvamt = @nowQ where cuno=@cuno;";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = " Select CuFirAdvamt,CuAdvamt from cust where cuno=@cuno;";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (dataGridViewT1.EditingControl != null)
                                        dataGridViewT1.EditingControl.Text = reader["CuFirAdvamt"].ToString();
                                    dt.Rows[e.RowIndex]["期初預收金額"] = reader["CuFirAdvamt"].ToDecimal();
                                    dt.Rows[e.RowIndex]["預收餘額"] = reader["CuAdvamt"].ToDecimal();
                                }
                            }
                            tn.Commit();
                        }
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (tn != null)
                            tn.Dispose();
                    }
                }
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.信用額度))
            { 
                #region 信用額度
                var cuno = dataGridViewT1["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var nowQ = dataGridViewT1["信用額度", e.RowIndex].EditedFormattedValue.ToDecimal();

                if (_OldCuCredit == nowQ)
                    return;

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("cuno", cuno);
                            cmd.Parameters.AddWithValue("nowQ", nowQ);
                            cn.Open();
                            tn = cn.BeginTransaction();
                            cmd.Transaction = tn;

                            cmd.CommandText = " Update cust set CuCredit = @nowQ where cuno=@cuno;";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = " Select CuCredit from cust where cuno=@cuno;";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (dataGridViewT1.EditingControl != null)
                                        dataGridViewT1.EditingControl.Text = reader["CuCredit"].ToString();
                                    dt.Rows[e.RowIndex]["信用額度"] = reader["CuCredit"].ToDecimal();
                                }
                            }
                            tn.Commit();
                        }
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (tn != null)
                            tn.Dispose();
                    }
                }
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.帳款期初匯率))
            {
                #region 帳款期初匯率
                var cuno = dataGridViewT1["客戶編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var now_xa1par = dataGridViewT1["帳款期初匯率", e.RowIndex].EditedFormattedValue.ToDecimal();

                if (_OldCuFirRcvPar == now_xa1par)
                    return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("CuFirRcvPar",now_xa1par);
                    cmd.Parameters.AddWithValue("cuno", cuno);
                    cmd.CommandText = "Update cust set CuFirRcvPar = @CuFirRcvPar where cuno=@cuno";
                    cmd.ExecuteNonQuery();

                }

                dt.Rows[e.RowIndex]["帳款期初匯率"] = now_xa1par;
                dataGridViewT1.InvalidateRow(e.RowIndex);


                #endregion
            }
        }

         

    }
}
