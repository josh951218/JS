using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmFQuotToShop : Formbase
    {
        public string FaNo = "";
        public string FqNo = "";
        public DataRow MasterRow = null;
        public DataTable dtFQuotD = new DataTable();
        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();

        public FrmFQuotToShop()
        {
            InitializeComponent();
            cn.ConnectionString = Common.sqlConnString;

            this.詢價總額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.單價.DefaultCellStyle.Format = "f" + Common.MF;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;

            this.詢價總額.Visible = this.單價.Visible = Common.User_ShopPrice;
        }

        private void FrmFQuotToShop_Load(object sender, EventArgs e)
        { 
            this.詢價日期.DataPropertyName = (Common.User_DateTime == 1) ? "fqdate" : "fqdate1";
              
            LoadMaster();
            qNo.Text = SeekNo;

            if (dataGridViewT1.Rows.Count > 0)
                dataGridViewT1.Search("詢價單號", qNo.Text, "廠商編號", fNo.Text);
        }

        void LoadMaster()
        {
            dtM.Clear();
            dataGridViewT1.DataSource = dtM;
            daM.SelectCommand.Parameters["@fano"].Value = FaNo;
            if (FaNo.Trim() == "")
                daM.SelectCommand.CommandText = "select * from fquot order by fqno desc";
            daM.Fill(dtM);
        }

        void LoadDetail(object fqno)
        {
            dtD.Clear();
            dataGridViewT2.DataSource = dtD;
            daD.SelectCommand.CommandText = @"select * ,ItNoUdf = (select  top  1  ItNoUdf  from item where item.itno = fquotd.itno )"
                                            + " from fquotd WHERE fqno =@fqno order by recordno";
            daD.SelectCommand.Parameters["@fqno"].Value = fqno;
            daD.Fill(dtD);
            cn.Close();
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dataGridViewT2["點選", i].Value = "V";
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
                if (dataGridViewT1.SelectedRows != null)
                    if (dataGridViewT1.SelectedRows.Count > 0)
                        if (e.StateChanged == DataGridViewElementStates.Selected)
                        {
                            cn.Close();
                            LoadDetail(dataGridViewT1["詢價單號", e.Row.Index].Value);
                        }
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel; break;
                case Keys.F2:
                    btnCKall.PerformClick(); break;
                case Keys.F3:
                    btnCKnull.PerformClick(); break;
                case Keys.F4:
                    this.DialogResult = DialogResult.Cancel; break;
                case Keys.F9:
                    btnGet.PerformClick();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
          
        private void btnCKall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewT2.Rows.Count; i++)
            {
                dataGridViewT2["點選", i].Value = "V";
                dataGridViewT2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnCKnull_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewT2.Rows.Count; i++)
            {
                dataGridViewT2["點選", i].Value = "";
                dataGridViewT2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            dtFQuotD = dtD.Copy();
            dtFQuotD.Clear();
            for (int i = 0; i < dataGridViewT2.Rows.Count; i++)
            {
                if (dataGridViewT2["點選", i].Value.ToString() == "V")
                    dtFQuotD.ImportRow(dtD.Rows[i]);
            }
            if (dtFQuotD.Rows.Count > 0)
            {
                if (dataGridViewT1.Rows.Count > 0)
                    if (dataGridViewT1.SelectedRows != null)
                        if (dataGridViewT1.SelectedRows.Count > 0)
                        {
                            MasterRow = dtM.Rows[dataGridViewT1.SelectedRows[0].Index];
                            FqNo = dataGridViewT1.SelectedRows[0].Cells["詢價單號"].Value.ToString();
                        }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
         
        private void dataGridViewT2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.Rows.Count == 0) return;
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            if (dataGridViewT2.Columns[e.ColumnIndex].Name != "點選") return;
            DataGridViewCell cell = dataGridViewT2["點選", e.RowIndex];
            cell.Value = (cell.Value.ToString() == "V") ? "" : "V";
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            btnGet_Click(null, null);
        }

        private void qNo_TextChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
                dataGridViewT1.Search("詢價單號", qNo.Text, "廠商編號", fNo.Text);
        }
         






































    }
}
