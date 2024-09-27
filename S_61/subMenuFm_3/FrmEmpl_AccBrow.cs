using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmEmpl_AccBrow : Formbase
    {
        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();
        IEnumerable<IGrouping<string, DataRow>> GroupbyDate = null;
        IGrouping<string, DataRow> g = null;
        RPT rp;
        public string DateRange = "";
        public string spname = "";

        public FrmEmpl_AccBrow()
        {
            InitializeComponent();

            this.前期帳款.DefaultCellStyle.Format = "f" + Common.M;
            this.交易筆數.DefaultCellStyle.Format = "f0";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.M;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.M;
            this.應付總計.DefaultCellStyle.Format = "f" + Common.M;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.M;
            this.已付加預付.DefaultCellStyle.Format = "f" + Common.M;
            this.本期應付.DefaultCellStyle.Format = "f" + Common.M;
            this.前期加本期.DefaultCellStyle.Format = "f" + Common.M;
        }

        private void FrmEmpl_AccBrow_Load(object sender, EventArgs e)
        {
            radioT1.Checked = true;
            radioT7.Checked = true;
             
            radioT3.SetUserDefineRpt("採購員_應付帳款總額表自定一.rpt");

            dt = dt.AsEnumerable().OrderBy(r => r["emno"].ToString().Trim()).CopyToDataTable();
            GroupbyDate = dt.AsEnumerable().GroupBy(r => r["emno"].ToString().Trim());   
            g = GroupbyDate.FirstOrDefault();
            WriteToTitle(g);        
        }

        void WriteToTitle(IGrouping<string, DataRow> grow)
        {
            if (grow.IsNotNull())
            {
                temp = grow.CopyToDataTable();
                var p = temp.AsEnumerable();

                textBoxT10.Text = grow.Key;
                textBoxT11.Text = temp.Rows[0]["emname"].ToString();
                textBoxT1.Text = p.Sum(r => r["前期總金額"].ToDecimal()).ToString("f" + Common.M);

                textBoxT2.Text = p.Sum(r => r["交易總筆數"].ToDecimal()).ToString("f0");
                textBoxT3.Text = p.Sum(r => r["稅前總金額"].ToDecimal()).ToString("f" + Common.M);
                textBoxT4.Text = p.Sum(r => r["營業稅總額"].ToDecimal()).ToString("f" + Common.M);
                textBoxT5.Text = p.Sum(r => r["應付總金額"].ToDecimal()).ToString("f" + Common.M);

                textBoxT6.Text = p.Sum(r => r["折扣總金額"].ToDecimal()).ToString("f" + Common.M);
                textBoxT7.Text = p.Sum(r => r["已付加預付"].ToDecimal()).ToString("f" + Common.M);
                textBoxT8.Text = p.Sum(r => r["本期總金額"].ToDecimal()).ToString("f" + Common.M);
                textBoxT9.Text = p.Sum(r => r["前期加本期"].ToDecimal()).ToString("f" + Common.M);

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
            g = GroupbyDate.FirstOrDefault();
            WriteToTitle(g);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == 0)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[--index];
                WriteToTitle(g);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == GroupbyDate.Count() - 1)
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[++index];
                WriteToTitle(g);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            g = GroupbyDate.LastOrDefault();
            WriteToTitle(g);
        }

        void paramsInit()
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\採購員應付帳款_本幣總額表_稅前報表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\採購員應付帳款_本幣總額表_稅後報表.rpt";
            }
            else if (radioT3.Checked)
            {
                path = Common.reportaddress + "Report\\採購員應付帳款_本幣總額表_自定一.rpt";
            }

            string txtend = "";
            if (radioT7.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT11.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            rp = new RPT(dt, path);
            rp.office = "採購員應付帳款總額表";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
            rp.lobj.Add(new string[] { "Xa1No", "幣    別：本幣" });
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
