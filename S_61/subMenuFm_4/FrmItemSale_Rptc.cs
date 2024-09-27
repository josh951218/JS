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
    public partial class FrmItemSale_Rptc : Formbase
    {
        public DataTable TResult = new DataTable();
        public DataTable ShowTResult = new DataTable();
        public string dateRange = "";
        public int choosemode = 0;
        public int rk = 0;

        string CuNoSelected = "";
        string ReportFileName = "";
        string ReportPath = "";
        string RankState = "";
        bool IsFastReport = false;

        public FrmItemSale_Rptc(bool isFastReport = false)
        {
            InitializeComponent();

            this.毛利率.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨毛利.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨成本.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨數量.DefaultCellStyle.Format = "f" + Common.Q;

            if (this.IsFastReport = isFastReport)
            {
                btnWord.Visible = false;
                btnExcel.Visible = false;

                panelT1.Controls.Remove(btnWord);
                panelT1.Controls.Remove(btnExcel);
                panelT1.Refresh();

                btnPrint.MouseDown -= new MouseEventHandler(btnPrint_MouseDown);
                btnPrint.MouseDown += new MouseEventHandler(btnPrint_MouseDown);
            }
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    OutReport(RptMode.Design);
            }
        }

        private void FrmItemSale_Rptc_Load(object sender, EventArgs e)
        {
            if (this.IsFastReport)
            {
                rd2.SetUserDefineRpt("Reitemud5.frx", @"ReportF\");
                rd3.SetUserDefineRpt("Reitemud6.frx", @"ReportF\");
            }
            else
            {
                rd2.SetUserDefineRpt("Reitemud5.rpt");
                rd3.SetUserDefineRpt("Reitemud6.rpt");
            }

            decimal totmny = 0;
            decimal totcost = 0;
            decimal totprofit = 0;
            decimal totqty = 0;
            for (int i = 0; i < TResult.Rows.Count; i++)
            {
                TResult.Rows[i]["序號"] = (i + 1);

                totmny += TResult.Rows[i]["mny"].ToDecimal();
                totcost += TResult.Rows[i]["銷貨成本"].ToDecimal();
                totprofit += TResult.Rows[i]["銷貨毛利"].ToDecimal();
                totqty += TResult.Rows[i]["qty"].ToDecimal();
            }

            textBoxT1.Text = totmny.ToString("f" + Common.TPS);
            textBoxT2.Text = totcost.ToString("f" + Common.TPS);
            textBoxT3.Text = totprofit.ToString("f" + Common.TPS);
            textBoxT4.Text = totqty.ToString("f" + Common.Q);
            textBoxT6.Text = (0M).ToString("f" + Common.TPS);

            if (totmny != 0)
            {
                textBoxT6.Text = ((totprofit / totmny) * 100).ToString("f" + Common.TPS);
            }

            ShowTResult = TResult.Clone();
            if (rk == 0)
            {
                dataGridViewT1.DataSource = TResult.DefaultView;
            }
            else
            {
                dataGridViewT1.DataSource = ShowTResult.DefaultView;
            }

            if (choosemode == 1)
            {
                RankState = "銷貨數量";
                CuNoSelected = "1";

                doSort("qty desc");
                SetSelectRow(CuNoSelected);

                btnItem.ForeColor = Color.Black;
                btnSaleQty.ForeColor = Color.Red;
                btnSaleMny.ForeColor = Color.Black;
                btnProfit.ForeColor = Color.Black;
            }
            else if (choosemode == 2)
            {
                RankState = "銷貨金額";
                CuNoSelected = "1";

                doSort("mny desc");
                SetSelectRow(CuNoSelected);

                btnItem.ForeColor = Color.Black;
                btnSaleQty.ForeColor = Color.Black;
                btnSaleMny.ForeColor = Color.Red;
                btnProfit.ForeColor = Color.Black;
            }
            else
            {
                RankState = "銷貨毛利";
                CuNoSelected = "1";

                doSort("銷貨毛利 desc");
                SetSelectRow(CuNoSelected);

                btnItem.ForeColor = Color.Black;
                btnSaleQty.ForeColor = Color.Black;
                btnSaleMny.ForeColor = Color.Black;
                btnProfit.ForeColor = Color.Red;
            }
        }

        void doSort(string sort)
        {
            if (rk == 0)
            {
                TResult.DefaultView.Sort = sort;
            }
            else
            {
                ShowTResult.Clear();
                TResult.DefaultView.Sort = sort;

                decimal totmny = 0;
                decimal totcost = 0;
                decimal totprofit = 0;
                decimal totqty = 0;
                for (int i = 0; i < TResult.DefaultView.Count; i++)
                {
                    if (i >= rk)
                        break;

                    ShowTResult.ImportRow(TResult.DefaultView[i].Row);

                    totmny += ShowTResult.Rows[i]["mny"].ToDecimal();
                    totcost += ShowTResult.Rows[i]["銷貨成本"].ToDecimal();
                    totprofit += ShowTResult.Rows[i]["銷貨毛利"].ToDecimal();
                    totqty += ShowTResult.Rows[i]["qty"].ToDecimal();
                }
                ShowTResult.DefaultView.Sort = sort;
                dataGridViewT1.DataSource = ShowTResult.DefaultView;

                textBoxT1.Text = totmny.ToString("f" + Common.TPS);
                textBoxT2.Text = totcost.ToString("f" + Common.TPS);
                textBoxT3.Text = totprofit.ToString("f" + Common.TPS);
                textBoxT4.Text = totqty.ToString("f" + Common.Q);
                textBoxT6.Text = (0M).ToString("f" + Common.TPS);
            }
        }

        void SetSelectRow(string No)
        {
            var index = 0;
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == No)
                {
                    index = i;
                    break;
                }
            }
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.Rows[index].Selected = true;
            dataGridViewT1.CurrentCell = null;
            dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", index];
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TResult.Clear();
            ShowTResult.Clear();

            this.Dispose();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            RankState = "產品編號+單位";
            if (dataGridViewT1.SelectedRows.Count > 0)
                CuNoSelected = dataGridViewT1["序號", dataGridViewT1.SelectedRows[0].Index].Value.ToString();

            doSort("itno,itunit");
            SetSelectRow(CuNoSelected);

            btnItem.ForeColor = Color.Red;
            btnSaleQty.ForeColor = Color.Black;
            btnSaleMny.ForeColor = Color.Black;
            btnProfit.ForeColor = Color.Black;
        }

        private void btnSaleQty_Click(object sender, EventArgs e)
        {
            RankState = "銷貨數量";
            if (dataGridViewT1.SelectedRows.Count > 0)
                CuNoSelected = dataGridViewT1["序號", dataGridViewT1.SelectedRows[0].Index].Value.ToString();

            doSort("qty desc");
            SetSelectRow(CuNoSelected);

            btnItem.ForeColor = Color.Black;
            btnSaleQty.ForeColor = Color.Red;
            btnSaleMny.ForeColor = Color.Black;
            btnProfit.ForeColor = Color.Black;
        }

        private void btnSaleMny_Click(object sender, EventArgs e)
        {
            RankState = "銷貨金額";
            if (dataGridViewT1.SelectedRows.Count > 0)
                CuNoSelected = dataGridViewT1["序號", dataGridViewT1.SelectedRows[0].Index].Value.ToString();

            doSort("mny desc");
            SetSelectRow(CuNoSelected);

            btnItem.ForeColor = Color.Black;
            btnSaleQty.ForeColor = Color.Black;
            btnSaleMny.ForeColor = Color.Red;
            btnProfit.ForeColor = Color.Black;
        }

        private void btnProfit_Click(object sender, EventArgs e)
        {
            RankState = "銷貨毛利";
            if (dataGridViewT1.SelectedRows.Count > 0)
                CuNoSelected = dataGridViewT1["序號", dataGridViewT1.SelectedRows[0].Index].Value.ToString();

            doSort("銷貨毛利 desc");
            SetSelectRow(CuNoSelected);

            btnItem.ForeColor = Color.Black;
            btnSaleQty.ForeColor = Color.Black;
            btnSaleMny.ForeColor = Color.Black;
            btnProfit.ForeColor = Color.Red;
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
            if (this.IsFastReport)
            {
                OutReport(mode);
                return;
            }

            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\";

            if (rd1.Checked)
            {
                ReportPath += "產品銷售報表_銷售排行榜.rpt";
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
                ReportPath += "Reitemud5.rpt";
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
                ReportPath += "Reitemud6.rpt";
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

            var db1 = dataGridViewT1.DataSource as DataView;
            DataTable db2 = TResult.Clone();
            if (this.RankState == "產品編號+單位")
            {
                if (db1 != null)
                {
                    db2 = db1.ToTable()
                        .AsEnumerable()
                        .OrderBy(r => r["itno"].ToString().Trim(), StringComparer.Ordinal)
                        .ThenBy(r => r["itunit"].ToString().Trim())
                        .CopyToDataTable();
                }

                oRpt.SetDataSource(db2);
            }
            else
            {
                oRpt.SetDataSource(TResult.DefaultView.ToTable());
            }
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

        void OutReport(RptMode mode)
        {
            if (TResult.DefaultView.Count == 0 && mode != RptMode.Design)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            var dt = TResult.DefaultView.ToTable();
            var path = Common.reportaddress + @"ReportF\產品銷售排行表.frx";

            using (var fs = new JBS.FReport())
            {
                var txtend = "";
                if (r1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (r2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (r3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (r4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (r5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();

                fs.dy.Add("txtend", txtend);
                fs.dy.Add("DateRange", dateRange);
                fs.dy.Add("RankState", "排行方式:" + RankState);

                fs.OutReport(mode, dt, "Table1", path);
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
