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
    public partial class FrmItemSale_Rptb : Formbase
    {
        class MyClass
        {
            public string itno { get; set; }
            public string unit { get; set; }
        }
        List<MyClass> list = new List<MyClass>();

        public DataTable TResult = new DataTable();
        public DataTable TBom = new DataTable();
        public string dateRange = "";
        public bool CheckAVG = false;

        string ReportFileName = "";
        string ReportPath = "";

        public FrmItemSale_Rptb()
        {
            InitializeComponent();

            數量.DefaultCellStyle.Format = "f" + Common.Q;
            售價.DefaultCellStyle.Format = "f" + Common.MS;
            折數.DefaultCellStyle.Format = "f3";
            稅前售價.DefaultCellStyle.Format = "f6";
            稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;
            包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            銷貨成本.DefaultCellStyle.Format = "f" + Common.TPS;
            銷貨毛利.DefaultCellStyle.Format = "f" + Common.TPS;
            毛利率.DefaultCellStyle.Format = "f" + Common.TPS;

            
        }

        private void FrmItemSale_Rptb_Load(object sender, EventArgs e)
        {
            this.倉庫名稱.Visible = CheckAVG;

            rd2.SetUserDefineRpt("Reitemud1.rpt");
            rd3.SetUserDefineRpt("Reitemud2.rpt");
            rd4.SetUserDefineRpt("Reitemud4.rpt");
            rd5.SetUserDefineRpt("Reitemud5.rpt");
             
            list = TResult.AsEnumerable()
                .GroupBy(r => new
                {
                    key1 = r["itno"].ToString().Trim(),
                    key2 = r["itunit"].ToString().Trim()
                })
                .Select(g => new MyClass()
                {
                    itno = g.Key.key1,
                    unit = g.Key.key2
                })
                .ToList();

            dataGridViewT1.DataSource = TResult;
            LoadDB(list.First());
        }

        void LoadDB(MyClass mc)
        {
            TResult.DefaultView.RowFilter = "ItNo='" + mc.itno + "' and ItUnit = '" + mc.unit + "'";

            textBoxT1.Text = TResult.DefaultView[0]["itname"].ToString();
            textBoxT2.Text = mc.itno;
            textBoxT3.Text = mc.unit;

            var qty = 0M;
            var mny = 0M;
            var cost = 0M;
            var profit = 0M;
            for (int i = 0; i < TResult.DefaultView.Count; i++)
            {
                qty += TResult.DefaultView[i]["qty"].ToDecimal("f" + Common.Q);
                mny += TResult.DefaultView[i]["mny"].ToDecimal("f" + Common.TPS);
                cost += TResult.DefaultView[i]["銷貨成本"].ToDecimal("f" + Common.TPS);
                profit += TResult.DefaultView[i]["銷貨毛利"].ToDecimal("f" + Common.TPS);
            }
            textBoxT4.Text = mny.ToString("f" + Common.TPS);
            textBoxT5.Text = cost.ToString("f" + Common.TPS);
            textBoxT6.Text = qty.ToString("f" + Common.Q);
            textBoxT7.Text = profit.ToString("f" + Common.TPS);

            if (mny == 0)
                textBoxT8.Text = 0.ToString("f" + Common.TPS);
            else
                textBoxT8.Text = ((profit / mny) * 100).ToString("f" + Common.TPS);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            LoadDB(list.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.FindIndex(mc => mc.itno == textBoxT2.Text && mc.unit == textBoxT3.Text) - 1;
            if (index <= 0)
                index = 0;
            LoadDB(list.ElementAt(index));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.FindIndex(mc => mc.itno == textBoxT2.Text && mc.unit == textBoxT3.Text) + 1;
            if (index >= list.Count - 1)
                index = list.Count - 1;
            LoadDB(list.ElementAt(index));
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            LoadDB(list.Last());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            list.Clear();
            TResult.Clear();
            TBom.Clear();

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
                if (d1.Checked)
                    ReportPath += "產品銷售報表O_簡要表.rpt";
                else
                    ReportPath += "產品銷售報表_簡要表.rpt";
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
                ReportPath += "Reitemud1.rpt";
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
                ReportPath += "Reitemud2.rpt";
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
            else if (rd4.Checked)
            {
                if (File.Exists(ReportPath + "Reitemud3.rpt"))
                {
                    oRpt.Load(ReportPath + "Reitemud3.rpt");
                }
                else
                {
                    if (d1.Checked)
                        ReportPath += "產品銷售報表O_組件自定一.rpt";
                    else
                        ReportPath += "產品銷售報表_組件自定一.rpt";
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
            }
            else if (rd5.Checked)
            {
                ReportPath += "Reitemud4.rpt";
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

            if (rd4.Checked || rd5.Checked)
            {
                //TBOM  撈bom的資料, 現在還沒撈
                oRpt.SetDataSource(TResult);
                //oRpt.SetDataSource(TBom);
            }
            else
                oRpt.SetDataSource(TResult);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                TResult.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
         
    }
}
