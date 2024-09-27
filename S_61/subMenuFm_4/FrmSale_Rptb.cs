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
    public partial class FrmSale_Rptb : Formbase
    {
        public DataTable dt = new DataTable();
        public string dateRange = "";
         
        List<string> list = new List<string>();
        string keyCuNo = "";
        string ReportFileName = "";
        string ReportPath = "";

        public FrmSale_Rptb()
        {
            InitializeComponent();

            this.銷退數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.銷貨數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.銷退金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨淨額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨成本.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨毛利.DefaultCellStyle.Format = "f" + Common.TPS;
            this.毛利率.DefaultCellStyle.Format = "f" + Common.TPS;
        }

        private void FrmSale_Rptb_Load(object sender, EventArgs e)
        {
            rd2.SetUserDefineRpt("Recustud1.rpt");
            rd3.SetUserDefineRpt("Recustud2.rpt");

            dataGridViewT1.DataSource = dt.DefaultView;

            list = dt.AsEnumerable().Select(r => r["cuno"].ToString()).Distinct().ToList();
            keyCuNo = list.First();
            LoadDB(keyCuNo);
        }

        void LoadDB(string Tkey)
        {
            dt.DefaultView.RowFilter = "CuNo = '" + Tkey + "'";

            CuNo.Text = Tkey;
            CuName1.Text = dt.DefaultView[0]["CuName1"].ToString();

            var ltView = dt.DefaultView.OfType<DataRowView>().ToList();
            textBoxT1.Text = ltView.Sum(v => v["銷退金額"].ToDecimal()).ToString("f" + Common.TPS);
            textBoxT2.Text = ltView.Sum(v => v["銷貨金額"].ToDecimal()).ToString("f" + Common.TPS);
            textBoxT3.Text = (textBoxT1.Text.ToDecimal() + textBoxT2.Text.ToDecimal()).ToString("f" + Common.TPS);
            textBoxT4.Text = ltView.Sum(v => v["銷貨成本"].ToDecimal()).ToString("f" + Common.TPS);
            textBoxT5.Text = (textBoxT3.Text.ToDecimal() - textBoxT4.Text.ToDecimal()).ToString("f" + Common.TPS);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                keyCuNo = list.First();
                LoadDB(keyCuNo);
            }
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                if (list.First() == keyCuNo)
                {

                }
                else
                {
                    int i = list.IndexOf(keyCuNo);
                    keyCuNo = list[--i];
                    LoadDB(keyCuNo);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                if (list.Last() == keyCuNo)
                {

                }
                else
                {
                    int i = list.IndexOf(keyCuNo);
                    keyCuNo = list[++i];
                    LoadDB(keyCuNo);
                }
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                keyCuNo = list.Last();
                LoadDB(keyCuNo);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.Dispose();
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
                ReportPath += "客戶銷售報表_簡要表.rpt";
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
                ReportPath += "Recustud1.rpt";
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
                ReportPath += "Recustud2.rpt";
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

            oRpt.SetDataSource(dt);
            if (Common.Sql_LogMod == 2)//混合驗證
            {
                Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                foreach (CrystalDecisions.CrystalReports.Engine.Table tb in oRpt.Database.Tables)
                {
                    tb.ApplyLogOnInfo(Common.logOnInfo);
                }
            }
            else if (Common.Sql_LogMod == 1)//SQL驗證
            {
                Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                foreach (CrystalDecisions.CrystalReports.Engine.Table tb in oRpt.Database.Tables)
                {
                    tb.ApplyLogOnInfo(Common.logOnInfo);
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
                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                    oRpt.SetParameterValue("銷項稅額小數", Common.TS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                Common.FrmReport = new Report.Frmreport();
                Common.FrmReport.cview.ReportSource = oRpt;
                Common.FrmReport.rpt1 = oRpt;

                if (rd1.Checked && mode == RptMode.Excel)
                {
                    DialogResult dialogResult = MessageBox.Show("是否需要解析Excel檔案?", "解析Excel視窗", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Common.FrmReport.excel(ReportFileName, true, "客戶簡稱:", "總        計：", "品名規格", false, true);
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
                    Common.FrmReport.word(ReportFileName);
                else if (mode == RptMode.Excel)
                    Common.FrmReport.excel(ReportFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
