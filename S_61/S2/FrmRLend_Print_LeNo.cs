using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmRLend_Print_LeNo : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; } 
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>();
        int index = 0;

        public FrmRLend_Print_LeNo()
        {
            InitializeComponent();
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單價.DefaultCellStyle.Format = "f" + Common.MS;
            this.單價.Visible = Common.User_SalePrice;
        }

        private void FrmLend_Print_LeNoNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            dataGridViewT1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (Common.User_DateTime == 1)
                this.還入日期.DataPropertyName = "ledate";
            else
                this.還入日期.DataPropertyName = "ledate1";

             
            loadM();
            if (list.Count > 0)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["leno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["leno"].ToString()) > 0);
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
                    SqlDataAdapter dd = new SqlDataAdapter("select * from rlend order by leno", cn);
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
                    string sql = "select 序號='',* from rlendd where leno=@leno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("leno", dataGridViewT1.SelectedRows[0].Cells["還入單號"].Value.ToString());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (LeNo.Text == "") return;

            try
            {
                //foreach (Control tb in tableLayoutPnl2.Controls)
                //{
                //    if (tb is TextBox)
                //    {
                //        (tb as TextBox).Enter += new EventHandler(txtonenter);
                //    }
                //}

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from rlend where leno like '%' + @leno + '%' order by leno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("leno", LeNo.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
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
            string leno = dataGridViewT1.SelectedRows[0].Cells["還入單號"].Value.ToString();
            loadM();
            index = list.FindIndex(r => r["leno"].ToString() == leno);
            if (index == -1)
            {
                MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.TResult = leno.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
         
        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void dataGridViewT2_DoubleClick(object sender, EventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void LeNo_TextChanged(object sender, EventArgs e)
        { 
            if (LeNo.Text == "") return;
            loadM();
            if (list.Count > 0)
            {
                index = list.FindIndex(r => string.CompareOrdinal(LeNo.Text, r["leno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(LeNo.Text, r["leno"].ToString()) > 0);
                    if (index == -1 || ++index >= list.Count)
                        index = list.Count - 1;
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                    dataGridViewT1.Rows[index].Selected = true;
                }
                else
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                    dataGridViewT1.Rows[index].Selected = true;
                }
            }
        }
         



    }
}
