using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;

namespace S_61.subMenuFm_3
{
    public partial class 公司開窗 : Formbase
    {
        public DataRow TResult;
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;
         
        public string 票據連線字串 = "";
        DataTable tb = new DataTable();
        List<DataRow> list = new List<DataRow>();

        public 公司開窗()
        {
            InitializeComponent();
        }

        private void 公司開窗_Load(object sender, EventArgs e)
        { 
            load();
            CoNo.Focus();
        }

        void load()
        {
            using (SqlConnection cn = new SqlConnection(票據連線字串))
            {
                cn.Open();
                SqlDataAdapter dd = new SqlDataAdapter("select * from comp where cono not in ('df') and copaths != '' order by cono", cn);
                tb.Clear();
                list.Clear();
                dd.Fill(tb);
                if (tb.Rows.Count > 0) list = tb.AsEnumerable().ToList(); 
                dataGridViewT1.DataSource = tb;
            }
            if (list.Count > 0)
            {
                int i = list.FindIndex(r => string.CompareOrdinal(r["cono"].ToString().Trim(), this.TSeekNo) == 0);
                if (i == -1)
                    i = list.FindIndex(r => string.CompareOrdinal(r["cono"].ToString().Trim(), this.TSeekNo) > 0);
                if (i == -1)
                    i = list.Count - 1;
                dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                dataGridViewT1.Rows[i].Selected = true;
            }
        }

        private void Get_Click(object sender, EventArgs e)
        {
            this.TResult = tb.Rows[dataGridViewT1.SelectedRows[0].Index];
            this.DialogResult = DialogResult.OK;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            { 
                case Keys.F9:
                    Get.PerformClick();
                    break;
                case Keys.F11:
                    Exit.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0)
                return;

            this.TResult = tb.Rows[dataGridViewT1.SelectedRows[0].Index];
            this.DialogResult = DialogResult.OK;
        }

        private void CoNo_TextChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0 || CoNo.Text.Trim() == "") return;
            int i = list.FindIndex(r => string.CompareOrdinal(r["cono"].ToString().Trim(), CoNo.Text.Trim()) == 0);
            if (i == -1)
                i = list.FindIndex(r => string.CompareOrdinal(r["cono"].ToString().Trim(), CoNo.Text.Trim()) > 0);
            if (i == -1)
                i = list.Count - 1;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
            dataGridViewT1.Rows[i].Selected = true;
        }
    }
}
