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
    public partial class FrmAdjust_Print_AdNo : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>();

        public FrmAdjust_Print_AdNo()
        {
            InitializeComponent();
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
        }

        private void FrmAdjust_Print_AdNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            dataGridViewT1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (Common.User_DateTime == 1)
                this.調整日期.DataPropertyName = "AdDate";
            else
                this.調整日期.DataPropertyName = "AdDate1";

            dataGridViewT2.AutoGenerateColumns = false;
            dataGridViewT2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewT2.Columns["品名規格"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            loadM();
            if (Master.Rows.Count > 0)
            {
                writeToTxt();

                int i = list.FindIndex(r => r["adno"].ToString() == this.TSeekNo);
                if (i == -1)
                {
                    i = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["adno"].ToString()) > 0);
                    i = (i == -1) ? 0 : i;
                    dataGridViewT1.FirstDisplayedScrollingColumnIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;

                }
                AdNo.Focus();
            }

        }

        private void loadM()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select * from adjust order by adno COLLATE Chinese_Taiwan_Stroke_BIN";
                    SqlDataAdapter dd = new SqlDataAdapter(sql, cn);
                    Master.Clear();
                    dd.Fill(Master);
                    if (Master.Rows.Count > 0)
                    {
                        list = Master.AsEnumerable().ToList();
                        //lblT1.Text = "總共有" + list.Count.ToString() + "筆資料";
                    }
                    //else
                    //lblT1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void writeToTxt()
        {
            if (Master.Rows.Count > 0)
            {
                dataGridViewT1.DataSource = Master;
                dataGridViewT2.DataSource = Detail;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (AdNo.Text == "") return;

            foreach (Control tb in this.Controls)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Enter += new EventHandler(txtonenter);
                }
            }

            try
            {
                string sql = "select adno from adjust where '0'='0'";
                if (AdNo.Text.ToString() != "0")
                {
                    sql += " and adno like '%' + @adno + '%'";
                }
                else
                {
                    sql += "order by SaNo COLLATE Chinese_Taiwan_Stroke_BIN";
                }
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (AdNo.Text.ToString() != "0") cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            Master.Clear();
                            dd.Fill(Master);
                            if (Master.Rows.Count > 0)
                            {
                                list.Clear();
                                list = Master.AsEnumerable().ToList();
                            }
                            else
                            {
                                list.Clear();
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

        private void txtonenter(object sender, EventArgs e)
        {
            loadM();
            foreach (Control tb in (sender as Control).Parent.Controls)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Text = "";
                    (tb as TextBox).Enter -= txtonenter;
                }
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 0) return;
            var TempID = dataGridViewT1.SelectedRows[0].Cells["調整單號"].Value.ToString();

            this.TResult = TempID.Trim();
            this.DialogResult = DialogResult.OK; 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
                btnQuery.PerformClick();

            if (keyData == Keys.F9)
                btnGet.PerformClick();

            if (keyData == Keys.F11)
                btnExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.Rows.Count > 0)
                btnGet_Click(null, null);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count < 1)
            {
                Detail.Clear();
                return;
            }
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                try
                {
                    string adno = dataGridViewT1.SelectedRows[0].Cells["調整單號"].Value.ToString();
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            string sql = "select * from adjustd where adno=@adno COLLATE Chinese_Taiwan_Stroke_CS_AS";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("adno", adno.Trim());
                            cmd.CommandText = sql;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                Detail.Clear();
                                da.Fill(Detail);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
        }

        private void AdNo_TextChanged(object sender, EventArgs e)
        {
            if (AdNo.Text == "")
                return;

            loadM();
            if (Master.Rows.Count < 0) return;
            writeToTxt();
            if (list.Count > 0)
            {
                if (AdNo.Text != "")
                {
                    int i = list.FindIndex(r => r.Field<string>("adno").ToString() == AdNo.Text);
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingColumnIndex = i;
                        dataGridViewT1.Rows[i].Selected = true;
                    }
                    else
                    {
                        i = list.FindLastIndex(r => string.CompareOrdinal(AdNo.Text, r.Field<string>("adno")) > 0);
                        if (i == -1) i = 0;
                        dataGridViewT1.FirstDisplayedScrollingColumnIndex = i;
                        dataGridViewT1.Rows[i].Selected = true;


                    }

                }
            }
        }

    }
}
