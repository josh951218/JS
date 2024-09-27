using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmEmplNotInb : Formbase
    {
        public DataTable Dt = new DataTable();
        public string DateRange = "";

        RPT rp;
        string NO = "";
        string txtmiddle = "";

        public FrmEmplNotInb()
        {
            InitializeComponent();

            this.進價.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
        }

        private void FrmEmplNotInb_Load(object sender, EventArgs e)
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
            Dt = Dt.AsEnumerable().OrderBy(r => r["emno"].ToString().Trim()).CopyToDataTable();

            WriteToTitle();
            btnBrowT1.PerformClick();
        }

        void WriteToTitle()
        {
            textBoxT1.Text = Dt.Rows[0]["emno"].ToString();
            textBoxT2.Text = Dt.Rows[0]["fano"].ToString();
            textBoxT3.Text = Dt.Rows[0]["fano"].ToString();
            dataGridViewT1.DataSource = Dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            txtmiddle = "1";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            Dt.DefaultView.Sort = "emno,esdate";
            SetButtonColor();
            btnBrowT1.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            txtmiddle = "2";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            Dt.DefaultView.Sort = "emno,itno,esdate";
            SetButtonColor();
            btnBrowT2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        void SetButtonColor()
        {
            btnBrowT1.ForeColor = btnBrowT2.ForeColor = SystemColors.ControlText;
        }

        void SetSelectRow(string NO)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == NO)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }
        }

        void paramsInit()
        {
            string path = "";
            string HaveO = "";

            if (radioT7.Checked) HaveO = "O";

            if(Common.Sys_DBqty == 1)
                path = Common.reportaddress + "Report\\業務別未到貨明細" + txtmiddle + HaveO + ".rpt";
            else if (Common.Sys_DBqty == 2)
                path = Common.reportaddress + "Report\\業務別未到貨明細" + txtmiddle + HaveO + "P.rpt";

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
            rp.office = txtmiddle + "業務別未到貨明細";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtmiddle", "業務別-未到貨明細" });
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
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                Dt.Excel匯出並開啟(this.Text);
            }
            switch (keyData)
            {
                case Keys.F2:
                    btnBrowT1.Focus();
                    btnBrowT1.PerformClick();
                    break;
                case Keys.F3:
                    btnBrowT2.Focus();
                    btnBrowT2.PerformClick();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            textBoxT1.Text = dataGridViewT1.SelectedRows[0].Cells["業務編號"].Value.ToString();
            textBoxT2.Text = dataGridViewT1.SelectedRows[0].Cells["廠商編號"].Value.ToString();
            textBoxT2.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
        }
    }
}
