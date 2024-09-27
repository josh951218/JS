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
    public partial class FrmFact_Receivd_N1 : Formbase
    {
        public DataTable dtM = new DataTable();
        public DataTable dtM2 = new DataTable();
        public DataTable dtD = new DataTable();
        List<TextBox> lTitle = new List<TextBox>();
        List<string> lFact = new List<string>();
        string reportfilename = "";
        int 進貨單價小數, 進貨單據小數, 進項稅額小數, 本幣金額小數, 庫存數量小數;

        public FrmFact_Receivd_N1()
        {
            InitializeComponent();

            this.折讓金額.Set進貨單據小數();
            this.現金金額.Set進貨單據小數();
            this.刷卡金額.Set進貨單據小數();
            this.禮卷金額.Set進貨單據小數();
            this.支票金額.Set進貨單據小數();
            this.匯出金額.Set進貨單據小數();
            this.其他金額.Set進貨單據小數();
            this.取用預付.Set進貨單據小數();
            this.沖帳合計.Set進貨單據小數();
            this.累入預付.Set進貨單據小數();
            this.沖抵帳款.Set進貨單據小數();

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
        }

        private void FrmFact_Receivdb_Load(object sender, EventArgs e)
        {
            reportfilename = "廠商別已付帳款_明細簡要表";

            radio1.SetUserDefineRpt("廠商別已付帳款_明細簡要表_明細表.rpt");
            radio3.SetUserDefineRpt("廠商別已付帳款_明細簡要表_自定一.rpt");

            ToolTip tip = new ToolTip();
            tip.SetToolTip(radio2, reportfilename + "_簡要表");

            if (Common.User_DateTime == 1)
                dataGridViewT1.Columns["付款日期西元"].Visible = false;
            else
                dataGridViewT1.Columns["付款日期民國"].Visible = false;

            dataGridViewT1.DataSource = dtD.DefaultView;

            lFact = dtM.AsEnumerable()
                .Select(o => o["廠商編號"].ToString().Trim())
                .Distinct()
                .OrderBy(fano => fano)
                .ToList();

            writeToDatagridview(lFact.First());
            SetRdUdf();
            writeToDatagridview(lFact.First());
        }

        public void writeToDatagridview(string fact)
        {
            dtD.DefaultView.RowFilter = "廠商編號 = '" + fact + "'";

            var row = dtM.AsEnumerable().FirstOrDefault(r => r["廠商編號"].ToString().Trim() == fact);
            if (row == null)
            {
                lTitle.ForEach(t => t.Clear());
            }
            else
            {
                lTitle.ForEach(t =>
                {
                    if (t.Name.Length != 5)
                        t.Text = row[t.Name].ToString();
                    else
                        t.Text = row[t.Name].ToDecimal().ToString("f" + Common.MFT);
                });
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToDatagridview(lFact.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var fano = 廠商編號.Text.Trim();
            var index = lFact.IndexOf(fano) - 1;

            if (index <= 0)
                index = 0;


            writeToDatagridview(lFact[index]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var fano = 廠商編號.Text.Trim();
            var index = lFact.IndexOf(fano) + 1;

            if (index >= lFact.Count - 1)
                index = lFact.Count - 1;

            writeToDatagridview(lFact[index]);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToDatagridview(lFact.Last());
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox5);
            pVar.SaveRadioUdf(pnlist, "FrmFact_Receivd_N1");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox5);
            pVar.SetRadioUdf(pnlist, "FrmFact_Receivd_N1");
        }

        void dataintodocument(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();

            if (radio1.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_明細表.rpt");
            else if (radio2.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_簡要表.rpt");
            else if (radio3.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自定一.rpt");

            if (radio2.Checked)
            {
                dtM2.Clear();
                dtM2 = dtM.Clone();
                var listdtm = dtM.AsEnumerable().GroupBy(r => r["付款憑證"].ToString()).ToList();//0531改
                for (int I = 0; I < listdtm.Count; I++)
                {
                    var tempdtm = dtM.AsEnumerable().Where(r => r["付款憑證"].ToString() == listdtm[I].Key.ToString()).FirstOrDefault();
                    dtM2.ImportRow(tempdtm);
                }

                oRpt.SetDataSource(dtM2);
            }
            else
            {
                oRpt.SetDataSource(dtM);
            }
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
                    if (radio15.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                    else if (radio16.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                    else if (radio17.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                    else if (radio18.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                    else if (radio19.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
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
            dtD.Clear();
            lTitle.Clear();
            lFact.Clear();

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
