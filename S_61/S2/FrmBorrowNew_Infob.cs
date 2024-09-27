using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;


namespace S_61.S2
{
    public partial class FrmBorrowNew_Infob : Formbase
    {
        public string DateRange = "";
        public DataTable dtD = new DataTable();

        string sort = "F2";
        decimal No = 0;

        public FrmBorrowNew_Infob()
        {
            InitializeComponent();
            this.單據日期.DataPropertyName = "單據日期";
            radioT2.SetUserDefineRpt("借入還出資料瀏覽_自定一");

            this.進價.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
            this.本幣單價.Visible = Common.User_ShopPrice;
            this.本幣稅前金額.Visible = Common.User_ShopPrice;
            this.本幣稅前單價.Visible = Common.User_ShopPrice;
            textBoxT7.Visible = Common.User_ShopPrice;
            textBoxT8.Visible = Common.User_ShopPrice;
            textBoxT9.Visible = Common.User_ShopPrice;
        }

        private void FrmBorrowNew_Infob_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dtD.DefaultView;

            sort = "F2";
            ChangColor(F2, "BoNo ASC");

            textBoxT4.Text = dtD.AsEnumerable().Count(r => r["單據"].ToString().Trim() == "借入").ToString();
            textBoxT5.Text = dtD.AsEnumerable().Count(r => r["單據"].ToString().Trim() == "還出").ToString();
            textBoxT6.Text = dtD.Rows.Count.ToString();

            var p1 = dtD.AsEnumerable().Where(r => r["單據"].ToString().Trim() == "借入").Sum(r => r["mny"].ToDecimal());
            var p2 = dtD.AsEnumerable().Where(r => r["單據"].ToString().Trim() == "還出").Sum(r => r["mny"].ToDecimal());
            textBoxT7.Text = p1.ToString("f" + Common.TPF);
            textBoxT8.Text = p2.ToString("f" + Common.TPF);
            textBoxT9.Text = (p1 + p2).ToString("f" + Common.TPF);
        }

        void OutReport(RptMode mode)
        {
            var path = "";
            if (sort == "F2") path = Common.reportaddress + "Report\\借入還出資料瀏覽F2_簡要表.rpt";
            else if (sort == "F3") path = Common.reportaddress + "Report\\借入還出資料瀏覽F3_簡要表.rpt";
            else if (sort == "F4") path = Common.reportaddress + "Report\\借入還出資料瀏覽F4_簡要表.rpt";
            else if (sort == "F5") path = Common.reportaddress + "Report\\借入還出資料瀏覽F5_簡要表.rpt";

            RPT rp = new RPT(dtD.DefaultView.ToTable(), path);
            
            var txtend = "";
            if (radioT3.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();

            rp.lobj.Add(new string[] { "DateRange", "日期區間: " + DateRange });

            if (mode == RptMode.Print)
            {
                rp.Print();
            }
            else if (mode == RptMode.PreView)
            {
                rp.PreView();
            }
            else if (mode == RptMode.Word)
            {
                rp.Word();
            }
            else if (mode == RptMode.Excel)
            {
                rp.Excel();
            }
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

        private void F2_Click(object sender, EventArgs e)
        {
            sort = "F2";
            ChangColor(sender, "BoNo ASC");
        }

        private void F3_Click(object sender, EventArgs e)
        {
            sort = "F3";
            ChangColor(sender, "FaNo ASC,Bodate DESC");
        }

        private void F4_Click(object sender, EventArgs e)
        {
            sort = "F4";
            ChangColor(sender, "ItNo ASC,ItUnit ASC,Bodate DESC");
        }

        private void F5_Click(object sender, EventArgs e)
        {
            sort = "F5";
            ChangColor(sender, "EmNo ASC,Bodate DESC");
        }

        void ChangColor(object sender, string sorter)
        {
            F2.ForeColor = Color.Black;
            F3.ForeColor = Color.Black;
            F4.ForeColor = Color.Black;
            F5.ForeColor = Color.Black;
            ((Button)sender).ForeColor = Color.Red;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) No = 1;
            else
            {
                No = dtD.DefaultView[index]["序號"].ToString().ToDecimal();
            }

            dtD.DefaultView.Sort = sorter;
            index = dtD.DefaultView.FindIndex("序號 = " + No);

            if (index == -1) return;
            if (index >= dtD.Rows.Count) index = dtD.Rows.Count - 1;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                textBoxT1.Text = textBoxT2.Text = textBoxT3.Text = "";
                return;
            }
            textBoxT1.Text = dtD.DefaultView[index]["FaNo"].ToString().Trim();
            textBoxT3.Text = dtD.DefaultView[index]["EmNo"].ToString().Trim();
            textBoxT2.Text = dtD.DefaultView[index]["ItNo"].ToString().Trim();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtD.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

    }
}
