using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_AccBrowx : Formbase
    {
        public DataTable dt = new DataTable();
        public string DateRange = "";
        public string spname = "";
        RPT rp;

        public FrmCust_AccBrowx()
        {
            InitializeComponent();
        }

        private void FrmCust_AccBrowx_Load(object sender, EventArgs e)
        {
            radioT1.Checked = radioT6.Checked = true;
            radioT3.SetUserDefineRpt("客戶別應收帳款_幣別總額表_自定一.rpt");
            radioT4.SetUserDefineRpt("客戶別應收帳款_幣別總額表_自定二.rpt");
            radioT5.SetUserDefineRpt("客戶別應收帳款_幣別總額表_標籤自定一.rpt");
             
            dataGridViewT1.DataSource = dt.AsEnumerable().OrderBy(r => r["cuno"].ToString().Trim()).CopyToDataTable();
        }

        void paramsInit()
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_幣別總額表_稅前報表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_幣別總額表_稅後報表.rpt";
            }
            else if (radioT3.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_幣別總額表_自定一.rpt";
            }
            else if (radioT4.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_幣別總額表_自定二.rpt";
            }
            else if (radioT5.Checked)
            {
                path = Common.reportaddress + "Report\\客戶別應收帳款_幣別總額表_標籤自定一.rpt";
            }

            string txtend = "";
            if (radioT6.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            rp = new RPT(dt, path);
            rp.office = "客戶別應收帳款";
            
            //公司抬頭
            if (rdHeader1.Checked){rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[0]["pnname"].ToString() });}
            else if (rdHeader2.Checked){rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[1]["pnname"].ToString() });}
            else if (rdHeader3.Checked){rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[2]["pnname"].ToString() });}
            else if (rdHeader4.Checked){rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[3]["pnname"].ToString() });}
            else if (rdHeader5.Checked){rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[4]["pnname"].ToString() });}
            else { rp.lobj.Add(new string[] { "txtstart", "" }); }

            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Excel();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Word();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (e.StateChanged == DataGridViewElementStates.Selected)
                {
                    WriteToTitle(e.Row.Index);
                }
            }
        }

        void WriteToTitle(int i)
        {
            if (i == -1)
            {
                textBoxT1.Clear();
                textBoxT2.Clear();
                textBoxT3.Clear();
            }
            else
            {
                textBoxT1.Text = dataGridViewT1["客戶編號", i].Value.ToString().Trim();
                textBoxT2.Text = dataGridViewT1["客戶簡稱", i].Value.ToString().Trim();
                textBoxT3.Text = dataGridViewT1["交易筆數", i].Value.ToString().Trim();
            }
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
