using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmQuote_Print_QuNo : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>();
        
        int index = 0;

        public FrmQuote_Print_QuNo()
        {
            InitializeComponent();

            this.報價總額.DefaultCellStyle.Format = "f" + Common.MST;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單價.DefaultCellStyle.Format = "f" + Common.MS;

            this.稅別.Visible = this.報價總額.Visible = this.單價.Visible = Common.User_SalePrice;
        }

        private void FrmQuote_Print_QuNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            if (Common.User_DateTime == 1)
                this.報價日期.DataPropertyName = "qudate";
            else
                this.報價日期.DataPropertyName = "qudate1";

            loadM();
            if (list.Count > 0)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["quno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["quno"].ToString()) > 0);
                    if (index == -1 || ++index >= list.Count)
                        index = list.Count - 1;
                }
                dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                dataGridViewT1.Rows[index].Selected = true;
            }
            QuNo.Focus();
        }
         
        void loadM()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlDataAdapter dd = new SqlDataAdapter("select x3name = "
                        + " case "
                        + " when x3no=1 then '外加稅'"
                        + " when x3no=2 then '內含稅'"
                        + " when x3no=3 then '零稅'"
                        + " when x3no=4 then '免稅'"
                        + " end,* from quote order by quno", cn);
                    Master.Clear();
                    list.Clear();
                    dd.Fill(Master);
                    if (Master.Rows.Count > 0)
                    {
                        list = Master.AsEnumerable().ToList(); 
                        dataGridViewT1.DataSource = Master;
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select 序號='',* from quoted where quno=@quno";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("quno", dataGridViewT1.SelectedRows[0].Cells["報價單號"].Value.ToString());
                    cmd.CommandText = sql;
                    SqlDataAdapter dd = new SqlDataAdapter(cmd);
                    Detail.Clear();
                    dd.Fill(Detail);
                    if (Detail.Rows.Count > 0)
                    {
                        for (int i = 0; i < Detail.Rows.Count; i++)
                            Detail.Rows[i]["序號"] = (i + 1).ToString();
                        dataGridViewT2.DataSource = Detail;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void dataGridViewT2_DoubleClick(object sender, EventArgs e)
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (QuNo.Text == "") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from quoted where quno like '%' + @quno + '%' order by quno";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("quno", QuNo.Text);
                    cmd.CommandText = sql;
                    SqlDataAdapter dd = new SqlDataAdapter(cmd);
                    Master.Clear();
                    list.Clear();
                    dd.Fill(Master);
                    if (Master.Rows.Count > 0)
                    {
                        list = Master.AsEnumerable().ToList();
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = 0;
                        dataGridViewT1.Rows[0].Selected = true;
                    }
                    else
                    {
                        Detail.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            string quno = dataGridViewT1.SelectedRows[0].Cells["報價單號"].Value.ToString();
            loadM();
            index = list.FindIndex(r => r["quno"].ToString() == quno);
            if (index == -1)
            {
                MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.TResult = quno;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
