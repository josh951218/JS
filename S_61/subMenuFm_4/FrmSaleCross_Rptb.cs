using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmSaleCross_Rptb : Formbase
    {
        public DataTable dtD;
        public string DateRange = "";
        List<Button> query;
        string NO = "";
        string ReportFileName = "";
        string ReportPath = "";
        string ReportStatus = "客戶產品";
        string Reportmiddle = "客  戶";

        public FrmSaleCross_Rptb()
        {
            InitializeComponent();

            query = new List<Button> { query2, query3, query4, query5, query6, query7, query8, query9 };
            銷貨總金額.LastNum = 銷貨總毛利.LastNum = 銷貨總成本.LastNum = Common.M;
            銷貨總數量.LastNum = Common.Q;
            銷貨毛利率.LastNum = 3;
            銷貨總金額.FirstNum = 銷貨總毛利.FirstNum = 銷貨總成本.FirstNum = (20 - 1 - Common.M);
            銷貨總數量.FirstNum = (20 - 1 - Common.Q);
            銷貨毛利率.FirstNum = 16;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.折數.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前售價.DefaultCellStyle.Format = "f" + Common.TPS;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨成本.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨毛利.DefaultCellStyle.Format = "f" + Common.TPS;
            this.毛利率.DefaultCellStyle.Format = "f" + Common.TPS;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
        }

        private void FrmSaleCross_Rptb_Load(object sender, EventArgs e)
        {
            SearchUserReport("客戶產品");

            dtD.Columns.Add("序號", typeof(String));
            dataGridViewT1.DataSource = dtD;
            query2.ForeColor = Color.Red;
            decimal tot金額 = 0, tot成本 = 0, tot毛利 = 0, tot數量 = 0;

            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = i;
                tot金額 += dataGridViewT1["稅前金額", i].Value.ToDecimal();
                tot成本 += dataGridViewT1["銷貨成本", i].Value.ToDecimal();
                tot毛利 += dataGridViewT1["銷貨毛利", i].Value.ToDecimal();
                tot數量 += dataGridViewT1["數量", i].Value.ToDecimal();
            }

            銷貨總金額.Text = tot金額.ToString("f" + Common.TPS);
            銷貨總成本.Text = tot成本.ToString("f" + Common.TPS);
            銷貨總毛利.Text = tot毛利.ToString("f" + Common.TPS);
            銷貨總數量.Text = tot數量.ToString("f" + Common.Q);

            if (tot金額 == 0)
                銷貨毛利率.Text = "0.00";
            else
                銷貨毛利率.Text = ((tot毛利 / tot金額) * 100).ToString("f2");

            checkorder();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            CuNo.Text = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();
            ItNo.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
            EmNo.Text = dataGridViewT1.SelectedRows[0].Cells["業務編號"].Value.ToString();
        }

        void SetButtonColor()
        {
            query.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow(string NO)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == NO)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }

        }

        void checkorder()
        {
            if (rd1.Checked && (ReportStatus == "產品" || ReportStatus == "業務產品" || ReportStatus == "業務輔助"))
            {
                groupBoxT2.Enabled = true;
            }
            else groupBoxT2.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();
            
            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtD.Excel匯出並開啟(this.Text);
            }
            else if (keyValue.Contains("Shift") || keyValue.Contains("F2"))
            {
                query2.Focus();
                query2.PerformClick();
            }
            else if (keyValue.Contains("Shift") || keyValue.Contains("F3"))
            {
                query3.Focus();
                query3.PerformClick();
            }
            else if (keyValue.Contains("Shift") || keyValue.Contains("F4"))
            {
                query4.Focus();
                query4.PerformClick();
            }
            else if (keyValue.Contains("Shift") || keyValue.Contains("F5"))
            {
                query5.Focus();
                query5.PerformClick();
            }
            else if (keyValue.Contains("Shift") || keyValue.Contains("F6"))
            {
                query6.Focus();
                query6.PerformClick();
            }
            else if (keyValue.Contains("Shift") || keyValue.Contains("F7"))
            {
                query7.Focus();
                query7.PerformClick();
            }
            else if (keyValue.Contains("Shift") || keyValue.Contains("F8"))
            {
                query8.Focus();
                query8.PerformClick();
            }
            else if (keyValue.Contains("F11"))
            {
                btnExit.Focus();
                btnExit.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void SearchUserReport(string sort)
        {
            ReportFileName = "銷售交叉分析報表";
            rd1.Checked = true;
            rd3.SetUserDefineRpt("銷售交叉分析報表" + sort + "_自定一.rpt");
            rd4.SetUserDefineRpt("銷售交叉分析報表" + sort + "_自定二.rpt");
        }

        private void query2_Click(object sender, EventArgs e)
        {
            SearchUserReport("客戶產品");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "客戶編號,產品編號";
            SetButtonColor();
            query2.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "客戶產品";
            Reportmiddle = "客  戶";
            checkorder();
        }

        private void query3_Click(object sender, EventArgs e)
        {
            SearchUserReport("產品");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "產品編號";
            SetButtonColor();
            query3.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "產品";
            Reportmiddle = "產  品";
            checkorder();
        }

        private void query4_Click(object sender, EventArgs e)
        {
            SearchUserReport("業務產品");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "業務編號,產品編號";
            SetButtonColor();
            query4.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "業務產品";
            Reportmiddle = "業  務";
            checkorder();
        }

        private void query5_Click(object sender, EventArgs e)
        {
            SearchUserReport("業務客戶產品");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "業務編號,客戶編號,產品編號";
            SetButtonColor();
            query5.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "業務客戶產品";
            Reportmiddle = "業  務";
            checkorder();
        }

        private void query6_Click(object sender, EventArgs e)
        {
            SearchUserReport("業務輔助");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "業務編號,輔助編號";
            SetButtonColor();
            query6.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "業務輔助";
            Reportmiddle = "業  務";
            checkorder();
        }

        private void query7_Click(object sender, EventArgs e)
        {
            SearchUserReport("專案產品");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "專案編號,產品編號";
            SetButtonColor();
            query7.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "專案產品";
            Reportmiddle = "專  案";
            checkorder();
        }

        private void query8_Click(object sender, EventArgs e)
        {
            SearchUserReport("送貨產品");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "送貨編號,產品編號";
            SetButtonColor();
            query8.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "送貨產品";
            Reportmiddle = "送  貨";
            checkorder();
        }

        private void query9_Click(object sender, EventArgs e)
        {
            SearchUserReport("業務產品");
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dtD.DefaultView.Sort = "業務編號,單據日期";
            SetButtonColor();
            query9.ForeColor = Color.Red;
            SetSelectRow(NO);
            ReportStatus = "業務產品";
            Reportmiddle = "業  務";
            checkorder();
        }

        void OutReport(RptMode mode)
        {
            if (rd1.Checked)
            {
                if (groupBoxT2.Enabled == true)
                {
                    if (radioT1.Checked == true) ReportPath = Common.reportaddress + "Report\\" + ReportFileName + ReportStatus + "O_明細表.rpt";
                    else ReportPath = Common.reportaddress + "Report\\" + ReportFileName + ReportStatus + "_明細表.rpt";
                }
                else
                    ReportPath = Common.reportaddress + "Report\\" + ReportFileName + ReportStatus + "_明細表.rpt";
            }
            else if (rd2.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + ReportStatus + "_簡要表.rpt";
            else if (rd3.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + ReportStatus + "_自定一.rpt";
            else
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + ReportStatus + "_自定二.rpt";

            RPT rp = new RPT(dtD, ReportPath);
            rp.office = "類別銷售報表_明細表";
            //制表日期
            rp.lobj.Add(new string[] { "制表日期", Date.GetDateTime(Common.User_DateTime, true) });
            //日期區間
            rp.lobj.Add(new string[] { "日期區間", DateRange });
            //報表二頭
            if (rd2.Checked)
                rp.lobj.Add(new string[] { "txtmiddle", Reportmiddle + "  銷  貨  簡  要  表" });
            else
                rp.lobj.Add(new string[] { "txtmiddle", Reportmiddle + "  銷  貨  明  細  表" });
            //單行註腳
            if (this.FindControl("groupBoxT2") != null)
            {
                string txtend;
                if (((RadioT)this.FindControl("r" + "1")).Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (((RadioT)this.FindControl("r" + "2")).Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (((RadioT)this.FindControl("r" + "3")).Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (((RadioT)this.FindControl("r" + "4")).Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (((RadioT)this.FindControl("r" + "5")).Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                rp.lobj.Add(new string[] { "txtend", txtend });
            }

            if (mode == RptMode.Print)
                rp.Print();
            else if (mode == RptMode.PreView)
                rp.PreView();
            else if (mode == RptMode.Word)
                rp.Word();
            else if (mode == RptMode.Excel)
                rp.Excel();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreview_Click(object sender, EventArgs e)
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



    }
}
