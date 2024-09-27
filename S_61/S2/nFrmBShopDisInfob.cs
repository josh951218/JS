using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class nFrmBShopDisInfob : Formbase
    {
        public DataSet ds { get; set; }
        public String X5Name { get; set; }

        public nFrmBShopDisInfob()
        {
            InitializeComponent();

            if (ds == null)
                ds = new DataSet();

            ds.Tables.Clear();
            ds.Clear();

            發票日期.DataPropertyName = string.Concat("indate", Common.User_DateTime == 1 ? "" : "1");
            折讓日期.DataPropertyName = string.Concat("didate", Common.User_DateTime == 1 ? "" : "1");

            稅前合計.Set進貨單據小數();
            營業稅額.Set進項稅額小數();
            發票總額.Set進貨單據小數();
        }

        private void FrmBShopDisInfob_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = ds.Tables[0];
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
            textBoxT2.Text = ds.Tables[0].Rows[index]["FaNo"].ToString().Trim();
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
                path = Common.reportaddress + "Report\\公司別進項折讓_簡要表.rpt";
            }
            else
            {
                path = Common.reportaddress + "Report\\公司別進項折讓_明細表.rpt";
            }

            RPT rp = new RPT(ds, path);
            rp.lobj.Add(new string[] { "製表日期", Date.GetDateTime(Common.User_DateTime, true) });
            rp.lobj.Add(new string[] { "發票模式", X5Name.Trim() });

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
    }
}
