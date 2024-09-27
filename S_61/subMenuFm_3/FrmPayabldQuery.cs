using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmPayabldQuery : Formbase
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

        public FrmPayabldQuery()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            cn.ConnectionString = Common.sqlConnString;

            this.沖抵帳款.Set銷貨單據小數();
            this.累入預付.Set銷貨單據小數();
            this.折讓金額.Set銷貨單據小數();
            this.沖帳金額.Set銷貨單據小數();
            this.未付金額.Set銷貨單據小數();
            this.單據總計.Set銷貨單據小數();
        }

        private void FrmReceivdQuery_Load(object sender, EventArgs e)
        {
            this.付款日期.DataPropertyName = (Common.User_DateTime == 1) ? "PaDate" : "PaDate1";
            this.帳款日期.DataPropertyName = (Common.User_DateTime == 1) ? "BsDateAc" : "BsDateAc1";
            LoadMaster();

            dtD.DefaultView.Search(ref dataGridViewT1, "PaNo", this.TSeekNo ?? "");
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
            //var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            //if (index == -1)
            //{
            //    if (dataGridViewT1.Rows.Count > 0) index = 0;
            //    else return;
            //}
            //LoadDetail(dtM.Rows[index]["PaNo"].ToString().Trim());

            string PaNo = "";
            if (dataGridViewT1.CurrentRow != null && dataGridViewT1["付款憑證", e.Row.Index].Value != null)
            {
                PaNo = dataGridViewT1["付款憑證", e.Row.Index].Value.ToString();
            }
            LoadDetail(PaNo);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                this.TResult = dataGridViewT1.SelectedRows[0].Cells["付款憑證"].Value.ToString();
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

        private void qFano_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (qPano.TrimTextLenth() == 0 && qFano.TrimTextLenth() == 0)
            {
                dtM.DefaultView.RowFilter = "1=1";
            }
        }

        private void qFano_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void qReno_TextChanged(object sender, EventArgs e)
        {
            dtD.Clear();
            if (qPano.TrimTextLenth() > 0 && qFano.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "PaNo Like '" + qPano.Text.Trim() + "%' And FaNo Like '" + qFano.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "PaNo", qPano.Text.Trim(), "FaNo", qFano.Text.Trim());
            }
            else if (qPano.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "PaNo Like '" + qPano.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "", qPano.Text.Trim());
            }
            else if (qFano.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "FaNo Like '" + qFano.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "FaNo", qFano.Text.Trim());
            }
            else
            {
                dtM.DefaultView.RowFilter = "1=1";
                dtD.DefaultView.Search(ref dataGridViewT1, "PaNo", "");
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;
            var no = dataGridViewT1["付款憑證", index].Value.ToString().Trim();
            LoadDetail(no);
        }

        private void qFano_TextChanged(object sender, EventArgs e)
        {
            dtD.Clear();
            if (qPano.TrimTextLenth() > 0 && qFano.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "PaNo Like '" + qPano.Text.Trim() + "%' And FaNo Like '" + qFano.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "PaNo", qPano.Text.Trim(), "FaNo", qFano.Text.Trim());
            }
            else if (qPano.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "PaNo Like '" + qPano.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "", qPano.Text.Trim());
            }
            else if (qFano.TrimTextLenth() > 0)
            {
                dtM.DefaultView.RowFilter = "FaNo Like '" + qFano.Text.Trim() + "%'";
                dtD.DefaultView.Search(ref dataGridViewT1, "FaNo", qFano.Text.Trim());
            }
            else
            {
                dtM.DefaultView.RowFilter = "1=1";
                dtD.DefaultView.Search(ref dataGridViewT1, "PaNo", "");
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;
            var no = dataGridViewT1["付款憑證", index].Value.ToString().Trim();
            LoadDetail(no);
        }
    }
}
