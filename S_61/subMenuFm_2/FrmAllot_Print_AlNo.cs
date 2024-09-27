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
    public partial class FrmAllot_Print_AlNo : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>();
        int index = 0;

        public FrmAllot_Print_AlNo()
        {
            InitializeComponent();
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
        }

        private void FrmAllot_Print_AlNo_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            dataGridViewT1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (Common.User_DateTime == 1)
                this.調撥日期.DataPropertyName = "aldate1";
            else
                this.調撥日期.DataPropertyName = "aldate2";

            dataGridViewT2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewT2.Columns["品名規格"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
 

            loadM();
            if (list.Count > 0)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["alno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["alno"].ToString()) > 0);
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
                    using (SqlDataAdapter dd = new SqlDataAdapter("select * from allot order by alno", cn))
                    {
                        Master.Clear();
                        list.Clear();
                        dd.Fill(Master);
                        if (Master.Rows.Count > 0)
                        {
                            list = Master.AsEnumerable().ToList();
                            //AlNoCount.Text = "總共有" + (Master.Rows.Count).ToString() + "筆資料";
                            dataGridViewT1.DataSource = Master;
                        }
                        //else
                        //{
                        //    AlNoCount.Text = "";
                        //}
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
                    string sql = "select 序號='',* from allotd where alno=@alno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("alno", dataGridViewT1.SelectedRows[0].Cells["調撥單號"].Value.ToString());
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
            if (AlNo.Text == "") return;

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
                    string sql = "select * from allot where alno like '%' + @alno + '%' order by alno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("alno", AlNo.Text);
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
            string alno = dataGridViewT1.SelectedRows[0].Cells["調撥單號"].Value.ToString();
            
            this.TResult = alno;
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

        private void AlNo_TextChanged(object sender, EventArgs e)
        { 
            if (AlNo.Text == "") 
                return;

            loadM();
            if (list.Count > 0)
            {
                index = list.FindIndex(r => string.CompareOrdinal(AlNo.Text, r["alno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(AlNo.Text, r["alno"].ToString()) > 0);
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
