using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace JBS.JS
{
    public partial class FrmXxBrow<T> : Formbase, JBS.JS.IxOpen where T : JBS.JS.IxBrow, JBS.JS.IxValidate, new()
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo { get; set; }

        T t; 
        string Columns = "*"; 
        List<TextBoxbase> list = new List<TextBoxbase>();
        DataTable dt = new DataTable();
        List<DataRow> li = new List<DataRow>();

        public FrmXxBrow(int count = 2, string columns = "*", string[] gtext = null, string[] gdata = null)
        {
            InitializeComponent();
            this.t = new T();
            this.list = this.getEnumMember();

            this.XNo.MaxLength = t.xNoLength;
            this.XNo.Name = t.xNoID;

            this.XName.MaxLength = t.xNameLength;
            this.XName.Name = t.xNameID;

            this.編號.HeaderText = t.gridNoText;
            this.編號.DataPropertyName = t.xNoID;
            this.編號.MaxInputLength = 61;

            this.名稱.HeaderText = t.gridNameText;
            this.名稱.DataPropertyName = t.xNameID;
            this.名稱.MaxInputLength = 61;

            this.lblNo.Text = t.gridNoText;
            this.lblName.Text = t.gridNameText;
             
            this.Columns = columns;

            if (count == 3)
            {
                this.三行.Visible = true;

                this.編號.MaxInputLength = 40;
                this.名稱.MaxInputLength = 40;
                this.三行.MaxInputLength = 40;

                this.三行.HeaderText = gtext[0];
                this.三行.DataPropertyName = gdata[0];
            }

            if (count == 5)
            {
                this.三行.Visible = true;
                this.四行.Visible = true;
                this.五行.Visible = true;

                this.編號.MaxInputLength = 23;
                this.名稱.MaxInputLength = 23;
                this.三行.MaxInputLength = 23;
                this.四行.MaxInputLength = 23;
                this.五行.MaxInputLength = 23;

                this.三行.HeaderText = gtext[0];
                this.四行.HeaderText = gtext[1];
                this.五行.HeaderText = gtext[2];

                this.三行.DataPropertyName = gdata[0];
                this.四行.DataPropertyName = gdata[1];
                this.五行.DataPropertyName = gdata[2];
            }
        }

        private void FrmXXBorw_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.btnAppend.Enabled = false;
                this.Style = FormStyle.Max;
            }
            else
            {
                this.btnAppend.Enabled = true;
                this.Style = FormStyle.Mini;

                if (t is JBS.JS.XX05)
                    this.btnAppend.Enabled = false;

                if (t is JBS.JS.XX03)
                    this.btnAppend.Enabled = false;
            }

            LoadDB();
            dataGridViewT1.DataSource = dt;

            GoToTSeekNo();

            XNo.Focus();
        }

        private void LoadDB()
        {
            dt.Clear();
            li.Clear();
            using (var db = new JBS.xSQL())
            {
                var tsql = "Select " + Columns + " from " + t.ValiTable + " order by " + t.ValiKey;
                db.Fill(tsql, spc => spc.AddWithValue("x", "x"), ref dt);
                this.li = dt.AsEnumerable().ToList();
            }
        }

        private void GoToTSeekNo()
        {
            if (dt.Rows.Count > 0)
            {
                int index = li.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo ?? "", r[t.ValiKey].ToString()) > 0) + 1;
                if (index >= li.Count)
                    index = li.Count - 1;

                dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                dataGridViewT1.Rows[index].Selected = true;
            }
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            this.TSeekNo = t.ShowDialog();

            LoadDB();
            GoToTSeekNo();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                string TempID = dataGridViewT1.SelectedRows[0].Cells["編號"].Value.ToString().Trim();

                LoadDB();
                var row = li.Find(r => r.Field<string>(t.ValiKey) == TempID);
                if (row == null)
                {
                    MessageBox.Show("您選取的資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.TResult = row[t.ValiKey].ToString().Trim();
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

        private void XNo_TextChanged(object sender, EventArgs e)
        {
            if (XNo.TrimTextLenth() > 0 && XName.TrimTextLenth() > 0)
                dt.DefaultView.Search(ref dataGridViewT1, t.xNoID, XNo.Text, t.xNameID, XName.Text);
            else if (XNo.TrimTextLenth() > 0)
                dt.DefaultView.Search(ref dataGridViewT1, t.xNoID, XNo.Text);
            else if (XName.TrimTextLenth() > 0)
                dt.DefaultView.Search(ref dataGridViewT1, t.xNameID, XName.Text);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2 && btnAppend.Enabled) btnAppend.PerformClick();
            else if (keyData == Keys.F6) btnQuery.PerformClick();
            else if (keyData == Keys.F9) btnGet.PerformClick();
            else if (keyData == Keys.F11) btnExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            LoadDB();
            foreach (Control tb in list)
            {
                tb.Text = "";
                tb.Enter -= Text_OnEnter;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (XNo.Text.Trim() == "" && XName.Text.Trim() == "") return;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@No", XNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", XName.Text.Trim());

                    string str = "Select " + this.Columns + " from " + t.ValiTable + " Where 0=0 ";

                    foreach (Control tb in list)
                    {
                        if (XNo.Text.Trim() != "")
                            str += " and " + t.xNoID + " like '%' + @No + '%'";
                        if (XName.Text.Trim() != "")
                            str += " and " + t.xNameID + " like '%' + @Name + '%'";

                        if (tb is TextBox)
                        {
                            (tb as TextBox).Enter += new EventHandler(Text_OnEnter);
                        }
                    }

                    str += " Order by " + t.ValiKey;

                    cmd.CommandText = str;

                    dt.Clear();
                    li.Clear();

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        li = dt.AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}