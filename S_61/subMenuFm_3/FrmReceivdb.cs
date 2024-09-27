using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmReceivdb : Formbase
    {
        DataTable dt = new DataTable();
        DataTable dtBomD = new DataTable();
        List<DataRow> list = new List<DataRow>();
        List<DataRow> listReceivd = new List<DataRow>();
        DataRow dr;
        public TextBox tb;
        public string size;
        public string current;
        public string callType;

        public FrmReceivdb()
        {
            InitializeComponent();

            //dataGridViewT1.限制輸入_列名_整數_小數_空值 = new string[] 
            //{
            //    "沖抵帳款,"+14+","+  Common.MST +",0,0,1",
            //    "累入預收,"+14+","+  Common.MST +",0,0,1"
            //};
            this.沖抵帳款.DefaultCellStyle.Format = "f" + Common.MST;
            this.累入預收.DefaultCellStyle.Format = "f" + Common.MST;
            //dataGridViewT2.限制輸入_列名_整數_小數_空值 = new string[] 
            //{
            //    "折讓金額,"+14+","+  Common.MST +",0,0,1",
            //    "沖帳金額,"+14+","+  Common.MST +",0,0,1"
            //};
            this.折讓金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.沖帳金額.DefaultCellStyle.Format = "f" + Common.MST;
        }

        private void FrmReceivdb_Load(object sender, EventArgs e)
        {
            loadDB();
            if (dt.Rows.Count > 0)
            {
                writeToTxt();
                int t = list.FindIndex(r => r.Field<string>("ReNo") == current);
                if (t != -1)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = t;
                    dataGridViewT1.Rows[t].Selected = true;
                }
            }
            ReNo.Focus();
        }

        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select * from Receiv order by ReNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    SqlDataAdapter da = new SqlDataAdapter(str, conn);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        list.Clear();
                        list = dt.AsEnumerable().ToList();
                    }
                    else
                    {
                        list.Clear();
                    }
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void loadBomD()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                string str = "select * from Receivd where ReNo=@reno order by RecordNo";
                SqlCommand cmd = conn.CreateCommand();
                cmd.Parameters.AddWithValue("reno", dataGridViewT1.CurrentRow.Cells["沖款單號"].Value);
                cmd.CommandText = str;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtBomD.Clear();
                da.Fill(dtBomD);
                if (dtBomD.Rows.Count > 0)
                {
                    listReceivd.Clear();
                    listReceivd = dtBomD.AsEnumerable().ToList();
                }
                else
                {
                    listReceivd.Clear();
                }
                da.Dispose();
                cmd.Cancel(); cmd.Dispose();
                conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadBomDError:\n" + ex.ToString());
            }
        }

        private void writeToTxt()
        {
            if (dt.Rows.Count > 0)
            {
                dataGridViewT1.DataSource = dt;
            }
            else
            {
                dataGridViewT1.DataSource = null;
            }
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
        }

        private void btnStockQuery_Click(object sender, EventArgs e)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                loadBomD();
                if (dtBomD.Rows.Count > 0)
                {
                    dataGridViewT2.DataSource = dtBomD;
                }
                else
                {
                    dataGridViewT2.DataSource = null;
                }
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (list.Count < 1) return;
            current = dataGridViewT1.CurrentCell.OwningRow.Cells["沖款單號"].Value.ToString();
            dr = list.Find(r => r.Field<string>("ReNo") == current);
            if (dr == null)
            {
                writeToTxt();
                MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (callType == "PrintReNo")
            {
                tb.Text = current;
            }
            else if (callType == "PrintReNo_1")
            {
                tb.Text = current;
            }
            else
            {
                pVar.FrmReceivd.Browtemp = dataGridViewT1.CurrentCell.OwningRow.Cells["沖款單號"].Value.ToString();
                pVar.FrmReceivd.receivd_Brow();
            }
            this.Close();
            this.Dispose();
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void ReNo_TextChanged(object sender, EventArgs e)
        {
            loadDB();
            writeToTxt();
            if (list.Count > 0)
            {
                int i;
                try
                {
                    i = list.FindIndex(r => r.Field<string>((sender as TextBox).Name).StartsWith((sender as TextBox).Text.Trim()));
                }
                catch
                {
                    i = -1;
                }
                if (i != -1)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Cells[0].Selected = true;
                    dataGridViewT1_SelectionChanged(null, null);
                }
                else
                {
                    i = list.FindLastIndex(r => string.CompareOrdinal((sender as TextBox).Text, r.Field<string>((sender as TextBox).Name)) > 0);
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        dataGridViewT1_SelectionChanged(null, null);
                    }
                    else
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = list.Count - 1;
                        dataGridViewT1.Rows[list.Count - 1].Cells[0].Selected = true;
                        dataGridViewT1_SelectionChanged(null, null);
                    }
                }
            }
        }

    }
}
