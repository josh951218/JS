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
    public partial class FrmCustb : Formbase, JBS.JS.IxOpen
    {
        JBS.JS.Cust jCust;
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; } 
        [Obsolete("Don't use this", true)]
        public new string SeekNo;  

        List<TextBoxT> list;
        DataTable dt = new DataTable();

        public FrmCustb()
        {
            InitializeComponent();
            this.jCust = new JBS.JS.Cust();
            this.Style = FormStyle.Mini;
            this.list = new List<TextBoxT>() { CuNo, CuPer1, CuName1, CuTel1, CuAtel1, CuIme, CuUdf1, cuaddr1 };
        }

        private void FrmCustb_Load(object sender, EventArgs e)
        {  
            if (this.Owner != null)
            {
                this.點選.DisplayIndex = 0;
                this.點選.Visible = true;
                this.btnBatDel.Enabled = true;
                this.btnAppend.Enabled = false;
                this.Style = FormStyle.Max;
            }

            dataGridViewT1.DataSource = dt.DefaultView; 
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            jCust.Search(this.TSeekNo, ref dt);
            jCust.SeekCurrent(this.TSeekNo, dt, dataGridViewT1);
            dataGridViewT1.Focus();
            //CuNo.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCust())
            {
                var cuno = "";
                frm.ShowDialog(out cuno);

                jCust.Search(cuno, ref dt);
                jCust.SeekCurrent(cuno, dt, dataGridViewT1);
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "點選")
                return;

            btnGet_Click(null, null);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            this.TResult = "";
            if (dataGridViewT1.Rows.Count > 0)
            {
                string TempID = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();
                if (jCust.IsExist(TempID) == false)
                {
                    MessageBox.Show("您選取的資料已被刪除!");

                    jCust.Search("", ref dt);
                    jCust.SeekCurrent("", dt, dataGridViewT1);
                    return;
                }
                this.TResult = TempID.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.Tag != null && this.Tag.ToString() == "delete")
                this.DialogResult = DialogResult.Yes;

            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var A = keyData.ToString();
            if (keyData == Keys.F2) btnAppend.PerformClick();
            else if (keyData == Keys.F6) btnQuery.PerformClick();
            else if (keyData == Keys.F9) btnGet.PerformClick();
            else if (keyData == Keys.F11) btnExit.PerformClick();
            else if (keyData.ToString() == "Tab")
            {
                if (!dataGridViewT1.Focused)
                {
                    dataGridViewT1.CurrentCell = dataGridViewT1[0, dataGridViewT1.CurrentCell.RowIndex];
                    dataGridViewT1.Focus();
                    return true;
                }
                else
                {
                    CuNo.Focus();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBatDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("請確定是否批次刪除客戶?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                return;

            var dlist = dt.AsEnumerable()
                .Where(r => r["點選"].ToString() == "V")
                .Select(r => r["cuno"].ToString().Trim());

            if (dlist.Any() == false)
                return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("cuno", "");
                foreach (var cuno in dlist)
                {
                    cmd.Parameters["cuno"].Value = cuno;
                    cmd.CommandText = " Delete from cust where cuno=@cuno ";
                    cmd.ExecuteNonQuery();
                }
            }

            this.Tag = "delete";
            jCust.Search("", ref dt);
            jCust.SeekCurrent("", dt, dataGridViewT1);
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt.Rows.Count == 0) return;
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "點選")
            {
                dt.Rows[e.RowIndex]["點選"] = dt.Rows[e.RowIndex]["點選"].ToString().Trim() == "V" ? "" : "V";
                if (dt.Rows[e.RowIndex]["點選"].ToString().Trim() == "V")
                {
                    DataTable t = new DataTable();
                    var b = false;
                    var cuno = dt.Rows[e.RowIndex]["cuno"].ToString().Trim();
                    try
                    {
                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        {
                            cn.Open();
                            SqlCommand cmd = cn.CreateCommand();
                            cmd.Parameters.AddWithValue("cuno", cuno);
                            cmd.CommandText = "select COUNT(*) from sale where cuno=@cuno";
                            b = b | cmd.ExecuteScalar().ToDecimal() > 0;

                            cmd.CommandText = "select COUNT(*) from rsale where cuno=@cuno";
                            b = b | cmd.ExecuteScalar().ToDecimal() > 0;

                            cmd.CommandText = "select COUNT(*) from receiv where cuno=@cuno";
                            b = b | cmd.ExecuteScalar().ToDecimal() > 0;

                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (b)
                    {
                        dt.Rows[e.RowIndex]["點選"] = "";
                        MessageBox.Show("此客戶已有交易記錄，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (cuno == Common.User_CuNo)
                    {
                        dt.Rows[e.RowIndex]["點選"] = "";
                        MessageBox.Show("此客戶為預設客戶，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void CuNo_TextChanged(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() == ""
            && CuPer1.Text.Trim() == ""
            && CuAtel1.Text.Trim() == ""
            && CuName1.Text.Trim() == ""
            && CuIme.Text.Trim() == ""
            && CuTel1.Text.Trim() == ""
            && CuUdf1.Text.Trim() == ""
            && cuaddr1.Text.Trim() == "")
            {
                jCust.Search("", ref dt);
                jCust.SeekCurrent("", dt, dataGridViewT1);
                return;
            }

            if (CuNo.TrimTextLenth() > 0)
            {
                jCust.Search(CuNo.Text.Trim(), ref dt);
                jCust.SeekCurrent(CuNo.Text.Trim(), dt, dataGridViewT1);
            }
            else
            {
                OtherQuery();
            }
        }

        private void OtherQuery()
        {
            if (list.All(t => t.TrimTextLenth() == 0))
                return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = "Select 點選='',* from Cust where 0=0 ";
                foreach (TextBoxT tb in list)
                {
                    if (tb.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue(tb.Name, tb.Text);
                        cmd.CommandText += " and " + tb.Name + " like '%'+@" + tb.Name + "+'%'";
                    } 
                }

                var ob = string.Join(",", list.Where(t => t.TrimTextLenth() > 0).Select(t => t.Name));
                cmd.CommandText += " order by ";
                cmd.CommandText += ob;

                dt.Clear();
                da.Fill(dt);
            }

            if (CuNo.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuNo.Text, dt, dataGridViewT1, CuNo.Name);
            else if (CuName1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuName1.Text, dt, dataGridViewT1, CuName1.Name);
            else if (CuPer1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuPer1.Text, dt, dataGridViewT1, CuPer1.Name);
            else if (CuIme.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuIme.Text, dt, dataGridViewT1, CuIme.Name);
            else if (CuTel1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuTel1.Text, dt, dataGridViewT1, CuTel1.Name);
            else if (CuAtel1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuAtel1.Text, dt, dataGridViewT1, CuAtel1.Name);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (list.All(t => t.TrimTextLenth() == 0))
                return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = "Select 點選='',* from Cust where 0=0 ";
                foreach (TextBoxT tb in list)
                {
                    if (tb.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue(tb.Name, tb.Text);
                        cmd.CommandText += " and " + tb.Name + " like '%'+@" + tb.Name + "+'%'";
                    }
                    tb.Enter -= new EventHandler(Text_OnEnter);
                    tb.Enter += new EventHandler(Text_OnEnter);
                }

                var ob = string.Join(",", list.Where(t => t.TrimTextLenth() > 0).Select(t => t.Name));
                cmd.CommandText += " order by ";
                cmd.CommandText += ob;

                dt.Clear();
                da.Fill(dt);
            }

            if (CuNo.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuNo.Text, dt, dataGridViewT1, CuNo.Name);
            else if (CuName1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuName1.Text, dt, dataGridViewT1, CuName1.Name);
            else if (CuPer1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuPer1.Text, dt, dataGridViewT1, CuPer1.Name);
            else if (CuIme.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuIme.Text, dt, dataGridViewT1, CuIme.Name);
            else if (CuTel1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuTel1.Text, dt, dataGridViewT1, CuTel1.Name);
            else if (CuAtel1.TrimTextLenth() > 0)
                jCust.SeekCurrent(CuAtel1.Text, dt, dataGridViewT1, CuAtel1.Name);
            else if (CuAtel1.TrimTextLenth() > 0)
                jCust.SeekCurrent(cuaddr1.Text, dt, dataGridViewT1, cuaddr1.Name);
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            foreach (TextBoxT tb in list)
            {
                tb.Clear();
                tb.Enter -= new EventHandler(Text_OnEnter);
            }

            jCust.Search("", ref dt);
            jCust.SeekCurrent("", dt, dataGridViewT1);
        }

    }
}
