using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Collections.Generic;

namespace S_61.S2
{
    public partial class nFrmSaleDisInfob : Formbase
    {
        public DataSet ds { get; set; }
        public String X5Name { get; set; }

        public nFrmSaleDisInfob()
        {
            InitializeComponent();

            if (ds == null)
                ds = new DataSet();

            ds.Tables.Clear();
            ds.Clear();

            發票日期.DataPropertyName = string.Concat("indate", Common.User_DateTime == 1 ? "" : "1");
            折讓日期.DataPropertyName = string.Concat("didate", Common.User_DateTime == 1 ? "" : "1");

            稅前合計.Set銷貨單據小數();
            營業稅額.Set銷項稅額小數();
            發票總額.Set銷貨單據小數();
        }

        private void FrmSaleDisInfob_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = ds.Tables[0];

            SetRdUdf();
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT2);
            pVar.SetRadioUdf(pnlist, "SaleDis");
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected)
                return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                Clear();
                return;
            }

            Fill(index);
        }

        void Fill(int index)
        {
            textBoxT1.Text = X5Name;
            textBoxT2.Text = ds.Tables[0].Rows[index]["CuNo"].ToString().Trim();
            textBoxT3.Text = ds.Tables[0].Rows[index]["invtaxno"].ToString().Trim();
            textBoxT4.Text = ds.Tables[0].Rows[index]["cono"].ToString().Trim();
            textBoxT5.Text = ds.Tables[0].Rows[index]["invname"].ToString().Trim();
            textBoxT6.Text = ds.Tables[0].Rows[index]["invaddr1"].ToString().Trim();
        }

        void Clear()
        {
            textBoxT1.Clear();
            textBoxT2.Clear();
            textBoxT3.Clear();
            textBoxT4.Clear();
            textBoxT5.Clear();
            textBoxT6.Clear();
        }

        void OutReport(RptMode mode)
        {
            var path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\公司別銷項折讓_簡要表.rpt";
            }
            else
            {
                path = Common.reportaddress + "Report\\公司別銷項折讓_明細表.rpt";
            }

            RPT rp = new RPT(ds, path);
            rp.lobj.Add(new string[] { "製表日期", Date.GetDateTime(Common.User_DateTime, true) });
            rp.lobj.Add(new string[] { "發票模式", X5Name.Trim() });

            var txtstart = "";
            var txtadress = "";
            var txttel = "";

            if (r1.Checked) txtstart = Common.dtstart.Rows[0]["pnname"].ToString();
            else if (r2.Checked) txtstart = Common.dtstart.Rows[1]["pnname"].ToString();
            else if (r3.Checked) txtstart = Common.dtstart.Rows[2]["pnname"].ToString();
            else if (r4.Checked) txtstart = Common.dtstart.Rows[3]["pnname"].ToString();
            else if (r5.Checked) txtstart = Common.dtstart.Rows[4]["pnname"].ToString();

            if (r1.Checked) txtadress = Common.dtstart.Rows[0]["pnaddr"].ToString();
            else if (r2.Checked) txtadress = Common.dtstart.Rows[1]["pnaddr"].ToString();
            else if (r3.Checked) txtadress = Common.dtstart.Rows[2]["pnaddr"].ToString();
            else if (r4.Checked) txtadress = Common.dtstart.Rows[3]["pnaddr"].ToString();
            else if (r5.Checked) txtadress = Common.dtstart.Rows[4]["pnaddr"].ToString();

            if (r1.Checked) txttel = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
            else if (r2.Checked) txttel = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
            else if (r3.Checked) txttel = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
            else if (r4.Checked) txttel = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
            else if (r5.Checked) txttel = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();

            rp.lobj.Add(new string[] { "txtstart", txtstart });
            rp.lobj.Add(new string[] { "txtadress", txtadress });
            rp.lobj.Add(new string[] { "txttel", txttel });

            if (mode == RptMode.Print)
                rp.Print(1);
            else if (mode == RptMode.PreView)
                rp.PreView(1);
            else if (mode == RptMode.Word)
                rp.Word(1);
            else if (mode == RptMode.Excel)
                rp.Excel(1);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ds.Dispose();
            base.OnClosing(e);
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT2);
            pVar.SaveRadioUdf(pnlist, "SaleDis");
        }
    }
}
