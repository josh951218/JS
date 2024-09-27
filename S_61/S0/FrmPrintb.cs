using System;
using System.Data;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.S0
{
    public partial class FrmPrintb : Form
    {
        public string TSeekNo { private get; set; }
        public string TResult { get; private set; }

        DataTable dt = new DataTable();

        public FrmPrintb()
        {
            InitializeComponent();
             
            dt.Columns.Add("印表機型號", typeof(string));

            if (JEInitialize.IsRunTime)
            {
                dataGridViewT1.DefaultCellStyle.Font = JEInitialize.ControlFontSize;
                
            }
        }

        private void FrmPrintb_Load(object sender, EventArgs e)
        { 
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                dt.Rows.Add(new object[] { printer });
            }
            dataGridViewT1.DataSource = dt;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0 && dataGridViewT1.SelectedRows.Count > 0)
                this.TResult = dataGridViewT1.SelectedRows[0].Cells["印表機型號"].Value.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            this.Close();
        }

        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            btnGet_Click(null, null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F9:
                    btnSave.Focus();
                    btnSave.PerformClick();
                    break;
                case Keys.F11:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dt.Clear();
            base.OnFormClosing(e);
        }
    }
}
