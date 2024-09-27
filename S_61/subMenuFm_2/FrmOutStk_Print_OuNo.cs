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
    public partial class FrmOutStk_Print_OuNo : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
       
        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>(); 
        int index = 0;

        public FrmOutStk_Print_OuNo()
        {
            InitializeComponent();
            this.領出數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.寄庫未領數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單價.DefaultCellStyle.Format = "f" + Common.MS;
        }

        private void FrmOutStk_Print_OuNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            dataGridViewT1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (Common.User_DateTime == 1)
                this.領出日期.DataPropertyName = "oudate";
            else
                this.領出日期.DataPropertyName = "oudate1";

             
            loadM();
            if (list.Count > 0)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["ouno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["ouno"].ToString()) > 0);
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
                    SqlDataAdapter dd = new SqlDataAdapter("select * from oustk order by ouno", cn);
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
                    string sql = "select 序號='',* from oustkd where ouno=@ouno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("ouno", dataGridViewT1.SelectedRows[0].Cells["領出單號"].Value.ToString());
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
            if (OuNo.Text == "") return;

            try
            {
                 
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from oustk where ouno like '%' + @ouno + '%' order by ouno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("ouno", OuNo.Text.Trim());
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
            string ouno = dataGridViewT1.SelectedRows[0].Cells["領出單號"].Value.ToString();
            loadM();
            index = list.FindIndex(r => r["ouno"].ToString() == ouno);
            if (index == -1)
            {
                MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.SeekNo = ouno;
                //result = leno;
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

        private void OuNo_TextChanged(object sender, EventArgs e)
        { 
            if (OuNo.Text == "") return;
            loadM();
            if (list.Count > 0)
            {
                index = list.FindIndex(r => string.CompareOrdinal(OuNo.Text, r["ouno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(OuNo.Text, r["ouno"].ToString()) > 0);
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
