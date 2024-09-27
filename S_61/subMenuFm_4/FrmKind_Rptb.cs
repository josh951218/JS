using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmKind_Rptb : Formbase
    {
        public DataTable table;
        public string DateRange = "";
        public bool CheckAVG = false;
        List<Button> query;
        string NO = "";
        string ReportFileName = "";
        string ReportPath = "";

        public FrmKind_Rptb()
        {
            InitializeComponent();
            query = new List<Button> { query2, query3, query4, query5 };
            銷貨總金額.LastNum = 銷貨總毛利.LastNum = 銷貨總成本.LastNum = Common.TPS;
            銷貨總數量.LastNum = Common.Q;
            //銷貨毛利率.LastNum = 3;
            銷貨總金額.FirstNum = 銷貨總毛利.FirstNum = 銷貨總成本.FirstNum = (20 - 1 - Common.TPS);
            銷貨總數量.FirstNum = (20 - 1 - Common.Q);
            //銷貨毛利率.LastNum = 20 - 1 - 3;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.折數.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨成本.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨毛利.DefaultCellStyle.Format = "f" + Common.TPS;
            this.毛利率.DefaultCellStyle.Format = "f" + Common.TPS;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
        }

        private void FrmKind_Rptb_Load(object sender, EventArgs e)
        {
            this.倉庫名稱.Visible = CheckAVG;

            ReportFileName = "類別銷售報表_明細表";
            rd3.SetUserDefineRpt("類別銷售報表_明細表_自定一.rpt");

            table.Columns.Add("序號", typeof(String));

            dataGridViewT1.AutoGenerateColumns = false;
            dataGridViewT1.DataSource = table;
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
            if (tot金額 == 0) 銷貨毛利率.Text = (0M).ToString("f" + Common.TPS);
            else
                銷貨毛利率.Text = ((tot毛利 / tot金額) * 100).ToString("f" + Common.TPS);
        }

        RPT paramsInit()
        {
            if (rd1.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_明細表.rpt";
            else if (rd2.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_簡要表.rpt";
            else
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_自定一.rpt";
            RPT rp = new RPT(table, ReportPath);
            rp.office = "類別銷售報表_明細表";
            //制表日期
            rp.lobj.Add(new string[] { "制表日期", Date.GetDateTime(Common.User_DateTime, true) });
            //日期區間
            rp.lobj.Add(new string[] { "日期區間", DateRange });
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
            return rp;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit().Print();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            paramsInit().PreView();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit().Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit().Excel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
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

        private void query2_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "產品類別,產品編號";
            SetButtonColor();
            query2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query3_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "產品類別,客戶編號,產品編號";
            SetButtonColor();
            query3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query4_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "產品類別,業務編號,產品編號";
            SetButtonColor();
            query4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query5_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table.DefaultView.Sort = "產品類別,說明,產品編號";
            SetButtonColor();
            query5.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();
            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                table.Excel匯出並開啟(this.Text);
            }
            else if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                query2.Focus();
                query2.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                query3.Focus();
                query3.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                query4.Focus();
                query4.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                query5.Focus();
                query5.PerformClick();
            }
            else if (keyData == Keys.F11)
            {
                btnExit.Focus();
                btnExit.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
         
         
    }
}
