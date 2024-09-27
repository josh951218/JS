using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmEmplCust_AccBrowx : Formbase
    {
        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();
        IEnumerable<IGrouping<string, DataRow>> GroupbyDate = null;
        IGrouping<string, DataRow> g = null;
        RPT rp;
        public string DateRange = "";

        public FrmEmplCust_AccBrowx()
        {
            InitializeComponent();

            this.前期帳款.DefaultCellStyle.Format = "f" + Common.MST;
            this.交易筆數.DefaultCellStyle.Format = "f0";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.TS;
            this.應收總計.DefaultCellStyle.Format = "f" + Common.MST;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.已收加預收.DefaultCellStyle.Format = "f" + Common.MST;
            this.本期應收.DefaultCellStyle.Format = "f" + Common.MST;
            this.前期加本期.DefaultCellStyle.Format = "f" + Common.MST;
        }

        private void FrmEmplCust_AccBrowx_Load(object sender, EventArgs e)
        {
            radio1.Checked = true;
            radioT1.Checked = true;

            radio3.SetUserDefineRpt("業務別應收帳款_幣別總額表自定一.rpt");
            radio4.SetUserDefineRpt("業務別應收帳款_幣別總額表自定二.rpt");
            radio5.SetUserDefineRpt("業務別應收帳款_幣別總額表標籤自定一.rpt");

            GroupbyDate = dt.AsEnumerable().GroupBy(r => r["emno"].ToString().Trim());
            g = GroupbyDate.FirstOrDefault();
            WriteToTitle(g);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void WriteToTitle(IGrouping<string, DataRow> grow)
        {
            if (grow.IsNotNull())
            {
                temp = grow.CopyToDataTable();
                var p = temp.AsEnumerable();

                textBoxT1.Text = grow.Key;
                textBoxT2.Text = temp.Rows[0]["emname"].ToString();
                textBoxT3.Text = p.Sum(r => r["筆數"].ToDecimal()).ToString("f0");

                dataGridViewT1.DataSource = temp;
            }
            else
            {
                foreach (var item in this.Controls.OfType<TextBox>())
                {
                    item.Clear();
                }
                temp.Clear();
                dataGridViewT1.DataSource = null;
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            g = GroupbyDate.FirstOrDefault();
            WriteToTitle(g);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == 0)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[--index];
                WriteToTitle(g);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == GroupbyDate.Count() - 1)
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[++index];
                WriteToTitle(g);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            g = GroupbyDate.LastOrDefault();
            WriteToTitle(g);
        }

        void paramsInit()
        {
            string path = "";
            if (radio1.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款_幣別總額稅前報表.rpt";
            }
            else if (radio2.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款_幣別總額稅後報表.rpt";
            }
            else if (radio3.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款_幣別總額自定一.rpt";
            }
            else if (radio4.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款_幣別總額自定二.rpt";
            }
            else if (radio5.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款_幣別總額標籤自定一.rpt";
            }

            string txtend = "";
            if (radioT1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            rp = new RPT(dt, path);
            rp.office = "業務別應收帳款總額表";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Excel();
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
