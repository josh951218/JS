using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmFordBshopdb : Formbase
    {
        public DataTable Dt=new DataTable();
        DataTable temp = new DataTable();
        IEnumerable<IGrouping<string, DataRow>> GroupByFono = null;
        IGrouping<string, DataRow> g = null;
        RPT rp;

        public FrmFordBshopdb()
        {
            InitializeComponent(); 
        }

        private void FrmFordBshopdb_Load(object sender, EventArgs e)
        {
            radioT1.Checked = true;
             
            this.採購數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.已交量.DefaultCellStyle.Format = "f" + Common.Q;
            this.未交量.DefaultCellStyle.Format = "f" + Common.Q;

            //產品類別轉為文字
            foreach (DataRow i in Dt.Rows)
            {
                string itt = i["ittrait"].ToString().Trim();
                if (itt == "3") i["產品類別"] = "單一商品";
                if (itt == "2") i["產品類別"] = "組裝品";
                if (itt == "1") i["產品類別"] = "組合品";
            }

            Dt = Dt.AsEnumerable().ToList().OrderBy(r=>r["fono"].ToString().Trim()).CopyToDataTable();
            GroupByFono = Dt.AsEnumerable().GroupBy(r => r["fono"].ToString().Trim());
            g = GroupByFono.FirstOrDefault();
            WriteToTitle(g); 
        }

        void WriteToTitle(IGrouping<string, DataRow> grow)
        {
            if (grow.IsNotNull())
            {
                temp = grow.CopyToDataTable();
                var p = temp.AsEnumerable();

                textBoxT1.Text = grow.Key;
                textBoxT2.Text = temp.Rows[0]["xa1no"].ToString();
                if (Common.User_DateTime == 1) textBoxT3.Text = Date.AddLine(temp.Rows[0]["fodate"].ToString().Trim());
                else textBoxT3.Text = Date.AddLine(temp.Rows[0]["fodate1"].ToString().Trim()); ;
                textBoxT4.Text = temp.Rows[0]["xa1name"].ToString().Trim();
                textBoxT5.Text = temp.Rows[0]["fano"].ToString().Trim();
                textBoxT6.Text = temp.Rows[0]["faname1"].ToString().Trim();
                textBoxT7.Text = temp.Rows[0]["xa1par"].ToDecimal().ToString("f4").Trim();

                
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

        void paramsInit()
        {
            string path = "";

            path = Common.reportaddress + "Report\\採購進貨明細_總額表.rpt";

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
            rp.office = "採購進貨明細_總額表";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateCreated", p });
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
                temp.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 













    }    
}
