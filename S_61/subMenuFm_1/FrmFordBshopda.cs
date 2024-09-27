using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmFordBshopda : Formbase
    {
        public DataTable Dt = new DataTable();
        DataTable temp = new DataTable();
        IEnumerable<IGrouping<string, DataRow>> GroupByFono = null;
        IGrouping<string, DataRow> g = null;
        RPT rp;
        string NO = "";
        string txtmiddle = "";

        public FrmFordBshopda()
        {
            InitializeComponent();

            this.進價.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
        }

        private void FrmFordBshopda_Load(object sender, EventArgs e)
        { 
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前進價.DefaultCellStyle.Format = "f6";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }

            //產品類別轉為文字
            foreach (DataRow i in Dt.Rows)
            {
                string itt = i["ittrait"].ToString().Trim();
                if (itt == "3") i["產品類別"] = "單一商品";
                if (itt == "2") i["產品類別"] = "組裝品";
                if (itt == "1") i["產品類別"] = "組合品";
            }

            Dt = Dt.AsEnumerable().ToList().OrderBy(r => r["fono"].ToString().Trim()).CopyToDataTable();
            GroupByFono = Dt.AsEnumerable().GroupBy(r => r["fono"].ToString().Trim());
            g = GroupByFono.FirstOrDefault();
            WriteToTitle(g);

            btnBrowT1.PerformClick();
        }

        void WriteToTitle(IGrouping<string, DataRow> grow)
        {
            if (grow.IsNotNull())
            {
                temp = grow.CopyToDataTable();
                var p = temp.AsEnumerable();

                textBoxT1.Text = grow.Key;
                textBoxT2.Text = temp.Rows[0]["xa1no"].ToString();               
                textBoxT3.Text = temp.Rows[0]["fano"].ToString().Trim();
                textBoxT4.Text = temp.Rows[0]["faname1"].ToString().Trim();
                textBoxT5.Text = temp.Rows[0]["xa1name"].ToString().Trim();
                textBoxT6.Text = temp.Rows[0]["itno"].ToString().Trim();
                textBoxT7.Text = temp.Rows[0]["xa1par"].ToDecimal().ToString("f4").Trim();

                //加入序號
                int num=0;
                foreach (DataRow i in temp.Rows)
                {
                    i["序號"] = num.ToString();                   
                    num++;
                }

                dataGridViewT1.DataSource = temp;
            }
            else
            {
                foreach (var item in this.Controls.OfType<TextBox>())
                {
                    item.Clear();
                }
                temp.Clear();
                dataGridViewT1.DataSource = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            g = GroupByFono.FirstOrDefault();
            WriteToTitle(g);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = GroupByFono.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == 0)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupByFono.ToList()[--index];
                WriteToTitle(g);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = GroupByFono.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == GroupByFono.Count() - 1)
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupByFono.ToList()[++index];
                WriteToTitle(g);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            g = GroupByFono.LastOrDefault();
            WriteToTitle(g);
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            txtmiddle = "採購產品別";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            temp.DefaultView.Sort = "itno,itunit,bsdate,bsno";
            SetButtonColor();
            btnBrowT1.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            txtmiddle = "採購日期別";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            temp.DefaultView.Sort = "bsdate,bsno";
            SetButtonColor();
            btnBrowT2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        void SetButtonColor(){
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
                path = Common.reportaddress + "Report\\" + txtmiddle + HaveO +"_進貨明細表.rpt";
            else if (Common.Sys_DBqty == 2)
                path = Common.reportaddress + "Report\\" + txtmiddle + HaveO + "_進貨明細表P.rpt";

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
            rp.office = txtmiddle+"進貨明細表";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateCreated", p });
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
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
            textBoxT6.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();

        }
         



















    }
}
