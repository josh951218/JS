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
    public partial class FrmFQuot_Print_FqNo : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>();
          
        int index = 0;

        public FrmFQuot_Print_FqNo()
        {
            InitializeComponent();
            this.詢價總額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單價.DefaultCellStyle.Format = "f" + Common.MF;

            this.詢價總額.Visible = Common.User_ShopPrice;
            this.單價.Visible = Common.User_ShopPrice;
        }

        private void FrmFQuot_Print_FqNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            if (Common.User_DateTime == 1)
                this.詢價日期.DataPropertyName = "fqdate";
            else
                this.詢價日期.DataPropertyName = "fqdate1";

            loadM();
            if (list.Count > 0)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["fqno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["fqno"].ToString()) > 0);
                    if (index == -1 || ++index >= list.Count)
                        index = list.Count - 1;
                }
                dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                dataGridViewT1.Rows[index].Selected = true;
            }
        }
         
        void loadM()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlDataAdapter dd = new SqlDataAdapter("select * from fquot order by fqno", cn);
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
                    string sql = "select 序號='',* from fquotd where fqno=@fqno";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fqno", dataGridViewT1.SelectedRows[0].Cells["詢價單號"].Value.ToString());
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
            if (FqNo.Text == "") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from fquot where fqno like '%' + @fqno + '%' order by fqno";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fqno", FqNo.Text);
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
            string fqno = dataGridViewT1.SelectedRows[0].Cells["詢價單號"].Value.ToString();
            loadM();
            index = list.FindIndex(r => r["fqno"].ToString() == fqno);
            if (index == -1)
            {
                MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.TResult = fqno;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridViewT2_DoubleClick(object sender, EventArgs e)
        {
            btnGet_Click(null, null);
        }
         
    }
}
