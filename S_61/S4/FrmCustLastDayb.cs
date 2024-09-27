using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S4
{
    public partial class FrmCustLastDayb : Formbase
    {
        public DataTable dt = new DataTable();

        public FrmCustLastDayb()
        {
            InitializeComponent();
            this.未交易日數.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            radioT2.SetUserDefineRpt("久未交易客戶分析_自定一.rpt");
        }

        private void FrmCustLastDayb_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string Get單行註腳()
        {
            var rd = groupBoxT2.Controls.OfType<RadioT>().FirstOrDefault(r => r.Checked);

            if (rd == null)
                return string.Empty;

            if (rd.Text == "第一組")
                return Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (rd.Text == "第二組")
                return Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (rd.Text == "第三組")
                return Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (rd.Text == "第四組")
                return Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (rd.Text == "第五組")
                return Common.dtEnd.Rows[4]["tamemo"].ToString();
            else
                return string.Empty;
        }

        void OutReport(RptMode mode)
        {
            var path = Common.reportaddress + @"Report\久未交易客戶分析_標準報表.rpt";

            if (radioT2.Checked)
                path = Common.reportaddress + @"Report\久未交易客戶分析_自定一.rpt";

            RPT rp = new RPT(dt, path);
            rp.lobj.Add(new string[] { "txtend", Get單行註腳() });

            if (mode == RptMode.Print)
                rp.Print();
            else if (mode == RptMode.PreView)
                rp.PreView();
            else if (mode == RptMode.Word)
                rp.Word();
            else if (mode == RptMode.Excel)
                rp.Excel();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
