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
    public partial class FrmFactb : Formbase, JBS.JS.IxOpen
    {
        JBS.JS.Fact jFact;
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        List<TextBoxT> list;
        DataTable dt = new DataTable(); 

        public FrmFactb()
        {
            InitializeComponent();
            this.jFact = new JBS.JS.Fact();
            this.Style = FormStyle.Mini;
            this.list = new List<TextBoxT>() { FaNo, FaName1, FaPer1, FaTel1, FaAtel1, FaIme, FaUdf1 };
        }

        private void FrmFactb_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.點選.DisplayIndex = 0;
                this.點選.Visible = true;
                this.btnBatDel.Enabled = true;
                this.btnAppend.Enabled = false;
                this.Style = FormStyle.Max;
            }

            dataGridViewT1.DataSource = dt; 
        }

        protected override void OnShown(EventArgs e)
        { 
            base.OnShown(e);

            jFact.Search(this.TSeekNo, ref dt);
            jFact.SeekCurrent(this.TSeekNo, dt, dataGridViewT1);
            dataGridViewT1.Focus();
            //FaNo.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmFact())
            {
                var fano = "";
                frm.ShowDialog(out fano);

                jFact.Search(fano, ref dt);
                jFact.SeekCurrent(fano, dt, dataGridViewT1);
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
                string TempID = dataGridViewT1.CurrentCell.OwningRow.Cells["廠商編號"].Value.ToString();
                if (jFact.IsExist(TempID) == false)
                {
                    MessageBox.Show("您選取的資料已被刪除");

                    jFact.Search("", ref dt);
                    jFact.SeekCurrent("", dt, dataGridViewT1);
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
            if (keyData == Keys.F2) btnAppend.PerformClick();
            else if (keyData == Keys.F6) btnQuery.PerformClick();
            else if (keyData == Keys.F9) btnGet.PerformClick();
            else if (keyData == Keys.F11) btnExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBatDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("請確定是否批次刪除廠商?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) 
                return;

            var dlist = dt.AsEnumerable()
                 .Where(r => r["點選"].ToString() == "V")
                 .Select(r => r["fano"].ToString().Trim());

            if (dlist.Any() == false)
                return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("fano", "");
                foreach (var fano in dlist)
                {
                    cmd.Parameters["fano"].Value = fano;
                    cmd.CommandText = " Delete from fact where fano=@fano ";
                    cmd.ExecuteNonQuery();
                }
            }

            this.Tag = "delete";
            jFact.Search("", ref dt);
            jFact.SeekCurrent("", dt, dataGridViewT1);
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
                    var fano = dt.Rows[e.RowIndex]["fano"].ToString().Trim();
                    try
                    {
                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        {
                            cn.Open();
                            SqlCommand cmd = cn.CreateCommand();
                            cmd.Parameters.AddWithValue("fano", fano);
                            cmd.CommandText = "select COUNT(*) from bshop where fano=@fano";
                            b = b | cmd.ExecuteScalar().ToDecimal() > 0;

                            cmd.CommandText = "select COUNT(*) from rshop where fano=@fano";
                            b = b | cmd.ExecuteScalar().ToDecimal() > 0;

                            cmd.CommandText = "select COUNT(*) from payabl where fano=@fano";
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
                        MessageBox.Show("此廠商已有交易記錄，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void FaNo_TextChanged(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() == ""
            && FaPer1.Text.Trim() == ""
            && FaAtel1.Text.Trim() == ""
            && FaName1.Text.Trim() == ""
            && FaIme.Text.Trim() == ""
            && FaTel1.Text.Trim() == ""
            && FaUdf1.Text.Trim() == "")
            {
                jFact.Search("", ref dt);
                jFact.SeekCurrent("", dt, dataGridViewT1);
                return;
            }

            if (FaNo.TrimTextLenth() > 0)
            {
                jFact.Search(FaNo.Text.Trim(), ref dt);
                jFact.SeekCurrent(FaNo.Text.Trim(), dt, dataGridViewT1);
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
                cmd.CommandText = "Select 點選='',* from Fact where 0=0 ";
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

            if (FaNo.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaNo.Text, dt, dataGridViewT1, FaNo.Name);
            else if (FaName1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaName1.Text, dt, dataGridViewT1, FaName1.Name);
            else if (FaPer1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaPer1.Text, dt, dataGridViewT1, FaPer1.Name);
            else if (FaIme.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaIme.Text, dt, dataGridViewT1, FaIme.Name);
            else if (FaTel1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaTel1.Text, dt, dataGridViewT1, FaTel1.Name);
            else if (FaAtel1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaAtel1.Text, dt, dataGridViewT1, FaAtel1.Name);
        }
         
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (list.All(t => t.TrimTextLenth() == 0))
                return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = "Select 點選='',* from Fact where 0=0 ";
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

            if (FaNo.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaNo.Text, dt, dataGridViewT1, FaNo.Name);
            else if (FaName1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaName1.Text, dt, dataGridViewT1, FaName1.Name);
            else if (FaPer1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaPer1.Text, dt, dataGridViewT1, FaPer1.Name);
            else if (FaIme.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaIme.Text, dt, dataGridViewT1, FaIme.Name);
            else if (FaTel1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaTel1.Text, dt, dataGridViewT1, FaTel1.Name);
            else if (FaAtel1.TrimTextLenth() > 0)
                jFact.SeekCurrent(FaAtel1.Text, dt, dataGridViewT1, FaAtel1.Name);
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            foreach (TextBoxT tb in list)
            {
                tb.Clear();
                tb.Enter -= new EventHandler(Text_OnEnter);
            }

            jFact.Search("", ref dt);
            jFact.SeekCurrent("", dt, dataGridViewT1);
        }
    }
}