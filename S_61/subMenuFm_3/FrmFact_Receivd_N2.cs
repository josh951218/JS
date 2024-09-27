using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmFact_Receivd_N2 : Formbase
    {
        public DataTable dtM = new DataTable();
        List<TextBox> lTitle = new List<TextBox>();
        string reportfilename = "";
        int 進貨單價小數, 進貨單據小數, 進項稅額小數, 本幣金額小數, 庫存數量小數;

        public FrmFact_Receivd_N2()
        {
            InitializeComponent();

            進貨單價小數 = Common.MF;
            進貨單據小數 = Common.MFT;
            進項稅額小數 = Common.TF;
            本幣金額小數 = Common.M;
            庫存數量小數 = Common.Q;

            lTitle = this.Controls.OfType<TextBox>().ToList();

            foreach (var t in lTitle)
            {
                if (t is TextBoxNumberT)
                {
                    ((TextBoxNumberT)t).FirstNum = Common.nFirst;
                    ((TextBoxNumberT)t).LastNum = 進貨單據小數;
                }
            }
            沖帳總筆數1.LastNum = 0;

            沖帳筆數d2.DefaultCellStyle.Format = "f0";
            折讓金額d2.Set本幣金額小數();
            現金金額d2.Set本幣金額小數();
            刷卡金額d2.Set本幣金額小數();
            禮卷金額d2.Set本幣金額小數();
            支票金額d2.Set本幣金額小數();
            匯出金額d2.Set本幣金額小數();
            其他金額d2.Set本幣金額小數();
            取用預付d2.Set本幣金額小數();
            沖帳合計d2.Set本幣金額小數();
            累入預付d2.Set本幣金額小數();
            沖抵帳款d2.Set本幣金額小數(); 
        }

        private void FrmFact_Receivdb_Load(object sender, EventArgs e)
        {
            reportfilename = "廠商別已付帳款_本幣總額表";

            dataGridViewT2.DataSource = dtM.DefaultView;

            var 沖帳總筆數 = 0M;
            var 折讓總金額 = 0M;
            var 現金總金額 = 0M;
            var 刷卡總金額 = 0M;
            var 禮券總金額 = 0M;
            var 支票總金額 = 0M;
            var 匯出總金額 = 0M;
            var 其他總金額 = 0M;
            var 取用總金額 = 0M;
            var 沖帳總合計 = 0M;
            var 累入總金額 = 0M;
            var 沖抵總金額 = 0M;

            var p = dtM.AsEnumerable()
                .Aggregate(0, (x, row) =>
                {
                    沖帳總筆數 += row["沖帳筆數"].ToDecimal("f" + Common.M);
                    折讓總金額 += row["折讓金額"].ToDecimal("f" + Common.MF);
                    現金總金額 += row["現金金額"].ToDecimal("f" + Common.M);
                    刷卡總金額 += row["刷卡金額"].ToDecimal("f" + Common.M);
                    禮券總金額 += row["禮卷金額"].ToDecimal("f" + Common.M);
                    支票總金額 += row["支票金額"].ToDecimal("f" + Common.M);
                    匯出總金額 += row["匯出金額"].ToDecimal("f" + Common.M);
                    其他總金額 += row["其他金額"].ToDecimal("f" + Common.M);
                    取用總金額 += row["取用預付"].ToDecimal("f" + Common.M);
                    沖帳總合計 += row["沖帳合計"].ToDecimal("f" + Common.M);
                    累入總金額 += row["累入預付"].ToDecimal("f" + Common.M);
                    沖抵總金額 += row["沖抵帳款"].ToDecimal("f" + Common.M);

                    return x;
                });

            沖帳總筆數1.Text = 沖帳總筆數.ToString("f0");
            折讓總金額1.Text = 折讓總金額.ToString();
            現金總金額1.Text = 現金總金額.ToString();
            刷卡總金額1.Text = 刷卡總金額.ToString();
            禮券總金額1.Text = 禮券總金額.ToString();
            支票總金額1.Text = 支票總金額.ToString();
            匯出總金額1.Text = 匯出總金額.ToString();
            其他總金額1.Text = 其他總金額.ToString();
            取用總金額1.Text = 取用總金額.ToString();
            沖帳總合計1.Text = 沖帳總合計.ToString();
            累入總金額1.Text = 累入總金額.ToString();
            沖抵總金額1.Text = 沖抵總金額.ToString();

            SetRdUdf();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox10);
            pVar.SaveRadioUdf(pnlist, "FrmFact_Receivd_N2");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox10);
            pVar.SetRadioUdf(pnlist, "FrmFact_Receivd_N2");
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
                    if (radio26.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (radio27.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (radio28.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (radio29.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (radio30.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
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
            lTitle.Clear();

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
