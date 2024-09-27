using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;


namespace S_61.subMenuFm_2
{
    public partial class FrmBShop_Inv : Formbase
    {
        public string[] Result { get; set; }
        public string ivno = "";

        string SaleOrShop = "";
        string tableName = "";
        string pkno = "";
        bool IsPOS = true;

        DataTable dt = new DataTable();

        public FrmBShop_Inv()
        {
            InitializeComponent();
            Result = new string[4];

        }

        public FrmBShop_Inv(string name, string pk, bool b)
        {
            InitializeComponent();
            Result = new string[4];

            tableName = name;
            pkno = pk;
            IsPOS = b;
        }

        private void FrmBShop_Inv_Load(object sender, EventArgs e)
        {
            InvDate.SetDateLength();
            InvDate.ReadOnly = InvAddr1.ReadOnly = InvName.ReadOnly = InvTaxNo.ReadOnly = false;

            this.發票日期.DataPropertyName = Common.User_DateTime == 1 ? "inv1date" : "inv1date1";

            if (tableName.ToUpper().Contains("SALE")) SaleOrShop = "sano";
            if (tableName.ToUpper().Contains("SHOP")) SaleOrShop = "bsno";
            this.單據憑證.DataPropertyName = SaleOrShop;

            this.稅前金額.Set銷貨單據小數();
            this.營業稅額.Set銷項稅額小數();
            this.應收總額.Set銷貨單據小數();
            dataGridViewT1.DataSource = dt;

            dataGridViewT1.ReadOnly = false;
            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name == "發票號碼") dataGridViewT1.Columns[i].ReadOnly = false;
                else
                    dataGridViewT1.Columns[i].ReadOnly = true;
            }

            InvDate.Text = Result[0];
            InvName.Text = Result[1];
            InvTaxNo.Text = Result[2];
            InvAddr1.Text = Result[3];

            if (pkno.Trim().Length > 0) LoadDB();
            if (dataGridViewT1.Rows.Count > 0)
            {
                dataGridViewT1.CurrentCell = dataGridViewT1["發票號碼", 0];
            }

        }

        void LoadDB()
        {
            string SaleOrShop = "";
            string sql = "";

            if (tableName.ToUpper().Contains("SALE")) SaleOrShop = "sano";
            if (tableName.ToUpper().Contains("SHOP")) SaleOrShop = "bsno";
            if (IsPOS)
            {
                sql = "select *,inv1date = invdate,inv1date1=invdate1,oldinvno=invno from posinv where " + SaleOrShop + " =@pkno order by invno";
            }
            else
            {
                sql = "select *,inv1date = invdate,inv1date1=invdate1,oldinvno=invno from " + tableName + " where " + SaleOrShop + " =@pkno";
            }
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("pkno", pkno);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            da.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["inv1date"] = Date.AddLine(dt.Rows[i]["invdate"].ToString());
                                dt.Rows[i]["inv1date1"] = Date.AddLine(dt.Rows[i]["invdate1"].ToString());
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //媒體申報
            if (InvKind.Text != "")
            {
                if (InvKind.Text.Substring(0, 2) == "28")
                {
                    if (CustomsNo.Text.Trim() == "")
                    {
                        CustomsNo.Focus();
                        MessageBox.Show("海關憑證不可為空，請確認後重新輸入。");
                        return;
                    }
                }
            }

            if (tableName.ToUpper().Contains("SALE")) SaleOrShop = "sano";
            if (tableName.ToUpper().Contains("SHOP")) SaleOrShop = "bsno";

            Result[0] = InvDate.Text.Trim();
            Result[1] = InvName.Text.Trim();
            Result[2] = InvTaxNo.Text.Trim();
            Result[3] = InvAddr1.Text.Trim();

            try
            {
                if (dataGridViewT1.Rows.Count > 0)
                {
                    var index = dataGridViewT1.Rows.Count - 1;
                    ivno = dt.AsEnumerable().Max(r => r["invno"].ToString());

                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            if (IsPOS)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("invno", dt.Rows[i]["invno"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(InvDate.Text.Trim()));
                                    cmd.Parameters.AddWithValue("invdate1", Date.ToUSDate(InvDate.Text.Trim()));
                                    cmd.Parameters.AddWithValue("poid", dt.Rows[i]["poid"].ToString().Trim());
                                    cmd.CommandText = " update posinv set "
                                        + " invno =@invno,"
                                        + " invdate =@invdate,"
                                        + " invdate1 =@invdate1"
                                        + " where poid=@poid";
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("InvDate", Date.ToTWDate(Result[0]));
                            cmd.Parameters.AddWithValue("InvDate1", Date.ToUSDate(Result[0]));
                            cmd.Parameters.AddWithValue("InvName", Result[1]);
                            cmd.Parameters.AddWithValue("InvTaxNo", Result[2]);
                            cmd.Parameters.AddWithValue("InvAddr1", Result[3]);
                            cmd.Parameters.AddWithValue("invno", ivno);
                            cmd.Parameters.AddWithValue("pkno", pkno.Trim());
                            cmd.CommandText = "Update " + tableName
                                + " Set InvDate=@InvDate"
                                + ",InvDate1=@InvDate1"
                                + ",InvName=@InvName"
                                + ",InvTaxNo=@InvTaxNo"
                                + ",InvAddr1=@InvAddr1"
                                + ",invno=@invno"
                                + " where " + SaleOrShop + " =@pkno";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("修改完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }

        private void InvDate_Validating(object sender, CancelEventArgs e)
        {
            if (!InvDate.IsDateTime())
            {
                e.Cancel = true;
                if (Common.User_DateTime == 1)
                    InvDate.Text = Date.GetDateTime(1, false);
                else
                    InvDate.Text = Date.GetDateTime(2, false);

                InvDate.SelectAll();
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["invdate"] = Date.ToTWDate(InvDate.Text);
                    dt.Rows[i]["invdate1"] = Date.ToUSDate(InvDate.Text);
                    dt.Rows[i]["inv1date"] = Date.AddLine(dt.Rows[i]["invdate"].ToString());
                    dt.Rows[i]["inv1date1"] = Date.AddLine(dt.Rows[i]["invdate1"].ToString());
                }
            }

        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (btnExit.Focused) return;
            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dt.Rows[e.RowIndex]["invno"] = dataGridViewT1["發票號碼", e.RowIndex].EditedFormattedValue.ToString().Trim();

            //if (dataGridViewT1.Columns[e.ColumnIndex].Name == "發票號碼")
            //{
            //    var vno = dataGridViewT1["發票號碼", e.RowIndex].EditedFormattedValue.ToString().Trim();
            //    var vno1 = dataGridViewT1["舊的發票號碼", e.RowIndex].Value.ToString().Trim();
            //    if (vno.skipString(2).ToDecimal() < vno1.skipString(2).ToDecimal())
            //    {
            //        MessageBox.Show("新更正的發票號碼不可小於訂正前的發票號碼！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        e.Cancel = true;
            //        return;
            //    }
            //    if (vno != vno1)
            //    {
            //        try
            //        {
            //            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            //            {
            //                string sql = "select * from posinv where invno=N'" + vno + "'";
            //                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            //                {
            //                    DataTable t = new DataTable();
            //                    da.Fill(t);
            //                    if (t.Rows.Count == 1)
            //                    {
            //                        MessageBox.Show("此發票號碼已被開立，銷貨單號：" + t.Rows[0]["sano"].ToString() + "！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                        e.Cancel = true;
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString());
            //        }
            //    }
            //}

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                btnSave.PerformClick();
            }
            else if (keyData == Keys.F4)
            {
                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dt.Clear();
            base.OnFormClosing(e);
        }

        private void InvKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InvKind.Text.Substring(0, 2) == "28")
                CustomsNo.ReadOnly = false;
            else
            {
                CustomsNo.ReadOnly = true;
                CustomsNo.Text = "";
            }

        }
    }
}
