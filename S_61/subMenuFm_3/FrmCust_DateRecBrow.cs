using System;
using System.Data;
using System.Linq;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_DateRecBrow : Formbase
    {
        public DataTable dt = new DataTable();
        public string DateRange = "";
         
        public FrmCust_DateRecBrow()
        {
            InitializeComponent();

            this.折讓金額.DefaultCellStyle.Format = "f" + Common.M;
            this.現金金額.DefaultCellStyle.Format = "f" + Common.M;
            this.刷卡金額.DefaultCellStyle.Format = "f" + Common.M;
            this.禮卷金額.DefaultCellStyle.Format = "f" + Common.M;
            this.支票金額.DefaultCellStyle.Format = "f" + Common.M;
            this.匯入金額.DefaultCellStyle.Format = "f" + Common.M;
            this.其它金額.DefaultCellStyle.Format = "f" + Common.M;
            this.取用預收.DefaultCellStyle.Format = "f" + Common.M;
            this.沖帳合計.DefaultCellStyle.Format = "f" + Common.M;
            this.累入預收.DefaultCellStyle.Format = "f" + Common.M;
            this.沖抵帳款.DefaultCellStyle.Format = "f" + Common.M;

            radioT2.SetUserDefineRpt("日期別已收帳款自定一.rpt");
            radioT3.SetUserDefineRpt("日期別已收帳款自定二.rpt");
        }

        private void FrmCust_DateRecBrow_Load(object sender, EventArgs e)
        {
            this.收款日期.DataPropertyName = Common.User_DateTime == 1 ? "redate" : "redate1";
            dataGridViewT1.DataSource = dt;

            WriteToTitle(dt.AsEnumerable().FirstOrDefault());
        }

        void WriteToTitle(DataRow dr)
        {
            if (dr.IsNotNull())
            {
                textBoxT1.Text = dr["筆數總"].ToDecimal().ToString();
                textBoxT2.Text = dr["折讓總"].ToDecimal().ToString("f" + Common.M);
                textBoxT3.Text = dr["現金總"].ToDecimal().ToString("f" + Common.M);
                textBoxT4.Text = dr["刷卡總"].ToDecimal().ToString("f" + Common.M);
                textBoxT5.Text = dr["禮卷總"].ToDecimal().ToString("f" + Common.M);
                textBoxT6.Text = dr["支票總"].ToDecimal().ToString("f" + Common.M);
                textBoxT7.Text = dr["匯入總"].ToDecimal().ToString("f" + Common.M);
                textBoxT8.Text = dr["其它總"].ToDecimal().ToString("f" + Common.M);
                textBoxT9.Text = dr["取用總"].ToDecimal().ToString("f" + Common.M);
                textBoxT10.Text = dr["沖帳總"].ToDecimal().ToString("f" + Common.M);
                textBoxT11.Text = dr["累入總"].ToDecimal().ToString("f" + Common.M);
                textBoxT12.Text = dr["沖抵總"].ToDecimal().ToString("f" + Common.M);
            }
            else
            {
                textBoxT1.Text = "0";
                textBoxT2.Text = "0";
                textBoxT3.Text = "0";
                textBoxT4.Text = "0";
                textBoxT5.Text = "0";
                textBoxT6.Text = "0";
                textBoxT7.Text = "0";
                textBoxT8.Text = "0";
                textBoxT9.Text = "0";
                textBoxT10.Text = "0";
                textBoxT11.Text = "0";
                textBoxT12.Text = "0";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\日期別已收帳款總額表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\日期別已收帳款自定一.rpt";
            }
            else if (radioT3.Checked)
            {
                path = Common.reportaddress + "Report\\日期別已收帳款自定二.rpt";
            }

            string txtend = "";
            if (radioT4.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            var rp = new RPT(dt, path);
            rp.office = "日期別已收帳款";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
            rp.lobj.Add(new string[] { "DateCreated", p });
            rp.lobj.Add(new string[] { "Xa1No", "幣    別：新臺幣" });

            if (mode == RptMode.Print)
                rp.Print();
            else if (mode == RptMode.PreView)
                rp.PreView();
            else if (mode == RptMode.Mail)
                rp.Mail("日期別已收帳款(總)");
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Mail);
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
