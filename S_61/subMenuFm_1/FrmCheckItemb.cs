using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmCheckItemb : Formbase
    {
        public DataTable CuDt = new DataTable();
        public DataTable ItDt = new DataTable();
        public string DateRange = "";
        string NO = "";
        string select = "";

        public FrmCheckItemb()
        {
            InitializeComponent();
            rpt3.判斷有無CF或RF("訂單撿貨報表_自定一",@"ReportF\");
            select = "F2";
        }

        private void FrmCheckItemb_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = CuDt.DefaultView;
            this.訂單數量.Set庫存數量小數();
            CuDt.TableName = "cudt";
            ItDt.TableName = "itdt";
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            btnBrowT1.Focus();
            select = "F2";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            CuDt.DefaultView.Sort = "orno";
            btnBrowT1.ForeColor = btnBrowT2.ForeColor = System.Drawing.SystemColors.ControlText;
            btnBrowT1.ForeColor = System.Drawing.Color.Red;
            SetSelectRow(NO);
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            btnBrowT2.Focus();
            select = "F3";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            CuDt.DefaultView.Sort = "itno";
            btnBrowT1.ForeColor = btnBrowT2.ForeColor = System.Drawing.SystemColors.ControlText;
            btnBrowT2.ForeColor = System.Drawing.Color.Red;
            SetSelectRow(NO);
        }

        void SetSelectRow(string NO)
        {
            int i =  CuDt.DefaultView.FindIndex("bomid=" + NO);
            dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
            dataGridViewT1.Rows[i].Selected = true;
            dataGridViewT1.Focus();
        }


        void dataintodocument(RptMode mode)
        {
            string path = Common.reportaddress;
            if (rpt1.Checked)
                path += "ReportF\\訂單撿貨報表"+select+"_內定報表.frx";
            else if (rpt2.Checked)
                path += "ReportF\\訂單撿貨報表_標籤.frx";
            else
                path += "ReportF\\訂單撿貨報表_自定一.frx";

            if (System.IO.File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DataTable printtb;
            if (!rpt2.Checked)
            {
                if (select.Equals("F3"))
                    printtb = ItDt;

                else
                    printtb = CuDt;
            }
            else
                printtb = CuDt;


            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                string txtend = "";
                if (radioT1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (radioT2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (radioT3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (radioT4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (radioT5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                FastReport.dy.Add("txtend", txtend);
                FastReport.dy.Add("txtstart", Common.Sys_StcPnName);

                FastReport.PreView(path, printtb, printtb.TableName, null, null, mode, path);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.PreView);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string path = Common.reportaddress;
                if (rpt1.Checked)
                    path += "ReportF\\訂單撿貨報表" + select + "_內定報表.frx";
                else if (rpt2.Checked)
                    path += "ReportF\\訂單撿貨報表_標籤.frx";
                else
                    path += "ReportF\\訂單撿貨報表_自定一.frx";

                var dl = MessageBox.Show("是否要修改報表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl != DialogResult.Yes) return;

                JBS.FReport.Design(path);
            }
        }
    }
}
