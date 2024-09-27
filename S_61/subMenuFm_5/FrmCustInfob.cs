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
    public partial class FrmCustInfob : Formbase
    {
        public DataTable dt = new DataTable();  

        public FrmCustInfob()
        {
            InitializeComponent();
        }

        private void FrmCustInfob_Load(object sender, EventArgs e)
        { 
            radio3.SetUserDefineRpt("客戶資料瀏覽_自訂一.rpt");
            radio4.SetUserDefineRpt("客戶資料瀏覽_自訂二.rpt");
            radio5.SetUserDefineRpt("客戶資料瀏覽_自訂三.rpt");
             
            if (dt.Rows.Count > 0)
            {
                dataGridViewT1.DataSource = dt;
            }
            else
            {
                dataGridViewT1.DataSource = null;
            }
             
            btnPrint.Focus();
        }

        void dataintodocument(RptMode mode)
        {  
            ReportDocument oRpt = new ReportDocument();
            if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\客戶資料瀏覽_內定報表.rpt");
            else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\客戶資料瀏覽_預收餘額表.rpt");
            else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\客戶資料瀏覽_自訂一.rpt");
            else if (radio4.Checked) oRpt.Load(Common.reportaddress + "Report\\客戶資料瀏覽_自訂二.rpt");
            else if (radio5.Checked) oRpt.Load(Common.reportaddress + "Report\\客戶資料瀏覽_自訂三.rpt");

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

            if (radio2.Checked)
                oRpt.SetParameterValue("f", Common.dtSysSettings.Rows[0]["MnyDeciS"].ToString());//銷貨單價小數

            Common.FrmReport = new Report.Frmreport();
            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;


            if ((radio1.Checked || radio2.Checked) && mode == RptMode.Excel)
            {
                DialogResult dialogResult = MessageBox.Show("是否需要解析Excel檔案?", "解析Excel視窗", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Common.FrmReport.excel("客戶資料瀏覽", true, "客戶編號", "(以下空白)", "客戶編號",true,true);
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
                Common.FrmReport.word("客戶資料瀏覽");
            else if (mode == RptMode.Excel)
                Common.FrmReport.excel("客戶資料瀏覽");
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

        private void bteExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt.Excel匯出並開啟(this.Text);
            }
            else if (keyData == Keys.F11) 
                this.Dispose();

            return base.ProcessCmdKey(ref msg, keyData);
        } 
    }
}
