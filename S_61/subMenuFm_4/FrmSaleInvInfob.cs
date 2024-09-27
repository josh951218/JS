using System;
using System.Data;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.subMenuFm_4
{
    public partial class FrmSaleInvInfob : Formbase
    {
        public DataTable printtb = new DataTable();
        public DataTable printtbD = new DataTable();
    
        string ReportPath = "";
        RPT rp;
        public FrmSaleInvInfob()
        {
            InitializeComponent();
            Date1.SetDateLength();
            Date2.SetDateLength();

            this.銷貨日期.DataPropertyName = Common.User_DateTime == 1 ? "sadate" : "sadate1";
            this.發票日.DataPropertyName = Common.User_DateTime == 1 ? "invdate" : "invdate1";
            if (Common.Sys_StockKind == 1) this.作廢.Visible = false;
        }

        private void FrmSaleInvInfob_Load(object sender, EventArgs e)
        {
            this.銷售合計.DefaultCellStyle.Format = "f0";
            this.營業稅.DefaultCellStyle.Format = "f0";
            this.總計.DefaultCellStyle.Format = "f0";

            //ReportFileName = "銷項發票瀏覽明細表";
            rd2.SetUserDefineRpt("銷項發票瀏覽明細表_自定一.rpt");
            rd3.SetUserDefineRpt("銷項發票瀏覽明細表_自定二.rpt");
            dataGridViewT1.DataSource = printtb;
        }

        void paramsInit()
        {
            ReportPath = Common.reportaddress + "Report\\" + "銷項發票瀏覽明細表";
            if (rd1.Checked)
            {
                ReportPath += "_標準報表.rpt";
            }
            else if (rd2.Checked)
            {
                ReportPath += "_自定一.rpt";
            }
            else if (rd3.Checked)
            {
                ReportPath += "_自定二.rpt";
            }

            string txtend = "";
            if (rdFooter1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (rdFooter2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (rdFooter3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (rdFooter4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (rdFooter5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";
            
            rp = new RPT(printtbD, ReportPath);
            rp.lobj.Add(new string[] { "DateRange", "發票日期:"+Date1.Text + " ~ " + Date2.Text });
            rp.txtstart = Common.Sys_StcPnName;
            rp.txtend = txtend;
            //rp.txtadress = txtadress;
            //rp.txttel = txttel;
            rp.office = "銷項發票瀏覽明細表";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Excel();
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
                printtb.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
