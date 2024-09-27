using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmFord_Payabldb : Formbase
    {
        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();
        IEnumerable<IGrouping<string, DataRow>> GroupbyDate = null;
        IGrouping<string, DataRow> g = null;
        public string DateRange = "";
         
        public FrmFord_Payabldb()
        {
            InitializeComponent();

            this.折讓金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.現金金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.刷卡金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.禮卷金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.支票金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.匯出金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.其它金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.取用預付.DefaultCellStyle.Format = "f" + Common.MST;
            this.沖帳合計.DefaultCellStyle.Format = "f" + Common.MST;
            this.累入預付.DefaultCellStyle.Format = "f" + Common.MST;
            this.沖抵帳款.DefaultCellStyle.Format = "f" + Common.MST;
        }

        private void FrmFord_Payabldb_Load(object sender, EventArgs e)
        { 
            radioT1.Checked = radioT4.Checked = true;

            this.付款日期.DataPropertyName = Common.User_DateTime == 1 ? "padate" : "padate1";

            radioT2.SetUserDefineRpt("採購員已付帳款自定一.rpt");
            radioT3.SetUserDefineRpt("採購員已付帳款自定二.rpt");
           
            GroupbyDate = from r in dt.AsEnumerable()
                          group r by r["EmNo"].ToString();

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
                dataGridViewT1.DataSource = temp;

                var p = temp.AsEnumerable();
                textBoxT1.Text = grow.Key;
                textBoxT2.Text = temp.Rows[0]["emname"].ToString();
                textBoxT3.Text = temp.Rows.Count.ToString();
                textBoxT4.Text = p.Sum(r => r["TotDisc"].ToDecimal()).ToString("f" + Common.MST);

                textBoxT5.Text = p.Sum(r => r["CashMny"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT6.Text = p.Sum(r => r["CardMny"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT7.Text = p.Sum(r => r["Ticket"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT8.Text = p.Sum(r => r["CheckMny"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT9.Text = p.Sum(r => r["RemitMny"].ToDecimal()).ToString("f" + Common.MST);

                textBoxT10.Text = p.Sum(r => r["OtherMny"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT11.Text = p.Sum(r => r["GetPrvAcc"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT12.Text = p.Sum(r => r["TotReve"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT13.Text = p.Sum(r => r["AddPrvAcc"].ToDecimal()).ToString("f" + Common.MST);
                textBoxT14.Text = p.Sum(r => r["TotMny"].ToDecimal()).ToString("f" + Common.MST);
            }
            else temp.Clear();
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

        void OutReport(RptMode mode)
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\採購員已付帳款簡要表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\採購員已付帳款自定一.rpt";
            }
            else if (radioT3.Checked)
            {
                path = Common.reportaddress + "Report\\採購員已付帳款自定二.rpt";
            }

            string txtend = "";
            if (radioT4.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            var rp = new RPT(dt, path);
            rp.office = "採購員已付帳款";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
            rp.lobj.Add(new string[] { "DateCreated", p });
            rp.lobj.Add(new string[] { "Xa1No", "幣    別：新臺幣" });

            if (mode == RptMode.Word)
                rp.Word();
            else if (mode == RptMode.Excel)
                rp.Excel();
            else if (mode == RptMode.PreView)
                rp.PreView();
            else if (mode == RptMode.Print)
                rp.Print();
            else if (mode == RptMode.Mail)
                rp.Mail("採購員已付帳款");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
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

        private void btnMail_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Mail);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                temp.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
