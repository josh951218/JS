using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.S0
{
    public partial class FrmMoneyBoxLog : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;

        public FrmMoneyBoxLog()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            Day.SetDateLength();
            Day1.SetDateLength();
            Day.Text = Date.GetDateTime(Common.User_DateTime);
            Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmMoneyBoxLog_Load(object sender, EventArgs e)
        {

        }

        private void Day_Validating(object sender, CancelEventArgs e)
        {
            xe.DateValidate(sender, e);
        }

        private void ShNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Shift>(sender, row =>
            {
                ShNo.Text = row["ShNo"].ToString().Trim();
                ShName.Text = row["ShName"].ToString().Trim();
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void ShNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Shift>(sender, e, row => ShName.Text = row["ShName"].ToString().Trim(), true);

            if (ShNo.TrimTextLenth() == 0)
                ShName.Clear();
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;
             
            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row => EmName.Text = row["EmName"].ToString().Trim(), true);

            if (EmNo.TrimTextLenth() == 0)
                EmName.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {
            DataTable temp = new DataTable();
            using (var db = new JBS.xSQL())
            {
                var tsql = "Select * from moneyboxlog where opdate >= @opdate and opdate <= @opdate1";
                if (machine.TrimTextLenth() > 0)
                    string.Concat(tsql, " and machine=@machine");
                if (ShNo.TrimTextLenth() > 0)
                    string.Concat(tsql, " and shno=@shno");
                if (EmNo.TrimTextLenth() > 0)
                    string.Concat(tsql, " and emno=@emno");

                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("opdate", Date.ToTWDate(Day.Text));
                    spc.AddWithValue("opdate1", Date.ToTWDate(Day1.Text));
                    spc.AddWithValue("machine", machine.Text.Trim());
                    spc.AddWithValue("shno", ShNo.Text.Trim());
                    spc.AddWithValue("emno", EmNo.Text.Trim());
                }, ref temp);
            }

            if (temp.Rows.Count == 0 && mode != RptMode.Design)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            var path = Common.reportaddress + @"ReportF\錢櫃開啟記錄表.frx";

            using (var fs = new JBS.FReport())
            {
                fs.OutReport(mode, temp, "moneyboxlog", path);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    OutReport(RptMode.Design);
            }
            else
            {
                OutReport(RptMode.Print);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }





    }
}
