using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmReceivdQuery : Formbase
    {
        JBS.JS.xEvents xe;
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        public DataRow MasterRow = null;
        public DataTable dtDetail = new DataTable();
        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();

        public FrmReceivdQuery()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            cn.ConnectionString = Common.sqlConnString;

            this.沖抵帳款.Set銷貨單據小數();
            this.累入預收.Set銷貨單據小數();
            this.折讓金額.Set銷貨單據小數();
            this.沖帳金額.Set銷貨單據小數();
            this.未收金額.Set銷貨單據小數();
            this.單據總計.Set銷貨單據小數();
        }

        private void FrmReceivdQuery_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            this.收款日期.DataPropertyName = (Common.User_DateTime == 1) ? "ReDate" : "ReDate1";
            this.帳款日期.DataPropertyName = (Common.User_DateTime == 1) ? "SaDateAc" : "SaDateAc1";
            LoadMaster();

            dtD.DefaultView.Search(ref dataGridViewT1, "ReNo", this.TSeekNo);
        }

        void LoadMaster()
        {
            cn.Close();
            dtM.Clear();
            dataGridViewT1.DataSource = dtM.DefaultView;
            daM.Fill(dtM);
            cn.Close();
        }

        void LoadDetail(object orno)
        {
            cn.Close();
            dtD.Clear();
            dataGridViewT2.DataSource = dtD;
            daD.SelectCommand.Parameters["@No"].Value = orno;
            daD.Fill(dtD);
            cn.Close();
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            string reno = "";
            if (dataGridViewT1.CurrentRow != null && dataGridViewT1["收款憑證", e.Row.Index].Value != null)
            {
                reno = dataGridViewT1["收款憑證", e.Row.Index].Value.ToString();
            }
            LoadDetail(reno);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                this.TResult = dataGridViewT1.SelectedRows[0].Cells["收款憑證"].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel; break;
                case Keys.F4:
                    this.DialogResult = DialogResult.Cancel; break;
                case Keys.F9:
                    btnGet.PerformClick();
                    this.DialogResult = DialogResult.OK; break;
                case Keys.F3:
                    btnQoo.PerformClick();
                    this.DialogResult = DialogResult.OK; break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            btnGet_Click(null, null);
        }

        private void dataGridViewT2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridViewT2["序號", e.RowIndex].Value = (e.RowIndex + 1).ToString();
        }

        private void qCuno_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (qReno.TrimTextLenth() == 0 && qCuno.TrimTextLenth() == 0)
            {
                dtM.DefaultView.RowFilter = "1=1";
            }
        }

        private void qCuno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void qReno_TextChanged(object sender, EventArgs e)
        {
            dtD.Clear();
            if (qReno.TrimTextLenth() > 0 && qCuno.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "ReNo Like '" + qReno.Text.Trim() + "%' And CuNo Like'" + qCuno.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "ReNo", qReno.Text.Trim(), "CuNo", qCuno.Text.Trim());
            }
            else if (qReno.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "ReNo Like '" + qReno.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "", qReno.Text.Trim());
            }
            else if (qCuno.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "CuNo Like '" + qCuno.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", qCuno.Text.Trim());
            }
            else
            {
                dtM.DefaultView.RowFilter = "1=1";
                dtD.DefaultView.Search(ref dataGridViewT1, "ReNo", "");
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;
            var no = dataGridViewT1["收款憑證", index].Value.ToString().Trim();
            LoadDetail(no);
        }

        private void qCuno_TextChanged(object sender, EventArgs e)
        {
            dtD.Clear();
            if (qReno.TrimTextLenth() > 0 && qCuno.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "ReNo Like '" + qReno.Text.Trim() + "%' And CuNo Like '" + qCuno.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "ReNo", qReno.Text.Trim(), "CuNo", qCuno.Text.Trim());
            }
            else if (qReno.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "ReNo Like '" + qReno.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "", qReno.Text.Trim());
            }
            else if (qCuno.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "CuNo Like '" + qCuno.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "CuNo", qCuno.Text.Trim());
            }
            else
            {
                dtM.DefaultView.RowFilter = "1=1";
                dtD.DefaultView.Search(ref dataGridViewT1, "ReNo", "");
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;
            var no = dataGridViewT1["收款憑證", index].Value.ToString().Trim();
            LoadDetail(no);
        }
    }
}
