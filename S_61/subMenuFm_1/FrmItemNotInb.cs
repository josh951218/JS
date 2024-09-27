using System;
using System.Data;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.subMenuFm_1
{
    public partial class FrmItemNotInb : Formbase
    {
        public DataTable Dt = new DataTable();
        public string DateRange = "";

        RPT rp;
        string txtmiddle = "";

        public FrmItemNotInb()
        {
            InitializeComponent();

            this.進價.Visible = this.稅前進價.Visible = this.稅前金額.Visible = Common.User_ShopPrice;
        }

        private void FrmItemNotInb_Load(object sender, EventArgs e)
        {
            radioT10.Enabled = false;
             
            this.採購數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.未交量.DefaultCellStyle.Format = "f" + Common.Q;
            this.折數.DefaultCellStyle.Format = "f3";
            this.進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.稅前進價.DefaultCellStyle.Format = "f6";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.MFT;
            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }

            //依客戶編號交貨日期排序
            Dt = Dt.AsEnumerable().OrderBy(r => r["esdate"].ToString().Trim()).CopyToDataTable();
            Dt = Dt.AsEnumerable().OrderBy(r => r["fano"].ToString().Trim()).CopyToDataTable();

            WriteToTitle();
        }

        void WriteToTitle()
        {
            textBoxT1.Text = Dt.Rows[0]["itno"].ToString();
            textBoxT2.Text = Dt.Rows[0]["fano"].ToString();
            dataGridViewT1.DataSource = Dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void paramsInit()
        {
            string path = "";
            string HaveO = "";

            if (radioT7.Checked) HaveO = "O";

            if(Common.Sys_DBqty == 1)
                path = Common.reportaddress + "Report\\產品別未到貨明細"+ HaveO + ".rpt";
            else if (Common.Sys_DBqty == 2)
                path = Common.reportaddress + "Report\\產品別未到貨明細" + HaveO + "P.rpt";

            string txtend = "";
            if (radioT1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            rp = new RPT(Dt, path);
            rp.office = txtmiddle + "產品別未到貨明細";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtmiddle", "產品別-未到貨明細" });
            rp.lobj.Add(new string[] { "DateCreated", p });
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

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            textBoxT1.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
            textBoxT2.Text = dataGridViewT1.SelectedRows[0].Cells["廠商編號"].Value.ToString();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                Dt.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

    }
}
