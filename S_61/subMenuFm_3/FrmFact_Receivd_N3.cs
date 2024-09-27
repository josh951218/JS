using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.subMenuFm_3
{
    public partial class FrmFact_Receivd_N3 : Formbase
    {
        public DataTable dtM = new DataTable();
        string reportfilename = "";
        int 進貨單價小數, 進貨單據小數, 進項稅額小數, 本幣金額小數, 庫存數量小數;

        public FrmFact_Receivd_N3()
        {
            InitializeComponent();

            進貨單價小數 = Common.MF;
            進貨單據小數 = Common.MFT;
            進項稅額小數 = Common.TF;
            本幣金額小數 = Common.M;
            庫存數量小數 = Common.Q;

            沖帳筆數d3.DefaultCellStyle.Format = "f0";
            折讓金額d3.Set進貨單據小數();
            現金金額d3.Set進貨單據小數();
            刷卡金額d3.Set進貨單據小數();
            禮卷金額d3.Set進貨單據小數();
            支票金額d3.Set進貨單據小數();
            匯出金額d3.Set進貨單據小數();
            其他金額d3.Set進貨單據小數();
            取用預付d3.Set進貨單據小數();
            沖帳合計d3.Set進貨單據小數();
            累入預付d3.Set進貨單據小數();
            沖抵帳款d3.Set進貨單據小數();
        }

        private void FrmFact_Receivdb_Load(object sender, EventArgs e)
        {
            reportfilename = "廠商別已付帳款_幣別總額表";
            dataGridViewT3.DataSource = dtM.DefaultView;
        }
         
        void dataintodocument(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_內定表.rpt");
            oRpt.SetDataSource(dtM);
            oRpt.Database.Tables[0].ApplyLogOnInfo(Common.logOnInfo);

            TextObject myFieldTitleName;
            //公司抬頭
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();
            }
            catch { }

            //帳款區間
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtrange"];
                myFieldTitleName.Text = pVar.FrmFact_Receivd.PaDateAcs.Text.ToString() + "~" + pVar.FrmFact_Receivd.PaDateAcs_1.Text.ToString();
            }
            catch { }
            //製表日期
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttoday"];
                switch (Common.User_DateTime)
                {
                    case 1:
                        myFieldTitleName.Text = Date.GetDateTime(1, true);
                        break;
                    case 2:
                        myFieldTitleName.Text = Date.GetDateTime(2, true);
                        break;
                }
            }
            catch { }
            //地址、電話傳真、三行註腳
            if (pVar.FrmFact_Receivd.radio1.Checked)
            {
                try
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                    myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcAddr1"].ToString();
                }
                catch { }
                try
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttel"];
                    myFieldTitleName.Text = "TEL：" + Common.dtSysSettings.Rows[0]["StcTel"].ToString()
                       + " FAX：" + Common.dtSysSettings.Rows[0]["StcTel"].ToString();
                }
                catch { }

                try
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (radio37.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (radio38.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (radio39.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (radio40.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (radio41.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                catch { }

            }

            List<ParameterField> num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
            if (num.Find(p => p.Name == "顯示千分位") != null)
            {
                if (pVar.TRS != "")
                    pVar.ShowTRS = true;
                oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
            }
            if (num.Find(p => p.Name == "千分位") != null)
                oRpt.SetParameterValue("千分位", pVar.TRS);
            if (num.Find(p => p.Name == "進貨單價小數") != null)
            {
                oRpt.SetParameterValue("進貨單價小數", 進貨單價小數.ToString());
            }
            if (num.Find(p => p.Name == "進貨單據小數") != null)
            {
                oRpt.SetParameterValue("進貨單據小數", 進貨單據小數.ToString());
            }
            if (num.Find(p => p.Name == "進項稅額小數") != null)
            {
                oRpt.SetParameterValue("進項稅額小數", 進項稅額小數.ToString());
            }
            if (num.Find(p => p.Name == "本幣金額小數") != null)
            {
                oRpt.SetParameterValue("本幣金額小數", 本幣金額小數.ToString());
            }
            if (num.Find(p => p.Name == "庫存數量小數") != null)
            {
                oRpt.SetParameterValue("庫存數量小數", 庫存數量小數.ToString());
            }
            if (num.Find(p => p.Name == "日期") != null)
            {
                switch (Common.User_DateTime)
                {
                    case 1:
                        oRpt.SetParameterValue("日期", "民國");
                        break;
                    case 2:
                        oRpt.SetParameterValue("日期", "西元");
                        break;
                }
            }

            Common.FrmReport = new Report.Frmreport();
            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;

            if (mode == RptMode.Print)
                Common.FrmReport.button1_Click(null, null);
            else if (mode == RptMode.PreView)
                Common.FrmReport.ShowDialog();
            else if (mode == RptMode.Word)
                Common.FrmReport.word(reportfilename);
            else if (mode == RptMode.Excel)
                Common.FrmReport.excel(reportfilename);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }

        private void btnPreview_Click(object sender, EventArgs e)
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
            dtM.Clear();
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtM.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
