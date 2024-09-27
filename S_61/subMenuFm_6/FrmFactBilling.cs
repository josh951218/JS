using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class FrmFactBilling : Formbase
    {
        DataTable dt = new DataTable();

        decimal _OldFirMny = 0;
        decimal _OldFaFirAdvamt = 0;
        decimal _OldFaCredit = 0;
        decimal _OldFaFirPayPar = 0;

        public FrmFactBilling()
        {
            InitializeComponent(); 

            this.期初帳款金額.Set進貨單價小數();
            this.期初帳款餘額.Set進貨單價小數();
            this.期初預付金額.Set進貨單價小數();
            this.預付餘額.Set進貨單價小數();
            this.應付帳款餘額.Set進貨單價小數();
            this.信用額度.Set進貨單價小數();

            this.帳款期初匯率.FirstNum = 12;
            this.帳款期初匯率.LastNum = 4;
            this.帳款期初匯率.DefaultCellStyle.Format = "f4";
        }

        private void FrmFactBilling_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt; 
            loadDB(); 
        }

        private void FrmFactBilling_Shown(object sender, EventArgs e)
        {
            fact.Focus();
        }

        private void fact_TextChanged(object sender, EventArgs e)
        {
            dataGridViewT1.Search("廠商編號", fact.Text.Trim());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D2:
                case Keys.NumPad2:
                    if (dataGridViewT1.EditingControl != null) break;
                    if (fact.Focused) break;
                    btnModify.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    if (dataGridViewT1.EditingControl != null) break;
                    if (fact.Focused) break;
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
            dataGridViewT1.Columns["廠商編號"].ReadOnly = true;
            dataGridViewT1.Columns["廠商簡稱"].ReadOnly = true;
            dataGridViewT1.Columns["期初帳款餘額"].ReadOnly = true;
            dataGridViewT1.Columns["預付餘額"].ReadOnly = true;
            dataGridViewT1.Columns["應付帳款餘額"].ReadOnly = true;
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
                    string sql = "Select Fact.FaNo as 廠商編號,Fact.FaName1 as 廠商簡稱,Fact.FaFirPayabl as 期初帳款金額,Fact.FaSparePay as 期初帳款餘額,Fact.FaFirPayAmt as 期初預付金額,Fact.FaPayAmt as 預付餘額,Fact.FaCredit as 信用額度,Fact.FaPayable as 應付帳款餘額,Xa01.Xa1Name as 幣別,ISNULL(Fact.FaFirPayPar,1) AS 帳款期初匯率 from Fact left join Xa01 on Xa01.Xa1no=Fact.FaXa1no order by fano COLLATE Chinese_Taiwan_Stroke_BIN";
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

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.期初預付金額))
            {
                _OldFaFirAdvamt = dataGridViewT1["期初預付金額", e.RowIndex].EditedFormattedValue.ToDecimal();
            }

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.信用額度))
            {
                _OldFaCredit = dataGridViewT1["信用額度", e.RowIndex].EditedFormattedValue.ToDecimal();
            }

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.帳款期初匯率))
            {
                _OldFaFirPayPar = dataGridViewT1["帳款期初匯率", e.RowIndex].EditedFormattedValue.ToDecimal();
            }
        }

        private void dataGridViewT1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        { 
            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.期初帳款金額))
            {
                #region 期初帳款金額
                var fano = dataGridViewT1["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
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
                            cmd.Parameters.AddWithValue("fano", fano);
                            cmd.Parameters.AddWithValue("nowQ", nowQ);
                            cn.Open();
                            tn = cn.BeginTransaction();
                            cmd.Transaction = tn;

                            cmd.CommandText = "  Update fact set FaPayable = ISNULL(FaPayable,0)-ISNULL(FaSparePay,0) where fano=@fano;";
                            cmd.CommandText += " Update fact set FaSparePay  = ISNULL(FaSparePay,0)-ISNULL(FaFirPayabl,0)+@nowQ where fano=@fano;";
                            cmd.CommandText += " Update fact set FaPayable = ISNULL(FaPayable,0)+ISNULL(FaSparePay,0) where fano=@fano;";
                            cmd.CommandText += " Update fact set FaFirPayabl = @nowQ where fano=@fano;";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = " Select FaFirPayabl,FaSparePay,FaPayable from fact where fano=@fano;";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (dataGridViewT1.EditingControl != null)
                                        dataGridViewT1.EditingControl.Text = reader["FaFirPayabl"].ToString();
                                    dt.Rows[e.RowIndex]["期初帳款金額"] = reader["FaFirPayabl"].ToDecimal();
                                    dt.Rows[e.RowIndex]["期初帳款餘額"] = reader["FaSparePay"].ToDecimal();
                                    dt.Rows[e.RowIndex]["應付帳款餘額"] = reader["FaPayable"].ToDecimal();
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
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.期初預付金額))
            {
                #region 期初預付金額
                var fano = dataGridViewT1["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var nowQ = dataGridViewT1["期初預付金額", e.RowIndex].EditedFormattedValue.ToDecimal();

                if (_OldFaFirAdvamt == nowQ)
                    return;

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("fano", fano);
                            cmd.Parameters.AddWithValue("nowQ", nowQ);
                            cn.Open();
                            tn = cn.BeginTransaction();
                            cmd.Transaction = tn;

                            cmd.CommandText = "  Update fact set FaPayAmt  = ISNULL(FaPayAmt,0)-ISNULL(FaFirPayAmt,0)+@nowQ where fano=@fano;";
                            cmd.CommandText += " Update fact set FaFirPayAmt = @nowQ where fano=@fano;";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = " Select FaFirPayAmt,FaPayAmt from fact where fano=@fano;";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (dataGridViewT1.EditingControl != null)
                                        dataGridViewT1.EditingControl.Text = reader["FaFirPayAmt"].ToString();
                                    dt.Rows[e.RowIndex]["期初預付金額"] = reader["FaFirPayAmt"].ToDecimal();
                                    dt.Rows[e.RowIndex]["預付餘額"] = reader["FaPayAmt"].ToDecimal();
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
                var fano = dataGridViewT1["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var nowQ = dataGridViewT1["信用額度", e.RowIndex].EditedFormattedValue.ToDecimal();

                if (_OldFaCredit == nowQ)
                    return;

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("fano", fano);
                            cmd.Parameters.AddWithValue("nowQ", nowQ);
                            cn.Open();
                            tn = cn.BeginTransaction();
                            cmd.Transaction = tn;

                            cmd.CommandText = " Update fact set FaCredit = @nowQ where fano=@fano;";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = " Select FaCredit from fact where fano=@fano;";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (dataGridViewT1.EditingControl != null)
                                        dataGridViewT1.EditingControl.Text = reader["FaCredit"].ToString();
                                    dt.Rows[e.RowIndex]["信用額度"] = reader["FaCredit"].ToDecimal();
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
                var cuno = dataGridViewT1["廠商編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var now_xa1par = dataGridViewT1["帳款期初匯率", e.RowIndex].EditedFormattedValue.ToDecimal();

                if (_OldFaFirPayPar == now_xa1par)
                    return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("FaFirPayPar", now_xa1par);
                    cmd.Parameters.AddWithValue("fano", cuno);
                    cmd.CommandText = "Update fact set FaFirPayPar = @FaFirPayPar where fano=@fano";
                    cmd.ExecuteNonQuery();

                }

                dt.Rows[e.RowIndex]["帳款期初匯率"] = now_xa1par;
                dataGridViewT1.InvalidateRow(e.RowIndex);


                #endregion
            }
        }

         

    }
}
