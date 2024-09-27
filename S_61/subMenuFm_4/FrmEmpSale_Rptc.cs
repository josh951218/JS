using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmEmpSale_Rptc : Formbase
    {
        public DataTable dt = new DataTable();
        public string dateRange { get; set; }
        string EmNoSelected = "";
        string ReportFileName = "";
        string ReportPath = "";
        string RankState = "";

        public FrmEmpSale_Rptc()
        {
            InitializeComponent();

            this.銷退金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨淨額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨成本.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨毛利.DefaultCellStyle.Format = "f" + Common.TPS;
            this.毛利率.DefaultCellStyle.Format = "f" + Common.TPS;
        }

        private void FrmEmpSale_Rptc_Load(object sender, EventArgs e)
        {
            rd2.SetUserDefineRpt("Recustud3.rpt");
            rd3.SetUserDefineRpt("Recustud4.rpt");

            var 銷貨淨額 = dt.AsEnumerable().Sum(r => r["銷貨淨額"].ToDecimal());
            var 銷貨毛利 = dt.AsEnumerable().Sum(r => r["銷貨毛利"].ToDecimal());
            var 毛利率 = 0M;
            if (銷貨淨額 != 0)
                毛利率 = ((銷貨毛利 / 銷貨淨額) * 100);

            textBoxT1.Text = dt.AsEnumerable().Sum(r => r["銷退金額"].ToDecimal()).ToString("f" + Common.TPS);
            textBoxT2.Text = dt.AsEnumerable().Sum(r => r["銷貨金額"].ToDecimal()).ToString("f" + Common.TPS);
            textBoxT3.Text = 銷貨淨額.ToString("f" + Common.TPS);
            textBoxT4.Text = dt.AsEnumerable().Sum(r => r["銷貨成本"].ToDecimal()).ToString("f" + Common.TPS);
            textBoxT5.Text = 銷貨毛利.ToString("f" + Common.TPS);
            textBoxT6.Text = 毛利率.ToString("f2");

            dataGridViewT1.DataSource = dt.DefaultView;
            btnprofit.PerformClick();

            dataGridViewT1.FirstDisplayedScrollingRowIndex = 0;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, 0];
            dataGridViewT1.Rows[0].Selected = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.Dispose();
        }

        private void btncust_Click(object sender, EventArgs e)
        {
            RankState = "業務編號";
            SetSortBtn();
            dt.DefaultView.Sort = "emno";
            SetSelectRow(EmNoSelected);
  
            btncust.ForeColor = Color.Red;
            btnrsalemny.ForeColor = Color.Black;
            btnsaleadd.ForeColor = Color.Black;
            btnprofit.ForeColor = Color.Black;
        }

        private void btnrsalemny_Click(object sender, EventArgs e)
        {
            RankState = "退貨金額";
            SetSortBtn();
            dt.DefaultView.Sort = "銷退金額";
            SetSelectRow(EmNoSelected);

            btncust.ForeColor = Color.Black;
            btnrsalemny.ForeColor = Color.Red;
            btnsaleadd.ForeColor = Color.Black;
            btnprofit.ForeColor = Color.Black;
        }

        private void btnsaleadd_Click(object sender, EventArgs e)
        {
            RankState = "銷貨淨額";
            SetSortBtn();
            dt.DefaultView.Sort = "銷貨淨額 DESC";
            SetSelectRow(EmNoSelected);

            btncust.ForeColor = Color.Black;
            btnrsalemny.ForeColor = Color.Black;
            btnsaleadd.ForeColor = Color.Red;
            btnprofit.ForeColor = Color.Black;
        }

        private void btnprofit_Click(object sender, EventArgs e)
        {
            RankState = "銷貨毛利";
            SetSortBtn();
            dt.DefaultView.Sort = "銷貨毛利 DESC";
            SetSelectRow(EmNoSelected);

            btncust.ForeColor = Color.Black;
            btnrsalemny.ForeColor = Color.Black;
            btnsaleadd.ForeColor = Color.Black;
            btnprofit.ForeColor = Color.Red;
        }

        void SetSortBtn()
        {
            if (dataGridViewT1.SelectedRows.Count > 0)
                EmNoSelected = dataGridViewT1["業務編號", dataGridViewT1.SelectedRows[0].Index].Value.ToString();
        }

        void SetSelectRow(string No)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["業務編號", i].Value.ToString() == No)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    dataGridViewT1.CurrentCell = null;
                    dataGridViewT1.CurrentCell = dataGridViewT1["業務編號", i];
                    break;
                }
            }
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt.Excel匯出並開啟(this.Text);
            }
            else if (keyData.Equals(Keys.F2))
                btncust.PerformClick();
            else if (keyData.Equals(Keys.F3))
                btnrsalemny.PerformClick();
            else if (keyData.Equals(Keys.F4))
                btnsaleadd.PerformClick();
            else if (keyData.Equals(Keys.F5))
                btnprofit.PerformClick();
            return base.ProcessCmdKey(ref msg, keyData);
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
         
        void dataintodocument(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\";

            if (rd1.Checked)
            {
                ReportPath += "業務銷售報表_銷售排行榜.rpt";
                if (File.Exists(ReportPath))
                {
                    oRpt.Load(ReportPath);
                }
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (rd2.Checked)
            {
                ReportPath += "Recustud3.rpt";
                if (File.Exists(ReportPath))
                {
                    oRpt.Load(ReportPath);
                }
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (rd3.Checked)
            {
                ReportPath += "Recustud4.rpt";
                if (File.Exists(ReportPath))
                {
                    oRpt.Load(ReportPath);
                }
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            oRpt.SetDataSource(dt.DefaultView.ToTable());
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
            try
            {
                //印表抬頭
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.Sys_StcPnName;
                }
                //日期區間
                if (Txt.Find(t => t.Name == "DateRange") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateRange"];
                    myFieldTitleName.Text += dateRange;
                }
                //單行註腳
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (r1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (r2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (r3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (r4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (r5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                //表頭-公司住址
                if (Txt.Find(t => t.Name == "txtadress") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                    myFieldTitleName.Text = "    " + Common.dtSysSettings.Rows[0]["StcAddr1"].ToString();
                }

                List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
                //日期格式
                if (Num.Find(p => p.Name == "排行方式") != null)
                {
                    oRpt.SetParameterValue("排行方式", "排行方式: " + RankState);
                }
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
                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    oRpt.SetParameterValue("進項金額小數", Common.TPF);

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


    }
}
