using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S4
{
    public partial class FrmPOSItemPrsb1 : Formbase
    {
        public string CreateDay = "";
        public DataTable dtD = new DataTable();

        List<string> list;

        public FrmPOSItemPrsb1()
        {
            InitializeComponent();
            this.數量.Set庫存數量小數();
            this.單價.Set銷貨單價小數();

            this.折數.DefaultCellStyle.Format = "f3";
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;

            this.稅前單價.DefaultCellStyle.Format = "f6";
            this.稅前單價.FirstNum = 9;
            this.稅前單價.LastNum = 6;

            this.稅前金額.DefaultCellStyle.Format = "f0";
            this.稅前金額.FirstNum = 15;
            this.稅前金額.LastNum = 0;

            this.包裝數量.Set庫存數量小數();
        }

        private void FrmPOSSaleb1_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dtD.DefaultView;
            list = dtD.AsEnumerable().GroupBy(r => r["itno"].ToString()).Select(g => g.Key).ToList();
            btnTop_Click(null, null);
        }

        void writeToText(string itno)
        {
            dtD.DefaultView.RowFilter = "ItNo = '" + itno + "'";
            dtD.DefaultView.Sort = "PRS DESC, PRICE ASC";

            if (dataGridViewT1.Rows.Count == 0) ItNo.Text = ItName.Text = "";
            else
            {
                ItNo.Text = dtD.DefaultView[0]["ItNo"].ToString();
                ItName.Text = dtD.DefaultView[0]["ItName"].ToString();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToText(list.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(ItNo.Text.Trim()) - 1;
            if (index <= -1) index = 0;
            writeToText(list.ElementAt(index));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(ItNo.Text.Trim()) + 1;
            if (index >= list.Count - 1) index = list.Count - 1;
            writeToText(list.ElementAt(index));
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToText(list.Last());
        }

        void OutReport(RptMode mode)
        {
            RPT rp = new RPT(dtD, Common.reportaddress + "Report\\前台產品折數_簡要表.rpt");
            rp.lobj.Add(new string[] { "CreateDay", "統計日期：" + CreateDay });

            if (mode == RptMode.Print)
            {
                rp.Print();
            }
            else if (mode == RptMode.PreView)
            {
                rp.PreView();
            }
            else if (mode == RptMode.Word)
            {
                rp.Word();
            }
            else if (mode == RptMode.Excel)
            {
                rp.Excel();
            }
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

        private void FrmPOSItemPrsb1_Shown(object sender, EventArgs e)
        {
            btnPreView.Focus();
        }
    }
}
