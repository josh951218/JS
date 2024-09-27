using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.S0
{
    public partial class Frmswipe_statisticsB : JE.MyControl.Formbase
    {
        public DataTable temp = new DataTable();
        public string DateRange = "";
        public Frmswipe_statisticsB()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            temp = temp.AsEnumerable().OrderBy(r => r["sano"].ToString().Trim()).CopyToDataTable();
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                temp.Rows[i]["cardmny"] = temp.Rows[i]["cardmny"].ToDecimal("f" + Common.MST);
            }
            dataGridViewT1.DataSource = temp;
        }

        void OutReport(RptMode mode)
        {
            var path = Common.reportaddress + @"ReportF\機台刷卡統計表.frx";
            using (var fs = new JBS.FReport())
            {
                fs.dy.Add("today", Date.AddLine(Date.GetDateTime(Common.User_DateTime)));
                fs.dy.Add("DateRange", DateRange);
                fs.dy.Add("銷項金額小數", Common.TPS);
                fs.dy.Add("銷貨單據小數", Common.MST);
                fs.OutReport(mode, temp, "Table", path);
            }
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    OutReport(RptMode.Design);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        } 
    }
}
