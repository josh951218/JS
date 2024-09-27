using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;
using JBS;
using System.IO;

namespace S_61.SOther
{
    public partial class FrmItem_Inventoryb : Formbase
    {
        string reportfilename = "";
        public string 明細區_頭value = "", 明細區_尾value = "", 明細區_欄位對應value = "";
        public DataTable dt = new DataTable();

        public FrmItem_Inventoryb()
        {
            InitializeComponent();
            tabcontrol.ItemSize = new Size(0, 1);

            this.庫存數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.庫存倉數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.借出倉數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.加工倉數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.借入倉數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.庫存總數量.DefaultCellStyle.Format = "f" + Common.Q;

            this.庫存數量三.DefaultCellStyle.Format = "f" + Common.Q;
            this.單位成本三.DefaultCellStyle.Format = "f" + Common.M;
            this.成本三.DefaultCellStyle.Format = "f" + Common.M;

            this.庫存總數量四.DefaultCellStyle.Format = "f" + Common.Q;
            this.庫存倉數量四.DefaultCellStyle.Format = "f" + Common.Q;
            this.借出倉數量四.DefaultCellStyle.Format = "f" + Common.Q;
            this.加工倉數量四.DefaultCellStyle.Format = "f" + Common.Q;
            this.借入倉數量四.DefaultCellStyle.Format = "f" + Common.Q;
            this.庫存總數量四.DefaultCellStyle.Format = "f" + Common.Q;
            this.單位成本四.DefaultCellStyle.Format = "f" + Common.M;
            this.成本四.DefaultCellStyle.Format = "f" + Common.M;

            this.庫存數量五.DefaultCellStyle.Format = "f" + Common.Q;
            this.單位成本五.DefaultCellStyle.Format = "f" + Common.M;
            this.成本五.DefaultCellStyle.Format = "f" + Common.M;

            this.成本三.Visible = this.成本四.Visible = this.成本五.Visible = Common.User_ShopPrice;
            this.單位成本三.Visible = this.單位成本四.Visible = this.單位成本五.Visible = Common.User_ShopPrice;

            date3.MaxLength = date4.MaxLength = date5.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
 
        }

        private void FrmItem_Inventoryb_Load(object sender, EventArgs e)
        {
            this.tabcontrol.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabPage1.Width, this.tabPage1.Height));
           

            if (dt.Rows.Count > 0)
            {
                if (pVar.FrmItem_Inventory.radio1.Checked)
                {
                    this.Text = "產品現有庫存表-明細表";
                    reportfilename = "產品現有庫存表_明細表";

                    radio3.SetUserDefineRpt("產品現有庫存表_明細表_自訂一.rpt");
                    radio4.SetUserDefineRpt("產品現有庫存表_明細表_自訂二.rpt");

                  
                }
                else if (pVar.FrmItem_Inventory.radio2.Checked)
                {
                    this.Text = "產品現有庫存表-現有總表";
                    reportfilename = "產品現有庫存表_現有總表";

                    radio2.Text = "自訂一";
                    radio2.SetUserDefineRpt("產品現有庫存表_現有總表_自訂一.rpt");
                    radio3.Text = "自訂二";
                    radio3.SetUserDefineRpt("產品現有庫存表_現有總表_自訂二.rpt");
                    radio4.Visible = false;
                 
                }
                else if (pVar.FrmItem_Inventory.radio3.Checked)
                {
                    this.Text = "產品現有庫存表-歷史明細表";
                    reportfilename = "產品現有庫存表_歷史明細表";

                    radio3.SetUserDefineRpt("產品現有庫存表_歷史明細表_自訂一.rpt");
                    radio4.SetUserDefineRpt("產品現有庫存表_歷史明細表_自訂二.rpt");

                }
                else if (pVar.FrmItem_Inventory.radio4.Checked)
                {
                    this.Text = "產品現有庫存表-歷史總表";
                    reportfilename = "產品現有庫存表_歷史總表";

                    radio2.Text = "自訂一";
                    radio2.SetUserDefineRpt("產品現有庫存表_歷史總表_自訂一.rpt");
                    radio3.Text = "自訂二";
                    radio3.SetUserDefineRpt("產品現有庫存表_歷史總表_自訂二.rpt");
                    radio4.Visible = false;

                  
                }
                else if (pVar.FrmItem_Inventory.radio5.Checked)
                {
                    this.Text = "產品現有庫存表_字元明細表";
                    reportfilename = "產品現有庫存表_字元明細表";

                    radio3.SetUserDefineRpt("產品現有庫存表_字元明細表_自訂一.rpt");
                    radio4.SetUserDefineRpt("產品現有庫存表_字元明細表_自訂二.rpt");

                }
            }
            radio1.Checked = true; 
            radio6.Checked = true; 

            if (dt.Rows.Count > 0)
            {
                if (pVar.FrmItem_Inventory.radio1.Checked)
                {
                    dataGridViewT1.DataSource = dt;
                    dataGridViewT1.ReadOnly = true;
                }
                else if (pVar.FrmItem_Inventory.radio2.Checked)
                {
                    dataGridViewT2.DataSource = dt;
                    dataGridViewT2.ReadOnly = true;
                }
                else if (pVar.FrmItem_Inventory.radio3.Checked)
                {
                    dataGridViewT3.DataSource = dt;
                    dataGridViewT3.ReadOnly = true;
                }
                else if (pVar.FrmItem_Inventory.radio4.Checked)
                {
                    dataGridViewT4.DataSource = dt;
                    dataGridViewT4.ReadOnly = true;
                }
                else if (pVar.FrmItem_Inventory.radio5.Checked)
                {
                    dataGridViewT5.DataSource = dt;
                    dataGridViewT5.ReadOnly = true;
                }
            }
        }

        void dataintodocument()
        {
            Common.FrmReport = new Report.Frmreport();
            ReportDocument oRpt = new ReportDocument();

            if (dt.Rows.Count > 0)
            {
                if (pVar.FrmItem_Inventory.radio1.Checked)
                {
                    if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_標準報表.rpt");
                    else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_雙單位表.rpt");
                    else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂一.rpt");
                    else if (radio4.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂二.rpt");
                }
                else if (pVar.FrmItem_Inventory.radio2.Checked)
                {
                    if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_標準報表.rpt");
                    else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂一.rpt");
                    else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂二.rpt");
                }
                else if (pVar.FrmItem_Inventory.radio3.Checked)
                {
                    if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_標準報表.rpt");
                    else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_雙單位表.rpt");
                    else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂一.rpt");
                    else if (radio4.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂二.rpt");
                }
                else if (pVar.FrmItem_Inventory.radio4.Checked)
                {
                    if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_標準報表.rpt");
                    else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_雙單位表.rpt");
                    else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂一.rpt");
                    else if (radio4.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂二.rpt");
                }
                else if (pVar.FrmItem_Inventory.radio5.Checked)
                {
                    if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_標準報表.rpt");
                    else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_雙單位表.rpt");
                    else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂一.rpt");
                    else if (radio4.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂二.rpt");
                }
            }
            oRpt.SetDataSource(dt);
            oRpt.Database.Tables[0].ApplyLogOnInfo(Common.logOnInfo);
            TextObject myFieldTitleName;
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();

                //歷史庫存表-截止日期
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["資料日期1"];
                myFieldTitleName.Text = Date.AddLine(date3.Text.Trim());
            }
            catch { }
            try
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
            if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                oRpt.SetParameterValue("銷貨單價小數", Common.MS);
            if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                oRpt.SetParameterValue("銷貨單據小數", Common.MST);
            if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                oRpt.SetParameterValue("銷項稅額小數", Common.TS);
            if (Num.Find(p => p.Name == "本幣金額小數") != null)
                oRpt.SetParameterValue("本幣金額小數", Common.M);
            if (Num.Find(p => p.Name == "庫存數量小數") != null)
                oRpt.SetParameterValue("庫存數量小數", Common.Q);
            if (Num.Find(p => p.Name == "是否顯示金額") != null)
                oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);

            if (Num.Find(p => p.Name == "hidepri") != null)
                oRpt.SetParameterValue("hidepri", Common.User_ShopPrice);

            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument();
            using (PrintDialog printDialog1 = new PrintDialog())
            {
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    Common.FrmReport.rpt1.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    Common.FrmReport.rpt1.PrintToPrinter(1, true, 0, 0);
                }
            }
            Common.FrmReport.Dispose();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            dataintodocument();
            Common.FrmReport.ShowDialog();
        }

        private void bteExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument();
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".doc");
            Process.Start(Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".doc");
            Common.FrmReport.Dispose();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument();
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls");
            if (radio1.Checked || radio2.Checked)
            {
                DialogResult dialogResult = MessageBox.Show("是否需要解析Excel檔案?", "解析Excel視窗", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string ImportFileName = Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls", ExportFileName = Application.StartupPath + "\\temp\\" + reportfilename + intRn + "_解析檔案";
                    ResloveCrystalReportExcel ResloveExcel = new ResloveCrystalReportExcel(ImportFileName, ExportFileName+".xls", 明細區_頭value, 明細區_尾value, 明細區_欄位對應value, true, true);
                    bool Exists = File.Exists(ExportFileName + ".xls");
                    while (!Exists)
                    {
                        Exists = File.Exists(ExportFileName + ".xls");
                    }
                    Process.Start(ExportFileName + ".xls");
                }
                else
                    Process.Start(Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls");
            }
            else
            {
                Process.Start(Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls");
            }
            Common.FrmReport.Dispose();
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {
            this.tabcontrol.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabcontrol.Width, this.tabcontrol.Height));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt.Excel匯出並開啟(this.Text);
            }

            if (keyData == Keys.Escape)
                bteExit.PerformClick();

            if (keyData == Keys.F11)
                bteExit.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }



















































    }
}
