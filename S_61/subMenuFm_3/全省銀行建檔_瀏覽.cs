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
    public partial class 全省銀行建檔_瀏覽 : Formbase
    {
        DataTable tbM = new DataTable();
        List<DataRow> li = new List<DataRow>();

        string CurrentRow = "";
        public string result = "";
        public DataRow Result;
        public bool CanAppend { get; set; }
        public bool 開窗模式 = false;
        public string 票據連線字串 = "";

        public 全省銀行建檔_瀏覽()
        {
            InitializeComponent();

            //SC = new List<btnT> { btnSave, btnCancel };
            //Others = new List<btnT> { btnModify, btnExit };
        }

        private void 全省銀行建檔_瀏覽_Load(object sender, EventArgs e)
        {
            CN.ConnectionString = 票據連線字串;

            //SC.ForEach(r => r.Enabled = false);
            //Others.ForEach(r => r.Enabled = true);

            if (!CanAppend) Append.Visible = false;
            if (開窗模式)
            {
                tableLayoutPanelbase3.Visible = false;
                //tableLayoutPnl4.Visible = false;
                qBaNo.Focus();
            }
            else
            {
                tableLayoutPanelbase1.Visible = false;
                tableLayoutPanelbase2.Visible = false;
                //tableLayoutPnl2.Visible = false;
                //tableLayoutPnl3.Visible = false;
                BaNo.Focus();
            }
            loadM();
            SelectRow(SeekNo, "");
        }

        void loadM()
        {
            try
            {
                dataGridViewT1.DataSource = null;
                tbM.Clear();
                li.Clear();
                Bank.Fill(tbM);
                if (tbM.Rows.Count > 0) li = tbM.AsEnumerable().ToList();
                dataGridViewT1.DataSource = tbM;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void SelectRow(string BaNo, string BaName)
        {
            if (li.Count > 0)
            {
                if (BaNo != "" && BaName == "")
                {
                    dataGridViewT1.Search("銀行編號", BaNo);
                }
                else if (BaNo == "" && BaName != "")
                {
                    int i = li.FindIndex(r => r["baname"].ToString().Contains(BaName));
                    if (i == -1) return;
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                }
                else if (BaNo != "" && BaName != "")
                {
                    int i = li.FindIndex(r => string.CompareOrdinal(r["bano"].ToString(), BaNo) == 0);
                    if (i != -1)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Selected = true;
                    }
                    else
                    {
                        i = li.FindIndex(r => string.CompareOrdinal(r["baname"].ToString(), BaName) == 0);
                        if (i != -1)
                        {
                            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                            dataGridViewT1.Rows[i].Selected = true;
                            return;
                        }
                        var temp = tbM.AsEnumerable().Where(r => r["bano"].ToString().StartsWith(BaNo));
                        foreach (var t in temp)
                        {
                            if (t["baname"].ToString().Contains(BaName))
                            {
                                i = li.FindIndex(r => r["bano"].ToString() == t["bano"].ToString());
                                dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                                dataGridViewT1.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
                return;
            //SC.ForEach(r => r.Enabled = true);
            //Others.ForEach(r => r.Enabled = false);
            dataGridViewT1.ReadOnly = false;
            this.銀行編號.ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            CurrentRow = dataGridViewT1.SelectedRows[0].Cells["銀行編號"].Value.ToString().Trim();
            try
            {
                Bank.Update(tbM);
                loadM();
                SelectRow(CurrentRow, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            btnCancel_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridViewT1.ReadOnly = true;
            //SC.ForEach(r => r.Enabled = false);
            //Others.ForEach(r => r.Enabled = true);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count != 0)
                result = dataGridViewT1.SelectedRows[0].Cells["銀行編號"].Value.ToString().Trim();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }


        private void qBaNo_TextChanged(object sender, EventArgs e)
        { 
            SelectRow(qBaNo.Text, qBaName.Text);
        }

        private void qBaName_TextChanged(object sender, EventArgs e)
        { 
            SelectRow(qBaNo.Text, qBaName.Text);
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    if (開窗模式)
                        Exit.PerformClick();
                    else
                        btnExit.PerformClick();
                    break;
                case Keys.F9:
                    if (開窗模式)
                        Get.PerformClick();
                    else
                        btnSave.PerformClick();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
                case Keys.F2:
                    if (開窗模式)
                        Append.PerformClick();
                    break;
                case Keys.Escape:
                    if (開窗模式)
                        Exit.PerformClick();
                    else
                        btnExit.PerformClick();
                    break;
                case Keys.F6:
                    if (開窗模式)
                        Query.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
         
        private void BaNo_TextChanged(object sender, EventArgs e)
        { 
            SelectRow(BaNo.Text, BaName.Text);
        }

        private void BaName_TextChanged(object sender, EventArgs e)
        { 
            SelectRow(BaNo.Text, BaName.Text);
        }
         
        private void Append_Click(object sender, EventArgs e)
        {
            using (var frm = new 全省銀行建檔())
            {
                frm.票據連線字串 = 票據連線字串;
                frm.ShowDialog();
            }
            loadM();
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {

        }

        private void Query_Click(object sender, EventArgs e)
        {
            if (BaNo.Text == "" && BaName.Text == "") return;
            using (SqlConnection cn = new SqlConnection(票據連線字串))
            {
                cn.Open();
                string sql = "select * from bank where '0'='0' ";
                if (BaNo.Text.Trim() != "")
                    sql += " and bano like '%' + @bano + '%'";
                if (BaName.Text.Trim() != "")
                    sql += " and baname like '%' + @baname + '%'";
                sql += " order by bano";
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    if (BaNo.Text.Trim() != "") cmd.Parameters.AddWithValue("bano", BaNo.Text.Trim());
                    if (BaName.Text.Trim() != "") cmd.Parameters.AddWithValue("baname", BaName.Text.Trim());
                    cmd.CommandText = sql;
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        tbM.Clear();
                        dataGridViewT1.DataSource = null;
                        dd.Fill(tbM);
                        dataGridViewT1.DataSource = tbM;
                        BaNo.Enter += new EventHandler(Text_OnEnter);
                        BaName.Enter += new EventHandler(Text_OnEnter);
                    }
                }
            }
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            loadM();
            BaNo.Text = BaName.Text = "";
            BaNo.Enter -= Text_OnEnter;
            BaName.Enter -= Text_OnEnter;
        }

        private void Get_Click(object sender, EventArgs e)
        {
            Result = tbM.Rows[dataGridViewT1.SelectedRows[0].Index];
            this.DialogResult = DialogResult.OK;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0 || !開窗模式) return;
            Result = tbM.Rows[dataGridViewT1.SelectedRows[0].Index];
            this.DialogResult = DialogResult.OK;
        }






    }
}
