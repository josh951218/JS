using System;
using System.Data;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.S5
{
    public partial class FrmStockInventory1 : Formbase
    {
        public DataTable dt = new DataTable();

        public string 明細區_頭value = "", 明細區_尾value = "", 明細區_欄位對應value = "";

        public FrmStockInventory1()
        {
            InitializeComponent();
            this.庫存倉數量.Set庫存數量小數();
            this.借出倉數量.Set庫存數量小數();
            this.加工倉數量.Set庫存數量小數();
            this.借入倉數量.Set庫存數量小數();
            this.庫存總數量.Set庫存數量小數();

            radioT2.SetUserDefineRpt("產品現有庫存表_現有總表_自定一.rpt");
            radioT3.SetUserDefineRpt("產品現有庫存表_現有總表_自定二.rpt");
        }

        private void FrmStockInventory1_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt;
        }

        void OutReport(RptMode mode)
        {
            var path = Common.reportaddress + "Report\\";
            if (radioT1.Checked) path += "產品現有庫存表_現有總表_標準報表.rpt";

            RPT rp = new RPT(dt, path);

            var txtend = "";
            if (radioT4.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            rp.lobj.Add(new string[] { "CreateDate", Date.GetDateTime(Common.User_DateTime, true) });
            rp.lobj.Add(new string[] { "txtend", txtend });

            if (radioT1.Checked  && mode == RptMode.Excel)
            {

                DialogResult dialogResult = MessageBox.Show("是否需要解析Excel檔案?", "解析Excel視窗", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    rp.Excel("", null, true, 明細區_頭value, 明細區_尾value, 明細區_欄位對應value, true, true);
                    return;
                }
                else if (dialogResult == DialogResult.No)
                {
                    //nothing
                }
            }


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
