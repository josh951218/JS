using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S4
{
    public partial class FrmPOSSaleb2 : Formbase
    {
        public DataTable dt = new DataTable();
        List<String> group;

        public FrmPOSSaleb2()
        {
            InitializeComponent();

            this.銷貨金額.Set銷貨單據小數();
            this.現金金額.Set銷貨單據小數();
            this.門票折抵.Set銷貨單據小數();
            this.刷卡金額.Set銷貨單據小數();
            this.未收金額.Set銷貨單據小數();
        }

        private void FrmPOSSaleb2_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt.DefaultView;

            group = dt.AsEnumerable().GroupBy(r => r["sadate"].ToString().Trim()).Select(g => g.Key).OrderBy(g => g).ToList();
            dt.DefaultView.RowFilter = "sadate = '" + group.First() + "'";
            textBoxT1.Text = group.First();
            writeToText();
        }

        void writeToText()
        {
            var temp = dt.DefaultView.ToTable();
            textBoxT2.Text = temp.AsEnumerable().Sum(r => r["totmny"].ToDecimal()).ToString("f" + Common.MST);
            textBoxT3.Text = temp.AsEnumerable().Sum(r => r["cashmny"].ToDecimal()).ToString("f" + Common.MST);
            textBoxT4.Text = temp.AsEnumerable().Sum(r => r["cardmny"].ToDecimal()).ToString("f" + Common.MST);
            textBoxT5.Text = temp.AsEnumerable().Sum(r => r["ticket"].ToDecimal()).ToString("f" + Common.MST);
            temp.Dispose();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = "sadate = '" + group.First() + "'";
            textBoxT1.Text = group.First();
            writeToText();
        }

        private void btnPreVior_Click(object sender, EventArgs e)
        {
            var index = group.IndexOf(textBoxT1.Text.Trim()) - 1;

            if (index < 0) index = 0;
            dt.DefaultView.RowFilter = "sadate = '" + group.ElementAt(index) + "'";
            textBoxT1.Text = group.ElementAt(index);
            writeToText();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = group.IndexOf(textBoxT1.Text.Trim()) + 1;

            if (index > group.Count() - 1) index = group.Count() - 1;
            dt.DefaultView.RowFilter = "sadate = '" + group.ElementAt(index) + "'";
            textBoxT1.Text = group.ElementAt(index);
            writeToText();
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = "sadate = '" + group.Last() + "'";
            textBoxT1.Text = group.Last();
            writeToText();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        void OutReport(RptMode mode)
        {
            RPT rp;
            rp = new RPT(dt, Common.reportaddress + "Report\\前台日營運報表_明細表.rpt");
            if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
            else if (mode == RptMode.Word) rp.Word();
            else if (mode == RptMode.Excel) rp.Excel();
        }
    }
}
