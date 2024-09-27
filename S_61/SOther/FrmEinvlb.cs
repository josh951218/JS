using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmEinvlb : Formbase, JBS.JS.IxOpen
    {
        public FrmEinvlb()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable dt = new DataTable();
        List<DataRow> list = new List<DataRow>();

        private void FrmEinvlb_Shown(object sender, EventArgs e)
        {
            EinvidQ.Focus();
        }

        private void FrmEinvlb_Load(object sender, EventArgs e)
        {
            try
            {
                this.TResult = "";

                // this.Style = FormStyle.Max;


                loadDB();
                if (dt.Rows.Count > 0)
                {
                    writeToTxt();
                    int row = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo ?? "", r["Einvid"].ToString()) > 0) + 1;
                    row = (row >= list.Count) ? list.Count - 1 : row;
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = row;
                    dataGridViewT1.Rows[row].Selected = true;
                }
                EinvidQ.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from Einvsetup  order by Einvid ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        dt.Clear(); list.Clear();

                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            list = dt.AsEnumerable().ToList();
                        }

                    }
                }
                writeToTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void writeToTxt()
        {
            if (dt.Rows.Count > 0)
                dataGridViewT1.DataSource = dt;
            else
                dataGridViewT1.DataSource = null;
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (EinvidQ.Text.Trim() == "" && EinvTitleQ.Text.Trim() == "")
                return;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cm = conn.CreateCommand();
                    cm.Parameters.Clear();

                    cm.Parameters.AddWithValue("@Einvid", EinvidQ.Text.Trim());
                    cm.Parameters.AddWithValue("@EinvTitle", EinvTitleQ.Text.Trim());


                    string str = "select * from Einvsetup where 0 = 0 ";
                    foreach (Control tb in this.Controls)
                    {
                        if (tb is TextBox)
                        {

                            if (EinvidQ.Text.Trim() != "")
                                str += " and Einvid like '%'+@Einvid+'%'";
                            if (EinvTitleQ.Text.Trim() != "")
                                str += " and EinvTitle like '%'+@EinvTitle+'%'";
                        }
                    }
                    str += " order by Einvid ";
                    cm.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(cm);

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
                    cm.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                string TempID = dataGridViewT1.SelectedRows[0].Cells["Einvid"].Value.ToString();
                loadDB();
                if (list.Count < 1) return;
                var dr = list.Find(r => r.Field<string>("Einvid") == TempID);
                if (dr == null)
                {
                    writeToTxt();
                    MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.TResult = dr["Einvid"].ToString().Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }
    }
}
