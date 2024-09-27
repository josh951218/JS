using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmFactInfob : Formbase
    {
        public DataTable dt = new DataTable();

        public FrmFactInfob()
        {
            InitializeComponent();
        }

        private void FrmFactInfob_Load(object sender, EventArgs e)
        {
            radio2.SetUserDefineRpt("廠商資料瀏覽_自訂一.rpt");
            radio3.SetUserDefineRpt("廠商資料瀏覽_自訂二.rpt");
            radio4.SetUserDefineRpt("廠商資料瀏覽_自訂三.rpt");
            radio5.SetUserDefineRpt("廠商資料瀏覽_自訂四.rpt");

            dataGridViewT1.DataSource = dt;
        }

        private void bteExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        void dataintodocument(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();
            if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\廠商資料瀏覽_內定報表.rpt");
            else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\廠商資料瀏覽_自訂一.rpt");
            else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\廠商資料瀏覽_自訂二.rpt");
            else if (radio4.Checked) oRpt.Load(Common.reportaddress + "Report\\廠商資料瀏覽_自訂三.rpt");
            else if (radio5.Checked) oRpt.Load(Common.reportaddress + "Report\\廠商資料瀏覽_自訂四.rpt");
            oRpt.SetDataSource(dt);
            oRpt.Database.Tables[0].ApplyLogOnInfo(Common.logOnInfo);
            TextObject myFieldTitleName;
            List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();

            try
            {
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();
                }
            }
            catch { }
            try
            {
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (radio6.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (radio7.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (radio8.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (radio9.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (radio10.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                    else if (radio11.Checked) myFieldTitleName.Text = "";
                }
            }
            catch { }

            Common.FrmReport = new Report.Frmreport();
            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;


            if ((radio1.Checked || radio2.Checked) && mode == RptMode.Excel)
            {
                DialogResult dialogResult = MessageBox.Show("是否需要解析Excel檔案?", "解析Excel視窗", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Common.FrmReport.excel("廠商資料瀏覽", true, "廠商編號", "(以下空白)", "廠商編號", true, true);
                    return;
                }
                else if (dialogResult == DialogResult.No)
                {
                    //nothing
                }
            }

            if (mode == RptMode.Print)
                Common.FrmReport.button1_Click(null, null);
            else if (mode == RptMode.PreView)
                Common.FrmReport.ShowDialog();
            else if (mode == RptMode.Word)
                Common.FrmReport.word("廠商資料瀏覽");
            else if (mode == RptMode.Excel)
                Common.FrmReport.excel("廠商資料瀏覽");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Excel);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
                dt.Excel匯出並開啟(this.Text);

            if (keyData == Keys.Escape)
                bteExit.PerformClick();

            if (keyData == Keys.F11)
                bteExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
