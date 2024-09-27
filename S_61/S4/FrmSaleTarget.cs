using System;
using System.Data;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.S4
{
    public partial class FrmSaleTarget : Formbase
    {
        RPT rp;
        public string year = "";
        public string X1No = "";
        public string TCNo = "";
        public string TCName = "";
        public DataTable dtD = new DataTable();

        public FrmSaleTarget()
        {
            InitializeComponent();
        }

        private void FrmSaleTarget_Load(object sender, EventArgs e)
        {
            radioT3.SetUserDefineRpt("年度銷售實績管理表_自定一.rpt");
            if (X1No.Length > 0)
            {
                pVar.XX01Validate(X1No, textBoxT1, textBoxT2);
            }
            else textBoxT1.Text = textBoxT2.Text = "";

            textBoxT4.Text = this.TCNo;
            textBoxT3.Text = this.TCName;

            dataGridViewT1.DefaultCellStyle.Format = "f2";
            dataGridViewT1.DataSource = dtD;
        }

        void OutReport(RptMode mode)
        {
            var path = "";
            if (radioT1.Checked) path = Common.reportaddress + "Report\\年度銷售實績管理表.rpt";
            else if (radioT2.Checked) path = Common.reportaddress + "Report\\年度銷售實績管理表_金額.rpt";
            else if (radioT3.Checked) path = Common.reportaddress + "Report\\年度銷售實績管理表_自定一.rpt";

            rp = new RPT(dtD, path);
            rp.lobj.Add(new string[] { "X1No", textBoxT1.Text.Trim() });
            rp.lobj.Add(new string[] { "X1Name", textBoxT2.Text.Trim() });
            rp.lobj.Add(new string[] { "year", "【" + year + " 年度】銷售實績管理表" });
            rp.lval.Add(new string[] { "cuno", textBoxT4.Text.Trim() });
            rp.lval.Add(new string[] { "cuname1", textBoxT3.Text.Trim() });

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



