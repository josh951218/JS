using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;


namespace S_61.SOther
{
    public partial class FrmItemLevelb : Form
    { 
        public string TSeekNo { private get; set; }
  
        public decimal Result = 0;
        public string itunit = "";
        DataTable dt = new DataTable();

        public FrmItemLevelb()
        {
            InitializeComponent();
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.售價.Visible = Common.User_SalePrice;


            dt.Columns.Add("lv", typeof(string));
            dt.Columns.Add("price", typeof(decimal));

            if (JEInitialize.IsRunTime)
            {
                dataGridViewT1.DefaultCellStyle.Font = JEInitialize.ControlFontSize;
                for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
                {
                    dataGridViewT1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewT1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    if (dataGridViewT1.Columns[i].GetType() == typeof(DataGridViewTextBoxColumn) || dataGridViewT1.Columns[i].GetType() == typeof(DataGridViewTextNumberT))
                    {
                        var maxlen = ((DataGridViewTextBoxColumn)dataGridViewT1.Columns[i]).MaxInputLength;
                        if (maxlen <= 150)
                        {
                            dataGridViewT1.Columns[i].Width = (int)((maxlen * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
                        }
                    }
                }
            }
        }

        private void FrmItemLevelb_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("itno", this.TSeekNo ?? "");
                cmd.CommandText = "select * from item where itno=@itno";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        DataRow row;
                        if (reader["itunitp"].ToString().Trim() == itunit && itunit.Length > 0)
                        {
                            row = dt.NewRow();
                            row["lv"] = "售價";
                            row["price"] = reader["itpricep"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價一";
                            row["price"] = reader["itpricep1"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價二";
                            row["price"] = reader["itpricep2"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價三";
                            row["price"] = reader["itpricep3"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價四";
                            row["price"] = reader["itpricep4"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價五";
                            row["price"] = reader["itpricep5"].ToDecimal();
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            row = dt.NewRow();
                            row["lv"] = "售價";
                            row["price"] = reader["itprice"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價一";
                            row["price"] = reader["itprice1"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價二";
                            row["price"] = reader["itprice2"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價三";
                            row["price"] = reader["itprice3"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價四";
                            row["price"] = reader["itprice4"].ToDecimal();
                            dt.Rows.Add(row);

                            row = dt.NewRow();
                            row["lv"] = "售價五";
                            row["price"] = reader["itprice5"].ToDecimal();
                            dt.Rows.Add(row);
                        }
                        dt.AcceptChanges();
                    }
                }
            }
            dataGridViewT1.DataSource = dt;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0 && dataGridViewT1.SelectedRows.Count > 0)
                Result = dataGridViewT1.SelectedRows[0].Cells["售價"].Value.ToDecimal();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            this.Close();
        }

        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            btnGet_Click(null, null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F9:
                    btnSave.Focus();
                    btnSave.PerformClick();
                    break;
                case Keys.F11:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dt.Clear();
            base.OnFormClosing(e);
        }
    }
}
