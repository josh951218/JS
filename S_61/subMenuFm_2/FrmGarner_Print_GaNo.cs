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
    public partial class FrmGarner_Print_GaNo : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>(); 
        int index = 0;

        public FrmGarner_Print_GaNo()
        {
            InitializeComponent();
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
        }

        private void FrmGarner_Print_GaNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            if (Common.User_DateTime == 1)
                this.入庫日期.DataPropertyName = "gadate";
            else
                this.入庫日期.DataPropertyName = "gadate1";
             
            loadM();
            if (list.Count > 0)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["gano"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["gano"].ToString()) > 0);
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
                    SqlDataAdapter dd = new SqlDataAdapter("select * from garner order by gano", cn);
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
                    string sql = "select 序號='',* from garnerd where gano=@gano";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gano", dataGridViewT1.SelectedRows[0].Cells["入庫單號"].Value.ToString());
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
            if (GaNo.Text == "") return;

            foreach (Control tb in this.Controls)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Enter += new EventHandler(txtonenter);
                }
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from garner where gano like '%' + @gano + '%' order by gano";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gano", GaNo.Text.Trim());
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
            string gano = dataGridViewT1.SelectedRows[0].Cells["入庫單號"].Value.ToString();

            this.TResult = gano;
            this.DialogResult = DialogResult.OK;

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

        private void GaNo_TextChanged(object sender, EventArgs e)
        { 
            if (GaNo.Text == "") return;
            loadM();
            if (list.Count > 0)
            {
                index = list.FindIndex(r => string.CompareOrdinal(GaNo.Text, r["gano"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(GaNo.Text, r["gano"].ToString()) > 0);
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
