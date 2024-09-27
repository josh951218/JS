using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S4
{
    public partial class FrmPOSSaleb1 : Formbase
    {
        public DataTable dt = new DataTable();
        public DataTable dtD = new DataTable();

        public FrmPOSSaleb1()
        {
            InitializeComponent();

            this.現金金額.Set銷貨單據小數();
            this.門票折抵.Set銷貨單據小數();
            this.刷卡金額.Set銷貨單據小數();
            this.未收金額.Set銷貨單據小數();
            this.已收金額.Set銷貨單據小數();
            this.銷貨總金額.Set銷貨單據小數();


            this.銷貨數量.Set庫存數量小數();
            this.銷售金額.Set銷項金額小數();
            this.數量比.DefaultCellStyle.Format = "F2";
            this.金額比.DefaultCellStyle.Format = "F2";
        }

        private void FrmPOSSaleb1_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt.DefaultView;
            dataGridViewT2.DataSource = dtD.DefaultView;
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dt.Rows.Count == 0) return;
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dtD.DefaultView.RowFilter = "1=0";
                return;
            }
            else
            {
                var day = dt.DefaultView[index]["sadate"].ToString().Trim();
                dtD.DefaultView.RowFilter = "sadate = '" + day + "'";
            }
        }

        void OutResport(RptMode mode)
        {
            RPT rp;
            rp = new RPT(dt, Common.reportaddress + "Report\\前台日營運報表_簡要表.rpt");
            if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
            else if (mode == RptMode.Word) rp.Word();
            else if (mode == RptMode.Excel) rp.Excel();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutResport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutResport(RptMode.PreView);
        }

        private void btnWorld_Click(object sender, EventArgs e)
        {
            OutResport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutResport(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmPOSSaleb1_Shown(object sender, EventArgs e)
        {
            btnPreView.Focus();
        }


    }
}
