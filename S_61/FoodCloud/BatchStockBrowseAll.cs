using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;
using System.IO;

namespace S_61.FoodCloud
{
    public partial class BatchStockBrowseAll : Formbase
    {
        public DataTable dt = new DataTable();
        DataTable orderdt = new DataTable();
        List<Button> qury;
        public BatchStockBrowseAll()
        {
            InitializeComponent();
            qury = new List<Button> { orderbyitno, orderbybatchno };
        }

        private void BatchStockBrowseAll_Load(object sender, EventArgs e)
        {
            orderbyitno_Click(null, null);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataintodocument(RptMode rptMode)
        {
            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                //報表參數
                List<string> Paramaters = new List<string>() { "User_DateTime", Common.User_DateTime.ToString() };
                FastReport.Paramaters = Paramaters;
                string 報表名稱 = "";
                if (rd1.Checked)
                {
                    if (orderbyitno.ForeColor == Color.Red)
                    {
                        報表名稱 = "批號現有庫存總表_產品排序";
                    }
                    else
                    {
                        報表名稱 = "批號現有庫存總表_批次排序";
                    }
                }
                else
                {
                    if (orderbyitno.ForeColor == Color.Red)
                    {
                        報表名稱 = "批號現有庫存總表_產品排序_自定一";
                    }
                    else
                    {
                        報表名稱 = "批號現有庫存總表_批次排序_自定一";
                    }
                }

                string ReportPath = Common.reportaddress + "ReportG\\" + 報表名稱 + ".frx";

                if (File.Exists(ReportPath) == false)
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //三行註腳
                string txtend = "";
                if (rd6.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (rd7.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (rd8.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (rd9.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (rd10.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                FastReport.dy.Add("txtend", txtend);
                FastReport.dy.Add("txtstart", Common.Sys_StcPnName);
                FastReport.PreView(ReportPath, orderdt, "Batch_", null, "", rptMode, 報表名稱);
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

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Excel);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    return;
                dataintodocument(RptMode.Design);
            }
        }

        private void orderbyitno_Click(object sender, EventArgs e)
        {
            
            orderdt.Clear();
            orderdt = dt.Clone();
            foreach (DataRow dr in dt.Select("", "itno"))
            {
                orderdt.ImportRow(dr);
            }
            if (Common.User_DateTime == 1)
            {
                for (int i = 0; i < orderdt.Rows.Count; i++)
                {
                    orderdt.Rows[i]["Date"] = orderdt.Rows[i]["Date"].ToString().Substring(0, 3) + "/" + orderdt.Rows[i]["Date"].ToString().Substring(3).Insert(2, "/");
                    orderdt.Rows[i]["Date1"] = orderdt.Rows[i]["Date1"].ToString().Substring(0, 3) + "/" + orderdt.Rows[i]["Date1"].ToString().Substring(3).Insert(2, "/");
                }
            }
            if (Common.User_DateTime == 2)
            {
                for (int i = 0; i < orderdt.Rows.Count; i++)
                {
                    orderdt.Rows[i]["Date"] = orderdt.Rows[i]["Date"].ToString().Substring(0, 3).ToInteger() + 1911 + "/" + orderdt.Rows[i]["Date"].ToString().Substring(3).Insert(2, "/");
                    orderdt.Rows[i]["Date1"] = orderdt.Rows[i]["Date1"].ToString().Substring(0, 3).ToInteger() + 1911 + "/" + orderdt.Rows[i]["Date1"].ToString().Substring(3).Insert(2, "/");
                }
            }
            dataGridViewT1.DataSource = orderdt;
            SetButtonColor();
            orderbyitno.ForeColor = Color.Red;
            SearchUserReport();
        }

        private void orderbybatchno_Click(object sender, EventArgs e)
        {
            
            orderdt.Clear();
            orderdt = dt.Clone();
            foreach (DataRow dr in dt.Select("", "batchno"))
            {
                orderdt.ImportRow(dr);
            }
            if (Common.User_DateTime == 1)
            {
                for (int i = 0; i < orderdt.Rows.Count; i++)
                {
                    orderdt.Rows[i]["Date"] = orderdt.Rows[i]["Date"].ToString().Substring(0, 3) + "/" + orderdt.Rows[i]["Date"].ToString().Substring(3).Insert(2, "/");
                    orderdt.Rows[i]["Date1"] = orderdt.Rows[i]["Date1"].ToString().Substring(0, 3) + "/" + orderdt.Rows[i]["Date1"].ToString().Substring(3).Insert(2, "/");
                }
            }
            if (Common.User_DateTime == 2)
            {
                for (int i = 0; i < orderdt.Rows.Count; i++)
                {
                    orderdt.Rows[i]["Date"] = orderdt.Rows[i]["Date"].ToString().Substring(0, 3).ToInteger() + 1911 + "/" + orderdt.Rows[i]["Date"].ToString().Substring(3).Insert(2, "/");
                    orderdt.Rows[i]["Date1"] = orderdt.Rows[i]["Date1"].ToString().Substring(0, 3).ToInteger() + 1911 + "/" + orderdt.Rows[i]["Date1"].ToString().Substring(3).Insert(2, "/");
                }
            }
            dataGridViewT1.DataSource = orderdt;
            SetButtonColor();
            orderbybatchno.ForeColor = Color.Red;
            SearchUserReport();
        }

        void SearchUserReport()
        {
            //rd1.Checked = true;

            if (orderbyitno.ForeColor == Color.Red)
                radioT1.SetUserDefineRpt("批號現有庫存總表_產品排序_自定一.frx",@"ReportG\");
            if (orderbybatchno.ForeColor == Color.Red)
                radioT1.SetUserDefineRpt("批號現有庫存總表_產品排序_自定一.frx",@"ReportG\");
        }

        void SetButtonColor()
        {
            qury.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }
    }
}
