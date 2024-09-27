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
    public partial class FrmEmplb : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable dt = new DataTable();
        List<DataRow> list = new List<DataRow>();
          
        public FrmEmplb()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }

        private void FrmEmplb_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            if (this.Owner != null)
            {
                btnAppend.Enabled = false;
                this.Style = FormStyle.Max;
            }

            loadDB();
            if (dt.Rows.Count > 0)
            {
                writeToTxt();
                int row = list.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo ?? "", r["EmNo"].ToString()) > 0) + 1;
                row = (row >= list.Count) ? list.Count - 1 : row;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = row;
                dataGridViewT1.Rows[row].Selected = true;
            }
            EmNo.Focus();
        }

        private void FrmEmplb_Shown(object sender, EventArgs e)
        {
            EmNo.Focus();
        }
         
        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from empl where Len(EmOutday) = 0 order by emno ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        dt.Clear(); list.Clear();

                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            list = dt.AsEnumerable().ToList();
                            lblT3.Text = "總共有 " + list.Count.ToString() + " 筆資料";
                        }
                        else
                            lblT3.Text = "";
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

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                string TempID = dataGridViewT1.SelectedRows[0].Cells["員工編號"].Value.ToString();
                loadDB();
                if (list.Count < 1) return;
                var dr = list.Find(r => r.Field<string>("EmNo") == TempID);
                if (dr == null)
                {
                    writeToTxt();
                    MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                 
                this.TResult = dr["EmNo"].ToString().Trim();
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
          
        void QueryFunction(int i)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmNo", EmNo.Text.Trim());
                cmd.Parameters.AddWithValue("@EmName", EmName.Text.Trim());
                cmd.Parameters.AddWithValue("@EmSex", EmSex.Text.Trim());
                cmd.Parameters.AddWithValue("@EmReg", EmReg.Text.Trim());
                cmd.Parameters.AddWithValue("@EmIdno", EmIdno.Text.Trim());
                cmd.Parameters.AddWithValue("@EmTel", EmTel.Text.Trim());
                cmd.Parameters.AddWithValue("@EmAtel1", EmAtel1.Text.Trim());



                string str = "select * from empl where Len(EmOutday) = 0 ";
                if (EmNo.Text.Trim() != "")
                    str += " and EmNo like @EmNo+'%'";
                if (EmName.Text.Trim() != "")
                    str += " and EmName like @EmName+'%'";
                if (EmSex.Text.Trim() != "")
                    str += " and EmSex like @EmSex+'%'";
                if (EmReg.Text.Trim() != "")
                    str += " and EmReg like  @EmReg+'%'";
                if (EmIdno.Text.Trim() != "")
                    str += " and EmIdno =  @EmIdno ";
                if (EmTel.Text.Trim() != "")
                    str += " and EmTel like  @EmTel+'%'";
                if (EmAtel1.Text.Trim() != "")
                    str += " and EmAtel1 like @EmAtel1+'%'";

                str += " COLLATE Chinese_Taiwan_Stroke_BIN";

                cmd.CommandText = str;
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    str = "0";
                    if (rd.HasRows)
                    {
                        if (rd.Read())
                            str = rd["EmNo"].ToString();
                    }
                }
                cmd.Dispose();
                conn.Close(); conn.Dispose();

                if (str != "0")
                {
                    i = list.FindIndex(r => r.Field<string>("EmNo") == str);
                    if (i == -1) return;
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Cells[0].Selected = true;
                }
                else
                {
                    if (EmNo.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("EmNo") == EmNo.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(EmNo.Text.Trim(), r.Field<string>("EmNo")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (EmName.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("EmName") == EmName.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(EmName.Text.Trim(), r.Field<string>("EmName")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (EmSex.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("EmSex") == EmSex.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(EmSex.Text.Trim(), r.Field<string>("EmSex")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (EmReg.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("EmReg") == EmReg.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(EmReg.Text.Trim(), r.Field<string>("EmReg")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (EmIdno.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("EmIdno") == EmIdno.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(EmIdno.Text.Trim(), r.Field<string>("EmIdno")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }
                    if (EmTel.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("EmTel") == EmTel.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(EmTel.Text.Trim(), r.Field<string>("EmTel")) > 0);
                            if (i == -1) return;
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        return;
                    }

                    if (EmAtel1.Text.Trim() != "")
                    {
                        i = list.FindIndex(r => r.Field<string>("EmAtel1") == EmAtel1.Text.Trim());
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Cells[0].Selected = true;
                        }
                        else
                        {
                            i = list.FindLastIndex(r => string.CompareOrdinal(EmAtel1.Text.Trim(), r.Field<string>("EmAtel1")) > 0);
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
            if (EmNo.Text.Trim() == ""
                && EmName.Text.Trim() == ""
                && EmSex.Text.Trim() == ""
                && EmReg.Text.Trim() == ""
                && EmIdno.Text.Trim() == ""
                && EmTel.Text.Trim() == ""
                && EmAtel1.Text.Trim() == "")
                return;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cm = conn.CreateCommand();
                    cm.Parameters.Clear();
                    cm.Parameters.AddWithValue("@EmNo", EmNo.Text.Trim());
                    cm.Parameters.AddWithValue("@EmName", EmName.Text.Trim());
                    cm.Parameters.AddWithValue("@EmSex", EmSex.Text.Trim());
                    cm.Parameters.AddWithValue("@EmIdno", EmIdno.Text.Trim());
                    cm.Parameters.AddWithValue("@EmReg", EmReg.Text.Trim());
                    cm.Parameters.AddWithValue("@EmTel", EmTel.Text.Trim());
                    cm.Parameters.AddWithValue("@EmAtel1", EmAtel1.Text.Trim());

                    string str = "select * from Empl";
                    str += " where Len(EmOutday) = 0 ";
                    foreach (Control tb in this.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if (EmNo.Text.Trim() != "")
                                str += " and EmNo like '%'+@EmNo+'%'";
                            if (EmName.Text.Trim() != "")
                                str += " and EmName like '%'+@EmName+'%'";
                            if (EmSex.Text.Trim() != "")
                                str += " and EmSex like '%'+@EmSex+'%'";
                            if (EmIdno.Text.Trim() != "")
                                str += " and EmIdno = '@EmIdno' ";//" and EmIdno like '%'+@EmIdno+'%'";
                            if (EmReg.Text.Trim() != "")
                                str += " and EmReg like '%'+@EmReg+'%'";
                            if (EmTel.Text.Trim() != "")
                                str += " and EmTel like '%'+@EmTel+'%'";
                            if (EmAtel1.Text.Trim() != "")
                                str += " and EmAtel1 like '%'+ @EmAtel1+'%'";
                            (tb as TextBox).Enter += new EventHandler(Text_OnEnter);
                        }
                    }
                    str += " order by emno ";
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

        private void Text_OnEnter(object sender, EventArgs e)
        {
            loadDB();
            foreach (Control tb in (sender as Control).Parent.Controls)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Text = "";
                    (tb as TextBox).Enter -= Text_OnEnter;
                }
            }
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmEmpl())
            {
                var itno = "";
                frm.ShowDialog(out itno);

                loadDB();
                if (dt.Rows.Count > 0)
                    dataGridViewT1.DataSource = dt;

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2) btnAppend.PerformClick();
            else if (keyData == Keys.F6) btnQuery.PerformClick();
            else if (keyData == Keys.F9) btnGet.PerformClick();
            else if (keyData == Keys.F11) btnExit.PerformClick();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void EmNo_TextChanged(object sender, EventArgs e)
        {
            if (EmNo.Text.Trim() == ""
                && EmName.Text.Trim() == ""
                && EmSex.Text.Trim() == ""
                && EmReg.Text.Trim() == ""
                && EmIdno.Text.Trim() == ""
                && EmTel.Text.Trim() == ""
                && EmAtel1.Text.Trim() == "")
                return;
            QueryFunction(0);
        }






    }
}
