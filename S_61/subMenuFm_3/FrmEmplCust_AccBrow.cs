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
    public partial class FrmEmplCust_AccBrow : Formbase
    {
        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();

        List<string> list = new List<string>();
        RPT rp;
        public string DateRange = "";
        public bool IsPrintBefore = false;


        public FrmEmplCust_AccBrow()
        {
            InitializeComponent();

            this.前期帳款.DefaultCellStyle.Format = "f" + Common.M;
            this.交易筆數.DefaultCellStyle.Format = "f0";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.M;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.M;
            this.應收總計.DefaultCellStyle.Format = "f" + Common.M;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.M;
            this.已收加預收.DefaultCellStyle.Format = "f" + Common.M;
            this.本期應收.DefaultCellStyle.Format = "f" + Common.M;
            this.前期加本期.DefaultCellStyle.Format = "f" + Common.M;
        }

        private void FrmEmplCust_AccBrow_Load(object sender, EventArgs e)
        {
            radio1.Checked = true;
            radioT1.Checked = true;

            radio3.SetUserDefineRpt("業務別應收帳款總額表自定一.rpt");
            radio4.SetUserDefineRpt("業務別應收帳款總額表自定二.rpt");
            radio5.SetUserDefineRpt("業務別應收帳款總額表標籤自定一.rpt");

            dataGridViewT1.DataSource = dt.DefaultView;

            list = dt.AsEnumerable().GroupBy(r => r["emno"].ToString().Trim()).Select(g => g.Key).ToList();
            WriteToTitle(list.First());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void WriteToTitle(string emno)
        {
            dt.DefaultView.RowFilter = "EmNo ='" + emno + "'";
            dt.DefaultView.Sort = "cuno";
            {
                textBoxT1.Text = emno;
                textBoxT2.Text = dt.DefaultView[0]["emname"].ToString();
                var p = dt.DefaultView.OfType<DataRowView>();

                textBoxT3.Text = p.Sum(r => r["前期帳款"].ToDecimal()).ToString("f" + Common.M);

                textBoxT4.Text = p.Sum(r => r["交易筆數"].ToDecimal()).ToString("f0");
                textBoxT5.Text = p.Sum(r => r["taxmnyb"].ToDecimal()).ToString("f" + Common.M);
                textBoxT6.Text = p.Sum(r => r["taxb"].ToDecimal()).ToString("f" + Common.M);
                textBoxT7.Text = p.Sum(r => r["totmnyb"].ToDecimal()).ToString("f" + Common.M);

                textBoxT8.Text = p.Sum(r => r["discount"].ToDecimal()).ToString("f" + Common.M);
                textBoxT9.Text = p.Sum(r => r["已收加預收"].ToDecimal()).ToString("f" + Common.M);
                textBoxT10.Text = p.Sum(r => r["acctmny"].ToDecimal()).ToString("f" + Common.M);
                textBoxT11.Text = p.Sum(r => r["前期加本期"].ToDecimal()).ToString("f" + Common.M);
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToTitle(list.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var emno = textBoxT1.Text;
            if (emno == list.First())
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var index = list.IndexOf(emno);
                WriteToTitle(list[--index]);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var emno = textBoxT1.Text;
            if (emno == list.Last())
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var index = list.IndexOf(emno);
                WriteToTitle(list[++index]);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToTitle(list.Last());
        }

        void paramsInit()
        {
            string path = "";
            if (radio1.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款稅前報表.rpt";
            }
            else if (radio2.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款稅後報表.rpt";
            }
            else if (radio3.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款總額表自定一.rpt";
            }
            else if (radio4.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款總額表自定二.rpt";
            }
            else if (radio5.Checked)
            {
                path = Common.reportaddress + "Report\\業務別應收帳款總額表標籤自定一.rpt";
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
            rp.office = "業務別應收帳款";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
            rp.lobj.Add(new string[] { "Xa1No", "幣    別：本幣" });
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
