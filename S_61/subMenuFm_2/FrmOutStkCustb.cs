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
    public partial class FrmOutStkCustb : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();
        List<DataRow> list = new List<DataRow>();
   

        public FrmOutStkCustb()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }

        private void FrmOutStkCustb_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            temp = dt.Copy();
            list = dt.AsEnumerable().ToList();

            if (dt.Rows.Count > 0)
            {
                writeToTxt();
                int row = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo, r["CuNo"].ToString()) > 0) + 1;
                row = (row >= list.Count) ? list.Count - 1 : row;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = row;
                dataGridViewT1.Rows[row].Selected = true;
            }
            CuNo.Focus();
        }

        void writeToTxt()
        {
            if (dt.Rows.Count > 0)
                dataGridViewT1.DataSource = dt;
            else
                dataGridViewT1.DataSource = null;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                string TempID = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();

                this.TResult = TempID.Trim();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6) btnQuery.PerformClick();
            else if (keyData == Keys.F9) btnGet.PerformClick();
            else if (keyData == Keys.F11) btnExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void QueryFunction(int i)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                string str = "select * from cust where '0'='0' ";
                if (CuNo.Text.Trim() != "") str += " and CuNo like @CuNo + '%' ";
                if (CuPer1.Text.Trim() != "") str += " and CuPer1 like @CuPer1 + '%' ";
                if (CuAtel1.Text.Trim() != "") str += " and CuAtel1 like @CuAtel1 + '%' ";
                if (CuName1.Text.Trim() != "") str += " and CuName1 like @CuName1 + '%' ";
                if (CuIme.Text.Trim() != "") str += " and CuIme like @CuIme + '%' ";
                if (CuTel1.Text.Trim() != "") str += " and CuTel1 like @CuTel1 + '%' ";
                if (CuUdf1.Text.Trim() != "") str += " and CuUdf1 like @CuUdf1 + '%' ";

                cmd.Parameters.Clear();
                if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                if (CuPer1.Text.Trim() != "") cmd.Parameters.AddWithValue("CuPer1", CuPer1.Text.Trim());
                if (CuAtel1.Text.Trim() != "") cmd.Parameters.AddWithValue("CuAtel1", CuAtel1.Text.Trim());
                if (CuName1.Text.Trim() != "") cmd.Parameters.AddWithValue("CuName1", CuName1.Text.Trim());
                if (CuIme.Text.Trim() != "") cmd.Parameters.AddWithValue("CuIme", CuIme.Text.Trim());
                if (CuTel1.Text.Trim() != "") cmd.Parameters.AddWithValue("CuTel1", CuTel1.Text.Trim());
                if (CuUdf1.Text.Trim() != "") cmd.Parameters.AddWithValue("CuUdf1", CuUdf1.Text.Trim());
                cmd.CommandText = str;
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    str = "0";
                    if (rd.HasRows)
                    {
                        if (rd.Read())
                            str = rd["CuNo"].ToString();
                    }
                }
                cmd.Dispose();
                conn.Close(); conn.Dispose();

                if (str != "0")
                {
                    i = list.FindIndex(r => r.Field<string>("CuNo") == str);
                    if (i == -1) return;
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Cells[0].Selected = true;
                }
                else
                {
                    if (CuNo.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("CuNo") == CuNo.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(CuNo.Text.Trim(), r.Field<string>("CuNo")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (CuPer1.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("CuPer1") == CuPer1.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(CuPer1.Text.Trim(), r.Field<string>("CuPer1")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (CuAtel1.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("CuAtel1") == CuAtel1.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(CuAtel1.Text.Trim(), r.Field<string>("CuAtel1")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (CuName1.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("CuName1") == CuName1.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(CuName1.Text.Trim(), r.Field<string>("CuName1")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (CuIme.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("CuIme") == CuIme.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(CuIme.Text.Trim(), r.Field<string>("CuIme")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (CuTel1.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("CuTel1") == CuTel1.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(CuTel1.Text.Trim(), r.Field<string>("CuTel1")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }

                    if (CuUdf1.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("CuUdf1") == CuUdf1.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(CuUdf1.Text.Trim(), r.Field<string>("CuUdf1")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
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
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Enter -= new EventHandler(Text_OnEnter);
                tb.Enter += new EventHandler(Text_OnEnter);

                if (dt.Rows.Count > 0)
                {
                    var rows = dt.AsEnumerable();
                    if (tb.Text.Length > 0)
                    {
                        rows = rows.Where(r => r[tb.Name.Trim()].ToString().Trim().Contains(tb.Text.Trim()));
                    }
                    if (rows.Count() == 0)
                    {
                        dt.Clear();
                    }
                    else
                    {
                        dt = rows.CopyToDataTable();
                    }
                }
            }
            writeToTxt();
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Clear();
            }
            dt = temp.Copy();
            writeToTxt();
        }

        private void CuNo_TextChanged(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() == ""
                && CuPer1.Text.Trim() == ""
                && CuAtel1.Text.Trim() == ""
                && CuName1.Text.Trim() == ""
                && CuIme.Text.Trim() == ""
                && CuTel1.Text.Trim() == ""
                && CuUdf1.Text.Trim() == "")
                return;
            QueryFunction(0);
        }
    }
}
