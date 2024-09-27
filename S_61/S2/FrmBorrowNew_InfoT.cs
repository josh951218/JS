using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmBorrowNew_InfoT : Formbase
    {
        public DataTable table = new DataTable();
        
        public string DateRange { get; set; }

        string DateCreated;

        
        List<string> list;

        public FrmBorrowNew_InfoT()
        {
            InitializeComponent();

        }

        private void FrmBorrowNew_InfoT_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = table.DefaultView;
            list = table.AsEnumerable().GroupBy(r => r["fano"].ToString()).Select(g => g.Key).ToList();
            btnTop_Click(null, null);
        }

        void writeToText(string fano)
        {
            table.DefaultView.RowFilter = "FaNo = '" + fano + "'";
            table.DefaultView.Sort = "PRS DESC, PRICE ASC";

            if (dataGridViewT1.Rows.Count == 0) FaNo.Text = FaName1.Text = "";
            else
            {
                FaNo.Text = table.DefaultView[0]["FaNo"].ToString();
                FaName1.Text = table.DefaultView[0]["FaName1"].ToString();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToText(list.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(FaNo.Text.Trim()) - 1;
            if (index <= -1) index = 0;
            writeToText(list.ElementAt(index));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(FaNo.Text.Trim()) + 1;
            if (index >= list.Count - 1) index = list.Count - 1;
            writeToText(list.ElementAt(index));
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToText(list.Last());
        }

        void OutReport(RptMode mode)
        {
            switch (Common.User_DateTime)
            {
                case 1:
                    DateCreated = Date.GetDateTime(1, true);
                    break;
                case 2:
                    DateCreated = Date.GetDateTime(2, true);
                    break;
            }

            RPT rp = new RPT(table, Common.reportaddress + "Report\\借入還出資料瀏覽_總表.rpt");
            rp.lobj.Add(new string[] { "DateRange",DateRange });
            rp.lobj.Add(new string[] { "DateCreated", "製表日期：" + DateCreated });

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
            this.Close();
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                table.Excel匯出並開啟(this.Text);
            }
            else if (keyData == Keys.F11)
            {
                btnExit_Click(null, null);
            }
            else if (keyData==Keys.Escape)
            {
                btnExit_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
