using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmAge_Receivdb : Formbase
    {
        public DataTable tbM = new DataTable();
        public string Rule1_2 = "", Rule2_1 = "", Rule2_2 = "", Rule3_1 = "", Rule3_2 = "", Rule4_1 = "", Rule4_2 = "", Rule5_1 = "";
        public string spname = "";
        List<TextBoxNumberT> Num;
        string path = "";
        string ReportFileName = "";
        public FrmAge_Receivdb()
        {
            InitializeComponent();
            Num = new List<TextBoxNumberT>() { Lv1Count, Lv2Count, Lv3Count, Lv4Count, Lv5Count, TotMny };
            Num.ForEach(r =>
            {
                r.MarkThousand = true;
                r.FirstNum = Common.nFirst;
                r.LastNum = Common.MST;
            });
            this.RuleLv1.DefaultCellStyle.Format = "N" + Common.MST;
            this.RuleLv2.DefaultCellStyle.Format = "N" + Common.MST;
            this.RuleLv3.DefaultCellStyle.Format = "N" + Common.MST;
            this.RuleLv4.DefaultCellStyle.Format = "N" + Common.MST;
            this.RuleLv5.DefaultCellStyle.Format = "N" + Common.MST;
        }

        private void FrmAge_Receivdb_Load(object sender, EventArgs e)
        {
            this.RuleLv1.HeaderText = Rule1_2;
            this.RuleLv2.HeaderText = Rule2_2 + " ~ " + Rule2_1 + "";
            this.RuleLv3.HeaderText = Rule3_2 + " ~ " + Rule3_1 + "";
            this.RuleLv4.HeaderText = Rule4_2 + " ~ " + Rule4_1 + "";
            this.RuleLv5.HeaderText = Rule5_1;
            lbLv5.Text = Rule5_1;
            lbLv4.Text = Rule4_2 + " ~ " + Rule4_1 + "";
            lbLv3.Text = Rule3_2 + " ~ " + Rule3_1 + "";
            lbLv2.Text = Rule2_2 + " ~ " + Rule2_1 + "";
            lbLv1.Text = Rule1_2;

       
            dataGridViewT1.DataSource = tbM;
            decimal lv1 = 0, lv2 = 0, lv3 = 0, lv4 = 0, lv5 = 0;
            for (int i = 0; i < tbM.Rows.Count; i++)
            {
                lv1 += tbM.Rows[i]["RuleLv1"].ToDecimal();
                lv2 += tbM.Rows[i]["RuleLv2"].ToDecimal();
                lv3 += tbM.Rows[i]["RuleLv3"].ToDecimal();
                lv4 += tbM.Rows[i]["RuleLv4"].ToDecimal();
                lv5 += tbM.Rows[i]["RuleLv5"].ToDecimal();
            }
            Lv1Count.Text = lv1.ToString("N" + Common.MST);
            Lv2Count.Text = lv2.ToString("N" + Common.MST);
            Lv3Count.Text = lv3.ToString("N" + Common.MST);
            Lv4Count.Text = lv4.ToString("N" + Common.MST);
            Lv5Count.Text = lv5.ToString("N" + Common.MST);
            TotMny.Text = (lv1 + lv2 + lv3 + lv4 + lv5).ToString("N" + Common.MST);
            Count.Text = tbM.Rows.Count.ToString();
            圖表設定();
        }

        void dataintodocument(RptMode mode)
        {
            path = Common.reportaddress + "Report\\應收帳齡分析_總額表.rpt";
            ReportDocument oRpt = new ReportDocument();

            if (File.Exists(path))
                oRpt.Load(path);
            else
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            oRpt.SetDataSource(tbM);
            if (Common.Sql_LogMod == 2)//混合驗證
            {
                Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in oRpt.Database.Tables)
                {
                    table.ApplyLogOnInfo(Common.logOnInfo);
                }
            }
            else if (Common.Sql_LogMod == 1)//SQL驗證
            {
                Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in oRpt.Database.Tables)
                {
                    table.ApplyLogOnInfo(Common.logOnInfo);
                }
            }
            oRpt.Refresh();
            TextObject myFieldTitleName;
            List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();

            if (Txt.Find(t => t.Name == "txtstart") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                //myFieldTitleName.Text = spname == "" ? Common.dtSysSettings.Rows[0]["StcPnName"].ToString() : spname;
                if (rdHeader1.Checked) myFieldTitleName.Text = Common.dtstart.Rows[0]["pnname"].ToString();
                else if (rdHeader2.Checked) myFieldTitleName.Text = Common.dtstart.Rows[1]["pnname"].ToString();
                else if (rdHeader3.Checked) myFieldTitleName.Text = Common.dtstart.Rows[2]["pnname"].ToString();
                else if (rdHeader4.Checked) myFieldTitleName.Text = Common.dtstart.Rows[3]["pnname"].ToString();
                else if (rdHeader5.Checked) myFieldTitleName.Text = Common.dtstart.Rows[4]["pnname"].ToString();
                else myFieldTitleName.Text = "";
            }
            if (Txt.Find(t => t.Name == "txtend") != null)
            {
                //三行註腳
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                if (單行註腳1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (單行註腳2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (單行註腳3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (單行註腳4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (單行註腳5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else myFieldTitleName.Text = "";
            }
            if (Txt.Find(t => t.Name == "lbLv1") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["lbLv1"];
                myFieldTitleName.Text = lbLv1.Text.Trim();
            }
            if (Txt.Find(t => t.Name == "lbLv2") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["lbLv2"];
                myFieldTitleName.Text = lbLv2.Text.Trim();
            }
            if (Txt.Find(t => t.Name == "lbLv3") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["lbLv3"];
                myFieldTitleName.Text = lbLv3.Text.Trim();
            }
            if (Txt.Find(t => t.Name == "lbLv4") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["lbLv4"];
                myFieldTitleName.Text = lbLv4.Text.Trim();
            }
            if (Txt.Find(t => t.Name == "lbLv5") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["lbLv5"];
                myFieldTitleName.Text = lbLv5.Text.Trim();
            }
            if (Txt.Find(t => t.Name == "制表日期") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["制表日期"];
                myFieldTitleName.Text = Date.GetDateTime(Common.User_DateTime, true);
            }

            List<ParameterField> num = oRpt.ParameterFields.OfType<ParameterField>().ToList();

            //日期格式

            if (num.Find(p => p.Name == "date") != null)
            {
                switch (Common.User_DateTime)
                {
                    case 1:
                        oRpt.SetParameterValue("date", "民國");
                        break;
                    case 2:
                        oRpt.SetParameterValue("date", "西元");
                        break;
                }
            }
            if (num.Find(p => p.Name == "備註說明") != null)
            {
                oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
            }

            //報表參數設定
            if (num.Find(p => p.Name == "顯示千分位") != null)
            {
                if (pVar.TRS != "")
                    pVar.ShowTRS = true;
                oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
            }
            if (num.Find(p => p.Name == "千分位") != null)
                oRpt.SetParameterValue("千分位", pVar.TRS);
            if (num.Find(p => p.Name == "進貨單價小數") != null)
                oRpt.SetParameterValue("進貨單價小數", Common.MF);
            if (num.Find(p => p.Name == "銷貨單價小數") != null)
                oRpt.SetParameterValue("銷貨單價小數", Common.MS);
            if (num.Find(p => p.Name == "銷貨單據小數") != null)
                oRpt.SetParameterValue("銷貨單據小數", Common.MST);
            if (num.Find(p => p.Name == "進貨單據小數") != null)
                oRpt.SetParameterValue("進貨單據小數", Common.MFT);
            if (num.Find(p => p.Name == "銷項稅額小數") != null)
                oRpt.SetParameterValue("銷項稅額小數", Common.TS);
            if (num.Find(p => p.Name == "進項稅額小數") != null)
                oRpt.SetParameterValue("進項稅額小數", Common.TF);
            if (num.Find(p => p.Name == "進項金額小數") != null)
                oRpt.SetParameterValue("進項金額小數", Common.TPF);
            if (num.Find(p => p.Name == "銷項金額小數") != null)
                oRpt.SetParameterValue("銷項金額小數", Common.TPS);
            if (num.Find(p => p.Name == "本幣金額小數") != null)
                oRpt.SetParameterValue("本幣金額小數", Common.M);
            if (num.Find(p => p.Name == "庫存數量小數") != null)
                oRpt.SetParameterValue("庫存數量小數", Common.Q);
            if (num.Find(p => p.Name == "是否顯示金額") != null)
                oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);

            Common.FrmReport = new Report.Frmreport();
            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;

            if (mode == RptMode.Print)
                Common.FrmReport.button1_Click(null, null);
            else if (mode == RptMode.PreView)
                Common.FrmReport.ShowDialog();
            else if (mode == RptMode.Word)
                Common.FrmReport.word(ReportFileName);
            else if (mode == RptMode.Excel)
                Common.FrmReport.excel(ReportFileName);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        void 圖表設定()
        {
            string[] xValues = { lbLv5.Text, lbLv4.Text, lbLv3.Text, lbLv2.Text, lbLv1.Text };
            decimal[] yValues = { Lv5Count.Text.ToDecimal(), Lv4Count.Text.ToDecimal(), Lv3Count.Text.ToDecimal(), Lv2Count.Text.ToDecimal(), Lv1Count.Text.ToDecimal(), };
            //設定 ChartArea1
            chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 50;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            //設定 Legends
            chart1.Legends["Legend1"].BackColor = Color.FromArgb(224, 224, 224);
            chart1.Legends["Legend1"].BackHatchStyle = ChartHatchStyle.DarkDownwardDiagonal;//斜線背景
            chart1.Legends["Legend1"].Font = new Font("細明體", 10);
            //設定 Series1
            chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
            chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
            chart1.Series["Series1"].LegendText = "#VALX:    [ #PERCENT{P1} ]"; //X軸 + 百分比
            chart1.Series["Series1"].Label = "#VALX\n#PERCENT{P1}"; //X軸 + 百分比
            chart1.Series["Series1"]["PieLabelStyle"] = "Outside"; //數值顯示在圓餅外
            //chart1.Series["Series1"]["PieLabelStyle"] = "Inside"; //數值顯示在圓餅內
            //Chart1.Series["Series1"]["PieLabelStyle"] = "Disabled"; //不顯示數值
            chart1.Series["Series1"]["PieDrawingStyle"] = "Default";
            chart1.Series["Series1"].Font = new Font("細明體", 10);
            chart1.Series["Series1"].Points.FindMaxByValue().LabelForeColor = Color.Red;//最多顯示紅字
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                tbM.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
