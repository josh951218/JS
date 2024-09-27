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
    public partial class 寄庫作業列印_選擇寄庫單號視窗 : Formbase
    {

        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }

        DataTable Master = new DataTable();
        DataTable Detail = new DataTable();
        List<DataRow> list = new List<DataRow>();
        int index = 0;

        public 寄庫作業列印_選擇寄庫單號視窗()
        {
            InitializeComponent();
            this.寄庫未領數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.寄庫數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單價.DefaultCellStyle.Format = "f" + Common.MS;

            this.TResult = "";

            dataGridViewT1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (Common.User_DateTime == 1)
                this.領出日期.DataPropertyName = "indate";
            else
                this.領出日期.DataPropertyName = "indate1";
            loadM();
            if (list.Count > 0)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["inno"].ToString()) == 0);
                if (index == -1)
                {
                    index = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["inno"].ToString()) > 0);
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
                    SqlDataAdapter dd = new SqlDataAdapter("select * from instk order by inno", cn);
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
                    string sql = "select 序號='',* from instkd where inno=@inno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("inno", dataGridViewT1.SelectedRows[0].Cells["寄庫單號"].Value.ToString());
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (InNo.Text == "") return;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from instk where inno like '%' + @inno + '%' order by inno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("inno", InNo.Text.Trim());
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
            string inno = dataGridViewT1.SelectedRows[0].Cells["寄庫單號"].Value.ToString();
            loadM();
            index = list.FindIndex(r => r["inno"].ToString() == inno);
            if (index == -1)
            {
                MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.SeekNo = inno;
                //result = leno;
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

        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            btnGet_Click(null, null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                btnQuery_Click(null,null);
                return true;      
            }
            else if(keyData == Keys.F9)
            {
                btnGet_Click(null, null);
                return true;
            }
            else if (keyData == Keys.F10)
            {
                btnExit_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}
