using System;
using System.Data;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.subMenuFm_4
{
    public partial class FrmSaleInvInfoe : Formbase
    {
        public DataTable printtb = new DataTable();
        public DataTable printtbD = new DataTable();
        //string ReportFileName = "";
        string ReportPath = "";
        RPT rp;

        public FrmSaleInvInfoe()
        {
            InitializeComponent();
            Date1.MaxLength = Date2.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
           
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.M;
        }

        private void FrmSaleInvInfoe_Load(object sender, EventArgs e)
        { 
            //ReportFileName = "進項發票瀏覽彙總表";

            rd2.SetUserDefineRpt("進項發票瀏覽彙總表_自定一.rpt");
            rd3.SetUserDefineRpt("進項發票瀏覽彙總表_自定二.rpt");
             
            dataGridViewT1.DataSource = printtbD;
        }

        void paramsInit()
        {
            ReportPath = Common.reportaddress + "Report\\" + "進項發票瀏覽彙總表";
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
            rp.lobj.Add(new string[] { "InvRange", "起訖發票:" + InvNo1.Text + " ~ " + InvNo2.Text });
            rp.lobj.Add(new string[] { "DateRange", "發票日期:" + Date1.Text + " ~ " + Date2.Text });
            rp.txtstart = Common.Sys_StcPnName;
            rp.txtend = txtend;
            rp.office = "進項發票瀏覽彙總表";
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
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                printtbD.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
