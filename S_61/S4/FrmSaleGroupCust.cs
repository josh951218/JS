using System;
using System.Data;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.S4
{
    public partial class FrmSaleGroupCust : Formbase
    {
        public string year = "";
        public DataTable dtD = new DataTable();

        public FrmSaleGroupCust()
        {
            InitializeComponent();
        }

        private void FrmSaleGroupCust_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DefaultCellStyle.Format = "f2";
            dataGridViewT1.DataSource = dtD;
        }

        void OutReport(RptMode mode)
        {
            var path = Common.reportaddress + "Report\\年度銷售客戶分組表.rpt"; 

            RPT rp = new RPT(dtD, path);
            rp.lobj.Add(new string[] { "year", "【" + year + " 年度】銷售客戶分組表" });

            if (mode == RptMode.PreView) rp.PreView();
            else if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.Excel) rp.Excel();
            else if (mode == RptMode.Word) rp.Word();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dtD.Clear();
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtD.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
