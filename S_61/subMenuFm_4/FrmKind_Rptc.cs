using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmKind_Rptc : Formbase
    {
        public DataTable table;
        public string DateRange = "";
        public string flag = "";
        List<Button> query;
        string NO = "";
        string ReportPath = "";
        bool IsFastReport = false;

        public FrmKind_Rptc(bool isFastReport = false)
        {
            InitializeComponent();

            query5.ForeColor = Color.Red;
            query = new List<Button> { query2, query3, query4, query5 };
            this.銷貨數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.銷貨金額.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨成本.DefaultCellStyle.Format = "f" + Common.TPS;
            this.銷貨毛利.DefaultCellStyle.Format = "f" + Common.TPS;
            this.毛利率.DefaultCellStyle.Format = "f" + Common.TPS;

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

        private void FrmKind_Rptc_Load(object sender, EventArgs e)
        {
            table.Columns.Add("序號", typeof(String));
            table.Columns.Add("排名", typeof(String));

            dataGridViewT1.DataSource = table;

            if (flag == "類別排行榜")
            {
                this.品名規格.Visible = false;
                this.單位.Visible = false;
                this.產品編號.Visible = false;
                query2.Text = "f2:產品類別";
            }
            else
            {
                this.產品類別.Visible = false;
                this.類別名稱.Visible = false;
                query2.Text = "f2:產品編號+單位";
            }

            decimal tot金額 = 0, tot成本 = 0, tot毛利 = 0, tot數量 = 0;
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = i;
                dataGridViewT1["排名", i].Value = i + 1;
                tot金額 += dataGridViewT1["銷貨金額", i].Value.ToDecimal();
                tot成本 += dataGridViewT1["銷貨成本", i].Value.ToDecimal();
                tot毛利 += dataGridViewT1["銷貨毛利", i].Value.ToDecimal();
                tot數量 += dataGridViewT1["銷貨數量", i].Value.ToDecimal();
            }

            銷貨總金額.Text = tot金額.ToString("f" + Common.TPS);
            銷貨總成本.Text = tot成本.ToString("f" + Common.TPS);
            銷貨總毛利.Text = tot毛利.ToString("f" + Common.TPS);
            銷貨總數量.Text = tot數量.ToString("f" + Common.Q);

            if (tot金額 == 0) 銷貨毛利率.Text = (0M).ToString("f" + Common.TPS);
            else
            {
                銷貨毛利率.Text = ((tot毛利 / tot金額) * 100).ToString("f" + Common.TPS);
            }
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
            dataGridViewT1.DataSource = null;
            if (flag == "類別排行榜")
                table = table.AsEnumerable().OrderByDescending(r => r["產品類別"].ToDecimal()).CopyToDataTable();
            else
                table = table.AsEnumerable().OrderByDescending(r => r["產品編號"].ToString()).ThenByDescending(r => r["單位"].ToString()).CopyToDataTable();

            int i = 1;
            table.AsEnumerable().ToList().ForEach(r =>
            {
                r["排名"] = i;
                ++i;
            });
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query3_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dataGridViewT1.DataSource = null;
            table = table.AsEnumerable().OrderByDescending(r => r["數量"].ToDecimal()).CopyToDataTable();
            int i = 1;
            table.AsEnumerable().ToList().ForEach(r =>
            {
                r["排名"] = i;
                ++i;
            });
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query4_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dataGridViewT1.DataSource = null;
            table = table.AsEnumerable().OrderByDescending(r => r["稅前金額"].ToDecimal()).CopyToDataTable();
            int i = 1;
            table.AsEnumerable().ToList().ForEach(r =>
            {
                r["排名"] = i;
                ++i;
            });
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query5_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dataGridViewT1.DataSource = null;
            table = table.AsEnumerable().OrderByDescending(r => r["毛利"].ToDecimal()).CopyToDataTable();
            int i = 1;
            table.AsEnumerable().ToList().ForEach(r =>
            {
                r["排名"] = i;
                ++i;
            });
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query5.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();
            if (keyValue.Contains("Shift") && keyValue.Contains("F2"))
            {
                query2.Focus();
                query2.PerformClick();
            }
            else if (keyValue.Contains("Shift") && keyValue.Contains("F3"))
            {
                query3.Focus();
                query3.PerformClick();
            }
            else if (keyValue.Contains("Shift") && keyValue.Contains("F4"))
            {
                query4.Focus();
                query4.PerformClick();
            }
            else if (keyValue.Contains("Shift") && keyValue.Contains("F5"))
            {
                query5.Focus();
                query5.PerformClick();
            }
            else if (keyValue.Contains("F11"))
            {
                btnExit.Focus();
                btnExit.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
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

        void OutReport(RptMode mode)
        {
            if (this.IsFastReport)
            {
                OutReportF(mode);
                return;
            }

            if (flag == "類別排行榜")
                ReportPath = Common.reportaddress + "Report\\類別銷售報表_類別銷售排行榜_標準報表.rpt";
            else
                ReportPath = Common.reportaddress + "Report\\類別銷售報表_產品銷售排行榜_標準報表.rpt";

            RPT rp = new RPT(table, ReportPath);

            if (flag == "類別排行榜")
                rp.office = "類別銷售報表_類別銷售排行榜";
            else
                rp.office = "類別銷售報表_產品銷售排行榜";

            //制表日期
            rp.lobj.Add(new string[] { "制表日期", Date.GetDateTime(Common.User_DateTime, true) });
            //日期區間
            rp.lobj.Add(new string[] { "日期區間", DateRange });
            //單行註腳
            if (this.FindControl("groupBoxT2") != null)
            {
                string txtend = "";
                if (r1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (r2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (r3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (r4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (r5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                rp.lobj.Add(new string[] { "txtend", txtend });
            }
            //排名方式
            (new ButtonSmallT[] { query2, query3, query4, query5 }).ToList().ForEach(r =>
            {
                if (r.ForeColor == Color.Red)
                    rp.lobj.Add(new string[] { "排行方式", r.Text.Substring(3) });
            });

            if (mode == RptMode.Print)
                rp.Print();
            else if (mode == RptMode.PreView)
                rp.PreView();
            else if (mode == RptMode.Word)
                rp.Word();
            else if (mode == RptMode.Excel)
                rp.Excel();
        }

        void OutReportF(RptMode mode)
        {
            if (table.Rows.Count == 0 && mode != RptMode.Design)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            var path = Common.reportaddress + @"ReportF\類別銷售排行表.frx";

            using (var fs = new JBS.FReport())
            {
                var txtend = "";
                if (r1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (r2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (r3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (r4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (r5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();

                //排名方式
                var RankState = "";
                (new ButtonSmallT[] { query2, query3, query4, query5 }).ToList().ForEach(r =>
                {
                    if (r.ForeColor == Color.Red)
                        RankState = r.Text.Substring(3);
                });

                fs.dy.Add("txtend", txtend);
                fs.dy.Add("DateRange", DateRange);
                fs.dy.Add("RankState", "排行方式:" + RankState);

                fs.OutReport(mode, table, "Table1", path);
            }
        }


    }
}
