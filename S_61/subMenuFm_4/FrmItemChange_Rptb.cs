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
    public partial class FrmItemChange_Rptb : Formbase
    {
        public string stno = "";
        public string stname = "";
        public DataTable dtD = new DataTable();
        public string DateRange { get; set; }
         
        string ReportFileName = "";
        string ReportPath = "";

        public FrmItemChange_Rptb()
        {
            InitializeComponent();

            this.期初數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.期初成本.DefaultCellStyle.Format = "f" + Common.M;
            this.期初金額.DefaultCellStyle.Format = "f" + Common.M;
            this.進貨數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.進貨平均單價.DefaultCellStyle.Format = "f" + Common.M;
            this.進貨金額.DefaultCellStyle.Format = "f" + Common.M;
            this.銷貨數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.銷貨成本.DefaultCellStyle.Format = "f" + Common.M;
            this.領料數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.入料數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.入庫數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.扣料數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.調整數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.借出數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.還入數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.借入數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.還出數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.撥入數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.撥出數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.結餘數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.寄庫數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.領庫數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.結餘單位成本.DefaultCellStyle.Format = "f" + Common.M;
            this.結餘金額.DefaultCellStyle.Format = "f" + Common.M;


            this.期初成本.Visible = Common.User_ShopPrice;
            this.期初金額.Visible = Common.User_ShopPrice;
            this.進貨平均單價.Visible = Common.User_ShopPrice;
            this.進貨金額.Visible = Common.User_ShopPrice;

            this.銷貨成本.Visible = Common.User_ShopPrice;
            this.結餘單位成本.Visible = Common.User_ShopPrice;
            this.結餘金額.Visible = Common.User_ShopPrice;

        }

        private void FrmItemChange_Rptb_Load(object sender, EventArgs e)
        {
            ReportFileName = "產品進銷異動表_彙總表";

            rd2.SetUserDefineRpt("產品進銷異動表_彙總表_自定一.rpt");
            rd3.SetUserDefineRpt("產品進銷異動表_彙總表_自定二.rpt");
             
            dtD.DefaultView.Sort = "itno";
            dataGridViewT1.DataSource = dtD;
        }

        void dataintodocument(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName;

            if (rd1.Checked)
            {
                ReportPath += "_標準報表.rpt";
                if (File.Exists(ReportPath))
                    oRpt.Load(ReportPath);
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (rd2.Checked)
            {
                ReportPath += "_自定一.rpt";
                if (File.Exists(ReportPath))
                    oRpt.Load(ReportPath);
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                ReportPath += "_自定二.rpt";
                if (File.Exists(ReportPath))
                    oRpt.Load(ReportPath);
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            oRpt.SetDataSource(dtD);

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
                if (rd6.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (rd7.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (rd8.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (rd9.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (rd10.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else myFieldTitleName.Text = "";
            }

            if (Txt.Find(t => t.Name == "txtadress") != null)
            {
                //表頭-公司住址
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                myFieldTitleName.Text = "    " + Common.dtSysSettings.Rows[0]["StcAddr1"].ToString();
            }
            //日期區間
            if (Txt.Find(t => t.Name == "DateRange") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateRange"];
                myFieldTitleName.Text = "日期區間:" + DateRange;
            }
            //倉庫編號名稱
            if (Txt.Find(t => t.Name == "StNoName") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["StNoName"];
                myFieldTitleName.Text = "倉庫名稱:[" + stno + "] " + stname;
            }

            List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
            //備註說明
            if (Num.Find(p => p.Name == "備註說明") != null)
                oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
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
            if (Num.Find(p => p.Name == "本幣金額小數") != null)
                oRpt.SetParameterValue("本幣金額小數", Common.M);
            if (Num.Find(p => p.Name == "庫存數量小數") != null)
                oRpt.SetParameterValue("庫存數量小數", Common.Q);
            if (Num.Find(p => p.Name == "是否顯示金額") != null)
                oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);
            if (Num.Find(p => p.Name == "是否顯示成本") != null)
                oRpt.SetParameterValue("是否顯示成本", Common.User_ShopPrice);

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
            dtD.Clear();
            this.Dispose();
            this.Close();
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
