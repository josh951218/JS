using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmBShop_Rptb : Formbase
    {
        public DataTable dtResult = new DataTable();
        public string DataRange = "";
        string ReportFileName = "";
        string ReportPath = "";
        List<string> list;

        public FrmBShop_Rptb()
        {
            InitializeComponent();

            this.退貨數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.退貨金額.DefaultCellStyle.Format = "f" + Common.TPF;
            this.進貨數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.進貨金額.DefaultCellStyle.Format = "f" + Common.TPF;
            this.進貨淨額.DefaultCellStyle.Format = "f" + Common.TPF;
             
            this.退貨金額.Visible = Common.User_ShopPrice;
            this.進貨金額.Visible = Common.User_ShopPrice;
            this.進貨淨額.Visible = Common.User_ShopPrice;
            TotB.Visible = Common.User_ShopPrice;
            TotR.Visible = Common.User_ShopPrice;
            TotMny.Visible = Common.User_ShopPrice;
        }

        private void FrmBShop_Rptb_Load(object sender, EventArgs e)
        {
            ReportFileName = "廠商進貨報表_簡要表.rpt";
             
            dataGridViewT1.DataSource = dtResult.DefaultView;

            list = dtResult.AsEnumerable().Select(r => r["fano"].ToString().Trim()).Distinct().ToList();
            WriteToText(list.First());
        }

        void WriteToText(string fano)
        {
            dtResult.DefaultView.RowFilter = "fano = '" + fano + "'";

            FaNo.Text = fano;
            FaName.Text = dtResult.DefaultView[0]["faname1"].ToString();

            var 退貨金額 = 0M;
            var 進貨金額 = 0M;
            var 進貨淨額 = 0M;
            for (int i = 0; i < dtResult.DefaultView.Count; i++)
            {
                退貨金額 += dtResult.DefaultView[i]["退貨金額"].ToDecimal();
                進貨金額 += dtResult.DefaultView[i]["進貨金額"].ToDecimal();
                進貨淨額 += dtResult.DefaultView[i]["進貨淨額"].ToDecimal();
            }
            TotR.Text = 退貨金額.ToString("f" + Common.TPF);
            TotB.Text = 進貨金額.ToString("f" + Common.TPF);
            TotMny.Text = 進貨淨額.ToString("f" + Common.TPF);
        }
         
        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToText(list.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(FaNo.Text) - 1;
            if (index <= 0) index = 0;

            WriteToText(list.ElementAt(index));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(FaNo.Text) + 1;
            if (index >= list.Count - 1) index = list.Count - 1;

            WriteToText(list.ElementAt(index));
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToText(list.Last());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName;

            try
            {
                if (File.Exists(ReportPath))
                {
                    oRpt.Load(ReportPath);
                }
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                oRpt.SetDataSource(dtResult);

                if (Common.Sql_LogMod == 2)//混合驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table dt in oRpt.Database.Tables)
                    {
                        dt.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                else if (Common.Sql_LogMod == 1)//SQL驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table dt in oRpt.Database.Tables)
                    {
                        dt.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                oRpt.Refresh();

                TextObject myFieldTitleName = null;
                List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
                //公司抬頭
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.Sys_StcPnName;
                }
                //單行註腳
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (rdFooter1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (rdFooter2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (rdFooter3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (rdFooter4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (rdFooter5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                //日期區間
                if (Txt.Find(t => t.Name == "DateRange") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateRange"];
                    myFieldTitleName.Text = DataRange;
                }
                List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();


                //日期格式
                if (Num.Find(p => p.Name == "date") != null)
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
                if (Num.Find(p => p.Name == "備註說明") != null)
                {
                    oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                }

                //報表參數設定
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

                if (Num.Find(p => p.Name == "進貨單價小數") != null)
                    oRpt.SetParameterValue("進貨單價小數", Common.MF);
                if (Num.Find(p => p.Name == "進貨單據小數") != null)
                    oRpt.SetParameterValue("進貨單據小數", Common.MFT);
                if (Num.Find(p => p.Name == "進項稅額小數") != null)
                    oRpt.SetParameterValue("進項稅額小數", Common.TF);

                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    oRpt.SetParameterValue("進項金額小數", Common.TPF);
                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                dtResult.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
