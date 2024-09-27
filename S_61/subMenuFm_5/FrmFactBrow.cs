using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;
using S_61.SOther;

namespace JE.S5
{
    public partial class FrmFactBrow : Formbase
    {
        public DataRow Result { get; set; }

        public string OpenGetResult { get; set; }

        public Formbase TOpemForm()
        {
            return new FrmFactBrow(true, FormStyle.Mini, true);
        }

        List<TextBoxbase> list;
        DataTable table = new DataTable();
        string SearchItem = "FaNo,FaName1,FaPer1,FaTel1,FaAtel1,FaFax1,FaPer,FaR1,FaAddr1,FaName2,FaXa1No,FaIme,FaX12No,FaEmno1,Faudf1";

        public FrmFactBrow() { }
        public FrmFactBrow(bool AllowAppend, FormStyle style = FormStyle.Max, bool WorkForBrow = true)
        {
            this.Style = style;
            InitializeComponent();
            list = this.getEnumMember();
            btnAppend.Enabled = AllowAppend;

            if (!WorkForBrow)
            {
                this.行動電話一.Visible = false;
                this.傳真.Visible = false;
                this.負責人.Visible = false;
                this.交易幣別.Visible = false;
                this.注音速查.Visible = false;
                this.廠商類別.Visible = false;
                this.採購人員.Visible = false;
            }
        }

        private void FrmFactBrow_Load(object sender, EventArgs e)
        {
            LoadDB(this.SeekNo);
            FaNo.Focus();
        }

        void LoadDB(string str)
        {
            try
            {
                table.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("key", str);

                    if (str.Trim() == "")
                    {
                        cmd.CommandText = "select top(" + Common.SearchCount + ")" + SearchItem + " from fact";
                        dd.Fill(table);
                    }
                    else
                    {
                        cmd.CommandText = "select top(" + Common.SearchCount / 2 + ")" + SearchItem + " from fact where fano <=@key order by fano desc";
                        dd.Fill(table);

                        cmd.CommandText = "select top(" + Common.SearchCount / 2 + ")" + SearchItem + " from fact where fano >@key";
                        dd.Fill(table);
                    }

                }

                dataGridViewT1.SuspendLayout();
                dataGridViewT1.DataSource = null;

                dataGridViewT1.DataSource = table;
                dataGridViewT1.ResumeLayout();

                GridViewSeekRow(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }

        void GridViewSeekRow(string value)
        {
            int index = 0;
            if (value.Trim() != "")
            {
                index = table.Rows.Count - 1;
                System.Threading.Tasks.Parallel.For(0, table.Rows.Count, r =>
                {
                    //找到一樣一筆，直接離開
                    if (table.Rows[r]["fano"].ToString().Trim() == value.Trim())
                    {
                        index = (int)r;
                        return;
                    }
                    else if (table.Rows[r]["fano"].ToString().Trim().StartsWith(value.Trim()))
                    {
                        if ((int)r < index)
                            index = (int)r;
                    }
                    //若找到相近一筆，要比r，比較小的給index

                });
                dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
                dataGridViewT1.Rows[index].Selected = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Result = null;
            table.Clear();
            list.Clear();
            this.Dispose();
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            using (FrmFact frm = new FrmFact())
            {
                this.SeekNo = frm.ShowDialogCallBack();
                LoadDB(SeekNo);
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index >= 0)
                {
                    Result = Common.load("Check", "fact", "fano", table.DefaultView[index]["fano"].ToString());
                    this.SeekNo = dataGridViewT1["廠商編號", index].Value.ToString();
                    OpenGetResult = dataGridViewT1["廠商編號", index].Value.ToString();
                    table.Clear();
                    list.Clear();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var HasSearchCode = list.Any(r => r.TrimTextLenth() > 0);
            if (!HasSearchCode) return;
            try
            {
                table.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Select " + SearchItem + " From fact where 0=0 ";

                    if (FaNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                        cmd.CommandText += " And FaNo like '%'+@FaNo+'%' ";
                    }
                    if (FaName1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@FaName1", FaName1.Text.Trim());
                        cmd.CommandText += " And FaName1 like '%'+@FaName1+'%' ";
                    }
                    if (FaPer1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@FaPer1", FaPer1.Text.Trim());
                        cmd.CommandText += " And FaPer1 like '%'+@FaPer1+'%' ";
                    }
                    if (FaTel1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@FaTel1", FaTel1.Text.Trim());
                        cmd.CommandText += " And FaTel1 like '%'+@FaTel1+'%' ";
                    }
                    if (FaIme.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@FaIme", FaIme.Text.Trim());
                        cmd.CommandText += " And FaIme like '%'+@FaIme+'%' ";
                    }
                    if (FaUdf1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@FaUdf1", FaUdf1.Text.Trim());
                        cmd.CommandText += " And FaUdf1 like '%'+@FaUdf1+'%' ";
                    }
                    if (FaAtel1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@FaAtel1", FaAtel1.Text.Trim());
                        cmd.CommandText += " And FaAtel1 like '%'+@FaAtel1+'%' ";
                    }
                    dataGridViewT1.SuspendLayout();
                    dataGridViewT1.DataSource = null;
                    da.Fill(table);
                }
                FaNo.Enter += new EventHandler(Text_OnEnter);
                FaName1.Enter += new EventHandler(Text_OnEnter);
                FaPer1.Enter += new EventHandler(Text_OnEnter);
                FaTel1.Enter += new EventHandler(Text_OnEnter);
                FaIme.Enter += new EventHandler(Text_OnEnter);
                FaUdf1.Enter += new EventHandler(Text_OnEnter);
                FaAtel1.Enter += new EventHandler(Text_OnEnter);

                dataGridViewT1.DataSource = table;
                dataGridViewT1.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            LoadDB(SeekNo);
            foreach (Control tb in (sender as Control).Parent.Controls)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Text = "";
                    (tb as TextBox).Enter -= Text_OnEnter;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                btnAppend.Focus();
                btnAppend.PerformClick();
            }
            else if (keyData == Keys.F6)
            {
                btnQuery.Focus();
                btnQuery.PerformClick();
            }
            else if (keyData == Keys.F9)
            {
                dataGridViewT1.Focus();
                btnGet_Click(null, null);
            }
            else if (keyData == Keys.F11)
            {
                this.Dispose();
            }
            else if (keyData == Keys.Escape)
            {
                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        
    }
}
