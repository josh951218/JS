using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_AccBrow : Formbase
    {
        public DataTable dt = new DataTable();
        public string DateRange = "";
        public string spname = "";
        RPT rp;

        public FrmCust_AccBrow()
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

        private void FrmCust_AccBrow_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt;
            radioT3.SetUserDefineRpt("客戶別應收帳款_本幣總額表_自定一.rpt");
            radioT4.SetUserDefineRpt("客戶別應收帳款_本幣總額表_自定二.rpt");
            radioT5.SetUserDefineRpt("客戶別應收帳款_本幣總額表_標籤自定一.rpt");

            textBoxT1.Text = dt.Select().Sum(r => r["前期金額"].ToDecimal()).ToString("f" + Common.M);
            textBoxT2.Text = dt.Select().Sum(r => r["交易筆數"].ToDecimal()).ToString("f" + Common.M);
            textBoxT3.Text = dt.Select().Sum(r => r["taxmnyb"].ToDecimal()).ToString("f" + Common.M);
            textBoxT4.Text = dt.Select().Sum(r => r["taxb"].ToDecimal()).ToString("f" + Common.M);
            textBoxT5.Text = dt.Select().Sum(r => r["totmnyb"].ToDecimal()).ToString("f" + Common.M);
            textBoxT6.Text = dt.Select().Sum(r => r["discount"].ToDecimal()).ToString("f" + Common.M);
            textBoxT7.Text = dt.Select().Sum(r => r["已收加預收"].ToDecimal()).ToString("f" + Common.M);
            textBoxT8.Text = dt.Select().Sum(r => r["acctmny"].ToDecimal()).ToString("f" + Common.M);
            textBoxT9.Text = dt.Select().Sum(r => r["前期加本期"].ToDecimal()).ToString("f" + Common.M);
            SetRdUdf();
        }

        void OutReport(RptMode mode)
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_本幣總額表_稅前報表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_本幣總額表_稅後報表.rpt";
            }
            else if (radioT3.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_本幣總額表_自定一.rpt";
            }
            else if (radioT4.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_本幣總額表_自定二.rpt";
            }
            else if (radioT5.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_本幣總額表_標籤自定一.rpt";
            }

            string txtend = "";
            if (radioT6.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            rp = new RPT(dt, path);
            rp.office = "客戶別應收帳款";

            //if (spname.Trim().Length > 0)
            //{
            //    rp.lobj.Add(new string[] { "txtstart", spname });
            //}

            //公司抬頭
            if (rdHeader1.Checked) { rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[0]["pnname"].ToString() }); }
            else if (rdHeader2.Checked) { rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[1]["pnname"].ToString() }); }
            else if (rdHeader3.Checked) { rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[2]["pnname"].ToString() }); }
            else if (rdHeader4.Checked) { rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[3]["pnname"].ToString() }); }
            else if (rdHeader5.Checked) { rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[4]["pnname"].ToString() }); }
            else { rp.lobj.Add(new string[] { "txtstart", "" }); }

            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });

            if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
            else if (mode == RptMode.Word) rp.Word();
            else if (mode == RptMode.Excel) rp.Excel();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pVar.SaveRadioUdf(pnlist, this.Name);
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pVar.SetRadioUdf(pnlist, this.Name);
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
