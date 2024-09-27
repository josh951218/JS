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
    public partial class FrmPayabldb : Formbase
    {
        DataTable dt = new DataTable();
        DataTable dtBomD = new DataTable();
        List<DataRow> list = new List<DataRow>();
        List<DataRow> listPayabld = new List<DataRow>();
        public TextBox tb;
        DataRow dr;
        public string size;
        public string current;
        public string callType;

        public FrmPayabldb()
        {
            InitializeComponent();

            //dataGridViewT1.限制輸入_列名_整數_小數_空值 = new string[] 
            //{
            //    "沖抵帳款,"+14+","+  Common.MFT +",0,0,1",
            //    "累入預付,"+14+","+  Common.MFT +",0,0,1"
            //};
            this.沖抵帳款.DefaultCellStyle.Format = "f" + Common.MFT;
            this.累入預付.DefaultCellStyle.Format = "f" + Common.MFT;
            //dataGridViewT2.限制輸入_列名_整數_小數_空值 = new string[] 
            //{
            //    "折讓金額,"+14+","+  Common.MFT +",0,0,1",
            //    "沖帳金額,"+14+","+  Common.MFT +",0,0,1"
            //};
            this.折讓金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.沖帳金額.DefaultCellStyle.Format = "f" + Common.MFT;
        }

        private void FrmPayabldb_Load(object sender, EventArgs e)
        {

            loadDB();
            if (dt.Rows.Count > 0)
            {
                writeToTxt();
                int t = list.FindIndex(r => r.Field<string>("PaNo") == current);
                if (t != -1)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = t;
                    dataGridViewT1.Rows[t].Selected = true;
                }
            }
            PaNo.Focus();
        }

        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select * from Payabl order by PaNo COLLATE Chinese_Taiwan_Stroke_BIN";
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
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = Common.sqlConnString;
                    conn.Open();
                    string str = "select * from Payabld where PaNo=@pano order by RecordNo";
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("pano", dataGridViewT1.CurrentRow.Cells["沖款單號"].Value);
                        cmd.CommandText = str;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dtBomD.Clear();
                        da.Fill(dtBomD);
                        if (dtBomD.Rows.Count > 0)
                        {
                            listPayabld.Clear();
                            listPayabld = dtBomD.AsEnumerable().ToList();
                        }
                        else
                        {
                            listPayabld.Clear();
                        }
                        da.Dispose();
                    }
                }
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



        //功能按鈕
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
            dr = list.Find(r => r.Field<string>("PaNo") == current);
            if (dr == null)
            {
                writeToTxt();
                MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (callType == "PrintPaNo")
            {
                tb.Text = current;
            }
            else if (callType == "PrintPaNo_1")
            {
                tb.Text = current;
            }
            else
            {
                pVar.FrmPayabld.Browtemp = dataGridViewT1.CurrentCell.OwningRow.Cells["沖款單號"].Value.ToString();
                pVar.FrmPayabld.Payabld_Brow();
            }
            this.Close();
            this.Dispose();
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void PaNo_TextChanged(object sender, EventArgs e)
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
