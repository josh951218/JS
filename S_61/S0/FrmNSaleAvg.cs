using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.S0
{
    public partial class FrmNSaleAvg : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;

        public FrmNSaleAvg()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            Day.SetDateLength();
            Day1.SetDateLength();
            Day.Text = Date.GetDateTime(Common.User_DateTime);
            Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmNSaleAvg_Load(object sender, EventArgs e)
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
                var Query = " and bracket=@bracket";
                if (machine.TrimTextLenth() > 0)
                    Query += " and seno=@machine";
                if (ShNo.TrimTextLenth() > 0)
                    Query += " and spno=@shno";
                if (EmNo.TrimTextLenth() > 0)
                    Query += " and emno=@emno";

                //總銷費金額
                var tsql = @"
                Select A.sadate,sadate1='',A.ct,B.SalTot,pg=0.0 from (
                    select sadate,COUNT(*)ct      from sale  where sadate >= @sadate and sadate <= @sadate1 " + Query + @" group by sadate
                    union all
                    select sadate,(-1)*COUNT(*)ct from rsale where sadate >= @sadate and sadate <= @sadate1 " + Query + @" group by sadate
                )A
                left join (
                    select sadate,SUM(qty*prs*price)SalTot      from saled  where sadate >= @sadate and sadate <= @sadate1 " + Query + @" group by sadate
                    union all
                    select sadate,(-1)*SUM(qty*prs*price)SalTot from rsaled where sadate >= @sadate and sadate <= @sadate1 " + Query + @" group by sadate
                )B
                on A.sadate = B.sadate";
                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("sadate", Date.ToTWDate(Day.Text));
                    spc.AddWithValue("sadate1", Date.ToTWDate(Day1.Text));
                    spc.AddWithValue("bracket", "前台");
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

            for (int i = 0; i < temp.Rows.Count; i++)
            { 
                temp.Rows[i]["sadate1"] = Date.AddLine(Date.ToUSDate(temp.Rows[i]["sadate"].ToString().Trim()));
                temp.Rows[i]["sadate"] = Date.AddLine(temp.Rows[i]["sadate"].ToString().Trim());

                var count = temp.Rows[i]["ct"].ToDecimal("f0");
                var money = temp.Rows[i]["SalTot"].ToDecimal("f0");

                temp.Rows[i]["ct"] = count;
                temp.Rows[i]["SalTot"] = money;

                if (count != 0)
                {
                    temp.Rows[i]["pg"] = money / count;
                }
            }

            var path = Common.reportaddress + @"ReportF\客戶單價分析表.frx";

            using (var fs = new JBS.FReport())
            {
                fs.dy.Add("DateRange", Day.Text + "～" + Day1.Text);
                fs.dy.Add("機台", machine.Text);
                fs.dy.Add("班別", "[" + ShNo.Text + "]" + ShName.Text);

                fs.OutReport(mode, temp, "Table", path);
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
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }
         
    }
}
