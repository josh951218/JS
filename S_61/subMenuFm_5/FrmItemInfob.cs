using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmItemInfob : Formbase
    {
        public DataTable dt = new DataTable();

        public FrmItemInfob()
        {
            InitializeComponent();
            this.現有存量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.安全存量.DefaultCellStyle.Format = "f" + Common.Q;

            this.包裝售價.DefaultCellStyle.Format = "f6";
            this.包裝進價.DefaultCellStyle.Format = "f6";
            this.包裝標準成本.DefaultCellStyle.Format = "f6";
            this.售價.DefaultCellStyle.Format = "f6";
            this.進價.DefaultCellStyle.Format = "f6";
            this.標準成本.DefaultCellStyle.Format = "f6";

            this.售價.Visible = this.包裝售價.Visible = Common.User_SalePrice;
            this.進價.Visible = this.包裝進價.Visible = Common.User_ShopPrice;
            this.標準成本.Visible = this.包裝標準成本.Visible = Common.User_ShopPrice;
        }

        private void FrmItemInfob_Load(object sender, EventArgs e)
        {
            radio3.SetUserDefineRpt("產品資料瀏覽_自訂一.rpt");
            radio4.SetUserDefineRpt("產品資料瀏覽_自訂二.rpt");
            radio5.SetUserDefineRpt("產品資料瀏覽_自訂三.rpt");

            radio1.Checked = true;
            radio6.Checked = true;

            dataGridViewT1.DataSource = dt;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
            //using (PrintDialog printDialog1 = new PrintDialog())
            //{
            //    if (printDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        Common.FrmReport.rpt1.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
            //        Common.FrmReport.rpt1.PrintToPrinter(1, true, 0, 0);
            //    }
            //}
            //Common.FrmReport.Dispose();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.PreView);
            //Common.FrmReport.ShowDialog();
        }

        private void bteExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Excel);
        }

        void dataintodocument(RptMode mode)
        {

            Common.FrmReport = new Report.Frmreport();

            ReportDocument oRpt = new ReportDocument();
            if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\產品資料瀏覽_內定報表.rpt");
            else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\產品資料瀏覽_標籤.rpt");
            else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\產品資料瀏覽_自訂一.rpt");
            else if (radio4.Checked) oRpt.Load(Common.reportaddress + "Report\\產品資料瀏覽_自訂二.rpt");
            else if (radio5.Checked) oRpt.Load(Common.reportaddress + "Report\\產品資料瀏覽_自訂三.rpt");
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
                    Common.FrmReport = new Report.Frmreport();
                }
            }
            catch { }

            List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
            if (Num.Find(p => p.Name == "顯示千分位") != null)
            {
                if (pVar.TRS != "")
                    pVar.ShowTRS = true;
                oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
            }
            if (Num.Find(p => p.Name == "千分位") != null)
                oRpt.SetParameterValue("千分位", pVar.TRS);

            if (radio1.Checked)
                oRpt.SetParameterValue("f", Common.dtSysSettings.Rows[0]["MnyDeciS"].ToString());//銷貨單價小數
            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;


             if ((radio1.Checked) && mode == RptMode.Excel)
            {
                DialogResult dialogResult = MessageBox.Show("是否需要解析Excel檔案?", "解析Excel視窗", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Common.FrmReport.excel("產品資料瀏覽", true, "產品編號", "(以下空白)", "產品編號", true, true);
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
                Common.FrmReport.word("產品資料瀏覽");
            else if (mode == RptMode.Excel)
                Common.FrmReport.excel("產品資料瀏覽");
        }
 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt.Excel匯出並開啟(this.Text);
            }
            if (keyData == Keys.Escape) this.Dispose();
            if (keyData == Keys.F11) this.Dispose();

            return base.ProcessCmdKey(ref msg, keyData);
        }



    }
}
