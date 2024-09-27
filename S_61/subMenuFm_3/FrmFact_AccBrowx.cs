using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmFact_AccBrowx : Formbase
    {
        public DataTable dt = new DataTable();
        public string DateRange = "";
        public string spname = "";
        RPT rp;

        public FrmFact_AccBrowx()
        {
            InitializeComponent();
        }

        private void FrmFact_AccBrowx_Load(object sender, EventArgs e)
        {
            radioT1.Checked = radioT6.Checked = true;
            radioT3.SetUserDefineRpt("廠商別應付帳款幣別總額表_自定一.rpt");
            radioT4.SetUserDefineRpt("廠商別應付帳款幣別總額表_自定二.rpt");
            radioT5.SetUserDefineRpt("廠商別應付帳款幣別總額表_標籤自定一.rpt");

            this.交易筆數.DefaultCellStyle.Format = "f0";
            this.前期帳款.DefaultCellStyle.Format = "f" + Common.MFT;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPF;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.TF;
            this.應付總計.DefaultCellStyle.Format = "f" + Common.MFT;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.已付加預付.DefaultCellStyle.Format = "f" + Common.MFT;
            this.本期應付.DefaultCellStyle.Format = "f" + Common.MFT;
            this.前期加本期.DefaultCellStyle.Format = "f" + Common.MFT;
             
            dataGridViewT1.DataSource = dt.AsEnumerable().OrderBy(r => r["fano"].ToString().Trim()).CopyToDataTable();
        }

        void paramsInit()
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\廠商別應付帳款_幣別總額表_稅前報表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\廠商別應付帳款_幣別總額表_稅後報表.rpt";
            }

            string txtend = "";
            if (radioT4.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            rp = new RPT(dt, path);
            rp.office = "廠商別應付帳款";
            if (spname.Trim().Length > 0)
            {
                rp.lobj.Add(new string[] { "txtstart", spname });
            }
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
                textBoxT1.Text = dataGridViewT1["廠商編號", i].Value.ToString().Trim();
                textBoxT2.Text = dataGridViewT1["廠商簡稱", i].Value.ToString().Trim();
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
